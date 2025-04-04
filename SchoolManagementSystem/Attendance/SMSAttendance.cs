using DevExpress.XtraEditors;
using MySql.Data.MySqlClient;
using System;
using System.Collections.ObjectModel;
using System.Windows.Forms;

namespace SchoolManagementSystem.Attendance
{
    public partial class SMSAttendance : DevExpress.XtraEditors.XtraUserControl
    {
        private static SMSAttendance _instance;

        public static SMSAttendance instance
        {
            get
            {
                if (_instance == null)
                    _instance = new SMSAttendance();
                return _instance;
            }
        }
        ObservableCollection<AllClass> allClass = new ObservableCollection<AllClass>();
        CommonFunctions fun = new CommonFunctions();
        public SMSAttendance()
        {
            InitializeComponent();
            loadfunctions();
        }
        public void loadfunctions()
        {
            load_classes();
            txtYear.Text = DateTime.Now.Year.ToString();
            txtMonth.Text = DateTime.Now.Month.ToString();
            txtDate.Text = DateTime.Now.Day.ToString();
        }
        public void permissions_btns(bool add, bool Edit, bool Delete)
        {
            BtnSendOutSMS.Enabled = false;
            BtnSendSMS.Enabled = false;
            if (add)
            {
                BtnSendOutSMS.Enabled = true;
                BtnSendSMS.Enabled = true;
            }
        }
        private void BtnSendSMS_Click(object sender, EventArgs e)
        {
            if (txtMonth.Text == "" || txtYear.Text == "" || txtDate.Text == "" || txtClass.EditValue == null || ddlsection.EditValue == null || txtType.Text == "")
            {
                MessageBox.Show("Fill all fields....", "Info");
                return;
            }
            SendINPresentSMS();
        }

