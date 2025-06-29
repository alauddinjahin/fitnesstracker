using _216678_FitnessTracker.Config;
using _216678_FitnessTracker.Models;
using _216678_FitnessTracker.Utils;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace _216678_FitnessTracker.Screens
{
    public partial class frmDashboard: Form
    {

        private ToolStripMenuItem selectedMenuItem = null;
        private Panel contentPanel;
        private User user;
        public frmDashboard()
        {
            InitializeComponent();
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MaximumSize = this.Size;
            this.MinimumSize = this.Size;
            this.user = FtAuth.AuthUser();
            //this.dashboardToolStripMenuItem.Checked = true;

            InitializeContentPanel();
        }


        private void InitializeContentPanel()
        {
            contentPanel = new Panel
            {
                Location = new Point(10, 50),
                Size = new Size(1133, 500),
                BorderStyle = BorderStyle.None
            };

            this.Controls.Add(contentPanel);

        }


        private void ShowContent(Form form)
        {
            contentPanel.Controls.Clear();

            form.TopLevel = false;
            form.FormBorderStyle = FormBorderStyle.None;
            form.Dock = DockStyle.Fill;

            contentPanel.Controls.Add(form);
            form.Show();

            contentPanel.Visible = true;
            //form.Owner = this;
        }

        private void HideContent()
        {
            contentPanel.Visible = false;
            foreach (Control ctrl in contentPanel.Controls)
            {
                if (ctrl is Form currentForm)
                {
                    currentForm.Hide();
                    //currentForm.Owner = null;
                }
            }


        }

        public void RefreshTotalActivities()
        {
            lblTotalActivities.Text = User.CountTotalActivities().ToString() ?? "0"; ;
        }

        public void RefreshAuthUser()
        {
            User usr = FtAuth.AuthUser();
            lblAuthUser.Text = usr.UserName;

            if (!string.IsNullOrEmpty(usr.UserPhoto))
            {

                if (File.Exists(usr.UserPhoto))
                {
                    picAvatarAuthUser.Image = new Bitmap(usr.UserPhoto);
                    picAvatarAuthUser.SizeMode = PictureBoxSizeMode.Zoom;
                }
            }

        }

        private void frmDashboard_Load(object sender, EventArgs e)
        {
            //Console.WriteLine(SessionManager.LoadSession());
            LoadSummaryData();
            ShowDashboardInitContent();
            lblAuthUser.Text = user.UserName;
            MenuActiveSelection(this.dashboardToolStripMenuItem);
            AttachClickEventToTableLayoutPanel();
            RefreshAuthUser();

        }


        private void AttachClickEventToTableLayoutPanel()
        {
            for (int i = 0; i < tableLayoutPanel1.Controls.Count; i++)
            {
                Control control = tableLayoutPanel1.Controls[i];
                control.Click += new EventHandler(TableLayoutPanel_Click_Users);


                Control control2 = tableLayoutPanel2.Controls[i];
                control2.Click += new EventHandler(TableLayoutPanel_Click_ActivitiesMetrics);

            }
        }

        private void TableLayoutPanel_Click_Users(object sender, EventArgs e)
        {
            frmUserDashboard frmUserDashboard = new frmUserDashboard();
            frmUserDashboard.ShowDialog();
        }

        private void TableLayoutPanel_Click_ActivitiesMetrics(object sender, EventArgs e)
        {
            frmActivitiesMatricsDashboard frmActivitiesMatrics = new frmActivitiesMatricsDashboard();
            frmActivitiesMatrics.Owner = this;
            frmActivitiesMatrics.ShowDialog();
        }

        private void ClearDashboardInitContent()
        {
            tableLayoutPanel1.Hide();
            tableLayoutPanel2.Hide();
            tableLayoutPanel3.Hide();
            tableLayoutPanel4.Hide();
            lblTotalUsers.Hide();
            lblTotalActivities.Hide();
            lblTargetedCaloriesGoal.Hide();
            lblActivitiesHeading.Hide();
            lblGoalHeading.Hide();
            lblUserHeading.Hide();
            lblAchieved.Hide();
            lblAchievedCount.Hide();
        }

        private void ShowDashboardInitContent()
        {
            tableLayoutPanel1.Show();
            tableLayoutPanel2.Show();
            tableLayoutPanel3.Show();
            tableLayoutPanel4.Show();
            lblTotalUsers.Show();
            lblTotalActivities.Show();
            lblTargetedCaloriesGoal.Show();
            lblActivitiesHeading.Show();
            lblGoalHeading.Show();
            lblUserHeading.Show();
            lblAchieved.Show();
            lblAchievedCount.Show();
        }


        private void LoadSummaryData()
        {
            lblTotalUsers.Text = User.CountUser().ToString() ?? "0";
            lblTotalActivities.Text = User.CountTotalActivities().ToString() ?? "0";
            lblTargetedCaloriesGoal.Text = Goal.TargtedGoalCalories(user.UserId).ToString() ?? "0";
            Goal goal = new Goal();
            var result = goal.CheckGoalAchievement(user.UserId);
            lblAchievedCount.Text = result.TotalCaloriesBurned.ToString() ?? "0";
            lblAchieved.Text = result.SuccessGoalStatus;

        }

        private void logoutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SessionManager.ClearSession();

            frmLogin loginForm = new frmLogin();
            loginForm.Show();
            this.Hide();
            lblAuthUser.Text = "";
            //MenuActiveSelection(sender, e);
        }

        private void MenuActiveSelectionHover(object sender)
        {

            var menuItem = sender as ToolStripMenuItem;
            if (menuItem != null)
            {
                var ForeColor = Color.Black;
                menuItem.ForeColor = ForeColor;

            }
        }

        private void MenuActiveSelectionLeave(object sender)
        {

            var menuItem = sender as ToolStripMenuItem;
            if (menuItem != null)
            {
                var ForeColor = Color.Black;
                menuItem.ForeColor = ForeColor;

            }
        }


        private void MenuActiveSelection(object sender)
        {
            // Reset previous selection
            if (selectedMenuItem != null)
            {
                ResetMenuColors(selectedMenuItem);
                ToolStripItem parent = selectedMenuItem.OwnerItem;
                while (parent is ToolStripMenuItem parentItem)
                {
                    ResetMenuColors(parentItem);
                    parent = parentItem.OwnerItem;
                }
            }

            // Apply new selection
            if (sender is ToolStripMenuItem menuItem)
            {
                ApplyMenuColors(menuItem);
                selectedMenuItem = menuItem;

                ToolStripItem parent = menuItem.OwnerItem;
                while (parent is ToolStripMenuItem parentItem)
                {
                    ApplyMenuColors(parentItem);
                    parent = parentItem.OwnerItem;
                }
            }
        }



        private void ApplyMenuColors(ToolStripMenuItem item)
        {
            item.BackColor = Color.Brown;
            item.ForeColor = Color.White;
        }

        private void ResetMenuColors(ToolStripMenuItem item)
        {
            item.BackColor = Color.Transparent;
            item.ForeColor = Color.Black;
        }

        private void fitnessGoalsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ClearDashboardInitContent();
            frmFitnessGoalsDashboard frmFitnessGoalsDashboardForm = new frmFitnessGoalsDashboard();
            ShowContent(frmFitnessGoalsDashboardForm);
            MenuActiveSelection(sender);
        }



        private void setFitnessGoalDefineTargetCaloriesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //HideContent();
            ClearDashboardInitContent();
            frmFitnessGoals fitnessGoalsForm = new frmFitnessGoals();
            fitnessGoalsForm.Show();
            //ShowContent(fitnessGoalsForm);
        }

        private void recordActivityChooseExerciseEnterMetricsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //frmRecordActivityDashboard recordActivityForm = new frmRecordActivityDashboard();
            ClearDashboardInitContent();
            frmActivityRecordDashboard recordActivityForm = new frmActivityRecordDashboard();
            ShowContent(recordActivityForm);
            MenuActiveSelection(sender);
        }

        private void dashboardToolStripMenuItem_Click(object sender, EventArgs e)
        {
            HideContent();
            ShowDashboardInitContent();
            LoadSummaryData();
            MenuActiveSelection(sender);

        } 
        
        private void dashboardToolStripMenuItem_Hover(object sender, EventArgs e)
        {
            //MenuActiveSelectionHover(sender);

        }   
        
        private void dashboardToolStripMenuItem_Leave(object sender, EventArgs e)
        {
            //MenuActiveSelectionLeave(sender);

        }

        private void activitiesToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void lblSearch_Click(object sender, EventArgs e)
        {

        }

        private void tableLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {
            
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void lblTotalUsers_Click(object sender, EventArgs e)
        {

        }

        private void tableLayoutPanel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void tableLayoutPanel3_Paint(object sender, PaintEventArgs e)
        {

        }

        private void helpSupportToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void panelFooter_Paint(object sender, PaintEventArgs e)
        {

        }

        private void btnLogout_Click(object sender, EventArgs e)
        {
            SessionManager.ClearSession();

            frmLogin loginForm = new frmLogin();
            loginForm.Show();
            this.Hide();
            lblAuthUser.Text = "";
        }

        private void lblUserHeading_Click(object sender, EventArgs e)
        {

        }

        private void activityListToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ClearDashboardInitContent();
            frmActivitiesMatricsDashboard frmActivitiesMatrics = new frmActivitiesMatricsDashboard();
            //frmActivitiesMatrics.ShowDialog();
            ShowContent(frmActivitiesMatrics);
            MenuActiveSelection(sender);

        }

        private void lblTotalActivities_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {

        }

        private void viewProfileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ClearDashboardInitContent();
            frmUserProfile frmUserProfile = new frmUserProfile(false);
            frmUserProfile.Owner = this;
            ShowContent(frmUserProfile);
            MenuActiveSelection(sender);
        }

        private void editProfileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ClearDashboardInitContent();
            frmUserProfile frmUserProfile = new frmUserProfile(true);
            frmUserProfile.Owner = this;
            ShowContent(frmUserProfile);
            MenuActiveSelection(sender);
        }

        private void changePasswordToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ClearDashboardInitContent();
            frmEditPassword frmEditPassword = new frmEditPassword();
            frmEditPassword.Owner = this;
            ShowContent(frmEditPassword);
            MenuActiveSelection(sender);
        }

        private void caloriesCalculatorEstimateCaloriesBurnedToolStripMenuItem_Click(object sender, EventArgs e)
        {
            
            ClearDashboardInitContent();
            frmCalorieBurnedCalculator frmCalorieBurnedCalculator = new frmCalorieBurnedCalculator();
            frmCalorieBurnedCalculator.Owner = this;
            ShowContent(frmCalorieBurnedCalculator);
            MenuActiveSelection(sender);
        }
    }
}
