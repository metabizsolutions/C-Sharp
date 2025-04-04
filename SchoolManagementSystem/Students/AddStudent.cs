using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.IO;
using System.Media;
using System.Windows.Forms;
using DevExpress.LookAndFeel;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Repository;
using DevExpress.XtraGrid;
using DevExpress.XtraGrid.Columns;
using DevExpress.XtraGrid.Views.Base;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraReports.UI;
using SchoolManagementSystem.Class;
using SchoolManagementSystem.Students;

namespace SchoolManagementSystem
{
    public partial class AddStudent : XtraUserControl
    {
        private static AddStudent _instance;

        public static AddStudent instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new AddStudent();
                }

                return _instance;
            }
        }

        private CommonFunctions fun = new CommonFunctions();
        public AddStudent()
        {
            InitializeComponent();
            loadfunctions();

        }
        public void permissions_btns(bool add, bool Edit, bool Delete)
        {
            btnNew.Enabled = false;
            if (add)
            {
                btnNew.Enabled = true;
            }
            gridView1.OptionsBehavior.Editable = false;
            if (Edit)
            {
                gridView1.OptionsBehavior.Editable = true;
            }
            btnDelete.Enabled = false;
            if (Delete)
            {
                btnDelete.Enabled = true;
            }

            string subject_wise = fun.GetSettings("Institute_Type");
            if (subject_wise == "Subject Wise Institute")
            {
                btn_subjectfee.Visible = true;
            }
            else
            {
                btn_subjectfee.Visible = false;
            }
        }
        private void AddStudent_Enter(object sender, EventArgs e)
        {

        }
        public void loadfunctions()
        {
            FillGridStudent();
        }

        private RepositoryItemSearchLookUpEdit riSearch;
        private int[] cat_ids;
        private String[] cat_titles;
        private object grid_ID;

        private DataTable student_details_tb(string where)
        {
            string query_cat = "SELECT field_id,title FROM student_fields ORDER BY title ASC  ";
            DataTable table_cat = fun.GetQueryTable(query_cat);
            cat_ids = new int[table_cat.Rows.Count];
            cat_titles = new String[table_cat.Rows.Count];
            int count = 0;
            int fee_cat_id;
            String extra_col = "";
            foreach (DataRow row in table_cat.Rows)
            {
                fee_cat_id = Convert.ToInt32(row["field_id"].ToString());
                cat_ids[count] = fee_cat_id;
                cat_titles[count] = row["title"].ToString();
                extra_col += ",(SELECT IFNULL(`value`,'') FROM student_fields_values WHERE field_id = '" + fee_cat_id + "' AND student_id = students.SrNo limit 1) AS '" + row["title"].ToString() + "'";
                count++;
            }

            string query = "SELECT students.*, " +
                " (SELECT concat(cla.name,' [',sec.name,'] ',inv.date,' Total=',inv.amount,' Paid=',inv.amount_paid) as pri_class FROM `change_class` as cc " +
                " join class as cla on cla.class_id = cc.class_id " +
                " join section as sec on sec.section_id = cc.section_id " +
                " join invoice as inv on inv.invoice_id = cc.invoice_id where cc.std_id = students.SrNo order by id desc limit 1) as pri_class " + extra_col + " " +
                " from students where passout != 1 " + where + " ";
            DataTable dt = fun.FetchDataTable(query);
            return dt;
        }
        public void FillGridStudent()
        {
            gridView1.Columns.Clear();
            gridAddStudents.DataSource = student_details_tb("");
            //Serial No
            GridColumn colCounter = gridView1.Columns.AddVisible("SrNo#");
            colCounter.VisibleIndex = 0;
            colCounter.UnboundType = DevExpress.Data.UnboundColumnType.Integer;
            colCounter.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.True;
            gridView1.CustomUnboundColumnData += (sender, e) =>
            {
                GridView v = sender as GridView;
                if (e.Column.FieldName == "SrNo#" && e.IsGetData)
                {
                    e.Value = v.GetRowHandle(e.ListSourceRowIndex) + 1;
                }
            };
            //End serial No
            #region column properites from database
            string query = "Select ID from grids where GridName = 'Student_Reg'";
            grid_ID = fun.Execute_Scaler_string(query);
            if (Convert.ToInt32(grid_ID) <= 0)
            {
                query = "INSERT INTO `grids`(`GridName`) VALUES ('Student_Reg')";
                grid_ID = fun.Execute_Insert(query);
            }
            ColumnView view = (ColumnView)gridAddStudents.FocusedView;

            view.ClearColumnsFilter();
            for (int i = 0; i < view.Columns.Count; i++)
            {
                string name = view.Columns[i].FieldName;
                view.Columns[i].OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.False;
                view.Columns[name].OptionsColumn.ReadOnly = true;
                if (name == "PassOut" || name == "Enotice_sms" || name == "Fees_sms" || name == "Exam_sms" || name == "Attendent_sms" || name == "free_student" || name == "free_std_remakrs")
                {
                    view.Columns[name].OptionsColumn.ReadOnly = false;
                }

                string select_col_query = "Select * from grids_columns where Grid_ID = '" + grid_ID + "' and Column_Name = '" + name + "'";
                DataTable grid_Column = fun.FetchDataTable(select_col_query);
                if (grid_Column.Rows.Count <= 0)
                {
                    query = "INSERT INTO `grids_columns`(`Grid_ID`, `Column_Name`, `Width`, `order`, `Visible`) VALUES ('" + grid_ID + "','" + name + "','" + view.Columns[i].Width + "','" + i + "','" + (view.Columns[i].Visible == true ? 1 : 0) + "')";
                    fun.Execute_Query(query);
                    grid_Column = fun.FetchDataTable(select_col_query);
                }
                bool visibility = Convert.ToInt32(grid_Column.Rows[0]["Visible"]) == 1 ? true : false;
                view.Columns[i].VisibleIndex = Convert.ToInt32(grid_Column.Rows[0]["order"]);
                view.Columns[i].Visible = visibility;
                view.Columns[i].Width = Convert.ToInt32(grid_Column.Rows[0]["Width"]);

                if (!fun.CheckPermission("fee_setting"))
                {
                    if (name == "Fee_concession" || name == "Addmission_concession" || name == "Security_concession" || name == "Annual_concession" || name == "Exam_concession")
                    {
                        view.Columns[name].Visible = false;
                        view.Columns[name].OptionsColumn.ReadOnly = true;
                    }
                }
                if (name == "Image")
                {
                    view.Columns[name].Width = 70;
                }

                if (name == "Class")
                {
                    gridView1.Columns["Class"].FieldNameSortGroup = "class_id";
                    view.Columns[name].GroupIndex = 0;

                }

                if (name == "Section")
                {
                    gridView1.Columns["Section"].FieldNameSortGroup = "section_id";
                    view.Columns[name].GroupIndex = 1;
                    GridGroupSummaryItem item = new GridGroupSummaryItem();
                    item.FieldName = name;
                    item.SummaryType = DevExpress.Data.SummaryItemType.Count;
                    gridView1.GroupSummary.Clear();
                    gridView1.GroupSummary.Add(item);
                }
            }

            #endregion column properties
            if (Main_FD.SelectedSession != fun.GetDefaultSessionName())
            {
                btnDelete.Enabled = false;
                gridView1.OptionsBehavior.Editable = false;
            }
            else
            {
                bool Delete = fun.isAllow("Delete", "registration");
                if (Delete)
                {
                    btnDelete.Enabled = true;
                    gridView1.OptionsBehavior.Editable = true;
                }
            }
            gridView1.ExpandAllGroups();
            //gridView1.ClearSorting();
        }
        private void btnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                DataRow row = gridView1.GetDataRow(gridView1.FocusedRowHandle);
                string query = "select * from mark where student_id = '" + row["SrNo"] + "'";
                DataTable ma_dt = fun.FetchDataTable(query);
                query = "select * from ac_transaction where ledger_to = '" + row["ladger_id"] + "' or ledger_from = '" + row["ladger_id"] + "'";
                DataTable ac_dt = fun.FetchDataTable(query);
                if (ma_dt.Rows.Count > 0)
                {
                    MessageBox.Show("Student have some data in exam .. you cannnot delete this student now ... just edit or passout this student", "Student Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
                else if (ac_dt.Rows.Count > 0)
                {
                    MessageBox.Show("Student have some data in Account .. you cannnot delete this student now ... just edit or passout this student", "Student Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
                else
                {
                    if (MessageBox.Show("Are you sure to delete?", "Confirm Delete!!", MessageBoxButtons.YesNo) == DialogResult.Yes)
                    {
                        query = "DELETE from student WHERE student_id='" + row["SrNo"] + "';";
                        fun.ExecuteQuery(query);
                        gridView1.DeleteRow(gridView1.FocusedRowHandle);
                    }
                }
            }
            catch (Exception ex)
            {
                SystemSounds.Hand.Play();
                MessageBox.Show(ex.Message, "Info");
            }
        }
        private void BtnPrint_Click(object sender, EventArgs e)
        {
            showReport();
        }

        private void showReport()
        {
            DataRow dr = gridView1.GetFocusedDataRow();
            XtraStudentList report = new XtraStudentList();
            Image logo = fun.Base64ToImage(Login.Logo);
            var school = fun.GetSettings("system_title");
            report.PicIogoBox.Image = logo;
            report.LabTitle.Text = school;
            report.LabAddress.Text = fun.GetSettings("address");
            //report.labExam.Text = txtExam.Text;
            report.LabClass.Text = "";//dr["class"] == null ? "" : dr["class"].ToString();//txtClass.Text;
            report.GridControl = gridAddStudents;

            ReportPrintTool printTool = new ReportPrintTool(report);
            printTool.PreviewForm.PrintingSystem.Document.AutoFitToPagesWidth = 1;
            printTool.ShowRibbonPreviewDialog(UserLookAndFeel.Default);
        }

        private void btnExport_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveFileDialog1 = new SaveFileDialog();
            saveFileDialog1.Filter = "Excel Files (*.xlsx)| *.xlsx";
            saveFileDialog1.Title = "Save an ExcelFile";
            saveFileDialog1.ShowDialog();
            if (saveFileDialog1.FileName != "")
            {
                System.IO.FileStream fs =
                    (System.IO.FileStream)saveFileDialog1.OpenFile();
                gridAddStudents.ExportToXlsx(fs);
                fs.Close();
            }
        }

        private void AddStudent_Load(object sender, EventArgs e)
        {

        }
        private void gridView1_CellValueChanged(object sender, CellValueChangedEventArgs e)
        {
            string c = e.Column.FieldName;
            GridView gv = sender as GridView;
            if (!is_add_student && (c == "PassOut" || c == "Enotice_sms" || c == "Fees_sms" || c == "Exam_sms" || c == "Attendent_sms" || c == "free_student" || c == "free_std_remakrs"))
            {
                string stdID = gv.GetRowCellValue(e.RowHandle, gv.Columns["SrNo"]).ToString();
                var val = "";
                if (c == "free_std_remakrs")
                {
                    val = e.Value.ToString();
                }
                else
                {
                    val = (Convert.ToBoolean(e.Value) == true) ? "1" : "0";
                }

                
                if (c == "PassOut")
                {
                    string query = "update student set `" + c + "` = '" + val + "',passout_date='"+DateTime.Now.ToString("yyyy-MM-dd")+"' where student_id = '" + stdID + "'";
                    fun.ExecuteQuery(query);
                    gv.DeleteRow(e.RowHandle);
                }
                else
                {
                    string query = "update student set `" + c + "` = '" + val + "' where student_id = '" + stdID + "'";
                    fun.ExecuteQuery(query);
                }
            }
            else
            {
                return;
            }
        }

        private bool is_add_student = false;
        private void btnNew_Click(object sender, EventArgs e)
        {
            btnNew.Enabled = false;
            using (NewStudent de = new NewStudent())
            {
                if (de.ShowDialog() == DialogResult.Yes) { }
                else
                {
                    if (de.inserted_student_id > 0)
                    {
                        FillGridStudent();
                        /*gridView1.AddNewRow();
                        int rowHandler = gridView1.FocusedRowHandle;
                        if (gridView1.IsNewItemRow(rowHandler))
                        {

                            is_add_student = true;
                            ColumnView view = (ColumnView)gridAddStudents.FocusedView;
                            DataTable dt = student_details_tb(" and SrNo = '" + de.inserted_student_id + "'");

                            for (int i = 0; i < view.Columns.Count; i++)
                            {
                                string name = view.Columns[i].FieldName;
                                if (name == "SrNo#")
                                {
                                }
                                else
                                {
                                    string val = dt.Rows[0][name].ToString();
                                    if (name == "addmission_date")
                                    { }// gridView1.SetRowCellValue(rowHandler, gridView1.Columns[name],Convert.ToDateTime(val).ToString());
                                    else
                                    {
                                        gridView1.SetRowCellValue(rowHandler, gridView1.Columns[name], val);
                                    }
                                }
                            }

                            gridView1.RefreshRow(rowHandler);
                        }*/
                    }
                }
            }
            btnNew.Enabled = true;
            is_add_student = false;

        }
        private void btnFieldManagement_Click(object sender, EventArgs e)
        {
            using (StudentFields aa = new StudentFields())
            {
                if (aa.ShowDialog() == DialogResult.OK)
                {
                }
            }
        }
        private List<XtraFeePerforma> FeePerforma_reports = new List<XtraFeePerforma>();

        private void Btn_Fee_Performa_Click(object sender, EventArgs e)
        {
            FeePerforma_reports.Clear();
            MessageBoxManager.Yes = "Print All";
            MessageBoxManager.No = "Print Selected";
            MessageBoxManager.Cancel = "Cancle";
            MessageBoxManager.Register();
            DialogResult a = MessageBox.Show("Please Select option for print....", "Printing", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Information);
            fun.loaderform(() =>
            {
                if (a != DialogResult.Cancel)
                {
                    if (a == DialogResult.Yes)
                    {
                        for (int i = 0; i < gridView1.DataRowCount; i++)
                        {
                            DataRow newrow = gridView1.GetDataRow(i);
                            stdRow = newrow;
                            FeePerforma_reports.Add(FeePerforma(newrow["SrNo"].ToString()));
                        }
                    }
                    else if (a == DialogResult.No)
                    {
                        DataRow row = gridView1.GetFocusedDataRow();
                        stdRow = row;
                        FeePerforma_reports.Add(FeePerforma(row["SrNo"].ToString()));
                    }
                    ReportPrintTool printTool = new ReportPrintTool(CreateReport_FeePerofrma());
                    printTool.PreviewForm.PrintingSystem.Document.AutoFitToPagesWidth = 1;
                    printTool.ShowRibbonPreviewDialog(UserLookAndFeel.Default);
                }
            });

            MessageBoxManager.Unregister();
        }
        public XtraFeePerforma CreateReport_FeePerofrma()
        {
            XtraFeePerforma output = new XtraFeePerforma();
            foreach (var rep in FeePerforma_reports)
            {
                rep.CreateDocument(false);
                output.Pages.AddRange(rep.Pages);
            }
            if (output == null)
            {
                output = new XtraFeePerforma();// new FeeRecipt();
            }

            output.PrintingSystem.ContinuousPageNumbering = true;
            return output;
        }
        public XtraFeePerforma FeePerforma(String student_id)
        {
            string query = "SELECT * FROM `student_fields`";
            DataTable dt_fields = fun.FetchDataTable(query);

            query = "SELECT student.*, class.name AS class, section.name AS section, parent.name AS parent, parent.phone AS parent_phone,tbl_school.name AS school ";
            foreach (DataRow dr in dt_fields.Rows)
            {
                query += ",(SELECT sfv.`value` FROM `student_fields_values` as sfv WHERE sfv.`student_id` = " + student_id + " and sfv.`field_id` = '" + dr["field_id"] + "') as `" + dr["title"] + "`";
            }
            query += " FROM student " +
            " INNER JOIN parent ON parent.parent_id = student.parent_id " +
            " INNER JOIN class ON class.class_id = student.class_id " +
            " INNER JOIN section ON section.section_id = student.section_id " +
            " LEFT JOIN tbl_school ON tbl_school.school_id = student.p_school " +
            " WHERE student.student_id = '{0}'";
            query = String.Format(query, student_id);
            DataTable table = fun.FetchDataTable(query);
            XtraFeePerforma report = new XtraFeePerforma();
            if (table.Rows.Count > 0)
            {
                DataRow row = table.Rows[0];
                report.LabGroupID.Text = !row.Table.Columns.Contains("GroupID") ? "" : string.IsNullOrEmpty(row["GroupID"].ToString()) ? "" : row["GroupID"].ToString();
                report.LabBoardRegNO.Text = !row.Table.Columns.Contains("BoardRegNO") ? "" : string.IsNullOrEmpty(row["BoardRegNO"].ToString()) ? "" : row["BoardRegNO"].ToString();
                report.LabPerviousClassMedium.Text = !row.Table.Columns.Contains("PerviousClassMedium") ? "" : string.IsNullOrEmpty(row["PerviousClassMedium"].ToString()) ? "" : row["PerviousClassMedium"].ToString();
                report.LabPluseSessionFee.Text = !row.Table.Columns.Contains("PlusSessionFee") ? "" : string.IsNullOrEmpty(row["PlusSessionFee"].ToString()) ? "" : row["PlusSessionFee"].ToString();
                report.LabTotalFeePart1.Text = !row.Table.Columns.Contains("TotalCollegeFeePart1") ? "" : string.IsNullOrEmpty(row["TotalCollegeFeePart1"].ToString()) ? "96000" : row["TotalCollegeFeePart1"].ToString();
                report.LabTotalFeePart2.Text = !row.Table.Columns.Contains("TotalCollegeFeePart2") ? "" : string.IsNullOrEmpty(row["TotalCollegeFeePart2"].ToString()) ? "" : row["TotalCollegeFeePart2"].ToString();
                report.LabBordFeePart1.Text = !row.Table.Columns.Contains("BoardFeePart1") ? "" : string.IsNullOrEmpty(row["BoardFeePart1"].ToString()) ? "5000" : row["BoardFeePart1"].ToString();
                report.LabBordFeePart2.Text = !row.Table.Columns.Contains("BoardFeePart2") ? "" : string.IsNullOrEmpty(row["BoardFeePart2"].ToString()) ? "3000" : row["BoardFeePart2"].ToString();
                report.labName.Text = !row.Table.Columns.Contains("name") ? "" : string.IsNullOrEmpty(row["name"].ToString()) ? "" : row["name"].ToString();
                report.LabPSchool.Text = !row.Table.Columns.Contains("school") ? "" : string.IsNullOrEmpty(row["school"].ToString()) ? "" : row["school"].ToString();
                report.LabSection.Text = !row.Table.Columns.Contains("section") ? "" : string.IsNullOrEmpty(row["section"].ToString()) ? "" : row["section"].ToString();
                report.LabClass.Text = !row.Table.Columns.Contains("class") ? "" : string.IsNullOrEmpty(row["class"].ToString()) ? "" : row["class"].ToString();
                report.LabFName.Text = !row.Table.Columns.Contains("parent") ? "" : string.IsNullOrEmpty(row["parent"].ToString()) ? "" : row["parent"].ToString();
                report.LabFormNO.Text = !row.Table.Columns.Contains("roll") ? "" : string.IsNullOrEmpty(row["roll"].ToString()) ? "" : row["roll"].ToString();
                report.labStudentId.Text = !row.Table.Columns.Contains("student_id") ? "" : string.IsNullOrEmpty(row["student_id"].ToString()) ? "" : row["student_id"].ToString();
                report.Lab9thMarks.Text = !row.Table.Columns.Contains("9thMarks") ? "" : string.IsNullOrEmpty(row["9thMarks"].ToString()) ? "" : row["9thMarks"].ToString();
                report.Lab10Marks.Text = !row.Table.Columns.Contains("10thMarks") ? "" : string.IsNullOrEmpty(row["10thMarks"].ToString()) ? "" : row["10thMarks"].ToString();
                report.lab10Roll.Text = !row.Table.Columns.Contains("10thRollnumber") ? "" : string.IsNullOrEmpty(row["10thRollnumber"].ToString()) ? "" : row["10thRollnumber"].ToString();
                report.LabDate.Text = DateTime.Now.ToString("MMMM/dd/yyyy hh:mm:ss");




                Image logo = fun.Base64ToImage(Login.Logo);
                var school = fun.GetSettings("system_title");
                report.PicIogoBox.Image = logo;
                report.LabTitle.Text = school;

                //report.LabTel.Text = fun.GetSettings("phone");
            }
            return report;
        }
        private List<XtraAdmissionForm> reports = new List<XtraAdmissionForm>();
        public static DataRow stdRow { get; set; }
        private void btnStudentForm_Click(object sender, EventArgs e)
        {
            reports.Clear();
            MessageBoxManager.Yes = "Print All";
            MessageBoxManager.No = "Print Selected";
            MessageBoxManager.Cancel = "Empty";
            MessageBoxManager.Register();
            DialogResult a = MessageBox.Show("Please Select option for print....", "Printing", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Information);
            fun.loaderform(() =>
            {
                if (a == DialogResult.Cancel)
                {
                    AppendReport(showReport("0"));
                }
                else if (a == DialogResult.Yes)
                {
                    for (int i = 0; i < gridView1.DataRowCount; i++)
                    {
                        DataRow newrow = gridView1.GetDataRow(i);
                        stdRow = newrow;
                        AppendReport(showReport(newrow["SrNo"].ToString()));
                    }
                }
                else if (a == DialogResult.No)
                {
                    DataRow row = gridView1.GetFocusedDataRow();
                    stdRow = row;
                    AppendReport(showReport(row["SrNo"].ToString()));
                }
                ReportPrintTool printTool = new ReportPrintTool(CreateReport());

                printTool.PreviewForm.PrintingSystem.Document.AutoFitToPagesWidth = 1;
                printTool.ShowRibbonPreviewDialog(UserLookAndFeel.Default);

            });
            MessageBoxManager.Unregister();


        }
        public XtraAdmissionForm showReport(String student_id)
        {

            String query = "SELECT student.*, class.name AS class, section.name AS section, parent.name AS parent, parent.phone AS parent_phone,tbl_school.name AS school " +
                            " FROM student " +
                            " INNER JOIN parent ON parent.parent_id = student.parent_id " +
                            " INNER JOIN class ON class.class_id = student.class_id " +
                            " INNER JOIN section ON section.section_id = student.section_id " +
                            " LEFT JOIN tbl_school ON tbl_school.school_id = student.p_school " +
                            " WHERE student.student_id = '{0}'";
            query = String.Format(query, student_id);
            DataTable table = fun.FetchDataTable(query);
            XtraAdmissionForm report = new XtraAdmissionForm();
            if (table.Rows.Count > 0)
            {
                DataRow row = table.Rows[0];
                report.labStudentId.Text = row["student_id"].ToString();
                report.LabClass.Text = row["class"].ToString();
                report.LabSection.Text = row["section"].ToString();
                //report.LabFName.Text = row["parent"].ToString();
                string DOB = Convert.ToDateTime(row["birthday"]).ToString("dd/MM/yyyy");
                report.Names(row["name"].ToString(), row["parent"].ToString(), row["phone"].ToString(), row["parent_phone"].ToString(), DOB);
                //report.LabPhone.Text = row["parent_phone"].ToString();
                report.LabGender.Text = row["sex"].ToString();
                report.LabPSchool.Text = row["school"].ToString();
                //report.LabDob.Text = row["birthday"].ToString();
                //report.LabCell.Text = row["phone"].ToString();
                report.LabSAddress.Text = row["address"].ToString();
                //report.labName.Text = row["name"].ToString();
                report.labRoll.Text = row["roll"].ToString() != "" ? row["roll"].ToString() : row["student_id"].ToString();

                string appPath = Path.GetDirectoryName(Application.ExecutablePath) + @"\Images\Students\"; // <---
                Bitmap bmp = null;
                string[] extensions = new string[] { ".JPEG", ".BMP", ".JPG", ".PNG" };
                foreach (string found in extensions)
                {
                    var files = Directory.GetFiles(appPath, student_id + "_std" + found, System.IO.SearchOption.AllDirectories);
                    foreach (var filename in files)
                    {
                        bmp = new Bitmap(filename);
                    }
                }
                if (bmp != null)
                {
                    report.PicStdBox.Image = bmp;
                }

                ActivityControl.DataSource = null;
                ActivitygridView.Columns.Clear();
                query = "SELECT student_fields.title,Type,Lenght,IFNULL(sfv.value,'') AS `value`  FROM student_fields " +
                " LEFT JOIN student_fields_values AS sfv ON sfv.field_id = student_fields.field_id AND sfv.student_id = '{0}' " +
                " ORDER BY student_fields.title ASC ";
                query = String.Format(query, student_id);

                DataTable activitytable = fun.FetchDataTable(query);
                report.extraField(activitytable);
                query = "SELECT (cf.admission - student.addmission_concession) AS admission_fee , " +
                        " (cf.security_deposit - student.security_concession) AS secuirity_charges, " +
                        " (cf.monthly - student.fee_concession) AS fee, (cf.annual_charges - student.annual_concession) AS anual_charges, " +
                        " (cf.exam_charges - student.exam_concession) AS exam_charges, (cf.card_charges) AS card_charges " +
                        " FROM class_fees AS cf " +
                        " INNER JOIN student ON student.class_id = cf.class_id " +
                        " WHERE student.student_id = {0}";
                query = String.Format(query, student_id);
                DataTable fee_table = fun.FetchDataTable(query);
                if (fee_table.Rows.Count > 0)
                {
                    row = activitytable.NewRow();
                    row["title"] = "Fees Infromation";
                    row["value"] = "";
                    activitytable.Rows.Add(row);

                    row = activitytable.NewRow();
                    row["title"] = "Admission Fee";
                    row["value"] = fee_table.Rows[0]["admission_fee"];
                    activitytable.Rows.Add(row);

                    row = activitytable.NewRow();
                    row["title"] = "Secuirity Fee";
                    row["value"] = fee_table.Rows[0]["secuirity_charges"];
                    activitytable.Rows.Add(row);

                    row = activitytable.NewRow();
                    row["title"] = "Monthly Fee";
                    row["value"] = fee_table.Rows[0]["fee"];
                    activitytable.Rows.Add(row);

                    row = activitytable.NewRow();
                    row["title"] = "Annual Fee";
                    row["value"] = fee_table.Rows[0]["anual_charges"];
                    activitytable.Rows.Add(row);

                    row = activitytable.NewRow();
                    row["title"] = "Exam Fee";
                    row["value"] = fee_table.Rows[0]["exam_charges"];
                    activitytable.Rows.Add(row);

                    row = activitytable.NewRow();
                    row["title"] = "Card Fee";
                    row["value"] = fee_table.Rows[0]["card_charges"];
                    activitytable.Rows.Add(row);
                }

                query = "SELECT fees_category.fees_title AS title, fcf.fees_val AS `value` " +
                        " FROM fees_category_student AS fcf " +
                        " INNER JOIN fees_category ON fees_category.fee_cat_id = fcf.fees_cat_id " +
                        " WHERE fcf.student_id = '{0}'";
                query = String.Format(query, student_id);
                fee_table = fun.FetchDataTable(query);
                if (fee_table.Rows.Count > 0)
                {
                    foreach (DataRow drow in fee_table.Rows)
                    {
                        row = activitytable.NewRow();
                        row["title"] = drow["title"];
                        row["value"] = drow["value"];
                        activitytable.Rows.Add(row);
                    }
                }
                report.extraField(activitytable);

                ActivityControl.DataSource = activitytable;
                ActivitygridView.BestFitColumns();
                ActivitygridView.OptionsPrint.AutoWidth = false;
                ActivitygridView.OptionsView.ColumnAutoWidth = false;

                foreach (GridColumn col in ActivitygridView.Columns)
                {
                    col.AppearanceCell.Font = new Font("Tahoma", 9f, FontStyle.Regular);
                    col.Width = 362;
                }
                //report.ContainerAcitvity.PrintableComponent = ActivityControl;

                Image logo = fun.Base64ToImage(Login.Logo);
                var school = fun.GetSettings("system_title");
                report.PicIogoBox.Image = logo;
                report.LabTitle.Text = school;
                report.LabAddress.Text = fun.GetSettings("address");
                report.LabTel.Text = fun.GetSettings("phone");
            }
            else
            {
                Image logo = fun.Base64ToImage(Login.Logo);
                var school = fun.GetSettings("system_title");
                report.PicIogoBox.Image = logo;
                report.LabTitle.Text = school;
                report.LabAddress.Text = fun.GetSettings("address");
                report.LabTel.Text = fun.GetSettings("phone");

                ActivityControl.DataSource = null;
                ActivitygridView.Columns.Clear();
                query = "SELECT student_fields.title,Type,Lenght,IFNULL(sfv.value,'') AS `value`  FROM student_fields " +
                " LEFT JOIN student_fields_values AS sfv ON sfv.field_id = student_fields.field_id AND sfv.student_id = '{0}' " +
                " ORDER BY student_fields.title ASC ";
                query = String.Format(query, student_id);
                DataTable activitytable = fun.FetchDataTable(query);
                report.extraField(activitytable);
                report.Names("", "", "", "", "");
            }
            return report;
        }

        public void AppendReport(XtraAdmissionForm re)
        {
            re.DrawWatermark = true;
            re.Watermark.Text = "WWW.TNSBAY.COM";
            //Watermark.Image = Image.FromFile("~\\Practice\\StudentManagementSGC\\SchoolManagementSystem\\Resources\\logo.png");
            re.Watermark.Font = new Font(re.Watermark.Font.FontFamily, 40);
            re.Watermark.ForeColor = Color.DodgerBlue;
            re.Watermark.TextTransparency = 150;
            re.Watermark.ShowBehind = false;
            reports.Add(re);
        }

        private void gridView1_DragObjectDrop(object sender, DragObjectDropEventArgs e)
        {
            GridColumn cln = e.DragObject as GridColumn;
            ColumnView view = (ColumnView)gridAddStudents.FocusedView;
            view.ClearColumnsFilter();
            if (cln != null)
            {
                if (cln.Visible)
                {
                    for (int i = 0; i < view.Columns.Count; i++)
                    {
                        string name = view.Columns[i].FieldName;
                        int order = view.Columns[i].VisibleIndex;
                        if (view.Columns[i].Visible || name == cln.FieldName)
                        {
                            string query = "UPDATE `grids_columns` SET `order`=" + order + ",`Visible`='1' WHERE `Grid_ID` ='" + grid_ID + "' and `Column_Name` = '" + name + "'";
                            fun.ExecuteInsert(query);
                        }
                    }
                }
                else
                {
                    string name = cln.FieldName;
                    string query = "UPDATE `grids_columns` SET `Visible`='0' WHERE `Grid_ID` ='" + grid_ID + "' and `Column_Name` = '" + name + "'";
                    fun.ExecuteInsert(query);
                }
            }
        }

        private void gridView1_ColumnWidthChanged(object sender, ColumnEventArgs e)
        {
            ColumnView view = (ColumnView)gridAddStudents.FocusedView;
            view.ClearColumnsFilter();
            for (int i = 0; i < view.Columns.Count; i++)
            {
                string name = view.Columns[i].FieldName;
                int width = view.Columns[i].VisibleWidth;
                string query = "UPDATE `grids_columns` SET `Width`=" + width + " WHERE `Grid_ID` ='" + grid_ID + "' and `Column_Name` = '" + name + "'";
                fun.ExecuteInsert(query);
            }
        }

        private void btn_subjectfee_Click(object sender, EventArgs e)
        {
            DataRow dr = gridView1.GetFocusedDataRow();
            using (teacher_subject_fee insform = new teacher_subject_fee(dr))
            {
                if (insform.ShowDialog() == DialogResult.OK)
                {

                }
            }
        }

        private void btn_std_details_Click(object sender, EventArgs e)
        {
            DataRow dr = gridView1.GetFocusedDataRow();
            int row_handle = gridView1.FocusedRowHandle;
            using (student_details sd = new student_details(dr))
            {
                if (sd.ShowDialog() == DialogResult.OK)
                {

                }
                else
                {
                    ColumnView view = (ColumnView)gridAddStudents.FocusedView;
                    DataTable dt = student_details_tb(" and SrNo = '" + dr["SrNo"] + "'");
                    gridView1.BeginUpdate();
                    for (int i = 0; i < view.Columns.Count; i++)
                    {
                        string name = view.Columns[i].FieldName;
                        if (name == "SrNo#") { }
                        else
                        {
                            string val = dt.Rows[0][name].ToString();
                            if (name == "PassOut" || name == "Enotice_sms" || name == "Fees_sms" || name == "Exam_sms" || name == "Attendent_sms" || name == "free_student" || name == "free_std_remakrs")
                            { }
                            else if (name == "addmission_date") { } // addmission_date have some error need some extra work will do later ... now addmission date update on reload
                            else
                            {
                                if (view.Columns[i].Visible)
                                {
                                    try
                                    {
                                        gridView1.SetRowCellValue(row_handle, name, val);
                                    }
                                    catch (Exception ex)
                                    {

                                    }
                                }
                            }
                        }
                    }
                    gridView1.EndUpdate();
                }
            }
        }

        public XtraAdmissionForm CreateReport()
        {
            XtraAdmissionForm output = new XtraAdmissionForm();
            foreach (var rep in reports)
            {
                rep.CreateDocument(false);
                output.Pages.AddRange(rep.Pages);
            }
            if (output == null)
            {
                output = new XtraAdmissionForm();// new FeeRecipt();
            }
            //output.DrawWatermark = true;
            //output.Watermark.Text = "WWW.TNSBAY.COM";
            ////output.Watermark.Image = Image.FromFile(Path.GetDirectoryName(Application.ExecutablePath)+"\\Resources\\logo.png");
            //output.Watermark.Font = new Font(output.Watermark.Font.FontFamily, 40);
            //output.Watermark.ForeColor = Color.DodgerBlue;
            //output.Watermark.TextTransparency = 150;
            //output.Watermark.ShowBehind = false;
            output.PrintingSystem.ContinuousPageNumbering = true;
            return output;
        }
    }

}
