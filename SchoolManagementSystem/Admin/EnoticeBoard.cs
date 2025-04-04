using DevExpress.CodeParser;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Repository;
using MySql.Data.MySqlClient;
using QRCoder;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Media;
using System.Net;
using System.Text;
using System.Windows.Forms;

namespace SchoolManagementSystem.Admin
{
    public partial class EnoticeBoard : DevExpress.XtraEditors.XtraUserControl
    {
        private static EnoticeBoard _instance;

        public static EnoticeBoard instance
        {
            get
            {
                if (_instance == null)
                    _instance = new EnoticeBoard();
                return _instance;
            }
        }
        ObservableCollection<AllClass> allClass = new ObservableCollection<AllClass>();
        CommonFunctions fun = new CommonFunctions();
        static int exam_id = 0;
        static int exam_id_old = 0;
        public Boolean flag_checkbox = false;
        public EnoticeBoard()
        {
            InitializeComponent();
            loadfunctions();
        }
        public void loadfunctions()
        {
            LoadNoticebord();
            string sms_count = fun.GetSettings("Create_Per_SMS");
            Create_per_sms(sms_count);
            QRCode();
        }


        public void QRCode()
        {
            string hostName = Dns.GetHostName(); // Retrive the Name of HOST 
            // Get the IP  
            string myIP = Dns.GetHostByName(hostName).AddressList[0].ToString();
            string myaddress = "http://" + myIP + "/sms.php";
            QRCodeGenerator qr = new QRCodeGenerator();
            QRCodeData qrdata = qr.CreateQrCode(myaddress, QRCodeGenerator.ECCLevel.Q);
            QRCode code = new QRCode(qrdata);
            pictureBox1.Image = code.GetGraphic(5);
        }
        GridCheckMarksSelection gridCheckMarksSA;
        public void LoadNoticebord()
        {
            allClass.Clear();
            txtSendSms.Properties.Items.Clear();
            //Receiver List
            txtSendSms.Properties.Items.Add("Student");
            txtSendSms.Properties.Items.Add("Parent");
            txtSendSms.Properties.Items.Add("Teacher");
            txtSendSms.Properties.Items.Add("Visitor");
            txtSendSms.Properties.Items.Add("Passout Students");
            txtSendSms.Properties.Items.Add("Staffs Left");

            FillGridNoriceBord();
        }
        public void permissions_btns(bool add, bool Edit, bool Delete)
        {
            //bool add = fun.isAllow("Add", "e_noticeboard");
            if (add)
            {
                btnNAdd.Enabled = true;
            }
            //bool Edit = fun.isAllow("Edit", "e_noticeboard");
            if (Edit)
            {
                gridView1.OptionsBehavior.Editable = true;
            }
            //bool Delete = fun.isAllow("Delete", "e_noticeboard");
            if (Delete)
                BtnNDelete.Enabled = true;
        }
        void gridCheckMarks_SelectionChanged(object sender, EventArgs e)
        {
            if (ActiveControl is GridLookUpEdit)
            {
                StringBuilder sb = new StringBuilder();
                foreach (DataRowView rv in (sender as GridCheckMarksSelection).Selection)
                {
                    if (sb.ToString().Length > 0) { sb.Append(", "); }
                    sb.Append(rv["name"].ToString());
                }
                (ActiveControl as GridLookUpEdit).Text = sb.ToString();
            }
        }

        void gridLookUpEdit1_CustomDisplayText(object sender, DevExpress.XtraEditors.Controls.CustomDisplayTextEventArgs e)
        {
            StringBuilder sb = new StringBuilder();
            GridCheckMarksSelection gridCheckMark = sender is GridLookUpEdit ? (sender as GridLookUpEdit).Properties.Tag as GridCheckMarksSelection : (sender as RepositoryItemGridLookUpEdit).Tag as GridCheckMarksSelection;
            if (gridCheckMark == null) return;
            foreach (DataRowView rv in gridCheckMark.Selection)
            {
                if (sb.ToString().Length > 0) { sb.Append(", "); }
                sb.Append(rv["name"].ToString());
            }
            e.DisplayText = sb.ToString();
        }

