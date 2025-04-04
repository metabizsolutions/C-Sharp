using DevExpress.XtraSplashScreen;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Media;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using sarwar_settings;

namespace SchoolManagementSystem
{
    public partial class Login : DevExpress.XtraEditors.XtraForm
    {
        CommonFunctions fun = new CommonFunctions();

        private BackgroundWorker Bell_Worker = new BackgroundWorker();
        public static string server_ip = ConfigurationManager.AppSettings["server_id"];
        public static string CS_database = ConfigurationManager.AppSettings["database"];
        public static string CS_userid = ConfigurationManager.AppSettings["user_id"];
        public static string CS_password = ConfigurationManager.AppSettings["password"];
        public static string tariq_api_path = ConfigurationManager.AppSettings["tariq_api_path"];
        public static string sarwar_api_path = ConfigurationManager.AppSettings["sarwar_api_path"];
        public static string update_type = ConfigurationManager.AppSettings["update_type"];
        public static string constring = "server="+server_ip+";port=3306;user id="+CS_userid+"; password="+CS_password+"; database="+CS_database+";persist security info=true;CharSet=utf8;Allow User Variables=True;default command timeout=300;Convert Zero Datetime=True;Max Pool Size=200;Allow Zero Datetime=true;";//ConfigurationManager.ConnectionStrings["tnsbay_school_Connection"].ConnectionString;
        
        public static string compare_server = "server=170.39.76.40;port=3306;user id=ospeduco_dev; password=Dev@3790; database=ospeduco_sgc_account;persist security info=true;CharSet=utf8;Allow User Variables=True;default command timeout=300;Convert Zero Datetime=True";
        

        public static string constringSQLite = "Data Source=MyDatabase.sqlite;Version=3;";
        public static string CurrentUserID;
        public static string CurrentUserName;
        public static string CurrentUserEmail;
        public static string CurrentUserStatus;
        public static string CurrentUserStatus_id;
        public static string Logo;
        public static string Exam_Sign;
        public static string Principal_Sign;
        public static string Vice_Principal_Sign;
        public static Main_FD a1;
        public static string SchoolCode;
        public static string session_name;
        public static string Backuppath;

