namespace SchoolManagementSystem
{
    partial class Login
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Login));
            this.txtPassword = new DevExpress.XtraEditors.TextEdit();
            this.labWorngPass = new DevExpress.XtraEditors.LabelControl();
            this.txtEmail = new DevExpress.XtraEditors.TextEdit();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            this.timer_bill = new System.Windows.Forms.Timer(this.components);
            this.panel1 = new System.Windows.Forms.Panel();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.btn_aboutus = new DevExpress.XtraEditors.SimpleButton();
            this.BtnPassword = new DevExpress.XtraEditors.SimpleButton();
            this.txtPartnar_title = new System.Windows.Forms.TextBox();
            this.txtpartnar_website = new System.Windows.Forms.TextBox();
            this.txtpartnar_discription = new System.Windows.Forms.RichTextBox();
            this.pictureBox_partner = new System.Windows.Forms.PictureBox();
            this.slider_box = new System.Windows.Forms.PictureBox();
            this.simpleButton1 = new DevExpress.XtraEditors.SimpleButton();
            ((System.ComponentModel.ISupportInitialize)(this.txtPassword.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtEmail.Properties)).BeginInit();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_partner)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.slider_box)).BeginInit();
            this.SuspendLayout();
            // 
            // txtPassword
            // 
            this.txtPassword.EditValue = "";
            this.txtPassword.Location = new System.Drawing.Point(146, 77);
            this.txtPassword.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.txtPassword.Name = "txtPassword";
            // 
            // 
            // 
            this.txtPassword.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple;
            this.txtPassword.Properties.PasswordChar = '*';
            this.txtPassword.Size = new System.Drawing.Size(280, 26);
            this.txtPassword.TabIndex = 1;
            this.txtPassword.ToolTipIconType = DevExpress.Utils.ToolTipIconType.Information;
            this.txtPassword.ToolTipTitle = "Password";
            this.txtPassword.Enter += new System.EventHandler(this.txtPassword_Enter);
            this.txtPassword.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtPassword_KeyDown);
            // 
            // labWorngPass
            // 
            this.labWorngPass.Appearance.ForeColor = System.Drawing.Color.DarkRed;
            this.labWorngPass.Appearance.Options.UseForeColor = true;
            this.labWorngPass.Location = new System.Drawing.Point(124, 5);
            this.labWorngPass.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.labWorngPass.Name = "labWorngPass";
            this.labWorngPass.Size = new System.Drawing.Size(195, 20);
            this.labWorngPass.TabIndex = 6;
            this.labWorngPass.Text = "Wrong password. Try again.";
            this.labWorngPass.Visible = false;
            // 
            // txtEmail
            // 
            this.txtEmail.EditValue = "";
            this.txtEmail.Location = new System.Drawing.Point(146, 34);
            this.txtEmail.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.txtEmail.Name = "txtEmail";
            // 
            // 
            // 
            this.txtEmail.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple;
            this.txtEmail.Size = new System.Drawing.Size(280, 26);
            this.txtEmail.TabIndex = 0;
            this.txtEmail.Enter += new System.EventHandler(this.txtEmail_Enter);
            this.txtEmail.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtEmail_KeyDown);
            // 
            // labelControl1
            // 
            this.labelControl1.Appearance.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelControl1.Appearance.ForeColor = System.Drawing.Color.Black;
            this.labelControl1.Appearance.Options.UseFont = true;
            this.labelControl1.Appearance.Options.UseForeColor = true;
            this.labelControl1.Location = new System.Drawing.Point(20, 34);
            this.labelControl1.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(97, 28);
            this.labelControl1.TabIndex = 4;
            this.labelControl1.Text = "Username:";
            // 
            // labelControl2
            // 
            this.labelControl2.Appearance.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelControl2.Appearance.ForeColor = System.Drawing.Color.Black;
            this.labelControl2.Appearance.Options.UseFont = true;
            this.labelControl2.Appearance.Options.UseForeColor = true;
            this.labelControl2.Location = new System.Drawing.Point(18, 77);
            this.labelControl2.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.labelControl2.Name = "labelControl2";
            this.labelControl2.Size = new System.Drawing.Size(97, 28);
            this.labelControl2.TabIndex = 5;
            this.labelControl2.Text = "Password :";
            // 
            // timer_bill
            // 
            this.timer_bill.Enabled = true;
            this.timer_bill.Interval = 2000;
            this.timer_bill.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.labWorngPass);
            this.panel1.Controls.Add(this.labelControl1);
            this.panel1.Controls.Add(this.pictureBox1);
            this.panel1.Controls.Add(this.btn_aboutus);
            this.panel1.Controls.Add(this.BtnPassword);
            this.panel1.Controls.Add(this.txtEmail);
            this.panel1.Controls.Add(this.txtPassword);
            this.panel1.Controls.Add(this.labelControl2);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel1.Location = new System.Drawing.Point(0, 453);
            this.panel1.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(854, 172);
            this.panel1.TabIndex = 11;
            this.panel1.Paint += new System.Windows.Forms.PaintEventHandler(this.panel1_Paint);
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackgroundImage = global::SchoolManagementSystem.Properties.Resources.ospsm;
            this.pictureBox1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
            this.pictureBox1.Location = new System.Drawing.Point(572, 5);
            this.pictureBox1.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(278, 163);
            this.pictureBox1.TabIndex = 8;
            this.pictureBox1.TabStop = false;
            // 
            // btn_aboutus
            // 
            this.btn_aboutus.Appearance.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.btn_aboutus.Appearance.Options.UseFont = true;
            this.btn_aboutus.ButtonStyle = DevExpress.XtraEditors.Controls.BorderStyles.Office2003;
            this.btn_aboutus.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("btn_aboutus.ImageOptions.Image")));
            this.btn_aboutus.Location = new System.Drawing.Point(435, 34);
            this.btn_aboutus.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.btn_aboutus.Name = "btn_aboutus";
            this.btn_aboutus.Size = new System.Drawing.Size(128, 35);
            this.btn_aboutus.TabIndex = 2;
            this.btn_aboutus.Text = "About Us";
            this.btn_aboutus.ToolTip = "login";
            this.btn_aboutus.Click += new System.EventHandler(this.btn_aboutus_Click);
            // 
            // BtnPassword
            // 
            this.BtnPassword.Appearance.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.BtnPassword.Appearance.Options.UseFont = true;
            this.BtnPassword.ButtonStyle = DevExpress.XtraEditors.Controls.BorderStyles.Office2003;
            this.BtnPassword.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("BtnPassword.ImageOptions.Image")));
            this.BtnPassword.Location = new System.Drawing.Point(146, 117);
            this.BtnPassword.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.BtnPassword.Name = "BtnPassword";
            this.BtnPassword.Size = new System.Drawing.Size(280, 35);
            this.BtnPassword.TabIndex = 2;
            this.BtnPassword.Text = "Login";
            this.BtnPassword.ToolTip = "login";
            this.BtnPassword.Click += new System.EventHandler(this.BtnPassword_Click);
            // 
            // txtPartnar_title
            // 
            this.txtPartnar_title.BackColor = System.Drawing.SystemColors.ControlLight;
            this.txtPartnar_title.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtPartnar_title.Font = new System.Drawing.Font("Microsoft Tai Le", 16F, System.Drawing.FontStyle.Bold);
            this.txtPartnar_title.Location = new System.Drawing.Point(6, 238);
            this.txtPartnar_title.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.txtPartnar_title.Name = "txtPartnar_title";
            this.txtPartnar_title.Size = new System.Drawing.Size(284, 41);
            this.txtPartnar_title.TabIndex = 43;
            this.txtPartnar_title.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // txtpartnar_website
            // 
            this.txtpartnar_website.BackColor = System.Drawing.SystemColors.ControlLight;
            this.txtpartnar_website.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtpartnar_website.Font = new System.Drawing.Font("Microsoft Tai Le", 16F, System.Drawing.FontStyle.Bold);
            this.txtpartnar_website.Location = new System.Drawing.Point(6, 291);
            this.txtpartnar_website.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.txtpartnar_website.Name = "txtpartnar_website";
            this.txtpartnar_website.Size = new System.Drawing.Size(284, 41);
            this.txtpartnar_website.TabIndex = 42;
            this.txtpartnar_website.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // txtpartnar_discription
            // 
            this.txtpartnar_discription.BackColor = System.Drawing.SystemColors.ControlLight;
            this.txtpartnar_discription.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtpartnar_discription.Font = new System.Drawing.Font("Microsoft Tai Le", 12F, System.Drawing.FontStyle.Bold);
            this.txtpartnar_discription.Location = new System.Drawing.Point(6, 343);
            this.txtpartnar_discription.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.txtpartnar_discription.Name = "txtpartnar_discription";
            this.txtpartnar_discription.Size = new System.Drawing.Size(843, 100);
            this.txtpartnar_discription.TabIndex = 41;
            this.txtpartnar_discription.Text = "";
            // 
            // pictureBox_partner
            // 
            this.pictureBox_partner.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pictureBox_partner.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox_partner.Image")));
            this.pictureBox_partner.Location = new System.Drawing.Point(26, 98);
            this.pictureBox_partner.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.pictureBox_partner.Name = "pictureBox_partner";
            this.pictureBox_partner.Size = new System.Drawing.Size(240, 131);
            this.pictureBox_partner.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox_partner.TabIndex = 44;
            this.pictureBox_partner.TabStop = false;
            // 
            // slider_box
            // 
            this.slider_box.Image = global::SchoolManagementSystem.Properties.Resources.softpich_logo;
            this.slider_box.Location = new System.Drawing.Point(298, 0);
            this.slider_box.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.slider_box.Name = "slider_box";
            this.slider_box.Size = new System.Drawing.Size(555, 334);
            this.slider_box.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.slider_box.TabIndex = 46;
            this.slider_box.TabStop = false;
            this.slider_box.MouseLeave += new System.EventHandler(this.slider_box_MouseLeave);
            this.slider_box.MouseHover += new System.EventHandler(this.slider_box_MouseHover);
            // 
            // simpleButton1
            // 
            this.simpleButton1.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("simpleButton1.ImageOptions.Image")));
            this.simpleButton1.Location = new System.Drawing.Point(801, 0);
            this.simpleButton1.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.simpleButton1.Name = "simpleButton1";
            this.simpleButton1.Size = new System.Drawing.Size(52, 52);
            this.simpleButton1.TabIndex = 47;
            this.simpleButton1.Click += new System.EventHandler(this.simpleButton1_Click);
            // 
            // Login
            // 
            this.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.Appearance.Options.UseBackColor = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(854, 625);
            this.Controls.Add(this.simpleButton1);
            this.Controls.Add(this.slider_box);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.txtPartnar_title);
            this.Controls.Add(this.pictureBox_partner);
            this.Controls.Add(this.txtpartnar_website);
            this.Controls.Add(this.txtpartnar_discription);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.IconOptions.Icon = ((System.Drawing.Icon)(resources.GetObject("Login.IconOptions.Icon")));
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Name = "Login";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Login";
            this.Load += new System.EventHandler(this.Login_Load);
            ((System.ComponentModel.ISupportInitialize)(this.txtPassword.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtEmail.Properties)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_partner)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.slider_box)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraEditors.TextEdit txtPassword;
        private DevExpress.XtraEditors.LabelControl labWorngPass;
        private DevExpress.XtraEditors.SimpleButton BtnPassword;
        private DevExpress.XtraEditors.TextEdit txtEmail;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.XtraEditors.LabelControl labelControl2;
        private System.Windows.Forms.Timer timer_bill;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.TextBox txtPartnar_title;
        private System.Windows.Forms.TextBox txtpartnar_website;
        private System.Windows.Forms.RichTextBox txtpartnar_discription;
        private System.Windows.Forms.PictureBox pictureBox_partner;
        private System.Windows.Forms.PictureBox slider_box;
        private DevExpress.XtraEditors.SimpleButton simpleButton1;
        private DevExpress.XtraEditors.SimpleButton btn_aboutus;
    }
}

