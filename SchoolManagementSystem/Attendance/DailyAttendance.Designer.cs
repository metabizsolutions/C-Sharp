namespace SchoolManagementSystem.Attendance
{
    partial class DailyAttendance
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(DailyAttendance));
            this.gridDailyAttendance = new DevExpress.XtraGrid.GridControl();
            this.gridView1 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.groupControl1 = new DevExpress.XtraEditors.GroupControl();
            this.btnSummary = new DevExpress.XtraEditors.SimpleButton();
            this.txtIgnoreDA = new DevExpress.XtraEditors.CheckEdit();
            this.btnFindAll = new DevExpress.XtraEditors.SimpleButton();
            this.btnUpdate = new DevExpress.XtraEditors.SimpleButton();
            this.btnSave = new DevExpress.XtraEditors.SimpleButton();
            this.labelControl3 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl4 = new DevExpress.XtraEditors.LabelControl();
            this.searchLookUpEdit1 = new DevExpress.XtraEditors.SearchLookUpEdit();
            this.searchLookUpEdit1View = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            this.BtnPrint = new DevExpress.XtraEditors.SimpleButton();
            this.BtnUpdateAttend = new DevExpress.XtraEditors.SimpleButton();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.BtnManageAttendance = new DevExpress.XtraEditors.SimpleButton();
            this.btnDAFind = new DevExpress.XtraEditors.SimpleButton();
            this.labelControl9 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl8 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl7 = new DevExpress.XtraEditors.LabelControl();
            this.txtDate = new DevExpress.XtraEditors.ComboBoxEdit();
            this.txtYear = new DevExpress.XtraEditors.ComboBoxEdit();
            this.txtMonth = new DevExpress.XtraEditors.ComboBoxEdit();
            this.txtSection = new DevExpress.XtraEditors.SearchLookUpEdit();
            this.searchLookUpEdit2View = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.txtClass = new DevExpress.XtraEditors.SearchLookUpEdit();
            this.gridView2 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.txtVal = new DevExpress.XtraEditors.LookUpEdit();
            ((System.ComponentModel.ISupportInitialize)(this.gridDailyAttendance)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).BeginInit();
            this.groupControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtIgnoreDA.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.searchLookUpEdit1.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.searchLookUpEdit1View)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtDate.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtYear.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtMonth.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtSection.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.searchLookUpEdit2View)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtClass.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtVal.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // gridDailyAttendance
            // 
            this.gridDailyAttendance.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridDailyAttendance.Location = new System.Drawing.Point(0, 163);
            this.gridDailyAttendance.MainView = this.gridView1;
            this.gridDailyAttendance.Name = "gridDailyAttendance";
            this.gridDailyAttendance.Size = new System.Drawing.Size(1124, 261);
            this.gridDailyAttendance.TabIndex = 7;
            this.gridDailyAttendance.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridView1});
            // 
            // gridView1
            // 
            this.gridView1.GridControl = this.gridDailyAttendance;
            this.gridView1.Name = "gridView1";
            this.gridView1.OptionsBehavior.Editable = false;
            this.gridView1.OptionsBehavior.ReadOnly = true;
            this.gridView1.OptionsFind.AlwaysVisible = true;
            this.gridView1.RowCellStyle += new DevExpress.XtraGrid.Views.Grid.RowCellStyleEventHandler(this.gridView1_RowCellStyle);
            this.gridView1.RowStyle += new DevExpress.XtraGrid.Views.Grid.RowStyleEventHandler(this.gridView1_RowStyle);
            this.gridView1.CustomSummaryCalculate += new DevExpress.Data.CustomSummaryEventHandler(this.gridView1_CustomSummaryCalculate);
            this.gridView1.RowUpdated += new DevExpress.XtraGrid.Views.Base.RowObjectEventHandler(this.gridView1_RowUpdated);
            // 
            // groupControl1
            // 
            this.groupControl1.Controls.Add(this.btnSummary);
            this.groupControl1.Controls.Add(this.txtIgnoreDA);
            this.groupControl1.Controls.Add(this.btnFindAll);
            this.groupControl1.Controls.Add(this.btnUpdate);
            this.groupControl1.Controls.Add(this.btnSave);
            this.groupControl1.Controls.Add(this.labelControl3);
            this.groupControl1.Controls.Add(this.labelControl4);
            this.groupControl1.Controls.Add(this.searchLookUpEdit1);
            this.groupControl1.Controls.Add(this.labelControl2);
            this.groupControl1.Controls.Add(this.BtnPrint);
            this.groupControl1.Controls.Add(this.BtnUpdateAttend);
            this.groupControl1.Controls.Add(this.labelControl1);
            this.groupControl1.Controls.Add(this.BtnManageAttendance);
            this.groupControl1.Controls.Add(this.btnDAFind);
            this.groupControl1.Controls.Add(this.labelControl9);
            this.groupControl1.Controls.Add(this.labelControl8);
            this.groupControl1.Controls.Add(this.labelControl7);
            this.groupControl1.Controls.Add(this.txtDate);
            this.groupControl1.Controls.Add(this.txtYear);
            this.groupControl1.Controls.Add(this.txtMonth);
            this.groupControl1.Controls.Add(this.txtSection);
            this.groupControl1.Controls.Add(this.txtClass);
            this.groupControl1.Controls.Add(this.txtVal);
            this.groupControl1.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupControl1.Location = new System.Drawing.Point(0, 0);
            this.groupControl1.Name = "groupControl1";
            this.groupControl1.Size = new System.Drawing.Size(1124, 163);
            this.groupControl1.TabIndex = 6;
            this.groupControl1.Text = "Daily Attendance";
            // 
            // btnSummary
            // 
            this.btnSummary.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("btnSummary.ImageOptions.Image")));
            this.btnSummary.Location = new System.Drawing.Point(477, 40);
            this.btnSummary.Name = "btnSummary";
            this.btnSummary.Size = new System.Drawing.Size(88, 23);
            this.btnSummary.TabIndex = 98;
            this.btnSummary.Text = "Summary";
            this.btnSummary.Click += new System.EventHandler(this.btnSummary_Click);
            // 
            // txtIgnoreDA
            // 
            this.txtIgnoreDA.Location = new System.Drawing.Point(737, 17);
            this.txtIgnoreDA.Name = "txtIgnoreDA";
            this.txtIgnoreDA.Properties.Caption = "Ignore Attendance Disable";
            this.txtIgnoreDA.Size = new System.Drawing.Size(155, 20);
            this.txtIgnoreDA.TabIndex = 97;
            // 
            // btnFindAll
            // 
            this.btnFindAll.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("btnFindAll.ImageOptions.Image")));
            this.btnFindAll.Location = new System.Drawing.Point(571, 40);
            this.btnFindAll.Name = "btnFindAll";
            this.btnFindAll.Size = new System.Drawing.Size(69, 23);
            this.btnFindAll.TabIndex = 96;
            this.btnFindAll.Text = "Find All";
            this.btnFindAll.Click += new System.EventHandler(this.btnFindAll_Click);
            // 
            // btnUpdate
            // 
            this.btnUpdate.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("btnUpdate.ImageOptions.Image")));
            this.btnUpdate.Location = new System.Drawing.Point(577, 133);
            this.btnUpdate.Name = "btnUpdate";
            this.btnUpdate.Size = new System.Drawing.Size(98, 23);
            this.btnUpdate.TabIndex = 95;
            this.btnUpdate.Text = "update";
            this.btnUpdate.Click += new System.EventHandler(this.btnUpdate_Click);
            // 
            // btnSave
            // 
            this.btnSave.Enabled = false;
            this.btnSave.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("btnSave.ImageOptions.Image")));
            this.btnSave.Location = new System.Drawing.Point(473, 133);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(98, 23);
            this.btnSave.TabIndex = 94;
            this.btnSave.Text = "Save";
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // labelControl3
            // 
            this.labelControl3.Location = new System.Drawing.Point(317, 117);
            this.labelControl3.Name = "labelControl3";
            this.labelControl3.Size = new System.Drawing.Size(27, 13);
            this.labelControl3.TabIndex = 93;
            this.labelControl3.Text = "Value";
            // 
            // labelControl4
            // 
            this.labelControl4.Location = new System.Drawing.Point(5, 116);
            this.labelControl4.Name = "labelControl4";
            this.labelControl4.Size = new System.Drawing.Size(70, 13);
            this.labelControl4.TabIndex = 91;
            this.labelControl4.Text = "Select Student";
            // 
            // searchLookUpEdit1
            // 
            this.searchLookUpEdit1.Location = new System.Drawing.Point(5, 135);
            this.searchLookUpEdit1.Name = "searchLookUpEdit1";
            this.searchLookUpEdit1.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.searchLookUpEdit1.Properties.PopupView = this.searchLookUpEdit1View;
            this.searchLookUpEdit1.Size = new System.Drawing.Size(306, 20);
            this.searchLookUpEdit1.TabIndex = 90;
            // 
            // searchLookUpEdit1View
            // 
            this.searchLookUpEdit1View.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus;
            this.searchLookUpEdit1View.Name = "searchLookUpEdit1View";
            this.searchLookUpEdit1View.OptionsSelection.EnableAppearanceFocusedCell = false;
            this.searchLookUpEdit1View.OptionsView.ShowGroupPanel = false;
            // 
            // labelControl2
            // 
            this.labelControl2.Location = new System.Drawing.Point(5, 66);
            this.labelControl2.Name = "labelControl2";
            this.labelControl2.Size = new System.Drawing.Size(36, 13);
            this.labelControl2.TabIndex = 88;
            this.labelControl2.Text = "Section";
            // 
            // BtnPrint
            // 
            this.BtnPrint.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("BtnPrint.ImageOptions.Image")));
            this.BtnPrint.Location = new System.Drawing.Point(379, 82);
            this.BtnPrint.Name = "BtnPrint";
            this.BtnPrint.Size = new System.Drawing.Size(90, 23);
            this.BtnPrint.TabIndex = 87;
            this.BtnPrint.Text = "Print";
            this.BtnPrint.Click += new System.EventHandler(this.BtnPrint_Click);
            // 
            // BtnUpdateAttend
            // 
            this.BtnUpdateAttend.Enabled = false;
            this.BtnUpdateAttend.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("BtnUpdateAttend.ImageOptions.Image")));
            this.BtnUpdateAttend.Location = new System.Drawing.Point(302, 82);
            this.BtnUpdateAttend.Name = "BtnUpdateAttend";
            this.BtnUpdateAttend.Size = new System.Drawing.Size(71, 23);
            this.BtnUpdateAttend.TabIndex = 63;
            this.BtnUpdateAttend.Text = "Update";
            this.BtnUpdateAttend.Click += new System.EventHandler(this.BtnUpdateAttend_Click);
            // 
            // labelControl1
            // 
            this.labelControl1.Location = new System.Drawing.Point(646, 26);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(25, 13);
            this.labelControl1.TabIndex = 61;
            this.labelControl1.Text = "Class";
            // 
            // BtnManageAttendance
            // 
            this.BtnManageAttendance.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("BtnManageAttendance.ImageOptions.Image")));
            this.BtnManageAttendance.Location = new System.Drawing.Point(161, 82);
            this.BtnManageAttendance.Name = "BtnManageAttendance";
            this.BtnManageAttendance.Size = new System.Drawing.Size(135, 23);
            this.BtnManageAttendance.TabIndex = 60;
            this.BtnManageAttendance.Text = "Manage Attendance";
            this.BtnManageAttendance.Click += new System.EventHandler(this.BtnManageAttendance_Click);
            // 
            // btnDAFind
            // 
            this.btnDAFind.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("btnDAFind.ImageOptions.Image")));
            this.btnDAFind.Location = new System.Drawing.Point(802, 43);
            this.btnDAFind.Name = "btnDAFind";
            this.btnDAFind.Size = new System.Drawing.Size(52, 23);
            this.btnDAFind.TabIndex = 59;
            this.btnDAFind.Text = "Find";
            this.btnDAFind.Click += new System.EventHandler(this.btnDAFind_Click);
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
            // txtSection
            // 
            this.txtSection.Location = new System.Drawing.Point(5, 85);
            this.txtSection.Name = "txtSection";
            this.txtSection.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.txtSection.Properties.NullText = "";
            this.txtSection.Properties.PopupSizeable = false;
            this.txtSection.Properties.PopupView = this.searchLookUpEdit2View;
            this.txtSection.Size = new System.Drawing.Size(150, 20);
            this.txtSection.TabIndex = 89;
            // 
            // searchLookUpEdit2View
            // 
            this.searchLookUpEdit2View.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus;
            this.searchLookUpEdit2View.Name = "searchLookUpEdit2View";
            this.searchLookUpEdit2View.OptionsSelection.EnableAppearanceFocusedCell = false;
            this.searchLookUpEdit2View.OptionsView.ShowGroupPanel = false;
            // 
            // txtClass
            // 
            this.txtClass.EditValue = System.Drawing.Color.Empty;
            this.txtClass.Location = new System.Drawing.Point(646, 43);
            this.txtClass.Name = "txtClass";
            this.txtClass.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.txtClass.Properties.NullText = "";
            this.txtClass.Properties.PopupView = this.gridView2;
            this.txtClass.Size = new System.Drawing.Size(150, 20);
            this.txtClass.TabIndex = 62;
            this.txtClass.EditValueChanged += new System.EventHandler(this.txtClass_EditValueChanged);
            // 
            // gridView2
            // 
            this.gridView2.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus;
            this.gridView2.Name = "gridView2";
            this.gridView2.OptionsSelection.EnableAppearanceFocusedCell = false;
            this.gridView2.OptionsView.ShowGroupPanel = false;
            // 
            // txtVal
            // 
            this.txtVal.Location = new System.Drawing.Point(317, 135);
            this.txtVal.Name = "txtVal";
            this.txtVal.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.txtVal.Properties.NullText = "";
            this.txtVal.Size = new System.Drawing.Size(150, 20);
            this.txtVal.TabIndex = 92;
            // 
            // DailyAttendance
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.gridDailyAttendance);
            this.Controls.Add(this.groupControl1);
            this.Name = "DailyAttendance";
            this.Size = new System.Drawing.Size(1124, 424);
            this.Enter += new System.EventHandler(this.DailyAttendance_Enter);
            ((System.ComponentModel.ISupportInitialize)(this.gridDailyAttendance)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).EndInit();
            this.groupControl1.ResumeLayout(false);
            this.groupControl1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtIgnoreDA.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.searchLookUpEdit1.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.searchLookUpEdit1View)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtDate.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtYear.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtMonth.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtSection.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.searchLookUpEdit2View)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtClass.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtVal.Properties)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraGrid.GridControl gridDailyAttendance;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView1;
        private DevExpress.XtraEditors.GroupControl groupControl1;
        private DevExpress.XtraEditors.SimpleButton btnDAFind;
        private DevExpress.XtraEditors.LabelControl labelControl9;
        private DevExpress.XtraEditors.LabelControl labelControl8;
        private DevExpress.XtraEditors.LabelControl labelControl7;
        private DevExpress.XtraEditors.ComboBoxEdit txtDate;
        private DevExpress.XtraEditors.ComboBoxEdit txtYear;
        private DevExpress.XtraEditors.ComboBoxEdit txtMonth;
        private DevExpress.XtraEditors.SimpleButton BtnManageAttendance;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.XtraEditors.SimpleButton BtnUpdateAttend;
        private DevExpress.XtraEditors.SimpleButton BtnPrint;
        private DevExpress.XtraEditors.LabelControl labelControl2;
        private DevExpress.XtraEditors.LabelControl labelControl4;
        private DevExpress.XtraEditors.SearchLookUpEdit searchLookUpEdit1;
        private DevExpress.XtraGrid.Views.Grid.GridView searchLookUpEdit1View;
        private DevExpress.XtraEditors.LabelControl labelControl3;
        private DevExpress.XtraEditors.SimpleButton btnUpdate;
        private DevExpress.XtraEditors.SimpleButton btnSave;
        private DevExpress.XtraEditors.SimpleButton btnFindAll;
        private DevExpress.XtraEditors.CheckEdit txtIgnoreDA;
        private DevExpress.XtraEditors.SimpleButton btnSummary;
        private DevExpress.XtraEditors.SearchLookUpEdit txtSection;
        private DevExpress.XtraGrid.Views.Grid.GridView searchLookUpEdit2View;
        private DevExpress.XtraEditors.SearchLookUpEdit txtClass;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView2;
        private DevExpress.XtraEditors.LookUpEdit txtVal;
    }
}
