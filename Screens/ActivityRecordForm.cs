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
    public partial class frmActivityRecord: Form
    {
        public Action<bool> callback;
        List<FtActivity> ActivityTypeList = new List<FtActivity>();
        public int RecordId;
        public int ActivityId;
        public int UserId;
        public string ActivityType;
        public DateTime CreatedAt;
        public float Metric1;
        public float Metric2;
        public float Metric3;
        public float CaloriesBurned;

        public frmActivityRecord(Action<bool> callback = null, int RecordId = -1)
        {
            InitializeComponent();
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MaximumSize = this.Size;
            this.MinimumSize = this.Size;
            this.callback = callback;
            this.RecordId = RecordId;
            TrackMatricsLabels(1);

        }


        private void ClearForm()
        {
            txtMetric1.Clear();
            txtMetric2.Clear();
            txtMetric3.Clear();
            dtpActivityDate.Value = DateTime.Now;
            cmbActivityType.SelectedIndex = 0;
            TrackMatricsLabels(1);
        }

        private void frmActivityRecord_Load(object sender, EventArgs e)
        {

            ActivityTypeList.AddRange(FtActivity.GetAll());
            cmbActivityType.DataSource = ActivityTypeList;
            cmbActivityType.DisplayMember = "ActivityName"; 
            cmbActivityType.ValueMember = "ActivityId"; 

            if (ActivityTypeList.Count > 0)
            {
                cmbActivityType.SelectedIndex = 0;
                //TrackMatricsLabels();
            }

            if (RecordId > 0)
            {

                ActivityRecord activity = ActivityRecord.Find(RecordId);
                cmbActivityType.SelectedValue = activity.ActivityId;

                TrackMatricsLabels(activity.ActivityId);

                txtMetric1.Text = activity.Metric1.ToString();
                txtMetric2.Text = activity.Metric2.ToString();
                txtMetric3.Text = activity.Metric3.ToString();
                dtpActivityDate.Value = activity.CreatedAt;
                btnSubmitActivity.Text = "Update";

            }
        }


        private void TrackMatricsLabels(int activityId=-1)
        {
            List<ActivityMetric> activityMetric;

            if (activityId < 0)
            {
                lblMetric1.Text = "Metric1";
                lblMetric2.Text = "Metric2";
                lblMetric3.Text = "Metric3";
                return;
            }

            activityMetric = ActivityMetric.GetAll(activityId);

            if (activityMetric != null && activityMetric.Count > 0)
            {

                for (var i = 0; i < activityMetric.Count; i++)
                {
                    var MetricName = activityMetric[i].MetricName;
                    switch (i)
                    {
                        case 0:
                            lblMetric1.Text = MetricName;
                            break;
                        case 1:
                            lblMetric2.Text = MetricName;
                            break;
                        case 2:
                            lblMetric3.Text = MetricName;
                            break;
                    }
                }
            }
        }
        private void cmbActivityType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (int.TryParse(cmbActivityType.SelectedValue?.ToString(), out int activityId))
            {
                TrackMatricsLabels(activityId);
            }

            
        }


        private void btnSubmitActivity_Click(object sender, EventArgs e)
        {

            Console.WriteLine(cmbActivityType.SelectedItem?.ToString());

            if (cmbActivityType.SelectedItem == null)
            {
                MessageBox.Show("Please select ActivityType.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
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


            if (string.IsNullOrEmpty(dtpActivityDate.Text?.ToString()))
            {
                MessageBox.Show("Please enter duration.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }


            if (!int.TryParse(cmbActivityType.SelectedValue?.ToString(), out int activityId))
            {
                MessageBox.Show("Please select a valid activity type." + activityId, "Invalid Input", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (!float.TryParse(txtMetric1.Text, out float metric1))
            {
                MessageBox.Show($"Please enter a valid value for {lblMetric1.Text}.", "Invalid Input", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (!float.TryParse(txtMetric2.Text, out float metric2))
            {
                MessageBox.Show($"Please enter a valid value for {lblMetric2.Text}.", "Invalid Input", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (!float.TryParse(txtMetric3.Text, out float metric3))
            {
                MessageBox.Show($"Please enter a valid value for {lblMetric3.Text}.", "Invalid Input", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }



            if (RecordId > 0)
            {
                var activityRecord = new ActivityRecord
                {
                    UserId = FtAuth.AuthUser().UserId,
                    RecordId = RecordId,
                    ActivityId = activityId,
                    Metric1 = float.Parse(txtMetric1.Text),
                    Metric2 = float.Parse(txtMetric2.Text),
                    Metric3 = float.Parse(txtMetric3.Text),
                    CreatedAt = dtpActivityDate.Value
                };

                if (activityRecord.Update(activityRecord))
                {
                    MessageBox.Show(
                        "Activity updated successfully!",
                        "Success",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information
                    );

                    ClearForm();
                    this.Hide();
                    callback?.Invoke(true);

                }
                else
                {
                    callback?.Invoke(false);
                }
            }
            else
            {


                var activityRecord = new ActivityRecord{
                    UserId = FtAuth.AuthUser().UserId,
                    ActivityId = activityId,
                    Metric1 = float.Parse(txtMetric1.Text),
                    Metric2 = float.Parse(txtMetric2.Text),
                    Metric3 = float.Parse(txtMetric3.Text),
                    CreatedAt = dtpActivityDate.Value
                };

                if (activityRecord.Save(activityRecord))
                {
                    MessageBox.Show("You added activity record successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
}
