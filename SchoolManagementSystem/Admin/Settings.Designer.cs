namespace SchoolManagementSystem.Admin
{
    partial class Settings
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
            this.PSettings = new System.Windows.Forms.Panel();
            this.SuspendLayout();
            // 
            // PSettings
            // 
            this.PSettings.Dock = System.Windows.Forms.DockStyle.Fill;
            this.PSettings.Location = new System.Drawing.Point(0, 0);
            this.PSettings.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.PSettings.Name = "PSettings";
            this.PSettings.Size = new System.Drawing.Size(1797, 855);
            this.PSettings.TabIndex = 0;
            this.PSettings.Paint += new System.Windows.Forms.PaintEventHandler(this.PSettings_Paint);
            // 
            // Settings
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1797, 855);
            this.Controls.Add(this.PSettings);
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Name = "Settings";
            this.Text = "Settings";
            this.Leave += new System.EventHandler(this.Settings_Leave);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel PSettings;
    }
}