namespace SchoolManagementSystem.Hostel
{
    partial class Hostelsform
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
            this.XUCHosForm = new DevExpress.XtraEditors.XtraUserControl();
            this.SuspendLayout();
            // 
            // XUCHosForm
            // 
            this.XUCHosForm.Dock = System.Windows.Forms.DockStyle.Fill;
            this.XUCHosForm.Location = new System.Drawing.Point(0, 0);
            this.XUCHosForm.Name = "XUCHosForm";
            this.XUCHosForm.Size = new System.Drawing.Size(519, 450);
            this.XUCHosForm.TabIndex = 0;
            // 
            // Hostelsform
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(519, 450);
            this.Controls.Add(this.XUCHosForm);
            this.Name = "Hostelsform";
            this.Text = "Hostelsform";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.Hostelsform_FormClosed);
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.XtraUserControl XUCHosForm;
    }
}