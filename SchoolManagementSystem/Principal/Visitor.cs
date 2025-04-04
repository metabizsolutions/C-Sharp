using DevExpress.LookAndFeel;
using DevExpress.XtraCharts;
using DevExpress.XtraGrid.Columns;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraReports.UI;
using MySql.Data.MySqlClient;
using SchoolManagementSystem.Exam;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

namespace SchoolManagementSystem.Principal
{
    public partial class Visitor : DevExpress.XtraEditors.XtraUserControl
    {
        private BackgroundWorker Back_Worker = new BackgroundWorker();

        CommonFunctions fun = new CommonFunctions();
        public Visitor()
        {
            InitializeComponent();
            fun.DateFormat(txtdate);

            pictureEdit2.Image = fun.Base64ToImage(Login.Logo);
            FillStaffGrid(); FillGrid();
            timer1.Interval = fun.ConvertMinutesToMilliseconds(Convert.ToInt32(1));

        }


        void FillStaffGrid()
        {
            DateTime Day = DateTime.Now;
            string day = Day.Year + "-" + Day.Month + "-" + Day.Day;
            MySqlConnection con = new MySqlConnection(Login.constring);
            con.Open();
            //  MySqlCommand cmdP = new MySqlCommand("SELECT teacher.name as Teacher ,attendance.time_in as TimeIn,attendance.time_out  as TimeOut,TIME_TO_SEC(TIMEDIFF(attendance.time_in,teacher.timeStart))/60 as `L.Time`  from teacher  inner join attendance on attendance.student_id=teacher.teacher_id AND dayofmonth(attendance.date)='" + DateTime.Now.Day + "' and month(attendance.date)='" + DateTime.Now.Month + "' and year(attendance.date)='" + DateTime.Now.Year + "' order by teacher.name", con);
            MySqlCommand cmdP = new MySqlCommand("SELECT teacher.teacher_id as ID, teacher.name as Teacher,teacher.staff_type as `Staff Type`   from teacher  order by teacher.staff_type,teacher.name", con);
            MySqlDataAdapter adp = new MySqlDataAdapter(cmdP);
            DataTable table = new DataTable();
            adp.Fill(table);
            gridControl1.DataSource = null;
            gridView1.Columns.Clear();
            gridControl1.DataSource = CommonFunctions.AutoNumberedTable(table);
            gridView1.BestFitColumns();
            con.Close();
            var col4 = gridView2.Columns["Sr#"];
            col4.Width = 40;
            GridColumn Column3 = gridView2.Columns["Sr#"];
            Column3.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            GridColumn Column4 = gridView2.Columns["ID"];
            Column4.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;

        }
        private void gridView2_RowClick(object sender, DevExpress.XtraGrid.Views.Grid.RowClickEventArgs e)
        {
            DataRow row = gridView1.GetDataRow(gridView1.FocusedRowHandle);
            int ID = int.Parse(row[1].ToString());
            TeacherProfile(ID);

        }
        void TeacherProfile(int id)
        {
            MessageBox.Show("Under Processing....!!");
        }
        void FillGrid()
        {
            MySqlConnection con = new MySqlConnection(Login.constring);
            con.Open();
            MySqlCommand cmdP = new MySqlCommand("SELECT id as ID,visitor_name as Visitor,subject as Subject, description as Description,student.name as Student,student.roll as Roll,class.name as Class,date as Date,waiting_list.`check` as `Check`,student.student_id,student.class_id,student.section_id FROM waiting_list   join student on student.student_id=waiting_list.student_id  join class on class.class_id=student.class_id where waiting_list.date='" + DateTime.Now.ToShortDateString() + "' order by waiting_list.`check`,id", con);
            MySqlDataAdapter adp = new MySqlDataAdapter(cmdP);
            DataTable table = new DataTable();
            adp.Fill(table);
            gridControl2.DataSource = null;
            gridView1.Columns.Clear();
            gridControl2.DataSource = CommonFunctions.AutoNumberedTable(table);
            gridView1.BestFitColumns();
            var col1 = gridView1.Columns["ID"];
            col1.OptionsColumn.ReadOnly = true;
            col1.Visible = false;
            GridColumn Column3 = gridView1.Columns["Sr#"];
            Column3.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            GridColumn Column4 = gridView1.Columns["Roll"];
            Column4.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            var col2 = gridView1.Columns["student_id"];
            col2.Visible = false;
            var col3 = gridView1.Columns["class_id"];
            col3.Visible = false;
            var col4 = gridView1.Columns["section_id"];
            col4.Visible = false;

        }

