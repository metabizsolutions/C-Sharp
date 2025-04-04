using DevExpress.LookAndFeel;
using DevExpress.XtraEditors;
using DevExpress.XtraGrid;
using DevExpress.XtraGrid.Columns;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraReports.UI;
using MySql.Data.MySqlClient;
using SchoolManagementSystem.Attendance;
using SchoolManagementSystem.Exam;
using SchoolManagementSystem.Fees;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

namespace SchoolManagementSystem.Principal
{
    public partial class PDashboard : DevExpress.XtraEditors.XtraUserControl
    {
        private static PDashboard _instance;

        public static PDashboard instance
        {
            get
            {
                if (_instance == null)
                    _instance = new PDashboard();
                return _instance;
            }
        }
        private BackgroundWorker Back_Worker = new BackgroundWorker();
        public static Queue<Notification> notifi = new Queue<Notification>();
        public static Queue<Notification> notifiAdminUser = new Queue<Notification>();
        public static Queue<Notification> notifiAdmin = new Queue<Notification>();

        int count = 0;

        CommonFunctions fun = new CommonFunctions();
        ObservableCollection<Attend> dataStdA = new ObservableCollection<Attend>();
        Stack<Attend> stackStdA = new Stack<Attend>();
        ObservableCollection<Attend> dataStfA = new ObservableCollection<Attend>();
        Stack<Attend> stackStfA = new Stack<Attend>();
        ObservableCollection<Attend> dataStdFee = new ObservableCollection<Attend>();
        int stopStdAttd = 0; int stopStaffAttd = 0;
        int stafAbsentAttd = 0;
        public PDashboard()
        {
            InitializeComponent();
            loadfunctions();
        }
        public void loadfunctions()
        {
            fun.DateFormat(txtTo);
            fun.DateFormat(txtFrom);
            txtTo.DateTime = Convert.ToDateTime("1/1/" + DateTime.Now.ToString("yyyy"));

            loadstudents();
            timer1.Interval = 500; //fun.ConvertMinutesToMilliseconds(Convert.ToInt32(1));
            fun.DateFormat(txtdate);
            pictureEdit2.Image = fun.Base64ToImage(Login.Logo);

            backcolor(Color.Transparent);
            FillGridStudentAttendance();
            FillGridStudentFee();
            FillGridStaffAttendance();
            FillGridMeeting(); FillGridAppoint();
            FillComboAssigined();
            FillComboSendTo();
        }

