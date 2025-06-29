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
    public partial class frmFitnessGoalsDashboard: Form
    {
        private List<Goal> goals = new List<Goal>();

        public frmFitnessGoalsDashboard()
        {
            InitializeComponent();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void frmFitnessGoalsDashboard_Load(object sender, EventArgs e)
        {
            LoadGoalsData();
        }

        private void LoadGoalsData()
        {
            User user = FtAuth.AuthUser();
            goals = Goal.GetAllGoalsForUser(user.UserId);

            gvGoals.Rows.Clear();
            if (!gvGoals.Columns.Contains("Action"))
            {
                DataGridViewButtonColumn actionColumn = new DataGridViewButtonColumn
                {
                    HeaderText = "Action",
                    Name = "Action",
                    Text = "Edit/Delete", 
                    UseColumnTextForButtonValue = false, 
                    //Text = "Center"
                };

                gvGoals.Columns.Add(actionColumn);
            }

            gvGoals.AutoGenerateColumns = false;
            gvGoals.AllowUserToAddRows = false;

            if (goals.Any())
            {

                foreach (Goal goal in goals)
                {
                    gvGoals.Rows.Add(
                        goal.GoalId,
                        goal.UserId,
                        goal.GoalCalories,
                        goal.SetDate.ToString("yyyy-MM-dd")
                    );
                }

                gvGoals.CellPainting += gvGoals_CellPainting;
            }


            gvGoals.Refresh();



        }

        private void gvGoals_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        {
            if (e.ColumnIndex == gvGoals.Columns["Action"].Index && e.RowIndex >= 0)
            {
                e.Paint(e.CellBounds, DataGridViewPaintParts.All);

                // Define button sizes
                int buttonWidth = e.CellBounds.Width / 2 - 5;
                int buttonHeight = e.CellBounds.Height - 6;

                Rectangle editButtonRect = new Rectangle(
                    e.CellBounds.X + 3,
                    e.CellBounds.Y + 3,
                    buttonWidth,
                    buttonHeight
                );

                Rectangle deleteButtonRect = new Rectangle(
                    e.CellBounds.X + buttonWidth + 7,
                    e.CellBounds.Y + 3,
                    buttonWidth,
                    buttonHeight
                );

                // Draw the "Edit" button
                ButtonRenderer.DrawButton(e.Graphics, editButtonRect, System.Windows.Forms.VisualStyles.PushButtonState.Normal);
                TextRenderer.DrawText(e.Graphics, "Edit", gvGoals.Font, editButtonRect, Color.Black, TextFormatFlags.HorizontalCenter | TextFormatFlags.VerticalCenter);

                // Draw the "Delete" button
                ButtonRenderer.DrawButton(e.Graphics, deleteButtonRect, System.Windows.Forms.VisualStyles.PushButtonState.Normal);
                TextRenderer.DrawText(e.Graphics, "Delete", gvGoals.Font, deleteButtonRect, Color.Black, TextFormatFlags.HorizontalCenter | TextFormatFlags.VerticalCenter);

                e.Handled = true; // Prevent default rendering
            }
        }


        private void gvGoals_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0 || e.ColumnIndex < 0)
                return;

            DataGridViewRow selectedRow = gvGoals.Rows[e.RowIndex];

            string goalId = selectedRow.Cells[0].Value?.ToString() ?? "";
            string Calories = selectedRow.Cells[2].Value?.ToString() ?? "";

            if (gvGoals.Columns[e.ColumnIndex].Name == "Action")
            {

                Rectangle cellBounds = gvGoals.GetCellDisplayRectangle(e.ColumnIndex, e.RowIndex, false);

                // Define button sizes based on the previous painting logic
                int buttonWidth = cellBounds.Width / 2 - 5;
                int buttonHeight = cellBounds.Height - 6;

                Rectangle editButtonRect = new Rectangle(
                    cellBounds.X + 3,
                    cellBounds.Y + 3,
                    buttonWidth,
                    buttonHeight
                );

                Rectangle deleteButtonRect = new Rectangle(
                    cellBounds.X + buttonWidth + 7,
                    cellBounds.Y + 3,
                    buttonWidth,
                    buttonHeight
                );

                Point mousePosition = gvGoals.PointToClient(Cursor.Position);

                if (editButtonRect.Contains(mousePosition))
                {
                    //Console.WriteLine("Edit button clicked");
                    frmFitnessGoals fitnessGoalsForm = new frmFitnessGoals(
                        (bool success) =>
                        {
                            if (success)
                            {
                                LoadGoalsData();
                                gvGoals.Refresh();
                            }
                        },
                        int.Parse(goalId),
                        int.Parse(Calories)
                     );


                    fitnessGoalsForm.Show();

                }
                else if (deleteButtonRect.Contains(mousePosition))
                {
                    var confirm = MessageBox.Show(
                        "Are you sure you want to delete this item?",
                        "Confirm Delete",
                        MessageBoxButtons.YesNo,
                        MessageBoxIcon.Question
                    );

                    if (confirm == DialogResult.Yes)
                    {
                        Goal goal = new Goal();
                        if (goal.Delete(int.Parse(goalId)))
                        {
                            MessageBox.Show(
                                "Data deleted successfully!",
                                "Success",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Information
                            );

                            LoadGoalsData();
                        }

                    }
                }


            }
            else
            {
                //frmUserDetailsModal modal = new frmUserDetailsModal(userId, firstName, createdAt);
                //modal.ShowDialog(); // Show as a modal
            }
        }



        private void txtSearch_TextChanged(object sender, EventArgs e)
        {

            string searchText = txtSearch.Text.Trim();
            if (string.IsNullOrEmpty(searchText))
            {
                LoadGoalsData();
                return;
            }

            List<Goal> filteredGoals = goals
            .Where(g =>
                g.GoalId.ToString().ToLower().Contains(searchText.ToLower()) ||
                g.UserId.ToString().ToLower().Contains(searchText.ToLower()) ||
                g.GoalCalories.ToString().ToLower().Contains(searchText.ToLower()) ||
                g.SetDate.ToString("yyyy-MM-dd").Contains(searchText.ToLower())
             )
            .ToList();


            gvGoals.Rows.Clear();

            foreach (Goal goal in filteredGoals)
            {
                gvGoals.Rows.Add(
                    goal.GoalId,
                    goal.UserId,
                    goal.GoalCalories,
                    goal.SetDate.ToString("yyyy-MM-dd")
                );
            }

            gvGoals.Refresh();
        }

        private void btnAddNew_Click(object sender, EventArgs e)
        {
            frmFitnessGoals fitnessGoalsForm = new frmFitnessGoals(
                (bool success) =>
                {
                    if (success)
                    {
                        LoadGoalsData();
                        gvGoals.Refresh();
                    }
                }
            );
            fitnessGoalsForm.Show();
        }

        private void gvGoals_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
