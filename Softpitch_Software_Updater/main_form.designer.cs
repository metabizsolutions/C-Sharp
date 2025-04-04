
namespace Softpitch_Software_Updater
{
    partial class main_form
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(main_form));
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.label1 = new System.Windows.Forms.Label();
            this.notifyIcon1 = new System.Windows.Forms.NotifyIcon(this.components);
            this.lblinfo = new System.Windows.Forms.Label();
            this.btn_download_software = new DevExpress.XtraEditors.SimpleButton();
            this.label4 = new System.Windows.Forms.Label();
            this.lbl_inernet_speed = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.flowLayoutPanel4 = new System.Windows.Forms.FlowLayoutPanel();
            this.label5 = new System.Windows.Forms.Label();
            this.lbl_total_files = new System.Windows.Forms.Label();
            this.flowLayoutPanel3 = new System.Windows.Forms.FlowLayoutPanel();
            this.label3 = new System.Windows.Forms.Label();
            this.lbl_file_downloaded = new System.Windows.Forms.Label();
            this.progressBarControl1 = new DevExpress.XtraEditors.ProgressBarControl();
            this.progressBarControl2 = new DevExpress.XtraEditors.ProgressBarControl();
            this.flowLayoutPanel2 = new System.Windows.Forms.FlowLayoutPanel();
            this.label2 = new System.Windows.Forms.Label();
            this.lbl_installing_info = new System.Windows.Forms.Label();
            this.imageSlider1 = new DevExpress.XtraEditors.Controls.ImageSlider();
            this.btn_settings = new DevExpress.XtraEditors.SimpleButton();
            this.btn_close_form = new DevExpress.XtraEditors.SimpleButton();
            this.change_type = new System.Windows.Forms.ComboBox();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.panel2.SuspendLayout();
            this.flowLayoutPanel4.SuspendLayout();
            this.flowLayoutPanel3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.progressBarControl1.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.progressBarControl2.Properties)).BeginInit();
            this.flowLayoutPanel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.imageSlider1)).BeginInit();
            this.SuspendLayout();
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
            this.pictureBox1.Location = new System.Drawing.Point(177, 12);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(97, 76);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox1.TabIndex = 0;
            this.pictureBox1.TabStop = false;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F);
            this.label1.Location = new System.Drawing.Point(280, 12);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(334, 26);
            this.label1.TabIndex = 1;
            this.label1.Text = "Softpitch Software House Layyah";
            // 
            // notifyIcon1
            // 
            this.notifyIcon1.BalloonTipTitle = "Softpitch Software Updater";
            this.notifyIcon1.Icon = ((System.Drawing.Icon)(resources.GetObject("notifyIcon1.Icon")));
            this.notifyIcon1.Text = "Softpitch Software Updater";
            this.notifyIcon1.Visible = true;
            this.notifyIcon1.Click += new System.EventHandler(this.notifyIcon1_Click);
            // 
            // lblinfo
            // 
            this.lblinfo.AutoSize = true;
            this.lblinfo.BackColor = System.Drawing.Color.Transparent;
            this.lblinfo.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F);
            this.lblinfo.ForeColor = System.Drawing.Color.Green;
            this.lblinfo.Location = new System.Drawing.Point(302, 38);
            this.lblinfo.Name = "lblinfo";
            this.lblinfo.Size = new System.Drawing.Size(279, 24);
            this.lblinfo.TabIndex = 5;
            this.lblinfo.Text = "We are updateing your Software";
            // 
            // btn_download_software
            // 
            this.btn_download_software.Appearance.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_download_software.Appearance.Options.UseFont = true;
            this.btn_download_software.ImageOptions.SvgImage = ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("btn_download_software.ImageOptions.SvgImage")));
            this.btn_download_software.Location = new System.Drawing.Point(407, 63);
            this.btn_download_software.Name = "btn_download_software";
            this.btn_download_software.Size = new System.Drawing.Size(231, 23);
            this.btn_download_software.TabIndex = 7;
            this.btn_download_software.Text = "Start Download Software";
            this.btn_download_software.Click += new System.EventHandler(this.btn_download_software_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.BackColor = System.Drawing.Color.Transparent;
            this.label4.Font = new System.Drawing.Font("Microsoft Tai Le", 10F, System.Drawing.FontStyle.Bold);
            this.label4.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.label4.Location = new System.Drawing.Point(286, 5);
            this.label4.Margin = new System.Windows.Forms.Padding(5, 5, 5, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(110, 18);
            this.label4.TabIndex = 13;
            this.label4.Text = ":Internet Speed";
            // 
            // lbl_inernet_speed
            // 
            this.lbl_inernet_speed.AutoSize = true;
            this.lbl_inernet_speed.BackColor = System.Drawing.Color.Transparent;
            this.flowLayoutPanel4.SetFlowBreak(this.lbl_inernet_speed, true);
            this.lbl_inernet_speed.Font = new System.Drawing.Font("Microsoft Tai Le", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_inernet_speed.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.lbl_inernet_speed.Location = new System.Drawing.Point(217, 5);
            this.lbl_inernet_speed.Margin = new System.Windows.Forms.Padding(5, 5, 5, 0);
            this.lbl_inernet_speed.Name = "lbl_inernet_speed";
            this.lbl_inernet_speed.Size = new System.Drawing.Size(59, 21);
            this.lbl_inernet_speed.TabIndex = 14;
            this.lbl_inernet_speed.Text = "0 Mb/s";
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.Transparent;
            this.panel2.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("panel2.BackgroundImage")));
            this.panel2.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.panel2.Controls.Add(this.flowLayoutPanel4);
            this.panel2.Controls.Add(this.flowLayoutPanel3);
            this.panel2.Controls.Add(this.progressBarControl1);
            this.panel2.Controls.Add(this.progressBarControl2);
            this.panel2.Controls.Add(this.flowLayoutPanel2);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel2.Location = new System.Drawing.Point(0, 363);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(789, 102);
            this.panel2.TabIndex = 10;
            // 
            // flowLayoutPanel4
            // 
            this.flowLayoutPanel4.Controls.Add(this.label4);
            this.flowLayoutPanel4.Controls.Add(this.lbl_inernet_speed);
            this.flowLayoutPanel4.Controls.Add(this.label5);
            this.flowLayoutPanel4.Controls.Add(this.lbl_total_files);
            this.flowLayoutPanel4.Dock = System.Windows.Forms.DockStyle.Right;
            this.flowLayoutPanel4.FlowDirection = System.Windows.Forms.FlowDirection.RightToLeft;
            this.flowLayoutPanel4.Location = new System.Drawing.Point(388, 27);
            this.flowLayoutPanel4.Name = "flowLayoutPanel4";
            this.flowLayoutPanel4.Size = new System.Drawing.Size(401, 50);
            this.flowLayoutPanel4.TabIndex = 23;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.BackColor = System.Drawing.Color.Transparent;
            this.label5.Font = new System.Drawing.Font("Microsoft Tai Le", 10F, System.Drawing.FontStyle.Bold);
            this.label5.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.label5.Location = new System.Drawing.Point(287, 26);
            this.label5.Margin = new System.Windows.Forms.Padding(5, 0, 35, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(79, 18);
            this.label5.TabIndex = 20;
            this.label5.Text = ":Total Files";
            // 
            // lbl_total_files
            // 
            this.lbl_total_files.AutoSize = true;
            this.lbl_total_files.BackColor = System.Drawing.Color.Transparent;
            this.lbl_total_files.Font = new System.Drawing.Font("Microsoft Tai Le", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_total_files.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.lbl_total_files.Location = new System.Drawing.Point(243, 26);
            this.lbl_total_files.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.lbl_total_files.Name = "lbl_total_files";
            this.lbl_total_files.Size = new System.Drawing.Size(34, 21);
            this.lbl_total_files.TabIndex = 21;
            this.lbl_total_files.Text = "0/0";
            // 
            // flowLayoutPanel3
            // 
            this.flowLayoutPanel3.Controls.Add(this.label3);
            this.flowLayoutPanel3.Controls.Add(this.lbl_file_downloaded);
            this.flowLayoutPanel3.Dock = System.Windows.Forms.DockStyle.Left;
            this.flowLayoutPanel3.Location = new System.Drawing.Point(0, 27);
            this.flowLayoutPanel3.Margin = new System.Windows.Forms.Padding(5, 5, 5, 0);
            this.flowLayoutPanel3.Name = "flowLayoutPanel3";
            this.flowLayoutPanel3.Size = new System.Drawing.Size(387, 50);
            this.flowLayoutPanel3.TabIndex = 22;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.BackColor = System.Drawing.Color.Transparent;
            this.label3.Font = new System.Drawing.Font("Microsoft Tai Le", 10F, System.Drawing.FontStyle.Bold);
            this.label3.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.label3.Location = new System.Drawing.Point(5, 5);
            this.label3.Margin = new System.Windows.Forms.Padding(5, 5, 5, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(97, 18);
            this.label3.TabIndex = 19;
            this.label3.Text = "Downloaded:";
            // 
            // lbl_file_downloaded
            // 
            this.lbl_file_downloaded.AutoSize = true;
            this.lbl_file_downloaded.BackColor = System.Drawing.Color.Transparent;
            this.lbl_file_downloaded.Font = new System.Drawing.Font("Microsoft Tai Le", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_file_downloaded.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.lbl_file_downloaded.Location = new System.Drawing.Point(112, 5);
            this.lbl_file_downloaded.Margin = new System.Windows.Forms.Padding(5, 5, 5, 0);
            this.lbl_file_downloaded.Name = "lbl_file_downloaded";
            this.lbl_file_downloaded.Size = new System.Drawing.Size(46, 21);
            this.lbl_file_downloaded.TabIndex = 20;
            this.lbl_file_downloaded.Text = "0 MB";
            // 
            // progressBarControl1
            // 
            this.progressBarControl1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.progressBarControl1.Location = new System.Drawing.Point(0, 77);
            this.progressBarControl1.Name = "progressBarControl1";
            this.progressBarControl1.Properties.Appearance.BackColor = System.Drawing.Color.Green;
            this.progressBarControl1.Properties.Appearance.BackColor2 = System.Drawing.Color.Lime;
            this.progressBarControl1.Properties.Appearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.progressBarControl1.Properties.EndColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            this.progressBarControl1.Properties.ShowTitle = true;
            this.progressBarControl1.Properties.StartColor = System.Drawing.Color.DarkRed;
            this.progressBarControl1.Size = new System.Drawing.Size(789, 25);
            this.progressBarControl1.TabIndex = 24;
            // 
            // progressBarControl2
            // 
            this.progressBarControl2.Dock = System.Windows.Forms.DockStyle.Top;
            this.progressBarControl2.Location = new System.Drawing.Point(0, 24);
            this.progressBarControl2.Name = "progressBarControl2";
            this.progressBarControl2.Properties.EndColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            this.progressBarControl2.Properties.StartColor = System.Drawing.Color.DarkRed;
            this.progressBarControl2.Size = new System.Drawing.Size(789, 3);
            this.progressBarControl2.TabIndex = 20;
            // 
            // flowLayoutPanel2
            // 
            this.flowLayoutPanel2.Controls.Add(this.label2);
            this.flowLayoutPanel2.Controls.Add(this.lbl_installing_info);
            this.flowLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.flowLayoutPanel2.Location = new System.Drawing.Point(0, 0);
            this.flowLayoutPanel2.Margin = new System.Windows.Forms.Padding(5, 5, 5, 0);
            this.flowLayoutPanel2.Name = "flowLayoutPanel2";
            this.flowLayoutPanel2.Size = new System.Drawing.Size(789, 24);
            this.flowLayoutPanel2.TabIndex = 21;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.Transparent;
            this.label2.Font = new System.Drawing.Font("Microsoft Tai Le", 10F, System.Drawing.FontStyle.Bold);
            this.label2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.label2.Location = new System.Drawing.Point(5, 5);
            this.label2.Margin = new System.Windows.Forms.Padding(5, 5, 5, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(35, 18);
            this.label2.TabIndex = 21;
            this.label2.Text = "File:";
            // 
            // lbl_installing_info
            // 
            this.lbl_installing_info.AutoSize = true;
            this.lbl_installing_info.BackColor = System.Drawing.Color.Transparent;
            this.lbl_installing_info.Font = new System.Drawing.Font("Microsoft Tai Le", 10F);
            this.lbl_installing_info.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.lbl_installing_info.Location = new System.Drawing.Point(50, 5);
            this.lbl_installing_info.Margin = new System.Windows.Forms.Padding(5, 5, 5, 0);
            this.lbl_installing_info.Name = "lbl_installing_info";
            this.lbl_installing_info.Size = new System.Drawing.Size(206, 18);
            this.lbl_installing_info.TabIndex = 20;
            this.lbl_installing_info.Text = "We are updateing your Software";
            // 
            // imageSlider1
            // 
            this.imageSlider1.AllowLooping = true;
            this.imageSlider1.AnimationTime = 500;
            this.imageSlider1.AutoSlide = DevExpress.XtraEditors.Controls.AutoSlide.Forward;
            this.imageSlider1.AutoSlideInterval = 3000;
            this.imageSlider1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.imageSlider1.Location = new System.Drawing.Point(0, 100);
            this.imageSlider1.Name = "imageSlider1";
            this.imageSlider1.ScrollButtonVisibility = DevExpress.Utils.DefaultBoolean.True;
            this.imageSlider1.Size = new System.Drawing.Size(789, 263);
            this.imageSlider1.TabIndex = 11;
            this.imageSlider1.Text = "imageSlider1";
            // 
            // btn_settings
            // 
            this.btn_settings.Appearance.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_settings.Appearance.Options.UseFont = true;
            this.btn_settings.ImageOptions.SvgImage = ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("btn_settings.ImageOptions.SvgImage")));
            this.btn_settings.Location = new System.Drawing.Point(8, 59);
            this.btn_settings.Name = "btn_settings";
            this.btn_settings.PaintStyle = DevExpress.XtraEditors.Controls.PaintStyles.Light;
            this.btn_settings.Size = new System.Drawing.Size(39, 36);
            this.btn_settings.TabIndex = 12;
            this.btn_settings.Click += new System.EventHandler(this.btn_settings_Click);
            // 
            // btn_close_form
            // 
            this.btn_close_form.Appearance.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_close_form.Appearance.Options.UseFont = true;
            this.btn_close_form.ImageOptions.SvgImage = ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("btn_close_form.ImageOptions.SvgImage")));
            this.btn_close_form.Location = new System.Drawing.Point(753, -2);
            this.btn_close_form.Name = "btn_close_form";
            this.btn_close_form.PaintStyle = DevExpress.XtraEditors.Controls.PaintStyles.Light;
            this.btn_close_form.Size = new System.Drawing.Size(36, 34);
            this.btn_close_form.TabIndex = 13;
            this.btn_close_form.Click += new System.EventHandler(this.btn_close_form_Click);
            // 
            // change_type
            // 
            this.change_type.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.change_type.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.change_type.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold);
            this.change_type.FormattingEnabled = true;
            this.change_type.Items.AddRange(new object[] {
            "",
            "update",
            "upgrade"});
            this.change_type.Location = new System.Drawing.Point(280, 65);
            this.change_type.Name = "change_type";
            this.change_type.Size = new System.Drawing.Size(121, 24);
            this.change_type.TabIndex = 14;
            // 
            // main_form
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(255)))));
            this.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("$this.BackgroundImage")));
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(789, 465);
            this.Controls.Add(this.change_type);
            this.Controls.Add(this.btn_close_form);
            this.Controls.Add(this.btn_settings);
            this.Controls.Add(this.imageSlider1);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.btn_download_software);
            this.Controls.Add(this.lblinfo);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.pictureBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "main_form";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Softpitch Updater";
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.panel2.ResumeLayout(false);
            this.flowLayoutPanel4.ResumeLayout(false);
            this.flowLayoutPanel4.PerformLayout();
            this.flowLayoutPanel3.ResumeLayout(false);
            this.flowLayoutPanel3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.progressBarControl1.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.progressBarControl2.Properties)).EndInit();
            this.flowLayoutPanel2.ResumeLayout(false);
            this.flowLayoutPanel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.imageSlider1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.NotifyIcon notifyIcon1;
        private System.Windows.Forms.Label lblinfo;
        private DevExpress.XtraEditors.SimpleButton btn_download_software;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label lbl_inernet_speed;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label lbl_total_files;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel3;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label lbl_file_downloaded;
        private DevExpress.XtraEditors.ProgressBarControl progressBarControl2;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel2;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label lbl_installing_info;
        private DevExpress.XtraEditors.Controls.ImageSlider imageSlider1;
        private DevExpress.XtraEditors.ProgressBarControl progressBarControl1;
        private DevExpress.XtraEditors.SimpleButton btn_settings;
        private DevExpress.XtraEditors.SimpleButton btn_close_form;
        private System.Windows.Forms.ComboBox change_type;
    }
}

