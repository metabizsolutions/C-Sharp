namespace SchoolManagementSystem.Admin
{
    partial class MarketingSMS
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MarketingSMS));
            this.panel25 = new System.Windows.Forms.Panel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.txtTitle = new DevExpress.XtraEditors.TextEdit();
            this.btnSample = new DevExpress.XtraEditors.SimpleButton();
            this.label136 = new System.Windows.Forms.Label();
            this.txtFilePath = new DevExpress.XtraEditors.TextEdit();
            this.BtnSendSms = new DevExpress.XtraEditors.SimpleButton();
            this.label1 = new System.Windows.Forms.Label();
            this.txtMessage = new System.Windows.Forms.TextBox();
            this.BtnExlCellNO = new DevExpress.XtraEditors.SimpleButton();
            this.gridNoticebord = new DevExpress.XtraGrid.GridControl();
            this.gridView1 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.txtprogressbar = new DevExpress.XtraEditors.ProgressBarControl();
            this.txtStatus = new DevExpress.XtraEditors.ListBoxControl();
            this.panel25.SuspendLayout();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtTitle.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtFilePath.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridNoticebord)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtprogressbar.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtStatus)).BeginInit();
            this.SuspendLayout();
            // 
            // panel25
            // 
            this.panel25.BackColor = System.Drawing.Color.AliceBlue;
            this.panel25.Controls.Add(this.panel1);
            this.panel25.Controls.Add(this.gridNoticebord);
            this.panel25.Controls.Add(this.txtprogressbar);
            this.panel25.Controls.Add(this.txtStatus);
            this.panel25.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel25.Location = new System.Drawing.Point(0, 0);
            this.panel25.Name = "panel25";
            this.panel25.Size = new System.Drawing.Size(959, 464);
            this.panel25.TabIndex = 4;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.txtTitle);
            this.panel1.Controls.Add(this.btnSample);
            this.panel1.Controls.Add(this.label136);
            this.panel1.Controls.Add(this.txtFilePath);
            this.panel1.Controls.Add(this.BtnSendSms);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.txtMessage);
            this.panel1.Controls.Add(this.BtnExlCellNO);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(491, 220);
            this.panel1.TabIndex = 106;
            // 
            // txtTitle
            // 
            this.txtTitle.Location = new System.Drawing.Point(68, 3);
            this.txtTitle.Name = "txtTitle";
            this.txtTitle.Size = new System.Drawing.Size(420, 20);
            this.txtTitle.TabIndex = 102;
            // 
            // btnSample
            // 
            this.btnSample.Appearance.BorderColor = System.Drawing.Color.Purple;
            this.btnSample.Appearance.Font = new System.Drawing.Font("Segoe UI Semibold", 11F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSample.Appearance.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.btnSample.Appearance.Options.UseBorderColor = true;
            this.btnSample.Appearance.Options.UseFont = true;
            this.btnSample.Appearance.Options.UseForeColor = true;
            this.btnSample.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("btnSample.ImageOptions.Image")));
            this.btnSample.ImageOptions.Location = DevExpress.XtraEditors.ImageLocation.MiddleLeft;
            this.btnSample.Location = new System.Drawing.Point(318, 148);
            this.btnSample.Margin = new System.Windows.Forms.Padding(4);
            this.btnSample.Name = "btnSample";
            this.btnSample.Size = new System.Drawing.Size(170, 23);
            this.btnSample.TabIndex = 105;
            this.btnSample.Text = "Download Sample";
            this.btnSample.Click += new System.EventHandler(this.btnSample_Click);
            // 
            // label136
            // 
            this.label136.AutoSize = true;
            this.label136.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label136.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.label136.Location = new System.Drawing.Point(1, 25);
            this.label136.Name = "label136";
            this.label136.Size = new System.Drawing.Size(61, 17);
            this.label136.TabIndex = 53;
            this.label136.Text = "Message";
            // 
            // txtFilePath
            // 
            this.txtFilePath.EditValue = "File Path";
            this.txtFilePath.Enabled = false;
            this.txtFilePath.Location = new System.Drawing.Point(68, 151);
            this.txtFilePath.Name = "txtFilePath";
            this.txtFilePath.Size = new System.Drawing.Size(93, 20);
            this.txtFilePath.TabIndex = 104;
            // 
            // BtnSendSms
            // 
            this.BtnSendSms.Appearance.BorderColor = System.Drawing.Color.Purple;
            this.BtnSendSms.Appearance.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtnSendSms.Appearance.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.BtnSendSms.Appearance.Options.UseBorderColor = true;
            this.BtnSendSms.Appearance.Options.UseFont = true;
            this.BtnSendSms.Appearance.Options.UseForeColor = true;
            this.BtnSendSms.Enabled = false;
            this.BtnSendSms.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("BtnSendSms.ImageOptions.Image")));
            this.BtnSendSms.ImageOptions.Location = DevExpress.XtraEditors.ImageLocation.MiddleLeft;
            this.BtnSendSms.Location = new System.Drawing.Point(68, 178);
            this.BtnSendSms.Name = "BtnSendSms";
            this.BtnSendSms.Size = new System.Drawing.Size(417, 29);
            this.BtnSendSms.TabIndex = 54;
            this.BtnSendSms.Text = "Send Sms";
            this.BtnSendSms.Click += new System.EventHandler(this.BtnSendSms_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.label1.Location = new System.Drawing.Point(1, 6);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(33, 17);
            this.label1.TabIndex = 103;
            this.label1.Text = "Title";
            // 
            // txtMessage
            // 
            this.txtMessage.Location = new System.Drawing.Point(68, 25);
            this.txtMessage.Multiline = true;
            this.txtMessage.Name = "txtMessage";
            this.txtMessage.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtMessage.Size = new System.Drawing.Size(420, 120);
            this.txtMessage.TabIndex = 57;
            // 
            // BtnExlCellNO
            // 
            this.BtnExlCellNO.Appearance.BorderColor = System.Drawing.Color.Purple;
            this.BtnExlCellNO.Appearance.Font = new System.Drawing.Font("Segoe UI Semibold", 11F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtnExlCellNO.Appearance.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.BtnExlCellNO.Appearance.Options.UseBorderColor = true;
            this.BtnExlCellNO.Appearance.Options.UseFont = true;
            this.BtnExlCellNO.Appearance.Options.UseForeColor = true;
            this.BtnExlCellNO.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("BtnExlCellNO.ImageOptions.Image")));
            this.BtnExlCellNO.ImageOptions.Location = DevExpress.XtraEditors.ImageLocation.MiddleLeft;
            this.BtnExlCellNO.Location = new System.Drawing.Point(168, 148);
            this.BtnExlCellNO.Margin = new System.Windows.Forms.Padding(4);
            this.BtnExlCellNO.Name = "BtnExlCellNO";
            this.BtnExlCellNO.Size = new System.Drawing.Size(142, 23);
            this.BtnExlCellNO.TabIndex = 73;
            this.BtnExlCellNO.Text = "Import Excel";
            this.BtnExlCellNO.Click += new System.EventHandler(this.BtnExlCellNO_Click);
            // 
            // gridNoticebord
            // 
            this.gridNoticebord.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.gridNoticebord.Location = new System.Drawing.Point(0, 220);
            this.gridNoticebord.MainView = this.gridView1;
            this.gridNoticebord.Name = "gridNoticebord";
            this.gridNoticebord.Size = new System.Drawing.Size(494, 226);
            this.gridNoticebord.TabIndex = 100;
            this.gridNoticebord.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridView1});
            // 
            // gridView1
            // 
            this.gridView1.GridControl = this.gridNoticebord;
            this.gridView1.Name = "gridView1";
            this.gridView1.OptionsBehavior.EditingMode = DevExpress.XtraGrid.Views.Grid.GridEditingMode.EditFormInplace;
            this.gridView1.OptionsFind.AlwaysVisible = true;
            // 
            // txtprogressbar
            // 
            this.txtprogressbar.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.txtprogressbar.Location = new System.Drawing.Point(0, 446);
            this.txtprogressbar.Name = "txtprogressbar";
            this.txtprogressbar.Properties.AutoHeight = true;
            this.txtprogressbar.Properties.ShowTitle = true;
            this.txtprogressbar.Properties.Step = 1;
            this.txtprogressbar.Size = new System.Drawing.Size(494, 18);
            this.txtprogressbar.TabIndex = 99;
            this.txtprogressbar.TabStop = true;
            // 
            // txtStatus
            // 
            this.txtStatus.Cursor = System.Windows.Forms.Cursors.Default;
            this.txtStatus.Dock = System.Windows.Forms.DockStyle.Right;
            this.txtStatus.ItemAutoHeight = true;
            this.txtStatus.Location = new System.Drawing.Point(494, 0);
            this.txtStatus.Name = "txtStatus";
            this.txtStatus.Size = new System.Drawing.Size(465, 464);
            this.txtStatus.TabIndex = 75;
            // 
            // MarketingSMS
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.panel25);
            this.Name = "MarketingSMS";
            this.Size = new System.Drawing.Size(959, 464);
            this.panel25.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtTitle.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtFilePath.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridNoticebord)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtprogressbar.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtStatus)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel25;
        private DevExpress.XtraEditors.SimpleButton BtnExlCellNO;
        private System.Windows.Forms.TextBox txtMessage;
        private DevExpress.XtraEditors.SimpleButton BtnSendSms;
        private DevExpress.XtraEditors.ListBoxControl txtStatus;
        private DevExpress.XtraEditors.ProgressBarControl txtprogressbar;
        private DevExpress.XtraGrid.GridControl gridNoticebord;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView1;
        private DevExpress.XtraEditors.TextEdit txtTitle;
        private DevExpress.XtraEditors.TextEdit txtFilePath;
        private DevExpress.XtraEditors.SimpleButton btnSample;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label136;
        private System.Windows.Forms.Panel panel1;
    }
}
