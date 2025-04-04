using DevExpress.LookAndFeel;
using DevExpress.XtraGrid.Columns;
using DevExpress.XtraReports.UI;
using MySql.Data.MySqlClient;
using System;
using System.Collections.ObjectModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Web.UI.WebControls;
using System.Windows.Forms;

namespace SchoolManagementSystem.Exam
{
    public partial class CheckList : DevExpress.XtraEditors.XtraUserControl
    {
        private static CheckList _instance;

        public static CheckList instance
        {
            get
            {
                if (_instance == null)
                    _instance = new CheckList();
                return _instance;
            }
        }
        ObservableCollection<CheckListItems> list = new ObservableCollection<CheckListItems>();

        ObservableCollection<AllClass> allClass = new ObservableCollection<AllClass>();
        ObservableCollection<TeachingStaff> allStaff = new ObservableCollection<TeachingStaff>();

        CommonFunctions fun = new CommonFunctions();

        public CheckList()
        {
            InitializeComponent();
            loadfunctions();
        }
        public void loadfunctions()
        {
            txtExam.Properties.DataSource = fun.GetAllExams_dt();
            txtExam.Properties.DisplayMember = "name";
            txtExam.Properties.ValueMember = "exam_id";

            txtclass.Properties.DataSource = fun.GetAllClasses_dt();
            txtclass.Properties.DisplayMember = "name";
            txtclass.Properties.ValueMember = "class_id";

            var qry = " and Staff_Type in('Administration','Teaching','Visiting')";
            txtTeacher.Properties.DataSource = fun.GetAllTeacher_dt(qry);
            txtTeacher.Properties.DisplayMember = "name";
            txtTeacher.Properties.ValueMember = "teacher_id";

        }
        string exam_ids;

        private void btnCRShow_Click(object sender, System.EventArgs e)
        {
            if (txtReport.Text == "" || txtExam.Text == "")
            {
                MessageBox.Show("Must Select Report type and Exam...!!!", "Info");
                return;
            }
            exam_ids = txtExam.EditValue.ToString();
            FillGridTotalMarks();
        }
        private void FillGridTotalMarks()
        {
            String query = "SELECT tms.smark_id as ID ,class.name as Class,section.name as Section,sub.name as Subject ,tms.check_list AS Checked,teacher.name as Teacher,test_not_deliver AS Deliver,conducted_on AS Conduct,submit_on AS Submit,exam.name as Exam,IF(sms_date IS NULL,'NO','YES') AS SMS,IFNULL(sms_date,'-') AS SMSDate " +
                " from tbl_mark_subject as tms " +
                " inner join section_subject as ss on ss.subject_id=tms.subject_id and ss.class_id = tms.class_id and ss.section_id = tms.section_id " +
                " inner join subject_default as sub on sub.id = ss.subject_id " +
                " inner join section on section.section_id=ss.section_id " +
                " inner join class on class.class_id=section.class_id " +
                " left join teacher on teacher.teacher_id=tms.teacher_id " +
                " inner join exam on tms.exam_id=exam.exam_id " +
                " where tms.exam_id in(" + exam_ids + ")";

            MySqlConnection con = new MySqlConnection(Login.constring);
            MySqlCommand cmd4 = new MySqlCommand(query, con);
            MySqlDataAdapter adp = new MySqlDataAdapter(cmd4);
            DataTable table = new DataTable();

            adp.Fill(table);
            gridCheckList.DataSource = null;
            gridView1.Columns.Clear();
            gridCheckList.DataSource = table;

            gridView1.BestFitColumns();
            var col0 = gridView1.Columns["ID"];
            col0.Visible = false;
            var col = gridView1.Columns["Class"];
            col.OptionsColumn.ReadOnly = true;
            var col1 = gridView1.Columns["Subject"];
            col1.OptionsColumn.ReadOnly = true;
            var col2 = gridView1.Columns["Section"];
            col2.OptionsColumn.ReadOnly = true;
            var col3 = gridView1.Columns["Teacher"];
            col3.OptionsColumn.ReadOnly = true;
            var col4 = gridView1.Columns["Exam"];
            col4.OptionsColumn.ReadOnly = true;
            col4.Group();
            col.Group();
            gridView1.ExpandAllGroups();

        }

