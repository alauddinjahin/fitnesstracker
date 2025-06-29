using _216678_FitnessTracker.Models;
using _216678_FitnessTracker.Screens;
using _216678_FitnessTracker.Utils;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace _216678_FitnessTracker
{
    public partial class frmRegister: Form
    {

        public string UserName { get; set; }
        public string Email { get; set; }

        public string Password { get; set; }
        public string ConfirmPassword { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string PhoneNumber { get; set; }
        public string Gender { get; set; }




        public frmRegister()
        {
            InitializeComponent();
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MaximumSize = this.Size;
            this.MinimumSize = this.Size;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            RenderCmbGenderList();
        }

        private void RenderCmbGenderList()
        {
            cmbGender.Items.Add("Male");
            cmbGender.Items.Add("Female");
            //cmbGender.Items.Add("Others");
            cmbGender.SelectedIndex = 0; // Default
        }

        private void lblLinkForLogin_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            frmLogin loginForm = new frmLogin();
            loginForm.Show();
            this.Hide();
        }

        private void btnRegister_Click(object sender, EventArgs e)
        {
            
            string ErrorForUserName = FtFormValidator.ValidateUserName(UserName);
            if (!string.IsNullOrEmpty(ErrorForUserName))
            {
                MessageBox.Show(ErrorForUserName, "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string ErrorForEmail = FtFormValidator.ValidateEmail(Email);
            if (!string.IsNullOrEmpty(ErrorForEmail))
            {
                MessageBox.Show(ErrorForEmail, "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string ErrorForPhoneNumber = FtFormValidator.ValidatePhoneNumber(PhoneNumber);
            if (!string.IsNullOrEmpty(ErrorForPhoneNumber))
            {
                MessageBox.Show(ErrorForPhoneNumber, "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string ErrorForDateOfBirth = FtFormValidator.ValidateDateOfBirth(dtpDateOfBirth.Value.ToString());
            if (!string.IsNullOrEmpty(ErrorForDateOfBirth))
            {
                MessageBox.Show(ErrorForDateOfBirth, "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }


            if (LeavePasswordError())
            {
                return;
            }


            User user = new User
            {
                UserName = UserName,
                Email = Email,
                PasswordHash = FtAuth.GenerateHashPassword(Password),
                DateOfBirth = dtpDateOfBirth.Value,
                PhoneNumber = PhoneNumber,
                Gender = Gender,
                RememberMe = false
            };

            if (user.Save())
            {
                MessageBox.Show(
                    "User registered successfully!",
                    "Success",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information
                );

                SessionManager.SaveSession(user);

                ClearForm();

                frmDashboard dashboardForm = new frmDashboard();
                dashboardForm.Show();
                this.Hide();
            }
            
        }

        private void ClearForm()
        {
            txtUserName.Clear();
            txtEmail.Clear();
            txtPassword.Clear();
            txtConfirmPassword.Clear();
            txtPhoneNumber.Clear();
            dtpDateOfBirth.Value = DateTime.Now;
            txtPhoneNumber.Clear();
            cmbGender.SelectedIndex = 0;
        }




        private void txtPassword_TextChanged(object sender, EventArgs e)
        {
            Password = txtPassword.Text.Trim();
            //LeavePasswordError();
        }
        
        private void txtConfirmPassword_TextChanged(object sender, EventArgs e)
        {
            ConfirmPassword = txtConfirmPassword.Text.Trim();

            //LeavePasswordError();
        }

        
        private bool LeavePasswordError()
        {
            bool IsInvalid = false;
            //Console.WriteLine(ConfirmPassword);
            if (!string.IsNullOrEmpty(Password) && !string.IsNullOrEmpty(ConfirmPassword))
            {
                string Error = FtFormValidator.ValidatePassword(Password, ConfirmPassword);
                if (!string.IsNullOrEmpty(Error))
                {
                    MessageBox.Show(Error, "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    IsInvalid = true;
                }
            }
            else
            {
                if (string.IsNullOrEmpty(Password))
                {
                    MessageBox.Show("Password is required!", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    IsInvalid = true;
                }

                if (string.IsNullOrEmpty(ConfirmPassword))
                {
                    MessageBox.Show("ConfirmPassword is required!", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    IsInvalid = true;
                }
            }

             return IsInvalid;
        }

        private void txtEmail_TextChanged(object sender, EventArgs e)
        {
            Email = txtEmail.Text.Trim();
        }

        private void txtPhoneNumber_TextChanged(object sender, EventArgs e)
        {
            PhoneNumber = txtPhoneNumber.Text.Trim();
        }

        private void dtpDateOfBirth_ValueChanged(object sender, EventArgs e)
        {
            DateOfBirth = dtpDateOfBirth.Value;
        }

        private void cmbGender_SelectedIndexChanged(object sender, EventArgs e)
        {
            Gender = cmbGender.Text;
        }

        private void txtUserName_TextChanged(object sender, EventArgs e)
        {
            UserName = txtUserName.Text.Trim();
        }
    }
}
