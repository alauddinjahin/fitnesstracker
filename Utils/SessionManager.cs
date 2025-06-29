using _216678_FitnessTracker.Models;
using System;
using System.IO;

namespace _216678_FitnessTracker.Utils
{
    class SessionManager
    {
        private static readonly string folderPath = Path.Combine(Directory.GetCurrentDirectory(), "sessions");
        private static readonly string sessionFilePath = Path.Combine(folderPath, "session.txt");
        private static User currentUser;


        public static bool IsSessionActive()
        {
            return LoadSession() != null;

            //User user = LoadSession();
            //return user != null && (user.RememberMe == true);
        }

        public static void SaveSession(User user)
        {
            try
            {
                if (!Directory.Exists(folderPath))
                {
                    Directory.CreateDirectory(folderPath);
                }

                string[] sessionData = {
                    $"UserId={user.UserId}",
                    $"UserName={user.UserName}",
                    $"Email={user.Email}",
                    $"DateOfBirth={user.DateOfBirth:yyyy-MM-dd}",
                    $"Gender={user.Gender}",
                    $"PhoneNumber={user.PhoneNumber}",
                    $"CreatedAt={user.CreatedAt:yyyy-MM-dd HH:mm:ss}",
                    $"UserPhoto={user.UserPhoto ?? ""}"

                };

                File.WriteAllLines(sessionFilePath, sessionData);
                Console.WriteLine("Session saved successfully.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error saving session: {ex.Message}");
            }
        }

        public static User LoadSession()
        {
            try
            {
                if (File.Exists(sessionFilePath))
                {
                    string[] lines = File.ReadAllLines(sessionFilePath);
                    User user = new User();

                    foreach (string line in lines)
                    {
                        string[] parts = line.Split('=');
                        if (parts.Length == 2)
                        {
                            switch (parts[0])
                            {
                                case "UserId":
                                    user.UserId = int.Parse(parts[1]);
                                    break;
                                case "UserName":
                                    user.UserName = parts[1];
                                    break;
                                case "Email":
                                    user.Email = parts[1];
                                    break;
                                case "DateOfBirth":
                                    user.DateOfBirth = DateTime.Parse(parts[1]);
                                    break;
                                case "Gender":
                                    user.Gender = parts[1];
                                    break;
                                case "PhoneNumber":
                                    user.PhoneNumber = parts[1];
                                    break;
                                case "CreatedAt":
                                    user.CreatedAt = DateTime.Parse(parts[1]);
                                    break;
                                case "UserPhoto":
                                    user.UserPhoto = parts[1];
                                    break;
                            }
                        }
                    }

                    return user;
                }
                else
                {
                    Console.WriteLine("Session file not found.");
                    return null;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error loading session: {ex.Message}");
            }

            return null;
        }

        public static void ClearSession()
        {
            try
            {
                if (File.Exists(sessionFilePath))
                {
                    File.Delete(sessionFilePath);
                    Console.WriteLine("Session cleared successfully.");
                }
                else
                {
                    Console.WriteLine("No session file to clear.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error clearing session: {ex.Message}");
            }
        }
    }
}