        public void loadstudents()
        {
            string query = "select student_id,student.name as Name,class.name as Class,section.name as Section from student " +
                            " join class on class.class_id = student.class_id " +
                            " join section on section.section_id = student.section_id " +
                            " where passout =0 ";
            DataTable stdlist = fun.FetchDataTable(query);
            std_ListID.Properties.DataSource = stdlist;
            std_ListID.Properties.DisplayMember = "Name";
            std_ListID.Properties.ValueMember = "student_id";
        }
        private void Visitor_Enter(object sender, EventArgs e)
        {
            FillGridAppoint();

        }
        void backcolor(Color name)
        {
            groupControl1.BackColor = name;
            groupControl2.BackColor = name;
            groupControl3.BackColor = name;
            groupControl4.BackColor = name;
            groupControl5.BackColor = name;
            groupControl6.BackColor = name;

            panelControl1.BackColor = name;
            panelControl2.BackColor = name;
            panel1.BackColor = name;
            //xtraScrollableControl1.BackColor = name;
            //splitContainerControl4.BackColor = name;
        }
        #region Student Attendance
        private void FillGridStudentAttendance()
        {
            var qry = "SELECT ' ' as `key`,COUNT(attendance.student_id) as present,count(*) AS total FROM student inner join class on class.class_id=student.class_id left JOIN attendance ON attendance.student_id= student.student_id and attendance.status= 1 and attendance.date= current_date() where class.time_start<'12:00:00'";
            stdAttd(qry);
            Attend s = new Attend
            {
                key = " ",
                val = qry,
            };
            if (stackStdA.Count == 0)
                stackStdA.Push(s);
        }
        static string sex = "";
        static string typeA_P = "";
        private void tileViewStdA_ItemClick(object sender, DevExpress.XtraGrid.Views.Tile.TileViewItemClickEventArgs e)
        {
            object rowID = tileViewStdA.GetRowCellValue(e.Item.RowHandle, colkey);

            var qry = "";
            if (rowID != null && stopStdAttd == 0)
            {
                if (rowID.ToString() == " ")
                {
                    sex = "";
                    qry = "SELECT student.sex as `key`,COUNT(attendance.student_id) as present,count(*) AS total FROM student inner join class on class.class_id=student.class_id left JOIN attendance ON attendance.student_id= student.student_id and attendance.status= 1 and attendance.date= current_date() where class.time_start<'12:00:00' group by student.sex";
                }
                else if (rowID.ToString() == "Male" || rowID.ToString() == "Female")
                {
                    qry = "SELECT class.name as `key`,COUNT(attendance.student_id) as present,count(*) AS total FROM student inner join class on class.class_id=student.class_id left JOIN attendance ON attendance.student_id= student.student_id and attendance.status= 1 and attendance.date= current_date() where class.time_start<'12:00:00' and student.sex='" + rowID.ToString() + "' group by class.class_id";
                    sex = rowID.ToString();
                }
                else
                {
                    stopStdAttd = 1;
                    qry = "SELECT section.name as `key`,COUNT(attendance.student_id) as present,count(*) AS total FROM student inner join class on class.class_id=student.class_id inner join section on section.section_id=student.section_id left JOIN attendance ON attendance.student_id= student.student_id and attendance.status= 1 and attendance.date= current_date() where class.time_start<'12:00:00' and student.sex='" + sex + "' and student.class_id='" + fun.GetClassIDisSession(rowID.ToString(), Main_FD.SelectedSession) + "' group by section.section_id";
                }
                stdAttd(qry);
                Attend s = new Attend
                {
                    key = rowID.ToString(),
                    val = qry,
                };
                stackStdA.Push(s);
                labelStdAttd.Text = "";
                for (int i = stackStdA.Count - 1; i > -1; i--)
                {
                    labelStdAttd.Text += stackStdA.ToArray()[i].key + "\\";
                }
            }
            else if (stopStdAttd == 1)
            {
                var query = "SELECT student.student_id as `SID`,student.roll as `Roll No`,concat( student.name,' / ',parent.name) as Student, parent.phone as `Father Cell#` , case when attendance.status = '1' then 'Present' when attendance.status = '0' then 'Absent' when attendance.status = '2' then 'Leave' when attendance.status = '3' then 'Short_Leave' end as Status, attendance.time_in as TimeIn,attendance.time_out as TimeOut from student left join class on (class.class_id=student.class_id)  left join attendance on attendance.student_id=student.student_id AND attendance.date=current_date() join parent on parent.parent_id=student.parent_id"
                            + " where student.section_id ='" + fun.GetSectionID(rowID.ToString()) + "'and student.attendent_sms = '1' order by Status, student.roll, student.name";
                DataTable table = SqlFunctions.SqlExecuteDataAdapter(query);
                for (int i = 0; i < table.Rows.Count; i++)
                {
                    var s = table.Rows[i]["Status"].ToString();
                    if (table.Rows[i]["Status"].ToString() == "")
                        table.Rows[i]["Status"] = "Absent";
                }
                gridControl2.DataSource = null;
                gridView2.Columns.Clear();
                gridControl2.DataSource = table;

                var col2 = gridView2.Columns["Father Cell#"];
                col2.Width = 60; var col3 = gridView2.Columns["Student"];
                col3.Width = 80;
                GridColumn Column1 = gridView2.Columns["SID"];
                Column1.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
                GridColumn Column2 = gridView2.Columns["Roll No"];
                Column2.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
                GridColumn Column3 = gridView2.Columns["TimeIn"];
                Column3.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
                GridColumn Column4 = gridView2.Columns["TimeOut"];
                Column4.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
                GridColumn Column5 = gridView2.Columns["Status"];
                Column5.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
                GridColumn Column6 = gridView2.Columns["Father Cell#"];
                Column6.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;

                XtraDaliyAttendanceReport report = new XtraDaliyAttendanceReport();
                Image logo = fun.Base64ToImage(Login.Logo);
                var school = fun.GetSettings("system_title");
                report.PicIogoBox.Image = logo;
                report.LabTitle.Text = school;
                report.LabAddress.Text = fun.GetSettings("address");
                report.GridControl = gridControl2;
                report.LabDate.Text = DateTime.Now.ToShortDateString();

                ReportPrintTool printTool = new ReportPrintTool(report);
                printTool.PreviewForm.PrintingSystem.Document.AutoFitToPagesWidth = 1;
                printTool.ShowRibbonPreviewDialog(UserLookAndFeel.Default);




            }
        }
        private void btnBackStdAttd_Click(object sender, EventArgs e)
        {
            if (stackStdA.Count > 1)
            {
                stackStdA.Pop();
                labelStdAttd.Text = "";
                for (int i = stackStdA.Count - 1; i > -1; i--)
                    labelStdAttd.Text += stackStdA.ToArray()[i].key + "\\";
                stdAttd(stackStdA.Peek().val);
                stopStdAttd = 0;
            }
        }
        void stdAttd(string qry)
        {
            dataStdA.Clear();
            MySqlConnection con = new MySqlConnection(Login.constring);
            con.Open();
            MySqlCommand cmdV = new MySqlCommand(qry, con);
            MySqlDataReader reader1 = cmdV.ExecuteReader();
            if (reader1.HasRows)
            {
                while (reader1.Read())
                {
                    double total = Convert.ToInt32(reader1["total"]);
                    double present = Convert.ToInt32(reader1["present"]);
                    double absent = total - present;
                    total = total > 0 ? total : 1;
                    var val = absent + "/" + total;
                    var Age = Convert.ToInt32((present / total) * 100) + " %";

                    Attend D = new Attend
                    {
                        key = Convert.ToString(reader1["key"]),
                        age = Age,
                        val = val,
                    };
                    dataStdA.Add(D);
                }
            }
            gridStudentAttendance.DataSource = dataStdA;
            gridStudentAttendance.BackColor = Color.Transparent;
            tileViewStdA.Appearance.EmptySpace.BackColor = tileViewStdA.GridControl.Parent.BackColor;

            con.Close();
        }
        #endregion

