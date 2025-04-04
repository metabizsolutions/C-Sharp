using DevExpress.LookAndFeel;
using DevExpress.XtraEditors;
using DevExpress.XtraGrid;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraPrinting;
using DevExpress.XtraReports.UI;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Drawing;
using System.Drawing.Printing;
using System.Windows.Forms;

namespace SchoolManagementSystem.Exam
{
    public partial class ResultSummary : XtraUserControl
    {
        private static ResultSummary _instance;

        public static ResultSummary instance
        {
            get
            {
                if (_instance == null)
                    _instance = new ResultSummary();
                return _instance;
            }
        }
        ObservableCollection<AllClass> allClass = new ObservableCollection<AllClass>();
        // ObservableCollection<Result> resultclass = new ObservableCollection<Result>();

        CommonFunctions fun = new CommonFunctions();
        public ResultSummary()
        {
            InitializeComponent();
            loadfunctions();

        }
        public void loadfunctions()
        {
            String examSQL = "SELECT exam_id,`name` FROM exam ORDER BY exam_id DESC";
            DropdownExam.Properties.DataSource = fun.FetchDataTable(examSQL);
            DropdownExam.Properties.DisplayMember = "name";
            DropdownExam.Properties.ValueMember = "exam_id";
        }

        ObservableCollection<string> s = new ObservableCollection<string>();
        public void TeacherSummary(String wise)
        {
            String where = "", group_by = "";
            gridClassResult.DataSource = null;
            gridView1.Columns.Clear();
            gridView1.Columns.Clear();
            gridView1.GroupSummary.Clear();

            if (txtFrom.Text != "")
            {
                String toFrom = txtFrom.DateTime.ToString("yyyy-MM-dd");
                where += " AND exam.date >= '" + toFrom + "'";
            }
            if (txtTo.Text != "")
            {
                String toDate = txtTo.DateTime.ToString("yyyy-MM-dd");
                where += " AND exam.date <= '" + toDate + "'";
            }
            if(wise == "section")
            {
                group_by = " , mark.section_id ";
            } else if(wise == "class")
            {
                group_by = " , mark.subject_id ";
            }
            
            String query = "SELECT *,FLOOR((obtain/total*100)) AS percent FROM( " +
                            " SELECT teacher.teacher_id, teacher.name,  class.name AS class, section.name AS section,"+
                            " sum(case when tms.marks <= 0 then 0 when mark.mark_obtained = -1 then 0 when mark.mark_obtained = -2 then 0 else mark.mark_obtained end) AS obtain," +
                            " sum(tms.marks) AS total " +
                            " FROM teacher " +
                            " INNER JOIN tbl_mark_subject AS tms ON tms.teacher_id = teacher.teacher_id " +
                            " INNER JOIN mark ON mark.subject_id = tms.subject_id AND mark.exam_id = tms.exam_id " +
                            " INNER JOIN exam ON exam.exam_id = tms.exam_id " +
                            " INNER JOIN class ON class.class_id = mark.class_id " +
                            " INNER JOIN section ON section.section_id = mark.section_id " +
                            " WHERE 1 = 1 " + where+
                            " GROUP BY teacher.teacher_id "+group_by+"" +
                          " ) AS tb";
            DataTable table = fun.FetchDataTable(query);
            gridView1.Columns.Clear();
            gridClassResult.DataSource = null;
            gridClassResult.DataSource = table;
            gridView1.IndicatorWidth = 30;
            gridView1.OptionsView.AllowHtmlDrawHeaders = true;
            gridView1.OptionsView.ShowFooter = true;
            gridView1.Appearance.HeaderPanel.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            gridView1.ColumnPanelRowHeight = 40;
            gridView1.BestFitColumns();

            gridView1.GroupSummary.Add(DevExpress.Data.SummaryItemType.Sum, "obtain", gridView1.Columns["obtain"]);
            gridView1.GroupSummary.Add(DevExpress.Data.SummaryItemType.Sum, "total", gridView1.Columns["total"]);
            gridView1.GroupSummary.Add(DevExpress.Data.SummaryItemType.Average, "percent", gridView1.Columns["percent"]);
           
            gridView1.Columns["name"].Group();
            gridView1.ExpandAllGroups();
           
        }
        public void OverAllResultPercent()
        {
            String where = "", exam_join = "";
            gridClassResult.DataSource = null;
            gridView1.Columns.Clear();
            gridView1.GroupSummary.Clear();
            if (txtFrom.Text != "")
            {
                exam_join = " INNER JOIN exam ON exam.exam_id = mark.exam_id ";
                String toFrom = txtFrom.DateTime.ToString("yyyy-MM-dd");
                where += " AND exam.date >= '" + toFrom + "'";
            }
            if (txtTo.Text != "")
            {
                exam_join = " INNER JOIN exam ON exam.exam_id = mark.exam_id ";
                String toDate = txtTo.DateTime.ToString("yyyy-MM-dd");
                where += " AND exam.date <= '" + toDate + "'";
            }
            String query = "SELECT class.name AS class, section.name AS section, tbl.obtain, tbl.total,FLOOR((obtain/total*100)) AS percent  " + 
                            " FROM( "+ 
                            " SELECT student.class_id, student.section_id, "+
                            " SUM(CASE WHEN tms.marks <= 0 THEN 0 WHEN mark.mark_obtained = -1 THEN 0 WHEN mark.mark_obtained = -2 THEN 0 ELSE mark.mark_obtained END) AS obtain,  " +
                            " SUM(tms.marks) AS total "+
                            " FROM mark "+ exam_join+
                            " INNER JOIN student ON student.student_id = mark.student_id " + 
                            " INNER JOIN tbl_mark_subject AS tms ON tms.subject_id = mark.subject_id AND tms.exam_id = mark.exam_id "+
                            " WHERE 1 = 1 AND student.passout = 0  " + where +
                            " GROUP BY student.class_id, student.section_id " + 
                            " ) AS tbl " + 
                            " INNER JOIN class ON class.class_id = tbl.class_id " + 
                            " INNER JOIN section ON section.section_id = tbl.section_id "+ 
                            " ORDER BY percent DESC  ";
            DataTable table = fun.FetchDataTable(query);
            gridView1.Columns.Clear();
            gridClassResult.DataSource = null;
            gridClassResult.DataSource = table;
            gridView1.IndicatorWidth = 30;
            gridView1.OptionsView.AllowHtmlDrawHeaders = true;
            gridView1.OptionsView.ShowFooter = true;
            gridView1.Appearance.HeaderPanel.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            gridView1.ColumnPanelRowHeight = 40;
            gridView1.BestFitColumns();

            gridView1.GroupSummary.Add(DevExpress.Data.SummaryItemType.Count, string.Empty);
            gridView1.GroupSummary.Add(DevExpress.Data.SummaryItemType.Average, "percent");

            gridView1.Columns["class"].Group();
            gridView1.ExpandAllGroups();
            

        }
        public void CampusFailure()
        {
            gridClassResult.DataSource = null;
            gridView1.Columns.Clear();

            int FailMarks = fun.GetFailMarks();
            if (DropdownExam.EditValue == null)
            {
                MessageBox.Show("Exam need to be selected!");
                return;
            }
            String exam_id = DropdownExam.EditValue.ToString();
            String query = "SELECT tbl.student_id,tbl.roll, tbl.name, class.name AS class, section.name AS section, GROUP_CONCAT(sub.name) AS subjects "+
                            " FROM( "+
                            " SELECT std.student_id, std.roll, std.name, std.class_id, std.section_id, m.subject_id, " +
                            " FLOOR((IF(tms.marks >= 0, m.mark_obtained, 0) / IF(tms.marks >= 0, tms.marks, 0)) * 100) AS percent " +
                            " FROM student as std " +
                            " LEFT JOIN mark as m ON std.student_id = m.student_id " +
                            " left JOIN tbl_mark_subject AS tms ON tms.`class_id` = m.`class_id` AND tms.`section_id` = m.`section_id` AND tms.`subject_id` = m.`subject_id` AND tms.`exam_id` = m.`exam_id`  " +
                            " WHERE m.exam_id in ({0}) AND std.passout = 0 ) AS tbl " +
                            " INNER JOIN class ON class.class_id = tbl.class_id " +
                            " INNER JOIN section ON section.section_id = tbl.section_id " +
                            " INNER JOIN subject_default as sub ON sub.id = tbl.subject_id " +
                            " WHERE tbl.percent <= {1}  " +
                            " GROUP BY tbl.student_id ";
            query = String.Format(query, exam_id, FailMarks);
            DataTable table = fun.FetchDataTable(query);

            gridView1.Columns.Clear();
            gridClassResult.DataSource = null;
            gridClassResult.DataSource = table;
            gridView1.IndicatorWidth = 30;
            gridView1.OptionsView.AllowHtmlDrawHeaders = true;
            gridView1.OptionsView.ShowFooter = true;
            gridView1.Appearance.HeaderPanel.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            gridView1.ColumnPanelRowHeight = 40;
            gridView1.BestFitColumns();
            gridView1.GroupSummary.Add(DevExpress.Data.SummaryItemType.Count, string.Empty);

        }
        public void SubjectFailure() {
            gridClassResult.DataSource = null;
            gridView1.Columns.Clear();
            gridView1.GroupSummary.Clear();

            int FailMarks = fun.GetFailMarks();
            if (DropdownExam.EditValue == null)
            {
                MessageBox.Show("Exam need to be selected!");
                return;
            }
            String exam_id = DropdownExam.EditValue.ToString();
            String query = "SELECT tbl.student_id,tbl.roll, tbl.name, class.name AS class, section.name AS section, sub.name AS subjects " +
                            " FROM( " +
                            " SELECT std.student_id, std.roll, std.name, std.class_id, std.section_id, m.subject_id, " +
                            " FLOOR((IF(tms.marks >= 0, m.mark_obtained, 0) / IF(tms.marks >= 0, tms.marks, 0)) * 100) AS percent " +
                            " FROM student as std " +
                            " LEFT JOIN mark as m ON std.student_id = m.student_id " +
                            " left JOIN tbl_mark_subject AS tms ON tms.`class_id` = m.`class_id` AND tms.`section_id` = m.`section_id` AND tms.`subject_id` = m.`subject_id` AND tms.`exam_id` = m.`exam_id`  " +
                            " WHERE m.exam_id in ({0}) AND std.passout = 0 ) AS tbl " +
                            " INNER JOIN class ON class.class_id = tbl.class_id " +
                            " INNER JOIN section ON section.section_id = tbl.section_id " +
                            " INNER JOIN subject_default as sub ON sub.id = tbl.subject_id " +
                            " WHERE tbl.percent <= {1}  " +
                            " GROUP BY tbl.student_id ";
            query = String.Format(query, exam_id, FailMarks);
            DataTable table = fun.FetchDataTable(query);


            gridView1.Columns.Clear();
            gridClassResult.DataSource = null;
            gridClassResult.DataSource = table;
            gridView1.IndicatorWidth = 30;
            gridView1.OptionsView.AllowHtmlDrawHeaders = true;
            gridView1.OptionsView.ShowFooter = true;
            gridView1.Appearance.HeaderPanel.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            gridView1.ColumnPanelRowHeight = 40;
            gridView1.BestFitColumns();

            gridView1.GroupSummary.Add(DevExpress.Data.SummaryItemType.Count, string.Empty);
            gridView1.Columns["subjects"].Group();
            gridView1.ExpandAllGroups();
        }
        public void StudentFailure()
        {
            gridClassResult.DataSource = null;
            gridView1.Columns.Clear();
            gridView1.GroupSummary.Clear();

            int FailMarks = fun.GetFailMarks();
            if (DropdownExam.EditValue == null)
            {
                MessageBox.Show("Exam need to be selected!");
                return;
            }
            String exam_id = DropdownExam.EditValue.ToString();
            String query = "SELECT tbl.student_id,tbl.roll, tbl.name, class.name AS class, section.name AS section, COUNT(tbl.subject_id) AS subject_count,GROUP_CONCAT(sub.name) AS subjects " +
                            " FROM( " +
                            " SELECT std.student_id, std.roll, std.name, std.class_id, std.section_id, m.subject_id, " +
                            " FLOOR((IF(tms.marks >= 0, m.mark_obtained, 0) / IF(tms.marks >= 0, tms.marks, 0)) * 100) AS percent " +
                            " FROM student as std " +
                            " LEFT JOIN mark as m ON std.student_id = m.student_id " +
                            " left JOIN tbl_mark_subject AS tms ON tms.`class_id` = m.`class_id` AND tms.`section_id` = m.`section_id` AND tms.`subject_id` = m.`subject_id` AND tms.`exam_id` = m.`exam_id`  " +
                            " WHERE m.exam_id in ({0}) AND std.passout = 0 ) AS tbl " +
                            " INNER JOIN class ON class.class_id = tbl.class_id " +
                            " INNER JOIN section ON section.section_id = tbl.section_id " +
                            " INNER JOIN subject_default as sub ON sub.id = tbl.subject_id " +
                            " WHERE tbl.percent <= {1} " +
                            " GROUP BY tbl.student_id ";
            query = String.Format(query, exam_id, FailMarks);
            DataTable table = fun.FetchDataTable(query);


            gridView1.Columns.Clear();
            gridClassResult.DataSource = null;
            gridClassResult.DataSource = table;
            gridView1.IndicatorWidth = 30;
            gridView1.OptionsView.AllowHtmlDrawHeaders = true;
            gridView1.OptionsView.ShowFooter = true;
            gridView1.Appearance.HeaderPanel.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            gridView1.ColumnPanelRowHeight = 40;
            gridView1.BestFitColumns();

            gridView1.GroupSummary.Add(DevExpress.Data.SummaryItemType.Count, string.Empty);
            gridView1.Columns["subject_count"].Group();
            gridView1.ExpandAllGroups();
        }
        private void ClassTopper(String wise) {
            gridClassResult.DataSource = null;
            gridView1.Columns.Clear();
            gridView1.GroupSummary.Clear();
            
            if(DropdownExam.EditValue == null)
            {
                MessageBox.Show("Exam need to be selected!");
                return;
            }
            String limitnumber = txtNumber.Text;
            String exam_id = DropdownExam.EditValue.ToString();
            String query = " SELECT m.student_id,std.roll,std.name,std.sex AS gender,class.name AS class,section.name AS section, " +
                            " SUM(IF(m.mark_obtained >= 0, m.mark_obtained, 0)) AS obtain, SUM(tms.marks) AS total, " +
                            " FORMAT((100 * (SUM(IF(m.mark_obtained >= 0, m.mark_obtained, 0)) / SUM(tms.marks))),2) AS percent, " +
                            " class.class_id,section.section_id" +
                            " FROM mark as m " +
                            " INNER JOIN student as std ON m.student_id = std.student_id " +
                            " left JOIN tbl_mark_subject AS tms ON tms.`class_id` = m.`class_id` AND tms.`section_id` = m.`section_id` AND tms.`subject_id` = m.`subject_id` AND tms.`exam_id` = m.`exam_id`  " +
                            " INNER JOIN class ON class.class_id = std.class_id " +
                            " INNER JOIN section ON section.section_id = std.section_id " +
                            " WHERE m.exam_id in ({0}) AND std.passout = 0 " +
                            " GROUP BY std.student_id,std.class_id " +
                            " ORDER BY (100 * (SUM(IF(m.mark_obtained >= 0, m.mark_obtained, 0)) / SUM(tms.marks))) DESC ";
            query = String.Format(query, exam_id);
            if (wise == "class")
            {
                query = " SET @num := 0, @user_id := '',@pre_percent = 0;" +
                        " SELECT student_id,roll,`name`,gender,class,section,obtain,total,percent,`rank` " +
                        " FROM (" +
                        " SELECT tbl.*,@num:= IF(@user_id = class_id, if(@pre_percent = percent,@num,@num + 1), 1) AS `rank`,@user_id := class_id AS dummy,@pre_percent := percent as dummy2 FROM (" + query + ") AS tbl WHERE 1 = 1 ORDER BY class_id,obtain DESC " +
                        " ) AS tbl2  " +
                        " WHERE `rank` <=  " + limitnumber;
            }
            else if (wise == "section")
            {
                query = " SET @num := 0, @user_id := '',@pre_percent = 0;" +
                        " SELECT student_id,roll,`name`,gender,class,section,obtain,total,percent,`rank` " +
                        " FROM (" +
                        " SELECT tbl.*,@num:= IF(@user_id = section_id, if(@pre_percent = percent,@num,@num + 1), 1) AS `rank`,@user_id := section_id AS dummy,@pre_percent := percent as dummy2 FROM (" + query + ") AS tbl WHERE 1 = 1 ORDER BY section_id,obtain DESC " +
                        " ) AS tbl2  " +
                        " WHERE `rank` <=  " + limitnumber;
            }
            else
            {
                query = query.Replace("ORDER BY", " ORDER BY std.sex desc, ");
                query = " SET @num := 0, @user_id := '',@pre_percent = 0;" +
                        " SELECT student_id,roll,`name`,gender,class,section,obtain,total,percent,`rank` " +
                        " FROM (" +
                        " SELECT tbl.*,@num:= IF(@user_id = gender COLLATE utf8_unicode_ci, if(@pre_percent = percent,@num,@num + 1), 1) AS `rank`,@user_id := gender AS dummy,@pre_percent := percent as dummy2 FROM (" + query + ") AS tbl WHERE 1 = 1 ORDER BY gender,obtain DESC " +
                        " ) AS tbl2  " +
                        " WHERE `rank` <=  " + limitnumber;
            }
            DataTable table = fun.FetchDataTable(query);
            gridView1.Columns.Clear();
            gridClassResult.DataSource = null;
            gridClassResult.DataSource = table;
            gridView1.IndicatorWidth = 30;
            gridView1.OptionsView.AllowHtmlDrawHeaders = true;
            gridView1.OptionsView.ShowFooter = true;
            gridView1.Appearance.HeaderPanel.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            gridView1.ColumnPanelRowHeight = 40;
            gridView1.BestFitColumns();


            gridView1.GroupSummary.Add(DevExpress.Data.SummaryItemType.Count, string.Empty);
            gridView1.GroupSummary.Add(DevExpress.Data.SummaryItemType.Average, "percent");
            if (wise == "class")
            {
                gridView1.Columns["class"].Group();
                gridView1.ExpandAllGroups();
            }
            else if(wise == "section")
            {
                gridView1.Columns["class"].Group();
                gridView1.Columns["section"].Group();
                gridView1.ExpandAllGroups();
            }
            else
            {
                gridView1.Columns["gender"].Group();
                gridView1.ExpandAllGroups();
            }
        }

