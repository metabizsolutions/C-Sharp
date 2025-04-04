using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.XtraGrid;
using SchoolManagementSystem.Students;
using DevExpress.XtraReports.UI;
using DevExpress.LookAndFeel;

namespace SchoolManagementSystem.Admin
{
    public partial class sms_history : UserControl
    {
        private static sms_history _instance;

        public static sms_history instance
        {
            get
            {
                if (_instance == null)
                    _instance = new sms_history();
                return _instance;
            }
        }
        CommonFunctions fun = new CommonFunctions();
        public sms_history()
        {
            InitializeComponent();
            loadfunctions();
        }
        public void loadfunctions()
        {
            DateTime now = DateTime.Now;

            dtp_from.Value = new DateTime(now.Year, now.Month, 1);
            dtp_To.Value = dtp_from.Value.AddMonths(1).AddDays(-1);
            string todaydate = dtp_To.Value.ToString("yyyy-MM-dd"); ;
            string fromdate = dtp_from.Value.ToString("yyyy-MM-dd"); ;
            FillGridsmsHistory( fromdate, todaydate);
        }
        void FillGridsmsHistory(string from, string to)
        {
            string query = "SELECT `id`, `mobile`, `sms`, `status`, FROM_UNIXTIME(`ondate`,'%Y-%m-%d') as Date,FROM_UNIXTIME(`ondate`,'%h:%i:%s') as Time, `count`, `response` FROM `sms_history` where FROM_UNIXTIME(`ondate`,'%Y-%m-%d') between '"+from+"' and '"+to+"'";
            gridControl1.DataSource = fun.FetchDataTable(query);

            GridColumnSummaryItem item1 = new GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Sum, "count", "Sum={0}");
            gridView1.Columns["count"].Summary.Clear();
            gridView1.Columns["count"].Summary.Add(item1);

        }
        private void dtp_from_ValueChanged(object sender, EventArgs e)
        {
            dtp_To.Enabled = false;
            dtp_To.MinDate = dtp_from.Value;
            dtp_To.Enabled = true;
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            string todaydate = dtp_To.Value.ToString("yyyy-MM-dd"); ;
            string fromdate = dtp_from.Value.ToString("yyyy-MM-dd"); ;
           fun.loaderform(() => { FillGridsmsHistory(fromdate, todaydate); });
        }

        private void btnnPrint_Click(object sender, EventArgs e)
        {
            XtraStudentAdmissionReport report = new XtraStudentAdmissionReport();
            Image logo = fun.Base64ToImage(Login.Logo);
            var school = fun.GetSettings("system_title");
            report.LabTel.Text = "SMS History Report";
            report.PicIogoBox.Image = logo;
            report.LabTitle.Text = school;
            report.LabAddress.Text = fun.GetSettings("address");
            report.labDate.Text = dtp_To.Value.ToString("yyyy-MM-dd")+" - " + dtp_from.Value.ToString("yyyy-MM-dd");
            report.GridControl = gridControl1;

            ReportPrintTool printTool = new ReportPrintTool(report);
            printTool.PreviewForm.PrintingSystem.Document.AutoFitToPagesWidth = 1;
            printTool.ShowRibbonPreviewDialog(UserLookAndFeel.Default);
        }
    }
}
