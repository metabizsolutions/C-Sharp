
namespace SchoolManagementSystem.Exam
{
    partial class Multi_ClassResult
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Multi_ClassResult));
            this.gridClassResult = new DevExpress.XtraGrid.GridControl();
            this.gridView1 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.groupControl1 = new DevExpress.XtraEditors.GroupControl();
            this.toggleSwitch1 = new DevExpress.XtraEditors.ToggleSwitch();
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            this.txtOrderby = new DevExpress.XtraEditors.ComboBoxEdit();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.btnExportPdf = new DevExpress.XtraEditors.SimpleButton();
            this.btnExportExcel = new DevExpress.XtraEditors.SimpleButton();
            this.btnPrint = new DevExpress.XtraEditors.SimpleButton();
            this.btn_Banded_result = new DevExpress.XtraEditors.SimpleButton();
            this.btnCRShow = new DevExpress.XtraEditors.SimpleButton();
            this.labelControl8 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl7 = new DevExpress.XtraEditors.LabelControl();
            this.txtSection = new DevExpress.XtraEditors.CheckedComboBoxEdit();
            this.txtExam = new DevExpress.XtraEditors.CheckedComboBoxEdit();
            this.txtClass = new DevExpress.XtraEditors.SearchLookUpEdit();
            this.searchLookUpEdit1View = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.txtIgnoreAbsent = new DevExpress.XtraEditors.CheckEdit();
            ((System.ComponentModel.ISupportInitialize)(this.gridClassResult)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).BeginInit();
            this.groupControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.toggleSwitch1.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtOrderby.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtSection.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtExam.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtClass.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.searchLookUpEdit1View)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtIgnoreAbsent.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // gridClassResult
            // 
            this.gridClassResult.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridClassResult.Location = new System.Drawing.Point(0, 75);
            this.gridClassResult.MainView = this.gridView1;
            this.gridClassResult.Name = "gridClassResult";
            this.gridClassResult.Size = new System.Drawing.Size(1133, 450);
            this.gridClassResult.TabIndex = 11;
            this.gridClassResult.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridView1});
            // 
            // gridView1
            // 
            this.gridView1.GridControl = this.gridClassResult;
            this.gridView1.Name = "gridView1";
            this.gridView1.OptionsBehavior.AllowSortAnimation = DevExpress.Utils.DefaultBoolean.False;
            this.gridView1.OptionsBehavior.Editable = false;
            this.gridView1.OptionsBehavior.ReadOnly = true;
            this.gridView1.OptionsFind.AlwaysVisible = true;
            this.gridView1.OptionsView.BestFitMode = DevExpress.XtraGrid.Views.Grid.GridBestFitMode.Fast;
            this.gridView1.OptionsView.ColumnAutoWidth = false;
            // 
            // groupControl1
            // 
            this.groupControl1.Controls.Add(this.txtIgnoreAbsent);
            this.groupControl1.Controls.Add(this.toggleSwitch1);
            this.groupControl1.Controls.Add(this.labelControl2);
            this.groupControl1.Controls.Add(this.txtOrderby);
            this.groupControl1.Controls.Add(this.labelControl1);
            this.groupControl1.Controls.Add(this.btnExportPdf);
            this.groupControl1.Controls.Add(this.btnExportExcel);
            this.groupControl1.Controls.Add(this.btnPrint);
            this.groupControl1.Controls.Add(this.btn_Banded_result);
            this.groupControl1.Controls.Add(this.btnCRShow);
            this.groupControl1.Controls.Add(this.labelControl8);
            this.groupControl1.Controls.Add(this.labelControl7);
            this.groupControl1.Controls.Add(this.txtSection);
            this.groupControl1.Controls.Add(this.txtExam);
            this.groupControl1.Controls.Add(this.txtClass);
            this.groupControl1.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupControl1.Location = new System.Drawing.Point(0, 0);
            this.groupControl1.Name = "groupControl1";
            this.groupControl1.Size = new System.Drawing.Size(1133, 75);
            this.groupControl1.TabIndex = 10;
            this.groupControl1.Text = "Class Result";
            // 
            // toggleSwitch1
            // 
            this.toggleSwitch1.Location = new System.Drawing.Point(657, 51);
            this.toggleSwitch1.Name = "toggleSwitch1";
            this.toggleSwitch1.Properties.OffText = "Full Details";
            this.toggleSwitch1.Properties.OnText = "Half Details";
            this.toggleSwitch1.Size = new System.Drawing.Size(124, 17);
            this.toggleSwitch1.TabIndex = 67;
            // 
            // labelControl2
            // 
            this.labelControl2.Location = new System.Drawing.Point(473, 31);
            this.labelControl2.Name = "labelControl2";
            this.labelControl2.Size = new System.Drawing.Size(41, 13);
            this.labelControl2.TabIndex = 65;
            this.labelControl2.Text = "Order By";
            // 
            // txtOrderby
            // 
            this.txtOrderby.Location = new System.Drawing.Point(473, 50);
            this.txtOrderby.Name = "txtOrderby";
            this.txtOrderby.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.txtOrderby.Properties.DisplayFormat.FormatString = "d";
            this.txtOrderby.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.txtOrderby.Properties.EditFormat.FormatString = "d";
            this.txtOrderby.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.txtOrderby.Properties.Items.AddRange(new object[] {
            "Roll#",
            "%Avg"});
            this.txtOrderby.Size = new System.Drawing.Size(89, 20);
            this.txtOrderby.TabIndex = 66;
            // 
            // labelControl1
            // 
            this.labelControl1.Location = new System.Drawing.Point(317, 31);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(69, 13);
            this.labelControl1.TabIndex = 63;
            this.labelControl1.Text = "Select Section";
            // 
            // btnExportPdf
            // 
            this.btnExportPdf.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("btnExportPdf.ImageOptions.Image")));
            this.btnExportPdf.Location = new System.Drawing.Point(1051, -1);
            this.btnExportPdf.Name = "btnExportPdf";
            this.btnExportPdf.Size = new System.Drawing.Size(82, 23);
            this.btnExportPdf.TabIndex = 62;
            this.btnExportPdf.Text = "Export Pdf";
            this.btnExportPdf.Visible = false;
            // 
            // btnExportExcel
            // 
            this.btnExportExcel.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("btnExportExcel.ImageOptions.Image")));
            this.btnExportExcel.Location = new System.Drawing.Point(950, -1);
            this.btnExportExcel.Name = "btnExportExcel";
            this.btnExportExcel.Size = new System.Drawing.Size(95, 23);
            this.btnExportExcel.TabIndex = 61;
            this.btnExportExcel.Text = "Export Excel";
            this.btnExportExcel.Click += new System.EventHandler(this.btnExportExcel_Click);
            // 
            // btnPrint
            // 
            this.btnPrint.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("btnPrint.ImageOptions.Image")));
            this.btnPrint.Location = new System.Drawing.Point(855, -1);
            this.btnPrint.Name = "btnPrint";
            this.btnPrint.Size = new System.Drawing.Size(89, 23);
            this.btnPrint.TabIndex = 60;
            this.btnPrint.Text = "Print Preview";
            this.btnPrint.Click += new System.EventHandler(this.btnPrint_Click);
            // 
            // btn_Banded_result
            // 
            this.btn_Banded_result.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("btn_Banded_result.ImageOptions.Image")));
            this.btn_Banded_result.Location = new System.Drawing.Point(787, 46);
            this.btn_Banded_result.Name = "btn_Banded_result";
            this.btn_Banded_result.Size = new System.Drawing.Size(112, 23);
            this.btn_Banded_result.TabIndex = 59;
            this.btn_Banded_result.Text = "Banded Result";
            this.btn_Banded_result.Click += new System.EventHandler(this.btn_Banded_result_Click);
            // 
            // btnCRShow
            // 
            this.btnCRShow.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("btnCRShow.ImageOptions.Image")));
            this.btnCRShow.Location = new System.Drawing.Point(568, 47);
            this.btnCRShow.Name = "btnCRShow";
            this.btnCRShow.Size = new System.Drawing.Size(83, 23);
            this.btnCRShow.TabIndex = 59;
            this.btnCRShow.Text = "View Result";
            this.btnCRShow.Click += new System.EventHandler(this.btnCRShow_Click);
            // 
            // labelControl8
            // 
            this.labelControl8.Location = new System.Drawing.Point(161, 31);
            this.labelControl8.Name = "labelControl8";
            this.labelControl8.Size = new System.Drawing.Size(58, 13);
            this.labelControl8.TabIndex = 51;
            this.labelControl8.Text = "Select Class";
            // 
            // labelControl7
            // 
            this.labelControl7.Location = new System.Drawing.Point(5, 31);
            this.labelControl7.Name = "labelControl7";
            this.labelControl7.Size = new System.Drawing.Size(59, 13);
            this.labelControl7.TabIndex = 49;
            this.labelControl7.Text = "Select Exam";
            // 
            // txtSection
            // 
            this.txtSection.Location = new System.Drawing.Point(317, 50);
            this.txtSection.Name = "txtSection";
            this.txtSection.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.txtSection.Properties.DisplayFormat.FormatString = "d";
            this.txtSection.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.txtSection.Properties.EditFormat.FormatString = "d";
            this.txtSection.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.txtSection.Size = new System.Drawing.Size(150, 20);
            this.txtSection.TabIndex = 64;
            // 
            // txtExam
            // 
            this.txtExam.Location = new System.Drawing.Point(5, 50);
            this.txtExam.Name = "txtExam";
            this.txtExam.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.txtExam.Size = new System.Drawing.Size(150, 20);
            this.txtExam.TabIndex = 50;
            // 
            // txtClass
            // 
            this.txtClass.Location = new System.Drawing.Point(161, 50);
            this.txtClass.Name = "txtClass";
            this.txtClass.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.txtClass.Properties.DisplayFormat.FormatString = "d";
            this.txtClass.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.txtClass.Properties.EditFormat.FormatString = "d";
            this.txtClass.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.txtClass.Properties.NullText = "";
            this.txtClass.Properties.PopupSizeable = false;
            this.txtClass.Properties.PopupView = this.searchLookUpEdit1View;
            this.txtClass.Size = new System.Drawing.Size(150, 20);
            this.txtClass.TabIndex = 52;
            this.txtClass.EditValueChanged += new System.EventHandler(this.txtClass_EditValueChanged);
            // 
            // searchLookUpEdit1View
            // 
            this.searchLookUpEdit1View.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus;
            this.searchLookUpEdit1View.Name = "searchLookUpEdit1View";
            this.searchLookUpEdit1View.OptionsSelection.EnableAppearanceFocusedCell = false;
            this.searchLookUpEdit1View.OptionsView.ShowGroupPanel = false;
            // 
            // txtIgnoreAbsent
            // 
            this.txtIgnoreAbsent.Location = new System.Drawing.Point(568, 24);
            this.txtIgnoreAbsent.Name = "txtIgnoreAbsent";
            this.txtIgnoreAbsent.Properties.Caption = "Ignore Absent";
            this.txtIgnoreAbsent.Size = new System.Drawing.Size(100, 20);
            this.txtIgnoreAbsent.TabIndex = 74;
            // 
            // Multi_ClassResult
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.gridClassResult);
            this.Controls.Add(this.groupControl1);
            this.Name = "Multi_ClassResult";
            this.Size = new System.Drawing.Size(1133, 525);
            ((System.ComponentModel.ISupportInitialize)(this.gridClassResult)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).EndInit();
            this.groupControl1.ResumeLayout(false);
            this.groupControl1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.toggleSwitch1.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtOrderby.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtSection.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtExam.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtClass.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.searchLookUpEdit1View)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtIgnoreAbsent.Properties)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraGrid.GridControl gridClassResult;
        private DevExpress.XtraEditors.GroupControl groupControl1;
        private DevExpress.XtraEditors.LabelControl labelControl2;
        private DevExpress.XtraEditors.ComboBoxEdit txtOrderby;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.XtraEditors.SimpleButton btnExportPdf;
        private DevExpress.XtraEditors.SimpleButton btnExportExcel;
        private DevExpress.XtraEditors.SimpleButton btnPrint;
        private DevExpress.XtraEditors.SimpleButton btnCRShow;
        private DevExpress.XtraEditors.LabelControl labelControl8;
        private DevExpress.XtraEditors.LabelControl labelControl7;
        private DevExpress.XtraEditors.CheckedComboBoxEdit txtSection;
        private DevExpress.XtraEditors.CheckedComboBoxEdit txtExam;
        private DevExpress.XtraEditors.SearchLookUpEdit txtClass;
        private DevExpress.XtraGrid.Views.Grid.GridView searchLookUpEdit1View;
        private DevExpress.XtraEditors.SimpleButton btn_Banded_result;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView1;
        private DevExpress.XtraEditors.ToggleSwitch toggleSwitch1;
        private DevExpress.XtraEditors.CheckEdit txtIgnoreAbsent;
    }
}
