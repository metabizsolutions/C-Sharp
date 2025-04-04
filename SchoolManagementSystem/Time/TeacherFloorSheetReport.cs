using DevExpress.XtraGrid;

namespace SchoolManagementSystem.Time
{
    public partial class TeacherFloorSheetReport : DevExpress.XtraReports.UI.XtraReport
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
        public TeacherFloorSheetReport()
        {
            InitializeComponent();
        }

    }
}
