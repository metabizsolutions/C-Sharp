using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Forms;

namespace SchoolManagementSystem.Admin
{
    public partial class BellSetting : DevExpress.XtraEditors.XtraUserControl
    {
        ObservableCollection<AllClass> allClass = new ObservableCollection<AllClass>();
        ObservableCollection<Setting> typesetting = new ObservableCollection<Setting>();
        CommonFunctions fun = new CommonFunctions();

        private static BellSetting _instance;
        public static BellSetting instance
        {
            get
            {
                if (_instance == null)
                    _instance = new BellSetting();
                return _instance;
            }
        }

        public BellSetting()
        {
            InitializeComponent();
            loadfunctions();
        }
        public void loadfunctions()
        {
            tuneSetting();
        }
        public void permissions_btns(bool add, bool Edit, bool Delete)
        {
            //bool Edit = fun.isAllow("Edit", "bell_setting");
            BtnUploadAssemblyTune.Enabled = false;
            btnUploadBreakTune.Enabled = false;
            btnUploadLectureTune.Enabled = false;
            btnUploadOffTune.Enabled = false;
            if (Edit)
            {
                BtnUploadAssemblyTune.Enabled = true;
                btnUploadBreakTune.Enabled = true;
                btnUploadLectureTune.Enabled = true;
                btnUploadOffTune.Enabled = true;
                
            }
        }

        public void tuneSetting()
        {
            allClass.Clear();
            List<Setting> myType = new List<Setting>
            {
                new Setting{type = "assembly_tune"},
                new Setting{type = "lecture_tune"},
                new Setting{type = "break_tune"},
                new Setting{type = "off_tune"},
            };
            MySqlConnection con = new MySqlConnection(Login.constring);

            for (int i = 0; i < myType.Count; i++)
            {
                con.Open();
                MySqlCommand cmd1 = new MySqlCommand("SELECT description FROM settings where type='" + myType[i].type.ToString() + "'", con);
                MySqlDataReader reader1 = cmd1.ExecuteReader();
                if (reader1.HasRows)
                {
                    while (reader1.Read())
                    {
                        if (myType[i].type.ToString() == "assembly_tune")
                            labAssemblyTune.Text = reader1["description"].ToString();
                        else if (myType[i].type.ToString() == "lecture_tune")
                            labLectureTune.Text = reader1["description"].ToString();
                        else if (myType[i].type.ToString() == "break_tune")
                            labBreakTune.Text = reader1["description"].ToString();
                        else if (myType[i].type.ToString() == "off_tune")
                            labOffTune.Text = reader1["description"].ToString();

                    }
                }
                con.Close();
            }

        }

        string tune = "";
        private void BtnSelectAssemTune_Click(object sender, EventArgs e)
        {
            OpenFileDialog fileOpen = new OpenFileDialog();
            fileOpen.Title = "Open Image file";
            fileOpen.Filter = "WAV Files (*.wav)| *.wav";

            if (fileOpen.ShowDialog() == DialogResult.OK)
            {
                labAssemblyTune.Text = fileOpen.FileName;
                tune = labAssemblyTune.Text;
            }
            fileOpen.Dispose();
            BtnUploadAssemblyTune.Enabled = true;
        }

        private void BtnUploadAssemblyTune_Click(object sender, EventArgs e)
        {

            var s = tune.Split(':');
            var r = s[0] + ":\\" + s[1];
            tune = r;
            MySqlConnection con = new MySqlConnection(Login.constring);
            con.Open();
            MySqlCommand cmd2 = new MySqlCommand("update settings SET description='" + tune + "',sync='0' where type = 'assembly_tune' ;", con);
            cmd2.ExecuteNonQuery();
            con.Close();
            tune = "";
            MessageBox.Show("Tune Successfully uploaded.", "Info");

        }
        private void BtnUploadAssemblyTune_ChangeUICues(object sender, UICuesEventArgs e)
        {
         
        }

        private void BtnSelectTuneLecture_Click(object sender, EventArgs e)
        {

            OpenFileDialog fileOpen = new OpenFileDialog();
            fileOpen.Title = "Open Image file";
            fileOpen.Filter = "WAV Files (*.wav)| *.wav";

            if (fileOpen.ShowDialog() == DialogResult.OK)
            {
                labLectureTune.Text = fileOpen.FileName;
                tune = labLectureTune.Text;
            }
            fileOpen.Dispose();
            btnUploadLectureTune.Enabled = true;
        }

        private void btnUploadLectureTune_Click(object sender, EventArgs e)
        {
            MySqlConnection con = new MySqlConnection(Login.constring);
            con.Open();
            MySqlCommand cmd2 = new MySqlCommand("update settings SET description='" + tune + "',sync='0' where type = 'lecture_tune' ;", con);
            cmd2.ExecuteNonQuery();
            con.Close();
            tune = "";
            MessageBox.Show("Tune Successfully uploaded.", "Info");

        }

        private void BtnSelectTuneBreak_Click(object sender, EventArgs e)
        {

            OpenFileDialog fileOpen = new OpenFileDialog();
            fileOpen.Title = "Open Image file";
            fileOpen.Filter = "WAV Files (*.wav)| *.wav";

            if (fileOpen.ShowDialog() == DialogResult.OK)
            {
                labBreakTune.Text = fileOpen.FileName;
                tune = labBreakTune.Text;
            }
            fileOpen.Dispose();
            btnUploadBreakTune.Enabled = true;
        }

        private void btnUploadBreakTune_Click(object sender, EventArgs e)
        {
            MySqlConnection con = new MySqlConnection(Login.constring);
            con.Open();
            MySqlCommand cmd2 = new MySqlCommand("update settings SET description='" + tune + "',sync='0' where type = 'break_tune' ;", con);
            cmd2.ExecuteNonQuery();
            con.Close();
            tune = "";
            MessageBox.Show("Tune Successfully uploaded.", "Info");

        }

        private void BtnSelectTuneOff_Click(object sender, EventArgs e)
        {

            OpenFileDialog fileOpen = new OpenFileDialog();
            fileOpen.Title = "Open Image file";
            fileOpen.Filter = "WAV Files (*.wav)| *.wav";

            if (fileOpen.ShowDialog() == DialogResult.OK)
            {
                labOffTune.Text = fileOpen.FileName;
                tune = labOffTune.Text;
            }
            fileOpen.Dispose();
            btnUploadOffTune.Enabled = true;
        }

        private void btnUploadOffTune_Click(object sender, EventArgs e)
        {
            MySqlConnection con = new MySqlConnection(Login.constring);
            con.Open();
            MySqlCommand cmd2 = new MySqlCommand("update settings SET description='" + tune + "',sync='0' where type = 'off_tune' ;", con);
            cmd2.ExecuteNonQuery();
            con.Close();
            tune = "";
            MessageBox.Show("Tune Successfully uploaded.", "Info");

        }

    }
}