        async void SendINPresentSMS()
        {
            txtMemoStatus.Text = "";
            string day = txtYear.Text + "-" + txtMonth.Text + "-" + txtDate.Text;
            var SMS = fun.GetSetting("enable_in_sms");
            if (SMS[0].Name == "1")
            {
                MySqlConnection con = new MySqlConnection(Login.constring);
                MySqlConnection con1 = new MySqlConnection(Login.constring);
                string phone = "";
                object sections = ddlsection.EditValue;

                con.Open();
                var query = "SELECT student.name as student, parent.name as parent, parent.phone, attendance.date,attendance.status,cls.name as class,sec.name as section "
                    + " FROM student "
                    + " INNER JOIN parent ON parent.parent_id = student.parent_id "
                    + " inner join class as cls on cls.class_id = student.class_id "
                    + " inner join section as sec on sec.section_id = student.section_id "
                    + " LEFT JOIN attendance ON attendance.student_id = student.student_id AND attendance.date = '" + day + "' "
                    + " WHERE student.passout != 1 AND student.attendent_sms = 1 and student.section_id in (" + sections + ") ";
                if (txtHosteliesed.Text == "Hostel")
                    query += " AND student.student_id in  (select student_id from hostels_student as hos where  hos.student_id = student.student_id)";
                else if (txtHosteliesed.Text == "Non-Hostel")
                    query += " AND Not student.student_id in  (select student_id from hostels_student as hos where  hos.student_id = student.student_id)";

                query += " AND student.student_id NOT IN(SELECT student_id FROM tbl_students_leaves WHERE student_id = student.student_id AND start_date <= '" + day + "' AND end_date >= '" + day + "') ";
                var query1 = "SELECT count(*) "
                    + " FROM student INNER JOIN parent ON parent.parent_id = student.parent_id "
                    + " LEFT JOIN attendance ON attendance.student_id = student.student_id AND attendance.date = '" + day + "' "
                    + " WHERE student.passout != 1 AND student.attendent_sms = 1 and student.section_id in (" + sections + ") ";
                if (txtHosteliesed.Text == "Hostel")
                    query1 += " AND student.student_id in  (select student_id from hostels_student as hos where  hos.student_id = student.student_id)";
                else if (txtHosteliesed.Text == "Non-Hostel")
                    query1 += " AND Not student.student_id in  (select student_id from hostels_student as hos where  hos.student_id = student.student_id)";

                MySqlCommand cmd1 = new MySqlCommand(query, con);
                MySqlCommand cmd2 = new MySqlCommand(query1, con);
                int mysqlint = Convert.ToInt32(cmd2.ExecuteScalar());
                MySqlDataReader reader1 = cmd1.ExecuteReader();
                if (reader1.HasRows)
                {
                    int count = 1;
                    while (reader1.Read())
                    {
                        var sms = "";
                        phone = reader1["phone"].ToString();
                        if (txtType.Text == "Both Present and Absent")
                        {
                            allClass.Clear();
                            if (reader1["status"].ToString() == "1")
                            {
                                var mark = fun.GetSetting("in_sms");
                                string mark_template = mark[0].Name;
                                sms = mark_template.Trim().Replace("[parent]", reader1["parent"].ToString())
                                                    .Replace("[student]", reader1["student"].ToString())
                                                    .Replace("[date]", day.ToString()).Replace("[class]", reader1["class"].ToString()).Replace("[section]", reader1["section"].ToString());
                            }
                            else
                            {
                                var mark = fun.GetSetting("absent_sms");
                                string mark_template = mark[0].Name;
                                sms = mark_template.Trim()
                                    .Replace("[parent]", reader1["parent"].ToString())
                                    .Replace("[student]", reader1["student"].ToString())
                                    .Replace("[date]", day.ToString()).Replace("[class]", reader1["class"].ToString()).Replace("[section]", reader1["section"].ToString());
                            }
                            con1.Open();
                            MySqlCommand cmd = new MySqlCommand("INSERT into sms_que(mobile,sms,status) VALUES('" + phone + "','" + sms + "','" + 0 + "');", con1);
                            cmd.ExecuteNonQuery();
                            con1.Close();
                            txtMemoStatus.Text += "\r\n Send SMS To " + phone + " ->  " + sms + " " + count + "/" + mysqlint + "\r\n";


                        }
                        if (txtType.Text.Trim() == "Present")
                        {
                            if (reader1["status"].ToString() == "1")
                            {
                                var mark = fun.GetSetting("in_sms");
                                string mark_template = mark[0].Name;
                                sms = mark_template.Trim()
                                    .Replace("[parent]", reader1["parent"].ToString())
                                    .Replace("[student]", reader1["student"].ToString())
                                    .Replace("[date]", day.ToString());

                                con1.Open();
                                MySqlCommand cmd = new MySqlCommand("INSERT into sms_que(mobile,sms,status) VALUES('" + phone + "','" + sms + "','" + 0 + "');", con1);
                                cmd.ExecuteNonQuery();
                                con1.Close();
                                txtMemoStatus.Text += "\r\n Send SMS To " + phone + " ->  " + sms + " " + count + "/" + mysqlint + "\r\n";

                            }
                        }
                        if (txtType.Text == "Absent")
                        {
                            if (reader1["status"].ToString() != "1")
                            {
                                var mark = fun.GetSetting("absent_sms");
                                string mark_template = mark[0].Name;
                                sms = mark_template.Trim()
                                                .Replace("[parent]", reader1["parent"].ToString())
                                                .Replace("[student]", reader1["student"].ToString())
                                                .Replace("[date]", day.ToString());

                                con1.Open();
                                MySqlCommand cmd = new MySqlCommand("INSERT into sms_que(mobile,sms,status) VALUES('" + phone + "','" + sms + "','" + 0 + "');", con1);
                                cmd.ExecuteNonQuery();
                                con1.Close();
                                txtMemoStatus.Text += "\r\n Send SMS To " + phone + " ->  " + sms + " " + count + "/" + mysqlint + "\r\n";

                            }
                        }

                        var progress = new Progress<ProgressReport>();
                        progress.ProgressChanged += (o, report) =>
                        {
                            txtProgressStatus.EditValue = report.PercentComplete;
                            txtProgressStatus.Update();
                        };
                        await fun.ProcessDate(count, mysqlint, progress);
                        count++;
                    }
                }
                con.Close();

            }
            else
                XtraMessageBox.Show("In SMS is Not Enabled please set enable and try again", "Info", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
        private async void BtnSendOutSMS_Click(object sender, EventArgs e)
        {
            txtMemoStatus.Text = "";
            if (txtMonth.Text == "" || txtYear.Text == "" || txtDate.Text == "" || txtClass.EditValue == null || ddlsection.EditValue == null || txtType.Text == "")
            {
                MessageBox.Show("Fill all fields....", "Info");
                return;
            }
            string day = txtYear.Text + "-" + txtMonth.Text + "-" + txtDate.Text;
            var SMS = fun.GetSetting("enable_exit_sms");
            if (SMS[0].Name == "1")
            {
                MySqlConnection con = new MySqlConnection(Login.constring);
                MySqlConnection con1 = new MySqlConnection(Login.constring);
                var sms = "";
                string phone = "";
                object sections = ddlsection.EditValue;


                con.Open();
                var query = "SELECT student.student_id as id, student.name as student, parent.name as parent, parent.phone, attendance.date,attendance.status "
                    + " FROM student INNER JOIN parent ON parent.parent_id = student.parent_id "
                    + " LEFT JOIN attendance ON attendance.student_id = student.student_id AND attendance.date = '" + day + "' "
                    + " WHERE  student.passout = 0 AND student.attendent_sms = 1 AND status=1 and student.section_id in (" + sections + ") ";
                if (txtHosteliesed.Text == "Hostel")
                    query += " AND student.student_id in  (select student_id from hostels_student as hos where  hos.student_id = student.student_id)";
                else if (txtHosteliesed.Text == "Non-Hostel")
                    query += " AND Not student.student_id in  (select student_id from hostels_student as hos where  hos.student_id = student.student_id)";

                var queryCount = "SELECT count(*) "
                    + " FROM student INNER JOIN parent ON parent.parent_id = student.parent_id "
                    + " LEFT JOIN attendance ON attendance.student_id = student.student_id AND attendance.date = '" + day + "' "
                    + " WHERE  student.passout = 0 AND student.attendent_sms = 1 AND status=1  and student.section_id in (" + sections + ") ";
                if (txtHosteliesed.Text == "Hostel")
                    queryCount += " AND student.student_id in  (select student_id from hostels_student as hos where  hos.student_id = student.student_id)";
                else if (txtHosteliesed.Text == "Non-Hostel")
                    queryCount += " AND Not student.student_id in  (select student_id from hostels_student as hos where  hos.student_id = student.student_id)";

                MySqlCommand cmd1 = new MySqlCommand(query, con);
                MySqlCommand cmd2 = new MySqlCommand(queryCount, con);
                int mysqlint = Convert.ToInt32(cmd2.ExecuteScalar());
                MySqlDataReader reader1 = cmd1.ExecuteReader();
                if (reader1.HasRows)
                {
                    int count = 1;
                    txtMemoStatus.Text = "";
                    while (reader1.Read())
                    {

                        phone = reader1["phone"].ToString();
                        var mark = fun.GetSetting("out_sms");
                        string mark_template = mark[0].Name;
                        sms = mark_template.Trim().Replace("[student]", reader1["student"].ToString())
                            .Replace("[date]", day.ToString());
                        txtMemoStatus.Text += "\r\n Send SMS To " + phone + " ->  " + sms + " " + count + "/" + mysqlint + "\r\n";
                        var progress = new Progress<ProgressReport>();
                        progress.ProgressChanged += (o, report) =>
                        {
                            txtProgressStatus.EditValue = report.PercentComplete;
                            txtProgressStatus.Update();
                        };
                        await fun.ProcessDate(count, mysqlint, progress);
                        con1.Open();
                        var query1 = "UPDATE attendance set time_out='" + DateTime.Now.TimeOfDay + "',sync='0'WHERE student_id='" + reader1["id"].ToString() + "' and date='" + day + "';";
                        query1 += "INSERT into sms_que(mobile, sms, status) VALUES('" + phone + "', '" + sms + "', '" + 0 + "')";
                        MySqlCommand cmd = new MySqlCommand(query1, con1);
                        cmd.ExecuteNonQuery();
                        con1.Close();
                        count++;
                    }
                }
                con.Close();
            }
            else
                XtraMessageBox.Show("Exit SMS is Not Enabled please set enable and try again", "Info", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
        void load_classes()
        {
            txtClass.Properties.DataSource = fun.GetAllClasses_dt();
            txtClass.Properties.DisplayMember = "name";
            txtClass.Properties.ValueMember = "class_id";
        }
        void load_sections(string class_id)
        {
            string query = "select section_id,name from section where class_id in (" + class_id + ")";
            ddlsection.Properties.DataSource = fun.FetchDataTable(query);
            ddlsection.Properties.DisplayMember = "name";
            ddlsection.Properties.ValueMember = "section_id";
        }

        private void txtClass_EditValueChanged(object sender, EventArgs e)
        {
            if (txtClass.EditValue != null && !string.IsNullOrEmpty(txtClass.EditValue.ToString()))
                load_sections(txtClass.EditValue.ToString());
        }
    }
}