        private void GradeComparison()
        {
            gridClassResult.DataSource = null;
            gridView1.Columns.Clear();
            gridView1.GroupSummary.Clear();
            if (DropdownExam.EditValue == null)
            {
                MessageBox.Show("Exam need to be selected!");
                return;
            }
            String exam_id = DropdownExam.EditValue.ToString().Replace(" ","");
            Dictionary<string, string> dic = new Dictionary<string, string>();
            dic.Add(exam_id, "para_exam_id");
            DataTable table = fun.FetchDataTable_storeproc("grade_comparision",dic);
            /*String query = "SELECT tbl.*,FLOOR((obtain/total*100)) AS percent, (SELECT CONCAT(`name`,'(',mark_from,'-',mark_upto,')') FROM grade WHERE FLOOR((obtain/total*100)) BETWEEN mark_from AND mark_upto) AS grade " +
                            " FROM "+
                            " (SELECT mark.student_id, student.roll, student.name, student.sex AS gender,class.name AS class,section.name AS section,  " +
                            " SUM(IF(mark.mark_obtained >= 0, mark.mark_obtained, 0)) AS obtain, SUM(tms.marks) AS total, " +
                            " IFNULL(FLOOR(100 * (SUM(IF(mark.mark_obtained >= 0, mark.mark_obtained, 0)) / SUM(tms.marks))), 0) AS percent " +
                            " FROM mark " +
                            " INNER JOIN student ON student.`student_id` = mark.`student_id` AND student.`class_id` = mark.`class_id` AND student.`section_id` = mark.`section_id` " +
                            " INNER JOIN tbl_mark_subject AS tms ON tms.subject_id = mark.subject_id AND tms.exam_id = mark.exam_id " +
                            " INNER JOIN class ON class.class_id = student.class_id " +
                            " INNER JOIN section ON section.section_id = student.section_id " +
                            " WHERE mark.exam_id in ({0}) AND student.passout = 0 " +
                            " GROUP BY mark.student_id ) AS tbl " +
                            " ORDER BY percent DESC ";
            query = String.Format(query, exam_id);
            DataTable table = fun.FetchDataTable(query);*/
            gridView1.Columns.Clear();
            gridClassResult.DataSource = null;
            gridClassResult.DataSource = table;
            gridView1.IndicatorWidth = 30;
            gridView1.OptionsView.AllowHtmlDrawHeaders = true;
            gridView1.OptionsView.ShowFooter = true;
            gridView1.Appearance.HeaderPanel.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            gridView1.ColumnPanelRowHeight = 40;
            gridView1.BestFitColumns();


            gridView1.GroupSummary.Add(DevExpress.Data.SummaryItemType.Count, string.Empty);
           
            gridView1.Columns["class"].Group();
            gridView1.Columns["section"].Group();
            gridView1.Columns["grade"].Group();
            gridView1.ExpandAllGroups();
            
        }


