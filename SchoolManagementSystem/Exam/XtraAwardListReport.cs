using DevExpress.XtraGrid;

namespace SchoolManagementSystem.Exam
{
    public partial class XtraAwardListReport : DevExpress.XtraReports.UI.XtraReport
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
        public XtraAwardListReport()
        {
            InitializeComponent();
        }

    }
}
