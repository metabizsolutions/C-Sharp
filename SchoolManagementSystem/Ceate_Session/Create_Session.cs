using System;
using System.Configuration;
using System.Data;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using DevExpress.XtraEditors.Repository;
using DevExpress.XtraGrid.Views.Grid;
using MySql.Data.MySqlClient;

namespace SchoolManagementSystem.Ceate_Session
{
    public partial class Create_Session : DevExpress.XtraEditors.XtraUserControl
    {
        CommonFunctions fun = new CommonFunctions();
        private static Create_Session _instance;

        public static Create_Session instance
        {
            get
            {
                if (_instance == null)
                    _instance = new Create_Session();
                return _instance;
            }
        }
        public Create_Session()
        {
            InitializeComponent();
            loadfunctions();
        }
        public void loadfunctions()
        {
            loadclasses();
            loadsession_grid();
        }
        void loadsession_grid()
        {
            string query = "select sec.section_id,sec.class_id,cla.name as Class,cla.degree_id,cla.name_digit,sec.name as Section from section as sec join class as cla on cla.class_id = sec.class_id " +
                " where cla.name_digit > 0 and cla.degree_id > 0 and not cla.degree_id is null order by cla.name_digit desc;";

            gridsession.DataSource = fun.FetchDataTable(query);

            gridView1.Columns["section_id"].Visible = false;
            gridView1.Columns["class_id"].Visible = false;

            RepositoryItemLookUpEdit degreecom = new RepositoryItemLookUpEdit();
            degreecom.DataSource = fun.degrees();
            degreecom.DisplayMember = "name";
            degreecom.ValueMember = "degree_id";
            gridView1.Columns["degree_id"].ColumnEdit = degreecom;

        }
        void loadclasses()
        {
            string query = "select * from class where class_id = 3";
            DataTable classdt = fun.FetchDataTable(query);
            foreach (DataRow dr in classdt.Rows)
            {
                query = "select * from section where class_id='" + dr["class_id"] + "';";
                DataTable sec_dt = fun.FetchDataTable(query);
            }
            //Label lbl = new Label();
            //lbl.AutoSize = false;
            //lbl.Text = "Installment:" + totalins.ToString();
            //lbl.Margin = new Padding(38, 6, 0, 0);
            //lbl.BorderStyle = BorderStyle.Fixed3D;
            //panelinstallment.Controls.Add(lbl);
            //TextBox txt = new TextBox();
            //panelinstallment.Controls.Add(txt);
            //txt.TextChanged += txtPlusSession_TextChanged;
            //txt.Size = new Size(100, 21);
            //txt.Name = "Ins" + totalins;
            //txt.Text = "0";
            //DateTimePicker dtp = new DateTimePicker();
            //panelinstallment.Controls.Add(dtp);
            //dtp.Size = new Size(100, 21);
            //dtp.Format = DateTimePickerFormat.Short;
            //dtp.Name = "dtp" + totalins;
            //panelinstallment.SetFlowBreak(dtp, true);
        }

        private void Btn_CreateSession_Click(object sender, System.EventArgs e)
        {
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
                promot_classes(PreviousYear+"_"+ CurrentYear);
            }
            else
            {
                MessageBox.Show("Session With Named '" + PreviousSession_Name + "' is Alreadey Created in DataBase Please Chack That");
                return;
            }
        }

        string Directorypath = Path.GetDirectoryName(Application.ExecutablePath) + "\\DbBackup\\file.sql";// its is use to create temporary file in directory and the we delete it after use that
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
                                mb.ExportToFile(Directorypath);
                                conn.Close();
                                //fun.EncriptDB(Directorypath, full_path, "!?O??v??");
                                //File.Delete(Directorypath);
                            }
                        }
                        string full_text = System.IO.File.ReadAllText(Directorypath);
                        fun.save_File(full_text, file_name, full_path);
                        //fun.EncriptDB(Directorypath, txtBackup.Text.Trim() + "\\backup -" + DateTime.Now.ToString("yyyy-MM-dd") + ".sql", "!?O??v??")
                        File.Delete(Directorypath);
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
                //fun.DecriptDB(full_path, Directorypath, "!?O??v??");
                string full_text = System.IO.File.ReadAllText(full_path);
                fun.save_File(fun.Decrypt(full_text, "Zee0107"), "file.sql", Directorypath);
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
                            mb.ImportFromFile(Directorypath);
                            File.Delete(Directorypath);
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
            string name = start+"-"+End;
            string query = "select * from session order by session_id desc";
            DataTable dt = fun.FetchDataTable(query);
            foreach(DataRow dr in dt.Rows)
            {
                query = "UPDATE `session` SET `session_id`=`session_id`+1,`default_session`=0 WHERE `session_id`='" + dr["session_id"]+"'";
                fun.ExecuteQuery(query);
            }
            query = "INSERT INTO session(session_id, name,year_start,year_end,default_session) VALUES " +
                "(1,'"+name+"','"+start+"','"+End+"',1)";
            fun.ExecuteQuery(query);
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

                string query = "select * from class where degree_id ='"+ degreeid + "' and name_digit =1+"+ namedigit + " limit 1";
                DataTable classdt = fun.FetchDataTable(query);
                long classid = 0;
                if (classdt.Rows.Count <= 0)
                {
                    query = "INSERT INTO `class`(`name`, `session_id`, `degree_id`, `name_digit`) VALUES " +
                        "('"+ cls + namedigit + "',1,'"+ degreeid + "',1+" + namedigit + ")";
                    classid = fun.Execute_Insert(query);
                }
                else
                    classid = Convert.ToInt64(classdt.Rows[0]["class_id"]);
                query = "INSERT INTO `section`(`name`, `days`, `class_id`) VALUES " +
                    "('"+ (degreeid + i)+ "_New','Monday, Tuesday, Wednesday, Thursday, Friday, Saturday','"+ classid + "')";
                long sectionid = fun.Execute_Insert(query);

                query = "select * from student where class_id = '" + row_class_id + "' and section_id = '" + row_section_id + "'";
                DataTable studentdt = fun.FetchDataTable(query);

                foreach (DataRow stddr in studentdt.Rows)
                {
                    query = "UPDATE student SET class_id='"+ classid + "' ,section_id='"+ sectionid + "' where student_id='"+ stddr["student_id"] + "'";
                    fun.Execute_Query(query);
                }
            }
        }

        private void BtnPDelete_Click(object sender, EventArgs e)
        {
            gridView1.DeleteRow(gridView1.GetFocusedDataSourceRowIndex());
        }
    }
}
