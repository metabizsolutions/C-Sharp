namespace SchoolManagementSystem
{
    partial class AddStudent
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AddStudent));
            this.txtCardId = new DevExpress.XtraEditors.TextEdit();
            this.gridAddStudents = new DevExpress.XtraGrid.GridControl();
            this.gridView1 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.dxValidationProvider1 = new DevExpress.XtraEditors.DXErrorProvider.DXValidationProvider(this.components);
            this.searchLookUpEdit1 = new DevExpress.XtraEditors.SearchLookUpEdit();
            this.gridView3 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.layoutControlItem9 = new DevExpress.XtraLayout.LayoutControlItem();
            this.ActivityControl = new DevExpress.XtraGrid.GridControl();
            this.ActivitygridView = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.groupControl1 = new DevExpress.XtraEditors.GroupControl();
            this.flowLayoutPanel2 = new System.Windows.Forms.FlowLayoutPanel();
            this.BtnPrint = new DevExpress.XtraEditors.SimpleButton();
            this.btnExport = new DevExpress.XtraEditors.SimpleButton();
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.btnNew = new DevExpress.XtraEditors.SimpleButton();
            this.btnDelete = new DevExpress.XtraEditors.SimpleButton();
            this.btn_std_details = new DevExpress.XtraEditors.SimpleButton();
            this.btnFieldManagement = new DevExpress.XtraEditors.SimpleButton();
            this.btnStudentForm = new DevExpress.XtraEditors.SimpleButton();
            this.Btn_Fee_Performa = new DevExpress.XtraEditors.SimpleButton();
            this.btn_subjectfee = new DevExpress.XtraEditors.SimpleButton();
            ((System.ComponentModel.ISupportInitialize)(this.txtCardId.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridAddStudents)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dxValidationProvider1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.searchLookUpEdit1.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem9)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ActivityControl)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ActivitygridView)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).BeginInit();
            this.groupControl1.SuspendLayout();
            this.flowLayoutPanel2.SuspendLayout();
            this.flowLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // txtCardId
            // 
            this.txtCardId.Location = new System.Drawing.Point(120, 142);
            this.txtCardId.Name = "txtCardId";
            this.txtCardId.Size = new System.Drawing.Size(1127, 20);
            this.txtCardId.TabIndex = 11;
            // 
            // gridAddStudents
            // 
            this.gridAddStudents.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridAddStudents.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F);
            this.gridAddStudents.Location = new System.Drawing.Point(0, 32);
            this.gridAddStudents.MainView = this.gridView1;
            this.gridAddStudents.Name = "gridAddStudents";
            this.gridAddStudents.Size = new System.Drawing.Size(1096, 557);
            this.gridAddStudents.TabIndex = 2;
            this.gridAddStudents.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridView1});
            // 
            // gridView1
            // 
            this.gridView1.GridControl = this.gridAddStudents;
            this.gridView1.Name = "gridView1";
            this.gridView1.OptionsBehavior.Editable = false;
            this.gridView1.OptionsFind.AlwaysVisible = true;
            this.gridView1.OptionsFind.ClearFindOnClose = false;
            this.gridView1.OptionsFind.FindDelay = 100;
            this.gridView1.OptionsView.ColumnAutoWidth = false;
            this.gridView1.DragObjectDrop += new DevExpress.XtraGrid.Views.Base.DragObjectDropEventHandler(this.gridView1_DragObjectDrop);
            this.gridView1.ColumnWidthChanged += new DevExpress.XtraGrid.Views.Base.ColumnEventHandler(this.gridView1_ColumnWidthChanged);
            this.gridView1.CellValueChanged += new DevExpress.XtraGrid.Views.Base.CellValueChangedEventHandler(this.gridView1_CellValueChanged);
            // 
            // searchLookUpEdit1
            // 
            this.searchLookUpEdit1.EditValue = "0";
            this.searchLookUpEdit1.Location = new System.Drawing.Point(51, 309);
            this.searchLookUpEdit1.Name = "searchLookUpEdit1";
            this.searchLookUpEdit1.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.searchLookUpEdit1.Properties.PopupView = this.gridView3;
            this.searchLookUpEdit1.Size = new System.Drawing.Size(1016, 20);
            this.searchLookUpEdit1.TabIndex = 71;
            // 
            // gridView3
            // 
            this.gridView3.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus;
            this.gridView3.Name = "gridView3";
            this.gridView3.OptionsSelection.EnableAppearanceFocusedCell = false;
            this.gridView3.OptionsView.ShowGroupPanel = false;
            // 
            // layoutControlItem9
            // 
            this.layoutControlItem9.Control = this.searchLookUpEdit1;
            this.layoutControlItem9.ControlAlignment = System.Drawing.ContentAlignment.TopCenter;
            this.layoutControlItem9.CustomizationFormText = "Select Student :";
            this.layoutControlItem9.Location = new System.Drawing.Point(0, 297);
            this.layoutControlItem9.Name = "layoutControlItem9";
            this.layoutControlItem9.Size = new System.Drawing.Size(1059, 24);
            this.layoutControlItem9.Text = "School ";
            this.layoutControlItem9.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.AutoSize;
            this.layoutControlItem9.TextLocation = DevExpress.Utils.Locations.Left;
            this.layoutControlItem9.TextSize = new System.Drawing.Size(34, 13);
            this.layoutControlItem9.TextToControlDistance = 5;
            // 
            // ActivityControl
            // 
            this.ActivityControl.Location = new System.Drawing.Point(5, 421);
            this.ActivityControl.MainView = this.ActivitygridView;
            this.ActivityControl.Name = "ActivityControl";
            this.ActivityControl.Size = new System.Drawing.Size(564, 165);
            this.ActivityControl.TabIndex = 12;
            this.ActivityControl.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.ActivitygridView});
            this.ActivityControl.Visible = false;
            // 
            // ActivitygridView
            // 
            this.ActivitygridView.AppearancePrint.EvenRow.Font = new System.Drawing.Font("Times New Roman", 8.25F);
            this.ActivitygridView.AppearancePrint.EvenRow.Options.UseFont = true;
            this.ActivitygridView.AppearancePrint.FilterPanel.Font = new System.Drawing.Font("Times New Roman", 8.25F);
            this.ActivitygridView.AppearancePrint.FilterPanel.Options.UseFont = true;
            this.ActivitygridView.AppearancePrint.FooterPanel.Font = new System.Drawing.Font("Times New Roman", 8.25F);
            this.ActivitygridView.AppearancePrint.FooterPanel.Options.UseFont = true;
            this.ActivitygridView.AppearancePrint.GroupFooter.Font = new System.Drawing.Font("Times New Roman", 8.25F);
            this.ActivitygridView.AppearancePrint.GroupFooter.Options.UseFont = true;
            this.ActivitygridView.AppearancePrint.GroupRow.Font = new System.Drawing.Font("Times New Roman", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ActivitygridView.AppearancePrint.GroupRow.Options.UseFont = true;
            this.ActivitygridView.AppearancePrint.HeaderPanel.Font = new System.Drawing.Font("Times New Roman", 8.25F, System.Drawing.FontStyle.Bold);
            this.ActivitygridView.AppearancePrint.HeaderPanel.Options.UseFont = true;
            this.ActivitygridView.AppearancePrint.Lines.Font = new System.Drawing.Font("Times New Roman", 8.25F);
            this.ActivitygridView.AppearancePrint.Lines.Options.UseFont = true;
            this.ActivitygridView.AppearancePrint.OddRow.Font = new System.Drawing.Font("Times New Roman", 8.25F);
            this.ActivitygridView.AppearancePrint.OddRow.Options.UseFont = true;
            this.ActivitygridView.AppearancePrint.Preview.Font = new System.Drawing.Font("Times New Roman", 8.25F);
            this.ActivitygridView.AppearancePrint.Preview.Options.UseFont = true;
            this.ActivitygridView.AppearancePrint.Row.Font = new System.Drawing.Font("Times New Roman", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ActivitygridView.AppearancePrint.Row.Options.UseFont = true;
            this.ActivitygridView.GridControl = this.ActivityControl;
            this.ActivitygridView.GroupFormat = "[#image]{1} {2}";
            this.ActivitygridView.Name = "ActivitygridView";
            this.ActivitygridView.OptionsBehavior.Editable = false;
            this.ActivitygridView.OptionsBehavior.ReadOnly = true;
            this.ActivitygridView.OptionsFind.AlwaysVisible = true;
            // 
            // groupControl1
            // 
            this.groupControl1.Controls.Add(this.flowLayoutPanel2);
            this.groupControl1.Controls.Add(this.flowLayoutPanel1);
            this.groupControl1.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupControl1.Location = new System.Drawing.Point(0, 0);
            this.groupControl1.Name = "groupControl1";
            this.groupControl1.ShowCaption = false;
            this.groupControl1.Size = new System.Drawing.Size(1096, 32);
            this.groupControl1.TabIndex = 3;
            this.groupControl1.Text = "groupControl1";
            // 
            // flowLayoutPanel2
            // 
            this.flowLayoutPanel2.Controls.Add(this.BtnPrint);
            this.flowLayoutPanel2.Controls.Add(this.btnExport);
            this.flowLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Right;
            this.flowLayoutPanel2.FlowDirection = System.Windows.Forms.FlowDirection.RightToLeft;
            this.flowLayoutPanel2.Location = new System.Drawing.Point(894, 2);
            this.flowLayoutPanel2.Name = "flowLayoutPanel2";
            this.flowLayoutPanel2.Size = new System.Drawing.Size(200, 28);
            this.flowLayoutPanel2.TabIndex = 15;
            // 
            // BtnPrint
            // 
            this.BtnPrint.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.BtnPrint.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("BtnPrint.ImageOptions.Image")));
            this.BtnPrint.Location = new System.Drawing.Point(175, 3);
            this.BtnPrint.Name = "BtnPrint";
            this.BtnPrint.Size = new System.Drawing.Size(22, 22);
            this.BtnPrint.TabIndex = 38;
            this.BtnPrint.ToolTip = "Print Preview";
            this.BtnPrint.Click += new System.EventHandler(this.BtnPrint_Click);
            // 
            // btnExport
            // 
            this.btnExport.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnExport.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("btnExport.ImageOptions.Image")));
            this.btnExport.Location = new System.Drawing.Point(146, 3);
            this.btnExport.Name = "btnExport";
            this.btnExport.Size = new System.Drawing.Size(23, 22);
            this.btnExport.TabIndex = 39;
            this.btnExport.ToolTip = "Export To Excel";
            this.btnExport.Click += new System.EventHandler(this.btnExport_Click);
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.Controls.Add(this.btnNew);
            this.flowLayoutPanel1.Controls.Add(this.btnDelete);
            this.flowLayoutPanel1.Controls.Add(this.btn_std_details);
            this.flowLayoutPanel1.Controls.Add(this.btnFieldManagement);
            this.flowLayoutPanel1.Controls.Add(this.btnStudentForm);
            this.flowLayoutPanel1.Controls.Add(this.Btn_Fee_Performa);
            this.flowLayoutPanel1.Controls.Add(this.btn_subjectfee);
            this.flowLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flowLayoutPanel1.Location = new System.Drawing.Point(2, 2);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(1092, 28);
            this.flowLayoutPanel1.TabIndex = 14;
            // 
            // btnNew
            // 
            this.btnNew.Enabled = false;
            this.btnNew.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("btnNew.ImageOptions.Image")));
            this.btnNew.Location = new System.Drawing.Point(3, 3);
            this.btnNew.Name = "btnNew";
            this.btnNew.Size = new System.Drawing.Size(53, 22);
            this.btnNew.TabIndex = 40;
            this.btnNew.Text = "New";
            this.btnNew.Click += new System.EventHandler(this.btnNew_Click);
            // 
            // btnDelete
            // 
            this.btnDelete.Enabled = false;
            this.btnDelete.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("btnDelete.ImageOptions.Image")));
            this.btnDelete.Location = new System.Drawing.Point(62, 3);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(75, 22);
            this.btnDelete.TabIndex = 37;
            this.btnDelete.Text = "Delete";
            this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
            // 
            // btn_std_details
            // 
            this.btn_std_details.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("btn_std_details.ImageOptions.Image")));
            this.btn_std_details.Location = new System.Drawing.Point(143, 3);
            this.btn_std_details.Name = "btn_std_details";
            this.btn_std_details.Size = new System.Drawing.Size(116, 22);
            this.btn_std_details.TabIndex = 43;
            this.btn_std_details.Text = "Student Details";
            this.btn_std_details.Click += new System.EventHandler(this.btn_std_details_Click);
            // 
            // btnFieldManagement
            // 
            this.btnFieldManagement.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("btnFieldManagement.ImageOptions.Image")));
            this.btnFieldManagement.Location = new System.Drawing.Point(265, 3);
            this.btnFieldManagement.Name = "btnFieldManagement";
            this.btnFieldManagement.Size = new System.Drawing.Size(124, 22);
            this.btnFieldManagement.TabIndex = 41;
            this.btnFieldManagement.Text = "Field Management";
            this.btnFieldManagement.Click += new System.EventHandler(this.btnFieldManagement_Click);
            // 
            // btnStudentForm
            // 
            this.btnStudentForm.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("btnStudentForm.ImageOptions.Image")));
            this.btnStudentForm.Location = new System.Drawing.Point(395, 3);
            this.btnStudentForm.Name = "btnStudentForm";
            this.btnStudentForm.Size = new System.Drawing.Size(98, 22);
            this.btnStudentForm.TabIndex = 42;
            this.btnStudentForm.Text = "Student Form";
            this.btnStudentForm.Click += new System.EventHandler(this.btnStudentForm_Click);
            // 
            // Btn_Fee_Performa
            // 
            this.Btn_Fee_Performa.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("Btn_Fee_Performa.ImageOptions.Image")));
            this.Btn_Fee_Performa.Location = new System.Drawing.Point(499, 3);
            this.Btn_Fee_Performa.Name = "Btn_Fee_Performa";
            this.Btn_Fee_Performa.Size = new System.Drawing.Size(98, 22);
            this.Btn_Fee_Performa.TabIndex = 43;
            this.Btn_Fee_Performa.Text = "Fee Performa";
            this.Btn_Fee_Performa.Click += new System.EventHandler(this.Btn_Fee_Performa_Click);
            // 
            // btn_subjectfee
            // 
            this.btn_subjectfee.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("btn_subjectfee.ImageOptions.Image")));
            this.btn_subjectfee.Location = new System.Drawing.Point(603, 3);
            this.btn_subjectfee.Name = "btn_subjectfee";
            this.btn_subjectfee.Size = new System.Drawing.Size(116, 22);
            this.btn_subjectfee.TabIndex = 43;
            this.btn_subjectfee.Text = "Assigen Subjets";
            this.btn_subjectfee.Click += new System.EventHandler(this.btn_subjectfee_Click);
            // 
            // AddStudent
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.ActivityControl);
            this.Controls.Add(this.gridAddStudents);
            this.Controls.Add(this.groupControl1);
            this.Name = "AddStudent";
            this.Size = new System.Drawing.Size(1096, 589);
            this.Load += new System.EventHandler(this.AddStudent_Load);
            this.Enter += new System.EventHandler(this.AddStudent_Enter);
            ((System.ComponentModel.ISupportInitialize)(this.txtCardId.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridAddStudents)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dxValidationProvider1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.searchLookUpEdit1.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem9)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ActivityControl)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ActivitygridView)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).EndInit();
            this.groupControl1.ResumeLayout(false);
            this.flowLayoutPanel2.ResumeLayout(false);
            this.flowLayoutPanel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private DevExpress.XtraEditors.TextEdit txtCardId;
        private DevExpress.XtraGrid.GridControl gridAddStudents;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView1;
        private DevExpress.XtraEditors.DXErrorProvider.DXValidationProvider dxValidationProvider1;
        private DevExpress.XtraEditors.SearchLookUpEdit searchLookUpEdit1;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView3;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem9;
        private DevExpress.XtraGrid.GridControl ActivityControl;
        private DevExpress.XtraGrid.Views.Grid.GridView ActivitygridView;
        private DevExpress.XtraEditors.GroupControl groupControl1;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel2;
        private DevExpress.XtraEditors.SimpleButton BtnPrint;
        private DevExpress.XtraEditors.SimpleButton btnExport;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        private DevExpress.XtraEditors.SimpleButton btnNew;
        private DevExpress.XtraEditors.SimpleButton btnDelete;
        private DevExpress.XtraEditors.SimpleButton btnFieldManagement;
        private DevExpress.XtraEditors.SimpleButton btnStudentForm;
        private DevExpress.XtraEditors.SimpleButton Btn_Fee_Performa;
        private DevExpress.XtraEditors.SimpleButton btn_subjectfee;
        private DevExpress.XtraEditors.SimpleButton btn_std_details;
    }
}