        private void ClassResult_Enter(object sender, System.EventArgs e)
        {
            loadfunctions();
        }
        string examDate = "";
        private void simpleButton1_Click(object sender, System.EventArgs e)
        {
            showReport();
            // PreviewPrintableComponent(gridClassResult, gridClassResult.LookAndFeel);
            // gridClassResult.ShowRibbonPrintPreview();
            //var examInfo = fun.GetExamDetailIsSession(txtExam.Text, Main_FD.SelectedSession);
            // examDate = examInfo[0].Salary;
        }

        void PreviewPrintableComponent(IPrintable component, UserLookAndFeel lookAndFeel)
        {
            PrintableComponentLink link = new PrintableComponentLink()
            {
                PrintingSystemBase = new PrintingSystemBase(),
                Component = component,
                Landscape = false,
                PaperKind = PaperKind.A4,
                Margins = new Margins(20, 20, 20, 20)
            };
            // Create a link that will print a control. 
            //... 
            // Subscribe to the CreateReportHeaderArea event used to generate the report header. 
            link.CreateReportHeaderArea += link_CreateReportHeaderArea;
            // Show the report. 
            link.ShowRibbonPreview(lookAndFeel);
        }

        private void link_CreateReportHeaderArea(object sender, CreateAreaEventArgs e)
        {
            Image logo = fun.Base64ToImage(Login.Logo);
            var school = fun.GetSettings("system_title");

            string reportHeader = "                                                                   " + school + "\n\r\n\r"
                + "                  " + "Session: " + Main_FD.SelectedSession
                + "                  " + "       Examination Date:  " + examDate;
            e.Graph.StringFormat = new BrickStringFormat(StringAlignment.Near);
            e.Graph.Font = new Font("Tahoma", 11, FontStyle.Regular);
            RectangleF rec = new RectangleF(0, 5, e.Graph.ClientPageSize.Width, 90);
            e.Graph.DrawString(reportHeader, Color.Black, rec, BorderSide.None);
            RectangleF recI = new RectangleF(5, 7, 70, 70);
            e.Graph.DrawImage(logo, recI, BorderSide.None, Color.Transparent);
        }

