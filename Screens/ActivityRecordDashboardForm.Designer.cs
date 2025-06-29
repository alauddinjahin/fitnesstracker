namespace _216678_FitnessTracker.Screens
{
    partial class frmActivityRecordDashboard
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            this.lblSearch = new System.Windows.Forms.Label();
            this.txtSearch = new System.Windows.Forms.TextBox();
            this.btnAddNew = new System.Windows.Forms.Button();
            this.gvRecordActivity = new System.Windows.Forms.DataGridView();
            this.colRecordId = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colActivityId = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colUserId = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colActivityType = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colMetric1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colMetric2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colMetric3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colCaloriesBurned = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colCreatedAt = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.gvRecordActivity)).BeginInit();
            this.SuspendLayout();
            // 
            // lblSearch
            // 
            this.lblSearch.AutoSize = true;
            this.lblSearch.Location = new System.Drawing.Point(180, 45);
            this.lblSearch.Name = "lblSearch";
            this.lblSearch.Padding = new System.Windows.Forms.Padding(10, 5, 10, 5);
            this.lblSearch.Size = new System.Drawing.Size(73, 26);
            this.lblSearch.TabIndex = 11;
            this.lblSearch.Text = "Search:";
            // 
            // txtSearch
            // 
            this.txtSearch.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtSearch.Location = new System.Drawing.Point(273, 45);
            this.txtSearch.Name = "txtSearch";
            this.txtSearch.Size = new System.Drawing.Size(782, 27);
            this.txtSearch.TabIndex = 10;
            this.txtSearch.TextChanged += new System.EventHandler(this.txtSearch_TextChanged);
            // 
            // btnAddNew
            // 
            this.btnAddNew.Location = new System.Drawing.Point(56, 43);
            this.btnAddNew.Name = "btnAddNew";
            this.btnAddNew.Size = new System.Drawing.Size(88, 26);
            this.btnAddNew.TabIndex = 9;
            this.btnAddNew.Text = "Add New";
            this.btnAddNew.UseVisualStyleBackColor = true;
            this.btnAddNew.Click += new System.EventHandler(this.btnAddNew_Click);
            // 
            // gvRecordActivity
            // 
            this.gvRecordActivity.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.gvRecordActivity.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colRecordId,
            this.colActivityId,
            this.colUserId,
            this.colActivityType,
            this.colMetric1,
            this.colMetric2,
            this.colMetric3,
            this.colCaloriesBurned,
            this.colCreatedAt});
            this.gvRecordActivity.Location = new System.Drawing.Point(56, 84);
            this.gvRecordActivity.Name = "gvRecordActivity";
            this.gvRecordActivity.RowHeadersWidth = 51;
            this.gvRecordActivity.RowTemplate.Height = 24;
            this.gvRecordActivity.Size = new System.Drawing.Size(999, 308);
            this.gvRecordActivity.TabIndex = 8;
            this.gvRecordActivity.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.gvRecordActivity_CellClick);
            this.gvRecordActivity.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.gvRecordActivity_CellContentClick_1);
            this.gvRecordActivity.CellContentDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.gvRecordActivity_CellContentClick);
            // 
            // colRecordId
            // 
            this.colRecordId.Frozen = true;
            this.colRecordId.HeaderText = "Record ID";
            this.colRecordId.MinimumWidth = 6;
            this.colRecordId.Name = "colRecordId";
            this.colRecordId.Width = 125;
            // 
            // colActivityId
            // 
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.colActivityId.DefaultCellStyle = dataGridViewCellStyle1;
            this.colActivityId.HeaderText = "Activity ID";
            this.colActivityId.MinimumWidth = 6;
            this.colActivityId.Name = "colActivityId";
            this.colActivityId.Visible = false;
            this.colActivityId.Width = 150;
            // 
            // colUserId
            // 
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.colUserId.DefaultCellStyle = dataGridViewCellStyle2;
            this.colUserId.HeaderText = "User ID";
            this.colUserId.MinimumWidth = 6;
            this.colUserId.Name = "colUserId";
            this.colUserId.Width = 150;
            // 
            // colActivityType
            // 
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.colActivityType.DefaultCellStyle = dataGridViewCellStyle3;
            this.colActivityType.HeaderText = "ActivityType";
            this.colActivityType.MinimumWidth = 6;
            this.colActivityType.Name = "colActivityType";
            this.colActivityType.Width = 200;
            // 
            // colMetric1
            // 
            this.colMetric1.HeaderText = "Metric1";
            this.colMetric1.MinimumWidth = 6;
            this.colMetric1.Name = "colMetric1";
            this.colMetric1.Width = 125;
            // 
            // colMetric2
            // 
            this.colMetric2.HeaderText = "Metric2";
            this.colMetric2.MinimumWidth = 6;
            this.colMetric2.Name = "colMetric2";
            this.colMetric2.Width = 125;
            // 
            // colMetric3
            // 
            this.colMetric3.HeaderText = "Metric3";
            this.colMetric3.MinimumWidth = 6;
            this.colMetric3.Name = "colMetric3";
            this.colMetric3.Width = 125;
            // 
            // colCaloriesBurned
            // 
            this.colCaloriesBurned.HeaderText = "Calories Burned";
            this.colCaloriesBurned.MinimumWidth = 6;
            this.colCaloriesBurned.Name = "colCaloriesBurned";
            this.colCaloriesBurned.Width = 125;
            // 
            // colCreatedAt
            // 
            this.colCreatedAt.HeaderText = "Activity Date";
            this.colCreatedAt.MinimumWidth = 6;
            this.colCreatedAt.Name = "colCreatedAt";
            this.colCreatedAt.Width = 125;
            // 
            // frmActivityRecordDashboard
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1110, 498);
            this.Controls.Add(this.lblSearch);
            this.Controls.Add(this.txtSearch);
            this.Controls.Add(this.btnAddNew);
            this.Controls.Add(this.gvRecordActivity);
            this.Name = "frmActivityRecordDashboard";
            this.Text = "Activity Record Dashboard";
            this.Load += new System.EventHandler(this.frmActivityRecordDashboard_Load);
            ((System.ComponentModel.ISupportInitialize)(this.gvRecordActivity)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblSearch;
        private System.Windows.Forms.TextBox txtSearch;
        private System.Windows.Forms.Button btnAddNew;
        private System.Windows.Forms.DataGridView gvRecordActivity;
        private System.Windows.Forms.DataGridViewTextBoxColumn colRecordId;
        private System.Windows.Forms.DataGridViewTextBoxColumn colActivityId;
        private System.Windows.Forms.DataGridViewTextBoxColumn colUserId;
        private System.Windows.Forms.DataGridViewTextBoxColumn colActivityType;
        private System.Windows.Forms.DataGridViewTextBoxColumn colMetric1;
        private System.Windows.Forms.DataGridViewTextBoxColumn colMetric2;
        private System.Windows.Forms.DataGridViewTextBoxColumn colMetric3;
        private System.Windows.Forms.DataGridViewTextBoxColumn colCaloriesBurned;
        private System.Windows.Forms.DataGridViewTextBoxColumn colCreatedAt;
    }
}