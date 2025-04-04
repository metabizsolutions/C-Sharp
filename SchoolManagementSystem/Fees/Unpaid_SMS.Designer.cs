namespace SchoolManagementSystem.Fees
{
    partial class Unpaid_SMS
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Unpaid_SMS));
            this.groupControlUS = new DevExpress.XtraEditors.GroupControl();
            this.dtp_month = new System.Windows.Forms.DateTimePicker();
            this.labelControl5 = new DevExpress.XtraEditors.LabelControl();
            this.txtStudent = new DevExpress.XtraEditors.CheckedComboBoxEdit();
            this.labelControl4 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl3 = new DevExpress.XtraEditors.LabelControl();
            this.txtSection = new DevExpress.XtraEditors.CheckedComboBoxEdit();
            this.txtClass = new DevExpress.XtraEditors.CheckedComboBoxEdit();
            this.cbo_smstype = new System.Windows.Forms.ComboBox();
            this.btnSend = new DevExpress.XtraEditors.SimpleButton();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl10 = new DevExpress.XtraEditors.LabelControl();
            this.gridControl1 = new DevExpress.XtraGrid.GridControl();
            this.gridView1 = new DevExpress.XtraGrid.Views.Grid.GridView();
            ((System.ComponentModel.ISupportInitialize)(this.groupControlUS)).BeginInit();
            this.groupControlUS.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtStudent.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtSection.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtClass.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridControl1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // groupControlUS
            // 
            this.groupControlUS.Controls.Add(this.dtp_month);
            this.groupControlUS.Controls.Add(this.labelControl5);
            this.groupControlUS.Controls.Add(this.txtStudent);
            this.groupControlUS.Controls.Add(this.labelControl4);
            this.groupControlUS.Controls.Add(this.labelControl3);
            this.groupControlUS.Controls.Add(this.txtSection);
            this.groupControlUS.Controls.Add(this.txtClass);
            this.groupControlUS.Controls.Add(this.cbo_smstype);
            this.groupControlUS.Controls.Add(this.btnSend);
            this.groupControlUS.Controls.Add(this.labelControl1);
            this.groupControlUS.Controls.Add(this.labelControl10);
            this.groupControlUS.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupControlUS.Location = new System.Drawing.Point(0, 0);
            this.groupControlUS.Name = "groupControlUS";
            this.groupControlUS.Size = new System.Drawing.Size(1225, 68);
            this.groupControlUS.TabIndex = 9;
            this.groupControlUS.Text = "Send Unpaid SMS";
            // 
            // dtp_month
            // 
            this.dtp_month.CustomFormat = "MMM-yyyy";
            this.dtp_month.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtp_month.Location = new System.Drawing.Point(5, 41);
            this.dtp_month.Name = "dtp_month";
            this.dtp_month.Size = new System.Drawing.Size(107, 20);
            this.dtp_month.TabIndex = 97;
            // 
            // labelControl5
            // 
            this.labelControl5.Location = new System.Drawing.Point(436, 22);
            this.labelControl5.Name = "labelControl5";
            this.labelControl5.Size = new System.Drawing.Size(70, 13);
            this.labelControl5.TabIndex = 95;
            this.labelControl5.Text = "Select Student";
            // 
            // txtStudent
            // 
            this.txtStudent.Location = new System.Drawing.Point(436, 41);
            this.txtStudent.Name = "txtStudent";
            this.txtStudent.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.txtStudent.Size = new System.Drawing.Size(172, 20);
            this.txtStudent.TabIndex = 94;
            // 
            // labelControl4
            // 
            this.labelControl4.Location = new System.Drawing.Point(330, 22);
            this.labelControl4.Name = "labelControl4";
            this.labelControl4.Size = new System.Drawing.Size(69, 13);
            this.labelControl4.TabIndex = 93;
            this.labelControl4.Text = "Select Section";
            // 
            // labelControl3
            // 
            this.labelControl3.Location = new System.Drawing.Point(193, 22);
            this.labelControl3.Name = "labelControl3";
            this.labelControl3.Size = new System.Drawing.Size(58, 13);
            this.labelControl3.TabIndex = 92;
            this.labelControl3.Text = "Select Class";
            // 
            // txtSection
            // 
            this.txtSection.Location = new System.Drawing.Point(330, 41);
            this.txtSection.Name = "txtSection";
            this.txtSection.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.txtSection.Size = new System.Drawing.Size(100, 20);
            this.txtSection.TabIndex = 91;
            this.txtSection.EditValueChanged += new System.EventHandler(this.txtSection_EditValueChanged);
            // 
            // txtClass
            // 
            this.txtClass.Location = new System.Drawing.Point(193, 41);
            this.txtClass.Name = "txtClass";
            this.txtClass.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.txtClass.Size = new System.Drawing.Size(131, 20);
            this.txtClass.TabIndex = 90;
            this.txtClass.EditValueChanged += new System.EventHandler(this.txtClass_EditValueChanged);
            // 
            // cbo_smstype
            // 
            this.cbo_smstype.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbo_smstype.FormattingEnabled = true;
            this.cbo_smstype.Items.AddRange(new object[] {
            "All",
            "UnPaid",
            "Half Paid"});
            this.cbo_smstype.Location = new System.Drawing.Point(117, 41);
            this.cbo_smstype.Name = "cbo_smstype";
            this.cbo_smstype.Size = new System.Drawing.Size(70, 21);
            this.cbo_smstype.TabIndex = 78;
            // 
            // btnSend
            // 
            this.btnSend.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("btnSend.ImageOptions.Image")));
            this.btnSend.ImageOptions.Location = DevExpress.XtraEditors.ImageLocation.MiddleLeft;
            this.btnSend.Location = new System.Drawing.Point(614, 39);
            this.btnSend.Name = "btnSend";
            this.btnSend.Size = new System.Drawing.Size(115, 23);
            this.btnSend.TabIndex = 77;
            this.btnSend.Text = "Send ";
            this.btnSend.Click += new System.EventHandler(this.btnSend_Click);
            // 
            // labelControl1
            // 
            this.labelControl1.Location = new System.Drawing.Point(117, 23);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(53, 13);
            this.labelControl1.TabIndex = 74;
            this.labelControl1.Text = "Select type";
            // 
            // labelControl10
            // 
            this.labelControl10.Location = new System.Drawing.Point(5, 23);
            this.labelControl10.Name = "labelControl10";
            this.labelControl10.Size = new System.Drawing.Size(30, 13);
            this.labelControl10.TabIndex = 72;
            this.labelControl10.Text = "Month";
            // 
            // gridControl1
            // 
            this.gridControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridControl1.Location = new System.Drawing.Point(0, 68);
            this.gridControl1.MainView = this.gridView1;
            this.gridControl1.Name = "gridControl1";
            this.gridControl1.Size = new System.Drawing.Size(1225, 440);
            this.gridControl1.TabIndex = 10;
            this.gridControl1.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridView1});
            // 
            // gridView1
            // 
            this.gridView1.GridControl = this.gridControl1;
            this.gridView1.Name = "gridView1";
            // 
            // Unpaid_SMS
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.gridControl1);
            this.Controls.Add(this.groupControlUS);
            this.Name = "Unpaid_SMS";
            this.Size = new System.Drawing.Size(1225, 508);
            ((System.ComponentModel.ISupportInitialize)(this.groupControlUS)).EndInit();
            this.groupControlUS.ResumeLayout(false);
            this.groupControlUS.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtStudent.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtSection.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtClass.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridControl1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.GroupControl groupControlUS;
        private DevExpress.XtraEditors.SimpleButton btnSend;
        private DevExpress.XtraEditors.LabelControl labelControl10;
        private System.Windows.Forms.ComboBox cbo_smstype;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.XtraGrid.GridControl gridControl1;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView1;
        private DevExpress.XtraEditors.LabelControl labelControl5;
        private DevExpress.XtraEditors.CheckedComboBoxEdit txtStudent;
        private DevExpress.XtraEditors.LabelControl labelControl4;
        private DevExpress.XtraEditors.LabelControl labelControl3;
        private DevExpress.XtraEditors.CheckedComboBoxEdit txtSection;
        private DevExpress.XtraEditors.CheckedComboBoxEdit txtClass;
        private System.Windows.Forms.DateTimePicker dtp_month;
    }
}
