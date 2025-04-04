using AltoHttp;
using AltoHttp.Exceptions;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RestSharp;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Windows.Forms;
using sarwar_settings;

namespace Softpitch_Software_Updater
{
    public partial class main_form : Form
    {
        public static string server_ip = "";
        public static string CS_database = "";
        public static string CS_userid = "";
        public static string CS_password = "";
        public static string tariq_api_path = "";
        public static string sarwar_api_path = "";
        public static string update_type = "";
        public static string constring = "";
        List<string> slider_img = new List<string>();
        string softpitch_adds = Path.GetDirectoryName(Application.ExecutablePath) + @"\softpitch_adds";
        MySql_settings ms = new MySql_settings();

        public main_form()
        {
            InitializeComponent();
            load_config_data();
            update_type = ConfigurationManager.AppSettings["update_type"];
            change_type.Enabled = false;
            if (update_type == "update")
                change_type.SelectedIndex = 1;
            else if (update_type == "upgrade")
                change_type.SelectedIndex = 2;
            else
                change_type.Enabled = true;
            Images_adds();
            load_next_image();
            lblinfo.Text = "We are Processing on your Software Please Wait";
            if (!string.IsNullOrEmpty(update_type))
                AddUpdateAppSettings("update_type", "Running");

        }
        public void load_config_data()
        {
            server_ip = ConfigurationManager.AppSettings["server_id"];
            CS_database = ConfigurationManager.AppSettings["database"];
            CS_userid = ConfigurationManager.AppSettings["user_id"];
            CS_password = ConfigurationManager.AppSettings["password"];
            tariq_api_path = ConfigurationManager.AppSettings["tariq_api_path"];
            sarwar_api_path = ConfigurationManager.AppSettings["sarwar_api_path"];
            update_type = "";
            constring = "server=" + server_ip + ";port=3306;user id=" + CS_userid + "; password=" + CS_password + "; database=" + CS_database + ";persist security info=true;CharSet=utf8;Allow User Variables=True;default command timeout=300;Convert Zero Datetime=True;Max Pool Size=200;Allow Zero Datetime=true;";//ConfigurationManager.ConnectionStrings["tnsbay_school_Connection"].ConnectionString;
            if (sarwar_MySql_Helper.isCOnnection(constring)) // if valid connection then encript password
            {
                AddUpdateAppSettings("password", ms.Encrypt(CS_password, "Zee0107"));//password incripting in app settings
            }
            else
            {
                CS_password = ms.Decrypt(CS_password, "Zee0107");
                constring = "server=" + server_ip + ";port=3306;user id=" + CS_userid + "; password=" + CS_password + "; database=" + CS_database + ";persist security info=true;CharSet=utf8;Allow User Variables=True;default command timeout=300;Convert Zero Datetime=True;Max Pool Size=200;Allow Zero Datetime=true;";
            }
            MySql_settings.constring = constring;
        }
        public void Images_adds()
        {
            try
            {
                string fileName = "";
                string fileName_inapi = "";
                string url = tariq_api_path + "api/advertisement_banner?group_id=1&code=OSPLYA";
                JObject json = get_data(url);
                JArray login_img = JArray.Parse(json["login"].ToString());
                if (login_img != null)
                {
                    foreach (string img in login_img)
                    {
                        fileName = Path.GetFileName(img);
                        if (!Directory.Exists(softpitch_adds))
                        {
                            Directory.CreateDirectory(softpitch_adds);
                        }
                        List<string> hasfile = Directory.GetFiles(softpitch_adds, fileName, SearchOption.AllDirectories).ToList();
                        if (hasfile.Count <= 0)
                        {
                            using (WebClient client = new WebClient())
                            {
                                try
                                {
                                    client.DownloadFile(new Uri(img), softpitch_adds + @"\" + fileName);
                                }
                                catch (Exception e) { }
                            }
                        }
                    }
                    List<string> hasfile_list = Directory.GetFiles(softpitch_adds, "*", SearchOption.AllDirectories).ToList();
                    foreach (string img_val in hasfile_list)
                    {
                        fileName = Path.GetFileName(img_val);
                        bool img_find = false;
                        foreach (string img in login_img)
                        {
                            fileName_inapi = Path.GetFileName(img);
                            if (fileName == fileName_inapi)
                            {
                                img_find = true;
                                break;
                            }
                            else
                                img_find = false;
                        }
                        if (!img_find)
                            File.Delete(img_val);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Advertisement Error", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
        public void AddUpdateAppSettings(string key, string value)
        {
            try
            {
                Configuration c = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
                var settings = c.AppSettings.Settings;
                //check if input key is new or already existing
                if (settings[key] == null)
                {
                    //if new then add the value
                    settings.Add(key, value);
                }
                else
                {
                    //if not then update the key value
                    settings[key].Value = value;
                }
                c.Save(ConfigurationSaveMode.Modified);
            }
            catch (ConfigurationErrorsException exs)
            {
                throw new Exception("Error writing app settings" + exs.InnerException);
            }
        }
        void load_next_image()
        {
            if (Directory.Exists(softpitch_adds))
                slider_img = Directory.GetFiles(softpitch_adds, "*.*", SearchOption.AllDirectories).ToList();
            foreach (string img in slider_img)
            {
                imageSlider1.Images.Add(Image.FromFile(img, true));
            }
        }
        BackgroundWorker bw = new BackgroundWorker();
        private void Form1_Load(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(update_type))
            {
                PriorProcess();
                update_files();
            }
        }
        public Process PriorProcess()
        // Returns a System.Diagnostics.Process pointing to
        // a pre-existing process with the same name as the
        // current one, if any; or null if the current process
        // is unique.
        {
            Process firstProc = new Process();
            firstProc.StartInfo.FileName = "OSP Management";
            firstProc.StartInfo.WorkingDirectory = Path.GetDirectoryName(Application.ExecutablePath);
            firstProc.EnableRaisingEvents = true;
            Process[] procs = Process.GetProcessesByName(System.IO.Path.GetFileNameWithoutExtension(firstProc.StartInfo.FileName));
            foreach (Process p in procs)
            {
                p.Kill();
            }
            return null;
        }
        int file_count = 0;
        void update_files()
        {
            file_count = 0;
            btn_download_software.Enabled = false;
            lbl_installing_info.Text = "Please wait We Are Chacking settings";
            bw.DoWork += Bw_DoWork;
            bw.RunWorkerCompleted += Bw_RunWorkerCompleted;
            if (!bw.IsBusy)
                bw.RunWorkerAsync();

        }

        string download_file_name = "";
        int total_files = 0;
        private void Bw_DoWork(object sender, DoWorkEventArgs e)
        {
            get_all_files();
            run_software();
        }
        void get_all_files()
        {
            string url = "";
            if (update_type == "upgrade")
                url = sarwar_api_path + "Desktop_Api/get_all_files?software_type=1";
            else if (update_type == "update")
                url = sarwar_api_path + "Desktop_Api/get_software_update_files?software_type=1";

            if (!string.IsNullOrEmpty(url))
            {
                download_file_name = "....Loading Files";
                JObject result = get_data(url);
                JObject json = (JObject)result["data"];
                total_files = Convert.ToInt32(result["total_files"]);
                lbl_total_files.Invoke((MethodInvoker)(() => lbl_total_files.Text = "0/" + total_files.ToString()));
                string my_dir = Path.GetDirectoryName(Application.ExecutablePath) + "\\Softpitch OSP";
                string[] impotent_files = { my_dir + "\\OSP Management.application", my_dir + "\\OSP Management.exe.manifest", my_dir + "\\OSP Management.exe", my_dir + "\\OSP Management.pdb", my_dir + "\\sarwar_settings.dll", my_dir + "\\sarwar_settings.pdb" };
                foreach (string file_url in impotent_files)
                {
                    if (File.Exists(file_url))
                        File.Delete(file_url);
                }
                if (!Directory.Exists(my_dir))
                    Directory.CreateDirectory(my_dir);

                foreach (var data in json)
                {
                    var folder_name = data.Key;
                    string file_path = "";
                    string tempFile = "";

                    if (folder_name == "Debug")
                    {
                        file_path = sarwar_api_path + "OSP_FULL/Debug/";
                        tempFile = my_dir;

                        JArray all_tables = JArray.Parse(json[folder_name].ToString());
                        DataTable dt = JsonConvert.DeserializeObject<DataTable>(all_tables.ToString());

                        download_file_and_save(dt, file_path, tempFile, bw);
                    }
                    else
                    {
                        file_path = sarwar_api_path + "OSP_FULL/Debug/" + folder_name + "/";
                        tempFile = my_dir + "\\" + folder_name;
                        if (!Directory.Exists(tempFile))
                            Directory.CreateDirectory(tempFile);
                        if (data.Value.Type == JTokenType.Array)
                        {
                            //Main Debug folder
                            JArray all_tables = JArray.Parse(json[folder_name].ToString());
                            DataTable dt = JsonConvert.DeserializeObject<DataTable>(all_tables.ToString());

                            download_file_and_save(dt, file_path, tempFile, bw);
                        }
                        else// sub folder
                        {
                            JObject ob = JObject.Parse(json[folder_name].ToString());
                            foreach (var sub_data in ob)
                            {
                                string sub_folder_name = sub_data.Key;
                                int myInt;
                                bool isNumerical = int.TryParse(sub_folder_name, out myInt);
                                if (!isNumerical)
                                {
                                    file_path = sarwar_api_path + "OSP_FULL/Debug/" + folder_name + "/" + sub_folder_name + "/";
                                    tempFile = my_dir + "\\" + folder_name + "\\" + sub_folder_name;
                                    if (!Directory.Exists(tempFile))
                                    {
                                        Directory.CreateDirectory(tempFile);
                                    }
                                    if (data.Value.Type == JTokenType.Array)
                                    {
                                        //sub sub folder
                                        JArray all_tables = JArray.Parse(ob[sub_folder_name].ToString());
                                        DataTable dt = JsonConvert.DeserializeObject<DataTable>(all_tables.ToString());

                                        download_file_and_save(dt, file_path, tempFile, bw);
                                    }
                                }
                                else
                                {
                                    file_path = sarwar_api_path + "OSP_FULL/Debug/" + folder_name + "/";
                                    tempFile = my_dir + "\\" + folder_name + "\\";
                                    string array = "[" + ob[sub_folder_name].ToString() + "]";
                                    JArray all_tables = JArray.Parse(array);
                                    DataTable dt = JsonConvert.DeserializeObject<DataTable>(all_tables.ToString());

                                    download_file_and_save(dt, file_path, tempFile, bw);
                                }
                            }
                        }
                    }

                }
            }
        }
        private void Bw_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (e.Cancelled)
                progressBarControl1.EditValue = 0;
            else if (e.Error != null)
                MessageBox.Show(e.Error.Message, "File update Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            else
            {

            }

            btn_download_software.Enabled = true;
        }
        void run_software()
        {
            AddUpdateAppSettings("update_type", "done");
            Process firstProc = new Process();
            firstProc.StartInfo.FileName = "OSP Management.exe";
            firstProc.StartInfo.WorkingDirectory = Path.GetDirectoryName(Application.ExecutablePath) + "\\Softpitch OSP";
            firstProc.EnableRaisingEvents = true;
            firstProc.Start();
            this.Invoke((MethodInvoker)delegate
            {
                this.Close();
            });

        }
        bool isfile_downloaded = false;
        void download_file_and_save(DataTable dt, string download_link, string save_path, BackgroundWorker bw)
        {
            string file_name = "";
            string modified_date = "";
            string file_link = "";
            string file_Path_save = "";
            string file_size = "";
            foreach (DataRow dr in dt.Rows)
            {
                file_name = dr["file_name"].ToString();
                modified_date = dr["Modified_Date"].ToString();
                file_size = dr["file_size"].ToString();

                file_link = download_link + file_name;
                file_Path_save = save_path + "\\" + file_name;
                if (File.Exists(file_Path_save))// && file_size == filesize_in_disk(file_Path_save)
                {
                    file_count = file_count + 1;
                    percent = file_count * 100 / total_files;

                    this.Invoke((MethodInvoker)delegate
                    {
                        progressBarControl1.EditValue = percent;
                        lbl_total_files.Text = file_count.ToString() + "/" + total_files.ToString();
                    });
                }
                else
                {
                    isfile_downloaded = false;
                    alto_download_file(file_link, file_Path_save);
                    while (!isfile_downloaded)// this loop is running when file downoad complete then loop breaks
                    {

                    }
                }
            }
        }
        string filesize_in_disk(string path)
        {
            FileInfo fi = new FileInfo(path);
            long bytes = fi.Length;
            double val = Math.Round(Convert.ToDouble(bytes / 1024 / 1024 / 1024), 1);//number_format($bytes / 1073741824, $decimal_places);
            if (val > 0)
            {
                return val.ToString() + " GB";
            }
            val = Math.Round(Convert.ToDouble(bytes / 1024 / 1024), 1); // megabytes with 1 digit//number_format($bytes / 1048576, $decimal_places);
            if (val > 0)
            {
                return val.ToString() + " MB";
            }
            else
            {
                val = Math.Round(Convert.ToDouble(bytes / 1024), 2); // kilobytes with two digits//number_format($bytes / 1024, $decimal_places);
                return val.ToString() + " KB";
            }
        }
        public JObject get_data(string url)
        {
            JObject json = new JObject();
            try
            {
                ServicePointManager.Expect100Continue = true;
                ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
                ServicePointManager.ServerCertificateValidationCallback = delegate { return true; };
                var client = new RestClient(url);
                client.AddDefaultHeader("Content-Type", "application/json");
                client.Timeout = -1;
                var request = new RestRequest(Method.GET);
                IRestResponse response = client.Execute(request);
                json = JObject.Parse(response.Content);
            }
            catch (NullReferenceException exc)
            {
                MessageBox.Show(exc.Message + "    May be Internet Broken", "API Error null", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "API Error", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            return json;
        }
        public void download_file(string url, string tempFile)
        {
            JObject json = new JObject();
            try
            {
                ServicePointManager.Expect100Continue = true;
                ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
                ServicePointManager.ServerCertificateValidationCallback = delegate { return true; };
                using (WebClient client = new WebClient())
                {
                    client.Credentials = CredentialCache.DefaultNetworkCredentials;
                    client.DownloadProgressChanged += Client_DownloadProgressChanged;

                    download_file_name = tempFile;
                    Uri uri = new Uri(url);
                    client.DownloadFileAsync(uri, tempFile, download_file_name);
                }
            }
            catch (NullReferenceException exc)
            {
                MessageBox.Show(exc.Message + " May be Internet Broken", "API Error null", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "API Error", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            //return json;
        }
        DateTime _startedAt;
        int percent = 0;
        private void Client_DownloadProgressChanged(object sender, DownloadProgressChangedEventArgs e)
        {
            this.Invoke((MethodInvoker)delegate
            {
                progressBarControl2.EditValue = e.ProgressPercentage;
                lbl_installing_info.Text = e.UserState.ToString();

                if (e.ProgressPercentage == 100)
                {

                    file_count = file_count + 1;
                    percent = file_count * 100 / total_files;
                    progressBarControl1.EditValue = percent;
                    lbl_total_files.Text = file_count.ToString() + "/" + total_files.ToString();
                }
                //lbl_file_size.Text = string.Format("{0} Mbs", (e.TotalBytesToReceive / 1024d / 1024d).ToString("0.00"));
                lbl_file_downloaded.Text = string.Format("{0} Mbs", (e.BytesReceived / 1024d / 1024d).ToString("0.00"));

            });
            if (_startedAt == default(DateTime))
            {
                _startedAt = DateTime.Now;
            }
            else
            {
                var timeSpan = DateTime.Now - _startedAt;
                if ((long)timeSpan.TotalSeconds > 0)
                {
                    var bytesPerSecond = e.BytesReceived / (long)timeSpan.TotalSeconds;
                    lbl_inernet_speed.Invoke((MethodInvoker)(() => lbl_inernet_speed.Text = string.Format("{0} Kb/s", (bytesPerSecond / 125).ToString("0.00"))));
                }
            }
        }
        private HttpDownloader downloader = null;
        public void alto_download_file(string url, string tempFile)
        {
            if (Program.MSG == null)
            {
                downloader = new HttpDownloader(url, tempFile);
                downloader.DownloadInfoReceived += downloader_DownloadInfoReceived;
                downloader.DownloadCompleted += downloader_DownloadCompleted;
                downloader.ProgressChanged += downloader_ProgressChanged;
                downloader.ErrorOccured += downloader_ErrorOccured;
                downloader.Start();
            }
        }

        void downloader_ErrorOccured(object sender, ErrorEventArgs e)
        {
            var ex = e.GetException();
            //if (ex is FileValidationFailedException)
            //{
            downloader.Pause();
            this.Invoke((MethodInvoker)delegate
            {
                btn_download_software.Text = "Resume Download Software";
                btn_download_software.Enabled = true;
            });
            //}
            MessageBox.Show("Some Error Accured in Your Connection Please chack your internet and resume downloading","Downloading Error",MessageBoxButtons.OK,MessageBoxIcon.Information);
            //MessageBox.Show("Error: " + e.GetException().Message + " " + e.GetException().StackTrace);
        }
        void downloader_ProgressChanged(object sender, AltoHttp.ProgressChangedEventArgs e)
        {
            if (downloader.Info != null)
            {
                this.Invoke((MethodInvoker)delegate
                {
                    string info_value = downloader.Info == null ? "" : downloader.Info.Length > 0 ? downloader.Info.Length.ToHumanReadableSize() : "Unknown";
                    lbl_file_downloaded.Text = string.Format("{0} / {1}", e.TotalBytesReceived.ToHumanReadableSize(), info_value);
                    //lblProgress.Text = e.Progress.ToString("0.00") + "%";
                    lbl_inernet_speed.Text = e.SpeedInBytes.ToHumanReadableSize() + "/s";
                    progressBarControl2.EditValue = (int)(e.Progress);

                });
            }

        }

        void downloader_DownloadCompleted(object sender, EventArgs e)
        {
            isfile_downloaded = true;
            int percent = 0;
            this.Invoke((MethodInvoker)delegate
            {
                file_count = file_count + 1;
                percent = file_count * 100 / total_files;
                progressBarControl1.EditValue = percent;
                lbl_total_files.Text = file_count.ToString() + "/" + total_files.ToString();
            });
        }
        void downloader_DownloadInfoReceived(object sender, EventArgs e)
        {
            if (downloader.Info != null)
            {
                //var saveDirectory = Path.GetDirectoryName(downloader.FullFileName);
                //var serverFileName = downloader.Info.ServerFileName;

                //var newFilePath = Path.Combine(saveDirectory, serverFileName);

                //downloader.FullFileName = newFilePath;
                this.Invoke((MethodInvoker)delegate
                {
                    lbl_installing_info.Text = downloader.Info == null ? "" : downloader.Info.ServerFileName;

                    //lblResumeability.Text = downloader.Info.AcceptRange ? "Yes" : "No";
                    //lbl_file_size.Text = downloader.Info.Length > 0 ? downloader.Info.Length.ToHumanReadableSize() : "Unknown";
                    //lblIsChunked.Text = downloader.Info.IsChunked ? "Yes" : "No";
                });
            }

        }

        private void btn_download_software_Click(object sender, EventArgs e)
        {
            btn_download_software.Enabled = false;
            if (btn_download_software.Text.Contains("Resume"))
            {
                if (ms.CheckForInternetConnection())
                {
                    btn_download_software.Text = "Start Download Software";
                    downloader.Resume();
                }
                else
                {
                    MessageBox.Show("Please Chack your internet There is not download speed available", "Internet Connections info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            else
            {
                if (string.IsNullOrEmpty(change_type.Text))
                {
                    MessageBox.Show("Please Select Change type", "update/upgrade info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
                if (!ms.CheckForInternetConnection())
                {
                    MessageBox.Show("Please chack your internet connection", "Internet Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
                string softpitch_password = ms.GetSettings("device_password");

                using (confirm_softpitch de = new confirm_softpitch())
                {
                    if (de.ShowDialog() == DialogResult.Yes) { }
                    else if (de.has_password == true && de.txt_password.Text == softpitch_password)
                    {
                        update_type = change_type.Text;
                        update_files();
                    }
                    else
                    {
                        MessageBox.Show("Incorrect Password", "info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        btn_download_software.Enabled = true;
                    }
                }
            }
        }

        private void btn_close_form_Click(object sender, EventArgs e)
        {
            this.Hide();
        }
        private void pictureBox3_Click(object sender, EventArgs e)
        {

        }
        private void notifyIcon1_Click(object sender, EventArgs e)
        {
            this.Show();
        }

        private void btn_settings_Click(object sender, EventArgs e)
        {
            system_settings ss = new system_settings(this);
            ss.Show();
        }
    }
}