        private void btnPrint_Click(object sender, System.EventArgs e)
        {
            exam_ids = txtExam.EditValue.ToString();

            var classID = txtclass.EditValue.ToString();
            //  var classID = fun.GetClassIDisSession(txtclass.Text, fun.GetDefaultSessionName());
            var classQuery = "";
            if (txtReport.Text == "" || txtExam.Text == "")
            {
                MessageBox.Show("Must Select Report type and Exam...!!!", "Info");
                return;
            }
            if (txtReport.Text == "Section Wise Report")
            {
                if (classID != "")
                {
                    classQuery = "and class.class_id in (" + classID + ")";
                }
                if (txtSubject.EditValue != null)
                    classQuery += "and sub.id in(" + txtSubject.EditValue + ")";
                if (txtChecked.Checked == true)
                    classQuery += "and tms.check_list = 1";

                if (txtUnChecked.Checked == true)
                    classQuery += " and tms.check_list != 1";

                String query4 = "SELECT tms.smark_id as ID ,class.name as Class,section.name as Section,sub.name as Subject ,tms.check_list,teacher.name as Teacher " +
                    " from tbl_mark_subject as tms " +
                    " inner join section_subject as ss on ss.subject_id=tms.subject_id and ss.class_id = tms.class_id and ss.section_id = tms.section_id " +
                    " inner join subject_default as sub on sub.id = ss.subject_id " +
                    " inner join section on section.section_id=ss.`section_id` " +
                    " inner join class on class.class_id=section.class_id " +
                    " left join teacher on teacher.teacher_id=tms.teacher_id " +
                    " where tms.exam_id in (" + exam_ids + ") " + classQuery + " order by class.name,section.name";
                XtraCheckListReportSectionWise report = new XtraCheckListReportSectionWise();
                DataTable table = fun.FetchDataTable(query4);
                list.Clear();
                for (int row = 0; row <= table.Rows.Count - 1; row++)
                {
                    CheckListItems cl;
                    if (row % 2 == 0)
                    {
                        cl = new CheckListItems
                        {
                            Class = (table.Rows[row][1].ToString()),
                            Section = (table.Rows[row][2].ToString()),

                            Subject = (table.Rows[row][3].ToString()),
                            Check = Convert.ToBoolean(table.Rows[row][4]),
                            Teacher = table.Rows[row][5].ToString()


                        };
                        list.Add(cl);
                    }
                    else
                    {
                        var item = list.FirstOrDefault(i => i.Section == table.Rows[row][2].ToString() && i.Subject1 == null);
                        if (item != null)
                        {
                            item.Subject1 = (table.Rows[row][3].ToString());
                            item.Check1 = Convert.ToBoolean(table.Rows[row][4]);
                            item.Teacher1 = table.Rows[row][5].ToString();

                        }
                        else
                        {
                            cl = new CheckListItems
                            {
                                Class = (table.Rows[row][1].ToString()),
                                Section = (table.Rows[row][2].ToString()),

                                Subject = (table.Rows[row][3].ToString()),
                                Check = Convert.ToBoolean(table.Rows[row][4]),
                                Teacher = table.Rows[row][5].ToString()

                            };
                            list.Add(cl);
                        }
                    }

                }
                report.GridControl = list;
                System.Drawing.Image logo = fun.Base64ToImage(Login.Logo);
                var school = fun.GetSettings("system_title");
                report.PicIogoBox.Image = logo;
                report.LabTitle.Text = school;
                report.LabAddress.Text = fun.GetSettings("address");
                report.LabExam.Text = txtExam.Text;
                report.LabTel.Text = "Check List Report Section Wise";
                try
                {
                    var info = fun.GetExamDetailIsSession(txtExam.Text, fun.GetDefaultSessionName());
                    report.labExamDate.Text = Convert.ToDateTime(info[0].Salary).ToShortDateString() + " - " + DateTime.Now.ToShortDateString();
                }
                catch (Exception ep) {
                }

                ReportPrintTool printTool = new ReportPrintTool(report);
                printTool.PreviewForm.PrintingSystem.Document.AutoFitToPagesWidth = 1;
                printTool.ShowRibbonPreviewDialog(UserLookAndFeel.Default);
            }
            else if (txtReport.Text == "Subject Wise Report")
            {
                if (classID != "")
                    classQuery = "and class.class_id in (" + classID + ")";
                if (txtSubject.EditValue != null)
                    classQuery += " and sub.id in(" + txtSubject.EditValue.ToString() + ")";
                if (txtChecked.Checked == true)
                    classQuery += " and tms.check_list = 1";

                if (txtUnChecked.Checked == true)
                    classQuery += "and tms.check_list != 1";
                XtraCheckListReportSubjectWise report = new XtraCheckListReportSubjectWise();
                string query = "SELECT tms.smark_id as ID ,class.name as Class,section.name as Section,sub.name as Subject ,tms.check_list ,teacher.name as Teacher,conducted_on,submit_on  " +
                     " from tbl_mark_subject as tms " +
                     " inner join section_subject as ss on ss.subject_id=tms.subject_id and ss.class_id = tms.class_id and ss.section_id = tms.section_id " +
                     " inner join subject_default as sub on sub.id = ss.subject_id " +
                     " inner join section on section.section_id=tms.section_id " +
                     " inner join class on class.class_id=section.class_id " +
                     " left join teacher on teacher.teacher_id=tms.teacher_id " +
                     " where tms.exam_id in(" + exam_ids + ") " + classQuery + " " +
                     " order by conducted_on, class.name,sub.name";
                DataTable table = fun.FetchDataTable(query);
                list.Clear();
                for (int row = 0; row <= table.Rows.Count - 1; row++)
                {
                    string dateC = "";
                    if (table.Rows[row][6].ToString() != "")
                        dateC = Convert.ToDateTime(table.Rows[row][6]).ToString("dd-MM-yyyy");
                    string dateD = "";
                    if (table.Rows[row][7].ToString() != "")
                        dateD = Convert.ToDateTime(table.Rows[row][7]).ToString("dd-MM-yyyy");

                    CheckListItems cl;
                    if (row % 2 == 0)
                    {
                        try
                        {
                            cl = new CheckListItems
                            {
                                Class = (table.Rows[row][1].ToString()),
                                Subject = (table.Rows[row][3].ToString()),
                                Date = dateC,
                                Section = (table.Rows[row][2].ToString()),
                                Check = Convert.ToBoolean(table.Rows[row][4]),
                                Teacher = table.Rows[row][5].ToString(),
                                DateD = dateD
                            };
                            list.Add(cl);
                        }
                        catch (Exception ep) { }
                    }
                    else
                    {
                        var item = list.FirstOrDefault(i => i.Subject == table.Rows[row][3].ToString() && i.Subject1 == null);
                        if (item != null)
                        {
                            item.Subject1 = (table.Rows[row][2].ToString());
                            item.Check1 = Convert.ToBoolean(table.Rows[row][4]);
                            item.Teacher1 = table.Rows[row][5].ToString();
                            item.DateD1 = dateD;
                        }
                        else
                        {
                            cl = new CheckListItems
                            {
                                Class = (table.Rows[row][1].ToString()),
                                Subject = (table.Rows[row][3].ToString()),
                                Date = dateC,
                                Section = (table.Rows[row][2].ToString()),
                                Check = Convert.ToBoolean(table.Rows[row][4]),
                                Teacher = table.Rows[row][5].ToString(),
                                DateD = dateD
                            };
                            list.Add(cl);
                        }
                    }
                }
                report.GridControl = list;
                System.Drawing.Image logo = fun.Base64ToImage(Login.Logo);
                var school = fun.GetSettings("system_title");
                report.PicIogoBox.Image = logo;
                report.xrLabel17.Text = school;
                report.LabAddress.Text = fun.GetSettings("address");
                report.LabExam.Text = txtExam.Text;
                report.LabTel.Text = "Check List Report Subject Wise";

                var info = fun.GetExamDetailIsSession(txtExam.Text, fun.GetDefaultSessionName());
                // report.labExamDate.Text = Convert.ToDateTime(info[0].Salary).ToShortDateString() + " - " + DateTime.Now.ToShortDateString();
                report.CreateDocument();
                ReportPrintTool printTool = new ReportPrintTool(report);
                printTool.PreviewForm.PrintingSystem.Document.AutoFitToPagesWidth = 1;
                printTool.ShowRibbonPreviewDialog(UserLookAndFeel.Default);
            }
            else
            if (txtReport.Text == "Teacher Wise Report")
            {
                if (classID != "")
                    classQuery = "and class.class_id in(" + classID + ")";
                if (txtSubject.EditValue != null)
                    classQuery += "and sub.id in(" + txtSubject.EditValue + ")";
                if (txtTeacher.EditValue != null)
                    classQuery += "and teacher.teacher_id in(" + txtTeacher.EditValue + ")";
                if (txtChecked.Checked == true)
                    classQuery += "and tms.check_list = 1";
                if (txtUnChecked.Checked == true)
                    classQuery += "and tms.check_list != 1";

                XtraCheckListReportTeacherWise report = new XtraCheckListReportTeacherWise();
                string query = "SELECT tms.smark_id as ID ,class.name as Class,section.name as Section,sub.name as Subject ,tms.check_list ,teacher.name as Teacher,conducted_on,submit_on,exam.name as Exam  " +
                    " from tbl_mark_subject as tms " +
                    " inner join section_subject as ss on ss.subject_id=tms.subject_id and ss.class_id = tms.class_id and ss.section_id = tms.section_id " +
                    " inner join subject_default as sub on sub.id = ss.subject_id " +
                    " inner join section on section.section_id=ss.section_id " +
                    " inner join class on class.class_id=section.class_id " +
                    " left join teacher on teacher.teacher_id=tms.teacher_id " +
                    " inner join exam on tms.exam_id=exam.exam_id " +
                    " where tms.exam_id in(" + exam_ids + ") " + classQuery + " order by conducted_on, class.name,sub.name";

                DataTable table = fun.FetchDataTable(query);
                list.Clear();
                for (int row = 0; row <= table.Rows.Count - 1; row++)
                {
                    string dateC = "";
                    if (table.Rows[row][6].ToString() != "")
                        dateC = Convert.ToDateTime(table.Rows[row][6]).ToString("dd-MM-yyyy");
                    string dateD = "";
                    if (table.Rows[row][7].ToString() != "")
                        dateD = Convert.ToDateTime(table.Rows[row][7]).ToString("dd-MM-yyyy");

                    CheckListItems cl;
                    if (row % 2 == 0)
                    {
                        cl = new CheckListItems
                        {
                            Date = dateC,
                            Class = (table.Rows[row][1].ToString()),
                            Teacher = table.Rows[row][5].ToString(),
                            Subject = (table.Rows[row][3].ToString()),
                            Section = (table.Rows[row][2].ToString()),
                            Check = Convert.ToBoolean(table.Rows[row][4]),
                            Exam = (table.Rows[row][8].ToString()),
                            DateD = dateD
                        };
                        list.Add(cl);
                    }
                    else
                    {
                        var item = list.FirstOrDefault(i => i.Exam == table.Rows[row][8].ToString() && i.Exam1 == null);
                        if (item != null)
                        {
                            item.Subject1 = (table.Rows[row][2].ToString());
                            item.Check1 = Convert.ToBoolean(table.Rows[row][4]);
                            item.Exam1 = table.Rows[row][8].ToString();
                            item.DateD1 = dateD;
                        }
                        else
                        {
                            cl = new CheckListItems
                            {
                                Date = dateC,
                                Class = (table.Rows[row][1].ToString()),
                                Teacher = table.Rows[row][5].ToString(),
                                Subject = (table.Rows[row][3].ToString()),
                                Section = (table.Rows[row][2].ToString()),
                                Exam = (table.Rows[row][8].ToString()),
                                Check = Convert.ToBoolean(table.Rows[row][4]),
                                DateD = dateD
                            };
                            list.Add(cl);
                        }
                    }
                }
                report.GridControl = list;
                System.Drawing.Image logo = fun.Base64ToImage(Login.Logo);
                var school = fun.GetSettings("system_title");
                report.PicIogoBox.Image = logo;
                report.xrLabel17.Text = school;
                report.LabAddress.Text = fun.GetSettings("address");
                report.LabExam.Text = txtSubject.Text;
                report.LabTel.Text = "Check List Report Teacher Wise";
                report.CreateDocument();
                ReportPrintTool printTool = new ReportPrintTool(report);
                printTool.PreviewForm.PrintingSystem.Document.AutoFitToPagesWidth = 1;
                printTool.ShowRibbonPreviewDialog(UserLookAndFeel.Default);
            }

        }

