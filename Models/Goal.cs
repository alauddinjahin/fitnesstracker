using _216678_FitnessTracker.Config;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace _216678_FitnessTracker.Models
{
    public class Goal
    {
        public int GoalId { get; set; }
        public int UserId { get; set; }
        public int GoalCalories { get; set; }
        public DateTime SetDate { get; set; }

        private string table = "Goals";

        public Goal() { }

        public Goal(Dictionary<string, object> attributes)
        {
            Fill(attributes);
        }

        // allow only fillable property to make it insertable
        public void Fill(Dictionary<string, object> attributes)
        {
            foreach (var property in typeof(Goal).GetProperties())
            {
                string propName = property.Name;
                if (attributes.ContainsKey(propName))
                {
                    property.SetValue(this, attributes[propName]);
                }
            }
        }

        //Save user goals into database
        public bool Save()
        {
            using (SqlConnection connection = FtDB.GetDbConnection())
            {

                string query = "INSERT INTO [" + table + "] (UserId, GoalCalories";
                if (SetDate != null) query += ", SetDate";
                query += ") OUTPUT INSERTED.GoalId VALUES (@UserId, @GoalCalories";
                if (SetDate != null) query += ", @SetDate";
                query += ")";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@UserId", UserId);
                    command.Parameters.AddWithValue("@GoalCalories", GoalCalories);

                    if (SetDate != null)
                    {
                        command.Parameters.AddWithValue("@SetDate", SetDate);
                    }

                    object result = command.ExecuteScalar(); // Get inserted GoalId
                    if (result != null)
                    {
                        GoalId = Convert.ToInt32(result);
                        return true;
                    }

                    return false;
                }
            }
        }

        public static Goal Find(int goalId)
        {
            using (SqlConnection connection = FtDB.GetDbConnection())
            {
                string query = "SELECT GoalId, UserId, GoalCalories, SetDate FROM [Goals] WHERE GoalId = @GoalId";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@GoalId", goalId);
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            return new Goal
                            {
                                GoalId = reader.GetInt32(0),
                                UserId = reader.GetInt32(1),
                                GoalCalories = reader.GetInt32(2),  
                                SetDate = reader.GetDateTime(3)
                            };
                        }
                    }
                }
            }
            return null;
        }

        public bool Update()
        {
            if (GoalId == 0) return false;

            using (SqlConnection connection = FtDB.GetDbConnection())
            {
                string query = "UPDATE [Goals] SET GoalCalories = @GoalCalories WHERE GoalId = @GoalId";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@GoalCalories", GoalCalories);
                    //command.Parameters.AddWithValue("@SetDate", SetDate);
                    command.Parameters.AddWithValue("@GoalId", GoalId);

                    int rowsAffected = command.ExecuteNonQuery();
                    return rowsAffected > 0;
                }
            }
        }

        public bool Delete(int goalId)
        {
            if (goalId == 0) return false;

            var goal = Find(goalId);

            using (SqlConnection connection = FtDB.GetDbConnection())
            {
                string query = "DELETE FROM [Goals] WHERE GoalId = @GoalId";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@GoalId", goalId);
                    int rowsAffected = command.ExecuteNonQuery();
                    //if(rowsAffected > 0)
                    //{
                    //    var CaloriesBurned = ActivityRecord.FindTotalCaloriesBurnedByUser(goal.UserId);
                    //    if(CaloriesBurned > 0)
                    //    {
                    //        User.UpdateTotalCaloriesBurnedByDecrease(goal.UserId, goal.GoalCalories);
                    //    }
                    //}

                    return rowsAffected > 0;
                }
            }
        }

        //User.UpdateTotalCaloriesBurnedByDecrease();

        public static List<Goal> GetAllGoalsForUser(int userId)
        {
            List<Goal> goals = new List<Goal>();
            using (SqlConnection connection = FtDB.GetDbConnection())
            {
                string query = "SELECT GoalId, UserId, GoalCalories, SetDate FROM [Goals] WHERE UserId = @UserId";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@UserId", userId);
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            goals.Add(new Goal
                            {
                                GoalId = reader.GetInt32(0),
                                UserId = reader.GetInt32(1),
                                GoalCalories = reader.GetInt32(2),
                                SetDate = reader.GetDateTime(3)
                            });
                        }
                    }
                }
            }
            return goals;
        }


        public float GetGoalCalories(int userId)
        {
            using (SqlConnection connection = FtDB.GetDbConnection())
            {
                var query = "SELECT GoalCalories FROM [Goals] WHERE UserId = @UserId";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@UserId", userId);
                    return Convert.ToSingle(command.ExecuteScalar());
                }
            }
        }

        public static float TargtedGoalCalories(int userId)
        {
            using (SqlConnection connection = FtDB.GetDbConnection())
            {
                var query = "SELECT COALESCE(SUM(GoalCalories), 0) as GoalCalories FROM [Goals] WHERE UserId = @UserId";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@UserId", userId);
                    var result = command.ExecuteScalar();
                    if (result == DBNull.Value || result == null)
                    {
                        return 0f;
                    }

                    return Convert.ToSingle(result);
                }
            }
        }

        public static float GetTotalCaloriesBurned(int userId)
        {
            using (SqlConnection connection = FtDB.GetDbConnection())
            {
                var query = "SELECT SUM( COALESCE(CaloriesBurned, 0) ) as TotalCaloriesBurned FROM [ActivityRecords] WHERE UserId = @UserId";
                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@UserId", userId);
                    var result = command.ExecuteScalar();
                    if (result == DBNull.Value || result == null)
                    {
                        return 0f;
                    }

                    return Convert.ToSingle(result);
                }
            }
        }

        public GoalAchievementResult CheckGoalAchievement(int userId)
        {
            float goalCalories = TargtedGoalCalories(userId);
            float totalCaloriesBurned = GetTotalCaloriesBurned(userId);

            // Return an instance of the custom class
            return new GoalAchievementResult
            {
                TotalCaloriesBurned = totalCaloriesBurned,
                SuccessGoalStatus = totalCaloriesBurned == 0 ? "Achievement" : (totalCaloriesBurned >= goalCalories ? "Goal Achieved!" : "Keep Going!")
            };
        }


        





    }
}
