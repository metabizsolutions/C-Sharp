namespace SchoolManagementSystem.Attendance
{
    partial class TeacherAttendance
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(TeacherAttendance));
            this.gridTDailyAttendance = new DevExpress.XtraGrid.GridControl();
            this.gridView1 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.groupControl1 = new DevExpress.XtraEditors.GroupControl();
            this.btnSummary = new DevExpress.XtraEditors.SimpleButton();
            this.btnUpdate = new DevExpress.XtraEditors.SimpleButton();
            this.btnSave = new DevExpress.XtraEditors.SimpleButton();
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.BtnTAPrint = new DevExpress.XtraEditors.SimpleButton();
            this.BtnUpdateTAttend = new DevExpress.XtraEditors.SimpleButton();
            this.BtnManageAttendance = new DevExpress.XtraEditors.SimpleButton();
            this.btnTAFind = new DevExpress.XtraEditors.SimpleButton();
            this.labelControl9 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl8 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl7 = new DevExpress.XtraEditors.LabelControl();
            this.txtDate = new DevExpress.XtraEditors.ComboBoxEdit();
            this.txtYear = new DevExpress.XtraEditors.ComboBoxEdit();
            this.txtMonth = new DevExpress.XtraEditors.ComboBoxEdit();
            this.txtVal = new DevExpress.XtraEditors.LookUpEdit();
            this.txtSelectTeacher = new DevExpress.XtraEditors.SearchLookUpEdit();
            this.searchLookUpEdit1View = new DevExpress.XtraGrid.Views.Grid.GridView();
            ((System.ComponentModel.ISupportInitialize)(this.gridTDailyAttendance)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).BeginInit();
            this.groupControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtDate.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtYear.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtMonth.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtVal.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtSelectTeacher.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.searchLookUpEdit1View)).BeginInit();
            this.SuspendLayout();
            // 
            // gridTDailyAttendance
            // 
            this.gridTDailyAttendance.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridTDailyAttendance.Location = new System.Drawing.Point(0, 113);
            this.gridTDailyAttendance.MainView = this.gridView1;
            this.gridTDailyAttendance.Name = "gridTDailyAttendance";
            this.gridTDailyAttendance.Size = new System.Drawing.Size(1110, 332);
            this.gridTDailyAttendance.TabIndex = 9;
            this.gridTDailyAttendance.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridView1});
            // 
            // gridView1
            // 
            this.gridView1.GridControl = this.gridTDailyAttendance;
            this.gridView1.Name = "gridView1";
            this.gridView1.OptionsBehavior.Editable = false;
            this.gridView1.OptionsBehavior.ReadOnly = true;
            this.gridView1.OptionsFind.AlwaysVisible = true;
            this.gridView1.RowStyle += new DevExpress.XtraGrid.Views.Grid.RowStyleEventHandler(this.gridView1_RowStyle);
            this.gridView1.RowUpdated += new DevExpress.XtraGrid.Views.Base.RowObjectEventHandler(this.gridView1_RowUpdated);
            // 
            // groupControl1
            // 
            this.groupControl1.Controls.Add(this.btnSummary);
            this.groupControl1.Controls.Add(this.btnUpdate);
            this.groupControl1.Controls.Add(this.btnSave);
            this.groupControl1.Controls.Add(this.labelControl2);
            this.groupControl1.Controls.Add(this.labelControl1);
            this.groupControl1.Controls.Add(this.BtnTAPrint);
            this.groupControl1.Controls.Add(this.BtnUpdateTAttend);
            this.groupControl1.Controls.Add(this.BtnManageAttendance);
            this.groupControl1.Controls.Add(this.btnTAFind);
            this.groupControl1.Controls.Add(this.labelControl9);
            this.groupControl1.Controls.Add(this.labelControl8);
            this.groupControl1.Controls.Add(this.labelControl7);
            this.groupControl1.Controls.Add(this.txtDate);
            this.groupControl1.Controls.Add(this.txtYear);
            this.groupControl1.Controls.Add(this.txtMonth);
            this.groupControl1.Controls.Add(this.txtVal);
            this.groupControl1.Controls.Add(this.txtSelectTeacher);
            this.groupControl1.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupControl1.Location = new System.Drawing.Point(0, 0);
            this.groupControl1.Name = "groupControl1";
            this.groupControl1.Size = new System.Drawing.Size(1110, 113);
            this.groupControl1.TabIndex = 8;
            this.groupControl1.Text = "Daily Attendance";
            // 
            // btnSummary
            // 
            this.btnSummary.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("btnSummary.ImageOptions.Image")));
            this.btnSummary.Location = new System.Drawing.Point(556, 39);
            this.btnSummary.Name = "btnSummary";
            this.btnSummary.Size = new System.Drawing.Size(88, 23);
            this.btnSummary.TabIndex = 99;
            this.btnSummary.Text = "Summary";
            this.btnSummary.Click += new System.EventHandler(this.btnSummary_Click);
            // 
            // btnUpdate
            // 
            this.btnUpdate.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("btnUpdate.ImageOptions.Image")));
            this.btnUpdate.Location = new System.Drawing.Point(421, 84);
            this.btnUpdate.Name = "btnUpdate";
            this.btnUpdate.Size = new System.Drawing.Size(98, 23);
            this.btnUpdate.TabIndex = 93;
            this.btnUpdate.Text = "update";
            this.btnUpdate.Click += new System.EventHandler(this.btnUpdate_Click);
            // 
            // btnSave
            // 
            this.btnSave.Enabled = false;
            this.btnSave.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("btnSave.ImageOptions.Image")));
            this.btnSave.Location = new System.Drawing.Point(317, 84);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(98, 23);
            this.btnSave.TabIndex = 92;
            this.btnSave.Text = "Save";
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // labelControl2
            // 
            this.labelControl2.Location = new System.Drawing.Point(161, 68);
            this.labelControl2.Name = "labelControl2";
            this.labelControl2.Size = new System.Drawing.Size(26, 13);
            this.labelControl2.TabIndex = 91;
            this.labelControl2.Text = "Value";
            // 
            // labelControl1
            // 
            this.labelControl1.Location = new System.Drawing.Point(5, 68);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(71, 13);
            this.labelControl1.TabIndex = 90;
            this.labelControl1.Text = "Select Teacher";
            // 
            // BtnTAPrint
            // 
            this.BtnTAPrint.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("BtnTAPrint.ImageOptions.Image")));
            this.BtnTAPrint.Location = new System.Drawing.Point(888, 40);
            this.BtnTAPrint.Name = "BtnTAPrint";
            this.BtnTAPrint.Size = new System.Drawing.Size(75, 23);
            this.BtnTAPrint.TabIndex = 87;
            this.BtnTAPrint.Text = "Print";
            this.BtnTAPrint.Click += new System.EventHandler(this.BtnTAPrint_Click);
            // 
            // BtnUpdateTAttend
            // 
            this.BtnUpdateTAttend.Enabled = false;
            this.BtnUpdateTAttend.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("BtnUpdateTAttend.ImageOptions.Image")));
            this.BtnUpdateTAttend.Location = new System.Drawing.Point(811, 40);
            this.BtnUpdateTAttend.Name = "BtnUpdateTAttend";
            this.BtnUpdateTAttend.Size = new System.Drawing.Size(71, 23);
            this.BtnUpdateTAttend.TabIndex = 63;
            this.BtnUpdateTAttend.Text = "Update";
            this.BtnUpdateTAttend.Click += new System.EventHandler(this.BtnUpdateTAttend_Click);
            // 
            // BtnManageAttendance
            // 
            this.BtnManageAttendance.Enabled = false;
            this.BtnManageAttendance.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("BtnManageAttendance.ImageOptions.Image")));
            this.BtnManageAttendance.Location = new System.Drawing.Point(670, 40);
            this.BtnManageAttendance.Name = "BtnManageAttendance";
            this.BtnManageAttendance.Size = new System.Drawing.Size(135, 23);
            this.BtnManageAttendance.TabIndex = 60;
            this.BtnManageAttendance.Text = "Manage Attendance";
            this.BtnManageAttendance.Click += new System.EventHandler(this.BtnManageAttendance_Click);
            // 
            // btnTAFind
            // 
            this.btnTAFind.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("btnTAFind.ImageOptions.Image")));
            this.btnTAFind.Location = new System.Drawing.Point(473, 40);
            this.btnTAFind.Name = "btnTAFind";
            this.btnTAFind.Size = new System.Drawing.Size(67, 23);
            this.btnTAFind.TabIndex = 59;
            this.btnTAFind.Text = "Find";
            this.btnTAFind.Click += new System.EventHandler(this.btnDAFind_Click);
            // 
            // labelControl9
            // 
            this.labelControl9.Location = new System.Drawing.Point(317, 23);
            this.labelControl9.Name = "labelControl9";
            this.labelControl9.Size = new System.Drawing.Size(23, 13);
            this.labelControl9.TabIndex = 53;
            this.labelControl9.Text = "Date";
            // 
            // labelControl8
            // 
            this.labelControl8.Location = new System.Drawing.Point(161, 23);
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
            this.txtDate.Location = new System.Drawing.Point(317, 42);
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
            this.txtDate.Size = new System.Drawing.Size(150, 20);
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
            this.txtYear.Size = new System.Drawing.Size(150, 20);
            this.txtYear.TabIndex = 50;
            // 
            // txtMonth
            // 
            this.txtMonth.Location = new System.Drawing.Point(161, 42);
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
            this.txtMonth.Size = new System.Drawing.Size(150, 20);
            this.txtMonth.TabIndex = 52;
            // 
            // txtVal
            // 
            this.txtVal.Location = new System.Drawing.Point(161, 86);
            this.txtVal.Name = "txtVal";
            this.txtVal.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.txtVal.Properties.NullText = "";
            this.txtVal.Properties.PopupSizeable = false;
            this.txtVal.Size = new System.Drawing.Size(150, 20);
            this.txtVal.TabIndex = 89;
            // 
            // txtSelectTeacher
            // 
            this.txtSelectTeacher.Location = new System.Drawing.Point(5, 86);
            this.txtSelectTeacher.Name = "txtSelectTeacher";
            this.txtSelectTeacher.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.txtSelectTeacher.Properties.NullText = "";
            this.txtSelectTeacher.Properties.PopupSizeable = false;
            this.txtSelectTeacher.Properties.PopupView = this.searchLookUpEdit1View;
            this.txtSelectTeacher.Size = new System.Drawing.Size(150, 20);
            this.txtSelectTeacher.TabIndex = 88;
            // 
            // searchLookUpEdit1View
            // 
            this.searchLookUpEdit1View.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus;
            this.searchLookUpEdit1View.Name = "searchLookUpEdit1View";
            this.searchLookUpEdit1View.OptionsSelection.EnableAppearanceFocusedCell = false;
            this.searchLookUpEdit1View.OptionsView.ShowGroupPanel = false;
            // 
            // TeacherAttendance
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.gridTDailyAttendance);
            this.Controls.Add(this.groupControl1);
            this.Name = "TeacherAttendance";
            this.Size = new System.Drawing.Size(1110, 445);
            this.Enter += new System.EventHandler(this.TeacherAttendance_Enter);
            ((System.ComponentModel.ISupportInitialize)(this.gridTDailyAttendance)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).EndInit();
            this.groupControl1.ResumeLayout(false);
            this.groupControl1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtDate.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtYear.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtMonth.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtVal.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtSelectTeacher.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.searchLookUpEdit1View)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraGrid.GridControl gridTDailyAttendance;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView1;
        private DevExpress.XtraEditors.GroupControl groupControl1;
        private DevExpress.XtraEditors.SimpleButton BtnTAPrint;
        private DevExpress.XtraEditors.SimpleButton BtnUpdateTAttend;
        private DevExpress.XtraEditors.SimpleButton BtnManageAttendance;
        private DevExpress.XtraEditors.SimpleButton btnTAFind;
        private DevExpress.XtraEditors.LabelControl labelControl9;
        private DevExpress.XtraEditors.LabelControl labelControl8;
        private DevExpress.XtraEditors.LabelControl labelControl7;
        private DevExpress.XtraEditors.ComboBoxEdit txtDate;
        private DevExpress.XtraEditors.ComboBoxEdit txtYear;
        private DevExpress.XtraEditors.ComboBoxEdit txtMonth;
        private DevExpress.XtraEditors.LabelControl labelControl2;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.XtraEditors.SimpleButton btnSave;
        private DevExpress.XtraEditors.SimpleButton btnUpdate;
        private DevExpress.XtraEditors.SimpleButton btnSummary;
        private DevExpress.XtraEditors.LookUpEdit txtVal;
        private DevExpress.XtraEditors.SearchLookUpEdit txtSelectTeacher;
        private DevExpress.XtraGrid.Views.Grid.GridView searchLookUpEdit1View;
    }
}
