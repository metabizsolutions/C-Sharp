namespace SchoolManagementSystem.Fees
{
    partial class FeeRecipt_Cashold
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
            DevExpress.DataAccess.ObjectBinding.ObjectConstructorInfo objectConstructorInfo1 = new DevExpress.DataAccess.ObjectBinding.ObjectConstructorInfo();
            this.GroupDetailList = new DevExpress.XtraReports.UI.DetailBand();
            this.xrLabel6 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel12 = new DevExpress.XtraReports.UI.XRLabel();
            this.TopMargin = new DevExpress.XtraReports.UI.TopMarginBand();
            this.BottomMargin = new DevExpress.XtraReports.UI.BottomMarginBand();
            this.PageHeader = new DevExpress.XtraReports.UI.PageHeaderBand();
            this.LabelDueDate = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel14 = new DevExpress.XtraReports.UI.XRLabel();
            this.LabelAmountWord = new DevExpress.XtraReports.UI.XRLabel();
            this.LabelinvoiceAmount = new DevExpress.XtraReports.UI.XRLabel();
            this.LabelAmountTitle = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel23 = new DevExpress.XtraReports.UI.XRLabel();
            this.labChallanN = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel21 = new DevExpress.XtraReports.UI.XRLabel();
            this.LabDate = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel4 = new DevExpress.XtraReports.UI.XRLabel();
            this.LabStudentId = new DevExpress.XtraReports.UI.XRLabel();
            this.LabSClass = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel8 = new DevExpress.XtraReports.UI.XRLabel();
            this.LabSName = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLine2 = new DevExpress.XtraReports.UI.XRLine();
            this.xrLine1 = new DevExpress.XtraReports.UI.XRLine();
            this.xrLabel2 = new DevExpress.XtraReports.UI.XRLabel();
            this.LabTitle = new DevExpress.XtraReports.UI.XRLabel();
            this.LabAddress = new DevExpress.XtraReports.UI.XRLabel();
            this.PicIogoBox = new DevExpress.XtraReports.UI.XRPictureBox();
            this.LabTel = new DevExpress.XtraReports.UI.XRLabel();
            this.LabReport = new DevExpress.XtraReports.UI.XRLabel();
            this.LabelIssueDate = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel5 = new DevExpress.XtraReports.UI.XRLabel();
            this.LabSection = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel11 = new DevExpress.XtraReports.UI.XRLabel();
            this.LabFName = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel9 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel7 = new DevExpress.XtraReports.UI.XRLabel();
            this.LabRoll = new DevExpress.XtraReports.UI.XRLabel();
            this.picPrincipal_Sign = new DevExpress.XtraReports.UI.XRPictureBox();
            this.LabPrincipal = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel19 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLine5 = new DevExpress.XtraReports.UI.XRLine();
            this.GroupFooter1 = new DevExpress.XtraReports.UI.GroupFooterBand();
            this.LabelNotes = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLine3 = new DevExpress.XtraReports.UI.XRLine();
            this.GroupDetail = new DevExpress.XtraReports.UI.GroupHeaderBand();
            this.xrLabel1 = new DevExpress.XtraReports.UI.XRLabel();
            this.objectDataSource1 = new DevExpress.DataAccess.ObjectBinding.ObjectDataSource(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.objectDataSource1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
            // 
            // GroupDetailList
            // 
            this.GroupDetailList.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrLabel6,
            this.xrLabel12});
            this.GroupDetailList.HeightF = 17.62492F;
            this.GroupDetailList.Name = "GroupDetailList";
            this.GroupDetailList.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 100F);
            this.GroupDetailList.StylePriority.UseTextAlignment = false;
            this.GroupDetailList.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            this.GroupDetailList.BeforePrint += new System.Drawing.Printing.PrintEventHandler(this.GroupDetailList_BeforePrint);
            // 
            // xrLabel6
            // 
            this.xrLabel6.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top) 
            | DevExpress.XtraPrinting.BorderSide.Right) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrLabel6.ExpressionBindings.AddRange(new DevExpress.XtraReports.UI.ExpressionBinding[] {
            new DevExpress.XtraReports.UI.ExpressionBinding("BeforePrint", "Text", "[Amount]")});
            this.xrLabel6.LocationFloat = new DevExpress.Utils.PointFloat(198.8219F, 0F);
            this.xrLabel6.Name = "xrLabel6";
            this.xrLabel6.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrLabel6.SizeF = new System.Drawing.SizeF(60.12451F, 17.62492F);
            this.xrLabel6.StylePriority.UseBorders = false;
            this.xrLabel6.StylePriority.UsePadding = false;
            this.xrLabel6.StylePriority.UseTextAlignment = false;
            this.xrLabel6.Text = "xrTableCell1";
            this.xrLabel6.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            // 
            // xrLabel12
            // 
            this.xrLabel12.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top) 
            | DevExpress.XtraPrinting.BorderSide.Right) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrLabel12.ExpressionBindings.AddRange(new DevExpress.XtraReports.UI.ExpressionBinding[] {
            new DevExpress.XtraReports.UI.ExpressionBinding("BeforePrint", "Text", "[Method]")});
            this.xrLabel12.LocationFloat = new DevExpress.Utils.PointFloat(2.824936F, 0F);
            this.xrLabel12.Name = "xrLabel12";
            this.xrLabel12.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrLabel12.SizeF = new System.Drawing.SizeF(195.997F, 17.62492F);
            this.xrLabel12.StylePriority.UseBorders = false;
            this.xrLabel12.StylePriority.UsePadding = false;
            this.xrLabel12.StylePriority.UseTextAlignment = false;
            this.xrLabel12.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft;
            // 
            // TopMargin
            // 
            this.TopMargin.HeightF = 0F;
            this.TopMargin.Name = "TopMargin";
            this.TopMargin.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 100F);
            this.TopMargin.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft;
            // 
            // BottomMargin
            // 
            this.BottomMargin.HeightF = 25F;
            this.BottomMargin.Name = "BottomMargin";
            this.BottomMargin.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 100F);
            this.BottomMargin.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft;
            // 
            // PageHeader
            // 
            this.PageHeader.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.LabelDueDate,
            this.xrLabel14,
            this.LabelAmountWord,
            this.LabelinvoiceAmount,
            this.LabelAmountTitle,
            this.xrLabel23,
            this.labChallanN,
            this.xrLabel21,
            this.LabDate,
            this.xrLabel4,
            this.LabStudentId,
            this.LabSClass,
            this.xrLabel8,
            this.LabSName,
            this.xrLine2,
            this.xrLine1,
            this.xrLabel2,
            this.LabTitle,
            this.LabAddress,
            this.PicIogoBox,
            this.LabTel,
            this.LabReport,
            this.LabelIssueDate,
            this.xrLabel5,
            this.LabSection,
            this.xrLabel11,
            this.LabFName,
            this.xrLabel9,
            this.xrLabel7,
            this.LabRoll});
            this.PageHeader.HeightF = 300.5835F;
            this.PageHeader.Name = "PageHeader";
            // 
            // LabelDueDate
            // 
            this.LabelDueDate.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top) 
            | DevExpress.XtraPrinting.BorderSide.Right)));
            this.LabelDueDate.Font = new System.Drawing.Font("Segoe UI", 7.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LabelDueDate.LocationFloat = new DevExpress.Utils.PointFloat(199F, 89.83339F);
            this.LabelDueDate.Name = "LabelDueDate";
            this.LabelDueDate.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.LabelDueDate.SizeF = new System.Drawing.SizeF(59F, 23F);
            this.LabelDueDate.StylePriority.UseBorders = false;
            this.LabelDueDate.StylePriority.UseFont = false;
            this.LabelDueDate.StylePriority.UseTextAlignment = false;
            this.LabelDueDate.Text = "DD";
            this.LabelDueDate.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            // 
            // xrLabel14
            // 
            this.xrLabel14.Borders = ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top)));
            this.xrLabel14.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xrLabel14.LocationFloat = new DevExpress.Utils.PointFloat(133.8219F, 89.83339F);
            this.xrLabel14.Name = "xrLabel14";
            this.xrLabel14.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrLabel14.SizeF = new System.Drawing.SizeF(65F, 23F);
            this.xrLabel14.StylePriority.UseBorders = false;
            this.xrLabel14.StylePriority.UseFont = false;
            this.xrLabel14.StylePriority.UseTextAlignment = false;
            this.xrLabel14.Text = "Due Date";
            this.xrLabel14.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            // 
            // LabelAmountWord
            // 
            this.LabelAmountWord.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top) 
            | DevExpress.XtraPrinting.BorderSide.Right) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.LabelAmountWord.Font = new System.Drawing.Font("Segoe UI", 7.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LabelAmountWord.LocationFloat = new DevExpress.Utils.PointFloat(1.831214F, 273.8335F);
            this.LabelAmountWord.Name = "LabelAmountWord";
            this.LabelAmountWord.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.LabelAmountWord.SizeF = new System.Drawing.SizeF(256.1688F, 22.99997F);
            this.LabelAmountWord.StylePriority.UseBorders = false;
            this.LabelAmountWord.StylePriority.UseFont = false;
            this.LabelAmountWord.StylePriority.UseTextAlignment = false;
            this.LabelAmountWord.Text = "Zero Rupees";
            this.LabelAmountWord.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            // 
            // LabelinvoiceAmount
            // 
            this.LabelinvoiceAmount.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top) 
            | DevExpress.XtraPrinting.BorderSide.Right)));
            this.LabelinvoiceAmount.Font = new System.Drawing.Font("Segoe UI", 7.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LabelinvoiceAmount.LocationFloat = new DevExpress.Utils.PointFloat(75.02901F, 250.8335F);
            this.LabelinvoiceAmount.Name = "LabelinvoiceAmount";
            this.LabelinvoiceAmount.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.LabelinvoiceAmount.SizeF = new System.Drawing.SizeF(182.971F, 23.00002F);
            this.LabelinvoiceAmount.StylePriority.UseBorders = false;
            this.LabelinvoiceAmount.StylePriority.UseFont = false;
            this.LabelinvoiceAmount.StylePriority.UseTextAlignment = false;
            this.LabelinvoiceAmount.Text = "0";
            this.LabelinvoiceAmount.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            // 
            // LabelAmountTitle
            // 
            this.LabelAmountTitle.Borders = ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top)));
            this.LabelAmountTitle.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LabelAmountTitle.LocationFloat = new DevExpress.Utils.PointFloat(1.614706F, 250.8335F);
            this.LabelAmountTitle.Name = "LabelAmountTitle";
            this.LabelAmountTitle.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.LabelAmountTitle.SizeF = new System.Drawing.SizeF(73F, 23F);
            this.LabelAmountTitle.StylePriority.UseBorders = false;
            this.LabelAmountTitle.StylePriority.UseFont = false;
            this.LabelAmountTitle.StylePriority.UseTextAlignment = false;
            this.LabelAmountTitle.Text = "Amount";
            this.LabelAmountTitle.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            // 
            // xrLabel23
            // 
            this.xrLabel23.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Top | DevExpress.XtraPrinting.BorderSide.Right) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrLabel23.Font = new System.Drawing.Font("Segoe UI Light", 7.75F);
            this.xrLabel23.LocationFloat = new DevExpress.Utils.PointFloat(103.8745F, 60.00001F);
            this.xrLabel23.Name = "xrLabel23";
            this.xrLabel23.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrLabel23.SizeF = new System.Drawing.SizeF(64.77698F, 21.50005F);
            this.xrLabel23.StylePriority.UseBorders = false;
            this.xrLabel23.StylePriority.UseFont = false;
            this.xrLabel23.StylePriority.UseTextAlignment = false;
            this.xrLabel23.Text = "Challan# :";
            this.xrLabel23.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            // 
            // labChallanN
            // 
            this.labChallanN.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Top | DevExpress.XtraPrinting.BorderSide.Right) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.labChallanN.Font = new System.Drawing.Font("Segoe UI", 7.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labChallanN.LocationFloat = new DevExpress.Utils.PointFloat(168.6515F, 60.00001F);
            this.labChallanN.Name = "labChallanN";
            this.labChallanN.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.labChallanN.SizeF = new System.Drawing.SizeF(89.3485F, 21.50005F);
            this.labChallanN.StylePriority.UseBorders = false;
            this.labChallanN.StylePriority.UseFont = false;
            this.labChallanN.StylePriority.UseTextAlignment = false;
            this.labChallanN.Text = "CN";
            this.labChallanN.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            // 
            // xrLabel21
            // 
            this.xrLabel21.Borders = ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top)));
            this.xrLabel21.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xrLabel21.LocationFloat = new DevExpress.Utils.PointFloat(1.821884F, 112.8334F);
            this.xrLabel21.Name = "xrLabel21";
            this.xrLabel21.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrLabel21.SizeF = new System.Drawing.SizeF(73F, 23F);
            this.xrLabel21.StylePriority.UseBorders = false;
            this.xrLabel21.StylePriority.UseFont = false;
            this.xrLabel21.StylePriority.UseTextAlignment = false;
            this.xrLabel21.Text = "Reg#";
            this.xrLabel21.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            // 
            // LabDate
            // 
            this.LabDate.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top) 
            | DevExpress.XtraPrinting.BorderSide.Right)));
            this.LabDate.Font = new System.Drawing.Font("Segoe UI", 7.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LabDate.LocationFloat = new DevExpress.Utils.PointFloat(75.02901F, 227.8335F);
            this.LabDate.Name = "LabDate";
            this.LabDate.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.LabDate.SizeF = new System.Drawing.SizeF(182.971F, 22.99997F);
            this.LabDate.StylePriority.UseBorders = false;
            this.LabDate.StylePriority.UseFont = false;
            this.LabDate.StylePriority.UseTextAlignment = false;
            this.LabDate.Text = "DD";
            this.LabDate.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            // 
            // xrLabel4
            // 
            this.xrLabel4.Borders = ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top)));
            this.xrLabel4.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xrLabel4.LocationFloat = new DevExpress.Utils.PointFloat(1.614706F, 227.8335F);
            this.xrLabel4.Name = "xrLabel4";
            this.xrLabel4.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrLabel4.SizeF = new System.Drawing.SizeF(73F, 23F);
            this.xrLabel4.StylePriority.UseBorders = false;
            this.xrLabel4.StylePriority.UseFont = false;
            this.xrLabel4.StylePriority.UseTextAlignment = false;
            this.xrLabel4.Text = "Month ";
            this.xrLabel4.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            // 
            // LabStudentId
            // 
            this.LabStudentId.Borders = ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top)));
            this.LabStudentId.Font = new System.Drawing.Font("Segoe UI", 7.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LabStudentId.LocationFloat = new DevExpress.Utils.PointFloat(74.82187F, 112.8334F);
            this.LabStudentId.Name = "LabStudentId";
            this.LabStudentId.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.LabStudentId.SizeF = new System.Drawing.SizeF(59F, 23.00002F);
            this.LabStudentId.StylePriority.UseBorders = false;
            this.LabStudentId.StylePriority.UseFont = false;
            this.LabStudentId.StylePriority.UseTextAlignment = false;
            this.LabStudentId.Text = "-";
            this.LabStudentId.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            // 
            // LabSClass
            // 
            this.LabSClass.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top) 
            | DevExpress.XtraPrinting.BorderSide.Right)));
            this.LabSClass.Font = new System.Drawing.Font("Segoe UI", 7.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LabSClass.LocationFloat = new DevExpress.Utils.PointFloat(74.82187F, 181.8335F);
            this.LabSClass.Name = "LabSClass";
            this.LabSClass.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.LabSClass.SizeF = new System.Drawing.SizeF(183.1781F, 23F);
            this.LabSClass.StylePriority.UseBorders = false;
            this.LabSClass.StylePriority.UseFont = false;
            this.LabSClass.StylePriority.UseTextAlignment = false;
            this.LabSClass.Text = "...";
            this.LabSClass.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            this.LabSClass.WordWrap = false;
            // 
            // xrLabel8
            // 
            this.xrLabel8.Borders = ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top)));
            this.xrLabel8.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xrLabel8.LocationFloat = new DevExpress.Utils.PointFloat(1.821884F, 181.8335F);
            this.xrLabel8.Name = "xrLabel8";
            this.xrLabel8.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrLabel8.SizeF = new System.Drawing.SizeF(73F, 23F);
            this.xrLabel8.StylePriority.UseBorders = false;
            this.xrLabel8.StylePriority.UseFont = false;
            this.xrLabel8.StylePriority.UseTextAlignment = false;
            this.xrLabel8.Text = "Class ";
            this.xrLabel8.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            // 
            // LabSName
            // 
            this.LabSName.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top) 
            | DevExpress.XtraPrinting.BorderSide.Right)));
            this.LabSName.Font = new System.Drawing.Font("Segoe UI", 7.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LabSName.LocationFloat = new DevExpress.Utils.PointFloat(74.82187F, 135.8335F);
            this.LabSName.Name = "LabSName";
            this.LabSName.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.LabSName.SizeF = new System.Drawing.SizeF(183.1781F, 23F);
            this.LabSName.StylePriority.UseBorders = false;
            this.LabSName.StylePriority.UseFont = false;
            this.LabSName.StylePriority.UseTextAlignment = false;
            this.LabSName.Text = "Student";
            this.LabSName.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            // 
            // xrLine2
            // 
            this.xrLine2.LocationFloat = new DevExpress.Utils.PointFloat(2.103615F, 84.54173F);
            this.xrLine2.Name = "xrLine2";
            this.xrLine2.SizeF = new System.Drawing.SizeF(255.8964F, 5.291672F);
            // 
            // xrLine1
            // 
            this.xrLine1.LineStyle = System.Drawing.Drawing2D.DashStyle.Dash;
            this.xrLine1.LocationFloat = new DevExpress.Utils.PointFloat(2.103615F, 82.75003F);
            this.xrLine1.Name = "xrLine1";
            this.xrLine1.SizeF = new System.Drawing.SizeF(255.8964F, 5.291679F);
            // 
            // xrLabel2
            // 
            this.xrLabel2.Borders = ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top)));
            this.xrLabel2.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xrLabel2.LocationFloat = new DevExpress.Utils.PointFloat(1.821884F, 135.8334F);
            this.xrLabel2.Name = "xrLabel2";
            this.xrLabel2.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrLabel2.SizeF = new System.Drawing.SizeF(73F, 23F);
            this.xrLabel2.StylePriority.UseBorders = false;
            this.xrLabel2.StylePriority.UseFont = false;
            this.xrLabel2.StylePriority.UseTextAlignment = false;
            this.xrLabel2.Text = "Name ";
            this.xrLabel2.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            // 
            // LabTitle
            // 
            this.LabTitle.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LabTitle.LocationFloat = new DevExpress.Utils.PointFloat(52.41674F, 0F);
            this.LabTitle.Name = "LabTitle";
            this.LabTitle.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.LabTitle.SizeF = new System.Drawing.SizeF(205.5833F, 38.20833F);
            this.LabTitle.StylePriority.UseFont = false;
            this.LabTitle.StylePriority.UseTextAlignment = false;
            this.LabTitle.Text = "LabTitle";
            this.LabTitle.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            // 
            // LabAddress
            // 
            this.LabAddress.Font = new System.Drawing.Font("Segoe UI", 7.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LabAddress.LocationFloat = new DevExpress.Utils.PointFloat(52.41674F, 38.20832F);
            this.LabAddress.Name = "LabAddress";
            this.LabAddress.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.LabAddress.SizeF = new System.Drawing.SizeF(129.7765F, 21.79168F);
            this.LabAddress.StylePriority.UseFont = false;
            this.LabAddress.StylePriority.UseTextAlignment = false;
            this.LabAddress.Text = "LabAddress";
            this.LabAddress.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            // 
            // PicIogoBox
            // 
            this.PicIogoBox.Borders = DevExpress.XtraPrinting.BorderSide.None;
            this.PicIogoBox.ImageAlignment = DevExpress.XtraPrinting.ImageAlignment.MiddleCenter;
            this.PicIogoBox.LocationFloat = new DevExpress.Utils.PointFloat(1.614706F, 0F);
            this.PicIogoBox.Name = "PicIogoBox";
            this.PicIogoBox.SizeF = new System.Drawing.SizeF(50.80204F, 60.00001F);
            this.PicIogoBox.Sizing = DevExpress.XtraPrinting.ImageSizeMode.ZoomImage;
            this.PicIogoBox.StylePriority.UseBorders = false;
            // 
            // LabTel
            // 
            this.LabTel.Font = new System.Drawing.Font("Segoe UI", 7.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LabTel.LocationFloat = new DevExpress.Utils.PointFloat(182.1933F, 38.20832F);
            this.LabTel.Name = "LabTel";
            this.LabTel.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.LabTel.SizeF = new System.Drawing.SizeF(75.80673F, 21.79168F);
            this.LabTel.StylePriority.UseFont = false;
            this.LabTel.StylePriority.UseTextAlignment = false;
            this.LabTel.Text = "LabTel";
            this.LabTel.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            // 
            // LabReport
            // 
            this.LabReport.BackColor = System.Drawing.Color.Transparent;
            this.LabReport.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top) 
            | DevExpress.XtraPrinting.BorderSide.Right) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.LabReport.Font = new System.Drawing.Font("Segoe UI Light", 7.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LabReport.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.LabReport.LocationFloat = new DevExpress.Utils.PointFloat(1.878516F, 60.00001F);
            this.LabReport.Name = "LabReport";
            this.LabReport.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.LabReport.SizeF = new System.Drawing.SizeF(101.996F, 21.50006F);
            this.LabReport.StylePriority.UseBackColor = false;
            this.LabReport.StylePriority.UseBorders = false;
            this.LabReport.StylePriority.UseFont = false;
            this.LabReport.StylePriority.UseForeColor = false;
            this.LabReport.StylePriority.UseTextAlignment = false;
            this.LabReport.Text = "Fee Receipt";
            this.LabReport.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            // 
            // LabelIssueDate
            // 
            this.LabelIssueDate.Borders = ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top)));
            this.LabelIssueDate.Font = new System.Drawing.Font("Segoe UI", 7.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LabelIssueDate.LocationFloat = new DevExpress.Utils.PointFloat(74.82188F, 89.83339F);
            this.LabelIssueDate.Name = "LabelIssueDate";
            this.LabelIssueDate.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.LabelIssueDate.SizeF = new System.Drawing.SizeF(59F, 23F);
            this.LabelIssueDate.StylePriority.UseBorders = false;
            this.LabelIssueDate.StylePriority.UseFont = false;
            this.LabelIssueDate.StylePriority.UseTextAlignment = false;
            this.LabelIssueDate.Text = "DD";
            this.LabelIssueDate.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            // 
            // xrLabel5
            // 
            this.xrLabel5.Borders = ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top)));
            this.xrLabel5.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xrLabel5.LocationFloat = new DevExpress.Utils.PointFloat(1.821884F, 89.83339F);
            this.xrLabel5.Name = "xrLabel5";
            this.xrLabel5.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrLabel5.SizeF = new System.Drawing.SizeF(73F, 23F);
            this.xrLabel5.StylePriority.UseBorders = false;
            this.xrLabel5.StylePriority.UseFont = false;
            this.xrLabel5.StylePriority.UseTextAlignment = false;
            this.xrLabel5.Text = "Issue Date";
            this.xrLabel5.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            // 
            // LabSection
            // 
            this.LabSection.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top) 
            | DevExpress.XtraPrinting.BorderSide.Right)));
            this.LabSection.Font = new System.Drawing.Font("Segoe UI", 7.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LabSection.LocationFloat = new DevExpress.Utils.PointFloat(74.82187F, 204.8335F);
            this.LabSection.Name = "LabSection";
            this.LabSection.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.LabSection.SizeF = new System.Drawing.SizeF(183.1781F, 23.00002F);
            this.LabSection.StylePriority.UseBorders = false;
            this.LabSection.StylePriority.UseFont = false;
            this.LabSection.StylePriority.UseTextAlignment = false;
            this.LabSection.Text = "...";
            this.LabSection.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            this.LabSection.WordWrap = false;
            // 
            // xrLabel11
            // 
            this.xrLabel11.Borders = ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top)));
            this.xrLabel11.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xrLabel11.LocationFloat = new DevExpress.Utils.PointFloat(1.821884F, 204.8335F);
            this.xrLabel11.Name = "xrLabel11";
            this.xrLabel11.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrLabel11.SizeF = new System.Drawing.SizeF(73F, 23F);
            this.xrLabel11.StylePriority.UseBorders = false;
            this.xrLabel11.StylePriority.UseFont = false;
            this.xrLabel11.StylePriority.UseTextAlignment = false;
            this.xrLabel11.Text = "Section";
            this.xrLabel11.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            // 
            // LabFName
            // 
            this.LabFName.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top) 
            | DevExpress.XtraPrinting.BorderSide.Right)));
            this.LabFName.Font = new System.Drawing.Font("Segoe UI", 7.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LabFName.LocationFloat = new DevExpress.Utils.PointFloat(74.87852F, 158.8335F);
            this.LabFName.Name = "LabFName";
            this.LabFName.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.LabFName.SizeF = new System.Drawing.SizeF(183.1215F, 23F);
            this.LabFName.StylePriority.UseBorders = false;
            this.LabFName.StylePriority.UseFont = false;
            this.LabFName.StylePriority.UseTextAlignment = false;
            this.LabFName.Text = "...";
            this.LabFName.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            // 
            // xrLabel9
            // 
            this.xrLabel9.Borders = ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top)));
            this.xrLabel9.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xrLabel9.LocationFloat = new DevExpress.Utils.PointFloat(1.821884F, 158.8335F);
            this.xrLabel9.Name = "xrLabel9";
            this.xrLabel9.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrLabel9.SizeF = new System.Drawing.SizeF(73F, 23F);
            this.xrLabel9.StylePriority.UseBorders = false;
            this.xrLabel9.StylePriority.UseFont = false;
            this.xrLabel9.StylePriority.UseTextAlignment = false;
            this.xrLabel9.Text = "Father";
            this.xrLabel9.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            // 
            // xrLabel7
            // 
            this.xrLabel7.Borders = ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top)));
            this.xrLabel7.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xrLabel7.LocationFloat = new DevExpress.Utils.PointFloat(133.8219F, 112.8334F);
            this.xrLabel7.Name = "xrLabel7";
            this.xrLabel7.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrLabel7.SizeF = new System.Drawing.SizeF(65.17807F, 23.00003F);
            this.xrLabel7.StylePriority.UseBorders = false;
            this.xrLabel7.StylePriority.UseFont = false;
            this.xrLabel7.StylePriority.UseTextAlignment = false;
            this.xrLabel7.Text = "Roll#";
            this.xrLabel7.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            // 
            // LabRoll
            // 
            this.LabRoll.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top) 
            | DevExpress.XtraPrinting.BorderSide.Right)));
            this.LabRoll.Font = new System.Drawing.Font("Segoe UI", 7.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LabRoll.LocationFloat = new DevExpress.Utils.PointFloat(199F, 112.8334F);
            this.LabRoll.Name = "LabRoll";
            this.LabRoll.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.LabRoll.SizeF = new System.Drawing.SizeF(59F, 23.00002F);
            this.LabRoll.StylePriority.UseBorders = false;
            this.LabRoll.StylePriority.UseFont = false;
            this.LabRoll.StylePriority.UseTextAlignment = false;
            this.LabRoll.Text = "-";
            this.LabRoll.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            // 
            // picPrincipal_Sign
            // 
            this.picPrincipal_Sign.Borders = DevExpress.XtraPrinting.BorderSide.None;
            this.picPrincipal_Sign.ImageAlignment = DevExpress.XtraPrinting.ImageAlignment.MiddleCenter;
            this.picPrincipal_Sign.LocationFloat = new DevExpress.Utils.PointFloat(104.4008F, 36.5001F);
            this.picPrincipal_Sign.Name = "picPrincipal_Sign";
            this.picPrincipal_Sign.SizeF = new System.Drawing.SizeF(142.7498F, 39.79169F);
            this.picPrincipal_Sign.Sizing = DevExpress.XtraPrinting.ImageSizeMode.StretchImage;
            this.picPrincipal_Sign.StylePriority.UseBorders = false;
            // 
            // LabPrincipal
            // 
            this.LabPrincipal.LocationFloat = new DevExpress.Utils.PointFloat(104.4008F, 104.7501F);
            this.LabPrincipal.Multiline = true;
            this.LabPrincipal.Name = "LabPrincipal";
            this.LabPrincipal.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.LabPrincipal.SizeF = new System.Drawing.SizeF(142.7498F, 18.04169F);
            this.LabPrincipal.StylePriority.UseTextAlignment = false;
            this.LabPrincipal.Text = "( Stamp / Signature )";
            this.LabPrincipal.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            // 
            // xrLabel19
            // 
            this.xrLabel19.LocationFloat = new DevExpress.Utils.PointFloat(104.4008F, 76.29178F);
            this.xrLabel19.Multiline = true;
            this.xrLabel19.Name = "xrLabel19";
            this.xrLabel19.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrLabel19.SizeF = new System.Drawing.SizeF(142.7498F, 18.04163F);
            this.xrLabel19.StylePriority.UseTextAlignment = false;
            this.xrLabel19.Text = "Accountant";
            this.xrLabel19.TextAlignment = DevExpress.XtraPrinting.TextAlignment.BottomCenter;
            // 
            // xrLine5
            // 
            this.xrLine5.LocationFloat = new DevExpress.Utils.PointFloat(104.4008F, 89.79184F);
            this.xrLine5.Name = "xrLine5";
            this.xrLine5.SizeF = new System.Drawing.SizeF(142.7498F, 23F);
            // 
            // GroupFooter1
            // 
            this.GroupFooter1.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrLine5,
            this.xrLabel19,
            this.LabPrincipal,
            this.picPrincipal_Sign,
            this.LabelNotes});
            this.GroupFooter1.HeightF = 122.7918F;
            this.GroupFooter1.Name = "GroupFooter1";
            // 
            // LabelNotes
            // 
            this.LabelNotes.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top) 
            | DevExpress.XtraPrinting.BorderSide.Right) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.LabelNotes.Font = new System.Drawing.Font("Segoe UI", 7.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LabelNotes.LocationFloat = new DevExpress.Utils.PointFloat(2.878281F, 0F);
            this.LabelNotes.Multiline = true;
            this.LabelNotes.Name = "LabelNotes";
            this.LabelNotes.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.LabelNotes.SizeF = new System.Drawing.SizeF(255.8965F, 36.5001F);
            this.LabelNotes.StylePriority.UseBorders = false;
            this.LabelNotes.StylePriority.UseFont = false;
            this.LabelNotes.StylePriority.UseTextAlignment = false;
            this.LabelNotes.Text = "Notes here";
            this.LabelNotes.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft;
            // 
            // xrLine3
            // 
            this.xrLine3.LineStyle = System.Drawing.Drawing2D.DashStyle.Dash;
            this.xrLine3.LocationFloat = new DevExpress.Utils.PointFloat(1.831214F, 0F);
            this.xrLine3.Name = "xrLine3";
            this.xrLine3.SizeF = new System.Drawing.SizeF(256.9436F, 5.291679F);
            // 
            // GroupDetail
            // 
            this.GroupDetail.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrLabel1,
            this.xrLine3});
            this.GroupDetail.HeightF = 30.37488F;
            this.GroupDetail.Name = "GroupDetail";
            this.GroupDetail.BeforePrint += new System.Drawing.Printing.PrintEventHandler(this.GroupDetail_BeforePrint);
            // 
            // xrLabel1
            // 
            this.xrLabel1.BackColor = System.Drawing.Color.Gainsboro;
            this.xrLabel1.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top) 
            | DevExpress.XtraPrinting.BorderSide.Right) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrLabel1.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xrLabel1.LocationFloat = new DevExpress.Utils.PointFloat(1.878516F, 5.291685F);
            this.xrLabel1.Name = "xrLabel1";
            this.xrLabel1.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrLabel1.SizeF = new System.Drawing.SizeF(255.8964F, 22.99998F);
            this.xrLabel1.StylePriority.UseBackColor = false;
            this.xrLabel1.StylePriority.UseBorders = false;
            this.xrLabel1.StylePriority.UseFont = false;
            this.xrLabel1.StylePriority.UseTextAlignment = false;
            this.xrLabel1.Text = "Detail";
            this.xrLabel1.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            // 
            // objectDataSource1
            // 
            this.objectDataSource1.Constructor = objectConstructorInfo1;
            this.objectDataSource1.DataSource = typeof(SchoolManagementSystem.PreviousFee);
            this.objectDataSource1.Name = "objectDataSource1";
            // 
            // FeeRecipt_Cashold
            // 
            this.Bands.AddRange(new DevExpress.XtraReports.UI.Band[] {
            this.GroupDetailList,
            this.TopMargin,
            this.BottomMargin,
            this.PageHeader,
            this.GroupDetail,
            this.GroupFooter1});
            this.ComponentStorage.AddRange(new System.ComponentModel.IComponent[] {
            this.objectDataSource1});
            this.DataSource = this.objectDataSource1;
            this.Margins = new System.Drawing.Printing.Margins(0, 568, 0, 25);
            this.PageHeight = 1169;
            this.PageWidth = 827;
            this.PaperKind = System.Drawing.Printing.PaperKind.A4;
            this.Version = "20.1";
            ((System.ComponentModel.ISupportInitialize)(this.objectDataSource1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this)).EndInit();

        }

        #endregion

        private DevExpress.XtraReports.UI.DetailBand GroupDetailList;
        private DevExpress.XtraReports.UI.TopMarginBand TopMargin;
        private DevExpress.XtraReports.UI.BottomMarginBand BottomMargin;
        private DevExpress.XtraReports.UI.PageHeaderBand PageHeader;
        public DevExpress.XtraReports.UI.XRLabel LabTitle;
        public DevExpress.XtraReports.UI.XRLabel LabAddress;
        public DevExpress.XtraReports.UI.XRPictureBox PicIogoBox;
        public DevExpress.XtraReports.UI.XRLabel LabTel;
        public DevExpress.XtraReports.UI.XRLabel LabReport;
        public DevExpress.XtraReports.UI.XRLabel LabSClass;
        public DevExpress.XtraReports.UI.XRLabel xrLabel8;
        public DevExpress.XtraReports.UI.XRLabel LabSName;
        private DevExpress.XtraReports.UI.XRLine xrLine2;
        private DevExpress.XtraReports.UI.XRLine xrLine1;
        public DevExpress.XtraReports.UI.XRLabel xrLabel2;
        public DevExpress.XtraReports.UI.XRLabel LabStudentId;
        public DevExpress.XtraReports.UI.XRLabel LabDate;
        public DevExpress.XtraReports.UI.XRLabel xrLabel4;
        private DevExpress.DataAccess.ObjectBinding.ObjectDataSource objectDataSource1;
        private DevExpress.XtraReports.UI.XRLabel xrLabel6;
        private DevExpress.XtraReports.UI.XRLabel xrLabel12;
        public DevExpress.XtraReports.UI.XRLabel xrLabel21;
        public DevExpress.XtraReports.UI.XRLabel xrLabel23;
        public DevExpress.XtraReports.UI.XRLabel labChallanN;
        public DevExpress.XtraReports.UI.XRPictureBox picPrincipal_Sign;
        public DevExpress.XtraReports.UI.XRLabel LabPrincipal;
        private DevExpress.XtraReports.UI.XRLabel xrLabel19;
        private DevExpress.XtraReports.UI.XRLine xrLine5;
        private DevExpress.XtraReports.UI.GroupFooterBand GroupFooter1;
        public DevExpress.XtraReports.UI.XRLabel LabelAmountTitle;
        public DevExpress.XtraReports.UI.XRLabel LabelinvoiceAmount;
        public DevExpress.XtraReports.UI.XRLabel LabelAmountWord;
        public DevExpress.XtraReports.UI.XRLabel xrLabel14;
        public DevExpress.XtraReports.UI.XRLabel LabelDueDate;
        public DevExpress.XtraReports.UI.XRLabel LabelNotes;
        private DevExpress.XtraReports.UI.XRLine xrLine3;
        private DevExpress.XtraReports.UI.GroupHeaderBand GroupDetail;
        public DevExpress.XtraReports.UI.XRLabel xrLabel1;
        public DevExpress.XtraReports.UI.XRLabel LabelIssueDate;
        public DevExpress.XtraReports.UI.XRLabel xrLabel5;
        public DevExpress.XtraReports.UI.XRLabel LabSection;
        public DevExpress.XtraReports.UI.XRLabel xrLabel11;
        public DevExpress.XtraReports.UI.XRLabel LabFName;
        public DevExpress.XtraReports.UI.XRLabel xrLabel9;
        public DevExpress.XtraReports.UI.XRLabel xrLabel7;
        public DevExpress.XtraReports.UI.XRLabel LabRoll;
    }
}