        #region Student Fee
        private void FillGridStudentFee()
        {
            var qry = "SELECT ' ' as `key`,sum(amount) as total,sum(amount_paid) as paid FROM invoice where month(date)= month(current_date())";
            stdFee(qry);
        }
        void stdFee(string qry)
        {
            dataStdFee.Clear();
            DataTable feedt = fun.FetchDataTable(qry); 
                for (int i=0;i< feedt.Rows.Count; i++)
                {
                    try
                    {
                        double total = feedt.Rows[i]["total"].ToString() == "" ? 0 : Convert.ToInt32(feedt.Rows[i]["total"].ToString());
                        double paid = feedt.Rows[i]["paid"].ToString() == "" ? 0 : Convert.ToInt32(feedt.Rows[i]["paid"].ToString());
                        double due = total - paid;
                        var val = due + "/" + total;
                    double avrage = 0;
                    if (total > 0)
                        avrage = paid / total * 100;
                        var Age = avrage.ToString()+ " %";


                    Attend D = new Attend
                        {
                            key = Convert.ToString(feedt.Rows[i]["key"]),
                            age = Age,
                            val = val,
                        };
                        dataStdFee.Add(D);
                    }
                    catch (Exception)
                    {
                    }

                }
            
            gridStudentFee.DataSource = dataStdFee;
            //gridStudentFee.BackColor = Color.Transparent;
            tileViewStdFee.Appearance.EmptySpace.BackColor = tileViewStdFee.GridControl.Parent.BackColor;
        }
        #endregion