        private void btnNewAppoint_Click(object sender, EventArgs e)
        {
            VisitorAppointment app = new VisitorAppointment();
            app.ShowDialog();
            FillGrid();
        }

        private void Visitor_Enter(object sender, EventArgs e)
        {
            FillStaffGrid();
            FillGrid();
        }

        private void gridView1_RowUpdated(object sender, DevExpress.XtraGrid.Views.Base.RowObjectEventArgs e)
        {
            DataRow row = gridView1.GetDataRow(gridView1.FocusedRowHandle);
            MySqlConnection con = new MySqlConnection(Login.constring);
            con.Open();
            var rs = row[9].ToString() == "True" ? '1' : '0';
            var query = "UPDATE waiting_list set `check`='" + rs + "'WHERE id='" + row[1].ToString() + "' ;";
            MySqlCommand cmd = new MySqlCommand(query, con);
            cmd.ExecuteNonQuery();
            con.Close();
        }

        private void gridView1_RowClick(object sender, DevExpress.XtraGrid.Views.Grid.RowClickEventArgs e)
        {
            DataRow row = gridView1.GetDataRow(gridView1.FocusedRowHandle);
            string stdName = row[5].ToString();
            int stdId = int.Parse(row[10].ToString());
            int classId = int.Parse(row[11].ToString());
            int sectionId = int.Parse(row[12].ToString());
            result(stdName, stdId, classId, sectionId);
        }
        DataTable ResultTable;
        ObservableCollection<AllClass> allClass = new ObservableCollection<AllClass>();
        string stdInfo;
        int present = 0;
        int absent = 0;
        int sunday = 0;
        int totaldays = 0;

