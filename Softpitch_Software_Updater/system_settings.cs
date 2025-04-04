using System;
using System.Configuration;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
using sarwar_settings;

namespace Softpitch_Software_Updater
{
    public partial class system_settings : Form
    {
        public static string server_ip = ConfigurationManager.AppSettings["server_id"];
        public static string CS_database = ConfigurationManager.AppSettings["database"];
        public static string CS_userid = ConfigurationManager.AppSettings["user_id"];
        public static string CS_password = ConfigurationManager.AppSettings["password"];
        public static string tariq_api_path = ConfigurationManager.AppSettings["tariq_api_path"];
        public static string sarwar_api_path = ConfigurationManager.AppSettings["sarwar_api_path"];
        public static string update_type = "";
        public static string constring = "server=" + server_ip + ";port=3306;user id=" + CS_userid + "; password=" + CS_password + "; database=" + CS_database + ";persist security info=true;CharSet=utf8;Allow User Variables=True;default command timeout=300;Convert Zero Datetime=True;Max Pool Size=200;Allow Zero Datetime=true;";//ConfigurationManager.ConnectionStrings["tnsbay_school_Connection"].ConnectionString;

        MySql_settings fun = new MySql_settings(constring);
        main_form mf;
        public system_settings(main_form MF)
        {
            InitializeComponent();
            server_ip = ConfigurationManager.AppSettings["server_id"];
            CS_database = ConfigurationManager.AppSettings["database"];
            CS_userid = ConfigurationManager.AppSettings["user_id"];
            CS_password = ConfigurationManager.AppSettings["password"];
            tariq_api_path = ConfigurationManager.AppSettings["tariq_api_path"];
            sarwar_api_path = ConfigurationManager.AppSettings["sarwar_api_path"];
            update_type = "";
            constring = "server=" + server_ip + ";port=3306;user id=" + CS_userid + "; password=" + CS_password + "; database=" + CS_database + ";persist security info=true;CharSet=utf8;Allow User Variables=True;default command timeout=300;Convert Zero Datetime=True;Max Pool Size=200;Allow Zero Datetime=true;";//ConfigurationManager.ConnectionStrings["tnsbay_school_Connection"].ConnectionString;
            mf = MF;
            if (sarwar_MySql_Helper.isCOnnection(constring)) // if valid connection then encript password
            {
                AddUpdateAppSettings("password", fun.Encrypt(CS_password, "Zee0107"));//password incripting in app settings
            }
            else
            {
                CS_password = fun.Decrypt(CS_password, "Zee0107");
                constring = "server=" + server_ip + ";port=3306;user id=" + CS_userid + "; password=" + CS_password + "; database=" + CS_database + ";persist security info=true;CharSet=utf8;Allow User Variables=True;default command timeout=300;Convert Zero Datetime=True;Max Pool Size=200;Allow Zero Datetime=true;";
            }
            if (!sarwar_MySql_Helper.isCOnnection_Valid(constring))
            {
                MessageBox.Show("Please check your Wamp server is running or not!", "info", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }

            t_api.Text = tariq_api_path;
            s_api.Text = sarwar_api_path;
            txt_school_code.Text = fun.GetSettings("school_code");
            txt_server.Text = server_ip;
            txt_database_name.Text = CS_database;
            txt_user_id.Text = CS_userid;
            txt_password.Text = CS_password;
        }
        public void AddUpdateAppSettings(string key, string value)
        {
            try
            {
                var configFile = ConfigurationManager.OpenExeConfiguration(Application.ExecutablePath);
                var settings = configFile.AppSettings.Settings;
                if (settings[key] == null)
                {
                    settings.Add(key, value);
                }
                else
                {
                    settings[key].Value = value;
                }
                configFile.Save(ConfigurationSaveMode.Modified);
                ConfigurationManager.RefreshSection("appSettings");
            }
            catch (ConfigurationErrorsException)
            {
                Console.WriteLine("Error writing app settings");
            }
        }
        private void btn_update_info_Click(object sender, EventArgs e)
        {
            string softpitch_password = fun.GetSettings("device_password");
            using (confirm_softpitch de = new confirm_softpitch())
            {
                if (de.ShowDialog() == DialogResult.Yes) { }
                else if (de.has_password == true && de.txt_password.Text == softpitch_password)
                {
                    MySqlConnectionStringBuilder mscsb = new MySqlConnectionStringBuilder(constring);
                    mscsb.Server = txt_server.Text.Trim();
                    mscsb.Database = txt_database_name.Text.Trim();
                    mscsb.UserID = txt_user_id.Text.Trim();
                    mscsb.Password = txt_password.Text.Trim();

                    if (!sarwar_MySql_Helper.isCOnnection_Valid(mscsb.ConnectionString))
                    {
                        MessageBox.Show("Please chack your credantials its not correct", "DB Connection Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return;
                    }
                    MySql_settings.constring = mscsb.ConnectionString;
                    AddUpdateAppSettings("server_id", txt_server.Text);
                    AddUpdateAppSettings("database", txt_database_name.Text);
                    AddUpdateAppSettings("user_id", txt_user_id.Text);
                    AddUpdateAppSettings("password", fun.Encrypt(txt_password.Text, "Zee0107"));
                    AddUpdateAppSettings("sarwar_api_path", s_api.Text);
                    AddUpdateAppSettings("tariq_api_path", t_api.Text);

                    if (!string.IsNullOrEmpty(txt_school_code.Text.Trim()))
                        fun.update_setting(txt_school_code.Text.Trim(), "school_code");
                    MessageBox.Show("Values Updated Successfully", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    mf.load_config_data();
                }
                else
                {
                    MessageBox.Show("Values Not Update Dated Properly", "Updation Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
    }
}
