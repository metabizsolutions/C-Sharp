using DevExpress.XtraGrid;

namespace SchoolManagementSystem.Exam
{
    public partial class XtraStdMarksSheet : DevExpress.XtraReports.UI.XtraReport
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

        public XtraStdMarksSheet()
        {
            InitializeComponent();
        }

    }
}