        private void simpleButton2_Click(object sender, System.EventArgs e)
        {
            SaveFileDialog saveFileDialog1 = new SaveFileDialog();
            saveFileDialog1.Filter = "Excel Files (*.xlsx)| *.xlsx";
            saveFileDialog1.Title = "Save an ExcelFile";
            saveFileDialog1.ShowDialog();
            if (saveFileDialog1.FileName != "")
            {
                System.IO.FileStream fs =
                    (System.IO.FileStream)saveFileDialog1.OpenFile();
                gridClassResult.ExportToXlsx(fs);
                fs.Close();
            }
        }

        private void btnExportPdf_Click(object sender, System.EventArgs e)
        {
            SaveFileDialog saveFileDialog1 = new SaveFileDialog();
            saveFileDialog1.Filter = "Pdf Files (*.pdf)| *.pdf";
            saveFileDialog1.Title = "Save an PDF File";
            saveFileDialog1.ShowDialog();
            if (saveFileDialog1.FileName != "")
            {
                System.IO.FileStream fs =
                    (System.IO.FileStream)saveFileDialog1.OpenFile();
                gridClassResult.ExportToPdf(fs);
                fs.Close();
            }
        }
        int validRowCount = 0;
        double total = 0;
        double result2 = 0;
        private void gridView1_CustomSummaryCalculate(object sender, DevExpress.Data.CustomSummaryEventArgs e)
        {
            GridGroupSummaryItem item = e.Item as GridGroupSummaryItem;
            GridView view = sender as GridView;
            if (Equals("Pass", item.Tag))
            {

                if (e.SummaryProcess == DevExpress.Data.CustomSummaryProcess.Start)
                {
                    validRowCount = 0;
                }
                if (e.SummaryProcess == DevExpress.Data.CustomSummaryProcess.Calculate)
                {
                    var a = view.GetRowCellValue(e.RowHandle, "Result").ToString();
                    if (a == "Pass")
                    {
                        validRowCount++;

                    }
                    //var s = Convert.ToBoolean(view.GetRowCellValue(e.RowHandle, "status"));
                    //if (Convert.ToBoolean(view.GetRowCellValue(e.RowHandle, "Status")))
                    //    validRowCount++;
                }
                if (e.SummaryProcess == DevExpress.Data.CustomSummaryProcess.Finalize)
                    e.TotalValue = validRowCount;
            }
            if (Equals("Fail", item.Tag))
            {
                if (e.SummaryProcess == DevExpress.Data.CustomSummaryProcess.Start)
                {
                    validRowCount = 0;
                }
                if (e.SummaryProcess == DevExpress.Data.CustomSummaryProcess.Calculate)
                {
                    var a = view.GetRowCellValue(e.RowHandle, "Result").ToString();
                    if (a == "Fail")
                        validRowCount++;
                }
                if (e.SummaryProcess == DevExpress.Data.CustomSummaryProcess.Finalize)
                    e.TotalValue = validRowCount;

            }
            if (Equals("Age", item.Tag))
            {
                if (e.SummaryProcess == DevExpress.Data.CustomSummaryProcess.Start)
                {
                    validRowCount = 0; total = 0; result2 = 0;
                }
                if (e.SummaryProcess == DevExpress.Data.CustomSummaryProcess.Calculate)
                {
                    total++;
                    var a = view.GetRowCellValue(e.RowHandle, "Result").ToString();
                    if (a == "Pass")
                        validRowCount++;
                    result2 = (validRowCount / total) * 100;

                }
                if (e.SummaryProcess == DevExpress.Data.CustomSummaryProcess.Finalize)
                {
                    e.TotalValue = Math.Round(result2, 2);
                }
            }
        }
        void showReport()
        {
            XtraClassResultReport report = new XtraClassResultReport();
            Image logo = fun.Base64ToImage(Login.Logo);
            var school = fun.GetSettings("system_title");
            report.PicIogoBox.Image = logo;
            report.LabTitle.Text = school;
            report.lab_phone.Text = fun.GetSettings("phone");
            report.LabAddress.Text = fun.GetSettings("address");
            report.labClass.Text = "All";
            report.LabSection.Text = "All";
            report.GridControl = gridClassResult;

            ReportPrintTool printTool = new ReportPrintTool(report);
            printTool.PreviewForm.PrintingSystem.Document.AutoFitToPagesWidth = 1;
            printTool.ShowRibbonPreviewDialog(UserLookAndFeel.Default);
        }

