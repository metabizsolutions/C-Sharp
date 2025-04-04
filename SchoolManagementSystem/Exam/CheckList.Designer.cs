namespace SchoolManagementSystem.Exam
{
    partial class CheckList
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
            this.gridCheckList = new DevExpress.XtraGrid.GridControl();
            this.gridView1 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.groupControl1 = new DevExpress.XtraEditors.GroupControl();
            this.labelControl5 = new DevExpress.XtraEditors.LabelControl();
            this.txtTeacher = new DevExpress.XtraEditors.CheckedComboBoxEdit();
            this.txtUnChecked = new DevExpress.XtraEditors.CheckEdit();
            this.txtChecked = new DevExpress.XtraEditors.CheckEdit();
            this.labelControl4 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl3 = new DevExpress.XtraEditors.LabelControl();
            this.txtSubject = new DevExpress.XtraEditors.CheckedComboBoxEdit();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            this.btnExportPdf = new DevExpress.XtraEditors.SimpleButton();
            this.btnExportExcel = new DevExpress.XtraEditors.SimpleButton();
            this.btnPrint = new DevExpress.XtraEditors.SimpleButton();
            this.btnCRShow = new DevExpress.XtraEditors.SimpleButton();
            this.txtExam = new DevExpress.XtraEditors.CheckedComboBoxEdit();
            this.txtclass = new DevExpress.XtraEditors.CheckedComboBoxEdit();
            this.txtReport = new DevExpress.XtraEditors.ComboBoxEdit();
            ((System.ComponentModel.ISupportInitialize)(this.gridCheckList)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).BeginInit();
            this.groupControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtTeacher.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtUnChecked.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtChecked.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtSubject.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtExam.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtclass.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtReport.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // gridCheckList
            // 
            this.gridCheckList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridCheckList.Location = new System.Drawing.Point(0, 77);
            this.gridCheckList.MainView = this.gridView1;
            this.gridCheckList.Name = "gridCheckList";
            this.gridCheckList.Size = new System.Drawing.Size(1005, 387);
            this.gridCheckList.TabIndex = 13;
            this.gridCheckList.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridView1});
            // 
            // gridView1
            // 
            this.gridView1.DetailHeight = 239;
            this.gridView1.FixedLineWidth = 1;
            this.gridView1.GridControl = this.gridCheckList;
            this.gridView1.Name = "gridView1";
            this.gridView1.OptionsFind.AlwaysVisible = true;
            this.gridView1.RowUpdated += new DevExpress.XtraGrid.Views.Base.RowObjectEventHandler(this.gridView1_RowUpdated);
            // 
            // groupControl1
            // 
            this.groupControl1.Controls.Add(this.labelControl5);
            this.groupControl1.Controls.Add(this.txtTeacher);
            this.groupControl1.Controls.Add(this.txtUnChecked);
            this.groupControl1.Controls.Add(this.txtChecked);
            this.groupControl1.Controls.Add(this.labelControl4);
            this.groupControl1.Controls.Add(this.labelControl3);
            this.groupControl1.Controls.Add(this.txtSubject);
            this.groupControl1.Controls.Add(this.labelControl1);
            this.groupControl1.Controls.Add(this.labelControl2);
            this.groupControl1.Controls.Add(this.btnExportPdf);
            this.groupControl1.Controls.Add(this.btnExportExcel);
            this.groupControl1.Controls.Add(this.btnPrint);
            this.groupControl1.Controls.Add(this.btnCRShow);
            this.groupControl1.Controls.Add(this.txtExam);
            this.groupControl1.Controls.Add(this.txtclass);
            this.groupControl1.Controls.Add(this.txtReport);
            this.groupControl1.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupControl1.Location = new System.Drawing.Point(0, 0);
            this.groupControl1.Name = "groupControl1";
            this.groupControl1.Size = new System.Drawing.Size(1005, 77);
            this.groupControl1.TabIndex = 12;
            this.groupControl1.Text = "Class Result";
            // 
            // labelControl5
            // 
            this.labelControl5.Location = new System.Drawing.Point(608, 28);
            this.labelControl5.Name = "labelControl5";
            this.labelControl5.Size = new System.Drawing.Size(73, 13);
            this.labelControl5.TabIndex = 77;
            this.labelControl5.Text = "Select Teacher";
            // 
            // txtTeacher
            // 
            this.txtTeacher.Location = new System.Drawing.Point(608, 47);
            this.txtTeacher.Name = "txtTeacher";
            this.txtTeacher.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.txtTeacher.Size = new System.Drawing.Size(129, 20);
            this.txtTeacher.TabIndex = 76;
            // 
            // txtUnChecked
            // 
            this.txtUnChecked.Location = new System.Drawing.Point(837, 22);
            this.txtUnChecked.Name = "txtUnChecked";
            this.txtUnChecked.Properties.Caption = "Un Checked";
            this.txtUnChecked.Size = new System.Drawing.Size(89, 20);
            this.txtUnChecked.TabIndex = 75;
            this.txtUnChecked.CheckedChanged += new System.EventHandler(this.txtUnChecked_CheckedChanged);
            // 
            // txtChecked
            // 
            this.txtChecked.Location = new System.Drawing.Point(743, 22);
            this.txtChecked.Name = "txtChecked";
            this.txtChecked.Properties.Caption = "Checked";
            this.txtChecked.Size = new System.Drawing.Size(89, 20);
            this.txtChecked.TabIndex = 74;
            this.txtChecked.CheckedChanged += new System.EventHandler(this.txtChecked_CheckedChanged);
            // 
            // labelControl4
            // 
            this.labelControl4.Location = new System.Drawing.Point(5, 28);
            this.labelControl4.Name = "labelControl4";
            this.labelControl4.Size = new System.Drawing.Size(65, 13);
            this.labelControl4.TabIndex = 72;
            this.labelControl4.Text = "Select Report";
            // 
            // labelControl3
            // 
            this.labelControl3.Location = new System.Drawing.Point(473, 28);
            this.labelControl3.Name = "labelControl3";
            this.labelControl3.Size = new System.Drawing.Size(69, 13);
            this.labelControl3.TabIndex = 71;
            this.labelControl3.Text = "Select Subject";
            // 
            // txtSubject
            // 
            this.txtSubject.Location = new System.Drawing.Point(473, 47);
            this.txtSubject.Name = "txtSubject";
            this.txtSubject.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.txtSubject.Size = new System.Drawing.Size(129, 20);
            this.txtSubject.TabIndex = 70;
            // 
            // labelControl1
            // 
            this.labelControl1.Location = new System.Drawing.Point(317, 28);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(58, 13);
            this.labelControl1.TabIndex = 67;
            this.labelControl1.Text = "Select Class";
            // 
            // labelControl2
            // 
            this.labelControl2.Location = new System.Drawing.Point(161, 28);
            this.labelControl2.Name = "labelControl2";
            this.labelControl2.Size = new System.Drawing.Size(59, 13);
            this.labelControl2.TabIndex = 65;
            this.labelControl2.Text = "Select Exam";
            // 
            // btnExportPdf
            // 
            this.btnExportPdf.Location = new System.Drawing.Point(917, 28);
            this.btnExportPdf.Name = "btnExportPdf";
            this.btnExportPdf.Size = new System.Drawing.Size(95, 23);
            this.btnExportPdf.TabIndex = 62;
            this.btnExportPdf.Text = "Export Pdf";
            this.btnExportPdf.Visible = false;
            // 
            // btnExportExcel
            // 
            this.btnExportExcel.Location = new System.Drawing.Point(917, 39);
            this.btnExportExcel.Name = "btnExportExcel";
            this.btnExportExcel.Size = new System.Drawing.Size(95, 23);
            this.btnExportExcel.TabIndex = 61;
            this.btnExportExcel.Text = "Export Excel";
            this.btnExportExcel.Visible = false;
            // 
            // btnPrint
            // 
            this.btnPrint.Location = new System.Drawing.Point(837, 44);
            this.btnPrint.Name = "btnPrint";
            this.btnPrint.Size = new System.Drawing.Size(89, 23);
            this.btnPrint.TabIndex = 60;
            this.btnPrint.Text = "Print Preview";
            this.btnPrint.Click += new System.EventHandler(this.btnPrint_Click);
            // 
            // btnCRShow
            // 
            this.btnCRShow.Location = new System.Drawing.Point(743, 44);
            this.btnCRShow.Name = "btnCRShow";
            this.btnCRShow.Size = new System.Drawing.Size(89, 23);
            this.btnCRShow.TabIndex = 59;
            this.btnCRShow.Text = "View Result";
            this.btnCRShow.Click += new System.EventHandler(this.btnCRShow_Click);
            // 
            // txtExam
            // 
            this.txtExam.Location = new System.Drawing.Point(161, 47);
            this.txtExam.Name = "txtExam";
            this.txtExam.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.txtExam.Properties.DisplayFormat.FormatString = "d";
            this.txtExam.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.txtExam.Properties.EditFormat.FormatString = "d";
            this.txtExam.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.txtExam.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.Standard;
            this.txtExam.Size = new System.Drawing.Size(150, 20);
            this.txtExam.TabIndex = 66;
            // 
            // txtclass
            // 
            this.txtclass.Location = new System.Drawing.Point(317, 47);
            this.txtclass.Name = "txtclass";
            this.txtclass.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.txtclass.Properties.DisplayFormat.FormatString = "d";
            this.txtclass.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.txtclass.Properties.EditFormat.FormatString = "d";
            this.txtclass.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.txtclass.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.Standard;
            this.txtclass.Size = new System.Drawing.Size(150, 20);
            this.txtclass.TabIndex = 68;
            this.txtclass.EditValueChanged += new System.EventHandler(this.txtclass_EditValueChanged);
            // 
            // txtReport
            // 
            this.txtReport.Location = new System.Drawing.Point(5, 47);
            this.txtReport.Name = "txtReport";
            this.txtReport.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.txtReport.Properties.DisplayFormat.FormatString = "d";
            this.txtReport.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.txtReport.Properties.EditFormat.FormatString = "d";
            this.txtReport.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.txtReport.Properties.Items.AddRange(new object[] {
            "Subject Wise Report",
            "Section Wise Report",
            "Teacher Wise Report"});
            this.txtReport.Size = new System.Drawing.Size(150, 20);
            this.txtReport.TabIndex = 73;
            // 
            // CheckList
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.gridCheckList);
            this.Controls.Add(this.groupControl1);
            this.Name = "CheckList";
            this.Size = new System.Drawing.Size(1005, 464);
            ((System.ComponentModel.ISupportInitialize)(this.gridCheckList)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).EndInit();
            this.groupControl1.ResumeLayout(false);
            this.groupControl1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtTeacher.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtUnChecked.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtChecked.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtSubject.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtExam.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtclass.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtReport.Properties)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraGrid.GridControl gridCheckList;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView1;
        private DevExpress.XtraEditors.GroupControl groupControl1;
        private DevExpress.XtraEditors.SimpleButton btnExportPdf;
        private DevExpress.XtraEditors.SimpleButton btnExportExcel;
        private DevExpress.XtraEditors.SimpleButton btnPrint;
        private DevExpress.XtraEditors.SimpleButton btnCRShow;
        private DevExpress.XtraEditors.LabelControl labelControl2;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.XtraEditors.LabelControl labelControl3;
        private DevExpress.XtraEditors.CheckedComboBoxEdit txtSubject;
        private DevExpress.XtraEditors.LabelControl labelControl4;
        private DevExpress.XtraEditors.CheckEdit txtUnChecked;
        private DevExpress.XtraEditors.CheckEdit txtChecked;
        private DevExpress.XtraEditors.CheckedComboBoxEdit txtExam;
        private DevExpress.XtraEditors.LabelControl labelControl5;
        private DevExpress.XtraEditors.CheckedComboBoxEdit txtTeacher;
        private DevExpress.XtraEditors.CheckedComboBoxEdit txtclass;
        private DevExpress.XtraEditors.ComboBoxEdit txtReport;
    }
}
