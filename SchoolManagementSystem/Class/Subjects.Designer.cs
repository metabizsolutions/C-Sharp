namespace SchoolManagementSystem.Class
{
    partial class Subjects
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Subjects));
            this.gridSubject = new DevExpress.XtraGrid.GridControl();
            this.gridView1 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.groupControl1 = new DevExpress.XtraEditors.GroupControl();
            this.txtSection = new DevExpress.XtraEditors.SearchLookUpEdit();
            this.gridView2 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.txtClass = new DevExpress.XtraEditors.SearchLookUpEdit();
            this.gridView3 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.link_add_default_subjects = new System.Windows.Forms.LinkLabel();
            this.simpleButton3 = new DevExpress.XtraEditors.SimpleButton();
            this.simpleButton2 = new DevExpress.XtraEditors.SimpleButton();
            this.simpleButton1 = new DevExpress.XtraEditors.SimpleButton();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl3 = new DevExpress.XtraEditors.LabelControl();
            this.DropdownTeacher = new DevExpress.XtraEditors.SearchLookUpEdit();
            this.gridView29 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.txt_SubjectFee = new DevExpress.XtraEditors.TextEdit();
            this.txtWorkLoad = new DevExpress.XtraEditors.TextEdit();
            this.labelControl4 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            this.BtnSDelete = new DevExpress.XtraEditors.SimpleButton();
            this.btnSAdd = new DevExpress.XtraEditors.SimpleButton();
            this.labelControl11 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl9 = new DevExpress.XtraEditors.LabelControl();
            this.CCB_subjects = new DevExpress.XtraEditors.SearchLookUpEdit();
            this.searchLookUpEdit1View = new DevExpress.XtraGrid.Views.Grid.GridView();
            ((System.ComponentModel.ISupportInitialize)(this.gridSubject)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).BeginInit();
            this.groupControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtSection.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtClass.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.DropdownTeacher.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView29)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txt_SubjectFee.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtWorkLoad.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.CCB_subjects.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.searchLookUpEdit1View)).BeginInit();
            this.SuspendLayout();
            // 
            // gridSubject
            // 
            this.gridSubject.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridSubject.Location = new System.Drawing.Point(0, 80);
            this.gridSubject.MainView = this.gridView1;
            this.gridSubject.Name = "gridSubject";
            this.gridSubject.Size = new System.Drawing.Size(1216, 339);
            this.gridSubject.TabIndex = 5;
            this.gridSubject.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridView1});
            // 
            // gridView1
            // 
            this.gridView1.GridControl = this.gridSubject;
            this.gridView1.Name = "gridView1";
            this.gridView1.OptionsBehavior.Editable = false;
            this.gridView1.OptionsFind.AlwaysVisible = true;
            this.gridView1.RowUpdated += new DevExpress.XtraGrid.Views.Base.RowObjectEventHandler(this.gridView1_RowUpdated);
            // 
            // groupControl1
            // 
            this.groupControl1.Controls.Add(this.txtSection);
            this.groupControl1.Controls.Add(this.txtClass);
            this.groupControl1.Controls.Add(this.link_add_default_subjects);
            this.groupControl1.Controls.Add(this.simpleButton3);
            this.groupControl1.Controls.Add(this.simpleButton2);
            this.groupControl1.Controls.Add(this.simpleButton1);
            this.groupControl1.Controls.Add(this.labelControl1);
            this.groupControl1.Controls.Add(this.labelControl3);
            this.groupControl1.Controls.Add(this.DropdownTeacher);
            this.groupControl1.Controls.Add(this.txt_SubjectFee);
            this.groupControl1.Controls.Add(this.txtWorkLoad);
            this.groupControl1.Controls.Add(this.labelControl4);
            this.groupControl1.Controls.Add(this.labelControl2);
            this.groupControl1.Controls.Add(this.BtnSDelete);
            this.groupControl1.Controls.Add(this.btnSAdd);
            this.groupControl1.Controls.Add(this.labelControl11);
            this.groupControl1.Controls.Add(this.labelControl9);
            this.groupControl1.Controls.Add(this.CCB_subjects);
            this.groupControl1.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupControl1.Location = new System.Drawing.Point(0, 0);
            this.groupControl1.Name = "groupControl1";
            this.groupControl1.Size = new System.Drawing.Size(1216, 80);
            this.groupControl1.TabIndex = 4;
            this.groupControl1.Text = "Add Subjects";
            // 
            // txtSection
            // 
            this.txtSection.EditValue = "";
            this.txtSection.Location = new System.Drawing.Point(312, 49);
            this.txtSection.Name = "txtSection";
            this.txtSection.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.txtSection.Properties.NullText = "";
            this.txtSection.Properties.PopupView = this.gridView2;
            this.txtSection.Size = new System.Drawing.Size(150, 20);
            this.txtSection.TabIndex = 86;
            // 
            // gridView2
            // 
            this.gridView2.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus;
            this.gridView2.Name = "gridView2";
            this.gridView2.OptionsSelection.EnableAppearanceFocusedCell = false;
            this.gridView2.OptionsView.ShowGroupPanel = false;
            // 
            // txtClass
            // 
            this.txtClass.EditValue = "";
            this.txtClass.Location = new System.Drawing.Point(159, 49);
            this.txtClass.Name = "txtClass";
            this.txtClass.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.txtClass.Properties.NullText = "";
            this.txtClass.Properties.PopupView = this.gridView3;
            this.txtClass.Size = new System.Drawing.Size(147, 20);
            this.txtClass.TabIndex = 85;
            this.txtClass.EditValueChanged += new System.EventHandler(this.txtClass_EditValueChanged);
            // 
            // gridView3
            // 
            this.gridView3.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus;
            this.gridView3.Name = "gridView3";
            this.gridView3.OptionsSelection.EnableAppearanceFocusedCell = false;
            this.gridView3.OptionsView.ShowGroupPanel = false;
            // 
            // link_add_default_subjects
            // 
            this.link_add_default_subjects.AutoSize = true;
            this.link_add_default_subjects.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.link_add_default_subjects.Location = new System.Drawing.Point(58, 28);
            this.link_add_default_subjects.Name = "link_add_default_subjects";
            this.link_add_default_subjects.Size = new System.Drawing.Size(33, 14);
            this.link_add_default_subjects.TabIndex = 84;
            this.link_add_default_subjects.TabStop = true;
            this.link_add_default_subjects.Text = "new";
            this.link_add_default_subjects.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.link_add_default_subjects_LinkClicked);
            // 
            // simpleButton3
            // 
            this.simpleButton3.Location = new System.Drawing.Point(1083, 3);
            this.simpleButton3.Name = "simpleButton3";
            this.simpleButton3.Size = new System.Drawing.Size(128, 23);
            this.simpleButton3.TabIndex = 83;
            this.simpleButton3.Text = "upload to new table";
            this.simpleButton3.Click += new System.EventHandler(this.simpleButton3_Click);
            // 
            // simpleButton2
            // 
            this.simpleButton2.Location = new System.Drawing.Point(1105, 53);
            this.simpleButton2.Name = "simpleButton2";
            this.simpleButton2.Size = new System.Drawing.Size(106, 23);
            this.simpleButton2.TabIndex = 82;
            this.simpleButton2.Text = "Update Names";
            this.simpleButton2.Click += new System.EventHandler(this.simpleButton2_Click);
            // 
            // simpleButton1
            // 
            this.simpleButton1.Location = new System.Drawing.Point(920, 53);
            this.simpleButton1.Name = "simpleButton1";
            this.simpleButton1.Size = new System.Drawing.Size(179, 23);
            this.simpleButton1.TabIndex = 81;
            this.simpleButton1.Text = "Load subjects from old table";
            this.simpleButton1.Click += new System.EventHandler(this.simpleButton1_Click);
            // 
            // labelControl1
            // 
            this.labelControl1.Location = new System.Drawing.Point(5, 30);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(36, 13);
            this.labelControl1.TabIndex = 79;
            this.labelControl1.Text = "Subject";
            // 
            // labelControl3
            // 
            this.labelControl3.Location = new System.Drawing.Point(468, 30);
            this.labelControl3.Name = "labelControl3";
            this.labelControl3.Size = new System.Drawing.Size(40, 13);
            this.labelControl3.TabIndex = 77;
            this.labelControl3.Text = "Teacher";
            // 
            // DropdownTeacher
            // 
            this.DropdownTeacher.Location = new System.Drawing.Point(468, 49);
            this.DropdownTeacher.Name = "DropdownTeacher";
            this.DropdownTeacher.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.DropdownTeacher.Properties.PopupView = this.gridView29;
            this.DropdownTeacher.Size = new System.Drawing.Size(147, 20);
            this.DropdownTeacher.TabIndex = 76;
            // 
            // gridView29
            // 
            this.gridView29.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus;
            this.gridView29.Name = "gridView29";
            this.gridView29.OptionsSelection.EnableAppearanceFocusedCell = false;
            this.gridView29.OptionsView.ShowGroupPanel = false;
            // 
            // txt_SubjectFee
            // 
            this.txt_SubjectFee.Location = new System.Drawing.Point(687, 50);
            this.txt_SubjectFee.Name = "txt_SubjectFee";
            this.txt_SubjectFee.Properties.Mask.EditMask = "n";
            this.txt_SubjectFee.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric;
            this.txt_SubjectFee.Size = new System.Drawing.Size(65, 20);
            this.txt_SubjectFee.TabIndex = 64;
            // 
            // txtWorkLoad
            // 
            this.txtWorkLoad.Location = new System.Drawing.Point(621, 50);
            this.txtWorkLoad.Name = "txtWorkLoad";
            this.txtWorkLoad.Size = new System.Drawing.Size(58, 20);
            this.txtWorkLoad.TabIndex = 64;
            // 
            // labelControl4
            // 
            this.labelControl4.Location = new System.Drawing.Point(687, 35);
            this.labelControl4.Name = "labelControl4";
            this.labelControl4.Size = new System.Drawing.Size(57, 13);
            this.labelControl4.TabIndex = 63;
            this.labelControl4.Text = "Subject Fee";
            // 
            // labelControl2
            // 
            this.labelControl2.Location = new System.Drawing.Point(621, 35);
            this.labelControl2.Name = "labelControl2";
            this.labelControl2.Size = new System.Drawing.Size(53, 13);
            this.labelControl2.TabIndex = 63;
            this.labelControl2.Text = "Work Load";
            // 
            // BtnSDelete
            // 
            this.BtnSDelete.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("BtnSDelete.ImageOptions.Image")));
            this.BtnSDelete.Location = new System.Drawing.Point(831, 48);
            this.BtnSDelete.Name = "BtnSDelete";
            this.BtnSDelete.Size = new System.Drawing.Size(67, 21);
            this.BtnSDelete.TabIndex = 60;
            this.BtnSDelete.Text = "Delete";
            this.BtnSDelete.Click += new System.EventHandler(this.BtnSDelete_Click);
            // 
            // btnSAdd
            // 
            this.btnSAdd.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("btnSAdd.ImageOptions.Image")));
            this.btnSAdd.Location = new System.Drawing.Point(758, 48);
            this.btnSAdd.Name = "btnSAdd";
            this.btnSAdd.Size = new System.Drawing.Size(67, 21);
            this.btnSAdd.TabIndex = 59;
            this.btnSAdd.Text = "Add";
            this.btnSAdd.Click += new System.EventHandler(this.btnSAdd_Click);
            // 
            // labelControl11
            // 
            this.labelControl11.Location = new System.Drawing.Point(312, 30);
            this.labelControl11.Name = "labelControl11";
            this.labelControl11.Size = new System.Drawing.Size(36, 13);
            this.labelControl11.TabIndex = 57;
            this.labelControl11.Text = "Section";
            // 
            // labelControl9
            // 
            this.labelControl9.Location = new System.Drawing.Point(159, 30);
            this.labelControl9.Name = "labelControl9";
            this.labelControl9.Size = new System.Drawing.Size(25, 13);
            this.labelControl9.TabIndex = 53;
            this.labelControl9.Text = "Class";
            // 
            // CCB_subjects
            // 
            this.CCB_subjects.EditValue = "";
            this.CCB_subjects.Location = new System.Drawing.Point(5, 49);
            this.CCB_subjects.Name = "CCB_subjects";
            this.CCB_subjects.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.CCB_subjects.Properties.NullText = "";
            this.CCB_subjects.Properties.PopupView = this.searchLookUpEdit1View;
            this.CCB_subjects.Size = new System.Drawing.Size(150, 20);
            this.CCB_subjects.TabIndex = 80;
            // 
            // searchLookUpEdit1View
            // 
            this.searchLookUpEdit1View.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus;
            this.searchLookUpEdit1View.Name = "searchLookUpEdit1View";
            this.searchLookUpEdit1View.OptionsSelection.EnableAppearanceFocusedCell = false;
            this.searchLookUpEdit1View.OptionsView.ShowGroupPanel = false;
            // 
            // Subjects
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.gridSubject);
            this.Controls.Add(this.groupControl1);
            this.Name = "Subjects";
            this.Size = new System.Drawing.Size(1216, 419);
            this.Enter += new System.EventHandler(this.Subjects_Enter);
            ((System.ComponentModel.ISupportInitialize)(this.gridSubject)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).EndInit();
            this.groupControl1.ResumeLayout(false);
            this.groupControl1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtSection.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtClass.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.DropdownTeacher.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView29)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txt_SubjectFee.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtWorkLoad.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.CCB_subjects.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.searchLookUpEdit1View)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraGrid.GridControl gridSubject;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView1;
        private DevExpress.XtraEditors.GroupControl groupControl1;
        private DevExpress.XtraEditors.SimpleButton BtnSDelete;
        private DevExpress.XtraEditors.SimpleButton btnSAdd;
        private DevExpress.XtraEditors.LabelControl labelControl11;
        private DevExpress.XtraEditors.LabelControl labelControl9;
        private DevExpress.XtraEditors.TextEdit txtWorkLoad;
        private DevExpress.XtraEditors.LabelControl labelControl2;
        private DevExpress.XtraEditors.LabelControl labelControl3;
        private DevExpress.XtraEditors.SearchLookUpEdit DropdownTeacher;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView29;
        private DevExpress.XtraEditors.TextEdit txt_SubjectFee;
        private DevExpress.XtraEditors.LabelControl labelControl4;

        private DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.XtraEditors.SearchLookUpEdit CCB_subjects;
        private DevExpress.XtraGrid.Views.Grid.GridView searchLookUpEdit1View;
        private DevExpress.XtraEditors.SimpleButton simpleButton3;
        private DevExpress.XtraEditors.SimpleButton simpleButton2;
        private DevExpress.XtraEditors.SimpleButton simpleButton1;
        private System.Windows.Forms.LinkLabel link_add_default_subjects;
        private DevExpress.XtraEditors.SearchLookUpEdit txtSection;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView2;
        private DevExpress.XtraEditors.SearchLookUpEdit txtClass;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView3;
    }
}
