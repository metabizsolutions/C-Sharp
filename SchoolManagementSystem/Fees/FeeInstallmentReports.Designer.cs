namespace SchoolManagementSystem.Exam
{
    partial class FeeInstallmentReports
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FeeInstallmentReports));
            this.gridCheckList = new DevExpress.XtraGrid.GridControl();
            this.gridView1 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.groupControl1 = new DevExpress.XtraEditors.GroupControl();
            this.labelControl3 = new DevExpress.XtraEditors.LabelControl();
            this.txtSection = new DevExpress.XtraEditors.CheckedComboBoxEdit();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.btnPrint = new DevExpress.XtraEditors.SimpleButton();
            this.btnCRShow = new DevExpress.XtraEditors.SimpleButton();
            this.txtclass = new DevExpress.XtraEditors.CheckedComboBoxEdit();
            ((System.ComponentModel.ISupportInitialize)(this.gridCheckList)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).BeginInit();
            this.groupControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtSection.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtclass.Properties)).BeginInit();
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
            this.gridView1.OptionsView.ShowFooter = true;
            this.gridView1.CellValueChanged += new DevExpress.XtraGrid.Views.Base.CellValueChangedEventHandler(this.gridView1_CellValueChanged);
            this.gridView1.RowUpdated += new DevExpress.XtraGrid.Views.Base.RowObjectEventHandler(this.gridView1_RowUpdated);
            // 
            // groupControl1
            // 
            this.groupControl1.Controls.Add(this.labelControl3);
            this.groupControl1.Controls.Add(this.txtSection);
            this.groupControl1.Controls.Add(this.labelControl1);
            this.groupControl1.Controls.Add(this.btnPrint);
            this.groupControl1.Controls.Add(this.btnCRShow);
            this.groupControl1.Controls.Add(this.txtclass);
            this.groupControl1.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupControl1.Location = new System.Drawing.Point(0, 0);
            this.groupControl1.Name = "groupControl1";
            this.groupControl1.Size = new System.Drawing.Size(1005, 77);
            this.groupControl1.TabIndex = 12;
            this.groupControl1.Text = "Class Result";
            // 
            // labelControl3
            // 
            this.labelControl3.Location = new System.Drawing.Point(167, 28);
            this.labelControl3.Name = "labelControl3";
            this.labelControl3.Size = new System.Drawing.Size(67, 13);
            this.labelControl3.TabIndex = 71;
            this.labelControl3.Text = "Select Section";
            // 
            // txtSection
            // 
            this.txtSection.Location = new System.Drawing.Point(167, 47);
            this.txtSection.Name = "txtSection";
            this.txtSection.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.txtSection.Size = new System.Drawing.Size(129, 20);
            this.txtSection.TabIndex = 70;
            // 
            // labelControl1
            // 
            this.labelControl1.Location = new System.Drawing.Point(11, 28);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(57, 13);
            this.labelControl1.TabIndex = 67;
            this.labelControl1.Text = "Select Class";
            // 
            // btnPrint
            // 
            this.btnPrint.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("btnPrint.ImageOptions.Image")));
            this.btnPrint.Location = new System.Drawing.Point(424, 44);
            this.btnPrint.Name = "btnPrint";
            this.btnPrint.Size = new System.Drawing.Size(89, 23);
            this.btnPrint.TabIndex = 60;
            this.btnPrint.Text = "Print Preview";
            this.btnPrint.Click += new System.EventHandler(this.btnPrint_Click);
            // 
            // btnCRShow
            // 
            this.btnCRShow.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("btnCRShow.ImageOptions.Image")));
            this.btnCRShow.Location = new System.Drawing.Point(312, 45);
            this.btnCRShow.Name = "btnCRShow";
            this.btnCRShow.Size = new System.Drawing.Size(106, 23);
            this.btnCRShow.TabIndex = 59;
            this.btnCRShow.Text = "View By Section";
            this.btnCRShow.Click += new System.EventHandler(this.btnCRShow_Click);
            // 
            // txtclass
            // 
            this.txtclass.Location = new System.Drawing.Point(11, 47);
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
            // FeeInstallmentReports
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.gridCheckList);
            this.Controls.Add(this.groupControl1);
            this.Name = "FeeInstallmentReports";
            this.Size = new System.Drawing.Size(1005, 464);
            ((System.ComponentModel.ISupportInitialize)(this.gridCheckList)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).EndInit();
            this.groupControl1.ResumeLayout(false);
            this.groupControl1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtSection.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtclass.Properties)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraGrid.GridControl gridCheckList;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView1;
        private DevExpress.XtraEditors.GroupControl groupControl1;
        private DevExpress.XtraEditors.SimpleButton btnPrint;
        private DevExpress.XtraEditors.SimpleButton btnCRShow;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.XtraEditors.LabelControl labelControl3;
        private DevExpress.XtraEditors.CheckedComboBoxEdit txtSection;
        private DevExpress.XtraEditors.CheckedComboBoxEdit txtclass;
    }
}
