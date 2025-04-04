namespace SchoolManagementSystem.Fees
{
    partial class SalaryManagement
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SalaryManagement));
            this.gridSalary = new DevExpress.XtraGrid.GridControl();
            this.gridView1 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.ribbonControl1 = new DevExpress.XtraBars.Ribbon.RibbonControl();
            this.barButPaySlip = new DevExpress.XtraBars.BarButtonItem();
            this.txtMonth = new DevExpress.XtraBars.BarEditItem();
            this.RetxtMonth = new DevExpress.XtraEditors.Repository.RepositoryItemComboBox();
            this.txtYear = new DevExpress.XtraBars.BarEditItem();
            this.RetxtYear = new DevExpress.XtraEditors.Repository.RepositoryItemComboBox();
            this.barButGSalary = new DevExpress.XtraBars.BarButtonItem();
            this.BtnFind = new DevExpress.XtraBars.BarButtonItem();
            this.btnPrint = new DevExpress.XtraBars.BarButtonItem();
            this.CmboTeacher = new DevExpress.XtraBars.BarEditItem();
            this.CmboTeacherName = new DevExpress.XtraEditors.Repository.RepositoryItemCheckedComboBoxEdit();
            this.barButtonItem1 = new DevExpress.XtraBars.BarButtonItem();
            this.barButtonItem2 = new DevExpress.XtraBars.BarButtonItem();
            this.txtDatepicker = new DevExpress.XtraBars.BarEditItem();
            this.txtDate = new DevExpress.XtraEditors.Repository.RepositoryItemDateEdit();
            this.SpinnerCash = new DevExpress.XtraBars.BarEditItem();
            this.SpinnerCashAsset = new DevExpress.XtraEditors.Repository.RepositoryItemGridLookUpEdit();
            this.repositoryItemGridLookUpEdit1View = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.txtpaid = new DevExpress.XtraBars.BarEditItem();
            this.txtPaidElement = new DevExpress.XtraEditors.Repository.RepositoryItemTextEdit();
            this.DateFrom = new DevExpress.XtraBars.BarEditItem();
            this.repositoryItemDateEdit1 = new DevExpress.XtraEditors.Repository.RepositoryItemDateEdit();
            this.DateTo = new DevExpress.XtraBars.BarEditItem();
            this.repositoryItemDateEdit2 = new DevExpress.XtraEditors.Repository.RepositoryItemDateEdit();
            this.ribbonPage1 = new DevExpress.XtraBars.Ribbon.RibbonPage();
            this.ribbonPageGroup2 = new DevExpress.XtraBars.Ribbon.RibbonPageGroup();
            this.ribbonPage2 = new DevExpress.XtraBars.Ribbon.RibbonPage();
            this.ribbonPageGroup3 = new DevExpress.XtraBars.Ribbon.RibbonPageGroup();
            this.RetxtFMonth = new DevExpress.XtraEditors.Repository.RepositoryItemComboBox();
            this.RetxtFYear = new DevExpress.XtraEditors.Repository.RepositoryItemComboBox();
            this.RetxtReports = new DevExpress.XtraEditors.Repository.RepositoryItemComboBox();
            this.repositoryItemCheckedComboBoxEdit2 = new DevExpress.XtraEditors.Repository.RepositoryItemCheckedComboBoxEdit();
            this.barEditItem3 = new DevExpress.XtraBars.BarEditItem();
            this.barButFind = new DevExpress.XtraBars.BarButtonItem();
            this.barEditItem4 = new DevExpress.XtraBars.BarEditItem();
            ((System.ComponentModel.ISupportInitialize)(this.gridSalary)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ribbonControl1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.RetxtMonth)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.RetxtYear)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.CmboTeacherName)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtDate)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtDate.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.SpinnerCashAsset)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemGridLookUpEdit1View)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtPaidElement)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemDateEdit1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemDateEdit1.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemDateEdit2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemDateEdit2.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.RetxtFMonth)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.RetxtFYear)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.RetxtReports)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemCheckedComboBoxEdit2)).BeginInit();
            this.SuspendLayout();
            // 
            // gridSalary
            // 
            this.gridSalary.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridSalary.Location = new System.Drawing.Point(0, 52);
            this.gridSalary.MainView = this.gridView1;
            this.gridSalary.Name = "gridSalary";
            this.gridSalary.Size = new System.Drawing.Size(1061, 387);
            this.gridSalary.TabIndex = 15;
            this.gridSalary.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridView1});
            // 
            // gridView1
            // 
            this.gridView1.GridControl = this.gridSalary;
            this.gridView1.Name = "gridView1";
            this.gridView1.OptionsBehavior.Editable = false;
            this.gridView1.OptionsBehavior.EditingMode = DevExpress.XtraGrid.Views.Grid.GridEditingMode.Inplace;
            this.gridView1.OptionsFind.AlwaysVisible = true;
            this.gridView1.OptionsFind.FindDelay = 100;
            this.gridView1.OptionsView.ColumnAutoWidth = false;
            this.gridView1.OptionsView.ShowFooter = true;
            this.gridView1.RowUpdated += new DevExpress.XtraGrid.Views.Base.RowObjectEventHandler(this.gridView1_RowUpdated);
            // 
            // ribbonControl1
            // 
            this.ribbonControl1.ColorScheme = DevExpress.XtraBars.Ribbon.RibbonControlColorScheme.Blue;
            this.ribbonControl1.ExpandCollapseItem.Id = 0;
            this.ribbonControl1.Items.AddRange(new DevExpress.XtraBars.BarItem[] {
            this.ribbonControl1.ExpandCollapseItem,
            this.barButPaySlip,
            this.txtMonth,
            this.txtYear,
            this.barButGSalary,
            this.BtnFind,
            this.btnPrint,
            this.CmboTeacher,
            this.barButtonItem1,
            this.barButtonItem2,
            this.txtDatepicker,
            this.SpinnerCash,
            this.txtpaid,
            this.DateFrom,
            this.DateTo});
            this.ribbonControl1.Location = new System.Drawing.Point(0, 0);
            this.ribbonControl1.MaxItemId = 28;
            this.ribbonControl1.Name = "ribbonControl1";
            this.ribbonControl1.Pages.AddRange(new DevExpress.XtraBars.Ribbon.RibbonPage[] {
            this.ribbonPage1,
            this.ribbonPage2});
            this.ribbonControl1.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.RetxtMonth,
            this.RetxtYear,
            this.RetxtFMonth,
            this.RetxtFYear,
            this.RetxtReports,
            this.CmboTeacherName,
            this.txtDate,
            this.SpinnerCashAsset,
            this.repositoryItemCheckedComboBoxEdit2,
            this.txtPaidElement,
            this.repositoryItemDateEdit1,
            this.repositoryItemDateEdit2});
            this.ribbonControl1.RibbonStyle = DevExpress.XtraBars.Ribbon.RibbonControlStyle.OfficeUniversal;
            this.ribbonControl1.Size = new System.Drawing.Size(1061, 52);
            // 
            // barButPaySlip
            // 
            this.barButPaySlip.Caption = "Pay Slip";
            this.barButPaySlip.Id = 1;
            this.barButPaySlip.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("barButPaySlip.ImageOptions.Image")));
            this.barButPaySlip.ImageOptions.LargeImage = ((System.Drawing.Image)(resources.GetObject("barButPaySlip.ImageOptions.LargeImage")));
            this.barButPaySlip.Name = "barButPaySlip";
            this.barButPaySlip.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barButPaySlip_ItemClick);
            // 
            // txtMonth
            // 
            this.txtMonth.Caption = "Month";
            this.txtMonth.Edit = this.RetxtMonth;
            this.txtMonth.EditWidth = 100;
            this.txtMonth.Id = 5;
            this.txtMonth.Name = "txtMonth";
            this.txtMonth.EditValueChanged += new System.EventHandler(this.barEditItem1_EditValueChanged);
            // 
            // RetxtMonth
            // 
            this.RetxtMonth.AutoHeight = false;
            this.RetxtMonth.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.RetxtMonth.DropDownRows = 12;
            this.RetxtMonth.Name = "RetxtMonth";
            // 
            // txtYear
            // 
            this.txtYear.Caption = "Year";
            this.txtYear.Edit = this.RetxtYear;
            this.txtYear.EditWidth = 70;
            this.txtYear.Id = 6;
            this.txtYear.Name = "txtYear";
            this.txtYear.EditValueChanged += new System.EventHandler(this.barEditItem1_EditValueChanged);
            // 
            // RetxtYear
            // 
            this.RetxtYear.AutoHeight = false;
            this.RetxtYear.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.RetxtYear.DropDownRows = 15;
            this.RetxtYear.Items.AddRange(new object[] {
            "2015",
            "2016",
            "2017",
            "2018",
            "2019",
            "2020",
            "2021",
            "2022",
            "2023",
            "2024",
            "2025",
            "2026",
            "2027",
            "2028",
            "2029",
            "2030"});
            this.RetxtYear.Name = "RetxtYear";
            // 
            // barButGSalary
            // 
            this.barButGSalary.Caption = "Generate Salary";
            this.barButGSalary.Id = 7;
            this.barButGSalary.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("barButGSalary.ImageOptions.Image")));
            this.barButGSalary.ImageOptions.LargeImage = ((System.Drawing.Image)(resources.GetObject("barButGSalary.ImageOptions.LargeImage")));
            this.barButGSalary.Name = "barButGSalary";
            this.barButGSalary.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barButGSalary_ItemClick);
            // 
            // BtnFind
            // 
            this.BtnFind.Caption = "Find";
            this.BtnFind.Id = 15;
            this.BtnFind.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("BtnFind.ImageOptions.Image")));
            this.BtnFind.Name = "BtnFind";
            this.BtnFind.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.BtnFind_ItemClick);
            // 
            // btnPrint
            // 
            this.btnPrint.Caption = "Print";
            this.btnPrint.Id = 16;
            this.btnPrint.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("btnPrint.ImageOptions.Image")));
            this.btnPrint.Name = "btnPrint";
            this.btnPrint.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btnPrint_ItemClick);
            // 
            // CmboTeacher
            // 
            this.CmboTeacher.Caption = "Select Teacher";
            this.CmboTeacher.Edit = this.CmboTeacherName;
            this.CmboTeacher.EditWidth = 150;
            this.CmboTeacher.Id = 19;
            this.CmboTeacher.Name = "CmboTeacher";
            this.CmboTeacher.EditValueChanged += new System.EventHandler(this.CmboTeacher_EditValueChanged);
            // 
            // CmboTeacherName
            // 
            this.CmboTeacherName.AutoHeight = false;
            this.CmboTeacherName.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.CmboTeacherName.Name = "CmboTeacherName";
            // 
            // barButtonItem1
            // 
            this.barButtonItem1.Caption = "Refresh List";
            this.barButtonItem1.Id = 20;
            this.barButtonItem1.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("barButtonItem1.ImageOptions.Image")));
            this.barButtonItem1.ImageOptions.LargeImage = ((System.Drawing.Image)(resources.GetObject("barButtonItem1.ImageOptions.LargeImage")));
            this.barButtonItem1.Name = "barButtonItem1";
            this.barButtonItem1.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barButtonItem1_ItemClick);
            // 
            // barButtonItem2
            // 
            this.barButtonItem2.Caption = "Paid Salary";
            this.barButtonItem2.Id = 21;
            this.barButtonItem2.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("barButtonItem2.ImageOptions.Image")));
            this.barButtonItem2.ImageOptions.LargeImage = ((System.Drawing.Image)(resources.GetObject("barButtonItem2.ImageOptions.LargeImage")));
            this.barButtonItem2.Name = "barButtonItem2";
            this.barButtonItem2.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barButtonItem2_ItemClick);
            // 
            // txtDatepicker
            // 
            this.txtDatepicker.Caption = "Date";
            this.txtDatepicker.Edit = this.txtDate;
            this.txtDatepicker.EditWidth = 120;
            this.txtDatepicker.Id = 22;
            this.txtDatepicker.Name = "txtDatepicker";
            // 
            // txtDate
            // 
            this.txtDate.AutoHeight = false;
            this.txtDate.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.txtDate.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.txtDate.EditFormat.FormatString = "yyyy-MM-dd";
            this.txtDate.EditFormat.FormatType = DevExpress.Utils.FormatType.Custom;
            this.txtDate.Name = "txtDate";
            // 
            // SpinnerCash
            // 
            this.SpinnerCash.Caption = "Cash Assets";
            this.SpinnerCash.Edit = this.SpinnerCashAsset;
            this.SpinnerCash.EditWidth = 150;
            this.SpinnerCash.Id = 23;
            this.SpinnerCash.Name = "SpinnerCash";
            // 
            // SpinnerCashAsset
            // 
            this.SpinnerCashAsset.AutoHeight = false;
            this.SpinnerCashAsset.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.SpinnerCashAsset.Name = "SpinnerCashAsset";
            this.SpinnerCashAsset.PopupView = this.repositoryItemGridLookUpEdit1View;
            // 
            // repositoryItemGridLookUpEdit1View
            // 
            this.repositoryItemGridLookUpEdit1View.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus;
            this.repositoryItemGridLookUpEdit1View.Name = "repositoryItemGridLookUpEdit1View";
            this.repositoryItemGridLookUpEdit1View.OptionsSelection.EnableAppearanceFocusedCell = false;
            this.repositoryItemGridLookUpEdit1View.OptionsView.ShowGroupPanel = false;
            // 
            // txtpaid
            // 
            this.txtpaid.Caption = "Paid Amount";
            this.txtpaid.Edit = this.txtPaidElement;
            this.txtpaid.Id = 25;
            this.txtpaid.Name = "txtpaid";
            this.txtpaid.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
            // 
            // txtPaidElement
            // 
            this.txtPaidElement.AutoHeight = false;
            this.txtPaidElement.Name = "txtPaidElement";
            // 
            // DateFrom
            // 
            this.DateFrom.Caption = "From";
            this.DateFrom.Edit = this.repositoryItemDateEdit1;
            this.DateFrom.EditWidth = 125;
            this.DateFrom.Id = 26;
            this.DateFrom.Name = "DateFrom";
            // 
            // repositoryItemDateEdit1
            // 
            this.repositoryItemDateEdit1.AutoHeight = false;
            this.repositoryItemDateEdit1.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.repositoryItemDateEdit1.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.repositoryItemDateEdit1.Name = "repositoryItemDateEdit1";
            // 
            // DateTo
            // 
            this.DateTo.Caption = "To";
            this.DateTo.Edit = this.repositoryItemDateEdit2;
            this.DateTo.EditWidth = 125;
            this.DateTo.Id = 27;
            this.DateTo.Name = "DateTo";
            // 
            // repositoryItemDateEdit2
            // 
            this.repositoryItemDateEdit2.AutoHeight = false;
            this.repositoryItemDateEdit2.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.repositoryItemDateEdit2.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.repositoryItemDateEdit2.Name = "repositoryItemDateEdit2";
            // 
            // ribbonPage1
            // 
            this.ribbonPage1.Groups.AddRange(new DevExpress.XtraBars.Ribbon.RibbonPageGroup[] {
            this.ribbonPageGroup2});
            this.ribbonPage1.Name = "ribbonPage1";
            this.ribbonPage1.Text = "Home";
            // 
            // ribbonPageGroup2
            // 
            this.ribbonPageGroup2.ItemLinks.Add(this.txtMonth);
            this.ribbonPageGroup2.ItemLinks.Add(this.txtYear);
            this.ribbonPageGroup2.ItemLinks.Add(this.BtnFind);
            this.ribbonPageGroup2.ItemLinks.Add(this.btnPrint);
            this.ribbonPageGroup2.ItemLinks.Add(this.barButPaySlip);
            this.ribbonPageGroup2.ItemLinks.Add(this.DateFrom);
            this.ribbonPageGroup2.ItemLinks.Add(this.DateTo);
            this.ribbonPageGroup2.ItemLinks.Add(this.barButGSalary);
            this.ribbonPageGroup2.Name = "ribbonPageGroup2";
            this.ribbonPageGroup2.Text = "ribbonPageGroup2";
            // 
            // ribbonPage2
            // 
            this.ribbonPage2.Groups.AddRange(new DevExpress.XtraBars.Ribbon.RibbonPageGroup[] {
            this.ribbonPageGroup3});
            this.ribbonPage2.Name = "ribbonPage2";
            this.ribbonPage2.Text = "Bulk Paid";
            // 
            // ribbonPageGroup3
            // 
            this.ribbonPageGroup3.ItemLinks.Add(this.CmboTeacher);
            this.ribbonPageGroup3.ItemLinks.Add(this.barButtonItem1);
            this.ribbonPageGroup3.ItemLinks.Add(this.txtpaid);
            this.ribbonPageGroup3.ItemLinks.Add(this.txtDatepicker);
            this.ribbonPageGroup3.ItemLinks.Add(this.SpinnerCash);
            this.ribbonPageGroup3.ItemLinks.Add(this.barButtonItem2);
            this.ribbonPageGroup3.Name = "ribbonPageGroup3";
            this.ribbonPageGroup3.Text = "ribbonPageGroup3";
            // 
            // RetxtFMonth
            // 
            this.RetxtFMonth.AutoHeight = false;
            this.RetxtFMonth.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.RetxtFMonth.DropDownRows = 12;
            this.RetxtFMonth.Name = "RetxtFMonth";
            // 
            // RetxtFYear
            // 
            this.RetxtFYear.AutoHeight = false;
            this.RetxtFYear.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.RetxtFYear.DropDownRows = 17;
            this.RetxtFYear.Items.AddRange(new object[] {
            "2015",
            "2016",
            "2017",
            "2018",
            "2019",
            "2020",
            "2021",
            "2022",
            "2023",
            "2024",
            "2025",
            "2026",
            "2027",
            "2028",
            "2029",
            "2030"});
            this.RetxtFYear.Name = "RetxtFYear";
            // 
            // RetxtReports
            // 
            this.RetxtReports.AutoHeight = false;
            this.RetxtReports.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.RetxtReports.Items.AddRange(new object[] {
            "Bank Slip",
            "Admin Staff",
            "Permanent Teaching Staff",
            "Visiting Teaching Staff",
            "Non Teaching Staff"});
            this.RetxtReports.Name = "RetxtReports";
            // 
            // repositoryItemCheckedComboBoxEdit2
            // 
            this.repositoryItemCheckedComboBoxEdit2.AutoHeight = false;
            this.repositoryItemCheckedComboBoxEdit2.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.repositoryItemCheckedComboBoxEdit2.Name = "repositoryItemCheckedComboBoxEdit2";
            // 
            // barEditItem3
            // 
            this.barEditItem3.Caption = "Month";
            this.barEditItem3.Edit = this.RetxtMonth;
            this.barEditItem3.Id = 5;
            this.barEditItem3.Name = "barEditItem3";
            // 
            // barButFind
            // 
            this.barButFind.Caption = "Find";
            this.barButFind.Id = 10;
            this.barButFind.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("barButFind.ImageOptions.Image")));
            this.barButFind.ImageOptions.LargeImage = ((System.Drawing.Image)(resources.GetObject("barButFind.ImageOptions.LargeImage")));
            this.barButFind.Name = "barButFind";
            // 
            // barEditItem4
            // 
            this.barEditItem4.Caption = "Date";
            this.barEditItem4.Edit = this.txtDate;
            this.barEditItem4.EditWidth = 120;
            this.barEditItem4.Id = 22;
            this.barEditItem4.Name = "barEditItem4";
            // 
            // SalaryManagement
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.gridSalary);
            this.Controls.Add(this.ribbonControl1);
            this.Name = "SalaryManagement";
            this.Size = new System.Drawing.Size(1061, 439);
            ((System.ComponentModel.ISupportInitialize)(this.gridSalary)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ribbonControl1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.RetxtMonth)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.RetxtYear)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.CmboTeacherName)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtDate.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtDate)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.SpinnerCashAsset)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemGridLookUpEdit1View)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtPaidElement)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemDateEdit1.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemDateEdit1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemDateEdit2.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemDateEdit2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.RetxtFMonth)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.RetxtFYear)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.RetxtReports)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemCheckedComboBoxEdit2)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraGrid.GridControl gridSalary;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView1;
        private DevExpress.XtraBars.Ribbon.RibbonControl ribbonControl1;
        private DevExpress.XtraBars.Ribbon.RibbonPage ribbonPage1;
        private DevExpress.XtraBars.BarButtonItem barButPaySlip;
        private DevExpress.XtraBars.Ribbon.RibbonPageGroup ribbonPageGroup2;
        private DevExpress.XtraBars.BarEditItem txtMonth;
        private DevExpress.XtraEditors.Repository.RepositoryItemComboBox RetxtMonth;
        private DevExpress.XtraBars.BarEditItem txtYear;
        private DevExpress.XtraEditors.Repository.RepositoryItemComboBox RetxtYear;
        private DevExpress.XtraBars.BarEditItem barEditItem3;
        private DevExpress.XtraBars.BarButtonItem barButGSalary;
        private DevExpress.XtraEditors.Repository.RepositoryItemComboBox RetxtFMonth;
        private DevExpress.XtraEditors.Repository.RepositoryItemComboBox RetxtFYear;
        private DevExpress.XtraEditors.Repository.RepositoryItemComboBox RetxtReports;
        private DevExpress.XtraBars.BarButtonItem barButFind;
        private DevExpress.XtraBars.BarButtonItem BtnFind;
        private DevExpress.XtraBars.BarButtonItem btnPrint;
        private DevExpress.XtraBars.Ribbon.RibbonPage ribbonPage2;
        private DevExpress.XtraBars.Ribbon.RibbonPageGroup ribbonPageGroup3;
        private DevExpress.XtraBars.BarEditItem CmboTeacher;
        private DevExpress.XtraEditors.Repository.RepositoryItemCheckedComboBoxEdit CmboTeacherName;
        private DevExpress.XtraBars.BarButtonItem barButtonItem1;
        private DevExpress.XtraBars.BarButtonItem barButtonItem2;
        private DevExpress.XtraBars.BarEditItem txtDatepicker;
        private DevExpress.XtraEditors.Repository.RepositoryItemDateEdit txtDate;
        private DevExpress.XtraBars.BarEditItem SpinnerCash;
        private DevExpress.XtraEditors.Repository.RepositoryItemGridLookUpEdit SpinnerCashAsset;
        private DevExpress.XtraGrid.Views.Grid.GridView repositoryItemGridLookUpEdit1View;
        private DevExpress.XtraEditors.Repository.RepositoryItemCheckedComboBoxEdit repositoryItemCheckedComboBoxEdit2;
        private DevExpress.XtraBars.BarEditItem txtpaid;
        private DevExpress.XtraEditors.Repository.RepositoryItemTextEdit txtPaidElement;
        private DevExpress.XtraBars.BarEditItem DateFrom;
        private DevExpress.XtraEditors.Repository.RepositoryItemDateEdit repositoryItemDateEdit1;
        private DevExpress.XtraBars.BarEditItem DateTo;
        private DevExpress.XtraEditors.Repository.RepositoryItemDateEdit repositoryItemDateEdit2;
        private DevExpress.XtraBars.BarEditItem barEditItem4;
    }
}
