using _216678_FitnessTracker.Models;
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

namespace _216678_FitnessTracker.Screens
{
    public partial class frmEditPassword: Form
    {
        User user = null;
        string Password;
        string ConfirmPassword;
        public frmEditPassword()
        {
            InitializeComponent();
            this.user = FtAuth.AuthUser();
        }

        private void frmEditPassword_Load(object sender, EventArgs e)
        {
            txtUserId.Text = user.UserId.ToString();
        }

        private void txtNewPass_TextChanged(object sender, EventArgs e)
        {
            Password = txtNewPass.Text;
        }

        private void txtConfirmNewPass_TextChanged(object sender, EventArgs e)
        {
            ConfirmPassword = txtConfirmNewPass.Text;
        }


        private void btnUpdatePass_Click(object sender, EventArgs e)
        {
            if(string.IsNullOrEmpty(Password) || string.IsNullOrEmpty(ConfirmPassword))
            {
                MessageBox.Show("Password fields are required.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }


            string Error = FtFormValidator.ValidatePassword(Password, ConfirmPassword);
            if (!string.IsNullOrEmpty(Error))
            {
                MessageBox.Show(Error, "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }



            User usr = new User
            {
                UserId = user.UserId,
                PasswordHash = FtAuth.GenerateHashPassword(Password)
            };

            if (usr.Update())
            {
                MessageBox.Show("Password updated successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtConfirmNewPass.Text = "";
                txtNewPass.Text = "";
            }
        }



    }
}
