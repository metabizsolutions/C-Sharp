using System.Collections.ObjectModel;

namespace SchoolManagementSystem.Fees
{
    public partial class PaySlipNew : DevExpress.XtraReports.UI.XtraReport
    {

        private ObservableCollection<PreviousFee> control;
        public ObservableCollection<PreviousFee> GridControl
        {
            get
            {
                return control;
            }
            set
            {
                control = value;
                objectDataSource1.DataSource = control;
            }
        }
        public PaySlipNew()
        {
            InitializeComponent();
        }

    }
}
