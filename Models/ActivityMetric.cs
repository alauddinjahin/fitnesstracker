using _216678_FitnessTracker.Config;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _216678_FitnessTracker.Models
{
    class ActivityMetric
    {
        public int MetricId { get; set; }
        public int ActivityId { get; set; }
        public string MetricName { get; set; }


        private static readonly HashSet<string> guarded = new HashSet<string> { "MetricId" };

        public ActivityMetric() { }

        public ActivityMetric(Dictionary<string, object> attributes)
        {
            Fill(attributes);
        }
        // only allow fillable property to insert into database
        public void Fill(Dictionary<string, object> attributes)
        {
            foreach (var property in typeof(ActivityMetric).GetProperties())
            {
                string propName = property.Name;
                if (!guarded.Contains(propName) && attributes.ContainsKey(propName))
                {
                    property.SetValue(this, attributes[propName]);
                }
            }
        }

        // Save metrics into database
        public bool Save()
        {
            using (SqlConnection connection = FtDB.GetDbConnection())
            {
                string query = @"
                    INSERT INTO [ActivityMetrics] (ActivityId, MetricName)
                    VALUES (@ActivityId, @MetricName)";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@ActivityId", ActivityId);
                    command.Parameters.AddWithValue("@MetricName", MetricName);

                    int rowsAffected = command.ExecuteNonQuery();
                    return rowsAffected > 0;
                }
            }
        }

        // get all metric list
        public static List<ActivityMetric> GetAll(int activityId=-1)
        {
            List<ActivityMetric> activityMetrics = new List<ActivityMetric>();
            using (SqlConnection connection = FtDB.GetDbConnection())
            {
                string query = "SELECT MetricId, ActivityId, MetricName FROM [ActivityMetrics]";
                if(activityId > 0)
                {
                    query += " WHERE ActivityId = @ActivityId";
                }

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    if(activityId > 0)
                    {
                        command.Parameters.AddWithValue("@activityId", activityId);
                    }

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            activityMetrics.Add(new ActivityMetric
                            {
                                MetricId = reader.GetInt32(0),
                                ActivityId = reader.GetInt32(1),
                                MetricName = reader.GetString(2)
                            });
                        }
                    }
                }
            }
            return activityMetrics;
        }

        // get single metric record
        public static ActivityMetric Find(int metricId)
        {
            using (SqlConnection connection = FtDB.GetDbConnection())
            {
                string query = "SELECT MetricId, ActivityId, MetricName FROM [ActivityMetrics] WHERE MetricId = @MetricId";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@MetricId", metricId);
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            return new ActivityMetric
                            {
                                MetricId = reader.GetInt32(0),
                                ActivityId = reader.GetInt32(1),
                                MetricName = reader.GetString(2)
                            };
                        }
                    }
                }
            }
            return null;
        }

        // update an existing record of metric
        public bool Update()
        {
            using (SqlConnection connection = FtDB.GetDbConnection())
            {
                string query = @"
                    UPDATE [ActivityMetrics]
                    SET ActivityId = @ActivityId, MetricName = @MetricName
                    WHERE MetricId = @MetricId";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@MetricId", MetricId);
                    command.Parameters.AddWithValue("@ActivityId", ActivityId);
                    command.Parameters.AddWithValue("@MetricName", MetricName);

                    int rowsAffected = command.ExecuteNonQuery();
                    return rowsAffected > 0;
                }
            }
        }


        // delete metric item that exists inside database
        public static bool Delete(int metricId)
        {
            using (SqlConnection connection = FtDB.GetDbConnection())
            {
                string query = "DELETE FROM [ActivityMetrics] WHERE MetricId = @MetricId";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@MetricId", metricId);

                    int rowsAffected = command.ExecuteNonQuery();
                    return rowsAffected > 0;
                }
            }
        }












    }
}
