namespace SchoolManagementSystem.Fees
{
    partial class InstallmentPaymentBulk
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(InstallmentPaymentBulk));
            this.gridTakePayment = new DevExpress.XtraGrid.GridControl();
            this.gridView1 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.btnTPAdd = new DevExpress.XtraEditors.SimpleButton();
            this.txtDate = new DevExpress.XtraEditors.DateEdit();
            this.txtPayment = new DevExpress.XtraEditors.TextEdit();
            this.simpleButton1 = new DevExpress.XtraEditors.SimpleButton();
            this.groupControl1 = new DevExpress.XtraEditors.GroupControl();
            this.btnCreateBulkInstallment = new DevExpress.XtraEditors.SimpleButton();
            ((System.ComponentModel.ISupportInitialize)(this.gridTakePayment)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtDate.Properties.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtDate.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtPayment.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).BeginInit();
            this.groupControl1.SuspendLayout();
            this.SuspendLayout();
            // 
            // gridTakePayment
            // 
            this.gridTakePayment.Dock = System.Windows.Forms.DockStyle.Top;
            this.gridTakePayment.Location = new System.Drawing.Point(0, 202);
            this.gridTakePayment.MainView = this.gridView1;
            this.gridTakePayment.Name = "gridTakePayment";
            this.gridTakePayment.Size = new System.Drawing.Size(265, 176);
            this.gridTakePayment.TabIndex = 84;
            this.gridTakePayment.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridView1});
            // 
            // gridView1
            // 
            this.gridView1.GridControl = this.gridTakePayment;
            this.gridView1.Name = "gridView1";
            // 
            // labelControl2
            // 
            this.labelControl2.Location = new System.Drawing.Point(35, 69);
            this.labelControl2.Name = "labelControl2";
            this.labelControl2.Size = new System.Drawing.Size(45, 13);
            this.labelControl2.TabIndex = 74;
            this.labelControl2.Text = "Due Date";
            // 
            // labelControl1
            // 
            this.labelControl1.Location = new System.Drawing.Point(35, 24);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(37, 13);
            this.labelControl1.TabIndex = 76;
            this.labelControl1.Text = "Amount";
            // 
            // btnTPAdd
            // 
            this.btnTPAdd.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("btnTPAdd.ImageOptions.Image")));
            this.btnTPAdd.Location = new System.Drawing.Point(35, 114);
            this.btnTPAdd.Name = "btnTPAdd";
            this.btnTPAdd.Size = new System.Drawing.Size(200, 23);
            this.btnTPAdd.TabIndex = 73;
            this.btnTPAdd.Text = "Add Installment";
            this.btnTPAdd.Click += new System.EventHandler(this.btnTPAdd_Click);
            // 
            // txtDate
            // 
            this.txtDate.EditValue = null;
            this.txtDate.Location = new System.Drawing.Point(35, 88);
            this.txtDate.Name = "txtDate";
            this.txtDate.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.txtDate.Properties.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.txtDate.Properties.Mask.EditMask = "";
            this.txtDate.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.None;
            this.txtDate.Size = new System.Drawing.Size(200, 20);
            this.txtDate.TabIndex = 75;
            // 
            // txtPayment
            // 
            this.txtPayment.Location = new System.Drawing.Point(35, 43);
            this.txtPayment.Name = "txtPayment";
            this.txtPayment.Size = new System.Drawing.Size(200, 20);
            this.txtPayment.TabIndex = 77;
            // 
            // simpleButton1
            // 
            this.simpleButton1.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("simpleButton1.ImageOptions.Image")));
            this.simpleButton1.Location = new System.Drawing.Point(169, 173);
            this.simpleButton1.Name = "simpleButton1";
            this.simpleButton1.Size = new System.Drawing.Size(91, 23);
            this.simpleButton1.TabIndex = 84;
            this.simpleButton1.Text = "Delete";
            this.simpleButton1.Click += new System.EventHandler(this.simpleButton1_Click);
            // 
            // groupControl1
            // 
            this.groupControl1.Controls.Add(this.simpleButton1);
            this.groupControl1.Controls.Add(this.txtPayment);
            this.groupControl1.Controls.Add(this.txtDate);
            this.groupControl1.Controls.Add(this.btnTPAdd);
            this.groupControl1.Controls.Add(this.labelControl1);
            this.groupControl1.Controls.Add(this.labelControl2);
            this.groupControl1.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupControl1.Location = new System.Drawing.Point(0, 0);
            this.groupControl1.Name = "groupControl1";
            this.groupControl1.Size = new System.Drawing.Size(265, 202);
            this.groupControl1.TabIndex = 85;
            this.groupControl1.Text = "Installment Management";
            // 
            // btnCreateBulkInstallment
            // 
            this.btnCreateBulkInstallment.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("simpleButton2.ImageOptions.Image")));
            this.btnCreateBulkInstallment.Location = new System.Drawing.Point(35, 384);
            this.btnCreateBulkInstallment.Name = "btnCreateBulkInstallment";
            this.btnCreateBulkInstallment.Size = new System.Drawing.Size(200, 23);
            this.btnCreateBulkInstallment.TabIndex = 86;
            this.btnCreateBulkInstallment.Text = "Confirm Bulk Installment";
            this.btnCreateBulkInstallment.Click += new System.EventHandler(this.btnCreateBulkInstallment_Click);
            // 
            // InstallmentPaymentBulk
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(265, 412);
            this.Controls.Add(this.btnCreateBulkInstallment);
            this.Controls.Add(this.gridTakePayment);
            this.Controls.Add(this.groupControl1);
            this.Name = "InstallmentPaymentBulk";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.InstallmentPayment_FormClosing);
            this.Load += new System.EventHandler(this.TakePayment_Load);
            ((System.ComponentModel.ISupportInitialize)(this.gridTakePayment)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtDate.Properties.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtDate.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtPayment.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).EndInit();
            this.groupControl1.ResumeLayout(false);
            this.groupControl1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private DevExpress.XtraGrid.GridControl gridTakePayment;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView1;
        private DevExpress.XtraEditors.LabelControl labelControl2;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.XtraEditors.SimpleButton btnTPAdd;
        public DevExpress.XtraEditors.DateEdit txtDate;
        public DevExpress.XtraEditors.TextEdit txtPayment;
        private DevExpress.XtraEditors.SimpleButton simpleButton1;
        private DevExpress.XtraEditors.GroupControl groupControl1;
        private DevExpress.XtraEditors.SimpleButton btnCreateBulkInstallment;
    }
}