        private void btnCRShow_Click(object sender, System.EventArgs e)
        {
            fun.loaderform(() =>TeacherSummary("class"));
        }
        private void simpleButton2_Click_1(object sender, EventArgs e)
        {
            fun.loaderform(() =>TeacherSummary("section"));
        }

        private void simpleButton3_Click(object sender, EventArgs e)
        {
            fun.loaderform(() =>TeacherSummary("subject"));
        }
        private void simpleButton1_Click_1(object sender, EventArgs e)
        {
            fun.loaderform(() =>CampusFailure());
        }
        private void btnSujbectFailure_Click(object sender, EventArgs e)
        {
            fun.loaderform(() =>SubjectFailure());
        }
        private void btnStudentwiseFailure_Click(object sender, EventArgs e)
        {
            fun.loaderform(() => StudentFailure());
        }
        private void btnClassTopper_Click(object sender, EventArgs e)
        {
            fun.loaderform(() =>ClassTopper("class"));
        }
        private void btnCampusTopper_Click(object sender, EventArgs e)
        {
            fun.loaderform(() => ClassTopper("campus"));
        }
        private void btnOverallPercent_Click(object sender, EventArgs e)
        {
            fun.loaderform(() =>OverAllResultPercent());
        }
        private void btnGradeComparison_Click(object sender, EventArgs e)
        {
            fun.loaderform(() =>GradeComparison());
        }

        private void btnSectionTopper_Click(object sender, EventArgs e)
        {
            fun.loaderform(() =>ClassTopper("section"));
        }

    }
}
