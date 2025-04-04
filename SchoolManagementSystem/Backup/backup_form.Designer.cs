namespace SchoolManagementSystem.Backup
{
    partial class backup_form
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

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.UCTake = new DevExpress.XtraEditors.XtraUserControl();
            this.SuspendLayout();
            // 
            // UCTake
            // 
            this.UCTake.Dock = System.Windows.Forms.DockStyle.Fill;
            this.UCTake.Location = new System.Drawing.Point(0, 0);
            this.UCTake.Name = "UCTake";
            this.UCTake.Size = new System.Drawing.Size(1062, 193);
            this.UCTake.TabIndex = 1;
            // 
            // backup_form
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1062, 193);
            this.Controls.Add(this.UCTake);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "backup_form";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Back Up / Restore";
            this.Load += new System.EventHandler(this.backup_form_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.XtraUserControl UCTake;
    }
}