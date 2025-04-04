using DevExpress.LookAndFeel;
using DevExpress.Utils;
using DevExpress.XtraEditors.Mask;
using DevExpress.XtraEditors.Repository;
using DevExpress.XtraPrinting;
using MySql.Data.MySqlClient;
using System;
using System.Collections.ObjectModel;
using System.Data;
using System.Drawing;
using System.Drawing.Printing;
using System.IO;
using System.Media;
using System.Windows.Forms;

namespace SchoolManagementSystem.Class
{
    public partial class ManageTeacher : DevExpress.XtraEditors.XtraUserControl
    {
        private static ManageTeacher _instance;

        public static ManageTeacher instance
        {
            get
            {
                if (_instance == null)
                    _instance = new ManageTeacher();
                return _instance;
            }
        }
        CommonFunctions fun = new CommonFunctions();
        public ManageTeacher()
        {
            InitializeComponent();
            loadfunctions();
        }
        public void permissions_btns(bool add, bool Edit, bool Delete)
        {
            btnNew.Enabled = false;
            gridView1.OptionsBehavior.Editable = false;
            BtnTDelete.Enabled = false;
            if (add)
            {
                btnNew.Enabled = true;
            }
            if (Edit)
            {
                gridView1.OptionsBehavior.Editable = true;
            }
            if (Delete)
                BtnTDelete.Enabled = true;
        }
        public void loadfunctions()
        {
            txtTimeStart.Properties.Mask.MaskType = MaskType.DateTime;
            txtTimeStart.Properties.Mask.EditMask = "HH:mm";
            txtTimeStart.MaskBox.Mask.UseMaskAsDisplayFormat = true;
            txtTimeEnd.Properties.Mask.MaskType = MaskType.DateTime;
            txtTimeEnd.Properties.Mask.EditMask = "HH:mm";
            txtTimeEnd.MaskBox.Mask.UseMaskAsDisplayFormat = true;
            load_staff_type();
            FillGridTeacher();

            fun.DateFormat(txtDOB);

            txtSubjectCode.Properties.Items.Clear();
            allClass.Clear();
            allClass = fun.GetAllSubjects();//fun.GetAllSubjectCode();
            foreach (var allclass in allClass)
                txtSubjectCode.Properties.Items.Add(allclass.Name);
            txtSubjectCode.Properties.Items.Add("N/A");
        }
        int SmsAttendent = 0;
        void checksms()
        {
            if (txtSmsAttendent.Checked == true)
                SmsAttendent = 1;
        }
        private void btnTAdd_Click(object sender, EventArgs e)
        {
            if (txtName.Text == "" || txtPhone.Text == "" || txtSalary.Text == "")
            {
                MessageBox.Show("Select all fields with *", "Info");
                return;
            }
            var ts = Convert.ToDateTime(txtTimeStart.Text).ToShortTimeString().Replace("AM", ":00").Replace("PM", ":00").Replace(" ", "");
            var te = Convert.ToDateTime(txtTimeEnd.Text).ToShortTimeString().Replace("AM", ":00").Replace("PM", ":00").Replace(" ", "");
            if (string.IsNullOrEmpty(txtJoiningDate.Text))
                txtJoiningDate.Text = DateTime.Now.ToShortDateString();
            MySqlConnection con = new MySqlConnection(Login.constring);
            checksms();
            con.Open();
            try
            {
                MySqlCommand cmd = new MySqlCommand("INSERT into teacher " +
                    "(name,birthday,sex,address,phone,JoiningDate,attendent_sms,salary,phone2,sync,FName,CNIC,timeStart,timeEnd,designation,staff_type," +
                    "subject_code,bank_account,executed_date,password) VALUES " +
                    "('" + txtName.Text + "','" + txtDOB.Text + "','" + txtGender.Text + "','" + txtAddress.Text + "','" + txtPhone.Text + "'," +
                    "'" + Convert.ToDateTime(txtJoiningDate.Text).ToString("yyyy-MM-dd") + "','" + SmsAttendent + "','" + txtSalary.Text + "'," +
                    "'" + txtCellNo.Text + "','0','" + txtFatherName.Text + "','" + txtCNICNo.Text + "','" + ts + "','" + te + "','" + txtDesignation.Text + "'," +
                    "'" + txtStaffType.Text + "','" + txtSubjectCode.Text + "','" + txtAccountN.Text + "','" + DateTime.Now.ToString("yyyy-MM-dd") + "','" + txt_password.Text + "');", con);
                cmd.ExecuteNonQuery();
            }
            catch (MySqlException ex)
            {
                SystemSounds.Hand.Play();
                MessageBox.Show(ex.Message, "Error");
                return;
            }
            con.Close();
            FillGridTeacher();
            if (pictureEdit1.Image != null)
                saveImage(fun.GetTeacherID(txtName.Text));
            empty();
        }
        private void empty()
        {
            txtName.Text = "";
            txtDOB.Text = "";
            txtGender.Text = "";
            txtJoiningDate.Text = "";
            txtAddress.Text = "";
            txtPhone.Text = "";
            txtSalary.Text = "";
            txtCellNo.Text = "";
            layoutControl1.Visible = false;
            btnNew.Visible = true;
        }
        //  ObservableCollection<AllValues> allValue;

