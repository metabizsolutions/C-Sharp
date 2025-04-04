using DevExpress.XtraGrid;
using System.Collections.ObjectModel;

namespace SchoolManagementSystem.Exam
{
    public partial class XtraCheckListReport : DevExpress.XtraReports.UI.XtraReport
    {
        private GridControl control;
        public GridControl GridControl
        {
            get
            {
                return control;
            }
            set
            {
                control = value;
                printableComponentContainer1.PrintableComponent = control;
            }
        }
        CheckList a = new CheckList();
        public XtraCheckListReport()
        {
            InitializeComponent();
            }

        private void XtraCheckListReport_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
      


        }
    }
}
