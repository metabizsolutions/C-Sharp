using DevExpress.LookAndFeel;
using DevExpress.Utils;
using DevExpress.XtraGrid;
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

namespace SchoolManagementSystem.Exam
{
    public partial class AwradList : DevExpress.XtraEditors.XtraUserControl
    {
        private static AwradList _instance;

        public static AwradList instance
        {
            get
            {
                if (_instance == null)
                    _instance = new AwradList();
                return _instance;
            }
        }
        ObservableCollection<AllClass> allClass = new ObservableCollection<AllClass>();

        CommonFunctions fun = new CommonFunctions();
        int P, A = 0;
        public AwradList()
        {
            InitializeComponent();
            loadfunctions();
        }
        public void loadfunctions()
        {
            allClass.Clear();
           DataTable dt_exam = fun.GetAllExams_dt();
            txtExam.Properties.DataSource = dt_exam;
            txtExam.Properties.DisplayMember = "name";
            txtExam.Properties.ValueMember = "exam_id";

            DataTable dt_class = fun.GetAllClasses_dt();
            txtClass.Properties.DataSource = dt_class;
            txtClass.Properties.DisplayMember = "name";
            txtClass.Properties.ValueMember = "class_id";


            string subject_wise = fun.GetSettings("Institute_Type");
            if (subject_wise == "Subject Wise Institute")
            {
                btnmanag_byteacher.Visible = true;
                gridLookUpEdit_teacher.Visible = true;
                labelControl2.Visible = true;
            }
            else
            {
                btnmanag_byteacher.Visible = false;
                gridLookUpEdit_teacher.Visible = false;
                labelControl2.Visible = false;
            }

        }
        private void txtClass_EditValueChanged(object sender, EventArgs e)
        {
            if (txtClass.EditValue != null)
            {
                DataTable dt_class = fun.GetAllSection_dt(txtClass.EditValue.ToString());
                txtSection.Properties.DataSource = dt_class;
                txtSection.Properties.DisplayMember = "name";
                txtSection.Properties.ValueMember = "section_id";
            }
        }

        private void txtSection_EditValueChanged(object sender, EventArgs e)
        {
            if (txtSection.EditValue != null)
            {
                DataTable dt_sub = fun.GetAllSubject_by_section_dt(txtSection.EditValue.ToString());
                txtSubject.Properties.DataSource = dt_sub;
                txtSubject.Properties.DisplayMember = "name";
                txtSubject.Properties.ValueMember = "subject_id";
            }
        }

