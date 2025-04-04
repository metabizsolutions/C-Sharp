namespace SchoolManagementSystem.Ceate_Session
{
    partial class New_Session
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(New_Session));
            this.gridsession = new DevExpress.XtraGrid.GridControl();
            this.gridView1 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.panel3 = new System.Windows.Forms.Panel();
            this.Btn_CreateSession = new DevExpress.XtraEditors.SimpleButton();
            this.panel1 = new System.Windows.Forms.Panel();
            this.BtnPDelete = new DevExpress.XtraEditors.SimpleButton();
            this.toggle_old_new = new DevExpress.XtraEditors.ToggleSwitch();
            ((System.ComponentModel.ISupportInitialize)(this.gridsession)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).BeginInit();
            this.panel3.SuspendLayout();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.toggle_old_new.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // gridsession
            // 
            this.gridsession.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridsession.Location = new System.Drawing.Point(0, 39);
            this.gridsession.MainView = this.gridView1;
            this.gridsession.Name = "gridsession";
            this.gridsession.Size = new System.Drawing.Size(1243, 432);
            this.gridsession.TabIndex = 6;
            this.gridsession.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridView1});
            // 
            // gridView1
            // 
            this.gridView1.GridControl = this.gridsession;
            this.gridView1.Name = "gridView1";
            this.gridView1.CustomRowCellEditForEditing += new DevExpress.XtraGrid.Views.Grid.CustomRowCellEditEventHandler(this.gridView1_CustomRowCellEditForEditing);
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.Btn_CreateSession);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel3.Location = new System.Drawing.Point(0, 471);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(1243, 39);
            this.panel3.TabIndex = 5;
            // 
            // Btn_CreateSession
            // 
            this.Btn_CreateSession.Location = new System.Drawing.Point(1101, 6);
            this.Btn_CreateSession.Name = "Btn_CreateSession";
            this.Btn_CreateSession.Size = new System.Drawing.Size(92, 23);
            this.Btn_CreateSession.TabIndex = 0;
            this.Btn_CreateSession.Text = "Create Session";
            this.Btn_CreateSession.Click += new System.EventHandler(this.Btn_CreateSession_Click);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.toggle_old_new);
            this.panel1.Controls.Add(this.BtnPDelete);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1243, 39);
            this.panel1.TabIndex = 4;
            // 
            // BtnPDelete
            // 
            this.BtnPDelete.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("BtnPDelete.ImageOptions.Image")));
            this.BtnPDelete.Location = new System.Drawing.Point(565, 8);
            this.BtnPDelete.Name = "BtnPDelete";
            this.BtnPDelete.Size = new System.Drawing.Size(67, 23);
            this.BtnPDelete.TabIndex = 61;
            this.BtnPDelete.Text = "Delete";
            this.BtnPDelete.Click += new System.EventHandler(this.BtnPDelete_Click);
            // 
            // toggle_old_new
            // 
            this.toggle_old_new.Location = new System.Drawing.Point(638, 11);
            this.toggle_old_new.Name = "toggle_old_new";
            this.toggle_old_new.Properties.OffText = "New Classes";
            this.toggle_old_new.Properties.OnText = "Old Classes";
            this.toggle_old_new.Size = new System.Drawing.Size(145, 18);
            this.toggle_old_new.TabIndex = 62;
            this.toggle_old_new.Toggled += new System.EventHandler(this.toggle_old_new_Toggled);
            // 
            // New_Session
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1243, 510);
            this.Controls.Add(this.gridsession);
            this.Controls.Add(this.panel3);
            this.Controls.Add(this.panel1);
            this.Name = "New_Session";
            this.Text = "New_Session";
            ((System.ComponentModel.ISupportInitialize)(this.gridsession)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).EndInit();
            this.panel3.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.toggle_old_new.Properties)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraGrid.GridControl gridsession;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView1;
        private System.Windows.Forms.Panel panel3;
        private DevExpress.XtraEditors.SimpleButton Btn_CreateSession;
        private System.Windows.Forms.Panel panel1;
        private DevExpress.XtraEditors.SimpleButton BtnPDelete;
        private DevExpress.XtraEditors.ToggleSwitch toggle_old_new;
    }
}