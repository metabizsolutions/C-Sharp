using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Linq;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace SchoolManagementSystem.Exam
{
    public partial class SendMarksBySMS : DevExpress.XtraEditors.XtraUserControl
    {
        private static SendMarksBySMS _instance;

        public static SendMarksBySMS instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new SendMarksBySMS();
                }

                return _instance;
            }
        }

        private ObservableCollection<AllClass> allClass = new ObservableCollection<AllClass>();
        private ObservableCollection<SendMark> marksend = new ObservableCollection<SendMark>();
        private CommonFunctions fun = new CommonFunctions();
        public SendMarksBySMS()
        {
            InitializeComponent();
            loadfunctions();
        }

        private string subject_wise = "";
        public void loadfunctions()
        {
            exam_list();
            subject_wise = fun.GetSettings("Institute_Type");

            txtClass.Properties.DataSource = fun.GetAllClasses_dt();
            txtClass.Properties.DisplayMember = "name";
            txtClass.Properties.ValueMember = "class_id";
        }

        private void exam_list()

        {
            txtExam.Properties.DataSource = fun.GetAllExams_dt();
            txtExam.Properties.DisplayMember = "name";
            txtExam.Properties.ValueMember = "exam_id";
        }

        private void section_list(string class_id)
        {
            txtSection.Properties.DataSource = fun.GetAllSection_dt(class_id);
            txtSection.Properties.DisplayMember = "name";
            txtSection.Properties.ValueMember = "section_id";
        }
        public void permissions_btns(bool add, bool Edit, bool Delete)
        {
            btnSendSMS.Enabled = false;
            if (add)
            {
                btnSendSMS.Enabled = true;
            }
        }
        private void txtSection_EditValueChanged(object sender, EventArgs e)
        {
            if (txtSection.EditValue != null && !string.IsNullOrEmpty(txtSection.EditValue.ToString()))
            {
                var classID = txtClass.EditValue;
                string query = "SELECT student.student_id,student.name as student,student.phone as phone,class.name as class,parent.phone as Pphone FROM student " +
                    "join parent on parent.parent_id=student.parent_id left join class on (class.class_id=student.class_id) where student.class_id='" + classID + "' and student.section_id in (" + txtSection.EditValue + ") and student.passout = 0 ";
                txtReceiver.Properties.DataSource = fun.FetchDataTable(query);
                txtReceiver.Properties.DisplayMember = "student";
                txtReceiver.Properties.ValueMember = "student_id";

                txtSubject.Properties.DataSource = fun.GetAllSubject_by_section_dt(txtSection.EditValue.ToString());
                txtSubject.Properties.DisplayMember = "name";
                txtSubject.Properties.ValueMember = "subject_id";
            }

        }

        private async void sendmarks_sms()
        {
            var student = txtReceiver.EditValue.ToString();
            int progressPre = 1;
            int Count = 1;
            List<int> subject_list = new List<int>();

            if (txtExam.Text == "" || txtClass.Text == "" || txtSection.Text == "" || txtReceiver.Text == "")
            {
                MessageBox.Show("Fill all fields...!!", "Info");
                return;
            }
            try
            {
                var examID = txtExam.EditValue;//fun.GetExamIDIsSession(txtExam.Text, fun.GetSessionID(fun.GetDefaultSessionName()));
                var classID = txtClass.EditValue;
                var sectionID = txtSection.EditValue;//fun.GetSectionIDisClass(txtSection.Text, classID);
                var conduct_subids = txtSubject.EditValue;
                marksend.Clear();
                txtStatus.Text = "";
                var SMS = fun.GetSetting("exam_sms");
                if (SMS[0].Name == "1")
                {
                    DataTable std_result = fun.multi_classresult_send_mark_by_sms(examID.ToString(), classID.ToString(), sectionID.ToString(), conduct_subids.ToString(),"%Avg",true);
                    DataRow[] dr = std_result.Select("student_id in (" + student + ")");
                    foreach (DataRow read in dr)
                    {
                        SendMark d = new SendMark();

                        d.Stdid = read["student_id"].ToString();
                        d.StdNo = read["Roll_Number"].ToString();
                        d.StdName = read["student"].ToString();
                        d.StdPhone = read["student_phone"].ToString();
                        d.Parent = read["parent"].ToString();
                        d.ParentPhone = read["parent_phone"].ToString();
                        d.classname = read["class"].ToString();
                        d.sectionname = read["section"].ToString();

                        string[] SubID = txtSubject.Text.Split('>');
                        string query_conduct = "SELECT `conducted_on` from tbl_mark_subject WHERE `exam_id` in (" + examID + ") and subject_id in (" + conduct_subids + ") limit 1;";
                        object ConductedDate = fun.Execute_Scaler_string(query_conduct).ToString();
                        if (ConductedDate == null)
                        {
                            d.ExamDate = "";
                        }
                        else
                        {
                            d.ExamDate = ConductedDate.ToString();
                        }

                        string temp = null;
                        string temp2 = null;
                        double total = 0;
                        double Obtained = 0;
                        double age = 0;
                        foreach (DataColumn c in read.Table.Columns)
                        {
                            string col = c.ColumnName;
                            if (col == "Sr#" || col == "student_id" || col == "Roll_Number" || col == "student" || col == "student_phone" || col == "parent" || col == "parent_phone" || col == "class" || col == "section" || col == "Result" || col == "Rank" || col == "Obtained" || col == "Total" || col == "Average") { }
                            else
                            {
                                string[] marks = read[col].ToString().Split('/');
                                string subname = col;
                                var mobt = marks[0].ToString() == "" ? 0 : Convert.ToDouble(marks[0]);
                                var obt = Convert.ToDouble(mobt);
                                Obtained += (obt == -1 || obt == -2) ? 0 : obt;
                                total += Convert.ToInt32(marks[1].ToString() == "" ? 0 : Convert.ToInt32(marks[1]));
                                var test = marks[1].ToString();
                                temp2 = "";
                                if (!string.IsNullOrEmpty(marks[1].ToString()))
                                {
                                    if (txtType.Text.Trim() == "Present" && obt > 0)
                                    {
                                        if (obt == -2)
                                        {
                                            temp2 = "N-A";
                                        }
                                        else
                                        {
                                            temp2 = marks[0].ToString();
                                        }
                                    }
                                    else if (txtType.Text == "Absent" && obt == -1)
                                    {
                                        if (obt == -1)
                                        {
                                            temp2 = "A";
                                        }
                                        else if (obt == -2)
                                        {
                                            temp2 = "N-A";
                                        }
                                    }
                                    else if (txtType.Text == "Both Present and Absent" || string.IsNullOrEmpty(txtType.Text))
                                    {
                                        if (obt == -1)
                                        {
                                            temp2 = "A";
                                        }
                                        else if (obt == -2)
                                        {
                                            temp2 = "N-A";
                                        }
                                        else
                                        {
                                            temp2 = marks[0].ToString();
                                        }
                                    }
                                    if (!string.IsNullOrEmpty(temp2))
                                    {
                                        temp += subname + ": " + temp2 + "/" + marks[1].ToString() + "\r\n";
                                    }
                                }
                            }
                        }
                        age = 100.00 * (Obtained / total);
                        var res = age.ToString();
                        temp += "% :" + Convert.ToDecimal(res == "NaN" ? 0 : age).ToString("F2") + "%";
                        d.Marks = temp;
                        d.Obtained = Convert.ToDouble(read["Obtained"]);
                        d.Total = Convert.ToInt32(read["Total"]);
                        d.Grade = read["Result"].ToString();
                        d.Position = read["Rank"].ToString();
                        marksend.Add(d);
                    }
                    double rs = -1;
                    int i = 0;
                    string mark_template = fun.GetSettings("marks_sms");
                    string[] std_info = student.Split(',');
                    txtStatus.Text = "";
                    Count = 1;
                    for (int j = 0; j < marksend.Count; j++)
                    {
                        if (rs != marksend[j].Obtained)
                        {
                            i++;
                        }

                        rs = marksend[j].Obtained;

                        var a = mark_template.Trim().Replace("[parent]", marksend[j].Parent.ToString())
                             .Replace("[student]", marksend[j].StdName.ToString())
                             .Replace("[roll]", marksend[j].StdNo.ToString())
                             .Replace("[class]", marksend[j].classname)
                             .Replace("[section]", marksend[j].sectionname)
                             .Replace("[exam]", txtExam.Text.ToString())
                             .Replace("[marks]", marksend[j].Marks)
                             .Replace("[grade]", marksend[j].Grade)
                             .Replace("[obtained]", marksend[j].Obtained.ToString())
                             .Replace("[total]", marksend[j].Total.ToString())
                             .Replace("[position]", marksend[j].Position);// i.ToString());
                        if (marksend[j].ExamDate != null)
                        {
                            a = a.Replace("[date]", Convert.ToDateTime(marksend[j].ExamDate.ToString()).ToShortDateString());
                        }
                        else
                        {
                            a = a.Replace("[date]", "");
                        }

                        var temp = "\r\nSend SMS To " + marksend[j].ParentPhone + " ->  " + a + " " + Count + "/" + std_info.Length + "\r\n";
                        string query = "INSERT into sms_que(mobile,sms,status) VALUES('" + marksend[j].ParentPhone + "','" + a + "','" + 0 + "');";
                        if (string.IsNullOrEmpty(marksend[j].Marks))
                        {
                            marksend[j].Marks = "0";
                        }
                        if (txtType.Text.Trim() == "Present" && marksend[j].Obtained > 0)
                        {
                            fun.ExecuteQuery(query);
                            txtStatus.Text += temp;
                            Count++;
                        }
                        else if (txtType.Text == "Absent" && marksend[j].Marks.Contains("Absent"))
                        {
                            fun.ExecuteQuery(query);
                            txtStatus.Text += temp;
                            Count++;
                        }
                        else if (txtType.Text == "Both Present and Absent" || string.IsNullOrEmpty(txtType.Text))
                        {
                            fun.ExecuteQuery(query);
                            txtStatus.Text += temp;
                            Count++;
                        }

                        var progress = new Progress<ProgressReport>();
                        progress.ProgressChanged += (o, report) =>
                        {
                            txtprogressbar.EditValue = report.PercentComplete;
                            txtprogressbar.Update();
                        };
                        await fun.ProcessDate(progressPre, marksend.Count, progress);
                        progressPre++;
                    }

                    //Update Exam SMS date
                    String current_date = fun.CurrentDate();
                    String sms_sent_query = "update tbl_mark_subject set sms_date = '{0}' where exam_id in ({1}) and subject_id in ({2}) and class_id in ({3}) and section_id in ({4}) ";
                    sms_sent_query = string.Format(sms_sent_query, current_date, examID, conduct_subids, classID, sectionID);
                    fun.ExecuteQuery(sms_sent_query);

                }


            }
            catch (Exception ex)
            {
                MessageBox.Show("Selected Class Result Not Available\n\r" + ex.Message, "Info");
            }
        }

        private async void sendmarks_sms_bysubject()
        {
            var student = txtReceiver.EditValue.ToString().Split(',');
            int progressPre = 1;
            int Count = 1;
            List<int> subject_list = new List<int>();

            if (txtExam.EditValue == null || txtClass.EditValue == null || txtSection.EditValue == null || txtReceiver.EditValue == null)
            {
                MessageBox.Show("Fill all fields...!!", "Info");
                return;
            }
            //try
            //{
            var examID = txtExam.EditValue;
            var classID = txtClass.EditValue;
            var sectionID = txtSection.EditValue;
            marksend.Clear();
            txtStatus.Text = "";
            MySqlConnection con = new MySqlConnection(Login.constring);
            MySqlConnection con1 = new MySqlConnection(Login.constring);
            var SMS = fun.GetSetting("exam_sms");
            if (SMS[0].Name == "1")
            {
                allClass.Clear();
                con.Open();
                MySqlCommand cmd1 = new MySqlCommand("SELECT student_id,student.roll as Roll_Number, student.name AS student, student.phone AS student_phone, parent.name AS parent, parent.phone AS parent_phone,class.name as class,section.name as section "
                                                   + " FROM student INNER JOIN parent ON parent.parent_id = student.parent_id "
                                                   + " join class on class.class_id = student.class_id "
                                                   + " join section on section.section_id = student.section_id "
                                                   + " WHERE student.passout = 0 AND student.class_id = '" + classID + "' and student.section_id in (" + sectionID + ") and student.exam_sms = '1'", con);
                MySqlDataReader reader1 = cmd1.ExecuteReader();
                if (reader1.HasRows)
                {
                    while (reader1.Read())
                    {
                        SendMark d = new SendMark();

                        d.Stdid = reader1["student_id"].ToString();
                        d.StdNo = reader1["Roll_Number"].ToString();
                        d.StdName = reader1["student"].ToString();
                        d.StdPhone = reader1["student_phone"].ToString();
                        d.Parent = reader1["parent"].ToString();
                        d.ParentPhone = reader1["parent_phone"].ToString();
                        d.classname = reader1["class"].ToString();
                        d.sectionname = reader1["section"].ToString();
                        //d.ExamID = Exam[0].Name;
                        //d.ExamDate = Exam[0].Salary;

                        //if (!txtSubject.Text.Contains(','))
                        //{
                        //    string[] SubID = txtSubject.Text.Split('>');
                        //    string ConductedDate = fun.Execute_Scaler_string("SELECT `conducted_on` from tbl_mark_subject WHERE `exam_id` = '" + d.ExamID + "' and subject_id = '" + SubID[0] + "';").ToString();
                        //    if (ConductedDate == null || ConductedDate == "")
                        //        d.ExamDate = "";
                        //    else
                        //        d.ExamDate = Convert.ToDateTime(ConductedDate).ToString();

                        //}
                        marksend.Add(d);
                    }
                }
                if (txtSubject.Text != "")
                {
                    con.Close();
                    var a = txtSubject.EditValue.ToString();
                    var info = a.Split(',');

                    con.Open();
                    foreach (string sec in txtSection.EditValue.ToString().Split(','))
                    {
                        if (info.Count() != 0) // in this query selecting student with subject assigened
                        {
                            var query = "SELECT std.student_id as id, std.roll as roll,";
                            for (int i = 0; i < info.Count(); i++)
                            {
                                string subname = txtSubject.Text.Split(',')[i].Trim();

                                query += " MAX(CASE m.subject_id WHEN '" + info[i] + "' THEN (CASE WHEN m.subject_id  = IFNULL((fbst.`subject_id`),0)  THEN mark_obtained else '0' end) END) AS `" + subname + "`,";
                                query += " MAX(CASE m.subject_id WHEN '" + info[i] + "' THEN (CASE WHEN m.subject_id  = IFNULL((fbst.`subject_id`),0)  THEN IF(mark_obtained < 0,0,tms.marks) else '0' end) END) AS `" + subname + "Total" + "`,";
                                subject_list.Add(Convert.ToInt32(info[i]));
                            }
                            query += "sum( CASE when m.subject_id = IFNULL((fbst.`subject_id`),0)  then (tms.marks) else '0' end) as `Total`" +
                                " FROM mark as m " +
                                " join student as std on(std.student_id = m.student_id) " +
                                " INNER JOIN fee_by_subject_teacher AS fbst ON fbst.`student_id` = m.`student_id` AND fbst.`section_id` =m.`section_id`" +
                                " INNER JOIN tbl_mark_subject AS tms ON tms.`class_id` = m.`class_id` AND tms.`section_id` = m.`section_id` AND tms.`subject_id` = m.`subject_id` AND tms.`exam_id` = m.`exam_id`   " +
                                " where m.class_id = '" + classID + "' and m.section_id in (" + sectionID + ") and m.exam_id in (" + examID + ") AND std.passout = 0 " +
                                        "GROUP BY m.student_id ,m.class_id";
                            DataTable result = fun.FetchDataTable(query);
                            foreach (DataRow reader2 in result.Rows)
                            {
                                string temp = null;
                                string temp2 = null;
                                double total = 0;
                                double Obtained = 0;
                                double age = 0;
                                for (int j = 0; j < info.Count(); j++)
                                {
                                    string subname = txtSubject.Text.Split(',')[j].Trim();
                                    var mobt = reader2[subname].ToString() == "" ? 0 : reader2[subname];
                                    var obt = Convert.ToDouble(mobt);
                                    Obtained += (obt == -1 || obt == -2) ? 0 : obt;
                                    total += Convert.ToInt32(reader2[subname + "Total"].ToString() == "" ? 0 : reader2[subname + "Total"]);
                                    var test = reader2[subname + "Total"].ToString();
                                    if (reader2[subname + "Total"].ToString() != "0" && reader2[subname + "Total"].ToString() != "")
                                    {
                                        if (obt == -1)
                                        {
                                            temp2 = "Absent";
                                        }
                                        else if (obt == -2)
                                        {
                                            temp2 = "Not Attempt";
                                        }
                                        else
                                        {
                                            temp2 = reader2[subname].ToString();
                                        }

                                        temp += subname + ": " + temp2 + "/" + reader2[subname + "Total"] + "\r\n";
                                    }
                                }
                                age = 100.00 * (Obtained / total);
                                var res = age.ToString();
                                temp += Convert.ToDecimal(res == "NaN" ? 0 : age).ToString("F2") + "%";

                                if (marksend.Count > 0)
                                {
                                    {
                                        var item = marksend.FirstOrDefault(l => l.Stdid == reader2["id"].ToString());//Convert.ToString(info[i].Split('>')[1])
                                        if (item != null)
                                        {
                                            item.Marks = temp;
                                            item.Obtained = Convert.ToInt32(Obtained);
                                            item.Total = Convert.ToInt32(total);
                                            MySqlCommand cmd3 = new MySqlCommand("select name from grade where '" + age + "' >= mark_from and '" + age + "' <= mark_upto", con1);
                                            con1.Open();
                                            MySqlDataReader reader3 = cmd3.ExecuteReader();
                                            if (reader3.HasRows)
                                            {
                                                while (reader3.Read())
                                                {
                                                    item.Grade = reader3["name"].ToString();
                                                }
                                            }
                                            con1.Close();
                                        }
                                    }
                                }
                                temp = "";
                            }

                        }
                    }
                    con.Close();
                }
                if (subject_list.Count > 0)
                {
                    allClass.Clear();
                    var mark = fun.GetSetting("marks_sms");
                    string mark_template = mark[0].Name;
                    ObservableCollection<SendMark> newlist = new ObservableCollection<SendMark>();
                    newlist = new ObservableCollection<SendMark>(from i in marksend orderby i.Obtained descending select i);
                    if (newlist.Count > 0)
                    {
                        int i = 0;
                        double rs = -1;
                        foreach (var Marks in newlist)
                        {
                            if (rs != Marks.Obtained)
                            {
                                i++;
                            }
                            rs = Marks.Obtained;

                            var a = mark_template.Trim().Replace("[parent]", Marks.Parent.ToString())
                                 .Replace("[student]", Marks.StdName.ToString())
                                 .Replace("[roll]", Marks.StdNo.ToString())
                                 .Replace("[class]", Marks.classname)
                                 .Replace("[section]", Marks.sectionname)
                                 .Replace("[exam]", txtExam.Text.ToString())
                                 .Replace("[marks]", Marks.Marks)
                                 .Replace("[grade]", Marks.Grade)
                                 .Replace("[obtained]", Marks.Obtained.ToString())
                                 .Replace("[total]", Marks.Total.ToString())
                                 .Replace("[position]", i.ToString());
                            //if (!string.IsNullOrEmpty(Marks.ExamDate.ToString()))
                            //    a = a.Replace("[date]", Convert.ToDateTime(Marks.ExamDate.ToString()).ToShortDateString());
                            //else
                            //    a = a.Replace("[date]", "");
                            foreach (var std in student)
                            {
                                if (std == Marks.Stdid && Marks.Total > 0)
                                {
                                    var temp = "\r\nSend SMS To " + Marks.ParentPhone + " ->  " + a + " " + Count + "/" + student.Count() + "\r\n";

                                    string query = "INSERT into sms_que(mobile,sms,status) VALUES('" + Marks.ParentPhone + "','" + a + "','" + 0 + "');";
                                    if (txtType.Text.Trim() == "Present" && Marks.Marks != "-1")
                                    {
                                        fun.ExecuteQuery(query);
                                        txtStatus.Text += temp;
                                        Count++;
                                    }
                                    else if (txtType.Text == "Absent" && Marks.Marks == "-1")
                                    {
                                        fun.ExecuteQuery(query);
                                        txtStatus.Text += temp;
                                        Count++;
                                    }
                                    else if (txtType.Text == "Both Present and Absent" || string.IsNullOrEmpty(txtType.Text))
                                    {
                                        fun.ExecuteQuery(query);
                                        txtStatus.Text += temp;
                                        Count++;
                                    }
                                    //con.Open();
                                    //try
                                    //{
                                    //    MySqlCommand cmd = new MySqlCommand("INSERT into sms_que(mobile,sms,status) VALUES('" + Marks.ParentPhone + "','" + a + "','" + 0 + "');", con);
                                    //    cmd.ExecuteNonQuery();
                                    //}
                                    //catch (MySqlException ex)
                                    //{
                                    //    SystemSounds.Hand.Play();
                                    //    MessageBox.Show(ex.Message, "Error");
                                    //    return;
                                    //}
                                    //con.Close();
                                    //Count++;
                                }
                            }
                            var progress = new Progress<ProgressReport>();
                            progress.ProgressChanged += (o, report) =>
                            {
                                txtprogressbar.EditValue = report.PercentComplete;
                                txtprogressbar.Update();
                            };
                            await fun.ProcessDate(progressPre, marksend.Count, progress);
                            progressPre++;
                        }

                    }

                    //Update Exam SMS date
                    String current_date = fun.CurrentDate();
                    String sms_sent_query = "update tbl_mark_subject set sms_date = '{0}' where exam_id = '{1}' and subject_id in({2})";
                    sms_sent_query = string.Format(sms_sent_query, current_date, examID, String.Join(",", subject_list));
                    fun.ExecuteQuery(sms_sent_query);
                }
            }


            //}
            //catch (Exception ex)
            //{
            //    MessageBox.Show("Selected Class Result Not Available\n\r" + ex.Message, "Info");
            //}
        }
        private void btnSendSMS_Click(object sender, EventArgs e)
        {
            if (txtExam.EditValue == null || txtClass.EditValue == null || txtSection.EditValue == null || txtSubject.EditValue == null)
            {
                MessageBox.Show("Please fill all fields and try again", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            fun.loaderform(() =>
            {
                if (subject_wise == "Subject Wise Institute")
                {
                    sendmarks_sms_bysubject();
                }
                else
                {
                    sendmarks_sms();
                }
            });
        }
        private void SendMarksBySMS_Enter(object sender, EventArgs e)
        {
            loadfunctions();
        }

        private void txtClass_EditValueChanged(object sender, EventArgs e)
        {
            if (txtClass.EditValue != null)
            {
                section_list(txtClass.EditValue.ToString());
            }
        }
    }
}

