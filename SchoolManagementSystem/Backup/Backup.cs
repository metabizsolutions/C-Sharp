using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Linq;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using MySql.Data.MySqlClient;
using System.Media;
using System.IO;
using System.Security.Cryptography;

namespace SchoolManagementSystem.Backup
{
    public partial class Backup : XtraUserControl
    {
        private static Backup _instance;

        public static Backup instance
        {
            get
            {
                if (_instance == null)
                    _instance = new Backup();
                return _instance;
            }
        }
        CommonFunctions fun = new CommonFunctions();
        string lastdateofmonth = "";
        public Backup()
        {
            InitializeComponent();
            txtBackup.Text = Login.Backuppath;
        }
        #region Backup/Restore
        string Directorypath = Path.GetDirectoryName(Application.ExecutablePath) + "\\DbBackup\\file.sql";// its is use to create temporary file in directory and the we delete it after use that
        internal void TakeBackup(string partialName = "DbBackup")
        {
            DateTime now = DateTime.Now;
            String lastdateofmonth = new DateTime(now.Year, now.Month, DateTime.DaysInMonth(now.Year, now.Month)).ToString("yyyy-MM-dd");

            string path = Directory.GetCurrentDirectory();
            string file_name = DateTime.Now.ToString("yyyy-MM-dd") + ".sql";
            string full_path = path + "\\" + partialName + "\\" + file_name;
            if (path.Trim() != "")
            {
                try
                {
                    string FolderName = Directory.GetDirectories(path).Where(s => s.Equals(path + "\\" + partialName)).LastOrDefault();
                    if (FolderName != path + "\\" + partialName)
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
                                mb.ExportToFile(Directorypath);
                                conn.Close();
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
        void deleteBackupfiles(string date)
        {
            string partialName = "Tnsbay Pos BackUp";
            string path = Login.Backuppath;
            try
            {
                IEnumerable<string> listD = Directory.GetDirectories(path);
                DirectoryInfo dir = new DirectoryInfo(path + partialName);
                FileInfo[] filesD = dir.GetFiles("backup*", SearchOption.TopDirectoryOnly);
                for (int j = 0; j < filesD.Length; j++)
                {
                    //if (j != 0)
                    //{
                    string Filename = filesD[j].Name;
                    string CFdate = getdate(Filename);

                    if (!System.Text.RegularExpressions.Regex.IsMatch(CFdate, @"^[\d]{4}-[\d]{2}$") && !System.Text.RegularExpressions.Regex.IsMatch(CFdate, @"^[\d]{4}$"))
                    {
                        DateTime cfdate = Convert.ToDateTime(CFdate);
                        DateTime dates = Convert.ToDateTime(date);
                        if (dates == cfdate && cfdate.ToString("dddd") == "Monday")
                        {
                            return;
                        }
                        else if (dates > cfdate)
                        {
                            if (Convert.ToDateTime(CFdate).AddDays(1).ToString("dddd") != "Monday")
                            {
                                string pathdel = path + partialName + "/" + filesD[j].Name;
                                File.Delete(pathdel);
                            }
                        }

                    }
                    else if (System.Text.RegularExpressions.Regex.IsMatch(CFdate, @"^[\d]{4}-[\d]{2}$"))
                    {
                        DirectoryInfo dirY = new DirectoryInfo(path + partialName);
                        FileInfo[] filesmonth = dirY.GetFiles("backup*", SearchOption.TopDirectoryOnly);
                        for (int k = 0; k < filesmonth.Length; k++)
                        {
                            string filename = filesmonth[k].Name;
                            string datesM = getdate(filename);
                            if (!System.Text.RegularExpressions.Regex.IsMatch(datesM, @"^[\d]{4}-[\d]{2}$") && !System.Text.RegularExpressions.Regex.IsMatch(datesM, @"^[\d]{4}$"))
                            {
                                if (Convert.ToDateTime(datesM).ToString("MMMM") == DateTime.Now.AddMonths(-1).ToString("MMMM"))
                                {
                                    string pathdel = path + partialName + "/" + filesmonth[k].Name;
                                    File.Delete(pathdel);
                                }
                            }
                        }
                    }
                    else if (System.Text.RegularExpressions.Regex.IsMatch(CFdate, @"^[\d]{4}$"))
                    {
                        DirectoryInfo dirY = new DirectoryInfo(path + partialName);
                        FileInfo[] filesyears = dirY.GetFiles("backup*", SearchOption.TopDirectoryOnly);
                        for (int k = 0; k < filesyears.Length; k++)
                        {
                            string filename = filesyears[k].Name;
                            string datesM = getdate(filename);
                            if (!System.Text.RegularExpressions.Regex.IsMatch(datesM, @"^[\d]{4}$"))
                            {
                                if (Convert.ToDateTime(datesM).ToString("yyyy") == DateTime.Now.AddYears(-1).ToString("yyyy"))
                                {
                                    string pathdel = path + partialName + "/" + filesyears[k].Name;
                                    File.Delete(pathdel);
                                }
                            }
                        }

                    }


                    // }
                }
            }
            catch (Exception ex)
            {

            }

        }
        string getdate(string Filename)
        {

            string[] words = Filename.Split('-', '.');
            string CFdate = "";
            for (int i = 0; i < words.Length; i++)
            {
                if (words.Length == 3)
                {
                    CFdate = words[1].ToString();
                }
                else if (words.Length == 4)
                {
                    if (i == 2)
                        CFdate += words[i] = "-" + words[i];

                    if (i == 1)
                        CFdate += words[i].ToString();
                }
                else
                {
                    if (i == 2)
                        words[i] = "-" + words[i] + "-";

                    if (i > 0 && i <= 3)
                        CFdate += words[i].ToString();
                }
            }
            return CFdate;
        }
        private string genratekey()
        {
            DESCryptoServiceProvider desCrypto = (DESCryptoServiceProvider)DESCryptoServiceProvider.Create();
            return ASCIIEncoding.ASCII.GetString(desCrypto.Key);
        }
        
        private void BtnBBrowse_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog fbd = new FolderBrowserDialog();
            if (fbd.ShowDialog() == DialogResult.OK)
            {
                txtBackup.Text = fbd.SelectedPath;
                string query = "update settings SET description='" + txtBackup.Text + "' where type = 'BackUp Path' ;";
                fun.ExecuteQuery(query);
                B_BtnBackup.Enabled = true;
            }
        }
        private void BtnBackup_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtBackup.Text == string.Empty)
                {
                    MessageBox.Show("Please Enter the backup file location.", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MySqlConnectionStringBuilder mscsb = new MySqlConnectionStringBuilder(Login.constring);
                    string constring = "Server="+mscsb.Server+";Port=3306;Database="+ mscsb.Database+ ";User id="+ mscsb.UserID+ "; password="+mscsb.Password+";CharSet=utf8;Allow User Variables=True;Allow Zero Datetime=true;";
                    using (MySqlConnection conn = new MySqlConnection(constring))
                    {
                        using (MySqlCommand cmd = new MySqlCommand())
                        {
                            using (MySqlBackup mb = new MySqlBackup(cmd))
                            {
                                cmd.Connection = conn;
                                conn.Open();
                                fun.loaderform(() => mb.ExportToFile(Directorypath));
                                conn.Close();
                            }
                        }
                        string full_text = System.IO.File.ReadAllText(Directorypath);
                        fun.loaderform(() => 
                            fun.save_File(full_text, "backup - " + DateTime.Now.ToString("yyyy-MM-dd") + ".sql",txtBackup.Text.Trim() + "\\backup -" + DateTime.Now.ToString("yyyy-MM-dd") + ".sql")
                            //fun.EncriptDB(Directorypath, txtBackup.Text.Trim() + "\\backup -" + DateTime.Now.ToString("yyyy-MM-dd") + ".sql", "!?O??v??")
                        );
                        fun.loaderform(() => File.Delete(Directorypath));
                        MessageBox.Show("Database beckup done successefully", "Successfull", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        B_BtnBackup.Enabled = false;
                    }
                }
            }
            catch (Exception ex)
            {
                SystemSounds.Hand.Play();
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void BtnRBrowse_Click(object sender, EventArgs e)
        {
            OpenFileDialog dlg = new OpenFileDialog();
            dlg.Filter = "MySql SERVER database backup files|*.sql";
            dlg.Title = "Datebase restore";
            if (dlg.ShowDialog() == DialogResult.OK)
            {
                txtRestore.Text = dlg.FileName;
                B_BtnRestore.Enabled = true;
            }
        }
        private void BtnRestore_Click(object sender, EventArgs e)
        {
            //fun.DecriptDB(txtRestore.Text.Trim(), Directorypath, "!?O??v??");
            string full_text = System.IO.File.ReadAllText(txtRestore.Text.Trim());
            fun.loaderform(() =>fun.save_File(fun.Decrypt(full_text, "Zee0107"), "file.sql", Directorypath,false));
            try
            {
                if (txtRestore.Text == string.Empty)
                {
                    MessageBox.Show("Please Select the backup file.", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    B_BtnRestore.Enabled = false;
                    MySqlConnectionStringBuilder mscsb = new MySqlConnectionStringBuilder(Login.constring);
                    string constring = "Server=" + mscsb.Server + ";Port=3306;Database=" + mscsb.Database + ";User id=" + mscsb.UserID + "; password=" + mscsb.Password + ";CharSet=utf8;Allow User Variables=True;Allow Zero Datetime=true;";
                    using (MySqlConnection conn = new MySqlConnection(constring))
                    {
                        using (MySqlCommand cmd = new MySqlCommand())
                        {
                            using (MySqlBackup mb = new MySqlBackup(cmd))
                            {
                                cmd.Connection = conn;
                                conn.Open();

                                cmd.CommandText = "SET GLOBAL max_allowed_packet=32*1024*1024;";
                                cmd.ExecuteNonQuery();
                                conn.Close();
                                conn.Open();
                                fun.loaderform(() => mb.ImportFromFile(Directorypath));
                                conn.Close();
                                MessageBox.Show("Database restore Successefully", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                File.Delete(Directorypath);

                            }
                        }
                        B_BtnRestore.Enabled = true;
                        logout();
                    }
                }
            }
            catch (Exception ex)
            {
                File.Delete(Directorypath);
                SystemSounds.Hand.Play();
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }
        void logout()
        {
            this.ParentForm.Visible = false;
            this.ParentForm.Close();
            Login a = new Login();
            a.ShowDialog();
        }
        #endregion
    }
}
