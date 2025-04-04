namespace SchoolManagementSystem.Exam
{
    partial class ManageMarksGovt
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ManageMarksGovt));
            this.gridManageMarks = new DevExpress.XtraGrid.GridControl();
            this.gridView1 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.groupControl1 = new DevExpress.XtraEditors.GroupControl();
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            this.btnMMPrintP = new DevExpress.XtraEditors.SimpleButton();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.btnMMAdd = new DevExpress.XtraEditors.SimpleButton();
            this.labelControl8 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl7 = new DevExpress.XtraEditors.LabelControl();
            this.txtExam = new DevExpress.XtraEditors.SearchLookUpEdit();
            this.searchLookUpEdit1View = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.txtClass = new DevExpress.XtraEditors.SearchLookUpEdit();
            this.gridView2 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.labelControl4 = new DevExpress.XtraEditors.LabelControl();
            this.txtSection = new DevExpress.XtraEditors.CheckedComboBoxEdit();
            this.txtSubject = new DevExpress.XtraEditors.CheckedComboBoxEdit();
            ((System.ComponentModel.ISupportInitialize)(this.gridManageMarks)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).BeginInit();
            this.groupControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtExam.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.searchLookUpEdit1View)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtClass.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtSection.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtSubject.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // gridManageMarks
            // 
            this.gridManageMarks.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridManageMarks.Location = new System.Drawing.Point(0, 70);
            this.gridManageMarks.MainView = this.gridView1;
            this.gridManageMarks.Name = "gridManageMarks";
            this.gridManageMarks.Size = new System.Drawing.Size(977, 355);
            this.gridManageMarks.TabIndex = 7;
            this.gridManageMarks.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridView1});
            this.gridManageMarks.EditorKeyDown += new System.Windows.Forms.KeyEventHandler(this.gridManageMarks_EditorKeyDown);
            // 
            // gridView1
            // 
            this.gridView1.GridControl = this.gridManageMarks;
            this.gridView1.Name = "gridView1";
            this.gridView1.OptionsBehavior.EditingMode = DevExpress.XtraGrid.Views.Grid.GridEditingMode.Inplace;
            this.gridView1.OptionsFind.AlwaysVisible = true;
            this.gridView1.InvalidRowException += new DevExpress.XtraGrid.Views.Base.InvalidRowExceptionEventHandler(this.gridView1_InvalidRowException);
            this.gridView1.ValidateRow += new DevExpress.XtraGrid.Views.Base.ValidateRowEventHandler(this.gridView1_ValidateRow);
            this.gridView1.RowUpdated += new DevExpress.XtraGrid.Views.Base.RowObjectEventHandler(this.gridView1_RowUpdated);
            // 
            // groupControl1
            // 
            this.groupControl1.Controls.Add(this.labelControl4);
            this.groupControl1.Controls.Add(this.labelControl2);
            this.groupControl1.Controls.Add(this.btnMMPrintP);
            this.groupControl1.Controls.Add(this.labelControl1);
            this.groupControl1.Controls.Add(this.btnMMAdd);
            this.groupControl1.Controls.Add(this.labelControl8);
            this.groupControl1.Controls.Add(this.labelControl7);
            this.groupControl1.Controls.Add(this.txtExam);
            this.groupControl1.Controls.Add(this.txtClass);
            this.groupControl1.Controls.Add(this.txtSection);
            this.groupControl1.Controls.Add(this.txtSubject);
            this.groupControl1.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupControl1.Location = new System.Drawing.Point(0, 0);
            this.groupControl1.Name = "groupControl1";
            this.groupControl1.Size = new System.Drawing.Size(977, 70);
            this.groupControl1.TabIndex = 6;
            this.groupControl1.Text = "Add Manage Marks";
            // 
            // labelControl2
            // 
            this.labelControl2.Appearance.ForeColor = System.Drawing.SystemColors.HotTrack;
            this.labelControl2.Appearance.Options.UseForeColor = true;
            this.labelControl2.Location = new System.Drawing.Point(5, 68);
            this.labelControl2.Name = "labelControl2";
            this.labelControl2.Size = new System.Drawing.Size(424, 13);
            this.labelControl2.TabIndex = 63;
            this.labelControl2.Text = "Absent (A)=-1          Unfair Means Conducted (UMC)=-2          Cancellation of E" +
    "xam(CA)=-3";
            this.labelControl2.Visible = false;
            // 
            // btnMMPrintP
            // 
            this.btnMMPrintP.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("btnMMPrintP.ImageOptions.Image")));
            this.btnMMPrintP.Location = new System.Drawing.Point(742, 40);
            this.btnMMPrintP.Name = "btnMMPrintP";
            this.btnMMPrintP.Size = new System.Drawing.Size(108, 23);
            this.btnMMPrintP.TabIndex = 62;
            this.btnMMPrintP.Text = "Print Preview";
            this.btnMMPrintP.Click += new System.EventHandler(this.btnMMPrintP_Click);
            // 
            // labelControl1
            // 
            this.labelControl1.Location = new System.Drawing.Point(317, 23);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(69, 13);
            this.labelControl1.TabIndex = 60;
            this.labelControl1.Text = "Select Section";
            // 
            // btnMMAdd
            // 
            this.btnMMAdd.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("btnMMAdd.ImageOptions.Image")));
            this.btnMMAdd.Location = new System.Drawing.Point(629, 40);
            this.btnMMAdd.Name = "btnMMAdd";
            this.btnMMAdd.Size = new System.Drawing.Size(107, 23);
            this.btnMMAdd.TabIndex = 59;
            this.btnMMAdd.Text = "Manage Marks";
            this.btnMMAdd.Click += new System.EventHandler(this.btnMMAdd_Click);
            // 
            // labelControl8
            // 
            this.labelControl8.Location = new System.Drawing.Point(161, 23);
            this.labelControl8.Name = "labelControl8";
            this.labelControl8.Size = new System.Drawing.Size(58, 13);
            this.labelControl8.TabIndex = 51;
            this.labelControl8.Text = "Select Class";
            // 
            // labelControl7
            // 
            this.labelControl7.Location = new System.Drawing.Point(5, 23);
            this.labelControl7.Name = "labelControl7";
            this.labelControl7.Size = new System.Drawing.Size(59, 13);
            this.labelControl7.TabIndex = 49;
            this.labelControl7.Text = "Select Exam";
            // 
            // txtExam
            // 
            this.txtExam.Location = new System.Drawing.Point(5, 42);
            this.txtExam.Name = "txtExam";
            this.txtExam.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.txtExam.Properties.NullText = "";
            this.txtExam.Properties.PopupSizeable = false;
            this.txtExam.Properties.PopupView = this.searchLookUpEdit1View;
            this.txtExam.Size = new System.Drawing.Size(150, 20);
            this.txtExam.TabIndex = 50;
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
            this.txtClass.Location = new System.Drawing.Point(161, 42);
            this.txtClass.Name = "txtClass";
            this.txtClass.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.txtClass.Properties.DisplayFormat.FormatString = "d";
            this.txtClass.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.txtClass.Properties.EditFormat.FormatString = "d";
            this.txtClass.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.txtClass.Properties.NullText = "";
            this.txtClass.Properties.PopupSizeable = false;
            this.txtClass.Properties.PopupView = this.gridView2;
            this.txtClass.Size = new System.Drawing.Size(150, 20);
            this.txtClass.TabIndex = 52;
            this.txtClass.EditValueChanged += new System.EventHandler(this.txtClass_EditValueChanged);
            // 
            // gridView2
            // 
            this.gridView2.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus;
            this.gridView2.Name = "gridView2";
            this.gridView2.OptionsSelection.EnableAppearanceFocusedCell = false;
            this.gridView2.OptionsView.ShowGroupPanel = false;
            // 
            // labelControl4
            // 
            this.labelControl4.Location = new System.Drawing.Point(471, 23);
            this.labelControl4.Name = "labelControl4";
            this.labelControl4.Size = new System.Drawing.Size(69, 13);
            this.labelControl4.TabIndex = 100;
            this.labelControl4.Text = "Select Subject";
            // 
            // txtSection
            // 
            this.txtSection.Location = new System.Drawing.Point(317, 42);
            this.txtSection.Name = "txtSection";
            this.txtSection.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.txtSection.Properties.DisplayFormat.FormatString = "d";
            this.txtSection.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.txtSection.Properties.EditFormat.FormatString = "d";
            this.txtSection.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.txtSection.Size = new System.Drawing.Size(150, 20);
            this.txtSection.TabIndex = 99;
            this.txtSection.EditValueChanged += new System.EventHandler(this.txtSection_EditValueChanged);
            // 
            // txtSubject
            // 
            this.txtSubject.Location = new System.Drawing.Point(473, 42);
            this.txtSubject.Name = "txtSubject";
            this.txtSubject.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.txtSubject.Properties.DisplayFormat.FormatString = "d";
            this.txtSubject.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.txtSubject.Properties.EditFormat.FormatString = "d";
            this.txtSubject.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.txtSubject.Size = new System.Drawing.Size(150, 20);
            this.txtSubject.TabIndex = 101;
            // 
            // ManageMarksGovt
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.gridManageMarks);
            this.Controls.Add(this.groupControl1);
            this.Name = "ManageMarksGovt";
            this.Size = new System.Drawing.Size(977, 425);
            this.Enter += new System.EventHandler(this.ManageMarks_Enter);
            ((System.ComponentModel.ISupportInitialize)(this.gridManageMarks)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).EndInit();
            this.groupControl1.ResumeLayout(false);
            this.groupControl1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtExam.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.searchLookUpEdit1View)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtClass.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtSection.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtSubject.Properties)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraGrid.GridControl gridManageMarks;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView1;
        private DevExpress.XtraEditors.GroupControl groupControl1;
        private DevExpress.XtraEditors.SimpleButton btnMMAdd;
        private DevExpress.XtraEditors.LabelControl labelControl8;
        private DevExpress.XtraEditors.LabelControl labelControl7;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.XtraEditors.SimpleButton btnMMPrintP;
        private DevExpress.XtraEditors.LabelControl labelControl2;
        private DevExpress.XtraEditors.SearchLookUpEdit txtExam;
        private DevExpress.XtraGrid.Views.Grid.GridView searchLookUpEdit1View;
        private DevExpress.XtraEditors.SearchLookUpEdit txtClass;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView2;
        private DevExpress.XtraEditors.LabelControl labelControl4;
        private DevExpress.XtraEditors.CheckedComboBoxEdit txtSection;
        private DevExpress.XtraEditors.CheckedComboBoxEdit txtSubject;
    }
}
