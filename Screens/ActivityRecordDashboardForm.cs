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
    public partial class frmActivityRecordDashboard: Form
    {
        private List<ActivityRecord> activitiesList = new List<ActivityRecord>();
        User user = null;
        public frmActivityRecordDashboard()
        {
            InitializeComponent();
            this.user = FtAuth.AuthUser();
            this.activitiesList = ActivityRecord.GetAll(user.UserId);
        }

        private void btnAddNew_Click(object sender, EventArgs e)
        {
            frmActivityRecord frmActivityRecord = new frmActivityRecord(
               (bool success) =>
               {
                   if (success)
                   {

                       this.activitiesList = ActivityRecord.GetAll(user.UserId);
                       LoadData();
                       gvRecordActivity.Refresh();
                   }
               }
           );
            frmActivityRecord.Show();
        }


        private void frmActivityRecordDashboard_Load(object sender, EventArgs e)
        {
            LoadData();
        }


        private void LoadData()
        {
            
            gvRecordActivity.Rows.Clear();
            if (!gvRecordActivity.Columns.Contains("Action"))
            {
                DataGridViewButtonColumn actionColumn = new DataGridViewButtonColumn
                {
                    HeaderText = "Action",
                    Name = "Action",
                    Text = "Edit/Delete",
                    UseColumnTextForButtonValue = false,
                    //Text = "Center"
                };

                gvRecordActivity.Columns.Add(actionColumn);
            }

            gvRecordActivity.AutoGenerateColumns = false;
            gvRecordActivity.AllowUserToAddRows = false;

            if (activitiesList.Any())
            {

                foreach (ActivityRecord record in activitiesList)
                {
                    gvRecordActivity.Rows.Add(
                        record.RecordId,
                        record.ActivityId,
                        record.UserId,
                        record.ActivityType,
                        record.Metric1,
                        record.Metric2,
                        record.Metric3,
                        record.CaloriesBurned,
                        record.CreatedAt.ToString("yyyy-MM-dd")
                    );
                }

                gvRecordActivity.CellPainting += gvRecordActivity_CellPainting;
            }


            gvRecordActivity.Refresh();



        }

        private void gvRecordActivity_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        {
            if (e.ColumnIndex == gvRecordActivity.Columns["Action"].Index && e.RowIndex >= 0)
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
                TextRenderer.DrawText(e.Graphics, "Edit", gvRecordActivity.Font, editButtonRect, Color.Black, TextFormatFlags.HorizontalCenter | TextFormatFlags.VerticalCenter);

                // Draw the "Delete" button
                ButtonRenderer.DrawButton(e.Graphics, deleteButtonRect, System.Windows.Forms.VisualStyles.PushButtonState.Normal);
                TextRenderer.DrawText(e.Graphics, "Delete", gvRecordActivity.Font, deleteButtonRect, Color.Black, TextFormatFlags.HorizontalCenter | TextFormatFlags.VerticalCenter);

                e.Handled = true; // Prevent default rendering
            }
        }



        private void txtSearch_TextChanged(object sender, EventArgs e)
        {

            string searchText = txtSearch.Text.Trim();
            if (string.IsNullOrEmpty(searchText))
            {
                LoadData();
                return;
            }


            List<ActivityRecord> filteredActivities = activitiesList
            .Where(g =>
                g.RecordId.ToString().Contains(searchText) ||
                g.ActivityId.ToString().Contains(searchText) ||
                g.UserId.ToString().Contains(searchText) ||
                g.ActivityType.ToString().ToLower().Contains(searchText.ToLower()) ||
                g.Metric1.ToString().ToLower().Contains(searchText.ToLower()) ||
                g.Metric2.ToString().ToLower().Contains(searchText.ToLower()) ||
                g.Metric3.ToString().ToLower().Contains(searchText.ToLower()) ||
                g.CreatedAt.ToString("yyyy-MM-dd").Contains(searchText)
             )
            .ToList();


            gvRecordActivity.Rows.Clear();

            foreach (ActivityRecord record in filteredActivities)
            {
                gvRecordActivity.Rows.Add(
                    record.RecordId,
                    record.ActivityId,
                    record.UserId,
                    record.ActivityType,
                    record.Metric1,
                    record.Metric2,
                    record.Metric3,
                    record.CaloriesBurned,
                    record.CreatedAt.ToString("yyyy-MM-dd")
                );
            }

            gvRecordActivity.Refresh();
        }


        private void gvRecordActivity_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0 || e.ColumnIndex < 0)
                return;

            DataGridViewRow selectedRow = gvRecordActivity.Rows[e.RowIndex];

            string recordId = selectedRow.Cells[0].Value?.ToString() ?? "";
            string totalCalories = selectedRow.Cells[7]?.Value?.ToString() ?? "";

            if (gvRecordActivity.Columns[e.ColumnIndex].Name == "Action")
            {

                Rectangle cellBounds = gvRecordActivity.GetCellDisplayRectangle(e.ColumnIndex, e.RowIndex, false);

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

                Point mousePosition = gvRecordActivity.PointToClient(Cursor.Position);

                if (editButtonRect.Contains(mousePosition))
                {
                    //Console.WriteLine("Edit button clicked");
                    frmActivityRecord frmRecordActivity = new frmActivityRecord(
                        (bool success) =>
                        {
                            if (success)
                            {

                                this.activitiesList = ActivityRecord.GetAll(user.UserId);
                                LoadData();
                                gvRecordActivity.Refresh();
                            }
                        },
                        int.Parse(recordId)
                     );


                    frmRecordActivity.Show();

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
                        ActivityRecord activity = new ActivityRecord();
                        if (activity.Delete(int.Parse(recordId)))
                        {
                            MessageBox.Show(
                                "Data deleted successfully!",
                                "Success",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Information
                            );


                            //User.UpdateTotalCaloriesBurnedByDecrease(user.UserId, float.Parse(totalCalories));
                            this.activitiesList = ActivityRecord.GetAll(user.UserId);

                            LoadData();
                        }

                    }
                }


            }
            else
            {
                frmActivityRecordDetails frmActivityRecordDetails = new frmActivityRecordDetails(selectedRow);
                frmActivityRecordDetails.ShowDialog();
            }
        }

        private void gvRecordActivity_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

            //DataGridViewRow selectedRow = gvRecordActivity.Rows[e.RowIndex];

            //frmActivityRecordDetails frmActivityRecordDetails = new frmActivityRecordDetails(selectedRow);
            //frmActivityRecordDetails.ShowDialog();
        }

        private void gvRecordActivity_CellContentClick_1(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
