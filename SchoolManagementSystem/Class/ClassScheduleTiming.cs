using DevExpress.XtraEditors.Repository;
using DevExpress.XtraGrid.Columns;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraGrid.Views.Grid.ViewInfo;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Media;
using System.Windows.Forms;

namespace SchoolManagementSystem.Class
{
    public partial class ClassScheduleTiming : DevExpress.XtraEditors.XtraUserControl
    {
        CommonFunctions fun = new CommonFunctions();
        ObservableCollection<AllClass> allClass = new ObservableCollection<AllClass>();
        int time_duration;

        string Time_Start;
        string Time_End;
        public ClassScheduleTiming()
        {
            InitializeComponent();
            allClass.Clear();
            allClass = fun.GetClassTiming();
            foreach (var allclass in allClass)
            {
                Time_Start = allclass.Name;
                Time_End = allclass.Salary;
            }

            time_duration = Convert.ToInt32(fun.GetSettings("time_duration"));
            fillTimeTable();


        }
        MySqlConnection con;
        MySqlCommand cmd1;
        MySqlDataReader reader1;
        public enum DayOfWeek
        {
            Monday = 1,
            Tuesday = 2,
            Wednesday = 3,
            Thursday = 4,
            Friday = 5,
            Saturday = 6,
        }
        IList<Tslats> SlatTeacher;
        IList<Sslats> SlatSection;
     //   IList<CalCreditHours> WorkLoad;
        IList<TimeTable> timeTable;
        IList<TeacherLLimit> TeacherLimit;
        IList<CreditHours> HoursListItem;


        // check teacher first in this block of code      
        //private void btnGenerate_Click(object sender, EventArgs e)
        //{
        //    simpleButton2.Enabled = true;
        //    con = new MySqlConnection(Login.constring);
        //    con.Open();
        //    MySqlCommand cmd = new MySqlCommand("TRUNCATE time_table", con);
        //    cmd.ExecuteNonQuery();
        //    con.Close();
        //    SlatSection = new List<Sslats>();
        //    SlatSection = getSectionTimeSlat();
        //    SlatTeacher = new List<Tslats>();
        //    //  SlatTeacher = getTeacherTimeSlat();
        //    SlatTeacher = getTeacherTimeSlat2();
        //    var ssc = SectionAvailableSlats(38);
        //    //var cs = ClassAvailableSlats(3);
        //    var cs = ClassAvailableSlats();
        //    //var subject = ClassSubject(3);
        //    var subject = ClassSubject();
        //    var ts = TeacherAvailableSlats();
        //    WorkLoad = new List<CalCreditHours>();
        //    WorkLoad = getCalWorkLoad();
        //    HoursListItem = new List<CreditHours>();
        //    HoursListItem = getSSWorkLoad();
        //    TeacherLimit = new List<TeacherLLimit>();
        //    TeacherLimit = getLlimit();

        //    timeTable = new List<TimeTable>();
        //    var ss = cs.Where(a => a.Cid == "1" || a.Cid == "3" || a.Cid == "5");

        //    var vvvv = ss.Count();
        //    var HoursList = HoursListItem.Where(a => a.Classid == 1 || a.Classid == 3 || a.Classid == 5);
        //    int n = 0;
        //    A:
        //    foreach (var s in ss.Where(a => a.Used == null))
        //    {
        //        var vv = ss.Where(a => a.Used == null).Count();
        //        if (vv < 10)
        //        {

        //        }
        //        foreach (var T in ts.Where(t => t.Slats == s.Slats && t.Day == s.SDay && t.Used == null))
        //        {
        //            foreach (var TL in TeacherLimit.Where(t => t.Llimit > 0 && t.ID == int.Parse(T.Teacherid)))
        //            {
        //                foreach (var sub in subject)
        //                {
        //                    foreach (var WL in HoursList.Where(t => t.SubjectCode == sub.Code && t.Sectionid == s.Sid && t.WorkLoad > 0))
        //                    {
        //                        if (s.Used != "1")
        //                            if (T.Used != "1")
        //                                if (TL.Llimit > 0)
        //                                    if (T.SubjectCode == sub.Code)
        //                                        if (WL.SubjectCode == sub.Code && WL.WorkLoad > 0)
        //                                            if (T.Day == s.SDay)
        //                                                if (T.Slats == s.Slats)
        //                                                {
        //                                                    if (s.SDay == "3" || s.SDay == "4")
        //                                                    {
        //                                                        if (s.SlatsVal == 3)
        //                                                        {
        //                                                            TimeTable dd = new TimeTable
        //                                                            {
        //                                                                Classid = s.Cid,
        //                                                                ClassName = s.CName,
        //                                                                Sectionid = s.Sid,
        //                                                                SectionName = s.SName,
        //                                                                SubjectCode = "",
        //                                                                Day = s.SDay,
        //                                                                Slat = s.Slats,
        //                                                                Teacherid = "",
        //                                                                TeacherName = "",
        //                                                                SlatVal = s.SlatsVal,
        //                                                                DayName = s.SDayName
        //                                                            };
        //                                                            timeTable.Add(dd);
        //                                                            s.Used = "1";
        //                                                            n++; goto A;

        //                                                        }
        //                                                        if (s.SlatsVal == 6)
        //                                                        {
        //                                                            TimeTable dd = new TimeTable
        //                                                            {
        //                                                                Classid = s.Cid,
        //                                                                ClassName = s.CName,
        //                                                                Sectionid = s.Sid,
        //                                                                SectionName = s.SName,
        //                                                                SubjectCode = "",
        //                                                                Day = s.SDay,
        //                                                                Slat = s.Slats,
        //                                                                Teacherid = "",
        //                                                                TeacherName = "",
        //                                                                SlatVal = s.SlatsVal,
        //                                                                DayName = s.SDayName
        //                                                            };
        //                                                            timeTable.Add(dd);
        //                                                            s.Used = "1";
        //                                                            n++; goto A;

        //                                                        }
        //                                                    }
        //                                                    else
        //                                                    {
        //                                                        if (s.SlatsVal == 5)
        //                                                        {
        //                                                            TimeTable dd = new TimeTable
        //                                                            {
        //                                                                Classid = s.Cid,
        //                                                                ClassName = s.CName,
        //                                                                Sectionid = s.Sid,
        //                                                                SectionName = s.SName,
        //                                                                SubjectCode = "",
        //                                                                Day = s.SDay,
        //                                                                Slat = s.Slats,
        //                                                                Teacherid = "",
        //                                                                TeacherName = "",
        //                                                                SlatVal = s.SlatsVal,
        //                                                                DayName = s.SDayName
        //                                                            };
        //                                                            timeTable.Add(dd);

        //                                                            s.Used = "1";
        //                                                            n++; goto A;

        //                                                        }
        //                                                    }
        //                                                    TimeTable d = new TimeTable
        //                                                    {
        //                                                        Classid = s.Cid,
        //                                                        ClassName = s.CName,
        //                                                        Sectionid = s.Sid,
        //                                                        SectionName = s.SName,
        //                                                        SubjectCode = sub.Code,
        //                                                        Day = s.SDay,
        //                                                        Slat = s.Slats,
        //                                                        Teacherid = T.Teacherid,
        //                                                        TeacherName = T.TeacherName,
        //                                                        SlatVal = s.SlatsVal,
        //                                                        DayName = s.SDayName
        //                                                    };
        //                                                    timeTable.Add(d);
        //                                                    WL.WorkLoad = WL.WorkLoad - 1;
        //                                                    TL.Llimit = TL.Llimit - 1;
        //                                                    T.Used = "1";
        //                                                    s.Used = "1";
        //                                                    n++;
        //                                                    //sub.index = subject.Count() + 1;
        //                                                    //foreach (var su in subject)
        //                                                    //{
        //                                                    //    if (su.index != subject.Count + 1)
        //                                                    //        su.index = su.index - 1;
        //                                                    //}
        //                                                    //if (sub.index == subject.Count + 1)
        //                                                    //    sub.index = subject.Count;
        //                                                    goto A;
        //                                                }
        //                    }
        //                }
        //            }
        //        }

        //    }
        //    var ttt = from c in cs
        //              where c.Cid == "1" || c.Cid == "2" || c.Cid == "3" && c.Used == null
        //              select c;
        //    var v = ttt.Count();
        //    DataTable table = new DataTable();
        //    DataColumn[] cols ={
        //              new DataColumn("ClassID",typeof(String)),
        //              new DataColumn("SectionID",typeof(String)),
        //              new DataColumn("Class",typeof(String)),
        //              new DataColumn("Section",typeof(String)),
        //              new DataColumn("Day",typeof(String)),
        //              new DataColumn("Days",typeof(String)),
        //           };
        //    table.Columns.AddRange(cols);
        //    string time = Time_Start;
        //    TimeSpan tss = TimeSpan.Parse(time);
        //    DateTime timeS = DateTime.Today.Add(tss);
        //    time = Time_End;
        //    tss = TimeSpan.Parse(time);
        //    DateTime timeE = DateTime.Today.Add(tss);
        //    timeE = timeE.AddHours(12);
        //    for (; timeS < timeE;)
        //    {
        //        DataColumn[] colsa ={
        //              new DataColumn(timeS.ToShortTimeString() + "-" + timeS.AddMinutes(time_duration).ToShortTimeString(),typeof(String))
        //        };
        //        table.Columns.AddRange(colsa);
        //        timeS = timeS.AddMinutes(time_duration);
        //    }

        //    DataRow row = table.NewRow();
        //    foreach (DayOfWeek day in Enum.GetValues(typeof(DayOfWeek)))
        //    {
        //        var TTres = from tt in timeTable
        //                    where tt.Day == day.GetHashCode().ToString()
        //                    orderby tt.SlatVal
        //                    select tt;

        //        foreach (var val in TTres)
        //        {
        //            int hasVal = 0;
        //            var res = table.AsEnumerable().Where(r => r.Field<string>("Day") == val.Day
        //                                                     && r.Field<string>("ClassID") == val.Classid && r.Field<string>("SectionID") == val.Sectionid);

        //            foreach (var re in res)
        //            {
        //                hasVal = 1;
        //            }
        //            if (hasVal == 0)
        //            {
        //                row = table.NewRow();
        //                row["ClassID"] = val.Classid;
        //                row["SectionID"] = val.Sectionid;
        //                row["Class"] = val.ClassName;
        //                row["Section"] = val.SectionName;
        //                row["Day"] = val.Day;
        //                row["Days"] = val.DayName;
        //                table.Rows.Add(row);
        //            }
        //            var rowsToUpdate = table.AsEnumerable().Where(r => r.Field<string>("Day") == val.Day
        //                                                     && r.Field<string>("ClassID") == val.Classid && r.Field<string>("SectionID") == val.Sectionid);
        //            time = Time_Start;
        //            tss = TimeSpan.Parse(time);
        //            timeS = DateTime.Today.Add(tss);
        //            time = Time_End;
        //            tss = TimeSpan.Parse(time);
        //            timeE = DateTime.Today.Add(tss);
        //            timeE = timeE.AddHours(12);

