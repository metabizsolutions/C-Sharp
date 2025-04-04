namespace SchoolManagementSystem.Exam
{
    partial class ExamGrades
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ExamGrades));
            this.gridExamGrades = new DevExpress.XtraGrid.GridControl();
            this.gridView1 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.groupControl1 = new DevExpress.XtraEditors.GroupControl();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            this.txtComment = new DevExpress.XtraEditors.TextEdit();
            this.BtnEGDelete = new DevExpress.XtraEditors.SimpleButton();
            this.btnEGAdd = new DevExpress.XtraEditors.SimpleButton();
            this.labelControl9 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl8 = new DevExpress.XtraEditors.LabelControl();
            this.txtName = new DevExpress.XtraEditors.TextEdit();
            this.labelControl7 = new DevExpress.XtraEditors.LabelControl();
            this.txtMarkFrom = new DevExpress.XtraEditors.TextEdit();
            this.txtGPoint = new DevExpress.XtraEditors.TextEdit();
            this.txtMarkUpto = new DevExpress.XtraEditors.TextEdit();
            ((System.ComponentModel.ISupportInitialize)(this.gridExamGrades)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).BeginInit();
            this.groupControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtComment.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtName.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtMarkFrom.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtGPoint.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtMarkUpto.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // gridExamGrades
            // 
            this.gridExamGrades.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridExamGrades.Location = new System.Drawing.Point(0, 72);
            this.gridExamGrades.MainView = this.gridView1;
            this.gridExamGrades.Name = "gridExamGrades";
            this.gridExamGrades.Size = new System.Drawing.Size(922, 330);
            this.gridExamGrades.TabIndex = 7;
            this.gridExamGrades.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridView1});
            // 
            // gridView1
            // 
            this.gridView1.GridControl = this.gridExamGrades;
            this.gridView1.Name = "gridView1";
            this.gridView1.OptionsBehavior.Editable = false;
            this.gridView1.OptionsBehavior.EditingMode = DevExpress.XtraGrid.Views.Grid.GridEditingMode.EditFormInplace;
            this.gridView1.OptionsFind.AlwaysVisible = true;
            this.gridView1.RowUpdated += new DevExpress.XtraGrid.Views.Base.RowObjectEventHandler(this.gridView1_RowUpdated);
            // 
            // groupControl1
            // 
            this.groupControl1.Controls.Add(this.labelControl1);
            this.groupControl1.Controls.Add(this.labelControl2);
            this.groupControl1.Controls.Add(this.txtComment);
            this.groupControl1.Controls.Add(this.BtnEGDelete);
            this.groupControl1.Controls.Add(this.btnEGAdd);
            this.groupControl1.Controls.Add(this.labelControl9);
            this.groupControl1.Controls.Add(this.labelControl8);
            this.groupControl1.Controls.Add(this.txtName);
            this.groupControl1.Controls.Add(this.labelControl7);
            this.groupControl1.Controls.Add(this.txtMarkFrom);
            this.groupControl1.Controls.Add(this.txtGPoint);
            this.groupControl1.Controls.Add(this.txtMarkUpto);
            this.groupControl1.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupControl1.Location = new System.Drawing.Point(0, 0);
            this.groupControl1.Name = "groupControl1";
            this.groupControl1.Size = new System.Drawing.Size(922, 72);
            this.groupControl1.TabIndex = 6;
            this.groupControl1.Text = "Add Exam Grades";
            // 
            // labelControl1
            // 
            this.labelControl1.Location = new System.Drawing.Point(629, 23);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(45, 13);
            this.labelControl1.TabIndex = 63;
            this.labelControl1.Text = "Comment";
            // 
            // labelControl2
            // 
            this.labelControl2.Location = new System.Drawing.Point(473, 23);
            this.labelControl2.Name = "labelControl2";
            this.labelControl2.Size = new System.Drawing.Size(63, 13);
            this.labelControl2.TabIndex = 61;
            this.labelControl2.Text = "Percent Upto";
            // 
            // txtComment
            // 
            this.txtComment.Location = new System.Drawing.Point(629, 42);
            this.txtComment.Name = "txtComment";
            this.txtComment.Size = new System.Drawing.Size(150, 20);
            this.txtComment.TabIndex = 64;
            // 
            // BtnEGDelete
            // 
            this.BtnEGDelete.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("BtnEGDelete.ImageOptions.Image")));
            this.BtnEGDelete.Location = new System.Drawing.Point(858, 40);
            this.BtnEGDelete.Name = "BtnEGDelete";
            this.BtnEGDelete.Size = new System.Drawing.Size(67, 23);
            this.BtnEGDelete.TabIndex = 60;
            this.BtnEGDelete.Text = "Delete";
            this.BtnEGDelete.Click += new System.EventHandler(this.BtnEGDelete_Click);
            // 
            // btnEGAdd
            // 
            this.btnEGAdd.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("btnEGAdd.ImageOptions.Image")));
            this.btnEGAdd.Location = new System.Drawing.Point(785, 40);
            this.btnEGAdd.Name = "btnEGAdd";
            this.btnEGAdd.Size = new System.Drawing.Size(67, 23);
            this.btnEGAdd.TabIndex = 59;
            this.btnEGAdd.Text = "Add";
            this.btnEGAdd.Click += new System.EventHandler(this.btnEGAdd_Click);
            // 
            // labelControl9
            // 
            this.labelControl9.Location = new System.Drawing.Point(317, 23);
            this.labelControl9.Name = "labelControl9";
            this.labelControl9.Size = new System.Drawing.Size(64, 13);
            this.labelControl9.TabIndex = 53;
            this.labelControl9.Text = "Percent From";
            // 
            // labelControl8
            // 
            this.labelControl8.Location = new System.Drawing.Point(161, 23);
            this.labelControl8.Name = "labelControl8";
            this.labelControl8.Size = new System.Drawing.Size(56, 13);
            this.labelControl8.TabIndex = 51;
            this.labelControl8.Text = "Grade Point";
            // 
            // txtName
            // 
            this.txtName.Location = new System.Drawing.Point(5, 42);
            this.txtName.Name = "txtName";
            this.txtName.Size = new System.Drawing.Size(150, 20);
            this.txtName.TabIndex = 50;
            // 
            // labelControl7
            // 
            this.labelControl7.Location = new System.Drawing.Point(5, 23);
            this.labelControl7.Name = "labelControl7";
            this.labelControl7.Size = new System.Drawing.Size(27, 13);
            this.labelControl7.TabIndex = 49;
            this.labelControl7.Text = "Name";
            // 
            // txtMarkFrom
            // 
            this.txtMarkFrom.Location = new System.Drawing.Point(317, 42);
            this.txtMarkFrom.Name = "txtMarkFrom";
            this.txtMarkFrom.Size = new System.Drawing.Size(150, 20);
            this.txtMarkFrom.TabIndex = 54;
            // 
            // txtGPoint
            // 
            this.txtGPoint.Location = new System.Drawing.Point(161, 42);
            this.txtGPoint.Name = "txtGPoint";
            this.txtGPoint.Properties.DisplayFormat.FormatString = "d";
            this.txtGPoint.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.txtGPoint.Properties.EditFormat.FormatString = "d";
            this.txtGPoint.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.txtGPoint.Size = new System.Drawing.Size(150, 20);
            this.txtGPoint.TabIndex = 52;
            // 
            // txtMarkUpto
            // 
            this.txtMarkUpto.Location = new System.Drawing.Point(473, 42);
            this.txtMarkUpto.Name = "txtMarkUpto";
            this.txtMarkUpto.Properties.DisplayFormat.FormatString = "d";
            this.txtMarkUpto.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.txtMarkUpto.Properties.EditFormat.FormatString = "d";
            this.txtMarkUpto.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.txtMarkUpto.Size = new System.Drawing.Size(150, 20);
            this.txtMarkUpto.TabIndex = 62;
            // 
            // ExamGrades
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.gridExamGrades);
            this.Controls.Add(this.groupControl1);
            this.Name = "ExamGrades";
            this.Size = new System.Drawing.Size(922, 402);
            ((System.ComponentModel.ISupportInitialize)(this.gridExamGrades)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).EndInit();
            this.groupControl1.ResumeLayout(false);
            this.groupControl1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtComment.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtName.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtMarkFrom.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtGPoint.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtMarkUpto.Properties)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraGrid.GridControl gridExamGrades;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView1;
        private DevExpress.XtraEditors.GroupControl groupControl1;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.XtraEditors.LabelControl labelControl2;
        private DevExpress.XtraEditors.TextEdit txtComment;
        private DevExpress.XtraEditors.SimpleButton BtnEGDelete;
        private DevExpress.XtraEditors.SimpleButton btnEGAdd;
        private DevExpress.XtraEditors.LabelControl labelControl9;
        private DevExpress.XtraEditors.LabelControl labelControl8;
        private DevExpress.XtraEditors.TextEdit txtName;
        private DevExpress.XtraEditors.LabelControl labelControl7;
        private DevExpress.XtraEditors.TextEdit txtMarkFrom;
        private DevExpress.XtraEditors.TextEdit txtGPoint;
        private DevExpress.XtraEditors.TextEdit txtMarkUpto;
    }
}
