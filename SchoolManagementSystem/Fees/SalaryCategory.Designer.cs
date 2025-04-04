namespace SchoolManagementSystem.Class
{
    partial class SalaryCategory
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SalaryCategory));
            this.groupControl1 = new DevExpress.XtraEditors.GroupControl();
            this.txtDefaultVal = new DevExpress.XtraEditors.TextEdit();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.BtnPDelete = new DevExpress.XtraEditors.SimpleButton();
            this.btnCAdd = new DevExpress.XtraEditors.SimpleButton();
            this.txtTitle = new DevExpress.XtraEditors.TextEdit();
            this.labelControl7 = new DevExpress.XtraEditors.LabelControl();
            this.gridClass = new DevExpress.XtraGrid.GridControl();
            this.gridView1 = new DevExpress.XtraGrid.Views.Grid.GridView();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).BeginInit();
            this.groupControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtDefaultVal.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtTitle.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridClass)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // groupControl1
            // 
            this.groupControl1.Controls.Add(this.txtDefaultVal);
            this.groupControl1.Controls.Add(this.labelControl1);
            this.groupControl1.Controls.Add(this.BtnPDelete);
            this.groupControl1.Controls.Add(this.btnCAdd);
            this.groupControl1.Controls.Add(this.txtTitle);
            this.groupControl1.Controls.Add(this.labelControl7);
            this.groupControl1.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupControl1.Location = new System.Drawing.Point(0, 0);
            this.groupControl1.Name = "groupControl1";
            this.groupControl1.Size = new System.Drawing.Size(966, 72);
            this.groupControl1.TabIndex = 0;
            this.groupControl1.Text = "Fees Category Management";
            // 
            // txtDefaultVal
            // 
            this.txtDefaultVal.EditValue = "0";
            this.txtDefaultVal.Location = new System.Drawing.Point(161, 42);
            this.txtDefaultVal.Name = "txtDefaultVal";
            this.txtDefaultVal.Size = new System.Drawing.Size(150, 20);
            this.txtDefaultVal.TabIndex = 62;
            this.txtDefaultVal.ToolTip = resources.GetString("txtDefaultVal.ToolTip");
            // 
            // labelControl1
            // 
            this.labelControl1.Location = new System.Drawing.Point(161, 23);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(64, 13);
            this.labelControl1.TabIndex = 61;
            this.labelControl1.Text = "Default Value";
            // 
            // BtnPDelete
            // 
            this.BtnPDelete.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("BtnPDelete.ImageOptions.Image")));
            this.BtnPDelete.Location = new System.Drawing.Point(878, 39);
            this.BtnPDelete.Name = "BtnPDelete";
            this.BtnPDelete.Size = new System.Drawing.Size(67, 23);
            this.BtnPDelete.TabIndex = 60;
            this.BtnPDelete.Text = "Delete";
            this.BtnPDelete.Click += new System.EventHandler(this.BtnPDelete_Click);
            // 
            // btnCAdd
            // 
            this.btnCAdd.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("btnCAdd.ImageOptions.Image")));
            this.btnCAdd.Location = new System.Drawing.Point(398, 43);
            this.btnCAdd.Name = "btnCAdd";
            this.btnCAdd.Size = new System.Drawing.Size(67, 23);
            this.btnCAdd.TabIndex = 59;
            this.btnCAdd.Text = "Add";
            this.btnCAdd.Click += new System.EventHandler(this.btnCAdd_Click);
            // 
            // txtTitle
            // 
            this.txtTitle.Location = new System.Drawing.Point(5, 42);
            this.txtTitle.Name = "txtTitle";
            this.txtTitle.Size = new System.Drawing.Size(150, 20);
            this.txtTitle.TabIndex = 50;
            // 
            // labelControl7
            // 
            this.labelControl7.Location = new System.Drawing.Point(5, 23);
            this.labelControl7.Name = "labelControl7";
            this.labelControl7.Size = new System.Drawing.Size(75, 13);
            this.labelControl7.TabIndex = 49;
            this.labelControl7.Text = "Category Name";
            // 
            // gridClass
            // 
            this.gridClass.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridClass.Location = new System.Drawing.Point(0, 72);
            this.gridClass.MainView = this.gridView1;
            this.gridClass.Name = "gridClass";
            this.gridClass.Size = new System.Drawing.Size(966, 400);
            this.gridClass.TabIndex = 8;
            this.gridClass.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridView1});
            // 
            // gridView1
            // 
            this.gridView1.GridControl = this.gridClass;
            this.gridView1.Name = "gridView1";
            this.gridView1.OptionsBehavior.Editable = false;
            this.gridView1.OptionsBehavior.EditingMode = DevExpress.XtraGrid.Views.Grid.GridEditingMode.Inplace;
            this.gridView1.OptionsFind.AlwaysVisible = true;
            this.gridView1.RowUpdated += new DevExpress.XtraGrid.Views.Base.RowObjectEventHandler(this.gridView1_RowUpdated_1);
            // 
            // SalaryCategory
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.gridClass);
            this.Controls.Add(this.groupControl1);
            this.Name = "SalaryCategory";
            this.Size = new System.Drawing.Size(966, 472);
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).EndInit();
            this.groupControl1.ResumeLayout(false);
            this.groupControl1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtDefaultVal.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtTitle.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridClass)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.GroupControl groupControl1;
        private DevExpress.XtraEditors.SimpleButton BtnPDelete;
        private DevExpress.XtraEditors.SimpleButton btnCAdd;
        private DevExpress.XtraEditors.TextEdit txtTitle;
        private DevExpress.XtraEditors.LabelControl labelControl7;
        private DevExpress.XtraEditors.TextEdit txtDefaultVal;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.XtraGrid.GridControl gridClass;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView1;
    }
}
