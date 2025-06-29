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
    public partial class frmCalorieBurnedCalculator: Form
    {
        List<FtActivity> ActivityTypeList = new List<FtActivity>();
        public frmCalorieBurnedCalculator()
        {
            InitializeComponent();
        }

        private void lblMetric1_Click(object sender, EventArgs e)
        {

        }

        private void txtMetric3_TextChanged(object sender, EventArgs e)
        {

        }

        private void dtpActivityDate_ValueChanged(object sender, EventArgs e)
        {

        }

        private void cmbActivityType_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void lblActivityType_Click(object sender, EventArgs e)
        {

        }

        private void lblActivityDate_Click(object sender, EventArgs e)
        {

        }

        private void lblMetric2_Click(object sender, EventArgs e)
        {

        }

        private void txtMetric2_TextChanged(object sender, EventArgs e)
        {

        }

        private void lblMetric3_Click(object sender, EventArgs e)
        {

        }

        private void txtMetric1_TextChanged(object sender, EventArgs e)
        {

        }

        private void btnSubmitActivity_Click(object sender, EventArgs e)
        {
            //cmbActivityType.Text;
            //txtDuration.Text;
            //txtWeight.Text;
            //txtResult.Text;
            int activityRecordId = default;
            CalorieBurned calorieBurned = new CalorieBurned();
            if (int.TryParse(cmbActivityType.SelectedValue?.ToString(), out int activityId))
            {
                activityRecordId = activityId;
            }

            //GetAllWithMetrics
            //FtActivity.Find(activityRecordId);
            //calorieBurned.CaloriesBurnedUsingMETFormula();
        }

        private void frmCalorieBurnedCalculator_Load(object sender, EventArgs e)
        {
            ActivityTypeList.AddRange(FtActivity.GetAll());
            cmbActivityType.DataSource = ActivityTypeList;
            cmbActivityType.DisplayMember = "ActivityName";
            cmbActivityType.ValueMember = "ActivityId";

            if (ActivityTypeList.Count > 0)
            {
                cmbActivityType.SelectedIndex = 0;
            }
        }
    }
}
