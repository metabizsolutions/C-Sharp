namespace SchoolManagementSystem.Students
{
    partial class student_card
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(student_card));
            DevExpress.XtraPrinting.BarCode.Code128Generator code128Generator1 = new DevExpress.XtraPrinting.BarCode.Code128Generator();
            this.Detail = new DevExpress.XtraReports.UI.DetailBand();
            this.card_panel = new DevExpress.XtraReports.UI.XRPanel();
            this.lbl_name = new DevExpress.XtraReports.UI.XRLabel();
            this.lbl_card = new DevExpress.XtraReports.UI.XRLabel();
            this.lbl_system_address = new DevExpress.XtraReports.UI.XRLabel();
            this.xrPictureBox1 = new DevExpress.XtraReports.UI.XRPictureBox();
            this.lbl_std_id = new DevExpress.XtraReports.UI.XRLabel();
            this.lbl_roll = new DevExpress.XtraReports.UI.XRLabel();
            this.lbl_class = new DevExpress.XtraReports.UI.XRLabel();
            this.lbl_system_title = new DevExpress.XtraReports.UI.XRLabel();
            this.PicStdBox = new DevExpress.XtraReports.UI.XRPictureBox();
            this.lbl_section = new DevExpress.XtraReports.UI.XRLabel();
            this.lbl_card_id = new DevExpress.XtraReports.UI.XRBarCode();
            this.TopMargin = new DevExpress.XtraReports.UI.TopMarginBand();
            this.BottomMargin = new DevExpress.XtraReports.UI.BottomMarginBand();
            this.objectDataSource1 = new DevExpress.DataAccess.ObjectBinding.ObjectDataSource(this.components);
            this.customInstaller1 = new MySql.Data.MySqlClient.CustomInstaller();
            this.examTableAdapter1 = new SchoolManagementSystem.tnsbay_schoolDataSetTableAdapters.examTableAdapter();
            this.examTableAdapter2 = new SchoolManagementSystem.tnsbay_schoolDataSetTableAdapters.examTableAdapter();
            this.objectDataSource2 = new DevExpress.DataAccess.ObjectBinding.ObjectDataSource(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.objectDataSource1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.objectDataSource2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
            // 
            // Detail
            // 
            this.Detail.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.card_panel});
            this.Detail.HeightF = 213.5416F;
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
            this.lbl_card,
            this.lbl_system_address,
            this.xrPictureBox1,
            this.lbl_std_id,
            this.lbl_roll,
            this.lbl_class,
            this.lbl_system_title,
            this.PicStdBox,
            this.lbl_section,
            this.lbl_card_id});
            this.card_panel.LocationFloat = new DevExpress.Utils.PointFloat(9.999998F, 0F);
            this.card_panel.Name = "card_panel";
            this.card_panel.SizeF = new System.Drawing.SizeF(353.9F, 204.5417F);
            this.card_panel.StylePriority.UseBackColor = false;
            this.card_panel.StylePriority.UseBorderWidth = false;
            // 
            // lbl_name
            // 
            this.lbl_name.BackColor = System.Drawing.Color.Transparent;
            this.lbl_name.BorderColor = System.Drawing.Color.Transparent;
            this.lbl_name.CanGrow = false;
            this.lbl_name.ExpressionBindings.AddRange(new DevExpress.XtraReports.UI.ExpressionBinding[] {
            new DevExpress.XtraReports.UI.ExpressionBinding("BeforePrint", "Text", "[Name]")});
            this.lbl_name.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_name.LocationFloat = new DevExpress.Utils.PointFloat(10F, 166.4167F);
            this.lbl_name.Name = "lbl_name";
            this.lbl_name.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 100F);
            this.lbl_name.SizeF = new System.Drawing.SizeF(169.4999F, 18.95836F);
            this.lbl_name.StylePriority.UseBackColor = false;
            this.lbl_name.StylePriority.UseBorderColor = false;
            this.lbl_name.StylePriority.UseFont = false;
            this.lbl_name.StylePriority.UsePadding = false;
            this.lbl_name.StylePriority.UseTextAlignment = false;
            this.lbl_name.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            this.lbl_name.WordWrap = false;
            // 
            // lbl_card
            // 
            this.lbl_card.AutoWidth = true;
            this.lbl_card.BorderColor = System.Drawing.Color.Transparent;
            this.lbl_card.CanShrink = true;
            this.lbl_card.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_card.ForeColor = System.Drawing.Color.Black;
            this.lbl_card.KeepTogether = true;
            this.lbl_card.LocationFloat = new DevExpress.Utils.PointFloat(123.2753F, 30.04152F);
            this.lbl_card.Multiline = true;
            this.lbl_card.Name = "lbl_card";
            this.lbl_card.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 100F);
            this.lbl_card.SizeF = new System.Drawing.SizeF(109.0832F, 19.125F);
            this.lbl_card.StylePriority.UseBorderColor = false;
            this.lbl_card.StylePriority.UseFont = false;
            this.lbl_card.StylePriority.UseForeColor = false;
            this.lbl_card.StylePriority.UsePadding = false;
            this.lbl_card.StylePriority.UseTextAlignment = false;
            this.lbl_card.Text = "Student Card";
            this.lbl_card.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            // 
            // lbl_system_address
            // 
            this.lbl_system_address.BackColor = System.Drawing.Color.White;
            this.lbl_system_address.BorderColor = System.Drawing.Color.White;
            this.lbl_system_address.CanGrow = false;
            this.lbl_system_address.ExpressionBindings.AddRange(new DevExpress.XtraReports.UI.ExpressionBinding[] {
            new DevExpress.XtraReports.UI.ExpressionBinding("BeforePrint", "Text", "[system_address]")});
            this.lbl_system_address.Font = new System.Drawing.Font("Microsoft JhengHei UI", 7F, System.Drawing.FontStyle.Bold);
            this.lbl_system_address.ForeColor = System.Drawing.Color.Black;
            this.lbl_system_address.KeepTogether = true;
            this.lbl_system_address.LocationFloat = new DevExpress.Utils.PointFloat(0.9999695F, 185.4167F);
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
            this.lbl_system_address.WordWrap = false;
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
            this.xrPictureBox1.LocationFloat = new DevExpress.Utils.PointFloat(216.4419F, 53.16653F);
            this.xrPictureBox1.Name = "xrPictureBox1";
            this.xrPictureBox1.SizeF = new System.Drawing.SizeF(103.0832F, 74.4167F);
            this.xrPictureBox1.Sizing = DevExpress.XtraPrinting.ImageSizeMode.StretchImage;
            this.xrPictureBox1.StylePriority.UseBorders = false;
            this.xrPictureBox1.StylePriority.UseBorderWidth = false;
            // 
            // lbl_std_id
            // 
            this.lbl_std_id.BorderColor = System.Drawing.Color.Black;
            this.lbl_std_id.Borders = ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top)));
            this.lbl_std_id.BorderWidth = 1F;
            this.lbl_std_id.CanGrow = false;
            this.lbl_std_id.ExpressionBindings.AddRange(new DevExpress.XtraReports.UI.ExpressionBinding[] {
            new DevExpress.XtraReports.UI.ExpressionBinding("BeforePrint", "Text", "[student_id]")});
            this.lbl_std_id.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_std_id.ForeColor = System.Drawing.Color.Black;
            this.lbl_std_id.LocationFloat = new DevExpress.Utils.PointFloat(188.6663F, 129.5832F);
            this.lbl_std_id.Name = "lbl_std_id";
            this.lbl_std_id.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 100F);
            this.lbl_std_id.SizeF = new System.Drawing.SizeF(75.98389F, 20.00003F);
            this.lbl_std_id.StylePriority.UseBorderColor = false;
            this.lbl_std_id.StylePriority.UseBorders = false;
            this.lbl_std_id.StylePriority.UseBorderWidth = false;
            this.lbl_std_id.StylePriority.UseFont = false;
            this.lbl_std_id.StylePriority.UseForeColor = false;
            this.lbl_std_id.StylePriority.UsePadding = false;
            this.lbl_std_id.StylePriority.UseTextAlignment = false;
            this.lbl_std_id.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            this.lbl_std_id.WordWrap = false;
            // 
            // lbl_roll
            // 
            this.lbl_roll.BorderColor = System.Drawing.Color.Black;
            this.lbl_roll.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top) 
            | DevExpress.XtraPrinting.BorderSide.Right)));
            this.lbl_roll.BorderWidth = 1F;
            this.lbl_roll.CanGrow = false;
            this.lbl_roll.ExpressionBindings.AddRange(new DevExpress.XtraReports.UI.ExpressionBinding[] {
            new DevExpress.XtraReports.UI.ExpressionBinding("BeforePrint", "Text", "[roll]")});
            this.lbl_roll.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_roll.ForeColor = System.Drawing.Color.Black;
            this.lbl_roll.LocationFloat = new DevExpress.Utils.PointFloat(264.6502F, 129.5832F);
            this.lbl_roll.Name = "lbl_roll";
            this.lbl_roll.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 100F);
            this.lbl_roll.SizeF = new System.Drawing.SizeF(77.24982F, 20.00003F);
            this.lbl_roll.StylePriority.UseBorderColor = false;
            this.lbl_roll.StylePriority.UseBorders = false;
            this.lbl_roll.StylePriority.UseBorderWidth = false;
            this.lbl_roll.StylePriority.UseFont = false;
            this.lbl_roll.StylePriority.UseForeColor = false;
            this.lbl_roll.StylePriority.UsePadding = false;
            this.lbl_roll.StylePriority.UseTextAlignment = false;
            this.lbl_roll.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            this.lbl_roll.WordWrap = false;
            // 
            // lbl_class
            // 
            this.lbl_class.BorderColor = System.Drawing.Color.Black;
            this.lbl_class.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top) 
            | DevExpress.XtraPrinting.BorderSide.Right)));
            this.lbl_class.BorderWidth = 1F;
            this.lbl_class.CanGrow = false;
            this.lbl_class.ExpressionBindings.AddRange(new DevExpress.XtraReports.UI.ExpressionBinding[] {
            new DevExpress.XtraReports.UI.ExpressionBinding("BeforePrint", "Text", "[Class]")});
            this.lbl_class.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_class.ForeColor = System.Drawing.Color.Black;
            this.lbl_class.LocationFloat = new DevExpress.Utils.PointFloat(188.6663F, 149.5833F);
            this.lbl_class.Name = "lbl_class";
            this.lbl_class.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 100F);
            this.lbl_class.SizeF = new System.Drawing.SizeF(153.2337F, 20.00003F);
            this.lbl_class.StylePriority.UseBorderColor = false;
            this.lbl_class.StylePriority.UseBorders = false;
            this.lbl_class.StylePriority.UseBorderWidth = false;
            this.lbl_class.StylePriority.UseFont = false;
            this.lbl_class.StylePriority.UseForeColor = false;
            this.lbl_class.StylePriority.UsePadding = false;
            this.lbl_class.StylePriority.UseTextAlignment = false;
            this.lbl_class.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            this.lbl_class.WordWrap = false;
            // 
            // lbl_system_title
            // 
            this.lbl_system_title.BackColor = System.Drawing.Color.White;
            this.lbl_system_title.BorderColor = System.Drawing.Color.Transparent;
            this.lbl_system_title.CanShrink = true;
            this.lbl_system_title.ExpressionBindings.AddRange(new DevExpress.XtraReports.UI.ExpressionBinding[] {
            new DevExpress.XtraReports.UI.ExpressionBinding("BeforePrint", "Text", "[system_title]")});
            this.lbl_system_title.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_system_title.LocationFloat = new DevExpress.Utils.PointFloat(0.9999695F, 1.000005F);
            this.lbl_system_title.Name = "lbl_system_title";
            this.lbl_system_title.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 100F);
            this.lbl_system_title.SizeF = new System.Drawing.SizeF(351.9F, 26.95836F);
            this.lbl_system_title.StylePriority.UseBackColor = false;
            this.lbl_system_title.StylePriority.UseBorderColor = false;
            this.lbl_system_title.StylePriority.UseFont = false;
            this.lbl_system_title.StylePriority.UsePadding = false;
            this.lbl_system_title.StylePriority.UseTextAlignment = false;
            this.lbl_system_title.Text = "Student Card";
            this.lbl_system_title.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            this.lbl_system_title.WordWrap = false;
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
            this.PicStdBox.LocationFloat = new DevExpress.Utils.PointFloat(32.06694F, 53.16653F);
            this.PicStdBox.Name = "PicStdBox";
            this.PicStdBox.SizeF = new System.Drawing.SizeF(103.0832F, 74.4167F);
            this.PicStdBox.Sizing = DevExpress.XtraPrinting.ImageSizeMode.StretchImage;
            this.PicStdBox.StylePriority.UseBorders = false;
            this.PicStdBox.StylePriority.UseBorderWidth = false;
            // 
            // lbl_section
            // 
            this.lbl_section.BorderColor = System.Drawing.Color.Black;
            this.lbl_section.BorderWidth = 1F;
            this.lbl_section.CanGrow = false;
            this.lbl_section.ExpressionBindings.AddRange(new DevExpress.XtraReports.UI.ExpressionBinding[] {
            new DevExpress.XtraReports.UI.ExpressionBinding("BeforePrint", "Text", "[Section]")});
            this.lbl_section.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_section.ForeColor = System.Drawing.Color.Black;
            this.lbl_section.LocationFloat = new DevExpress.Utils.PointFloat(188.6663F, 169.5833F);
            this.lbl_section.Name = "lbl_section";
            this.lbl_section.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 100F);
            this.lbl_section.SizeF = new System.Drawing.SizeF(153.2336F, 15.83337F);
            this.lbl_section.StylePriority.UseBorderColor = false;
            this.lbl_section.StylePriority.UseBorderWidth = false;
            this.lbl_section.StylePriority.UseFont = false;
            this.lbl_section.StylePriority.UseForeColor = false;
            this.lbl_section.StylePriority.UsePadding = false;
            this.lbl_section.StylePriority.UseTextAlignment = false;
            this.lbl_section.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            this.lbl_section.WordWrap = false;
            // 
            // lbl_card_id
            // 
            this.lbl_card_id.AutoModule = true;
            this.lbl_card_id.BackColor = System.Drawing.Color.Transparent;
            this.lbl_card_id.BorderWidth = 0F;
            this.lbl_card_id.ExpressionBindings.AddRange(new DevExpress.XtraReports.UI.ExpressionBinding[] {
            new DevExpress.XtraReports.UI.ExpressionBinding("BeforePrint", "Text", "[card_id]")});
            this.lbl_card_id.Font = new System.Drawing.Font("Tahoma", 8.25F);
            this.lbl_card_id.LocationFloat = new DevExpress.Utils.PointFloat(9.999989F, 129.5832F);
            this.lbl_card_id.Name = "lbl_card_id";
            this.lbl_card_id.Padding = new DevExpress.XtraPrinting.PaddingInfo(10, 10, 0, 0, 100F);
            this.lbl_card_id.SizeF = new System.Drawing.SizeF(169.4999F, 36.83348F);
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
            // objectDataSource1
            // 
            this.objectDataSource1.DataSourceType = null;
            this.objectDataSource1.Name = "objectDataSource1";
            // 
            // examTableAdapter1
            // 
            this.examTableAdapter1.ClearBeforeFill = true;
            // 
            // examTableAdapter2
            // 
            this.examTableAdapter2.ClearBeforeFill = true;
            // 
            // objectDataSource2
            // 
            this.objectDataSource2.DataSource = typeof(SchoolManagementSystem.Students.studentcard);
            this.objectDataSource2.Name = "objectDataSource2";
            // 
            // student_card
            // 
            this.Bands.AddRange(new DevExpress.XtraReports.UI.Band[] {
            this.Detail,
            this.TopMargin,
            this.BottomMargin});
            this.ComponentStorage.AddRange(new System.ComponentModel.IComponent[] {
            this.objectDataSource2,
            this.objectDataSource1});
            this.DataAdapter = this.examTableAdapter1;
            this.DataSource = this.objectDataSource2;
            this.Margins = new System.Drawing.Printing.Margins(35, 36, 7, 8);
            this.PageHeight = 1169;
            this.PageWidth = 827;
            this.PaperKind = System.Drawing.Printing.PaperKind.A4;
            this.ReportPrintOptions.DetailCountOnEmptyDataSource = 6;
            this.Version = "20.1";
            this.Watermark.ShowBehind = false;
            this.Watermark.TextTransparency = 0;
            ((System.ComponentModel.ISupportInitialize)(this.objectDataSource1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.objectDataSource2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this)).EndInit();

        }

        #endregion

        private DevExpress.XtraReports.UI.DetailBand Detail;
        private DevExpress.XtraReports.UI.TopMarginBand TopMargin;
        private DevExpress.XtraReports.UI.BottomMarginBand BottomMargin;
        private DevExpress.DataAccess.ObjectBinding.ObjectDataSource objectDataSource1;
        private DevExpress.DataAccess.ObjectBinding.ObjectDataSource objectDataSource2;
        public DevExpress.XtraReports.UI.XRPictureBox PicStdBox;
        private MySql.Data.MySqlClient.CustomInstaller customInstaller1;
        public DevExpress.XtraReports.UI.XRPictureBox xrPictureBox1;
        public DevExpress.XtraReports.UI.XRPanel card_panel;
        private tnsbay_schoolDataSetTableAdapters.examTableAdapter examTableAdapter1;
        public DevExpress.XtraReports.UI.XRLabel lbl_system_title;
        public DevExpress.XtraReports.UI.XRLabel lbl_system_address;
        public DevExpress.XtraReports.UI.XRLabel lbl_card;
        public DevExpress.XtraReports.UI.XRLabel lbl_section;
        public DevExpress.XtraReports.UI.XRLabel lbl_std_id;
        public DevExpress.XtraReports.UI.XRLabel lbl_roll;
        public DevExpress.XtraReports.UI.XRLabel lbl_class;
        public DevExpress.XtraReports.UI.XRBarCode lbl_card_id;
        public DevExpress.XtraReports.UI.XRLabel lbl_name;
        private tnsbay_schoolDataSetTableAdapters.examTableAdapter examTableAdapter2;
    }
}