        //            foreach (var rows in rowsToUpdate)
        //            {
        //                for (; timeS < timeE;)
        //                {
        //                    string field = timeS.ToShortTimeString() + "-" + timeS.AddMinutes(time_duration).ToShortTimeString();
        //                    if (field == val.Slat)
        //                        rows.SetField(field, val.SubjectCode + "-- " + val.TeacherName);

        //                    timeS = timeS.AddMinutes(time_duration);
        //                }

        //            }
        //            hasVal = 0;
        //        }
        //    }
        //    gridControl1.DataSource = null;
        //    gridView1.Columns.Clear();
        //    gridControl1.DataSource = table;
        //    RepositoryItemMemoEdit rimemo = new RepositoryItemMemoEdit();
        //    gridView1.Columns[6].ColumnEdit = rimemo;
        //    gridView1.Columns[7].ColumnEdit = rimemo;
        //    gridView1.Columns[8].ColumnEdit = rimemo;
        //    gridView1.Columns[9].ColumnEdit = rimemo;
        //    gridView1.Columns[10].ColumnEdit = rimemo;
        //    gridView1.Columns[11].ColumnEdit = rimemo;
        //    gridView1.Columns[12].ColumnEdit = rimemo;
        //    gridView1.Columns[13].ColumnEdit = rimemo;
        //    gridView1.OptionsView.RowAutoHeight = true;
        //    gridView1.Appearance.Row.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
        //    gridView1.RowHeight = 35;
        //    var col = gridView1.Columns["ClassID"];
        //    col.Visible = false;
        //    var col1 = gridView1.Columns["SectionID"];
        //    col1.Visible = false;
        //    var col2 = gridView1.Columns["Day"];
        //    col2.Visible = false;
        //    var col3 = gridView1.Columns["Class"];
        //    col3.Group();
        //    var col4 = gridView1.Columns["Section"];
        //    col4.Group();
        //    gridView1.ExpandAllGroups();


        //}
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        // check subject first in this block of code
        //private void btnGenerate_Click(object sender, EventArgs e)
        //{
        //    simpleButton2.Enabled = true;
        //    con = new MySqlConnection(Login.constring);
        //    con.Open();
        //    MySqlCommand cmd = new MySqlCommand("TRUNCATE time_table", con);
        //    cmd.ExecuteNonQuery();
        //    con.Close();
        //    SlatSection = new List<Sslats>();
        //    SlatSection = getSectionTimeSlat();
        //    SlatTeacher = new List<Tslats>();
        //    //  SlatTeacher = getTeacherTimeSlat();
        //    SlatTeacher = getTeacherTimeSlat2();
        //    var ssc = SectionAvailableSlats(38);
        //    //var cs = ClassAvailableSlats(3);
        //    var cs = ClassAvailableSlats();
        //    //var subject = ClassSubject(3);
        //    var subject = ClassSubject();
        //    var ts = TeacherAvailableSlats();
        //    // WorkLoad = new List<CalCreditHours>();
        //    // WorkLoad = getCalWorkLoad();
        //    HoursListItem = new List<CreditHours>();
        //    HoursListItem = getSSWorkLoad();
        //    TeacherLimit = new List<TeacherLLimit>();
        //    TeacherLimit = getLlimit();
        //    timeTable = new List<TimeTable>();

        //    var ttl = ts.Where(a => subject.Select(b => b.Code).Contains(a.SubjectCode));
        //    var count = ttl.Count();
        //    //  var ss = cs.Where(a => a.Cid == "1" || a.Cid == "3" || a.Cid == "5" || a.Cid == "7");
        //    var ss = cs;
        //    var vvvv = ss.Count();
        //    //  var HoursList = HoursListItem.Where(a => a.Classid == 1 || a.Classid == 3 || a.Classid == 5 || a.Classid == 7);
        //    var HoursList = HoursListItem;
        //    int n = 0;
        //    A:

        //    foreach (var s in ss.Where(c => c.Used == null)) //foreach (var s in cs.Skip(n))
        //    {
        //        if (n == 59) { }
        //        if (n > 650) { }
        //        if (s.Used != "1")
        //            foreach (var sub in subject.OrderBy(a => a.index))
        //            {
        //                var ff = HoursList.Where(a => a.SubjectCode == sub.Code && a.Sectionid == s.Sid && a.WorkLoad > 0).Count();
        //                if (ff == 0)
        //                {
        //                    var val = sub.index;
        //                    if (val > 1)
        //                    {

        //                    }
        //                    sub.index = subject.Count() + val;
        //                    foreach (var su in subject)
        //                    {
        //                        if (su.index != subject.Count + val)//&& su.index > val)
        //                            su.index = su.index - val;
        //                    }
        //                    if (sub.index == subject.Count + val)
        //                        sub.index = subject.Count;

        //                }
        //                foreach (var HL in HoursList.Where(a => a.SubjectCode == sub.Code && a.Sectionid == s.Sid && a.WorkLoad > 0))
        //                {

        //                    foreach (var TL in TeacherLimit.Where(a => a.Llimit > 0 && a.SCode == sub.Code))
        //                    {
        //                        foreach (var T in ttl.Where(a => a.SubjectCode == sub.Code && a.Slats == s.Slats && TL.ID == int.Parse(a.Teacherid)
        //   && a.Day == s.SDay && a.Used == null))
        //                            if (T.Used != "1")
        //                                if (TL.Llimit > 0)
        //                                    if (T.SubjectCode == sub.Code)
        //                                        if (HL.SubjectCode == sub.Code && HL.WorkLoad > 0)//<= SubjectWorkload(int.Parse(s.Sid), sub.Code))
        //                                            if (T.Day == s.SDay)
        //                                                if (T.Slats == s.Slats)
        //                                                {
        //                                                    if (s.SDay == "3" || s.SDay == "4")
        //                                                    {
        //                                                        if (s.SlatsVal == 3)
        //                                                        {
        //                                                            TimeTable dd = new TimeTable
        //                                                            {
        //                                                                Classid = s.Cid,
        //                                                                ClassName = s.CName,
        //                                                                Sectionid = s.Sid,
        //                                                                SectionName = s.SName,
        //                                                                SubjectCode = "",
        //                                                                Day = s.SDay,
        //                                                                Slat = s.Slats,
        //                                                                Teacherid = "",
        //                                                                TeacherName = "",
        //                                                                SlatVal = s.SlatsVal,
        //                                                                DayName = s.SDayName
        //                                                            };
        //                                                            timeTable.Add(dd);
        //                                                            s.Used = "1";
        //                                                            n++; goto A;

        //                                                        }
        //                                                        if (s.SlatsVal == 6)
        //                                                        {
        //                                                            TimeTable dd = new TimeTable
        //                                                            {
        //                                                                Classid = s.Cid,
        //                                                                ClassName = s.CName,
        //                                                                Sectionid = s.Sid,
        //                                                                SectionName = s.SName,
        //                                                                SubjectCode = "",
        //                                                                Day = s.SDay,
        //                                                                Slat = s.Slats,
        //                                                                Teacherid = "",
        //                                                                TeacherName = "",
        //                                                                SlatVal = s.SlatsVal,
        //                                                                DayName = s.SDayName
        //                                                            };
        //                                                            timeTable.Add(dd);
        //                                                            s.Used = "1";
        //                                                            n++; goto A;

        //                                                        }
        //                                                    }
        //                                                    else
        //                                                    {
        //                                                        if (s.SlatsVal == 5)
        //                                                        {
        //                                                            TimeTable dd = new TimeTable
        //                                                            {
        //                                                                Classid = s.Cid,
        //                                                                ClassName = s.CName,
        //                                                                Sectionid = s.Sid,
        //                                                                SectionName = s.SName,
        //                                                                SubjectCode = "",
        //                                                                Day = s.SDay,
        //                                                                Slat = s.Slats,
        //                                                                Teacherid = "",
        //                                                                TeacherName = "",
        //                                                                SlatVal = s.SlatsVal,
        //                                                                DayName = s.SDayName
        //                                                            };
        //                                                            timeTable.Add(dd);

        //                                                            s.Used = "1";
        //                                                            n++; goto A;

        //                                                        }
        //                                                    }
        //                                                    TimeTable d = new TimeTable
        //                                                    {
        //                                                        Classid = s.Cid,
        //                                                        ClassName = s.CName,
        //                                                        Sectionid = s.Sid,
        //                                                        SectionName = s.SName,
        //                                                        SubjectCode = sub.Code,
        //                                                        Day = s.SDay,
        //                                                        Slat = s.Slats,
        //                                                        Teacherid = T.Teacherid,
        //                                                        TeacherName = T.TeacherName,
        //                                                        SlatVal = s.SlatsVal,
        //                                                        DayName = s.SDayName
        //                                                    };
        //                                                    timeTable.Add(d);
        //                                                    HL.WorkLoad = HL.WorkLoad - 1;
        //                                                    TL.Llimit = TL.Llimit - 1;
        //                                                    T.Used = "1";
        //                                                    s.Used = "1";
        //                                                    n++;
        //                                                    var val = sub.index;
        //                                                    if (val > 1)
        //                                                    {

        //                                                    }
        //                                                    sub.index = subject.Count() + val;
        //                                                    foreach (var su in subject)
        //                                                    {
        //                                                        if (su.index != subject.Count + val)//&& su.index > val)
        //                                                            su.index = su.index - val;
        //                                                    }
        //                                                    if (sub.index == subject.Count + val)
        //                                                        sub.index = subject.Count;
        //                                                    goto A;
        //                                                }
        //                    }
        //                }
        //            }

        //    }
        //    var ttt = from c in cs
        //              where c.Used == null
        //              select c;
        //    var v = ttt.Count();
        //    DataTable table = new DataTable();
        //    DataColumn[] cols ={
        //              new DataColumn("ClassID",typeof(String)),
        //              new DataColumn("SectionID",typeof(String)),
        //              new DataColumn("Class",typeof(String)),
        //              new DataColumn("Section",typeof(String)),
        //              new DataColumn("Day",typeof(String)),
        //              new DataColumn("Days",typeof(String)),
        //           };
        //    table.Columns.AddRange(cols);
        //    string time = Time_Start;
        //    TimeSpan tss = TimeSpan.Parse(time);
        //    DateTime timeS = DateTime.Today.Add(tss);
        //    time = Time_End;
        //    tss = TimeSpan.Parse(time);
        //    DateTime timeE = DateTime.Today.Add(tss);
        //    timeE = timeE.AddHours(12);
        //    for (; timeS < timeE;)
        //    {
        //        DataColumn[] colsa ={
        //              new DataColumn(timeS.ToShortTimeString() + "-" + timeS.AddMinutes(time_duration).ToShortTimeString(),typeof(String))
        //        };
        //        table.Columns.AddRange(colsa);
        //        timeS = timeS.AddMinutes(time_duration);
        //    }