        private void gridView1_RowUpdated(object sender, DevExpress.XtraGrid.Views.Base.RowObjectEventArgs e)
        {
            DataRow row = gridView1.GetDataRow(gridView1.FocusedRowHandle);
            MySqlConnection con = new MySqlConnection(Login.constring);
            con.Open();
            var res1 = (row[4].ToString() == "True") ? 1 : 0;
            var res2 = (row[6].ToString() == "True") ? 1 : 0;
            string c_on = "";
            string s_on = "";
            var query = "";
            if (row[7].ToString() != "")
                c_on = ",conducted_on = '" + Convert.ToDateTime(row[7]).ToString("yyyy-MM-dd") + "'";
            if (row[8].ToString() != "")
                s_on = ",submit_on = '" + Convert.ToDateTime(row[8]).ToString("yyyy-MM-dd") + "'";
            query = "UPDATE `tbl_mark_subject` SET `check_list`='" + res1 + "', test_not_deliver='" + res2 + "'" + c_on + s_on + " where smark_id= '" + row[0] + "';";
            MySqlCommand cmd = new MySqlCommand(query, con);
            cmd.ExecuteNonQuery();
            con.Close();
        }
        private void txtclass_EditValueChanged(object sender, EventArgs e)
        {
            var classID = txtclass.EditValue.ToString();
            if (txtReport.Text == "Subject Wise Report")
            {
                txtSubject.Properties.DataSource = fun.GetAllSubject_by_class_dt(classID);
                txtSubject.Properties.DisplayMember = "name";
                txtSubject.Properties.ValueMember = "subject_id";
            }
            else if (txtReport.Text == "Section Wise Report")
            {
                txtSubject.Properties.DataSource = fun.GetAllSubject_by_class_dt(classID);
                txtSubject.Properties.DisplayMember = "name";
                txtSubject.Properties.ValueMember = "subject_id";
            }
            else if (txtReport.Text == "Teacher Wise Report")
            {
                txtSubject.Properties.DataSource = fun.GetAllSubject_by_class_dt(classID);
                txtSubject.Properties.DisplayMember = "name";
                txtSubject.Properties.ValueMember = "subject_id";
            }

        }

        private void txtReport_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void txtChecked_CheckedChanged(object sender, EventArgs e)
        {
            if (txtUnChecked.Checked == true)
                txtUnChecked.Checked = false;
        }

        private void txtUnChecked_CheckedChanged(object sender, EventArgs e)
        {
            if (txtChecked.Checked == true)
                txtChecked.Checked = false;

        }


    }
}
