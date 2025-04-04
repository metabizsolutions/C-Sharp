namespace SchoolManagementSystem.Transport
{
    partial class Transport_student
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
            this.panel1 = new System.Windows.Forms.Panel();
            this.txtTransport_charges = new System.Windows.Forms.NumericUpDown();
            this.month = new System.Windows.Forms.DateTimePicker();
            this.btn_receipt = new System.Windows.Forms.Button();
            this.btnpaid = new System.Windows.Forms.Button();
            this.btn_create_fee = new System.Windows.Forms.Button();
            this.btnAdd = new System.Windows.Forms.Button();
            this.ddlstudents = new DevExpress.XtraEditors.SearchLookUpEdit();
            this.gridView2 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.label1 = new System.Windows.Forms.Label();
            this.ddlstops = new DevExpress.XtraEditors.SearchLookUpEdit();
            this.gridView3 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.label19 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label20 = new System.Windows.Forms.Label();
            this.gridControl1 = new DevExpress.XtraGrid.GridControl();
            this.gridView1 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.label3 = new System.Windows.Forms.Label();
            this.txtprevious = new System.Windows.Forms.NumericUpDown();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtTransport_charges)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ddlstudents.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ddlstops.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridControl1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtprevious)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.txtprevious);
            this.panel1.Controls.Add(this.txtTransport_charges);
            this.panel1.Controls.Add(this.month);
            this.panel1.Controls.Add(this.btn_receipt);
            this.panel1.Controls.Add(this.btnpaid);
            this.panel1.Controls.Add(this.btn_create_fee);
            this.panel1.Controls.Add(this.btnAdd);
            this.panel1.Controls.Add(this.ddlstudents);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.ddlstops);
            this.panel1.Controls.Add(this.label19);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.label20);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(998, 59);
            this.panel1.TabIndex = 0;
            // 
            // txtTransport_charges
            // 
            this.txtTransport_charges.Location = new System.Drawing.Point(432, 7);
            this.txtTransport_charges.Maximum = new decimal(new int[] {
            100000000,
            0,
            0,
            0});
            this.txtTransport_charges.Name = "txtTransport_charges";
            this.txtTransport_charges.Size = new System.Drawing.Size(65, 20);
            this.txtTransport_charges.TabIndex = 88;
            // 
            // month
            // 
            this.month.CustomFormat = "MMMM/yyyy";
            this.month.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.month.Location = new System.Drawing.Point(708, 8);
            this.month.MaxDate = new System.DateTime(9998, 12, 1, 0, 0, 0, 0);
            this.month.Name = "month";
            this.month.Size = new System.Drawing.Size(126, 20);
            this.month.TabIndex = 87;
            this.month.ValueChanged += new System.EventHandler(this.month_ValueChanged);
            // 
            // btn_receipt
            // 
            this.btn_receipt.Location = new System.Drawing.Point(519, 33);
            this.btn_receipt.Name = "btn_receipt";
            this.btn_receipt.Size = new System.Drawing.Size(54, 23);
            this.btn_receipt.TabIndex = 86;
            this.btn_receipt.Text = "Receipt";
            this.btn_receipt.UseVisualStyleBackColor = true;
            this.btn_receipt.Click += new System.EventHandler(this.btn_receipt_Click);
            // 
            // btnpaid
            // 
            this.btnpaid.Location = new System.Drawing.Point(438, 33);
            this.btnpaid.Name = "btnpaid";
            this.btnpaid.Size = new System.Drawing.Size(59, 23);
            this.btnpaid.TabIndex = 86;
            this.btnpaid.Text = "Fee Pay";
            this.btnpaid.UseVisualStyleBackColor = true;
            this.btnpaid.Click += new System.EventHandler(this.btnpaid_Click);
            // 
            // btn_create_fee
            // 
            this.btn_create_fee.Location = new System.Drawing.Point(840, 6);
            this.btn_create_fee.Name = "btn_create_fee";
            this.btn_create_fee.Size = new System.Drawing.Size(75, 23);
            this.btn_create_fee.TabIndex = 86;
            this.btn_create_fee.Text = "Create Fee";
            this.btn_create_fee.UseVisualStyleBackColor = true;
            this.btn_create_fee.Click += new System.EventHandler(this.btn_create_fee_Click);
            // 
            // btnAdd
            // 
            this.btnAdd.Location = new System.Drawing.Point(603, 5);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(39, 23);
            this.btnAdd.TabIndex = 86;
            this.btnAdd.Text = "Add";
            this.btnAdd.UseVisualStyleBackColor = true;
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // ddlstudents
            // 
            this.ddlstudents.EditValue = "0";
            this.ddlstudents.Location = new System.Drawing.Point(66, 8);
            this.ddlstudents.Name = "ddlstudents";
            this.ddlstudents.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.ddlstudents.Properties.PopupView = this.gridView2;
            this.ddlstudents.Size = new System.Drawing.Size(111, 20);
            this.ddlstudents.TabIndex = 85;
            // 
            // gridView2
            // 
            this.gridView2.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus;
            this.gridView2.Name = "gridView2";
            this.gridView2.OptionsSelection.EnableAppearanceFocusedCell = false;
            this.gridView2.OptionsView.ShowGroupPanel = false;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(16, 10);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(44, 13);
            this.label1.TabIndex = 82;
            this.label1.Text = "Student";
            // 
            // ddlstops
            // 
            this.ddlstops.EditValue = "0";
            this.ddlstops.Location = new System.Drawing.Point(264, 8);
            this.ddlstops.Name = "ddlstops";
            this.ddlstops.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.ddlstops.Properties.PopupView = this.gridView3;
            this.ddlstops.Size = new System.Drawing.Size(111, 20);
            this.ddlstops.TabIndex = 85;
            // 
            // gridView3
            // 
            this.gridView3.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus;
            this.gridView3.Name = "gridView3";
            this.gridView3.OptionsSelection.EnableAppearanceFocusedCell = false;
            this.gridView3.OptionsView.ShowGroupPanel = false;
            // 
            // label19
            // 
            this.label19.AutoSize = true;
            this.label19.Location = new System.Drawing.Point(183, 11);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(75, 13);
            this.label19.TabIndex = 82;
            this.label19.Text = "Transport stop";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(665, 10);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(37, 13);
            this.label2.TabIndex = 83;
            this.label2.Text = "Month";
            // 
            // label20
            // 
            this.label20.AutoSize = true;
            this.label20.Location = new System.Drawing.Point(381, 11);
            this.label20.Name = "label20";
            this.label20.Size = new System.Drawing.Size(45, 13);
            this.label20.TabIndex = 83;
            this.label20.Text = "charges";
            // 
            // gridControl1
            // 
            this.gridControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridControl1.Location = new System.Drawing.Point(0, 59);
            this.gridControl1.MainView = this.gridView1;
            this.gridControl1.Name = "gridControl1";
            this.gridControl1.Size = new System.Drawing.Size(998, 304);
            this.gridControl1.TabIndex = 1;
            this.gridControl1.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridView1});
            // 
            // gridView1
            // 
            this.gridView1.GridControl = this.gridControl1;
            this.gridView1.Name = "gridView1";
            this.gridView1.OptionsView.ShowFooter = true;
            this.gridView1.RowUpdated += new DevExpress.XtraGrid.Views.Base.RowObjectEventHandler(this.gridView1_RowUpdated);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(503, 10);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(23, 13);
            this.label3.TabIndex = 83;
            this.label3.Text = "Pre";
            // 
            // txtprevious
            // 
            this.txtprevious.Location = new System.Drawing.Point(532, 7);
            this.txtprevious.Maximum = new decimal(new int[] {
            100000000,
            0,
            0,
            0});
            this.txtprevious.Name = "txtprevious";
            this.txtprevious.Size = new System.Drawing.Size(65, 20);
            this.txtprevious.TabIndex = 88;
            // 
            // Transport_student
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.gridControl1);
            this.Controls.Add(this.panel1);
            this.Name = "Transport_student";
            this.Size = new System.Drawing.Size(998, 363);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtTransport_charges)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ddlstudents.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ddlstops.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridControl1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtprevious)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private DevExpress.XtraGrid.GridControl gridControl1;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView1;
        private DevExpress.XtraEditors.SearchLookUpEdit ddlstops;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView3;
        private System.Windows.Forms.Label label19;
        private System.Windows.Forms.Label label20;
        private DevExpress.XtraEditors.SearchLookUpEdit ddlstudents;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnAdd;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.DateTimePicker month;
        private System.Windows.Forms.Button btn_create_fee;
        private System.Windows.Forms.Button btnpaid;
        private System.Windows.Forms.NumericUpDown txtTransport_charges;
        private System.Windows.Forms.Button btn_receipt;
        private System.Windows.Forms.NumericUpDown txtprevious;
        private System.Windows.Forms.Label label3;
    }
}
