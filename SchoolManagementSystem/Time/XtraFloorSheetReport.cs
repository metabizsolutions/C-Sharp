using System.Collections.ObjectModel;

namespace SchoolManagementSystem.Time
{
    public partial class XtraFloorSheetReport : DevExpress.XtraReports.UI.XtraReport
    {
        private ObservableCollection<FloorSheetItems> control;
        public ObservableCollection<FloorSheetItems> GridControl
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
        public XtraFloorSheetReport()
        {
            InitializeComponent();
        }

    }
}
