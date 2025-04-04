using DevExpress.LookAndFeel;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using DevExpress.XtraEditors.Repository;
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
    public partial class ManageMarksGovt : DevExpress.XtraEditors.XtraUserControl
    {
        private static ManageMarksGovt _instance;

        public static ManageMarksGovt instance
        {
            get
            {
                if (_instance == null)
                    _instance = new ManageMarksGovt();
                return _instance;
            }
        }
        ObservableCollection<AllClass> allClass;
        CommonFunctions fun = new CommonFunctions();
        GridCheckMarksSelection gridCheckMarksSA;
        public ManageMarksGovt()
        {
            InitializeComponent();
            loadfunctions();
        }
        public void loadfunctions()
        {
            txtExam.Properties.DataSource = fun.GetAllExams_dt();
            txtExam.Properties.DisplayMember = "name";
            txtExam.Properties.ValueMember = "exam_id";

            txtClass.Properties.DataSource = fun.GetAllClasses_dt();
            txtClass.Properties.DisplayMember = "name";
            txtClass.Properties.ValueMember = "class_id";
        }
        private void txtClass_EditValueChanged(object sender, EventArgs e)
        {
            if (txtClass.EditValue != null)
            {
                txtSection.Properties.DataSource = fun.GetAllSection_dt(txtClass.EditValue.ToString());
                txtSection.Properties.DisplayMember = "name";
                txtSection.Properties.ValueMember = "section_id";
            }
        }
        int alreadyExit = 0;
        int classID, examID;
        string section, subjectID;
        private void btnMMAdd_Click(object sender, EventArgs e)
        {
            if (txtExam.EditValue == null || txtClass.EditValue == null || txtSection.EditValue == null || txtSubject.EditValue == null)
            {
                MessageBox.Show("Fill all fields...!!", "Info");
                return;
            }
            try
            {
                classID = Convert.ToInt32(txtClass.EditValue);
                examID = Convert.ToInt32(txtExam.EditValue);
                section = txtSection.EditValue.ToString();
                subjectID = txtSubject.EditValue.ToString();

                String check_query = "select * from tbl_mark_subject where class_id = '{0}' and section_id in ({1}) and subject_id in ({2}) and exam_id = '{3}' ";
                check_query = String.Format(check_query, classID, section, subjectID, examID);
                DataTable check_table = fun.FetchDataTable(check_query);
                if (check_table.Rows.Count == 0)
                { //No Exam total makrs 
                    check_query = " INSERT INTO tbl_mark_subject(class_id,section_id,subject_id,exam_id,marks,check_list,teacher_id,test_not_deliver,conducted_on,submit_on,due_date,incharge_id,room) " +
                                  " VALUES('{0}','{1}','{2}', '{3}', 100, 0, 0, 0, '{4}', '{4}', '{4}', 0, '0')";
                    check_query = String.Format(check_query, classID, section, subjectID, examID, fun.CurrentDate());
                    fun.ExecuteQuery(check_query);
                }

                var sms = "and student.exam_sms = '1'";
                // allClass = new ObservableCollection<AllClass>();
                DataTable allClass = new DataTable();//fun.GetAllStudentsWId_S_C_S(classID, section, sms);
                foreach (DataRow allclass in allClass.Rows)
                {
                    var query = "SELECT mark.* FROM mark right join student on(student.student_id = mark.student_id)  WHERE student.student_id='" + allclass["student_id"] + "'and exam_id = '" + examID + "' and student.class_id = '" + classID + "' and student.section_id = '" + allclass["section_id"] + "' and subject_id='" + allclass["subject_id"] + "' AND student.passout = 0;";
                    DataTable reader1 = fun.FetchDataTable(query);
                    if (reader1.Rows.Count > 0)
                    {
                        alreadyExit = 1;
                    }
                    if (alreadyExit == 0)
                    {
                        DataRow r = reader1.Rows[0];
                        int total = fun.GetSubjectTotalMarks(Convert.ToInt32(r["class_id"]), Convert.ToInt32(r["section_id"]), Convert.ToInt32(r["subject_id"]), Convert.ToInt32(r["exam_id"]));
                        var totalSubject = (total == 0) ? 0 : total;
                        var q = "INSERT into mark(student_id,subject_id,class_id,section_id,exam_id,mark_obtained,mark_total,sync,ondate) VALUES('" + allclass["student_id"] + "','" + allclass["subject_id"] + "','" + classID + "','" + allclass["section_id"] + "','" + examID + "','0','" + totalSubject + "','0','" + DateTime.Now.ToString("yyyy-MM-dd") + "');";
                        fun.ExecuteInsert(q);
                        var qry = "UPDATE `tbl_mark_subject` SET submit_on='" + DateTime.Now.ToString("yyyy-MM-dd") + "' where class_id = '"+classID+"' and section_id = '"+ allclass["section_id"] + "' and subject_id='" + allclass["subject_id"] + "' and exam_id='" + examID + "';";
                        fun.ExecuteInsert(qry);
                    }
                    alreadyExit = 0;
                }

                FillGridExamManege();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Info");
                return;
            }
        }

        private void FillGridExamManege()
        {
            MySqlConnection con = new MySqlConnection(Login.constring);
            con.Open();
            var classID = txtClass.EditValue;
            var examID = txtExam.EditValue;
            var section_ids = txtSection.EditValue;
            var subjectids = txtSubject.EditValue;

            String sql = "";
            sql = "SELECT mark_id as ID,std.roll as `Roll No`, concat( std.name,' / ',parent.name)  as Student,mark_obtained as `Obtained`,tms.marks as Total,comment as Remarks,m.subject_id,std.section_id " +
                " FROM mark as m " +
                " join student as std on (std.student_id=m.student_id) " +
                " join parent on parent.parent_id=std.parent_id  " +
                " INNER JOIN tbl_mark_subject AS tms ON tms.`class_id` = m.`class_id` AND tms.`section_id` = m.`section_id` AND tms.`subject_id` = m.`subject_id` AND tms.`exam_id` = m.`exam_id`   " +
                " WHERE m.exam_id = '" + examID + "' and m.class_id='" + classID + "'and m.section_id IN(" + section_ids + ") AND std.passout = 0 order by std.roll";
            MySqlCommand cmdP = new MySqlCommand(sql, con);
            MySqlDataAdapter adp = new MySqlDataAdapter(cmdP);
            DataTable table = new DataTable();
            adp.Fill(table);
            gridManageMarks.DataSource = null;
            gridView1.Columns.Clear();
            gridManageMarks.DataSource = CommonFunctions.AutoNumberedTable(table);
            gridView1.IndicatorWidth = 30;
            gridView1.BestFitColumns();
            var col = gridView1.Columns["ID"];
            col.OptionsColumn.ReadOnly = true;
            col.Visible = false;
            var col3 = gridView1.Columns["Roll No"];
            col3.OptionsColumn.ReadOnly = true;
            col3.Width = 55;
            var col1 = gridView1.Columns["Student"];
            col1.OptionsColumn.ReadOnly = true;
            var col2 = gridView1.Columns["Total"];
            col2.OptionsColumn.ReadOnly = true;
            GridColumn Column = gridView1.Columns["Total"];
            Column.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            GridColumn Column2 = gridView1.Columns["Obtained"];
            Column2.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            GridColumn Column3 = gridView1.Columns["Sr#"];
            Column3.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            GridColumn Column4 = gridView1.Columns["Roll No"];
            Column4.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;

            var col4 = gridView1.Columns["Sr#"];
            col4.Width = 40;
        }

        private void gridView1_RowUpdated(object sender, DevExpress.XtraGrid.Views.Base.RowObjectEventArgs e)
        {
            DataRow row = gridView1.GetDataRow(gridView1.FocusedRowHandle);
            MySqlConnection con = new MySqlConnection(Login.constring);
            con.Open();
            var query = "UPDATE `tbl_mark_subject` SET `check_list`=1,test_not_deliver='0' where subject_id='" + row["subject_id"] + "' and exam_id='" + examID + "';";
            query += "UPDATE mark set mark_obtained='" + row[4] + "',comment='" + row[6] + "',sync='0',ondate='" + DateTime.Now.ToString("yyyy-MM-dd") + "' WHERE mark_id='" + row[1] + "';";
            MySqlCommand cmd = new MySqlCommand(query, con);
            cmd.ExecuteNonQuery();
            con.Close();
        }

        private void gridView1_ValidateRow(object sender, DevExpress.XtraGrid.Views.Base.ValidateRowEventArgs e)
        {
            GridColumn obtained = gridView1.Columns["Obtained"];
            GridColumn total = gridView1.Columns["Total"];

            int unitInObtained = Convert.ToInt32(gridView1.GetRowCellValue(gridView1.FocusedRowHandle, obtained));
            int unitInTotal = Convert.ToInt32(gridView1.GetRowCellValue(gridView1.FocusedRowHandle, total));
            if (unitInTotal < unitInObtained)
            {
                gridView1.SetColumnError(obtained, "The value should be less then total marks.");
                gridView1.SetColumnError(total, "The Marks on obtained should be less then this value.");
                gridView1.SetColumnError(null, "Invalid data");
                e.Valid = false;
                e.ErrorText = "'Mark In Obtained' and 'Marks In total' values are not consistent.";

            }
        }

        private void gridView1_InvalidRowException(object sender, DevExpress.XtraGrid.Views.Base.InvalidRowExceptionEventArgs e)
        {
            e.ExceptionMode = ExceptionMode.NoAction;
        }

        private void ManageMarks_Enter(object sender, EventArgs e)
        {

        }

        private void txtSection_EditValueChanged(object sender, EventArgs e)
        {
            if (txtSection.EditValue != null)
            {
                txtSubject.Properties.DataSource = fun.GetAllSubject_by_section_dt(txtSection.EditValue.ToString());
                txtSubject.Properties.DisplayMember = "name";
                txtSubject.Properties.ValueMember = "subject_id";
            }
        }

        private void btnMMPrintP_Click(object sender, EventArgs e)
        {
            XtraManageMarks report = new XtraManageMarks();
            Image logo = fun.Base64ToImage(Login.Logo);
            var school = fun.GetSettings("system_title");
            report.PicIogoBox.Image = logo;
            report.LabTitle.Text = school;
            report.LabAddress.Text = fun.GetSettings("address");
            report.GridControl = gridManageMarks;
            report.LabExam.Text = txtExam.Text;
            report.labClass.Text = txtClass.Text;

            String label_section = "";

            report.LabSection.Text = label_section;// txtSection.Text;
            report.LabSubject.Text = txtSubject.Text;
            report.LabDate.Text = DateTime.Now.ToShortDateString();
            ReportPrintTool printTool = new ReportPrintTool(report);
            printTool.PreviewForm.PrintingSystem.Document.AutoFitToPagesWidth = 1;
            printTool.ShowRibbonPreviewDialog(UserLookAndFeel.Default);
        }
        private void GridView1_CustomRowCellEditForEditing(object sender, DevExpress.XtraGrid.Views.Grid.CustomRowCellEditEventArgs e)
        {
            if (e.Column.FieldName != "Obtained") return;
            GridView gv = sender as GridView;
            if (e.Column.FieldName == "Obtained")
            {
                gv.SetRowCellValue(e.RowHandle, gv.Columns[""], null);
            }
        }
        private void gridManageMarks_EditorKeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                gridView1.MoveNext();
            }
        }

    }
}
