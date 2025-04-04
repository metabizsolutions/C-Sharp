using MySql.Data.MySqlClient;
using System;
using System.Collections.ObjectModel;
using System.Data;
using System.Data.OleDb;
using System.Diagnostics;
using System.IO;
using System.Media;
using System.Reflection;
using System.Windows.Forms;

namespace SchoolManagementSystem.Admin
{
    public partial class MarketingSMS : DevExpress.XtraEditors.XtraUserControl
    {
        private static MarketingSMS _instance;

        public static MarketingSMS instance
        {
            get
            {
                if (_instance == null)
                    _instance = new MarketingSMS();
                return _instance;
            }
        }
        ObservableCollection<AllClass> allClass = new ObservableCollection<AllClass>();
        CommonFunctions fun = new CommonFunctions();
        static int exam_id = 0;
        static int exam_id_old = 0;
        public Boolean flag_checkbox = false;
        public MarketingSMS()
        {
            InitializeComponent();
            loadfunctions();
        }
        public void loadfunctions()
        {
            FillGridNoriceBord();

            String sample = "Aslam-O-Alikum Dear [B] ! Your result was announced yesterday. \r\n" +
                            "Obtaind Marks : [C] \r\n" +
                            "Result is : [D] \r\n" +
                            "Grade is : [E] \r\n" +
                            "Position : [F]";
            txtMessage.Text = sample;
        }
        public void permissions_btns(bool add, bool Edit, bool Delete)
        {
            //bool add = fun.isAllow("Add", "marketing_sms");
            BtnSendSms.Enabled = false;
            if (add)
            {
                BtnSendSms.Enabled = true;
            }
            //bool Edit = fun.isAllow("Edit", "registration");
            //if (Edit)
            //{
            //    //gridView1.OptionsBehavior.Editable = true;
            //    //gridView1.OptionsBehavior.ReadOnly = false;
            //}
            //bool Delete = fun.isAllow("Delete", "registration");
            //if (Delete)
            //    btnDelete.Enabled = true;
        }
        //Upload excel FIle
        String filename = "";

