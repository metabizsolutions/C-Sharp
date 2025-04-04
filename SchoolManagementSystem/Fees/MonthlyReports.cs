using DevExpress.LookAndFeel;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Repository;
using DevExpress.XtraGrid;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraPrinting;
using DevExpress.XtraReports.UI;
using MySql.Data.MySqlClient;
using System;
using System.Collections;
using System.Data;
using System.Drawing;
using System.Drawing.Printing;
using System.Text;

namespace SchoolManagementSystem.Fees
{
    public partial class MonthlyReports : DevExpress.XtraEditors.XtraUserControl
    {
        private static MonthlyReports _instance;

        public static MonthlyReports instance
        {
            get
            {
                if (_instance == null)
                    _instance = new MonthlyReports();
                return _instance;
            }
        }
        GridCheckMarksSelection gridCheckMarksSA;
        CommonFunctions fun = new CommonFunctions();
        public MonthlyReports()
        {
            InitializeComponent();
            loadfunctions();

        }
        public void loadfunctions()
        {
            string query = "SELECT teacher_id AS id,`name`,designation,staff_type,IF(passout = 1, 'Left','Active') AS passout FROM teacher ORDER BY `name` ASC ";
            DataTable table = fun.FetchDataTable(query);
            GridTeacherList.Text = "";
            GridTeacherList.Properties.DataSource = table;
            GridTeacherList.Properties.DisplayMember = "name";
            GridTeacherList.Properties.ValueMember = "id";
            GridTeacherList.Properties.View.OptionsSelection.MultiSelect = true;
            GridTeacherList.CustomDisplayText += new DevExpress.XtraEditors.Controls.CustomDisplayTextEventHandler(gridLookUpEdit1_CustomDisplayText);
            GridTeacherList.Properties.PopulateViewColumns();

            gridCheckMarksSA = new GridCheckMarksSelection(GridTeacherList.Properties);
            gridCheckMarksSA.SelectionChanged += new GridCheckMarksSelection.SelectionChangedEventHandler(gridCheckMarks_SelectionChanged);
            GridTeacherList.Properties.Tag = gridCheckMarksSA;
        }
        void gridCheckMarks_SelectionChanged(object sender, EventArgs e)
        {
            if (ActiveControl is GridLookUpEdit)
            {
                StringBuilder sb = new StringBuilder();
                foreach (DataRowView rv in (sender as GridCheckMarksSelection).Selection)
                {
                    if (sb.ToString().Length > 0) { sb.Append(", "); }
                    sb.Append(rv["name"].ToString());
                }
                (ActiveControl as GridLookUpEdit).Text = sb.ToString();
            }
        }


