using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Repository;
using DevExpress.XtraGrid;
using DevExpress.XtraGrid.Views.Grid;
using MySql.Data.MySqlClient;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace SchoolManagementSystem.Exam
{
    public partial class StdResult : DevExpress.XtraEditors.XtraUserControl
    {
        private static StdResult _instance;

        public static StdResult instance
        {
            get
            {
                if (_instance == null)
                    _instance = new StdResult();
                return _instance;
            }
        }
        ObservableCollection<AllSubject> allResultSub;
        ObservableCollection<StudentInfo> studentinfo = new ObservableCollection<StudentInfo>();

        CommonFunctions fun = new CommonFunctions();

        public StdResult()
        {
            InitializeComponent();
            loadfunctions();

            GridExamList.Properties.View.OptionsSelection.MultiSelect = true;
            GridExamList.CustomDisplayText += new DevExpress.XtraEditors.Controls.CustomDisplayTextEventHandler(gridLookUpEdit1_CustomDisplayText);
            GridExamList.Properties.PopulateViewColumns();

            gridCheckMarksSA = new GridCheckMarksSelection(GridExamList.Properties);
            gridCheckMarksSA.SelectionChanged += new GridCheckMarksSelection.SelectionChangedEventHandler(gridCheckMarks_SelectionChanged);
            //gridCheckMarksSA.SelectAll(table.DefaultView);
            GridExamList.Properties.Tag = gridCheckMarksSA;
            fun.DateFormat(txtTo);
            fun.DateFormat(txtFrom);
            txtTo.DateTime = Convert.ToDateTime("1/1/" + DateTime.Now.ToString("yyyy"));
        }
        public void loadfunctions()
        {

            load();

        }
        GridCheckMarksSelection gridCheckMarksSA;
        void load()
        {
            String student_sql = "SELECT student_id,student.name AS student,student.roll AS roll,parent.name AS parent,class.name AS class ,section.name AS section " +
                                "FROM student " +
                                "INNER JOIN parent ON parent.parent_id = student.parent_id " +
                                "INNER JOIN class ON  class.class_id=student.class_id " +
                                "INNER JOIN section ON section.section_id=student.section_id " +
                                "WHERE student.passout != 1 " +
                                "ORDER BY student.student_id ASC ";
            searchLookUpEdit1.Properties.DataSource = fun.FetchDataTable(student_sql);
            searchLookUpEdit1.Properties.DisplayMember = "student";
            searchLookUpEdit1.Properties.ValueMember = "student_id";

            string query = "SELECT exam_id AS id,`name` ,`date` FROM exam ORDER BY exam_id DESC   ";
            DataTable table = fun.FetchDataTable(query);
            GridExamList.Text = "";
            GridExamList.Properties.DataSource = table;
            GridExamList.Properties.DisplayMember = "name";
            GridExamList.Properties.ValueMember = "id";
            // this is commented bcz after reloaded form one new chack box wil be added 
            //GridExamList.Properties.View.OptionsSelection.MultiSelect = true;
            //GridExamList.CustomDisplayText += new DevExpress.XtraEditors.Controls.CustomDisplayTextEventHandler(gridLookUpEdit1_CustomDisplayText);
            //GridExamList.Properties.PopulateViewColumns();

            //gridCheckMarksSA = new GridCheckMarksSelection(GridExamList.Properties);
            //gridCheckMarksSA.SelectionChanged += new GridCheckMarksSelection.SelectionChangedEventHandler(gridCheckMarks_SelectionChanged);
            ////gridCheckMarksSA.SelectAll(table.DefaultView);
            //GridExamList.Properties.Tag = gridCheckMarksSA;

        }
        void gridCheckMarks_SelectionChanged(object sender, EventArgs e)
        {
            if (ActiveControl is GridLookUpEdit)
            {
                StringBuilder sb = new StringBuilder();
                foreach (DataRowView rv in (sender as GridCheckMarksSelection).Selection)
                {
                    if (sb.ToString().Length > 0) { sb.Append(", "); }
                    sb.Append(rv["name"].ToString());
                }
                (ActiveControl as GridLookUpEdit).Text = sb.ToString();
            }
        }


        void gridLookUpEdit1_CustomDisplayText(object sender, DevExpress.XtraEditors.Controls.CustomDisplayTextEventArgs e)
        {
            StringBuilder sb = new StringBuilder();
            GridCheckMarksSelection gridCheckMark = sender is GridLookUpEdit ? (sender as GridLookUpEdit).Properties.Tag as GridCheckMarksSelection : (sender as RepositoryItemGridLookUpEdit).Tag as GridCheckMarksSelection;
            if (gridCheckMark == null) return;
            foreach (DataRowView rv in gridCheckMark.Selection)
            {
                if (sb.ToString().Length > 0) { sb.Append(", "); }
                sb.Append(rv["name"].ToString());
            }
            e.DisplayText = sb.ToString();
        }
        private void btnCRShow_Click(object sender, System.EventArgs e)
        {
            if (searchLookUpEdit1.Text == "[EditValue is null]")
            {
                MessageBox.Show("Fill all fields....!!", "Info");
                return;
            }
            fun.loaderform(() =>
            {
                XtraStdReport rep = viewsinglestudent(int.Parse(searchLookUpEdit1.EditValue.ToString()), txtTo.DateTime, txtFrom.DateTime, txtIgnoreAbsent.Checked ? true : false, txtEnableRanking.Checked ? true : false, gridClassResult, gridView1, AttndControl, AttndgridView, chbattd_details.Checked, chb_show_section.Checked);
                documentViewer1.DocumentSource = rep;
                rep.CreateDocument();
                documentViewer1.PrintingSystem.Document.AutoFitToPagesWidth = 1;
            });
        }

        public static XtraStdReport stdreport(int stdid, DateTime todate, DateTime fromdate, bool ignorabsent, bool enableRanking, bool showattendance,bool show_section)
        {
            XtraStdReport rep = instance.viewsinglestudent(stdid, todate, fromdate, ignorabsent, enableRanking, instance.gridClassResult, instance.gridView1, instance.AttndControl, instance.AttndgridView, showattendance, show_section);

            return rep;
        }
        public XtraStdReport viewsinglestudent(int stdid, DateTime todate, DateTime fromdate, bool ignorabsent, bool enableRanking, GridControl classresultGrid, GridView classResultGW, GridControl attendcontrol, GridView attendGrid, bool showattendance,bool show_section)
        {
            /*
            allResultSub = new ObservableCollection<AllSubject>();
            var info = fun.GetStudentInfo(stdid);
            int sectionID = int.Parse(info.Split('>')[2]);
            allResultSub = fun.GetAllSubjectISS(sectionID);*/
            //
            GridCheckMarksSelection gridCheckMark = GridExamList.Properties.Tag as GridCheckMarksSelection;
            ArrayList valueList = gridCheckMark.Selection;
            int[] exam_array = new int[valueList.Count];
            int count = 0;

            foreach (DataRowView rv in valueList)
            {
                exam_array[count] = Convert.ToInt32(rv["id"]);
                count++;
            }
            XtraStdReport rep = fun.std_result_card(stdid, todate, fromdate, ignorabsent, enableRanking, classresultGrid, classResultGW, attendcontrol, attendGrid, exam_array, showattendance,false,show_section);
            return rep;
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            fun.loaderform(() =>
            {
                reports.Clear();
                var info = fun.GetStudentInfo(int.Parse(searchLookUpEdit1.EditValue.ToString()));
                int classID = int.Parse(info.Split('>')[1]);
                int sectionID = int.Parse(info.Split('>')[2]);
                allResultSub = new ObservableCollection<AllSubject>();
                allResultSub = fun.GetAllSubjectISS(sectionID);
                string query = "SELECT * FROM student inner join section on section.section_id=student.section_id where student.passout != 1 AND section.section_id='" + sectionID + "' order by roll ";
                DataTable reader2 = fun.FetchDataTable(query);
                //Grid exam selection
                GridCheckMarksSelection gridCheckMark = GridExamList.Properties.Tag as GridCheckMarksSelection;
                ArrayList valueList = gridCheckMark.Selection;
                int[] exam_array = new int[valueList.Count];
                int count = 0;
                foreach (DataRowView rv in valueList)
                {
                    exam_array[count] = Convert.ToInt32(rv["id"]);
                    count++;
                }

                if (reader2.Rows.Count > 0)
                {
                    gridView1.BestFitColumns(true);
                    int student_count = 1;
                    var percent = 0;
                    //std_rpt_percent.Visible = true;
                    foreach (DataRow dr in reader2.Rows)
                    {
                        var stdID = int.Parse(dr["student_id"].ToString());
                        this.Invoke((MethodInvoker)delegate
                        {
                            percent = (student_count / reader2.Rows.Count) * 100;
                            std_rpt_percent.EditValue = percent;
                            std_rpt_percent.Update();
                        });
                        XtraStdReport rep = fun.std_result_card(stdID, txtTo.DateTime, txtFrom.DateTime, txtIgnoreAbsent.Checked ? true : false, txtEnableRanking.Checked ? true : false, gridClassResult, gridView1, AttndControl, AttndgridView, exam_array, chbattd_details.Checked, true, chb_show_section.Checked);
                        rep.PrintingSystem.Document.AutoFitToPagesWidth = 1;
                        rep.PrintingSystem.Document.CanChangePageSettings = true;
                        AppendReport(rep);
                        student_count++;
                    }
                }
                documentViewer1.DocumentSource = CreateReport();
                documentViewer1.PrintingSystem.Document.CanChangePageSettings = true;
                documentViewer1.PrintingSystem.Document.AutoFitToPagesWidth = 1;
                std_rpt_percent.Visible = false;
            });
        }
            

        private void txtSection_SelectedIndexChanged(object sender, System.EventArgs e)
            {
                /*
                var classID = fun.GetClassIDisSession(txtClass.Text, Main_FD.SelectedSession);
                var sectionID = fun.GetSectionIDisClass(txtSection.Text, classID);
                studentinfo.Clear();
                string str = "where student.class_id='" + classID + "' and student.section_id='" + sectionID + "'";
                studentinfo = fun.GetAllStudentsWId_S_C_S(str);
                searchLookUpEdit1.Text = "";
                searchLookUpEdit1.Properties.DataSource = studentinfo;
                searchLookUpEdit1.Properties.DisplayMember = "ID";
                searchLookUpEdit1.Properties.ValueMember = "Name";
                */
            }

            private void btnPrint_Click(object sender, System.EventArgs e)
            {
            }
            private void gridView1_RowCellStyle(object sender, RowCellStyleEventArgs e)
            {
                try
                {
                    GridView View = sender as GridView;

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
                        //    for (int i = 0; i < allResultSub.Count; i++)
                        //    {
                        //        if (e.Column.FieldName == allResultSub[i].Name)
                        //        {
                        //            string val = View.GetRowCellDisplayText(e.RowHandle, View.Columns[allResultSub[i].Name]);
                        //            if (val.Contains("A"))
                        //                e.Appearance.Font = new Font("Calibri", 10, style: FontStyle.Bold);
                        //        }
                        //    }

                        //}
                        //if (e.Column.FieldName == "%")
                        //{

                        //    e.Appearance.BackColor = Color.Silver;
                        //    e.Appearance.ForeColor = Color.Black;

                        //}
                        //if (e.Column.FieldName == "Examination" || e.Column.FieldName == "Exam_Date")
                        //{
                        //    e.Appearance.BackColor = Color.Transparent;
                        //    e.Appearance.ForeColor = Color.Black;

                    }
                }
                catch (Exception ej)
                {
                }
            }

            private void StdResult_Enter(object sender, EventArgs e)
            {

            }

        private List<XtraStdReport> reports = new List<XtraStdReport>();
        public void AppendReport(XtraStdReport report)
        {
            reports.Add(report);
        }
        public XtraStdReport CreateReport()
        {
            XtraStdReport output = new XtraStdReport();
            foreach (var report in reports)
            {
                report.CreateDocument(false);
                output.Pages.AddRange(report.Pages);
            }
            if (output == null) output = new XtraStdReport();
            output.PrintingSystem.ContinuousPageNumbering = true;
            return output;
        }
        private void gridView1_CustomColumnDisplayText(object sender, DevExpress.XtraGrid.Views.Base.CustomColumnDisplayTextEventArgs e)
        {
            try
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
            catch (Exception ed) { }
        }

    }
}
