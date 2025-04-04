using DevExpress.XtraReports.UI;
using System.Data;

namespace SchoolManagementSystem.Fees
{
    public partial class FeeRecipt_single: DevExpress.XtraReports.UI.XtraReport
    {
        DataRow row1;
        public FeeRecipt_single(DataRow row1)
        {
            InitializeComponent();
            this.row1 = row1;
        }
        CommonFunctions fun = new CommonFunctions();
        public FeeRecipt_single()
        {
            InitializeComponent();
        }
        private void xrRichText1_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            if (row1 != null)
                xrRichText5.LoadFile(fun.fee_receipts(row1, true, "  Student Copy"), XRRichTextStreamType.XmlText);
            
        }
    }
}
