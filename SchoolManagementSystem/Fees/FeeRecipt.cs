using System.Collections.ObjectModel;

namespace SchoolManagementSystem.Fees
{
    public partial class FeeRecipt : DevExpress.XtraReports.UI.XtraReport
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


        public FeeRecipt()
        {
            InitializeComponent();
        }

    }
}
