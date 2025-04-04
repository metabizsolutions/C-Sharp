using DevExpress.XtraGrid;

namespace SchoolManagementSystem.Attendance
{
    public partial class XtraStdAttendanceReport : DevExpress.XtraReports.UI.XtraReport
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
        public XtraStdAttendanceReport()
        {
            InitializeComponent();
        }

    }
}
