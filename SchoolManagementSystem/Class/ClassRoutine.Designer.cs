namespace SchoolManagementSystem.Class
{
    partial class ClassRoutine
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ClassRoutine));
            this.gridClassRoutine = new DevExpress.XtraGrid.GridControl();
            this.gridView1 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.groupControl1 = new DevExpress.XtraEditors.GroupControl();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.btnCrDelete = new DevExpress.XtraEditors.SimpleButton();
            this.btnCrAdd = new DevExpress.XtraEditors.SimpleButton();
            this.labelControl11 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl9 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl8 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl7 = new DevExpress.XtraEditors.LabelControl();
            this.txtClass = new DevExpress.XtraEditors.ComboBoxEdit();
            this.txtDay = new DevExpress.XtraEditors.ComboBoxEdit();
            this.txtSubject = new DevExpress.XtraEditors.ComboBoxEdit();
            this.txtsTime = new DevExpress.XtraEditors.TimeEdit();
            this.txteTime = new DevExpress.XtraEditors.TimeEdit();
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            this.txtSection = new DevExpress.XtraEditors.ComboBoxEdit();
            ((System.ComponentModel.ISupportInitialize)(this.gridClassRoutine)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).BeginInit();
            this.groupControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtClass.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtDay.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtSubject.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtsTime.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txteTime.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtSection.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // gridClassRoutine
            // 
            this.gridClassRoutine.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridClassRoutine.Location = new System.Drawing.Point(0, 72);
            this.gridClassRoutine.MainView = this.gridView1;
            this.gridClassRoutine.Name = "gridClassRoutine";
            this.gridClassRoutine.Size = new System.Drawing.Size(1055, 407);
            this.gridClassRoutine.TabIndex = 5;
            this.gridClassRoutine.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridView1});
            // 
            // gridView1
            // 
            this.gridView1.GridControl = this.gridClassRoutine;
            this.gridView1.Name = "gridView1";
            this.gridView1.OptionsFind.AlwaysVisible = true;
            this.gridView1.CustomRowCellEditForEditing += new DevExpress.XtraGrid.Views.Grid.CustomRowCellEditEventHandler(this.gridView1_CustomRowCellEditForEditing);
            this.gridView1.RowUpdated += new DevExpress.XtraGrid.Views.Base.RowObjectEventHandler(this.gridView1_RowUpdated);
            // 
            // groupControl1
            // 
            this.groupControl1.Controls.Add(this.labelControl2);
            this.groupControl1.Controls.Add(this.txtSection);
            this.groupControl1.Controls.Add(this.labelControl1);
            this.groupControl1.Controls.Add(this.btnCrDelete);
            this.groupControl1.Controls.Add(this.btnCrAdd);
            this.groupControl1.Controls.Add(this.labelControl11);
            this.groupControl1.Controls.Add(this.labelControl9);
            this.groupControl1.Controls.Add(this.labelControl8);
            this.groupControl1.Controls.Add(this.labelControl7);
            this.groupControl1.Controls.Add(this.txtClass);
            this.groupControl1.Controls.Add(this.txtDay);
            this.groupControl1.Controls.Add(this.txtSubject);
            this.groupControl1.Controls.Add(this.txtsTime);
            this.groupControl1.Controls.Add(this.txteTime);
            this.groupControl1.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupControl1.Location = new System.Drawing.Point(0, 0);
            this.groupControl1.Name = "groupControl1";
            this.groupControl1.Size = new System.Drawing.Size(1055, 72);
            this.groupControl1.TabIndex = 4;
            this.groupControl1.Text = "Add Class Routine";
            // 
            // labelControl1
            // 
            this.labelControl1.Location = new System.Drawing.Point(785, 23);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(57, 13);
            this.labelControl1.TabIndex = 61;
            this.labelControl1.Text = "Ending Time";
            // 
            // btnCrDelete
            // 
            this.btnCrDelete.Image = ((System.Drawing.Image)(resources.GetObject("btnCrDelete.Image")));
            this.btnCrDelete.Location = new System.Drawing.Point(1014, 40);
            this.btnCrDelete.Name = "btnCrDelete";
            this.btnCrDelete.Size = new System.Drawing.Size(67, 23);
            this.btnCrDelete.TabIndex = 60;
            this.btnCrDelete.Text = "Delete";
            this.btnCrDelete.Click += new System.EventHandler(this.btnCrDelete_Click);
            // 
            // btnCrAdd
            // 
            this.btnCrAdd.Image = ((System.Drawing.Image)(resources.GetObject("btnCrAdd.Image")));
            this.btnCrAdd.Location = new System.Drawing.Point(941, 40);
            this.btnCrAdd.Name = "btnCrAdd";
            this.btnCrAdd.Size = new System.Drawing.Size(67, 23);
            this.btnCrAdd.TabIndex = 59;
            this.btnCrAdd.Text = "Add";
            this.btnCrAdd.Click += new System.EventHandler(this.btnCrAdd_Click);
            // 
            // labelControl11
            // 
            this.labelControl11.Location = new System.Drawing.Point(629, 23);
            this.labelControl11.Name = "labelControl11";
            this.labelControl11.Size = new System.Drawing.Size(63, 13);
            this.labelControl11.TabIndex = 57;
            this.labelControl11.Text = "Starting Time";
            // 
            // labelControl9
            // 
            this.labelControl9.Location = new System.Drawing.Point(161, 23);
            this.labelControl9.Name = "labelControl9";
            this.labelControl9.Size = new System.Drawing.Size(25, 13);
            this.labelControl9.TabIndex = 53;
            this.labelControl9.Text = "Class";
            // 
            // labelControl8
            // 
            this.labelControl8.Location = new System.Drawing.Point(473, 23);
            this.labelControl8.Name = "labelControl8";
            this.labelControl8.Size = new System.Drawing.Size(36, 13);
            this.labelControl8.TabIndex = 51;
            this.labelControl8.Text = "Subject";
            // 
            // labelControl7
            // 
            this.labelControl7.Location = new System.Drawing.Point(5, 23);
            this.labelControl7.Name = "labelControl7";
            this.labelControl7.Size = new System.Drawing.Size(19, 13);
            this.labelControl7.TabIndex = 49;
            this.labelControl7.Text = "Day";
            // 
            // txtClass
            // 
            this.txtClass.Location = new System.Drawing.Point(161, 42);
            this.txtClass.Name = "txtClass";
            this.txtClass.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.txtClass.Size = new System.Drawing.Size(150, 20);
            this.txtClass.TabIndex = 54;
            this.txtClass.SelectedIndexChanged += new System.EventHandler(this.txtClass_SelectedIndexChanged);
            // 
            // txtDay
            // 
            this.txtDay.Location = new System.Drawing.Point(5, 42);
            this.txtDay.Name = "txtDay";
            this.txtDay.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.txtDay.Properties.Items.AddRange(new object[] {
            "Monday",
            "Tuesday",
            "Wednesday",
            "Thursday",
            "Friday",
            "Saturday",
            "Sunday"});
            this.txtDay.Size = new System.Drawing.Size(150, 20);
            this.txtDay.TabIndex = 50;
            // 
            // txtSubject
            // 
            this.txtSubject.Location = new System.Drawing.Point(473, 42);
            this.txtSubject.Name = "txtSubject";
            this.txtSubject.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.txtSubject.Size = new System.Drawing.Size(150, 20);
            this.txtSubject.TabIndex = 52;
            // 
            // txtsTime
            // 
            this.txtsTime.EditValue = null;
            this.txtsTime.Location = new System.Drawing.Point(629, 42);
            this.txtsTime.Name = "txtsTime";
            this.txtsTime.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.txtsTime.Properties.EditValueChangedFiringMode = DevExpress.XtraEditors.Controls.EditValueChangedFiringMode.Default;
            this.txtsTime.Properties.Mask.EditMask = "";
            this.txtsTime.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.None;
            this.txtsTime.Properties.TimeEditStyle = DevExpress.XtraEditors.Repository.TimeEditStyle.TouchUI;
            this.txtsTime.Size = new System.Drawing.Size(150, 20);
            this.txtsTime.TabIndex = 58;
            // 
            // txteTime
            // 
            this.txteTime.EditValue = null;
            this.txteTime.Location = new System.Drawing.Point(785, 42);
            this.txteTime.Name = "txteTime";
            this.txteTime.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.txteTime.Properties.EditValueChangedFiringMode = DevExpress.XtraEditors.Controls.EditValueChangedFiringMode.Default;
            this.txteTime.Properties.Mask.EditMask = "";
            this.txteTime.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.None;
            this.txteTime.Properties.TimeEditStyle = DevExpress.XtraEditors.Repository.TimeEditStyle.TouchUI;
            this.txteTime.Size = new System.Drawing.Size(150, 20);
            this.txteTime.TabIndex = 62;
            // 
            // labelControl2
            // 
            this.labelControl2.Location = new System.Drawing.Point(317, 23);
            this.labelControl2.Name = "labelControl2";
            this.labelControl2.Size = new System.Drawing.Size(35, 13);
            this.labelControl2.TabIndex = 63;
            this.labelControl2.Text = "Section";
            // 
            // txtSection
            // 
            this.txtSection.Location = new System.Drawing.Point(317, 42);
            this.txtSection.Name = "txtSection";
            this.txtSection.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.txtSection.Size = new System.Drawing.Size(150, 20);
            this.txtSection.TabIndex = 64;
            this.txtSection.SelectedIndexChanged += new System.EventHandler(this.txtSection_SelectedIndexChanged);
            // 
            // ClassRoutine
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.gridClassRoutine);
            this.Controls.Add(this.groupControl1);
            this.Name = "ClassRoutine";
            this.Size = new System.Drawing.Size(1055, 479);
            this.Enter += new System.EventHandler(this.ClassRoutine_Enter);
            ((System.ComponentModel.ISupportInitialize)(this.gridClassRoutine)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).EndInit();
            this.groupControl1.ResumeLayout(false);
            this.groupControl1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtClass.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtDay.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtSubject.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtsTime.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txteTime.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtSection.Properties)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraGrid.GridControl gridClassRoutine;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView1;
        private DevExpress.XtraEditors.GroupControl groupControl1;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.XtraEditors.SimpleButton btnCrDelete;
        private DevExpress.XtraEditors.SimpleButton btnCrAdd;
        private DevExpress.XtraEditors.LabelControl labelControl11;
        private DevExpress.XtraEditors.LabelControl labelControl9;
        private DevExpress.XtraEditors.LabelControl labelControl8;
        private DevExpress.XtraEditors.LabelControl labelControl7;
        private DevExpress.XtraEditors.ComboBoxEdit txtClass;
        private DevExpress.XtraEditors.ComboBoxEdit txtDay;
        private DevExpress.XtraEditors.ComboBoxEdit txtSubject;
        private DevExpress.XtraEditors.TimeEdit txtsTime;
        private DevExpress.XtraEditors.TimeEdit txteTime;
        private DevExpress.XtraEditors.LabelControl labelControl2;
        private DevExpress.XtraEditors.ComboBoxEdit txtSection;
    }
}
