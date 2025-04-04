using DevExpress.XtraReports.UI;
using System.Data;

namespace SchoolManagementSystem.Fees
{
    public partial class FeeRecipt_double: DevExpress.XtraReports.UI.XtraReport
    {
        DataRow row1, row2, row3, row4;
        public FeeRecipt_double(DataRow row1, DataRow row2, DataRow row3, DataRow row4)
        {
            InitializeComponent();
            this.row1 = row1;
            this.row2 = row2;
            this.row3 = row3;
            this.row4 = row4;
        }
        CommonFunctions fun = new CommonFunctions();
        public FeeRecipt_double()
        {
            InitializeComponent();
        }
        private void xrSubreport1_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
           /* if (row1 != null)
            {
                FeeRecipt_Cashold recipt = new FeeRecipt_Cashold();
                recipt = inCome.showReportCash(row1, true);
                recipt.LabReport.Text = "  Student Copy";
                ((XRSubreport)sender).ReportSource = recipt;
            }
            else
            {
                ((XRSubreport)sender).ReportSource.Visible = false;
            }*/
        }
        private void xrSubreport2_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            /*if (row2 != null)
            {
                FeeRecipt_Cashold recipt = new FeeRecipt_Cashold();
                recipt = inCome.showReportCash(row2, true);
                recipt.LabReport.Text = "  Accounts Copy";
                ((XRSubreport)sender).ReportSource = recipt;
            }
            else
            {
                ((XRSubreport)sender).ReportSource.Visible = false;
            }*/

        }
        private void xrRichText1_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            if (row1 != null)
                xrRichText1.LoadFile(fun.fee_receipts(row1, true, "  Student Copy"), XRRichTextStreamType.XmlText);
            
        }

        private void xrRichText2_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            if (row2 != null)
                xrRichText2.LoadFile(fun.fee_receipts(row2, true, "  Accounts Copy"), XRRichTextStreamType.XmlText);
        }

        private void xrRichText3_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            if (row3 != null)
                xrRichText3.LoadFile(fun.fee_receipts(row3, true, "  Student Copy"), XRRichTextStreamType.XmlText);
        }

        private void xrRichText4_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            if (row4 != null)
                xrRichText4.LoadFile(fun.fee_receipts(row4, true, "  Accounts Copy"), XRRichTextStreamType.XmlText);
            
        }

        
    }
}
