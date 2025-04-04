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
using System.Windows.Forms;

namespace SchoolManagementSystem.Exam
{
    public partial class ClassResultBySubjects : DevExpress.XtraEditors.XtraUserControl
    {
        private static ClassResultBySubjects _instance;

        public static ClassResultBySubjects instance
        {
            get
            {
                if (_instance == null)
                    _instance = new ClassResultBySubjects();
                return _instance;
            }
        }
        ObservableCollection<AllClass> allClass;

        CommonFunctions fun = new CommonFunctions();

        public ClassResultBySubjects()
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

        }
        GridCheckMarksSelection gridCheckMarksSA;
        public void loadfunctions()
        {
            fun.DateFormat(txtTo);
            fun.DateFormat(txtFrom);

            txtClass.Properties.DataSource = fun.GetAllClasses_dt();
            txtClass.Properties.DisplayMember = "name";
            txtClass.Properties.ValueMember = "class_id";

            GridExamList.Text = "";
            GridExamList.Properties.DataSource = fun.GetAllExams_dt();
            GridExamList.Properties.DisplayMember = "name";
            GridExamList.Properties.ValueMember = "exam_id";


            
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
        int classID, sectionID;
        string dateRang;

        private void btnCRShow_Click(object sender, System.EventArgs e)
        {
            if(txtClass.EditValue == null || txtSection.EditValue == null || txtSubject.EditValue == null || GridExamList.EditValue == null)
            {
                MessageBox.Show("Fill all fields....!!", "Info");
                return;
            }
            fun.loaderform(() =>result());
        }

        private void btnPrint_Click(object sender, EventArgs e)
        {
            showReport();
        }
        int countExam = 0;
        int SubjectId;
        public void result()
        {
            classID = Convert.ToInt32(txtClass.EditValue);
            sectionID = Convert.ToInt32(txtSection.EditValue);
            SubjectId = Convert.ToInt32(txtSubject.EditValue);
            gridClassResult.DataSource = null;
            gridView1.Columns.Clear();
            MySqlConnection con = new MySqlConnection(Login.constring);

            GridCheckMarksSelection gridCheckMark = GridExamList.Properties.Tag as GridCheckMarksSelection;
            ArrayList valueList = gridCheckMark.Selection;
            string exam_array = "";
            int count = 0;
            foreach (DataRowView rv in valueList)
            {
                if (count == 0)
                    exam_array = rv["exam_id"].ToString();
                else
                    exam_array = exam_array+"," + rv["exam_id"].ToString();
                count++;
            }
            if (exam_array != "")
                dateRang = " and m.exam_id in (" + exam_array + ")";
            else
                dateRang = "and exam.date>='" + Convert.ToDateTime(txtTo.Text).ToString("yyyy-MM-dd") + "' and  exam.date<='" + Convert.ToDateTime(txtFrom.Text).ToString("yyyy-MM-dd") + "'";
            DataTable allClass = fun.GetAllExamsIsC_S(classID.ToString(),sectionID.ToString(), SubjectId.ToString(), dateRang);
            con.Open();
            if (allClass.Rows.Count > 0)
            {
                var exam_id = "";
                var totalSubject = 0; var query = "";
                string metricmarks = "";
                if (fun.GetSettings("show_metric_number") == "1")
                {
                    metricmarks = "(SELECT IFNULL(sfv.value,'') AS val FROM student_fields_values AS sfv " +
                            " WHERE sfv.student_id = std.student_id and sfv.field_id = 2) as `9thMarks`,(SELECT IFNULL(sfv.value, '') AS val FROM student_fields_values AS sfv " +
                            " WHERE sfv.student_id = std.student_id and sfv.field_id = 3) as `10thMarks`,";
                }
                if (txtIgnoreAbsent.Checked == false)
                {
                    query = "SELECT std.student_id as ID,std.roll As Roll ,concat( std.name,' / ',parent.name) as Student,std.section_id as Section,"+ metricmarks + "";
                    foreach(DataRow dr in allClass.Rows)
                    {
                        int total = Convert.ToInt32(dr["total"]);
                        totalSubject = totalSubject + total;
                        query += " IFNULL(MAX(CASE m.exam_id WHEN '" + dr["exam_id"] + "' THEN IF(mark_obtained = '-1', 'A', IF(mark_obtained = '-2', 'N/A', mark_obtained)) END),0) AS `" + dr["name"] + "(" + total + ")" + "`,";
                    }
                    query += "sum(IF(m.mark_obtained = '-1', 0,m.mark_obtained)) as Obt, convert( concat( sum(IF(m.mark_obtained = '-1' OR m.mark_obtained = '-2', 0,m.mark_obtained))  ,'/'," + totalSubject + "),char(45)) as Total, round( sum(IF(m.mark_obtained = '-1' OR m.mark_obtained = '-2', 0,m.mark_obtained))/" + totalSubject + "*100,1) as `%Age` " +
                        " FROM mark as m " +
                        " inner join student as std on(std.student_id = m.student_id) " +
                        " inner JOIN exam ON exam.`exam_id` = m.`exam_id` " +
                        " join parent on parent.parent_id=std.parent_id "
                        + " where std.passout = 0 and m.class_id ='"+classID+"' AND m.section_id='" + sectionID + "' and m.subject_id='" + SubjectId + "' "+ dateRang + " GROUP BY m.student_id ,m.class_id  order by `Obt`DESC  ;";
                }
                else
                {
                    query = "SELECT std.student_id as ID,std.roll As Roll ,concat( std.name,' / ',parent.name) as Student,std.section_id as Section," + metricmarks + "";
                    foreach (DataRow dr in allClass.Rows)
                    {
                        int total = Convert.ToInt32(dr["total"]);
                        totalSubject = totalSubject + total;
                        query += " IFNULL(MAX(CASE m.exam_id WHEN '" + dr["exam_id"] + "' THEN IF(mark_obtained = '-1', 'A', mark_obtained) END),0) AS `" + dr["name"] + "(" + total + ")" + "`,";
                    }
                    query += "sum(IF(m.mark_obtained = '-1', 0,m.mark_obtained)) as Obt, convert( concat( sum(IF(m.mark_obtained = '-1', 0,m.mark_obtained))  ,'/',SUM(if(mark_obtained= '-1',0,tms.marks))),char(45)) as Total,IFNULL( round( sum(IF(m. mark_obtained = '-1', 0,m. mark_obtained))/SUM(if(mark_obtained= '-1',0,tms.marks))*100,1),0 )as `%Age` " +
                        " FROM mark as m " +
                        " inner join student as std on(std.student_id = m.student_id) " +
                        " inner JOIN exam ON exam.`exam_id` = m.`exam_id` " +
                        " INNER JOIN tbl_mark_subject AS tms ON tms.`class_id` = m.`class_id` AND tms.`section_id` = m.`section_id` AND tms.`subject_id` = m.`subject_id` AND tms.`exam_id` = m.`exam_id` " +
                        " join parent on parent.parent_id=std.parent_id "
                        + " where std.passout = 0 and m.class_id ='" + classID + "' AND m.section_id='" + sectionID + "' and m.subject_id='" + SubjectId + "' "+ dateRang + " GROUP BY m.student_id ,m.class_id  order by `%Age` DESC  ;";
                }
                MySqlCommand cmd = new MySqlCommand(query, con);
                MySqlDataAdapter adp = new MySqlDataAdapter(cmd);
                DataTable table = new DataTable();
                adp.Fill(table);
                table = CommonFunctions.AutoNumberedTable(table);
                DataColumn newColumn = new DataColumn("Rating", typeof(string));
                newColumn.DefaultValue = "-";
                table.Columns.Add(newColumn);
                int R = 0;
                int rs = -1;
                foreach (DataRow row in table.Rows)
                {
                    try
                    {
                        if (rs != Convert.ToInt32(row["%Age"] == null ? 0 : row["%Age"]))
                            R++;
                        row["Rating"] = R;
                        rs = Convert.ToInt32(row["%Age"]);
                        if (rs == 0)
                            break;
                    }
                    catch(Exception ex)
                    {
                        MessageBox.Show(ex.Message, "Age%", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
                gridView1.Columns.Clear();
                gridClassResult.DataSource = null;
                gridClassResult.DataSource = table;
                var col1 = gridView1.Columns["Obt"];
                col1.Visible = false;
                gridView1.OptionsView.AllowHtmlDrawHeaders = true;
                gridView1.Appearance.HeaderPanel.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
                gridView1.ColumnPanelRowHeight = 40;
                gridView1.BestFitColumns();

                GridColumn Column1 = gridView1.Columns[0];
                Column1.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
                GridColumn Column2 = gridView1.Columns[2];
                Column2.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;

                /*for (int i = 0; i < allClass.Count + 5; i++)
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
                con.Close();
                var col3 = gridView1.Columns["Roll"];
                col3.Width = 50;
                var col4 = gridView1.Columns["Total"];
                col4.Width = 80;
                var col5 = gridView1.Columns["%Age"];
                col5.Width = 50;
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
            report.LabSub.Text = txtSubject.Text;
            report.GridControl = gridClassResult;

            ReportPrintTool printTool = new ReportPrintTool(report);
            printTool.PreviewForm.PrintingSystem.Document.AutoFitToPagesWidth = 1;
            printTool.ShowRibbonPreviewDialog(UserLookAndFeel.Default);
        }

        private void gridView1_RowCellStyle(object sender, DevExpress.XtraGrid.Views.Grid.RowCellStyleEventArgs e)
        {
            //   allClass = new ObservableCollection<AllClass>();
            //   allClass = fun.GetAllExamsIsC_S(classID, SubjectId, dateRang);

            GridView View = sender as GridView;
            if (e.RowHandle >= 0)
            {
                /*for (int i = 0; i < countExam; i++)
                {
                    int total = fun.GetSubjectTotalMarks(SubjectId, int.Parse(allClass[i].Salary));
                    var totalSubject = (total == 0) ? 100 : total;
                    var res = allClass[i].Name + "(" + total + ")";

                    string present = View.GetRowCellDisplayText(e.RowHandle, View.Columns[i + 5]);
                    if (present == "A")
                    {
                        if (e.Column.FieldName == res)//
                            e.Appearance.ForeColor = Color.Red;
                        e.Appearance.Font = new Font("Calibri", 10, style: FontStyle.Bold);
                    }
                }*/


                //for (int i = 0; i < allClass.Count; i++)
                //{
                //    int total = fun.GetSubjectTotalMarks(SubjectId, int.Parse(allClass[i].Salary));
                //    var totalSubject = (total == 0) ? 100 : total;
                //    var res = allClass[i].Name + "(" + total + ")";
                //    string present = View.GetRowCellDisplayText(e.RowHandle, View.Columns[res]);

                //    if (e.Column.FieldName == res)//
                //    {
                //        if (present == "A")
                //        {
                //            e.Appearance.ForeColor = Color.Red;
                //            e.Appearance.Font = new Font("Calibri", 10, style: FontStyle.Bold);

                //        }
                //    }
                //}
            }
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
        void subject_list(string sectionid)
        {
            txtSubject.Properties.DataSource = fun.GetAllSubject_by_section_dt(sectionid);
            txtSubject.Properties.DisplayMember = "name";
            txtSubject.Properties.ValueMember = "subject_id";
        }
        private void txtSection_EditValueChanged(object sender, EventArgs e)
        {
            if (txtSection.EditValue != null)
                subject_list(txtSection.EditValue.ToString());
        }
        private void ClassResultBySubjects_Enter(object sender, System.EventArgs e)
        {

        }
    }
}
