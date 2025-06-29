using _216678_FitnessTracker.Config;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ListView;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;

namespace _216678_FitnessTracker.Models
{
    class FtActivity
    {
        public int ActivityId { get; set; }
        public string ActivityName { get; set; }
        public string Metric1 { get; set; }
        public string Metric2 { get; set; }
        public string Metric3 { get; set; }

        private static readonly HashSet<string> guarded = new HashSet<string> { "ActivityId" };

        public FtActivity() { }

        public FtActivity(Dictionary<string, object> attributes)
        {
            Fill(attributes);
        }

        public void Fill(Dictionary<string, object> attributes)
        {
            foreach (var property in typeof(FtActivity).GetProperties())
            {
                string propName = property.Name;
                if (!guarded.Contains(propName) && attributes.ContainsKey(propName))
                {
                    property.SetValue(this, attributes[propName]);
                }
            }
        }


        public bool Save()
        {
            using (SqlConnection connection = FtDB.GetDbConnection())
            {
                string query = @"INSERT INTO [FtActivities] (ActivityName)
                                OUTPUT INSERTED.ActivityId
                                VALUES (@ActivityName)";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@ActivityName", ActivityName);

                    object result = command.ExecuteScalar();
                    
                    if (result != null)
                    {
                        ActivityId = Convert.ToInt32(result);

                        connection.Close();
                        return true;
                    }

                    connection.Close();
                    return false;

                }
            }
        }


        public static FtActivity Find(int ActivityId)
        {
            using (SqlConnection connection = FtDB.GetDbConnection())
            {
                string query = "SELECT ActivityId, ActivityName FROM [FtActivities] WHERE ActivityId = @ActivityId";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@ActivityId", ActivityId);
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            return new FtActivity
                            {
                                ActivityId = reader.GetInt32(0),
                                ActivityName = reader.GetString(1)
                            };
                        }
                    }
                }
            }
            return null;
        }


        public static FtActivity FindWithMetrics(int ActivityId)
        {

            try
            {
                using (SqlConnection connection = FtDB.GetDbConnection())
                {
                    string query = @"
                    SELECT 
                        a.ActivityId, 
                        a.ActivityName,
                        MAX(CASE WHEN rn = 1 THEN am.MetricName END) AS Metric1,
                        MAX(CASE WHEN rn = 2 THEN am.MetricName END) AS Metric2,
                        MAX(CASE WHEN rn = 3 THEN am.MetricName END) AS Metric3
                    FROM FtActivities a
                    LEFT JOIN
                        (SELECT ActivityId, MetricName,
                                ROW_NUMBER() OVER(PARTITION BY ActivityId ORDER BY MetricId) AS rn
                         FROM ActivityMetrics) am
                    ON a.ActivityId = am.ActivityId
                    WHERE a.ActivityId = @ActivityId
                    GROUP BY a.ActivityId, a.ActivityName
                    ORDER BY a.ActivityId;";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@ActivityId", ActivityId);

                        using (SqlDataReader reader = command.ExecuteReader())
                        {

                            if (reader.Read())
                            {
                                return new FtActivity
                                {
                                    ActivityId = reader.GetInt32(0),
                                    ActivityName = reader.GetString(1),
                                    Metric1 = reader.IsDBNull(2) ? null : reader.GetString(2),
                                    Metric2 = reader.IsDBNull(3) ? null : reader.GetString(3),
                                    Metric3 = reader.IsDBNull(4) ? null : reader.GetString(4)
                                };
                            }

                        }
                    }
                }

                return null;
            }
            catch (SqlException ex)
            {
                Console.WriteLine($"Database error: {ex.Message}");
                return null;
            }
        }




        public static List<FtActivity> GetAll()
        {
            List<FtActivity> activities = new List<FtActivity>();

            try
            {
                using (SqlConnection connection = FtDB.GetDbConnection())
                {
                    string query = "SELECT ActivityId, ActivityName FROM [FtActivities]";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                try
                                {

                                    activities.Add(new FtActivity
                                    {
                                        ActivityId = reader.GetInt32(0),
                                        ActivityName = reader.GetString(1)
                                    });

                                }
                                catch (InvalidCastException ex)
                                {
                                    Console.WriteLine($"Data conversion error: {ex.Message}");
                                }
                            }
                        }
                    }
                }

                return activities;
            }
            catch (SqlException ex)
            {
                Console.WriteLine($"Database error: {ex.Message}");
                return activities;
            }
        }


        public static List<FtActivity> GetAllWithMetrics()
        {
            List<FtActivity> activities = new List<FtActivity>();

            try
            {
                using (SqlConnection connection = FtDB.GetDbConnection())
                {
                    string query = @"
                SELECT 
                    a.ActivityId, 
                    a.ActivityName,
                    MAX(CASE WHEN rn = 1 THEN am.MetricName END) AS Metric1,
                    MAX(CASE WHEN rn = 2 THEN am.MetricName END) AS Metric2,
                    MAX(CASE WHEN rn = 3 THEN am.MetricName END) AS Metric3
                FROM FtActivities a
                LEFT JOIN
                    (SELECT ActivityId, MetricName,
                            ROW_NUMBER() OVER(PARTITION BY ActivityId ORDER BY MetricId) AS rn
                     FROM ActivityMetrics) am
                ON a.ActivityId = am.ActivityId
                GROUP BY a.ActivityId, a.ActivityName
                ORDER BY a.ActivityId;";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {

                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                try
                                {
                                    activities.Add(new FtActivity
                                    {
                                        ActivityId = reader.GetInt32(0),
                                        ActivityName = reader.GetString(1),
                                        Metric1 = reader.IsDBNull(2) ? null : reader.GetString(2),
                                        Metric2 = reader.IsDBNull(3) ? null : reader.GetString(3),
                                        Metric3 = reader.IsDBNull(4) ? null : reader.GetString(4)
                                    });
                                }
                                catch (InvalidCastException ex)
                                {
                                    Console.WriteLine($"Data conversion error: {ex.Message}");
                                }
                            }
                        }
                    }
                }

                return activities;
            }
            catch (SqlException ex)
            {
                Console.WriteLine($"Database error: {ex.Message}");
                return activities;
            }
        }





    }
}
