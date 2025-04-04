namespace SchoolManagementSystem.Exam
{
    partial class AwradList
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AwradList));
            this.groupControl1 = new DevExpress.XtraEditors.GroupControl();
            this.gridLookUpEdit_teacher = new DevExpress.XtraEditors.GridLookUpEdit();
            this.gridView2 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.btntoggleReport = new DevExpress.XtraEditors.ToggleSwitch();
            this.labelControl3 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl7 = new DevExpress.XtraEditors.LabelControl();
            this.btnMMPrintP = new DevExpress.XtraEditors.SimpleButton();
            this.btnmanag_byteacher = new DevExpress.XtraEditors.SimpleButton();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.btnMMAdd = new DevExpress.XtraEditors.SimpleButton();
            this.labelControl8 = new DevExpress.XtraEditors.LabelControl();
            this.txtExam = new DevExpress.XtraEditors.SearchLookUpEdit();
            this.searchLookUpEdit1View = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.txtClass = new DevExpress.XtraEditors.SearchLookUpEdit();
            this.gridView3 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.txtSection = new DevExpress.XtraEditors.SearchLookUpEdit();
            this.gridView4 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.txtSubject = new DevExpress.XtraEditors.SearchLookUpEdit();
            this.gridView5 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.gridAwardList = new DevExpress.XtraGrid.GridControl();
            this.gridView1 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.btn_excel = new DevExpress.XtraEditors.SimpleButton();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).BeginInit();
            this.groupControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridLookUpEdit_teacher.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btntoggleReport.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtExam.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.searchLookUpEdit1View)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtClass.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtSection.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtSubject.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView5)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridAwardList)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // groupControl1
            // 
            this.groupControl1.Controls.Add(this.gridLookUpEdit_teacher);
            this.groupControl1.Controls.Add(this.btntoggleReport);
            this.groupControl1.Controls.Add(this.labelControl3);
            this.groupControl1.Controls.Add(this.labelControl2);
            this.groupControl1.Controls.Add(this.labelControl7);
            this.groupControl1.Controls.Add(this.btn_excel);
            this.groupControl1.Controls.Add(this.btnMMPrintP);
            this.groupControl1.Controls.Add(this.btnmanag_byteacher);
            this.groupControl1.Controls.Add(this.labelControl1);
            this.groupControl1.Controls.Add(this.btnMMAdd);
            this.groupControl1.Controls.Add(this.labelControl8);
            this.groupControl1.Controls.Add(this.txtExam);
            this.groupControl1.Controls.Add(this.txtClass);
            this.groupControl1.Controls.Add(this.txtSection);
            this.groupControl1.Controls.Add(this.txtSubject);
            this.groupControl1.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupControl1.Location = new System.Drawing.Point(0, 0);
            this.groupControl1.Name = "groupControl1";
            this.groupControl1.Size = new System.Drawing.Size(1269, 80);
            this.groupControl1.TabIndex = 7;
            this.groupControl1.Text = "Add Manage Marks";
            // 
            // gridLookUpEdit_teacher
            // 
            this.gridLookUpEdit_teacher.Location = new System.Drawing.Point(853, 46);
            this.gridLookUpEdit_teacher.Margin = new System.Windows.Forms.Padding(3, 8, 3, 3);
            this.gridLookUpEdit_teacher.Name = "gridLookUpEdit_teacher";
            this.gridLookUpEdit_teacher.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.gridLookUpEdit_teacher.Properties.PopupView = this.gridView2;
            this.gridLookUpEdit_teacher.Size = new System.Drawing.Size(119, 20);
            this.gridLookUpEdit_teacher.TabIndex = 71;
            // 
            // gridView2
            // 
            this.gridView2.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus;
            this.gridView2.Name = "gridView2";
            this.gridView2.OptionsSelection.EnableAppearanceFocusedCell = false;
            this.gridView2.OptionsView.ShowGroupPanel = false;
            // 
            // btntoggleReport
            // 
            this.btntoggleReport.Location = new System.Drawing.Point(637, 47);
            this.btntoggleReport.Name = "btntoggleReport";
            this.btntoggleReport.Properties.OffText = "Full Details";
            this.btntoggleReport.Properties.OnText = "Half Details";
            this.btntoggleReport.Size = new System.Drawing.Size(107, 18);
            this.btntoggleReport.TabIndex = 70;
            // 
            // labelControl3
            // 
            this.labelControl3.Location = new System.Drawing.Point(9, 28);
            this.labelControl3.Name = "labelControl3";
            this.labelControl3.Size = new System.Drawing.Size(58, 13);
            this.labelControl3.TabIndex = 68;
            this.labelControl3.Text = "Select Exam";
            // 
            // labelControl2
            // 
            this.labelControl2.Location = new System.Drawing.Point(853, 28);
            this.labelControl2.Name = "labelControl2";
            this.labelControl2.Size = new System.Drawing.Size(71, 13);
            this.labelControl2.TabIndex = 66;
            this.labelControl2.Text = "Select Teacher";
            // 
            // labelControl7
            // 
            this.labelControl7.Location = new System.Drawing.Point(435, 27);
            this.labelControl7.Name = "labelControl7";
            this.labelControl7.Size = new System.Drawing.Size(73, 13);
            this.labelControl7.TabIndex = 66;
            this.labelControl7.Text = "Select Subjecct";
            // 
            // btnMMPrintP
            // 
            this.btnMMPrintP.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("btnMMPrintP.ImageOptions.Image")));
            this.btnMMPrintP.Location = new System.Drawing.Point(750, 52);
            this.btnMMPrintP.Name = "btnMMPrintP";
            this.btnMMPrintP.Size = new System.Drawing.Size(59, 23);
            this.btnMMPrintP.TabIndex = 62;
            this.btnMMPrintP.Text = "Print";
            this.btnMMPrintP.Click += new System.EventHandler(this.btnMMPrintP_Click);
            // 
            // btnmanag_byteacher
            // 
            this.btnmanag_byteacher.Location = new System.Drawing.Point(977, 44);
            this.btnmanag_byteacher.Name = "btnmanag_byteacher";
            this.btnmanag_byteacher.Size = new System.Drawing.Size(104, 23);
            this.btnmanag_byteacher.TabIndex = 59;
            this.btnmanag_byteacher.Text = "Manage By Teacher";
            this.btnmanag_byteacher.Click += new System.EventHandler(this.btnmanag_byteacher_Click);
            // 
            // labelControl1
            // 
            this.labelControl1.Location = new System.Drawing.Point(295, 27);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(67, 13);
            this.labelControl1.TabIndex = 60;
            this.labelControl1.Text = "Select Section";
            // 
            // btnMMAdd
            // 
            this.btnMMAdd.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("btnMMAdd.ImageOptions.Image")));
            this.btnMMAdd.Location = new System.Drawing.Point(566, 43);
            this.btnMMAdd.Name = "btnMMAdd";
            this.btnMMAdd.Size = new System.Drawing.Size(65, 23);
            this.btnMMAdd.TabIndex = 59;
            this.btnMMAdd.Text = "Manage";
            this.btnMMAdd.Click += new System.EventHandler(this.btnMMAdd_Click);
            // 
            // labelControl8
            // 
            this.labelControl8.Location = new System.Drawing.Point(149, 28);
            this.labelControl8.Name = "labelControl8";
            this.labelControl8.Size = new System.Drawing.Size(57, 13);
            this.labelControl8.TabIndex = 51;
            this.labelControl8.Text = "Select Class";
            // 
            // txtExam
            // 
            this.txtExam.Location = new System.Drawing.Point(5, 46);
            this.txtExam.Name = "txtExam";
            this.txtExam.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.txtExam.Properties.DisplayFormat.FormatString = "d";
            this.txtExam.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.txtExam.Properties.EditFormat.FormatString = "d";
            this.txtExam.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.txtExam.Properties.NullText = "";
            this.txtExam.Properties.PopupSizeable = false;
            this.txtExam.Properties.PopupView = this.searchLookUpEdit1View;
            this.txtExam.Size = new System.Drawing.Size(138, 20);
            this.txtExam.TabIndex = 69;
            // 
            // searchLookUpEdit1View
            // 
            this.searchLookUpEdit1View.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus;
            this.searchLookUpEdit1View.Name = "searchLookUpEdit1View";
            this.searchLookUpEdit1View.OptionsSelection.EnableAppearanceFocusedCell = false;
            this.searchLookUpEdit1View.OptionsView.ShowGroupPanel = false;
            // 
            // txtClass
            // 
            this.txtClass.Location = new System.Drawing.Point(149, 46);
            this.txtClass.Name = "txtClass";
            this.txtClass.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.txtClass.Properties.DisplayFormat.FormatString = "d";
            this.txtClass.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.txtClass.Properties.EditFormat.FormatString = "d";
            this.txtClass.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.txtClass.Properties.NullText = "";
            this.txtClass.Properties.PopupSizeable = false;
            this.txtClass.Properties.PopupView = this.gridView3;
            this.txtClass.Size = new System.Drawing.Size(140, 20);
            this.txtClass.TabIndex = 52;
            this.txtClass.EditValueChanged += new System.EventHandler(this.txtClass_EditValueChanged);
            // 
            // gridView3
            // 
            this.gridView3.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus;
            this.gridView3.Name = "gridView3";
            this.gridView3.OptionsSelection.EnableAppearanceFocusedCell = false;
            this.gridView3.OptionsView.ShowGroupPanel = false;
            // 
            // txtSection
            // 
            this.txtSection.Location = new System.Drawing.Point(295, 46);
            this.txtSection.Name = "txtSection";
            this.txtSection.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.txtSection.Properties.DisplayFormat.FormatString = "d";
            this.txtSection.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.txtSection.Properties.EditFormat.FormatString = "d";
            this.txtSection.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.txtSection.Properties.NullText = "";
            this.txtSection.Properties.PopupSizeable = false;
            this.txtSection.Properties.PopupView = this.gridView4;
            this.txtSection.Size = new System.Drawing.Size(134, 20);
            this.txtSection.TabIndex = 61;
            this.txtSection.EditValueChanged += new System.EventHandler(this.txtSection_EditValueChanged);
            // 
            // gridView4
            // 
            this.gridView4.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus;
            this.gridView4.Name = "gridView4";
            this.gridView4.OptionsSelection.EnableAppearanceFocusedCell = false;
            this.gridView4.OptionsView.ShowGroupPanel = false;
            // 
            // txtSubject
            // 
            this.txtSubject.Location = new System.Drawing.Point(435, 46);
            this.txtSubject.Name = "txtSubject";
            this.txtSubject.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.txtSubject.Properties.NullText = "";
            this.txtSubject.Properties.PopupSizeable = false;
            this.txtSubject.Properties.PopupView = this.gridView5;
            this.txtSubject.Size = new System.Drawing.Size(125, 20);
            this.txtSubject.TabIndex = 67;
            this.txtSubject.EditValueChanged += new System.EventHandler(this.txtSubject_EditValueChanged);
            // 
            // gridView5
            // 
            this.gridView5.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus;
            this.gridView5.Name = "gridView5";
            this.gridView5.OptionsSelection.EnableAppearanceFocusedCell = false;
            this.gridView5.OptionsView.ShowGroupPanel = false;
            // 
            // gridAwardList
            // 
            this.gridAwardList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridAwardList.Location = new System.Drawing.Point(0, 80);
            this.gridAwardList.MainView = this.gridView1;
            this.gridAwardList.Name = "gridAwardList";
            this.gridAwardList.Size = new System.Drawing.Size(1269, 325);
            this.gridAwardList.TabIndex = 8;
            this.gridAwardList.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridView1});
            // 
            // gridView1
            // 
            this.gridView1.GridControl = this.gridAwardList;
            this.gridView1.Name = "gridView1";
            this.gridView1.CustomSummaryCalculate += new DevExpress.Data.CustomSummaryEventHandler(this.gridView1_CustomSummaryCalculate);
            // 
            // btn_excel
            // 
            this.btn_excel.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("simpleButton1.ImageOptions.Image")));
            this.btn_excel.Location = new System.Drawing.Point(750, 23);
            this.btn_excel.Name = "btn_excel";
            this.btn_excel.Size = new System.Drawing.Size(59, 23);
            this.btn_excel.TabIndex = 62;
            this.btn_excel.Text = "Excel";
            this.btn_excel.Click += new System.EventHandler(this.btn_excel_Click);
            // 
            // AwradList
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.gridAwardList);
            this.Controls.Add(this.groupControl1);
            this.Name = "AwradList";
            this.Size = new System.Drawing.Size(1269, 405);
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).EndInit();
            this.groupControl1.ResumeLayout(false);
            this.groupControl1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridLookUpEdit_teacher.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btntoggleReport.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtExam.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.searchLookUpEdit1View)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtClass.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtSection.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtSubject.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView5)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridAwardList)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.GroupControl groupControl1;
        private DevExpress.XtraEditors.SimpleButton btnMMPrintP;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.XtraEditors.SimpleButton btnMMAdd;
        private DevExpress.XtraEditors.LabelControl labelControl8;
        private DevExpress.XtraGrid.GridControl gridAwardList;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView1;
        private DevExpress.XtraEditors.LabelControl labelControl7;
        private DevExpress.XtraEditors.LabelControl labelControl3;
        private DevExpress.XtraEditors.ToggleSwitch btntoggleReport;
        private DevExpress.XtraEditors.LabelControl labelControl2;
        private DevExpress.XtraEditors.SimpleButton btnmanag_byteacher;
        private DevExpress.XtraEditors.GridLookUpEdit gridLookUpEdit_teacher;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView2;
        private DevExpress.XtraEditors.SearchLookUpEdit txtExam;
        private DevExpress.XtraGrid.Views.Grid.GridView searchLookUpEdit1View;
        private DevExpress.XtraEditors.SearchLookUpEdit txtClass;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView3;
        private DevExpress.XtraEditors.SearchLookUpEdit txtSection;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView4;
        private DevExpress.XtraEditors.SearchLookUpEdit txtSubject;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView5;
        private DevExpress.XtraEditors.SimpleButton btn_excel;
    }
}
