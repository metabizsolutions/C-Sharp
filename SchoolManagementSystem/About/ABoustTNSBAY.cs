using System;
using System.ComponentModel;
using System.Data;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using MySql.Data.MySqlClient;
using SchoolManagementSystem.Ceate_Session;
using System.Configuration;
using System.Drawing;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using System.IO;
using System.IO.Compression;
using System.Collections.Generic;

namespace SchoolManagementSystem.About
{
    public partial class ABoustTNSBAY : XtraUserControl
    {
        private static ABoustTNSBAY _instance;
        public static ABoustTNSBAY instance
        {
            get
            {
                if (_instance == null)
                    _instance = new ABoustTNSBAY();
                return _instance;
            }
        }
        systemvalidation objvalidation = new systemvalidation();
        CommonFunctions fun = new CommonFunctions();
        Main_FD main;
        public ABoustTNSBAY(Main_FD maingpage)
        {
            main = maingpage;
            InitializeComponent();
            loadfunctions();

        }
        public ABoustTNSBAY()
        {
            InitializeComponent();
            loadfunctions();
        }
        public void loadfunctions()
        {
            activate();
        }
        void activate()
        {
            string[] system_info = objvalidation.activate();
            label_keyinfo_np.Text = system_info[1];

        }
        private void BtnACTIVITE_Click(object sender, EventArgs e)
        {
            if (txtActivite.Text.Trim() == "")
            {
                MessageBox.Show("Enter correct Code", "Info");
                return;
            }
            string activation_key = txtActivite.Text.Trim();
            string school_code = fun.GetSettings("school_code");
            string response = objvalidation.check_registeration(activation_key, school_code);
            string key_info = objvalidation.extract_keyinfo_softpitch(activation_key);

            if (response == "OK")
            {
                MySqlConnection con = new MySqlConnection(Login.constring);
                con.Open();
                string query = "UPDATE settings set description = '" + txtActivite.Text.Trim() + "',sync='0' WHERE type = 'activition_key'; ";
                MySqlCommand cmd = new MySqlCommand(query, con);
                cmd.ExecuteNonQuery();
                con.Close();

                MessageBox.Show("Congratulation Your key Upgraded! \n " + key_info);
            }
            else
            {
                MessageBox.Show(response);
            }
            activate();
        }

        static string live_db = Login.compare_server;

