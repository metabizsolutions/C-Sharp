using DevExpress.XtraEditors.Repository;
using DevExpress.XtraGrid.Views.Grid;
using MySql.Data.MySqlClient;
using SchoolManagementSystem.Ceate_Session;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace SchoolManagementSystem.Admin
{
    public partial class GerenalSetting : DevExpress.XtraEditors.XtraUserControl
    {
        ObservableCollection<AllClassSet> allClass = new ObservableCollection<AllClassSet>();
        ObservableCollection<Setting> typesetting = new ObservableCollection<Setting>();
        CommonFunctions fun = new CommonFunctions();

        private static GerenalSetting _instance;
        public static GerenalSetting instance
        {
            get
            {
                if (_instance == null)
                    _instance = new GerenalSetting();
                return _instance;
            }
        }

        public GerenalSetting()
        {
            InitializeComponent();
            loadfunctions();
        }
        public void loadfunctions()
        {
            systemSetting();
            ShowImageLogo();

            txt_font_family.Text = fun.GetSettings("font_family");
            txt_font_size.Text = fun.GetSettings("font_size");
        }
        public void permissions_btns(bool add, bool Edit, bool Delete)
        {
            BtnSelectImageES.Enabled = false;
            BtnSelectImageLogo.Enabled = false;
            BtnSelectImagePS.Enabled = false;
            BtnSelectImageVPS.Enabled = false;
            gridView1.OptionsBehavior.Editable = false;
            if (Edit)
            {
                BtnSelectImageES.Enabled = true;
                BtnSelectImageLogo.Enabled = true;
                BtnSelectImagePS.Enabled = true;
                BtnSelectImageVPS.Enabled = true;
                gridView1.OptionsBehavior.Editable = true;

            }
        }
        RepositoryItemComboBox riComboDes = new RepositoryItemComboBox();
        RepositoryItemComboBox riCombotext = new RepositoryItemComboBox();
        public void systemSetting()
        {
            allClass.Clear();
            List<Setting> myType = new List<Setting>
            {
                new Setting{type = "system_name"},
                new Setting{type = "system_title"},
                new Setting{type = "address"},
                new Setting{type = "phone"},
                new Setting{type = "student_attendance_mobile"},
                new Setting{type = "teacher_absent_mobile"},
                new Setting{type = "principal"},
                new Setting{type = "vice_d/n" },
                new Setting{type = "controller_exam"},
                new Setting{type = "account_name"},
                new Setting{type = "account_number"},
                new Setting{type = "paypal_email"},
                new Setting{type = "currency"},
                new Setting{type = "system_email"},
                new Setting{type = "language"},
                new Setting{type = "text_align"},
                new Setting{type = "leave_allowed"},
                new Setting{type = "time_duration"},
                new Setting{type = "ded_minutes"},
                new Setting{type = "course_points"},
                new Setting{type = "PaperDays_points"},
                new Setting{type = "PaperSubmit_points"},
                new Setting{type = "ClassTime_points"},

                new Setting{type = "dress_points"},
                new Setting{type = "attendance_points"},
                new Setting{type = "sync_timer"},
                new Setting{type = "photo_path"},
                new Setting{type = "bank_receipt_detail"},
                new Setting{type = "fees_receipt_margin"},
                new Setting{type = "feecollection_sendsms_time"},
                new Setting{type = "show_metric_number"},
                new Setting{type = "has_yearly_fee"},
                new Setting{type = "Create_Per_SMS"},
                new Setting{type = "move_exam_by_section"}
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
                        AllClassSet d = new AllClassSet
                        {
                            Name = myType[i].type.ToString(),
                            Salary = reader1["description"].ToString()
                        };
                        allClass.Add(d);
                    }
                }
                con.Close();
            }
            gridSystemSetting.DataSource = allClass;
        }

        private void gridView1_CustomRowCellEdit(object sender, DevExpress.XtraGrid.Views.Grid.CustomRowCellEditEventArgs e)
        {
            if (e.Column.FieldName != "Salary") return;
            GridView gv = sender as GridView;
            string fieldValue = gv.GetRowCellValue(e.RowHandle, gv.Columns["Name"]).ToString();
            switch (fieldValue)
            {
                case "language":
                    e.RepositoryItem = riComboDes;
                    break;
                case "text_align":
                    e.RepositoryItem = riCombotext;
                    break;
            }
        }

        private void gridView1_RowUpdated(object sender, DevExpress.XtraGrid.Views.Base.RowObjectEventArgs e)
        {
            MySqlConnection con = new MySqlConnection(Login.constring);
            if (allClass.Count > 0)// && row[0].ToString() != null)
            {
                for (int i = 0; i < allClass.Count; i++)
                {
                    var item = allClass.FirstOrDefault(j => j.Name == Convert.ToString(allClass[i].Name));
                    if (item != null)
                    {
                        item.Salary = allClass[i].Salary;
                        con.Open();
                        string des = allClass[i].Salary;
                        if (allClass[i].Name == "photo_path")
                            des = @allClass[i].Salary+"'";
                        MySqlCommand cmd = new MySqlCommand("UPDATE settings set description='" + des + "',sync='0' WHERE type='" + allClass[i].Name + "';", con);
                        cmd.ExecuteNonQuery();
                        con.Close();
                    }
                }
            }
            systemSetting();
        }
        private void ShowImageLogo()
        {
            if (Login.Logo != null)
                pictureEdit1.Image = fun.Base64ToImage(Login.Logo);
            if (Login.Principal_Sign != "")
                pictureEdit2.Image = fun.Base64ToImage(Login.Principal_Sign);
            if (Login.Exam_Sign != "")
                pictureEdit3.Image = fun.Base64ToImage(Login.Exam_Sign);
            if (Login.Vice_Principal_Sign != "")
                pictureEdit4.Image = fun.Base64ToImage(Login.Vice_Principal_Sign);

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
                pic = fun.ImageToBase64(pictureEdit1.Image, System.Drawing.Imaging.ImageFormat.Jpeg);
                if (pic.Count() > 1000000)
                {
                    MessageBox.Show("Image Size to long..");
                    pictureEdit1.Image = null;
                }

            }
            fileOpen.Dispose();
            if (pictureEdit1.Image != null)
                upload_logo();
        }
        void upload_logo()
        {
            MySqlConnection con = new MySqlConnection(Login.constring);
            con.Open();
            MySqlCommand cmd2 = new MySqlCommand("update settings SET description='" + pic + "',sync='0' where type = 'logo' ;", con);
            cmd2.ExecuteNonQuery();
            con.Close();
            Login.Logo = pic;
            MessageBox.Show("Image Successfully uploaded.", "Info");
        }
        private void BtnUpload_Click(object sender, EventArgs e)
        {

        }

        private void BtnSelectImagePS_Click(object sender, EventArgs e)
        {
            OpenFileDialog fileOpen = new OpenFileDialog();
            fileOpen.Title = "Open Image file";
            fileOpen.Filter = "JPG Files (*.Jpg)| *.Jpg";

            if (fileOpen.ShowDialog() == DialogResult.OK)
            {
                pictureEdit2.Image = Image.FromFile(fileOpen.FileName);
                pic = fun.ImageToBase64(pictureEdit2.Image, System.Drawing.Imaging.ImageFormat.Jpeg);
                if (pic.Count() > 65000)
                {
                    MessageBox.Show("Image Size to long..");
                    pictureEdit2.Image = null;
                    return;
                }

            }
            fileOpen.Dispose();
            if (pictureEdit2.Image != null)
                Principlesignature();
        }
        void Principlesignature()
        {
            MySqlConnection con = new MySqlConnection(Login.constring);
            con.Open();
            MySqlCommand cmd2 = new MySqlCommand("update settings SET description='" + pic + "',sync='0' where type = 'principal_sign' ;", con);
            cmd2.ExecuteNonQuery();
            con.Close();
            Login.Logo = pic;
            MessageBox.Show("Image Successfully uploaded.", "Info");
        }
        private void btnUploadPS_Click(object sender, EventArgs e)
        {

        }

        private void BtnSelectImageES_Click(object sender, EventArgs e)
        {
            OpenFileDialog fileOpen = new OpenFileDialog();
            fileOpen.Title = "Open Image file";
            fileOpen.Filter = "JPG Files (*.Jpg)| *.Jpg";

            if (fileOpen.ShowDialog() == DialogResult.OK)
            {
                pictureEdit3.Image = Image.FromFile(fileOpen.FileName);
                pic = fun.ImageToBase64(pictureEdit3.Image, System.Drawing.Imaging.ImageFormat.Jpeg);
                if (pic.Count() > 65000)
                {
                    MessageBox.Show("Image Size to long..");
                    pictureEdit3.Image = null;
                }

            }
            fileOpen.Dispose();
            if (pictureEdit3.Image != null)
                UploadExaminar();
        }
        void UploadExaminar()
        {
            MySqlConnection con = new MySqlConnection(Login.constring);
            con.Open();
            MySqlCommand cmd2 = new MySqlCommand("update settings SET description='" + pic + "',sync='0' where type = 'exam_sign' ;", con);
            cmd2.ExecuteNonQuery();
            con.Close();
            Login.Logo = pic;
            MessageBox.Show("Image Successfully uploaded.", "Info");
        }
        private void btnUploadEX_Click(object sender, EventArgs e)
        {

        }

        private void BtnSelectImageVPS_Click(object sender, EventArgs e)
        {
            OpenFileDialog fileOpen = new OpenFileDialog();
            fileOpen.Title = "Open Image file";
            fileOpen.Filter = "JPG Files (*.Jpg)| *.Jpg";

            if (fileOpen.ShowDialog() == DialogResult.OK)
            {
                pictureEdit4.Image = Image.FromFile(fileOpen.FileName);
                pic = fun.ImageToBase64(pictureEdit4.Image, System.Drawing.Imaging.ImageFormat.Jpeg);
                if (pic.Count() > 65000)
                {
                    MessageBox.Show("Image Size to long..");
                    pictureEdit4.Image = null;

                }

            }
            fileOpen.Dispose();
            if (pictureEdit4.Image != null)
                UploadVPS();
        }
        void UploadVPS()
        {
            MySqlConnection con = new MySqlConnection(Login.constring);
            con.Open();
            MySqlCommand cmd2 = new MySqlCommand("update settings SET description='" + pic + "',sync='0' where type = 'vice_principal_sign' ;", con);
            cmd2.ExecuteNonQuery();
            con.Close();
            Login.Logo = pic;
            MessageBox.Show("Image Successfully uploaded.", "Info");
        }

        private void txt_font_family_SelectedIndexChanged(object sender, EventArgs e)
        {
            fun.update_setting(txt_font_family.Text, "font_family");
        }

        private void txt_font_size_EditValueChanged(object sender, EventArgs e)
        {
            fun.update_setting(txt_font_size.Text, "font_size");
        }
    }
}