        private void btnNAdd_Click(object sender, System.EventArgs e)
        {
            txtStatus.Items.Clear();
            GridCheckMarksSelection gridCheckMark = GridReceiverList.Properties.Tag as GridCheckMarksSelection;
            ArrayList ReceiverList = gridCheckMark.Selection;

            if (ReceiverList.Count == 0 || txtTitle.Text == "" || txtDate.Text == "")
            {
                MessageBox.Show("Receiver,Message and Date are required fields", "INFO");
                return;
            }
            txtStatus.Text = "";
            string date = Convert.ToDateTime(txtDate.Text).ToString("yyyy-MM-dd");

            try
            {
                String insert_query = "INSERT INTO noticeboard(notice_title,notice,`date`,receiver,total,attachment) " +
                    " VALUES('{0}','{1}','{2}','{3}','{4}','{5}')";
                insert_query = String.Format(insert_query, txtTitle.Text, txtNotice.Text, date, txtSendSms.Text, ReceiverList.Count, txtfilepath.Text);
                fun.ExecuteQuery(insert_query);
            }
            catch (MySqlException ex)
            {
                SystemSounds.Hand.Play();
                MessageBox.Show(ex.Message, "Error");
                return;
            }
            FillGridNoriceBord();

            sendSMS(txtNotice.Text, txtSendSms.Text, ReceiverList);
            empty();
        }
        private void empty()
        {
            txtTitle.Text = "";
            //txtDate.Text = "";
            txtNotice.Text = "";
            txtfilepath.Text = "";
            //txtSendSms.Text = "";
            //txtSendSms.Properties.Items.Clear();
        }

        async void sendSMS(string message, String SendSMS, ArrayList ReceiverList)
        {
            String SMS = fun.GetSettings("enotice_sms");
            if (ReceiverList.Count > 0 && SMS == "1")
            {
                int count = 1;
                int total_count = ReceiverList.Count;
                String insert_query, mobile, name = "", mymessage;
                var progress = new Progress<ProgressReport>();
                progress.ProgressChanged += (o, report) =>
                {
                    txtprogressbar.EditValue = report.PercentComplete;
                    txtprogressbar.Update();
                };
                int sms_no = 0;
                foreach (DataRowView item in ReceiverList)
                {
                    sms_no++;
                    mobile = item.Row["phone"].ToString();
                    name = item.Row["name"].ToString();

                    txtStatus.Items.Add("SEND SMS TO " + mobile + "-" + name + " ->" + message + " " + count + "/" + total_count + "\r\n");
                    try
                    {
                        mymessage = message.Replace("[name]", name) + " #" + sms_no.ToString();
                        insert_query = "INSERT into sms_que(mobile,sms,status) VALUES('" + mobile + "','" + mymessage + "','0');";
                        fun.ExecuteQuery(insert_query);
                    }
                    catch (MySqlException ex)
                    {
                        SystemSounds.Hand.Play();
                        MessageBox.Show(ex.Message, "Error");
                        return;
                    }
                    await fun.ProcessDate(count, total_count, progress);
                    count++;
                }
            }
        }

        private void FillGridNoriceBord()
        {
            gridNoticebord.DataSource = null;
            gridView1.Columns.Clear();
            String sql = "SELECT notice_id AS ID,notice_title AS Title,`date` AS `Date`,receiver AS Receiver,notice AS Notice,  total,expire as Expire,attachment " +
                            " FROM noticeboard WHERE receiver != 'Marketing'" +
                            " ORDER BY notice_id DESC ";
            DataTable table = fun.FetchDataTable(sql);
            gridNoticebord.DataSource = table;
            gridView1.BestFitColumns();
            var col = gridView1.Columns["ID"];
            col.OptionsColumn.ReadOnly = true;
            RepositoryItemMemoEdit me = new RepositoryItemMemoEdit();
            gridView1.Columns["Notice"].ColumnEdit = me;
            gridView1.Columns["attachment"].OptionsColumn.ReadOnly = true;

        }

