using _216678_FitnessTracker.Config;
using _216678_FitnessTracker.Utils;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _216678_FitnessTracker.Models
{
    class ActivityRecord
    {
        public int RecordId { get; set; }
        public int ActivityId { get; set; }
        public int UserId { get; set; }
        public string ActivityType { get; set; }
        public float Metric1 { get; set; }
        public float Metric2 { get; set; }
        public float Metric3 { get; set; }
        public float CaloriesBurned { get; set; }
        public DateTime CreatedAt { get; set; }


        private static readonly HashSet<string> guarded = new HashSet<string> { "RecordId", "CreatedAt" };

        public ActivityRecord() { }

        public ActivityRecord(Dictionary<string, object> attributes)
        {
            Fill(attributes);
        }

        public void Fill(Dictionary<string, object> attributes)
        {
            foreach (var property in typeof(ActivityRecord).GetProperties())
            {
                string propName = property.Name;
                if (!guarded.Contains(propName) && attributes.ContainsKey(propName))
                {
                    property.SetValue(this, attributes[propName]);
                }
            }
        }


        public static List<ActivityRecord> GetAll(int userId)
        {
            List<ActivityRecord> activities = new List<ActivityRecord>();

            try
            {
                using (SqlConnection connection = FtDB.GetDbConnection())
                {
                    string query = @"
                                    SELECT 
                                        ar.RecordId, 
                                        ar.ActivityId, 
                                        ar.UserId, 
                                        ar.Metric1, 
                                        ar.Metric2, 
                                        ar.Metric3, 
                                        ar.CaloriesBurned, 
                                        ar.CreatedAt, 
                                        fa.ActivityName AS ActivityType 
                                    FROM 
                                        ActivityRecords ar 
                                    INNER JOIN 
                                        FtActivities fa ON ar.ActivityId = fa.ActivityId 
                                    WHERE 
                                        ar.UserId = @UserId";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@UserId", userId);
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                try
                                {

                                    activities.Add(new ActivityRecord
                                    {
                                        RecordId = reader.GetInt32(0), 
                                        ActivityId = reader.GetInt32(1),
                                        UserId = reader.GetInt32(2), 
                                        Metric1 = reader.IsDBNull(3) ? 0f : Convert.ToSingle(reader[3]), 
                                        Metric2 = reader.IsDBNull(4) ? 0f : Convert.ToSingle(reader[4]), 
                                        Metric3 = reader.IsDBNull(5) ? 0f : Convert.ToSingle(reader[5]), 
                                        CaloriesBurned = reader.IsDBNull(6) ? 0f : Convert.ToSingle(reader[6]),
                                        CreatedAt = reader.IsDBNull(7) ? DateTime.MinValue : Convert.ToDateTime(reader[7]),
                                        ActivityType = reader.IsDBNull(8) ? string.Empty : reader.GetString(8)
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


        private bool AddActivityRecord(ActivityRecord record)
        {
            using (SqlConnection connection = FtDB.GetDbConnection())
            {

                var columns = new List<string> { "UserId", "ActivityId", "Metric1", "Metric2", "Metric3", "CaloriesBurned" };
                var values = new List<string> { "@UserId", "@ActivityId", "@Metric1", "@Metric2", "@Metric3", "@CaloriesBurned" };
                if (record.CreatedAt != DateTime.MinValue) 
                {
                    columns.Add("CreatedAt");
                    values.Add("@CreatedAt");
                }

                string query = $@"INSERT INTO ActivityRecords ({string.Join(", ", columns)}) VALUES ({string.Join(", ", values)})";

                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@UserId", record.UserId);
                    command.Parameters.AddWithValue("@ActivityId", record.ActivityId);
                    command.Parameters.AddWithValue("@Metric1", record.Metric1);
                    command.Parameters.AddWithValue("@Metric2", record.Metric2);
                    command.Parameters.AddWithValue("@Metric3", record.Metric3);
                    command.Parameters.AddWithValue("@CaloriesBurned", record.CaloriesBurned);
                    //command.ExecuteNonQuery();

                    if (record.CreatedAt != DateTime.MinValue)
                    {
                        command.Parameters.AddWithValue("@CreatedAt", record.CreatedAt);
                    }

                    int rowsAffected = command.ExecuteNonQuery();
                    return rowsAffected > 0;
                }
            }
        }


        private float CaloriesBurnedFormulaResult(ActivityRecord record)
        {
            float CaloriesBurned = 0;

            switch (record.ActivityId)
            {
                case 1: // Walking
                    CaloriesBurned = (record.Metric1 * 0.01f) + (record.Metric2 * 0.5f) + (record.Metric3 * 0.3f);
                    break;

                case 2: // Swimming
                    CaloriesBurned = (record.Metric1 * 1.2f) + (record.Metric2 * 0.6f) + (record.Metric3 * 0.05f);
                    break;

                case 3: // Running
                    CaloriesBurned = (record.Metric1 * 1.0f) + (record.Metric2 * 0.4f) + (record.Metric3 * 0.05f);
                    break;

                case 4: // Cycling
                    CaloriesBurned = (record.Metric1 * 0.9f) + (record.Metric2 * 0.3f) + (record.Metric3 * 0.5f);
                    break;

                case 5: // Jump Rope
                    CaloriesBurned = (record.Metric1 * 0.1f) + (record.Metric2 * 0.5f) + (record.Metric3 * 0.05f);
                    break;

                case 6: // Weightlifting
                    CaloriesBurned = (record.Metric1 * 0.5f) + (record.Metric2 * 0.8f) + (record.Metric3 * 0.3f);
                    break;

                default:
                    CaloriesBurned = (record.Metric1 * 0.4f) + (record.Metric2 * 0.7f) + (record.Metric3 * 0.3f);
                    break;
            }

            return CaloriesBurned;
        }


        public bool Save(ActivityRecord record)
        {

            record.CaloriesBurned = CalorieBurned.CaloriesBurnedCustomFormulaWithMET(record, 70);
            //record.CaloriesBurned = CaloriesBurnedFormulaResult(record);

            // Save the activity record
            if (this.AddActivityRecord(record))
            {
                User.UpdateTotalCaloriesBurned(record.UserId, record.CaloriesBurned);
                return true;
            }

            return false;

        }




        public bool Update(ActivityRecord record)
        {
            record.CaloriesBurned = CaloriesBurnedFormulaResult(record);

            using (SqlConnection connection = FtDB.GetDbConnection())
            {
                string query = @"
                                UPDATE [ActivityRecords] 
                                SET 
                                    Metric1 = @Metric1, 
                                    Metric2 = @Metric2, 
                                    Metric3 = @Metric3, 
                                    CaloriesBurned = @CaloriesBurned 
                                " + (
                                record.CreatedAt != DateTime.MinValue ? (", CreatedAt = @CreatedAt") : ""
                                ) + " WHERE RecordId = @RecordId";

               

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Metric1", record.Metric1);
                    command.Parameters.AddWithValue("@Metric2", record.Metric2);
                    command.Parameters.AddWithValue("@Metric3", record.Metric3);
                    command.Parameters.AddWithValue("@CaloriesBurned", record.CaloriesBurned);
                    command.Parameters.AddWithValue("@RecordId", record.RecordId);

                    if (record.CreatedAt != DateTime.MinValue)
                    {
                        command.Parameters.AddWithValue("@CreatedAt", record.CreatedAt);
                    }

                    int rowsAffected = command.ExecuteNonQuery();

                    if (rowsAffected > 0)
                    {
                        float oldCaloriesBurned = GetCaloriesBurnedById(record.RecordId);
                        float caloriesDifference = record.CaloriesBurned - oldCaloriesBurned;

                        User.UpdateTotalCaloriesBurned(record.UserId, caloriesDifference);

                        return true;
                    }
                }
            }

            return false;
        }

        private float GetCaloriesBurnedById(int recordId)
        {
            using (SqlConnection connection = FtDB.GetDbConnection())
            {
                string query = "SELECT CaloriesBurned FROM [ActivityRecords] WHERE RecordId = @RecordId";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@RecordId", recordId);
                    object result = command.ExecuteScalar();
                    return result == null ? 0f : Convert.ToSingle(result);
                }
            }
        }





        public static ActivityRecord Find(int recordId)
        {
            using (SqlConnection connection = FtDB.GetDbConnection())
            {
                string query = @"
                                SELECT 
                                    ar.RecordId, 
                                    ar.ActivityId, 
                                    ar.UserId, 
                                    ar.Metric1, 
                                    ar.Metric2, 
                                    ar.Metric3, 
                                    ar.CaloriesBurned, 
                                    ar.CreatedAt, 
                                    fa.ActivityName AS ActivityType 
                                FROM 
                                    ActivityRecords ar 
                                INNER JOIN 
                                    FtActivities fa ON ar.ActivityId = fa.ActivityId 
                                WHERE 
                                    ar.RecordId = @RecordId";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@RecordId", recordId);
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            return new ActivityRecord
                            {
                                RecordId = reader.GetInt32(0),
                                ActivityId = reader.GetInt32(1),
                                UserId = reader.GetInt32(2),
                                Metric1 = reader.IsDBNull(3) ? 0f : Convert.ToSingle(reader[3]),
                                Metric2 = reader.IsDBNull(4) ? 0f : Convert.ToSingle(reader[4]),
                                Metric3 = reader.IsDBNull(5) ? 0f : Convert.ToSingle(reader[5]),
                                CaloriesBurned = reader.IsDBNull(6) ? 0f : Convert.ToSingle(reader[6]),
                                CreatedAt = reader.IsDBNull(7) ? DateTime.MinValue : Convert.ToDateTime(reader[7]),
                                ActivityType = reader.IsDBNull(8) ? string.Empty : reader.GetString(8)
                            };
                        }
                    }
                }
            }
            return null;
        }



        public bool Delete(int recordId)
        {
            if (recordId == 0) return false;


            ActivityRecord GetRecord = Find(recordId);
            if (GetRecord == null) return false;

            using (SqlConnection connection = FtDB.GetDbConnection())
            {
                string query = "DELETE FROM [ActivityRecords] WHERE RecordId = @RecordId";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@RecordId", recordId);
                    int rowsAffected = command.ExecuteNonQuery();

                    if (rowsAffected > 0)
                    {
                        float oldCaloriesBurned = GetRecord.CaloriesBurned;
                        User.UpdateTotalCaloriesBurnedByDecrease(GetRecord.UserId, oldCaloriesBurned);
                        return true;
                    }

                    return rowsAffected > 0;
                }
            }
        }


        public static float FindTotalCaloriesBurnedByUser(int userId)
        {
            using (SqlConnection connection = FtDB.GetDbConnection())
            {
                string query = @"
                                SELECT 
                                    SUM(COALESCE(ar.CaloriesBurned, 0)) as CaloriesBurned
                                FROM 
                                    ActivityRecords ar 
                                INNER JOIN 
                                    FtActivities fa ON ar.ActivityId = fa.ActivityId 
                                WHERE 
                                    ar.UserId = @UserId";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@UserId", userId);
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        return Convert.ToSingle(command.ExecuteScalar());
                    }
                }
            }
        }









    }
}
