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
using System.IO;
using DevExpress.XtraGrid.Views.Base;
using DevExpress.XtraGrid.Views.Grid;

namespace SchoolManagementSystem.Admin
{
    public partial class XUCSoftPitch_settings : DevExpress.XtraEditors.XtraUserControl
    {
        private static XUCSoftPitch_settings _instance;

        public static XUCSoftPitch_settings instance
        {
            get
            {
                if (_instance == null)
                    _instance = new XUCSoftPitch_settings();
                return _instance;
            }
        }
        CommonFunctions objfunctions = new CommonFunctions();
        public XUCSoftPitch_settings()
        {
            InitializeComponent();
            loadfunctions();
            keyinfo();
        }
        void keyinfo()
        {
            systemvalidation objvalidation = new systemvalidation();
            string activation_key = objfunctions.GetSettings("activition_key");
            string key_info = objvalidation.extract_keyinfo_softpitch(activation_key);
            label_keyinfo_np.Text = key_info;
        }
        public void loadfunctions()
        {
            Settings();
            comboBox1.Text = objfunctions.GetSettings("Institute_Type");
        }
        public void permissions_btns(bool add, bool Edit, bool Delete)
        {
            gridView39.OptionsBehavior.Editable = false;
            comboBox1.Enabled = false;
            if (Edit)
            {
                gridView39.OptionsBehavior.Editable = true;
                comboBox1.Enabled = true;
            }
        }
        #region Settings
        //Settings
        private void gridView39_RowStyle(object sender, RowStyleEventArgs e)
        {
            GridView View = sender as GridView;

            if (e.RowHandle % 2 == 0)
            {
                Color color = ColorTranslator.FromHtml("#EEE9E9");
                e.Appearance.BackColor = Color.FromArgb(150, color);
                e.Appearance.FontStyleDelta = FontStyle.Bold;
            }
            else
            {
                Color color = ColorTranslator.FromHtml("#DBF6FA");
                e.Appearance.BackColor = Color.FromArgb(150, color);
                e.Appearance.FontStyleDelta = FontStyle.Bold;
            }
        }
        public void Settings()
        {
            string sql = "SELECT * from settings where settings_id in (97,98,100,101) order by settings_id";
            DataTable table = objfunctions.FetchDataTable(sql);
            SttingsgridControl.DataSource = table;
            var col = gridView39.Columns["type"];
            col.OptionsColumn.ReadOnly = true;
            var col1 = gridView39.Columns["description"];
            col1.OptionsColumn.ReadOnly = false;
            gridView39.BestFitColumns();
            for (int i = 0; i < gridView39.RowCount; i++)
            {
                string val = gridView39.GetRowCellValue(i, "type").ToString();
                if (val == "Show_company")
                {
                    //SttingsgridControl.RepositoryItems.Add(_riEditor);
                    //gridView39.Columns[1].ColumnEdit = _riEditor;

                    string des = gridView39.GetRowCellValue(i, "description").ToString();

                    if (des == "Yes")
                    {
                        rdbpartnar_yes.Checked = true;
                        rdbpartnar_no.Checked = false;
                    }
                    if (des == "No")
                    {
                        rdbpartnar_no.Checked = true;
                        rdbpartnar_yes.Checked = false;
                    }
                }
            }


            try
            {

                string sql2 = "SELECT description from settings where type = 'Company_LOGO'";
                DataTable table2 = objfunctions.FetchDataTable(sql2);
                string name = table2.Rows[0]["description"].ToString();
                if (!string.IsNullOrEmpty(name))
                    pictureEdit1.Image = objfunctions.Base64ToImage(name);
            }
            catch (Exception e)
            {
                MessageBox.Show(e.ToString(), "BackGround Image Error", MessageBoxButtons.OK, MessageBoxIcon.Hand);
            }

        }
        void updatesettingrdb(string rdbtext, string type)
        {
            MySqlConnection con = new MySqlConnection(Login.constring);
            con.Open();
            MySqlCommand cmdM = new MySqlCommand("update settings SET description='" + rdbtext + "' where type = '" + type + "' ;", con);
            cmdM.ExecuteNonQuery();
            con.Close();
            Settings();
        }
        private void rdbsmallprinter_CheckedChanged(object sender, EventArgs e)
        {
            if (rdbpartnar_yes.Checked == true)
                updatesettingrdb(rdbpartnar_yes.Text, "Show_company");
            if (rdbpartnar_no.Checked == true)
                updatesettingrdb(rdbpartnar_no.Text, "Show_company");
        }

        private void gridView39_RowUpdated(object sender, RowObjectEventArgs e)
        {
            DataRow row = gridView39.GetDataRow(gridView39.FocusedRowHandle);
            for (int i = 0; i < gridView39.RowCount; i++)
            {
                DataRow dr = gridView39.GetDataRow(i);
                string type = dr["type"].ToString();
                string Description = dr["description"].ToString();
                if (type == "Show_company")
                {
                    if (Description != "Yes" || Description != "No")
                    {
                        if (rdbpartnar_yes.Checked)
                            dr["description"] = rdbpartnar_yes.Text;
                        else if (rdbpartnar_no.Checked)
                            dr["description"] = rdbpartnar_no.Text;
                    }
                }
            }
            MySqlConnection con = new MySqlConnection(Login.constring);
            con.Open();
            MySqlCommand cmdM = new MySqlCommand("update settings SET description='" + row[2] + "' where settings_id = '" + row[0] + "' ;", con);
            cmdM.ExecuteNonQuery();
            con.Close();
        }

        string pic = "";
        private void BtnSelectImage_Click(object sender, EventArgs e)
        {
            OpenFileDialog fileOpen = new OpenFileDialog();
            fileOpen.Title = "Open Image file";
            fileOpen.Filter = "JPG Files (*.Jpg)| *.Jpg";

            if (fileOpen.ShowDialog() == DialogResult.OK)
            {
                pictureEdit1.Image = Image.FromFile(fileOpen.FileName);
                pic = objfunctions.ImageToBase64(pictureEdit1.Image, System.Drawing.Imaging.ImageFormat.Jpeg);
                if (pic.Count() > 65000)
                {
                    MessageBox.Show("Image Size to long..");
                    pictureEdit1.Image = null;
                }
                BtnUploadLogo.Enabled = true;
            }
            
            fileOpen.Dispose();
        }

        private void BtnUpload_Click(object sender, EventArgs e)
        {
            BtnUploadLogo.Enabled = false;
            MySqlConnection con = new MySqlConnection(Login.constring);
            con.Open();
            MySqlCommand cmd2 = new MySqlCommand("update settings SET description='" + pic + "',sync='0' where type = 'Company_LOGO' ;", con);
            cmd2.ExecuteNonQuery();
            con.Close();
            MessageBox.Show("Image Successfully uploaded.", "Info");
            Settings();
        }

        #endregion

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

            string query = "update settings SET description='" + comboBox1.Text + "',sync='0' where type = 'Institute_Type' ;";
            objfunctions.ExecuteQuery(query);
        }
    }
}
