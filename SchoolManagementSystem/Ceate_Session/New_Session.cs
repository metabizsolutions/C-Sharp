using DevExpress.XtraEditors.Repository;
using DevExpress.XtraGrid.Views.Grid;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SchoolManagementSystem.Ceate_Session
{
    public partial class New_Session : Form
    {
        CommonFunctions fun = new CommonFunctions();
        public New_Session()
        {
            InitializeComponent();
            fun.selectall_Controls(this);
            loadsession_grid();
        }
        RepositoryItemSearchLookUpEdit ddl1;
        RepositoryItemSearchLookUpEdit ddl2;
        void loadsession_grid()
        {
            DataTable dt = get_data_session();
            dt.Columns.Add("passout", typeof(bool));
            gridsession.DataSource = dt;
            gridView1.Columns["Class"].Group();
            gridView1.ExpandAllGroups();
            gridView1.Columns["section_id"].Visible = false;
            gridView1.Columns["class_id"].Visible = false;

            RepositoryItemCheckEdit ce = new RepositoryItemCheckEdit();
            gridView1.Columns["passout"].ColumnEdit = ce;

            ddl1 = new RepositoryItemSearchLookUpEdit();
            ddl1.DataSource = fun.GetAllClasses_dt();
            ddl1.DisplayMember = "name";
            ddl1.ValueMember = "class_id";
            gridView1.Columns["New_class"].ColumnEdit = ddl1;
            ddl2 = new RepositoryItemSearchLookUpEdit();
            ddl2.DataSource = fun.GetAllSection_dt("-1");
            ddl2.DisplayMember = "name";
            ddl2.ValueMember = "section_id";
            gridView1.Columns["New_section"].ColumnEdit = ddl2;

        }

        private void gridView1_CustomRowCellEditForEditing(object sender, DevExpress.XtraGrid.Views.Grid.CustomRowCellEditEventArgs e)
        {
            GridView gv = sender as GridView;
            if (e.Column.FieldName == "New_section")
            {
                string classN = gv.GetRowCellValue(e.RowHandle, gv.Columns["New_class"]) == null ? "" : gv.GetRowCellValue(e.RowHandle, gv.Columns["New_class"]).ToString();

                ddl2.DataSource = fun.GetAllSection_dt(classN);
                ddl2.DisplayMember = "name";
                ddl2.ValueMember = "section_id";
                gridView1.Columns["New_section"].ColumnEdit = ddl2;
            }
            else if (e.Column.FieldName == "New_class")
            {
                ddl1.DataSource = fun.GetAllClasses_dt();
                ddl1.DisplayMember = "name";
                ddl1.ValueMember = "class_id";
                gridView1.Columns["New_class"].ColumnEdit = ddl1;
            }
            else
                return;
        }

        private void Btn_CreateSession_Click(object sender, System.EventArgs e)
        {
            fun.loaderform(()=>{
                DataTable dt = gridsession.DataSource as DataTable;
                string PreviousYear = DateTime.Now.AddYears(-1).Year.ToString();
                string CurrentYear = DateTime.Now.Year.ToString();
                string NextYear = DateTime.Now.AddYears(1).Year.ToString();
                string PreviousSession_Name = "tnsbay_school_" + PreviousYear + "_" + CurrentYear;
                string query = "show databases like '%" + PreviousSession_Name + "%'";
                dt = fun.FetchDataTable(query);
                if (dt.Rows.Count == 0)
                {
                    //step 1
                    query = "Create database " + PreviousSession_Name + ";";
                    fun.ExecuteQuery(query);
                    //step 2 
                    TakeBackup(PreviousSession_Name);
                    //step 3
                    Upload_Backup(PreviousSession_Name);
                    //step 4
                    insert_session_rowintable();
                    //step 5
                    promot_class();//promot_classes(PreviousYear + "_" + CurrentYear);
                }
                else
                {
                    MessageBox.Show("Session With Named '" + PreviousSession_Name + "' is Alreadey Created in DataBase Please Chack That");
                    return;
                }
            });
            
        }

        internal void TakeBackup(string databsename)
        {
            DateTime now = DateTime.Now;
            String lastdateofmonth = new DateTime(now.Year, now.Month, DateTime.DaysInMonth(now.Year, now.Month)).ToString("yyyy-MM-dd");
            string partialName = "sessionBackup"; // its a Folder Name
            string path = Directory.GetCurrentDirectory();
            string file_name = databsename + ".sql";
            string full_path = path + "/" + partialName + "/" + file_name;
            if (path.Trim() != "")
            {
                try
                {
                    string FolderName = Directory.GetDirectories(path).Where(s => s.Equals(partialName)).LastOrDefault();
                    if (FolderName != partialName)
                    {
                        System.IO.Directory.CreateDirectory(partialName);
                    }
                    using (MySqlConnection conn = new MySqlConnection(Login.constring))
                    {
                        using (MySqlCommand cmd = new MySqlCommand())
                        {
                            using (MySqlBackup mb = new MySqlBackup(cmd))
                            {
                                cmd.Connection = conn;
                                conn.Open();
                                mb.ExportToFile(full_path);
                                conn.Close();
                            }
                        }

                    }
                }
                catch (Exception e)
                {
                    //MessageBox.Show(e.Message, "Auto BackUp Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        internal void Upload_Backup(string databsename)
        {
            string ConnectionString = ConfigurationManager.ConnectionStrings["tnsbay_school_Connection"].ConnectionString;
            ConnectionString = ConnectionString.Replace("tnsbay_school", databsename);
            string partialName = "sessionBackup"; // its a Folder Name
            string path = Directory.GetCurrentDirectory();
            string file_name = databsename + ".sql";
            string full_path = path + "\\" + partialName + "\\" + file_name;
            try
            {
                using (MySqlConnection conn = new MySqlConnection(ConnectionString))
                {
                    using (MySqlCommand cmd = new MySqlCommand())
                    {
                        cmd.Connection = conn;
                        conn.Open();
                        cmd.CommandText = "SET GLOBAL max_allowed_packet=1024*1024*1024;";
                        cmd.ExecuteNonQuery();
                        using (MySqlBackup mb = new MySqlBackup(cmd))
                        {
                            mb.ExportInfo.MaxSqlLength = 1024 * 1024 * 1024; // 1GB
                            mb.ImportFromFile(full_path);
                        }
                        conn.Close();
                    }

                }
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message, "Uploading Backup Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
        }

        void insert_session_rowintable()
        {
            string start = DateTime.Now.Year.ToString();
            string End = DateTime.Now.AddYears(1).Year.ToString();
            string name = start + "-" + End;
            string query = "select * from session order by session_id desc";
            DataTable dt = fun.FetchDataTable(query);
            foreach (DataRow dr in dt.Rows)
            {
                query = "UPDATE `session` SET `session_id`=`session_id`+1,`default_session`=0 WHERE `session_id`='" + dr["session_id"] + "'";
                fun.ExecuteQuery(query);
            }
            query = "INSERT INTO session(session_id, name,year_start,year_end,default_session) VALUES " +
                "(1,'" + name + "','" + start + "','" + End + "',1)";
            fun.ExecuteQuery(query);
        }

        void promot_class()
        {
            String student_id;
            DataTable session_dt = gridsession.DataSource as DataTable;
            //Sorting the Table
            DataView dv = session_dt.DefaultView;
            dv.Sort = "New_section ASC";
            DataTable dt = dv.ToTable();
            foreach (DataRow dr in dt.Rows)
            {
                string passout = dr["passout"].ToString();
                var val = (Convert.ToBoolean(string.IsNullOrEmpty(passout) ?"False": passout) == true) ? 1 : 0;
                string old_class_id = dr["class_id"].ToString();
                string old_section_id = dr["section_id"].ToString();

                string newclass_id = dr["New_class"].ToString();
                string new_section_id = dr["New_section"].ToString();
                if (Convert.ToInt32(string.IsNullOrEmpty(new_section_id)?"0":new_section_id) <= 0 && val == 1) 
                {
                    string query = "update student set `passout` = '" + val + "' where class_id = '" + old_class_id + "' and section_id = '" + old_section_id + "'";
                    fun.FetchDataTable(query);
                }
                else if(Convert.ToInt32(string.IsNullOrEmpty(new_section_id) ? "0" : new_section_id) > 0)
                {
                    

                    string myquery = "SELECT tbl1.subject_id AS pID, tbl1.marks AS pmarks, " +
                                "tbl2.subject_id AS nID, tbl2.marks AS nmarks, tbl2.exam_id " +
                                "FROM(" +
                                    "SELECT subject.name, subject.subject_code, subject.subject_id, tms.marks, tms.exam_id FROM `subject`  " +
                                    "INNER JOIN tbl_mark_subject AS tms ON tms.subject_id = subject.subject_id " +
                                    "WHERE subject.section_id = {0} " +
                                ") AS tbl1 " +
                                "INNER JOIN( " +
                                    "SELECT subject.name, subject.subject_code, subject.subject_id, tms.marks, tms.exam_id FROM `subject`  " +
                                    "INNER JOIN tbl_mark_subject AS tms ON tms.subject_id = subject.subject_id " +
                                    "WHERE subject.section_id = {1}  " +
                                ") AS tbl2 ON(tbl2.name = tbl1.name OR tbl2.subject_code = tbl1.subject_code) AND tbl1.exam_id = tbl2.exam_id";
                    myquery = String.Format(myquery, old_section_id, new_section_id);
                    DataTable tablesub = fun.FetchDataTable(myquery);

                    string query = "select * from student where class_id = '" + old_class_id + "' and section_id = '" + old_section_id + "'";
                    DataTable studentdt = fun.FetchDataTable(query);
                    foreach (DataRow row in studentdt.Rows)
                    {
                        student_id = row["student_id"].ToString();
                        fun.ChangeSection(student_id, old_section_id, new_section_id, tablesub);
                    }
                }
            }
        }
        void promot_classes(string sec_cla)
        {
            DataTable dt = gridsession.DataSource as DataTable;

            for (int i = 0; i < gridView1.DataRowCount; i++)
            {
                int namedigit = Convert.ToInt32(gridView1.GetRowCellValue(i, "name_digit"));
                int degreeid = Convert.ToInt32(gridView1.GetRowCellValue(i, "degree_id"));
                string cls = gridView1.GetRowCellValue(i, "Class").ToString();
                int row_class_id = Convert.ToInt32(gridView1.GetRowCellValue(i, "class_id"));
                int row_section_id = Convert.ToInt32(gridView1.GetRowCellValue(i, "section_id"));

                string query = "select * from class where degree_id ='" + degreeid + "' and name_digit =1+" + namedigit + " limit 1";
                DataTable classdt = fun.FetchDataTable(query);
                long classid = 0;
                if (classdt.Rows.Count <= 0)
                {
                    query = "INSERT INTO `class`(`name`, `session_id`, `degree_id`, `name_digit`) VALUES " +
                        "('" + cls + namedigit + "',1,'" + degreeid + "',1+" + namedigit + ")";
                    classid = fun.Execute_Insert(query);
                }
                else
                    classid = Convert.ToInt64(classdt.Rows[0]["class_id"]);
                query = "INSERT INTO `section`(`name`, `days`, `class_id`) VALUES " +
                    "('" + (degreeid + i) + "_New','Monday, Tuesday, Wednesday, Thursday, Friday, Saturday','" + classid + "')";
                long sectionid = fun.Execute_Insert(query);

                query = "select * from student where class_id = '" + row_class_id + "' and section_id = '" + row_section_id + "'";
                DataTable studentdt = fun.FetchDataTable(query);

                foreach (DataRow stddr in studentdt.Rows)
                {
                    query = "UPDATE student SET class_id='" + classid + "' ,section_id='" + sectionid + "' where student_id='" + stddr["student_id"] + "'";
                    fun.Execute_Query(query);
                }
            }
        }

        private void BtnPDelete_Click(object sender, EventArgs e)
        {
            gridView1.DeleteRow(gridView1.GetFocusedDataSourceRowIndex());
        }

        private void toggle_old_new_Toggled(object sender, EventArgs e)
        {
            loadsession_grid();
        }
         private DataTable get_data_session()
        {
            bool val = Convert.ToBoolean(toggle_old_new.EditValue);
            string query = "";
            if (val) //old classes
            {
                query = "select sec.class_id,cla.name as Class,sec.section_id,sec.name as Section,sec.class_id as New_class,sec.section_id as New_section " +
                " from section as sec join class as cla on cla.class_id = sec.class_id ";
            }
            else // new classess
            {
                query = "select sec.class_id,cla.name as Class,sec.section_id,sec.name as Section,0 as New_class,0 as New_section " +
                 " from section as sec join class as cla on cla.class_id = sec.class_id ";
            }
            DataTable dt = fun.FetchDataTable(query);
            return dt;
        }
    }
}
