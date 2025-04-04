using DevExpress.LookAndFeel;
using DevExpress.XtraEditors.Repository;
using DevExpress.XtraGrid.Columns;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraReports.UI;
using MySql.Data.MySqlClient;
using Newtonsoft.Json;
using System;
using System.Collections.ObjectModel;
using System.Data;
using System.Drawing;
using System.Media;
using System.Windows.Forms;

namespace SchoolManagementSystem.Attendance
{
    public partial class TeacherAttendance : DevExpress.XtraEditors.XtraUserControl
    {
        private static TeacherAttendance _instance;

        public static TeacherAttendance instance
        {
            get
            {
                if (_instance == null)
                    _instance = new TeacherAttendance();
                return _instance;
            }
        }
        ObservableCollection<ManageAttendance> ManageAtted = new ObservableCollection<ManageAttendance>();
        ObservableCollection<AllClass> allClass = new ObservableCollection<AllClass>();

        CommonFunctions fun = new CommonFunctions();
        public TeacherAttendance()
        {
            InitializeComponent();
            loadfunctions();
        }
        public void loadfunctions()
        {
            txtYear.Text = DateTime.Now.Year.ToString();
            txtMonth.Text = DateTime.Now.Month.ToString();
            txtDate.Text = DateTime.Now.Day.ToString();
            txtSelectTeacher.Properties.DataSource = fun.GetAllTeacher_dt();
            txtSelectTeacher.Properties.DisplayMember = "name";
            txtSelectTeacher.Properties.ValueMember = "teacher_id";

            txtVal.Properties.DataSource = fun.Attendance_status();
            txtVal.Properties.DisplayMember = "title";
            txtVal.Properties.ValueMember = "status";
        }
        public void permissions_btns(bool add, bool Edit, bool Delete)
        {
            BtnUpdateTAttend.Enabled = false;
            btnSave.Enabled = false;
            if (Edit)
            {
                BtnUpdateTAttend.Enabled = true;
                btnSave.Enabled = true;
            }
        }
        private void btnDAFind_Click(object sender, EventArgs e)
        {
            FillAttendance();
        }
        void FillAttendance()
        {
            if (txtMonth.Text == "" || txtYear.Text == "" || txtDate.Text == "")
            {
                MessageBox.Show("Fill all fields....", "Info");
                return;
            }
            string day1 = txtYear.Text + "-" + txtMonth.Text + "-" + txtDate.Text;
            BtnManageAttendance.Enabled = true;
            MySqlConnection con = new MySqlConnection(Login.constring);
            con.Open();
            string query = "SELECT attendance.date as Date,teacher.name as Teacher ,teacher.staff_type as Staff_Type,teacher.phone as `Phone #`, ifnull(ats.abbr,'A') as Status, " +
                "attendance.time_in as TimeIn,attendance.time_out  as TimeOut, " +
                "if(TIME_TO_SEC(TIMEDIFF(attendance.time_in,attendance.setting_in))/60<=0,0,TIME_TO_SEC(TIMEDIFF(attendance.time_in,attendance.setting_in))/60) as `L.Time` ,'' as Remarks " +
                " from teacher  " +
                " left join attendance on attendance.student_id=teacher.teacher_id AND attendance.date='" + day1 + "'  " +
                " left join attendance_status as ats on ats.status = attendance.status " +
                " where teacher.passout = 0 " +
                " order by teacher.name";
            MySqlCommand cmdP = new MySqlCommand(query, con);
            MySqlDataAdapter adp = new MySqlDataAdapter(cmdP);
            DataTable table = new DataTable();
            adp.Fill(table);
            gridTDailyAttendance.DataSource = null;
            gridView1.Columns.Clear();
            gridTDailyAttendance.DataSource = CommonFunctions.AutoNumberedTable(table);
            gridView1.BestFitColumns();
            con.Close();
            gridView1.OptionsBehavior.Editable = false;
            gridView1.OptionsBehavior.ReadOnly = true;
            var col4 = gridView1.Columns["Sr#"];
            col4.Width = 40;
            GridColumn Column = gridView1.Columns["Date"];
            Column.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            GridColumn Column2 = gridView1.Columns["Phone #"];
            Column2.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            GridColumn Column3 = gridView1.Columns["Sr#"];
            Column3.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            GridColumn Column4 = gridView1.Columns["TimeIn"];
            Column4.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            GridColumn Column5 = gridView1.Columns["TimeOut"];
            Column5.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            GridColumn Column6 = gridView1.Columns["L.Time"];
            Column6.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            GridColumn Column7 = gridView1.Columns["Status"];
            Column7.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
        }
        private void BtnManageAttendance_Click(object sender, EventArgs e)
        {
            if (txtMonth.Text == "" || txtYear.Text == "" || txtDate.Text == "")
            {
                MessageBox.Show("Fill all fields....", "Info");
                return;
            }
            updateAttend();
        }
        void updateAttend()
        {
            ManageAtted.Clear();
            string day = txtYear.Text + "-" + txtMonth.Text + "-" + txtDate.Text;
            MySqlConnection con = new MySqlConnection(Login.constring);
            con.Open();
            string query = "SELECT attendance_id,teacher.teacher_id as id,teacher.name as Teacher,ifnull(attendance.status,0) as status FROM teacher"
                + " LEFT JOIN attendance ON attendance.student_id = teacher.teacher_id and attendance.date = '" + day + "' where  teacher.passout != 1";
            MySqlCommand cmdP = new MySqlCommand(query, con);
            MySqlDataReader reader1 = cmdP.ExecuteReader();
            if (reader1.HasRows)
            {
                while (reader1.Read())
                {
                    ManageAttendance d = new ManageAttendance
                    {
                        Att_ID = reader1["attendance_id"].ToString(),
                        ID = reader1["id"].ToString(),
                        Student = reader1["Teacher"].ToString(),
                        Status = reader1["status"].ToString()
                    };
                    ManageAtted.Add(d);
                }
            }
            con.Close();
            gridTDailyAttendance.DataSource = null;
            gridView1.Columns.Clear();
            gridTDailyAttendance.DataSource = ManageAtted;
            gridView1.BestFitColumns();
            RepositoryItemLookUpEdit att_status = new RepositoryItemLookUpEdit();
            att_status.DataSource = fun.Attendance_status();
            att_status.DisplayMember = "title";
            att_status.ValueMember = "status";
            gridView1.Columns["Status"].ColumnEdit = att_status;
            var col = gridView1.Columns["Att_ID"];
            col.OptionsColumn.ReadOnly = true;
            col.Visible = false;
            gridView1.OptionsBehavior.Editable = true;
            gridView1.OptionsBehavior.ReadOnly = false;
        }
        private void BtnUpdateTAttend_Click(object sender, EventArgs e)
        {
            if (txtMonth.Text == "" || txtYear.Text == "" || txtDate.Text == "")
            {
                MessageBox.Show("Fill all fields....", "Info");
                return;
            }
            string day = txtYear.Text + "-" + txtMonth.Text + "-" + txtDate.Text;
            MySqlConnection con = new MySqlConnection(Login.constring);
            updateAttend();
            if (ManageAtted.Count == 0)
            {
                MessageBox.Show("First Select Current Days Attendance..!!", "Info");
                return;
            }
            foreach (var std in ManageAtted)
            {
                var alreadyExit = "";
                MySqlConnection con1 = new MySqlConnection(Login.constring);
                con1.Open();
                MySqlCommand cmd1 = new MySqlCommand("SELECT * from attendance Where date='" + day + "' and student_id='" + std.ID.ToString() + "'", con1);
                MySqlDataReader reader1 = cmd1.ExecuteReader();
                if (reader1.HasRows)
                {
                    while (reader1.Read())
                    {
                        alreadyExit = reader1["student_id"].ToString();
                    }
                }
                con1.Close();
                if (alreadyExit == "")
                {
                    con.Open();
                    try
                    {
                        MySqlCommand cmd = new MySqlCommand("INSERT into attendance(status,student_id,date,time_in,time_out,sync) VALUES('1','" + std.ID.ToString() + "','" + day + "','" + DateTime.Now.TimeOfDay + "','','0')", con);
                        cmd.ExecuteNonQuery();
                    }
                    catch (MySqlException ex)
                    {
                        SystemSounds.Hand.Play();
                        MessageBox.Show(ex.Message, "Error");
                        return;
                    }
                    con.Close();
                }
            }
            updateAttend();
        }
        private void gridView1_RowUpdated(object sender, DevExpress.XtraGrid.Views.Base.RowObjectEventArgs e)
        {
            MySqlConnection con = new MySqlConnection(Login.constring);
            string day = txtYear.Text + "-" + txtMonth.Text + "-" + txtDate.Text;

            var row = gridView1.GetRow(gridView1.FocusedRowHandle);
            var rowS = JsonConvert.SerializeObject(row);
            var v = rowS.ToString().Split(',');
            var result = "";
            var status = v[3].Split(':')[1].Trim('\"', '}', '{');
            var tea_id = v[1].Split(':')[1].Trim('\"', '}', '{');
            var att_id = v[0].Split(':')[1].Trim('\"', '}', '{');

            if (status == "Absent")
                result = "0";
            if (status == "Present")
                result = "1";
            if (status == "Leave")
                result = "2";
            var query = "";
            if (att_id != "")
                query = "UPDATE attendance set status='" + result + "',date='" + day + "',time_in='" + DateTime.Now.ToString("HH:mm") + "',sync='0'WHERE student_id='" + tea_id + "' and date='" + day + "';";
            else
                query = "INSERT into attendance(status,student_id,date,time_in,time_out,sync) VALUES('" + result + "','" + tea_id + "','" + day + "','" + DateTime.Now.ToString("HH:mm") + "','','0')";
            con.Open();
            MySqlCommand cmd = new MySqlCommand(query, con);
            cmd.ExecuteNonQuery();
            con.Close();
        }
        private void BtnTAPrint_Click(object sender, EventArgs e)
        {
            showReport();
        }
        void showReport()
        {
            XtraDaliyAttendanceTeacher report = new XtraDaliyAttendanceTeacher();
            Image logo = fun.Base64ToImage(Login.Logo);
            var school = fun.GetSettings("system_title");
            report.PicIogoBox.Image = logo;
            report.LabTitle.Text = school;
            report.LabAddress.Text = fun.GetSettings("address");
            report.labDate.Text = txtDate.Text + "-" + txtMonth.Text + "-" + txtYear.Text;
            report.GridControl = gridTDailyAttendance;

            ReportPrintTool printTool = new ReportPrintTool(report);
            printTool.PreviewForm.PrintingSystem.Document.AutoFitToPagesWidth = 1;
            printTool.ShowRibbonPreviewDialog(UserLookAndFeel.Default);
        }
        private void TeacherAttendance_Enter(object sender, EventArgs e)
        {
            FillAttendance();
        }
        private void btnSave_Click(object sender, EventArgs e)
        {
            if (txtMonth.Text == "" || txtYear.Text == "" || txtDate.Text == "" || txtSelectTeacher.EditValue == null || string.IsNullOrEmpty(txtVal.EditValue.ToString()))
            {
                MessageBox.Show("Fill all fields....", "Info");
                return;
            }
            string day = txtYear.Text + "-" + txtMonth.Text + "-" + txtDate.Text;
            var query = ""; var result = txtVal.EditValue.ToString();
            int alreadyExit = 0;
            
            MySqlConnection con = new MySqlConnection(Login.constring);
            con.Open();
            query = "select * from attendance WHERE student_id='" + txtSelectTeacher.EditValue.ToString() + "' and date='" + day + "'";
            MySqlCommand cmd = new MySqlCommand(query, con);
            MySqlDataReader reader1 = cmd.ExecuteReader();
            if (reader1.HasRows)
                alreadyExit = 1;
            con.Close();
            if (alreadyExit == 1)
                query = "UPDATE attendance set status='" + result + "',time_in='" + DateTime.Now.ToString("HH:mm") + "' ,sync='0'WHERE student_id='" + txtSelectTeacher.EditValue.ToString() + "' and date='" + day + "';";
            else
                query = "INSERT into attendance(status,student_id,date,time_in,time_out,sync) VALUES('" + result + "','" + txtSelectTeacher.EditValue.ToString() + "','" + day + "','" + DateTime.Now.ToString("HH:mm") + "','','0')";
            con.Open();
            MySqlCommand cmd1 = new MySqlCommand(query, con);
            cmd1.ExecuteNonQuery();
            con.Close();
            alreadyExit = 0;
            FillAttendance();
        }
        private void btnUpdate_Click(object sender, EventArgs e)
        {
            string day = txtYear.Text + "-" + txtMonth.Text + "-" + txtDate.Text;
            MySqlConnection con = new MySqlConnection(Login.constring);
            con.Open(); var result = txtVal.EditValue.ToString();
            var query = "UPDATE attendance set status='" + result + "',time_in='" + DateTime.Now.ToString("HH:mm") + "' ,sync='0'WHERE student_id='" + txtSelectTeacher.EditValue.ToString() + "' and date='" + day + "';";
            MySqlCommand cmd = new MySqlCommand(query, con);
            cmd.ExecuteNonQuery();
            con.Close();
            FillAttendance();
        }

