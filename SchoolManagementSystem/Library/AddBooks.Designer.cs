namespace SchoolManagementSystem.Library
{
    partial class AddBooks
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AddBooks));
            this.gridLibrary = new DevExpress.XtraGrid.GridControl();
            this.gridView2 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.groupControl2 = new DevExpress.XtraEditors.GroupControl();
            this.labelControl7 = new DevExpress.XtraEditors.LabelControl();
            this.txtPublisher = new DevExpress.XtraEditors.TextEdit();
            this.labelControl6 = new DevExpress.XtraEditors.LabelControl();
            this.btnLDelete = new DevExpress.XtraEditors.SimpleButton();
            this.BtnLAdd = new DevExpress.XtraEditors.SimpleButton();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl3 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl4 = new DevExpress.XtraEditors.LabelControl();
            this.txtLName = new DevExpress.XtraEditors.TextEdit();
            this.labelControl5 = new DevExpress.XtraEditors.LabelControl();
            this.txtLStatus = new DevExpress.XtraEditors.ComboBoxEdit();
            this.txtCategory = new DevExpress.XtraEditors.ComboBoxEdit();
            this.txtLPrice = new DevExpress.XtraEditors.TextEdit();
            this.txtLDes = new DevExpress.XtraEditors.TextEdit();
            this.txtLAuthor = new DevExpress.XtraEditors.TextEdit();
            ((System.ComponentModel.ISupportInitialize)(this.gridLibrary)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl2)).BeginInit();
            this.groupControl2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtPublisher.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtLName.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtLStatus.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtCategory.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtLPrice.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtLDes.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtLAuthor.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // gridLibrary
            // 
            this.gridLibrary.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridLibrary.Location = new System.Drawing.Point(0, 115);
            this.gridLibrary.MainView = this.gridView2;
            this.gridLibrary.Name = "gridLibrary";
            this.gridLibrary.Size = new System.Drawing.Size(957, 375);
            this.gridLibrary.TabIndex = 5;
            this.gridLibrary.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridView2});
            // 
            // gridView2
            // 
            this.gridView2.GridControl = this.gridLibrary;
            this.gridView2.Name = "gridView2";
            this.gridView2.OptionsBehavior.EditingMode = DevExpress.XtraGrid.Views.Grid.GridEditingMode.EditFormInplace;
            this.gridView2.OptionsFind.AlwaysVisible = true;
            this.gridView2.RowUpdated += new DevExpress.XtraGrid.Views.Base.RowObjectEventHandler(this.gridView2_RowUpdated);
            // 
            // groupControl2
            // 
            this.groupControl2.Controls.Add(this.labelControl7);
            this.groupControl2.Controls.Add(this.txtPublisher);
            this.groupControl2.Controls.Add(this.labelControl6);
            this.groupControl2.Controls.Add(this.btnLDelete);
            this.groupControl2.Controls.Add(this.BtnLAdd);
            this.groupControl2.Controls.Add(this.labelControl1);
            this.groupControl2.Controls.Add(this.labelControl2);
            this.groupControl2.Controls.Add(this.labelControl3);
            this.groupControl2.Controls.Add(this.labelControl4);
            this.groupControl2.Controls.Add(this.txtLName);
            this.groupControl2.Controls.Add(this.labelControl5);
            this.groupControl2.Controls.Add(this.txtLStatus);
            this.groupControl2.Controls.Add(this.txtCategory);
            this.groupControl2.Controls.Add(this.txtLPrice);
            this.groupControl2.Controls.Add(this.txtLDes);
            this.groupControl2.Controls.Add(this.txtLAuthor);
            this.groupControl2.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupControl2.Location = new System.Drawing.Point(0, 0);
            this.groupControl2.Name = "groupControl2";
            this.groupControl2.Size = new System.Drawing.Size(957, 115);
            this.groupControl2.TabIndex = 4;
            this.groupControl2.Text = "Add Books";
            // 
            // labelControl7
            // 
            this.labelControl7.Location = new System.Drawing.Point(317, 23);
            this.labelControl7.Name = "labelControl7";
            this.labelControl7.Size = new System.Drawing.Size(43, 13);
            this.labelControl7.TabIndex = 75;
            this.labelControl7.Text = "Publisher";
            // 
            // txtPublisher
            // 
            this.txtPublisher.Location = new System.Drawing.Point(317, 42);
            this.txtPublisher.Name = "txtPublisher";
            this.txtPublisher.Size = new System.Drawing.Size(150, 20);
            this.txtPublisher.TabIndex = 76;
            // 
            // labelControl6
            // 
            this.labelControl6.Location = new System.Drawing.Point(317, 68);
            this.labelControl6.Name = "labelControl6";
            this.labelControl6.Size = new System.Drawing.Size(31, 13);
            this.labelControl6.TabIndex = 73;
            this.labelControl6.Text = "Status";
            // 
            // btnLDelete
            // 
            this.btnLDelete.Image = ((System.Drawing.Image)(resources.GetObject("btnLDelete.Image")));
            this.btnLDelete.Location = new System.Drawing.Point(556, 84);
            this.btnLDelete.Name = "btnLDelete";
            this.btnLDelete.Size = new System.Drawing.Size(67, 23);
            this.btnLDelete.TabIndex = 72;
            this.btnLDelete.Text = "Delete";
            this.btnLDelete.Click += new System.EventHandler(this.btnLDelete_Click);
            // 
            // BtnLAdd
            // 
            this.BtnLAdd.Image = ((System.Drawing.Image)(resources.GetObject("BtnLAdd.Image")));
            this.BtnLAdd.Location = new System.Drawing.Point(483, 84);
            this.BtnLAdd.Name = "BtnLAdd";
            this.BtnLAdd.Size = new System.Drawing.Size(67, 23);
            this.BtnLAdd.TabIndex = 71;
            this.BtnLAdd.Text = "Add";
            this.BtnLAdd.Click += new System.EventHandler(this.BtnLAdd_Click);
            // 
            // labelControl1
            // 
            this.labelControl1.Location = new System.Drawing.Point(161, 68);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(45, 13);
            this.labelControl1.TabIndex = 69;
            this.labelControl1.Text = "Category";
            // 
            // labelControl2
            // 
            this.labelControl2.Location = new System.Drawing.Point(5, 68);
            this.labelControl2.Name = "labelControl2";
            this.labelControl2.Size = new System.Drawing.Size(23, 13);
            this.labelControl2.TabIndex = 67;
            this.labelControl2.Text = "Price";
            // 
            // labelControl3
            // 
            this.labelControl3.Location = new System.Drawing.Point(473, 23);
            this.labelControl3.Name = "labelControl3";
            this.labelControl3.Size = new System.Drawing.Size(53, 13);
            this.labelControl3.TabIndex = 65;
            this.labelControl3.Text = "Description";
            // 
            // labelControl4
            // 
            this.labelControl4.Location = new System.Drawing.Point(161, 23);
            this.labelControl4.Name = "labelControl4";
            this.labelControl4.Size = new System.Drawing.Size(33, 13);
            this.labelControl4.TabIndex = 63;
            this.labelControl4.Text = "Author";
            // 
            // txtLName
            // 
            this.txtLName.Location = new System.Drawing.Point(5, 42);
            this.txtLName.Name = "txtLName";
            this.txtLName.Size = new System.Drawing.Size(150, 20);
            this.txtLName.TabIndex = 62;
            // 
            // labelControl5
            // 
            this.labelControl5.Location = new System.Drawing.Point(5, 23);
            this.labelControl5.Name = "labelControl5";
            this.labelControl5.Size = new System.Drawing.Size(27, 13);
            this.labelControl5.TabIndex = 61;
            this.labelControl5.Text = "Name";
            // 
            // txtLStatus
            // 
            this.txtLStatus.Location = new System.Drawing.Point(317, 87);
            this.txtLStatus.Name = "txtLStatus";
            this.txtLStatus.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.txtLStatus.Properties.Items.AddRange(new object[] {
            "Available",
            "Unavailable"});
            this.txtLStatus.Size = new System.Drawing.Size(150, 20);
            this.txtLStatus.TabIndex = 74;
            // 
            // txtCategory
            // 
            this.txtCategory.Location = new System.Drawing.Point(161, 87);
            this.txtCategory.Name = "txtCategory";
            this.txtCategory.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.txtCategory.Size = new System.Drawing.Size(150, 20);
            this.txtCategory.TabIndex = 70;
            // 
            // txtLPrice
            // 
            this.txtLPrice.Location = new System.Drawing.Point(5, 87);
            this.txtLPrice.Name = "txtLPrice";
            this.txtLPrice.Size = new System.Drawing.Size(150, 20);
            this.txtLPrice.TabIndex = 68;
            // 
            // txtLDes
            // 
            this.txtLDes.Location = new System.Drawing.Point(473, 42);
            this.txtLDes.Name = "txtLDes";
            this.txtLDes.Size = new System.Drawing.Size(150, 20);
            this.txtLDes.TabIndex = 66;
            // 
            // txtLAuthor
            // 
            this.txtLAuthor.Location = new System.Drawing.Point(161, 42);
            this.txtLAuthor.Name = "txtLAuthor";
            this.txtLAuthor.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.txtLAuthor.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.txtLAuthor.Size = new System.Drawing.Size(150, 20);
            this.txtLAuthor.TabIndex = 64;
            // 
            // AddBooks
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.gridLibrary);
            this.Controls.Add(this.groupControl2);
            this.Name = "AddBooks";
            this.Size = new System.Drawing.Size(957, 490);
            this.Enter += new System.EventHandler(this.AddBooks_Enter);
            ((System.ComponentModel.ISupportInitialize)(this.gridLibrary)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl2)).EndInit();
            this.groupControl2.ResumeLayout(false);
            this.groupControl2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtPublisher.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtLName.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtLStatus.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtCategory.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtLPrice.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtLDes.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtLAuthor.Properties)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraGrid.GridControl gridLibrary;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView2;
        private DevExpress.XtraEditors.GroupControl groupControl2;
        private DevExpress.XtraEditors.LabelControl labelControl7;
        private DevExpress.XtraEditors.TextEdit txtPublisher;
        private DevExpress.XtraEditors.LabelControl labelControl6;
        private DevExpress.XtraEditors.SimpleButton btnLDelete;
        private DevExpress.XtraEditors.SimpleButton BtnLAdd;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.XtraEditors.LabelControl labelControl2;
        private DevExpress.XtraEditors.LabelControl labelControl3;
        private DevExpress.XtraEditors.LabelControl labelControl4;
        private DevExpress.XtraEditors.TextEdit txtLName;
        private DevExpress.XtraEditors.LabelControl labelControl5;
        private DevExpress.XtraEditors.ComboBoxEdit txtLStatus;
        private DevExpress.XtraEditors.ComboBoxEdit txtCategory;
        private DevExpress.XtraEditors.TextEdit txtLPrice;
        private DevExpress.XtraEditors.TextEdit txtLDes;
        private DevExpress.XtraEditors.TextEdit txtLAuthor;
    }
}