        private void BtnExlCellNO_Click(object sender, EventArgs e)
        {
            filename = "";
            using (OpenFileDialog openFileDialog1 = new OpenFileDialog())
            {
                openFileDialog1.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
                openFileDialog1.Title = "Open file";
                openFileDialog1.Filter = "Excel Files|*.xls;*.xlsx;*.xlsm";
                if (openFileDialog1.ShowDialog() == DialogResult.OK)
                {
                    filename = openFileDialog1.FileName;
                    txtFilePath.Text = filename;
                    
                }
            }
        }
        public async void ImportXfile(string filename, String message)
        {
            
            String[] alphabatic = {"[A]", "[B]", "[C]", "[D]", "[E]", "[F]", "[G]", "[H]", "[I]", "[J]",
                    "[K]", "[L]", "[M]", "[N]", "[O]", "[P]", "[Q]", "[R]", "[S]", "[T]", "[U]", "[V]", "[W]",
                    "[X]", "[Y]", "[Z]" };
            string ConStringOleDb = string.Format("Provider=Microsoft.ACE.OLEDB.12.0;Data Source={0};" + "Extended Properties='Excel 12.0 Xml;HDR=YES;IMEX=1;';", filename);
            OleDbConnection xlConn = new OleDbConnection(ConStringOleDb);
            xlConn.Open();
            OleDbCommand selectCmd = new OleDbCommand("SELECT * FROM [Sheet1$]", xlConn);
            OleDbDataReader dr = selectCmd.ExecuteReader();

            if (dr.HasRows)
            {
                int count = 1;
                //total count
                selectCmd = new OleDbCommand("SELECT count(*) AS mycount FROM [Sheet1$]", xlConn);
                OleDbDataReader drCount = selectCmd.ExecuteReader();
                drCount.Read();
                int total_count = (int)drCount["mycount"];

                String insert_query, mymessage, key_val, mobile;
                var progress = new Progress<ProgressReport>();
                progress.ProgressChanged += (o, report) =>
                {
                    txtprogressbar.EditValue = report.PercentComplete;
                    txtprogressbar.Update();
                };
                while (dr.Read())
                {
                    mymessage = message;
                    try
                    {
                        
                            mobile = dr[0].ToString().Trim();
                        if (mobile != "")
                        {
                            if (mobile.Substring(0, 1) == "0")
                                mobile = mobile.Substring(1);
                            if (mobile.Substring(0, 2) != "92")
                                mobile = "92" + mobile;
                        }
                        else
                        {
                            mobile = "";
                        }
                        for (int i = 1; i < dr.FieldCount; i++)
                        {
                            key_val = dr[i].ToString();
                            if (i <= 26)
                            {
                                mymessage = mymessage.Replace(alphabatic[i], key_val);
                            }

                        }
                        if (count == 1)
                        {

                            DialogResult confirm = MessageBox.Show(mymessage, "Do you want to continue? ", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
                            if (confirm == DialogResult.Cancel)
                            {
                                break;
                            }
                            else
                            {
                                string date = DateTime.Now.ToString("yyyy-MM-dd");
                                insert_query = "INSERT INTO noticeboard(notice_title,notice,`date`,receiver,total) " +
                                    " VALUES('{0}','{1}','{2}','{3}','{4}')";
                                insert_query = String.Format(insert_query, txtTitle.Text, message, date, "Marketing", total_count);
                                fun.ExecuteQuery(insert_query);
                            }
                        }

                        txtStatus.Items.Add("SEND SMS TO " + mobile + " ->" + message + " " + count + "/" + total_count + "\r\n");
                        insert_query = "INSERT into sms_que(mobile,sms,status) VALUES('{0}','{1}','0');";
                        insert_query = String.Format(insert_query, mobile, mymessage);
                        fun.ExecuteQuery(insert_query);
                        await fun.ProcessDate(count, total_count, progress);
                        count++;

                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                    count++;
                }
            }
            xlConn.Close();
            FillGridNoriceBord();
        }

      
 
        private void FillGridNoriceBord()
        {
            gridNoticebord.DataSource = null;
            gridView1.Columns.Clear();
            String sql = "SELECT notice_id AS ID,notice_title AS Title,`date` AS `Date`,receiver AS Receiver,notice AS Notice,  total " +
                            " FROM noticeboard WHERE receiver = 'Marketing' " +
                            " ORDER BY notice_id DESC ";

            DataTable table = fun.FetchDataTable(sql);
            gridNoticebord.DataSource = table;
            gridView1.BestFitColumns();
            var col = gridView1.Columns["ID"];
            col.OptionsColumn.ReadOnly = true;

        }

        private void BtnNDelete_Click(object sender, EventArgs e)
        {
            try
            {
                if (MessageBox.Show("Are you sure to delete?", "Confirm Delete!!", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    DataRow row = gridView1.GetDataRow(gridView1.FocusedRowHandle);
                    MySqlConnection con = new MySqlConnection(Login.constring);
                    con.Open();
                    MySqlCommand cmdM = new MySqlCommand("DELETE from noticeboard WHERE notice_id='" + row[0] + "';", con);
                    cmdM.ExecuteNonQuery();
                    con.Close();
                    FillGridNoriceBord();
                }
            }
            catch (Exception ex)
            {
                SystemSounds.Hand.Play();
                MessageBox.Show(ex.Message, "Info");
            }
        }

        private void BtnSendSms_Click(object sender, EventArgs e)
        {
            String message = txtMessage.Text;
            if (message == "" || filename == "")
            {
                MessageBox.Show("Message and Filename is required! ");
            }
            else
            { 
                ImportXfile(filename, message);
            }

        }

        private void btnSample_Click(object sender, EventArgs e)
        {
            string executableLocation = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            string xslLocation = Path.Combine(executableLocation, "MarketingSMS.xlsx");
            Process.Start(xslLocation);
        }
    }
}