        public void result(string stdName, int stdID, int classID, int sectionID)
        {
            ResultTable = new DataTable();


            gridControl3.DataSource = null;
            gridView3.Columns.Clear();
            MySqlConnection con = new MySqlConnection(Login.constring);
            allClass = fun.GetAllSubject(sectionID);
            con.Open();
            var queryExamList = "";
            var queryGrandAge = "";
            // if (txtTo.Text == "" && txtFrom.Text == "")
            {
                queryExamList = "SELECT max(name) as name, max(exam.date) as date FROM exam inner join mark on mark.exam_id=exam.exam_id and mark.student_id = '" + stdID + "' group by exam.date order by date;";

                //  queryExamList = "SELECT * FROM exam  order by date;";
                queryGrandAge = "";
            }
            //else
            //{
            //    queryExamList = "SELECT * FROM exam where dayofmonth(exam.date)>='" + txtTo.DateTime.Day + "' and month(exam.date)>='" + txtTo.DateTime.Month + "' and year(exam.date)>='" + txtTo.DateTime.Year + "' and dayofmonth(exam.date)<='" + txtFrom.DateTime.Day + "' and month(exam.date)<='" + txtFrom.DateTime.Month + "' and year(exam.date)<='" + txtFrom.DateTime.Year + "' order by date;";
            //    queryGrandAge = "and dayofmonth(exam.date)>='" + txtTo.DateTime.Day + "' and month(exam.date)>='" + txtTo.DateTime.Month + "' and year(exam.date)>='" + txtTo.DateTime.Year + "' and dayofmonth(exam.date)<='" + txtFrom.DateTime.Day + "' and month(exam.date)<='" + txtFrom.DateTime.Month + "' and year(exam.date)<='" + txtFrom.DateTime.Year + "'";
            //}
            MySqlCommand cmdM = new MySqlCommand(queryExamList, con);
            MySqlDataReader readerM = cmdM.ExecuteReader();
            List<string> MList = new List<string>();
            if (readerM.HasRows)
            {
                DateTime mcount = new DateTime();
                while (readerM.Read())
                {

                    var d = readerM["date"].ToString();
                    DateTime dateM = Convert.ToDateTime(d);
                    if (dateM.Month != mcount.Month)
                    {
                        MList.Add(dateM.ToShortDateString());
                        mcount = dateM;
                    }

                }
            }
            con.Close();
            con.Open();
            if (allClass.Count != 0)
            {

                foreach (var date in MList)
                {
                    /*DATE_FORMAT(exam.date,  '" + "%M -%Y" + "')*/
                    var query = "SELECT student.roll as Roll, exam.name as Examination,exam.date as Exam_Date,CONCAT( month(exam.date),'-',year(exam.date)) as Month,";
                    //   var query2 = "SELECT '' as Roll, 'Subject Wise Total' as Examination,'" + DateTime.Now.ToShortDateString() + "' as Exam_Date,'TOTAL' as Month, ";
                    var query3 = "SELECT '' as Roll, 'Average' as Examination,'" + DateTime.Now.ToString("yyyy-MM-dd") + "' as Exam_Date ,'" + DateTime.Now.ToString("yyyy-MM") + "' as Month,";

                    for (int i = 0; i < allClass.Count; i++)
                    {
                        query += " IFNULL(MAX(CONCAT(CASE mark.subject_id WHEN '" + allClass[i].Salary + "' THEN if(mark_obtained=-1,'A',mark_obtained) END,' / ',CASE mark.subject_id WHEN '" + allClass[i].Salary + "' THEN tbl_mark_subject.marks end)),'/') AS `" + allClass[i].Name + "`,";
                        //      query2 += "concat(CONVERT(sum(CASE mark.subject_id WHEN '" + allClass[i].Salary + "' THEN IF(mark_obtained = '-1', 0, mark_obtained) END), CHAR(45)),' / ',CONVERT( sum(CASE mark.subject_id WHEN '" + allClass[i].Salary + "' THEN tbl_mark_subject.marks END), char(45))) as `" + allClass[i].Name + "`,";
                        query3 += "CONVERT(round(sum(CASE mark.subject_id WHEN '" + allClass[i].Salary + "' THEN IF(mark_obtained = '-1', 0, mark_obtained) END)/sum(CASE mark.subject_id WHEN '" + allClass[i].Salary + "' THEN tbl_mark_subject.marks END)*100,2), char(45)) as `" + allClass[i].Name + "`,";
                    }

                    query += "CONCAT(CONVERT(sum(IF(mark_obtained = '-1', 0, mark_obtained)),char(45)),'/',CONVERT((select sum(marks) from tbl_mark_subject where exam_id=exam.exam_id and subject_id in(select subject_id from subject where section_id = '" + sectionID + "')),char(45)) )as `Total`,round((sum(IF(mark_obtained = '-1', 0, mark_obtained))/(select sum(marks) from tbl_mark_subject where exam_id=exam.exam_id and subject_id in(select subject_id from subject where section_id = '" + sectionID + "'))*100 ),2)as `%`, if(round((sum(mark_obtained)/(select sum(marks) from tbl_mark_subject where exam_id=exam.exam_id and subject_id in(select subject_id from subject where section_id = '" + sectionID + "'))*100 ),2)<=(SELECT mark_upto FROM grade where name='F'),'Fail','Pass') as  Result FROM mark inner join student on(student.student_id = mark.student_id) inner join exam on exam.exam_id=mark.exam_id inner join tbl_mark_subject on tbl_mark_subject.subject_id=mark.subject_id and tbl_mark_subject.exam_id=mark.exam_id  "
                        + "where student.class_id = '" + classID + "'and student.section_id='" + sectionID + "'and student.student_id='" + stdID + "' and month( exam.date)='" + Convert.ToDateTime(date).Month + "'" +
                                "GROUP BY mark.student_id ,exam.exam_id  ";
                    // query2 += " concat(CONVERT(sum(IF(mark_obtained = '-1', 0, mark_obtained)), CHAR(45)),' / ',CONVERT(sum( tbl_mark_subject.marks), CHAR(45))) as Total,'' AS `%`,'' AS Result FROM mark inner join student on(student.student_id = mark.student_id) inner join exam on exam.exam_id=mark.exam_id inner join tbl_mark_subject on tbl_mark_subject.subject_id=mark.subject_id and tbl_mark_subject.exam_id=mark.exam_id where student.class_id = '" + classID + "'and student.section_id='" + sectionID + "'and student.student_id='" + stdID + "'and month( exam.date)='" + Convert.ToDateTime(date).Month + "'";
                    query3 += "CONVERT(round(CONVERT(sum(IF(mark_obtained = '-1', 0, mark_obtained)),char(45))/CONVERT((select sum(marks) from tbl_mark_subject where exam_id=4 and subject_id in(select subject_id from subject where section_id = '41')),char(45))*100,2),char(45))as Total,'' AS `%`,'' AS Result FROM mark inner join student on(student.student_id = mark.student_id) inner join exam on exam.exam_id=mark.exam_id inner join tbl_mark_subject on tbl_mark_subject.subject_id=mark.subject_id and tbl_mark_subject.exam_id=mark.exam_id  "
                       + "where student.class_id = '" + classID + "'and student.section_id='" + sectionID + "'and student.student_id='" + stdID + "'and month( exam.date)='" + Convert.ToDateTime(date).Month + "' order by `Exam_Date`";
                    query +=/* " union all " + query2 + */" union all " + query3;
                    MySqlCommand cmd = new MySqlCommand(query, con);
                    MySqlDataAdapter adp = new MySqlDataAdapter(cmd);
                    DataTable table = new DataTable();

                    adp.Fill(table);

                    for (int row = 0; row <= table.Rows.Count - 1; row++)
                    {
                        string month = table.Rows[0][3].ToString();
                        table.Rows[row][3] = month;
                    }
                    ResultTable.Merge(table);

                }


                // var query4 = "SELECT '' as Roll, 'Total Marks' as Examination,'' as Exam_Date,'TOTAL' as Month, ";
                var query5 = "SELECT '' as Roll, 'Grand Average' as Examination,'' as Exam_Date ,'" + DateTime.Now.ToString("yyyy-MM") + "' as Month,";

                for (int i = 0; i < allClass.Count; i++)
                {
                    //   query4 += "concat(CONVERT(sum(CASE mark.subject_id WHEN '" + allClass[i].Salary + "' THEN IF(mark_obtained = '-1', 0, mark_obtained) END), CHAR(45)),' / ',CONVERT( sum(CASE mark.subject_id WHEN '" + allClass[i].Salary + "' THEN tbl_mark_subject.marks END), char(45))) as `" + allClass[i].Name + "`,";
                    query5 += "CONVERT(round(sum(CASE mark.subject_id WHEN '" + allClass[i].Salary + "' THEN IF(mark_obtained = '-1', 0, mark_obtained) END)/sum(CASE mark.subject_id WHEN '" + allClass[i].Salary + "' THEN tbl_mark_subject.marks END)*100,2), char(45)) as `" + allClass[i].Name + "`,";
                }

                //  query4 += " concat(CONVERT(sum(IF(mark_obtained = '-1', 0, mark_obtained)), CHAR(45)),' / ',CONVERT(sum( tbl_mark_subject.marks), CHAR(45))) as Total,'' AS `%`,'' AS Result FROM mark inner join student on(student.student_id = mark.student_id) inner join exam on exam.exam_id=mark.exam_id inner join tbl_mark_subject on tbl_mark_subject.subject_id=mark.subject_id and tbl_mark_subject.exam_id=mark.exam_id  where student.class_id = '" + classID + "'and student.section_id='" + sectionID + "'and student.student_id='" + stdID + "' " + queryGrandAge + "order by `Exam_Date` desc";
                query5 += "CONVERT(round(sum(IF(mark_obtained = '-1', 0, mark_obtained))/sum( tbl_mark_subject.marks)*100,2), CHAR(45))as Total,'' AS `%`,'' AS Result FROM mark inner join student on(student.student_id = mark.student_id) inner join exam on exam.exam_id=mark.exam_id inner join tbl_mark_subject on tbl_mark_subject.subject_id=mark.subject_id and tbl_mark_subject.exam_id=mark.exam_id  "
                   + "where student.class_id = '" + classID + "'and student.section_id='" + sectionID + "'and student.student_id='" + stdID + "' " + queryGrandAge + " ";
                //  query5 += " union all " + query4;
                MySqlCommand cmdT = new MySqlCommand(query5, con);
                MySqlDataAdapter adpT = new MySqlDataAdapter(cmdT);
                DataTable tableT = new DataTable();

                adpT.Fill(tableT);
                ResultTable.Merge(tableT);
                var rowsToUpdate = ResultTable.AsEnumerable().Where(r => r.Field<string>("Exam_Date") == DateTime.Now.ToString("yyyy-MM-dd"));


                foreach (var rows in rowsToUpdate)
                {
                    rows.SetField("Exam_Date", "");
                }
                gridView1.Columns.Clear();// null;
                gridControl3.DataSource = null;

                DataTable dtCloned = ResultTable.Clone();
                dtCloned.Columns[3].DataType = typeof(DateTime);

                foreach (DataRow row in ResultTable.Rows)
                {
                    dtCloned.ImportRow(row);
                }
                gridControl3.DataSource = dtCloned;

                gridView3.BestFitColumns();
                gridView3.GroupRowHeight = 25;
                var col2 = gridView3.Columns[3];

                col2.Group();
                gridView3.ExpandAllGroups();
                var col1 = gridView3.Columns["Roll"];
                col1.Visible = false;
                var col3 = gridView3.Columns["Result"];
                col3.Visible = false;
                for (int i = 0; i <= (allClass.Count + 3); i++)
                {
                    GridColumn Column = gridView3.Columns[i + 2];
                    Column.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
                }
                con.Close();
                stdInfo = fun.GetStudentInfo(stdID.ToString());
            }


            int sun = 0;
            int total = 0;
            int days = 0;
            var queryAtt = "";
            DateTime dateAtt = new DateTime();
            for (; dateAtt.Month <= DateTime.Now.Month;)
            {
                days += DateTime.DaysInMonth(dateAtt.Year, dateAtt.Month);
                dateAtt = dateAtt.AddMonths(1);
            }
            dateAtt = new DateTime();
            queryAtt += "SELECT  ";
            for (int i = 1; i <= days; i++)
            {
                var day = dateAtt.DayOfWeek;
                if (day.ToString() == "Sunday")
                {
                    sun++;
                }
                dateAtt = dateAtt.AddDays(1);
                total = i;
            }
            total = total - sun;
            queryAtt += "count(dayofmonth(date)) as 'Present'," + total + "-count(dayofmonth(date)) as 'Absent' FROM student left join attendance on (attendance.student_id = student.student_id) and dayofmonth(attendance.date)>='" + dateAtt.Day + "' and month(attendance.date)>='" + dateAtt.Month + "' and year(attendance.date)>='" + dateAtt.Year + "' and dayofmonth(attendance.date)<='" + DateTime.Now.Day + "' and month(attendance.date)<='" + DateTime.Now.Month + "' and year(attendance.date)<='" + DateTime.Now.Year + "'" +
                        " join class on(class.class_id=student.class_id) left join section on section.section_id=student.section_id where student.class_id = '" + classID + "'and student.section_id='" + sectionID + "' and attendance.student_id = '" + stdID + "' ";
            con.Open();
            MySqlCommand cmdAtt = new MySqlCommand(queryAtt, con);
            MySqlDataReader readerAtt = cmdAtt.ExecuteReader();
            if (readerAtt.HasRows)
            {
                while (readerAtt.Read())
                {
                    present = Convert.ToInt32(readerAtt["Present"]);
                    absent = Convert.ToInt32(readerAtt["Absent"]);
                    sunday = sun;
                    totaldays = days;
                }
            }
            con.Close();

            gridControl3.LookAndFeel.Style = DevExpress.LookAndFeel.LookAndFeelStyle.Flat;
            gridControl3.LookAndFeel.UseDefaultLookAndFeel = false; // <<<<<<<<
            gridView3.AppearancePrint.HeaderPanel.Options.UseBackColor = true;
            gridView3.AppearancePrint.HeaderPanel.BackColor = Color.White;
            gridView3.AppearancePrint.GroupRow.Options.UseBackColor = true;
            gridView3.AppearancePrint.GroupRow.BackColor = Color.LightGray;
            gridView3.AppearancePrint.GroupRow.ForeColor = Color.Black;
            gridView3.AppearancePrint.GroupRow.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            var col4 = gridView3.Columns["%"];
            col4.Caption = "%Age";

            print(stdName, stdID, fun.GetClassName(classID), fun.GetSectionName(sectionID));

        }
        void print(string stdName, int stdID, string classN, string section)
        {
            Image logo = fun.Base64ToImage(Login.Logo);
            var school = fun.GetSettings("system_title");
            var std = stdInfo.Split('>');
            XtraStdReport report = new XtraStdReport();
            report.PicIogoBox.Image = logo;
            if (Login.Principal_Sign != "")
                report.picPrincipal_Sign.Image = fun.Base64ToImage(Login.Principal_Sign);
            if (Login.Exam_Sign != "")
                report.PicExam_Sign.Image = fun.Base64ToImage(Login.Exam_Sign);
            report.LabTitle.Text = school;
            report.labName.Text = stdName;
            report.LabFName.Text = std[0];
            report.LabClass.Text = std[4];
            report.LabSection.Text = std[5];
            report.LabSAddress.Text = std[2];
            report.labRoll.Text = std[3];
            //report.LabPhone.Text = std[1];
            report.LabDob.Text = std[6];
            report.LabGender.Text = std[7];
            report.FieldValue1.Text = std[8];
            report.FieldValue2.Text = std[9];
            report.FieldValue3.Text = std[10];
            //report.txtCategory.Text = std[11];
            //report.LabReligion.Text = "Muslim";
            report.GridControl = gridControl3;
            report.GridControl.Width = 100;
            report.LabAddress.Text = fun.GetSettings("address");
            report.LabTel.Text = fun.GetSettings("phone");
            //report.LabTotal.Text = totaldays.ToString();
            //report.LabPresent.Text = present.ToString();
            //report.LabAbsent.Text = absent.ToString();
            //report.LabSLeave.Text = sunday.ToString();
            report.LabPrincipal.Text = "( " + fun.GetSettings("principal") + " )";
            report.exam_controler.Text = "( " + fun.GetSettings("controller_exam") + " )";


            DataTable table = new DataTable("Table1");

            // Add two columns to the table.
            table.Columns.Add("Argument", typeof(Int32));
            table.Columns.Add("Value", typeof(Int32));

            // Add data rows to the table.
            Random rnd = new Random();
            DataRow row = null;
            int j = 1;
            for (int i = 0; i < ResultTable.Rows.Count; i++)
            {
                if (ResultTable.Rows[i]["%"].ToString() != "")
                {
                    var t = ResultTable.Rows[i][11];
                    decimal s = Convert.ToDecimal(t.ToString().Replace('"', '0'));
                    int val = Convert.ToInt32(s);
                    row = table.NewRow();
                    row["Argument"] = j;
                    row["Value"] = val;
                    table.Rows.Add(row);
                    j++;
                }
            }


            report.xrChart1.DataSource = table;
            Series series = new Series("Series1", ViewType.Line);
            report.xrChart1.Series.Add(series);
            // Specify data members to bind the series.

            series.ArgumentScaleType = ScaleType.Numerical;
            series.ArgumentDataMember = "Argument";
            series.ValueScaleType = ScaleType.Numerical;
            series.ValueDataMembers.AddRange(new string[] { "Value" });


            report.xrChart1.Legend.Visibility = DevExpress.Utils.DefaultBoolean.False;

            // documentViewer1.DocumentSource = report;

            // report.CreateDocument();
            // documentViewer1.PrintingSystem.Document.AutoFitToPagesWidth = 1;
            ReportPrintTool printTool = new ReportPrintTool(report);
            printTool.PreviewForm.PrintControl.Width = 96;
            printTool.ShowRibbonPreviewDialog(UserLookAndFeel.Default);

        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {

            MySqlConnection con = new MySqlConnection(Login.constring);
            con.Open();
            MySqlCommand cmdP = new MySqlCommand("SELECT id as ID,visitor_name as Visitor,subject as Subject, description as Description,student.name as Student,student.roll as Roll,class.name as Class,date as Date,waiting_list.`check` as `Check`,student.student_id,student.class_id,student.section_id FROM waiting_list   join student on student.student_id=waiting_list.student_id  join class on class.class_id=student.class_id where waiting_list.date='" + txtdate.DateTime.ToShortDateString() + "' order by waiting_list.`check`,id", con);
            MySqlDataAdapter adp = new MySqlDataAdapter(cmdP);
            DataTable table = new DataTable();
            adp.Fill(table);
            gridControl2.DataSource = null;
            gridView1.Columns.Clear();
            gridControl2.DataSource = CommonFunctions.AutoNumberedTable(table);
            gridView1.BestFitColumns();
            var col1 = gridView1.Columns["ID"];
            col1.OptionsColumn.ReadOnly = true;
            col1.Visible = false;
            GridColumn Column3 = gridView1.Columns["Sr#"];
            Column3.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            GridColumn Column4 = gridView1.Columns["Roll"];
            Column4.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            var col2 = gridView1.Columns["student_id"];
            col2.Visible = false;
            var col3 = gridView1.Columns["class_id"];
            col3.Visible = false;
            var col4 = gridView1.Columns["section_id"];
            col4.Visible = false;



        }

        private void timer1_Tick(object sender, EventArgs e)
        {

            FillStaffGrid(); FillGrid();
        }

        private void gridView3_CustomColumnDisplayText(object sender, DevExpress.XtraGrid.Views.Base.CustomColumnDisplayTextEventArgs e)
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

        private void gridView3_RowCellStyle(object sender, DevExpress.XtraGrid.Views.Grid.RowCellStyleEventArgs e)
        {
            GridView View = sender as GridView;
            if (e.RowHandle >= 0)
            {

                string present = View.GetRowCellDisplayText(e.RowHandle, View.Columns["Examination"]);
                //present == "Subject Wise %age" || present == "Subject Wise Total" || 
                if (present == "Total Marks" || present == "Grand Average")
                {
                    e.Appearance.ForeColor = Color.Black;
                    e.Appearance.BackColor = Color.Silver;
                    e.Appearance.FontSizeDelta = 1;
                    e.Appearance.Font = new Font("Calibri", 10, style: FontStyle.Bold);
                }
                if (present == "Average")
                {
                    e.Appearance.ForeColor = Color.Black;
                    e.Appearance.BackColor = Color.Silver;
                    e.Appearance.FontSizeDelta = 1;
                    e.Appearance.Font = new Font("Calibri", 10, style: FontStyle.Bold);

                }

            }
            if (e.Column.FieldName == "%")
            {

                e.Appearance.BackColor = Color.Silver;
                e.Appearance.ForeColor = Color.Black;

            }
            if (e.Column.FieldName == "Examination" || e.Column.FieldName == "Exam_Date")
            {
                // e.Appearance.Font = new Font("Calibri", 10, style: FontStyle.Bold);
                e.Appearance.BackColor = Color.Transparent;
                e.Appearance.ForeColor = Color.Black;

            }
        }
    }
}