        DataTable Livedb = new DataTable();
        DataTable livedb_view = new DataTable();
        DataTable Localdb = new DataTable();
        //Compare DB
        BackgroundWorker bw = new BackgroundWorker();
        private void btnCompareDB_Click(object sender, EventArgs e)
        {
            btnCompareDB.Enabled = false;
            main.lbl_comparetablename.Visible = true;
            main.progressBarControl.Visible = true;
            main.lbl_comparetablename.Text = "Comparison Starting please wait";
            bw.DoWork += Bw_DoWork;
            bw.RunWorkerCompleted += Bw_RunWorkerCompleted;
            bw.ProgressChanged += Bw_ProgressChanged;
            bw.WorkerReportsProgress = true;
            if (!bw.IsBusy)
                bw.RunWorkerAsync();
        }
        /*private void Bw_DoWork(object sender, DoWorkEventArgs e)
        {
            string query = "show full tables where Table_Type = 'BASE TABLE';";
            Livedb = fun.Compare_FetchDataTable(query, live_db);
            Localdb = fun.FetchDataTable(query);

            query = "SHOW FULL TABLES WHERE TABLE_TYPE LIKE 'VIEW';";
            livedb_view = fun.Compare_FetchDataTable(query, live_db);

            int sum = 1;
            int total = Livedb.Rows.Count;
            int percent = 0;
            foreach (DataRow liverow in Livedb.Rows)
            {
                query = "SHOW TABLES LIKE '" + liverow[0] + "'";
                DataTable hastable = fun.FetchDataTable(query);
                query = "DESCRIBE " + liverow[0] + "";
                DataTable str = fun.Compare_FetchDataTable(query, live_db);
                int count = 0;

                foreach (DataRow str_row in str.Rows)
                {
                    string defaultval = "" + str_row["Null"] == "NO" ? "NOT NULL" : str_row["Default"].ToString() == "NULL" ? " NULL" : !string.IsNullOrEmpty(str_row["Default"].ToString()) ? "'" + str_row["Default"].ToString() + "'" : " ";
                    string primery = "";
                    if (defaultval != "NOT NULL" && !string.IsNullOrEmpty(defaultval) && defaultval != " ")
                        defaultval = "DEFAULT " + defaultval;
                    if (str_row["Key"].ToString() == "PRI")
                    {
                        primery = str_row["Extra"] + " PRIMARY KEY";
                    }
                    if (liverow[0].ToString() == "student" && str_row["Field"].ToString() == "roll")
                    { }

                    else if (hastable.Rows.Count <= 0)
                    {
                        if (count == 0)
                            query = "create table " + liverow[0] + "(`" + str_row["Field"] + "` " + str_row["Type"] + " NOT NULL " + primery + ");";
                        else
                            query = "ALTER TABLE " + liverow[0] + " ADD `" + str_row["Field"] + "` " + str_row["Type"] + " " + defaultval + ";";
                        fun.ExecuteQuery(query);
                    }
                    else
                    {
                        query = "SHOW COLUMNS FROM " + liverow[0] + " LIKE '" + str_row["Field"] + "';";
                        DataTable hascol = fun.FetchDataTable(query);
                        if (hascol.Rows.Count > 0)
                            query = "ALTER TABLE " + liverow[0] + " CHANGE `" + str_row["Field"] + "` `" + str_row["Field"] + "` " + str_row["Type"] + " " + defaultval + " " + str_row["Extra"] + ";";
                        else
                            query = "ALTER TABLE " + liverow[0] + " ADD `" + str_row["Field"] + "` " + str_row["Type"] + " " + defaultval + " " + str_row["Extra"] + ";";
                        fun.ExecuteQuery(query);
                    }
                    count++;
                }

                if (liverow[0].ToString() == "settings")
                {
                    query = "select * from settings";
                    DataTable setting = fun.Compare_FetchDataTable(query, live_db);
                    foreach (DataRow dr in setting.Rows)
                    {
                        query = "select * from settings where type = '" + dr["type"] + "'";
                        DataTable local_set = fun.FetchDataTable(query);
                        if (local_set.Rows.Count <= 0)
                        {
                            query = "INSERT INTO `settings`(`type`, `description`) VALUES ('" + dr["type"] + "','" + dr["description"] + "')";
                            fun.ExecuteQuery(query);
                        }
                    }
                }


                percent = sum * 100 / total;
                comparetable_name = liverow[0].ToString() + "      " + total + "/" + sum;
                sum++;
                bw.ReportProgress(percent);
                if (bw.CancellationPending)
                {
                    e.Cancel = true;
                    bw.ReportProgress(0);
                    return;
                }
            }
            total = livedb_view.Rows.Count;
            foreach (DataRow view_row in livedb_view.Rows)
            {
                query = "SHOW CREATE VIEW `" + view_row[0] + "`;";
                DataTable view_dt = fun.Compare_FetchDataTable(query, live_db);
                if (view_dt.Rows.Count > 0)
                {
                    query = "DROP VIEW IF EXISTS `" + view_dt.Rows[0]["View"] + "`";
                    fun.ExecuteQuery(query);
                }

                string create_view = view_dt.Rows[0]["Create View"].ToString().Replace(live_view_definer, " ");
                fun.ExecuteQuery(create_view);

                percent = sum * 100 / total;
                comparetable_name = view_row[0].ToString() + "      " + total + "/" + sum;
                sum++;
                bw.ReportProgress(percent);
                if (bw.CancellationPending)
                {
                    e.Cancel = true;
                    bw.ReportProgress(0);
                    return;
                }
            }
            e.Result = sum;
        }
        */
        private void Bw_DoWork(object sender, DoWorkEventArgs e)
        {
            string url = Login.sarwar_api_path + "Desktop_Api/get_tables_views";
            JObject json = fun.get_data(url);
            JArray all_tables = JArray.Parse(json["tables"].ToString());
            Livedb = JsonConvert.DeserializeObject<DataTable>(all_tables.ToString());

            JArray all_views = JArray.Parse(json["views"].ToString());
            livedb_view = JsonConvert.DeserializeObject<DataTable>(all_views.ToString());

            string query = "show full tables where Table_Type = 'BASE TABLE';";
            Localdb = fun.FetchDataTable(query);

            int sum = 1;
            int total = Livedb.Rows.Count;
            int percent = 0;
            foreach (DataRow liverow in Livedb.Rows)
            {
                query = "SHOW TABLES LIKE '" + liverow[0] + "'";
                DataTable hastable = fun.FetchDataTable(query);

                url = Login.sarwar_api_path + "Desktop_Api/get_table_info?tablename=" + liverow[0] + "&&type=1";
                JObject tb_str = fun.get_data(url);
                JArray jstr = JArray.Parse(tb_str["table_str"].ToString());
                DataTable str = JsonConvert.DeserializeObject<DataTable>(jstr.ToString());
                int count = 0;
                if (liverow[0].ToString() == "student")
                {
                    query = "UPDATE student AS s SET s.`birthday` = DATE(STR_TO_DATE(s.`birthday`, '%Y-%m-%d')); ";
                    fun.ExecuteQuery(query);
                    query = "UPDATE student AS s SET s.`birthday` = '0001-01-01' where s.`birthday` = '0000-00-00'; ";
                    fun.ExecuteQuery(query);
                }
                else if (liverow[0].ToString() == "teacher")
                {
                    query = "UPDATE teacher AS t SET t.`birthday` = DATE(STR_TO_DATE(t.`birthday`, '%Y-%m-%d')), t.`JoiningDate` = DATE(STR_TO_DATE(t.`JoiningDate`, '%Y-%m-%d')); ";
                    fun.ExecuteQuery(query);
                    query = "UPDATE teacher AS t SET t.`birthday` = '0001-01-01' where t.`birthday` = '0000-00-00'; ";
                    query += "UPDATE teacher AS t SET t.`JoiningDate` = '0001-01-01' where t.`JoiningDate` = '0000-00-00'; ";
                    fun.ExecuteQuery(query);
                }
                foreach (DataRow str_row in str.Rows)
                {
                    query = "";
                    string defaultval = "" + str_row["Null"] == "NO" ? "NOT NULL" : str_row["Default"].ToString() == "NULL" ? " NULL" : !string.IsNullOrEmpty(str_row["Default"].ToString()) ? "'" + str_row["Default"].ToString() + "'" : " ";
                    string primery = "";
                    if (defaultval != "NOT NULL" && !string.IsNullOrEmpty(defaultval) && defaultval != " ")
                        defaultval = "DEFAULT " + defaultval;
                    if (str_row["Key"].ToString() == "PRI")
                    {
                        primery = str_row["Extra"] + " PRIMARY KEY";
                    }
                    if (liverow[0].ToString() == "student" && str_row["Field"].ToString() == "roll")
                    { }

                    else if (hastable.Rows.Count <= 0)
                    {
                        if (count == 0)
                            query = "create table " + liverow[0] + "(`" + str_row["Field"] + "` " + str_row["Type"] + " NOT NULL " + primery + ");";
                        else
                            query = "ALTER TABLE " + liverow[0] + " ADD `" + str_row["Field"] + "` " + str_row["Type"] + " " + defaultval + ";";
                        fun.ExecuteQuery(query);
                    }
                    else
                    {
                        query = "SHOW COLUMNS FROM " + liverow[0] + " LIKE '" + str_row["Field"] + "';";
                        DataTable hascol = fun.FetchDataTable(query);
                        if (hascol.Rows.Count > 0)
                            query = "ALTER TABLE " + liverow[0] + " CHANGE `" + str_row["Field"] + "` `" + str_row["Field"] + "` " + str_row["Type"] + " " + defaultval + " " + str_row["Extra"] + ";";
                        else
                            query = "ALTER TABLE " + liverow[0] + " ADD `" + str_row["Field"] + "` " + str_row["Type"] + " " + defaultval + " " + str_row["Extra"] + ";";
                        fun.ExecuteQuery(query);
                    }
                    //if(!string.IsNullOrEmpty(query))
                      //  myall_quires += System.Environment.NewLine + query;
                    count++;
                }
                
                
                if (liverow[0].ToString() == "settings")
                {
                    url = Login.sarwar_api_path + "Desktop_Api/get_table_info?tablename=" + liverow[0] + "&&type=2";
                    tb_str = fun.get_data(url);
                    jstr = JArray.Parse(tb_str["table_str"].ToString());
                    DataTable setting = JsonConvert.DeserializeObject<DataTable>(jstr.ToString());
                    foreach (DataRow dr in setting.Rows)
                    {
                        query = "select * from settings where type = '" + dr["type"] + "'";
                        DataTable local_set = fun.FetchDataTable(query);
                        if (local_set.Rows.Count <= 0)
                        {
                            query = "INSERT INTO `settings`(`type`, `description`) VALUES ('" + dr["type"] + "','" + dr["description"] + "')";
                            fun.ExecuteQuery(query);
                        }
                        //if (!string.IsNullOrEmpty(query))
                          //  myall_quires += System.Environment.NewLine + query;
                    }
                }

                percent = sum * 100 / total;
                comparetable_name = liverow[0].ToString() + "      " + total + "/" + sum;
                sum++;
                bw.ReportProgress(percent);
                if (bw.CancellationPending)
                {
                    e.Cancel = true;
                    bw.ReportProgress(0);
                    return;
                }
            }
            //string showmyquires = myall_quires;
            
            total = livedb_view.Rows.Count;
            foreach (DataRow view_row in livedb_view.Rows)
            {
                url = Login.sarwar_api_path + "Desktop_Api/get_table_info?tablename=" + view_row[0] + "&&type=3";
                JObject tb_str = fun.get_data(url);
                JArray jstr = JArray.Parse(tb_str["table_str"].ToString());
                DataTable view_dt = JsonConvert.DeserializeObject<DataTable>(jstr.ToString());
                if (view_dt.Rows.Count > 0)
                {
                    query = "DROP VIEW IF EXISTS `" + view_dt.Rows[0]["View"] + "`";
                    fun.ExecuteQuery(query);
                }

                string create_view = view_dt.Rows[0]["Create View"].ToString();
                fun.ExecuteQuery(create_view);

                percent = sum * 100 / total;
                comparetable_name = view_row[0].ToString() + "      " + total + "/" + sum;
                sum++;
                bw.ReportProgress(percent);
                if (bw.CancellationPending)
                {
                    e.Cancel = true;
                    bw.ReportProgress(0);
                    return;
                }
            }
            e.Result = sum;
    }
        string comparetable_name = "";
        private void Bw_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {

            main.lbl_comparetablename.Text = comparetable_name;
            main.lbl_comparetablename.ForeColor = Color.Black;
            main.progressBarControl.EditValue = e.ProgressPercentage;
            main.progressBarControl.Update();
        }
        private void Bw_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (e.Cancelled)
                main.progressBarControl.EditValue = 0;
            else if (e.Error != null)
                MessageBox.Show(e.Error.Message, "Comparison Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            else
            {
                main.lbl_comparetablename.Text = "Comparison Completed Successfully";
                main.lbl_comparetablename.ForeColor = Color.Green;
                main.progressBarControl.EditValue = 0;
                main.progressBarControl.Update();
                MessageBox.Show("DataBase Compared Successfully", "INFO", MessageBoxButtons.OK, MessageBoxIcon.Information);
                main.lbl_comparetablename.Visible = false;
                main.progressBarControl.Visible = false;
            }
            btnCompareDB.Enabled = true;

            //else
            //    main.progressBar.Value = Convert.ToInt32(e.Result);
        }
        //End Compare DB
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
        //post db with data to live server
        BackgroundWorker bw_pd = new BackgroundWorker();
        private void btn_trubool_shoot_db_Click(object sender, EventArgs e)
        {
            btn_trubool_shoot_db.Enabled = false;
            main.lbl_comparetablename.Visible = true;
            main.progressBarControl.Visible = true;
            main.lbl_comparetablename.Text = "Truble Shoot Starting please wait";
            bw.DoWork += Bw_pd_DoWork;
            bw.RunWorkerCompleted += Bw_pd_RunWorkerCompleted;
            bw.ProgressChanged += Bw_pd_ProgressChanged;
            bw.WorkerReportsProgress = true;
            if (!bw.IsBusy)
                bw.RunWorkerAsync();
        }
        DataTable local_tables = new DataTable();
        string Directorypath_sql = Path.GetDirectoryName(Application.ExecutablePath) + "\\noticeboardfile\\test_db.sql";// its is use to create temporary file in directory and the we delete it after use that
        string Directorypath_zip = Path.GetDirectoryName(Application.ExecutablePath) + "\\noticeboardfile\\test_db.zip";// its is use to create temporary file in directory and the we delete it after use that
        
        
        private void Bw_pd_DoWork(object sender, DoWorkEventArgs e)
        {
            try
            {
                string query = "show full tables where Table_Type = 'BASE TABLE';";
                DataTable all_dt = fun.FetchDataTable(query);
                int sum = 1;
                int total = all_dt.Rows.Count;
                int percent = 0;
                foreach (DataRow dr in all_dt.Rows)
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
                            File.Delete(Directorypath_zip);
                        using (ZipArchive zip = ZipFile.Open(Directorypath_zip, ZipArchiveMode.Create))
                        {
                            zip.CreateEntryFromFile(Directorypath_sql, "test_db.sql");
                        }
                        if (File.Exists(Directorypath_sql))
                            File.Delete(Directorypath_sql);

                        DataTable post_req_dt = new DataTable();
                        post_req_dt.Columns.Add("name");
                        post_req_dt.Columns.Add("data");
                        post_req_dt.Columns.Add("type");
                        DataRow row = post_req_dt.NewRow();
                        row["name"] = "db_file";
                        row["data"] = Directorypath_zip;
                        row["type"] = "file";
                        post_req_dt.Rows.Add(row);
                        string url = Login.sarwar_api_path + "Desktop_Api/myupload";
                        JObject tb_str = fun.post_data(url, post_req_dt);
                        //JArray jstr = tb_str.Value<JArray>("message");
                        //MessageBox.Show("", "Test Database Info", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    }
                    percent = sum * 100 / total;
                    comparetable_name = dr[0].ToString() + "      " + total + "/" + sum;
                    sum++;
                    bw.ReportProgress(percent);
                    if (bw.CancellationPending)
                    {
                        e.Cancel = true;
                        bw.ReportProgress(0);
                        return;
                    }
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
            main.lbl_comparetablename.ForeColor = Color.Black;
            main.progressBarControl.EditValue = e.ProgressPercentage;
            main.progressBarControl.Update();
        }
        private void Bw_pd_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (e.Cancelled)
                main.progressBarControl.EditValue = 0;
            else if (e.Error != null)
                MessageBox.Show(e.Error.Message, "Trubleshoot Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            else
            {
                main.lbl_comparetablename.Text = "Trubleshoot Completed Successfully";
                main.lbl_comparetablename.ForeColor = Color.Green;
                main.progressBarControl.EditValue = 0;
                main.progressBarControl.Update();
                MessageBox.Show("Trubleshoot Successfully", "INFO", MessageBoxButtons.OK, MessageBoxIcon.Information);
                main.lbl_comparetablename.Visible = false;
                main.progressBarControl.Visible = false;
            }
            btn_trubool_shoot_db.Enabled = true;

            //else
            //    main.progressBar.Value = Convert.ToInt32(e.Result);
        }
        //End post db with data to live server
    }
}
