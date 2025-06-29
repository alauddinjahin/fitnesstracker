namespace _216678_FitnessTracker.Screens
{
    partial class frmEditPassword
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
            this.btnUpdatePass = new System.Windows.Forms.Button();
            this.txtNewPass = new System.Windows.Forms.TextBox();
            this.lblUserId = new System.Windows.Forms.Label();
            this.txtUserId = new System.Windows.Forms.TextBox();
            this.lblActivityDetailsHeadingP2 = new System.Windows.Forms.Label();
            this.lblActivityDetailsHeadingP1 = new System.Windows.Forms.Label();
            this.lblNewPass = new System.Windows.Forms.Label();
            this.lblConfirmNewPass = new System.Windows.Forms.Label();
            this.txtConfirmNewPass = new System.Windows.Forms.TextBox();
            this.pbProfile = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.pbProfile)).BeginInit();
            this.SuspendLayout();
            // 
            // btnUpdatePass
            // 
            this.btnUpdatePass.BackColor = System.Drawing.Color.Brown;
            this.btnUpdatePass.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.btnUpdatePass.FlatAppearance.BorderSize = 0;
            this.btnUpdatePass.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnUpdatePass.ForeColor = System.Drawing.Color.White;
            this.btnUpdatePass.Location = new System.Drawing.Point(368, 260);
            this.btnUpdatePass.Name = "btnUpdatePass";
            this.btnUpdatePass.Size = new System.Drawing.Size(134, 35);
            this.btnUpdatePass.TabIndex = 107;
            this.btnUpdatePass.Text = "Update";
            this.btnUpdatePass.UseVisualStyleBackColor = false;
            this.btnUpdatePass.Click += new System.EventHandler(this.btnUpdatePass_Click);
            // 
            // txtNewPass
            // 
            this.txtNewPass.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtNewPass.Location = new System.Drawing.Point(368, 160);
            this.txtNewPass.Name = "txtNewPass";
            this.txtNewPass.Size = new System.Drawing.Size(342, 30);
            this.txtNewPass.TabIndex = 0;
            this.txtNewPass.UseSystemPasswordChar = true;
            this.txtNewPass.TextChanged += new System.EventHandler(this.txtNewPass_TextChanged);
            // 
            // lblUserId
            // 
            this.lblUserId.AutoSize = true;
            this.lblUserId.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblUserId.Location = new System.Drawing.Point(162, 113);
            this.lblUserId.Name = "lblUserId";
            this.lblUserId.Size = new System.Drawing.Size(67, 20);
            this.lblUserId.TabIndex = 116;
            this.lblUserId.Text = "User ID";
            // 
            // txtUserId
            // 
            this.txtUserId.AllowDrop = true;
            this.txtUserId.Enabled = false;
            this.txtUserId.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtUserId.Location = new System.Drawing.Point(370, 112);
            this.txtUserId.Name = "txtUserId";
            this.txtUserId.ReadOnly = true;
            this.txtUserId.Size = new System.Drawing.Size(342, 30);
            this.txtUserId.TabIndex = 115;
            // 
            // lblActivityDetailsHeadingP2
            // 
            this.lblActivityDetailsHeadingP2.AutoSize = true;
            this.lblActivityDetailsHeadingP2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblActivityDetailsHeadingP2.Location = new System.Drawing.Point(441, 60);
            this.lblActivityDetailsHeadingP2.Name = "lblActivityDetailsHeadingP2";
            this.lblActivityDetailsHeadingP2.Size = new System.Drawing.Size(119, 25);
            this.lblActivityDetailsHeadingP2.TabIndex = 114;
            this.lblActivityDetailsHeadingP2.Text = "Information";
            // 
            // lblActivityDetailsHeadingP1
            // 
            this.lblActivityDetailsHeadingP1.AutoSize = true;
            this.lblActivityDetailsHeadingP1.Font = new System.Drawing.Font("Microsoft Sans Serif", 16.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblActivityDetailsHeadingP1.Location = new System.Drawing.Point(440, 28);
            this.lblActivityDetailsHeadingP1.Name = "lblActivityDetailsHeadingP1";
            this.lblActivityDetailsHeadingP1.Size = new System.Drawing.Size(77, 32);
            this.lblActivityDetailsHeadingP1.TabIndex = 113;
            this.lblActivityDetailsHeadingP1.Text = "User";
            // 
            // lblNewPass
            // 
            this.lblNewPass.AutoSize = true;
            this.lblNewPass.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblNewPass.Location = new System.Drawing.Point(160, 166);
            this.lblNewPass.Name = "lblNewPass";
            this.lblNewPass.Size = new System.Drawing.Size(121, 20);
            this.lblNewPass.TabIndex = 110;
            this.lblNewPass.Text = "New Password";
            // 
            // lblConfirmNewPass
            // 
            this.lblConfirmNewPass.AutoSize = true;
            this.lblConfirmNewPass.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblConfirmNewPass.Location = new System.Drawing.Point(160, 208);
            this.lblConfirmNewPass.Name = "lblConfirmNewPass";
            this.lblConfirmNewPass.Size = new System.Drawing.Size(185, 20);
            this.lblConfirmNewPass.TabIndex = 108;
            this.lblConfirmNewPass.Text = "Confirm New Password";
            // 
            // txtConfirmNewPass
            // 
            this.txtConfirmNewPass.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtConfirmNewPass.Location = new System.Drawing.Point(368, 207);
            this.txtConfirmNewPass.Name = "txtConfirmNewPass";
            this.txtConfirmNewPass.Size = new System.Drawing.Size(342, 30);
            this.txtConfirmNewPass.TabIndex = 103;
            this.txtConfirmNewPass.UseSystemPasswordChar = true;
            this.txtConfirmNewPass.TextChanged += new System.EventHandler(this.txtConfirmNewPass_TextChanged);
            // 
            // pbProfile
            // 
            this.pbProfile.Cursor = System.Windows.Forms.Cursors.Hand;
            this.pbProfile.Enabled = false;
            this.pbProfile.Image = global::_216678_FitnessTracker.Properties.Resources.profile__1_;
            this.pbProfile.Location = new System.Drawing.Point(368, 28);
            this.pbProfile.Name = "pbProfile";
            this.pbProfile.Size = new System.Drawing.Size(61, 57);
            this.pbProfile.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pbProfile.TabIndex = 112;
            this.pbProfile.TabStop = false;
            // 
            // frmEditPassword
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(871, 450);
            this.Controls.Add(this.btnUpdatePass);
            this.Controls.Add(this.txtNewPass);
            this.Controls.Add(this.lblUserId);
            this.Controls.Add(this.txtUserId);
            this.Controls.Add(this.lblActivityDetailsHeadingP2);
            this.Controls.Add(this.lblActivityDetailsHeadingP1);
            this.Controls.Add(this.pbProfile);
            this.Controls.Add(this.lblNewPass);
            this.Controls.Add(this.lblConfirmNewPass);
            this.Controls.Add(this.txtConfirmNewPass);
            this.Name = "frmEditPassword";
            this.Text = "EditPasswordForm";
            this.Load += new System.EventHandler(this.frmEditPassword_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pbProfile)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnUpdatePass;
        private System.Windows.Forms.TextBox txtNewPass;
        private System.Windows.Forms.Label lblUserId;
        private System.Windows.Forms.TextBox txtUserId;
        private System.Windows.Forms.Label lblActivityDetailsHeadingP2;
        private System.Windows.Forms.Label lblActivityDetailsHeadingP1;
        private System.Windows.Forms.PictureBox pbProfile;
        private System.Windows.Forms.Label lblNewPass;
        private System.Windows.Forms.Label lblConfirmNewPass;
        private System.Windows.Forms.TextBox txtConfirmNewPass;
    }
}