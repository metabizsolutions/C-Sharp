namespace SchoolManagementSystem.Fees
{
    partial class InstallmentPayment
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(InstallmentPayment));
            this.labelControl5 = new DevExpress.XtraEditors.LabelControl();
            this.txtTotal = new DevExpress.XtraEditors.TextEdit();
            this.labelControl3 = new DevExpress.XtraEditors.LabelControl();
            this.txtpaid = new DevExpress.XtraEditors.TextEdit();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            this.btnTPAdd = new DevExpress.XtraEditors.SimpleButton();
            this.labelControl9 = new DevExpress.XtraEditors.LabelControl();
            this.txtdue = new DevExpress.XtraEditors.TextEdit();
            this.txtDate = new DevExpress.XtraEditors.DateEdit();
            this.txtPayment = new DevExpress.XtraEditors.TextEdit();
            this.gridTakePayment = new DevExpress.XtraGrid.GridControl();
            this.gridView1 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.groupControl1 = new DevExpress.XtraEditors.GroupControl();
            this.simpleButton1 = new DevExpress.XtraEditors.SimpleButton();
            ((System.ComponentModel.ISupportInitialize)(this.txtTotal.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtpaid.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtdue.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtDate.Properties.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtDate.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtPayment.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridTakePayment)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).BeginInit();
            this.groupControl1.SuspendLayout();
            this.SuspendLayout();
            // 
            // labelControl5
            // 
            this.labelControl5.Location = new System.Drawing.Point(38, 23);
            this.labelControl5.Name = "labelControl5";
            this.labelControl5.Size = new System.Drawing.Size(64, 13);
            this.labelControl5.TabIndex = 82;
            this.labelControl5.Text = "Total Amount";
            // 
            // txtTotal
            // 
            this.txtTotal.Location = new System.Drawing.Point(38, 42);
            this.txtTotal.Name = "txtTotal";
            this.txtTotal.Properties.ReadOnly = true;
            this.txtTotal.Size = new System.Drawing.Size(200, 20);
            this.txtTotal.TabIndex = 83;
            // 
            // labelControl3
            // 
            this.labelControl3.Location = new System.Drawing.Point(38, 68);
            this.labelControl3.Name = "labelControl3";
            this.labelControl3.Size = new System.Drawing.Size(60, 13);
            this.labelControl3.TabIndex = 80;
            this.labelControl3.Text = "Amount Paid";
            // 
            // txtpaid
            // 
            this.txtpaid.Location = new System.Drawing.Point(38, 87);
            this.txtpaid.Name = "txtpaid";
            this.txtpaid.Properties.ReadOnly = true;
            this.txtpaid.Size = new System.Drawing.Size(200, 20);
            this.txtpaid.TabIndex = 81;
            // 
            // labelControl1
            // 
            this.labelControl1.Location = new System.Drawing.Point(38, 158);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(37, 13);
            this.labelControl1.TabIndex = 76;
            this.labelControl1.Text = "Amount";
            // 
            // labelControl2
            // 
            this.labelControl2.Location = new System.Drawing.Point(38, 203);
            this.labelControl2.Name = "labelControl2";
            this.labelControl2.Size = new System.Drawing.Size(45, 13);
            this.labelControl2.TabIndex = 74;
            this.labelControl2.Text = "Due Date";
            // 
            // btnTPAdd
            // 
            this.btnTPAdd.Image = ((System.Drawing.Image)(resources.GetObject("btnTPAdd.Image")));
            this.btnTPAdd.Location = new System.Drawing.Point(38, 248);
            this.btnTPAdd.Name = "btnTPAdd";
            this.btnTPAdd.Size = new System.Drawing.Size(200, 23);
            this.btnTPAdd.TabIndex = 73;
            this.btnTPAdd.Text = "Add Installment";
            this.btnTPAdd.Click += new System.EventHandler(this.btnTPAdd_Click);
            // 
            // labelControl9
            // 
            this.labelControl9.Location = new System.Drawing.Point(38, 113);
            this.labelControl9.Name = "labelControl9";
            this.labelControl9.Size = new System.Drawing.Size(19, 13);
            this.labelControl9.TabIndex = 71;
            this.labelControl9.Text = "Due";
            // 
            // txtdue
            // 
            this.txtdue.Location = new System.Drawing.Point(38, 132);
            this.txtdue.Name = "txtdue";
            this.txtdue.Properties.ReadOnly = true;
            this.txtdue.Size = new System.Drawing.Size(200, 20);
            this.txtdue.TabIndex = 72;
            // 
            // txtDate
            // 
            this.txtDate.EditValue = null;
            this.txtDate.Location = new System.Drawing.Point(38, 222);
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
            this.txtPayment.Location = new System.Drawing.Point(38, 177);
            this.txtPayment.Name = "txtPayment";
            this.txtPayment.Size = new System.Drawing.Size(200, 20);
            this.txtPayment.TabIndex = 77;
            // 
            // gridTakePayment
            // 
            this.gridTakePayment.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridTakePayment.Location = new System.Drawing.Point(0, 324);
            this.gridTakePayment.MainView = this.gridView1;
            this.gridTakePayment.Name = "gridTakePayment";
            this.gridTakePayment.Size = new System.Drawing.Size(265, 174);
            this.gridTakePayment.TabIndex = 84;
            this.gridTakePayment.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridView1});
            // 
            // gridView1
            // 
            this.gridView1.GridControl = this.gridTakePayment;
            this.gridView1.Name = "gridView1";
            // 
            // groupControl1
            // 
            this.groupControl1.Controls.Add(this.simpleButton1);
            this.groupControl1.Controls.Add(this.labelControl5);
            this.groupControl1.Controls.Add(this.txtPayment);
            this.groupControl1.Controls.Add(this.txtTotal);
            this.groupControl1.Controls.Add(this.txtDate);
            this.groupControl1.Controls.Add(this.labelControl3);
            this.groupControl1.Controls.Add(this.txtdue);
            this.groupControl1.Controls.Add(this.labelControl9);
            this.groupControl1.Controls.Add(this.txtpaid);
            this.groupControl1.Controls.Add(this.btnTPAdd);
            this.groupControl1.Controls.Add(this.labelControl1);
            this.groupControl1.Controls.Add(this.labelControl2);
            this.groupControl1.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupControl1.Location = new System.Drawing.Point(0, 0);
            this.groupControl1.Name = "groupControl1";
            this.groupControl1.Size = new System.Drawing.Size(265, 324);
            this.groupControl1.TabIndex = 85;
            this.groupControl1.Text = "Installment Management";
            // 
            // simpleButton1
            // 
            this.simpleButton1.Image = ((System.Drawing.Image)(resources.GetObject("simpleButton1.Image")));
            this.simpleButton1.Location = new System.Drawing.Point(169, 295);
            this.simpleButton1.Name = "simpleButton1";
            this.simpleButton1.Size = new System.Drawing.Size(91, 23);
            this.simpleButton1.TabIndex = 84;
            this.simpleButton1.Text = "Delete";
            this.simpleButton1.Click += new System.EventHandler(this.simpleButton1_Click);
            // 
            // InstallmentPayment
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(265, 498);
            this.Controls.Add(this.gridTakePayment);
            this.Controls.Add(this.groupControl1);
            this.Name = "InstallmentPayment";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.InstallmentPayment_FormClosing);
            this.Load += new System.EventHandler(this.TakePayment_Load);
            ((System.ComponentModel.ISupportInitialize)(this.txtTotal.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtpaid.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtdue.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtDate.Properties.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtDate.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtPayment.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridTakePayment)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).EndInit();
            this.groupControl1.ResumeLayout(false);
            this.groupControl1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.LabelControl labelControl5;
        private DevExpress.XtraEditors.LabelControl labelControl3;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.XtraEditors.LabelControl labelControl2;
        private DevExpress.XtraEditors.SimpleButton btnTPAdd;
        private DevExpress.XtraEditors.LabelControl labelControl9;
        private DevExpress.XtraGrid.GridControl gridTakePayment;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView1;
        private DevExpress.XtraEditors.GroupControl groupControl1;
        public DevExpress.XtraEditors.TextEdit txtTotal;
        public DevExpress.XtraEditors.TextEdit txtpaid;
        public DevExpress.XtraEditors.TextEdit txtdue;
        public DevExpress.XtraEditors.DateEdit txtDate;
        public DevExpress.XtraEditors.TextEdit txtPayment;
        private DevExpress.XtraEditors.SimpleButton simpleButton1;
    }
}