        private void FillGridTeacher()
        {
            string query = "SELECT teacher_id as ID,name as Name,FName as FatherName,CNIC,sex as Gender,(CASE `birthday` WHEN '0000-00-00' THEN '0001-01-01' ELSE `birthday` END) as DOB,phone as Teacher_Cell," +
                " (CASE `JoiningDate` WHEN '0000-00-00' THEN '0001-01-01' ELSE `JoiningDate` END) as JoiningDate, " +
                            " attendent_sms as Attendent_SMS,phone2 as Guardian_Cell,salary as Salary,address as Address,timeStart as `Starting Time` ,timeEnd as `Ending Time`, " +
                            " designation as Designation,staff_type as Staff_Type,subject_code as Subject_Code,bank_account as Bank_Account ,passout,EndingDate,password " +
                            " from teacher where passout=0";
            DataTable table = fun.FetchDataTable(query);

            gridTeacher.DataSource = table;
            gridView1.BestFitColumns();
            var col = gridView1.Columns["ID"];
            col.OptionsColumn.ReadOnly = true;
            var col0 = gridView1.Columns["DOB"];
            RepositoryItemComboBox riCombo = new RepositoryItemComboBox();
            riCombo.Items.Add("Male");
            riCombo.Items.Add("Female");
            gridView1.Columns["Gender"].ColumnEdit = riCombo;
            RepositoryItemComboBox riComboST = new RepositoryItemComboBox();
            foreach (DataRow dr in staff_type_dt.Rows)
                riComboST.Items.Add(dr["type_name"]);
            gridView1.Columns["Staff_Type"].ColumnEdit = riComboST;
            RepositoryItemDateEdit ridate = new RepositoryItemDateEdit();
            ridate.DisplayFormat.FormatString = "M/d/yyyy";
            ridate.DisplayFormat.FormatType = FormatType.DateTime;
            ridate.EditFormat.FormatString = "M/d/yyyy";
            ridate.EditFormat.FormatType = FormatType.DateTime;
            ridate.Mask.EditMask = "M/d/yyyy";
            gridView1.Columns["DOB"].ColumnEdit = ridate;
            RepositoryItemDateEdit ridateS = new RepositoryItemDateEdit();
            ridateS.DisplayFormat.FormatString = "M/d/yyyy";
            ridateS.DisplayFormat.FormatType = FormatType.DateTime;
            ridateS.EditFormat.FormatString = "M/d/yyyy";
            ridateS.EditFormat.FormatType = FormatType.DateTime;
            ridateS.Mask.EditMask = "M/d/yyyy";
            gridView1.Columns["JoiningDate"].ColumnEdit = ridateS;
            RepositoryItemDateEdit ridateE = new RepositoryItemDateEdit();
            ridateE.DisplayFormat.FormatString = "M/d/yyyy";
            ridateE.DisplayFormat.FormatType = FormatType.DateTime;
            ridateE.EditFormat.FormatString = "M/d/yyyy";
            ridateE.EditFormat.FormatType = FormatType.DateTime;
            ridateE.Mask.EditMask = "M/d/yyyy";
            gridView1.Columns["EndingDate"].ColumnEdit = ridateE;
            RepositoryItemComboBox riComboSC = new RepositoryItemComboBox();
            allClass.Clear();
            allClass = fun.GetAllSubjects();
            foreach (var allclass in allClass)
                riComboSC.Items.Add(allclass.Name);
            riComboSC.Items.Add("N/A");
            gridView1.Columns["Subject_Code"].ColumnEdit = riComboSC;


        }

