using DevExpress.LookAndFeel;
using DevExpress.XtraGrid.Columns;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraReports.UI;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

namespace SchoolManagementSystem.Attendance
{
    public partial class StudentAttendance : DevExpress.XtraEditors.XtraUserControl
    {
        private static StudentAttendance _instance;

        public static StudentAttendance instance
        {
            get
            {
                if (_instance == null)
                    _instance = new StudentAttendance();
                return _instance;
            }
        }
        ObservableCollection<AllClass> allClass = new ObservableCollection<AllClass>();
      //  ObservableCollection<StudentAtt> studentatt = new ObservableCollection<StudentAtt>();

        CommonFunctions fun = new CommonFunctions();
        public StudentAttendance()
        {
            InitializeComponent();
            loadfunctions();
        }
        public void loadfunctions()
        {
            fun.DateFormatOnlyYM(txtFMonth);
            fun.DateFormatOnlyYM(txtTMonth);

            txtClass.Properties.DataSource = fun.GetAllClasses_dt();
            txtClass.Properties.DisplayMember = "name";
            txtClass.Properties.ValueMember = "class_id";
        }
        int std;
        DataTable stdInfo = new DataTable();
        private void btnSAFind_Click(object sender, System.EventArgs e)
        {
            if (txtFMonth.Text == "" || txtTMonth.Text == "" || string.IsNullOrEmpty(txtClass.EditValue.ToString()) || string.IsNullOrEmpty(txtStudent.EditValue.ToString()))
            {
                MessageBox.Show("Fill all field....!!", "Info");
                return;
            }
            var classID = Convert.ToInt32(txtClass.EditValue.ToString());//fun.GetClassIDisSession(txtClass.Text, Main_FD.SelectedSession);
            var sectionID = Convert.ToInt32(txtSection.EditValue.ToString());//fun.GetSectionIDisClass(txtSection.Text, classID);

            //studentatt.Clear();
            DateTime Fdate = txtFMonth.DateTime;
            DateTime Tdate = txtTMonth.DateTime;
            gridStudentAttendance.DataSource = null;
            gridView1.Columns.Clear();
            MySqlConnection con = new MySqlConnection(Login.constring);
            std = Convert.ToInt32(txtStudent.EditValue.ToString());//fun.GetStudentID(txtStudent.Text.Split('>')[0]);
            stdInfo = fun.GetStudentInfo_n(std);
            int Present = 0;
            int Absent = 0;
            var tempdate = Fdate.Month + "/" + 1 + "/" + Fdate.Year;
            DateTime date = Convert.ToDateTime(tempdate);
            if (classID != 0)
            {
                int sun = 0;
                int total = 0;
                var query = "";
                query += "SELECT  group_concat(month(attendance.date),'-',year(date)) as Month";
                for (int i = 1; i <= 31; i++)
                {
                    var day = date.DayOfWeek;

                    if (day.ToString() == "Sunday")
                    {
                        sun++;
                    }
                    query += ",MAX(CASE WHEN DAYOFMONTH(DATE)='" + i + "' THEN att_s.`abbr` ELSE 'A' END) AS '" + i + "' ";
                    //query += " ,MAX(CASE WHEN dayofmonth(date)='" + i + "' and attendance.status='1' THEN 'P' WHEN dayofmonth(date)='" + i + "' and attendance.status='2'  THEN 'L' WHEN dayofmonth(date)='" + i + "' and attendance.status='3'  THEN 'S/L' ELSE 'A' END) AS '" + i + "'";
                    date = date.AddDays(1);
                    total = i;
                }
                total = total - sun;
                query += ",'' as 'Present','' as 'Absent','' as 'Leave','' as 'SLeave','' as 'Sunday','' as 'Holidays' FROM student " +
                    " left join attendance on (attendance.student_id = student.student_id) and month(date) >= '" + Fdate.Month + "'and month(date) <= '" + Tdate.Month + "' and year(date)>='" + Fdate.Year + "' and year(date)<='" + Tdate.Year + "' " +
                    " LEFT JOIN `attendance_status` AS att_s ON att_s.`status` = attendance.status " +
                            " join class on(class.class_id=student.class_id) left join section on section.section_id=student.section_id where student.class_id = '" + classID + "'and student.section_id='" + sectionID + "' and attendance.student_id = '" + std + "' group by month(date),year(date) ";
                MySqlCommand cmd = new MySqlCommand(query, con);
                MySqlDataAdapter adp = new MySqlDataAdapter(cmd);
                DataTable table = new DataTable();
                adp.Fill(table);
                foreach (DataRow row in table.Rows)
                {
                    var month = row[0].ToString().Split(',')[0];
                    string temp = month.Split('-')[0] + "-" + 1 + "-" + month.Split('-')[1];
                    date = Convert.ToDateTime(temp);
                    List<DateTime> dateHolidays = new List<DateTime>();
                    dateHolidays = fun.GetAllHolidaysAMonth(date,sectionID.ToString());
                    List<string> WorkingDays = new List<string>();
                    WorkingDays = fun.GetAllWorkingDaysS(sectionID);

                    int s = 0;
                    int count = 1;
                    int h = 0;
                    int days = DateTime.DaysInMonth(Convert.ToInt32(month.Split('-')[1]), Convert.ToInt32(month.Split('-')[0]));
                    for (int i = 1; i <= days; i++)
                    {
                        var day = date.DayOfWeek;
                        if (date <= DateTime.Now.Date)
                            if (dateHolidays.Contains(date))
                                if (day.ToString() == "Sunday")
                                {
                                    if (row[i].ToString() == "P")
                                        row[i] = "P/S";
                                    else
                                        row[i] = "S/H";
                                    s++;
                                }
                                else
                                {
                                    if (row[i].ToString() == "P")
                                        row[i] = "P/H";
                                    else
                                        row[i] = "H";
                                }
                            else if (!WorkingDays.Contains(day.ToString()))
                            {
                                if (day.ToString() == "Sunday")
                                {
                                    if (row[i].ToString() == "P")
                                        row[i] = "P/S";
                                    else
                                        row[i] = "S";
                                    s++;
                                }
                                else
                                    row[i] = " ";

                            }
                        if (date > DateTime.Now.Date)
                        {
                            row[i] = "-";
                            h++;
                        }
                        date = date.AddDays(1);
                        count++;
                    }
                    for (; count <= 31; count++)
                        row[count] = "-";
                    int absent = 0;
                    int leave = 0;
                    int sleave = 0;
                    int present = 0;
                    int Holiday = 0;
                    for (int k = 1; k <= 31; k++)
                    {
                        if (row[k].ToString() == "A")
                            absent++;
                        if (row[k].ToString() == "P" || row[k].ToString() == "P/H")
                            present++;
                        if (row[k].ToString() == "L")
                            leave++;
                        if (row[k].ToString() == "S/L")
                        { sleave++; present++; }
                        if (row[k].ToString() == "H")
                            Holiday++;
                    }
                    row[32] = present;
                    row[33] = absent;
                    row[34] = leave;
                    row[35] = leave;
                    row[36] = s;
                    row["Holidays"] = Holiday;
                    row[0] = month;


                    gridStudentAttendance.DataSource = table;
                    gridView1.BestFitColumns();
                    GridColumn Column = gridView1.Columns["Month"];
                    Column.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
                    GridColumn Column2 = gridView1.Columns["Present"];
                    Column2.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
                    GridColumn Column3 = gridView1.Columns["Absent"];
                    Column3.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
                    GridColumn Column4 = gridView1.Columns["Sunday"];
                    Column4.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
                    GridColumn Column5 = gridView1.Columns["Holidays"];
                    Column5.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;

                    con.Close();
                    txtp.Text = Present.ToString();
                    txta.Text = Absent.ToString();

                }
            }
        }
        private void BtnPrint_Click(object sender, EventArgs e)
        {
            showReport();
        }
        void showReport()
        {
            XtraStdAttendanceReport report = new XtraStdAttendanceReport();

            Image logo = fun.Base64ToImage(Login.Logo);
            var school = fun.GetSettings("system_title");
            var a = txtStudent.Text;
            var stdName = txtStudent.Text;
            var stdID = txtStudent.EditValue.ToString();
            if (stdInfo.Rows.Count > 0) {
                report.PicIogoBox.Image = logo;
                report.LabTitle.Text = school;
                report.labName.Text = stdName;
                report.LabFName.Text = stdInfo.Rows[0]["FatherName"].ToString();
                report.LabClass.Text = txtClass.Text;
                report.LabSection.Text = txtSection.Text;
                report.LabSAddress.Text = stdInfo.Rows[0]["Address"].ToString();
                report.labRoll.Text = stdInfo.Rows[0]["Roll"].ToString();
                report.LabPhone.Text = stdInfo.Rows[0]["FatherPhone"].ToString();
                report.PicStdBox.Image = fun.get_image(@"\Images\Students\", stdID.Trim() + "_std", false, stdInfo.Rows[0]["Gender"].ToString());
            }
            report.GridControl = gridStudentAttendance;
            report.GridControl.Width = 100;
            report.LabAddress.Text = fun.GetSettings("address");
            report.LabTel.Text = fun.GetSettings("phone");
            report.labMonth.Text = txtTMonth.Text + " To " + txtFMonth.Text;
            
            ReportPrintTool printTool = new ReportPrintTool(report);
            printTool.PreviewForm.PrintingSystem.Document.AutoFitToPagesWidth = 1;
            printTool.ShowRibbonPreviewDialog(UserLookAndFeel.Default);
        }
        private void StudentAttendance_Enter(object sender, EventArgs e)
        {
        }

        private void gridView1_RowStyle(object sender, DevExpress.XtraGrid.Views.Grid.RowStyleEventArgs e)
        {
            GridView View = sender as GridView;
            if (e.RowHandle >= 0 && View.Columns["Status"] != null)
            {

                string present = View.GetRowCellDisplayText(e.RowHandle, View.Columns["Status"]);
                if (present == "Absent")
                {
                    e.Appearance.BackColor = Color.DarkRed;
                }
                if (present == "Present")
                {
                    e.Appearance.BackColor = Color.YellowGreen;
                }

            }
        }

        private void gridView1_RowCellStyle(object sender, RowCellStyleEventArgs e)
        {
            GridView View = sender as GridView;
            for (int i = 1; i <= 31; i++)
            {
                if (e.Column.FieldName == i.ToString())
                {
                    string present = View.GetRowCellDisplayText(e.RowHandle, View.Columns[i.ToString()]);
                    if (present == "S" || present == "S/H")
                        e.Appearance.ForeColor = Color.DarkBlue;
                    if (present == "A")
                        e.Appearance.ForeColor = Color.Red;
                    if (present == "P" || present == "P/S")
                        e.Appearance.ForeColor = Color.Green;
                    if (present == "P/H" || present == "H")
                        e.Appearance.ForeColor = Color.Blue;
                    if (present == "L")
                        e.Appearance.ForeColor = Color.Orange;
                    if (present == "S/L")
                        e.Appearance.ForeColor = Color.OrangeRed;
                }
            }
        }

        private void txtClass_EditValueChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtClass.EditValue.ToString()))
            {
                txtSection.Properties.DataSource = fun.GetAllSection_dt(txtClass.EditValue.ToString());
                txtSection.Properties.DisplayMember = "name";
                txtSection.Properties.ValueMember = "section_id";
            }
        }

        private void txtSection_EditValueChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtSection.EditValue.ToString()))
            {
                int classID = Convert.ToInt32(txtClass.EditValue.ToString());
                int sectionID = Convert.ToInt32(txtSection.EditValue.ToString());
                txtStudent.Properties.DataSource = fun.GetAllStudentsWId_S_C_S(classID, sectionID);
                txtStudent.Properties.DisplayMember = "student";
                txtStudent.Properties.ValueMember = "student_id";
            }
        }
    }
}
