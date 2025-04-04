using DevExpress.XtraGrid;

namespace SchoolManagementSystem.Fees
{
    public partial class SalaryStatmentPermanentTeacher : DevExpress.XtraReports.UI.XtraReport
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


        public SalaryStatmentPermanentTeacher()
        {
            InitializeComponent();
        }

    }
}
