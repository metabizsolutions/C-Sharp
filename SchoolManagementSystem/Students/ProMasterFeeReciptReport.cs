using DevExpress.XtraReports.UI;
using SchoolManagementSystem.Students;

namespace SchoolManagementSystem.Fees
{
    public partial class ProMasterFeeReciptReport : DevExpress.XtraReports.UI.XtraReport
    {
        public ProMasterFeeReciptReport()
        {
            InitializeComponent();
        }
        VisitingInformation inCome = new VisitingInformation();

        private void xrSubreport1_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            ProFeeRecipt recipt = new ProFeeRecipt();
            recipt = inCome.showReport();
            recipt.LabReport.Text = "Student Copy";
            ((XRSubreport)sender).ReportSource = recipt;
        }

        private void xrSubreport2_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            ProFeeRecipt recipt = new ProFeeRecipt();
            recipt = inCome.showReport();
            recipt.LabReport.Text = "Admin Copy";
            ((XRSubreport)sender).ReportSource = recipt;

        }

        private void xrSubreport3_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            ProFeeRecipt recipt = new ProFeeRecipt();
            recipt = inCome.showReport();
            recipt.LabReport.Text = "Bank Copy";
            ((XRSubreport)sender).ReportSource = recipt;

        }
    }
}