        //    DataRow row = table.NewRow();
        //    foreach (DayOfWeek day in Enum.GetValues(typeof(DayOfWeek)))
        //    {
        //        var TTres = from tt in timeTable
        //                    where tt.Day == day.GetHashCode().ToString()
        //                    orderby tt.SlatVal
        //                    select tt;

        //        foreach (var val in TTres)
        //        {
        //            int hasVal = 0;
        //            var res = table.AsEnumerable().Where(r => r.Field<string>("Day") == val.Day
        //                                                     && r.Field<string>("ClassID") == val.Classid && r.Field<string>("SectionID") == val.Sectionid);

        //            foreach (var re in res)
        //            {
        //                hasVal = 1;
        //            }
        //            if (hasVal == 0)
        //            {
        //                row = table.NewRow();
        //                row["ClassID"] = val.Classid;
        //                row["SectionID"] = val.Sectionid;
        //                row["Class"] = val.ClassName;
        //                row["Section"] = val.SectionName;
        //                row["Day"] = val.Day;
        //                row["Days"] = val.DayName;
        //                table.Rows.Add(row);
        //            }
        //            var rowsToUpdate = table.AsEnumerable().Where(r => r.Field<string>("Day") == val.Day
        //                                                     && r.Field<string>("ClassID") == val.Classid && r.Field<string>("SectionID") == val.Sectionid);
        //            time = Time_Start;
        //            tss = TimeSpan.Parse(time);
        //            timeS = DateTime.Today.Add(tss);
        //            time = Time_End;
        //            tss = TimeSpan.Parse(time);
        //            timeE = DateTime.Today.Add(tss);
        //            timeE = timeE.AddHours(12);

        //            foreach (var rows in rowsToUpdate)
        //            {
        //                for (; timeS < timeE;)
        //                {
        //                    string field = timeS.ToShortTimeString() + "-" + timeS.AddMinutes(time_duration).ToShortTimeString();
        //                    if (field == val.Slat)
        //                        rows.SetField(field, val.SubjectCode + "-- " + val.TeacherName);

        //                    timeS = timeS.AddMinutes(time_duration);
        //                }

        //            }
        //            hasVal = 0;
        //        }
        //    }
        //    gridControl1.DataSource = null;
        //    gridView1.Columns.Clear();
        //    gridControl1.DataSource = table;
        //    RepositoryItemMemoEdit rimemo = new RepositoryItemMemoEdit();
        //    gridView1.Columns[6].ColumnEdit = rimemo;
        //    gridView1.Columns[7].ColumnEdit = rimemo;
        //    gridView1.Columns[8].ColumnEdit = rimemo;
        //    gridView1.Columns[9].ColumnEdit = rimemo;
        //    gridView1.Columns[10].ColumnEdit = rimemo;
        //    gridView1.Columns[11].ColumnEdit = rimemo;
        //    gridView1.Columns[12].ColumnEdit = rimemo;
        //    gridView1.Columns[13].ColumnEdit = rimemo;
        //    gridView1.OptionsView.RowAutoHeight = true;
        //    gridView1.Appearance.Row.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
        //    gridView1.RowHeight = 35;
        //    var col = gridView1.Columns["ClassID"];
        //    col.Visible = false;
        //    var col1 = gridView1.Columns["SectionID"];
        //    col1.Visible = false;
        //    var col2 = gridView1.Columns["Day"];
        //    col2.Visible = false;
        //    var col3 = gridView1.Columns["Class"];
        //    col3.Group();
        //    var col4 = gridView1.Columns["Section"];
        //    col4.Group();
        //    gridView1.ExpandAllGroups();


        //}

