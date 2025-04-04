namespace SchoolManagementSystem.Class
{
    partial class teacher_card
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

        #region Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(teacher_card));
            DevExpress.XtraPrinting.BarCode.Code128Generator code128Generator1 = new DevExpress.XtraPrinting.BarCode.Code128Generator();
            this.Detail = new DevExpress.XtraReports.UI.DetailBand();
            this.card_panel = new DevExpress.XtraReports.UI.XRPanel();
            this.xrLabel5 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel2 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel1 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel4 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel3 = new DevExpress.XtraReports.UI.XRLabel();
            this.lbl_designation = new DevExpress.XtraReports.UI.XRLabel();
            this.lbl_system_address = new DevExpress.XtraReports.UI.XRLabel();
            this.xrPictureBox1 = new DevExpress.XtraReports.UI.XRPictureBox();
            this.lbl_staff_type = new DevExpress.XtraReports.UI.XRLabel();
            this.PicStdBox = new DevExpress.XtraReports.UI.XRPictureBox();
            this.lbl_card_id = new DevExpress.XtraReports.UI.XRBarCode();
            this.TopMargin = new DevExpress.XtraReports.UI.TopMarginBand();
            this.BottomMargin = new DevExpress.XtraReports.UI.BottomMarginBand();
            this.customInstaller1 = new MySql.Data.MySqlClient.CustomInstaller();
            this.examTableAdapter1 = new SchoolManagementSystem.tnsbay_schoolDataSetTableAdapters.examTableAdapter();
            this.examTableAdapter2 = new SchoolManagementSystem.tnsbay_schoolDataSetTableAdapters.examTableAdapter();
            this.lbl_system_title = new DevExpress.XtraReports.UI.XRLabel();
            this.lbl_name = new DevExpress.XtraReports.UI.XRLabel();
            this.objectDataSource1 = new DevExpress.DataAccess.ObjectBinding.ObjectDataSource(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.objectDataSource1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
            // 
            // Detail
            // 
            this.Detail.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.card_panel});
            this.Detail.HeightF = 210.7917F;
            this.Detail.KeepTogether = true;
            this.Detail.MultiColumn.ColumnCount = 2;
            this.Detail.MultiColumn.ColumnSpacing = 0.2F;
            this.Detail.MultiColumn.ColumnWidth = 125F;
            this.Detail.MultiColumn.Layout = DevExpress.XtraPrinting.ColumnLayout.AcrossThenDown;
            this.Detail.MultiColumn.Mode = DevExpress.XtraReports.UI.MultiColumnMode.UseColumnCount;
            this.Detail.Name = "Detail";
            this.Detail.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 100F);
            this.Detail.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft;
            // 
            // card_panel
            // 
            this.card_panel.BackColor = System.Drawing.Color.MediumSpringGreen;
            this.card_panel.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top) 
            | DevExpress.XtraPrinting.BorderSide.Right) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.card_panel.BorderWidth = 0F;
            this.card_panel.CanGrow = false;
            this.card_panel.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.lbl_name,
            this.lbl_system_title,
            this.xrLabel5,
            this.xrLabel2,
            this.xrLabel1,
            this.xrLabel4,
            this.xrLabel3,
            this.lbl_designation,
            this.lbl_system_address,
            this.xrPictureBox1,
            this.lbl_staff_type,
            this.PicStdBox,
            this.lbl_card_id});
            this.card_panel.LocationFloat = new DevExpress.Utils.PointFloat(9.999994F, 0F);
            this.card_panel.Name = "card_panel";
            this.card_panel.SizeF = new System.Drawing.SizeF(353.9F, 204.5417F);
            this.card_panel.StylePriority.UseBackColor = false;
            this.card_panel.StylePriority.UseBorderWidth = false;
            // 
            // xrLabel5
            // 
            this.xrLabel5.BackColor = System.Drawing.Color.Transparent;
            this.xrLabel5.BorderColor = System.Drawing.Color.Transparent;
            this.xrLabel5.CanShrink = true;
            this.xrLabel5.Font = new System.Drawing.Font("Sitka Small", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xrLabel5.LocationFloat = new DevExpress.Utils.PointFloat(10F, 98.70839F);
            this.xrLabel5.Name = "xrLabel5";
            this.xrLabel5.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 100F);
            this.xrLabel5.SizeF = new System.Drawing.SizeF(93.49998F, 14.95836F);
            this.xrLabel5.StylePriority.UseBackColor = false;
            this.xrLabel5.StylePriority.UseBorderColor = false;
            this.xrLabel5.StylePriority.UseFont = false;
            this.xrLabel5.StylePriority.UsePadding = false;
            this.xrLabel5.StylePriority.UseTextAlignment = false;
            this.xrLabel5.Text = "Designation";
            this.xrLabel5.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            // 
            // xrLabel2
            // 
            this.xrLabel2.BackColor = System.Drawing.Color.Transparent;
            this.xrLabel2.BorderColor = System.Drawing.Color.Transparent;
            this.xrLabel2.CanShrink = true;
            this.xrLabel2.Font = new System.Drawing.Font("Sitka Small", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xrLabel2.LocationFloat = new DevExpress.Utils.PointFloat(10F, 114.6667F);
            this.xrLabel2.Name = "xrLabel2";
            this.xrLabel2.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 100F);
            this.xrLabel2.SizeF = new System.Drawing.SizeF(93.49997F, 14.95835F);
            this.xrLabel2.StylePriority.UseBackColor = false;
            this.xrLabel2.StylePriority.UseBorderColor = false;
            this.xrLabel2.StylePriority.UseFont = false;
            this.xrLabel2.StylePriority.UsePadding = false;
            this.xrLabel2.StylePriority.UseTextAlignment = false;
            this.xrLabel2.Text = "Gender";
            this.xrLabel2.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            // 
            // xrLabel1
            // 
            this.xrLabel1.BackColor = System.Drawing.Color.Transparent;
            this.xrLabel1.BorderColor = System.Drawing.Color.Transparent;
            this.xrLabel1.CanShrink = true;
            this.xrLabel1.Font = new System.Drawing.Font("Sitka Small", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xrLabel1.LocationFloat = new DevExpress.Utils.PointFloat(10F, 130.6251F);
            this.xrLabel1.Name = "xrLabel1";
            this.xrLabel1.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 100F);
            this.xrLabel1.SizeF = new System.Drawing.SizeF(93.49997F, 15.45825F);
            this.xrLabel1.StylePriority.UseBackColor = false;
            this.xrLabel1.StylePriority.UseBorderColor = false;
            this.xrLabel1.StylePriority.UseFont = false;
            this.xrLabel1.StylePriority.UsePadding = false;
            this.xrLabel1.StylePriority.UseTextAlignment = false;
            this.xrLabel1.Text = "Joining";
            this.xrLabel1.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            // 
            // xrLabel4
            // 
            this.xrLabel4.BackColor = System.Drawing.Color.Transparent;
            this.xrLabel4.BorderColor = System.Drawing.Color.Transparent;
            this.xrLabel4.CanGrow = false;
            this.xrLabel4.ExpressionBindings.AddRange(new DevExpress.XtraReports.UI.ExpressionBinding[] {
            new DevExpress.XtraReports.UI.ExpressionBinding("BeforePrint", "Text", "[JoiningDate]")});
            this.xrLabel4.Font = new System.Drawing.Font("Sitka Small", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xrLabel4.LocationFloat = new DevExpress.Utils.PointFloat(103.5F, 130.6251F);
            this.xrLabel4.Name = "xrLabel4";
            this.xrLabel4.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 100F);
            this.xrLabel4.SizeF = new System.Drawing.SizeF(130.3584F, 15.45827F);
            this.xrLabel4.StylePriority.UseBackColor = false;
            this.xrLabel4.StylePriority.UseBorderColor = false;
            this.xrLabel4.StylePriority.UseFont = false;
            this.xrLabel4.StylePriority.UsePadding = false;
            this.xrLabel4.StylePriority.UseTextAlignment = false;
            this.xrLabel4.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            this.xrLabel4.WordWrap = false;
            // 
            // xrLabel3
            // 
            this.xrLabel3.BackColor = System.Drawing.Color.Transparent;
            this.xrLabel3.BorderColor = System.Drawing.Color.Transparent;
            this.xrLabel3.CanGrow = false;
            this.xrLabel3.ExpressionBindings.AddRange(new DevExpress.XtraReports.UI.ExpressionBinding[] {
            new DevExpress.XtraReports.UI.ExpressionBinding("BeforePrint", "Text", "[gender]")});
            this.xrLabel3.Font = new System.Drawing.Font("Sitka Small", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xrLabel3.LocationFloat = new DevExpress.Utils.PointFloat(103.5F, 114.6667F);
            this.xrLabel3.Name = "xrLabel3";
            this.xrLabel3.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 100F);
            this.xrLabel3.SizeF = new System.Drawing.SizeF(130.3584F, 14.95837F);
            this.xrLabel3.StylePriority.UseBackColor = false;
            this.xrLabel3.StylePriority.UseBorderColor = false;
            this.xrLabel3.StylePriority.UseFont = false;
            this.xrLabel3.StylePriority.UsePadding = false;
            this.xrLabel3.StylePriority.UseTextAlignment = false;
            this.xrLabel3.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            this.xrLabel3.WordWrap = false;
            // 
            // lbl_designation
            // 
            this.lbl_designation.BackColor = System.Drawing.Color.Transparent;
            this.lbl_designation.BorderColor = System.Drawing.Color.Transparent;
            this.lbl_designation.CanGrow = false;
            this.lbl_designation.ExpressionBindings.AddRange(new DevExpress.XtraReports.UI.ExpressionBinding[] {
            new DevExpress.XtraReports.UI.ExpressionBinding("BeforePrint", "Text", "[designation]")});
            this.lbl_designation.Font = new System.Drawing.Font("Sitka Small", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_designation.LocationFloat = new DevExpress.Utils.PointFloat(103.5F, 98.70839F);
            this.lbl_designation.Name = "lbl_designation";
            this.lbl_designation.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 100F);
            this.lbl_designation.SizeF = new System.Drawing.SizeF(130.3584F, 14.95836F);
            this.lbl_designation.StylePriority.UseBackColor = false;
            this.lbl_designation.StylePriority.UseBorderColor = false;
            this.lbl_designation.StylePriority.UseFont = false;
            this.lbl_designation.StylePriority.UsePadding = false;
            this.lbl_designation.StylePriority.UseTextAlignment = false;
            this.lbl_designation.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            this.lbl_designation.WordWrap = false;
            // 
            // lbl_system_address
            // 
            this.lbl_system_address.BackColor = System.Drawing.Color.White;
            this.lbl_system_address.BorderColor = System.Drawing.Color.White;
            this.lbl_system_address.ExpressionBindings.AddRange(new DevExpress.XtraReports.UI.ExpressionBinding[] {
            new DevExpress.XtraReports.UI.ExpressionBinding("BeforePrint", "Text", "[system_address]")});
            this.lbl_system_address.Font = new System.Drawing.Font("Microsoft JhengHei UI", 7F, System.Drawing.FontStyle.Bold);
            this.lbl_system_address.ForeColor = System.Drawing.Color.Black;
            this.lbl_system_address.KeepTogether = true;
            this.lbl_system_address.LocationFloat = new DevExpress.Utils.PointFloat(0.9999695F, 185.4167F);
            this.lbl_system_address.Multiline = true;
            this.lbl_system_address.Name = "lbl_system_address";
            this.lbl_system_address.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 100F);
            this.lbl_system_address.SizeF = new System.Drawing.SizeF(351.9F, 19F);
            this.lbl_system_address.StylePriority.UseBackColor = false;
            this.lbl_system_address.StylePriority.UseBorderColor = false;
            this.lbl_system_address.StylePriority.UseFont = false;
            this.lbl_system_address.StylePriority.UseForeColor = false;
            this.lbl_system_address.StylePriority.UsePadding = false;
            this.lbl_system_address.StylePriority.UseTextAlignment = false;
            this.lbl_system_address.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            // 
            // xrPictureBox1
            // 
            this.xrPictureBox1.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top) 
            | DevExpress.XtraPrinting.BorderSide.Right) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrPictureBox1.BorderWidth = 0F;
            this.xrPictureBox1.ExpressionBindings.AddRange(new DevExpress.XtraReports.UI.ExpressionBinding[] {
            new DevExpress.XtraReports.UI.ExpressionBinding("BeforePrint", "ImageSource", "[system_logo]")});
            this.xrPictureBox1.ImageSource = new DevExpress.XtraPrinting.Drawing.ImageSource("img", resources.GetString("xrPictureBox1.ImageSource"));
            this.xrPictureBox1.LocationFloat = new DevExpress.Utils.PointFloat(76.41666F, 21.41666F);
            this.xrPictureBox1.Name = "xrPictureBox1";
            this.xrPictureBox1.SizeF = new System.Drawing.SizeF(91.62486F, 58.7917F);
            this.xrPictureBox1.Sizing = DevExpress.XtraPrinting.ImageSizeMode.StretchImage;
            this.xrPictureBox1.StylePriority.UseBorders = false;
            this.xrPictureBox1.StylePriority.UseBorderWidth = false;
            // 
            // lbl_staff_type
            // 
            this.lbl_staff_type.BackColor = System.Drawing.Color.White;
            this.lbl_staff_type.BorderColor = System.Drawing.Color.Transparent;
            this.lbl_staff_type.CanGrow = false;
            this.lbl_staff_type.ExpressionBindings.AddRange(new DevExpress.XtraReports.UI.ExpressionBinding[] {
            new DevExpress.XtraReports.UI.ExpressionBinding("BeforePrint", "Text", "[staff_type]")});
            this.lbl_staff_type.Font = new System.Drawing.Font("Arial", 8F, System.Drawing.FontStyle.Bold);
            this.lbl_staff_type.LocationFloat = new DevExpress.Utils.PointFloat(234.8584F, 2.000014F);
            this.lbl_staff_type.Name = "lbl_staff_type";
            this.lbl_staff_type.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 100F);
            this.lbl_staff_type.SizeF = new System.Drawing.SizeF(115.0417F, 16.95836F);
            this.lbl_staff_type.StylePriority.UseBackColor = false;
            this.lbl_staff_type.StylePriority.UseBorderColor = false;
            this.lbl_staff_type.StylePriority.UseFont = false;
            this.lbl_staff_type.StylePriority.UsePadding = false;
            this.lbl_staff_type.StylePriority.UseTextAlignment = false;
            this.lbl_staff_type.Text = "Student Card";
            this.lbl_staff_type.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            this.lbl_staff_type.WordWrap = false;
            // 
            // PicStdBox
            // 
            this.PicStdBox.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top) 
            | DevExpress.XtraPrinting.BorderSide.Right) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.PicStdBox.BorderWidth = 0F;
            this.PicStdBox.ExpressionBindings.AddRange(new DevExpress.XtraReports.UI.ExpressionBinding[] {
            new DevExpress.XtraReports.UI.ExpressionBinding("BeforePrint", "ImageSource", "[image_url]")});
            this.PicStdBox.ImageSource = new DevExpress.XtraPrinting.Drawing.ImageSource("img", resources.GetString("PicStdBox.ImageSource"));
            this.PicStdBox.LocationFloat = new DevExpress.Utils.PointFloat(233.8584F, 24.95835F);
            this.PicStdBox.Name = "PicStdBox";
            this.PicStdBox.SizeF = new System.Drawing.SizeF(114.0416F, 95.70837F);
            this.PicStdBox.Sizing = DevExpress.XtraPrinting.ImageSizeMode.StretchImage;
            this.PicStdBox.StylePriority.UseBorders = false;
            this.PicStdBox.StylePriority.UseBorderWidth = false;
            // 
            // lbl_card_id
            // 
            this.lbl_card_id.AutoModule = true;
            this.lbl_card_id.BackColor = System.Drawing.Color.Transparent;
            this.lbl_card_id.BorderWidth = 0F;
            this.lbl_card_id.ExpressionBindings.AddRange(new DevExpress.XtraReports.UI.ExpressionBinding[] {
            new DevExpress.XtraReports.UI.ExpressionBinding("BeforePrint", "Text", "[card_id]")});
            this.lbl_card_id.Font = new System.Drawing.Font("Tahoma", 8.25F);
            this.lbl_card_id.LocationFloat = new DevExpress.Utils.PointFloat(10F, 147.0834F);
            this.lbl_card_id.Name = "lbl_card_id";
            this.lbl_card_id.Padding = new DevExpress.XtraPrinting.PaddingInfo(10, 10, 0, 0, 100F);
            this.lbl_card_id.SizeF = new System.Drawing.SizeF(223.8584F, 29.41681F);
            this.lbl_card_id.StylePriority.UseBackColor = false;
            this.lbl_card_id.StylePriority.UseBorderWidth = false;
            this.lbl_card_id.StylePriority.UseFont = false;
            this.lbl_card_id.StylePriority.UseTextAlignment = false;
            this.lbl_card_id.Symbology = code128Generator1;
            this.lbl_card_id.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            // 
            // TopMargin
            // 
            this.TopMargin.HeightF = 7.291667F;
            this.TopMargin.Name = "TopMargin";
            this.TopMargin.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 100F);
            this.TopMargin.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft;
            // 
            // BottomMargin
            // 
            this.BottomMargin.HeightF = 8.333365F;
            this.BottomMargin.Name = "BottomMargin";
            this.BottomMargin.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 100F);
            this.BottomMargin.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft;
            // 
            // examTableAdapter1
            // 
            this.examTableAdapter1.ClearBeforeFill = true;
            // 
            // examTableAdapter2
            // 
            this.examTableAdapter2.ClearBeforeFill = true;
            // 
            // lbl_system_title
            // 
            this.lbl_system_title.BackColor = System.Drawing.Color.White;
            this.lbl_system_title.BorderColor = System.Drawing.Color.Transparent;
            this.lbl_system_title.CanGrow = false;
            this.lbl_system_title.ExpressionBindings.AddRange(new DevExpress.XtraReports.UI.ExpressionBinding[] {
            new DevExpress.XtraReports.UI.ExpressionBinding("BeforePrint", "Text", "[system_title]")});
            this.lbl_system_title.Font = new System.Drawing.Font("Arial", 8F, System.Drawing.FontStyle.Bold);
            this.lbl_system_title.LocationFloat = new DevExpress.Utils.PointFloat(2.416794F, 2.000014F);
            this.lbl_system_title.Name = "lbl_system_title";
            this.lbl_system_title.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 100F);
            this.lbl_system_title.SizeF = new System.Drawing.SizeF(232.4416F, 16.95836F);
            this.lbl_system_title.StylePriority.UseBackColor = false;
            this.lbl_system_title.StylePriority.UseBorderColor = false;
            this.lbl_system_title.StylePriority.UseFont = false;
            this.lbl_system_title.StylePriority.UsePadding = false;
            this.lbl_system_title.StylePriority.UseTextAlignment = false;
            this.lbl_system_title.Text = "Student Card";
            this.lbl_system_title.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            this.lbl_system_title.WordWrap = false;
            // 
            // lbl_name
            // 
            this.lbl_name.BackColor = System.Drawing.Color.Transparent;
            this.lbl_name.BorderColor = System.Drawing.Color.Black;
            this.lbl_name.Borders = DevExpress.XtraPrinting.BorderSide.Bottom;
            this.lbl_name.BorderWidth = 1F;
            this.lbl_name.CanGrow = false;
            this.lbl_name.ExpressionBindings.AddRange(new DevExpress.XtraReports.UI.ExpressionBinding[] {
            new DevExpress.XtraReports.UI.ExpressionBinding("BeforePrint", "Text", "[Name]")});
            this.lbl_name.Font = new System.Drawing.Font("Sitka Small", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_name.LocationFloat = new DevExpress.Utils.PointFloat(10F, 80.75002F);
            this.lbl_name.Name = "lbl_name";
            this.lbl_name.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 100F);
            this.lbl_name.SizeF = new System.Drawing.SizeF(223.0416F, 14.95836F);
            this.lbl_name.StylePriority.UseBackColor = false;
            this.lbl_name.StylePriority.UseBorderColor = false;
            this.lbl_name.StylePriority.UseBorders = false;
            this.lbl_name.StylePriority.UseBorderWidth = false;
            this.lbl_name.StylePriority.UseFont = false;
            this.lbl_name.StylePriority.UsePadding = false;
            this.lbl_name.StylePriority.UseTextAlignment = false;
            this.lbl_name.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            this.lbl_name.WordWrap = false;
            // 
            // objectDataSource1
            // 
            this.objectDataSource1.DataSource = typeof(SchoolManagementSystem.Class.teachercard);
            this.objectDataSource1.Name = "objectDataSource1";
            // 
            // teacher_card
            // 
            this.Bands.AddRange(new DevExpress.XtraReports.UI.Band[] {
            this.Detail,
            this.TopMargin,
            this.BottomMargin});
            this.ComponentStorage.AddRange(new System.ComponentModel.IComponent[] {
            this.objectDataSource1});
            this.DataAdapter = this.examTableAdapter1;
            this.DataSource = this.objectDataSource1;
            this.Margins = new System.Drawing.Printing.Margins(35, 36, 7, 8);
            this.PageHeight = 1169;
            this.PageWidth = 827;
            this.PaperKind = System.Drawing.Printing.PaperKind.A4;
            this.ReportPrintOptions.DetailCountOnEmptyDataSource = 6;
            this.Version = "20.1";
            this.Watermark.ShowBehind = false;
            this.Watermark.TextTransparency = 0;
            ((System.ComponentModel.ISupportInitialize)(this.objectDataSource1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this)).EndInit();

        }

        #endregion

        private DevExpress.XtraReports.UI.DetailBand Detail;
        private DevExpress.XtraReports.UI.TopMarginBand TopMargin;
        private DevExpress.XtraReports.UI.BottomMarginBand BottomMargin;
        private MySql.Data.MySqlClient.CustomInstaller customInstaller1;
        private tnsbay_schoolDataSetTableAdapters.examTableAdapter examTableAdapter1;
        private tnsbay_schoolDataSetTableAdapters.examTableAdapter examTableAdapter2;
        private DevExpress.DataAccess.ObjectBinding.ObjectDataSource objectDataSource1;
        public DevExpress.XtraReports.UI.XRPanel card_panel;
        public DevExpress.XtraReports.UI.XRLabel xrLabel5;
        public DevExpress.XtraReports.UI.XRLabel xrLabel2;
        public DevExpress.XtraReports.UI.XRLabel xrLabel1;
        public DevExpress.XtraReports.UI.XRLabel xrLabel4;
        public DevExpress.XtraReports.UI.XRLabel xrLabel3;
        public DevExpress.XtraReports.UI.XRLabel lbl_designation;
        public DevExpress.XtraReports.UI.XRLabel lbl_system_address;
        public DevExpress.XtraReports.UI.XRPictureBox xrPictureBox1;
        public DevExpress.XtraReports.UI.XRLabel lbl_staff_type;
        public DevExpress.XtraReports.UI.XRPictureBox PicStdBox;
        public DevExpress.XtraReports.UI.XRBarCode lbl_card_id;
        public DevExpress.XtraReports.UI.XRLabel lbl_system_title;
        public DevExpress.XtraReports.UI.XRLabel lbl_name;
    }
}
