using _216678_FitnessTracker.Models;
using _216678_FitnessTracker.Utils;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.Remoting.Metadata.W3cXsd2001;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace _216678_FitnessTracker.Screens
{
    public partial class frmFitnessGoals: Form
    {
        private int GoalCalories;
        private int UserId;
        private int GoalId;
        private Action<bool> callback;
        public frmFitnessGoals(Action<bool> callback = null, int goalId=-1, int calories = -1)
        {
            InitializeComponent();

            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MaximumSize = this.Size;
            this.MinimumSize = this.Size;
            this.GoalId = goalId;
            this.GoalCalories = calories;
            this.callback = callback;
        }


        private void FitnessGoalsFormcs_Load(object sender, EventArgs e)
        {
            RenderCmbUserList();
            if(GoalId > 0)
            {
                txtGoalCalories.Text = GoalCalories.ToString();
                btnSubmitGoalCalories.Text = "Update";  
            }
        }

        private void RenderCmbUserList()
        {
            List<User> users = new List<User>();

            // add role wise logic here
            users.Add(FtAuth.AuthUser());

            cmbUserId.DataSource = users;
            cmbUserId.DisplayMember = "UserName"; // Show UserName in dropdown
            cmbUserId.ValueMember = "UserId"; // Store UserId as selected value

            if (users.Count > 0)
            {
                cmbUserId.SelectedIndex = 0;
            }
        }

        private void ClearForm()
        {
            txtGoalCalories.Clear();
            cmbUserId.SelectedIndex = 0;
        }

        private void btnSubmitGoalCalories_Click(object sender, EventArgs e)
        {
            
            if (string.IsNullOrEmpty(txtGoalCalories.Text))
            {
                MessageBox.Show("Please enter Goal Calories.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string errorMsg = FtFormValidator.ValidateInteger(txtGoalCalories.Text);

            if (!string.IsNullOrEmpty(errorMsg))
            {
                MessageBox.Show(errorMsg, "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (int.TryParse(cmbUserId.SelectedValue.ToString(), out int userId))
            {
                UserId = userId;
            }

            GoalCalories = int.Parse(txtGoalCalories.Text);


            if(GoalId > 0)
            {
                Goal goal = new Goal
                {
                    GoalCalories = GoalCalories,
                    GoalId = GoalId
                };

                if (goal.Update())
                {
                    MessageBox.Show(
                        "Goal updated successfully!",
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
                Goal goal = new Goal
                {
                    GoalCalories = GoalCalories,
                    UserId = UserId,
                    SetDate = DateTime.Now
                };

                if (goal.Save())
                {
                    MessageBox.Show("You added Goal Calories successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
