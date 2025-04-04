namespace SchoolManagementSystem.Attendance
{
    partial class StudentAttendance
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(StudentAttendance));
            this.gridStudentAttendance = new DevExpress.XtraGrid.GridControl();
            this.gridView1 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.groupControl1 = new DevExpress.XtraEditors.GroupControl();
            this.labelControl3 = new DevExpress.XtraEditors.LabelControl();
            this.txta = new DevExpress.XtraEditors.LabelControl();
            this.labelControl4 = new DevExpress.XtraEditors.LabelControl();
            this.txtp = new DevExpress.XtraEditors.LabelControl();
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.txtFMonth = new DevExpress.XtraEditors.DateEdit();
            this.txtTMonth = new DevExpress.XtraEditors.DateEdit();
            this.BtnPrint = new DevExpress.XtraEditors.SimpleButton();
            this.btnSAFind = new DevExpress.XtraEditors.SimpleButton();
            this.labelControl9 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl8 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl7 = new DevExpress.XtraEditors.LabelControl();
            this.txtClass = new DevExpress.XtraEditors.SearchLookUpEdit();
            this.searchLookUpEdit1View = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.txtSection = new DevExpress.XtraEditors.SearchLookUpEdit();
            this.gridView2 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.txtStudent = new DevExpress.XtraEditors.SearchLookUpEdit();
            this.gridView3 = new DevExpress.XtraGrid.Views.Grid.GridView();
            ((System.ComponentModel.ISupportInitialize)(this.gridStudentAttendance)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).BeginInit();
            this.groupControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtFMonth.Properties.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtFMonth.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtTMonth.Properties.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtTMonth.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtClass.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.searchLookUpEdit1View)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtSection.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtStudent.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView3)).BeginInit();
            this.SuspendLayout();
            // 
            // gridStudentAttendance
            // 
            this.gridStudentAttendance.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridStudentAttendance.Location = new System.Drawing.Point(0, 72);
            this.gridStudentAttendance.MainView = this.gridView1;
            this.gridStudentAttendance.Name = "gridStudentAttendance";
            this.gridStudentAttendance.Size = new System.Drawing.Size(1069, 337);
            this.gridStudentAttendance.TabIndex = 11;
            this.gridStudentAttendance.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridView1});
            // 
            // gridView1
            // 
            this.gridView1.GridControl = this.gridStudentAttendance;
            this.gridView1.Name = "gridView1";
            this.gridView1.OptionsBehavior.Editable = false;
            this.gridView1.OptionsBehavior.ReadOnly = true;
            this.gridView1.OptionsFind.AlwaysVisible = true;
            this.gridView1.RowCellStyle += new DevExpress.XtraGrid.Views.Grid.RowCellStyleEventHandler(this.gridView1_RowCellStyle);
            this.gridView1.RowStyle += new DevExpress.XtraGrid.Views.Grid.RowStyleEventHandler(this.gridView1_RowStyle);
            // 
            // groupControl1
            // 
            this.groupControl1.Controls.Add(this.labelControl3);
            this.groupControl1.Controls.Add(this.txta);
            this.groupControl1.Controls.Add(this.labelControl4);
            this.groupControl1.Controls.Add(this.txtp);
            this.groupControl1.Controls.Add(this.labelControl2);
            this.groupControl1.Controls.Add(this.labelControl1);
            this.groupControl1.Controls.Add(this.txtFMonth);
            this.groupControl1.Controls.Add(this.txtTMonth);
            this.groupControl1.Controls.Add(this.BtnPrint);
            this.groupControl1.Controls.Add(this.btnSAFind);
            this.groupControl1.Controls.Add(this.labelControl9);
            this.groupControl1.Controls.Add(this.labelControl8);
            this.groupControl1.Controls.Add(this.labelControl7);
            this.groupControl1.Controls.Add(this.txtClass);
            this.groupControl1.Controls.Add(this.txtSection);
            this.groupControl1.Controls.Add(this.txtStudent);
            this.groupControl1.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupControl1.Location = new System.Drawing.Point(0, 0);
            this.groupControl1.Name = "groupControl1";
            this.groupControl1.Size = new System.Drawing.Size(1069, 72);
            this.groupControl1.TabIndex = 10;
            this.groupControl1.Text = "Class Attendance";
            // 
            // labelControl3
            // 
            this.labelControl3.Location = new System.Drawing.Point(473, 23);
            this.labelControl3.Name = "labelControl3";
            this.labelControl3.Size = new System.Drawing.Size(35, 13);
            this.labelControl3.TabIndex = 96;
            this.labelControl3.Text = "Section";
            // 
            // txta
            // 
            this.txta.Location = new System.Drawing.Point(1046, 45);
            this.txta.Name = "txta";
            this.txta.Size = new System.Drawing.Size(0, 13);
            this.txta.TabIndex = 95;
            // 
            // labelControl4
            // 
            this.labelControl4.Location = new System.Drawing.Point(1002, 45);
            this.labelControl4.Name = "labelControl4";
            this.labelControl4.Size = new System.Drawing.Size(38, 13);
            this.labelControl4.TabIndex = 94;
            this.labelControl4.Text = "Absent:";
            // 
            // txtp
            // 
            this.txtp.Location = new System.Drawing.Point(986, 45);
            this.txtp.Name = "txtp";
            this.txtp.Size = new System.Drawing.Size(0, 13);
            this.txtp.TabIndex = 93;
            // 
            // labelControl2
            // 
            this.labelControl2.Location = new System.Drawing.Point(939, 45);
            this.labelControl2.Name = "labelControl2";
            this.labelControl2.Size = new System.Drawing.Size(41, 13);
            this.labelControl2.TabIndex = 92;
            this.labelControl2.Text = "Present:";
            // 
            // labelControl1
            // 
            this.labelControl1.Location = new System.Drawing.Point(629, 23);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(70, 13);
            this.labelControl1.TabIndex = 91;
            this.labelControl1.Text = "Select Student";
            // 
            // txtFMonth
            // 
            this.txtFMonth.EditValue = null;
            this.txtFMonth.Location = new System.Drawing.Point(5, 42);
            this.txtFMonth.Name = "txtFMonth";
            this.txtFMonth.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.txtFMonth.Properties.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.txtFMonth.Properties.DisplayFormat.FormatString = "";
            this.txtFMonth.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.txtFMonth.Properties.EditFormat.FormatString = "";
            this.txtFMonth.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.txtFMonth.Properties.Mask.EditMask = "";
            this.txtFMonth.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.None;
            this.txtFMonth.Size = new System.Drawing.Size(150, 20);
            this.txtFMonth.TabIndex = 88;
            // 
            // txtTMonth
            // 
            this.txtTMonth.EditValue = null;
            this.txtTMonth.Location = new System.Drawing.Point(161, 42);
            this.txtTMonth.Name = "txtTMonth";
            this.txtTMonth.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.txtTMonth.Properties.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.txtTMonth.Properties.DisplayFormat.FormatString = "";
            this.txtTMonth.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.txtTMonth.Properties.EditFormat.FormatString = "";
            this.txtTMonth.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.txtTMonth.Properties.Mask.EditMask = "";
            this.txtTMonth.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.None;
            this.txtTMonth.Size = new System.Drawing.Size(150, 20);
            this.txtTMonth.TabIndex = 89;
            // 
            // BtnPrint
            // 
            this.BtnPrint.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("BtnPrint.ImageOptions.Image")));
            this.BtnPrint.Location = new System.Drawing.Point(858, 39);
            this.BtnPrint.Name = "BtnPrint";
            this.BtnPrint.Size = new System.Drawing.Size(75, 23);
            this.BtnPrint.TabIndex = 87;
            this.BtnPrint.Text = "Print";
            this.BtnPrint.Click += new System.EventHandler(this.BtnPrint_Click);
            // 
            // btnSAFind
            // 
            this.btnSAFind.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("btnSAFind.ImageOptions.Image")));
            this.btnSAFind.Location = new System.Drawing.Point(785, 39);
            this.btnSAFind.Name = "btnSAFind";
            this.btnSAFind.Size = new System.Drawing.Size(67, 23);
            this.btnSAFind.TabIndex = 59;
            this.btnSAFind.Text = "Find";
            this.btnSAFind.Click += new System.EventHandler(this.btnSAFind_Click);
            // 
            // labelControl9
            // 
            this.labelControl9.Location = new System.Drawing.Point(317, 23);
            this.labelControl9.Name = "labelControl9";
            this.labelControl9.Size = new System.Drawing.Size(25, 13);
            this.labelControl9.TabIndex = 53;
            this.labelControl9.Text = "Class";
            // 
            // labelControl8
            // 
            this.labelControl8.Location = new System.Drawing.Point(161, 23);
            this.labelControl8.Name = "labelControl8";
            this.labelControl8.Size = new System.Drawing.Size(12, 13);
            this.labelControl8.TabIndex = 51;
            this.labelControl8.Text = "To";
            // 
            // labelControl7
            // 
            this.labelControl7.Location = new System.Drawing.Point(5, 23);
            this.labelControl7.Name = "labelControl7";
            this.labelControl7.Size = new System.Drawing.Size(24, 13);
            this.labelControl7.TabIndex = 49;
            this.labelControl7.Text = "From";
            // 
            // txtClass
            // 
            this.txtClass.Location = new System.Drawing.Point(317, 42);
            this.txtClass.Name = "txtClass";
            this.txtClass.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.txtClass.Properties.NullText = "";
            this.txtClass.Properties.PopupSizeable = false;
            this.txtClass.Properties.PopupView = this.searchLookUpEdit1View;
            this.txtClass.Size = new System.Drawing.Size(150, 20);
            this.txtClass.TabIndex = 54;
            this.txtClass.EditValueChanged += new System.EventHandler(this.txtClass_EditValueChanged);
            // 
            // searchLookUpEdit1View
            // 
            this.searchLookUpEdit1View.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus;
            this.searchLookUpEdit1View.Name = "searchLookUpEdit1View";
            this.searchLookUpEdit1View.OptionsSelection.EnableAppearanceFocusedCell = false;
            this.searchLookUpEdit1View.OptionsView.ShowGroupPanel = false;
            // 
            // txtSection
            // 
            this.txtSection.Location = new System.Drawing.Point(473, 42);
            this.txtSection.Name = "txtSection";
            this.txtSection.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.txtSection.Properties.NullText = "";
            this.txtSection.Properties.PopupSizeable = false;
            this.txtSection.Properties.PopupView = this.gridView2;
            this.txtSection.Size = new System.Drawing.Size(150, 20);
            this.txtSection.TabIndex = 97;
            this.txtSection.EditValueChanged += new System.EventHandler(this.txtSection_EditValueChanged);
            // 
            // gridView2
            // 
            this.gridView2.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus;
            this.gridView2.Name = "gridView2";
            this.gridView2.OptionsSelection.EnableAppearanceFocusedCell = false;
            this.gridView2.OptionsView.ShowGroupPanel = false;
            // 
            // txtStudent
            // 
            this.txtStudent.Location = new System.Drawing.Point(629, 42);
            this.txtStudent.Name = "txtStudent";
            this.txtStudent.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.txtStudent.Properties.NullText = "";
            this.txtStudent.Properties.PopupSizeable = false;
            this.txtStudent.Properties.PopupView = this.gridView3;
            this.txtStudent.Size = new System.Drawing.Size(150, 20);
            this.txtStudent.TabIndex = 90;
            // 
            // gridView3
            // 
            this.gridView3.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus;
            this.gridView3.Name = "gridView3";
            this.gridView3.OptionsSelection.EnableAppearanceFocusedCell = false;
            this.gridView3.OptionsView.ShowGroupPanel = false;
            // 
            // StudentAttendance
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.gridStudentAttendance);
            this.Controls.Add(this.groupControl1);
            this.Name = "StudentAttendance";
            this.Size = new System.Drawing.Size(1069, 409);
            this.Enter += new System.EventHandler(this.StudentAttendance_Enter);
            ((System.ComponentModel.ISupportInitialize)(this.gridStudentAttendance)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).EndInit();
            this.groupControl1.ResumeLayout(false);
            this.groupControl1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtFMonth.Properties.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtFMonth.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtTMonth.Properties.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtTMonth.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtClass.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.searchLookUpEdit1View)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtSection.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtStudent.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView3)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraGrid.GridControl gridStudentAttendance;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView1;
        private DevExpress.XtraEditors.GroupControl groupControl1;
        private DevExpress.XtraEditors.SimpleButton BtnPrint;
        private DevExpress.XtraEditors.SimpleButton btnSAFind;
        private DevExpress.XtraEditors.LabelControl labelControl9;
        private DevExpress.XtraEditors.LabelControl labelControl8;
        private DevExpress.XtraEditors.LabelControl labelControl7;
        private DevExpress.XtraEditors.DateEdit txtFMonth;
        private DevExpress.XtraEditors.DateEdit txtTMonth;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.XtraEditors.LabelControl txta;
        private DevExpress.XtraEditors.LabelControl labelControl4;
        private DevExpress.XtraEditors.LabelControl txtp;
        private DevExpress.XtraEditors.LabelControl labelControl2;
        private DevExpress.XtraEditors.LabelControl labelControl3;
        private DevExpress.XtraEditors.SearchLookUpEdit txtClass;
        private DevExpress.XtraGrid.Views.Grid.GridView searchLookUpEdit1View;
        private DevExpress.XtraEditors.SearchLookUpEdit txtSection;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView2;
        private DevExpress.XtraEditors.SearchLookUpEdit txtStudent;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView3;
    }
}
