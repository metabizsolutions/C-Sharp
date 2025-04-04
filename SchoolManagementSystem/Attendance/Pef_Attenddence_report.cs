using System;
using System.Windows.Forms;
using DevExpress.XtraGrid.Columns;
using DevExpress.XtraGrid;
using DevExpress.XtraReports.UI;
using DevExpress.LookAndFeel;
using System.Data;
using DevExpress.XtraGrid.Views.Grid;

namespace SchoolManagementSystem.Attendance
{
    public partial class Pef_Attenddence_report : UserControl
    {
        private static Pef_Attenddence_report _instance;

        public static Pef_Attenddence_report instance
        {
            get
            {
                if (_instance == null)
                    _instance = new Pef_Attenddence_report();
                return _instance;
            }
        }
        CommonFunctions fun = new CommonFunctions();
        public Pef_Attenddence_report()
        {
            InitializeComponent();
        }

        private void Btn_Find_Click(object sender, EventArgs e)
        {
            string query = "SELECT class_id,Class,Section,Boys,Girls,P_Boys,P_Girls,L_Boys,L_Girls,(tb.Boys-tb.P_Boys-tb.L_Boys) AS A_Boys, " +
                " (tb.Girls - tb.P_Girls - tb.L_Girls) AS A_Girls,(tb.P_Boys + tb.P_Girls) AS Present,(tb.L_Boys + tb.L_Girls) AS `Leave`,(tb.Total - (tb.P_Boys + tb.P_Girls) - (tb.L_Boys + tb.L_Girls)) AS Absent," +
                " FORMAT(((tb.P_Boys + tb.P_Girls) / tb.total * 100), 2) AS `%`,Total,description FROM(" +
                " SELECT cls.class_id, cls.name AS Class, stu.`section_id`, sec.name AS Section," +
                " COUNT(t_boys.`student_id`) AS Boys, COUNT(t_girls.`student_id`) AS Girls, COUNT(att_p_boys.`status`) AS P_Boys," +
                " COUNT(att_p_girls.`status`) AS P_Girls, COUNT(att_l_boys.`status`) AS L_Boys, COUNT(att_l_girls.`status`) AS L_Girls,COUNT(t_boys.`student_id`) + COUNT(t_girls.`student_id`) AS Total, hol.`title` AS description" +
                " FROM student AS stu " +
                " JOIN section AS sec ON sec.section_id = stu.section_id " +
                " JOIN class AS cls ON cls.class_id =stu.class_id " +
                " LEFT JOIN attendance AS att ON att.student_id = stu.student_id AND att.`date` = '" + DTP_Date.Value.ToString("yyyy-MM-dd") + "'   " +
                " LEFT JOIN student AS t_boys ON t_boys.`student_id` = stu.`student_id` AND t_boys.sex = 'Male' " +
                " LEFT JOIN student AS t_girls ON t_girls.`student_id` = stu.`student_id` AND t_girls.sex = 'Female' " +
                " LEFT JOIN attendance AS att_p_boys ON att_p_boys.`student_id` = t_boys.`student_id` AND att_p_boys.`date` = att.`date`  AND att_p_boys.`status` = '1'   " +
                " LEFT JOIN attendance AS att_p_girls ON att_p_girls.`student_id` = t_girls.`student_id` AND att_p_girls.`date` = att.`date`  AND att_p_girls.`status` = '1' " +
                " LEFT JOIN attendance AS att_l_boys ON att_l_boys.`student_id` = t_boys.`student_id` AND att_l_boys.`date` = att.`date`  AND att_l_boys.`status` = '2'  " +
                " LEFT JOIN attendance AS att_l_girls ON att_l_girls.`student_id` = t_girls.`student_id` AND att_l_girls.`date` = att.`date`  AND att_l_girls.`status` = '2'  " +
                " LEFT JOIN `tbl_holidays` AS hol ON hol.`section_id` = sec.`section_id`  AND '" + DTP_Date.Value.ToString("yyyy-MM-dd") + "' BETWEEN hol.`start_date` AND hol.`end_date`   " +
                " WHERE stu.passout != 1  GROUP BY stu.section_id) AS tb"; 
            
            DataTable att_dt = fun.FetchDataTable(query);
            gridControl1.DataSource = att_dt;
            
            foreach (GridColumn col in gridView1.Columns)
            {
                string c = col.FieldName;
                if (c != "Class" && c != "Section" && c!= "%")
                {
                    GridGroupSummaryItem item4 = new GridGroupSummaryItem();
                    item4.FieldName = c;
                    item4.SummaryType = DevExpress.Data.SummaryItemType.Sum;
                    item4.DisplayFormat = "{0:0.##}";
                    item4.ShowInGroupColumnFooter = gridView1.Columns[c];
                    gridView1.GroupSummary.Add(item4);

                    GridColumnSummaryItem item = new GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Sum, c, "{0}");
                    gridView1.Columns[c].Summary.Clear();
                    gridView1.Columns[c].Summary.Add(item);
                }
            }

