using DevExpress.XtraGrid;

namespace SchoolManagementSystem.Students
{
    public partial class XtraStudentAdmissionReport : DevExpress.XtraReports.UI.XtraReport
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
        public XtraStudentAdmissionReport()
        {
            InitializeComponent();
        }

    }
}