        DataTable table;
        DataTable exam_table;
        private void btnMMAdd_Click(object sender, EventArgs e)
        {
            load_award_list();

        }
        void load_award_list()
        {
            if (txtClass.EditValue == null || txtSection.EditValue == null)
            {
                MessageBox.Show("Fill all fields...!!", "Info");
                return;
            }
            var classID = txtClass.EditValue;
            var sectionID = txtSection.EditValue;
            awardlist(" and std.class_id='" + classID + "'and std.section_id='" + sectionID + "'");
        }
        private void btnmanag_byteacher_Click(object sender, EventArgs e)
        {
            if (gridLookUpEdit_teacher.EditValue != null && txtClass.EditValue != null && txtSection.EditValue != null)
            {
                var classID = txtClass.EditValue;
                var sectionID = txtSection.EditValue;
                awardlist(" and std.student_id in (SELECT `student_id` FROM `fee_by_subject_teacher` WHERE `teacher_id` ='" + gridLookUpEdit_teacher.EditValue + "') and std.class_id = '" + classID + "'and std.section_id = '" + sectionID + "'");
            }
        }
        void awardlist(string where)
        {
            reports.Clear();
            if (txtClass.EditValue == null || txtSection.EditValue == null || txtExam.EditValue == null || txtSubject.EditValue == null)
            {
                MessageBox.Show("Fill all fields...!!", "Info");
                return;
            }

            MySqlConnection con = new MySqlConnection(Login.constring);
            con.Open();

            //var a = txtSection.Text;
            //var info = a.Split(',');
            //foreach (var sec in info)
            //{
            exam_table = null;
            gridAwardList.BeginUpdate();

            try
            {
                var examID = txtExam.EditValue;
                var classID = txtClass.EditValue;
                var sectionID = txtSection.EditValue;
                var subjectID = txtSubject.EditValue;
                String exam_query = "SELECT tms.* ,teacher1.name AS Teacher, teacher2.name AS Incharge " +
                                    " FROM `tbl_mark_subject` AS tms " +
                                    " LEFT JOIN teacher AS teacher1 ON teacher1.teacher_id = tms.teacher_id " +
                                    " LEFT JOIN teacher AS teacher2 ON teacher2.teacher_id = tms.incharge_id " +
                                    " WHERE tms.subject_id='" + subjectID + "' and tms.exam_id='" + examID + "' and class_id = '"+classID+"' and section_id = '"+sectionID+"' ";
                exam_table = fun.FetchDataTable(exam_query);
                string query = "SELECT distinct std.student_id, std.roll as `Roll No`,concat( std.name,' / ',parent.name) as Student, " +
                    " std.section_id as Section,if(att.status = 1,'P','A') as Attnd,'' as Signature,'' as `Obt.Marks`,'' as Rechecking, tms.marks  as Total,'' as Remarks " +
                    " FROM student as std " +
                    " inner join parent on parent.parent_id=std.parent_id  " +
                    " LEFT JOIN tbl_mark_subject AS tms ON tms.`class_id` = std.`class_id` AND tms.`section_id` = std.`section_id` AND tms.`subject_id` = '" + subjectID + "' AND tms.`exam_id` = '" + examID + "'" +
                    " left join attendance as att on att.student_id=std.student_id  AND dayofmonth(att.date) = '" + DateTime.Now.Day + "' and month(att.date) = '" + DateTime.Now.Month + "' and year(att.date) = '" + DateTime.Now.Year + "' " +
                    " WHERE  1=1 " + where + " and std.exam_sms=1 AND std.passout != 1 order by std.roll";
                MySqlCommand cmdP = new MySqlCommand(query, con);
                MySqlDataAdapter adp = new MySqlDataAdapter(cmdP);
                table = new DataTable();
                adp.Fill(table);
                gridAwardList.DataSource = null;
                gridView1.Columns.Clear();
                gridAwardList.DataSource = CommonFunctions.AutoNumberedTable(table);
                
                //var col = gridView1.Columns["ID"];
                //col.OptionsColumn.ReadOnly = true;
                var col1 = gridView1.Columns["Student"];
                col1.OptionsColumn.ReadOnly = true;
                col1.Width = 200;
                var col2 = gridView1.Columns["Total"];
                col2.OptionsColumn.ReadOnly = true;
                var col3 = gridView1.Columns["Roll No"];
                col3.OptionsColumn.ReadOnly = true;
                col3.Width = 45;
                var col4 = gridView1.Columns["Section"];
                col4.Visible = false;
                gridView1.Columns["student_id"].Visible= false;
                if (table.Rows.Count > 0)
                {
                    GridColumn Column = gridView1.Columns["Roll No"];
                    Column.SortOrder = DevExpress.Data.ColumnSortOrder.Ascending;
                    Column.SortOrder = 0;
                    //Column.SortMode = ColumnSortMode.DisplayText;
                    Column.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
                    GridColumn Column3 = gridView1.Columns["Sr#"];
                    //Column3.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
                    var col5 = gridView1.Columns["Sr#"];
                    col5.Width = 40;
                    GridColumn Column1 = gridView1.Columns["Attnd"];
                    Column1.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
                }
                GridColumnSummaryItem item1 = new GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Custom, "Attnd", "{0}", tag: "Attnd");
                gridView1.Columns["Attnd"].Summary.Add(item1);
                gridView1.BestFitColumns();
            }
            finally
            {
                gridAwardList.EndUpdate();
            }
        }
        XtraAwardListReport report;
        private List<XtraAwardListReport> reports = new List<XtraAwardListReport>();
        public void AppendReport(XtraAwardListReport report)
        {
            reports.Add(report);
        }
        public XtraAwardListReport CreateReport()
        {
            XtraAwardListReport output = new XtraAwardListReport();
            //output.CreateDocument(false);
            foreach (var report in reports)
            {
                report.CreateDocument(false);
                output.Pages.AddRange(report.Pages);
            }
            if (output == null) output = new XtraAwardListReport();
            output.PrintingSystem.ContinuousPageNumbering = true;
            return output;
        }
        GridControl showReport(string type = "print")
        {
            #region Doubleside Award Lisy
            GridControl awardlist = new GridControl();
            DataTable orignalawardlist = gridAwardList.DataSource as DataTable;
            if (type == "excel")
            {
                DataTable dtlist = new DataTable();
                dtlist.Columns.Add("student_id");
                dtlist.Columns.Add("Student");
                dtlist.Columns.Add("Roll No");
                dtlist.Columns.Add("Class");
                dtlist.Columns.Add("Section");
                dtlist.Columns.Add("Subject");
                dtlist.Columns.Add("Attnd");
                dtlist.Columns.Add("Exam");
                dtlist.Columns.Add("Obtain Marks");
                dtlist.Columns.Add("Total");
                for (int i = 0; i < orignalawardlist.Rows.Count; i++)
                {
                    DataRow row = dtlist.NewRow();
                    row["student_id"] = orignalawardlist.Rows[i]["student_id"];
                    row["Student"] = orignalawardlist.Rows[i]["Student"];
                    row["Roll No"] = orignalawardlist.Rows[i]["Roll No"];
                    row["Class"] = txtClass.Text;
                    row["Section"] = txtSection.Text;
                    row["Subject"] = txtSubject.Text;
                    row["Attnd"] = orignalawardlist.Rows[i]["Attnd"];
                    row["Exam"] = txtExam.Text;
                    row["Obtain Marks"] = orignalawardlist.Rows[i]["Obt.Marks"];
                    row["Total"] = orignalawardlist.Rows[i]["Total"];
                    dtlist.Rows.Add(row);
                }
                gridAwardList.BeginUpdate();
                try
                {
                    gridAwardList.DataSource = null;
                    gridView1.Columns.Clear();
                    gridAwardList.DataSource = dtlist;
                }
                finally
                {
                    gridAwardList.EndUpdate();
                }
            }
            else if (Convert.ToBoolean(btntoggleReport.EditValue))
            {
                DataTable dtlist = new DataTable();
                dtlist.Columns.Add("Sr#");
                dtlist.Columns.Add("Roll No");
                dtlist.Columns.Add("Student");
                dtlist.Columns.Add("Obt.Marks");
                if (gridView1.Columns["Signature"].Visible)
                    dtlist.Columns.Add("Signature");
                dtlist.Columns.Add("Sr#.");
                dtlist.Columns.Add("Roll No.");
                dtlist.Columns.Add("Student.");
                dtlist.Columns.Add("Obt.Marks.");
                if (gridView1.Columns["Signature"].Visible)
                    dtlist.Columns.Add("Signature.");
                int count = 0;
                int wb = orignalawardlist.Rows.Count / 2;
                if (orignalawardlist.Rows.Count % 2 != 0)
                    wb += 1;
                for (int i = 0; i < orignalawardlist.Rows.Count; i++)
                {
                    count += 1;
                    if (count <= wb)
                    {
                        DataRow row = dtlist.NewRow();
                        row["Sr#"] = orignalawardlist.Rows[i]["Sr#"];
                        row["Roll No"] = orignalawardlist.Rows[i]["Roll No"];
                        row["Student"] = orignalawardlist.Rows[i]["Student"];
                        row["Obt.Marks"] = orignalawardlist.Rows[i]["Obt.Marks"];
                        if (gridView1.Columns["Signature"].Visible)
                            row["Signature"] = orignalawardlist.Rows[i]["Signature"];
                        dtlist.Rows.Add(row);

                    }
                    else
                    {
                        for (int j = 0; j < dtlist.Rows.Count; j++)
                        {
                            if (dtlist.Rows[j]["Sr#."].ToString() == "")
                            {
                                dtlist.Rows[j]["Sr#."] = orignalawardlist.Rows[i]["Sr#"];
                                dtlist.Rows[j]["Roll No."] = orignalawardlist.Rows[i]["Roll No"];
                                dtlist.Rows[j]["Student."] = orignalawardlist.Rows[i]["Student"];
                                dtlist.Rows[0]["Obt.Marks."] = orignalawardlist.Rows[i]["Obt.Marks"];
                                if (gridView1.Columns["Signature"].Visible)
                                    dtlist.Rows[0]["Signature."] = orignalawardlist.Rows[i]["Signature"];
                                break;
                            }
                        }
                    }

                }
                
                awardlist.DataSource = dtlist;
                var view = new GridView(awardlist);
                awardlist.MainView = view;
                view.PopulateColumns(dtlist);
                view.BestFitColumns();
                view.Columns["Sr#"].Width = gridView1.Columns["Sr#"].VisibleWidth;
                view.Columns["Sr#."].Width = gridView1.Columns["Sr#"].VisibleWidth;
                view.Columns["Roll No"].AppearanceCell.TextOptions.WordWrap = WordWrap.Wrap;
                view.Columns["Roll No."].AppearanceCell.TextOptions.WordWrap = WordWrap.Wrap;
                view.Columns["Roll No"].Width = gridView1.Columns["Roll No"].VisibleWidth;
                view.Columns["Roll No."].Width = gridView1.Columns["Roll No"].VisibleWidth;
                view.Columns["Student"].Width = gridView1.Columns["Student"].VisibleWidth;
                view.Columns["Student"].AppearanceCell.TextOptions.WordWrap = WordWrap.Wrap;
                view.Columns["Student."].Width = gridView1.Columns["Student"].VisibleWidth;
                view.Columns["Student."].AppearanceCell.TextOptions.WordWrap = WordWrap.Wrap;
                view.Columns["Obt.Marks"].Width = gridView1.Columns["Obt.Marks"].VisibleWidth;
                view.Columns["Obt.Marks."].Width = gridView1.Columns["Obt.Marks"].VisibleWidth;
                if (gridView1.Columns["Signature"].Visible)
                {
                    view.Columns["Signature"].Width = gridView1.Columns["Signature"].VisibleWidth;
                    view.Columns["Signature."].Width = gridView1.Columns["Signature"].VisibleWidth;
                }
            }
            #endregion

            
            if (Convert.ToBoolean(btntoggleReport.EditValue))
                return awardlist; // half murged
            else //if (!Convert.ToBoolean(btntoggleReport.EditValue))
                return gridAwardList; //full
            
        }
        private void btn_excel_Click(object sender, EventArgs e)
        {
            GridControl gc = new GridControl();
            gc = showReport("excel");
            SaveFileDialog saveFileDialog1 = new SaveFileDialog();
            saveFileDialog1.Filter = "Excel Files (*.xlsx)| *.xlsx";
            saveFileDialog1.Title = "Save an ExcelFile";
            saveFileDialog1.ShowDialog();
            if (saveFileDialog1.FileName != "")
            {
                System.IO.FileStream fs = (System.IO.FileStream)saveFileDialog1.OpenFile();
                gc.ExportToXlsx(fs);
                fs.Close();
                load_award_list();
            }
        }
        private void btnMMPrintP_Click(object sender, EventArgs e)
        {
            report = new XtraAwardListReport();
            report.GridControl = showReport();
            Image logo = fun.Base64ToImage(Login.Logo);
            var school = fun.GetSettings("system_title");
            report.labClass.Text = txtClass.Text;
            report.LabSection.Text = txtSection.Text;
            report.PicIogoBox.Image = logo;
            report.LabTitle.Text = school;
            report.LabAddress.Text = fun.GetSettings("address");

            report.LabSubject.Text = txtSubject.Text;
            report.labExam.Text = txtExam.Text;
            //report.LabDate.Text = dateEdit1.DateTime.ToShortDateString();
            report.LabPresent.Text = P.ToString();
            report.LabAbsent.Text = A.ToString();
            report.LabTotal.Text = (A + P).ToString();
            if (exam_table.Rows.Count > 0)
            {
                DataRow row = exam_table.Rows[0];
                report.LabelIncharge.Text = row["Incharge"].ToString();
                report.LabelTeacher.Text = row["Teacher"].ToString();
                report.LabelRoom.Text = row["room"].ToString();
                report.LabDate.Text = row["conducted_on"].ToString();
            }
            using (ReportPrintTool printTool = new ReportPrintTool(report))
            {
                printTool.ShowRibbonPreviewDialog();
                printTool.ShowRibbonPreview(UserLookAndFeel.Default);
            }
        }
        int validRowP, validRowA = 0;

