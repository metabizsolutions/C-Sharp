using DevExpress.XtraReports.UI;

namespace SchoolManagementSystem.Fees
{
    public partial class MasterFeeReciptReportBank : DevExpress.XtraReports.UI.XtraReport
    {
        public MasterFeeReciptReportBank()
        {
            InitializeComponent();
        }
        IncomeManagement inCome = new IncomeManagement();

        private void xrSubreport1_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            FeeRecipt_Bank recipt = new FeeRecipt_Bank();
            recipt = inCome.showReportBank(true);
            recipt.LabReport.Text = "Fee Invoice (Student Copy)";
            ((XRSubreport)sender).ReportSource = recipt;
        }

        private void xrSubreport2_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            FeeRecipt_Bank recipt = new FeeRecipt_Bank();
            recipt = inCome.showReportBank(true);
            recipt.LabReport.Text = "Fee Invoice (Accounts Copy)";
            ((XRSubreport)sender).ReportSource = recipt;

        }

        private void xrSubreport3_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            FeeRecipt_Bank recipt = new FeeRecipt_Bank();
            recipt = inCome.showReportBank(true);
            recipt.LabReport.Text = "Fee Invoice (Bank Copy)";
            ((XRSubreport)sender).ReportSource = recipt;

        }
    }
}
