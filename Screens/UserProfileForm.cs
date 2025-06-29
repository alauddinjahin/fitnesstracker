using _216678_FitnessTracker.Models;
using _216678_FitnessTracker.Utils;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ListView;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;

namespace _216678_FitnessTracker.Screens
{
    public partial class frmUserProfile: Form
    {
        private string selectedImagePath;
        private string savedImagePath; 
        private bool isImageSaved = false;
        private bool isUserDataSaved = false;
        private bool IsEnabledForEdit;
        private User User;
        public frmUserProfile(bool isEnabledForEdit=false)
        {
            InitializeComponent();
            this.IsEnabledForEdit = isEnabledForEdit;
            this.FormClosing += frmUserProfile_FormClosing;
            this.User = FtAuth.AuthUser();
        }

        private void frmUserProfile_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (isImageSaved && !isUserDataSaved)
            {
                try
                {
                    if (File.Exists(savedImagePath))
                    {
                        File.Delete(savedImagePath);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error deleting temporary image: " + ex.Message);
                }
            }
        }


        private void lblActivityDetailsHeadingP1_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {

            if (!pbProfile.Enabled) return;

            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Title = "Select an Image";
            openFileDialog.Filter = "Image Files|*.jpg;*.jpeg;*.png";

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    // Load selected image into PictureBox
                    pbProfile.Image = new Bitmap(openFileDialog.FileName);
                    pbProfile.SizeMode = PictureBoxSizeMode.StretchImage;
                    string selectedImagePath = openFileDialog.FileName;

                    // Save the image to your project folder (e.g., /Images/Profiles/)
                    string imagesFolder = Path.Combine(Application.StartupPath, "Resources", "Profiles");

                    // Make sure folder exists
                    if (!Directory.Exists(imagesFolder))
                    {
                        Directory.CreateDirectory(imagesFolder);
                    }

                    string fileName = Path.GetFileName(selectedImagePath);
                    savedImagePath = Path.Combine(imagesFolder, fileName);

                    File.Copy(selectedImagePath, savedImagePath, true);

                    isImageSaved = true;
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }

            }

        }

        private void btnUpdateUser_Click(object sender, EventArgs e)
        {
            string UserName = txtUsername.Text.Trim();
            string ValidationMsg = "";

            if (string.IsNullOrWhiteSpace(UserName))
            {
                ValidationMsg = "Please enter a Username.";
            }
            else if (User.UserName != UserName && FtFormValidator.CheckUniqueUserByName(UserName, User.UserId))
            {
                ValidationMsg = $"The Username '{UserName}' already been taken.";
            }
            
            
            if (!string.IsNullOrEmpty(ValidationMsg))
            {
                MessageBox.Show(ValidationMsg, "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string Phone = txtPhoneNumber.Text.Trim();
            if (string.IsNullOrWhiteSpace(Phone))
            {
                ValidationMsg = "Please enter a PhoneNumber.";
            }
            else if (User.PhoneNumber != Phone && FtFormValidator.CheckUniqueUserByPhone(Phone, User.UserId))
            {
                ValidationMsg = $"The PhoneNumber '{Phone}' already been taken.";
            }


            if (!string.IsNullOrEmpty(ValidationMsg))
            {
                MessageBox.Show(ValidationMsg, "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string EMail = txtEmail.Text.Trim();
            if (string.IsNullOrWhiteSpace(EMail))
            {
                ValidationMsg = "Please enter a valid Email.";
            }
            else if (User.Email != EMail && FtFormValidator.CheckUniqueUserByEmail(EMail, User.UserId))
            {
                ValidationMsg = $"The Email '{EMail}' already been taken.";
            }


            if (!string.IsNullOrEmpty(ValidationMsg))
            {
                MessageBox.Show(ValidationMsg, "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }


            User userToUpdate = new User
            {
                UserId = User.UserId,
                UserName = txtUsername.Text ?? "",
                PhoneNumber = txtPhoneNumber.Text ?? "",
                Email = txtEmail.Text ?? "",
                UserPhoto = savedImagePath,
            };

            if (userToUpdate.Update())
            {
                isUserDataSaved = true;

                User user = User.Find(User.UserId);
                user.UserPhoto = savedImagePath;
                SessionManager.SaveSession(
                    user
                );

                this.User = user;

                frmDashboard parent = this.Owner as frmDashboard;
                if (parent != null)
                {
                    parent.RefreshAuthUser();
                }

                MessageBox.Show("User updated successfully!");

            }
            else
            {
                isUserDataSaved = false;
            }


        }

        private void frmUserProfile_Load(object sender, EventArgs e)
        {

            loadData();

            if (IsEnabledForEdit)
            {
                EnabledEditMode();
                return;
            }

            EnabledViewMode();
        }


        private void loadData()
        {

            txtUserId.Text = User.UserId.ToString();
            txtUsername.Text = User.UserName ?? "";
            txtPhoneNumber.Text = User.PhoneNumber ?? "";
            txtEmail.Text = User.Email ?? "";
            txtGender.Text = User.Gender ?? "";
            txtDob.Text = User.DateOfBirth.ToString("yyyy-MM-dd");

            if (!string.IsNullOrEmpty(User.UserPhoto))
            {
                if (File.Exists(User.UserPhoto))
                {
                    pbProfile.Image = new Bitmap(User.UserPhoto);
                    pbProfile.SizeMode = PictureBoxSizeMode.StretchImage;
                }
            }

        }

        private void EnabledEditMode()
        {
            pbProfile.Cursor = Cursors.Hand; // when hover, show hand cursor
            pbProfile.Enabled = true;
            btnUpdateUser.Enabled = true;
            btnUpdateUser.Visible = true;
            txtUsername.ReadOnly = false;
            txtUsername.Enabled = true;
            txtPhoneNumber.ReadOnly = false;
            txtPhoneNumber.Enabled = true;
            txtEmail.ReadOnly = false;
            txtEmail.Enabled = true;

            txtGender.ReadOnly = true;
            txtDob.ReadOnly = true;

            Control firstControl = this.Controls
            .OfType<Control>()
            .Where(c => c.TabIndex == 0 && c.CanFocus && c.Enabled && c.Visible)
            .FirstOrDefault();

            if (firstControl != null)
            {
                firstControl.Focus();
            }
        }


        private void EnabledViewMode()
        {
            pbProfile.Cursor = Cursors.Default;
            pbProfile.Enabled = false;
            btnUpdateUser.Enabled = false;
            btnUpdateUser.Visible = false;
            txtUsername.ReadOnly = true;
            txtPhoneNumber.ReadOnly = true;
            txtEmail.ReadOnly = true;
            txtGender.ReadOnly = true;
            txtDob.ReadOnly = true;
        }
    }
}