        private void txtSubject_EditValueChanged(object sender, EventArgs e)
        {
            if (txtSubject.EditValue != null)
            {
                string query = "SELECT `teacher_id`,name FROM `teacher` WHERE `subject_code` ='" + txtSubject.Text + "'";
                gridLookUpEdit_teacher.Properties.DataSource = fun.FetchDataTable(query);
                gridLookUpEdit_teacher.Properties.DisplayMember = "name";
                gridLookUpEdit_teacher.Properties.ValueMember = "teacher_id";
            }
        }

        

        
        private void gridView1_CustomSummaryCalculate(object sender, DevExpress.Data.CustomSummaryEventArgs e)
        {
            GridColumnSummaryItem item = e.Item as GridColumnSummaryItem;
            GridView view = sender as GridView;
            if (Equals("Attnd", item.Tag))
            {
                if (e.SummaryProcess == DevExpress.Data.CustomSummaryProcess.Start)
                {
                    validRowP = 0; validRowA = 0;
                }
                if (e.SummaryProcess == DevExpress.Data.CustomSummaryProcess.Calculate)
                {
                    var a = view.GetRowCellValue(e.RowHandle, "Attnd").ToString();
                    if (a == "P")
                        validRowP++;
                    else
                        validRowA++;
                }
                if (e.SummaryProcess == DevExpress.Data.CustomSummaryProcess.Finalize)
                {
                    A = validRowA;
                    P = validRowP;
                }
            }
        }
    }
}
