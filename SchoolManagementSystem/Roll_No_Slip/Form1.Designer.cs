namespace SchoolManagementSystem.Roll_No_Slip
{
    partial class Form1
    {
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.Panel mainContainer;
        private System.Windows.Forms.RichTextBox rtbNotes;
        private System.Windows.Forms.Button btnResetNotes;
        private System.Windows.Forms.Button btnPrint;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.mainContainer = new System.Windows.Forms.Panel();
            this.rtbNotes = new System.Windows.Forms.RichTextBox();
            this.btnResetNotes = new System.Windows.Forms.Button();
            this.btnPrint = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // mainContainer
            // 
            this.mainContainer.AutoScroll = true;
            this.mainContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.mainContainer.Location = new System.Drawing.Point(0, 0);
            this.mainContainer.Name = "mainContainer";
            this.mainContainer.Size = new System.Drawing.Size(1280, 720);
            this.mainContainer.TabIndex = 0;
            // 
            // rtbNotes
            // 
            this.rtbNotes.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rtbNotes.Location = new System.Drawing.Point(62, 524);
            this.rtbNotes.Name = "rtbNotes";
            this.rtbNotes.Size = new System.Drawing.Size(600, 120);
            this.rtbNotes.TabIndex = 1;
            this.rtbNotes.Text = "";
            // 
            // btnResetNotes
            // 
            this.btnResetNotes.Font = new System.Drawing.Font("Arial", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnResetNotes.Location = new System.Drawing.Point(470, 770);
            this.btnResetNotes.Name = "btnResetNotes";
            this.btnResetNotes.Size = new System.Drawing.Size(120, 40);
            this.btnResetNotes.TabIndex = 2;
            this.btnResetNotes.Text = "Reset Notes";
            this.btnResetNotes.UseVisualStyleBackColor = true;
            this.btnResetNotes.Click += new System.EventHandler(this.btnResetNotes_Click);
            // 
            // btnPrint
            // 
            this.btnPrint.Font = new System.Drawing.Font("Arial", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnPrint.Location = new System.Drawing.Point(250, 770);
            this.btnPrint.Name = "btnPrint";
            this.btnPrint.Size = new System.Drawing.Size(150, 40);
            this.btnPrint.TabIndex = 3;
            this.btnPrint.Text = "Print Roll No Slip";
            this.btnPrint.UseVisualStyleBackColor = true;
            this.btnPrint.Click += new System.EventHandler(this.PrintButton_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(1280, 720);
            this.Controls.Add(this.btnPrint);
            this.Controls.Add(this.btnResetNotes);
            this.Controls.Add(this.rtbNotes);
            this.Controls.Add(this.mainContainer);
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Roll No Slip Generator";
            this.ResumeLayout(false);

        }
    }
}