        private void BtnTDelete_Click(object sender, EventArgs e)
        {
            try
            {
                DataRow row = gridView1.GetDataRow(gridView1.FocusedRowHandle);
                MySqlConnection con = new MySqlConnection(Login.constring);
                con.Open();
                MySqlCommand cmdM = new MySqlCommand("DELETE from teacher WHERE teacher_id='" + row[0] + "';", con);
                cmdM.ExecuteNonQuery();
                con.Close();
                FillGridTeacher();
            }
            catch (Exception ex)
            {
                SystemSounds.Hand.Play();
                MessageBox.Show(ex.Message, "Info");
            }
        }

        private void gridView1_RowUpdated(object sender, DevExpress.XtraGrid.Views.Base.RowObjectEventArgs e)
        {
            try
            {
                SmsAttendent = 0;
                DataRow row = gridView1.GetDataRow(gridView1.FocusedRowHandle);
                SmsAttendent = Convert.ToBoolean(row["Attendent_SMS"]) == true ? 1 : 0;

                string date = row["DOB"].ToString();
                string jdate = row["JoiningDate"].ToString();
                var edate = row["EndingDate"].ToString() != "" ? row["EndingDate"].ToString() : "";
                var passout = Convert.ToBoolean(row["passout"]) == true ? 1 : 0;
                string query = "UPDATE teacher set name='" + row["Name"] + "',FName='" + row["FatherName"] + "',CNIC='" + row["CNIC"] + "',birthday='" + date + "',sex='" + row["Gender"] + "',Address='" + row["Address"] + "',phone='" + row["Teacher_Cell"] + "' ,JoiningDate='" + jdate + "',attendent_sms='" + SmsAttendent + "',Salary='" + row["Salary"] + "',phone2='" + row["Guardian_Cell"] + "',sync='0',timeStart='" + row["Starting Time"] + "',timeEnd='" + row["Ending Time"] + "',designation='" + row["Designation"] + "',staff_type='" + row["Staff_Type"] + "' ,subject_code='" + row["Subject_Code"] + "',bank_account='" + row["Bank_Account"] + "'" +
                    ",passout='" + passout + "',EndingDate='" + edate + "',password='" + row["password"] + "' WHERE teacher_id='" + row["ID"] + "';";
                fun.ExecuteQuery(query);
                FillGridTeacher();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Info");
            }
        }
        OpenFileDialog fileOpen = new OpenFileDialog();

        private void btnSelectImage_Click(object sender, EventArgs e)
        {
            fileOpen.Title = "Open Image file";
            fileOpen.Filter = "All Images Files (*.png;*.jpeg;*.gif;*.jpg;*.bmp;*.tiff;*.tif)|*.png;*.jpeg;*.gif;*.jpg;*.bmp;*.tiff;*.tif";

            string appPath = Path.GetDirectoryName(Application.ExecutablePath) + @"\Images\Teachers\"; // <---

            if (Directory.Exists(appPath) == false)                                              // <---
            {                                                                                    // <---
                Directory.CreateDirectory(appPath);                                              // <---
            }
            if (fileOpen.ShowDialog() == DialogResult.OK)
            {
                string name = teach_id + ".Jpg";

                if (File.Exists(appPath + name))
                {
                    pictureEdit1.Image.Dispose();
                    GC.Collect();
                    File.Delete(appPath + name);
                }
                File.Copy(fileOpen.FileName, appPath + name, true);
                pictureEdit1.Image = Image.FromFile(fileOpen.FileName);

            }
        }

