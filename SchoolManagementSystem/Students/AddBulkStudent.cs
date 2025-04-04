using System;
using System.Collections.ObjectModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Reflection;
using System.Windows.Forms;

namespace SchoolManagementSystem.Students
{
    public partial class AddBulkStudent : DevExpress.XtraEditors.XtraUserControl
    {
        private static AddBulkStudent _instance;

        public static AddBulkStudent instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new AddBulkStudent();
                }

                return _instance;
            }
        }

        private ObservableCollection<AllClass> allClass = new ObservableCollection<AllClass>();
        private CommonFunctions fun = new CommonFunctions();
        public AddBulkStudent()
        {
            InitializeComponent();
            loadfunctions();

        }
        public void loadfunctions()
        {
            txtClass.Properties.DataSource = fun.GetAllClasses_dt();
            txtClass.Properties.DisplayMember = "name";
            txtClass.Properties.ValueMember = "class_id";
        }
        public void permissions_btns(bool add, bool Edit, bool Delete)
        {
            btnUpload.Enabled = false;
            if (add)
            {
                btnUpload.Enabled = true;
            }
        }

        private string filename = "";
        private void buttonEdit1_Click(object sender, System.EventArgs e)
        {

            using (OpenFileDialog openFileDialog1 = new OpenFileDialog())
            {
                openFileDialog1.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
                openFileDialog1.Title = "Open file";
                openFileDialog1.Filter = "Excel Files|*.xls;*.xlsx;*.xlsm";
                if (openFileDialog1.ShowDialog() == DialogResult.OK)
                {
                    filename = openFileDialog1.FileName;
                    buttonEdit1.Text = filename;

                }
            }
        }

        private void btnUpload_Click(object sender, EventArgs e)
        {

            if (txtClass.EditValue == null)
            {
                MessageBox.Show("Select Class");
                return;
            }
            if (txtSection.EditValue == null)
            {
                MessageBox.Show("Select Section");
                return;
            }
            if (buttonEdit1.Text == "")
            {
                MessageBox.Show("Select Excel File");
                return;
            }
            else
            {
                btnUpload.Enabled = true;
                bw_uploading_students.WorkerReportsProgress = true;
                if (!bw_uploading_students.IsBusy)
                    bw_uploading_students.RunWorkerAsync();
            }

        }

        private void empty()
        {
            txtClass.Text = "";
            txtSection.Text = "";
            buttonEdit1.Text = "";
        }

        private void AddBulkStudent_Enter(object sender, EventArgs e)
        {
        }

        private void btnSample_Click(object sender, EventArgs e)
        {
            string executableLocation = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            string xslLocation = Path.Combine(executableLocation, "Class-Section.xlsx");
            Process.Start(xslLocation);

        }

        private void txtClass_EditValueChanged(object sender, EventArgs e)
        {
            string class_id = txtClass.EditValue == null ? "0" : txtClass.EditValue.ToString();
            if (!string.IsNullOrEmpty(class_id) && class_id != "0")
            {
                txtSection.Properties.DataSource = fun.GetAllSection_dt(class_id);
                txtSection.Properties.DisplayMember = "name";
                txtSection.Properties.ValueMember = "section_id";
            }
        }

        string uploading_std = "";

        private void bw_uploading_students_DoWork(object sender, System.ComponentModel.DoWorkEventArgs e)
        {
            string filename = buttonEdit1.Text;
            string classID = txtClass.EditValue.ToString();
            string sectionID = txtSection.EditValue.ToString();
            var txt = "";
            DataTable excel_dt = fun.FetchData_excel(filename, "SELECT * FROM [Sheet1$]");
            DataRow[] chack = excel_dt.Select(" (name is null or name = '') and (not birthday is null or not sex is null or not phone is null or not roll is null or not `parent name` is null or not `parent mobile` is null) ");
            if (chack.Length > 0)
            {
                MessageBox.Show("Please Insert Student Names Properly", "Info", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            chack = excel_dt.Select(" (birthday is null) and not name is null ");
            if (chack.Length > 0)
            {
                MessageBox.Show("Please Insert Student Date Of Birth Properly", "Info", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            chack = excel_dt.Select(" (sex is null or sex = '') and not name is null ");
            if (chack.Length > 0)
            {
                MessageBox.Show("Please Insert Student Gender Properly", "Info", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            chack = excel_dt.Select(" (phone is null) and not name is null ");
            if (chack.Length > 0)
            {
                MessageBox.Show("Please Insert Student Phone Number Properly", "Info", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            chack = excel_dt.Select(" (roll is null) and not name is null ");
            if (chack.Length > 0)
            {
                MessageBox.Show("Please Insert Student Roll Number Properly", "Info", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            chack = excel_dt.Select(" (`parent name` is null or `parent name` = '') and not name is null ");
            if (chack.Length > 0)
            {
                MessageBox.Show("Please Insert Student Parent Name Properly", "Info", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            chack = excel_dt.Select(" (`parent mobile` is null) and not name is null ");
            if (chack.Length > 0)
            {
                MessageBox.Show("Please Insert Student Parent Mobile Properly", "Info", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            DataRow[] all_std = excel_dt.Select("not name is null");
            string query = "";
            string parent_id = "0";
            string school_id = "0";
            DataTable p_dt = new DataTable();
            int sum = 1;
            int total = all_std.Length;
            int percent = 0;

            foreach (DataRow dr in all_std)
            {
                percent = sum * 100 / total;
                uploading_std = dr["name"].ToString() + "(" + dr["parent name"].ToString() + ")" + total + "/" + sum;

                sum++;
                bw_uploading_students.ReportProgress(percent);
                if (bw_uploading_students.CancellationPending)
                {
                    e.Cancel = true;
                    bw_uploading_students.ReportProgress(0);
                    return;
                }
                var phone = dr["phone"].ToString().Trim();
                var pphone = dr["parent mobile"].ToString().Trim();
                if (phone != "")
                {
                    if (phone.Substring(0, 1) != "0")
                    {
                        phone = "0" + phone;
                    }
                }
                if (pphone != "")
                {
                    if (pphone.Substring(0, 1) != "0")
                    {
                        pphone = "0" + pphone;
                    }
                }
                parent_id = "0";
                query = "SELECT * from parent where name = '" + dr["parent name"] + "' and phone = '" + pphone + "';";
                p_dt = fun.FetchDataTable(query);
                if (p_dt.Rows.Count > 0)
                {
                    parent_id = p_dt.Rows[0]["parent_id"].ToString();
                }
                else
                {
                    query = "INSERT into parent(name,email,password,phone,address,profession,sync) VALUES('" + dr["parent name"] + "','" + dr["Parent Email"] + "','" + dr["password"] + "','" + pphone + "','" + dr["address"] + "','" + dr["Profession"] + "','0');";
                    parent_id = fun.Execute_Insert(query).ToString();
                }
                query = "select * from tbl_school where name = '" + dr["School"] + "'";
                p_dt = fun.FetchDataTable(query);
                if (p_dt.Rows.Count > 0)
                {
                    school_id = p_dt.Rows[0]["school_id"].ToString();
                }
                else
                {
                    query = "INSERT INTO `tbl_school` (`name`,`date`) VALUES ('" + dr["School"] + "','" + DateTime.Now.ToString("yyyy-MM-dd") + "');";
                    school_id = fun.Execute_Insert(query).ToString();
                }
                query = "SELECT * from student where class_id='" + classID + "' and section_id='" + sectionID + "' and name = '" + dr["name"] + "' and phone = '" + phone + "'; ";
                p_dt = fun.FetchDataTable(query);
                if (p_dt.Rows.Count > 0)
                {
                    //query = "UPDATE student set name='" + dr["name"] + "',birthday='" + dr["birthday"] + "',sex='" + dr["sex"] + "',address='" + dr["address"] + "' ,phone='" + phone + "',email='" + dr["email"] + "',class_id='" + classID + "',section_id='" + sectionID + "',roll='" + dr["roll"] + "',attendent_sms='1',exam_sms='1',fees_sms='1',enotice_sms='1',fee_concession='0',addmission_concession='0',sync='0',passout='0' WHERE student_id='" + p_dt.Rows[0]["student_id"] + "';";
                    //ExecuteQuery(query);
                }
                else
                {
                    query = "INSERT into student(name,birthday,sex,address,phone,email,password,class_id,section_id,parent_id,roll,card_id,sync,p_school) VALUES " +
                    " ('" + dr["name"] + "','" + Convert.ToDateTime(dr["birthday"]).ToString("yyyy-MM-dd") + "','" + dr["sex"] + "','" + dr["address"] + "','" + phone + "','" + dr["email"] + "','" + dr["password"] + "','" + classID + "','" + sectionID + "','" + parent_id + "','" + dr["roll"] + "','" + dr["Card ID"] + "','0','" + school_id + "');";
                    long std_id = fun.Execute_Insert(query);

                    string image_name = std_id.ToString() + "_std";
                    string imagepath = image_name + ".Jpeg";

                    query = "INSERT INTO `ac_ledger` (`head_id`,`title`,`balance`,`status`,`mobile`,`email`,`address`,`photo`,is_auto_genrated) VALUES " +
                    " ('1','" + dr["name"] + "','0',1,'" + phone + "','" + dr["email"] + "','" + dr["address"] + "','" + imagepath + "','1');";
                    long ladger_id = fun.Execute_Insert(query);

                    query = "update student set ladger_id = '" + ladger_id + "' where student_id = '" + std_id + "'";
                    fun.ExecuteQuery(query);
                }

            }
            MessageBox.Show("Task Completed Successfully...!!", "Info");

            if (txt != "")
            {
                MessageBox.Show("Following Row not save...!!\n\r" + txt, "Info");
            }

        }

        private void bw_uploading_students_ProgressChanged(object sender, System.ComponentModel.ProgressChangedEventArgs e)
        {
            lbl_uploading_std.Text = uploading_std;
            //main.lbl_compare_type.Text = comparetype;
            progressBarControl.EditValue = e.ProgressPercentage;
            progressBarControl.Update();
        }

        private void bw_uploading_students_RunWorkerCompleted(object sender, System.ComponentModel.RunWorkerCompletedEventArgs e)
        {
            if (e.Cancelled)
                progressBarControl.EditValue = 0;
            else if (e.Error != null)
                MessageBox.Show(e.Error.Message, "Comparison Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            else
            {
                lbl_uploading_std.Text = "Uploaded Completed Successfully";
                lbl_uploading_std.ForeColor = Color.Green;
                progressBarControl.EditValue = 0;
                progressBarControl.Update();
                MessageBox.Show("Uploaded Successfully", "INFO", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            btnUpload.Enabled = true;
            empty();
        }
    }
}