        #region Staff Attendance
        void FillGridStaffAttendance()
        {
            var qry = "SELECT ' ' as `key`,COUNT(attendance.student_id) as present,count(*) AS total FROM teacher left JOIN attendance ON  attendance.student_id = teacher.teacher_id and attendance.status = 1 and attendance.date = current_date()";
            staffAttd(qry);
            Attend s = new Attend
            {
                key = "  ",
                val = qry,
            };
            if (stackStfA.Count == 0)
                stackStfA.Push(s);
        }
        private void tileViewStaffA_ItemClick(object sender, DevExpress.XtraGrid.Views.Tile.TileViewItemClickEventArgs e)
        {
            object rowID = tileViewStaffA.GetRowCellValue(e.Item.RowHandle, colkey1);
            var qry = "";
            if (rowID != null && stopStaffAttd == 0)
            {
                if (rowID.ToString() == " ")
                {
                    qry = "SELECT teacher.staff_type as `key`,COUNT(attendance.student_id) as present,count(*) AS total FROM teacher left JOIN attendance ON  attendance.student_id = teacher.teacher_id and attendance.status = 1 and attendance.date = current_date() group by teacher.staff_type";
                    stopStaffAttd = 1;
                }
                //else if (rowID.ToString() == "Present" || rowID.ToString() == "Absent")
                //{
                //    typeA_P = rowID.ToString();
                //    qry = "SELECT teacher.name as `key`, attendance.time_in as present,'' AS total FROM teacher left JOIN attendance ON  attendance.student_id = teacher.teacher_id  and attendance.date = current_date() where teacher.staff_type = '" + S_type + "' ";
                //    stopStaffAttd = 1;
                //}
                //else if (rowID.ToString() != "")
                //{
                //    stafAbsentAttd = 1;
                //    S_type = rowID.ToString();
                //    qry = "SELECT case when ifnull(attendance.status,0) then 'Present' else 'Absent' end as `key`,COUNT(attendance.student_id) as present,count(*) AS total FROM teacher left JOIN attendance ON  attendance.student_id = teacher.teacher_id and attendance.status = 1 and attendance.date = current_date() where teacher.staff_type='" + rowID + "' group by attendance.status";
                //}
                staffAttd(qry);
                Attend s = new Attend
                {
                    key = rowID.ToString(),
                    val = qry,
                };
                stackStfA.Push(s);
                labelStafAttd.Text = "";
                for (int i = stackStfA.Count - 1; i > -1; i--)
                {
                    labelStafAttd.Text += stackStfA.ToArray()[i].key + "\\";
                }


            }
            else if (stopStaffAttd == 1)
            {
                var query = "SELECT teacher.name as Name ,teacher.phone as `Phone #`,case when attendance.status='1' then 'P' when attendance.status='2' then 'L' else 'A' END as Status,attendance.time_in as TimeIn,attendance.time_out  as TimeOut,round(if(TIME_TO_SEC(TIMEDIFF(attendance.time_in,teacher.timeStart))/60<=0,0,TIME_TO_SEC(TIMEDIFF(attendance.time_in,teacher.timeStart))/60),0) as `L.Time` ,'' as Remarks from teacher  left join attendance on attendance.student_id=teacher.teacher_id AND attendance.date=current_date() where teacher.staff_type='" + rowID + "' order by Status,teacher.name";
                DataTable table = SqlFunctions.SqlExecuteDataAdapter(query);

                gridControl2.DataSource = null;
                gridView2.Columns.Clear();
                gridControl2.DataSource = table;

                var col2 = gridView2.Columns["Phone #"];
                col2.Width = 60; var col3 = gridView2.Columns["Name"];
                col3.Width = 80;
                GridColumn Column2 = gridView2.Columns["L.Time"];
                Column2.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
                GridColumn Column3 = gridView2.Columns["TimeIn"];
                Column3.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
                GridColumn Column4 = gridView2.Columns["TimeOut"];
                Column4.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
                GridColumn Column5 = gridView2.Columns["Status"];
                Column5.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
                GridColumn Column6 = gridView2.Columns["Phone #"];
                Column6.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;

                XtraDaliyAttendanceTeacher report = new XtraDaliyAttendanceTeacher();
                Image logo = fun.Base64ToImage(Login.Logo);
                var school = fun.GetSettings("system_title");
                report.PicIogoBox.Image = logo;
                report.LabTitle.Text = school;
                report.LabAddress.Text = fun.GetSettings("address");
                report.GridControl = gridControl2;
                report.labDate.Text = DateTime.Now.ToShortDateString();

                ReportPrintTool printTool = new ReportPrintTool(report);
                printTool.PreviewForm.PrintingSystem.Document.AutoFitToPagesWidth = 1;
                printTool.ShowRibbonPreviewDialog(UserLookAndFeel.Default);




            }
        }
        private void btnBackStafAttd_Click(object sender, EventArgs e)
        {
            if (stackStfA.Count > 1)
            {
                stackStfA.Pop();
                labelStafAttd.Text = "";
                for (int i = stackStfA.Count - 1; i > -1; i--)
                    labelStafAttd.Text += stackStfA.ToArray()[i].key + "\\";
                staffAttd(stackStfA.Peek().val);
                stopStaffAttd = 0;
            }

        }
        void staffAttd(string qry)
        {
            dataStfA.Clear();
            MySqlConnection con = new MySqlConnection(Login.constring);
            con.Open();
            MySqlCommand cmdV = new MySqlCommand(qry, con);
            MySqlDataReader reader1 = cmdV.ExecuteReader();
            if (reader1.HasRows)
            {
                while (reader1.Read())
                {
                    var val = ""; var Age = "";

                    if (stafAbsentAttd == 1)
                    {
                        double total = Convert.ToInt32(reader1["total"]);
                        val = total.ToString();
                        Age = "-";

                    }
                    else if (typeA_P == "Present")
                    {
                        if (reader1["present"].ToString() == "")
                            continue;
                        Age = "-";
                        val = reader1["present"].ToString();
                    }
                    else if (typeA_P == "Absent")
                    {
                        if (reader1["present"].ToString() != "")
                            continue;
                        Age = "-";
                        val = reader1["present"].ToString();

                    }
                    else
                    {
                        double total = Convert.ToInt32(reader1["total"]);
                        double present = Convert.ToInt32(reader1["present"]);
                        double absent = total - present;
                        val = absent + "/" + total;
                        total = total > 0 ? total : 1;
                        Age = Convert.ToInt32((present / total) * 100) + " %";
                    }
                    Attend D = new Attend
                    {
                        key = Convert.ToString(reader1["key"]),
                        val = val,
                        age = Age
                    };
                    dataStfA.Add(D);
                }
                stafAbsentAttd = 0;
                typeA_P = "";

            }
            gridStaffAttendance.DataSource = dataStfA;
            gridStaffAttendance.BackColor = Color.Transparent;
            tileViewStaffA.Appearance.EmptySpace.BackColor = tileViewStaffA.GridControl.Parent.BackColor;

            con.Close();
        }
        #endregion

