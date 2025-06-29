namespace _216678_FitnessTracker.Screens
{
    partial class frmFitnessGoals
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
            this.btnSubmitGoalCalories = new System.Windows.Forms.Button();
            this.lblGoalCalories = new System.Windows.Forms.Label();
            this.txtGoalCalories = new System.Windows.Forms.TextBox();
            this.lblUserId = new System.Windows.Forms.Label();
            this.cmbUserId = new System.Windows.Forms.ComboBox();
            this.SuspendLayout();
            // 
            // btnSubmitGoalCalories
            // 
            this.btnSubmitGoalCalories.BackColor = System.Drawing.Color.Brown;
            this.btnSubmitGoalCalories.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.btnSubmitGoalCalories.FlatAppearance.BorderSize = 0;
            this.btnSubmitGoalCalories.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSubmitGoalCalories.ForeColor = System.Drawing.Color.White;
            this.btnSubmitGoalCalories.Location = new System.Drawing.Point(175, 149);
            this.btnSubmitGoalCalories.Name = "btnSubmitGoalCalories";
            this.btnSubmitGoalCalories.Size = new System.Drawing.Size(134, 35);
            this.btnSubmitGoalCalories.TabIndex = 17;
            this.btnSubmitGoalCalories.Text = "Submit";
            this.btnSubmitGoalCalories.UseVisualStyleBackColor = false;
            this.btnSubmitGoalCalories.Click += new System.EventHandler(this.btnSubmitGoalCalories_Click);
            // 
            // lblGoalCalories
            // 
            this.lblGoalCalories.AutoSize = true;
            this.lblGoalCalories.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblGoalCalories.Location = new System.Drawing.Point(51, 101);
            this.lblGoalCalories.Name = "lblGoalCalories";
            this.lblGoalCalories.Size = new System.Drawing.Size(111, 20);
            this.lblGoalCalories.TabIndex = 15;
            this.lblGoalCalories.Text = "Goal Calories";
            // 
            // txtGoalCalories
            // 
            this.txtGoalCalories.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtGoalCalories.Location = new System.Drawing.Point(175, 99);
            this.txtGoalCalories.Name = "txtGoalCalories";
            this.txtGoalCalories.Size = new System.Drawing.Size(342, 30);
            this.txtGoalCalories.TabIndex = 2;
            // 
            // lblUserId
            // 
            this.lblUserId.AutoSize = true;
            this.lblUserId.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblUserId.Location = new System.Drawing.Point(48, 59);
            this.lblUserId.Name = "lblUserId";
            this.lblUserId.Size = new System.Drawing.Size(45, 20);
            this.lblUserId.TabIndex = 14;
            this.lblUserId.Text = "User";
            this.lblUserId.UseMnemonic = false;
            // 
            // cmbUserId
            // 
            this.cmbUserId.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbUserId.FormattingEnabled = true;
            this.cmbUserId.Location = new System.Drawing.Point(175, 54);
            this.cmbUserId.Name = "cmbUserId";
            this.cmbUserId.Size = new System.Drawing.Size(342, 33);
            this.cmbUserId.TabIndex = 1;
            // 
            // frmFitnessGoals
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(588, 247);
            this.Controls.Add(this.cmbUserId);
            this.Controls.Add(this.btnSubmitGoalCalories);
            this.Controls.Add(this.lblGoalCalories);
            this.Controls.Add(this.txtGoalCalories);
            this.Controls.Add(this.lblUserId);
            this.Name = "frmFitnessGoals";
            this.Text = "Fitness Goals";
            this.Load += new System.EventHandler(this.FitnessGoalsFormcs_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnSubmitGoalCalories;
        private System.Windows.Forms.Label lblGoalCalories;
        private System.Windows.Forms.TextBox txtGoalCalories;
        private System.Windows.Forms.Label lblUserId;
        private System.Windows.Forms.ComboBox cmbUserId;
    }
}