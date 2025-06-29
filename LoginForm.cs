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
    public partial class frmLogin: Form
    {
        private string UserName = null;
        private string Password = null;
        private bool RememberMe = false;
        private int attempts = 0;
        private DateTime? blockTime = null;
        private Timer unblockTimer = new Timer();
        private int MinsToBlock = 1;
        public frmLogin()
        {
            InitializeComponent();
            unblockTimer.Interval = 1000; // 1 second interval
            unblockTimer.Tick += UnblockTimer_Tick;


            //set the form size (width and height) to be fixed
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MaximumSize = this.Size;  
            this.MinimumSize = this.Size;

        }

        private void UnblockTimer_Tick(object sender, EventArgs e)
        {
            // Check if 15 minutes have passed
            Console.WriteLine("Clock Triggering");

            if (blockTime.HasValue && DateTime.Now >= blockTime.Value.AddMinutes(MinsToBlock))
            {
                Console.WriteLine("Clock");
                // Enable login again
                attempts = 0; 
                blockTime = null; 
                btnLogin.Enabled = true;
                btnLogin.BackColor = Color.Brown; 
                txtUserName.Enabled = true;
                txtPassword.Enabled = true;
                lblLinkForRegister.Enabled = true;
                unblockTimer.Stop(); 
            }
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            Console.WriteLine(chkRememberMe.Checked);
            RememberMe = chkRememberMe.Checked;
        }

        private void frmLogin_Load(object sender, EventArgs e)
        {
            
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            if (blockTime.HasValue && DateTime.Now < blockTime.Value.AddMinutes(MinsToBlock))
            {
                MessageBox.Show("You are blocked. Please try again after 15 minutes.");
                return;
            }

            string ErrorForUserName = FtFormValidator.ValidateUserName(UserName, true);
            if (!string.IsNullOrEmpty(ErrorForUserName))
            {
                MessageBox.Show(ErrorForUserName, "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                attempts += 1;
                countAttempts();
                return;
            }

            if (!User.IsUserExistsByUserName(UserName))
            {
                attempts += 1;
                MessageBox.Show($"The Username '{UserName}' doesn't exists.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                countAttempts();
                return;
            }

            string ErrorForPass = FtFormValidator.ValidatePassword(Password, null, false);
            if (!string.IsNullOrEmpty(ErrorForPass))
            {
                MessageBox.Show(ErrorForPass, "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                attempts += 1;
                countAttempts();
                return;
            }


            User user = User.FindUserByUserName(UserName);
            if (!FtAuth.VerifyPassword(Password, user.PasswordHash))
            {
                attempts += 1;
                MessageBox.Show("Invalid credentials.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                countAttempts();
                return;

            }


            attempts = 0;
            blockTime = null;
            if(RememberMe == true)
            {
                User userToUpdate = new User
                {
                    UserId     = user.UserId,
                    RememberMe = RememberMe,
                };

                user.Update();
            }

            SessionManager.SaveSession(
                user
            );

            //MessageBox.Show("Login Successful!");

            frmDashboard dashboardForm = new frmDashboard();
            dashboardForm.Show();
            this.Hide();
        }

        private void countAttempts()
        {
            if (attempts > 5)
            {
                blockTime = DateTime.Now;
                btnLogin.Enabled = false;
                btnLogin.BackColor = Color.LightGray;
                txtUserName.Enabled = false;
                txtPassword.Enabled = false;
                lblLinkForRegister.Enabled = false;
                txtPassword.Clear();
                unblockTimer.Start();

                MessageBox.Show("Too many failed attempts. Please try again later.");
                return;

            }
        }

        private void txtUserName_TextChanged(object sender, EventArgs e)
        {
            UserName = txtUserName.Text.Trim();
        }

        private void txtPassword_TextChanged(object sender, EventArgs e)
        {
            Password = txtPassword.Text.Trim();
        }

        private void lblLinkForRegister_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            frmRegister registerForm = new frmRegister(); 
            registerForm.Show(); 
            this.Hide();
        }
    }
}
