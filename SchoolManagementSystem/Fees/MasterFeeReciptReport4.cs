using DevExpress.XtraReports.UI;
using System.Data;

namespace SchoolManagementSystem.Fees
{
    public partial class MasterFeeReciptReport4 : DevExpress.XtraReports.UI.XtraReport
    {
        DataRow row;
        public MasterFeeReciptReport4(DataRow row)
        {
            InitializeComponent();

            this.row = row;
        }
        public MasterFeeReciptReport4()
        {
            InitializeComponent();
        }
        CommonFunctions fun = new CommonFunctions();

        private void xrRichText1_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            if (row != null)
                xrRichText1.LoadFile(fun.fee_receipts(row, true, "  Student Copy"), XRRichTextStreamType.XmlText);
        }

        private void xrRichText2_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            if (row != null)
                xrRichText2.LoadFile(fun.fee_receipts(row, true, "  Admin Copy"), XRRichTextStreamType.XmlText);
        }

        private void xrRichText3_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            if (row != null)
                xrRichText3.LoadFile(fun.fee_receipts(row, true, "  Management Copy"), XRRichTextStreamType.XmlText);
        }

        private void xrRichText4_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            if (row != null)
                xrRichText4.LoadFile(fun.fee_receipts(row, true, "  Bank Copy"), XRRichTextStreamType.XmlText);
        }
    }
}