        #region Meeting
        ObservableCollection<Meetings> dataMeetings = new ObservableCollection<Meetings>();
        public static int RowSIDM = 0;
        public static int SectionID;
        public static string Issue;
        public static string TypeM;
        public static string SName;
        void FillComboSendTo()
        {
            var sendto = fun.GetSetting("send_to")[0].Name.Split(',');
            foreach (var send in sendto)
            {
                txtSendTo.Properties.Items.Add(send.ToString().Trim());
            }
        }
        void FillGridMeeting()
        {
            var query = "SELECT id as ID,student.name as Student,visitor_name as Visitor,subject as Issue, description as Description,date as Date, waiting_list.`check` as `Check`, student.student_id, student.class_id, student.section_id, waiting_list.type FROM waiting_list left  join student on student.student_id=waiting_list.student_id where waiting_list.date='" + DateTime.Now.ToShortDateString() + "' order by waiting_list.`check`,id";
            FillMeeting(query);
        }
        void FillMeeting(string query)
        {
            dataMeetings.Clear();
            MySqlConnection con = new MySqlConnection(Login.constring);
            con.Open();
            MySqlCommand cmdP = new MySqlCommand(query, con);
            MySqlDataReader reader1 = cmdP.ExecuteReader();
            if (reader1.HasRows)
            {
                while (reader1.Read())
                {
                    var name = reader1["Student"].ToString() == "" ? reader1["Visitor"].ToString() : (reader1["Student"].ToString() + " / " + reader1["Visitor"].ToString());
                    Meetings M = new Meetings
                    {
                        key = name,
                        val = reader1["Issue"].ToString() == "" ? "-" : reader1["Issue"].ToString(),
                        type = reader1["type"].ToString() == "" ? "-" : reader1["type"].ToString(),
                        ID = reader1["student_id"].ToString() == "" ? 0 : int.Parse(reader1["student_id"].ToString()),
                        SectionID = reader1["section_id"].ToString() == "" ? 0 : int.Parse(reader1["section_id"].ToString()),
                        status = reader1["Check"].ToString() == "" ? "0" : reader1["Check"].ToString(),
                    };
                    dataMeetings.Add(M);
                }
            }
            con.Close();
            gridMeeting.DataSource = dataMeetings;

            gridMeeting.BackColor = Color.Transparent;
            tileView1.Appearance.EmptySpace.BackColor = tileView1.GridControl.Parent.BackColor;

        }
        private void btnNewM_Click(object sender, EventArgs e)
        {
            VisitorAppointment app = new VisitorAppointment();
            app.ShowDialog();
            FillGridMeeting();
        }
        private void tileView1_ItemClick(object sender, DevExpress.XtraGrid.Views.Tile.TileViewItemClickEventArgs e)
        {
            RowSIDM = int.Parse(tileView1.GetRowCellValue(e.Item.RowHandle, colSIDM).ToString());
            SectionID = int.Parse(tileView1.GetRowCellValue(e.Item.RowHandle, colSectionIDM).ToString());
            Issue = tileView1.GetRowCellValue(e.Item.RowHandle, colvalM).ToString();
            SName = tileView1.GetRowCellValue(e.Item.RowHandle, colkeyM).ToString();
            TypeM = tileView1.GetRowCellValue(e.Item.RowHandle, coltypeM).ToString();
            status = tileView1.GetRowCellValue(e.Item.RowHandle, colvalM).ToString();
            if (RowSIDM != 0)
                btnReportCardM.Enabled = true;
            else
                btnReportCardM.Enabled = false;

        }
        private void btnSendM_Click(object sender, EventArgs e)
        {
            if (RowSIDM == 0)
            {
                XtraMessageBox.Show("Select record that's you invite!", "Info");
                return;
            }
            Notification n = new Notification
            {
                header = "Please Send..!",
                body = "Mr. " + SName + " for " + Issue
            };
            PDashboard.notifiAdminUser.Enqueue(n);
            PDashboard.notifiAdmin.Enqueue(n);
        }
        private void btn5MintM_Click(object sender, EventArgs e)
        {
            if (RowSIDM == 0)
            {
                XtraMessageBox.Show("Select record that's still waiting!", "Info");
                return;
            }
            Notification n = new Notification
            {
                header = "Please Wait 5 Minutes!",
                body = "Mr. " + SName + " for " + Issue
            };
            PDashboard.notifiAdminUser.Enqueue(n);
            PDashboard.notifiAdmin.Enqueue(n);
        }
        private void btnOther_Click(object sender, EventArgs e)
        {
            if (RowSIDM == 0)
            {
                XtraMessageBox.Show("Select record...!", "Info");
                return;
            }
            if (txtSendTo.Text == "")
            {
                XtraMessageBox.Show("Select Other Person...!", "Info");
                return;
            }
            Notification n = new Notification
            {
                header = "Please Send To " + txtSendTo.Text,
                body = "Mr. " + SName + " for " + Issue
            };
            PDashboard.notifiAdminUser.Enqueue(n);
            PDashboard.notifiAdmin.Enqueue(n);
        }

