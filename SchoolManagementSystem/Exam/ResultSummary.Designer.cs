namespace SchoolManagementSystem.Exam
{
    partial class ResultSummary
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ResultSummary));
            this.gridClassResult = new DevExpress.XtraGrid.GridControl();
            this.gridView1 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.groupControl1 = new DevExpress.XtraEditors.GroupControl();
            this.simpleButton3 = new DevExpress.XtraEditors.SimpleButton();
            this.simpleButton2 = new DevExpress.XtraEditors.SimpleButton();
            this.label1 = new System.Windows.Forms.Label();
            this.txtNumber = new System.Windows.Forms.TextBox();
            this.btnSectionTopper = new DevExpress.XtraEditors.SimpleButton();
            this.btnGradeComparison = new DevExpress.XtraEditors.SimpleButton();
            this.btnOverallPercent = new DevExpress.XtraEditors.SimpleButton();
            this.btnCampusTopper = new DevExpress.XtraEditors.SimpleButton();
            this.btnClassTopper = new DevExpress.XtraEditors.SimpleButton();
            this.btnStudentwiseFailure = new DevExpress.XtraEditors.SimpleButton();
            this.btnSujbectFailure = new DevExpress.XtraEditors.SimpleButton();
            this.labelControl7 = new DevExpress.XtraEditors.LabelControl();
            this.simpleButton1 = new DevExpress.XtraEditors.SimpleButton();
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl3 = new DevExpress.XtraEditors.LabelControl();
            this.txtTo = new DevExpress.XtraEditors.DateEdit();
            this.txtFrom = new DevExpress.XtraEditors.DateEdit();
            this.btnExportPdf = new DevExpress.XtraEditors.SimpleButton();
            this.btnExportExcel = new DevExpress.XtraEditors.SimpleButton();
            this.btnPrint = new DevExpress.XtraEditors.SimpleButton();
            this.btnTeacherSummary = new DevExpress.XtraEditors.SimpleButton();
            this.DropdownExam = new DevExpress.XtraEditors.CheckedComboBoxEdit();
            ((System.ComponentModel.ISupportInitialize)(this.gridClassResult)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).BeginInit();
            this.groupControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtTo.Properties.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtTo.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtFrom.Properties.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtFrom.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.DropdownExam.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // gridClassResult
            // 
            this.gridClassResult.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridClassResult.Location = new System.Drawing.Point(0, 126);
            this.gridClassResult.MainView = this.gridView1;
            this.gridClassResult.Name = "gridClassResult";
            this.gridClassResult.Size = new System.Drawing.Size(910, 302);
            this.gridClassResult.TabIndex = 9;
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
            this.gridView1.CustomSummaryCalculate += new DevExpress.Data.CustomSummaryEventHandler(this.gridView1_CustomSummaryCalculate);
            // 
            // groupControl1
            // 
            this.groupControl1.Controls.Add(this.simpleButton3);
            this.groupControl1.Controls.Add(this.simpleButton2);
            this.groupControl1.Controls.Add(this.label1);
            this.groupControl1.Controls.Add(this.txtNumber);
            this.groupControl1.Controls.Add(this.btnSectionTopper);
            this.groupControl1.Controls.Add(this.btnGradeComparison);
            this.groupControl1.Controls.Add(this.btnOverallPercent);
            this.groupControl1.Controls.Add(this.btnCampusTopper);
            this.groupControl1.Controls.Add(this.btnClassTopper);
            this.groupControl1.Controls.Add(this.btnStudentwiseFailure);
            this.groupControl1.Controls.Add(this.btnSujbectFailure);
            this.groupControl1.Controls.Add(this.labelControl7);
            this.groupControl1.Controls.Add(this.simpleButton1);
            this.groupControl1.Controls.Add(this.labelControl2);
            this.groupControl1.Controls.Add(this.labelControl3);
            this.groupControl1.Controls.Add(this.txtTo);
            this.groupControl1.Controls.Add(this.txtFrom);
            this.groupControl1.Controls.Add(this.btnExportPdf);
            this.groupControl1.Controls.Add(this.btnExportExcel);
            this.groupControl1.Controls.Add(this.btnPrint);
            this.groupControl1.Controls.Add(this.btnTeacherSummary);
            this.groupControl1.Controls.Add(this.DropdownExam);
            this.groupControl1.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupControl1.Location = new System.Drawing.Point(0, 0);
            this.groupControl1.Name = "groupControl1";
            this.groupControl1.Size = new System.Drawing.Size(910, 126);
            this.groupControl1.TabIndex = 8;
            this.groupControl1.Text = "Class Result";
            // 
            // simpleButton3
            // 
            this.simpleButton3.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("simpleButton3.ImageOptions.Image")));
            this.simpleButton3.Location = new System.Drawing.Point(544, 44);
            this.simpleButton3.Name = "simpleButton3";
            this.simpleButton3.Size = new System.Drawing.Size(113, 20);
            this.simpleButton3.TabIndex = 86;
            this.simpleButton3.Text = "Teacher Subject";
            this.simpleButton3.Click += new System.EventHandler(this.simpleButton3_Click);
            // 
            // simpleButton2
            // 
            this.simpleButton2.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("simpleButton2.ImageOptions.Image")));
            this.simpleButton2.Location = new System.Drawing.Point(425, 44);
            this.simpleButton2.Name = "simpleButton2";
            this.simpleButton2.Size = new System.Drawing.Size(113, 20);
            this.simpleButton2.TabIndex = 85;
            this.simpleButton2.Text = "Teacher Section";
            this.simpleButton2.Click += new System.EventHandler(this.simpleButton2_Click_1);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.label1.Location = new System.Drawing.Point(5, 106);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(75, 13);
            this.label1.TabIndex = 84;
            this.label1.Text = "Top Number";
            // 
            // txtNumber
            // 
            this.txtNumber.Location = new System.Drawing.Point(97, 103);
            this.txtNumber.Name = "txtNumber";
            this.txtNumber.Size = new System.Drawing.Size(65, 20);
            this.txtNumber.TabIndex = 83;
            this.txtNumber.Text = "5";
            // 
            // btnSectionTopper
            // 
            this.btnSectionTopper.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("btnSectionTopper.ImageOptions.Image")));
            this.btnSectionTopper.Location = new System.Drawing.Point(165, 104);
            this.btnSectionTopper.Name = "btnSectionTopper";
            this.btnSectionTopper.Size = new System.Drawing.Size(128, 20);
            this.btnSectionTopper.TabIndex = 82;
            this.btnSectionTopper.Text = "Section Wise Topper";
            this.btnSectionTopper.Click += new System.EventHandler(this.btnSectionTopper_Click);
            // 
            // btnGradeComparison
            // 
            this.btnGradeComparison.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("btnGradeComparison.ImageOptions.Image")));
            this.btnGradeComparison.Location = new System.Drawing.Point(567, 80);
            this.btnGradeComparison.Name = "btnGradeComparison";
            this.btnGradeComparison.Size = new System.Drawing.Size(120, 20);
            this.btnGradeComparison.TabIndex = 81;
            this.btnGradeComparison.Text = "Grade Comparison";
            this.btnGradeComparison.Click += new System.EventHandler(this.btnGradeComparison_Click);
            // 
            // btnOverallPercent
            // 
            this.btnOverallPercent.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("btnOverallPercent.ImageOptions.Image")));
            this.btnOverallPercent.Location = new System.Drawing.Point(728, 44);
            this.btnOverallPercent.Name = "btnOverallPercent";
            this.btnOverallPercent.Size = new System.Drawing.Size(111, 20);
            this.btnOverallPercent.TabIndex = 80;
            this.btnOverallPercent.Text = "Overall Result %";
            this.btnOverallPercent.Click += new System.EventHandler(this.btnOverallPercent_Click);
            // 
            // btnCampusTopper
            // 
            this.btnCampusTopper.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("btnCampusTopper.ImageOptions.Image")));
            this.btnCampusTopper.Location = new System.Drawing.Point(426, 104);
            this.btnCampusTopper.Name = "btnCampusTopper";
            this.btnCampusTopper.Size = new System.Drawing.Size(136, 20);
            this.btnCampusTopper.TabIndex = 79;
            this.btnCampusTopper.Text = "Gender Wise Topper";
            this.btnCampusTopper.Click += new System.EventHandler(this.btnCampusTopper_Click);
            // 
            // btnClassTopper
            // 
            this.btnClassTopper.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("btnClassTopper.ImageOptions.Image")));
            this.btnClassTopper.Location = new System.Drawing.Point(296, 104);
            this.btnClassTopper.Name = "btnClassTopper";
            this.btnClassTopper.Size = new System.Drawing.Size(126, 20);
            this.btnClassTopper.TabIndex = 78;
            this.btnClassTopper.Text = "Class Wise Topper";
            this.btnClassTopper.Click += new System.EventHandler(this.btnClassTopper_Click);
            // 
            // btnStudentwiseFailure
            // 
            this.btnStudentwiseFailure.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("btnStudentwiseFailure.ImageOptions.Image")));
            this.btnStudentwiseFailure.Location = new System.Drawing.Point(425, 80);
            this.btnStudentwiseFailure.Name = "btnStudentwiseFailure";
            this.btnStudentwiseFailure.Size = new System.Drawing.Size(136, 20);
            this.btnStudentwiseFailure.TabIndex = 77;
            this.btnStudentwiseFailure.Text = "Student Wise Failure";
            this.btnStudentwiseFailure.Click += new System.EventHandler(this.btnStudentwiseFailure_Click);
            // 
            // btnSujbectFailure
            // 
            this.btnSujbectFailure.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("btnSujbectFailure.ImageOptions.Image")));
            this.btnSujbectFailure.Location = new System.Drawing.Point(296, 80);
            this.btnSujbectFailure.Name = "btnSujbectFailure";
            this.btnSujbectFailure.Size = new System.Drawing.Size(126, 20);
            this.btnSujbectFailure.TabIndex = 76;
            this.btnSujbectFailure.Text = "Subject Wise Failure";
            this.btnSujbectFailure.Click += new System.EventHandler(this.btnSujbectFailure_Click);
            // 
            // labelControl7
            // 
            this.labelControl7.Location = new System.Drawing.Point(13, 64);
            this.labelControl7.Name = "labelControl7";
            this.labelControl7.Size = new System.Drawing.Size(59, 13);
            this.labelControl7.TabIndex = 74;
            this.labelControl7.Text = "Select Exam";
            // 
            // simpleButton1
            // 
            this.simpleButton1.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("simpleButton1.ImageOptions.Image")));
            this.simpleButton1.Location = new System.Drawing.Point(165, 80);
            this.simpleButton1.Name = "simpleButton1";
            this.simpleButton1.Size = new System.Drawing.Size(128, 20);
            this.simpleButton1.TabIndex = 73;
            this.simpleButton1.Text = "Campus Wise Failure";
            this.simpleButton1.Click += new System.EventHandler(this.simpleButton1_Click_1);
            // 
            // labelControl2
            // 
            this.labelControl2.Appearance.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.labelControl2.Appearance.Options.UseFont = true;
            this.labelControl2.Location = new System.Drawing.Point(167, 24);
            this.labelControl2.Name = "labelControl2";
            this.labelControl2.Size = new System.Drawing.Size(74, 13);
            this.labelControl2.TabIndex = 71;
            this.labelControl2.Text = "To (Optional)";
            // 
            // labelControl3
            // 
            this.labelControl3.Appearance.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.labelControl3.Appearance.Options.UseFont = true;
            this.labelControl3.Location = new System.Drawing.Point(11, 24);
            this.labelControl3.Name = "labelControl3";
            this.labelControl3.Size = new System.Drawing.Size(86, 13);
            this.labelControl3.TabIndex = 69;
            this.labelControl3.Text = "From(Optional)";
            // 
            // txtTo
            // 
            this.txtTo.EditValue = null;
            this.txtTo.Location = new System.Drawing.Point(167, 44);
            this.txtTo.Name = "txtTo";
            this.txtTo.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.txtTo.Properties.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.txtTo.Properties.Mask.EditMask = "";
            this.txtTo.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.None;
            this.txtTo.Size = new System.Drawing.Size(150, 20);
            this.txtTo.TabIndex = 72;
            // 
            // txtFrom
            // 
            this.txtFrom.EditValue = null;
            this.txtFrom.Location = new System.Drawing.Point(12, 44);
            this.txtFrom.Name = "txtFrom";
            this.txtFrom.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.txtFrom.Properties.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.txtFrom.Properties.DisplayFormat.FormatString = "";
            this.txtFrom.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.txtFrom.Properties.EditFormat.FormatString = "";
            this.txtFrom.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.txtFrom.Properties.Mask.EditMask = "";
            this.txtFrom.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.None;
            this.txtFrom.Size = new System.Drawing.Size(150, 20);
            this.txtFrom.TabIndex = 70;
            // 
            // btnExportPdf
            // 
            this.btnExportPdf.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("btnExportPdf.ImageOptions.Image")));
            this.btnExportPdf.Location = new System.Drawing.Point(847, 99);
            this.btnExportPdf.Name = "btnExportPdf";
            this.btnExportPdf.Size = new System.Drawing.Size(60, 20);
            this.btnExportPdf.TabIndex = 62;
            this.btnExportPdf.Text = "PDF";
            this.btnExportPdf.Click += new System.EventHandler(this.btnExportPdf_Click);
            // 
            // btnExportExcel
            // 
            this.btnExportExcel.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("btnExportExcel.ImageOptions.Image")));
            this.btnExportExcel.Location = new System.Drawing.Point(845, 73);
            this.btnExportExcel.Name = "btnExportExcel";
            this.btnExportExcel.Size = new System.Drawing.Size(60, 20);
            this.btnExportExcel.TabIndex = 61;
            this.btnExportExcel.Text = "EXCEL";
            this.btnExportExcel.Click += new System.EventHandler(this.simpleButton2_Click);
            // 
            // btnPrint
            // 
            this.btnPrint.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("btnPrint.ImageOptions.Image")));
            this.btnPrint.Location = new System.Drawing.Point(845, 44);
            this.btnPrint.Name = "btnPrint";
            this.btnPrint.Size = new System.Drawing.Size(60, 20);
            this.btnPrint.TabIndex = 60;
            this.btnPrint.Text = "PRINT";
            this.btnPrint.Click += new System.EventHandler(this.simpleButton1_Click);
            // 
            // btnTeacherSummary
            // 
            this.btnTeacherSummary.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("btnTeacherSummary.ImageOptions.Image")));
            this.btnTeacherSummary.Location = new System.Drawing.Point(323, 44);
            this.btnTeacherSummary.Name = "btnTeacherSummary";
            this.btnTeacherSummary.Size = new System.Drawing.Size(99, 20);
            this.btnTeacherSummary.TabIndex = 59;
            this.btnTeacherSummary.Text = "Teacher Class";
            this.btnTeacherSummary.Click += new System.EventHandler(this.btnCRShow_Click);
            // 
            // DropdownExam
            // 
            this.DropdownExam.EditValue = "";
            this.DropdownExam.Location = new System.Drawing.Point(5, 80);
            this.DropdownExam.Name = "DropdownExam";
            this.DropdownExam.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.DropdownExam.Properties.NullText = "[EditValue is null]";
            this.DropdownExam.Size = new System.Drawing.Size(156, 20);
            this.DropdownExam.TabIndex = 75;
            // 
            // ResultSummary
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.gridClassResult);
            this.Controls.Add(this.groupControl1);
            this.Name = "ResultSummary";
            this.Size = new System.Drawing.Size(910, 428);
            this.Enter += new System.EventHandler(this.ClassResult_Enter);
            ((System.ComponentModel.ISupportInitialize)(this.gridClassResult)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).EndInit();
            this.groupControl1.ResumeLayout(false);
            this.groupControl1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtTo.Properties.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtTo.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtFrom.Properties.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtFrom.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.DropdownExam.Properties)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraGrid.GridControl gridClassResult;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView1;
        private DevExpress.XtraEditors.GroupControl groupControl1;
        private DevExpress.XtraEditors.SimpleButton btnTeacherSummary;
        private DevExpress.XtraEditors.SimpleButton btnPrint;
        private DevExpress.XtraEditors.SimpleButton btnExportExcel;
        private DevExpress.XtraEditors.SimpleButton btnExportPdf;
        private DevExpress.XtraEditors.LabelControl labelControl2;
        private DevExpress.XtraEditors.LabelControl labelControl3;
        private DevExpress.XtraEditors.DateEdit txtTo;
        private DevExpress.XtraEditors.DateEdit txtFrom;
        private DevExpress.XtraEditors.SimpleButton simpleButton1;
        private DevExpress.XtraEditors.LabelControl labelControl7;
        private DevExpress.XtraEditors.SimpleButton btnSujbectFailure;
        private DevExpress.XtraEditors.SimpleButton btnStudentwiseFailure;
        private DevExpress.XtraEditors.SimpleButton btnClassTopper;
        private DevExpress.XtraEditors.SimpleButton btnCampusTopper;
        private DevExpress.XtraEditors.SimpleButton btnOverallPercent;
        private DevExpress.XtraEditors.SimpleButton btnGradeComparison;
        private DevExpress.XtraEditors.SimpleButton btnSectionTopper;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtNumber;
        private DevExpress.XtraEditors.SimpleButton simpleButton2;
        private DevExpress.XtraEditors.SimpleButton simpleButton3;
        private DevExpress.XtraEditors.CheckedComboBoxEdit DropdownExam;
    }
}
