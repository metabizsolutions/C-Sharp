namespace SchoolManagementSystem.Students
{
    partial class student_card_rpt
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(student_card_rpt));
            this.Detail = new DevExpress.XtraReports.UI.DetailBand();
            this.card_panel = new DevExpress.XtraReports.UI.XRPanel();
            this.TopMargin = new DevExpress.XtraReports.UI.TopMarginBand();
            this.BottomMargin = new DevExpress.XtraReports.UI.BottomMarginBand();
            this.objectDataSource1 = new DevExpress.DataAccess.ObjectBinding.ObjectDataSource(this.components);
            this.customInstaller1 = new MySql.Data.MySqlClient.CustomInstaller();
            this.examTableAdapter1 = new SchoolManagementSystem.tnsbay_schoolDataSetTableAdapters.examTableAdapter();
            this.examTableAdapter2 = new SchoolManagementSystem.tnsbay_schoolDataSetTableAdapters.examTableAdapter();
            this.objectDataSource2 = new DevExpress.DataAccess.ObjectBinding.ObjectDataSource(this.components);
            this.xrRichText1 = new DevExpress.XtraReports.UI.XRRichText();
            ((System.ComponentModel.ISupportInitialize)(this.objectDataSource1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.objectDataSource2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.xrRichText1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
            // 
            // Detail
            // 
            this.Detail.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.card_panel});
            this.Detail.HeightF = 227.0833F;
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
            this.card_panel.BackColor = System.Drawing.Color.Transparent;
            this.card_panel.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top) 
            | DevExpress.XtraPrinting.BorderSide.Right) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.card_panel.BorderWidth = 0F;
            this.card_panel.CanGrow = false;
            this.card_panel.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrRichText1});
            this.card_panel.LocationFloat = new DevExpress.Utils.PointFloat(10F, 0F);
            this.card_panel.Name = "card_panel";
            this.card_panel.SizeF = new System.Drawing.SizeF(353.9F, 218.0833F);
            this.card_panel.StylePriority.UseBackColor = false;
            this.card_panel.StylePriority.UseBorderWidth = false;
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
            // xrRichText1
            // 
            this.xrRichText1.Font = new System.Drawing.Font("Times New Roman", 9.75F);
            this.xrRichText1.LocationFloat = new DevExpress.Utils.PointFloat(0F, 0F);
            this.xrRichText1.Name = "xrRichText1";
            this.xrRichText1.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrRichText1.SerializableRtfString = resources.GetString("xrRichText1.SerializableRtfString");
            this.xrRichText1.SizeF = new System.Drawing.SizeF(353.9F, 217.0833F);
            this.xrRichText1.BeforePrint += new System.Drawing.Printing.PrintEventHandler(this.xrRichText1_BeforePrint);
            // 
            // student_card_rpt
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
            ((System.ComponentModel.ISupportInitialize)(this.xrRichText1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this)).EndInit();

        }

        #endregion

        private DevExpress.XtraReports.UI.DetailBand Detail;
        private DevExpress.XtraReports.UI.TopMarginBand TopMargin;
        private DevExpress.XtraReports.UI.BottomMarginBand BottomMargin;
        private DevExpress.DataAccess.ObjectBinding.ObjectDataSource objectDataSource1;
        private DevExpress.DataAccess.ObjectBinding.ObjectDataSource objectDataSource2;
        private MySql.Data.MySqlClient.CustomInstaller customInstaller1;
        public DevExpress.XtraReports.UI.XRPanel card_panel;
        private tnsbay_schoolDataSetTableAdapters.examTableAdapter examTableAdapter1;
        private tnsbay_schoolDataSetTableAdapters.examTableAdapter examTableAdapter2;
        private DevExpress.XtraReports.UI.XRRichText xrRichText1;
    }
}
