namespace SchoolManagementSystem.Hostel
{
    partial class Hostel_Student
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Hostel_Student));
            this.groupControl4 = new DevExpress.XtraEditors.GroupControl();
            this.dtpJoingDate = new System.Windows.Forms.DateTimePicker();
            this.GridStudentsList = new DevExpress.XtraEditors.GridLookUpEdit();
            this.gridView1 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            this.GridRoomsList = new DevExpress.XtraEditors.GridLookUpEdit();
            this.gridLookUpEdit1View = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.btnHLeft = new DevExpress.XtraEditors.SimpleButton();
            this.btnAddstdtoroom = new DevExpress.XtraEditors.SimpleButton();
            this.labelControl17 = new DevExpress.XtraEditors.LabelControl();
            this.txtRRent = new DevExpress.XtraEditors.TextEdit();
            this.labelControl19 = new DevExpress.XtraEditors.LabelControl();
            this.gridHostel_students = new DevExpress.XtraGrid.GridControl();
            this.gridView4 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.DtpLeftDate = new System.Windows.Forms.DateTimePicker();
            this.labelControl3 = new DevExpress.XtraEditors.LabelControl();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl4)).BeginInit();
            this.groupControl4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.GridStudentsList.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.GridRoomsList.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridLookUpEdit1View)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtRRent.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridHostel_students)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView4)).BeginInit();
            this.SuspendLayout();
            // 
            // groupControl4
            // 
            this.groupControl4.Controls.Add(this.DtpLeftDate);
            this.groupControl4.Controls.Add(this.labelControl3);
            this.groupControl4.Controls.Add(this.dtpJoingDate);
            this.groupControl4.Controls.Add(this.GridStudentsList);
            this.groupControl4.Controls.Add(this.labelControl2);
            this.groupControl4.Controls.Add(this.GridRoomsList);
            this.groupControl4.Controls.Add(this.labelControl1);
            this.groupControl4.Controls.Add(this.btnHLeft);
            this.groupControl4.Controls.Add(this.btnAddstdtoroom);
            this.groupControl4.Controls.Add(this.labelControl17);
            this.groupControl4.Controls.Add(this.txtRRent);
            this.groupControl4.Controls.Add(this.labelControl19);
            this.groupControl4.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupControl4.Location = new System.Drawing.Point(0, 0);
            this.groupControl4.Name = "groupControl4";
            this.groupControl4.Size = new System.Drawing.Size(1011, 70);
            this.groupControl4.TabIndex = 8;
            this.groupControl4.Text = "Add Hostel";
            // 
            // dtpJoingDate
            // 
            this.dtpJoingDate.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpJoingDate.Location = new System.Drawing.Point(488, 41);
            this.dtpJoingDate.Name = "dtpJoingDate";
            this.dtpJoingDate.Size = new System.Drawing.Size(94, 21);
            this.dtpJoingDate.TabIndex = 104;
            // 
            // GridStudentsList
            // 
            this.GridStudentsList.Location = new System.Drawing.Point(174, 42);
            this.GridStudentsList.Name = "GridStudentsList";
            this.GridStudentsList.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.GridStudentsList.Properties.PopupView = this.gridView1;
            this.GridStudentsList.Size = new System.Drawing.Size(152, 20);
            this.GridStudentsList.TabIndex = 103;
            // 
            // gridView1
            // 
            this.gridView1.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus;
            this.gridView1.Name = "gridView1";
            this.gridView1.OptionsSelection.EnableAppearanceFocusedCell = false;
            this.gridView1.OptionsView.ShowGroupPanel = false;
            // 
            // labelControl2
            // 
            this.labelControl2.Location = new System.Drawing.Point(16, 23);
            this.labelControl2.Name = "labelControl2";
            this.labelControl2.Size = new System.Drawing.Size(76, 13);
            this.labelControl2.TabIndex = 102;
            this.labelControl2.Text = "Avaiable Rooms";
            // 
            // GridRoomsList
            // 
            this.GridRoomsList.Location = new System.Drawing.Point(16, 42);
            this.GridRoomsList.Name = "GridRoomsList";
            this.GridRoomsList.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.GridRoomsList.Properties.PopupView = this.gridLookUpEdit1View;
            this.GridRoomsList.Size = new System.Drawing.Size(152, 20);
            this.GridRoomsList.TabIndex = 101;
            // 
            // gridLookUpEdit1View
            // 
            this.gridLookUpEdit1View.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus;
            this.gridLookUpEdit1View.Name = "gridLookUpEdit1View";
            this.gridLookUpEdit1View.OptionsSelection.EnableAppearanceFocusedCell = false;
            this.gridLookUpEdit1View.OptionsView.ShowGroupPanel = false;
            // 
            // labelControl1
            // 
            this.labelControl1.Location = new System.Drawing.Point(488, 23);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(45, 13);
            this.labelControl1.TabIndex = 100;
            this.labelControl1.Text = "Join Date";
            // 
            // btnHLeft
            // 
            this.btnHLeft.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("btnHDelete.ImageOptions.Image")));
            this.btnHLeft.Location = new System.Drawing.Point(790, 39);
            this.btnHLeft.Name = "btnHLeft";
            this.btnHLeft.Size = new System.Drawing.Size(67, 23);
            this.btnHLeft.TabIndex = 72;
            this.btnHLeft.Text = "Left";
            this.btnHLeft.Click += new System.EventHandler(this.btnHLeft_Click);
            // 
            // btnAddstdtoroom
            // 
            this.btnAddstdtoroom.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("btnAddstdtoroom.ImageOptions.Image")));
            this.btnAddstdtoroom.Location = new System.Drawing.Point(588, 40);
            this.btnAddstdtoroom.Name = "btnAddstdtoroom";
            this.btnAddstdtoroom.Size = new System.Drawing.Size(67, 23);
            this.btnAddstdtoroom.TabIndex = 71;
            this.btnAddstdtoroom.Text = "Join";
            this.btnAddstdtoroom.Click += new System.EventHandler(this.btnAddstdtoroom_Click);
            // 
            // labelControl17
            // 
            this.labelControl17.Location = new System.Drawing.Point(332, 23);
            this.labelControl17.Name = "labelControl17";
            this.labelControl17.Size = new System.Drawing.Size(64, 13);
            this.labelControl17.TabIndex = 65;
            this.labelControl17.Text = "Monthly Rent";
            // 
            // txtRRent
            // 
            this.txtRRent.Location = new System.Drawing.Point(332, 42);
            this.txtRRent.Name = "txtRRent";
            this.txtRRent.Size = new System.Drawing.Size(150, 20);
            this.txtRRent.TabIndex = 62;
            // 
            // labelControl19
            // 
            this.labelControl19.Location = new System.Drawing.Point(174, 23);
            this.labelControl19.Name = "labelControl19";
            this.labelControl19.Size = new System.Drawing.Size(43, 13);
            this.labelControl19.TabIndex = 61;
            this.labelControl19.Text = "Students";
            // 
            // gridHostel_students
            // 
            this.gridHostel_students.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridHostel_students.Location = new System.Drawing.Point(0, 0);
            this.gridHostel_students.MainView = this.gridView4;
            this.gridHostel_students.Name = "gridHostel_students";
            this.gridHostel_students.Size = new System.Drawing.Size(1011, 549);
            this.gridHostel_students.TabIndex = 9;
            this.gridHostel_students.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridView4});
            // 
            // gridView4
            // 
            this.gridView4.GridControl = this.gridHostel_students;
            this.gridView4.Name = "gridView4";
            this.gridView4.OptionsBehavior.EditingMode = DevExpress.XtraGrid.Views.Grid.GridEditingMode.EditFormInplace;
            this.gridView4.OptionsFind.AlwaysVisible = true;
            // 
            // DtpLeftDate
            // 
            this.DtpLeftDate.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.DtpLeftDate.Location = new System.Drawing.Point(661, 41);
            this.DtpLeftDate.Name = "DtpLeftDate";
            this.DtpLeftDate.Size = new System.Drawing.Size(94, 21);
            this.DtpLeftDate.TabIndex = 106;
            // 
            // labelControl3
            // 
            this.labelControl3.Location = new System.Drawing.Point(661, 23);
            this.labelControl3.Name = "labelControl3";
            this.labelControl3.Size = new System.Drawing.Size(45, 13);
            this.labelControl3.TabIndex = 105;
            this.labelControl3.Text = "Left Date";
            // 
            // Hostel_Student
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.groupControl4);
            this.Controls.Add(this.gridHostel_students);
            this.Name = "Hostel_Student";
            this.Size = new System.Drawing.Size(1011, 549);
            ((System.ComponentModel.ISupportInitialize)(this.groupControl4)).EndInit();
            this.groupControl4.ResumeLayout(false);
            this.groupControl4.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.GridStudentsList.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.GridRoomsList.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridLookUpEdit1View)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtRRent.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridHostel_students)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView4)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.GroupControl groupControl4;
        private DevExpress.XtraEditors.SimpleButton btnHLeft;
        private DevExpress.XtraEditors.SimpleButton btnAddstdtoroom;
        private DevExpress.XtraEditors.LabelControl labelControl17;
        private DevExpress.XtraEditors.TextEdit txtRRent;
        private DevExpress.XtraEditors.LabelControl labelControl19;
        private DevExpress.XtraGrid.GridControl gridHostel_students;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView4;
        private System.Windows.Forms.DateTimePicker dtpJoingDate;
        private DevExpress.XtraEditors.GridLookUpEdit GridStudentsList;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView1;
        private DevExpress.XtraEditors.LabelControl labelControl2;
        private DevExpress.XtraEditors.GridLookUpEdit GridRoomsList;
        private DevExpress.XtraGrid.Views.Grid.GridView gridLookUpEdit1View;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        private System.Windows.Forms.DateTimePicker DtpLeftDate;
        private DevExpress.XtraEditors.LabelControl labelControl3;
    }
}
