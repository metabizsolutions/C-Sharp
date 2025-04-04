using DevExpress.LookAndFeel;
using DevExpress.XtraEditors.Repository;
using DevExpress.XtraGrid;
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
    public partial class DailyAttendance : DevExpress.XtraEditors.XtraUserControl
    {
        private static DailyAttendance _instance;

        public static DailyAttendance instance
        {
            get
            {
                if (_instance == null)
                    _instance = new DailyAttendance();
                return _instance;
            }
        }
        ObservableCollection<AllClass> allClass = new ObservableCollection<AllClass>();
        ObservableCollection<ManageAttendance> ManageAtted = new ObservableCollection<ManageAttendance>();
        CommonFunctions fun = new CommonFunctions();
        public DailyAttendance()
        {
            InitializeComponent();
            loadfunctions();
        }
        public void loadfunctions()
        {
            txtYear.Text = DateTime.Now.Year.ToString();
            txtMonth.Text = DateTime.Now.Month.ToString();
            txtDate.Text = DateTime.Now.Day.ToString();

            txtClass.Properties.DataSource = fun.GetAllClasses_dt();
            txtClass.Properties.DisplayMember = "name";
            txtClass.Properties.ValueMember = "class_id";

            txtVal.Properties.DataSource = fun.Attendance_status();
            txtVal.Properties.DisplayMember = "title";
            txtVal.Properties.ValueMember = "status";

            string str = "";

            studentinfo = fun.GetAllStudentsWId_S_C_S(" where student.passout = 0 ");
            searchLookUpEdit1.Text = "";
            searchLookUpEdit1.Properties.DataSource = studentinfo;
            searchLookUpEdit1.Properties.DisplayMember = "Name";
            searchLookUpEdit1.Properties.ValueMember = "ID";

            //FillAttendance();
        }
        public void permissions_btns(bool add, bool Edit, bool Delete)
        {
            BtnUpdateAttend.Enabled = false;
            btnSave.Enabled = false;
            BtnManageAttendance.Enabled = false;
            btnUpdate.Enabled = false;
            gridView1.OptionsBehavior.Editable = false;
            if (Edit)
            {
                BtnUpdateAttend.Enabled = true;
                btnSave.Enabled = true;
                BtnManageAttendance.Enabled = true;
                btnUpdate.Enabled = true;
                gridView1.OptionsBehavior.Editable = true;
            }
        }
        private void btnDAFind_Click(object sender, EventArgs e)
        {
            FillAttendance();
        }
        public void FillAttendance()
        {
            if (txtMonth.Text == "" || txtYear.Text == "" || txtDate.Text == "")
            {
                MessageBox.Show("Fill all fields....", "Info");
                return;
            }
            var secQuery = "";
            if (txtClass.Text != "")
            {
                var classID = txtClass.EditValue == null ? 0 : txtClass.EditValue;//fun.GetClassIDisSession(txtClass.Text, Main_FD.SelectedSession);
                secQuery += "and student.class_id = '" + classID + "'";

                if (txtSection.Text != "")
                {
                    var sectionID = txtSection.EditValue == null ? 0 : txtSection.EditValue;//fun.GetSectionIDisClass(txtSection.Text,classID);
                    secQuery += "and student.section_id = '" + sectionID + "'";
                }
            }
            if (txtIgnoreDA.Checked == true)
            {
                secQuery += "and student.attendent_sms = '1'";

            }
            gridView1.GroupSummary.Clear();
            string day = txtYear.Text + "-" + txtMonth.Text + "-" + txtDate.Text;
            string month = txtYear.Text + "-" + txtMonth.Text;
            MySqlConnection con = new MySqlConnection(Login.constring);
            con.Open();
            string query = "SELECT DATE,`SID`,`Roll No`,Student,Gender,`Father Cell#`,Class,Section,STATUS, " +
                " TimeIn,TimeOut,total_presents,working_days,FORMAT((total_presents / working_days) * 100, 2) AS percentage, phone2 FROM " +
                " (SELECT class.name_digit,attendance.date AS DATE,student.student_id AS `SID`,student.roll AS `Roll No`, CONCAT( student.name, ' / ', parent.name) AS Student, " +
                " student.sex AS Gender,parent.phone AS `Father Cell#`,parent.phone2 ,class.name AS Class,section.name AS Section, ats.title AS STATUS, " +
                " attendance.time_in AS TimeIn, attendance.time_out AS TimeOut, " +
                " (SELECT COUNT(*) FROM attendance AS att WHERE att.student_id= student.student_id AND att.date BETWEEN '" + month + "-01' AND '" + day + "' AND att.status='1') AS total_presents, " +
                " ((DATEDIFF('" + day + "', DATE_FORMAT(DATE_SUB('" + day + "', INTERVAL DAYOFMONTH('" + day + "') DAY), '%Y-%m-%d'))) - " +
                " ((WEEK('" + day + "') - WEEK(DATE_FORMAT(DATE_SUB('" + day + "', INTERVAL DAYOFMONTH('" + day + "') DAY), '%Y-%m-%d'))) * " +
                " (7 - (CHAR_LENGTH(section.`days`) - CHAR_LENGTH(REPLACE(section.`days`, ',', '')) + 1)))) AS working_days " +
                " FROM student " +
                " LEFT JOIN class ON (class.class_id=student.class_id)  " +
                " LEFT JOIN section ON section.section_id = student.section_id " +
                " LEFT JOIN attendance ON attendance.student_id=student.student_id AND attendance.date= '" + day + "' " +
                " left join attendance_status as ats on ats.status = attendance.status " +
                " JOIN parent ON parent.parent_id= student.parent_id " +
                " WHERE 1 = 1 AND student.passout != 1 " + secQuery + " ORDER BY student.sex,class.name,section.name, student.roll, student.name) AS tb";
            /*
           string query = "SELECT class.name_digit,attendance.date as Date,student.student_id as `SID`,student.roll as `Roll No`,concat( student.name,' / ',parent.name) as Student,student.sex as Gender,parent.phone as `Father Cell#` ,class.name as Class,section.name as Section,case when attendance.status='1' then 'Present' when attendance.status = '0' then 'Absent' when attendance.status = '2' then 'Leave' when attendance.status = '3' then 'Short_Leave' end as Status,attendance.time_in as TimeIn,attendance.time_out as TimeOut from student "
                + "left join class on (class.class_id=student.class_id) left join section on section.section_id = student.section_id"
                + " left join attendance on attendance.student_id=student.student_id"
                + " AND attendance.date='" + day + "' join parent on parent.parent_id=student.parent_id  where 1 = 1 AND student.passout != 1 " + secQuery + " order by student.sex,class.name,section.name, student.roll, student.name";
           */
            MySqlCommand cmdP = new MySqlCommand(query, con);

            MySqlDataAdapter adp = new MySqlDataAdapter(cmdP);
            DataTable table = new DataTable();
            adp.Fill(table);
            for (int i = 0; i < table.Rows.Count; i++)
            {
                var s = table.Rows[i]["Status"].ToString();
                if (table.Rows[i]["Status"].ToString() == "")
                    table.Rows[i]["Status"] = "Absent";
            }
            gridDailyAttendance.DataSource = null;
            gridView1.Columns.Clear();
            gridDailyAttendance.DataSource = CommonFunctions.AutoNumberedTable(table);
            gridView1.BestFitColumns();
            var col0 = gridView1.Columns["Status"];
            //col0.Visible = false;
            var col = gridView1.Columns["Class"];
            col.Group();
            var col2 = gridView1.Columns["Section"];
            col2.Group();
            var col3 = gridView1.Columns["Gender"];
            col3.Group();
            gridView1.ExpandAllGroups();
            GridColumn Column = gridView1.Columns["Sr#"];
            //Column.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            GridColumn Column2 = gridView1.Columns["Roll No"];
            Column2.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            GridColumn Column3 = gridView1.Columns["TimeIn"];
            Column3.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            GridColumn Column4 = gridView1.Columns["TimeOut"];
            Column4.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            //GridColumn Column5 = gridView1.Columns["date"];
            //Column5.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            GridColumn Column6 = gridView1.Columns["Father Cell#"];
            Column6.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;


            gridView1.GroupSummary.Clear();
            GridGroupSummaryItem item = new GridGroupSummaryItem();
            item.FieldName = "Student";
            item.DisplayFormat = "Total={0}";
            item.SummaryType = DevExpress.Data.SummaryItemType.Count;
            gridView1.GroupSummary.Add(item);

            GridGroupSummaryItem item1 = new GridGroupSummaryItem();
            item1.FieldName = "Status";
            item1.Tag = "Present";
            item1.DisplayFormat = "Present={0}";
            item1.SummaryType = DevExpress.Data.SummaryItemType.Custom;
            gridView1.GroupSummary.Add(item1);

            GridGroupSummaryItem item2 = new GridGroupSummaryItem();
            item2.FieldName = "Status";
            item2.Tag = "Absent";
            item2.DisplayFormat = "Absent={0}";
            item2.SummaryType = DevExpress.Data.SummaryItemType.Custom;
            gridView1.GroupSummary.Add(item2);

            GridGroupSummaryItem item3 = new GridGroupSummaryItem();
            item3.FieldName = "Status";
            item3.Tag = "Leave";
            item3.DisplayFormat = "Leave={0}";
            item3.SummaryType = DevExpress.Data.SummaryItemType.Custom;
            gridView1.GroupSummary.Add(item3);

            GridGroupSummaryItem item4 = new GridGroupSummaryItem();
            item4.FieldName = "Status";
            item4.Tag = "Short_Leave";
            item4.DisplayFormat = "Short_Leave={0}";
            item4.SummaryType = DevExpress.Data.SummaryItemType.Custom;
            gridView1.GroupSummary.Add(item4);

            con.Close();
            gridView1.OptionsBehavior.Editable = false;
            gridView1.OptionsBehavior.ReadOnly = true;

            gridView1.Columns["Class"].SortOrder = DevExpress.Data.ColumnSortOrder.Descending;
            //gridView1.Columns["name_digit"].SortOrder = DevExpress.Data.ColumnSortOrder.Ascending;
            //gridView1.Columns["name_digit"].Visible = false;
        }
        private void BtnManageAttendance_Click(object sender, EventArgs e)
        {
            if (txtMonth.Text == "" || txtYear.Text == "" || txtDate.Text == "" || txtClass.Text == "" || txtSection.Text == "")
            {
                MessageBox.Show("Fill all fields....", "Info");
                return;
            }
            updateAttend();
        }
        void updateAttend()
        {
            var classID = txtClass.EditValue == null ? 0 : txtClass.EditValue;
            var sectionID = txtSection.EditValue == null ? 0 : txtSection.EditValue;

            string day = txtYear.Text + "-" + txtMonth.Text + "-" + txtDate.Text;
            string query = "SELECT  attendance_id as Att_ID,student.student_id as ID,student.name as Student,ifnull(attendance.status,0) as Status, student.roll as ROLL "
                + " FROM student "
                + " LEFT JOIN attendance ON attendance.student_id = student.student_id AND attendance.date = '" + day + "' "
                + " WHERE student.passout != 1 AND student.class_id = '" + classID + "'and student.section_id='" + sectionID + "' order by student.roll ";

            gridView1.Columns.Clear();
            gridDailyAttendance.DataSource = fun.FetchDataTable(query);
            gridView1.BestFitColumns();

            RepositoryItemLookUpEdit att_status = new RepositoryItemLookUpEdit();
            att_status.DataSource = fun.Attendance_status();
            att_status.DisplayMember = "title";
            att_status.ValueMember = "status";
            gridView1.Columns["Status"].ColumnEdit = att_status;
            var col = gridView1.Columns["Att_ID"];
            col.OptionsColumn.ReadOnly = true;
            col.Visible = false;
            gridView1.Columns["ROLL"].OptionsColumn.ReadOnly = true;
            gridView1.OptionsBehavior.Editable = true;
            gridView1.OptionsBehavior.ReadOnly = false;
        }
        private void BtnUpdateAttend_Click(object sender, EventArgs e)
        {
            var class_id = txtClass.EditValue == null ? 0 : txtClass.EditValue;
            var section_id = txtSection.EditValue == null ? 0 : txtSection.EditValue;
            string day = txtYear.Text + "-" + txtMonth.Text + "-" + txtDate.Text;
            fun.attendance_holidays_leaves_chack(section_id, day);
            updateAttend();
        }

        private void gridView1_RowUpdated(object sender, DevExpress.XtraGrid.Views.Base.RowObjectEventArgs e)
        {
            MySqlConnection con = new MySqlConnection(Login.constring);
            string day = txtYear.Text + "-" + txtMonth.Text + "-" + txtDate.Text;
            DataRow dr = gridView1.GetDataRow(e.RowHandle);
            var status = dr["Status"].ToString();
            var std_id = dr["ID"].ToString();
            var att_id = dr["Att_ID"].ToString();
            /*
            if (status == "Absent")
                result = "0";
            if (status == "Present")
                result = "1";
            if (status == "Leave")
                result = "2";
            if (status == "Short_Leave")
                result = "3";*/
            var query = "";
            if (att_id != "")
                query = "UPDATE attendance set status='" + status + "',date='" + day + "',time_in='" + DateTime.Now.ToString("HH:mm") + "',sync='0'WHERE student_id='" + std_id + "' and date='" + day + "';";
            else
                query = "INSERT into attendance(status,student_id,date,time_in,time_out,sync) VALUES('" + status + "','" + std_id + "','" + day + "','" + DateTime.Now.ToString("HH:mm") + "','','0')";
            con.Open();
            MySqlCommand cmd = new MySqlCommand(query, con);
            cmd.ExecuteNonQuery();
            con.Close();



            //if (ManageAtted.Count > 0)
            //{
            //    for (int i = 0; i < ManageAtted.Count; i++)
            //    {
            //        var item = ManageAtted.FirstOrDefault(j => j.ID == Convert.ToString(ManageAtted[i].ID));
            //        if (item != null)
            //        {
            //            var result = "";
            //            if (ManageAtted[i].Status == "Absent")
            //                result = "0";
            //            if (ManageAtted[i].Status == "Present")
            //                result = "1";
            //            if (ManageAtted[i].Status == "Leave")
            //                result = "2";
            //            var query = "";
            //            if (ManageAtted[i].Att_ID != "")
            //                query = "UPDATE attendance set status='" + result + "',date='" + day + "',time_in='" + DateTime.Now.ToString("HH:mm") + "',sync='0'WHERE student_id='" + ManageAtted[i].ID + "' and date='" + day + "';";
            //            else
            //                query = "INSERT into attendance(status,student_id,date,time_in,time_out,sync) VALUES('" + result + "','" + ManageAtted[i].ID + "','" + day + "','" + DateTime.Now.ToString("HH:mm") + "','','0')";
            //            con.Open();
            //            MySqlCommand cmd = new MySqlCommand(query, con);
            //            cmd.ExecuteNonQuery();
            //            con.Close();

            //        }
            //    }
            //}
        }
        int validRowCount = 0;
        private void gridView1_CustomSummaryCalculate(object sender, DevExpress.Data.CustomSummaryEventArgs e)
        {
            GridGroupSummaryItem item = e.Item as GridGroupSummaryItem;
            GridView view = sender as GridView;
            if (Equals("Present", item.Tag))
            {
                if (e.SummaryProcess == DevExpress.Data.CustomSummaryProcess.Start)
                {
                    validRowCount = 0;
                }
                if (e.SummaryProcess == DevExpress.Data.CustomSummaryProcess.Calculate)
                {
                    var a = view.GetRowCellValue(e.RowHandle, "Status").ToString();
                    if (a == "Present")
                        validRowCount++;
                }
                if (e.SummaryProcess == DevExpress.Data.CustomSummaryProcess.Finalize)
                    e.TotalValue = validRowCount;
            }
            if (Equals("Absent", item.Tag))
            {
                if (e.SummaryProcess == DevExpress.Data.CustomSummaryProcess.Start)
                {
                    validRowCount = 0;
                }
                if (e.SummaryProcess == DevExpress.Data.CustomSummaryProcess.Calculate)
                {
                    var a = view.GetRowCellValue(e.RowHandle, "Status").ToString();
                    if (a == "Absent")
                        validRowCount++;
                }
                if (e.SummaryProcess == DevExpress.Data.CustomSummaryProcess.Finalize)
                    e.TotalValue = validRowCount;
            }
            if (Equals("Leave", item.Tag))
            {
                if (e.SummaryProcess == DevExpress.Data.CustomSummaryProcess.Start)
                {
                    validRowCount = 0;
                }
                if (e.SummaryProcess == DevExpress.Data.CustomSummaryProcess.Calculate)
                {
                    var a = view.GetRowCellValue(e.RowHandle, "Status").ToString();
                    if (a == "Leave")
                        validRowCount++;
                }
                if (e.SummaryProcess == DevExpress.Data.CustomSummaryProcess.Finalize)
                    e.TotalValue = validRowCount;
            }
            if (Equals("Short_Leave", item.Tag))
            {
                if (e.SummaryProcess == DevExpress.Data.CustomSummaryProcess.Start)
                {
                    validRowCount = 0;
                }
                if (e.SummaryProcess == DevExpress.Data.CustomSummaryProcess.Calculate)
                {
                    var a = view.GetRowCellValue(e.RowHandle, "Status").ToString();
                    if (a == "Short_Leave")
                        validRowCount++;
                }
                if (e.SummaryProcess == DevExpress.Data.CustomSummaryProcess.Finalize)
                    e.TotalValue = validRowCount;
            }
        }

        private void BtnPrint_Click(object sender, EventArgs e)
        {
            showReport();
        }
        void showReport()
        {
            XtraDaliyAttendanceReport report = new XtraDaliyAttendanceReport();
            Image logo = fun.Base64ToImage(Login.Logo);
            var school = fun.GetSettings("system_title");
            report.PicIogoBox.Image = logo;
            report.LabTitle.Text = school;
            report.LabAddress.Text = fun.GetSettings("address");
            //report.labExam.Text = txtExam.Text;
            //  report.labClass.Text = txtClass.Text;
            // report.LabSection.Text = txtSection.Text;
            report.GridControl = gridDailyAttendance;
            report.LabDate.Text = DateTime.Now.ToShortDateString();

            ReportPrintTool printTool = new ReportPrintTool(report);
            printTool.PreviewForm.PrintingSystem.Document.AutoFitToPagesWidth = 1;
            printTool.ShowRibbonPreviewDialog(UserLookAndFeel.Default);
        }
        ObservableCollection<StudentInfo> studentinfo = new ObservableCollection<StudentInfo>();
        private void DailyAttendance_Enter(object sender, EventArgs e)
        {

        }

        private void txtClass_SelectedIndexChanged(object sender, EventArgs e)
        {

            /* allClass.Clear();
             allClass = fun.GetAllSectionisClass(txtClass.Text);
             foreach (var allclass in allClass)
                 txtSection.Properties.Items.Add(allclass.Name);*/
        }

        private void gridView1_RowCellStyle(object sender, RowCellStyleEventArgs e)
        {
            //GridView View = sender as GridView;
            //if (e.Column.FieldName == "Date")
            //{
            //    string present = View.GetRowCellDisplayText(e.RowHandle, View.Columns["Date"]);
            //    if (present != "")
            //    {
            //        e.Appearance.BackColor = Color.DeepSkyBlue;
            //        e.Appearance.BackColor2 = Color.LightCyan;
            //    }
            //}
        }

        private void gridView1_RowStyle(object sender, RowStyleEventArgs e)
        {
            GridView View = sender as GridView;
            if (e.RowHandle >= 0 && View.Columns["Status"] != null)
            {

                string present = View.GetRowCellDisplayText(e.RowHandle, View.Columns["Status"]);
                if (present == "Present")
                    e.Appearance.ForeColor = Color.Green;
                if (present == "Absent")
                    e.Appearance.ForeColor = Color.OrangeRed;
                if (present == "Leave")
                    e.Appearance.ForeColor = Color.Blue;
                if (present == "Short_Leave")
                    e.Appearance.ForeColor = Color.Orange;
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (txtMonth.Text == "" || txtYear.Text == "" || txtDate.Text == "" || string.IsNullOrEmpty(searchLookUpEdit1.EditValue.ToString()) || string.IsNullOrEmpty(txtVal.EditValue.ToString()))
            {
                MessageBox.Show("Fill all fields....", "Info");
                return;
            }
            string day = txtYear.Text + "-" + txtMonth.Text + "-" + txtDate.Text;
            var query = ""; var result = txtVal.EditValue.ToString();
            int alreadyExit = 0;


            MySqlConnection con = new MySqlConnection(Login.constring);
            con.Open();
            query = "select * from attendance WHERE student_id='" + int.Parse(searchLookUpEdit1.EditValue.ToString()) + "' and date='" + day + "'";
            MySqlCommand cmd = new MySqlCommand(query, con);
            MySqlDataReader reader1 = cmd.ExecuteReader();
            if (reader1.HasRows)
                alreadyExit = 1;
            con.Close();
            if (alreadyExit == 1)
                query = "UPDATE attendance set status='" + result + "',time_in='" + DateTime.Now.ToString("HH:mm") + "' ,sync='0'WHERE student_id='" + int.Parse(searchLookUpEdit1.EditValue.ToString()) + "' and date='" + day + "';";
            else
                query = "INSERT into attendance(status,student_id,date,time_in,time_out,sync) VALUES('" + result + "','" + int.Parse(searchLookUpEdit1.EditValue.ToString()) + "','" + day + "','" + DateTime.Now.ToString("HH:mm") + "','','0')";
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
            con.Open(); var result = txtVal.EditValue;
            var query = "UPDATE attendance set status='" + result + "',time_in='" + DateTime.Now.ToString("HH:mm") + "' ,sync='0'WHERE student_id='" + int.Parse(searchLookUpEdit1.EditValue.ToString()) + "' and date='" + day + "';";
            MySqlCommand cmd = new MySqlCommand(query, con);
            cmd.ExecuteNonQuery();
            con.Close();
            FillAttendance();
        }

        private void btnFindAll_Click(object sender, EventArgs e)
        {
            fun.loaderform(() => FillAttendance());
        }

        private void btnSummary_Click(object sender, EventArgs e)
        {
            if (txtMonth.Text == "" || txtYear.Text == "" || txtDate.Text == "")
            {
                MessageBox.Show("Year,Month and Date are required fields!", "Info");
                return;
            }



            gridView1.GroupSummary.Clear();
            string day = txtYear.Text + "-" + txtMonth.Text + "-" + txtDate.Text;
            String query = "SELECT class.name_digit,concat(class.name_digit,' ',class.name) AS class,section.name AS section,student.sex AS gender, " +
                            " COUNT(student.student_id) AS total, SUM(IF(attendance.status = 1, 1, 0)) AS present, SUM(IF(attendance.status IS NULL, 1, 0)) AS absent," +
                            " SUM(IF(attendance.status = 2 OR attendance.status = 3, 1, 0)) AS `leave`, FLOOR(100 * SUM(IF(attendance.status = 1, 1, 0)) / COUNT(student.student_id)) AS `present%` " +
                            " FROM student " +
                            " LEFT JOIN class ON class.class_id=student.class_id " +
                            " LEFT JOIN section ON section.section_id = student.section_id " +
                            " LEFT JOIN attendance ON attendance.student_id= student.student_id AND attendance.date= '" + day + "' " +
                            " WHERE 1 = 1 AND student.passout =0  " +
                            " GROUP BY section.section_id, student.sex " +
                            " ORDER BY class.name ASC";
            DataTable table = fun.FetchDataTable(query);

            gridDailyAttendance.DataSource = null;
            gridView1.Columns.Clear();
            gridDailyAttendance.DataSource = CommonFunctions.AutoNumberedTable(table);
            gridView1.BestFitColumns();


            gridView1.GroupSummary.Add(DevExpress.Data.SummaryItemType.Sum, "present", gridView1.Columns["present"]);
            gridView1.GroupSummary.Add(DevExpress.Data.SummaryItemType.Sum, "absent", gridView1.Columns["absent"]);
            gridView1.GroupSummary.Add(DevExpress.Data.SummaryItemType.Sum, "leave", gridView1.Columns["leave"]);
            gridView1.GroupSummary.Add(DevExpress.Data.SummaryItemType.Average, "present%", gridView1.Columns["present%"]);

            //gridView1.Columns["class"].OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.False;
            gridView1.Columns["class"].FieldNameSortGroup = "name_digit";
            gridView1.Columns["class"].Group();
            gridView1.Columns["name_digit"].Visible = false;


            gridView1.ExpandAllGroups();



        }

        private void txtClass_EditValueChanged(object sender, EventArgs e)
        {
            if (txtClass.EditValue != null)
            {
                txtSection.Properties.DataSource = fun.GetAllSection_dt(txtClass.EditValue.ToString());
                txtSection.Properties.DisplayMember = "name";
                txtSection.Properties.ValueMember = "section_id";
            }
        }
    }
}
