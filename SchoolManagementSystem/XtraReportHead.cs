using DevExpress.XtraGrid;

namespace SchoolManagementSystem
{
    public partial class XtraReportHead : DevExpress.XtraReports.UI.XtraReport
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
        public XtraReportHead()
        {
            InitializeComponent();
        }

    }
}
