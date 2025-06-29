namespace _216678_FitnessTracker.Screens
{
    partial class frmActivitiesMatricsDashboard
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmActivitiesMatricsDashboard));
            this.lblSearch = new System.Windows.Forms.Label();
            this.txtSearch = new System.Windows.Forms.TextBox();
            this.gvActivitiesMetrics = new System.Windows.Forms.DataGridView();
            this.colActivityId = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colActivityName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colMetric1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colMetric2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colMetric3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.btnAddNew = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.gvActivitiesMetrics)).BeginInit();
            this.SuspendLayout();
            // 
            // lblSearch
            // 
            this.lblSearch.AutoSize = true;
            this.lblSearch.Location = new System.Drawing.Point(158, 39);
            this.lblSearch.Name = "lblSearch";
            this.lblSearch.Padding = new System.Windows.Forms.Padding(10, 5, 10, 5);
            this.lblSearch.Size = new System.Drawing.Size(73, 26);
            this.lblSearch.TabIndex = 10;
            this.lblSearch.Text = "Search:";
            // 
            // txtSearch
            // 
            this.txtSearch.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtSearch.Location = new System.Drawing.Point(247, 38);
            this.txtSearch.Name = "txtSearch";
            this.txtSearch.Size = new System.Drawing.Size(790, 27);
            this.txtSearch.TabIndex = 9;
            this.txtSearch.TextChanged += new System.EventHandler(this.txtSearch_TextChanged);
            // 
            // gvActivitiesMetrics
            // 
            this.gvActivitiesMetrics.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.gvActivitiesMetrics.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colActivityId,
            this.colActivityName,
            this.colMetric1,
            this.colMetric2,
            this.colMetric3});
            this.gvActivitiesMetrics.Location = new System.Drawing.Point(49, 77);
            this.gvActivitiesMetrics.Name = "gvActivitiesMetrics";
            this.gvActivitiesMetrics.RowHeadersWidth = 51;
            this.gvActivitiesMetrics.RowTemplate.Height = 24;
            this.gvActivitiesMetrics.Size = new System.Drawing.Size(988, 308);
            this.gvActivitiesMetrics.TabIndex = 8;
            this.gvActivitiesMetrics.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.gvActivitiesMetrics_CellContentClick);
            // 
            // colActivityId
            // 
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.colActivityId.DefaultCellStyle = dataGridViewCellStyle1;
            this.colActivityId.Frozen = true;
            this.colActivityId.HeaderText = "Activity ID";
            this.colActivityId.MinimumWidth = 6;
            this.colActivityId.Name = "colActivityId";
            this.colActivityId.Width = 150;
            // 
            // colActivityName
            // 
            this.colActivityName.HeaderText = "Activity Type";
            this.colActivityName.MinimumWidth = 6;
            this.colActivityName.Name = "colActivityName";
            this.colActivityName.Width = 120;
            // 
            // colMetric1
            // 
            this.colMetric1.HeaderText = "Metric1";
            this.colMetric1.MinimumWidth = 6;
            this.colMetric1.Name = "colMetric1";
            this.colMetric1.Width = 200;
            // 
            // colMetric2
            // 
            this.colMetric2.HeaderText = "Metric2";
            this.colMetric2.MinimumWidth = 6;
            this.colMetric2.Name = "colMetric2";
            this.colMetric2.Width = 200;
            // 
            // colMetric3
            // 
            this.colMetric3.HeaderText = "Metric3";
            this.colMetric3.MinimumWidth = 6;
            this.colMetric3.Name = "colMetric3";
            this.colMetric3.Width = 200;
            // 
            // btnAddNew
            // 
            this.btnAddNew.Location = new System.Drawing.Point(49, 38);
            this.btnAddNew.Name = "btnAddNew";
            this.btnAddNew.Size = new System.Drawing.Size(88, 26);
            this.btnAddNew.TabIndex = 11;
            this.btnAddNew.Text = "Add New";
            this.btnAddNew.UseVisualStyleBackColor = true;
            this.btnAddNew.Click += new System.EventHandler(this.btnAddNew_Click);
            // 
            // frmActivitiesMatricsDashboard
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1096, 434);
            this.Controls.Add(this.btnAddNew);
            this.Controls.Add(this.lblSearch);
            this.Controls.Add(this.txtSearch);
            this.Controls.Add(this.gvActivitiesMetrics);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "frmActivitiesMatricsDashboard";
            this.Text = "Activities & Metrics";
            this.Load += new System.EventHandler(this.frmActivitiesMatrics_Load);
            ((System.ComponentModel.ISupportInitialize)(this.gvActivitiesMetrics)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblSearch;
        private System.Windows.Forms.TextBox txtSearch;
        private System.Windows.Forms.DataGridView gvActivitiesMetrics;
        private System.Windows.Forms.DataGridViewTextBoxColumn colActivityId;
        private System.Windows.Forms.DataGridViewTextBoxColumn colActivityName;
        private System.Windows.Forms.DataGridViewTextBoxColumn colMetric1;
        private System.Windows.Forms.DataGridViewTextBoxColumn colMetric2;
        private System.Windows.Forms.DataGridViewTextBoxColumn colMetric3;
        private System.Windows.Forms.Button btnAddNew;
    }
}