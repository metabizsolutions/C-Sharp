namespace SchoolManagementSystem.Class
{
    partial class Add_section_subject
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Add_section_subject));
            this.panelinstallment = new System.Windows.Forms.FlowLayoutPanel();
            this.panel3 = new System.Windows.Forms.Panel();
            this.btn_addsubject = new DevExpress.XtraEditors.SimpleButton();
            this.panel3.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelinstallment
            // 
            this.panelinstallment.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelinstallment.Location = new System.Drawing.Point(0, 40);
            this.panelinstallment.Name = "panelinstallment";
            this.panelinstallment.Size = new System.Drawing.Size(604, 11);
            this.panelinstallment.TabIndex = 45;
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.btn_addsubject);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel3.Location = new System.Drawing.Point(0, 0);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(604, 40);
            this.panel3.TabIndex = 46;
            // 
            // btn_addsubject
            // 
            this.btn_addsubject.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("btncreateInstallment.ImageOptions.Image")));
            this.btn_addsubject.Location = new System.Drawing.Point(244, 6);
            this.btn_addsubject.Name = "btn_addsubject";
            this.btn_addsubject.Size = new System.Drawing.Size(107, 22);
            this.btn_addsubject.TabIndex = 42;
            this.btn_addsubject.Text = "Add Subject";
            this.btn_addsubject.Click += new System.EventHandler(this.btn_addsubject_Click);
            // 
            // Add_section_subject
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(604, 450);
            this.Controls.Add(this.panelinstallment);
            this.Controls.Add(this.panel3);
            this.Name = "Add_section_subject";
            this.Text = "Add_section_subject";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.Add_section_subject_FormClosed);
            this.panel3.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.FlowLayoutPanel panelinstallment;
        private System.Windows.Forms.Panel panel3;
        private DevExpress.XtraEditors.SimpleButton btn_addsubject;
    }
}