        private void btnGenerate_Click(object sender, EventArgs e)
        {
            simpleButton2.Enabled = true;
            con = new MySqlConnection(Login.constring);
            con.Open();
            MySqlCommand cmd = new MySqlCommand("TRUNCATE time_table", con);
            cmd.ExecuteNonQuery();
            con.Close();
            SlatSection = new List<Sslats>();
            SlatSection = getSectionTimeSlat();
            SlatTeacher = new List<Tslats>();
            SlatTeacher = getTeacherTimeSlat();
            //SlatTeacher = getTeacherTimeSlat2();
            var ssc = SectionAvailableSlats(38);
            var cs = ClassAvailableSlats();
            // var cs = ClassAvailableSlats();
            var subject = ClassSubject();
            // var subject = ClassSubject();
            var ts = TeacherAvailableSlats();
            // WorkLoad = new List<CalCreditHours>();
            // WorkLoad = getCalWorkLoad();
            HoursListItem = new List<CreditHours>();
            HoursListItem = getSSWorkLoad();
            TeacherLimit = new List<TeacherLLimit>();
            TeacherLimit = getLlimit();
            timeTable = new List<TimeTable>();

            var ttl = ts.Where(a => subject.Select(b => b.Code).Contains(a.SubjectCode));
            var count = ttl.Count();
            //  var ss = cs.Where(a => a.Cid == "1" || a.Cid == "3" || a.Cid == "5" || a.Cid == "7");
            var ss = cs;

            var vvvv = ss.Count();
            // var HoursList = HoursListItem.Where(a => a.Classid == 1 || a.Classid == 3);// || a.Classid == 5 || a.Classid == 7);
            var HoursList = HoursListItem;
            int n = 0;
            A:

            foreach (var s in ss.Where(c => c.Used == null)) //foreach (var s in cs.Skip(n))
            {
                if (n == 269) { }
                if (n > 650) { }
                if (s.Used != "1")
                    foreach (var sub in subject.OrderBy(a => a.index))
                    {
                        var ff = HoursList.Where(a => a.SubjectCode == sub.Code && a.Sectionid == s.Sid && a.WorkLoad > 0).Count();
                        if (ff == 0)
                        {
                            var val = sub.index;
                            if (val > 1)
                            {

                            }
                            sub.index = subject.Count() + val;
                            foreach (var su in subject)
                            {
                                if (su.index != subject.Count + val)//&& su.index > val)
                                    su.index = su.index - val;
                            }
                            if (sub.index == subject.Count + val)
                                sub.index = subject.Count;

                        }
                        foreach (var HL in HoursList.Where(a => a.SubjectCode == sub.Code && a.Sectionid == s.Sid && a.WorkLoad > 0))
                        {

                            foreach (var TL in TeacherLimit.Where(a => a.Llimit > 0 && a.SCode == sub.Code))
                            {
                                foreach (var T in ttl.Where(a => a.SubjectCode == sub.Code && a.Slats == s.Slats && TL.ID == a.Teacherid
           && a.Day == s.SDay && a.Used == null))
                                    if (T.Used != "1")
                                        if (TL.Llimit > 0)
                                            if (T.SubjectCode == sub.Code)
                                                if (HL.SubjectCode == sub.Code && HL.WorkLoad > 0)//<= SubjectWorkload(int.Parse(s.Sid), sub.Code))
                                                    if (T.Day == s.SDay)
                                                        if (T.Slats == s.Slats)
                                                        {
                                                            if (s.SDay == "3" || s.SDay == "4")
                                                            {
                                                                if (s.SlatsVal == 3)
                                                                {
                                                                    TimeTable dd = new TimeTable
                                                                    {
                                                                        Classid = s.Cid,
                                                                        ClassName = s.CName,
                                                                        Sectionid = s.Sid,
                                                                        SectionName = s.SName,
                                                                        SubjectCode = "",
                                                                        Day = s.SDay,
                                                                        Slat = s.Slats,
                                                                        Teacherid = 0,
                                                                        TeacherName = "",
                                                                        SlatVal = s.SlatsVal,
                                                                        DayName = s.SDayName
                                                                    };
                                                                    timeTable.Add(dd);
                                                                    s.Used = "1";
                                                                    n++; goto A;

                                                                }
                                                                if (s.SlatsVal == 6)
                                                                {
                                                                    TimeTable dd = new TimeTable
                                                                    {
                                                                        Classid = s.Cid,
                                                                        ClassName = s.CName,
                                                                        Sectionid = s.Sid,
                                                                        SectionName = s.SName,
                                                                        SubjectCode = "",
                                                                        Day = s.SDay,
                                                                        Slat = s.Slats,
                                                                        Teacherid = 0,
                                                                        TeacherName = "",
                                                                        SlatVal = s.SlatsVal,
                                                                        DayName = s.SDayName
                                                                    };
                                                                    timeTable.Add(dd);
                                                                    s.Used = "1";
                                                                    n++; goto A;

                                                                }
                                                            }
                                                            else
                                                            {
                                                                if (s.SlatsVal == 5)
                                                                {
                                                                    TimeTable dd = new TimeTable
                                                                    {
                                                                        Classid = s.Cid,
                                                                        ClassName = s.CName,
                                                                        Sectionid = s.Sid,
                                                                        SectionName = s.SName,
                                                                        SubjectCode = "",
                                                                        Day = s.SDay,
                                                                        Slat = s.Slats,
                                                                        Teacherid = 0,
                                                                        TeacherName = "",
                                                                        SlatVal = s.SlatsVal,
                                                                        DayName = s.SDayName
                                                                    };
                                                                    timeTable.Add(dd);

                                                                    s.Used = "1";
                                                                    n++; goto A;

                                                                }
                                                            }
                                                            TimeTable d = new TimeTable
                                                            {
                                                                Classid = s.Cid,
                                                                ClassName = s.CName,
                                                                Sectionid = s.Sid,
                                                                SectionName = s.SName,
                                                                SubjectCode = sub.Code,
                                                                Day = s.SDay,
                                                                Slat = s.Slats,
                                                                Teacherid = T.Teacherid,
                                                                TeacherName = T.TeacherName,
                                                                SlatVal = s.SlatsVal,
                                                                DayName = s.SDayName
                                                            };
                                                            timeTable.Add(d);
                                                            HL.WorkLoad = HL.WorkLoad - 1;
                                                            TL.Llimit = TL.Llimit - 1;
                                                            T.Used = "1";
                                                            s.Used = "1";
                                                            n++;
                                                            var val = sub.index;
                                                            if (val > 1)
                                                            {

                                                            }
                                                            sub.index = subject.Count() + val;
                                                            foreach (var su in subject)
                                                            {
                                                                if (su.index != subject.Count + val)//&& su.index > val)
                                                                    su.index = su.index - val;
                                                            }
                                                            if (sub.index == subject.Count + val)
                                                                sub.index = subject.Count;
                                                            goto A;
                                                        }
                            }
                        }
                    }

            }
            var ttt = from c in cs
                      where c.Used == null
                      select c;
            var v = ttt.Count();
            DataTable table = new DataTable();
            DataColumn[] cols ={
                      new DataColumn("ClassID",typeof(String)),
                      new DataColumn("SectionID",typeof(String)),
                      new DataColumn("Class",typeof(String)),
                      new DataColumn("Section",typeof(String)),
                      new DataColumn("Day",typeof(String)),
                      new DataColumn("Days",typeof(String)),
                   };
            table.Columns.AddRange(cols);
            string time = Time_Start;
            TimeSpan tss = TimeSpan.Parse(time);
            DateTime timeS = DateTime.Today.Add(tss);
            time = Time_End;
            tss = TimeSpan.Parse(time);
            DateTime timeE = DateTime.Today.Add(tss);
            timeE = timeE.AddHours(12);
            for (; timeS < timeE;)
            {
                DataColumn[] colsa ={
                      new DataColumn(timeS.ToShortTimeString() + "-" + timeS.AddMinutes(time_duration).ToShortTimeString(),typeof(String))
                };
                table.Columns.AddRange(colsa);
                timeS = timeS.AddMinutes(time_duration);
            }

            DataRow row = table.NewRow();
            foreach (DayOfWeek day in Enum.GetValues(typeof(DayOfWeek)))
            {
                var TTres = from tt in timeTable
                            where tt.Day == day.GetHashCode().ToString()
                            orderby tt.SlatVal
                            select tt;

                foreach (var val in TTres)
                {
                    int hasVal = 0;
                    var res = table.AsEnumerable().Where(r => r.Field<string>("Day") == val.Day
                                                             && r.Field<int>("ClassID") == val.Classid && r.Field<int>("SectionID") == val.Sectionid);

                    foreach (var re in res)
                    {
                        hasVal = 1;
                    }
                    if (hasVal == 0)
                    {
                        row = table.NewRow();
                        row["ClassID"] = val.Classid;
                        row["SectionID"] = val.Sectionid;
                        row["Class"] = val.ClassName;
                        row["Section"] = val.SectionName;
                        row["Day"] = val.Day;
                        row["Days"] = val.DayName;
                        table.Rows.Add(row);
                    }
                    var rowsToUpdate = table.AsEnumerable().Where(r => r.Field<string>("Day") == val.Day
                                                             && r.Field<int>("ClassID") == val.Classid && r.Field<int>("SectionID") == val.Sectionid);
                    time = Time_Start;
                    tss = TimeSpan.Parse(time);
                    timeS = DateTime.Today.Add(tss);
                    time = Time_End;
                    tss = TimeSpan.Parse(time);
                    timeE = DateTime.Today.Add(tss);
                    timeE = timeE.AddHours(12);

                    foreach (var rows in rowsToUpdate)
                    {
                        for (; timeS < timeE;)
                        {
                            string field = timeS.ToShortTimeString() + "-" + timeS.AddMinutes(time_duration).ToShortTimeString();
                            if (field == val.Slat)
                                rows.SetField(field, val.SubjectCode + "-- " + val.TeacherName);

                            timeS = timeS.AddMinutes(time_duration);
                        }

                    }
                    hasVal = 0;
                }
            }
            gridControl1.DataSource = null;
            gridView1.Columns.Clear();
            gridControl1.DataSource = table;
            /*
            RepositoryItemMemoEdit rimemo = new RepositoryItemMemoEdit();
            gridView1.Columns[6].ColumnEdit = rimemo;
            gridView1.Columns[7].ColumnEdit = rimemo;
            gridView1.Columns[8].ColumnEdit = rimemo;
            gridView1.Columns[9].ColumnEdit = rimemo;
            gridView1.Columns[10].ColumnEdit = rimemo;
            gridView1.Columns[11].ColumnEdit = rimemo;
            gridView1.Columns[12].ColumnEdit = rimemo;
            gridView1.Columns[13].ColumnEdit = rimemo;
            */
            gridView1.OptionsView.RowAutoHeight = true;
            gridView1.Appearance.Row.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            gridView1.RowHeight = 35;
            var col = gridView1.Columns["ClassID"];
            col.Visible = false;
            var col1 = gridView1.Columns["SectionID"];
            col1.Visible = false;
            var col2 = gridView1.Columns["Day"];
            col2.Visible = false;
            var col3 = gridView1.Columns["Class"];
            col3.Group();
            var col4 = gridView1.Columns["Section"];
            col4.Group();
            gridView1.ExpandAllGroups();


        }
        private void simpleButton2_Click(object sender, EventArgs e)
        {
            foreach (DayOfWeek day in Enum.GetValues(typeof(DayOfWeek)))
            {
                var TTres = from tt in timeTable
                            where tt.Day == day.GetHashCode().ToString()
                            orderby tt.SlatVal
                            select tt;

                foreach (var val in TTres)
                {
                    con.Open();

                    try
                    {
                        MySqlCommand cmd = new MySqlCommand("INSERT into time_table(class_id,section_id,day,slate,teacher_id,subject_code) VALUES('" + val.Classid + "','" + val.Sectionid + "','" + val.DayName + "','" + val.Slat + "','" + val.Teacherid + "','" + val.SubjectCode + "');", con);
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
            fillTimeTable();
        }
        void fillTimeTable()
        {
            DataTable table = new DataTable();
            DataColumn[] cols ={
                       new DataColumn("Class",typeof(String)),
                       new DataColumn("Section",typeof(String)),
                       new DataColumn("Days",typeof(String)),
                   };
            table.Columns.AddRange(cols);
            string time = Time_Start;
            TimeSpan tss = TimeSpan.Parse(time);
            DateTime timeS = DateTime.Today.Add(tss);
            time = Time_End;
            tss = TimeSpan.Parse(time);
            DateTime timeE = DateTime.Today.Add(tss);
            timeE = timeE.AddHours(12);
            for (; timeS < timeE;)
            {
                DataColumn[] colsa ={
                      new DataColumn(timeS.ToShortTimeString() + "-" + timeS.AddMinutes(time_duration).ToShortTimeString(),typeof(String))
                };
                table.Columns.AddRange(colsa);
                timeS = timeS.AddMinutes(time_duration);
            }
            DataRow row = table.NewRow();
            // foreach (DayOfWeek day in Enum.GetValues(typeof(DayOfWeek)))
            {
                MySqlConnection con2 = new MySqlConnection(Login.constring);
                con2.Open();
                MySqlCommand cmd3 = new MySqlCommand("SELECT time_table.id, class.name as Class,section.name as Section,day as Days,slate,teacher.name as teacher,time_table.subject_code,attendance.date,time_table.date as d1,extra_teacher_id as extra   FROM time_table inner join class on class.class_id=time_table.class_id inner join section on section.section_id=time_table.section_id inner join teacher on teacher.teacher_id=time_table.teacher_id left join attendance on attendance.student_id = teacher.teacher_id and attendance.date = '" + DateTime.Now.Year + "-" + DateTime.Now.Month + "-" + DateTime.Now.Day + "'", con2);
                MySqlDataReader reader3 = cmd3.ExecuteReader();
                if (reader3.HasRows)
                {
                    while (reader3.Read())
                    {
                        if (reader3["id"].ToString() == "196")
                        {

                        }
                        int hasVal = 0;
                        var res = table.AsEnumerable().Where(r => r.Field<string>("Days") == reader3["Days"].ToString()
                                                                && r.Field<string>("Class") == reader3["Class"].ToString() && r.Field<string>("Section") == reader3["Section"].ToString());

                        foreach (var re in res)
                        {
                            hasVal = 1;
                        }
                        if (hasVal == 0)
                        {
                            row = table.NewRow();
                            row["Class"] = reader3["Class"].ToString();
                            row["Section"] = reader3["Section"].ToString();
                            row["Days"] = reader3["Days"].ToString();
                            table.Rows.Add(row);
                        }
                        var rowsToUpdate = table.AsEnumerable().Where(r => r.Field<string>("Days") == reader3["Days"].ToString()
                                                             && r.Field<string>("Class") == reader3["Class"].ToString() && r.Field<string>("Section") == reader3["Section"].ToString());
                        time = Time_Start;
                        tss = TimeSpan.Parse(time);
                        timeS = DateTime.Today.Add(tss);
                        time = Time_End;
                        tss = TimeSpan.Parse(time);
                        timeE = DateTime.Today.Add(tss);
                        timeE = timeE.AddHours(12);

                        foreach (var rows in rowsToUpdate)
                        {
                            for (; timeS < timeE;)
                            {
                                string field = timeS.ToShortTimeString() + "-" + timeS.AddMinutes(time_duration).ToShortTimeString();
                                if (field == reader3["slate"].ToString())
                                {
                                    var d = reader3["subject_code"].ToString() + " - " + reader3["teacher"].ToString();
                                    var date = "";
                                    if (reader3["date"].ToString() != "")
                                        date = Convert.ToDateTime(reader3["date"]).ToShortDateString();
                                    var d1 = "";
                                    if (reader3["d1"].ToString() != "")
                                        d1 = Convert.ToDateTime(reader3["d1"]).ToShortDateString();
                                    if (d1 == DateTime.Now.ToShortDateString())
                                        rows.SetField(field, reader3["subject_code"].ToString() + "_\n\r" + reader3["teacher"].ToString() + "_\n\r" + date + "_\n\r" + reader3["extra"].ToString());
                                    else
                                        rows.SetField(field, reader3["subject_code"].ToString() + "_\n\r" + reader3["teacher"].ToString() + "_\n\r" + date);
                                }
                                timeS = timeS.AddMinutes(time_duration);
                            }
                        }
                        hasVal = 0;
                    }
                }
                con2.Close();

            }
            gridControl1.DataSource = null;
            gridView1.Columns.Clear();
            gridControl1.DataSource = table;
            /*
            RepositoryItemMemoEdit rimemo = new RepositoryItemMemoEdit();
            gridView1.Columns[6].ColumnEdit = rimemo;
            gridView1.Columns[7].ColumnEdit = rimemo;
            gridView1.Columns[8].ColumnEdit = rimemo;
            gridView1.Columns[9].ColumnEdit = rimemo;
           // gridView1.Columns[10].ColumnEdit = rimemo;
            gridView1.Columns[5].ColumnEdit = rimemo;
            gridView1.Columns[4].ColumnEdit = rimemo;
            gridView1.Columns[3].ColumnEdit = rimemo;
            */
            gridView1.OptionsView.RowAutoHeight = true;
            gridView1.Appearance.Row.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            gridView1.RowHeight = 35;
            var col3 = gridView1.Columns["Class"];
            col3.Group();
            var col4 = gridView1.Columns["Section"];
            col4.Group();
            gridView1.ExpandAllGroups();

        }
        int SubjectWorkload(int Sid, string Scode)

        {
            int res = 0;
            MySqlConnection con2 = new MySqlConnection(Login.constring);
            con2.Open();
            MySqlCommand cmd3 = new MySqlCommand("select * from subject where section_id = '" + Sid + "' and subject_code='" + Scode + "'", con2);
            MySqlDataReader reader3 = cmd3.ExecuteReader();
            if (reader3.HasRows)
            {
                while (reader3.Read())
                {
                    res = int.Parse(reader3["work_load"].ToString());
                }
            }
            con2.Close();
            return res;
        }

        List<SubjectCode> classSubject;
        List<SubjectCode> ClassSubject()
        {
            classSubject = new List<SubjectCode>();
            MySqlConnection con2 = new MySqlConnection(Login.constring);
            con2.Open();
            MySqlCommand cmd3 = new MySqlCommand("SELECT max(subject_code) as code,max(work_load) as Work_load FROM subject join section on section.section_id =subject.section_id  group by subject_code", con2);//where class_id in (1,3,5,7)
            MySqlDataReader reader3 = cmd3.ExecuteReader();
            if (reader3.HasRows)
            {
                int n = 1;
                while (reader3.Read())
                {
                    SubjectCode d = new SubjectCode
                    {
                        Code = reader3["code"].ToString(),
                        WorkLoad = int.Parse(reader3["Work_load"].ToString()),
                        index = n
                    };
                    classSubject.Add(d);
                    n++;
                }
            }
            con2.Close();
            return classSubject;
        }
        List<SubjectCode> ClassSubject(int id)
        {
            classSubject = new List<SubjectCode>();
            MySqlConnection con2 = new MySqlConnection(Login.constring);
            con2.Open();
            MySqlCommand cmd3 = new MySqlCommand("SELECT max(subject_code) as code,max(work_load) as Work_load FROM subject join section on section.section_id =subject.section_id where section.class_id='" + id + "' group by subject_code", con2);
            MySqlDataReader reader3 = cmd3.ExecuteReader();
            if (reader3.HasRows)
            {
                int n = 1;
                while (reader3.Read())
                {
                    SubjectCode d = new SubjectCode
                    {
                        Code = reader3["code"].ToString(),
                        WorkLoad = int.Parse(reader3["Work_load"].ToString()),
                        index = n
                    };
                    classSubject.Add(d);
                    n++;
                }
            }
            con2.Close();
            return classSubject;
        }
        List<Tslats> TeacherAvailableSlats()
        {
            SlatTeacher = new List<Tslats>();
            SlatTeacher = getTeacherTimeSlat();

            var filteredResult = from s in SlatTeacher
                                 where s.Available == "1"
                                 orderby s.Day, /*s.SubjectCode,*/ s.SlatsVal
                                 select s;
            return filteredResult.ToList();

        }
        List<Sslats> ClassAvailableSlats()
        {
            SlatSection = new List<Sslats>();
            SlatSection = getSectionTimeSlat();

            var filteredResult = from s in SlatSection
                                 where s.Available == "1"
                                 orderby s.Cid, s.Sid, s.SDay, s.SlatsVal, s.Sid
                                 select s;
            return filteredResult.ToList();

        }
        List<Sslats> ClassAvailableSlats(int id)
        {
            SlatSection = new List<Sslats>();
            SlatSection = getSectionTimeSlat();

            var filteredResult = from s in SlatSection
                                 where s.Cid == id || s.Cid == 1 && s.Available == "1"
                                 orderby s.Cid, s.Sid, s.SDay, s.SlatsVal
                                 select s;
            return filteredResult.ToList();

        }
        List<Sslats> ClassAvailableSlats(int id, string day)

        {
            SlatSection = new List<Sslats>();
            SlatSection = getSectionTimeSlat();

            var filteredResult = from s in SlatSection
                                 where s.Cid == id && s.Available == "1" && s.SDay == day
                                 select s;

            return filteredResult.ToList();
        }

        List<Sslats> SectionAvailableSlats(int id)
        {
            SlatSection = new List<Sslats>();
            SlatSection = getSectionTimeSlat();

            var filteredResult = from s in SlatSection
                                 where s.Sid == id && s.Available == "1"
                                 select s;
            return filteredResult.ToList();

        }
        List<Sslats> SectionAvailableSlats(int id, string day)

        {
            SlatSection = new List<Sslats>();
            SlatSection = getSectionTimeSlat();

            var filteredResult = from s in SlatSection
                                 where s.Sid == id && s.Available == "1" && s.SDay == day
                                 select s;

            return filteredResult.ToList();
        }

        List<TeacherLLimit> Teacherllimit;
        List<TeacherLLimit> getLlimit()
        {
            Teacherllimit = new List<TeacherLLimit>();
            MySqlConnection con3 = new MySqlConnection(Login.constring);

            con3.Open();
            MySqlCommand cmd4 = new MySqlCommand("SELECT * FROM teacher where staff_type='Teaching' and teacher.lecture_limit>0 order by lecture_limit", con3);
            MySqlDataReader reader4 = cmd4.ExecuteReader();
            if (reader4.HasRows)
            {
                while (reader4.Read())
                {
                    string ds = reader4["lecture_limit"].ToString();
                    var ch = (ds == "") ? "0" : ds;

                    TeacherLLimit d = new TeacherLLimit
                    {
                        ID = int.Parse(reader4["teacher_id"].ToString()),
                        Llimit = int.Parse(ch.ToString()),
                        SCode = reader4["subject_code"].ToString()
                    };
                    Teacherllimit.Add(d);
                }
            }
            con3.Close();

            return Teacherllimit;
        }

        List<CreditHours> SectionSWorkLoad;
        List<CreditHours> getSSWorkLoad()
        {
            SectionSWorkLoad = new List<CreditHours>();
            con = new MySqlConnection(Login.constring);
            MySqlConnection con1 = new MySqlConnection(Login.constring);
            MySqlConnection con2 = new MySqlConnection(Login.constring);
            con.Open();
            cmd1 = new MySqlCommand("SELECT * FROM class ", con);
            reader1 = cmd1.ExecuteReader();
            if (reader1.HasRows)
            {
                while (reader1.Read())
                {
                    con1.Open();
                    MySqlCommand cmd2 = new MySqlCommand("SELECT * FROM section where class_id='" + reader1["class_id"] + "'", con1);
                    MySqlDataReader reader2 = cmd2.ExecuteReader();
                    if (reader2.HasRows)
                    {
                        while (reader2.Read())
                        {

                            con2.Open();
                            MySqlCommand cmd3 = new MySqlCommand("SELECT name, subject_code, work_load FROM subject where section_id='" + reader2["section_id"] + "'", con2);
                            MySqlDataReader reader3 = cmd3.ExecuteReader();
                            if (reader3.HasRows)
                            {
                                while (reader3.Read())
                                {
                                    string ds = reader3["work_load"].ToString();
                                    var ch = (ds == "") ? "0" : ds;

                                    CreditHours d = new CreditHours
                                    {
                                        Classid = int.Parse(reader1["class_id"].ToString()),
                                        Sectionid = int.Parse(reader2["section_id"].ToString()),
                                        SubjectCode = reader3["subject_code"].ToString(),
                                        WorkLoad = Convert.ToInt32(ch)
                                    };
                                    SectionSWorkLoad.Add(d);
                                }
                            }
                            con2.Close();
                        }
                    }
                    con1.Close();
                }
            }
            con.Close();
            return SectionSWorkLoad;
        }

        List<CalCreditHours> SectionWorkLoad;
        List<CalCreditHours> getCalWorkLoad()
        {
            SectionWorkLoad = new List<CalCreditHours>();
            int i = 0;
            con = new MySqlConnection(Login.constring);
            MySqlConnection con1 = new MySqlConnection(Login.constring);
            MySqlConnection con2 = new MySqlConnection(Login.constring);
            con.Open();
            cmd1 = new MySqlCommand("SELECT * FROM class ", con);
            reader1 = cmd1.ExecuteReader();
            if (reader1.HasRows)
            {
                while (reader1.Read())
                {
                    con1.Open();
                    MySqlCommand cmd2 = new MySqlCommand("SELECT * FROM section where class_id='" + reader1["class_id"] + "'", con1);
                    MySqlDataReader reader2 = cmd2.ExecuteReader();
                    if (reader2.HasRows)
                    {
                        while (reader2.Read())
                        {

                            con2.Open();
                            MySqlCommand cmd3 = new MySqlCommand("SELECT sum(work_load) as total FROM subject where section_id='" + reader2["section_id"] + "'", con2);
                            MySqlDataReader reader3 = cmd3.ExecuteReader();
                            if (reader3.HasRows)
                            {
                                while (reader3.Read())
                                {
                                    i++;
                                    CalCreditHours d = new CalCreditHours
                                    {
                                        index = i.ToString(),
                                        Classid = int.Parse(reader1["class_id"].ToString()),
                                        Sectionid = int.Parse(reader2["section_id"].ToString()),
                                        CalWorkLoad = reader3["total"].ToString()
                                    };
                                    SectionWorkLoad.Add(d);
                                }
                            }
                            con2.Close();
                        }
                    }
                    con1.Close();
                }
            }
            con.Close();
            return SectionWorkLoad;
        }

        List<Sslats> TimeSlatSection;
        List<Sslats> getSectionTimeSlat()
        {
            TimeSlatSection = new List<Sslats>();
            int i = 0;
            con = new MySqlConnection(Login.constring);
            MySqlConnection con1 = new MySqlConnection(Login.constring);
            con.Open();
            cmd1 = new MySqlCommand("SELECT * FROM class ", con);
            reader1 = cmd1.ExecuteReader();
            if (reader1.HasRows)
            {
                while (reader1.Read())
                {
                    con1.Open();
                    MySqlCommand cmd2 = new MySqlCommand("SELECT max(section.section_id) as section_id, max(section.name) as name,max(section.time_start) as time_start,max(section.time_end) as time_end FROM section inner join subject on subject.section_id=section.section_id where class_id='" + reader1["class_id"] + "' group by section.name order by section_id", con1);
                    MySqlDataReader reader2 = cmd2.ExecuteReader();
                    if (reader2.HasRows)
                    {
                        while (reader2.Read())
                        {
                            int slatsVal = 1;
                            int count = 0;
                            foreach (DayOfWeek day in Enum.GetValues(typeof(DayOfWeek)))
                            {
                                slatsVal = 1;
                                var dd = day.GetHashCode();
                                //string s = txtSTime.Text;
                                string s = Time_Start;
                                TimeSpan tss = TimeSpan.Parse(s);
                                DateTime ts = DateTime.Today.Add(tss);
                                // s = txtETime.Text;
                                s = Time_End;
                                tss = TimeSpan.Parse(s);
                                DateTime te = DateTime.Today.Add(tss);
                                te = te.AddHours(12);
                                for (; ts < te;)
                                {
                                    count++; i++;
                                    string s1 = reader2["time_start"].ToString();
                                    TimeSpan tss1 = TimeSpan.Parse(s1);
                                    DateTime ts1 = DateTime.Today.Add(tss1);
                                    s1 = reader2["time_end"].ToString();
                                    tss1 = TimeSpan.Parse(s1);
                                    DateTime te1 = DateTime.Today.Add(tss1);
                                    if (ts >= ts1 && ts <= te1)
                                    {
                                        Sslats d = new Sslats
                                        {
                                            index = i.ToString(),
                                            Cid = int.Parse(reader1["class_id"].ToString()),
                                            CName = reader1["name"].ToString(),
                                            Sid = int.Parse(reader2["section_id"].ToString()),
                                            SName = reader2["name"].ToString(),
                                            SDay = day.GetHashCode().ToString(),
                                            Slats = ts.ToShortTimeString() + "-" + ts.AddMinutes(time_duration).ToShortTimeString(),
                                            Available = "1",
                                            SlatsVal = slatsVal,
                                            SDayName = day.ToString()
                                        };
                                        TimeSlatSection.Add(d);
                                        ts = ts.AddMinutes(time_duration);
                                    }
                                    else
                                    {
                                        Sslats d = new Sslats
                                        {
                                            index = i.ToString(),
                                            Cid = int.Parse(reader1["class_id"].ToString()),
                                            Sid = int.Parse(reader2["section_id"].ToString()),
                                            CName = reader1["name"].ToString(),
                                            SName = reader2["name"].ToString(),
                                            SDay = day.GetHashCode().ToString(),
                                            Slats = slatsVal.ToString() + "-" + ts.ToShortTimeString() + "-" + ts.AddMinutes(time_duration).ToShortTimeString(),
                                            SlatsVal = slatsVal,
                                            Available = "0",
                                            SDayName = day.ToString()
                                        };
                                        TimeSlatSection.Add(d);
                                        ts = ts.AddMinutes(time_duration);
                                    }
                                    slatsVal++;

                                }
                            }
                        }
                    }
                    con1.Close();
                }
            }
            con.Close();
            return TimeSlatSection;
        }

        List<Tslats> TimeSlatTeacher;
        List<Tslats> getTeacherTimeSlat()
        {
            TimeSlatTeacher = new List<Tslats>();
            int i = 0;
            MySqlConnection con2 = new MySqlConnection(Login.constring);
            MySqlConnection con3 = new MySqlConnection(Login.constring);
            con2.Open();
            MySqlCommand cmd3 = new MySqlCommand("SELECT max(subject_code) as code FROM subject join section on section.section_id =subject.section_id group by subject_code", con2);
            MySqlDataReader reader3 = cmd3.ExecuteReader();
            if (reader3.HasRows)
            {
                while (reader3.Read())
                {
                    con3.Open();
                    MySqlCommand cmd4 = new MySqlCommand("SELECT * FROM teacher where subject_code='" + reader3["code"] + "' and staff_type='Teaching' and teacher.lecture_limit>0 ", con3);
                    MySqlDataReader reader4 = cmd4.ExecuteReader();
                    if (reader4.HasRows)
                    {
                        while (reader4.Read())
                        {

                            int slateVal = 1;
                            foreach (DayOfWeek day in Enum.GetValues(typeof(DayOfWeek)))
                            {
                                slateVal = 1;
                                // string s = txtSTime.Text;
                                string s = Time_Start;
                                TimeSpan tss = TimeSpan.Parse(s);
                                DateTime ts = DateTime.Today.Add(tss);
                                //  s = txtETime.Text;
                                s = Time_End;
                                tss = TimeSpan.Parse(s);
                                DateTime te = DateTime.Today.Add(tss);
                                te = te.AddHours(12);
                                for (; ts < te;)
                                {
                                    i++;
                                    string s1 = reader4["timeStart"].ToString();
                                    TimeSpan tss1 = TimeSpan.Parse(s1);
                                    DateTime ts1 = DateTime.Today.Add(tss1);
                                    s1 = reader4["timeEnd"].ToString();
                                    tss1 = TimeSpan.Parse(s1);
                                    DateTime te1 = DateTime.Today.Add(tss1);
                                    if (ts >= ts1 && ts <= te1)
                                    {
                                        Tslats d = new Tslats
                                        {
                                            index = i.ToString(),
                                            //  SubjectCode = reader3["code"].ToString(),
                                            Teacherid = int.Parse(reader4["teacher_id"].ToString()),
                                            TeacherName = reader4["name"].ToString(),
                                            Day = day.GetHashCode().ToString(),
                                            Slats = ts.ToShortTimeString() + "-" + ts.AddMinutes(time_duration).ToShortTimeString(),
                                            Available = "1",
                                            SlatsVal = slateVal,
                                        };
                                        TimeSlatTeacher.Add(d);
                                        ts = ts.AddMinutes(time_duration);
                                    }
                                    else
                                    {
                                        Tslats d = new Tslats
                                        {
                                            index = i.ToString(),
                                            //  SubjectCode = reader3["code"].ToString(),
                                            Teacherid = int.Parse(reader4["teacher_id"].ToString()),
                                            TeacherName = reader4["name"].ToString(),
                                            Day = day.GetHashCode().ToString(),
                                            Slats = ts.ToShortTimeString() + "-" + ts.AddMinutes(time_duration).ToShortTimeString(),
                                            Available = "0",
                                            SlatsVal = slateVal
                                        };
                                        TimeSlatTeacher.Add(d);
                                        ts = ts.AddMinutes(time_duration);

                                    }

                                    slateVal++;

                                }
                            }
                        }
                    }
                    con3.Close();
                }
            }
            con2.Close();

            return TimeSlatTeacher;
        }
        List<Tslats> getTeacherTimeSlat2()
        {
            TimeSlatTeacher = new List<Tslats>();
            int i = 0;
            MySqlConnection con2 = new MySqlConnection(Login.constring);
            MySqlConnection con3 = new MySqlConnection(Login.constring);


            con2.Open();
            MySqlCommand cmd3 = new MySqlCommand("SELECT max(subject.name) as code FROM subject join section on section.section_id =subject.section_id group by  subject.name", con2);
            MySqlDataReader reader3 = cmd3.ExecuteReader();
            if (reader3.HasRows)
            {
                while (reader3.Read())
                {
                    con3.Open();
                    MySqlCommand cmd4 = new MySqlCommand("SELECT * FROM teacher where subject_code='" + reader3["code"] + "' and staff_type='Teaching'", con3);
                    MySqlDataReader reader4 = cmd4.ExecuteReader();
                    if (reader4.HasRows)
                    {
                        while (reader4.Read())
                        {

                            int slateVal = 1;
                            foreach (DayOfWeek day in Enum.GetValues(typeof(DayOfWeek)))
                            {
                                slateVal = 1;
                                // string s = txtSTime.Text;
                                string s = Time_Start;
                                TimeSpan tss = TimeSpan.Parse(s);
                                DateTime ts = DateTime.Today.Add(tss);
                                //  s = txtETime.Text;
                                s = Time_End;
                                tss = TimeSpan.Parse(s);
                                DateTime te = DateTime.Today.Add(tss);
                                te = te.AddHours(12);
                                for (; ts < te;)
                                {
                                    i++;
                                    string s1 = reader4["timeStart"].ToString();
                                    TimeSpan tss1 = TimeSpan.Parse(s1);
                                    DateTime ts1 = DateTime.Today.Add(tss1);
                                    s1 = reader4["timeEnd"].ToString();
                                    tss1 = TimeSpan.Parse(s1);
                                    DateTime te1 = DateTime.Today.Add(tss1);
                                    if (ts >= ts1 && ts <= te1)
                                    {
                                        Tslats d = new Tslats
                                        {
                                            index = i.ToString(),
                                            //SubjectCode = reader3["code"].ToString(),
                                            Teacherid = int.Parse(reader4["teacher_id"].ToString()),
                                            TeacherName = reader4["name"].ToString(),
                                            Day = day.GetHashCode().ToString(),
                                            Slats = ts.ToShortTimeString() + "-" + ts.AddMinutes(time_duration).ToShortTimeString(),
                                            Available = "1",
                                            SlatsVal = slateVal,
                                        };
                                        TimeSlatTeacher.Add(d);
                                        ts = ts.AddMinutes(time_duration);
                                    }
                                    else
                                    {
                                        Tslats d = new Tslats
                                        {
                                            index = i.ToString(),
                                            //  SubjectCode = reader3["code"].ToString(),
                                            Teacherid = int.Parse(reader4["teacher_id"].ToString()),
                                            TeacherName = reader4["name"].ToString(),
                                            Day = day.GetHashCode().ToString(),
                                            Slats = ts.ToShortTimeString() + "-" + ts.AddMinutes(time_duration).ToShortTimeString(),
                                            Available = "0",
                                            SlatsVal = slateVal
                                        };
                                        TimeSlatTeacher.Add(d);
                                        ts = ts.AddMinutes(time_duration);

                                    }
                                    slateVal++;

                                }
                            }
                        }
                    }
                    con3.Close();
                }
            }
            con2.Close();

            return TimeSlatTeacher;
        }
        private void txtClass_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtSectionTo.Properties.Items.Clear();
            txtSectionFrom.Properties.Items.Clear();
            allClass.Clear();
            allClass = fun.GetAllSectionisClass(txtClass.Text);
            foreach (var allclass in allClass)
            {
                txtSectionTo.Properties.Items.Add(allclass.Name);
                txtSectionFrom.Properties.Items.Add(allclass.Name);
            }
            txtSectionFrom.Text = ""; txtSectionTo.Text = "";

            foreach (DayOfWeek day in Enum.GetValues(typeof(DayOfWeek)))
            {
                txtDayFrom.Properties.Items.Add(day);
                txtDayTo.Properties.Items.Add(day);
            }
            if (layoutControlGroup4.Text == "Create New")
            {
                txtSubject.Properties.Items.Clear();
                if (classSubject != null)
                    classSubject.Clear();
                classSubject = ClassSubject(fun.GetClassIDisSession(txtClass.Text, Main_FD.SelectedSession));
                foreach (var allclass in classSubject)
                {
                    txtSubject.Properties.Items.Add(allclass.Code);
                }
                txtSubject.Text = "";
            }

        }

        private void txtDayTo_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (layoutControlGroup4.Text == "Create New")
            {
                txtSlateTo.Properties.Items.Clear();
                var ClassID = fun.GetClassIDisSession(txtClass.SelectedText, Main_FD.SelectedSession);
                MySqlConnection con2 = new MySqlConnection(Login.constring);
                con2.Open();
                MySqlCommand cmd3 = new MySqlCommand("SELECT * FROM time_table where day='" + txtDayTo.Text + "' and class_id='" + ClassID + "' and section_id='" + fun.GetSectionIDisClass(txtSectionTo.SelectedText, ClassID) + "'", con2);
                MySqlDataReader reader3 = cmd3.ExecuteReader();
                if (reader3.HasRows)
                {
                    while (reader3.Read())
                    {
                        txtSlateTo.Properties.Items.Add(reader3["slate"].ToString());
                    }
                }
                con2.Close();
            }
            else
            {
                txtSlateTo.Properties.Items.Clear();
                var ClassID = fun.GetClassIDisSession(txtClass.SelectedText, Main_FD.SelectedSession);
                MySqlConnection con2 = new MySqlConnection(Login.constring);
                con2.Open();
                MySqlCommand cmd3 = new MySqlCommand("SELECT class.name as Class,section.name as Section,day as Days,slate,teacher.name as teacher,time_table.subject_code  FROM time_table inner join class on class.class_id=time_table.class_id inner join section on section.section_id=time_table.section_id inner join teacher on teacher.teacher_id=time_table.teacher_id where day='" + txtDayTo.Text + "' and class.class_id='" + ClassID + "' and section.section_id='" + fun.GetSectionIDisClass(txtSectionTo.SelectedText, ClassID) + "'", con2);
                MySqlDataReader reader3 = cmd3.ExecuteReader();
                if (reader3.HasRows)
                {
                    while (reader3.Read())
                    {
                        txtSlateTo.Properties.Items.Add(reader3["slate"].ToString());
                    }
                }
                con2.Close();
            }
        }

        private void txtDayFrom_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtSlateFrom.Properties.Items.Clear();
            var ClassID = fun.GetClassIDisSession(txtClass.SelectedText, Main_FD.SelectedSession);
            MySqlConnection con2 = new MySqlConnection(Login.constring);
            con2.Open();
            MySqlCommand cmd3 = new MySqlCommand("SELECT class.name as Class,section.name as Section,day as Days,slate,teacher.name as teacher,time_table.subject_code  FROM time_table inner join class on class.class_id=time_table.class_id inner join section on section.section_id=time_table.section_id inner join teacher on teacher.teacher_id=time_table.teacher_id where day='" + txtDayFrom.Text + "' and class.class_id='" + ClassID + "' and section.section_id='" + fun.GetSectionIDisClass(txtSectionFrom.SelectedText, ClassID) + "'", con2);
            MySqlDataReader reader3 = cmd3.ExecuteReader();
            if (reader3.HasRows)
            {
                while (reader3.Read())
                {
                    txtSlateFrom.Properties.Items.Add(reader3["slate"].ToString());
                }
            }
            con2.Close();
        }