        void saveImage(int name)
        {
            string appPath = Path.GetDirectoryName(Application.ExecutablePath) + @"\Images\Teachers\"; // <---

            if (Directory.Exists(appPath) == false)                                              // <---
            {                                                                                    // <---
                Directory.CreateDirectory(appPath);                                              // <---
            }
            // if (fileOpen.ShowDialog() == DialogResult.OK)
            string iName = fileOpen.SafeFileName;   // <---
            if (iName != "")
            {
                try
                {
                    var a = name.ToString() + ".Jpg";
                    if (File.Exists(appPath + name))
                    {
                        pictureEdit1.Image.Dispose();
                        GC.Collect();
                        File.Delete(appPath + name);
                    }

                    string filepath = fileOpen.FileName;    // <---
                    pictureEdit1.Image.Dispose();
                    File.Copy(filepath, appPath + a, true);
                    pictureEdit1.Image = Image.FromFile(appPath + a);
                }
                catch (Exception exp)
                {
                    MessageBox.Show("Unable to open file " + exp.Message);
                }
            }
            fileOpen.Dispose();
        }

        private void simpleButton2_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveFileDialog1 = new SaveFileDialog();
            saveFileDialog1.Filter = "Excel Files (*.xlsx)| *.xlsx";
            saveFileDialog1.Title = "Save an ExcelFile";
            saveFileDialog1.ShowDialog();
            if (saveFileDialog1.FileName != "")
            {
                System.IO.FileStream fs =
                    (System.IO.FileStream)saveFileDialog1.OpenFile();
                gridTeacher.ExportToXlsx(fs);
                fs.Close();
            }
        }

        private void btnPrint_Click(object sender, EventArgs e)
        {
            PreviewPrintableComponent(gridTeacher, gridTeacher.LookAndFeel);
        }

        void PreviewPrintableComponent(IPrintable component, UserLookAndFeel lookAndFeel)
        {
            PrintableComponentLink link = new PrintableComponentLink()
            {
                PrintingSystemBase = new PrintingSystemBase(),
                Component = component,
                Landscape = false,
                PaperKind = PaperKind.A4,
                Margins = new Margins(20, 20, 20, 20)
            };
            link.CreateReportHeaderArea += link_CreateReportHeaderArea;
            link.ShowRibbonPreview(lookAndFeel);
        }

        private void link_CreateReportHeaderArea(object sender, CreateAreaEventArgs e)
        {
            Image logo = fun.Base64ToImage(Login.Logo);
            var school = fun.GetSettings("system_title");

            string reportHeader = school + "\n\r" + "Teachers List";
            e.Graph.StringFormat = new BrickStringFormat(StringAlignment.Near);
            e.Graph.Font = new Font("Tahoma", 12, FontStyle.Regular);
            RectangleF rec = new RectangleF(60, 5, e.Graph.ClientPageSize.Width, 50);
            e.Graph.DrawString(reportHeader, Color.Black, rec, BorderSide.None);
            RectangleF recI = new RectangleF(5, 5, 50, 50);
            e.Graph.DrawImage(logo, recI);
        }

        private void txtTimeEnd_EditValueChanged(object sender, EventArgs e)
        {

        }

        private void btnNew_Click(object sender, EventArgs e)
        {
            layoutControl1.Visible = true;
            btnNew.Visible = false;
            btnTAdd.Enabled = true;
            btnTUpdate.Enabled = false;

        }
        DataRow edit_dr;
        int teach_id = 0;
        private void btnTedit_Click(object sender, EventArgs e)
        {
            layoutControl1.Visible = true;
            btnNew.Visible = false;
            btnTAdd.Enabled = false;
            btnTUpdate.Enabled = true;

            edit_dr = gridView1.GetFocusedDataRow();
            string appPath = Path.GetDirectoryName(Application.ExecutablePath) + @"\Images\Teachers\";
            Image teacher_img = null;
            if (File.Exists(appPath + edit_dr["ID"].ToString() + ".Jpg"))
                teacher_img = Image.FromFile(appPath + edit_dr["ID"].ToString() + ".Jpg");
            teach_id = Convert.ToInt32(edit_dr["ID"].ToString());
            pictureEdit1.Image = teacher_img;
            txtName.Text = edit_dr["name"].ToString();
            txtDOB.Text = edit_dr["DOB"].ToString();
            txtGender.Text = edit_dr["Gender"].ToString();
            txtAddress.Text = edit_dr["address"].ToString();
            txtPhone.Text = edit_dr["Teacher_Cell"].ToString();
            txtJoiningDate.Text = edit_dr["JoiningDate"].ToString();
            txtSalary.Text = edit_dr["salary"].ToString();
            txtCellNo.Text = edit_dr["Guardian_Cell"].ToString();
            txtFatherName.Text = edit_dr["FatherName"].ToString();
            txtCNICNo.Text = edit_dr["CNIC"].ToString();
            txtDesignation.Text = edit_dr["designation"].ToString();
            txtStaffType.Text = edit_dr["staff_type"].ToString();
            txtSubjectCode.Text = edit_dr["subject_code"].ToString();
            txtAccountN.Text = edit_dr["bank_account"].ToString();
            txt_password.Text = edit_dr["password"].ToString();
            txtTimeStart.EditValue = edit_dr["Starting Time"].ToString();
            txtTimeEnd.EditValue = edit_dr["Ending Time"].ToString();


        }
        ObservableCollection<AllClass> allClass = new ObservableCollection<AllClass>();

