namespace _216678_FitnessTracker.Screens
{
    partial class frmFitnessGoalsDashboard
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
            this.gvGoals = new System.Windows.Forms.DataGridView();
            this.colGoalId = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colUserId = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colGoalCalories = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colSetDate = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.btnAddNew = new System.Windows.Forms.Button();
            this.txtSearch = new System.Windows.Forms.TextBox();
            this.lblSearch = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.gvGoals)).BeginInit();
            this.SuspendLayout();
            // 
            // gvGoals
            // 
            this.gvGoals.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.gvGoals.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colGoalId,
            this.colUserId,
            this.colGoalCalories,
            this.colSetDate});
            this.gvGoals.Location = new System.Drawing.Point(52, 82);
            this.gvGoals.Name = "gvGoals";
            this.gvGoals.RowHeadersWidth = 51;
            this.gvGoals.RowTemplate.Height = 24;
            this.gvGoals.Size = new System.Drawing.Size(999, 308);
            this.gvGoals.TabIndex = 0;
            this.gvGoals.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.gvGoals_CellClick);
            this.gvGoals.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.gvGoals_CellContentClick);
            this.gvGoals.CellContentDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellContentClick);
            // 
            // colGoalId
            // 
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.colGoalId.DefaultCellStyle = dataGridViewCellStyle1;
            this.colGoalId.Frozen = true;
            this.colGoalId.HeaderText = "Goal ID";
            this.colGoalId.MinimumWidth = 6;
            this.colGoalId.Name = "colGoalId";
            this.colGoalId.Width = 150;
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
            // colGoalCalories
            // 
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.colGoalCalories.DefaultCellStyle = dataGridViewCellStyle3;
            this.colGoalCalories.HeaderText = "Goal Calories";
            this.colGoalCalories.MinimumWidth = 6;
            this.colGoalCalories.Name = "colGoalCalories";
            this.colGoalCalories.Width = 200;
            // 
            // colSetDate
            // 
            this.colSetDate.HeaderText = "SetDate";
            this.colSetDate.MinimumWidth = 6;
            this.colSetDate.Name = "colSetDate";
            this.colSetDate.Width = 125;
            // 
            // btnAddNew
            // 
            this.btnAddNew.Location = new System.Drawing.Point(52, 41);
            this.btnAddNew.Name = "btnAddNew";
            this.btnAddNew.Size = new System.Drawing.Size(88, 26);
            this.btnAddNew.TabIndex = 1;
            this.btnAddNew.Text = "Add New";
            this.btnAddNew.UseVisualStyleBackColor = true;
            this.btnAddNew.Click += new System.EventHandler(this.btnAddNew_Click);
            // 
            // txtSearch
            // 
            this.txtSearch.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtSearch.Location = new System.Drawing.Point(269, 43);
            this.txtSearch.Name = "txtSearch";
            this.txtSearch.Size = new System.Drawing.Size(782, 27);
            this.txtSearch.TabIndex = 2;
            this.txtSearch.TextChanged += new System.EventHandler(this.txtSearch_TextChanged);
            // 
            // lblSearch
            // 
            this.lblSearch.AutoSize = true;
            this.lblSearch.Location = new System.Drawing.Point(176, 43);
            this.lblSearch.Name = "lblSearch";
            this.lblSearch.Padding = new System.Windows.Forms.Padding(10, 5, 10, 5);
            this.lblSearch.Size = new System.Drawing.Size(73, 26);
            this.lblSearch.TabIndex = 3;
            this.lblSearch.Text = "Search:";
            // 
            // frmFitnessGoalsDashboard
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1108, 507);
            this.Controls.Add(this.lblSearch);
            this.Controls.Add(this.txtSearch);
            this.Controls.Add(this.btnAddNew);
            this.Controls.Add(this.gvGoals);
            this.Name = "frmFitnessGoalsDashboard";
            this.Text = "Fitness Goals Dashboard";
            this.Load += new System.EventHandler(this.frmFitnessGoalsDashboard_Load);
            ((System.ComponentModel.ISupportInitialize)(this.gvGoals)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView gvGoals;
        private System.Windows.Forms.Button btnAddNew;
        private System.Windows.Forms.TextBox txtSearch;
        private System.Windows.Forms.Label lblSearch;
        private System.Windows.Forms.DataGridViewTextBoxColumn colGoalId;
        private System.Windows.Forms.DataGridViewTextBoxColumn colUserId;
        private System.Windows.Forms.DataGridViewTextBoxColumn colGoalCalories;
        private System.Windows.Forms.DataGridViewTextBoxColumn colSetDate;
    }
}