        private void BtnNDelete_Click(object sender, EventArgs e)
        {
            try
            {
                if (MessageBox.Show("Are you sure to delete?", "Confirm Delete!!", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    DataRow row = gridView1.GetDataRow(gridView1.FocusedRowHandle);
                    //string query = "select attachment from noticeboard where notice_id='" + row["ID"] + "'";
                    //object filepath = fun.Execute_Scaler_string(query);
                    string query = "DELETE from noticeboard WHERE notice_id='" + row["ID"] + "';";
                    fun.ExecuteQuery(query);
                    FillGridNoriceBord();
                }
            }
            catch (Exception ex)
            {
                SystemSounds.Hand.Play();
                MessageBox.Show(ex.Message, "Info");
            }
        }

        private void txtSendSms_EditValueChanged(object sender, EventArgs e)
        {
            String[] receiver_array = txtSendSms.Text.Split(',');
            if (receiver_array.Length > 0)
            {
                String query = "";
                foreach (String receiver in receiver_array)
                {
                    if (receiver.Trim() == "Student")
                    {
                        if (query != "") { query += " UNION ALL "; }
                        query += "SELECT * FROM (" +
                            "SELECT student_id AS id, student.phone, class.name AS class, section.name AS section, student.name " +
                                    "FROM student " +
                                    "INNER JOIN class ON class.class_id = student.class_id " +
                                    "INNER JOIN section ON section.section_id = student.section_id " +
                                    " WHERE student.phone != '' and student.passout != 1 " +
                                    "ORDER BY class.name ASC, section.name ASC, student.name ASC" +
                                    ") tblstu ";
                    }
                    if (receiver.Trim() == "Passout Students")
                    {
                        if (query != "") { query += " UNION ALL "; }
                        query += "SELECT * FROM (" +
                            " SELECT student_id AS id, student.phone, class.name AS class, section.name AS section, student.name " +
                                    " FROM student " +
                                    " INNER JOIN class ON class.class_id = student.class_id " +
                                    " INNER JOIN section ON section.section_id = student.section_id " +
                                    " WHERE student.phone != '' and student.passout = 1 " +
                                    "ORDER BY class.name ASC, section.name ASC, student.name ASC" +
                                    ") tblstu ";
                    }
                    if (receiver.Trim() == "Parent")
                    {
                        if (query != "") { query += " UNION ALL "; }
                        query += "SELECT * FROM (" +
                            "SELECT student_id AS id,parent.phone, class.name AS class, section.name AS section, CONCAT(student.name,'/',parent.name) AS `name` " +
                                    "FROM student " +
                                    "INNER JOIN parent ON parent.parent_id = student.parent_id " +
                                    "INNER JOIN class ON class.class_id = student.class_id " +
                                    "INNER JOIN section ON section.section_id = student.section_id " +
                                     "WHERE parent.phone != '' AND student.passout != 1 " +
                                    "ORDER BY class.name ASC, section.name ASC, student.name ASC" +
                                    ") tblpar";
                    }

                    if (receiver.Trim() == "Teacher")
                    {
                        if (query != "") { query += " UNION ALL "; }
                        query += "SELECT * FROM (" +
                            "SELECT teacher_id AS id, teacher.phone, 'teacher' AS class, teacher.designation AS section, teacher.name " +
                                    "FROM teacher " +
                                     "WHERE teacher.phone != '' and teacher.passout != 1 " +
                                    "ORDER BY teacher.designation ASC, teacher.name ASC" +
                                    ") tblteach ";
                    }

                    if (receiver.Trim() == "Visitor")
                    {
                        if (query != "") { query += " UNION ALL "; }
                        query += "SELECT * FROM (" +
                            "SELECT VID AS id, visitor.sms AS phone, class.name AS class, tbl_school.name AS section, visitor.Name AS `name`  " +
                                    "FROM visitor " +
                                    "LEFT JOIN class ON class.class_id = visitor.ClassId " +
                                    "LEFT JOIN tbl_school ON tbl_school.school_id = visitor.School " +
                                    "WHERE visitor.sms != '' " +
                                    "ORDER BY class.name ASC, visitor.Name ASC" +
                                    ") tblvis";
                    }
                    if (receiver.Trim() == "Staffs Left")
                    {
                        if (query != "") { query += " UNION ALL "; }
                        query += "SELECT * FROM (" +
                            "SELECT teacher_id AS id, teacher.phone, 'teacher' AS class, teacher.designation AS section, teacher.name " +
                                    "FROM teacher " +
                                     "WHERE teacher.phone != '' and teacher.passout = 1 " +
                                    "ORDER BY teacher.designation ASC, teacher.name ASC" +
                                    ") tblteach ";
                    }
                }
                if (query != "")
                {
                    DataTable table = fun.FetchDataTable(query);
                    GridReceiverList.Properties.DataSource = table;
                    GridReceiverList.Text = "";
                    GridReceiverList.Properties.DisplayMember = "name";
                    GridReceiverList.Properties.ValueMember = "phone";
                    GridReceiverList.Properties.View.OptionsSelection.MultiSelect = true;
                    GridReceiverList.Properties.PopulateViewColumns();

                    if (!flag_checkbox)
                    {
                        gridCheckMarksSA = new GridCheckMarksSelection(GridReceiverList.Properties);
                        GridReceiverList.Properties.Tag = gridCheckMarksSA;
                        flag_checkbox = true;
                    }

                }
            }
        }

        private void gridView1_RowUpdated(object sender, DevExpress.XtraGrid.Views.Base.RowObjectEventArgs e)
        {
            DataRow row = gridView1.GetDataRow(gridView1.FocusedRowHandle);
            string date = DateTime.Now.ToString("yyyy-MM-dd");
            string expire = "";
            if (Convert.ToBoolean(row["Expire"]) == true)
                expire = "1";
            else
                expire = "0";
            string query = "UPDATE `noticeboard` SET `notice_title`='" + row["Title"] + "',`notice`='" + row["Notice"] + "',`date`='" + date + "',`receiver`='" + row["Receiver"] + "',`total`='" + row["total"] + "',`expire`='" + expire + "' WHERE `notice_id` ='" + row["ID"] + "'";
            fun.ExecuteQuery(query);
            FillGridNoriceBord();
        }
        private void BtnBBrowse_Click(object sender, EventArgs e)
        {
            if (fun.CheckForInternetConnection())
            {
                DialogResult result = openFileDialog1.ShowDialog(); // Show the dialog.
                if (result == DialogResult.OK) // Test result.
                {
                    string file = openFileDialog1.FileName;

                    string[] f = file.Split('\\');
                    // to get the only file name

                    string fn = f[(f.Length) - 1];
                    double length = new FileInfo(file).Length;
                    length = (length / 1024f) / 1024f;

                    string fileExt = Path.GetExtension(file);

                    if (length >= 25)
                    {
                        MessageBox.Show("file should be smaller then 25MB", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return;
                    }
                    try
                    {
                        string apipath = fun.GetSettings("api_url") + "/upload";
                        string school = fun.GetSettings("school_code");
                        string name = school + "_" + fn;
                        string dirpath = Directory.GetCurrentDirectory();
                        System.Net.WebClient Client = new System.Net.WebClient();
                        Client.Headers.Add("Content-Type", "binary/octet-stream");
                        byte[] resul = Client.UploadFile(apipath, "POST", file);
                        String uploadedpath = System.Text.Encoding.UTF8.GetString(resul, 0, resul.Length);
                        txtfilepath.Text = uploadedpath;
                    }
                    catch (Exception ex)
                    {

                    }

                }
            }
            else
                MessageBox.Show("Internet Connection is not available", "Informations", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
        private void txtNotice_TextChanged(object sender, EventArgs e)
        {
            if (txtNotice.Text.Length >= 0 && txtNotice.Text.Length <= 70)
                lblmsgcount.Text = "1";
            else if (txtNotice.Text.Length > 70 && txtNotice.Text.Length <= 134)
            {
                lblmsgcount.Text = "2";
            }
            else if (txtNotice.Text.Length > 134 && txtNotice.Text.Length <= 200)
            {
                lblmsgcount.Text = "3";
            }
            else if (txtNotice.Text.Length > 200 && txtNotice.Text.Length <= 268)
            {
                lblmsgcount.Text = "4";
            }
            else if (txtNotice.Text.Length > 268 && txtNotice.Text.Length <= 335)
            {
                lblmsgcount.Text = "5";
            }
            //else {
            //    lblmsgcount.Text = "3";
            //}
            lbltotalchar.Text = txtNotice.Text.Length.ToString();
        }
        void Create_per_sms(string count)
        {
            int msg_count = Convert.ToInt32(string.IsNullOrEmpty(count) ? "1" : count);
            if (msg_count == 1)
            {
                txtNotice.MaxLength = 70;
                msg_allowed_info.Text = "/1";
            }
            else if (msg_count == 2)
            {
                txtNotice.MaxLength = 134;
                msg_allowed_info.Text = "/2";
            }
            else if (msg_count == 3)
            {
                txtNotice.MaxLength = 200;
                msg_allowed_info.Text = "/3";
            }
            else if (msg_count == 4)
            {
                txtNotice.MaxLength = 268;
                msg_allowed_info.Text = "/4";
            }
            else if (msg_count >= 5)
            {
                txtNotice.MaxLength = 335;
                msg_allowed_info.Text = "/5";
            }
        }
        private void ChangeLanguage(string lang)
        {
            foreach (Control c in this.Controls)
            {
                ComponentResourceManager resources = new ComponentResourceManager(typeof(EnoticeBoard));
                resources.ApplyResources(c, c.Name, new CultureInfo(lang));
            }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox1.SelectedItem.ToString() == "Urdu")
            {
                ChangeLanguage("ur");
            }
            else // English
            {
                ChangeLanguage("en");
            }
        }

        private void txtNotice_KeyUp(object sender, KeyEventArgs e)
        {
            /*if (fun.has_spacial_character(txtNotice.Text))
            {
                txtNotice.Text.Remove(-1, 1);
                MessageBox.Show("special characters Not Allowed", "Info", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }*/
        }

        
    }
}
