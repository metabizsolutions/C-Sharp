
namespace SchoolManagementSystem.Students
{
    partial class student_details
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(student_details));
            this.std_panel = new System.Windows.Forms.FlowLayoutPanel();
            this.PicLogo = new System.Windows.Forms.PictureBox();
            this.lbl_school = new System.Windows.Forms.Label();
            this.groupControl1 = new DevExpress.XtraEditors.GroupControl();
            this.picBoxStudent = new DevExpress.XtraEditors.PictureEdit();
            this.BtnLoadImage = new DevExpress.XtraEditors.SimpleButton();
            this.BtnTakeImage = new DevExpress.XtraEditors.SimpleButton();
            this.panel1 = new System.Windows.Forms.Panel();
            this.btn_update = new DevExpress.XtraEditors.SimpleButton();
            this.std_panel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.PicLogo)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).BeginInit();
            this.groupControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picBoxStudent.Properties)).BeginInit();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // std_panel
            // 
            this.std_panel.AutoScroll = true;
            this.std_panel.Controls.Add(this.PicLogo);
            this.std_panel.Controls.Add(this.lbl_school);
            this.std_panel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.std_panel.Location = new System.Drawing.Point(200, 0);
            this.std_panel.Name = "std_panel";
            this.std_panel.Size = new System.Drawing.Size(1035, 484);
            this.std_panel.TabIndex = 0;
            // 
            // PicLogo
            // 
            this.PicLogo.Location = new System.Drawing.Point(3, 3);
            this.PicLogo.Name = "PicLogo";
            this.PicLogo.Size = new System.Drawing.Size(141, 70);
            this.PicLogo.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.PicLogo.TabIndex = 1;
            this.PicLogo.TabStop = false;
            // 
            // lbl_school
            // 
            this.lbl_school.AutoSize = true;
            this.std_panel.SetFlowBreak(this.lbl_school, true);
            this.lbl_school.Font = new System.Drawing.Font("Microsoft YaHei UI", 15.75F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_school.Location = new System.Drawing.Point(447, 10);
            this.lbl_school.Margin = new System.Windows.Forms.Padding(300, 10, 3, 0);
            this.lbl_school.Name = "lbl_school";
            this.lbl_school.Size = new System.Drawing.Size(80, 28);
            this.lbl_school.TabIndex = 0;
            this.lbl_school.Text = "School";
            // 
            // groupControl1
            // 
            this.groupControl1.Controls.Add(this.picBoxStudent);
            this.groupControl1.Controls.Add(this.BtnLoadImage);
            this.groupControl1.Controls.Add(this.BtnTakeImage);
            this.groupControl1.Dock = System.Windows.Forms.DockStyle.Left;
            this.groupControl1.Location = new System.Drawing.Point(0, 0);
            this.groupControl1.Name = "groupControl1";
            this.groupControl1.Size = new System.Drawing.Size(200, 513);
            this.groupControl1.TabIndex = 45;
            this.groupControl1.Text = "Picture";
            // 
            // picBoxStudent
            // 
            this.picBoxStudent.Cursor = System.Windows.Forms.Cursors.Default;
            this.picBoxStudent.Dock = System.Windows.Forms.DockStyle.Top;
            this.picBoxStudent.Location = new System.Drawing.Point(2, 23);
            this.picBoxStudent.Name = "picBoxStudent";
            this.picBoxStudent.Properties.ShowCameraMenuItem = DevExpress.XtraEditors.Controls.CameraMenuItemVisibility.Auto;
            this.picBoxStudent.Properties.SizeMode = DevExpress.XtraEditors.Controls.PictureSizeMode.Stretch;
            this.picBoxStudent.Size = new System.Drawing.Size(196, 175);
            this.picBoxStudent.TabIndex = 40;
            // 
            // BtnLoadImage
            // 
            this.BtnLoadImage.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("BtnLoadImage.ImageOptions.Image")));
            this.BtnLoadImage.Location = new System.Drawing.Point(80, 204);
            this.BtnLoadImage.Name = "BtnLoadImage";
            this.BtnLoadImage.Size = new System.Drawing.Size(100, 22);
            this.BtnLoadImage.TabIndex = 41;
            this.BtnLoadImage.Text = "Select Image";
            this.BtnLoadImage.Click += new System.EventHandler(this.BtnLoadImage_Click);
            // 
            // BtnTakeImage
            // 
            this.BtnTakeImage.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("BtnTakeImage.ImageOptions.Image")));
            this.BtnTakeImage.Location = new System.Drawing.Point(10, 204);
            this.BtnTakeImage.Name = "BtnTakeImage";
            this.BtnTakeImage.Size = new System.Drawing.Size(67, 22);
            this.BtnTakeImage.TabIndex = 42;
            this.BtnTakeImage.Text = "Take";
            this.BtnTakeImage.Click += new System.EventHandler(this.BtnTakeImage_Click);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.btn_update);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel1.Location = new System.Drawing.Point(200, 484);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1035, 29);
            this.panel1.TabIndex = 46;
            // 
            // btn_update
            // 
            this.btn_update.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("btn_update.ImageOptions.Image")));
            this.btn_update.Location = new System.Drawing.Point(455, 3);
            this.btn_update.Name = "btn_update";
            this.btn_update.Size = new System.Drawing.Size(72, 22);
            this.btn_update.TabIndex = 43;
            this.btn_update.Text = "Update";
            this.btn_update.Click += new System.EventHandler(this.btn_update_Click);
            // 
            // student_details
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1235, 513);
            this.Controls.Add(this.std_panel);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.groupControl1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow;
            this.Name = "student_details";
            this.Text = "student_details";
            this.std_panel.ResumeLayout(false);
            this.std_panel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.PicLogo)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).EndInit();
            this.groupControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.picBoxStudent.Properties)).EndInit();
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.FlowLayoutPanel std_panel;
        private DevExpress.XtraEditors.GroupControl groupControl1;
        public DevExpress.XtraEditors.PictureEdit picBoxStudent;
        private DevExpress.XtraEditors.SimpleButton BtnLoadImage;
        private DevExpress.XtraEditors.SimpleButton BtnTakeImage;
        private System.Windows.Forms.Label lbl_school;
        private System.Windows.Forms.PictureBox PicLogo;
        private System.Windows.Forms.Panel panel1;
        private DevExpress.XtraEditors.SimpleButton btn_update;
    }
}