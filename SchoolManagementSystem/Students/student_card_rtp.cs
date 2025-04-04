using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using System.Data;

namespace SchoolManagementSystem.Students
{
    public partial class student_card_rpt : DevExpress.XtraReports.UI.XtraReport
    {
        public student_card_rpt()
        {
            InitializeComponent();
        }
        DataRow row;
        public student_card_rpt(DataRow dr)
        {
            InitializeComponent();
            this.row = dr;
        }
        CommonFunctions fun = new CommonFunctions();
        private void xrRichText1_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            if (row != null)
                xrRichText1.LoadFile(fun.fee_receipts(row, true), XRRichTextStreamType.XmlText);
        }
    }
}
