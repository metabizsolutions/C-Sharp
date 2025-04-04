using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using MySql.Data.MySqlClient;
using System.Media;
using System.IO;

namespace SchoolManagementSystem.Backup
{
    public partial class Backupsync : DevExpress.XtraEditors.XtraUserControl
    {
        private static Backupsync _instance;

        public static Backupsync instance
        {
            get
            {
                if (_instance == null)
                    _instance = new Backupsync();
                return _instance;
            }
        }
        internal void TakeBackup(string partialName = "DbBackup")
        {
            DateTime now = DateTime.Now;
            String lastdateofmonth = new DateTime(now.Year, now.Month, DateTime.DaysInMonth(now.Year, now.Month)).ToString("yyyy-MM-dd");
            
            string path = Directory.GetCurrentDirectory();
            string file_name = DateTime.Now.ToString("yyyy-MM-dd") + ".sql";
            string full_path = path + partialName + "/" + file_name;
            if (path.Trim() != "")
            {
                try
                {
                    string FolderName = Directory.GetDirectories(path).Where(s => s.Equals(path + partialName)).LastOrDefault();
                    if (FolderName != path + partialName)
                    {
                        System.IO.Directory.CreateDirectory(path + partialName);
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
        public Backupsync()
        {
            InitializeComponent();
        }
        Sync sync = new Sync();
        private BackgroundWorker Sync_Worker = new BackgroundWorker();
        CommonFunctions fun = new CommonFunctions();
        #region Switch User and Sync
        private void Sync_Worker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {

        }

        private void timer_Tick(object sender, EventArgs e)
        {
            if (!Sync_Worker.IsBusy)
                Sync_Worker.RunWorkerAsync();
        }
        private void btnSync_Click(object sender, EventArgs e)
        {
            if (!Sync_Worker.IsBusy)
                Sync_Worker.RunWorkerAsync();

        }
        private void Sync_Worker_DoWork(object sender, DoWorkEventArgs e)
        {
            MySqlConnection con = new MySqlConnection(Login.constring);
            var sync_flag = fun.GetSettings("sync_flag");
            if (fun.CheckForInternetConnection() && sync_flag == "1")
            {
                var school_code = fun.GetSettings("school_code");
                var api_key = fun.GetSettings("api_key");
                var api_url = fun.GetSettings("api_url");
                sync.sync(school_code, api_key, api_url);
                con.Open();
                MySqlCommand cmd = new MySqlCommand("UPDATE settings set description='" + DateTime.Now.ToString("yyyy-MM-dd") + "',sync='0' WHERE type='last_sync';", con);
                cmd.ExecuteNonQuery();
                con.Close();
            }
        }
        private void BtnBBrowse_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog fbd = new FolderBrowserDialog();
            if (fbd.ShowDialog() == DialogResult.OK)
            {
                txtBackup.Text = fbd.SelectedPath;
                BtnBackup.Enabled = true;
            }
        }

        private void BtnBackup_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtBackup.Text == string.Empty)
                {
                    MessageBox.Show("Please Enter the backup file location.");
                }
                else
                {
                    // string constring = "server=localhost;user=root;pwd=qwerty;database=test;";
                    // string file = "C:\\backup.sql";

                    using (MySqlConnection conn = new MySqlConnection(Login.constring))
                    {
                        fun.loaderform(() =>
                        {
                            using (MySqlCommand cmd = new MySqlCommand())
                            {
                                using (MySqlBackup mb = new MySqlBackup(cmd))
                                {
                                    cmd.Connection = conn;
                                    conn.Open();
                                    mb.ExportToFile(txtBackup.Text.Trim() + "\\backup -" + DateTime.Now.ToString("yyyy-MM-dd") + ".sql");
                                    conn.Close();
                                }
                            }
                        });
                        MessageBox.Show("Database beckup done successfully");
                        BtnBackup.Enabled = false;
                    }
                }
            }
            catch (Exception ex)
            {
                SystemSounds.Hand.Play();
                MessageBox.Show(ex.Message, "Info");
            }
        }

        private void BtnRBrowse_Click(object sender, EventArgs e)
        {
            OpenFileDialog dlg = new OpenFileDialog();
            dlg.Filter = "MySql SERVER database backup files|*.sql";
            dlg.Title = "Datebase Restore";
            if (dlg.ShowDialog() == DialogResult.OK)
            {
                txtRestore.Text = dlg.FileName;
                BtnRestore.Enabled = true;
            }
        }

        private void BtnRestore_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtRestore.Text == string.Empty)
                {
                    MessageBox.Show("Please Select the backup file.");
                }
                else
                {
                    using (MySqlConnection conn = new MySqlConnection(Login.constring))
                    {
                        fun.loaderform(() =>
                        {
                            using (MySqlCommand cmd = new MySqlCommand())
                            {
                                using (MySqlBackup mb = new MySqlBackup(cmd))
                                {
                                    cmd.Connection = conn;
                                    conn.Open();
                                    mb.ImportFromFile(txtRestore.Text.Trim());
                                    conn.Close();
                                }
                            }
                        });
                        MessageBox.Show("Database restore Successefully");
                        BtnRestore.Enabled = false;
                    }
                }
            }
            catch (Exception ex)
            {
                SystemSounds.Hand.Play();
                MessageBox.Show(ex.Message, "Info");
            }
        }


        #endregion

        private void txtBackup_EditValueChanged(object sender, EventArgs e)
        {

        }
    }
}
