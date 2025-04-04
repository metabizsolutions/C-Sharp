using DevExpress.XtraCharts;
using DevExpress.XtraGrid;
using DevExpress.XtraReports.UI;
using System.Data;
using System.Drawing;

namespace SchoolManagementSystem.Exam
{
    public partial class XtraStdReport : DevExpress.XtraReports.UI.XtraReport
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


        private GridControl Atndcontrol;
        public GridControl AtndGridControl
        {
            get
            {
                return Atndcontrol;
            }
            set
            {
                Atndcontrol = value;
                PanelAttendance.PrintableComponent = Atndcontrol;
            }
        }

        private GridControl Feecontrol;
        public GridControl FeeGridControl
        {
            get
            {
                return Feecontrol;
            }
            set
            {
                Feecontrol = value;
                printableFeeDetails.PrintableComponent = Feecontrol;
            }
        }

        private ChartControl piecontrol;
        public ChartControl PieGridControl
        {
            get
            {
                return piecontrol;
            }
            set
            {
                piecontrol = value;
                PanelAttendancePie.PrintableComponent = piecontrol;
            }
        }



        public XtraStdReport()
        {
            InitializeComponent();
            
        }

        private void XtraStdReport_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            if(!FieldLabel1.Visible || !FieldValue1.Visible || !FieldLabel2.Visible || !FieldValue2.Visible || !FieldLabel3.Visible || !FieldValue3.Visible)
            {
                xrPanel1.HeightF = 192.29F; 
                //xrlblExams.LocationF= new PointF(xrlblExams.LocationF.X, xrlblExams.LocationF.Y - 52.8F);
                //printableComponentContainer1.LocationF = new PointF(printableComponentContainer1.LocationF.X, printableComponentContainer1.LocationF.Y - 83.8F);
                //xrChart1.LocationF = new PointF(xrChart1.LocationF.X, xrChart1.LocationF.Y - 79.8F);
                //xrlblAttendence.LocationF = new PointF(xrlblAttendence.LocationF.X, xrlblAttendence.LocationF.Y - 79.8F);
                //PanelAttendance.LocationF = new PointF(PanelAttendance.LocationF.X, PanelAttendance.LocationF.Y - 79.8F);
                //PanelAttendancePie.LocationF = new PointF(PanelAttendancePie.LocationF.X, PanelAttendancePie.LocationF.Y - 79.8F);
                //xrlblFee.LocationF = new PointF(xrlblFee.LocationF.X, xrlblFee.LocationF.Y - 79.8F);
                //printableFeeDetails.LocationF = new PointF(printableFeeDetails.LocationF.X, printableFeeDetails.LocationF.Y - 79.8F);
            }
            else
                xrPanel1.HeightF = 244.38F;
        }
    }
}
