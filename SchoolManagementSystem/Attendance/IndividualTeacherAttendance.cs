using DevExpress.XtraGrid;
using DevExpress.XtraGrid.Columns;
using DevExpress.XtraGrid.Views.Grid;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

namespace SchoolManagementSystem.Attendance
{
    public partial class IndividualTeacherAttendance : DevExpress.XtraEditors.XtraUserControl
    {
        private static IndividualTeacherAttendance _instance;

        public static IndividualTeacherAttendance instance
        {
            get
            {
                if (_instance == null)
                    _instance = new IndividualTeacherAttendance();
                return _instance;
            }
        }

        ObservableCollection<AllClass> allClass = new ObservableCollection<AllClass>();

        CommonFunctions fun = new CommonFunctions();
        public IndividualTeacherAttendance()
        {
            InitializeComponent();
            loadfunctions();
        }

        public void loadfunctions()
        {
            fun.DateFormat(txtDateFrom);
            fun.DateFormat(txtDateTO);
            txtTeacher.Properties.DataSource = fun.GetAllTeacher_dt();
            txtTeacher.Properties.DisplayMember = "name";
            txtTeacher.Properties.ValueMember = "teacher_id";
            load_staff_type();
        }
        void load_staff_type()
        {
            string query = "select * from `staff_type`";
            DataTable staff_type_dt = fun.FetchDataTable(query);
            txtStaffType.Properties.Items.Clear();
            foreach (DataRow dr in staff_type_dt.Rows)
                txtStaffType.Properties.Items.Add(dr["type_name"]);
        }
        private void FillGrodDailyAttendance()
        {

            // DateTime date = txtDateTO.DateTime;
            var tempdate = txtDateTO.DateTime.Month + "-" + txtDateTO.DateTime.Day + "-" + txtDateTO.DateTime.Year;
            DateTime date = Convert.ToDateTime(tempdate);

            var query = "";
            var dateHolidays = fun.GetAllHolidaysBetween(date, txtDateFrom.DateTime);

            MySqlConnection con = new MySqlConnection(Login.constring);
            con.Open();
            query += "SELECT  attendance.date as Date,attendance.time_in as TimeIn,attendance.time_out  as TimeOut,if(attendance.status=1,'P','') as Present,if(attendance.time_in!='','','A') as Absent,truncate(if(TIME_TO_SEC(TIMEDIFF(attendance.time_in,attendance.setting_in))/60<=0,0,TIME_TO_SEC(TIMEDIFF(attendance.time_in,attendance.setting_in))/60),0) as `L.Time` ,'' as Remarks from teacher  left join attendance on attendance.student_id=teacher.teacher_id  "
                           + " AND dayofmonth(attendance.date) = '" + date.Day + "' and month(attendance.date) = '" + date.Month + "' and year(attendance.date) = '" + date.Year + "' where teacher_id = '" + txtTeacher.EditValue.ToString() + "'";
            date = date.AddDays(1);
            for (; date < txtDateFrom.DateTime; date = date.AddDays(1))
            {

                var day = date.DayOfWeek;
                if (day.ToString() == "Sunday")
                {
                    query += "union all SELECT  attendance.date as Date,'' as TimeIn,''  as TimeOut,'' as Present,'' as Absent,'' as `L.Time` ,'Sunday' as Remarks  from teacher  left join attendance on attendance.student_id=teacher.teacher_id  "
                                       + " AND dayofmonth(attendance.date) = '" + date.Day + "' and month(attendance.date) = '" + date.Month + "' and year(attendance.date) = '" + date.Year + "' where teacher_id = '" + txtTeacher.EditValue.ToString() + "'";
                }
                else if (dateHolidays.Contains(date))
                {
                    query += "union all SELECT  attendance.date as Date,'' as TimeIn,''  as TimeOut,'' as Present,'' as Absent,'' as `L.Time` ,'Public Holidays' as Remarks  from teacher  left join attendance on attendance.student_id=teacher.teacher_id  "
                                     + " AND dayofmonth(attendance.date) = '" + date.Day + "' and month(attendance.date) = '" + date.Month + "' and year(attendance.date) = '" + date.Year + "' where teacher_id = '" + txtTeacher.EditValue.ToString() + "'";

                }
                else
                {
                    query += "union all SELECT attendance.date as Date,attendance.time_in as TimeIn,attendance.time_out  as TimeOut,if(attendance.status=1,'P','') as Present,if(attendance.time_in!='','','A') as Absent,truncate(if(TIME_TO_SEC(TIMEDIFF(attendance.time_in,attendance.setting_in))/60<=0,0,TIME_TO_SEC(TIMEDIFF(attendance.time_in,attendance.setting_in))/60),0) as `L.Time` ,'' as Remarks from teacher  left join attendance on attendance.student_id=teacher.teacher_id  "
                                   + " AND dayofmonth(attendance.date) = '" + date.Day + "' and month(attendance.date) = '" + date.Month + "' and year(attendance.date) = '" + date.Year + "' where teacher_id = '" + txtTeacher.EditValue.ToString() + "'";
                }
            }


            MySqlCommand cmdP = new MySqlCommand(query, con);
            MySqlDataAdapter adp = new MySqlDataAdapter(cmdP);
            DataTable table = new DataTable();
            adp.Fill(table);
            gridIndividualTeacherAttendance.DataSource = null;
            gridView1.Columns.Clear();
            gridIndividualTeacherAttendance.DataSource = CommonFunctions.AutoNumberedTable(table);
            gridView1.BestFitColumns();
            con.Close();
            var col4 = gridView1.Columns["Sr#"];
            col4.Width = 40;
            GridColumn Column = gridView1.Columns["Date"];
            Column.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            GridColumn Column3 = gridView1.Columns["Sr#"];
            Column3.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            GridColumn Column4 = gridView1.Columns["TimeIn"];
            Column4.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            GridColumn Column5 = gridView1.Columns["TimeOut"];
            Column5.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            GridColumn Column6 = gridView1.Columns["L.Time"];
            Column6.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            GridColumn Column7 = gridView1.Columns["Present"];
            Column7.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            GridColumn Column8 = gridView1.Columns["Absent"];
            Column8.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;

            GridColumnSummaryItem item1 = new GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Custom, "Present", "{0}", tag: "Present");
            GridColumnSummaryItem item2 = new GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Custom, "Absent", "{0}", tag: "Absent");
            GridColumnSummaryItem item3 = new GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Sum, "L.Time", "{0}");
            gridView1.Columns["Present"].Summary.Add(item1);
            gridView1.Columns["Absent"].Summary.Add(item2);
            gridView1.Columns["L.Time"].Summary.Add(item3);
        }

        private void btnfind_Click(object sender, EventArgs e)
        {
            if (txtSummery.Checked == false)
            {
                if (txtTeacher.EditValue == null)
                {
                    MessageBox.Show("Must select teacher Name for individual teacher attendance....!!", "Info");
                    return;
                }
                FillGrodDailyAttendance(); showReport();
            }
            else
            {
                var queryType = "where 1 = 1 ";
                if (!string.IsNullOrEmpty(txtStaffType.Text))
                    queryType += " and staff_type='" + txtStaffType.Text + "'";

                DateTime dateS = Convert.ToDateTime(txtDateTO.Text);
                DateTime dateE = Convert.ToDateTime(txtDateFrom.Text);
                var s = int.Parse((dateE - dateS).TotalDays.ToString());
                var query = "";
                int sun = fun.CountSundaysBetween(dateS, dateE);
                var res = fun.GetAllHolidaysBetween(dateS, dateE);
                String holiday_str = fun.GetExludeDates(dateS, dateE);
                if (holiday_str != "")
                {
                    queryType += " AND attendance.date NOT IN("+ holiday_str + ")";
                }
                s = s + 1;
                s = s - sun - res.Count;
                MySqlConnection con = new MySqlConnection(Login.constring);
                con.Open();
                query = "SELECT teacher.teacher_id as Teacher_ID,teacher.name as Name, min( attendance.date) as `MinDate`,max( attendance.date)  as MaxDate," +
                        "sum(if (attendance.status = 1,1,0)) as Present," + s + "- sum(if(attendance.status = 1,1,0)) as Absent," +
                        "truncate(sum(if (TIME_TO_SEC(TIMEDIFF(attendance.time_in, attendance.setting_in)) / 60 <= 0,0,TIME_TO_SEC(TIMEDIFF(attendance.time_in, attendance.setting_in)) / 60)),0) as `L.Time` ," +
                        "'' as Remarks from teacher left join attendance on attendance.student_id = teacher.teacher_id " +
                        "AND attendance.date >= '" + dateS.ToString("yyyy-MM-dd") + "' AND attendance.date <= '" + dateE.ToString("yyyy-MM-dd") + "' " + queryType + " group by teacher.teacher_id";

                MySqlCommand cmdP = new MySqlCommand(query, con);
                MySqlDataAdapter adp = new MySqlDataAdapter(cmdP);
                DataTable table = new DataTable();
                adp.Fill(table);
                gridIndividualTeacherAttendance.DataSource = null;
                gridView1.Columns.Clear();
                gridIndividualTeacherAttendance.DataSource = CommonFunctions.AutoNumberedTable(table);
                gridView1.BestFitColumns();
                con.Close();
                var col4 = gridView1.Columns["Sr#"];
                col4.Width = 40;
                GridColumn Column = gridView1.Columns["MinDate"];
                Column.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
                GridColumn Column2 = gridView1.Columns["MaxDate"];
                Column2.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
                GridColumn Column3 = gridView1.Columns["Sr#"];
                Column3.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
                GridColumn Column4 = gridView1.Columns["Teacher_ID"];
                Column4.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
                GridColumn Column6 = gridView1.Columns["L.Time"];
                Column6.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
                GridColumn Column7 = gridView1.Columns["Present"];
                Column7.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
                GridColumn Column8 = gridView1.Columns["Absent"];
                Column8.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;

                showReportSumery();

            }
        }
        void showReportSumery()
        {
            XtraStaffAttendance report = new XtraStaffAttendance();
            Image logo = fun.Base64ToImage(Login.Logo);
            var school = fun.GetSettings("system_title");
            report.PicIogoBox.Image = logo;
            report.LabTitle.Text = school;
            report.LabAddress.Text = fun.GetSettings("address");
            report.labDate.Text = txtDateTO.Text + " to " + txtDateFrom.Text;
            report.GridControl = gridIndividualTeacherAttendance;
            report.xrPanel1.Visible = false;
            documentViewer1.DocumentSource = report;

            report.CreateDocument();
            documentViewer1.PrintingSystem.Document.AutoFitToPagesWidth = 1;

            //    ReportPrintTool printTool = new ReportPrintTool(report);
            //    printTool.PreviewForm.PrintingSystem.Document.AutoFitToPagesWidth = 1;
            //    printTool.ShowRibbonPreviewDialog(UserLookAndFeel.Default);
        }

        void showReport()
        {
            if(txtTeacher.EditValue == null)
            {
                MessageBox.Show("Please Select Teacher and try again", "Select teacher", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            DataTable teacher_info = fun.GetTeacher_details(Convert.ToInt32(txtTeacher.EditValue.ToString()));

            XtraStaffAttendance report = new XtraStaffAttendance();
            Image logo = fun.Base64ToImage(Login.Logo);
            var school = fun.GetSettings("system_title");
            report.PicIogoBox.Image = logo;
            report.LabTitle.Text = school;
            report.LabAddress.Text = fun.GetSettings("address");
            report.labDate.Text = txtDateTO.Text + " to " + txtDateFrom.Text;
            report.labName.Text = txtTeacher.Text;
            report.LabNumber.Text = teacher_info.Rows[0]["phone"].ToString();
            report.LabSAddress.Text = teacher_info.Rows[0]["address"].ToString();
            report.LabDes.Text = teacher_info.Rows[0]["designation"].ToString();
            report.GridControl = gridIndividualTeacherAttendance;
            documentViewer1.DocumentSource = report;
            report.xrPanel1.Visible = true;
            report.CreateDocument();
            documentViewer1.PrintingSystem.Document.AutoFitToPagesWidth = 1;

            //    ReportPrintTool printTool = new ReportPrintTool(report);
            //    printTool.PreviewForm.PrintingSystem.Document.AutoFitToPagesWidth = 1;
            //    printTool.ShowRibbonPreviewDialog(UserLookAndFeel.Default);
        }

        int count = 0;
        private void gridView1_CustomSummaryCalculate(object sender, DevExpress.Data.CustomSummaryEventArgs e)
        {
            GridColumnSummaryItem item = e.Item as GridColumnSummaryItem;
            GridView view = sender as GridView;
            if (Equals("Present", item.Tag))
            {
                if (e.SummaryProcess == DevExpress.Data.CustomSummaryProcess.Start)
                {
                    count = 0;

                }
                if (e.SummaryProcess == DevExpress.Data.CustomSummaryProcess.Calculate)
                {
                    var res = view.GetRowCellValue(e.RowHandle, "Present").ToString();
                    if (res != "")
                        count++;//= Convert.ToInt32(view.GetRowCellValue(e.RowHandle, "Current"));
                }
                if (e.SummaryProcess == DevExpress.Data.CustomSummaryProcess.Finalize)
                    e.TotalValue = count;
            }
            if (Equals("Absent", item.Tag))
            {
                if (e.SummaryProcess == DevExpress.Data.CustomSummaryProcess.Start)
                {
                    count = 0;
                }
                if (e.SummaryProcess == DevExpress.Data.CustomSummaryProcess.Calculate)
                {
                    var res = view.GetRowCellValue(e.RowHandle, "Absent").ToString();
                    if (res != "")
                        count++;//+= Convert.ToInt32(view.GetRowCellValue(e.RowHandle, "Monthly"));
                    // if (!Convert.ToBoolean(view.GetRowCellValue(e.RowHandle, "Due"))) validRowCount++;
                }
                if (e.SummaryProcess == DevExpress.Data.CustomSummaryProcess.Finalize)
                    e.TotalValue = count;
            }
        }
    }
}