        string login_imgs = Path.GetDirectoryName(Application.ExecutablePath) + @"\slider_images\login";
        List<string> slider_img = new List<string>();
        public Login()
        {
            //Bunifu.MySQL.Start();
            fun.loaderform(() =>
            {
                if (update_type == "Running")
                {
                    MessageBox.Show("Please Wait Few Seconds We Are Updateing your software", "Softpicth Software House", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    Process.GetCurrentProcess().Kill();
                    return;
                }
                /*if (PriorProcess() != null)
                {
                    MessageBox.Show("Another instance of the app is already running.","Softpitch OSP Info",MessageBoxButtons.OK,MessageBoxIcon.Hand);
                    Process.GetCurrentProcess().Kill();
                    return;
                }*/
                InitializeComponent();

                if (sarwar_MySql_Helper.isCOnnection(constring)) // if valid connection then encript password
                {
                    fun.AddUpdateAppSettings("password", fun.Encrypt(CS_password, "Zee0107"));//password incripting in app settings
                }
                else
                {
                    CS_password = fun.Decrypt(CS_password, "Zee0107");
                    constring = "server=" + server_ip + ";port=3306;user id=" + CS_userid + "; password=" + CS_password + "; database=" + CS_database + ";persist security info=true;CharSet=utf8;Allow User Variables=True;default command timeout=300;Convert Zero Datetime=True;Max Pool Size=200;Allow Zero Datetime=true;";

                }
                if (!sarwar_MySql_Helper.isCOnnection_Valid(constring))
                {
                    MessageBox.Show("Please chack your Wamp server is running or not!", "info", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
                else
                {
                    slider_img = Directory.GetFiles(login_imgs, "*.*", SearchOption.AllDirectories).ToList();
                    load_next_image();
                    SchoolCode = fun.GetSettings("school_code");
                    Logo = fun.GetSettings("logo");
                    Principal_Sign = fun.GetSettings("principal_sign");
                    Exam_Sign = fun.GetSettings("exam_sign");
                    Vice_Principal_Sign = fun.GetSettings("vice_principal_sign");
                    Backuppath = fun.GetSettings("BackUp Path");
                    try
                    {
                        if (fun.GetSettings("Show_company") == "Yes")
                        {
                            //PicLogo.Dock = DockStyle.None;
                            slider_box.Dock = DockStyle.None;
                            string logo = fun.GetSettings("Company_LOGO");
                            if (!string.IsNullOrEmpty(logo))
                                pictureBox_partner.Image = fun.Base64ToImage(logo);
                            txtPartnar_title.Text = fun.GetSettings("Company_Name");
                            txtpartnar_website.Text = fun.GetSettings("Company_Website");
                            txtpartnar_discription.Text = fun.GetSettings("Company_Discription");
                        }
                        else
                        {
                            //PicLogo.Dock = DockStyle.Fill;
                            slider_box.Dock = DockStyle.Fill;
                            pictureBox_partner.Visible = false;
                            txtPartnar_title.Visible = false;
                            txtpartnar_website.Visible = false;
                            txtpartnar_discription.Visible = false;
                        }
                    }
                    catch (Exception e)
                    {
                    }

                    #region code not used bell flag BackgroundWorker
                    //var bell_flag = fun.GetSettings("bell_flag");
                    //if (bell_flag == "1")
                    //{
                    //    Bell_Worker.DoWork += new DoWorkEventHandler(Bell_Worker_DoWork);
                    //    //     Bell_Worker.RunWorkerCompleted += new RunWorkerCompletedEventHandler(Bill_Worker_RunWorkerCompleted);
                    //    //Bell_Worker.ProgressChanged += new ProgressChangedEventHandler(Sync_Worker_ProgressChanged);
                    //    Bell_Worker.WorkerReportsProgress = true;
                    //    Bell_Worker.WorkerSupportsCancellation = true;
                    //    timer_bill.Interval = fun.ConvertMinutesToMilliseconds(Convert.ToInt32(1));
                    //    timer_bill.Enabled = true;
                    //}
                    #endregion
                }
            });
        }
        public Process PriorProcess()
        // Returns a System.Diagnostics.Process pointing to
        // a pre-existing process with the same name as the
        // current one, if any; or null if the current process
        // is unique.
        {
            Process curr = Process.GetCurrentProcess();
            Process[] procs = Process.GetProcessesByName(System.IO.Path.GetFileNameWithoutExtension(curr.StartInfo.FileName));
            int running_count = procs.Length;
            foreach (Process p in procs)
            {
                if ((p.Id != curr.Id) && (p.MainModule.FileName == curr.MainModule.FileName))
                {
                    if (running_count == 1)
                        return p;
                    else
                        p.Kill();
                    running_count--;
                }
            }
                return null;
        }

        #region code not used bell flag Funcations
        private int img_no = 0;
        void load_next_image()
        {
            if (slider_img.Count <= img_no)
                img_no = 0;
            if (slider_img.Count > 0)
                slider_box.ImageLocation = slider_img[img_no];
            else
                slider_img = Directory.GetFiles(login_imgs, "*.*", SearchOption.AllDirectories).ToList();
            img_no++;
        }
        private void timer1_Tick(object sender, EventArgs e)
        {
            load_next_image();
            //if (!Bell_Worker.IsBusy)
            //  Bell_Worker.RunWorkerAsync();
        }

        private void Bell_Worker_DoWork(object sender, DoWorkEventArgs e)
        {
            MySqlConnection con = new MySqlConnection(Login.constring);
            var bell_flag = fun.GetSettings("bell_flag");
            if (bell_flag == "1")
            {
                var query = "SELECT * FROM tbl_bell where day='" + DateTime.Now.DayOfWeek + "'";
                con.Open();
                MySqlCommand cmd = new MySqlCommand(query, con);
                MySqlDataReader reader = cmd.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        var t = DateTime.Now.ToString("HH:mm");
                        var s = reader["time_slate"].ToString();
                        if (String.Compare(DateTime.Now.ToString("HH:mm"), reader["time_slate"].ToString()) == 0)
                        {
                            SoundPlayer player = new SoundPlayer();
                            if (reader["bell_type"].ToString().Trim() == "Assembly")
                                player.SoundLocation = fun.GetSettings("assembly_tune");
                            else if (reader["bell_type"].ToString().Trim() == "Lecture")
                                player.SoundLocation = fun.GetSettings("lecture_tune");
                            else if (reader["bell_type"].ToString().Trim() == "Break")
                                player.SoundLocation = fun.GetSettings("break_tune");
                            else if (reader["bell_type"].ToString().Trim() == "Off")
                                player.SoundLocation = fun.GetSettings("off_tune");
                            else
                                player.SoundLocation = null;
                            if (player.SoundLocation != "")
                            {
                                try
                                {
                                    player.Load();
                                    player.PlaySync();
                                }
                                catch (Exception ex)
                                {
                                    MessageBox.Show(ex.Message, "Info");
                                }

                            }
                        }
                    }
                }
                con.Close();
            }
            // MessageBox.Show("Zee");

        }
        #endregion
        bool login()
        {
            bool enable = false;
            DateTime dt = DateTime.Now;
            string d = dt.Year + "-" + dt.Month + "-" + dt.Day;
            try
            {
                /*string query = "SELECT * from tbl_role";
                DataTable detdt = fun.FetchDataTable(query);
                if (detdt.Rows.Count <= 0)
                {
                    query = "INSERT INTO `tbl_role` (`role_id`, `key`, `value`, `permission_id`) VALUES ('1', 'users', '1', '1');";
                    fun.Execute_Insert(query);
                }*/
                string query = "SELECT * from tbl_permission";
                DataTable detdt = fun.FetchDataTable(query);
                if (detdt.Rows.Count <= 0)
                {
                    query = "INSERT INTO `tbl_permission` (`permission_id`, `title`,type) VALUES ('1', 'SuperAdmin','Desktop');";
                    fun.Execute_Insert(query);
                }
                query = "SELECT * from admin";
                detdt = fun.FetchDataTable(query);
                if (detdt.Rows.Count <= 0)
                {
                    query = "insert into admin(name,email,password,level,sync) value('Sarwar', '" + "0107" + "','" + "0107Sar1992" + "','" + "1" + "','0');";
                    fun.Execute_Insert(query);
                }
                query = "SELECT admin.*,per.title FROM `admin` join tbl_permission as per on per.permission_id = admin.level where email = '" + txtEmail.Text + "' and Password = '" + txtPassword.Text + "' ";
                DataTable userdt = fun.FetchDataTable(query);
                if (userdt.Rows.Count > 0)
                {
                    CurrentUserID = Convert.ToString(userdt.Rows[0]["admin_id"]);
                    CurrentUserName = Convert.ToString(userdt.Rows[0]["name"]);
                    CurrentUserStatus_id = Convert.ToString(userdt.Rows[0]["level"]);
                    CurrentUserStatus = Convert.ToString(userdt.Rows[0]["title"]);
                    CurrentUserEmail = Convert.ToString(userdt.Rows[0]["email"]);
                    enable = true;
                }
                else
                    enable = false;
                return enable;
            }
            catch (MySqlException ex)
            {
                MessageBox.Show(ex.Message, "Contact Admin");
                return false;
            }


        }
        // public static string MacAddress;
        public static DateTime Time;
        public void close()
        {
            this.Visible = false;
        }
        private void btn_aboutus_Click(object sender, EventArgs e)
        {
            a1 = new Main_FD(true);
            a1.Text = "OSP" + " / " + SchoolCode;
            a1.barUserStatus.Caption = CurrentUserName + " (" + CurrentUserStatus + ")";
            a1.Show();
        }
        private void BtnPassword_Click(object sender, EventArgs e)
        {
            //Force garbage collection.
            GC.Collect();

            // Wait for all finalizers to complete before continuing.
            GC.WaitForPendingFinalizers();
            if (login())
            {
                Visible = false;
                SplashScreenManager.ShowForm(this, typeof(ss), true, true, false);
                //a = new Main();
                a1 = new Main_FD();
                // MacAddress = fun.FetchMacId();
                //  Time = fun.GetNistTime();
                a1.Text = "OSP" + " / " + SchoolCode;
                a1.barUserStatus.Caption = CurrentUserName + " (" + CurrentUserStatus +")";
                SplashScreenManager.CloseForm();
                a1.ShowDialog();
                
            }
            else
            {
                labWorngPass.Text = "Wrong password. Try again.";
                labWorngPass.Focus();
                labWorngPass.Visible = true;
                // MessageBox.Show("Password worng contact admin....", "Contact Admin");
                return;
            }
            Close();
        }
        private void txtEmail_Enter(object sender, EventArgs e)
        {
            labWorngPass.Text = "";
        }
        private void txtPassword_Enter(object sender, EventArgs e)
        {
            labWorngPass.Text = "";
        }

        private void txtPassword_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                BtnPassword.PerformClick();
            }
        }

        private void txtEmail_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                BtnPassword.PerformClick();
            }
        }
        private void Login_Load(object sender, EventArgs e)
        {

        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        show_slider_image ssi;
        private void slider_box_MouseHover(object sender, EventArgs e)
        {
            timer_bill.Stop();
            ssi = new show_slider_image();
            ssi.pictureBox1.Image = slider_box.Image;
            ssi.Show();
        }

        private void slider_box_MouseLeave(object sender, EventArgs e)
        {
            //ssi.Close();
            timer_bill.Start();
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
