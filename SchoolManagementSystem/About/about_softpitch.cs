using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.IO.Compression;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using sarwar_settings;
using SchoolManagementSystem.Backup;
using SchoolManagementSystem.Ceate_Session;

namespace SchoolManagementSystem.About
{
    public partial class about_softpitch : UserControl
    {
        private static ABoustTNSBAY _instance;
        public static ABoustTNSBAY instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new ABoustTNSBAY();
                }

                return _instance;
            }
        }

        //MySql_settings ms = new MySql_settings();
        private CommonFunctions fun = new CommonFunctions();
        private Main_FD main;
        public about_softpitch(Main_FD maingpage)
        {
            main = maingpage;
            InitializeComponent();
            loadfunctions();
        }
        public about_softpitch()
        {
            InitializeComponent();
            loadfunctions();
        }
        public void loadfunctions()
        {
            t_api.Text = ConfigurationManager.AppSettings["tariq_api_path"];
            s_api.Text = ConfigurationManager.AppSettings["sarwar_api_path"];
            txt_school_code.Text = Login.SchoolCode;

            txt_server.Text = Login.server_ip;
            txt_database_name.Text = Login.CS_database;
            txt_user_id.Text = Login.CS_userid;
            txt_password.Text = Login.CS_password;
        }

        private static string live_db = Login.compare_server;
        private DataTable Livedb = new DataTable();
        private DataTable livedb_indexs = new DataTable();
        private DataTable livedb_view = new DataTable();
        private DataTable livedb_procedure = new DataTable();

        #region Compare DB
        private void btnCompareDB_Click(object sender, EventArgs e)
        {
            btnCompareDB.Enabled = false;
            btn_trubool_shoot_db.Enabled = false;
            btn_sync_db_parent_app.Enabled = false;
            main.panel_percent.Visible = true;
            main.lbl_comparetablename.Text = "Comparison Starting please wait";
            main.lbl_compare_type.Text = "Looking for Data";
            if (!bw_compare_db.IsBusy)
            {
                bw_compare_db.RunWorkerAsync();
            }
        }
        private void bw_compare_db_DoWork(object sender, DoWorkEventArgs e)
        {
            string url = Login.sarwar_api_path + "Desktop_Api/get_tables_views?software_type=1";
            JObject json = fun.get_data(url);
            if (json["my_msg"].ToString() == "OK")
            {
                JArray all_tables = JArray.Parse(json["tables"].ToString());
                Livedb = JsonConvert.DeserializeObject<DataTable>(all_tables.ToString());

                JArray all_indexs = JArray.Parse(json["indexes"].ToString());
                livedb_indexs = JsonConvert.DeserializeObject<DataTable>(all_indexs.ToString());

                JArray all_views = JArray.Parse(json["views"].ToString());
                livedb_view = JsonConvert.DeserializeObject<DataTable>(all_views.ToString());

                JArray all_procedure = JArray.Parse(json["procedure"].ToString());
                livedb_procedure = JsonConvert.DeserializeObject<DataTable>(all_procedure.ToString());

                string query = "", query_chack = "";
                int sum = 1;
                int total = Livedb.Rows.Count;
                int percent = 0;
                comparetype = "Table";
                query = "SET SQL_MODE='';";
                fun.ExecuteQuery(query);
                foreach (DataRow liverow in Livedb.Rows)
                {
                    percent = sum * 100 / total;
                    comparetable_name = liverow[0].ToString() + "      " + total + "/" + sum;

                    sum++;
                    bw_compare_db.ReportProgress(percent);
                    if (bw_compare_db.CancellationPending)
                    {
                        e.Cancel = true;
                        bw_compare_db.ReportProgress(0);
                        return;
                    }
                    query = "SHOW TABLES LIKE '" + liverow[0] + "'";
                    DataTable hastable = fun.FetchDataTable(query);
                    int count = 0;
                    if (liverow[0].ToString() == "student")
                    {
                        query = "UPDATE student AS s SET s.`birthday` = DATE(STR_TO_DATE(s.`birthday`, '%Y-%m-%d')); ";
                        fun.ExecuteQuery(query);

                        query_chack = "SELECT COUNT(*) FROM student AS s where s.`birthday` = '0000-00-00'; ";
                        if (Convert.ToInt32(fun.Execute_Scaler_string(query_chack)) > 0)
                        {
                            query = "UPDATE student AS s SET s.`birthday` = '0001-01-01' where s.`birthday` = '0000-00-00'; ";
                        }

                        fun.ExecuteQuery(query);
                    }
                    else if (liverow[0].ToString() == "teacher")
                    {
                        query = "UPDATE teacher AS t SET t.`birthday` = DATE(STR_TO_DATE(t.`birthday`, '%Y-%m-%d')), t.`JoiningDate` = DATE(STR_TO_DATE(t.`JoiningDate`, '%Y-%m-%d')); ";
                        fun.ExecuteQuery(query);

                        query_chack = "SELECT COUNT(*) FROM teacher AS t where t.`birthday` = '0000-00-00'; ";
                        if (Convert.ToInt32(fun.Execute_Scaler_string(query_chack)) > 0)
                        {
                            query = "UPDATE teacher AS t SET t.`birthday` = '0001-01-01' where t.`birthday` = '0000-00-00'; ";
                        }

                        query_chack = "SELECT COUNT(*) FROM teacher AS t where t.`JoiningDate` = '0000-00-00'; ";
                        if (Convert.ToInt32(fun.Execute_Scaler_string(query_chack)) > 0)
                        {
                            query += "UPDATE teacher AS t SET t.`JoiningDate` = '0001-01-01' where t.`JoiningDate` = '0000-00-00'; ";
                        }

                        fun.ExecuteQuery(query);
                    }
                    else if (liverow[0].ToString() == "invoice")
                    {
                        query = "UPDATE invoice AS t SET t.`due_date` = DATE(STR_TO_DATE(t.`due_date`, '%Y-%m-%d')), t.`to_date` = DATE(STR_TO_DATE(t.`to_date`, '%Y-%m-%d')); ";
                        fun.ExecuteQuery(query);

                        query_chack = "SELECT COUNT(*) FROM invoice AS t WHERE t.`due_date` = '0000-00-00'; ";
                        if (Convert.ToInt32(fun.Execute_Scaler_string(query_chack)) > 0)
                        {
                            query = "UPDATE invoice AS t SET t.`due_date` = '0001-01-01' where t.`due_date` = '0000-00-00'; ";
                        }

                        query_chack = "SELECT COUNT(*) FROM invoice AS t where t.`to_date` = '0000-00-00'; ";
                        if (Convert.ToInt32(fun.Execute_Scaler_string(query_chack)) > 0)
                        {
                            query += "UPDATE invoice AS t SET t.`to_date` = '0001-01-01' where t.`to_date` = '0000-00-00'; ";
                        }

                        fun.ExecuteQuery(query);
                    }
                    url = Login.sarwar_api_path + "Desktop_Api/get_table_info?tablename=" + liverow[0] + "&type=1&software_type=1";
                    JObject tb_str = fun.get_data(url);
                    if (tb_str["my_msg"].ToString() == "OK")
                    {
                        JArray jstr = JArray.Parse(tb_str["table_str"].ToString());
                        DataTable str = JsonConvert.DeserializeObject<DataTable>(jstr.ToString());
                        foreach (DataRow str_row in str.Rows)
                        {
                            query = "";
                            string defaultval = "" + str_row["Null"] == "NO" ? "NOT NULL" : str_row["Default"].ToString() == "NULL" ? " NULL" : !string.IsNullOrEmpty(str_row["Default"].ToString()) ? "'" + str_row["Default"].ToString() + "'" : " ";
                            string primery = "";
                            if(defaultval == "'current_timestamp()'")
                            {
                                defaultval = " DEFAULT current_timestamp() ";
                            }
                            else if (defaultval != "NOT NULL" && !string.IsNullOrEmpty(defaultval) && defaultval != " ")
                            {
                                defaultval = "DEFAULT " + defaultval;
                            }

                            if (str_row["Key"].ToString() == "PRI")
                            {
                                primery = str_row["Extra"] + " PRIMARY KEY";
                            }
                            if (liverow[0].ToString() == "student" && str_row["Field"].ToString() == "addmission_date")
                            {
                                defaultval = " DEFAULT current_timestamp() ";
                            }

                            if (liverow[0].ToString() == "student" && str_row["Field"].ToString() == "roll")
                            { }

                            else if (hastable.Rows.Count <= 0)
                            {
                                if (count == 0)
                                {
                                    query = "create table " + liverow[0] + "(`" + str_row["Field"] + "` " + str_row["Type"] + " NOT NULL " + primery + ");";
                                }
                                else
                                {
                                    query = "ALTER TABLE " + liverow[0] + " ADD `" + str_row["Field"] + "` " + str_row["Type"] + " " + defaultval + ";";
                                }

                                fun.ExecuteQuery(query);
                            }
                            else
                            {
                                query = "SHOW COLUMNS FROM " + liverow[0] + " LIKE '" + str_row["Field"] + "';";
                                DataTable hascol = fun.FetchDataTable(query);
                                if (hascol.Rows.Count > 0)
                                {
                                    query = "ALTER TABLE " + liverow[0] + " CHANGE `" + str_row["Field"] + "` `" + str_row["Field"] + "` " + str_row["Type"] + " " + defaultval + " " + str_row["Extra"] + ";";
                                }
                                else
                                {
                                    query = "ALTER TABLE " + liverow[0] + " ADD `" + str_row["Field"] + "` " + str_row["Type"] + " " + defaultval + " " + str_row["Extra"] + ";";
                                }

                                fun.ExecuteQuery(query);
                            }
                            count++;
                        }

                        string tbl_row_query = "";
                        if (liverow[0].ToString() == "settings")
                        {
                            url = Login.sarwar_api_path + "Desktop_Api/get_table_info?tablename=" + liverow[0] + "&type=2&software_type=1";
                            tb_str = fun.get_data(url);
                            if (tb_str["my_msg"].ToString() == "OK")
                            {
                                jstr = JArray.Parse(tb_str["table_str"].ToString());
                                DataTable setting = JsonConvert.DeserializeObject<DataTable>(jstr.ToString());
                                tbl_row_query = "";

                                foreach (DataRow dr in setting.Rows)
                                {
                                    query = "select * from settings where type = '" + dr["type"] + "'";
                                    DataTable local_set = fun.FetchDataTable(query);
                                    string des = "";

                                    if (local_set.Rows.Count <= 0)
                                    {
                                        tbl_row_query += "INSERT INTO `settings`(`type`, `description`) VALUES ('" + dr["type"] + "','" + dr["description"] + "');";
                                    }
                                    /*if (dr["type"].ToString() == "photo_path")
{
   des = @Path.GetDirectoryName(Application.ExecutablePath).ToString() + @"\Images\Students\";
   tbl_row_query += "update `settings` set `description` = '"+ des.Replace(@"\", @"\\") + "' where `type` = '"+ dr["type"] + "';";
}*/
                                }
                                if (!string.IsNullOrEmpty(tbl_row_query))
                                {
                                    fun.ExecuteQuery(tbl_row_query);
                                }
                            }
                            else
                            {
                                MessageBox.Show("Inetrnet Connection Problem ... Please chack your internet and try again", "Inetnet Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                //if (bw_compare_db.IsBusy)
                                //bw_compare_db.CancelAsync();
                                //e.Cancel = true;
                                bw_compare_db.ReportProgress(0);
                                return;

                            }
                        }
                        if (liverow[0].ToString() == "attendance_status")
                        {
                            url = Login.sarwar_api_path + "Desktop_Api/get_table_info?tablename=" + liverow[0] + "&type=2&software_type=1";
                            tb_str = fun.get_data(url);
                            if (tb_str["my_msg"].ToString() == "OK")
                            {
                                jstr = JArray.Parse(tb_str["table_str"].ToString());
                                DataTable setting = JsonConvert.DeserializeObject<DataTable>(jstr.ToString());
                                tbl_row_query = "";
                                foreach (DataRow dr in setting.Rows)
                                {
                                    query = "select * from attendance_status where status = '" + dr["status"] + "'";
                                    DataTable local_set = fun.FetchDataTable(query);
                                    if (local_set.Rows.Count <= 0)
                                    {
                                        tbl_row_query += "INSERT INTO `attendance_status` (`title`, `status`, `abbr`) VALUES ('" + dr["title"] + "', '" + dr["status"] + "', '" + dr["abbr"] + "');";
                                    }
                                }
                                if (!string.IsNullOrEmpty(tbl_row_query))
                                {
                                    fun.ExecuteQuery(tbl_row_query);
                                }
                            }
                            else
                            {
                                MessageBox.Show("Inetrnet Connection Problem ... Please chack your internet and try again", "Inetnet Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                //if (bw_compare_db.IsBusy)
                                //bw_compare_db.CancelAsync();
                                //e.Cancel = true;
                                bw_compare_db.ReportProgress(0);
                                return;
                            }
                        }
                    }
                    else
                    {
                        MessageBox.Show("Inetrnet Connection Problem ... Please chack your internet and try again", "Inetnet Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        //if (bw_compare_db.IsBusy)
                        //bw_compare_db.CancelAsync();
                        //e.Cancel = true;
                        bw_compare_db.ReportProgress(0);
                        return;
                    }
                }

                sum = 1;
                total = livedb_view.Rows.Count;
                comparetype = "View";
                foreach (DataRow view_row in livedb_view.Rows)
                {
                    percent = sum * 100 / total;
                    comparetable_name = view_row[0].ToString() + "      " + total + "/" + sum;
                    sum++;
                    bw_compare_db.ReportProgress(percent);
                    if (bw_compare_db.CancellationPending)
                    {
                        e.Cancel = true;
                        bw_compare_db.ReportProgress(0);
                        return;
                    }
                    url = Login.sarwar_api_path + "Desktop_Api/get_table_info?tablename=" + view_row[0] + "&type=3&software_type=1";
                    JObject tb_str = fun.get_data(url);
                    if (tb_str["my_msg"].ToString() == "OK")
                    {
                        JArray jstr = JArray.Parse(tb_str["table_str"].ToString());
                        DataTable view_dt = JsonConvert.DeserializeObject<DataTable>(jstr.ToString());
                        if (view_dt.Rows.Count > 0)
                        {
                            query = "DROP TABLE IF EXISTS `" + view_dt.Rows[0]["View"] + "`; ";
                            query += "DROP VIEW IF EXISTS `" + view_dt.Rows[0]["View"] + "`";
                            fun.ExecuteQuery(query);
                        }

                        string create_view = view_dt.Rows[0]["Create View"].ToString().Replace("\"", "`");
                        fun.ExecuteQuery(create_view);
                    }
                    else
                    {
                        MessageBox.Show("Inetrnet Connection Problem ... Please chack your internet and try again", "Inetnet Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        //if (bw_compare_db.IsBusy)
                        //bw_compare_db.CancelAsync();
                        //e.Cancel = true;
                        bw_compare_db.ReportProgress(0);
                        return;
                    }
                }
                
                sum = 1;
                total = livedb_view.Rows.Count;
                comparetype = "Store Procedures";
                foreach (DataRow proce_row in livedb_procedure.Rows)
                {
                    percent = sum * 100 / total;
                    comparetable_name = proce_row["Name"].ToString() + "      " + total + "/" + sum;
                    sum++;
                    bw_compare_db.ReportProgress(percent);
                    if (bw_compare_db.CancellationPending)
                    {
                        e.Cancel = true;
                        bw_compare_db.ReportProgress(0);
                        return;
                    }
                    url = Login.sarwar_api_path + "Desktop_Api/get_table_info?tablename=" + proce_row["Name"] + "&type=4&software_type=1";
                    JObject tb_str = fun.get_data(url);
                    if (tb_str["my_msg"].ToString() == "OK")
                    {
                        JArray jstr = JArray.Parse(tb_str["table_str"].ToString());
                        DataTable view_dt = JsonConvert.DeserializeObject<DataTable>(jstr.ToString());
                        if (view_dt.Rows.Count > 0)
                        {
                            if (!string.IsNullOrEmpty(view_dt.Rows[0]["Create Procedure"].ToString()))
                            {
                                query = "DROP PROCEDURE IF EXISTS `" + view_dt.Rows[0]["Procedure"] + "`; ";
                                fun.ExecuteQuery(query);

                                string[] create_pro = view_dt.Rows[0]["Create Procedure"].ToString().Split(' ');
                                bool start_str = false;
                                string create_procedure = "";
                                foreach (string s in create_pro)
                                {
                                    if (s == "PROCEDURE")
                                        start_str = true;

                                    if (start_str)
                                    {
                                        create_procedure += " " + s;
                                    }
                                }
                                fun.ExecuteQuery("Create " + create_procedure);
                            }
                        }
                    }
                    else
                    {
                        MessageBox.Show("Inetrnet Connection Problem ... Please chack your internet and try again", "Inetnet Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        //if (bw_compare_db.IsBusy)
                        //bw_compare_db.CancelAsync();
                        //e.Cancel = true;
                        bw_compare_db.ReportProgress(0);
                        return;
                    }
                }
                
                sum = 1;
                total = livedb_indexs.Rows.Count;
                comparetype = "Index";
                foreach (DataRow indx_row in livedb_indexs.Rows)
                {
                    percent = sum * 100 / total;
                    comparetable_name = indx_row["table"].ToString() + "      " + total + "/" + sum;
                    sum++;
                    bw_compare_db.ReportProgress(percent);
                    if (bw_compare_db.CancellationPending)
                    {
                        e.Cancel = true;
                        bw_compare_db.ReportProgress(0);
                        return;
                    }
                    query = "SELECT * FROM INFORMATION_SCHEMA.STATISTICS  WHERE TABLE_NAME = '" + indx_row["table"] + "' AND INDEX_NAME = '" + indx_row["index"] + "' AND INDEX_SCHEMA = '" + Login.CS_database + "'";
                    DataTable has_idx = fun.FetchDataTable(query);
                    query = "";

                    if (has_idx.Rows.Count > 0)
                    {
                        query = "ALTER TABLE " + indx_row["table"] + " DROP INDEX " + indx_row["index"] + "; ";
                    }

                    query += "ALTER TABLE " + indx_row["table"] + " ADD KEY " + indx_row["index"] + " (" + indx_row["cols"] + ");";
                    fun.ExecuteQuery(query);
                }


                e.Result = sum;
            }
            else
            {
                //"Please Chack your internet Connection and try again"
                MessageBox.Show(json["my_msg"].ToString(), "", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private string comparetable_name = "";
        private string comparetype = "";
        private void bw_compare_db_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            main.lbl_comparetablename.Text = comparetable_name;
            main.lbl_compare_type.Text = comparetype;
            main.lbl_comparetablename.ForeColor = Color.Black;
            main.progressBarControl.EditValue = e.ProgressPercentage;
            main.progressBarControl.Update();
        }
        private void bw_compare_db_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (e.Cancelled)
            {
                main.progressBarControl.EditValue = 0;
            }
            else if (e.Error != null)
            {
                MessageBox.Show(e.Error.Message, "Comparison Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                main.lbl_comparetablename.Text = "Comparison Completed Successfully";
                main.lbl_compare_type.Text = "Done";
                main.lbl_comparetablename.ForeColor = Color.Green;
                main.progressBarControl.EditValue = 0;
                main.progressBarControl.Update();
                MessageBox.Show("DataBase Compared Successfully", "INFO", MessageBoxButtons.OK, MessageBoxIcon.Information);
                main.panel_percent.Visible = false;
            }
            btnCompareDB.Enabled = true;
            btn_trubool_shoot_db.Enabled = true;
            btn_sync_db_parent_app.Enabled = true;

        }
        #endregion

        #region post db with data to live server
        private BackgroundWorker bw_pd = new BackgroundWorker();
        private string dbcode = "";
        private void btn_trubool_shoot_db_Click(object sender, EventArgs e)
        {
            uploading_db_toLive_server("test");
        }
        private void btn_sync_db_parent_app_Click(object sender, EventArgs e)
        {
            string school_code = Login.SchoolCode;
            if (!string.IsNullOrEmpty(school_code))
            {
                uploading_db_toLive_server(school_code);
            }
            else
            {
                MessageBox.Show("No School Code Found Please Contect to Service Provider", "No School Code Available", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void uploading_db_toLive_server(string code)
        {
            dbcode = code;
            btn_trubool_shoot_db.Enabled = false;
            btn_sync_db_parent_app.Enabled = false;
            btnCompareDB.Enabled = false;
            main.panel_percent.Visible = true;
            main.lbl_comparetablename.Text = "Truble Shoot Starting please wait";
            main.lbl_compare_type.Text = "Looking For Data";
            bw_pd.DoWork += Bw_pd_DoWork;
            bw_pd.RunWorkerCompleted += Bw_pd_RunWorkerCompleted;
            bw_pd.ProgressChanged += Bw_pd_ProgressChanged;
            bw_pd.WorkerReportsProgress = true;
            if (!bw_pd.IsBusy)
            {
                bw_pd.RunWorkerAsync();
            }
        }

        private string Directorypath_sql = Path.GetDirectoryName(Application.ExecutablePath) + "\\noticeboardfile\\test_db.sql";// its is use to create temporary file in directory and the we delete it after use that
        private string Directorypath_zip = Path.GetDirectoryName(Application.ExecutablePath) + "\\noticeboardfile\\test_db.zip";// its is use to create temporary file in directory and the we delete it after use that
        private bool hasdb = false;
        private void Bw_pd_DoWork(object sender, DoWorkEventArgs e)
        {
            try
            {
                string url = Login.sarwar_api_path + "Desktop_Api/db_exists?code=" + dbcode;
                JObject json = fun.get_data(url);
                if (json["my_msg"].ToString() == "OK")
                {
                    hasdb = Convert.ToBoolean(json["hasdb"].ToString());
                    if (hasdb)
                    {
                        string query = "show full tables where Table_Type = 'BASE TABLE';";
                        DataTable all_dt = fun.FetchDataTable(query);
                        int sum = 1;
                        int total = all_dt.Rows.Count;
                        int percent = 0;
                        comparetype = "Table";
                        foreach (DataRow dr in all_dt.Rows)
                        {
                            percent = sum * 100 / total;
                            comparetable_name = dr[0].ToString() + "      " + total + "/" + sum;
                            sum++;
                            bw_pd.ReportProgress(percent);
                            if (bw_pd.CancellationPending)
                            {
                                e.Cancel = true;
                                bw_pd.ReportProgress(0);
                                return;
                            }
                            if (dr[0].ToString() == "tbl_role" || dr[0].ToString() == "tbl_permission") { }
                            else
                            {
                                using (MySqlConnection conn = new MySqlConnection(Login.constring))
                                {
                                    using (MySqlCommand cmd = new MySqlCommand())
                                    {
                                        using (MySqlBackup mb = new MySqlBackup(cmd))
                                        {
                                            cmd.Connection = conn;
                                            conn.Open();
                                            mb.ExportInfo.TablesToBeExportedList = new List<string> {
                                    dr[0].ToString()
                                };
                                            mb.ExportInfo.ExportFunctions = false;
                                            mb.ExportInfo.ExportViews = false;
                                            mb.ExportInfo.ExportTriggers = false;
                                            mb.ExportInfo.ExportEvents = false;
                                            mb.ExportInfo.ExportProcedures = false;
                                            mb.ExportInfo.ExportRoutinesWithoutDefiner = false;
                                            mb.ExportToFile(Directorypath_sql);
                                            conn.Close();
                                        }
                                    }
                                    if (File.Exists(Directorypath_zip))
                                    {
                                        File.Delete(Directorypath_zip);
                                    }

                                    using (ZipArchive zip = ZipFile.Open(Directorypath_zip, ZipArchiveMode.Create))
                                    {
                                        zip.CreateEntryFromFile(Directorypath_sql, "test_db.sql");
                                    }
                                    if (File.Exists(Directorypath_sql))
                                    {
                                        File.Delete(Directorypath_sql);
                                    }

                                    url = Login.sarwar_api_path + "Desktop_Api/myupload?code=" + dbcode + "";
                                    JObject json_data = fun.uploading_file_in_server(url, dbcode, "db_file", Directorypath_zip);
                                    if (json_data["my_msg"].ToString() != "OK")
                                    {
                                        MessageBox.Show("Inetrnet Connection Problem ... Please chack your internet and try again", "Inetnet Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                        e.Cancel = true;
                                        bw_pd.ReportProgress(0);
                                        return;
                                    }
                                }
                            }
                        }
                    }
                    else
                    {
                        MessageBox.Show("No DataBase Existes with your school name in server Please contect to service provider", "No DataBase Exists", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
                else
                {
                    MessageBox.Show("Inetrnet Connection Problem ... Please chack your internet and try again", "Inetnet Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void Bw_pd_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {

            main.lbl_comparetablename.Text = comparetable_name;
            main.lbl_compare_type.Text = comparetype;
            main.lbl_comparetablename.ForeColor = Color.Black;
            main.progressBarControl.EditValue = e.ProgressPercentage;
            main.progressBarControl.Update();
        }
        private void Bw_pd_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (e.Cancelled)
            {
                main.progressBarControl.EditValue = 0;
            }
            else if (e.Error != null)
            {
                MessageBox.Show(e.Error.Message, "Trubleshoot Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                main.lbl_comparetablename.Text = "Trubleshoot Completed Successfully";
                main.lbl_compare_type.Text = "DONE";
                main.lbl_comparetablename.ForeColor = Color.Green;
                main.progressBarControl.EditValue = 0;
                main.progressBarControl.Update();
                MessageBox.Show("Trubleshoot Successfully", "INFO", MessageBoxButtons.OK, MessageBoxIcon.Information);
                main.panel_percent.Visible = false;
            }
            btn_trubool_shoot_db.Enabled = true;
            btn_sync_db_parent_app.Enabled = true;
            btnCompareDB.Enabled = true;
        }
        #endregion End post db with data to live server

        #region Software update files
        private void btn_update_info_Click(object sender, EventArgs e)
        {
            string softpitch_password = fun.GetSettings("device_password");
            if (softpitch_password == "0")
            {
                MySqlConnectionStringBuilder mscsb = new MySqlConnectionStringBuilder(Login.constring);
                mscsb.Server = txt_server.Text.Trim();
                mscsb.Database = txt_database_name.Text.Trim();
                mscsb.UserID = txt_user_id.Text.Trim();
                mscsb.Password = txt_password.Text.Trim();

                if (!sarwar_MySql_Helper.isCOnnection_Valid(mscsb.ConnectionString))
                {
                    MessageBox.Show("Please chack your credantials its not correct", "DB Connection Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                fun.AddUpdateAppSettings("server_id", txt_server.Text.Trim());
                fun.AddUpdateAppSettings("user_id", txt_user_id.Text.Trim());
                fun.AddUpdateAppSettings("password", fun.Encrypt(txt_password.Text.Trim(), "Zee0107"));
                fun.AddUpdateAppSettings("database", txt_database_name.Text.Trim());
                fun.AddUpdateAppSettings("tariq_api_path", t_api.Text.Trim());
                fun.AddUpdateAppSettings("sarwar_api_path", s_api.Text.Trim());
                fun.AddUpdateAppSettings("update_type", "");

                Login.constring = mscsb.ConnectionString;
                Login.tariq_api_path = t_api.Text.Trim();
                Login.sarwar_api_path = s_api.Text.Trim();
                Login.server_ip = txt_server.Text.Trim();
                Login.CS_database = txt_database_name.Text.Trim();
                Login.CS_userid = txt_user_id.Text.Trim();
                Login.CS_password = txt_password.Text.Trim();

                if (!string.IsNullOrEmpty(txt_school_code.Text.Trim()))
                {
                    fun.update_setting(txt_school_code.Text.Trim(), "school_code");
                    Login.SchoolCode = txt_school_code.Text.Trim();
                }

                MessageBox.Show("Values Updated Successfully", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                using (confirm_softpitch de = new confirm_softpitch())
                {
                    if (de.ShowDialog() == DialogResult.Yes) { }
                    else if (de.has_password == true && de.txt_password.Text == softpitch_password)
                    {
                        MySqlConnectionStringBuilder mscsb = new MySqlConnectionStringBuilder(Login.constring);
                        mscsb.Server = txt_server.Text.Trim();
                        mscsb.Database = txt_database_name.Text.Trim();
                        mscsb.UserID = txt_user_id.Text.Trim();
                        mscsb.Password = txt_password.Text.Trim();

                        if (!sarwar_MySql_Helper.isCOnnection_Valid(mscsb.ConnectionString))
                        {
                            MessageBox.Show("Please chack your credantials its not correct", "DB Connection Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            return;
                        }


                        fun.AddUpdateAppSettings("server_id", txt_server.Text.Trim());
                        fun.AddUpdateAppSettings("user_id", txt_user_id.Text.Trim());
                        fun.AddUpdateAppSettings("password", fun.Encrypt(txt_password.Text.Trim(), "Zee0107"));
                        fun.AddUpdateAppSettings("database", txt_database_name.Text.Trim());
                        fun.AddUpdateAppSettings("tariq_api_path", t_api.Text.Trim());
                        fun.AddUpdateAppSettings("sarwar_api_path", s_api.Text.Trim());
                        fun.AddUpdateAppSettings("update_type", "");

                        Login.constring = mscsb.ConnectionString;
                        Login.tariq_api_path = t_api.Text.Trim();
                        Login.sarwar_api_path = s_api.Text.Trim();
                        Login.server_ip = txt_server.Text.Trim();
                        Login.CS_database = txt_database_name.Text.Trim();
                        Login.CS_userid = txt_user_id.Text.Trim();
                        Login.CS_password = txt_password.Text.Trim();


                        if (!string.IsNullOrEmpty(txt_school_code.Text.Trim()))
                        {
                            fun.update_setting(txt_school_code.Text.Trim(), "school_code");
                            Login.SchoolCode = txt_school_code.Text.Trim();
                        }

                        MessageBox.Show("Values Updated Successfully", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        MessageBox.Show("Values Not Update Dated Properly", "Updation Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }
        private void btn_update_software_Click(object sender, EventArgs e)
        {
            if (fun.is_server_pc())
            {
                run_updater("update");
            }
            else
            {
                MessageBox.Show("This is not Server PC ... This operation will work on server PC Only", "INFO", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
        private void btn_upgrade_software_Click(object sender, EventArgs e)
        {
            if (fun.is_server_pc())
            {
                string softpitch_password = fun.GetSettings("device_password");
                using (confirm_softpitch de = new confirm_softpitch())
                {
                    if (de.ShowDialog() == DialogResult.Yes) { }
                    else if (de.has_password == true && de.txt_password.Text == softpitch_password)
                    {
                        run_updater("upgrade");
                    }
                }
            }
            else
            {
                MessageBox.Show("This is not Server PC ... This operation will work on server PC Only", "INFO", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void run_updater(string update_type)
        {
            try
            {
                if (fun.CheckForInternetConnection())
                {
                    fun.AddUpdateAppSettings("update_type", update_type);
                    Process firstProc = new Process();
                    firstProc.StartInfo.FileName = "Softpitch_Software_Updater.exe";
                    firstProc.StartInfo.WorkingDirectory = @"../";
                    firstProc.EnableRaisingEvents = true;
                    firstProc.Start();
                }
                else
                {
                    MessageBox.Show("Please Connect internet first", "internet info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        #endregion

        //create session
        private void btn_create_session_Click(object sender, EventArgs e)
        {
            using (New_Session NS = new New_Session())
            {
                if (NS.ShowDialog() == DialogResult.Yes)
                {

                }
                else
                {

                }
            }
        }

        private void btn_backup_restore_Click(object sender, EventArgs e)
        {
            using (backup_form NS = new backup_form())
            {
                if (NS.ShowDialog() == DialogResult.Yes)
                {

                }
                else
                {

                }
            }
        }
        //End Create session
    }
}
