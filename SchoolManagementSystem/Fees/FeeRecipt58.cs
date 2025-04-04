using System.Collections.ObjectModel;

namespace SchoolManagementSystem.Fees
{
    public partial class FeeRecipt58 : DevExpress.XtraReports.UI.XtraReport
    {
        private ObservableCollection<PreviousFee> control;
        public bool DetailFlag = false;
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


        public FeeRecipt58()
        {
            InitializeComponent();
        }
        

        private void GroupDetail_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            e.Cancel = this.DetailFlag;
        }

        private void GroupDetailList_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            e.Cancel = this.DetailFlag;
        }
    }
}