            GridGroupSummaryItem item5 = new GridGroupSummaryItem();
            item5.FieldName = "%";
            item5.SummaryType = DevExpress.Data.SummaryItemType.Custom;
            item5.DisplayFormat = "{0:0.##}";
            item5.Tag = 2;
            item5.ShowInGroupColumnFooter = gridView1.Columns["%"];
            gridView1.GroupSummary.Add(item5);

            GridColumnSummaryItem item1 = new GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Custom, "%", "{0}");
            item1.Tag = 1;
            gridView1.Columns["%"].Summary.Clear();
            gridView1.Columns["%"].Summary.Add(item1);

            gridView1.Columns["Class"].FieldNameSortGroup = "class_id";
            gridView1.Columns["Class"].Group();
            gridView1.Columns["class_id"].Visible = false;
            gridView1.ExpandAllGroups();
        }

        private void btn_Print_Click(object sender, EventArgs e)
        {
            XtraReport_PEF pef = new XtraReport_PEF();
            pef.xrlblTitle.Text = "DAILY ATTENDANCE (ROZNAMCHA) "+ DTP_Date.Value.Year;
            pef.xrlblSchool.Text = fun.GetSettings("system_title"); ;
            pef.xrlblSchoolCode.Text = "";
            pef.xrlbldate.Text = "("+DTP_Date.Value.ToString("yyyy-MM-dd")+")";
            pef.xrlblPrinciple.Text = "";

            pef.GridControl = gridControl1;
            ReportPrintTool printTool = new ReportPrintTool(pef);
            printTool.PreviewForm.PrintingSystem.Document.AutoFitToPagesWidth = 1;
            printTool.ShowRibbonPreviewDialog(UserLookAndFeel.Default);
        }
        decimal totalstudents = 0;
        decimal total_present = 0;
        decimal grouptotalstudents = 0;
        decimal group_Present = 0;
        string tp = "";
        string gp = "";
        private void gridView1_CustomSummaryCalculate(object sender, DevExpress.Data.CustomSummaryEventArgs e)
        {
            GridView view = sender as GridView;
            int summeryid = Convert.ToInt32((e.Item as GridSummaryItem).Tag);

            if(e.SummaryProcess == DevExpress.Data.CustomSummaryProcess.Start)
            {
                grouptotalstudents = 0;
                group_Present = 0;
                totalstudents = 0;
                total_present = 0;
            }
            if (e.SummaryProcess == DevExpress.Data.CustomSummaryProcess.Calculate)
            {
                switch (summeryid) {
                    case 1:
                        totalstudents += Convert.ToInt32(view.GetRowCellDisplayText(e.RowHandle, "Total"));
                        total_present += Convert.ToInt32(view.GetRowCellDisplayText(e.RowHandle, "Present"));
                        tp = ((total_present / totalstudents) * 100).ToString("#0.00");
                        break;
                    case 2:
                        grouptotalstudents += Convert.ToInt32(view.GetRowCellDisplayText(e.RowHandle, "Total"));
                        group_Present += Convert.ToInt32(view.GetRowCellDisplayText(e.RowHandle, "Present"));
                        gp = ((group_Present / grouptotalstudents) * 100).ToString("#0.00");
                        break;
                }
            }
            if (e.SummaryProcess == DevExpress.Data.CustomSummaryProcess.Finalize)
            {
                switch (summeryid)
                {
                    case 1:
                        e.TotalValue = tp;
                        break;
                    case 2:
                        e.TotalValue = gp;
                        break;
                }
            }
        }
    }
}
