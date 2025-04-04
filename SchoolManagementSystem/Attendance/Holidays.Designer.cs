
namespace SchoolManagementSystem.Attendance
{
    partial class Holidays
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Holidays));
            this.panel1 = new System.Windows.Forms.Panel();
            this.btn_delete = new DevExpress.XtraEditors.SimpleButton();
            this.btn_edit = new DevExpress.XtraEditors.SimpleButton();
            this.btn_save = new DevExpress.XtraEditors.SimpleButton();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.txt_holiday_title = new System.Windows.Forms.TextBox();
            this.labelControl3 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl9 = new DevExpress.XtraEditors.LabelControl();
            this.label2 = new System.Windows.Forms.Label();
            this.dtp_to_date = new System.Windows.Forms.DateTimePicker();
            this.label1 = new System.Windows.Forms.Label();
            this.dtp_from_date = new System.Windows.Forms.DateTimePicker();
            this.txtClass = new DevExpress.XtraEditors.CheckedComboBoxEdit();
            this.txtSection = new DevExpress.XtraEditors.CheckedComboBoxEdit();
            this.grid_leaves = new DevExpress.XtraGrid.GridControl();
            this.gridView1 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtClass.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtSection.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grid_leaves)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.btn_delete);
            this.panel1.Controls.Add(this.btn_edit);
            this.panel1.Controls.Add(this.btn_save);
            this.panel1.Controls.Add(this.labelControl1);
            this.panel1.Controls.Add(this.txt_holiday_title);
            this.panel1.Controls.Add(this.labelControl3);
            this.panel1.Controls.Add(this.labelControl9);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.dtp_to_date);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.dtp_from_date);
            this.panel1.Controls.Add(this.txtClass);
            this.panel1.Controls.Add(this.txtSection);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1070, 48);
            this.panel1.TabIndex = 0;
            // 
            // btn_delete
            // 
            this.btn_delete.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("btn_delete.ImageOptions.Image")));
            this.btn_delete.Location = new System.Drawing.Point(944, 19);
            this.btn_delete.Name = "btn_delete";
            this.btn_delete.Size = new System.Drawing.Size(75, 23);
            this.btn_delete.TabIndex = 105;
            this.btn_delete.Text = "Delete";
            this.btn_delete.Click += new System.EventHandler(this.btn_delete_Click);
            // 
            // btn_edit
            // 
            this.btn_edit.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("btn_edit.ImageOptions.Image")));
            this.btn_edit.Location = new System.Drawing.Point(863, 19);
            this.btn_edit.Name = "btn_edit";
            this.btn_edit.Size = new System.Drawing.Size(75, 23);
            this.btn_edit.TabIndex = 105;
            this.btn_edit.Text = "Edit";
            this.btn_edit.Click += new System.EventHandler(this.btn_edit_Click);
            // 
            // btn_save
            // 
            this.btn_save.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("btn_save.ImageOptions.Image")));
            this.btn_save.Location = new System.Drawing.Point(782, 18);
            this.btn_save.Name = "btn_save";
            this.btn_save.Size = new System.Drawing.Size(75, 23);
            this.btn_save.TabIndex = 105;
            this.btn_save.Text = "Save";
            this.btn_save.Click += new System.EventHandler(this.btn_save_Click);
            // 
            // labelControl1
            // 
            this.labelControl1.Location = new System.Drawing.Point(579, 2);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(58, 13);
            this.labelControl1.TabIndex = 103;
            this.labelControl1.Text = "Title Holiday";
            // 
            // txt_holiday_title
            // 
            this.txt_holiday_title.Location = new System.Drawing.Point(579, 21);
            this.txt_holiday_title.Name = "txt_holiday_title";
            this.txt_holiday_title.Size = new System.Drawing.Size(197, 20);
            this.txt_holiday_title.TabIndex = 102;
            // 
            // labelControl3
            // 
            this.labelControl3.Location = new System.Drawing.Point(423, 2);
            this.labelControl3.Name = "labelControl3";
            this.labelControl3.Size = new System.Drawing.Size(36, 13);
            this.labelControl3.TabIndex = 100;
            this.labelControl3.Text = "Section";
            // 
            // labelControl9
            // 
            this.labelControl9.Location = new System.Drawing.Point(267, 2);
            this.labelControl9.Name = "labelControl9";
            this.labelControl9.Size = new System.Drawing.Size(25, 13);
            this.labelControl9.TabIndex = 98;
            this.labelControl9.Text = "Class";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(132, 3);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(46, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "To Date";
            // 
            // dtp_to_date
            // 
            this.dtp_to_date.CustomFormat = "dd-MMM-yyyy";
            this.dtp_to_date.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtp_to_date.Location = new System.Drawing.Point(135, 21);
            this.dtp_to_date.Name = "dtp_to_date";
            this.dtp_to_date.Size = new System.Drawing.Size(126, 20);
            this.dtp_to_date.TabIndex = 2;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(3, 3);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(56, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "From Date";
            // 
            // dtp_from_date
            // 
            this.dtp_from_date.CustomFormat = "dd-MMM-yyyy";
            this.dtp_from_date.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtp_from_date.Location = new System.Drawing.Point(6, 21);
            this.dtp_from_date.Name = "dtp_from_date";
            this.dtp_from_date.Size = new System.Drawing.Size(126, 20);
            this.dtp_from_date.TabIndex = 0;
            this.dtp_from_date.ValueChanged += new System.EventHandler(this.dtp_from_date_ValueChanged);
            // 
            // txtClass
            // 
            this.txtClass.Location = new System.Drawing.Point(267, 21);
            this.txtClass.Name = "txtClass";
            this.txtClass.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.txtClass.Size = new System.Drawing.Size(150, 20);
            this.txtClass.TabIndex = 99;
            this.txtClass.EditValueChanged += new System.EventHandler(this.txtClass_EditValueChanged);
            // 
            // txtSection
            // 
            this.txtSection.Location = new System.Drawing.Point(423, 21);
            this.txtSection.Name = "txtSection";
            this.txtSection.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.txtSection.Size = new System.Drawing.Size(150, 20);
            this.txtSection.TabIndex = 101;
            // 
            // grid_leaves
            // 
            this.grid_leaves.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grid_leaves.Location = new System.Drawing.Point(0, 48);
            this.grid_leaves.MainView = this.gridView1;
            this.grid_leaves.Name = "grid_leaves";
            this.grid_leaves.Size = new System.Drawing.Size(1070, 441);
            this.grid_leaves.TabIndex = 10;
            this.grid_leaves.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridView1});
            // 
            // gridView1
            // 
            this.gridView1.GridControl = this.grid_leaves;
            this.gridView1.Name = "gridView1";
            this.gridView1.OptionsBehavior.Editable = false;
            this.gridView1.OptionsBehavior.ReadOnly = true;
            this.gridView1.OptionsFind.AlwaysVisible = true;
            this.gridView1.RowUpdated += new DevExpress.XtraGrid.Views.Base.RowObjectEventHandler(this.gridView1_RowUpdated);
            // 
            // Holidays
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.grid_leaves);
            this.Controls.Add(this.panel1);
            this.Name = "Holidays";
            this.Size = new System.Drawing.Size(1070, 489);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtClass.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtSection.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grid_leaves)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DateTimePicker dtp_from_date;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.DateTimePicker dtp_to_date;
        private DevExpress.XtraEditors.LabelControl labelControl3;
        private DevExpress.XtraEditors.LabelControl labelControl9;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        private System.Windows.Forms.TextBox txt_holiday_title;
        private DevExpress.XtraGrid.GridControl grid_leaves;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView1;
        private DevExpress.XtraEditors.CheckedComboBoxEdit txtClass;
        private DevExpress.XtraEditors.CheckedComboBoxEdit txtSection;
        private DevExpress.XtraEditors.SimpleButton btn_delete;
        private DevExpress.XtraEditors.SimpleButton btn_save;
        private DevExpress.XtraEditors.SimpleButton btn_edit;
    }
}
