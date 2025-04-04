namespace SchoolManagementSystem.Library
{
    partial class IssueBooks
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(IssueBooks));
            this.gridLibrary = new DevExpress.XtraGrid.GridControl();
            this.gridView2 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.groupControl2 = new DevExpress.XtraEditors.GroupControl();
            this.labelControl7 = new DevExpress.XtraEditors.LabelControl();
            this.btnLDelete = new DevExpress.XtraEditors.SimpleButton();
            this.BtnIssue = new DevExpress.XtraEditors.SimpleButton();
            this.labelControl4 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl5 = new DevExpress.XtraEditors.LabelControl();
            this.txtQty = new DevExpress.XtraEditors.TextEdit();
            this.txtBook = new DevExpress.XtraEditors.ComboBoxEdit();
            this.txtIssueTo = new DevExpress.XtraEditors.ComboBoxEdit();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.txtBookID = new DevExpress.XtraEditors.TextEdit();
            ((System.ComponentModel.ISupportInitialize)(this.gridLibrary)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl2)).BeginInit();
            this.groupControl2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtQty.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtBook.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtIssueTo.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtBookID.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // gridLibrary
            // 
            this.gridLibrary.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridLibrary.Location = new System.Drawing.Point(0, 78);
            this.gridLibrary.MainView = this.gridView2;
            this.gridLibrary.Name = "gridLibrary";
            this.gridLibrary.Size = new System.Drawing.Size(976, 396);
            this.gridLibrary.TabIndex = 7;
            this.gridLibrary.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridView2});
            // 
            // gridView2
            // 
            this.gridView2.GridControl = this.gridLibrary;
            this.gridView2.Name = "gridView2";
            this.gridView2.OptionsBehavior.ReadOnly = true;
            this.gridView2.OptionsFind.AlwaysVisible = true;
            // 
            // groupControl2
            // 
            this.groupControl2.Controls.Add(this.labelControl1);
            this.groupControl2.Controls.Add(this.txtBookID);
            this.groupControl2.Controls.Add(this.labelControl7);
            this.groupControl2.Controls.Add(this.btnLDelete);
            this.groupControl2.Controls.Add(this.BtnIssue);
            this.groupControl2.Controls.Add(this.labelControl4);
            this.groupControl2.Controls.Add(this.labelControl5);
            this.groupControl2.Controls.Add(this.txtQty);
            this.groupControl2.Controls.Add(this.txtBook);
            this.groupControl2.Controls.Add(this.txtIssueTo);
            this.groupControl2.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupControl2.Location = new System.Drawing.Point(0, 0);
            this.groupControl2.Name = "groupControl2";
            this.groupControl2.Size = new System.Drawing.Size(976, 78);
            this.groupControl2.TabIndex = 6;
            this.groupControl2.Text = "Issue Books";
            // 
            // labelControl7
            // 
            this.labelControl7.Location = new System.Drawing.Point(317, 23);
            this.labelControl7.Name = "labelControl7";
            this.labelControl7.Size = new System.Drawing.Size(41, 13);
            this.labelControl7.TabIndex = 75;
            this.labelControl7.Text = "Issue To";
            // 
            // btnLDelete
            // 
            this.btnLDelete.Image = ((System.Drawing.Image)(resources.GetObject("btnLDelete.Image")));
            this.btnLDelete.Location = new System.Drawing.Point(702, 39);
            this.btnLDelete.Name = "btnLDelete";
            this.btnLDelete.Size = new System.Drawing.Size(67, 23);
            this.btnLDelete.TabIndex = 72;
            this.btnLDelete.Text = "Delete";
            // 
            // BtnIssue
            // 
            this.BtnIssue.Image = ((System.Drawing.Image)(resources.GetObject("BtnIssue.Image")));
            this.BtnIssue.Location = new System.Drawing.Point(629, 40);
            this.BtnIssue.Name = "BtnIssue";
            this.BtnIssue.Size = new System.Drawing.Size(67, 23);
            this.BtnIssue.TabIndex = 71;
            this.BtnIssue.Text = "Issue";
            this.BtnIssue.Click += new System.EventHandler(this.BtnIssue_Click);
            // 
            // labelControl4
            // 
            this.labelControl4.Location = new System.Drawing.Point(161, 23);
            this.labelControl4.Name = "labelControl4";
            this.labelControl4.Size = new System.Drawing.Size(42, 13);
            this.labelControl4.TabIndex = 63;
            this.labelControl4.Text = "Quantity";
            // 
            // labelControl5
            // 
            this.labelControl5.Location = new System.Drawing.Point(5, 23);
            this.labelControl5.Name = "labelControl5";
            this.labelControl5.Size = new System.Drawing.Size(55, 13);
            this.labelControl5.TabIndex = 61;
            this.labelControl5.Text = "Select Book";
            // 
            // txtQty
            // 
            this.txtQty.Location = new System.Drawing.Point(161, 42);
            this.txtQty.Name = "txtQty";
            this.txtQty.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.txtQty.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.txtQty.Size = new System.Drawing.Size(150, 20);
            this.txtQty.TabIndex = 64;
            // 
            // txtBook
            // 
            this.txtBook.Location = new System.Drawing.Point(5, 42);
            this.txtBook.Name = "txtBook";
            this.txtBook.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.txtBook.Size = new System.Drawing.Size(150, 20);
            this.txtBook.TabIndex = 62;
            this.txtBook.SelectedIndexChanged += new System.EventHandler(this.txtBook_SelectedIndexChanged);
            // 
            // txtIssueTo
            // 
            this.txtIssueTo.Location = new System.Drawing.Point(317, 42);
            this.txtIssueTo.Name = "txtIssueTo";
            this.txtIssueTo.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.txtIssueTo.Size = new System.Drawing.Size(150, 20);
            this.txtIssueTo.TabIndex = 76;
            // 
            // labelControl1
            // 
            this.labelControl1.Location = new System.Drawing.Point(473, 23);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(37, 13);
            this.labelControl1.TabIndex = 77;
            this.labelControl1.Text = "Book ID";
            // 
            // txtBookID
            // 
            this.txtBookID.Location = new System.Drawing.Point(473, 42);
            this.txtBookID.Name = "txtBookID";
            this.txtBookID.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.txtBookID.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.txtBookID.Size = new System.Drawing.Size(150, 20);
            this.txtBookID.TabIndex = 78;
            // 
            // IssueBooks
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.gridLibrary);
            this.Controls.Add(this.groupControl2);
            this.Name = "IssueBooks";
            this.Size = new System.Drawing.Size(976, 474);
            this.Enter += new System.EventHandler(this.IssueBooks_Enter);
            ((System.ComponentModel.ISupportInitialize)(this.gridLibrary)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl2)).EndInit();
            this.groupControl2.ResumeLayout(false);
            this.groupControl2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtQty.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtBook.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtIssueTo.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtBookID.Properties)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraGrid.GridControl gridLibrary;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView2;
        private DevExpress.XtraEditors.GroupControl groupControl2;
        private DevExpress.XtraEditors.LabelControl labelControl7;
        private DevExpress.XtraEditors.SimpleButton btnLDelete;
        private DevExpress.XtraEditors.SimpleButton BtnIssue;
        private DevExpress.XtraEditors.LabelControl labelControl4;
        private DevExpress.XtraEditors.LabelControl labelControl5;
        private DevExpress.XtraEditors.TextEdit txtQty;
        private DevExpress.XtraEditors.ComboBoxEdit txtBook;
        private DevExpress.XtraEditors.ComboBoxEdit txtIssueTo;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.XtraEditors.TextEdit txtBookID;
    }
}
