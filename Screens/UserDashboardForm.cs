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
    public partial class frmUserDashboard: Form
    {
        private List<User> users = new List<User>();

        public frmUserDashboard()
        {
            InitializeComponent();
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MaximumSize = this.Size;
            this.MinimumSize = this.Size;
        }

        private void frmUserDashboard_Load(object sender, EventArgs e)
        {
            LoadData();
        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            string searchText = txtSearch.Text.Trim();
            if (string.IsNullOrEmpty(searchText))
            {
                LoadData();
                return;
            }

            List<User> filteredUsers = users
            .Where(g =>
                g.UserId.ToString().Contains(searchText) ||
                g.UserName.ToString().ToLower().Contains(searchText.ToLower()) ||
                g.Email.ToString().ToLower().Contains(searchText.ToLower()) ||
                g.PhoneNumber.ToString().Contains(searchText) ||
                g.DateOfBirth.ToString("yyyy-MM-dd").Contains(searchText) ||
                g.Gender.ToString().ToLower().Contains(searchText.ToLower())
             )
            .ToList();


            gvUsers.Rows.Clear();

            foreach (User user in filteredUsers)
            {
                gvUsers.Rows.Add(
                    user.UserId,
                    user.UserName,
                    user.Email,
                    user.PhoneNumber,
                    user.DateOfBirth.ToString("yyyy-MM-dd"),
                    user.Gender
                );
            }

            gvUsers.Refresh();
        }

        private void LoadData()
        {
            users = User.All();

            gvUsers.Rows.Clear();
            gvUsers.AutoGenerateColumns = false;
            gvUsers.AllowUserToAddRows = false;

            if (users.Any())
            {

                foreach (User user in users)
                {
                    gvUsers.Rows.Add(
                        user.UserId,
                        user.UserName,
                        user.Email,
                        user.PhoneNumber,
                        user.DateOfBirth.ToString("yyyy-MM-dd"),
                        user.Gender
                    );
                }

            }


            gvUsers.Refresh();
        }
    }
}
