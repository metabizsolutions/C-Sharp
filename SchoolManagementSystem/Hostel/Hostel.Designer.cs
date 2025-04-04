namespace SchoolManagementSystem.Hostel
{
    partial class Hostel
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Hostel));
            this.gridHostel = new DevExpress.XtraGrid.GridControl();
            this.gridView4 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.groupControl4 = new DevExpress.XtraEditors.GroupControl();
            this.btnHDelete = new DevExpress.XtraEditors.SimpleButton();
            this.btnHAdd = new DevExpress.XtraEditors.SimpleButton();
            this.labelControl17 = new DevExpress.XtraEditors.LabelControl();
            this.txtHName = new DevExpress.XtraEditors.TextEdit();
            this.labelControl19 = new DevExpress.XtraEditors.LabelControl();
            this.txtHdes = new DevExpress.XtraEditors.TextEdit();
            ((System.ComponentModel.ISupportInitialize)(this.gridHostel)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl4)).BeginInit();
            this.groupControl4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtHName.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtHdes.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // gridHostel
            // 
            this.gridHostel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridHostel.Location = new System.Drawing.Point(0, 70);
            this.gridHostel.MainView = this.gridView4;
            this.gridHostel.Name = "gridHostel";
            this.gridHostel.Size = new System.Drawing.Size(463, 297);
            this.gridHostel.TabIndex = 7;
            this.gridHostel.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridView4});
            // 
            // gridView4
            // 
            this.gridView4.GridControl = this.gridHostel;
            this.gridView4.Name = "gridView4";
            this.gridView4.OptionsBehavior.EditingMode = DevExpress.XtraGrid.Views.Grid.GridEditingMode.EditFormInplace;
            this.gridView4.OptionsFind.AlwaysVisible = true;
            this.gridView4.RowUpdated += new DevExpress.XtraGrid.Views.Base.RowObjectEventHandler(this.gridView4_RowUpdated);
            // 
            // groupControl4
            // 
            this.groupControl4.Controls.Add(this.btnHDelete);
            this.groupControl4.Controls.Add(this.btnHAdd);
            this.groupControl4.Controls.Add(this.labelControl17);
            this.groupControl4.Controls.Add(this.txtHName);
            this.groupControl4.Controls.Add(this.labelControl19);
            this.groupControl4.Controls.Add(this.txtHdes);
            this.groupControl4.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupControl4.Location = new System.Drawing.Point(0, 0);
            this.groupControl4.Name = "groupControl4";
            this.groupControl4.Size = new System.Drawing.Size(463, 70);
            this.groupControl4.TabIndex = 6;
            this.groupControl4.Text = "Add Hostel";
            // 
            // btnHDelete
            // 
            this.btnHDelete.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("btnHDelete.ImageOptions.Image")));
            this.btnHDelete.Location = new System.Drawing.Point(390, 39);
            this.btnHDelete.Name = "btnHDelete";
            this.btnHDelete.Size = new System.Drawing.Size(67, 23);
            this.btnHDelete.TabIndex = 72;
            this.btnHDelete.Text = "Delete";
            this.btnHDelete.Click += new System.EventHandler(this.btnHDelete_Click);
            // 
            // btnHAdd
            // 
            this.btnHAdd.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("btnHAdd.ImageOptions.Image")));
            this.btnHAdd.Location = new System.Drawing.Point(317, 39);
            this.btnHAdd.Name = "btnHAdd";
            this.btnHAdd.Size = new System.Drawing.Size(67, 23);
            this.btnHAdd.TabIndex = 71;
            this.btnHAdd.Text = "Add";
            this.btnHAdd.Click += new System.EventHandler(this.btnHAdd_Click);
            // 
            // labelControl17
            // 
            this.labelControl17.Location = new System.Drawing.Point(161, 23);
            this.labelControl17.Name = "labelControl17";
            this.labelControl17.Size = new System.Drawing.Size(53, 13);
            this.labelControl17.TabIndex = 65;
            this.labelControl17.Text = "Description";
            // 
            // txtHName
            // 
            this.txtHName.Location = new System.Drawing.Point(5, 42);
            this.txtHName.Name = "txtHName";
            this.txtHName.Size = new System.Drawing.Size(150, 20);
            this.txtHName.TabIndex = 62;
            // 
            // labelControl19
            // 
            this.labelControl19.Location = new System.Drawing.Point(5, 23);
            this.labelControl19.Name = "labelControl19";
            this.labelControl19.Size = new System.Drawing.Size(60, 13);
            this.labelControl19.TabIndex = 61;
            this.labelControl19.Text = "Hostel Name";
            // 
            // txtHdes
            // 
            this.txtHdes.Location = new System.Drawing.Point(161, 42);
            this.txtHdes.Name = "txtHdes";
            this.txtHdes.Size = new System.Drawing.Size(150, 20);
            this.txtHdes.TabIndex = 66;
            // 
            // Hostel
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.gridHostel);
            this.Controls.Add(this.groupControl4);
            this.Name = "Hostel";
            this.Size = new System.Drawing.Size(463, 367);
            ((System.ComponentModel.ISupportInitialize)(this.gridHostel)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl4)).EndInit();
            this.groupControl4.ResumeLayout(false);
            this.groupControl4.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtHName.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtHdes.Properties)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraGrid.GridControl gridHostel;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView4;
        private DevExpress.XtraEditors.GroupControl groupControl4;
        private DevExpress.XtraEditors.SimpleButton btnHDelete;
        private DevExpress.XtraEditors.SimpleButton btnHAdd;
        private DevExpress.XtraEditors.LabelControl labelControl17;
        private DevExpress.XtraEditors.TextEdit txtHName;
        private DevExpress.XtraEditors.LabelControl labelControl19;
        private DevExpress.XtraEditors.TextEdit txtHdes;
    }
}
