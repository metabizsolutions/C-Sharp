using DevExpress.XtraEditors.Repository;
using MySql.Data.MySqlClient;
using System;
using System.Collections.ObjectModel;
using System.Data;
using System.Windows.Forms;

namespace SchoolManagementSystem.Exam
{
    public partial class SubjectTotalMarks : DevExpress.XtraEditors.XtraUserControl
    {
        private static SubjectTotalMarks _instance;

        public static SubjectTotalMarks instance
        {
            get
            {
                if (_instance == null)
                    _instance = new SubjectTotalMarks();
                return _instance;
            }
        }
        ObservableCollection<AllClass> allClass = new ObservableCollection<AllClass>();
        CommonFunctions fun = new CommonFunctions();
        static int exam_id = 0;
        static int exam_id_old = 0;
        public SubjectTotalMarks()
        {
            InitializeComponent();
            loadfunctions();
            // 
        }
        public void loadfunctions()
        {
            String sql = "SELECT exam_id,`name`,`date` FROM exam ORDER BY exam_id DESC ";
            DataTable exam_table = fun.FetchDataTable(sql);
            DropdownExamNew.Properties.DataSource = exam_table;
            DropdownExamNew.Properties.DisplayMember = "name";
            DropdownExamNew.Properties.ValueMember = "exam_id";
            FillGridTotalMarks();
        }
        public void permissions_btns(bool add, bool Edit, bool Delete)
        {
            gridView1.OptionsBehavior.Editable = false;
            if (Edit)
                gridView1.OptionsBehavior.Editable = true;
        }
        private void btnMSTM_Click(object sender, System.EventArgs e)
        {
            try
            {
                if (DropdownExamNew.EditValue != null)
                {
                    exam_id = Convert.ToInt32(DropdownExamNew.EditValue.ToString());
                    FillGridTotalMarks();
                }
                else
                {
                    MessageBox.Show("Exam Selection is Required 1!", "Info");
                    return;
                }
            }
            catch (Exception en)
            {
                MessageBox.Show("Exam Selection is Required 2!" + en.ToString());
                return;
            }
        }

        private void empty()
        {
            DropdownExamNew.EditValue = null;
        }



