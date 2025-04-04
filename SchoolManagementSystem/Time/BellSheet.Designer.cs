namespace SchoolManagementSystem.Time
{
    partial class BellSheet
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(BellSheet));
            this.groupControl1 = new DevExpress.XtraEditors.GroupControl();
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl11 = new DevExpress.XtraEditors.LabelControl();
            this.txtDay = new DevExpress.XtraEditors.CheckedComboBoxEdit();
            this.BtnFind = new DevExpress.XtraEditors.SimpleButton();
            this.btnPrint = new DevExpress.XtraEditors.SimpleButton();
            this.BtnPDelete = new DevExpress.XtraEditors.SimpleButton();
            this.btnSave = new DevExpress.XtraEditors.SimpleButton();
            this.txtTime = new DevExpress.XtraEditors.TimeEdit();
            this.txtBillType = new DevExpress.XtraEditors.ComboBoxEdit();
            this.gridBill = new DevExpress.XtraGrid.GridControl();
            this.gridView1 = new DevExpress.XtraGrid.Views.Grid.GridView();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).BeginInit();
            this.groupControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtDay.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtTime.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtBillType.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridBill)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // groupControl1
            // 
            this.groupControl1.Controls.Add(this.labelControl2);
            this.groupControl1.Controls.Add(this.labelControl1);
            this.groupControl1.Controls.Add(this.labelControl11);
            this.groupControl1.Controls.Add(this.txtDay);
            this.groupControl1.Controls.Add(this.BtnFind);
            this.groupControl1.Controls.Add(this.btnPrint);
            this.groupControl1.Controls.Add(this.BtnPDelete);
            this.groupControl1.Controls.Add(this.btnSave);
            this.groupControl1.Controls.Add(this.txtTime);
            this.groupControl1.Controls.Add(this.txtBillType);
            this.groupControl1.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupControl1.Location = new System.Drawing.Point(0, 0);
            this.groupControl1.Name = "groupControl1";
            this.groupControl1.Size = new System.Drawing.Size(1076, 68);
            this.groupControl1.TabIndex = 0;
            this.groupControl1.Text = "Teacher Profile";
            // 
            // labelControl2
            // 
            this.labelControl2.Location = new System.Drawing.Point(259, 23);
            this.labelControl2.Name = "labelControl2";
            this.labelControl2.Size = new System.Drawing.Size(43, 13);
            this.labelControl2.TabIndex = 72;
            this.labelControl2.Text = "Bell Type";
            // 
            // labelControl1
            // 
            this.labelControl1.Location = new System.Drawing.Point(132, 23);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(49, 13);
            this.labelControl1.TabIndex = 71;
            this.labelControl1.Text = "Time Slate";
            // 
            // labelControl11
            // 
            this.labelControl11.Location = new System.Drawing.Point(5, 23);
            this.labelControl11.Name = "labelControl11";
            this.labelControl11.Size = new System.Drawing.Size(19, 13);
            this.labelControl11.TabIndex = 67;
            this.labelControl11.Text = "Day";
            // 
            // txtDay
            // 
            this.txtDay.EditValue = "";
            this.txtDay.Location = new System.Drawing.Point(5, 42);
            this.txtDay.Name = "txtDay";
            this.txtDay.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.txtDay.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.Standard;
            this.txtDay.Size = new System.Drawing.Size(121, 20);
            this.txtDay.TabIndex = 68;
            // 
            // BtnFind
            // 
            this.BtnFind.Enabled = false;
            this.BtnFind.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("BtnFind.ImageOptions.Image")));
            this.BtnFind.Location = new System.Drawing.Point(533, 40);
            this.BtnFind.Name = "BtnFind";
            this.BtnFind.Size = new System.Drawing.Size(85, 23);
            this.BtnFind.TabIndex = 66;
            this.BtnFind.Text = "Edit Bells";
            this.BtnFind.Click += new System.EventHandler(this.BtnFind_Click);
            // 
            // btnPrint
            // 
            this.btnPrint.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("btnPrint.ImageOptions.Image")));
            this.btnPrint.Location = new System.Drawing.Point(700, 40);
            this.btnPrint.Name = "btnPrint";
            this.btnPrint.Size = new System.Drawing.Size(67, 23);
            this.btnPrint.TabIndex = 62;
            this.btnPrint.Text = "Print";
            this.btnPrint.Visible = false;
            this.btnPrint.Click += new System.EventHandler(this.btnPrint_Click);
            // 
            // BtnPDelete
            // 
            this.BtnPDelete.Enabled = false;
            this.BtnPDelete.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("BtnPDelete.ImageOptions.Image")));
            this.BtnPDelete.Location = new System.Drawing.Point(460, 40);
            this.BtnPDelete.Name = "BtnPDelete";
            this.BtnPDelete.Size = new System.Drawing.Size(67, 23);
            this.BtnPDelete.TabIndex = 61;
            this.BtnPDelete.Text = "Delete";
            this.BtnPDelete.Click += new System.EventHandler(this.BtnPDelete_Click);
            // 
            // btnSave
            // 
            this.btnSave.Enabled = false;
            this.btnSave.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("btnSave.ImageOptions.Image")));
            this.btnSave.Location = new System.Drawing.Point(386, 40);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(68, 23);
            this.btnSave.TabIndex = 10;
            this.btnSave.Text = "Save";
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // txtTime
            // 
            this.txtTime.EditValue = null;
            this.txtTime.Location = new System.Drawing.Point(132, 42);
            this.txtTime.Name = "txtTime";
            this.txtTime.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.txtTime.Properties.EditValueChangedFiringMode = DevExpress.XtraEditors.Controls.EditValueChangedFiringMode.Default;
            this.txtTime.Properties.Mask.EditMask = "";
            this.txtTime.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.None;
            this.txtTime.Properties.TimeEditStyle = DevExpress.XtraEditors.Repository.TimeEditStyle.TouchUI;
            this.txtTime.Size = new System.Drawing.Size(121, 20);
            this.txtTime.TabIndex = 69;
            // 
            // txtBillType
            // 
            this.txtBillType.Location = new System.Drawing.Point(259, 42);
            this.txtBillType.Name = "txtBillType";
            this.txtBillType.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.txtBillType.Properties.Items.AddRange(new object[] {
            "Assembly",
            "Lecture",
            "Break",
            "Off"});
            this.txtBillType.Size = new System.Drawing.Size(121, 20);
            this.txtBillType.TabIndex = 70;
            // 
            // gridBill
            // 
            this.gridBill.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridBill.Location = new System.Drawing.Point(0, 68);
            this.gridBill.MainView = this.gridView1;
            this.gridBill.Name = "gridBill";
            this.gridBill.Size = new System.Drawing.Size(1076, 362);
            this.gridBill.TabIndex = 1;
            this.gridBill.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridView1});
            // 
            // gridView1
            // 
            this.gridView1.GridControl = this.gridBill;
            this.gridView1.Name = "gridView1";
            this.gridView1.OptionsView.ShowFooter = true;
            this.gridView1.RowUpdated += new DevExpress.XtraGrid.Views.Base.RowObjectEventHandler(this.gridView1_RowUpdated);
            // 
            // BellSheet
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.gridBill);
            this.Controls.Add(this.groupControl1);
            this.Name = "BellSheet";
            this.Size = new System.Drawing.Size(1076, 430);
            this.Enter += new System.EventHandler(this.TeacherProfile_Enter);
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).EndInit();
            this.groupControl1.ResumeLayout(false);
            this.groupControl1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtDay.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtTime.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtBillType.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridBill)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.GroupControl groupControl1;
        private DevExpress.XtraGrid.GridControl gridBill;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView1;
        private DevExpress.XtraEditors.SimpleButton btnSave;
        private DevExpress.XtraEditors.SimpleButton BtnPDelete;
        private DevExpress.XtraEditors.SimpleButton btnPrint;
        private DevExpress.XtraEditors.SimpleButton BtnFind;
        private DevExpress.XtraEditors.LabelControl labelControl11;
        private DevExpress.XtraEditors.CheckedComboBoxEdit txtDay;
        private DevExpress.XtraEditors.LabelControl labelControl2;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.XtraEditors.TimeEdit txtTime;
        private DevExpress.XtraEditors.ComboBoxEdit txtBillType;
    }
}
