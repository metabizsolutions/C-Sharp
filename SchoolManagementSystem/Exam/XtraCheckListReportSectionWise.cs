﻿using System.Collections.ObjectModel;

namespace SchoolManagementSystem.Exam
{
    public partial class XtraCheckListReportSectionWise : DevExpress.XtraReports.UI.XtraReport
    {
        private ObservableCollection<CheckListItems> control;
        public ObservableCollection<CheckListItems> GridControl
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
        public XtraCheckListReportSectionWise()
        {
            InitializeComponent();
        }

    }
}
