namespace SchoolManagementSystem.Class
{
    partial class ManageSection
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ManageSection));
            this.gridSection = new DevExpress.XtraGrid.GridControl();
            this.gridView1 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.groupControl1 = new DevExpress.XtraEditors.GroupControl();
            this.BtnSDelete = new DevExpress.XtraEditors.SimpleButton();
            this.btnSAdd = new DevExpress.XtraEditors.SimpleButton();
            ((System.ComponentModel.ISupportInitialize)(this.gridSection)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).BeginInit();
            this.groupControl1.SuspendLayout();
            this.SuspendLayout();
            // 
            // gridSection
            // 
            this.gridSection.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridSection.Location = new System.Drawing.Point(0, 56);
            this.gridSection.MainView = this.gridView1;
            this.gridSection.Name = "gridSection";
            this.gridSection.Size = new System.Drawing.Size(1133, 367);
            this.gridSection.TabIndex = 3;
            this.gridSection.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridView1});
            // 
            // gridView1
            // 
            this.gridView1.GridControl = this.gridSection;
            this.gridView1.Name = "gridView1";
            this.gridView1.OptionsBehavior.Editable = false;
            this.gridView1.OptionsBehavior.EditingMode = DevExpress.XtraGrid.Views.Grid.GridEditingMode.Inplace;
            this.gridView1.OptionsFind.AlwaysVisible = true;
            this.gridView1.RowUpdated += new DevExpress.XtraGrid.Views.Base.RowObjectEventHandler(this.gridView1_RowUpdated);
            // 
            // groupControl1
            // 
            this.groupControl1.Controls.Add(this.BtnSDelete);
            this.groupControl1.Controls.Add(this.btnSAdd);
            this.groupControl1.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupControl1.Location = new System.Drawing.Point(0, 0);
            this.groupControl1.Name = "groupControl1";
            this.groupControl1.Size = new System.Drawing.Size(1133, 56);
            this.groupControl1.TabIndex = 2;
            this.groupControl1.Text = "Add Sections";
            // 
            // BtnSDelete
            // 
            this.BtnSDelete.Enabled = false;
            this.BtnSDelete.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("BtnSDelete.ImageOptions.Image")));
            this.BtnSDelete.Location = new System.Drawing.Point(109, 26);
            this.BtnSDelete.Name = "BtnSDelete";
            this.BtnSDelete.Size = new System.Drawing.Size(65, 23);
            this.BtnSDelete.TabIndex = 60;
            this.BtnSDelete.Text = "Delete";
            this.BtnSDelete.Click += new System.EventHandler(this.BtnSDelete_Click);
            // 
            // btnSAdd
            // 
            this.btnSAdd.Enabled = false;
            this.btnSAdd.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("btnSAdd.ImageOptions.Image")));
            this.btnSAdd.Location = new System.Drawing.Point(5, 26);
            this.btnSAdd.Name = "btnSAdd";
            this.btnSAdd.Size = new System.Drawing.Size(98, 23);
            this.btnSAdd.TabIndex = 59;
            this.btnSAdd.Text = "New Section";
            this.btnSAdd.Click += new System.EventHandler(this.btnSAdd_Click);
            // 
            // ManageSection
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.gridSection);
            this.Controls.Add(this.groupControl1);
            this.Name = "ManageSection";
            this.Size = new System.Drawing.Size(1133, 423);
            this.Enter += new System.EventHandler(this.ManageSection_Enter);
            ((System.ComponentModel.ISupportInitialize)(this.gridSection)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).EndInit();
            this.groupControl1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraGrid.GridControl gridSection;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView1;
        private DevExpress.XtraEditors.GroupControl groupControl1;
        private DevExpress.XtraEditors.SimpleButton BtnSDelete;
        private DevExpress.XtraEditors.SimpleButton btnSAdd;
    }
}
