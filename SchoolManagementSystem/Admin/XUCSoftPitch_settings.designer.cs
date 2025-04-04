namespace SchoolManagementSystem.Admin
{
    partial class XUCSoftPitch_settings
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(XUCSoftPitch_settings));
            this.repositoryItemMemoEdit1 = new DevExpress.XtraEditors.Repository.RepositoryItemMemoEdit();
            this.SttingsgridControl = new DevExpress.XtraGrid.GridControl();
            this.gridView39 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.gridColumn3 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn4 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.GENSet_groupControl = new DevExpress.XtraEditors.GroupControl();
            this.panel1 = new System.Windows.Forms.Panel();
            this.pictureEdit1 = new DevExpress.XtraEditors.PictureEdit();
            this.BtnSelectImageLogo = new DevExpress.XtraEditors.SimpleButton();
            this.BtnUploadLogo = new DevExpress.XtraEditors.SimpleButton();
            this.panel38 = new System.Windows.Forms.Panel();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.rdbpartnar_yes = new System.Windows.Forms.RadioButton();
            this.rdbpartnar_no = new System.Windows.Forms.RadioButton();
            this.label_keyinfo_np = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemMemoEdit1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.SttingsgridControl)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView39)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.GENSet_groupControl)).BeginInit();
            this.GENSet_groupControl.SuspendLayout();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureEdit1.Properties)).BeginInit();
            this.panel38.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // repositoryItemMemoEdit1
            // 
            this.repositoryItemMemoEdit1.Name = "repositoryItemMemoEdit1";
            // 
            // SttingsgridControl
            // 
            this.SttingsgridControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.SttingsgridControl.Location = new System.Drawing.Point(2, 23);
            this.SttingsgridControl.MainView = this.gridView39;
            this.SttingsgridControl.Name = "SttingsgridControl";
            this.SttingsgridControl.Size = new System.Drawing.Size(874, 627);
            this.SttingsgridControl.TabIndex = 3;
            this.SttingsgridControl.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridView39});
            // 
            // gridView39
            // 
            this.gridView39.Appearance.HeaderPanel.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold);
            this.gridView39.Appearance.HeaderPanel.ForeColor = System.Drawing.SystemColors.HotTrack;
            this.gridView39.Appearance.HeaderPanel.Options.UseFont = true;
            this.gridView39.Appearance.HeaderPanel.Options.UseForeColor = true;
            this.gridView39.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.gridColumn3,
            this.gridColumn4});
            this.gridView39.GridControl = this.SttingsgridControl;
            this.gridView39.Name = "gridView39";
            this.gridView39.OptionsBehavior.EditingMode = DevExpress.XtraGrid.Views.Grid.GridEditingMode.Inplace;
            this.gridView39.OptionsBehavior.EditorShowMode = DevExpress.Utils.EditorShowMode.Click;
            this.gridView39.OptionsView.RowAutoHeight = true;
            this.gridView39.RowStyle += new DevExpress.XtraGrid.Views.Grid.RowStyleEventHandler(this.gridView39_RowStyle);
            this.gridView39.RowUpdated += new DevExpress.XtraGrid.Views.Base.RowObjectEventHandler(this.gridView39_RowUpdated);
            // 
            // gridColumn3
            // 
            this.gridColumn3.FieldName = "type";
            this.gridColumn3.Name = "gridColumn3";
            this.gridColumn3.Visible = true;
            this.gridColumn3.VisibleIndex = 0;
            this.gridColumn3.Width = 163;
            // 
            // gridColumn4
            // 
            this.gridColumn4.ColumnEdit = this.repositoryItemMemoEdit1;
            this.gridColumn4.FieldName = "description";
            this.gridColumn4.Name = "gridColumn4";
            this.gridColumn4.Visible = true;
            this.gridColumn4.VisibleIndex = 1;
            this.gridColumn4.Width = 1054;
            // 
            // GENSet_groupControl
            // 
            this.GENSet_groupControl.AppearanceCaption.Font = new System.Drawing.Font("Times New Roman", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.GENSet_groupControl.AppearanceCaption.Options.UseFont = true;
            this.GENSet_groupControl.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple;
            this.GENSet_groupControl.CaptionImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("GENSet_groupControl.CaptionImageOptions.Image")));
            this.GENSet_groupControl.Controls.Add(this.SttingsgridControl);
            this.GENSet_groupControl.Controls.Add(this.panel1);
            this.GENSet_groupControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.GENSet_groupControl.Location = new System.Drawing.Point(0, 0);
            this.GENSet_groupControl.Name = "GENSet_groupControl";
            this.GENSet_groupControl.Size = new System.Drawing.Size(1237, 652);
            this.GENSet_groupControl.TabIndex = 28;
            this.GENSet_groupControl.Text = "General Settings";
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.label_keyinfo_np);
            this.panel1.Controls.Add(this.pictureEdit1);
            this.panel1.Controls.Add(this.BtnSelectImageLogo);
            this.panel1.Controls.Add(this.BtnUploadLogo);
            this.panel1.Controls.Add(this.panel38);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Right;
            this.panel1.Location = new System.Drawing.Point(876, 23);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(359, 627);
            this.panel1.TabIndex = 30;
            // 
            // pictureEdit1
            // 
            this.pictureEdit1.Cursor = System.Windows.Forms.Cursors.Default;
            this.pictureEdit1.Location = new System.Drawing.Point(9, 81);
            this.pictureEdit1.Name = "pictureEdit1";
            this.pictureEdit1.Properties.ShowCameraMenuItem = DevExpress.XtraEditors.Controls.CameraMenuItemVisibility.Auto;
            this.pictureEdit1.Properties.SizeMode = DevExpress.XtraEditors.Controls.PictureSizeMode.Stretch;
            this.pictureEdit1.Size = new System.Drawing.Size(158, 135);
            this.pictureEdit1.TabIndex = 30;
            // 
            // BtnSelectImageLogo
            // 
            this.BtnSelectImageLogo.Location = new System.Drawing.Point(9, 222);
            this.BtnSelectImageLogo.Name = "BtnSelectImageLogo";
            this.BtnSelectImageLogo.Size = new System.Drawing.Size(158, 23);
            this.BtnSelectImageLogo.TabIndex = 31;
            this.BtnSelectImageLogo.Text = "Select Image";
            this.BtnSelectImageLogo.Click += new System.EventHandler(this.BtnSelectImage_Click);
            // 
            // BtnUploadLogo
            // 
            this.BtnUploadLogo.Enabled = false;
            this.BtnUploadLogo.Location = new System.Drawing.Point(9, 251);
            this.BtnUploadLogo.Name = "BtnUploadLogo";
            this.BtnUploadLogo.Size = new System.Drawing.Size(90, 23);
            this.BtnUploadLogo.TabIndex = 32;
            this.BtnUploadLogo.Text = "upload";
            this.BtnUploadLogo.Click += new System.EventHandler(this.BtnUpload_Click);
            // 
            // panel38
            // 
            this.panel38.Controls.Add(this.comboBox1);
            this.panel38.Controls.Add(this.label1);
            this.panel38.Controls.Add(this.groupBox1);
            this.panel38.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel38.Location = new System.Drawing.Point(0, 0);
            this.panel38.Name = "panel38";
            this.panel38.Size = new System.Drawing.Size(359, 78);
            this.panel38.TabIndex = 29;
            // 
            // comboBox1
            // 
            this.comboBox1.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Items.AddRange(new object[] {
            "Subject Wise Institute",
            "Institute"});
            this.comboBox1.Location = new System.Drawing.Point(103, 24);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(121, 21);
            this.comboBox1.TabIndex = 10;
            this.comboBox1.SelectedIndexChanged += new System.EventHandler(this.comboBox1_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(103, 4);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(75, 13);
            this.label1.TabIndex = 9;
            this.label1.Text = "Institute Type";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.rdbpartnar_yes);
            this.groupBox1.Controls.Add(this.rdbpartnar_no);
            this.groupBox1.Location = new System.Drawing.Point(9, 1);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(88, 74);
            this.groupBox1.TabIndex = 8;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Show Partnar";
            // 
            // rdbpartnar_yes
            // 
            this.rdbpartnar_yes.AutoSize = true;
            this.rdbpartnar_yes.Location = new System.Drawing.Point(6, 20);
            this.rdbpartnar_yes.Name = "rdbpartnar_yes";
            this.rdbpartnar_yes.Size = new System.Drawing.Size(42, 17);
            this.rdbpartnar_yes.TabIndex = 4;
            this.rdbpartnar_yes.TabStop = true;
            this.rdbpartnar_yes.Text = "Yes";
            this.rdbpartnar_yes.UseVisualStyleBackColor = true;
            this.rdbpartnar_yes.CheckedChanged += new System.EventHandler(this.rdbsmallprinter_CheckedChanged);
            // 
            // rdbpartnar_no
            // 
            this.rdbpartnar_no.AutoSize = true;
            this.rdbpartnar_no.Location = new System.Drawing.Point(6, 43);
            this.rdbpartnar_no.Name = "rdbpartnar_no";
            this.rdbpartnar_no.Size = new System.Drawing.Size(38, 17);
            this.rdbpartnar_no.TabIndex = 7;
            this.rdbpartnar_no.TabStop = true;
            this.rdbpartnar_no.Text = "No";
            this.rdbpartnar_no.UseVisualStyleBackColor = true;
            this.rdbpartnar_no.CheckedChanged += new System.EventHandler(this.rdbsmallprinter_CheckedChanged);
            // 
            // label_keyinfo_np
            // 
            this.label_keyinfo_np.AllowDrop = true;
            this.label_keyinfo_np.BackColor = System.Drawing.Color.White;
            this.label_keyinfo_np.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label_keyinfo_np.ForeColor = System.Drawing.Color.Black;
            this.label_keyinfo_np.Location = new System.Drawing.Point(5, 289);
            this.label_keyinfo_np.Name = "label_keyinfo_np";
            this.label_keyinfo_np.Size = new System.Drawing.Size(346, 145);
            this.label_keyinfo_np.TabIndex = 33;
            this.label_keyinfo_np.Text = "About Software";
            // 
            // XUCSoftPitch_settings
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.GENSet_groupControl);
            this.Name = "XUCSoftPitch_settings";
            this.Size = new System.Drawing.Size(1237, 652);
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemMemoEdit1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.SttingsgridControl)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView39)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.GENSet_groupControl)).EndInit();
            this.GENSet_groupControl.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureEdit1.Properties)).EndInit();
            this.panel38.ResumeLayout(false);
            this.panel38.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraGrid.GridControl SttingsgridControl;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView39;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn3;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn4;
        private DevExpress.XtraEditors.Repository.RepositoryItemMemoEdit repositoryItemMemoEdit1;
        private DevExpress.XtraEditors.GroupControl GENSet_groupControl;
        private System.Windows.Forms.Panel panel38;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.RadioButton rdbpartnar_yes;
        private System.Windows.Forms.RadioButton rdbpartnar_no;
        private System.Windows.Forms.Panel panel1;
        private DevExpress.XtraEditors.PictureEdit pictureEdit1;
        private DevExpress.XtraEditors.SimpleButton BtnSelectImageLogo;
        private DevExpress.XtraEditors.SimpleButton BtnUploadLogo;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox comboBox1;
        private System.Windows.Forms.Label label_keyinfo_np;
    }
}
