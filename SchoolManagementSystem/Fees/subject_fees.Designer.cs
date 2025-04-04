namespace SchoolManagementSystem.Fees
{
    partial class subject_fees
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(subject_fees));
            this.panel1 = new System.Windows.Forms.Panel();
            this.btnIG = new DevExpress.XtraEditors.SimpleButton();
            this.labelControl5 = new DevExpress.XtraEditors.LabelControl();
            this.txtStudent = new DevExpress.XtraEditors.CheckedComboBoxEdit();
            this.labelControl4 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl3 = new DevExpress.XtraEditors.LabelControl();
            this.txtSection = new DevExpress.XtraEditors.CheckedComboBoxEdit();
            this.txtClass = new DevExpress.XtraEditors.CheckedComboBoxEdit();
            this.labelControl6 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            this.txtYearGI = new DevExpress.XtraEditors.ComboBoxEdit();
            this.txtMonthGI = new DevExpress.XtraEditors.ComboBoxEdit();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtStudent.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtSection.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtClass.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtYearGI.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtMonthGI.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.btnIG);
            this.panel1.Controls.Add(this.labelControl5);
            this.panel1.Controls.Add(this.txtStudent);
            this.panel1.Controls.Add(this.labelControl4);
            this.panel1.Controls.Add(this.labelControl3);
            this.panel1.Controls.Add(this.txtSection);
            this.panel1.Controls.Add(this.txtClass);
            this.panel1.Controls.Add(this.labelControl6);
            this.panel1.Controls.Add(this.labelControl1);
            this.panel1.Controls.Add(this.labelControl2);
            this.panel1.Controls.Add(this.txtYearGI);
            this.panel1.Controls.Add(this.txtMonthGI);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(627, 207);
            this.panel1.TabIndex = 0;
            // 
            // btnIG
            // 
            this.btnIG.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("btnIG.ImageOptions.Image")));
            this.btnIG.ImageOptions.Location = DevExpress.XtraEditors.ImageLocation.MiddleLeft;
            this.btnIG.Location = new System.Drawing.Point(92, 169);
            this.btnIG.Name = "btnIG";
            this.btnIG.Size = new System.Drawing.Size(115, 23);
            this.btnIG.TabIndex = 104;
            this.btnIG.Text = "Invoice Generate";
            this.btnIG.Click += new System.EventHandler(this.btnIG_Click);
            // 
            // labelControl5
            // 
            this.labelControl5.Location = new System.Drawing.Point(8, 146);
            this.labelControl5.Name = "labelControl5";
            this.labelControl5.Size = new System.Drawing.Size(70, 13);
            this.labelControl5.TabIndex = 102;
            this.labelControl5.Text = "Select Student";
            // 
            // txtStudent
            // 
            this.txtStudent.Location = new System.Drawing.Point(92, 143);
            this.txtStudent.Name = "txtStudent";
            this.txtStudent.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.txtStudent.Size = new System.Drawing.Size(193, 20);
            this.txtStudent.TabIndex = 101;
            // 
            // labelControl4
            // 
            this.labelControl4.Location = new System.Drawing.Point(11, 119);
            this.labelControl4.Name = "labelControl4";
            this.labelControl4.Size = new System.Drawing.Size(69, 13);
            this.labelControl4.TabIndex = 100;
            this.labelControl4.Text = "Select Section";
            // 
            // labelControl3
            // 
            this.labelControl3.Location = new System.Drawing.Point(21, 87);
            this.labelControl3.Name = "labelControl3";
            this.labelControl3.Size = new System.Drawing.Size(58, 13);
            this.labelControl3.TabIndex = 99;
            this.labelControl3.Text = "Select Class";
            // 
            // txtSection
            // 
            this.txtSection.Location = new System.Drawing.Point(92, 117);
            this.txtSection.Name = "txtSection";
            this.txtSection.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.txtSection.Size = new System.Drawing.Size(131, 20);
            this.txtSection.TabIndex = 98;
            this.txtSection.EditValueChanged += new System.EventHandler(this.txtSection_EditValueChanged);
            // 
            // txtClass
            // 
            this.txtClass.Location = new System.Drawing.Point(92, 91);
            this.txtClass.Name = "txtClass";
            this.txtClass.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.txtClass.Size = new System.Drawing.Size(131, 20);
            this.txtClass.TabIndex = 97;
            this.txtClass.EditValueChanged += new System.EventHandler(this.txtClass_EditValueChanged);
            // 
            // labelControl6
            // 
            this.labelControl6.Appearance.Font = new System.Drawing.Font("Tahoma", 14F, System.Drawing.FontStyle.Bold);
            this.labelControl6.Appearance.Options.UseFont = true;
            this.labelControl6.Location = new System.Drawing.Point(66, 3);
            this.labelControl6.Name = "labelControl6";
            this.labelControl6.Size = new System.Drawing.Size(141, 23);
            this.labelControl6.TabIndex = 95;
            this.labelControl6.Text = "Fee By Subject";
            // 
            // labelControl1
            // 
            this.labelControl1.Location = new System.Drawing.Point(56, 44);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(22, 13);
            this.labelControl1.TabIndex = 95;
            this.labelControl1.Text = "Year";
            // 
            // labelControl2
            // 
            this.labelControl2.Location = new System.Drawing.Point(48, 68);
            this.labelControl2.Name = "labelControl2";
            this.labelControl2.Size = new System.Drawing.Size(30, 13);
            this.labelControl2.TabIndex = 94;
            this.labelControl2.Text = "Month";
            // 
            // txtYearGI
            // 
            this.txtYearGI.Location = new System.Drawing.Point(92, 41);
            this.txtYearGI.Name = "txtYearGI";
            this.txtYearGI.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.txtYearGI.Properties.DisplayFormat.FormatString = "d";
            this.txtYearGI.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.txtYearGI.Properties.EditFormat.FormatString = "d";
            this.txtYearGI.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.txtYearGI.Properties.Items.AddRange(new object[] {
            "2015",
            "2016",
            "2017",
            "2018",
            "2019",
            "2020",
            "2021",
            "2022",
            "2023",
            "2024",
            "2025",
            "2026",
            "2027",
            "2028",
            "2029",
            "2030"});
            this.txtYearGI.Size = new System.Drawing.Size(100, 20);
            this.txtYearGI.TabIndex = 96;
            // 
            // txtMonthGI
            // 
            this.txtMonthGI.Location = new System.Drawing.Point(92, 65);
            this.txtMonthGI.Name = "txtMonthGI";
            this.txtMonthGI.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.txtMonthGI.Size = new System.Drawing.Size(100, 20);
            this.txtMonthGI.TabIndex = 103;
            // 
            // subject_fees
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(627, 207);
            this.Controls.Add(this.panel1);
            this.Name = "subject_fees";
            this.Text = "subject_fees";
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtStudent.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtSection.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtClass.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtYearGI.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtMonthGI.Properties)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private DevExpress.XtraEditors.LabelControl labelControl5;
        private DevExpress.XtraEditors.CheckedComboBoxEdit txtStudent;
        private DevExpress.XtraEditors.LabelControl labelControl4;
        private DevExpress.XtraEditors.LabelControl labelControl3;
        private DevExpress.XtraEditors.CheckedComboBoxEdit txtSection;
        private DevExpress.XtraEditors.CheckedComboBoxEdit txtClass;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.XtraEditors.LabelControl labelControl2;
        private DevExpress.XtraEditors.ComboBoxEdit txtYearGI;
        private DevExpress.XtraEditors.SimpleButton btnIG;
        private DevExpress.XtraEditors.LabelControl labelControl6;
        private DevExpress.XtraEditors.ComboBoxEdit txtMonthGI;
    }
}