        private void gridView1_RowStyle(object sender, DevExpress.XtraGrid.Views.Grid.RowStyleEventArgs e)
        {
            GridView View = sender as GridView;
            if (e.RowHandle >= 0 && View.Columns["Status"] != null)
            {
                string present = View.GetRowCellDisplayText(e.RowHandle, View.Columns["Status"]);
                if (present == "P" || present == "Present")
                    e.Appearance.ForeColor = Color.Green;
                if (present == "A" || present == "Absent")
                    e.Appearance.ForeColor = Color.OrangeRed;
                if (present == "L" || present == "Leave")
                    e.Appearance.ForeColor = Color.Blue;
            }
        }

        private void btnSummary_Click(object sender, EventArgs e)
        {
            if (txtMonth.Text == "" || txtYear.Text == "" || txtDate.Text == "")
            {
                MessageBox.Show("Year,Month and Date are required fields....", "Info");
                return;
            }
            string day1 = txtYear.Text + "-" + txtMonth.Text + "-" + txtDate.Text;

        
            string query = "SELECT teacher.staff_type,  "+
                            " COUNT(teacher.teacher_id) AS total, SUM(IF(attendance.status = 1, 1, 0)) AS present, SUM(IF(attendance.status IS NULL, 1, 0)) AS absent, "+
                            " SUM(IF(attendance.status = 2 OR attendance.status = 3, 1, 0)) AS num_leaves, FLOOR(100 * SUM(IF(attendance.status = 1, 1, 0)) / COUNT(teacher.teacher_id)) AS `present%` "+
                            " FROM teacher "+
                            " LEFT JOIN attendance ON attendance.student_id = teacher.teacher_id AND attendance.date = '"+ day1 + "' "+
                            " WHERE 1 = 1 GROUP BY teacher.staff_type";

            DataTable table = fun.FetchDataTable(query);

   
            gridTDailyAttendance.DataSource = null;
            gridView1.Columns.Clear();
            gridTDailyAttendance.DataSource = CommonFunctions.AutoNumberedTable(table);
            gridView1.BestFitColumns();

            gridView1.GroupSummary.Add(DevExpress.Data.SummaryItemType.Sum, "present", gridView1.Columns["present"]);
            gridView1.GroupSummary.Add(DevExpress.Data.SummaryItemType.Sum, "absent", gridView1.Columns["absent"]);
            gridView1.GroupSummary.Add(DevExpress.Data.SummaryItemType.Sum, "leave", gridView1.Columns["leave"]);
            gridView1.GroupSummary.Add(DevExpress.Data.SummaryItemType.Average, "present%", gridView1.Columns["present%"]);



        }
    }
}
