using DevExpress.XtraEditors;
using DevExpress.XtraLayout;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Media;
using System.Windows.Forms;

namespace SchoolManagementSystem.Students
{
    public partial class NewStudent : DevExpress.XtraEditors.XtraForm
    {
        CommonFunctions fun = new CommonFunctions();
        ObservableCollection<AllClass> allClass = new ObservableCollection<AllClass>();
        ObservableCollection<AllValues> allSchool;// = new ObservableCollection<StudentInfo>();
        Dictionary<int, TextEdit> txtedit_array = new Dictionary<int, TextEdit>();
        public long inserted_student_id { get; set; }
        public NewStudent()
        {
            InitializeComponent();
            fun.selectall_Controls(this);
            txt_religion.SelectedIndex = 0;
            txtClass.Properties.DataSource = fun.GetAllClasses_dt();
            txtClass.Properties.DisplayMember = "name";
            txtClass.Properties.ValueMember = "class_id";
            loadVisitor();
            loadSchool();
            loadHostels();
            loadTra_Stops();
            //add extra items 
            String query = "SELECT field_id,title FROM student_fields ORDER BY title DESC ";
            DataTable table = fun.FetchDataTable(query);
            TextEdit textEdit1;
            LayoutControlItem item1;
            int field_id;
            foreach (DataRow row in table.Rows)
            {
                item1 = layoutControlGroupAO.AddItem();
                textEdit1 = new TextEdit();
                textEdit1.Name = "txt_" + row["field_id"].ToString();
                field_id = Convert.ToInt32(row["field_id"]);
                txtedit_array[field_id] = textEdit1;
                item1.Control = textEdit1;
                item1.Text = row["title"].ToString();
            }
        }
        public void loadTra_Stops()
        {
            string query = "select id,name from transport_stops";
            DataTable stops = fun.FetchDataTable(query);
            gridtransport_stops.Properties.DataSource = stops;
            gridtransport_stops.Properties.DisplayMember = "name";
            gridtransport_stops.Properties.ValueMember = "id";
        }
        public void loadHostels()
        {
            string query = "SELECT hr.ID,hos.HostelName as Hostel,hr.room as Room,(Capacity -(select count(*) from hostels_student where room_id = hr.ID and LeftDate is null )) as Capacity FROM hostel_rooms as hr " +
                 " join hostels as hos on hos.ID = hr.hostel_id" +
                 " where (Capacity -(select count(*) from hostels_student where room_id = hr.ID and LeftDate is null )) > 0";
            ddllistHostel.Properties.DataSource = fun.FetchDataTable(query);
            ddllistHostel.Properties.DisplayMember = "Room";
            ddllistHostel.Properties.ValueMember = "ID";
        }

        private void btnFind_Click(object sender, EventArgs e)
        {
            try
            {
                MySqlConnection con = new MySqlConnection(Login.constring);
                con.Open();
                MySqlCommand cmd1 = new MySqlCommand("SELECT * FROM visitor where VID='" + txtVisitingID.EditValue + "'", con);
                MySqlDataReader reader1 = cmd1.ExecuteReader();
                if (reader1.HasRows)
                {
                    reader1.Read();
                    txtName.Text = reader1["Name"].ToString();
                    txtParent.Text = reader1["FName"].ToString();
                    txtAddress.Text = reader1["Address"].ToString();
                    txtPhone.Text = reader1["Cell"].ToString();
                    txtClass.EditValue = int.Parse(reader1["ClassId"].ToString());
                    txtGender.Text = reader1["sex"].ToString();
                    txtDOB.Text = string.IsNullOrEmpty(reader1["birthday"].ToString())?"":Convert.ToDateTime(txtDOB.Text).ToString("yyyy-MM-dd");
                    txtPSchool.EditValue = reader1["School"].ToString();
                    if (!string.IsNullOrEmpty(reader1["image"].ToString()))
                    {
                        string image = Path.GetDirectoryName(Application.ExecutablePath).ToString() + @"\Images\Students\" + reader1["image"].ToString();
                        picBoxStudent.Image = Image.FromFile(image);
                    }
                    //Select extra fields
                    TextEdit txtedit;
                    String query = "SELECT field_id,`value` FROM visiter_fields_values WHERE student_id = '" + txtVisitingID.EditValue + "'";
                    DataTable table = fun.FetchDataTable(query);
                    foreach (DataRow row in table.Rows)
                    {
                        try
                        {
                            txtedit = txtedit_array[Convert.ToInt32(row["field_id"])];
                            txtedit.Text = row["value"].ToString();
                        }
                        catch (Exception ee) { }
                    }


                }
                con.Close();
            }
            catch (Exception ep)
            {
            }
        }

