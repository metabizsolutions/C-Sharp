using DevExpress.LookAndFeel;
using DevExpress.XtraGrid;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraPrinting;
using DevExpress.XtraReports.UI;
using System;
using System.Collections.ObjectModel;
using System.Data;
using System.Drawing;
using System.Drawing.Printing;
using System.Windows.Forms;

namespace SchoolManagementSystem.Exam
{
    public partial class ClassResultAll : DevExpress.XtraEditors.XtraUserControl
    {
        private static ClassResultAll _instance;

        public static ClassResultAll instance
        {
            get
            {
                if (_instance == null)
                    _instance = new ClassResultAll();
                return _instance;
            }
        }
        ObservableCollection<AllClass> allClass = new ObservableCollection<AllClass>();
        // ObservableCollection<Result> resultclass = new ObservableCollection<Result>();

        CommonFunctions fun = new CommonFunctions();
        public ClassResultAll()
        {
            InitializeComponent();
            loadfunctions();

        }
        public void loadfunctions()
        {
            txtClass.Properties.DataSource = fun.GetAllClasses_dt();
            txtClass.Properties.DisplayMember = "name";
            txtClass.Properties.ValueMember = "class_id";
        }
        int examID, classID, sectionID;

        ObservableCollection<string> s = new ObservableCollection<string>();
        public void result()
        {
            DataTable table = new DataTable();
            gridClassResult.DataSource = null;
            gridView1.Columns.Clear();
            classID = Convert.ToInt32(txtClass.EditValue);
            sectionID = Convert.ToInt32(txtSection.EditValue);


            String toDate = txtTo.DateTime.ToString("yyyy-MM-dd");
            String toFrom = txtFrom.DateTime.ToString("yyyy-MM-dd");
            if (txtSection.Text != "")
            {

                String query = "SELECT student_id,`name`,GROUP_CONCAT(subject_name ORDER BY subject_id ASC) AS subject_name,FLOOR(100*(SUM(obtain)/SUM(total))) AS percent, " +
                                " SUM(obtain) AS sum_obtain, SUM(total) AS sum_total, GROUP_CONCAT(IFNULL(obtain, 0) ORDER BY subject_id ASC) AS obtain, GROUP_CONCAT(IFNULL(total, 0) ORDER BY subject_id ASC) AS total " +
                                " FROM( " +
                                " SELECT std.name, m.student_id, exam.exam_id, exam.name AS exam_name, m.subject_id, sub.name AS subject_name, " +
                                " SUM(IF(m.mark_obtained >= 0, m.mark_obtained, 0)) AS obtain, SUM(IF(tms.marks > 0, tms.marks, 0)) AS total " +
                                " FROM mark AS m " +
                                " INNER JOIN student AS STD ON m.student_id = std.student_id " +
                                " INNER JOIN exam ON exam.exam_id = m.exam_id " +
                                " INNER JOIN tbl_mark_subject AS tms ON tms.`class_id` = m.`class_id` AND tms.`section_id` = m.`section_id` AND tms.`subject_id` = m.`subject_id` AND tms.`exam_id` = m.`exam_id` " +
                                " INNER JOIN `subject_default` AS sub ON sub.id = m.subject_id " +
                                " WHERE std.passout != 1 AND m.section_id = '{0}' AND exam.date >= '{1}' AND exam.date <= '{2}' " +
                                " GROUP BY m.student_id, m.subject_id " +
                                " ) AS tbl  GROUP BY student_id ORDER BY(SUM(obtain) / SUM(total)) DESC ";
                query = String.Format(query, sectionID, toFrom, toDate);
                DataTable RawData = fun.FetchDataTable(query);
                string[] subject_array, obtain_array, total_array, percent_array;
                String percent_ids;
                table.Columns.Add("SRN");
                table.Columns.Add("StudentID");
                table.Columns.Add("Name");
                if (RawData.Rows.Count > 0)
                {
                    DataRow row = RawData.Rows[0];
                    subject_array = row["subject_name"].ToString().Split(',');
                    for (int i = 0; i < subject_array.Length; i++)
                    {
                        if (!table.Columns.Contains(subject_array[i]))
                            table.Columns.Add(subject_array[i]);
                    }
                }
                table.Columns.Add("Obtain");
                table.Columns.Add("Total");
                table.Columns.Add("Percent");
                DataRow tr;
                for (int j = 0; j < RawData.Rows.Count; j++)
                {
                    tr = table.NewRow();
                    tr["SRN"] = j + 1;
                    tr["StudentID"] = RawData.Rows[j]["student_id"];
                    tr["Name"] = RawData.Rows[j]["name"];
                    obtain_array = RawData.Rows[j]["obtain"].ToString().Split(',');
                    total_array = RawData.Rows[j]["total"].ToString().Split(',');
                    int k = 0;
                    for (k = 0; k < obtain_array.Length; k++)
                    {
                        if (table.Columns.Count >= k + 6)
                            tr[3 + k] = obtain_array[k] + "/" + total_array[k];
                    }
                    tr["Obtain"] = RawData.Rows[j]["sum_obtain"];
                    tr["Total"] = RawData.Rows[j]["sum_total"];
                    tr["Percent"] = RawData.Rows[j]["percent"];
                    table.Rows.Add(tr);
                }
            }


            gridView1.Columns.Clear();
            gridClassResult.DataSource = null;
            gridClassResult.DataSource = table;

            gridView1.IndicatorWidth = 30;
            gridView1.OptionsView.AllowHtmlDrawHeaders = true;
            gridView1.Appearance.HeaderPanel.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            gridView1.ColumnPanelRowHeight = 40;
            gridView1.BestFitColumns();
        }
        private void btnCRShow_Click(object sender, System.EventArgs e)
        {
            if(txtClass.EditValue == null || txtSection.EditValue == null)
            {
                MessageBox.Show("Please select class and section first", "info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            fun.loaderform(() => result());
        }

        private void ClassResult_Enter(object sender, System.EventArgs e)
        {
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
                + "                  " + "Session: " + Main_FD.SelectedSession + "                         Class: " + txtClass.Text + "                Section: " + txtSection.Text + "\n\r"
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

        private void txtClass_EditValueChanged(object sender, EventArgs e)
        {
            if (txtClass.EditValue != null)
                section_list(txtClass.EditValue.ToString());
        }
        void section_list(string classid)
        {
            txtSection.Properties.DataSource = fun.GetAllSection_dt(classid);
            txtSection.Properties.DisplayMember = "name";
            txtSection.Properties.ValueMember = "section_id";
        }

        /*
        private void gridView1_RowCellStyle(object sender, DevExpress.XtraGrid.Views.Grid.RowCellStyleEventArgs e)
        {
            GridView View = sender as GridView;
            if (e.Column.FieldName == "Sr#" || e.Column.FieldName == "ID" || e.Column.FieldName == "Student" || e.Column.FieldName == "Section" || e.Column.FieldName == "Roll" ||
                e.Column.FieldName == "Phone" || e.Column.FieldName == "Obtained" || e.Column.FieldName == "Total" || e.Column.FieldName == "Average")
            {
                return;
            }
            allClass.Clear();
            if (sectionID != 0)
            {
                allClass = fun.GetAllSubject(sectionID);

                for (int i = 0; i < allClass.Count; i++)
                {
                    int total = fun.GetSubjectTotalMarks(Convert.ToInt32(allClass[i].Salary), examID);
                    var totalSubject = (total == 0) ? 100 : total;
                    var res = allClass[i].Name + "(" + total + ")";
                    string present = View.GetRowCellDisplayText(e.RowHandle, View.Columns[res]);

                    if (e.Column.FieldName == res)//
                    {
                        if (present == "A")
                        {
                            e.Appearance.ForeColor = Color.Blue;
                        }
                        else
                            if (present == "UMC")
                        {
                            e.Appearance.ForeColor = Color.Orange;
                        }
                        else
                        if (present == "CA")
                        {
                            e.Appearance.ForeColor = Color.Red;
                        }
                    }
                }
            }
            if (e.Column.FieldName == "Result")
            {
                string present = View.GetRowCellDisplayText(e.RowHandle, View.Columns["Result"]);
                if (present == "Pass")
                {
                    e.Appearance.ForeColor = Color.YellowGreen;
                }
                else
                {
                    e.Appearance.ForeColor = Color.OrangeRed;
                }
            }

        }
        */
        void showReport()
        {
            XtraClassResultReport report = new XtraClassResultReport();
            Image logo = fun.Base64ToImage(Login.Logo);
            var school = fun.GetSettings("system_title");
            report.PicIogoBox.Image = logo;
            report.LabTitle.Text = school;
            report.lab_phone.Text = fun.GetSettings("phone");
            report.LabAddress.Text = fun.GetSettings("address");
            report.labClass.Text = txtClass.Text;
            report.LabSection.Text = txtSection.Text;
            report.GridControl = gridClassResult;

            ReportPrintTool printTool = new ReportPrintTool(report);
            printTool.PreviewForm.PrintingSystem.Document.AutoFitToPagesWidth = 1;
            printTool.ShowRibbonPreviewDialog(UserLookAndFeel.Default);
        }

    }
}
