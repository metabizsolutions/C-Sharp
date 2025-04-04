using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.IO;
using System.Media;
using System.Windows.Forms;

namespace SchoolManagementSystem.Class
{
    public partial class ManageSession : DevExpress.XtraEditors.XtraUserControl
    {
        CommonFunctions fun = new CommonFunctions();
        private static ManageSession _instance;
        public static ManageSession instance
        {
            get
            {
                if (_instance == null)
                    _instance = new ManageSession();
                return _instance;
            }
        }
        public ManageSession()
        {
            InitializeComponent();
            loadfunctions();
        }

        public void loadfunctions()
        {
            fun.DateFormatOnlyY(txtSYear);
            fun.DateFormatOnlyY(txtEYear);
            FillGridSession();
        }

        ObservableCollection<AllClass> allClass = new ObservableCollection<AllClass>();
        string db = "";
        private void btnSessionAdd_Click(object sender, System.EventArgs e)
        {
            db = fun.GetDefaultSessionName();
            MySqlConnection con = new MySqlConnection(Login.constring);
            con.Open();
            MySqlCommand cmd;
            try
            {
                if (txtDefault.Checked)
                {
                    cmd = new MySqlCommand("update session set default_session=0;", con);
                    cmd.ExecuteNonQuery();
                }
                var check = (txtDefault.CheckState.ToString() == "Checked") ? "1" : " 0";
                cmd = new MySqlCommand("INSERT into session(name,year_start,year_end,default_session,sync) VALUES('" + txtName.Text + "','" + txtSYear.Text + "','" + txtEYear.Text + "','" + check + "','0');", con);
                cmd.ExecuteNonQuery();
            }
            catch (MySqlException ex)
            {
                SystemSounds.Hand.Play();
                MessageBox.Show(ex.Message, "Error");
                return;
            }
            con.Close();
            FillGridSession();
            promoteStd();
            empty();
        }
        private void empty()
        {
            txtName.Text = "";
            txtSYear.Text = "";
            txtEYear.Text = "";
            txtDefault.Checked = false;
        }

        private void FillGridSession()
        {
            MySqlConnection con = new MySqlConnection(Login.constring);
            con.Open();
            MySqlCommand cmdP = new MySqlCommand("SELECT * from session", con);
            MySqlDataAdapter adp = new MySqlDataAdapter(cmdP);
            DataTable table = new DataTable();
            adp.Fill(table);
            gridSession.DataSource = table;
            gridView1.BestFitColumns();
            var col = gridView1.Columns["session_id"];
            col.OptionsColumn.ReadOnly = true;
            var col1 = gridView1.Columns["sync"];
            col1.Visible = false;
            con.Close();
        }

        private void gridView1_RowUpdated(object sender, DevExpress.XtraGrid.Views.Base.RowObjectEventArgs e)
        {
            DataRow row = gridView1.GetDataRow(gridView1.FocusedRowHandle);
            MySqlConnection con = new MySqlConnection(Login.constring);
            con.Open();
            MySqlCommand cmd;
            var s = row[4].ToString();
            var check = (row[4].ToString() == "True") ? "1" : "0";
            if (check == "1")
            {
                cmd = new MySqlCommand("update session set default_session=0;", con);
                cmd.ExecuteNonQuery();
            }
            cmd = new MySqlCommand("UPDATE session set name='" + row[1] + "',year_start='" + row[2] + "',year_end='" + row[3] + "' ,default_session='" + check + "',sync='0' WHERE session_id='" + row[0] + "';", con);
            cmd.ExecuteNonQuery();
            con.Close();
            FillGridSession();
        }

