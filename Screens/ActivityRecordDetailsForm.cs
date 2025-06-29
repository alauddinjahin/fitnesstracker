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
    public partial class frmActivityRecordDetails: Form
    {

        public int RecordId;
        public int ActivityId;
        public int UserId;
        public string ActivityType;
        public string CreatedAt;
        public float Metric1;
        public float Metric2;
        public float Metric3;
        public float CaloriesBurned;
        public DataGridViewRow SelectedRow;

        public frmActivityRecordDetails(DataGridViewRow selectedRow)
        {
            InitializeComponent();
            this.SelectedRow = selectedRow;
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {

        }

        private void frmActivityRecordDetails_Load(object sender, EventArgs e)
        {
            string recordId = SelectedRow.Cells[0].Value?.ToString() ?? "";
            if (string.IsNullOrEmpty(recordId))
            {
                this.Hide();
                return;
            }

            ActivityId = int.Parse(this.SelectedRow.Cells[1].Value.ToString());
            TrackMatricsLabels(ActivityId);

            UserId = int.Parse(this.SelectedRow.Cells[2].Value.ToString());
            ActivityType = this.SelectedRow.Cells[3].Value.ToString();
            Metric1 = float.Parse(this.SelectedRow.Cells[4].Value.ToString());
            Metric2 = float.Parse(this.SelectedRow.Cells[5].Value.ToString());
            Metric3 = float.Parse(this.SelectedRow.Cells[6].Value.ToString());
            CaloriesBurned = float.Parse(this.SelectedRow.Cells[7].Value.ToString());
            CreatedAt = this.SelectedRow.Cells[8].Value.ToString();

            txtRecordId.Text = recordId;
            txtActivityType.Text = ActivityType;
            txtMetric1.Text = Metric1.ToString();
            txtMetric2.Text = Metric2.ToString();
            txtMetric3.Text = Metric3.ToString();
            txtCaloriesBurned.Text = CaloriesBurned.ToString();
            txtActivityDate.Text = CreatedAt;


        }

        private void TrackMatricsLabels(int activityId = -1)
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
    }
}