        private void gridVMeeting_RowUpdated(object sender, DevExpress.XtraGrid.Views.Base.RowObjectEventArgs e)
        {
            //DataRow row = gridVMeeting.GetDataRow(gridVMeeting.FocusedRowHandle);
            //MySqlConnection con = new MySqlConnection(Login.constring);
            //con.Open();
            //var rs = row[9].ToString() == "True" ? '1' : '0';
            //var query = "UPDATE waiting_list set `check`='" + rs + "'WHERE id='" + row[1].ToString() + "' ;";
            //MySqlCommand cmd = new MySqlCommand(query, con);
            //cmd.ExecuteNonQuery();
            //con.Close();

        }
        private void btnViewM_Click(object sender, EventArgs e)
        {
            var query = "SELECT id as ID,visitor_name as Visitor,subject as Issue, description as Description,student.name as Student,student.roll as Roll,class.name as Class,date as Date,waiting_list.`check` as `Check`,student.student_id,student.class_id,student.section_id,waiting_list.type FROM waiting_list left  join student on student.student_id=waiting_list.student_id left join class on class.class_id=student.class_id where waiting_list.date='" + txtdate.DateTime.ToShortDateString() + "' order by waiting_list.`check`,id";
            FillMeeting(query);
        }
        #region Selected Student Result    
        ObservableCollection<AllSubject> allResultSub;
        private void btnReportCardM_Click(object sender, EventArgs e)
        {
            result();
        }
        private void tileView1_ItemDoubleClick(object sender, DevExpress.XtraGrid.Views.Tile.TileViewItemClickEventArgs e)
        {
            result();
        }
        void result()
        {
            if (RowSIDM != 0)
            {
                allResultSub = new ObservableCollection<AllSubject>();
                allResultSub = fun.GetAllSubjectISS(SectionID);

                XtraStdReport rep = fun.std_result_card(RowSIDM, Convert.ToDateTime("1/1/2017"), DateTime.Now, false, false, gridControl2, gridView2, null, null, null,true,false,true);
                
                ReportPrintTool printTool = new ReportPrintTool(rep);
                printTool.PreviewForm.PrintControl.Width = 96;
                printTool.ShowRibbonPreviewDialog(UserLookAndFeel.Default);
            }
            else
                XtraMessageBox.Show("Selected record is not student record");
        }
        private void gridView2_CustomColumnDisplayText(object sender, DevExpress.XtraGrid.Views.Base.CustomColumnDisplayTextEventArgs e)
        {
            if (e.Column.FieldName == "Month")
            {
                var VAL = Convert.ToDateTime(e.Value).ToString("M-yyyy");
                if (VAL == "TOTAL")
                {
                    e.DisplayText = "";

                }
                else if (VAL.Split('-')[0].ToString() == "1")
                    e.DisplayText = "Jan - " + VAL.Split('-')[1].ToString();
                else if (VAL.Split('-')[0].ToString() == "2")
                    e.DisplayText = "Feb - " + VAL.Split('-')[1].ToString();
                else if (VAL.Split('-')[0].ToString() == "3")
                    e.DisplayText = "Mar - " + VAL.Split('-')[1].ToString();
                else if (VAL.Split('-')[0].ToString() == "4")
                    e.DisplayText = "Apr - " + VAL.Split('-')[1].ToString();
                else if (VAL.Split('-')[0].ToString() == "5")
                    e.DisplayText = "May - " + VAL.Split('-')[1].ToString();
                else if (VAL.Split('-')[0].ToString() == "6")
                    e.DisplayText = "June - " + VAL.Split('-')[1].ToString();
                else if (VAL.Split('-')[0].ToString() == "7")
                    e.DisplayText = "July - " + VAL.Split('-')[1].ToString();
                else if (VAL.Split('-')[0].ToString() == "8")
                    e.DisplayText = "Aug - " + VAL.Split('-')[1].ToString();
                else if (VAL.Split('-')[0].ToString() == "9")
                    e.DisplayText = "Sep - " + VAL.Split('-')[1].ToString();
                else if (VAL.Split('-')[0].ToString() == "10")
                    e.DisplayText = "Oct - " + VAL.Split('-')[1].ToString();
                else if (VAL.Split('-')[0].ToString() == "11")
                    e.DisplayText = "Nov - " + VAL.Split('-')[1].ToString();
                else if (VAL.Split('-')[0].ToString() == "12")
                    e.DisplayText = "Dec - " + VAL.Split('-')[1].ToString();
            }
        }
        private void gridView2_RowCellStyle(object sender, RowCellStyleEventArgs e)
        {
            GridView View = sender as GridView;
            if (stopStdAttd == 0 && stopStaffAttd == 0)
            {
                if (e.RowHandle >= 0)
                {
                    if (e.Column.FieldName == "Roll")
                    {
                        return;
                    }
                    string present = View.GetRowCellDisplayText(e.RowHandle, View.Columns["Examination"]);
                    if (present == "Total Marks" || present == "Grand Average")
                    {
                        e.Appearance.ForeColor = Color.Black;
                        e.Appearance.BackColor = Color.Silver;
                        e.Appearance.FontSizeDelta = 1;
                        e.Appearance.Font = new Font("Calibri", 11, style: FontStyle.Bold);
                    }
                    if (present == "Average")
                    {
                        e.Appearance.ForeColor = Color.Black;
                        e.Appearance.BackColor = Color.Silver;
                        e.Appearance.FontSizeDelta = 1;
                        e.Appearance.Font = new Font("Calibri", 10, style: FontStyle.Bold);

                    }
                    for (int i = 0; i < allResultSub.Count; i++)
                    {
                        if (e.Column.FieldName == allResultSub[i].Name)
                        {
                            string val = View.GetRowCellDisplayText(e.RowHandle, View.Columns[allResultSub[i].Name]);
                            if (val.Contains("A"))
                                e.Appearance.Font = new Font("Calibri", 10, style: FontStyle.Bold);
                        }
                    }

                }
                if (e.Column.FieldName == "%")
                {

                    e.Appearance.BackColor = Color.Silver;
                    e.Appearance.ForeColor = Color.Black;

                }
                if (e.Column.FieldName == "Examination" || e.Column.FieldName == "Exam_Date")
                {
                    e.Appearance.BackColor = Color.Transparent;
                    e.Appearance.ForeColor = Color.Black;

                }
            }
        }
        #endregion

        #endregion