        void gridLookUpEdit1_CustomDisplayText(object sender, DevExpress.XtraEditors.Controls.CustomDisplayTextEventArgs e)
        {
            StringBuilder sb = new StringBuilder();
            GridCheckMarksSelection gridCheckMark = sender is GridLookUpEdit ? (sender as GridLookUpEdit).Properties.Tag as GridCheckMarksSelection : (sender as RepositoryItemGridLookUpEdit).Tag as GridCheckMarksSelection;
            if (gridCheckMark == null) return;
            foreach (DataRowView rv in gridCheckMark.Selection)
            {
                if (sb.ToString().Length > 0) { sb.Append(", "); }
                sb.Append(rv["name"].ToString());
            }
            e.DisplayText = sb.ToString();
        }
        String FromDateStr, ToDateStr;
        private void btnFind_Click(object sender, EventArgs e)
        {
            fun.loaderform(() =>
            {
                FromDateStr = Convert.ToDateTime(DateFrom.Text).ToString("yyyy-MM-dd");
                ToDateStr = Convert.ToDateTime(DateTo.Text).ToString("yyyy-MM-dd");

                if (txtCategory.Text == "Fee")
                    FMonthlyReport();
                if (txtCategory.Text == "Salary")
                    SMonthlyReport();
                if (txtCategory.Text == "Expanse/Income")
                    IncomeExpanseMonthlyReport();

            });
        }
        void SMonthlyReport()
        {
            GridCheckMarksSelection gridCheckMark = GridTeacherList.Properties.Tag as GridCheckMarksSelection;
            ArrayList valueList = gridCheckMark.Selection;
            int[] teacher_array = new int[valueList.Count];
            int count = 0;
            foreach (DataRowView rv in valueList)
            {
                teacher_array[count] = Convert.ToInt32(rv["id"]);
                count++;
            }


            string extra_col = "";
            string query_cat = "SELECT salary_cat_id,salary_title FROM salary_category ORDER BY salary_cat_id ASC ";
            DataTable table_cat = fun.GetQueryTable(query_cat);
            count = 0;
            int salary_cat_id;
            foreach (DataRow row in table_cat.Rows)
            {
                salary_cat_id = Convert.ToInt32(row["salary_cat_id"].ToString());
                extra_col += ",(SELECT amount FROM salary_cat_amount WHERE salary_cat_id = '" + salary_cat_id + "' AND salary_id = salarys.ID) AS '" + row["salary_title"].ToString() + "'";
                count++;
            }
            var where = " AND salarys.date >= '" + FromDateStr + "' AND salarys.date <= '" + ToDateStr + "' ";
            if (teacher_array.Length > 0)
            {
                where += " AND salary.teacher_id IN("+ String.Join(",", teacher_array) + ")";
            }
            String query = "SELECT salarys.* " + extra_col + " " +
                            " FROM salarys " +
                            " INNER JOIN salary ON salary.salary_id = salarys.ID " +
                            " where 1 = 1 " + where + " ORDER BY salarys.ID ASC";
            DataTable table = fun.GetQueryTable(query);

            gridMonthlyReport.DataSource = null;
            gridView1.Columns.Clear();
            gridMonthlyReport.DataSource = table;

            GridColumnSummaryItem item1 = new GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Sum, "Salary", "{0}");
            GridColumnSummaryItem item2 = new GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Sum, "Addition", "{0}");
            GridColumnSummaryItem item4 = new GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Sum, "Total", "{0}");
            GridColumnSummaryItem item5 = new GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Sum, "Advance", "{0}");
            GridColumnSummaryItem item7 = new GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Sum, "Paid", "{0}");
            GridColumnSummaryItem item8 = new GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Sum, "Due.", "{0}");
            gridView1.Columns["Salary"].Summary.Add(item1);
            gridView1.Columns["addition"].Summary.Add(item2);
            gridView1.Columns["Total"].Summary.Add(item4);
            gridView1.Columns["Advance"].Summary.Add(item5);
            gridView1.Columns["Paid"].Summary.Add(item7);
            gridView1.Columns["Due."].Summary.Add(item8);
            if (table_cat.Rows.Count > 0)
            {
                foreach (DataRow row in table_cat.Rows)
                {
                    gridView1.Columns[row["salary_title"].ToString()].Summary.Add(new GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Sum, row["salary_title"].ToString(), "{0}"));
                }
            }
            gridView1.BestFitColumns();
        }
        void FMonthlyReport()
        {
            var where = " AND date >= '" + FromDateStr + "' AND date <= '" + ToDateStr + "' ";
            MySqlConnection con = new MySqlConnection(Login.constring);
            con.Open();
            MySqlCommand cmdB = new MySqlCommand("SELECT student.student_id as StdID,student.name as Name,class.name as Class,section.name AS Section,title as Title,description as Description,previous_fee as Previous,current_fee as Current,invoice.fee_concession as Concession,amount as Amount,amount_paid as Paid,due as Due,status as Status,date as Date " +
                "FROM invoice join student on(student.student_id = invoice.student_id) " +
                "left join class on class.class_id=student.class_id " +
                " LEFT JOIN section ON section.section_id = student.section_id "+
                "where 1 = 1 " + where + " ;", con);
            MySqlDataAdapter adpStudent = new MySqlDataAdapter(cmdB);
            DataSet ds = new DataSet();
            adpStudent.Fill(ds, "Student");
            gridMonthlyReport.DataSource = null;
            gridView1.Columns.Clear();
            gridMonthlyReport.DataSource = ds.Tables["Student"];
            var col = gridView1.Columns["Date"];
            col.Group();
            gridView1.ExpandAllGroups();

            gridView1.GroupSummary.Clear();
            GridGroupSummaryItem item = new GridGroupSummaryItem();
            item.FieldName = "Name";
            item.SummaryType = DevExpress.Data.SummaryItemType.Count;
            gridView1.GroupSummary.Add(item);

            GridColumnSummaryItem item2 = new GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Sum, "Current", "Total={0}");
            GridColumnSummaryItem item3 = new GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Sum, "Concession", "Concession={0}");
            GridColumnSummaryItem item4 = new GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Sum, "Paid", "Paid={0}");
            GridColumnSummaryItem item1 = new GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Custom, "Due", "Dues={0}", tag: "Dues");

            gridView1.Columns["Current"].Summary.Add(item2);
            gridView1.Columns["Concession"].Summary.Add(item3);
            gridView1.Columns["Paid"].Summary.Add(item4);
            gridView1.Columns["Due"].Summary.Add(item1);
            gridView1.BestFitColumns();
            con.Close();
        }
        void IncomeExpanseMonthlyReport()
        {
            var where = " AND date >= '" + FromDateStr + "' AND date <= '" + ToDateStr + "' ";
            MySqlConnection con = new MySqlConnection(Login.constring);
            con.Open();
            //payment.expense_category_id != '8' and payment.expense_category_id != '7'
            MySqlCommand cmdP = new MySqlCommand("SELECT payment_id as ID,expense_category.name as Category,payment.title as Title,expense_category.type as PaymentType,cash_assets.title as Method, " +
                                                   "description as Description,amount as Payment, date as Date, admin.name as User " +
                                                   " FROM payment " +
                                                   " LEFT JOIN cash_assets ON cash_assets.cashasset_id = payment.cashasset_id " +
                                                   " join expense_category on(expense_category.expense_category_id = payment.expense_category_id) " +
                                                   " join admin on(admin.admin_id = payment.user_id) where 1=1  " + where + " ", con);

            string query = "SELECT student.student_id as StdID,student.name as Name,class.name as Class,section.name as Section ,title as Title,description as Description," +
                "previous_fee as Previous,current_fee as Current,invoice.fee_concession as Concession,amount as Amount," +
                "amount_paid as Paid,due as Due,status as Status,date as Date " +
                "FROM invoice join student on(student.student_id = invoice.student_id) " +
                "left join class on class.class_id=student.class_id " +
                "left join section on section.section_id=student.section_id " +
                "where 1 = 1 " + where + " ;";
            MySqlCommand cmdB = new MySqlCommand(query, con);
            MySqlDataAdapter adpStudent = new MySqlDataAdapter(cmdP);
            DataSet ds = new DataSet();
            adpStudent.Fill(ds, "IncomeExpanse");
            gridMonthlyReport.DataSource = null;
            gridView1.Columns.Clear();
            gridMonthlyReport.DataSource = ds.Tables["IncomeExpanse"];
            var col = gridView1.Columns["PaymentType"];
            col.Group();
            gridView1.ExpandAllGroups();
            long income = 0;
            long expense = 0;
            long total = 0;
            foreach(DataRow dr in ds.Tables["IncomeExpanse"].Rows)
            {
                if (dr["PaymentType"].ToString() == "Income")
                    income += Convert.ToInt64(dr["Payment"].ToString());
                if (dr["PaymentType"].ToString() == "Expense")
                    expense += Convert.ToInt64(dr["Payment"].ToString());
            }
            total = income - expense;
            gridView1.GroupSummary.Clear();
            GridGroupSummaryItem item = new GridGroupSummaryItem();
            item.FieldName = "Category";
            item.SummaryType = DevExpress.Data.SummaryItemType.Count;
            gridView1.GroupSummary.Add(item);

            gridView1.GroupSummary.Add(DevExpress.Data.SummaryItemType.Sum, "Payment", gridView1.Columns["Payment"]);
            GridColumnSummaryItem item4 = new GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Custom, "Payment", total.ToString());

            gridView1.Columns["Payment"].Summary.Add(item4);
            gridView1.BestFitColumns();
            con.Close();
        }
        int current = 0;
        int concession = 0;
        int paid = 0;
        private void gridView1_CustomSummaryCalculate(object sender, DevExpress.Data.CustomSummaryEventArgs e)
        {
            GridColumnSummaryItem item = e.Item as GridColumnSummaryItem;
            GridView view = sender as GridView;
            if (Equals("Dues", item.Tag))
            {
                if (e.SummaryProcess == DevExpress.Data.CustomSummaryProcess.Start)
                {
                    current = 0;
                    concession = 0;
                    paid = 0;
                }
                if (e.SummaryProcess == DevExpress.Data.CustomSummaryProcess.Calculate)
                {
                    current += Convert.ToInt32(view.GetRowCellValue(e.RowHandle, "Current"));
                    concession += Convert.ToInt32(view.GetRowCellValue(e.RowHandle, "Concession"));
                    paid += Convert.ToInt32(view.GetRowCellValue(e.RowHandle, "Paid"));
                }
                if (e.SummaryProcess == DevExpress.Data.CustomSummaryProcess.Finalize)
                    e.TotalValue = current - concession - paid;
            }
            if (Equals("SDues", item.Tag))
            {
                if (e.SummaryProcess == DevExpress.Data.CustomSummaryProcess.Start)
                {
                    current = 0;
                    paid = 0;
                }
                if (e.SummaryProcess == DevExpress.Data.CustomSummaryProcess.Calculate)
                {
                    current += Convert.ToInt32(view.GetRowCellValue(e.RowHandle, "Monthly"));
                    paid += Convert.ToInt32(view.GetRowCellValue(e.RowHandle, "Paid"));
                    // if (!Convert.ToBoolean(view.GetRowCellValue(e.RowHandle, "Due"))) validRowCount++;
                }
                if (e.SummaryProcess == DevExpress.Data.CustomSummaryProcess.Finalize)
                    e.TotalValue = current - paid;
            }
        }

        private void BtnPrint_Click(object sender, EventArgs e)
        {
            print();
            //  PreviewPrintableComponent(gridMonthlyReport, gridMonthlyReport.LookAndFeel);
        }
        void print()
        {
            Image logo = fun.Base64ToImage(Login.Logo);
            var school = fun.GetSettings("system_title");
            FeeReport report = new FeeReport();
            report.PicIogoBox.Image = logo;
            report.LabTitle.Text = school;
            report.GridControl = gridMonthlyReport;
            report.GridControl.Width = 100;
            report.LabAddress.Text = fun.GetSettings("address");
            report.LabTel.Text = fun.GetSettings("phone");
            report.LabReport.Text = txtCategory.Text + " Report";




            //documentViewer1.DocumentSource = report;

            //report.CreateDocument();
            //documentViewer1.PrintingSystem.Document.AutoFitToPagesWidth = 1;
            ReportPrintTool printTool = new ReportPrintTool(report);
            printTool.PreviewForm.PrintControl.Width = 96;
            printTool.ShowRibbonPreviewDialog(UserLookAndFeel.Default);

        }


        void PreviewPrintableComponent(IPrintable component, UserLookAndFeel lookAndFeel)
        {
            PrintableComponentLink link = new PrintableComponentLink()
            {
                PrintingSystemBase = new PrintingSystemBase(),
                Component = component,
                Landscape = false,
                PaperKind = PaperKind.A4,
                Margins = new Margins(20, 20, 20, 20)
            };
            link.CreateReportHeaderArea += link_CreateReportHeaderArea;
            link.ShowRibbonPreview(lookAndFeel);
        }

        private void txtCategory_EditValueChanged(object sender, EventArgs e)
        {
            if (txtCategory.Text == "Salary")
            {
                GridTeacherList.Visible = true;
            }
            else
            {
                GridTeacherList.Visible = false;
            }
        }

        private void link_CreateReportHeaderArea(object sender, CreateAreaEventArgs e)
        {
            Image logo = fun.Base64ToImage(Login.Logo);
            var school = fun.GetSettings("system_title");

            string reportHeader = "\t\t          " + school +
                                  "\n\r\n\r                      " + "Session: " + Main_FD.SelectedSession + " Report: " + txtCategory.Text
                                + "\n\r\t\t\t\t       From: " + FromDateStr + " To: " + ToDateStr;
            e.Graph.StringFormat = new BrickStringFormat(StringAlignment.Near);
            e.Graph.Font = new Font("Tahoma", 11, FontStyle.Regular);
            RectangleF rec = new RectangleF(0, 5, e.Graph.ClientPageSize.Width, 80);
            e.Graph.DrawString(reportHeader, Color.Black, rec, BorderSide.None);
            RectangleF recI = new RectangleF(10, 7, 80, 80);
            e.Graph.DrawImage(logo, recI, BorderSide.None, Color.Transparent);
        }
    }
}
