namespace SchoolManagementSystem.Class
{
    partial class ManageClass
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ManageClass));
            this.groupControl1 = new DevExpress.XtraEditors.GroupControl();
            this.BtnPDelete = new DevExpress.XtraEditors.SimpleButton();
            this.gridClass = new DevExpress.XtraGrid.GridControl();
            this.gridView1 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.btnCAdd = new DevExpress.XtraEditors.SimpleButton();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).BeginInit();
            this.groupControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridClass)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // groupControl1
            // 
            this.groupControl1.Controls.Add(this.btnCAdd);
            this.groupControl1.Controls.Add(this.BtnPDelete);
            this.groupControl1.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupControl1.Location = new System.Drawing.Point(0, 0);
            this.groupControl1.Name = "groupControl1";
            this.groupControl1.Size = new System.Drawing.Size(1227, 54);
            this.groupControl1.TabIndex = 0;
            this.groupControl1.Text = "Add Class";
            // 
            // BtnPDelete
            // 
            this.BtnPDelete.Enabled = false;
            this.BtnPDelete.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("BtnPDelete.ImageOptions.Image")));
            this.BtnPDelete.Location = new System.Drawing.Point(118, 26);
            this.BtnPDelete.Name = "BtnPDelete";
            this.BtnPDelete.Size = new System.Drawing.Size(67, 23);
            this.BtnPDelete.TabIndex = 60;
            this.BtnPDelete.Text = "Delete";
            this.BtnPDelete.Click += new System.EventHandler(this.BtnPDelete_Click);
            // 
            // gridClass
            // 
            this.gridClass.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridClass.Location = new System.Drawing.Point(0, 54);
            this.gridClass.MainView = this.gridView1;
            this.gridClass.Name = "gridClass";
            this.gridClass.Size = new System.Drawing.Size(1227, 418);
            this.gridClass.TabIndex = 1;
            this.gridClass.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridView1});
            // 
            // gridView1
            // 
            this.gridView1.GridControl = this.gridClass;
            this.gridView1.Name = "gridView1";
            this.gridView1.RowUpdated += new DevExpress.XtraGrid.Views.Base.RowObjectEventHandler(this.gridView1_RowUpdated);
            // 
            // btnCAdd
            // 
            this.btnCAdd.Enabled = false;
            this.btnCAdd.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("simpleButton1.ImageOptions.Image")));
            this.btnCAdd.Location = new System.Drawing.Point(5, 26);
            this.btnCAdd.Name = "btnCAdd";
            this.btnCAdd.Size = new System.Drawing.Size(107, 23);
            this.btnCAdd.TabIndex = 62;
            this.btnCAdd.Text = "Add New Class";
            this.btnCAdd.Click += new System.EventHandler(this.btnCAdd_Click);
            // 
            // ManageClass
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.gridClass);
            this.Controls.Add(this.groupControl1);
            this.Name = "ManageClass";
            this.Size = new System.Drawing.Size(1227, 472);
            this.Enter += new System.EventHandler(this.ManageClass_Enter);
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).EndInit();
            this.groupControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gridClass)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.GroupControl groupControl1;
        private DevExpress.XtraEditors.SimpleButton BtnPDelete;
        private DevExpress.XtraGrid.GridControl gridClass;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView1;
        private DevExpress.XtraEditors.SimpleButton btnCAdd;
    }
}
