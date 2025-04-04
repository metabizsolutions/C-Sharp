namespace SchoolManagementSystem.Attendance
{
    partial class SMSAttendance
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SMSAttendance));
            this.groupControl1 = new DevExpress.XtraEditors.GroupControl();
            this.BtnSendOutSMS = new DevExpress.XtraEditors.SimpleButton();
            this.labelControl3 = new DevExpress.XtraEditors.LabelControl();
            this.txtHosteliesed = new DevExpress.XtraEditors.ComboBoxEdit();
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            this.txtType = new DevExpress.XtraEditors.ComboBoxEdit();
            this.labelControl4 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.BtnSendSMS = new DevExpress.XtraEditors.SimpleButton();
            this.labelControl9 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl8 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl7 = new DevExpress.XtraEditors.LabelControl();
            this.txtDate = new DevExpress.XtraEditors.ComboBoxEdit();
            this.txtYear = new DevExpress.XtraEditors.ComboBoxEdit();
            this.txtMonth = new DevExpress.XtraEditors.ComboBoxEdit();
            this.ddlsection = new DevExpress.XtraEditors.CheckedComboBoxEdit();
            this.txtProgressStatus = new DevExpress.XtraEditors.ProgressBarControl();
            this.txtMemoStatus = new DevExpress.XtraEditors.MemoEdit();
            this.txtClass = new DevExpress.XtraEditors.CheckedComboBoxEdit();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).BeginInit();
            this.groupControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtHosteliesed.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtType.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtDate.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtYear.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtMonth.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ddlsection.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtProgressStatus.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtMemoStatus.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtClass.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // groupControl1
            // 
            this.groupControl1.Controls.Add(this.BtnSendOutSMS);
            this.groupControl1.Controls.Add(this.labelControl3);
            this.groupControl1.Controls.Add(this.txtHosteliesed);
            this.groupControl1.Controls.Add(this.labelControl2);
            this.groupControl1.Controls.Add(this.txtType);
            this.groupControl1.Controls.Add(this.labelControl4);
            this.groupControl1.Controls.Add(this.labelControl1);
            this.groupControl1.Controls.Add(this.BtnSendSMS);
            this.groupControl1.Controls.Add(this.labelControl9);
            this.groupControl1.Controls.Add(this.labelControl8);
            this.groupControl1.Controls.Add(this.labelControl7);
            this.groupControl1.Controls.Add(this.txtDate);
            this.groupControl1.Controls.Add(this.txtYear);
            this.groupControl1.Controls.Add(this.txtMonth);
            this.groupControl1.Controls.Add(this.ddlsection);
            this.groupControl1.Controls.Add(this.txtClass);
            this.groupControl1.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupControl1.Location = new System.Drawing.Point(0, 0);
            this.groupControl1.Name = "groupControl1";
            this.groupControl1.Size = new System.Drawing.Size(1265, 86);
            this.groupControl1.TabIndex = 7;
            this.groupControl1.Text = "Daily Attendance";
            // 
            // BtnSendOutSMS
            // 
            this.BtnSendOutSMS.Enabled = false;
            this.BtnSendOutSMS.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("BtnSendOutSMS.ImageOptions.Image")));
            this.BtnSendOutSMS.Location = new System.Drawing.Point(1007, 40);
            this.BtnSendOutSMS.Name = "BtnSendOutSMS";
            this.BtnSendOutSMS.Size = new System.Drawing.Size(171, 23);
            this.BtnSendOutSMS.TabIndex = 65;
            this.BtnSendOutSMS.Text = "Send Out Notification via SMS";
            this.BtnSendOutSMS.Click += new System.EventHandler(this.BtnSendOutSMS_Click);
            // 
            // labelControl3
            // 
            this.labelControl3.Location = new System.Drawing.Point(764, 3);
            this.labelControl3.Name = "labelControl3";
            this.labelControl3.Size = new System.Drawing.Size(55, 13);
            this.labelControl3.TabIndex = 63;
            this.labelControl3.Text = "Hosteliesed";
            // 
            // txtHosteliesed
            // 
            this.txtHosteliesed.Location = new System.Drawing.Point(764, 22);
            this.txtHosteliesed.Name = "txtHosteliesed";
            this.txtHosteliesed.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.txtHosteliesed.Properties.Items.AddRange(new object[] {
            "Both",
            "Hostel",
            "Non-Hostel"});
            this.txtHosteliesed.Size = new System.Drawing.Size(150, 20);
            this.txtHosteliesed.TabIndex = 64;
            // 
            // labelControl2
            // 
            this.labelControl2.Location = new System.Drawing.Point(585, 23);
            this.labelControl2.Name = "labelControl2";
            this.labelControl2.Size = new System.Drawing.Size(24, 13);
            this.labelControl2.TabIndex = 63;
            this.labelControl2.Text = "Type";
            // 
            // txtType
            // 
            this.txtType.Location = new System.Drawing.Point(585, 42);
            this.txtType.Name = "txtType";
            this.txtType.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.txtType.Properties.Items.AddRange(new object[] {
            "Both Present and Absent",
            "Present ",
            "Absent"});
            this.txtType.Size = new System.Drawing.Size(150, 20);
            this.txtType.TabIndex = 64;
            // 
            // labelControl4
            // 
            this.labelControl4.Location = new System.Drawing.Point(431, 23);
            this.labelControl4.Name = "labelControl4";
            this.labelControl4.Size = new System.Drawing.Size(36, 13);
            this.labelControl4.TabIndex = 61;
            this.labelControl4.Text = "Section";
            // 
            // labelControl1
            // 
            this.labelControl1.Location = new System.Drawing.Point(275, 23);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(25, 13);
            this.labelControl1.TabIndex = 61;
            this.labelControl1.Text = "Class";
            // 
            // BtnSendSMS
            // 
            this.BtnSendSMS.Enabled = false;
            this.BtnSendSMS.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("BtnSendSMS.ImageOptions.Image")));
            this.BtnSendSMS.Location = new System.Drawing.Point(785, 40);
            this.BtnSendSMS.Name = "BtnSendSMS";
            this.BtnSendSMS.Size = new System.Drawing.Size(216, 23);
            this.BtnSendSMS.TabIndex = 60;
            this.BtnSendSMS.Text = "Send IN/Presend Notification via SMS";
            this.BtnSendSMS.Click += new System.EventHandler(this.BtnSendSMS_Click);
            // 
            // labelControl9
            // 
            this.labelControl9.Location = new System.Drawing.Point(182, 23);
            this.labelControl9.Name = "labelControl9";
            this.labelControl9.Size = new System.Drawing.Size(23, 13);
            this.labelControl9.TabIndex = 53;
            this.labelControl9.Text = "Date";
            // 
            // labelControl8
            // 
            this.labelControl8.Location = new System.Drawing.Point(85, 23);
            this.labelControl8.Name = "labelControl8";
            this.labelControl8.Size = new System.Drawing.Size(30, 13);
            this.labelControl8.TabIndex = 51;
            this.labelControl8.Text = "Month";
            // 
            // labelControl7
            // 
            this.labelControl7.Location = new System.Drawing.Point(5, 23);
            this.labelControl7.Name = "labelControl7";
            this.labelControl7.Size = new System.Drawing.Size(22, 13);
            this.labelControl7.TabIndex = 49;
            this.labelControl7.Text = "Year";
            // 
            // txtDate
            // 
            this.txtDate.Location = new System.Drawing.Point(182, 42);
            this.txtDate.Name = "txtDate";
            this.txtDate.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.txtDate.Properties.Items.AddRange(new object[] {
            "01",
            "02",
            "03",
            "04",
            "05",
            "06",
            "07",
            "08",
            "09",
            "10",
            "11",
            "12",
            "13",
            "14",
            "15",
            "16",
            "17",
            "18",
            "19",
            "20",
            "21",
            "22",
            "23",
            "24",
            "25",
            "26",
            "27",
            "28",
            "29",
            "30",
            "31"});
            this.txtDate.Size = new System.Drawing.Size(88, 20);
            this.txtDate.TabIndex = 54;
            // 
            // txtYear
            // 
            this.txtYear.Location = new System.Drawing.Point(5, 42);
            this.txtYear.Name = "txtYear";
            this.txtYear.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.txtYear.Properties.Items.AddRange(new object[] {
            "2015",
            "2016",
            "2017",
            "2018",
            "2019",
            "2020",
            "2021",
            "2022",
            "2023",
            "2024",
            "2025",
            "2026",
            "2027",
            "2028",
            "2029",
            "2030"});
            this.txtYear.Size = new System.Drawing.Size(75, 20);
            this.txtYear.TabIndex = 50;
            // 
            // txtMonth
            // 
            this.txtMonth.Location = new System.Drawing.Point(85, 42);
            this.txtMonth.Name = "txtMonth";
            this.txtMonth.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.txtMonth.Properties.Items.AddRange(new object[] {
            "1",
            "2",
            "3",
            "4",
            "5",
            "6",
            "7",
            "8",
            "9",
            "10",
            "11",
            "12"});
            this.txtMonth.Size = new System.Drawing.Size(91, 20);
            this.txtMonth.TabIndex = 52;
            // 
            // ddlsection
            // 
            this.ddlsection.Location = new System.Drawing.Point(431, 42);
            this.ddlsection.Name = "ddlsection";
            this.ddlsection.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.ddlsection.Size = new System.Drawing.Size(150, 20);
            this.ddlsection.TabIndex = 62;
            // 
            // txtProgressStatus
            // 
            this.txtProgressStatus.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.txtProgressStatus.EditValue = null;
            this.txtProgressStatus.Location = new System.Drawing.Point(0, 442);
            this.txtProgressStatus.Name = "txtProgressStatus";
            this.txtProgressStatus.Properties.AutoHeight = true;
            this.txtProgressStatus.Properties.ShowTitle = true;
            this.txtProgressStatus.Size = new System.Drawing.Size(1265, 14);
            this.txtProgressStatus.TabIndex = 66;
            this.txtProgressStatus.TabStop = true;
            // 
            // txtMemoStatus
            // 
            this.txtMemoStatus.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtMemoStatus.Location = new System.Drawing.Point(0, 86);
            this.txtMemoStatus.Name = "txtMemoStatus";
            this.txtMemoStatus.Properties.ReadOnly = true;
            this.txtMemoStatus.Size = new System.Drawing.Size(1265, 356);
            this.txtMemoStatus.TabIndex = 67;
            // 
            // txtClass
            // 
            this.txtClass.Location = new System.Drawing.Point(275, 42);
            this.txtClass.Name = "txtClass";
            this.txtClass.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.txtClass.Size = new System.Drawing.Size(150, 20);
            this.txtClass.TabIndex = 62;
            this.txtClass.EditValueChanged += new System.EventHandler(this.txtClass_EditValueChanged);
            // 
            // SMSAttendance
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.txtMemoStatus);
            this.Controls.Add(this.groupControl1);
            this.Controls.Add(this.txtProgressStatus);
            this.Name = "SMSAttendance";
            this.Size = new System.Drawing.Size(1265, 456);
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).EndInit();
            this.groupControl1.ResumeLayout(false);
            this.groupControl1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtHosteliesed.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtType.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtDate.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtYear.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtMonth.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ddlsection.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtProgressStatus.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtMemoStatus.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtClass.Properties)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.GroupControl groupControl1;
        private DevExpress.XtraEditors.LabelControl labelControl2;
        private DevExpress.XtraEditors.ComboBoxEdit txtType;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.XtraEditors.SimpleButton BtnSendSMS;
        private DevExpress.XtraEditors.LabelControl labelControl9;
        private DevExpress.XtraEditors.LabelControl labelControl8;
        private DevExpress.XtraEditors.LabelControl labelControl7;
        private DevExpress.XtraEditors.ComboBoxEdit txtDate;
        private DevExpress.XtraEditors.ComboBoxEdit txtYear;
        private DevExpress.XtraEditors.ComboBoxEdit txtMonth;
        private DevExpress.XtraEditors.SimpleButton BtnSendOutSMS;
        private DevExpress.XtraEditors.ProgressBarControl txtProgressStatus;
        private DevExpress.XtraEditors.MemoEdit txtMemoStatus;
        private DevExpress.XtraEditors.LabelControl labelControl3;
        private DevExpress.XtraEditors.ComboBoxEdit txtHosteliesed;
        private DevExpress.XtraEditors.LabelControl labelControl4;
        private DevExpress.XtraEditors.CheckedComboBoxEdit ddlsection;
        private DevExpress.XtraEditors.CheckedComboBoxEdit txtClass;
    }
}
