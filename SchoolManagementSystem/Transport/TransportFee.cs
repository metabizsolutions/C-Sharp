using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;

namespace SchoolManagementSystem.Transport
{
    public partial class TransportFee : DevExpress.XtraReports.UI.XtraReport
    {
        public TransportFee()
        {
            InitializeComponent();
        }
        Transport_student inCome = new Transport_student();
        private void xrSubreport1_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            TraFee recipt = new TraFee();
            recipt = inCome.showReport();
            recipt.LabReport.Text = "Student Copy";
            ((XRSubreport)sender).ReportSource = recipt;
        }

        private void xrSubreport2_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            TraFee recipt = new TraFee();
            recipt = inCome.showReport();
            recipt.LabReport.Text = "Admin Copy";
            ((XRSubreport)sender).ReportSource = recipt;
        }

        private void xrSubreport3_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            TraFee recipt = new TraFee();
            recipt = inCome.showReport();
            recipt.LabReport.Text = "Bank Copy";
            ((XRSubreport)sender).ReportSource = recipt;
        }
    }
}
