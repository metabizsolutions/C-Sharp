using DevExpress.LookAndFeel;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Repository;
using DevExpress.XtraGrid;
using DevExpress.XtraGrid.Columns;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraReports.UI;
using MySql.Data.MySqlClient;
using System;
using System.Collections;
using System.Collections.ObjectModel;
using System.Data;
using System.Drawing;
using System.Text;

namespace SchoolManagementSystem.Exam
{
    public partial class ClassResultByDateRange : DevExpress.XtraEditors.XtraUserControl
    {
        private static ClassResultByDateRange _instance;

        public static ClassResultByDateRange instance
        {
            get
            {
                if (_instance == null)
                    _instance = new ClassResultByDateRange();
                return _instance;
            }
        }
        ObservableCollection<AllClass> allClass;

        CommonFunctions fun = new CommonFunctions();
        GridCheckMarksSelection gridCheckMarksSA;
        public ClassResultByDateRange()
        {
            InitializeComponent();
            loadfunctions();
            GridExamList.Properties.View.OptionsSelection.MultiSelect = true;
            GridExamList.CustomDisplayText += new DevExpress.XtraEditors.Controls.CustomDisplayTextEventHandler(gridLookUpEdit1_CustomDisplayText);
            GridExamList.Properties.PopulateViewColumns();

            gridCheckMarksSA = new GridCheckMarksSelection(GridExamList.Properties);
            gridCheckMarksSA.SelectionChanged += new GridCheckMarksSelection.SelectionChangedEventHandler(gridCheckMarks_SelectionChanged);
            GridExamList.Properties.Tag = gridCheckMarksSA;
        }
        public void loadfunctions()
        {
            fun.DateFormat(txtTo);
            fun.DateFormat(txtFrom);

            txtClass.Properties.DataSource = fun.GetAllClasses_dt();
            txtClass.Properties.DisplayMember = "name";
            txtClass.Properties.ValueMember = "class_id";

            string query = "SELECT exam_id AS id,`name` ,`date` FROM exam ORDER BY exam_id DESC   ";
            DataTable table = fun.FetchDataTable(query);
            GridExamList.Properties.BeginUpdate();
            try
            {
                gridView1.Columns.Clear();
                GridExamList.Properties.DataSource = null;
                GridExamList.Properties.DataSource = table;
                GridExamList.Properties.DisplayMember = "name";
                GridExamList.Properties.ValueMember = "id";
            }
            finally
            {
                GridExamList.Properties.EndUpdate();
            }
            
           
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
        int classID; //sectionID;
        string dateRang;
        private void btnCRShow_Click(object sender, System.EventArgs e)
        {
            if (txtClass.EditValue == null || txtSection.EditValue == null)
            {
                XtraMessageBox.Show("PLease Fill All fields and try again", "info");
                return;
            }
            fun.loaderform(() => result());
        }

        private void btnPrint_Click(object sender, EventArgs e)
        {
            showReport();
        }
        int countExam = 0;
        public void result()
        {
            classID = Convert.ToInt32(txtClass.EditValue);
            //sectionID = fun.GetSectionIDisClass(txtSection.Text, classID);
            gridClassResult.DataSource = null;
            gridView1.Columns.Clear();

            GridCheckMarksSelection gridCheckMark = GridExamList.Properties.Tag as GridCheckMarksSelection;
            ArrayList valueList = gridCheckMark.Selection;
            int[] exam_array = new int[valueList.Count];
            int count = 0;
            foreach (DataRowView rv in valueList)
            {
                exam_array[count] = Convert.ToInt32(rv["id"]);
                count++;
            }

            if (exam_array.Length > 0)
            {
                dateRang = " AND exam.exam_id IN(" + String.Join(",", exam_array) + ") ";
            }
            else
            {
                dateRang = "and exam.date>='" + Convert.ToDateTime(txtTo.Text).ToString("yyyy-MM-dd") + "' and  exam.date<='" + Convert.ToDateTime(txtFrom.Text).ToString("yyyy-MM-dd") + "'";
            }
            string sub_query = "SELECT GROUP_CONCAT(subject_id) FROM section_subject WHERE section_id  = '" + txtSection.EditValue + "'";
            object all_sub = fun.Execute_Scaler_string(sub_query);
            DataTable allClass = fun.GetAllExamsIsC_S(classID.ToString(), txtSection.EditValue.ToString(), all_sub.ToString(), dateRang);
            string metricmarks = "";
            if (fun.GetSettings("show_metric_number") == "1")
            {
                metricmarks = " ,(SELECT IFNULL(sfv.value,'') AS val FROM student_fields_values AS sfv WHERE sfv.student_id = std.student_id and sfv.field_id = 2) as `9thMarks`" +
                              " ,(SELECT IFNULL(sfv.value, '') AS val FROM student_fields_values AS sfv WHERE sfv.student_id = std.student_id and sfv.field_id = 3) as `10thMarks`";
            }

            if (allClass.Rows.Count > 0)
            {
                var exam_id = ""; var query = "";
                var total_qry = "";
                var obtain_qry = "";
                DataTable all_exams_std_dt = new DataTable();
                string where = "";
                if (exam_array.Length > 0)
                    where = " AND m.exam_id IN(" + String.Join(",", exam_array) + ") ";
                else
                    where = dateRang;
                if (txtIgnoreAbsent.Checked == false)
                {

                    
                    query = "SELECT exam.`name` as exam,m.exam_id,m.`student_id`,SUM(IF(mark_obtained = '-1', 0, mark_obtained)) AS obtain,SUM(tms.`marks`) AS total " +
                        " FROM mark AS m " +
                        " INNER JOIN exam ON exam.exam_id = m.exam_id " +
                        " INNER JOIN tbl_mark_subject AS tms ON tms.`class_id` = m.`class_id` AND tms.`section_id` = m.`section_id` AND tms.`subject_id` = m.`subject_id` AND tms.`exam_id` = m.`exam_id`   " +
                        " WHERE m.class_id = '"+classID+"' and m.section_id in ("+txtSection.EditValue+") AND m.`student_id` > 0 " +where +
                        "   AND m.subject_id IN("+ all_sub + ") " +
                        " GROUP BY m.student_id ,m.exam_id";
                    all_exams_std_dt = fun.FetchDataTable(query);
                }
                else
                {
                    query = "SELECT exam.`name` as exam,m.exam_id,m.`student_id`,SUM(IF(mark_obtained = '-1', 0, mark_obtained)) AS obtain,SUM(IF(mark_obtained = '-1', 0, tms.`marks`)) AS total " +
                        " FROM mark AS m " +
                        " INNER JOIN exam ON exam.exam_id = m.exam_id " +
                        " INNER JOIN tbl_mark_subject AS tms ON tms.`class_id` = m.`class_id` AND tms.`section_id` = m.`section_id` AND tms.`subject_id` = m.`subject_id` AND tms.`exam_id` = m.`exam_id`   " +
                        " WHERE m.class_id = '" + classID + "' and m.section_id in (" + txtSection.EditValue + ") AND m.`student_id` > 0 " + where+
                        "  AND m.subject_id IN(" + all_sub + ") " +
                        " GROUP BY m.student_id ,m.exam_id";
                    all_exams_std_dt = fun.FetchDataTable(query);
                }

                query = "SELECT std.student_id AS ID,std.roll AS Roll ,CONCAT( std.name,' / ',p.name) AS Student,sec.`name` AS Section " + metricmarks + "";
                foreach (DataRow dr in allClass.Rows)
                {
                    query += ",'0' AS `" + dr["name"] + "`";
                }

                query += ",'0' as Total,0 as `%Age`  FROM student as std " +
                    " INNER JOIN section AS sec ON sec.`section_id` = std.`section_id` " +
                    " INNER JOIN parent AS p ON p.`parent_id` = std.`parent_id` " +
                    " WHERE std.`section_id` IN(" + txtSection.EditValue + ") ";
                DataTable table = fun.FetchDataTable(query);
                decimal std_obtain = 0;
                decimal std_total = 0;
                foreach (DataRow dr in table.Rows)
                {
                    std_obtain = 0;
                    std_total = 0;
                    foreach (DataColumn cl in table.Columns)
                    {
                        string c = cl.ColumnName;
                        if (c == "ID" || c == "Roll" || c == "Student" || c == "Section" || c == "9thMarks" || c == "10thMarks" || c == "%Age" || c == "Total") { }
                        else
                        {
                            DataRow[] exam_row = all_exams_std_dt.Select("student_id = '" + dr["ID"] + "' and exam = '" + c + "'");
                            if (exam_row.Length > 0)
                            {
                                dr[c] = exam_row[0]["obtain"].ToString() + "/" + exam_row[0]["total"].ToString();
                                std_obtain += Convert.ToDecimal(exam_row[0]["obtain"]);
                                std_total += Convert.ToDecimal(exam_row[0]["total"]);
                            }
                            else
                                dr[c] = "0/0";
                        }
                    }
                    dr["Total"] = std_obtain + "/" + std_total;
                    if (std_obtain > 0 && std_total > 0)
                        dr["%Age"] = Math.Round(std_obtain / std_total * 100,2);
                }
                DataColumn newColumn = new DataColumn("Rating", typeof(string));
                newColumn.DefaultValue = "-";
                table.Columns.Add(newColumn);
                int R = 0;
                double rs = -1;
                foreach (DataRow row in table.Select("", "%Age DESC"))
                {
                    try
                    {
                        if (rs != Convert.ToDouble(row["%Age"]))
                            R++;
                        row["Rating"] = R;
                        rs = Convert.ToDouble(row["%Age"]);
                        if (rs == 0)
                            break;
                    }
                    catch (Exception ex)
                    {
                        //MessageBox.Show("Student named = '" + row["Student"] + "' has obtained marks like " + row["Obtained"] + " Please set these marks", "");
                    }
                }
                gridView1.Columns.Clear();
                gridClassResult.DataSource = null;
                gridClassResult.DataSource = table;

                gridView1.OptionsView.AllowHtmlDrawHeaders = true;
                gridView1.Appearance.HeaderPanel.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
                gridView1.ColumnPanelRowHeight = 40;
                gridView1.BestFitColumns();

                GridColumn Column1 = gridView1.Columns[0];
                Column1.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
                GridColumn Column2 = gridView1.Columns[2];
                Column2.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;

                /*for (int i = 0; i < allClass.Count + 4; i++)
                {
                    GridColumn Column = gridView1.Columns[i + 4];
                    Column.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
                }*/

                var col = gridView1.Columns["ID"];
                col.OptionsColumn.ReadOnly = true;
                col.Visible = false;
                GridFormatRule gridFormatRuleA = new GridFormatRule();
                FormatConditionRuleDataBar formatConditionRuleDataBarA = new FormatConditionRuleDataBar();

                gridFormatRuleA.Column = gridView1.Columns["%Age"];
                formatConditionRuleDataBarA.MaximumType = FormatConditionValueType.Automatic;
                formatConditionRuleDataBarA.MinimumType = FormatConditionValueType.Automatic;
                gridFormatRuleA.Rule = formatConditionRuleDataBarA;
                gridView1.FormatRules.Add(gridFormatRuleA);
                var col2 = gridView1.Columns["Section"];
                col2.Visible = false;
                gridView1.ExpandAllGroups();
                var col3 = gridView1.Columns["Roll"];
                col3.Width = 50;
                var col4 = gridView1.Columns["Total"];
                col4.Width = 80;
                var col5 = gridView1.Columns["%Age"];
                col5.Width = 50;
                col5.SortIndex = 0;
                col5.SortOrder = DevExpress.Data.ColumnSortOrder.Descending;
            }

        }
        void showReport()
        {
            XtraClassResultSubjectWiseReport report = new XtraClassResultSubjectWiseReport();
            Image logo = fun.Base64ToImage(Login.Logo);
            var school = fun.GetSettings("system_title");
            report.PicIogoBox.Image = logo;
            report.LabTitle.Text = school;
            report.LabAddress.Text = fun.GetSettings("address");
            report.labDate.Text = txtTo.Text + " - " + txtFrom.Text;
            report.labClass.Text = txtClass.Text;
            report.LabSection.Text = txtSection.Text;
            report.LabSub.Text = "";
            report.LabTel.Text = "Class Result by DateRange";
            report.GridControl = gridClassResult;

            ReportPrintTool printTool = new ReportPrintTool(report);
            printTool.PreviewForm.PrintingSystem.Document.AutoFitToPagesWidth = 1;
            printTool.ShowRibbonPreviewDialog(UserLookAndFeel.Default);
        }

        private void gridView1_RowCellStyle(object sender, DevExpress.XtraGrid.Views.Grid.RowCellStyleEventArgs e)
        {
            GridView View = sender as GridView;
            //  allClass = new ObservableCollection<AllClass>();
            //  allClass = fun.GetAllExamsIsC_Sec(classID, sectionID, dateRang);

            if (e.RowHandle >= 0)
            {
                /*for (int i = 0; i < countExam; i++)
                {
                    string present = View.GetRowCellDisplayText(e.RowHandle, View.Columns[i + 5]);
                    if (present == "A")
                    {
                        e.Appearance.ForeColor = Color.Red;
                        //e.Appearance.Font = new Font("Calibri", 10, style: FontStyle.Bold);
                    }
                }*/
            }


            /* for (int i = 0; i < allClass.Count; i++)
             {
                 //var res = allClass[i].Name;
                 //string present = View.GetRowCellDisplayText(e.RowHandle, View.Columns[res]);

                 //if (e.Column.FieldName == res)//
                 //{
                 //    if (present == "A")
                 //    {
                 //        e.Appearance.ForeColor = Color.Red;
                 //    }
                 //}
             }*/
        }

        private void ClassResultBySubjects_Enter(object sender, System.EventArgs e)
        {

        }
        void section_list(string classid)
        {
            txtSection.Properties.DataSource = fun.GetAllSection_dt(classid);
            txtSection.Properties.DisplayMember = "name";
            txtSection.Properties.ValueMember = "section_id";
        }
        private void txtClass_EditValueChanged(object sender, EventArgs e)
        {
            if (txtClass.EditValue != null)
                section_list(txtClass.EditValue.ToString());
        }
    }
}
