using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using DevExpress.XtraGrid;

namespace SchoolManagementSystem.Attendance
{
    public partial class XtraReport_PEF : DevExpress.XtraReports.UI.XtraReport
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
        public XtraReport_PEF()
        {
            InitializeComponent();
        }

    }
}
