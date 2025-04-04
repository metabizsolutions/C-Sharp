
namespace SchoolManagementSystem.Fees
{
    partial class invoice_delete_from
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(invoice_delete_from));
            this.label1 = new System.Windows.Forms.Label();
            this.txt_description = new System.Windows.Forms.TextBox();
            this.btn_yes = new DevExpress.XtraEditors.SimpleButton();
            this.btn_no = new DevExpress.XtraEditors.SimpleButton();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Tai Le", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.Red;
            this.label1.Location = new System.Drawing.Point(12, 4);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(368, 21);
            this.label1.TabIndex = 0;
            this.label1.Text = "Please Write down the reason of delete Invoice";
            // 
            // txt_description
            // 
            this.txt_description.Location = new System.Drawing.Point(23, 28);
            this.txt_description.Multiline = true;
            this.txt_description.Name = "txt_description";
            this.txt_description.Size = new System.Drawing.Size(347, 39);
            this.txt_description.TabIndex = 3;
            // 
            // btn_yes
            // 
            this.btn_yes.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("btn_yes.ImageOptions.Image")));
            this.btn_yes.ImageOptions.Location = DevExpress.XtraEditors.ImageLocation.MiddleLeft;
            this.btn_yes.Location = new System.Drawing.Point(123, 73);
            this.btn_yes.Name = "btn_yes";
            this.btn_yes.Size = new System.Drawing.Size(59, 23);
            this.btn_yes.TabIndex = 72;
            this.btn_yes.Text = "Yes";
            this.btn_yes.Click += new System.EventHandler(this.btn_yes_Click);
            // 
            // btn_no
            // 
            this.btn_no.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("btn_no.ImageOptions.Image")));
            this.btn_no.ImageOptions.Location = DevExpress.XtraEditors.ImageLocation.MiddleLeft;
            this.btn_no.Location = new System.Drawing.Point(188, 73);
            this.btn_no.Name = "btn_no";
            this.btn_no.Size = new System.Drawing.Size(59, 23);
            this.btn_no.TabIndex = 72;
            this.btn_no.Text = "No";
            this.btn_no.Click += new System.EventHandler(this.btn_no_Click);
            // 
            // invoice_delete_from
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(404, 101);
            this.Controls.Add(this.btn_no);
            this.Controls.Add(this.btn_yes);
            this.Controls.Add(this.txt_description);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "invoice_delete_from";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Information";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        public System.Windows.Forms.TextBox txt_description;
        private DevExpress.XtraEditors.SimpleButton btn_yes;
        private DevExpress.XtraEditors.SimpleButton btn_no;
    }
}