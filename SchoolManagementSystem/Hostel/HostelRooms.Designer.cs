namespace SchoolManagementSystem.Hostel
{
    partial class HostelRooms
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(HostelRooms));
            this.groupControl4 = new DevExpress.XtraEditors.GroupControl();
            this.GridHostelList = new DevExpress.XtraEditors.GridLookUpEdit();
            this.gridLookUpEdit1View = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.btncreateHostel = new DevExpress.XtraEditors.SimpleButton();
            this.btnHRDelete = new DevExpress.XtraEditors.SimpleButton();
            this.btnHRAdd = new DevExpress.XtraEditors.SimpleButton();
            this.labelControl17 = new DevExpress.XtraEditors.LabelControl();
            this.txtHRCapacity = new DevExpress.XtraEditors.TextEdit();
            this.txtHRName = new DevExpress.XtraEditors.TextEdit();
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl19 = new DevExpress.XtraEditors.LabelControl();
            this.txtHRdes = new DevExpress.XtraEditors.TextEdit();
            this.gridHostelRooms = new DevExpress.XtraGrid.GridControl();
            this.gridView4 = new DevExpress.XtraGrid.Views.Grid.GridView();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl4)).BeginInit();
            this.groupControl4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.GridHostelList.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridLookUpEdit1View)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtHRCapacity.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtHRName.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtHRdes.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridHostelRooms)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView4)).BeginInit();
            this.SuspendLayout();
            // 
            // groupControl4
            // 
            this.groupControl4.Controls.Add(this.GridHostelList);
            this.groupControl4.Controls.Add(this.btncreateHostel);
            this.groupControl4.Controls.Add(this.btnHRDelete);
            this.groupControl4.Controls.Add(this.btnHRAdd);
            this.groupControl4.Controls.Add(this.labelControl17);
            this.groupControl4.Controls.Add(this.txtHRCapacity);
            this.groupControl4.Controls.Add(this.txtHRName);
            this.groupControl4.Controls.Add(this.labelControl2);
            this.groupControl4.Controls.Add(this.labelControl1);
            this.groupControl4.Controls.Add(this.labelControl19);
            this.groupControl4.Controls.Add(this.txtHRdes);
            this.groupControl4.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupControl4.Location = new System.Drawing.Point(0, 0);
            this.groupControl4.Name = "groupControl4";
            this.groupControl4.Size = new System.Drawing.Size(1245, 70);
            this.groupControl4.TabIndex = 7;
            this.groupControl4.Text = "Add Hostel";
            // 
            // GridHostelList
            // 
            this.GridHostelList.Location = new System.Drawing.Point(5, 41);
            this.GridHostelList.Name = "GridHostelList";
            this.GridHostelList.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.GridHostelList.Properties.PopupView = this.gridLookUpEdit1View;
            this.GridHostelList.Size = new System.Drawing.Size(152, 20);
            this.GridHostelList.TabIndex = 99;
            // 
            // gridLookUpEdit1View
            // 
            this.gridLookUpEdit1View.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus;
            this.gridLookUpEdit1View.Name = "gridLookUpEdit1View";
            this.gridLookUpEdit1View.OptionsSelection.EnableAppearanceFocusedCell = false;
            this.gridLookUpEdit1View.OptionsView.ShowGroupPanel = false;
            // 
            // btncreateHostel
            // 
            this.btncreateHostel.Appearance.Font = new System.Drawing.Font("Tahoma", 10F);
            this.btncreateHostel.Appearance.Options.UseFont = true;
            this.btncreateHostel.Dock = System.Windows.Forms.DockStyle.Right;
            this.btncreateHostel.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("btncreateHostel.ImageOptions.Image")));
            this.btncreateHostel.Location = new System.Drawing.Point(1119, 20);
            this.btncreateHostel.Name = "btncreateHostel";
            this.btncreateHostel.Size = new System.Drawing.Size(124, 48);
            this.btncreateHostel.TabIndex = 72;
            this.btncreateHostel.Text = "Create Hostel";
            this.btncreateHostel.Click += new System.EventHandler(this.btncreateHostel_Click);
            // 
            // btnHRDelete
            // 
            this.btnHRDelete.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("btnHRDelete.ImageOptions.Image")));
            this.btnHRDelete.Location = new System.Drawing.Point(703, 38);
            this.btnHRDelete.Name = "btnHRDelete";
            this.btnHRDelete.Size = new System.Drawing.Size(67, 23);
            this.btnHRDelete.TabIndex = 72;
            this.btnHRDelete.Text = "Delete";
            this.btnHRDelete.Click += new System.EventHandler(this.btnHRDelete_Click);
            // 
            // btnHRAdd
            // 
            this.btnHRAdd.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("btnHRAdd.ImageOptions.Image")));
            this.btnHRAdd.Location = new System.Drawing.Point(630, 39);
            this.btnHRAdd.Name = "btnHRAdd";
            this.btnHRAdd.Size = new System.Drawing.Size(67, 23);
            this.btnHRAdd.TabIndex = 71;
            this.btnHRAdd.Text = "Add";
            this.btnHRAdd.Click += new System.EventHandler(this.btnHRAdd_Click);
            // 
            // labelControl17
            // 
            this.labelControl17.Location = new System.Drawing.Point(474, 22);
            this.labelControl17.Name = "labelControl17";
            this.labelControl17.Size = new System.Drawing.Size(53, 13);
            this.labelControl17.TabIndex = 65;
            this.labelControl17.Text = "Description";
            // 
            // txtHRCapacity
            // 
            this.txtHRCapacity.Location = new System.Drawing.Point(318, 41);
            this.txtHRCapacity.Name = "txtHRCapacity";
            this.txtHRCapacity.Size = new System.Drawing.Size(150, 20);
            this.txtHRCapacity.TabIndex = 62;
            // 
            // txtHRName
            // 
            this.txtHRName.Location = new System.Drawing.Point(163, 42);
            this.txtHRName.Name = "txtHRName";
            this.txtHRName.Size = new System.Drawing.Size(150, 20);
            this.txtHRName.TabIndex = 62;
            // 
            // labelControl2
            // 
            this.labelControl2.Location = new System.Drawing.Point(318, 23);
            this.labelControl2.Name = "labelControl2";
            this.labelControl2.Size = new System.Drawing.Size(42, 13);
            this.labelControl2.TabIndex = 61;
            this.labelControl2.Text = "Capacity";
            // 
            // labelControl1
            // 
            this.labelControl1.Location = new System.Drawing.Point(163, 23);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(27, 13);
            this.labelControl1.TabIndex = 61;
            this.labelControl1.Text = "Room";
            // 
            // labelControl19
            // 
            this.labelControl19.Location = new System.Drawing.Point(5, 23);
            this.labelControl19.Name = "labelControl19";
            this.labelControl19.Size = new System.Drawing.Size(30, 13);
            this.labelControl19.TabIndex = 61;
            this.labelControl19.Text = "Hostel";
            // 
            // txtHRdes
            // 
            this.txtHRdes.Location = new System.Drawing.Point(474, 41);
            this.txtHRdes.Name = "txtHRdes";
            this.txtHRdes.Size = new System.Drawing.Size(150, 20);
            this.txtHRdes.TabIndex = 66;
            // 
            // gridHostelRooms
            // 
            this.gridHostelRooms.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridHostelRooms.Location = new System.Drawing.Point(0, 70);
            this.gridHostelRooms.MainView = this.gridView4;
            this.gridHostelRooms.Name = "gridHostelRooms";
            this.gridHostelRooms.Size = new System.Drawing.Size(1245, 375);
            this.gridHostelRooms.TabIndex = 8;
            this.gridHostelRooms.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridView4});
            // 
            // gridView4
            // 
            this.gridView4.GridControl = this.gridHostelRooms;
            this.gridView4.Name = "gridView4";
            this.gridView4.OptionsBehavior.EditingMode = DevExpress.XtraGrid.Views.Grid.GridEditingMode.EditFormInplace;
            this.gridView4.OptionsFind.AlwaysVisible = true;
            this.gridView4.RowUpdated += new DevExpress.XtraGrid.Views.Base.RowObjectEventHandler(this.gridView4_RowUpdated);
            // 
            // HostelRooms
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.gridHostelRooms);
            this.Controls.Add(this.groupControl4);
            this.Name = "HostelRooms";
            this.Size = new System.Drawing.Size(1245, 445);
            ((System.ComponentModel.ISupportInitialize)(this.groupControl4)).EndInit();
            this.groupControl4.ResumeLayout(false);
            this.groupControl4.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.GridHostelList.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridLookUpEdit1View)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtHRCapacity.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtHRName.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtHRdes.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridHostelRooms)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView4)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.GroupControl groupControl4;
        private DevExpress.XtraEditors.SimpleButton btnHRDelete;
        private DevExpress.XtraEditors.SimpleButton btnHRAdd;
        private DevExpress.XtraEditors.LabelControl labelControl17;
        private DevExpress.XtraEditors.TextEdit txtHRCapacity;
        private DevExpress.XtraEditors.TextEdit txtHRName;
        private DevExpress.XtraEditors.LabelControl labelControl2;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.XtraEditors.LabelControl labelControl19;
        private DevExpress.XtraEditors.TextEdit txtHRdes;
        private DevExpress.XtraGrid.GridControl gridHostelRooms;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView4;
        private DevExpress.XtraEditors.GridLookUpEdit GridHostelList;
        private DevExpress.XtraGrid.Views.Grid.GridView gridLookUpEdit1View;
        private DevExpress.XtraEditors.SimpleButton btncreateHostel;
    }
}