        private void txtSlateTo_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (layoutControlGroup4.Text == "Create New")
            {
                allClass.Clear();
                allClass = fun.GetAllTeacher();
                txtTeacherTo.Properties.Items.Clear();
                foreach (var allclass in allClass)
                    txtTeacherTo.Properties.Items.Add(allclass.Name);

            }
            else
            {
                txtTeacherTo.Properties.Items.Clear();
                var ClassID = fun.GetClassIDisSession(txtClass.SelectedText, Main_FD.SelectedSession);
                MySqlConnection con2 = new MySqlConnection(Login.constring);
                con2.Open();
                MySqlCommand cmd3 = new MySqlCommand("SELECT class.name as Class,section.name as Section,day as Days,slate,teacher.name as teacher,time_table.subject_code  FROM time_table inner join class on class.class_id=time_table.class_id inner join section on section.section_id=time_table.section_id inner join teacher on teacher.teacher_id=time_table.teacher_id where day='" + txtDayTo.Text + "' and class.class_id='" + ClassID + "' and section.section_id='" + fun.GetSectionIDisClass(txtSectionTo.SelectedText, ClassID) + "' and slate='" + txtSlateTo.Text + "'", con2);
                MySqlDataReader reader3 = cmd3.ExecuteReader();
                if (reader3.HasRows)
                {
                    while (reader3.Read())
                    {
                        txtTeacherTo.Properties.Items.Add(reader3["Teacher"].ToString());
                    }
                }
                con2.Close();
            }
        }

