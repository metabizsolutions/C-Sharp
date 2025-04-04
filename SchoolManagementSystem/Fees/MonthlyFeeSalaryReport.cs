using DevExpress.LookAndFeel;
using DevExpress.XtraGrid;
using DevExpress.XtraPrinting;
using DevExpress.XtraReports.UI;
using MySql.Data.MySqlClient;
using System;
using System.Data;
using System.Drawing;
using System.Drawing.Printing;

namespace SchoolManagementSystem.Fees
{
    public partial class MonthlyFeeSalaryReport : DevExpress.XtraEditors.XtraUserControl
    {
        CommonFunctions fun = new CommonFunctions();
        string DateStr = "", FromDateStr, ToDateStr;
        public MonthlyFeeSalaryReport()
        {
            InitializeComponent();
        }
        private void btnFind_Click(object sender, EventArgs e)
        {
            fun.loaderform(() =>
            {
                if (txtCategory.Text == "Fee")
                    FMonthlyReport();
                if (txtCategory.Text == "Salary")
                    SMonthlyReport();

            });
        }
        void SMonthlyReport()
        {
            var where = " AND date >= '"+FromDateStr+"' AND date <= '"+ ToDateStr + "' ";

            MySqlConnection con = new MySqlConnection(Login.constring);
            con.Open();
            // MySqlCommand cmdB = new MySqlCommand("SELECT salary.salary_id as ID,teacher.name as Name,previous as Previous,current as Monthly, amount as Amount, paid as Paid, due as Due, status as Status, date as Date FROM salary join teacher on(teacher.teacher_id = salary.teacher_id)  where " + date + " ;", con);
            MySqlCommand cmdB = new MySqlCommand("SELECT NAME,`Salary`,`Addition`,`Total`,Advance,Paid,`Due.` FROM salarys WHERE 1 = 1 " + where, con);

            MySqlDataAdapter adpSalary = new MySqlDataAdapter(cmdB);

            DataSet ds = new DataSet();
            adpSalary.Fill(ds, "Salary");
            gridMonthlyReport.DataSource = null;
            gridView1.Columns.Clear();
            gridMonthlyReport.DataSource = ds.Tables["Salary"];

            GridColumnSummaryItem item1 = new GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Sum, "Salary", "{0}");
            GridColumnSummaryItem item2 = new GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Sum, "Addition", "{0}");
            GridColumnSummaryItem item4 = new GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Sum, "Total", "{0}");
            GridColumnSummaryItem item5 = new GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Sum, "Advance", "{0}");
            GridColumnSummaryItem item7 = new GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Sum, "Paid", "{0}");
            GridColumnSummaryItem item8 = new GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Sum, "Due.", "{0}");
            gridView1.Columns["Salary"].Summary.Add(item1);
            gridView1.Columns["Addition"].Summary.Add(item2);
            gridView1.Columns["Total"].Summary.Add(item4);
            gridView1.Columns["Advance"].Summary.Add(item5);
            gridView1.Columns["Paid"].Summary.Add(item7);
            gridView1.Columns["Due."].Summary.Add(item8);
            gridView1.BestFitColumns();
            con.Close();
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

        private void BtnPrint_Click(object sender, EventArgs e)
        {
            print();
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

        private void link_CreateReportHeaderArea(object sender, CreateAreaEventArgs e)
        {
            Image logo = fun.Base64ToImage(Login.Logo);
            var school = fun.GetSettings("system_title");

            string reportHeader = "\t\t          " + school +
                                  "\n\r\n\r                      " + "Session: " + Main_FD.SelectedSession + " Report: " + txtCategory.Text
                                + "\n\r\t\t\t\t       Month: " + DateStr;
            e.Graph.StringFormat = new BrickStringFormat(StringAlignment.Near);
            e.Graph.Font = new Font("Tahoma", 11, FontStyle.Regular);
            RectangleF rec = new RectangleF(0, 5, e.Graph.ClientPageSize.Width, 80);
            e.Graph.DrawString(reportHeader, Color.Black, rec, BorderSide.None);
            RectangleF recI = new RectangleF(10, 7, 80, 80);
            e.Graph.DrawImage(logo, recI, BorderSide.None, Color.Transparent);
        }
    }
}