        #region Appointment
        ObservableCollection<appoint> dataAppoint = new ObservableCollection<appoint>();
        void FillComboAssigined()
        {
            MySqlConnection con = new MySqlConnection(Login.constring);
            con.Open();
            MySqlCommand cmdP = new MySqlCommand("SELECT CustomField1 as CF FROM appointments group by CustomField1;", con);
            MySqlDataReader reader1 = cmdP.ExecuteReader();
            if (reader1.HasRows)
                while (reader1.Read())
                    txtAssigned.Properties.Items.Add(reader1["CF"].ToString().Trim());
            con.Close();
        }
        public static int edit = 0;
        public static int RowID;
        public static string subject;
        public static string des;
        public static string assigned;
        public static string type;
        public static string status;
        public static DateTime dateS;
        public static DateTime dateE;
        void FillGridAppoint()
        {
            var query = "SELECT ID, Subject, Description, CustomField1 as `Assigned To`, ResourceId as `Work Type`, StartDate, EndDate, Status FROM appointments where Status = '0'; ";
            FillGridAppointment(query);
        }
        void FillGridAppointment(string query)
        {
            dataAppoint.Clear();
            MySqlConnection con = new MySqlConnection(Login.constring);
            con.Open();
            MySqlCommand cmdP = new MySqlCommand(query, con);
            MySqlDataReader reader1 = cmdP.ExecuteReader();
            if (reader1.HasRows)
            {
                while (reader1.Read())
                {
                    var result = "";
                    var type = reader1["Work Type"].ToString();
                    if (type == "1")
                        result = "Urgent";
                    else if (type == "2")
                        result = "Short Term";
                    else if (type == "3")
                        result = "Long Term";
                    else result = "Any";
                    appoint A = new appoint
                    {
                        appoint_id = reader1["ID"].ToString() == "" ? 0 : int.Parse(reader1["ID"].ToString()),
                        key = reader1["Subject"].ToString() == "" ? "-" : reader1["Subject"].ToString(),
                        val = reader1["Assigned To"].ToString() == "" ? "-" : reader1["Assigned To"].ToString(),
                        type = result,
                        des = reader1["Description"].ToString() == "" ? "-" : reader1["Description"].ToString(),
                        dateS = reader1["StartDate"].ToString() == "" ? "-" : reader1["StartDate"].ToString(),
                        dateE = reader1["EndDate"].ToString() == "" ? "-" : reader1["EndDate"].ToString(),
                        status = reader1["Status"].ToString() == "" ? "-" : reader1["Status"].ToString(),
                    };
                    dataAppoint.Add(A);
                }
            }
            con.Close();
            gridAppointment.DataSource = dataAppoint;
            gridAppointment.BackColor = Color.Transparent;
            tileView3.Appearance.EmptySpace.BackColor = tileView3.GridControl.Parent.BackColor;
        }
        private void btnNewAppointment_Click(object sender, EventArgs e)
        {
            Appointments ap = new Appointments(); ap.ShowDialog();
            FillGridAppoint();
        }
        private void btnEdit_Click(object sender, EventArgs e)
        {
            if (RowID == 0)
            {
                XtraMessageBox.Show("Select record that's you want to edit..!", "Info");
                return;
            }
            edit = 1;
            Appointments ap = new Appointments(); ap.ShowDialog();
            FillGridAppoint();
            edit = 0;
        }
        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (RowID != 0)
            {
                if (MessageBox.Show("Are you sure to delete?", "Confirm Delete!!", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    var query = "DELETE FROM `appointments` WHERE `ID`='" + RowID + "';";
                    SqlFunctions.SqlExecuteNonQuery(query);
                }
            }
            else
            {
                XtraMessageBox.Show("Select record that's you want to delete..!", "Info");
                return;
            }
        }
        private void tileView3_ItemClick(object sender, DevExpress.XtraGrid.Views.Tile.TileViewItemClickEventArgs e)
        {
            RowID = int.Parse(tileView3.GetRowCellValue(e.Item.RowHandle, colIDA).ToString());
            subject = tileView3.GetRowCellValue(e.Item.RowHandle, colkeyA).ToString();
            assigned = tileView3.GetRowCellValue(e.Item.RowHandle, colvalA).ToString();
            des = tileView3.GetRowCellValue(e.Item.RowHandle, coldesA).ToString();
            type = tileView3.GetRowCellValue(e.Item.RowHandle, coltypeA).ToString();
            status = tileView3.GetRowCellValue(e.Item.RowHandle, colstatusA).ToString();
            dateS = Convert.ToDateTime(tileView3.GetRowCellValue(e.Item.RowHandle, coldateSA).ToString());
            dateE = Convert.ToDateTime(tileView3.GetRowCellValue(e.Item.RowHandle, coldateEA).ToString());
        }
        private void btnViewA_Click(object sender, EventArgs e)
        {
            var query = "";
            if (txtAssigned.Text == "")
            {
                query = "SELECT ID, Subject, Description, CustomField1 as `Assigned To`, ResourceId as `Work Type`, StartDate, EndDate, Status FROM appointments; ";
                //XtraMessageBox.Show("Select assigned person...!", "Info");
                //return;
            }
            else
                query = "SELECT ID, Subject, Description, CustomField1 as `Assigned To`, ResourceId as `Work Type`, StartDate, EndDate, Status FROM appointments where CustomField1='" + txtAssigned.Text.Trim() + "'; ";
            FillGridAppointment(query);
        }
        private void tileView3_ItemDoubleClick(object sender, DevExpress.XtraGrid.Views.Tile.TileViewItemClickEventArgs e)
        {
            if (RowID == 0)
            {
                XtraMessageBox.Show("Select record that's you want to edit..!", "Info");
                return;
            }
            edit = 1;
            Appointments ap = new Appointments(); ap.ShowDialog();
            FillGridAppoint();
            edit = 0;
        }
        private void btnBackAppoint_Click(object sender, EventArgs e)
        {
            var query = "SELECT ID, Subject, Description, CustomField1 as `Assigned To`, ResourceId as `Work Type`, StartDate, EndDate, Status FROM appointments where Status = '0';; ";
            FillGridAppointment(query);
        }
        #endregion

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (Login.CurrentUserStatus_id == "2")
            {
                count++;
                if (txtdate.DateTime.ToShortDateString() == DateTime.Now.ToShortDateString())
                    FillGridMeeting();
                if (notifi.Count > 0)
                {
                    var n = notifi.Dequeue();
                    toastNotificationsManager1.Notifications[0].Header = n.header;// "Visiter Waiting";
                    toastNotificationsManager1.Notifications[0].Body = n.body;// "Mr.Zeeshan wait outside";
                    Image img = fun.Base64ToImage(Login.Logo);
                    Image im = fun.ScaleImage(img, 115, 115);
                    toastNotificationsManager1.Notifications[0].Image = im;
                    toastNotificationsManager1.ShowNotification(toastNotificationsManager1.Notifications[0]);
                }
                if (notifiAdmin.Count > 0)
                {
                    var n = notifiAdmin.Dequeue();
                    toastNotificationsManager1.Notifications[2].Header = n.header;// "Visiter Waiting";
                    toastNotificationsManager1.Notifications[2].Body = n.body;// "Mr.Zeeshan wait outside";
                    Image img = fun.Base64ToImage(Login.Logo);
                    Image im = fun.ScaleImage(img, 115, 115);
                    toastNotificationsManager1.Notifications[2].Image = im;
                    toastNotificationsManager1.ShowNotification(toastNotificationsManager1.Notifications[2]);
                }
                if (count > 200)
                {
                    count = 0;
                    FillGridStudentAttendance();
                    FillGridStaffAttendance();
                    stopStaffAttd = 0;
                    stopStdAttd = 0;

                }
            }
            else if (Login.CurrentUserStatus_id == "1")
            {
                if (notifiAdminUser.Count > 0)
                {
                    var n = notifiAdminUser.Dequeue();
                    toastNotificationsManager1.Notifications[1].Header = n.header;// "Visiter Waiting";
                    toastNotificationsManager1.Notifications[1].Body = n.body;// "Mr.Zeeshan wait outside";
                    Image img = fun.Base64ToImage(Login.Logo);
                    Image im = fun.ScaleImage(img, 115, 115);
                    toastNotificationsManager1.Notifications[1].Image = im;
                    toastNotificationsManager1.ShowNotification(toastNotificationsManager1.Notifications[1]);
                }
            }
        }