        private void ManageTeacher_Enter(object sender, EventArgs e)
        {
        }

        private void btnTUpdate_Click(object sender, EventArgs e)
        {
            if (txtName.Text == "" || txtPhone.Text == "" || txtSalary.Text == "" || txtTimeStart.Text == "" || txtTimeEnd.Text == "")
            {
                MessageBox.Show("Select all fields with *", "Info");
                return;
            }
            var ts = Convert.ToDateTime(txtTimeStart.Text).ToShortTimeString().Replace("AM", ":00").Replace("PM", ":00").Replace(" ", "");
            var te = Convert.ToDateTime(txtTimeEnd.Text).ToShortTimeString().Replace("AM", ":00").Replace("PM", ":00").Replace(" ", "");
            if (txtJoiningDate.Text == "")
                txtJoiningDate.Text = DateTime.Now.ToShortDateString();
            MySqlConnection con = new MySqlConnection(Login.constring);
            checksms();
            con.Open();
            try
            {
                MySqlCommand cmd = new MySqlCommand("update teacher set " +
                    " name = '" + txtName.Text + "',birthday = '" + txtDOB.Text + "',sex = '" + txtGender.Text + "',address='" + txtAddress.Text + "',phone='" + txtPhone.Text + "'," +
                    "JoiningDate='" + Convert.ToDateTime(txtJoiningDate.Text).ToString("yyyy-MM-dd") + "',salary = '" + txtSalary.Text + "',phone2='" + txtCellNo.Text + "'," +
                    "FName = '" + txtFatherName.Text + "',CNIC = '" + txtCNICNo.Text + "',timeStart='" + ts + "',timeEnd='" + te + "',designation='" + txtDesignation.Text + "'," +
                    "staff_type='" + txtStaffType.Text + "',subject_code='" + txtSubjectCode.Text + "',bank_account='" + txtAccountN.Text + "',password ='" + txt_password.Text + "'" +
                    "where teacher_id = '" + edit_dr["ID"] + "';", con);
                cmd.ExecuteNonQuery();
            }
            catch (MySqlException ex)
            {
                SystemSounds.Hand.Play();
                MessageBox.Show(ex.Message, "Error");
                return;
            }
            con.Close();
            FillGridTeacher();
            if (pictureEdit1.Image != null)
                saveImage(fun.GetTeacherID(txtName.Text));
            empty();
        }

        private void btn_hide_panel_Click(object sender, EventArgs e)
        {
            empty();
        }
        DataTable staff_type_dt;
        void load_staff_type()
        {
            string query = "select * from `staff_type`";
            staff_type_dt = fun.FetchDataTable(query);
            txtStaffType.Properties.Items.Clear();
            foreach (DataRow dr in staff_type_dt.Rows)
                txtStaffType.Properties.Items.Add(dr["type_name"]);
        }

        private void btn_staff_category_Click(object sender, EventArgs e)
        {
            using (staff_types_form de = new staff_types_form())
            {
                if (de.ShowDialog() == DialogResult.Yes) { }
                else
                    load_staff_type();
            }
        }
    }
}
