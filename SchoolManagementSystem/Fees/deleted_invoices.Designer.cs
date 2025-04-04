
namespace SchoolManagementSystem.Fees
{
    partial class deleted_invoices
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
            this.gridInvoiceManage = new DevExpress.XtraGrid.GridControl();
            this.gridView1 = new DevExpress.XtraGrid.Views.Grid.GridView();
            ((System.ComponentModel.ISupportInitialize)(this.gridInvoiceManage)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // gridInvoiceManage
            // 
            this.gridInvoiceManage.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridInvoiceManage.Location = new System.Drawing.Point(0, 0);
            this.gridInvoiceManage.MainView = this.gridView1;
            this.gridInvoiceManage.Name = "gridInvoiceManage";
            this.gridInvoiceManage.Size = new System.Drawing.Size(1197, 400);
            this.gridInvoiceManage.TabIndex = 10;
            this.gridInvoiceManage.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridView1});
            // 
            // gridView1
            // 
            this.gridView1.GridControl = this.gridInvoiceManage;
            this.gridView1.Name = "gridView1";
            this.gridView1.OptionsBehavior.Editable = false;
            this.gridView1.OptionsFind.AlwaysVisible = true;
            this.gridView1.OptionsFind.FindDelay = 100;
            this.gridView1.OptionsView.ShowFooter = true;
            // 
            // deleted_invoices
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.gridInvoiceManage);
            this.Name = "deleted_invoices";
            this.Size = new System.Drawing.Size(1197, 400);
            ((System.ComponentModel.ISupportInitialize)(this.gridInvoiceManage)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraGrid.GridControl gridInvoiceManage;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView1;
    }
}
