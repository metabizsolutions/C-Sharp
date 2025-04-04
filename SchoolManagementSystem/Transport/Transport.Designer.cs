namespace SchoolManagementSystem.Transport
{
    partial class Transport
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Transport));
            this.gridTransport = new DevExpress.XtraGrid.GridControl();
            this.gridView3 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.groupControl3 = new DevExpress.XtraEditors.GroupControl();
            this.btnTDelete = new DevExpress.XtraEditors.SimpleButton();
            this.btnTAdd = new DevExpress.XtraEditors.SimpleButton();
            this.labelControl13 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl14 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl15 = new DevExpress.XtraEditors.LabelControl();
            this.txtTName = new DevExpress.XtraEditors.TextEdit();
            this.labelControl16 = new DevExpress.XtraEditors.LabelControl();
            this.txtTFare = new DevExpress.XtraEditors.TextEdit();
            this.txtTDes = new DevExpress.XtraEditors.TextEdit();
            this.txtTVehicle = new DevExpress.XtraEditors.TextEdit();
            this.btnstop = new DevExpress.XtraEditors.SimpleButton();
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            this.routcombo = new DevExpress.XtraEditors.GridLookUpEdit();
            this.gridLookUpEdit1View = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.txtstops = new DevExpress.XtraEditors.GridLookUpEdit();
            this.gridView1 = new DevExpress.XtraGrid.Views.Grid.GridView();
            ((System.ComponentModel.ISupportInitialize)(this.gridTransport)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl3)).BeginInit();
            this.groupControl3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtTName.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtTFare.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtTDes.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtTVehicle.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.routcombo.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridLookUpEdit1View)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtstops.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // gridTransport
            // 
            this.gridTransport.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridTransport.Location = new System.Drawing.Point(0, 70);
            this.gridTransport.MainView = this.gridView3;
            this.gridTransport.Name = "gridTransport";
            this.gridTransport.Size = new System.Drawing.Size(1093, 483);
            this.gridTransport.TabIndex = 5;
            this.gridTransport.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridView3});
            // 
            // gridView3
            // 
            this.gridView3.GridControl = this.gridTransport;
            this.gridView3.Name = "gridView3";
            this.gridView3.OptionsBehavior.EditingMode = DevExpress.XtraGrid.Views.Grid.GridEditingMode.EditFormInplace;
            this.gridView3.OptionsFind.AlwaysVisible = true;
            // 
            // groupControl3
            // 
            this.groupControl3.Controls.Add(this.btnTDelete);
            this.groupControl3.Controls.Add(this.btnstop);
            this.groupControl3.Controls.Add(this.btnTAdd);
            this.groupControl3.Controls.Add(this.labelControl13);
            this.groupControl3.Controls.Add(this.labelControl2);
            this.groupControl3.Controls.Add(this.labelControl1);
            this.groupControl3.Controls.Add(this.labelControl14);
            this.groupControl3.Controls.Add(this.labelControl15);
            this.groupControl3.Controls.Add(this.txtTName);
            this.groupControl3.Controls.Add(this.labelControl16);
            this.groupControl3.Controls.Add(this.txtTFare);
            this.groupControl3.Controls.Add(this.txtTDes);
            this.groupControl3.Controls.Add(this.txtTVehicle);
            this.groupControl3.Controls.Add(this.routcombo);
            this.groupControl3.Controls.Add(this.txtstops);
            this.groupControl3.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupControl3.Location = new System.Drawing.Point(0, 0);
            this.groupControl3.Name = "groupControl3";
            this.groupControl3.Size = new System.Drawing.Size(1093, 70);
            this.groupControl3.TabIndex = 4;
            this.groupControl3.Text = "Add Route";
            // 
            // btnTDelete
            // 
            this.btnTDelete.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("btnTDelete.ImageOptions.Image")));
            this.btnTDelete.Location = new System.Drawing.Point(702, 39);
            this.btnTDelete.Name = "btnTDelete";
            this.btnTDelete.Size = new System.Drawing.Size(67, 23);
            this.btnTDelete.TabIndex = 72;
            this.btnTDelete.Text = "Delete";
            this.btnTDelete.Click += new System.EventHandler(this.btnTDelete_Click);
            // 
            // btnTAdd
            // 
            this.btnTAdd.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("btnTAdd.ImageOptions.Image")));
            this.btnTAdd.Location = new System.Drawing.Point(629, 40);
            this.btnTAdd.Name = "btnTAdd";
            this.btnTAdd.Size = new System.Drawing.Size(67, 23);
            this.btnTAdd.TabIndex = 71;
            this.btnTAdd.Text = "Add";
            this.btnTAdd.Click += new System.EventHandler(this.btnTAdd_Click);
            // 
            // labelControl13
            // 
            this.labelControl13.Location = new System.Drawing.Point(473, 23);
            this.labelControl13.Name = "labelControl13";
            this.labelControl13.Size = new System.Drawing.Size(54, 13);
            this.labelControl13.TabIndex = 67;
            this.labelControl13.Text = "Route Fare";
            // 
            // labelControl1
            // 
            this.labelControl1.Location = new System.Drawing.Point(896, 23);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(22, 13);
            this.labelControl1.TabIndex = 65;
            this.labelControl1.Text = "Stop";
            // 
            // labelControl14
            // 
            this.labelControl14.Location = new System.Drawing.Point(317, 23);
            this.labelControl14.Name = "labelControl14";
            this.labelControl14.Size = new System.Drawing.Size(53, 13);
            this.labelControl14.TabIndex = 65;
            this.labelControl14.Text = "Description";
            // 
            // labelControl15
            // 
            this.labelControl15.Location = new System.Drawing.Point(161, 23);
            this.labelControl15.Name = "labelControl15";
            this.labelControl15.Size = new System.Drawing.Size(88, 13);
            this.labelControl15.TabIndex = 63;
            this.labelControl15.Text = "Number Of Vehicle";
            // 
            // txtTName
            // 
            this.txtTName.Location = new System.Drawing.Point(5, 42);
            this.txtTName.Name = "txtTName";
            this.txtTName.Size = new System.Drawing.Size(150, 20);
            this.txtTName.TabIndex = 62;
            // 
            // labelControl16
            // 
            this.labelControl16.Location = new System.Drawing.Point(5, 23);
            this.labelControl16.Name = "labelControl16";
            this.labelControl16.Size = new System.Drawing.Size(59, 13);
            this.labelControl16.TabIndex = 61;
            this.labelControl16.Text = "Route Name";
            // 
            // txtTFare
            // 
            this.txtTFare.Location = new System.Drawing.Point(473, 42);
            this.txtTFare.Name = "txtTFare";
            this.txtTFare.Size = new System.Drawing.Size(150, 20);
            this.txtTFare.TabIndex = 68;
            // 
            // txtTDes
            // 
            this.txtTDes.Location = new System.Drawing.Point(317, 42);
            this.txtTDes.Name = "txtTDes";
            this.txtTDes.Size = new System.Drawing.Size(150, 20);
            this.txtTDes.TabIndex = 66;
            // 
            // txtTVehicle
            // 
            this.txtTVehicle.Location = new System.Drawing.Point(161, 42);
            this.txtTVehicle.Name = "txtTVehicle";
            this.txtTVehicle.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.txtTVehicle.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.txtTVehicle.Size = new System.Drawing.Size(150, 20);
            this.txtTVehicle.TabIndex = 64;
            // 
            // btnstop
            // 
            this.btnstop.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("simpleButton1.ImageOptions.Image")));
            this.btnstop.Location = new System.Drawing.Point(1006, 39);
            this.btnstop.Name = "btnstop";
            this.btnstop.Size = new System.Drawing.Size(83, 23);
            this.btnstop.TabIndex = 71;
            this.btnstop.Text = "Add Stop";
            this.btnstop.Click += new System.EventHandler(this.btnstop_Click);
            // 
            // labelControl2
            // 
            this.labelControl2.Location = new System.Drawing.Point(782, 23);
            this.labelControl2.Name = "labelControl2";
            this.labelControl2.Size = new System.Drawing.Size(23, 13);
            this.labelControl2.TabIndex = 65;
            this.labelControl2.Text = "Rout";
            // 
            // routcombo
            // 
            this.routcombo.EditValue = "";
            this.routcombo.Location = new System.Drawing.Point(782, 42);
            this.routcombo.Name = "routcombo";
            this.routcombo.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.routcombo.Properties.NullText = "";
            this.routcombo.Properties.PopupSizeable = false;
            this.routcombo.Properties.PopupView = this.gridLookUpEdit1View;
            this.routcombo.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.Standard;
            this.routcombo.Size = new System.Drawing.Size(104, 20);
            this.routcombo.TabIndex = 73;
            // 
            // gridLookUpEdit1View
            // 
            this.gridLookUpEdit1View.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus;
            this.gridLookUpEdit1View.Name = "gridLookUpEdit1View";
            this.gridLookUpEdit1View.OptionsSelection.EnableAppearanceFocusedCell = false;
            this.gridLookUpEdit1View.OptionsView.ShowGroupPanel = false;
            // 
            // txtstops
            // 
            this.txtstops.EditValue = "";
            this.txtstops.Location = new System.Drawing.Point(896, 42);
            this.txtstops.Name = "txtstops";
            this.txtstops.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.txtstops.Properties.NullText = "";
            this.txtstops.Properties.PopupSizeable = false;
            this.txtstops.Properties.PopupView = this.gridView1;
            this.txtstops.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.Standard;
            this.txtstops.Size = new System.Drawing.Size(104, 20);
            this.txtstops.TabIndex = 73;
            // 
            // gridView1
            // 
            this.gridView1.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus;
            this.gridView1.Name = "gridView1";
            this.gridView1.OptionsSelection.EnableAppearanceFocusedCell = false;
            this.gridView1.OptionsView.ShowGroupPanel = false;
            // 
            // Transport
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.gridTransport);
            this.Controls.Add(this.groupControl3);
            this.Name = "Transport";
            this.Size = new System.Drawing.Size(1093, 553);
            ((System.ComponentModel.ISupportInitialize)(this.gridTransport)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl3)).EndInit();
            this.groupControl3.ResumeLayout(false);
            this.groupControl3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtTName.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtTFare.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtTDes.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtTVehicle.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.routcombo.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridLookUpEdit1View)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtstops.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraGrid.GridControl gridTransport;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView3;
        private DevExpress.XtraEditors.GroupControl groupControl3;
        private DevExpress.XtraEditors.SimpleButton btnTDelete;
        private DevExpress.XtraEditors.SimpleButton btnTAdd;
        private DevExpress.XtraEditors.LabelControl labelControl13;
        private DevExpress.XtraEditors.LabelControl labelControl14;
        private DevExpress.XtraEditors.LabelControl labelControl15;
        private DevExpress.XtraEditors.TextEdit txtTName;
        private DevExpress.XtraEditors.LabelControl labelControl16;
        private DevExpress.XtraEditors.TextEdit txtTFare;
        private DevExpress.XtraEditors.TextEdit txtTDes;
        private DevExpress.XtraEditors.TextEdit txtTVehicle;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.XtraEditors.SimpleButton btnstop;
        private DevExpress.XtraEditors.LabelControl labelControl2;
        private DevExpress.XtraEditors.GridLookUpEdit routcombo;
        private DevExpress.XtraGrid.Views.Grid.GridView gridLookUpEdit1View;
        private DevExpress.XtraEditors.GridLookUpEdit txtstops;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView1;
    }
}