        private void FillGridTotalMarks()
        {
            if (exam_id > 0)
            {
                var on_date = DateTime.Now.ToString("yyyy-MM-dd");
                var due_date = DateTime.Now.AddDays(3).ToString("yyyy-MM-dd");
                /*
                string sql = "INSERT INTO tbl_mark_subject (`class_id`,`section_id`,subject_id,marks,exam_id,teacher_id,incharge_id, room, conducted_on,due_date )  "+
                                " SELECT ss.class_id,ss.section_id,ss.subject_id,0 AS marks,'"+exam_id+"' AS exam_id, ss.teacher_id,0 AS incharge_id, '' AS room,'"+on_date+"' AS on_date, '"+due_date+"' AS due_date "+
                                " FROM `section_subject` AS ss "+
                                " LEFT JOIN tbl_mark_subject AS tms ON tms.`class_id` = ss.`class_id` AND tms.`section_id` = ss.`section_id` AND tms.`subject_id` = ss.`subject_id` AND tms.`exam_id` = '"+exam_id+"' "+
                                " WHERE tms.`exam_id` IS NULL";
                fun.ExecuteQuery(sql);
                string sql = "SELECT tms.smark_id AS ID,sub_def.name AS `Subject` ,sec.name AS Section,cls.name AS Class,tms.marks ,tms.teacher_id AS Teacher, tms.incharge_id AS Incharge, tms.room,tms.conducted_on,tms.due_date " +
                        " FROM tbl_mark_subject AS tms " +
                        " INNER JOIN `subject_default` as sub_def ON sub_def.id = tms.subject_id " +
                        " INNER JOIN section as sec ON sec.section_id = tms.section_id " +
                        " INNER JOIN class as cls ON cls.class_id=tms.class_id " +
                        " WHERE tms.exam_id='{0}'";*/
                string sql = "SELECT tms.smark_id AS ID,'{0}' as `exam_id`,sec_sub.`subject_id`,sub_def.name AS `Subject` ,sec.section_id,sec.name AS Section,cls.class_id,cls.name AS Class,tms.marks ,tms.teacher_id AS Teacher, tms.incharge_id AS Incharge, tms.room,tms.conducted_on,tms.due_date  " +
                    " FROM `section_subject` AS sec_sub " +
                    " INNER JOIN `subject_default` AS sub_def ON sub_def.`id` = sec_sub.`subject_id` " +
                    " INNER JOIN section AS sec ON sec.section_id = sec_sub.section_id " +
                    " INNER JOIN class AS cls ON cls.class_id=sec_sub.class_id " +
                    " LEFT JOIN tbl_mark_subject AS tms ON sec_sub.`subject_id` = tms.subject_id AND sec_sub.`section_id` = tms.`section_id` AND tms.`exam_id` = '{0}'; ";
                sql = String.Format(sql, exam_id);
                DataTable table = fun.FetchDataTable(sql);
                gridManageMarks.DataSource = null;
                gridView1.Columns.Clear();
                gridManageMarks.DataSource = table;
                gridView1.BestFitColumns();
                var col0 = gridView1.Columns["ID"];
                col0.Visible = false;
                gridView1.Columns["exam_id"].Visible = false;
                gridView1.Columns["subject_id"].Visible = false;
                gridView1.Columns["section_id"].Visible = false;
                gridView1.Columns["class_id"].Visible = false;
                var col = gridView1.Columns["Class"];
                col.OptionsColumn.ReadOnly = true;
                var col1 = gridView1.Columns["Subject"];
                col1.OptionsColumn.ReadOnly = true;
                var col2 = gridView1.Columns["Section"];
                col2.OptionsColumn.ReadOnly = true;

                gridView1.Columns["Class"].FieldNameSortGroup = "class_id";
                gridView1.Columns["Class"].GroupIndex = 0;
                gridView1.Columns["Section"].FieldNameSortGroup = "section_id";
                gridView1.Columns["Section"].GroupIndex = 1;

                RepositoryItemSearchLookUpEdit TeacherCmbo = new RepositoryItemSearchLookUpEdit();
                String TeacherSQL = "SELECT teacher_id,`name`,designation,subject_code FROM teacher ORDER BY name DESC";
                TeacherCmbo.DataSource = fun.FetchDataTable(TeacherSQL);
                TeacherCmbo.DisplayMember = "name";
                TeacherCmbo.ValueMember = "teacher_id";
                gridView1.Columns["Teacher"].ColumnEdit = TeacherCmbo;
                gridView1.Columns["Incharge"].ColumnEdit = TeacherCmbo;

                RepositoryItemDateEdit dob = new RepositoryItemDateEdit();
                gridView1.Columns["due_date"].ColumnEdit = dob;
                gridView1.Columns["conducted_on"].ColumnEdit = dob;

                gridView1.ExpandAllGroups();
            }
        }

        private void gridView1_RowUpdated(object sender, DevExpress.XtraGrid.Views.Base.RowObjectEventArgs e)
        {
            DataRow row = gridView1.GetDataRow(gridView1.FocusedRowHandle);
            int incharge_id = row["Incharge"].ToString() != "" ? Convert.ToInt32(row["Incharge"].ToString()) : 0;
            int teacher_id = row["Teacher"].ToString() != "" ? Convert.ToInt32(row["Teacher"].ToString()) : 0;
            var conducted_on = string.IsNullOrEmpty(row["conducted_on"].ToString())?DateTime.Now.ToString("yyyy-MM-dd") :fun.ToInDate(row["conducted_on"].ToString());
            var due_date = string.IsNullOrEmpty(row["due_date"].ToString()) ? DateTime.Now.ToString("yyyy-MM-dd") : fun.ToInDate(row["due_date"].ToString());
            string query = "";
            if (!string.IsNullOrEmpty(row["ID"].ToString()))
                query = "UPDATE tbl_mark_subject set marks='" + row["marks"] + "',teacher_id='" + teacher_id + "', incharge_id = '" + incharge_id + "', room = '" + row["room"].ToString() + "' , conducted_on = '" + conducted_on + "' , due_date = '" + due_date + "' WHERE smark_id='" + row["ID"] + "';";
            else
                query = "insert into tbl_mark_subject set exam_id='"+row["exam_id"] + "',subject_id='" + row["subject_id"] + "',section_id='" + row["section_id"] + "',class_id='" + row["class_id"] + "',marks='" + row["marks"] + "',teacher_id='" + teacher_id + "', incharge_id = '" + incharge_id + "', room = '" + row["room"].ToString() + "' , conducted_on = '" + conducted_on + "' , due_date = '" + due_date + "';";

            fun.ExecuteQuery(query);
            FillGridTotalMarks();
        }

        private void SubjectTotalMarks_Enter(object sender, EventArgs e)
        {
            //loadfunctions(); //FillGridTotalMarks();
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
