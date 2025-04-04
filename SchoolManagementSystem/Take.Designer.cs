namespace SchoolManagementSystem
{
    partial class Take
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Take));
            this.UCTake = new DevExpress.XtraEditors.XtraUserControl();
            this.SuspendLayout();
            // 
            // UCTake
            // 
            this.UCTake.Dock = System.Windows.Forms.DockStyle.Fill;
            this.UCTake.Location = new System.Drawing.Point(0, 0);
            this.UCTake.Name = "UCTake";
            this.UCTake.Size = new System.Drawing.Size(964, 506);
            this.UCTake.TabIndex = 0;
            // 
            // Take
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(964, 506);
            this.Controls.Add(this.UCTake);
            this.IconOptions.Icon = ((System.Drawing.Icon)(resources.GetObject("Take.IconOptions.Icon")));
            this.Name = "Take";
            this.Load += new System.EventHandler(this.Take_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.XtraUserControl UCTake;
    }
}