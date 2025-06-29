using _216678_FitnessTracker.Config;
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
    public partial class frmActivitiesMatric: Form
    {
        private Action<bool> callback;
        public frmActivitiesMatric(Action<bool> callback)
        {
            InitializeComponent();
            this.callback = callback;
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MaximumSize = this.Size;
            this.MinimumSize = this.Size;
        }

        private void lblActivityDetailsHeadingP2_Click(object sender, EventArgs e)
        {

        }

        private void lblActivityDetailsHeadingP1_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {

        }

        private void ActivitiesMatricForm_Load(object sender, EventArgs e)
        {

        }

        private void ClearForm()
        {
            txtActivityType.Clear();
            txtMetric1.Clear();
            txtMetric2.Clear();
            txtMetric3.Clear();

        }

        private void btnSubmitActivityMetrics_Click(object sender, EventArgs e)
        {

            if (string.IsNullOrEmpty(txtActivityType.Text?.ToString()))
            {
                MessageBox.Show($"Please enter Activity type.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (string.IsNullOrEmpty(txtMetric1.Text?.ToString()))
            {
                MessageBox.Show($"Please enter {lblMetric1.Text}.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (string.IsNullOrEmpty(txtMetric2.Text?.ToString()))
            {
                MessageBox.Show($"Please enter {lblMetric2.Text}.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (string.IsNullOrEmpty(txtMetric3.Text?.ToString()))
            {
                MessageBox.Show($"Please enter {lblMetric3.Text}.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }


            FtActivity ftActivity = new FtActivity
            {
                ActivityName = txtActivityType.Text.ToString()
            };

            if (ftActivity.Save())
            {
                string[] metricTexts = { txtMetric1.Text.ToString(), txtMetric2.Text.ToString(), txtMetric3.Text.ToString() };

                foreach (var metricText in metricTexts)
                {
                    ActivityMetric metric = new ActivityMetric
                    {
                        ActivityId = ftActivity.ActivityId,
                        MetricName = metricText
                    };
                    metric.Save();

                    FtDB.GetDbConnection().Close();
                }


                MessageBox.Show("Activity added successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                ClearForm();
                callback?.Invoke(true);


            }
            else
            {
                callback?.Invoke(false);
            }



        }
    }
}
