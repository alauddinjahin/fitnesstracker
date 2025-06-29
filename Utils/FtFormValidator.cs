using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;
using System.Windows.Forms;
using _216678_FitnessTracker.Models;
using System.Runtime.CompilerServices;

namespace _216678_FitnessTracker.Utils
{
    class FtFormValidator
    {
        public static string ValidateUserName(string fieldValue, bool IsLogIn=false)
        {
            string ValidationMsg = "";

            if (string.IsNullOrWhiteSpace(fieldValue))
            {
                ValidationMsg = "Please enter a Username.";
            }
            else if (!IsLogIn && !Regex.IsMatch(fieldValue, @"^[a-zA-Z0-9]+$"))
            {
                ValidationMsg = ("Username can only contain letters and numbers.");
            }
            else if (!IsLogIn && User.IsUserExistsByUserName(fieldValue))
            {
                ValidationMsg = $"The Username '{fieldValue}' already been taken.";
            }


            return ValidationMsg;
        }


        public static bool CheckUniqueUserByName(string fieldValue, int UserId = -1)
        {
            return User.IsUserExistsByUserName(fieldValue, UserId);
        }

        public static bool CheckUniqueUserByPhone(string fieldValue, int UserId = -1)
        {
            return User.IsUserExistsByPhoneNumber(fieldValue, UserId);
        }

        public static bool CheckUniqueUserByEmail(string fieldValue, int UserId = -1)
        {
            return User.IsUserExistsByEmail(fieldValue, UserId);
        }

        public static string ValidateFloat(string fieldValue)
        {
            string ValidationMsg = "";

            string pattern = @"^[-+]?\d*\.?\d+$";

            if (!Regex.IsMatch(fieldValue, pattern))
            {
                ValidationMsg = ("Please input valid number.");
            }

            return ValidationMsg;
        }


        public static string ValidateInteger(string fieldValue)
        {
            string ValidationMsg = "";

            string pattern = @"^[-+]?\d+$";

            if (!Regex.IsMatch(fieldValue, pattern))
            {
                ValidationMsg = ("Please input valid integer number.");
            }

            return ValidationMsg;
        }

        public static string ValidatePhoneNumber(string fieldValue)
        {
            string ValidationMsg = "";

            if (string.IsNullOrWhiteSpace(fieldValue))
            {
                ValidationMsg = "Please enter a PhoneNumber";
            }
            else if (User.IsUserExistsByPhoneNumber(fieldValue))
            {
                ValidationMsg = $"The PhoneNumber '{fieldValue}' already been taken.";
            }

            return ValidationMsg;
        }

        public static string ValidateDateOfBirth(string fieldValue)
        {
            string ValidationMsg = "";

            if (string.IsNullOrWhiteSpace(fieldValue))
            {
                ValidationMsg = "Please select date of birth.";
            }

            return ValidationMsg;
        }

        public static string ValidateEmail(string fieldValue)
        {
            string ValidationMsg = "";

            if (string.IsNullOrWhiteSpace(fieldValue) || !IsValidEmail(fieldValue))
            {
                ValidationMsg = "Please enter a valid email address.";
            }
            else if(User.IsUserExistsByEmail(fieldValue))
            {
                ValidationMsg = $"The Email '{fieldValue}' already been taken.";
            }


            return ValidationMsg;

        }


        public static bool IsValidEmail(string email)
        {
            try
            {
                var addr = new System.Net.Mail.MailAddress(email);
                return addr.Address == email;
            }
            catch
            {
                return false;
            }
        }



        public static string ValidatePassword(string Password, string ConfirmPassword=null, bool lenthChecker=true)
        {
            string ValidationMsg = "";

            if (string.IsNullOrWhiteSpace(Password))
            {
                ValidationMsg = "Please enter a password.";
            }

            else if (lenthChecker && Password.Length != 12)
            {
                ValidationMsg = ("Password must be exactly 12 characters long.");
            }
            else if (!Regex.IsMatch(Password, @"[a-z]") || !Regex.IsMatch(Password, @"[A-Z]"))  
            {
                ValidationMsg = ("Password must contain at least one lowercase and one uppercase letter.");
            }
            
            else if (Password != ConfirmPassword && ConfirmPassword != null)
            {
                ValidationMsg = ("Passwords do not match.");
            }


            return ValidationMsg;
        }
    }
}
