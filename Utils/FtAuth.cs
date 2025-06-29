using _216678_FitnessTracker.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace _216678_FitnessTracker.Utils
{
    class FtAuth
    {
        public static string GenerateHashPassword(string password)
        {
            using (SHA256 sha256 = SHA256.Create())
            {
                byte[] passwordBytes = Encoding.UTF8.GetBytes(password);
                byte[] hashBytes = sha256.ComputeHash(passwordBytes);

                StringBuilder hashString = new StringBuilder();
                foreach (byte b in hashBytes)
                {
                    hashString.Append(b.ToString("x2"));
                }

                return hashString.ToString();
            }
        }

        public static bool VerifyPassword(string inputPassword, string storedHash)
        {
            string inputPasswordHash = GenerateHashPassword(inputPassword);
            return inputPasswordHash == storedHash;
        }


        public static bool IsAuthenticate()
        {
            return SessionManager.IsSessionActive();
        }

        public static User AuthUser()
        {
            return SessionManager.LoadSession();
        }

        //user.PasswordHash
    }
}
