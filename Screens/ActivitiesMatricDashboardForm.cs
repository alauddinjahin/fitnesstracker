using _216678_FitnessTracker.Models;
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
    public partial class frmActivitiesMatricsDashboard: Form
    {


        private List<FtActivity> activities = new List<FtActivity>();

        private frmActivitiesMatric activitiesMatricForm;

        public frmActivitiesMatricsDashboard()
        {
            InitializeComponent();
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MaximumSize = this.Size;
            this.MinimumSize = this.Size;
            this.FormClosing += new FormClosingEventHandler(frmActivitiesMatricsDashboard_FormClosing);

        }


        private void frmActivitiesMatricsDashboard_FormClosing(object sender, FormClosingEventArgs e)
        {
            // Close the child form if it's open
            if (activitiesMatricForm != null && !activitiesMatricForm.IsDisposed)
            {
                activitiesMatricForm.Close();
            }
        }

        private void frmActivitiesMatrics_Load(object sender, EventArgs e)
        {
            LoadData();
        }

        private void gvActivitiesMetrics_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            string searchText = txtSearch.Text.Trim();
            if (string.IsNullOrEmpty(searchText))
            {
                LoadData();
                return;
            }

            List<FtActivity> filteredActivities = activities
            .Where(g =>
                g.ActivityId.ToString().Contains(searchText) ||
                g.ActivityName.ToString().ToLower().Contains(searchText.ToLower()) ||
                g.Metric1.ToString().ToLower().Contains(searchText.ToLower()) ||
                g.Metric2.ToString().ToLower().Contains(searchText.ToLower()) ||
                g.Metric3.ToString().ToLower().Contains(searchText.ToLower())
             )
            .ToList();


            gvActivitiesMetrics.Rows.Clear();


            foreach (FtActivity activity in filteredActivities)
            {
                gvActivitiesMetrics.Rows.Add(
                    activity.ActivityId,
                    activity.ActivityName,
                    activity.Metric1,
                    activity.Metric2,
                    activity.Metric3
                );
            }

            gvActivitiesMetrics.Refresh();
        }

        private void LoadData()
        {
            activities = FtActivity.GetAllWithMetrics();

            gvActivitiesMetrics.Rows.Clear();
            gvActivitiesMetrics.AutoGenerateColumns = false;
            gvActivitiesMetrics.AllowUserToAddRows = false;
            if (activities.Any())
            {

                foreach (FtActivity activity in activities)
                {
                    gvActivitiesMetrics.Rows.Add(
                        activity.ActivityId,
                        activity.ActivityName,
                        activity.Metric1,
                        activity.Metric2,
                        activity.Metric3
                    );
                }

            }


            gvActivitiesMetrics.Refresh();
        }

        
        private void btnAddNew_Click(object sender, EventArgs e)
        {
            if (activitiesMatricForm == null || activitiesMatricForm.IsDisposed)
            {

                frmDashboard parent = this.Owner as frmDashboard;

                activitiesMatricForm = new frmActivitiesMatric(
                    (bool success) =>
                    {
                        if (success)
                        {
                            LoadData();
                            gvActivitiesMetrics.Refresh();
                            if (parent != null)
                            {
                                parent.RefreshTotalActivities();
                            }
                        }
                    }
                );

                activitiesMatricForm.Show();
            }
            else
            {
                activitiesMatricForm.BringToFront(); // Optional: bring it to front if already open
            }
        }

















    }
}
