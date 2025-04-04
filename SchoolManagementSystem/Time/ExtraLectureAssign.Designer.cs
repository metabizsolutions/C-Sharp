using DevExpress.XtraCharts.Designer;
using DevExpress.XtraEditors;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraLayout;

namespace SchoolManagementSystem.Class
{
    partial class ExtraLectureAssign
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ExtraLectureAssign));
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.txtDateManage = new DevExpress.XtraEditors.DateEdit();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.btnReportPrint = new DevExpress.XtraEditors.SimpleButton();
            this.btnPrint = new DevExpress.XtraEditors.SimpleButton();
            this.gridControl1 = new DevExpress.XtraGrid.GridControl();
            this.gridView1 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.layoutControl1 = new DevExpress.XtraLayout.LayoutControl();
            this.GridSubject = new DevExpress.XtraEditors.GridLookUpEdit();
            this.gridLookUpEdit1View = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.btnClose = new System.Windows.Forms.LinkLabel();
            this.btnSave = new DevExpress.XtraEditors.SimpleButton();
            this.txtClass = new DevExpress.XtraEditors.TextEdit();
            this.txtSection = new DevExpress.XtraEditors.TextEdit();
            this.txtDay = new DevExpress.XtraEditors.TextEdit();
            this.GridTeacher = new DevExpress.XtraEditors.GridLookUpEdit();
            this.gridView3 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.txtRoom = new DevExpress.XtraEditors.TextEdit();
            this.GridTeacherAssign = new DevExpress.XtraEditors.GridLookUpEdit();
            this.gridView4 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.layoutControlGroup1 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlGroup4 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItem1 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem13 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem14 = new DevExpress.XtraLayout.LayoutControlItem();
            this.Subject = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem12 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem11 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem10 = new DevExpress.XtraLayout.LayoutControlItem();
            this.Teacher1 = new DevExpress.XtraLayout.LayoutControlItem();
            this.Teacher = new DevExpress.XtraLayout.LayoutControlItem();
            this.gridControl2 = new DevExpress.XtraGrid.GridControl();
            this.gridView2 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtDateManage.Properties.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtDateManage.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridControl1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).BeginInit();
            this.layoutControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.GridSubject.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridLookUpEdit1View)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtClass.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtSection.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtDay.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.GridTeacher.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtRoom.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.GridTeacherAssign.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem13)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem14)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Subject)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem12)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem11)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem10)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Teacher1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Teacher)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridControl2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView2)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.txtDateManage);
            this.groupBox1.Controls.Add(this.labelControl1);
            this.groupBox1.Controls.Add(this.btnReportPrint);
            this.groupBox1.Controls.Add(this.btnPrint);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBox1.Location = new System.Drawing.Point(0, 0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(1146, 71);
            this.groupBox1.TabIndex = 69;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Time Table";
            // 
            // txtDateManage
            // 
            this.txtDateManage.EditValue = null;
            this.txtDateManage.Location = new System.Drawing.Point(3, 39);
            this.txtDateManage.Name = "txtDateManage";
            this.txtDateManage.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.txtDateManage.Properties.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.txtDateManage.Size = new System.Drawing.Size(204, 20);
            this.txtDateManage.TabIndex = 81;
            // 
            // labelControl1
            // 
            this.labelControl1.Location = new System.Drawing.Point(6, 20);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(67, 13);
            this.labelControl1.TabIndex = 77;
            this.labelControl1.Text = "Select Section";
            // 
            // btnReportPrint
            // 
            this.btnReportPrint.Image = ((System.Drawing.Image)(resources.GetObject("btnReportPrint.Image")));
            this.btnReportPrint.Location = new System.Drawing.Point(473, 20);
            this.btnReportPrint.Name = "btnReportPrint";
            this.btnReportPrint.Size = new System.Drawing.Size(85, 39);
            this.btnReportPrint.TabIndex = 75;
            this.btnReportPrint.Text = "PRINT";
            this.btnReportPrint.Click += new System.EventHandler(this.btnReportPrint_Click);
            // 
            // btnPrint
            // 
            this.btnPrint.Image = ((System.Drawing.Image)(resources.GetObject("btnPrint.Image")));
            this.btnPrint.Location = new System.Drawing.Point(223, 37);
            this.btnPrint.Name = "btnPrint";
            this.btnPrint.Size = new System.Drawing.Size(75, 23);
            this.btnPrint.TabIndex = 74;
            this.btnPrint.Text = "Search";
            this.btnPrint.Click += new System.EventHandler(this.btnPrint_Click);
            // 
            // gridControl1
            // 
            this.gridControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridControl1.Location = new System.Drawing.Point(0, 71);
            this.gridControl1.MainView = this.gridView1;
            this.gridControl1.Name = "gridControl1";
            this.gridControl1.Size = new System.Drawing.Size(911, 398);
            this.gridControl1.TabIndex = 70;
            this.gridControl1.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridView1});
            // 
            // gridView1
            // 
            this.gridView1.GridControl = this.gridControl1;
            this.gridView1.Name = "gridView1";
            this.gridView1.RowCellStyle += new DevExpress.XtraGrid.Views.Grid.RowCellStyleEventHandler(this.gridView1_RowCellStyle);
            this.gridView1.MouseDown += new System.Windows.Forms.MouseEventHandler(this.gridView1_MouseDown);
            // 
            // layoutControl1
            // 
            this.layoutControl1.Controls.Add(this.GridSubject);
            this.layoutControl1.Controls.Add(this.btnClose);
            this.layoutControl1.Controls.Add(this.btnSave);
            this.layoutControl1.Controls.Add(this.txtClass);
            this.layoutControl1.Controls.Add(this.txtSection);
            this.layoutControl1.Controls.Add(this.txtDay);
            this.layoutControl1.Controls.Add(this.GridTeacher);
            this.layoutControl1.Controls.Add(this.txtRoom);
            this.layoutControl1.Controls.Add(this.GridTeacherAssign);
            this.layoutControl1.Dock = System.Windows.Forms.DockStyle.Right;
            this.layoutControl1.Location = new System.Drawing.Point(911, 71);
            this.layoutControl1.Name = "layoutControl1";
            this.layoutControl1.Root = this.layoutControlGroup1;
            this.layoutControl1.Size = new System.Drawing.Size(235, 398);
            this.layoutControl1.TabIndex = 71;
            this.layoutControl1.Text = "layoutControl1";
            this.layoutControl1.Visible = false;
            // 
            // GridSubject
            // 
            this.GridSubject.Enabled = false;
            this.GridSubject.Location = new System.Drawing.Point(7, 201);
            this.GridSubject.Name = "GridSubject";
            this.GridSubject.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.GridSubject.Properties.View = this.gridLookUpEdit1View;
            this.GridSubject.Size = new System.Drawing.Size(221, 20);
            this.GridSubject.StyleController = this.layoutControl1;
            this.GridSubject.TabIndex = 17;
            this.GridSubject.EditValueChanged += new System.EventHandler(this.GridSubject_EditValueChanged);
            // 
            // gridLookUpEdit1View
            // 
            this.gridLookUpEdit1View.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus;
            this.gridLookUpEdit1View.Name = "gridLookUpEdit1View";
            this.gridLookUpEdit1View.OptionsSelection.EnableAppearanceFocusedCell = false;
            this.gridLookUpEdit1View.OptionsView.ShowGroupPanel = false;
            // 
            // btnClose
            // 
            this.btnClose.Location = new System.Drawing.Point(7, 347);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(221, 20);
            this.btnClose.TabIndex = 15;
            this.btnClose.TabStop = true;
            this.btnClose.Text = "Hide";
            this.btnClose.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.btnClose_LinkClicked);
            // 
            // btnSave
            // 
            this.btnSave.Appearance.ForeColor = System.Drawing.Color.Black;
            this.btnSave.Appearance.Options.UseForeColor = true;
            this.btnSave.Image = ((System.Drawing.Image)(resources.GetObject("btnSave.Image")));
            this.btnSave.Location = new System.Drawing.Point(7, 305);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(221, 38);
            this.btnSave.StyleController = this.layoutControl1;
            this.btnSave.TabIndex = 13;
            this.btnSave.Text = "Save";
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // txtClass
            // 
            this.txtClass.Location = new System.Drawing.Point(7, 41);
            this.txtClass.Name = "txtClass";
            this.txtClass.Size = new System.Drawing.Size(221, 20);
            this.txtClass.StyleController = this.layoutControl1;
            this.txtClass.TabIndex = 4;
            // 
            // txtSection
            // 
            this.txtSection.Location = new System.Drawing.Point(7, 81);
            this.txtSection.Name = "txtSection";
            this.txtSection.Size = new System.Drawing.Size(221, 20);
            this.txtSection.StyleController = this.layoutControl1;
            this.txtSection.TabIndex = 4;
            // 
            // txtDay
            // 
            this.txtDay.Location = new System.Drawing.Point(7, 121);
            this.txtDay.Name = "txtDay";
            this.txtDay.Size = new System.Drawing.Size(221, 20);
            this.txtDay.StyleController = this.layoutControl1;
            this.txtDay.TabIndex = 4;
            // 
            // GridTeacher
            // 
            this.GridTeacher.Enabled = false;
            this.GridTeacher.Location = new System.Drawing.Point(7, 241);
            this.GridTeacher.Name = "GridTeacher";
            this.GridTeacher.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.GridTeacher.Properties.View = this.gridView3;
            this.GridTeacher.Size = new System.Drawing.Size(221, 20);
            this.GridTeacher.StyleController = this.layoutControl1;
            this.GridTeacher.TabIndex = 17;
            // 
            // gridView3
            // 
            this.gridView3.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus;
            this.gridView3.Name = "gridView3";
            this.gridView3.OptionsSelection.EnableAppearanceFocusedCell = false;
            this.gridView3.OptionsView.ShowGroupPanel = false;
            // 
            // txtRoom
            // 
            this.txtRoom.EditValue = "0";
            this.txtRoom.Location = new System.Drawing.Point(7, 161);
            this.txtRoom.Name = "txtRoom";
            this.txtRoom.Size = new System.Drawing.Size(221, 20);
            this.txtRoom.StyleController = this.layoutControl1;
            this.txtRoom.TabIndex = 4;
            // 
            // GridTeacherAssign
            // 
            this.GridTeacherAssign.Location = new System.Drawing.Point(7, 281);
            this.GridTeacherAssign.Name = "GridTeacherAssign";
            this.GridTeacherAssign.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.GridTeacherAssign.Properties.View = this.gridView4;
            this.GridTeacherAssign.Size = new System.Drawing.Size(221, 20);
            this.GridTeacherAssign.StyleController = this.layoutControl1;
            this.GridTeacherAssign.TabIndex = 17;
            // 
            // gridView4
            // 
            this.gridView4.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus;
            this.gridView4.Name = "gridView4";
            this.gridView4.OptionsSelection.EnableAppearanceFocusedCell = false;
            this.gridView4.OptionsView.ShowGroupPanel = false;
            // 
            // layoutControlGroup1
            // 
            this.layoutControlGroup1.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            this.layoutControlGroup1.GroupBordersVisible = false;
            this.layoutControlGroup1.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlGroup4});
            this.layoutControlGroup1.Location = new System.Drawing.Point(0, 0);
            this.layoutControlGroup1.Name = "Root";
            this.layoutControlGroup1.Padding = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
            this.layoutControlGroup1.Size = new System.Drawing.Size(235, 398);
            this.layoutControlGroup1.TextVisible = false;
            // 
            // layoutControlGroup4
            // 
            this.layoutControlGroup4.CustomizationFormText = "Add,Edit and Delete Slot";
            this.layoutControlGroup4.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem1,
            this.layoutControlItem13,
            this.layoutControlItem14,
            this.Subject,
            this.layoutControlItem12,
            this.layoutControlItem11,
            this.layoutControlItem10,
            this.Teacher1,
            this.Teacher});
            this.layoutControlGroup4.Location = new System.Drawing.Point(0, 0);
            this.layoutControlGroup4.Name = "layoutControlGroup4";
            this.layoutControlGroup4.Padding = new DevExpress.XtraLayout.Utils.Padding(2, 2, 2, 2);
            this.layoutControlGroup4.Size = new System.Drawing.Size(235, 398);
            this.layoutControlGroup4.Text = "Add,Edit and Delete Slot";
            // 
            // layoutControlItem1
            // 
            this.layoutControlItem1.Control = this.txtClass;
            this.layoutControlItem1.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem1.Name = "layoutControlItem1";
            this.layoutControlItem1.Size = new System.Drawing.Size(225, 40);
            this.layoutControlItem1.Text = "Class";
            this.layoutControlItem1.TextLocation = DevExpress.Utils.Locations.Top;
            this.layoutControlItem1.TextSize = new System.Drawing.Size(73, 13);
            // 
            // layoutControlItem13
            // 
            this.layoutControlItem13.Control = this.txtSection;
            this.layoutControlItem13.CustomizationFormText = "Class";
            this.layoutControlItem13.Location = new System.Drawing.Point(0, 40);
            this.layoutControlItem13.Name = "layoutControlItem13";
            this.layoutControlItem13.Size = new System.Drawing.Size(225, 40);
            this.layoutControlItem13.Text = "Section";
            this.layoutControlItem13.TextLocation = DevExpress.Utils.Locations.Top;
            this.layoutControlItem13.TextSize = new System.Drawing.Size(73, 13);
            // 
            // layoutControlItem14
            // 
            this.layoutControlItem14.Control = this.txtDay;
            this.layoutControlItem14.CustomizationFormText = "Class";
            this.layoutControlItem14.Location = new System.Drawing.Point(0, 80);
            this.layoutControlItem14.Name = "layoutControlItem14";
            this.layoutControlItem14.Size = new System.Drawing.Size(225, 40);
            this.layoutControlItem14.Text = "Day";
            this.layoutControlItem14.TextLocation = DevExpress.Utils.Locations.Top;
            this.layoutControlItem14.TextSize = new System.Drawing.Size(73, 13);
            // 
            // Subject
            // 
            this.Subject.Control = this.GridSubject;
            this.Subject.CustomizationFormText = "Subject";
            this.Subject.Enabled = false;
            this.Subject.Location = new System.Drawing.Point(0, 160);
            this.Subject.Name = "Subject";
            this.Subject.Size = new System.Drawing.Size(225, 40);
            this.Subject.TextLocation = DevExpress.Utils.Locations.Top;
            this.Subject.TextSize = new System.Drawing.Size(73, 13);
            // 
            // layoutControlItem12
            // 
            this.layoutControlItem12.Control = this.txtRoom;
            this.layoutControlItem12.CustomizationFormText = "Class";
            this.layoutControlItem12.Location = new System.Drawing.Point(0, 120);
            this.layoutControlItem12.Name = "layoutControlItem12";
            this.layoutControlItem12.Size = new System.Drawing.Size(225, 40);
            this.layoutControlItem12.Text = "Room#";
            this.layoutControlItem12.TextLocation = DevExpress.Utils.Locations.Top;
            this.layoutControlItem12.TextSize = new System.Drawing.Size(73, 13);
            // 
            // layoutControlItem11
            // 
            this.layoutControlItem11.Control = this.btnClose;
            this.layoutControlItem11.Location = new System.Drawing.Point(0, 322);
            this.layoutControlItem11.Name = "layoutControlItem11";
            this.layoutControlItem11.Size = new System.Drawing.Size(225, 48);
            this.layoutControlItem11.Text = "Close";
            this.layoutControlItem11.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem11.TextVisible = false;
            // 
            // layoutControlItem10
            // 
            this.layoutControlItem10.Control = this.btnSave;
            this.layoutControlItem10.Location = new System.Drawing.Point(0, 280);
            this.layoutControlItem10.Name = "layoutControlItem10";
            this.layoutControlItem10.Size = new System.Drawing.Size(225, 42);
            this.layoutControlItem10.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem10.TextVisible = false;
            // 
            // Teacher1
            // 
            this.Teacher1.Control = this.GridTeacherAssign;
            this.Teacher1.CustomizationFormText = "Teacher";
            this.Teacher1.Location = new System.Drawing.Point(0, 240);
            this.Teacher1.Name = "Teacher1";
            this.Teacher1.Size = new System.Drawing.Size(225, 40);
            this.Teacher1.Text = "Assign Teacher";
            this.Teacher1.TextLocation = DevExpress.Utils.Locations.Top;
            this.Teacher1.TextSize = new System.Drawing.Size(73, 13);
            // 
            // Teacher
            // 
            this.Teacher.Control = this.GridTeacher;
            this.Teacher.CustomizationFormText = "Teacher";
            this.Teacher.Enabled = false;
            this.Teacher.Location = new System.Drawing.Point(0, 200);
            this.Teacher.Name = "Teacher";
            this.Teacher.Size = new System.Drawing.Size(225, 40);
            this.Teacher.TextLocation = DevExpress.Utils.Locations.Top;
            this.Teacher.TextSize = new System.Drawing.Size(73, 13);
            // 
            // gridControl2
            // 
            this.gridControl2.Location = new System.Drawing.Point(8, 196);
            this.gridControl2.MainView = this.gridView2;
            this.gridControl2.Name = "gridControl2";
            this.gridControl2.Size = new System.Drawing.Size(263, 232);
            this.gridControl2.TabIndex = 72;
            this.gridControl2.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridView2});
            this.gridControl2.Visible = false;
            // 
            // gridView2
            // 
            this.gridView2.GridControl = this.gridControl2;
            this.gridView2.Name = "gridView2";
            // 
            // ExtraLectureAssign
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.gridControl2);
            this.Controls.Add(this.gridControl1);
            this.Controls.Add(this.layoutControl1);
            this.Controls.Add(this.groupBox1);
            this.Name = "ExtraLectureAssign";
            this.Size = new System.Drawing.Size(1146, 469);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtDateManage.Properties.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtDateManage.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridControl1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).EndInit();
            this.layoutControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.GridSubject.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridLookUpEdit1View)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtClass.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtSection.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtDay.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.GridTeacher.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtRoom.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.GridTeacherAssign.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem13)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem14)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Subject)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem12)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem11)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem10)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Teacher1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Teacher)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridControl2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView2)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.GroupBox groupBox1;
        private DevExpress.XtraGrid.GridControl gridControl1;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView1;
        private DevExpress.XtraLayout.LayoutControl layoutControl1;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup1;
        private DevExpress.XtraEditors.SimpleButton btnSave;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem10;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup4;
        private System.Windows.Forms.LinkLabel btnClose;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem11;
        private DevExpress.XtraEditors.SimpleButton btnPrint;
        private DevExpress.XtraGrid.GridControl gridControl2;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView2;
        private DevExpress.XtraEditors.SimpleButton btnReportPrint;
        private DevExpress.XtraEditors.TextEdit txtClass;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem1;
        private DevExpress.XtraEditors.TextEdit txtSection;
        private DevExpress.XtraEditors.TextEdit txtDay;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem13;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem14;
        private GridLookUpEdit GridSubject;
        private GridView gridLookUpEdit1View;
        private LayoutControlItem Subject;
        private GridLookUpEdit GridTeacher;
        private GridView gridView3;
        private LayoutControlItem Teacher;
        private TextEdit txtRoom;
        private LayoutControlItem layoutControlItem12;
        private LabelControl labelControl1;
        private DateEdit txtDateManage;
        private GridLookUpEdit GridTeacherAssign;
        private GridView gridView4;
        private LayoutControlItem Teacher1;
    }
}
