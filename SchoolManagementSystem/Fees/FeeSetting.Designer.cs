namespace SchoolManagementSystem.Fees
{
    partial class FeeSetting
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
            this.gridFeeSetting = new DevExpress.XtraGrid.GridControl();
            this.gridView1 = new DevExpress.XtraGrid.Views.Grid.GridView();
            ((System.ComponentModel.ISupportInitialize)(this.gridFeeSetting)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // gridFeeSetting
            // 
            this.gridFeeSetting.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridFeeSetting.Location = new System.Drawing.Point(0, 0);
            this.gridFeeSetting.MainView = this.gridView1;
            this.gridFeeSetting.Name = "gridFeeSetting";
            this.gridFeeSetting.Size = new System.Drawing.Size(954, 427);
            this.gridFeeSetting.TabIndex = 11;
            this.gridFeeSetting.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridView1});
            // 
            // gridView1
            // 
            this.gridView1.GridControl = this.gridFeeSetting;
            this.gridView1.Name = "gridView1";
            this.gridView1.OptionsBehavior.Editable = false;
            this.gridView1.OptionsBehavior.EditingMode = DevExpress.XtraGrid.Views.Grid.GridEditingMode.Inplace;
            this.gridView1.OptionsFind.AlwaysVisible = true;
            this.gridView1.RowUpdated += new DevExpress.XtraGrid.Views.Base.RowObjectEventHandler(this.gridView1_RowUpdated);
            // 
            // FeeSetting
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.gridFeeSetting);
            this.Name = "FeeSetting";
            this.Size = new System.Drawing.Size(954, 427);
            this.Enter += new System.EventHandler(this.FeeSetting_Enter);
            ((System.ComponentModel.ISupportInitialize)(this.gridFeeSetting)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraGrid.GridControl gridFeeSetting;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView1;
    }
}
