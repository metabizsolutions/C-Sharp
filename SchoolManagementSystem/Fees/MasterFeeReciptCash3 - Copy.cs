using DevExpress.Snap;
using DevExpress.XtraReports.UI;
using System.Data;
using System.IO;
using System.Windows.Forms;

namespace SchoolManagementSystem.Fees
{
    public partial class MasterFeeReciptCash3 : DevExpress.XtraReports.UI.XtraReport
    {
        DataRow row1, row2, row3;
        public MasterFeeReciptCash3(DataRow row1, DataRow row2, DataRow row3)
        {
            InitializeComponent();
            this.row1 = row1;
            this.row2 = row2;
            this.row3 = row3;
        }
        IncomeManagement inCome = new IncomeManagement();
        CommonFunctions fun = new CommonFunctions();
        private void xrSubreport1_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            /*if (row3 != null)
            {
                FeeRecipt_Cashold recipt = new FeeRecipt_Cashold();
                recipt = inCome.showReportCash(row3, true);
                ((XRSubreport)sender).ReportSource = recipt;
            }
            else
            {
                ((XRSubreport)sender).ReportSource.Visible = false;
            }*/
            if (row1 != null)
            {
                //FeeRecipt_Cashold recipt = new FeeRecipt_Cashold();
                //recipt = inCome.showReportCash(row1, true);
                string path = Path.Combine(Path.GetDirectoryName(Application.ExecutablePath), "Resources\\fee_receipt_row1.xml");
                using (FileStream fs = new FileStream(path, FileMode.Create))
                {
                    fun.showReportCash(row1,true).ExportDocument(fs, DevExpress.XtraRichEdit.DocumentFormat.WordML);
                    xrSubreport1.ReportSourceUrl = fs.Name;
                }

            }
            else
            {
                ((XRSubreport)sender).ReportSource.Visible = false;
            }
        }

        private void xrSubreport2_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            if (row2 != null)
            {
                //FeeRecipt_Cashold recipt = new FeeRecipt_Cashold();
                //recipt = inCome.showReportCash(row2, true);
                string path = Path.Combine(Path.GetDirectoryName(Application.ExecutablePath), "Resources\\fee_receipt_row2.xml");
                using (FileStream fs = new FileStream(path, FileMode.Create))
                {
                    fun.customerbill_sc(row2).ExportDocument(fs, DevExpress.XtraRichEdit.DocumentFormat.WordML);
                    xrSubreport2.ReportSourceUrl = fs.Name;
                }
                
            }
            else
            {
                ((XRSubreport)sender).ReportSource.Visible = false;
            }

        }

        private void xrSubreport3_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            if (row3 != null)
            {
                //FeeRecipt_Cashold recipt = new FeeRecipt_Cashold();
                //recipt = inCome.showReportCash(row3, true);
                string path = Path.Combine(Path.GetDirectoryName(Application.ExecutablePath), "Resources\\fee_receipt_row3.xml");
                using (FileStream fs = new FileStream(path, FileMode.Create))
                {
                    fun.customerbill_sc(row3).ExportDocument(fs, DevExpress.XtraRichEdit.DocumentFormat.WordML);
                    xrSubreport3.ReportSourceUrl = fs.Name;
                }

            }
            else
            {
                ((XRSubreport)sender).ReportSource.Visible = false;
            }
        }
    }
}