        private void txtSlateFrom_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtTeacherForm.Properties.Items.Clear();
            var ClassID = fun.GetClassIDisSession(txtClass.SelectedText, Main_FD.SelectedSession);
            MySqlConnection con2 = new MySqlConnection(Login.constring);
            con2.Open();
            MySqlCommand cmd3 = new MySqlCommand("SELECT class.name as Class,section.name as Section,day as Days,slate,teacher.name as teacher,time_table.subject_code  FROM time_table inner join class on class.class_id=time_table.class_id inner join section on section.section_id=time_table.section_id inner join teacher on teacher.teacher_id=time_table.teacher_id where day='" + txtDayFrom.Text + "' and class.class_id='" + ClassID + "' and section.section_id='" + fun.GetSectionIDisClass(txtSectionFrom.SelectedText, ClassID) + "' and slate='" + txtSlateFrom.Text + "'", con2);
            MySqlDataReader reader3 = cmd3.ExecuteReader();
            if (reader3.HasRows)
            {
                while (reader3.Read())
                {
                    txtTeacherForm.Properties.Items.Add(reader3["Teacher"].ToString());
                }
            }
            con2.Close();
        }


        private void btnSave_Click(object sender, EventArgs e)
        {
            if (layoutControlGroup4.Text == "Interchange Teacher")
            {
                var resTo = checkAvailability(txtSlateTo.Text, txtDayTo.Text, txtTeacherTo.Text);
                var resFrom = checkAvailability(txtSlateFrom.Text, txtDayFrom.Text, txtTeacherForm.Text);

                var res2To = checkAvailability(txtSlateFrom.Text, txtDayFrom.Text, txtTeacherTo.Text);
                var res2From = checkAvailability(txtSlateTo.Text, txtDayTo.Text, txtTeacherForm.Text);
                var subjectCodeTo = subjectCode(resTo);
                var subjectCodeFrom = subjectCode(resFrom);
                if (res2From == res2To && subjectCodeTo == subjectCodeFrom)
                {
                    var ClassId = fun.GetClassIDisSession(txtClass.Text, Main_FD.SelectedSession);

                    var sectionIDTo = fun.GetSectionIDisClass(txtSectionTo.Text, ClassId);
                    var teacherIDTo = fun.GetTeacherID(txtTeacherTo.Text);

                    var sectionIDFrom = fun.GetSectionIDisClass(txtSectionFrom.Text, ClassId);
                    var teacherIDFrom = fun.GetTeacherID(txtTeacherForm.Text);


                    var query = "UPDATE time_table set section_id='" + sectionIDTo + "',teacher_id='" + teacherIDTo + "' WHERE id='" + resFrom + "';";
                    query += "UPDATE time_table set section_id='" + sectionIDFrom + "',teacher_id='" + teacherIDFrom + "' WHERE id='" + resTo + "';";


                    MySqlConnection con = new MySqlConnection(Login.constring);
                    con.Open();
                    MySqlCommand cmd = new MySqlCommand(query, con);
                    cmd.ExecuteNonQuery();
                    con.Close();
                    fillTimeTable();
                    layoutControl1.Visible = false;
                }
                else
                    MessageBox.Show("Check Slate Availability...!!");
            }
            else if (layoutControlGroup4.Text == "Interchange Slate")
            {
                var resTo = checkAvailability(txtSlateTo.Text, txtDayTo.Text, txtTeacherTo.Text);
                var resFrom = checkAvailability(txtSlateFrom.Text, txtDayFrom.Text, txtTeacherForm.Text);

                var res2To = checkAvailability(txtSlateFrom.Text, txtDayFrom.Text, txtTeacherTo.Text);
                var res2From = checkAvailability(txtSlateTo.Text, txtDayTo.Text, txtTeacherForm.Text);

                if (res2From == res2To)
                {
                    var ClassId = fun.GetClassIDisSession(txtClass.Text, Main_FD.SelectedSession);

                    var sectionIDTo = fun.GetSectionIDisClass(txtSectionTo.Text, ClassId);
                    var teacherIDTo = fun.GetTeacherID(txtTeacherTo.Text);
                    var subjectCodeTo = subjectCode(resTo);

                    var sectionIDFrom = fun.GetSectionIDisClass(txtSectionFrom.Text, ClassId);
                    var teacherIDFrom = fun.GetTeacherID(txtTeacherForm.Text);
                    var subjectCodeFrom = subjectCode(resFrom);

                    var query = "UPDATE time_table set section_id='" + sectionIDTo + "',teacher_id='" + teacherIDTo + "',subject_code='" + subjectCodeTo + "'WHERE id='" + resFrom + "';";
                    query += "UPDATE time_table set section_id='" + sectionIDFrom + "',teacher_id='" + teacherIDFrom + "',subject_code='" + subjectCodeFrom + "'WHERE id='" + resTo + "';";


                    MySqlConnection con = new MySqlConnection(Login.constring);
                    con.Open();
                    MySqlCommand cmd = new MySqlCommand(query, con);
                    cmd.ExecuteNonQuery();
                    con.Close();
                    fillTimeTable();
                    layoutControl1.Visible = false;
                }
                else
                    MessageBox.Show("Check Slate Availability...!!");
            }
            else if (layoutControlGroup4.Text == "Create New")
            {
                var ClassId = fun.GetClassIDisSession(txtClass.Text, Main_FD.SelectedSession);

                var sectionID = fun.GetSectionIDisClass(txtSectionTo.Text, ClassId);

                var resTo = checkAvailability(txtSlateTo.Text, txtDayTo.Text, sectionID);

                if (0 != resTo)
                {
                    var teacherID = fun.GetTeacherID(txtTeacherTo.Text);

                    var query = "UPDATE time_table set teacher_id='" + teacherID + "',subject_code='" + txtSubject.Text + "'WHERE id='" + resTo + "';";

                    MySqlConnection con = new MySqlConnection(Login.constring);
                    con.Open();
                    MySqlCommand cmd = new MySqlCommand(query, con);

                    cmd.ExecuteNonQuery();
                    con.Close();
                    fillTimeTable();
                    layoutControl1.Visible = false;
                }
                else
                if (0 == resTo)
                {
                    var teacherID = fun.GetTeacherID(txtTeacherTo.Text);

                    MySqlConnection con = new MySqlConnection(Login.constring);
                    con.Open();
                    MySqlCommand cmd = new MySqlCommand("INSERT into time_table(class_id,section_id,day,slate,teacher_id,subject_code) VALUES('" + ClassId + "','" + sectionID + "','" + txtDayTo.Text + "','" + txtSlateTo.Text + "','" + teacherID + "','" + txtSubject.Text + "');", con);

                    cmd.ExecuteNonQuery();
                    con.Close();
                    fillTimeTable();
                    layoutControl1.Visible = false;
                }
                else
                    MessageBox.Show("Check Slate Availability...!!");

            }
            else if (layoutControlGroup4.Text == "Teacher Allocation")
            {
                var ClassId = fun.GetClassIDisSession(txtClass.Text, Main_FD.SelectedSession);

                var sectionID = fun.GetSectionIDisClass(txtSectionTo.Text, ClassId);

                var resTo = checkAvailability(txtSlateTo.Text, txtDayTo.Text, sectionID);

                if (0 != resTo)
                {
                    var teacherID = fun.GetTeacherID(txtSelTeacher.Text);
                    var teacherID2 = fun.GetTeacherID(txtTeacherTo.Text.Trim());
                    var date = DateTime.Now.Year + "-" + DateTime.Now.Month + "-" + DateTime.Now.Day;

                    var query = "UPDATE time_table set extra_teacher_id='" + txtSelTeacher.Text + "',date='" + date + "'WHERE id='" + resTo + "';";
                    query += "Insert into extra_lecture(class_id,section_id,day,slate,teacher_id,subject_code,extra_teacher_id,date) values('" + ClassId + "','" + sectionID + "','" + txtDayTo.Text + "','" + txtSlateTo.Text + "','" + teacherID2 + "','" + txtSubject.Text + "','" + teacherID + "','" + date + "')";
                    MySqlConnection con = new MySqlConnection(Login.constring);
                    con.Open();
                    MySqlCommand cmd = new MySqlCommand(query, con);

                    cmd.ExecuteNonQuery();
                    con.Close();
                    fillTimeTable();
                    layoutControl1.Visible = false;
                }
                else
                    MessageBox.Show("Check Slate Availability...!!");

            }
        }

        int checkAvailability(string slate, string day, string teacher)
        {
            MySqlConnection con2 = new MySqlConnection(Login.constring);
            con2.Open();
            MySqlCommand cmd3 = new MySqlCommand("SELECT time_table.id,class.name as Class,section.name as Section,day as Days,slate,teacher.name as teacher,time_table.subject_code  FROM time_table inner join class on class.class_id=time_table.class_id inner join section on section.section_id=time_table.section_id inner join teacher on teacher.teacher_id=time_table.teacher_id where day='" + day + "' and slate='" + slate + "' and teacher.teacher_id='" + fun.GetTeacherID(teacher) + "'", con2);
            MySqlDataReader reader3 = cmd3.ExecuteReader();
            if (reader3.HasRows)
            {
                while (reader3.Read())
                {
                    return int.Parse(reader3["id"].ToString());
                }
            }
            con2.Close();
            return 0;
        }
        int checkAvailability(string slate, string day, int sectionID)
        {
            MySqlConnection con2 = new MySqlConnection(Login.constring);
            con2.Open();
            MySqlCommand cmd3 = new MySqlCommand("SELECT * FROM time_table where day='" + day + "' and slate='" + slate + "'and section_id='" + sectionID + "' ", con2);
            MySqlDataReader reader3 = cmd3.ExecuteReader();
            if (reader3.HasRows)
            {
                while (reader3.Read())
                {
                    return int.Parse(reader3["id"].ToString());
                }
            }
            con2.Close();
            return 0;
        }
        string subjectCode(int id)
        {
            MySqlConnection con2 = new MySqlConnection(Login.constring);
            con2.Open();
            MySqlCommand cmd3 = new MySqlCommand("SELECT * from time_table where id='" + id + "'", con2);
            MySqlDataReader reader3 = cmd3.ExecuteReader();
            if (reader3.HasRows)
            {
                while (reader3.Read())
                {
                    return reader3["subject_code"].ToString();
                }
            }
            con2.Close();
            return "0";
        }
        private void simpleButton3_Click(object sender, EventArgs e)
        {
            empty();
            layoutControl1.Visible = true;
            layoutControlGroup4.Text = "Interchange Slate";
            layoutControlGroup3.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
            layoutControlItem22.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
            layoutControlItem12.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
            layoutControlGroup2.TextVisible = true;

            allClass.Clear();
            allClass = fun.GetAllClassisSession(fun.GetDefaultSessionName());
            txtClass.Properties.Items.Clear();
            foreach (var allclass in allClass)
                txtClass.Properties.Items.Add(allclass.Name);

        }

        private void simpleButton5_Click(object sender, EventArgs e)
        {
            empty();
            layoutControl1.Visible = true;
            layoutControlGroup4.Text = "Interchange Teacher";
            layoutControlGroup3.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
            layoutControlGroup2.TextVisible = true;
            layoutControlItem22.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
            layoutControlItem12.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;

            allClass.Clear();
            allClass = fun.GetAllClassisSession(fun.GetDefaultSessionName());
            txtClass.Properties.Items.Clear();
            foreach (var allclass in allClass)
                txtClass.Properties.Items.Add(allclass.Name);

        }

        private void simpleButton6_Click(object sender, EventArgs e)
        {
            empty();
            layoutControl1.Visible = true;
            layoutControlGroup4.Text = "Create New";
            layoutControlGroup3.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
            layoutControlItem22.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
            layoutControlItem12.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
            layoutControlGroup2.TextVisible = false;
            allClass.Clear();
            allClass = fun.GetAllClassisSession(fun.GetDefaultSessionName());
            txtClass.Properties.Items.Clear();
            foreach (var allclass in allClass)
                txtClass.Properties.Items.Add(allclass.Name);
        }

        private void btnClose_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            layoutControl1.Visible = false;
        }

        private void ClassScheduleTiming_Enter(object sender, EventArgs e)
        {
            fillTimeTable();
        }

        private void gridView1_RowCellStyle(object sender, DevExpress.XtraGrid.Views.Grid.RowCellStyleEventArgs e)
        {
            GridView View = sender as GridView;

            if (e.Column.FieldName == "Days")
                return;
            var time = Time_Start;
            var tss = TimeSpan.Parse(time);
            var timeS = DateTime.Today.Add(tss);
            time = Time_End;
            tss = TimeSpan.Parse(time);
            var timeE = DateTime.Today.Add(tss);
            timeE = timeE.AddHours(12);


            for (; timeS < timeE;)
            {
                string field = timeS.ToShortTimeString() + "-" + timeS.AddMinutes(time_duration).ToShortTimeString();
                string res = View.GetRowCellDisplayText(e.RowHandle, View.Columns[field]);
                var date = "";

                if (res.Contains('_'))
                {
                    var val = res.Split('_')[2];
                    if (res.Split('_')[2] != "\n\r")
                        date = Convert.ToDateTime(res.Split('_')[2]).ToShortDateString();
                }
                string res2 = View.GetRowCellDisplayText(e.RowHandle, View.Columns["Days"]);


                if (e.Column.FieldName == field)//
                {
                    if (date != DateTime.Now.Date.ToShortDateString() && res2 == DateTime.Now.DayOfWeek.ToString())
                    {
                        e.Appearance.ForeColor = Color.Red;
                    }

                }
                timeS = timeS.AddMinutes(time_duration);
            }
        }

        void empty()
        {
            txtClass.Text = "";
            txtSectionTo.Text = "";
            txtSectionFrom.Text = "";
            txtDayFrom.Text = "";
            txtDayTo.Text = "";
            txtSlateTo.Text = "";
            txtSlateFrom.Text = "";
            txtSubject.Text = "";
            txtTeacherTo.Text = "";
            txtTeacherForm.Text = "";
        }
        private void gridView1_MouseDown(object sender, MouseEventArgs e)
        {
            GridView View = sender as GridView;
            var hitInfo = gridView1.CalcHitInfo(e.Location);
            if (hitInfo.InRowCell)
            {
                int rowHandle = hitInfo.RowHandle;
                GridColumn column = hitInfo.Column;
                var s = DoRowDoubleClick(gridView1, e.Location);
                layoutControl1.Visible = true;
                layoutControlGroup4.Text = "Teacher Allocation";
                layoutControlGroup3.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
                layoutControlItem22.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
                layoutControlItem12.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;

                layoutControlGroup2.TextVisible = false;

                DataRow row = s.Item1;
                object Val = s.Item2;
                txtClass.Properties.Items.Clear();
                txtSectionTo.Properties.Items.Clear();
                txtDayTo.Properties.Items.Clear();
                txtSlateTo.Properties.Items.Clear();
                txtTeacherTo.Properties.Items.Clear();
                txtClass.Text = row[0].ToString();
                txtSectionTo.Text = row[1].ToString();
                txtDayTo.Text = row[2].ToString();
                txtSlateTo.Text = column.ToString();
                string val = Val.ToString();
                if (val.Contains('_'))
                {
                    txtSubject.Text = val.Split('_')[0];
                    txtTeacherTo.Text = val.Split('_')[1];
                }
                else
                {
                    txtSubject.Text = "";
                    txtTeacherTo.Text = "";
                }
                txtSelTeacher.Text = "";
                txtSelTeacher.Properties.Items.Clear();
                var ClassID = fun.GetClassIDisSession(txtClass.SelectedText, Main_FD.SelectedSession);
                MySqlConnection con2 = new MySqlConnection(Login.constring);
                con2.Open();
                var date = DateTime.Now.Year + "-" + DateTime.Now.Month + "-" + DateTime.Now.Day;
                MySqlCommand cmd3 = new MySqlCommand("SELECT max( teacher.name) as teacher FROM time_table join teacher on teacher.teacher_id=time_table.teacher_id inner join attendance on attendance.student_id=teacher.teacher_id and attendance.date='" + date + "' where day='" + txtDayTo.Text + "' and slate!='" + txtSlateTo + "' AND time_table.teacher_id NOT IN(select teacher_id from time_table where time_table.day='" + txtDayTo.Text + "' and time_table.subject_code='" + txtSubject.Text + "' and time_table.slate ='" + txtSlateTo.Text + "') group by teacher.name", con2);
                MySqlDataReader reader3 = cmd3.ExecuteReader();
                if (reader3.HasRows)
                {
                    while (reader3.Read())
                    {
                        txtSelTeacher.Properties.Items.Add(reader3["Teacher"].ToString());
                    }
                }
                con2.Close();
            }
        }
        public static Tuple<DataRow, object> DoRowDoubleClick(GridView view, Point pt)
        {
            GridHitInfo info = view.CalcHitInfo(pt);
            if (info.InRow || info.InRowCell)
            {
                string colCaption = info.Column == null ? "N/A" : info.Column.GetCaption();
                object val = view.GetRowCellValue(info.RowHandle, info.Column);
                //  if (val != null)
                //     MessageBox.Show(string.Format("DoubleClick on row: {0}, column: {1}, value: {2}", info.RowHandle, colCaption, val));
                DataRow row = view.GetDataRow(info.RowHandle);
                return Tuple.Create(row, val);

            }
            return null;
        }
    }

}