        private void toastNotificationsManager1_Activated(object sender, DevExpress.XtraBars.ToastNotifications.ToastNotificationEventArgs e)
        {
            //switch (e.NotificationID.ToString())
            //{
            //    case "a9aa05dc-9f83-457f-bcc0-00d53e20da8c":
            //        MessageBox.Show("Notification #1 Clicked");
            //        break;
            //    case "66501f90-ac6b-440d-bf73-483c5ab22143":
            //        MessageBox.Show("Notification #2 Clicked");
            //        break;
            //}
        }

        private void toastNotificationsManager1_TimedOut(object sender, DevExpress.XtraBars.ToastNotifications.ToastNotificationEventArgs e)
        {
            switch (e.NotificationID.ToString())
            {
                case "a9aa05dc-9f83-457f-bcc0-00d53e20da8c":
                    toastNotificationsManager1.ShowNotification(e.NotificationID);
                    break;

            }
        }

        private void btnCRShow_Click(object sender, EventArgs e)
        {
            if (std_ListID.Text == "[EditValue is null]")
            {
                MessageBox.Show("Fill all fields....!!", "Info");
                return;
            }
            XtraStdReport rep = StdResult.stdreport(int.Parse(std_ListID.EditValue.ToString()), txtTo.DateTime, txtFrom.DateTime,true,true,true,true);
            GridControl feegc = new GridControl();
            feegc = StudentFeeReport.feedetails(int.Parse(std_ListID.EditValue.ToString()), toggle_feetype.EditValue);
            rep.FeeGridControl = feegc;
            rep.xrlblFee.Visible = true;
            ReportPrintTool printTool = new ReportPrintTool(rep);
            printTool.PreviewForm.PrintingSystem.Document.AutoFitToPagesWidth = 1;
            printTool.ShowRibbonPreviewDialog(UserLookAndFeel.Default);
        }

        private void PDashboard_Load(object sender, EventArgs e)
        {

        }
    }
}

