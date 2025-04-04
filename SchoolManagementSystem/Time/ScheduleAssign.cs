using DevExpress.LookAndFeel;
using DevExpress.XtraEditors.Repository;
using DevExpress.XtraGrid.Columns;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraGrid.Views.Grid.ViewInfo;
using DevExpress.XtraReports.UI;
using SchoolManagementSystem.Time;
using System;
using System.Data;
using System.Drawing;

namespace SchoolManagementSystem.Class
{
    public partial class ScheduleAssign : DevExpress.XtraEditors.XtraUserControl
    {
        private static ScheduleAssign _instance;

        public static ScheduleAssign instance
        {
            get
            {
                if (_instance == null)
                    _instance = new ScheduleAssign();
                return _instance;
            }
        }
        CommonFunctions fun = new CommonFunctions();
       
        public ScheduleAssign()
        {
            InitializeComponent();
            loadfunctions();
        }
        public void loadfunctions()
        {
            string currentDate = DateTime.Today.ToString("MM/dd/yyyy");
            txtDateFrom.Text = currentDate;
            txtDateTo.Text = currentDate;
            FillAssignTable();
        }
        void FillAssignTable() {
            string DateFrom = txtDateFrom.Value.ToString("yyyy-MM-dd");
            string DateTo = txtDateTo.Value.ToString("yyyy-MM-dd");
            string sql = "SELECT  class.name AS class, section.name AS section,el.slate AS slot,el.date,teacher.name AS teacherFrom, teacher1.name AS teacherTo "+
                        " FROM extra_lecture AS el "+
                        " INNER JOIN class ON class.class_id = el.class_id "+
                        " INNER JOIN section ON section.section_id = el.section_id "+
                        " INNER JOIN teacher ON teacher.teacher_id = el.teacher_id "+
                        " INNER JOIN teacher AS teacher1 ON teacher1.teacher_id = el.extra_teacher_id "+
                        " WHERE el.date BETWEEN '{0}' AND '{1}'";
            sql = String.Format(sql, DateFrom, DateTo);
            DataTable table = fun.GetQueryTable(sql);
            GridScheduleAssign.DataSource = null;
            GridViewScheduleAssign.Columns.Clear();
            GridScheduleAssign.DataSource = table;
        }


        private void btnSearch_Click(object sender, EventArgs e)
        {
            FillAssignTable();
        }

        private void btnReportPrint_Click(object sender, EventArgs e)
        {
            GridViewScheduleAssign.OptionsView.AllowHtmlDrawHeaders = true;
            GridViewScheduleAssign.Appearance.HeaderPanel.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            GridViewScheduleAssign.AppearancePrint.HeaderPanel.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            GridViewScheduleAssign.ColumnPanelRowHeight = 30;

         

            Image logo = fun.Base64ToImage(Login.Logo);
            var school = fun.GetSettings("system_title");
            TimeTableReport report = new TimeTableReport();
            report.PicIogoBox.Image = logo;
            report.LabTitle.Text = school;
            report.GridControl = GridScheduleAssign;
            report.GridControl.Width = 100;
            report.LabAddress.Text = fun.GetSettings("address");
            report.LabTel.Text = fun.GetSettings("phone");
            ReportPrintTool printTool = new ReportPrintTool(report);
            printTool.PreviewForm.PrintingSystem.Document.AutoFitToPagesWidth = 1;
            printTool.ShowRibbonPreviewDialog(UserLookAndFeel.Default);
        }
    }

}