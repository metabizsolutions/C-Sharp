namespace SchoolManagementSystem.Fees
{
    partial class ExpenseManagement
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ExpenseManagement));
            this.gridInvoiceExpanse = new DevExpress.XtraGrid.GridControl();
            this.gridView1 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.groupControl1 = new DevExpress.XtraEditors.GroupControl();
            this.SpinnerCashAsset = new DevExpress.XtraEditors.SearchLookUpEdit();
            this.gridView2 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.btnAddHeads = new DevExpress.XtraEditors.SimpleButton();
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            this.txtDue = new DevExpress.XtraEditors.TextEdit();
            this.labelControl5 = new DevExpress.XtraEditors.LabelControl();
            this.txtTotal = new DevExpress.XtraEditors.TextEdit();
            this.labelControl3 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl4 = new DevExpress.XtraEditors.LabelControl();
            this.txtPayment = new DevExpress.XtraEditors.TextEdit();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.BtnEMDelete = new DevExpress.XtraEditors.SimpleButton();
            this.btnEMAdd = new DevExpress.XtraEditors.SimpleButton();
            this.labelControl9 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl8 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl7 = new DevExpress.XtraEditors.LabelControl();
            this.txtDes = new DevExpress.XtraEditors.TextEdit();
            this.txtTitle = new DevExpress.XtraEditors.TextEdit();
            this.txtECategory = new DevExpress.XtraEditors.ComboBoxEdit();
            this.txtDate = new DevExpress.XtraEditors.DateEdit();
            ((System.ComponentModel.ISupportInitialize)(this.gridInvoiceExpanse)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).BeginInit();
            this.groupControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.SpinnerCashAsset.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtDue.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtTotal.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtPayment.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtDes.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtTitle.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtECategory.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtDate.Properties.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtDate.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // gridInvoiceExpanse
            // 
            this.gridInvoiceExpanse.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridInvoiceExpanse.Location = new System.Drawing.Point(0, 115);
            this.gridInvoiceExpanse.MainView = this.gridView1;
            this.gridInvoiceExpanse.Name = "gridInvoiceExpanse";
            this.gridInvoiceExpanse.Size = new System.Drawing.Size(1009, 343);
            this.gridInvoiceExpanse.TabIndex = 11;
            this.gridInvoiceExpanse.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridView1});
            // 
            // gridView1
            // 
            this.gridView1.GridControl = this.gridInvoiceExpanse;
            this.gridView1.Name = "gridView1";
            this.gridView1.OptionsBehavior.Editable = false;
            this.gridView1.OptionsBehavior.EditingMode = DevExpress.XtraGrid.Views.Grid.GridEditingMode.Inplace;
            this.gridView1.OptionsFind.AlwaysVisible = true;
            this.gridView1.RowUpdated += new DevExpress.XtraGrid.Views.Base.RowObjectEventHandler(this.gridView1_RowUpdated);
            // 
            // groupControl1
            // 
            this.groupControl1.Controls.Add(this.SpinnerCashAsset);
            this.groupControl1.Controls.Add(this.btnAddHeads);
            this.groupControl1.Controls.Add(this.labelControl2);
            this.groupControl1.Controls.Add(this.txtDue);
            this.groupControl1.Controls.Add(this.labelControl5);
            this.groupControl1.Controls.Add(this.txtTotal);
            this.groupControl1.Controls.Add(this.labelControl3);
            this.groupControl1.Controls.Add(this.labelControl4);
            this.groupControl1.Controls.Add(this.txtPayment);
            this.groupControl1.Controls.Add(this.labelControl1);
            this.groupControl1.Controls.Add(this.BtnEMDelete);
            this.groupControl1.Controls.Add(this.btnEMAdd);
            this.groupControl1.Controls.Add(this.labelControl9);
            this.groupControl1.Controls.Add(this.labelControl8);
            this.groupControl1.Controls.Add(this.labelControl7);
            this.groupControl1.Controls.Add(this.txtDes);
            this.groupControl1.Controls.Add(this.txtTitle);
            this.groupControl1.Controls.Add(this.txtECategory);
            this.groupControl1.Controls.Add(this.txtDate);
            this.groupControl1.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupControl1.Location = new System.Drawing.Point(0, 0);
            this.groupControl1.Name = "groupControl1";
            this.groupControl1.Size = new System.Drawing.Size(1009, 115);
            this.groupControl1.TabIndex = 10;
            this.groupControl1.Text = "Add Expense";
            // 
            // SpinnerCashAsset
            // 
            this.SpinnerCashAsset.Location = new System.Drawing.Point(473, 41);
            this.SpinnerCashAsset.Name = "SpinnerCashAsset";
            this.SpinnerCashAsset.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.SpinnerCashAsset.Properties.PopupView = this.gridView2;
            this.SpinnerCashAsset.Size = new System.Drawing.Size(150, 20);
            this.SpinnerCashAsset.TabIndex = 89;
            // 
            // gridView2
            // 
            this.gridView2.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus;
            this.gridView2.Name = "gridView2";
            this.gridView2.OptionsSelection.EnableAppearanceFocusedCell = false;
            this.gridView2.OptionsView.ShowGroupPanel = false;
            // 
            // btnAddHeads
            // 
            this.btnAddHeads.Location = new System.Drawing.Point(136, 42);
            this.btnAddHeads.Name = "btnAddHeads";
            this.btnAddHeads.Size = new System.Drawing.Size(19, 20);
            this.btnAddHeads.TabIndex = 73;
            this.btnAddHeads.Text = "+";
            this.btnAddHeads.ToolTipTitle = "Add Account Heads";
            this.btnAddHeads.Click += new System.EventHandler(this.btnAddHeads_Click);
            // 
            // labelControl2
            // 
            this.labelControl2.Location = new System.Drawing.Point(317, 68);
            this.labelControl2.Name = "labelControl2";
            this.labelControl2.Size = new System.Drawing.Size(24, 13);
            this.labelControl2.TabIndex = 71;
            this.labelControl2.Text = "Dues";
            // 
            // txtDue
            // 
            this.txtDue.Location = new System.Drawing.Point(317, 87);
            this.txtDue.Name = "txtDue";
            this.txtDue.Properties.ReadOnly = true;
            this.txtDue.Size = new System.Drawing.Size(150, 20);
            this.txtDue.TabIndex = 72;
            // 
            // labelControl5
            // 
            this.labelControl5.Location = new System.Drawing.Point(161, 21);
            this.labelControl5.Name = "labelControl5";
            this.labelControl5.Size = new System.Drawing.Size(24, 13);
            this.labelControl5.TabIndex = 69;
            this.labelControl5.Text = "Total";
            // 
            // txtTotal
            // 
            this.txtTotal.Location = new System.Drawing.Point(161, 40);
            this.txtTotal.Name = "txtTotal";
            this.txtTotal.Size = new System.Drawing.Size(150, 20);
            this.txtTotal.TabIndex = 70;
            this.txtTotal.EditValueChanged += new System.EventHandler(this.txtTotal_EditValueChanged);
            // 
            // labelControl3
            // 
            this.labelControl3.Location = new System.Drawing.Point(317, 23);
            this.labelControl3.Name = "labelControl3";
            this.labelControl3.Size = new System.Drawing.Size(42, 13);
            this.labelControl3.TabIndex = 67;
            this.labelControl3.Text = "Payment";
            // 
            // labelControl4
            // 
            this.labelControl4.Location = new System.Drawing.Point(473, 23);
            this.labelControl4.Name = "labelControl4";
            this.labelControl4.Size = new System.Drawing.Size(59, 13);
            this.labelControl4.TabIndex = 65;
            this.labelControl4.Text = "Cash Assets";
            // 
            // txtPayment
            // 
            this.txtPayment.Location = new System.Drawing.Point(317, 42);
            this.txtPayment.Name = "txtPayment";
            this.txtPayment.Size = new System.Drawing.Size(150, 20);
            this.txtPayment.TabIndex = 68;
            this.txtPayment.TextChanged += new System.EventHandler(this.txtPayment_TextChanged);
            // 
            // labelControl1
            // 
            this.labelControl1.Location = new System.Drawing.Point(5, 68);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(23, 13);
            this.labelControl1.TabIndex = 63;
            this.labelControl1.Text = "Date";
            // 
            // BtnEMDelete
            // 
            this.BtnEMDelete.Enabled = false;
            this.BtnEMDelete.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("BtnEMDelete.ImageOptions.Image")));
            this.BtnEMDelete.Location = new System.Drawing.Point(629, 75);
            this.BtnEMDelete.Name = "BtnEMDelete";
            this.BtnEMDelete.Size = new System.Drawing.Size(67, 23);
            this.BtnEMDelete.TabIndex = 60;
            this.BtnEMDelete.Text = "Delete";
            this.BtnEMDelete.Click += new System.EventHandler(this.BtnEMDelete_Click);
            // 
            // btnEMAdd
            // 
            this.btnEMAdd.Enabled = false;
            this.btnEMAdd.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("btnEMAdd.ImageOptions.Image")));
            this.btnEMAdd.Location = new System.Drawing.Point(629, 46);
            this.btnEMAdd.Name = "btnEMAdd";
            this.btnEMAdd.Size = new System.Drawing.Size(67, 23);
            this.btnEMAdd.TabIndex = 59;
            this.btnEMAdd.Text = "Add";
            this.btnEMAdd.Click += new System.EventHandler(this.btnEMAdd_Click);
            // 
            // labelControl9
            // 
            this.labelControl9.Location = new System.Drawing.Point(473, 68);
            this.labelControl9.Name = "labelControl9";
            this.labelControl9.Size = new System.Drawing.Size(53, 13);
            this.labelControl9.TabIndex = 53;
            this.labelControl9.Text = "Description";
            // 
            // labelControl8
            // 
            this.labelControl8.Location = new System.Drawing.Point(161, 68);
            this.labelControl8.Name = "labelControl8";
            this.labelControl8.Size = new System.Drawing.Size(20, 13);
            this.labelControl8.TabIndex = 51;
            this.labelControl8.Text = "Title";
            // 
            // labelControl7
            // 
            this.labelControl7.Location = new System.Drawing.Point(5, 23);
            this.labelControl7.Name = "labelControl7";
            this.labelControl7.Size = new System.Drawing.Size(45, 13);
            this.labelControl7.TabIndex = 49;
            this.labelControl7.Text = "Category";
            // 
            // txtDes
            // 
            this.txtDes.Location = new System.Drawing.Point(473, 87);
            this.txtDes.Name = "txtDes";
            this.txtDes.Size = new System.Drawing.Size(150, 20);
            this.txtDes.TabIndex = 54;
            // 
            // txtTitle
            // 
            this.txtTitle.Location = new System.Drawing.Point(161, 87);
            this.txtTitle.Name = "txtTitle";
            this.txtTitle.Properties.DisplayFormat.FormatString = "d";
            this.txtTitle.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.txtTitle.Properties.EditFormat.FormatString = "d";
            this.txtTitle.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.txtTitle.Size = new System.Drawing.Size(150, 20);
            this.txtTitle.TabIndex = 52;
            // 
            // txtECategory
            // 
            this.txtECategory.Location = new System.Drawing.Point(5, 42);
            this.txtECategory.Name = "txtECategory";
            this.txtECategory.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.txtECategory.Size = new System.Drawing.Size(125, 20);
            this.txtECategory.TabIndex = 50;
            this.txtECategory.SelectedValueChanged += new System.EventHandler(this.txtECategory_SelectedValueChanged);
            // 
            // txtDate
            // 
            this.txtDate.EditValue = null;
            this.txtDate.Location = new System.Drawing.Point(5, 87);
            this.txtDate.Name = "txtDate";
            this.txtDate.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.txtDate.Properties.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.txtDate.Properties.Mask.EditMask = "";
            this.txtDate.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.None;
            this.txtDate.Size = new System.Drawing.Size(150, 20);
            this.txtDate.TabIndex = 62;
            // 
            // ExpenseManagement
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.gridInvoiceExpanse);
            this.Controls.Add(this.groupControl1);
            this.Name = "ExpenseManagement";
            this.Size = new System.Drawing.Size(1009, 458);
            ((System.ComponentModel.ISupportInitialize)(this.gridInvoiceExpanse)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).EndInit();
            this.groupControl1.ResumeLayout(false);
            this.groupControl1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.SpinnerCashAsset.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtDue.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtTotal.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtPayment.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtDes.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtTitle.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtECategory.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtDate.Properties.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtDate.Properties)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraGrid.GridControl gridInvoiceExpanse;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView1;
        private DevExpress.XtraEditors.GroupControl groupControl1;
        private DevExpress.XtraEditors.LabelControl labelControl5;
        private DevExpress.XtraEditors.TextEdit txtTotal;
        private DevExpress.XtraEditors.LabelControl labelControl3;
        private DevExpress.XtraEditors.LabelControl labelControl4;
        private DevExpress.XtraEditors.TextEdit txtPayment;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.XtraEditors.SimpleButton BtnEMDelete;
        private DevExpress.XtraEditors.SimpleButton btnEMAdd;
        private DevExpress.XtraEditors.LabelControl labelControl9;
        private DevExpress.XtraEditors.LabelControl labelControl8;
        private DevExpress.XtraEditors.LabelControl labelControl7;
        private DevExpress.XtraEditors.TextEdit txtDes;
        private DevExpress.XtraEditors.TextEdit txtTitle;
        private DevExpress.XtraEditors.ComboBoxEdit txtECategory;
        private DevExpress.XtraEditors.DateEdit txtDate;
        private DevExpress.XtraEditors.LabelControl labelControl2;
        private DevExpress.XtraEditors.TextEdit txtDue;
        private DevExpress.XtraEditors.SimpleButton btnAddHeads;
        private DevExpress.XtraEditors.SearchLookUpEdit SpinnerCashAsset;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView2;
    }
}