        private void BtnSessionDelete_Click(object sender, System.EventArgs e)
        {
            try
            {
                if (MessageBox.Show("Are you sure to delete?", "Confirm Delete!!", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    DataRow row = gridView1.GetDataRow(gridView1.FocusedRowHandle);
                    MySqlConnection con = new MySqlConnection(Login.constring);
                    con.Open();
                    MySqlCommand cmdM = new MySqlCommand("DELETE from session WHERE session_id='" + row[0] + "';", con);
                    cmdM.ExecuteNonQuery();
                    con.Close();
                    FillGridSession();
                }
            }
            catch (Exception ex)
            {
                SystemSounds.Hand.Play();
                MessageBox.Show(ex.Message, "Info");
            }
        }
        private void promoteStd()
        {
            var new_db = " tnsbay_school_" + db.Replace('-', '_');
            string appPath = Path.GetDirectoryName(Application.ExecutablePath) + @"\db\"; // <---
            MessageBox.Show("Create path "+ appPath);
            if (Directory.Exists(appPath) == false)                                              // <---
            {                                                                                    // <---
                Directory.CreateDirectory(appPath);                                              // <---
            }
            using (MySqlConnection conn = new MySqlConnection(Login.constring))
            {
                using (MySqlCommand cmd = new MySqlCommand())
                {
                    using (MySqlBackup mb = new MySqlBackup(cmd))
                    {
                        cmd.Connection = conn;
                        conn.Open();
                        mb.ExportToFile(appPath + "\\" + new_db + ".sql");
                        conn.Close();
                    }
                }
            }
            var query = "CREATE database " + new_db.Trim();
            SqlFunctions.SqlExecuteNonQuery(query);
            List<string> tables = new List<string>();
            tables.Add("admin");
            tables.Add("appointments");
            tables.Add("attendance");
            tables.Add("book");
            tables.Add("book_issue");
            tables.Add("books_category");
            tables.Add("ci_sessions");
            tables.Add("class");
            tables.Add("class_fees");
            tables.Add("class_routine");
            tables.Add("document");
            tables.Add("dormitory");
            tables.Add("exam");
            tables.Add("expense_category");
            tables.Add("extra_lecture");
            tables.Add("floor_sheet");
            tables.Add("grade");
            tables.Add("invoice");
            tables.Add("language");
            tables.Add("mark");
            tables.Add("message");
            tables.Add("message_thread");
            tables.Add("noticeboard");
            tables.Add("parent");
            tables.Add("payment");
            tables.Add("resources");
            tables.Add("salary");
            tables.Add("section");
            tables.Add("session");
            tables.Add("settings");
            tables.Add("sms_que");
            tables.Add("student");
            tables.Add("subject");
            tables.Add("tbl_bell");
            tables.Add("tbl_biometric_devices");
            tables.Add("tbl_holidays");
            tables.Add("tbl_mark_subject");
            tables.Add("tbl_permission");
            tables.Add("tbl_role");
            tables.Add("tbl_school");
            tables.Add("tbllog");
            tables.Add("teacher");
            tables.Add("time_table");
            tables.Add("time_teacher");
            tables.Add("transport");
            tables.Add("visitor");
            tables.Add("waiting_list");
            foreach (var tbl in tables)
            {
                var qry = "RENAME TABLE " + tbl + " TO " + new_db + "." + tbl + " ;";
                SqlFunctions.SqlExecuteNonQuery(qry);
            }

            using (MySqlConnection conn = new MySqlConnection(Login.constring))
            {
                using (MySqlCommand cmd = new MySqlCommand())
                {
                    using (MySqlBackup mb = new MySqlBackup(cmd))
                    {
                        cmd.Connection = conn;
                        conn.Open();
                        mb.ImportFromFile(appPath + "\\" + new_db + ".sql");
                        conn.Close();
                    }
                }
            }

            var q = "UPDATE `class` SET `session_id`='" + fun.GetSessionID(fun.GetDefaultSessionName()) + "' ;";
            SqlFunctions.SqlExecuteNonQuery(q);

            List<string> table = new List<string>();
            table.Add("attendance");
            table.Add("exam");
            table.Add("mark");
            table.Add("tbl_mark_subject");
            foreach (var tbl in table)
            {
                var qry = "truncate " + tbl + ";";
                SqlFunctions.SqlExecuteNonQuery(qry);
            }
        }
    }
}
