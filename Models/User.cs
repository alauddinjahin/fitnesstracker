using _216678_FitnessTracker.Config;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _216678_FitnessTracker.Models
{

    class User
    {
        public int UserId { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string PasswordHash { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string Gender { get; set; }
        public string PhoneNumber { get; set; }
        public string UserPhoto { get; set; }

        public bool RememberMe { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;

        public float TotalCaloriesBurned { get; set; }

        private static readonly HashSet<string> guarded = new HashSet<string> { "UserId", "CreatedAt" };

        public User() { }

        public User(Dictionary<string, object> attributes)
        {
            Fill(attributes);
        }

        public void Fill(Dictionary<string, object> attributes)
        {
            foreach (var property in typeof(User).GetProperties())
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
                string query = "INSERT INTO [Users] (UserName, Email, PasswordHash, DateOfBirth, Gender, PhoneNumber, RememberMe, CreatedAt) " +
                                "OUTPUT INSERTED.UserId " +
                               "VALUES (@UserName, @Email, @PasswordHash, @DateOfBirth, @Gender, @PhoneNumber, @RememberMe, @CreatedAt)";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@UserName", UserName);
                    command.Parameters.AddWithValue("@Email", Email);
                    command.Parameters.AddWithValue("@PasswordHash", PasswordHash);
                    command.Parameters.AddWithValue("@DateOfBirth", DateOfBirth);
                    command.Parameters.AddWithValue("@Gender", Gender);
                    command.Parameters.AddWithValue("@PhoneNumber", PhoneNumber);
                    command.Parameters.AddWithValue("@RememberMe", RememberMe);
                    command.Parameters.AddWithValue("@CreatedAt", CreatedAt);

                    object result = command.ExecuteScalar(); // Get the inserted UserId
                    connection.Close();
                    if (result != null)
                    {
                        UserId = Convert.ToInt32(result);
                        return true;
                    }

                    return false;

                    //int rowsAffected = command.ExecuteNonQuery();
                    //return rowsAffected > 0;
                }
            }
        }

        public static User Find(int userId)
        {
            using (SqlConnection connection = FtDB.GetDbConnection())
            {
                string query = "SELECT UserId, UserName, Email, DateOfBirth, Gender, PhoneNumber, RememberMe, CreatedAt " +
                               "FROM [Users] WHERE UserId = @UserId";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@UserId", userId);
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            return new User{
                                UserId = reader.GetInt32(0),
                                UserName = reader.GetString(1),
                                Email = reader.GetString(2),
                                DateOfBirth = reader.GetDateTime(3),
                                Gender = reader.GetString(4),
                                PhoneNumber = reader.GetString(5),
                                RememberMe = reader.GetBoolean(6),
                                CreatedAt = reader.GetDateTime(7)
                            };
                        }
                    }
                }
            }
            return null;
        }


        public static bool IsUserExistsByPhoneNumber(string phoneNumber, int UserId = -1)
        {
            using (SqlConnection connection = FtDB.GetDbConnection())
            {
                string query = "SELECT COUNT(*) FROM [Users] WHERE PhoneNumber = @PhoneNumber";
                if(UserId != -1)
                {
                    query += " AND UserId != @UserId";
                }

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@PhoneNumber", phoneNumber);
                    if (UserId != -1)
                    {
                        command.Parameters.AddWithValue("@UserId", UserId);
                    }

                    int count = (int)command.ExecuteScalar();
                    connection.Close();
                    return count > 0; // Returns true if user with this phone number exists
                }
            }
        }


        public static bool IsUserExistsByUserName(string userName, int UserId=-1)
        {
            using (SqlConnection connection = FtDB.GetDbConnection())
            {
                string query = "SELECT COUNT(*) FROM [Users] WHERE UserName = @UserName";
                if (UserId != -1)
                {
                    query += " AND UserId != @UserId";
                }

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@UserName", userName);
                    if (UserId != -1)
                    {
                        command.Parameters.AddWithValue("@UserId", UserId);
                    }

                    int count = (int)command.ExecuteScalar();
                    connection.Close();
                    return count > 0; // Returns true if user with this username exists
                }
            }
        }

        public static User FindUserByUserName(string userName)
        {
            using (SqlConnection connection = FtDB.GetDbConnection())
            {
                string query = "SELECT * FROM [Users] WHERE UserName = @UserName";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@UserName", userName);
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            return new User
                            {
                                UserId = reader.GetInt32(0),
                                UserName = reader.GetString(1),
                                Email = reader.GetString(2),
                                PasswordHash = reader.GetString(3),
                                DateOfBirth = reader.GetDateTime(4),
                                Gender = reader.GetString(5),
                                PhoneNumber = reader.GetString(6),
                                RememberMe = reader.GetBoolean(7),
                                CreatedAt = reader.GetDateTime(8)
                            };
                        }
                    }
                }


                return null;

            }
        }


        public static bool IsUserExistsByEmail(string email, int UserId = -1)
        {
            using (SqlConnection connection = FtDB.GetDbConnection())
            {
                string query = "SELECT COUNT(*) FROM [Users] WHERE Email = @Email";
                if (UserId != -1)
                {
                    query += " AND UserId != @UserId";
                }

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Email", email);

                    if (UserId != -1)
                    {
                        command.Parameters.AddWithValue("@UserId", UserId);
                    }

                    int count = (int)command.ExecuteScalar();
                    connection.Close();
                    return count > 0; // Returns true if user with this email exists
                }
            }
        }

        public static List<User> All()
        {
            List<User> users = new List<User>();
            using (SqlConnection connection = FtDB.GetDbConnection())
            {
                string query = "SELECT UserId, UserName, Email, DateOfBirth, Gender, PhoneNumber, RememberMe, CreatedAt FROM [Users]";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            users.Add(new User
                            {
                                UserId = reader.GetInt32(0),
                                UserName = reader.GetString(1),
                                Email = reader.GetString(2),
                                DateOfBirth = reader.GetDateTime(3),
                                Gender = reader.GetString(4),
                                PhoneNumber = reader.GetString(5),
                                RememberMe = reader.GetBoolean(6),
                                CreatedAt = reader.GetDateTime(7)
                            });
                        }
                    }
                }
            }
            return users;
        }


        public bool Update()
        {
            if (UserId == 0) return false;

            List<string> setClauses = new List<string>();
            List<SqlParameter> parameters = new List<SqlParameter>();

            if (!string.IsNullOrEmpty(UserName))
            {
                setClauses.Add("UserName = @UserName");
                parameters.Add(new SqlParameter("@UserName", UserName));
            }

            if (!string.IsNullOrEmpty(Email))
            {
                setClauses.Add("Email = @Email");
                parameters.Add(new SqlParameter("@Email", Email));
            }

            if (!string.IsNullOrEmpty(PasswordHash))
            {
                setClauses.Add("PasswordHash = @PasswordHash");
                parameters.Add(new SqlParameter("@PasswordHash", PasswordHash));
            }

            //if (DateOfBirth != null)
            //{
            //    setClauses.Add("DateOfBirth = @DateOfBirth");
            //    parameters.Add(new SqlParameter("@DateOfBirth", DateOfBirth));
            //}

            if (!string.IsNullOrEmpty(Gender))
            {
                setClauses.Add("Gender = @Gender");
                parameters.Add(new SqlParameter("@Gender", Gender));
            }

            if (!string.IsNullOrEmpty(PhoneNumber))
            {
                setClauses.Add("PhoneNumber = @PhoneNumber");
                parameters.Add(new SqlParameter("@PhoneNumber", PhoneNumber));
            }

            if (!string.IsNullOrEmpty(UserPhoto))
            {
                setClauses.Add("UserPhoto = @UserPhoto");
                parameters.Add(new SqlParameter("@UserPhoto", UserPhoto));
            }

            //if (UserId > 0)
            //{
            //    setClauses.Add("UserId = @UserId");
            //    parameters.Add(new SqlParameter("@UserId", UserId));
            //}

            //Console.WriteLine(RememberMe + "RememberMe");
            //Console.WriteLine(RememberMe.GetType());

            if (RememberMe != null)
            {
                setClauses.Add("RememberMe = @RememberMe");
                parameters.Add(new SqlParameter("@RememberMe", RememberMe));
            }

            if (setClauses.Count == 0) return false; // No fields to update

          
            string query = "UPDATE [Users] SET " + string.Join(", ", setClauses) + " WHERE UserId = @UserId";

            parameters.Add(new SqlParameter("@UserId", UserId));

            using (SqlConnection connection = FtDB.GetDbConnection())
            {
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddRange(parameters.ToArray());
                    int rowsAffected = command.ExecuteNonQuery();
                    return rowsAffected > 0;
                }
            }
        }


        public bool Delete(int userId)
        {
            if (userId == 0) return false;

            using (SqlConnection connection = FtDB.GetDbConnection())
            {
                string query = "DELETE FROM [Users] WHERE UserId = @UserId";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@UserId", userId);

                    int rowsAffected = command.ExecuteNonQuery();
                    // Goals and ActivityRecords table will be deleted further
                    return rowsAffected > 0;
                }
            }
        }



        public static bool UpdateTotalCaloriesBurned(int userId, float caloriesBurned)
        {
            using (SqlConnection connection = FtDB.GetDbConnection())
            {
                var query = "UPDATE Users SET TotalCaloriesBurned = COALESCE(TotalCaloriesBurned, 0) + @CaloriesBurned WHERE UserId = @UserId";
                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@CaloriesBurned", caloriesBurned);
                    command.Parameters.AddWithValue("@UserId", userId);
                    int rowsAffected = command.ExecuteNonQuery();
                    return rowsAffected > 0;
                }
            }
        }


        public static bool UpdateTotalCaloriesBurnedByDecrease(int userId, float caloriesBurned)
        {
            using (SqlConnection connection = FtDB.GetDbConnection())
            {
                var query = "UPDATE Users SET TotalCaloriesBurned = COALESCE(TotalCaloriesBurned, 0) - @CaloriesBurned WHERE UserId = @UserId";
                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@CaloriesBurned", caloriesBurned);
                    command.Parameters.AddWithValue("@UserId", userId);
                    int rowsAffected = command.ExecuteNonQuery();
                    return rowsAffected > 0;
                }
            }
        }


        public float GetTotalCaloriesBurned(int userId)
        {
            using (SqlConnection connection = FtDB.GetDbConnection())
            {
                var query = "SELECT TotalCaloriesBurned FROM Users WHERE UserId = @UserId";
                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@UserId", userId);
                    return Convert.ToSingle(command.ExecuteScalar());
                }
            }
        }


        public static float GetTotalCaloriesBurnedByUserId(int userId)
        {
            using (SqlConnection connection = FtDB.GetDbConnection())
            {
                var query = "SELECT TotalCaloriesBurned FROM Users WHERE UserId = @UserId";
                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@UserId", userId);
                    var result = command.ExecuteScalar();

                    if (result == null || result == DBNull.Value)
                    {
                        return 0; 
                    }

                    return Convert.ToSingle(result);
                }
            }
        }


        public static int CountUser()
        {
            using (SqlConnection connection = FtDB.GetDbConnection())
            {
                var query = "SELECT COUNT(*) FROM Users";
                using (var command = new SqlCommand(query, connection))
                {
                    return Convert.ToInt32(command.ExecuteScalar());
                }
            }
        }


        public static int CountTotalActivities()
        {
            using (SqlConnection connection = FtDB.GetDbConnection())
            {
                var query = "SELECT COUNT(*) FROM FtActivities";
                using (var command = new SqlCommand(query, connection))
                {

                    return Convert.ToInt32(command.ExecuteScalar());
                }
            }
        }



    }

}