        private void txtParent_SelectedValueChanged(object sender, EventArgs e)
        {
            try
            {
                if (txtParent.Text != "")
                {
                    var a = fun.GetParentInfo(txtParent.Text);
                    if (a != "")
                    {
                        var info = a.Split('>');
                        txtPhone.Text = info[1];
                        txtAddress.Text = info[2];
                        //   txtProfession.Text = info[3];
                    }
                }
                else
                {
                    txtPhone.Text = "";
                    txtAddress.Text = "";
                    //  txtProfession.Text = "";
                }
                if (txtParent.Text != txtParent.SelectedText)
                {
                    txtPhone.Text = "";
                    txtAddress.Text = "";
                    //  txtProfession.Text = "";
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("{0} Exception caught.", ex.Message);

            }
        }

        private void checkAdvanceOpation_CheckedChanged(object sender, EventArgs e)
        {
            if (checkAdvanceOpation.Checked)
            {
                panel_R_C_A.Visible = true;
                groupAdv.Visible = true;
                //layoutControlGroupAO.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
                //layoutControl1.Height = 295;
            }
            else
            {
                groupAdv.Visible = false;
                //layoutControlGroupAO.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
                if (!checkRoles.Checked && !checkFeeConcession.Checked && !checkAdvanceOpation.Checked)
                    panel_R_C_A.Visible = false;
            }
        }

        private void checkFeeConcession_CheckedChanged(object sender, EventArgs e)
        {
            if (checkFeeConcession.Checked)
            {
                panel_R_C_A.Visible = true;
                panelConcession.Visible = true;
                //layoutControlGroupFO.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
                //layoutControl1.Height = 295;
            }
            else
            {
                panelConcession.Visible = false;
                //layoutControlGroupFO.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
                if (!checkRoles.Checked && !checkFeeConcession.Checked && !checkAdvanceOpation.Checked)
                    panel_R_C_A.Visible = false;
            }
        }

        int[] cat_ids;
        String[] cat_titles;
        private void loadVisitor()
        {
            //extra collumns
            /*string query_cat = "SELECT field_id,title FROM student_fields ORDER BY title ASC  ";
            DataTable table_cat = fun.GetQueryTable(query_cat);
            cat_ids = new int[table_cat.Rows.Count];
            cat_titles = new String[table_cat.Rows.Count];
            int count = 0;
            int fee_cat_id;
            String extra_col = "";
            foreach (DataRow row in table_cat.Rows)
            {
                fee_cat_id = Convert.ToInt32(row["field_id"].ToString());
                cat_ids[count] = fee_cat_id;
                cat_titles[count] = row["title"].ToString();
                extra_col += ",(SELECT IFNULL(`value`,'') FROM visiter_fields_values WHERE field_id = '" + fee_cat_id + "' AND student_id = visitor.VID) AS '" + row["title"].ToString() + "'";
                count++;
            }*/

            String sql = "SELECT visitor.Date,VID,visitor.Name AS `Candidate`,CNIC, FName AS `Father`,FatherOcc AS `Occupation`  ,School,ClassId AS Class,Prospectus,Payment, " +
                        " Status,Cell ,visitor.Address,Reference,Remarks,Sms AS SMS,birthday, sex AS Gender " + //extra_col +
                        " FROM visitor WHERE status = 1 " +
                        " ORDER BY VID DESC ";

            DataTable table = fun.FetchDataTable(sql);
            txtVisitingID.Properties.DataSource = table;
            txtVisitingID.Properties.DisplayMember = "Candidate";
            txtVisitingID.Properties.ValueMember = "VID";
        }
        private void loadSchool()
        {
            DataTable table = fun.FetchDataTable("SELECT school_id AS id,name as title FROM tbl_school ORDER BY `name` ASC");
            txtPSchool.Properties.DataSource = table;
            txtPSchool.Properties.DisplayMember = "title";
            txtPSchool.Properties.ValueMember = "id";

        }
        private void checkRoles_CheckedChanged(object sender, EventArgs e)
        {
            if (checkRoles.Checked)
            {
                panel_R_C_A.Visible = true;
                groupRoles.Visible = true;
                //layoutControlGroupR.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
                //layoutControl1.Height = 295;
            }
            else
            {
                groupRoles.Visible = false;
                //layoutControlGroupR.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
                if (!checkRoles.Checked && !checkFeeConcession.Checked && !checkAdvanceOpation.Checked)
                    panel_R_C_A.Visible = false;
            }
        }

        private void BtnTakeImage_Click(object sender, EventArgs e)
        {
            TakeImage t = new TakeImage();

            t.ShowDialog();
            picBoxStudent.Image = t.imgCapture.Image;
        }

        private void btnAddStudent_Click(object sender, EventArgs e)
        {
            btnAddStudent.Enabled = false;
            if (txtName.Text == "" || txtParent.Name == "" || txtPhone.Text == "" || txtClass.EditValue == null || txtSection.EditValue == null || txtGender.EditValue == null)
            {
                MessageBox.Show("Select all fields with *", "Info");
                btnAddStudent.Enabled = true;
                return;
            }
            if (txtPhone.Text.Length != 11 || txtPhone.Text.Substring(0, 1) != "0")
            {
                MessageBox.Show("Cell number incorrect *", "Info");
                btnAddStudent.Enabled = true;
                //txtPhone.Text = "";
                return;
            }
            String roll = txtRoll.Text == "" ? "0" : txtRoll.Text;
            String school_id= txtPSchool.EditValue == null?"0":txtPSchool.EditValue.ToString();
            MySqlConnection con = new MySqlConnection(Login.constring);
            con.Open();
            checksms();
            String query;
            long parent_id = 0;
            try
            {
                query = "select * from parent where phone = '" + txtPhone.Text + "';";
                DataTable dt = fun.FetchDataTable(query);
                if (dt.Rows.Count <= 0)
                {
                    query = "INSERT into parent(name,email,phone,address,profession,sync) " +
                        " VALUES('" + txtParent.Text + "','" + txtEmail.Text + "','" + txtPhone.Text + "','" + txtAddress.Text + "','" + txt_profession.Text + "','0');";
                    parent_id = fun.Execute_Insert(query);
                }
                else
                {
                    query = "UPDATE `parent` SET `name` = '" + txtParent.Text + "', `email` = '" + txtEmail.Text + "'," +
                        " `phone` = '" + txtPhone.Text + "',`address` = '" + txtAddress.Text + "',`profession` = '" + txt_profession.Text + "' WHERE `parent_id` = '"+dt.Rows[0]["parent_id"].ToString() +"';";
                    fun.Execute_Insert(query);
                    parent_id = Convert.ToInt64(dt.Rows[0]["parent_id"]==null?0: dt.Rows[0]["parent_id"]);
                }
                int classID = Convert.ToInt32(txtClass.EditValue);
                int free_std = chb_free_std.Checked ? 1 : 0;
                string dob = string.IsNullOrEmpty(txtDOB.Text) ? "0001-01-01" : Convert.ToDateTime(txtDOB.Text).ToString("yyyy-MM-dd");
                query = "INSERT into student(registration_no,religion,free_student,free_std_remakrs,name,birthday,sex,address,phone,class_id,section_id,parent_id,roll,attendent_sms,exam_sms,fees_sms,enotice_sms,fee_concession,addmission_concession,p_school,security_concession,annual_concession,exam_concession) " +
                    " VALUES('"+txt_registration.Text+"','"+txt_religion.Text+"','"+ free_std + "','"+txt_free_std_remarks.Text+"','" + txtName.Text + "','" + dob + "','" + txtGender.Text + "','" + txtAddress.Text + "','" + txtPhone.Text + "','" + classID + "','" + txtSection.EditValue.ToString() + "','" + parent_id + "','" + roll + "','" + smsAttendant + "','" + smsexam + "','" + smsfee + "','" + smsenotice + "','" + Convert.ToInt32(txtfeeC.Text) + "','" + Convert.ToInt32(txtAdmission.Text) + "','" + school_id + "','" + txtSecurityC.Text + "','" + txtAnnualC.Text + "','" + txtExamC.Text + "');";
                long student_id = fun.Execute_Insert(query);

                inserted_student_id = student_id;
                string image_name = student_id.ToString() + "_std";
                string imagepath = image_name + ".Jpeg";
                //create student ladger
                query = "INSERT INTO `ac_ledger` (`head_id`,`title`,`balance`,`status`,`mobile`,`email`,`address`,`photo`,is_auto_genrated) VALUES " +
                    " ('1','" + txtName.Text + "','" + txt_open_balance.Text + "',1,'" + txtPhone.Text + "','" + txtEmail.Text + "','" + txtAddress.Text + "','"+imagepath+"','1');";
                long ladger_id = fun.Execute_Insert(query);
                //assigning ladger to student
                query = "update student set ladger_id = '"+ladger_id+"' where student_id = '"+student_id+"'";
                fun.ExecuteQuery(query);
                if (ddllistHostel.EditValue != null && ddllistHostel.EditValue == null)
                {
                    query = "INSERT INTO `hostels_student`(`room_id`, `student_id`, `Rent`,JoinDate) VALUES ('" + ddllistHostel.EditValue + "','" + student_id + "','" + txtRent.Text + "','" + fun.time(DateTime.Now) + "')";
                    int hs_id = fun.ExecuteInsert(query);
                }
                if (!string.IsNullOrEmpty(gridtransport_stops.EditValue.ToString()))
                {
                    query = "INSERT INTO `transport_student`(`student_id`, `stop_id`, `charges`, `Date`) VALUES('"+student_id+"','"+ gridtransport_stops.EditValue + "','"+txtTransport_charges.Text+ "','" + fun.CurrentDate() + "')";
                    int ts_id = fun.ExecuteInsert(query);
                    query = "INSERT INTO `transport_fee`(`ts_id`,`due`, `creation_date`) VALUES ('" + ts_id + "','" + txtTransport_charges.Text + "','" + fun.CurrentDate() + "')";
                    fun.Execute_Query(query);
                }
                //inert extra fields 
                TextEdit txtedit;

                foreach (int field_id in txtedit_array.Keys)
                {
                    txtedit = txtedit_array[field_id];
                    query = "INSERT INTO student_fields_values SET student_id = '{0}',field_id = '{1}',`value` = '{2}' ";
                    query = String.Format(query, student_id, field_id, txtedit.Text.ToString());
                    fun.ExecuteQuery(query);

                }
                empty();
                if (picBoxStudent.Image != null)
                {
                    fun.saveImage(image_name, fileOpen, picBoxStudent, @"\Images\Students\");
                    //Path.GetDirectoryName(Application.ExecutablePath).ToString() + @"\Images\Students\";
                    
                    query = "UPDATE `student` SET `image` = '"+ imagepath + "' WHERE `student_id` = '"+ student_id + "';";
                    fun.ExecuteQuery(query);
                }
                    

                addInInvoice(student_id);
                picBoxStudent.Image = null;
                btnAddStudent.Enabled = true;
                this.Close();
                //MessageBox.Show("Student Added Successfully","Student Added",MessageBoxButtons.OK,MessageBoxIcon.Information);
            }
            catch (MySqlException ex)
            {
                SystemSounds.Hand.Play();
                MessageBox.Show(ex.Message, "Error");
                return;
            }
            con.Close();

        }
        void addInInvoice(long student_id)
        {
            String query = "INSERT INTO fees_category_student(fees_cat_id,fees_val,student_id)  " +
                            " SELECT fee_cat_id, default_val,'" + student_id + "' AS student_id FROM fees_category";
            fun.ExecuteQuery(query);
        }
        OpenFileDialog fileOpen = new OpenFileDialog();
        private void BtnLoadImage_Click(object sender, EventArgs e)
        {
            fileOpen.Title = "Open Image file";
            fileOpen.Filter = "All Images Files (*.png;*.jpeg;*.gif;*.jpg;*.bmp;*.tiff;*.tif)|*.png;*.jpeg;*.gif;*.jpg;*.bmp;*.tiff;*.tif";

            if (fileOpen.ShowDialog() == DialogResult.OK)
            {
                picBoxStudent.Image = Image.FromFile(fileOpen.FileName);
            }
        }

        private void load()
        {
        }

        void empty()
        {
            txtName.Text = "";
            txtParent.Text = "";
            txtClass.EditValue = null;
            txtSection.EditValue = null;
            txtRoll.Text = "";
            //  txtPassword.Text = "";
            txtfeeC.Text = "0";
            txtAdmission.Text = "0";
            txtEmail.Text = "";
            txtPhone.Text = "";
            txtAddress.Text = "";
            //  txtProfession.Text = "";
            txtRent.Text = "0";
            ddllistHostel.EditValue = null;

        }
        int smsAttendant = 0;
        int smsfee = 0;
        int smsexam = 0;
        int smsenotice = 0;
        void checksms()
        {
            if (txtAttendant.Checked == true)
                smsAttendant = 1;
            if (txtfee.Checked == true)
                smsfee = 1;
            if (txtexam.Checked == true)
                smsexam = 1;
            if (txtenotice.Checked == true)
                smsenotice = 1;
        }

        private void NewStudent_FormClosed(object sender, FormClosedEventArgs e)
        {
            
        }

        private void NewStudent_Leave(object sender, EventArgs e)
        {
            
        }

        private void txtClass_EditValueChanged(object sender, EventArgs e)
        {
            if (txtClass.EditValue != null)
            {
                txtSection.Properties.DataSource = fun.GetAllSection_dt(txtClass.EditValue.ToString());
                txtSection.Properties.DisplayMember = "name";
                txtSection.Properties.ValueMember = "section_id";
            }
        }

        private void chb_free_std_CheckedChanged(object sender, EventArgs e)
        {
            if (chb_free_std.Checked)
                txt_free_std_remarks.Visible = true;
            else
                txt_free_std_remarks.Visible = false;
        }

        private void txtPhone_Leave(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtPhone.Text))
            {
                string query = "select * from parent where phone = '"+ txtPhone.Text + "';";
                DataTable dt = fun.FetchDataTable(query);
                if(dt.Rows.Count > 0)
                {
                    txtParent.Text = dt.Rows[0]["name"].ToString();
                    txtEmail.Text = dt.Rows[0]["email"].ToString();
                    txt_profession.Text = dt.Rows[0]["profession"].ToString();
                    txtAddress.Text = dt.Rows[0]["address"].ToString();
                }
            }
        }
    }
}
