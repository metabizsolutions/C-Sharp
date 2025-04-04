namespace SchoolManagementSystem.Class
{
    partial class ManageSession
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ManageSession));
            this.gridSession = new DevExpress.XtraGrid.GridControl();
            this.gridView1 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.groupControl1 = new DevExpress.XtraEditors.GroupControl();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.txtDefault = new DevExpress.XtraEditors.CheckEdit();
            this.BtnSessionDelete = new DevExpress.XtraEditors.SimpleButton();
            this.btnSessionAdd = new DevExpress.XtraEditors.SimpleButton();
            this.labelControl10 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl9 = new DevExpress.XtraEditors.LabelControl();
            this.txtSYear = new DevExpress.XtraEditors.DateEdit();
            this.txtEYear = new DevExpress.XtraEditors.DateEdit();
            this.txtName = new DevExpress.XtraEditors.TextEdit();
            ((System.ComponentModel.ISupportInitialize)(this.gridSession)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).BeginInit();
            this.groupControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtDefault.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtSYear.Properties.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtSYear.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtEYear.Properties.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtEYear.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtName.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // gridSession
            // 
            this.gridSession.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridSession.Location = new System.Drawing.Point(0, 72);
            this.gridSession.MainView = this.gridView1;
            this.gridSession.Name = "gridSession";
            this.gridSession.Size = new System.Drawing.Size(1002, 359);
            this.gridSession.TabIndex = 3;
            this.gridSession.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridView1});
            // 
            // gridView1
            // 
            this.gridView1.GridControl = this.gridSession;
            this.gridView1.Name = "gridView1";
            this.gridView1.OptionsBehavior.EditingMode = DevExpress.XtraGrid.Views.Grid.GridEditingMode.Inplace;
            this.gridView1.OptionsFind.AlwaysVisible = true;
            this.gridView1.RowUpdated += new DevExpress.XtraGrid.Views.Base.RowObjectEventHandler(this.gridView1_RowUpdated);
            // 
            // groupControl1
            // 
            this.groupControl1.Controls.Add(this.labelControl1);
            this.groupControl1.Controls.Add(this.txtDefault);
            this.groupControl1.Controls.Add(this.BtnSessionDelete);
            this.groupControl1.Controls.Add(this.btnSessionAdd);
            this.groupControl1.Controls.Add(this.labelControl10);
            this.groupControl1.Controls.Add(this.labelControl9);
            this.groupControl1.Controls.Add(this.txtSYear);
            this.groupControl1.Controls.Add(this.txtEYear);
            this.groupControl1.Controls.Add(this.txtName);
            this.groupControl1.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupControl1.Location = new System.Drawing.Point(0, 0);
            this.groupControl1.Name = "groupControl1";
            this.groupControl1.Size = new System.Drawing.Size(1002, 72);
            this.groupControl1.TabIndex = 2;
            this.groupControl1.Text = "Add Session";
            this.groupControl1.Visible = false;
            // 
            // labelControl1
            // 
            this.labelControl1.Location = new System.Drawing.Point(5, 23);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(27, 13);
            this.labelControl1.TabIndex = 64;
            this.labelControl1.Text = "Name";
            // 
            // txtDefault
            // 
            this.txtDefault.Location = new System.Drawing.Point(473, 42);
            this.txtDefault.Name = "txtDefault";
            this.txtDefault.Properties.Caption = "Default Session ";
            this.txtDefault.Size = new System.Drawing.Size(97, 19);
            this.txtDefault.TabIndex = 63;
            // 
            // BtnSessionDelete
            // 
            this.BtnSessionDelete.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("BtnSessionDelete.ImageOptions.Image")));
            this.BtnSessionDelete.Location = new System.Drawing.Point(649, 39);
            this.BtnSessionDelete.Name = "BtnSessionDelete";
            this.BtnSessionDelete.Size = new System.Drawing.Size(67, 23);
            this.BtnSessionDelete.TabIndex = 60;
            this.BtnSessionDelete.Text = "Delete";
            this.BtnSessionDelete.Visible = false;
            this.BtnSessionDelete.Click += new System.EventHandler(this.BtnSessionDelete_Click);
            // 
            // btnSessionAdd
            // 
            this.btnSessionAdd.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("btnSessionAdd.ImageOptions.Image")));
            this.btnSessionAdd.Location = new System.Drawing.Point(576, 39);
            this.btnSessionAdd.Name = "btnSessionAdd";
            this.btnSessionAdd.Size = new System.Drawing.Size(67, 23);
            this.btnSessionAdd.TabIndex = 59;
            this.btnSessionAdd.Text = "Add";
            this.btnSessionAdd.Visible = false;
            this.btnSessionAdd.Click += new System.EventHandler(this.btnSessionAdd_Click);
            // 
            // labelControl10
            // 
            this.labelControl10.Location = new System.Drawing.Point(317, 23);
            this.labelControl10.Name = "labelControl10";
            this.labelControl10.Size = new System.Drawing.Size(57, 13);
            this.labelControl10.TabIndex = 55;
            this.labelControl10.Text = "Ending Year";
            // 
            // labelControl9
            // 
            this.labelControl9.Location = new System.Drawing.Point(161, 23);
            this.labelControl9.Name = "labelControl9";
            this.labelControl9.Size = new System.Drawing.Size(63, 13);
            this.labelControl9.TabIndex = 53;
            this.labelControl9.Text = "Starting Year";
            // 
            // txtSYear
            // 
            this.txtSYear.EditValue = null;
            this.txtSYear.Location = new System.Drawing.Point(161, 42);
            this.txtSYear.Name = "txtSYear";
            this.txtSYear.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.txtSYear.Properties.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.txtSYear.Properties.CalendarView = DevExpress.XtraEditors.Repository.CalendarView.TouchUI;
            this.txtSYear.Properties.DisplayFormat.FormatString = "";
            this.txtSYear.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.txtSYear.Properties.EditFormat.FormatString = "";
            this.txtSYear.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.txtSYear.Properties.Mask.EditMask = "";
            this.txtSYear.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.None;
            this.txtSYear.Properties.VistaDisplayMode = DevExpress.Utils.DefaultBoolean.False;
            this.txtSYear.Size = new System.Drawing.Size(150, 20);
            this.txtSYear.TabIndex = 54;
            // 
            // txtEYear
            // 
            this.txtEYear.EditValue = null;
            this.txtEYear.Location = new System.Drawing.Point(317, 42);
            this.txtEYear.Name = "txtEYear";
            this.txtEYear.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.txtEYear.Properties.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.txtEYear.Properties.CalendarView = DevExpress.XtraEditors.Repository.CalendarView.TouchUI;
            this.txtEYear.Properties.DisplayFormat.FormatString = "";
            this.txtEYear.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.txtEYear.Properties.EditFormat.FormatString = "";
            this.txtEYear.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.txtEYear.Properties.Mask.EditMask = "";
            this.txtEYear.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.None;
            this.txtEYear.Properties.VistaDisplayMode = DevExpress.Utils.DefaultBoolean.False;
            this.txtEYear.Size = new System.Drawing.Size(150, 20);
            this.txtEYear.TabIndex = 56;
            // 
            // txtName
            // 
            this.txtName.Location = new System.Drawing.Point(5, 42);
            this.txtName.Name = "txtName";
            this.txtName.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.txtName.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.txtName.Size = new System.Drawing.Size(150, 20);
            this.txtName.TabIndex = 65;
            // 
            // ManageSession
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.gridSession);
            this.Controls.Add(this.groupControl1);
            this.Name = "ManageSession";
            this.Size = new System.Drawing.Size(1002, 431);
            ((System.ComponentModel.ISupportInitialize)(this.gridSession)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).EndInit();
            this.groupControl1.ResumeLayout(false);
            this.groupControl1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtDefault.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtSYear.Properties.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtSYear.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtEYear.Properties.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtEYear.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtName.Properties)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraGrid.GridControl gridSession;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView1;
        private DevExpress.XtraEditors.GroupControl groupControl1;
        private DevExpress.XtraEditors.SimpleButton BtnSessionDelete;
        private DevExpress.XtraEditors.SimpleButton btnSessionAdd;
        private DevExpress.XtraEditors.LabelControl labelControl10;
        private DevExpress.XtraEditors.LabelControl labelControl9;
        private DevExpress.XtraEditors.DateEdit txtSYear;
        private DevExpress.XtraEditors.DateEdit txtEYear;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.XtraEditors.CheckEdit txtDefault;
        private DevExpress.XtraEditors.TextEdit txtName;
    }
}
