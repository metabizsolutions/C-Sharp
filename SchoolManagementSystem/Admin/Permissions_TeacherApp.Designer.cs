namespace SchoolManagementSystem.Admin
{
    partial class Permissions_TeacherApp
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Permissions_TeacherApp));
            this.xtraScrollableControl1 = new DevExpress.XtraEditors.XtraScrollableControl();
            this.groupControl2 = new DevExpress.XtraEditors.GroupControl();
            this.btnApply = new DevExpress.XtraEditors.SimpleButton();
            this.floor_sheet = new DevExpress.XtraEditors.CheckEdit();
            this.timetable = new DevExpress.XtraEditors.CheckEdit();
            this.manage_marks = new DevExpress.XtraEditors.CheckEdit();
            this.std_report = new DevExpress.XtraEditors.CheckEdit();
            this.student_attendance = new DevExpress.XtraEditors.CheckEdit();
            this.teacher_attendance = new DevExpress.XtraEditors.CheckEdit();
            this.groupControl1 = new DevExpress.XtraEditors.GroupControl();
            this.btnEdit = new DevExpress.XtraEditors.SimpleButton();
            this.btnCreate = new DevExpress.XtraEditors.SimpleButton();
            this.txtUserPermission = new DevExpress.XtraEditors.ComboBoxEdit();
            this.label1 = new System.Windows.Forms.Label();
            this.administer = new DevExpress.XtraEditors.CheckEdit();
            this.xtraScrollableControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl2)).BeginInit();
            this.groupControl2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.floor_sheet.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.timetable.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.manage_marks.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.std_report.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.student_attendance.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.teacher_attendance.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).BeginInit();
            this.groupControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtUserPermission.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.administer.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // xtraScrollableControl1
            // 
            this.xtraScrollableControl1.AllowTouchScroll = true;
            this.xtraScrollableControl1.Controls.Add(this.groupControl2);
            this.xtraScrollableControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.xtraScrollableControl1.Location = new System.Drawing.Point(0, 44);
            this.xtraScrollableControl1.Name = "xtraScrollableControl1";
            this.xtraScrollableControl1.Size = new System.Drawing.Size(480, 250);
            this.xtraScrollableControl1.TabIndex = 0;
            // 
            // groupControl2
            // 
            this.groupControl2.Controls.Add(this.administer);
            this.groupControl2.Controls.Add(this.btnApply);
            this.groupControl2.Controls.Add(this.floor_sheet);
            this.groupControl2.Controls.Add(this.timetable);
            this.groupControl2.Controls.Add(this.manage_marks);
            this.groupControl2.Controls.Add(this.std_report);
            this.groupControl2.Controls.Add(this.student_attendance);
            this.groupControl2.Controls.Add(this.teacher_attendance);
            this.groupControl2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupControl2.Location = new System.Drawing.Point(0, 0);
            this.groupControl2.Name = "groupControl2";
            this.groupControl2.ShowCaption = false;
            this.groupControl2.Size = new System.Drawing.Size(480, 250);
            this.groupControl2.TabIndex = 0;
            this.groupControl2.Text = "groupControl2";
            this.groupControl2.Visible = false;
            // 
            // btnApply
            // 
            this.btnApply.Location = new System.Drawing.Point(377, 218);
            this.btnApply.Name = "btnApply";
            this.btnApply.Size = new System.Drawing.Size(88, 23);
            this.btnApply.TabIndex = 3;
            this.btnApply.Text = "Apply";
            this.btnApply.Click += new System.EventHandler(this.btnApply_Click);
            // 
            // floor_sheet
            // 
            this.floor_sheet.Location = new System.Drawing.Point(47, 40);
            this.floor_sheet.Name = "floor_sheet";
            this.floor_sheet.Properties.Caption = "Floor Sheet";
            this.floor_sheet.Size = new System.Drawing.Size(123, 19);
            this.floor_sheet.TabIndex = 39;
            // 
            // timetable
            // 
            this.timetable.Location = new System.Drawing.Point(47, 65);
            this.timetable.Name = "timetable";
            this.timetable.Properties.Caption = "Timetable";
            this.timetable.Size = new System.Drawing.Size(123, 19);
            this.timetable.TabIndex = 36;
            // 
            // manage_marks
            // 
            this.manage_marks.Location = new System.Drawing.Point(47, 90);
            this.manage_marks.Name = "manage_marks";
            this.manage_marks.Properties.Caption = "Manage Marks";
            this.manage_marks.Size = new System.Drawing.Size(123, 19);
            this.manage_marks.TabIndex = 27;
            // 
            // std_report
            // 
            this.std_report.Location = new System.Drawing.Point(47, 115);
            this.std_report.Name = "std_report";
            this.std_report.Properties.Caption = "Student Exam Report";
            this.std_report.Size = new System.Drawing.Size(123, 19);
            this.std_report.TabIndex = 22;
            this.std_report.CheckedChanged += new System.EventHandler(this.std_report_CheckedChanged);
            // 
            // student_attendance
            // 
            this.student_attendance.Location = new System.Drawing.Point(47, 140);
            this.student_attendance.Name = "student_attendance";
            this.student_attendance.Properties.Caption = "Student Attendance";
            this.student_attendance.Size = new System.Drawing.Size(123, 19);
            this.student_attendance.TabIndex = 11;
            // 
            // teacher_attendance
            // 
            this.teacher_attendance.Location = new System.Drawing.Point(47, 15);
            this.teacher_attendance.Name = "teacher_attendance";
            this.teacher_attendance.Properties.Caption = "Teacher Attendance";
            this.teacher_attendance.Size = new System.Drawing.Size(123, 19);
            this.teacher_attendance.TabIndex = 0;
            // 
            // groupControl1
            // 
            this.groupControl1.Controls.Add(this.btnEdit);
            this.groupControl1.Controls.Add(this.btnCreate);
            this.groupControl1.Controls.Add(this.txtUserPermission);
            this.groupControl1.Controls.Add(this.label1);
            this.groupControl1.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupControl1.Location = new System.Drawing.Point(0, 0);
            this.groupControl1.Name = "groupControl1";
            this.groupControl1.ShowCaption = false;
            this.groupControl1.Size = new System.Drawing.Size(480, 44);
            this.groupControl1.TabIndex = 0;
            this.groupControl1.Text = "groupControl1";
            // 
            // btnEdit
            // 
            this.btnEdit.Location = new System.Drawing.Point(377, 10);
            this.btnEdit.Name = "btnEdit";
            this.btnEdit.Size = new System.Drawing.Size(88, 23);
            this.btnEdit.TabIndex = 3;
            this.btnEdit.Text = "Edit";
            this.btnEdit.Click += new System.EventHandler(this.btnEdit_Click);
            // 
            // btnCreate
            // 
            this.btnCreate.Location = new System.Drawing.Point(283, 11);
            this.btnCreate.Name = "btnCreate";
            this.btnCreate.Size = new System.Drawing.Size(88, 23);
            this.btnCreate.TabIndex = 2;
            this.btnCreate.Text = "Create";
            this.btnCreate.Click += new System.EventHandler(this.btnCreate_Click);
            // 
            // txtUserPermission
            // 
            this.txtUserPermission.Location = new System.Drawing.Point(113, 13);
            this.txtUserPermission.Name = "txtUserPermission";
            this.txtUserPermission.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.txtUserPermission.Properties.Items.AddRange(new object[] {
            "Principal"});
            this.txtUserPermission.Size = new System.Drawing.Size(164, 20);
            this.txtUserPermission.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 16);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(91, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Permission Role : ";
            // 
            // administer
            // 
            this.administer.Location = new System.Drawing.Point(47, 165);
            this.administer.Name = "administer";
            this.administer.Properties.Caption = "Administer";
            this.administer.Size = new System.Drawing.Size(123, 19);
            this.administer.TabIndex = 40;
            // 
            // Permissions_TeacherApp
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(480, 294);
            this.Controls.Add(this.xtraScrollableControl1);
            this.Controls.Add(this.groupControl1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Permissions_TeacherApp";
            this.Text = "Permissions Teacher App";
            this.xtraScrollableControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.groupControl2)).EndInit();
            this.groupControl2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.floor_sheet.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.timetable.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.manage_marks.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.std_report.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.student_attendance.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.teacher_attendance.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).EndInit();
            this.groupControl1.ResumeLayout(false);
            this.groupControl1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtUserPermission.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.administer.Properties)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.XtraScrollableControl xtraScrollableControl1;
        private DevExpress.XtraEditors.GroupControl groupControl1;
        private DevExpress.XtraEditors.SimpleButton btnCreate;
        private DevExpress.XtraEditors.ComboBoxEdit txtUserPermission;
        private System.Windows.Forms.Label label1;
        private DevExpress.XtraEditors.GroupControl groupControl2;
        private DevExpress.XtraEditors.CheckEdit student_attendance;
        private DevExpress.XtraEditors.CheckEdit teacher_attendance;
        private DevExpress.XtraEditors.CheckEdit manage_marks;
        private DevExpress.XtraEditors.CheckEdit std_report;
        private DevExpress.XtraEditors.CheckEdit timetable;
        private DevExpress.XtraEditors.CheckEdit floor_sheet;
        private DevExpress.XtraEditors.SimpleButton btnApply;
        private DevExpress.XtraEditors.SimpleButton btnEdit;
        private DevExpress.XtraEditors.CheckEdit administer;
    }
}