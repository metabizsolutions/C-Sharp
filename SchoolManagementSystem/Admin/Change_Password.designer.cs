namespace SchoolManagementSystem.Admin
{
    partial class Change_Password
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Change_Password));
            this.label7 = new System.Windows.Forms.Label();
            this.pictureBox3 = new System.Windows.Forms.PictureBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.BtnChangePass = new DevExpress.XtraEditors.SimpleButton();
            this.OldPassword = new DevExpress.XtraEditors.TextEdit();
            this.NewPassword = new DevExpress.XtraEditors.TextEdit();
            this.lblfisrterror = new System.Windows.Forms.Label();
            this.lblsecerror = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.OldPassword.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.NewPassword.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Tahoma", 14.25F, System.Drawing.FontStyle.Bold);
            this.label7.Location = new System.Drawing.Point(171, 23);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(149, 23);
            this.label7.TabIndex = 61;
            this.label7.Text = "Employee Info";
            // 
            // pictureBox3
            // 
            this.pictureBox3.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox3.Image")));
            this.pictureBox3.Location = new System.Drawing.Point(100, 1);
            this.pictureBox3.Name = "pictureBox3";
            this.pictureBox3.Size = new System.Drawing.Size(65, 45);
            this.pictureBox3.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox3.TabIndex = 60;
            this.pictureBox3.TabStop = false;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(17, 119);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(77, 13);
            this.label2.TabIndex = 64;
            this.label2.Text = "New Password";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(22, 72);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(72, 13);
            this.label1.TabIndex = 62;
            this.label1.Text = "Old Password";
            // 
            // BtnChangePass
            // 
            this.BtnChangePass.Appearance.BorderColor = System.Drawing.Color.Purple;
            this.BtnChangePass.Appearance.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtnChangePass.Appearance.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.BtnChangePass.Appearance.Options.UseBorderColor = true;
            this.BtnChangePass.Appearance.Options.UseFont = true;
            this.BtnChangePass.Appearance.Options.UseForeColor = true;
            this.BtnChangePass.AppearanceHovered.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(255)))));
            this.BtnChangePass.AppearanceHovered.Options.UseBackColor = true;
            this.BtnChangePass.Cursor = System.Windows.Forms.Cursors.Hand;
            this.BtnChangePass.Enabled = false;
            this.BtnChangePass.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("BtnChangePass.ImageOptions.Image")));
            this.BtnChangePass.ImageOptions.Location = DevExpress.XtraEditors.ImageLocation.MiddleLeft;
            this.BtnChangePass.Location = new System.Drawing.Point(175, 155);
            this.BtnChangePass.Name = "BtnChangePass";
            this.BtnChangePass.Size = new System.Drawing.Size(94, 35);
            this.BtnChangePass.TabIndex = 66;
            this.BtnChangePass.Text = "Change";
            this.BtnChangePass.Click += new System.EventHandler(this.BtnChangePass_Click);
            // 
            // OldPassword
            // 
            this.OldPassword.EditValue = "";
            this.OldPassword.Location = new System.Drawing.Point(100, 69);
            this.OldPassword.Name = "OldPassword";
            this.OldPassword.Properties.Appearance.BackColor = System.Drawing.Color.Transparent;
            this.OldPassword.Properties.Appearance.BorderColor = System.Drawing.SystemColors.Highlight;
            this.OldPassword.Properties.Appearance.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.OldPassword.Properties.Appearance.Options.UseBackColor = true;
            this.OldPassword.Properties.Appearance.Options.UseBorderColor = true;
            this.OldPassword.Properties.Appearance.Options.UseFont = true;
            this.OldPassword.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.UltraFlat;
            this.OldPassword.Properties.PasswordChar = '•';
            this.OldPassword.Size = new System.Drawing.Size(270, 24);
            this.OldPassword.TabIndex = 67;
            this.OldPassword.Leave += new System.EventHandler(this.Password_Leave);
            // 
            // NewPassword
            // 
            this.NewPassword.Location = new System.Drawing.Point(100, 111);
            this.NewPassword.Name = "NewPassword";
            this.NewPassword.Properties.Appearance.BackColor = System.Drawing.Color.Transparent;
            this.NewPassword.Properties.Appearance.BorderColor = System.Drawing.SystemColors.Highlight;
            this.NewPassword.Properties.Appearance.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.NewPassword.Properties.Appearance.Options.UseBackColor = true;
            this.NewPassword.Properties.Appearance.Options.UseBorderColor = true;
            this.NewPassword.Properties.Appearance.Options.UseFont = true;
            this.NewPassword.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.UltraFlat;
            this.NewPassword.Properties.PasswordChar = '•';
            this.NewPassword.Size = new System.Drawing.Size(270, 24);
            this.NewPassword.TabIndex = 68;
            this.NewPassword.EditValueChanged += new System.EventHandler(this.NewPassword_EditValueChanged);
            // 
            // lblfisrterror
            // 
            this.lblfisrterror.AutoSize = true;
            this.lblfisrterror.ForeColor = System.Drawing.Color.Red;
            this.lblfisrterror.Location = new System.Drawing.Point(97, 96);
            this.lblfisrterror.Name = "lblfisrterror";
            this.lblfisrterror.Size = new System.Drawing.Size(71, 13);
            this.lblfisrterror.TabIndex = 69;
            this.lblfisrterror.Text = "FirstErrormsg";
            this.lblfisrterror.Visible = false;
            // 
            // lblsecerror
            // 
            this.lblsecerror.AutoSize = true;
            this.lblsecerror.ForeColor = System.Drawing.Color.Red;
            this.lblsecerror.Location = new System.Drawing.Point(97, 138);
            this.lblsecerror.Name = "lblsecerror";
            this.lblsecerror.Size = new System.Drawing.Size(85, 13);
            this.lblsecerror.TabIndex = 70;
            this.lblsecerror.Text = "SecondErrormsg";
            this.lblsecerror.Visible = false;
            // 
            // Change_Password
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(405, 202);
            this.Controls.Add(this.lblsecerror);
            this.Controls.Add(this.lblfisrterror);
            this.Controls.Add(this.NewPassword);
            this.Controls.Add(this.OldPassword);
            this.Controls.Add(this.BtnChangePass);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.pictureBox3);
            this.FormBorderEffect = DevExpress.XtraEditors.FormBorderEffect.Glow;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow;
            this.Name = "Change_Password";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Change Password";
            this.TopMost = true;
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.Change_Password_FormClosed);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.OldPassword.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.NewPassword.Properties)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.PictureBox pictureBox3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private DevExpress.XtraEditors.SimpleButton BtnChangePass;
        private DevExpress.XtraEditors.TextEdit OldPassword;
        private DevExpress.XtraEditors.TextEdit NewPassword;
        private System.Windows.Forms.Label lblfisrterror;
        private System.Windows.Forms.Label lblsecerror;
    }
}
