using DevExpress.XtraEditors;
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
    public partial class TeacherMAttendance : DevExpress.XtraEditors.XtraUserControl
    {
        private static TeacherMAttendance _instance;

        public static TeacherMAttendance instance
        {
            get
            {
                if (_instance == null)
                    _instance = new TeacherMAttendance();
                return _instance;
            }
        }
        ObservableCollection<AllClass> allClass = new ObservableCollection<AllClass>();
        CommonFunctions fun = new CommonFunctions();

        public TeacherMAttendance()
        {
            InitializeComponent();
            loadfunctions();
        }
        public void loadfunctions()
        {
            txtYear.Text = DateTime.Now.Year.ToString();
            txtMonth.Text = DateTime.Now.Month.ToString();
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
        private void FillGridTMAttendance()
        {
            MySqlConnection con = new MySqlConnection(Login.constring);
            con.Open();
            var tempdate = DateTime.Now.Month + "-" + 1 + "-" + DateTime.Now.Year;
            DateTime date = Convert.ToDateTime(tempdate);
            List<DateTime> dateHolidays = new List<DateTime>();
            dateHolidays = fun.GetAllHolidaysAMonth(date);
            int sun = 0;
            int total = 0;
            var query = "SELECT teacher.name as Name,teacher.phone as `Phone#`";
            int tDays = DateTime.DaysInMonth(Convert.ToInt32(txtYear.Text), Convert.ToInt32(txtMonth.Text));
            for (int i = 1; i <= tDays; i++)
            {
                var day = date.DayOfWeek;
                if (date <= DateTime.Now.Date)
                {
                    if (day.ToString() == "Sunday")
                        sun++;
                    total = i;
                }
                if (dateHolidays.Contains(date))
                {
                    if (day.ToString() == "Sunday")
                        query += " ,MAX(CASE dayofmonth(date) WHEN '" + i + "' THEN 'P/S' ELSE 'S/H' END) AS '" + i + "'";
                    else
                        query += " ,MAX(CASE  WHEN dayofmonth(date)='" + i + "' THEN 'P/H' ELSE 'H' END) AS '" + i + "'";
                }
                else if (day.ToString() == "Sunday")
                {
                    query += " ,MAX(CASE dayofmonth(date) WHEN '" + i + "' THEN 'P/S' ELSE 'S' END) AS '" + i + "'";
                }
                else
                    query += " ,MAX(CASE dayofmonth(date) WHEN '" + i + "' THEN 'P' ELSE 'A' END) AS '" + i + "'";
                date = date.AddDays(1);
            }
            total = total - sun;
            query += ",count(dayofmonth(date)) as 'Total_P'," + total + "-count(dayofmonth(date)) as 'Total_A' FROM teacher left join attendance on (attendance.student_id = teacher.teacher_id) and month(date) = '" + txtMonth.Text + "' and year(date)='" + txtYear.Text + "'  " +
                        " group by teacher.teacher_id";
            MySqlCommand cmd = new MySqlCommand(query, con);
            MySqlDataAdapter adp = new MySqlDataAdapter(cmd);
            DataTable table = new DataTable();
            adp.Fill(table);
            int absent = 0;
            for (int i = 0; i < table.Rows.Count; i++)
            {
                absent = 0;
                for (int k = 0; k < table.Columns.Count; k++)
                {
                    var s = table.Rows[i][k].ToString();
                    if (table.Rows[i][k].ToString() == "A")
                    {
                        absent++;
                    }
                }
                table.Rows[i]["Total_A"] = absent;
            }
            gridTMAttendance.DataSource = null;
            gridView1.Columns.Clear(); gridTMAttendance.DataSource = CommonFunctions.AutoNumberedTable(table);
            GridColumn Column = gridView1.Columns["Sr#"];
            Column.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            GridColumn Column3 = gridView1.Columns["Total_P"];
            Column3.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            GridColumn Column4 = gridView1.Columns["Total_A"];
            Column4.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;

            gridView1.BestFitColumns();
            con.Close();
            GridFormatRule gridFormatRuleP = new GridFormatRule();
            GridFormatRule gridFormatRuleA = new GridFormatRule();
            FormatConditionRuleDataBar formatConditionRuleDataBarP = new FormatConditionRuleDataBar();
            FormatConditionRuleDataBar formatConditionRuleDataBarA = new FormatConditionRuleDataBar();

            gridFormatRuleP.Column = gridView1.Columns["Total_P"];
            gridFormatRuleA.Column = gridView1.Columns["Total_A"];
            formatConditionRuleDataBarP.PredefinedName = "Green";
            formatConditionRuleDataBarP.MaximumType = FormatConditionValueType.Number;
            formatConditionRuleDataBarP.Maximum = total;
            formatConditionRuleDataBarP.MinimumType = FormatConditionValueType.Number;
            formatConditionRuleDataBarP.Minimum = 0;
            formatConditionRuleDataBarA.PredefinedName = "Coral";
            formatConditionRuleDataBarA.MaximumType = FormatConditionValueType.Number;
            formatConditionRuleDataBarA.Maximum = total;
            formatConditionRuleDataBarA.MinimumType = FormatConditionValueType.Number;
            formatConditionRuleDataBarA.Minimum = 0;

            gridFormatRuleP.Rule = formatConditionRuleDataBarP;
            gridFormatRuleA.Rule = formatConditionRuleDataBarA;
            gridView1.FormatRules.Add(gridFormatRuleP);
            gridView1.FormatRules.Add(gridFormatRuleA);
            showReport();

        }
        private void btnTMAFind_Click(object sender, EventArgs e)
        {
            if (txtMonth.Text == "" || txtYear.Text == "")
            {
                MessageBox.Show("Fill all fields....", "Info");
                return;
            }
            var queryType = "";
            if (!string.IsNullOrEmpty(txtStaffType.Text))
                queryType = " where staff_type='" + txtStaffType.Text + "' and passout = 0 ";
            MySqlConnection con = new MySqlConnection(Login.constring);
            var tempdate = txtMonth.Text + "/" + 1 + "/" + txtYear.Text;
            DateTime date = Convert.ToDateTime(tempdate);
            List<DateTime> dateHolidays = new List<DateTime>();
            dateHolidays = fun.GetAllHolidaysAMonth(date);

            con.Open();
            var str = "";
            int sun = 0;
            int total = 0;
            var query = "SELECT teacher.name as Name,teacher.phone as `Phone#`";
            int tDays = DateTime.DaysInMonth(Convert.ToInt32(txtYear.Text), Convert.ToInt32(txtMonth.Text));
            for (int i = 1; i <= tDays; i++)
            {
                str = "";
                var day = date.DayOfWeek;
                if (date <= DateTime.Now.Date)
                {
                    if (day.ToString() == "Sunday")
                        sun++;
                    total = i;
                    str = "ELSE 'A'";
                }
                if (dateHolidays.Contains(date))
                {
                    if (day.ToString() == "Sunday")
                        query += " ,MAX(CASE dayofmonth(date) WHEN '" + i + "' THEN 'P/S' ELSE 'S/H' END) AS '" + i + "'";
                    else
                        query += " ,MAX(CASE  WHEN dayofmonth(date)='" + i + "' THEN 'P/H' ELSE 'H' END) AS '" + i + "'";
                }
                else if (day.ToString() == "Sunday")
                {
                    query += " ,MAX(CASE dayofmonth(date) WHEN '" + i + "' THEN 'P/S' ELSE 'S' END) AS '" + i + "'";
                }
                else
                    query += " ,MAX(CASE WHEN dayofmonth(date)='" + i + "' and attendance.status='1' THEN 'P' WHEN dayofmonth(date)='" + i + "' and attendance.status='2' THEN 'L' " + str + " END) AS '" + i + "'";
                date = date.AddDays(1);
            }
            total = total - sun;
            query += ",''as 'T_P','' as 'T_A','' as 'T_L' FROM teacher left join attendance on (attendance.student_id = teacher.teacher_id) and month(date) = '" + txtMonth.Text + "' and year(date)='" + txtYear.Text + "'  " +
                        " " + queryType + " group by teacher.teacher_id";
            MySqlCommand cmd = new MySqlCommand(query, con);
            MySqlDataAdapter adp = new MySqlDataAdapter(cmd);
            DataTable table = new DataTable();
            adp.Fill(table);
            if (table.Rows.Count > 0)
            {
                int absent = 0;
                int leave = 0;
                int present = 0;
                for (int i = 0; i < table.Rows.Count; i++)
                {
                    absent = 0; leave = 0; present = 0;
                    for (int k = 0; k < table.Columns.Count; k++)
                    {
                        var s = table.Rows[i][k].ToString();
                        if (table.Rows[i][k].ToString() == "A")
                            absent++;
                        if (table.Rows[i][k].ToString() == "L")
                            leave++;
                        if (table.Rows[i][k].ToString() == "P")
                            present++;
                    }
                    table.Rows[i]["T_A"] = absent;
                    table.Rows[i]["T_L"] = leave;
                    table.Rows[i]["T_P"] = present;
                }
                gridTMAttendance.DataSource = null;
                gridView1.Columns.Clear();

                gridTMAttendance.DataSource = CommonFunctions.AutoNumberedTable(table);
                GridColumn Column = gridView1.Columns["Sr#"];
                Column.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;

                GridColumn Column3 = gridView1.Columns["T_P"];
                Column3.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
                GridColumn Column4 = gridView1.Columns["T_A"];
                Column4.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
                GridColumn Column6 = gridView1.Columns["T_L"];
                Column6.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
                var Column5 = gridView1.Columns["Name"];
                Column5.Width = 70;
                gridView1.BestFitColumns();
                var Column1 = gridView1.Columns["Phone#"];
                Column1.Width = 55;

                con.Close();
                GridFormatRule gridFormatRuleP = new GridFormatRule();
                GridFormatRule gridFormatRuleA = new GridFormatRule();
                FormatConditionRuleDataBar formatConditionRuleDataBarP = new FormatConditionRuleDataBar();
                FormatConditionRuleDataBar formatConditionRuleDataBarA = new FormatConditionRuleDataBar();

                gridFormatRuleP.Column = gridView1.Columns["T_P"];
                gridFormatRuleA.Column = gridView1.Columns["T_A"];
                formatConditionRuleDataBarP.PredefinedName = "Green";
                formatConditionRuleDataBarP.MaximumType = FormatConditionValueType.Number;
                formatConditionRuleDataBarP.Maximum = total;
                formatConditionRuleDataBarP.MinimumType = FormatConditionValueType.Number;
                formatConditionRuleDataBarP.Minimum = 0;
                formatConditionRuleDataBarA.PredefinedName = "Coral";
                formatConditionRuleDataBarA.MaximumType = FormatConditionValueType.Number;
                formatConditionRuleDataBarA.Maximum = total;
                formatConditionRuleDataBarA.MinimumType = FormatConditionValueType.Number;
                formatConditionRuleDataBarA.Minimum = 0;

                gridFormatRuleP.Rule = formatConditionRuleDataBarP;
                gridFormatRuleA.Rule = formatConditionRuleDataBarA;
                gridView1.FormatRules.Add(gridFormatRuleP);
                gridView1.FormatRules.Add(gridFormatRuleA);
                showReport();
            }
            else
            {
                MessageBox.Show("No Staff Available in this Staff type '" + txtStaffType.Text + "'", "info", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
        void showReport()
        {
            XtraMonthlyAttendanceTeacher report = new XtraMonthlyAttendanceTeacher();
            Image logo = fun.Base64ToImage(Login.Logo);
            var school = fun.GetSettings("system_title");
            report.PicIogoBox.Image = logo;
            report.LabTitle.Text = school;
            report.LabAddress.Text = fun.GetSettings("address");
            report.LabMonth.Text = txtMonth.Text + " - " + txtYear.Text;
            report.GridControl = gridTMAttendance;
            documentViewer1.DocumentSource = report;

            report.CreateDocument();
            documentViewer1.PrintingSystem.Document.AutoFitToPagesWidth = 1;
        }
        private void gridView1_RowCellStyle(object sender, DevExpress.XtraGrid.Views.Grid.RowCellStyleEventArgs e)
        {
            var tempdate = "";
            int tDays;
            if (txtMonth.Text == "")
            {
                tempdate = DateTime.Now.Month + "-" + 1 + "-" + DateTime.Now.Year;
                tDays = DateTime.DaysInMonth(Convert.ToInt32(DateTime.Now.Year), Convert.ToInt32(DateTime.Now.Month));

            }
            else
            {
                tempdate = txtMonth.Text + "-" + 1 + "-" + txtYear.Text;
                tDays = DateTime.DaysInMonth(Convert.ToInt32(txtYear.Text), Convert.ToInt32(txtMonth.Text));

            }
            DateTime date = Convert.ToDateTime(tempdate);
            GridView View = sender as GridView;
            for (int i = 1; i <= tDays; i++)
            {
                if (e.Column.FieldName == i.ToString())
                {
                    var day = date.DayOfWeek;
                    if (day.ToString() == "Sunday")
                    {
                        string present = View.GetRowCellDisplayText(e.RowHandle, View.Columns[i.ToString()]);
                        if (present != "")
                        {
                            e.Appearance.BackColor = Color.LightBlue;
                            e.Appearance.ForeColor = Color.DarkBlue;
                        }
                    }
                    else
                    {
                        string present = View.GetRowCellDisplayText(e.RowHandle, View.Columns[i.ToString()]);
                        if (present == "A")
                            e.Appearance.ForeColor = Color.Red;
                        if (present == "P" || present == "P/S")
                            e.Appearance.ForeColor = Color.Green;
                        if (present == "P/H" || present == "H")
                            e.Appearance.ForeColor = Color.Blue;
                        if (present == "L")
                            e.Appearance.ForeColor = Color.Orange;
                    }
                }
                date = date.AddDays(1);
            }
        }
    }
}
