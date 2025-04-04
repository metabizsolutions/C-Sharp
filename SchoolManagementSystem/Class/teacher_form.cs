using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using SchoolManagementSystem.Students;

namespace SchoolManagementSystem.Class
{
    public partial class teacher_form : Form
    {
        private CommonFunctions fun = new CommonFunctions();
        private long ID = 0;
        public teacher_form(int id)
        {
            InitializeComponent();
            fun.selectall_Controls(this);
            txt_religion.SelectedIndex = 0;
            ID = id;
            load_staff_type();

            txtSubjectCode.Properties.Items.Clear();
            DataTable sub_dt = fun.GetAllSubjects_dt();//fun.GetAllSubjectCode();
            foreach (DataRow dr in sub_dt.Rows)
            {
                txtSubjectCode.Properties.Items.Add(dr["Subject"]);
            }

            txtSubjectCode.Properties.Items.Add("N/A");

            string query = "SELECT `permission_id` as role_id,`title` FROM `tbl_permission`;";
            ddl_role.Properties.DataSource = fun.FetchDataTable(query);
            ddl_role.Properties.DisplayMember = "title";
            ddl_role.Properties.ValueMember = "role_id";
            if (ID > 0)
            {
                edit_teacher(ID);
            }

            query = "select * from teacher_education where teacher_id = '" + id + "'";
            DataTable edu_dt = fun.FetchDataTable(query);
            gc_education.DataSource = edu_dt;
            query = "select * from teacher_experience where teacher_id = '" + id + "'";
            DataTable exp_dt = fun.FetchDataTable(query);
            gc_work.DataSource = exp_dt;
        }

        private long ladger_id = 0;

        private void edit_teacher(long id)
        {
            string query = "select name,FName,birthday,sex,address,phone,JoiningDate,attendent_sms,salary,phone2,CNIC,timeStart,timeEnd,designation,staff_type," +
                    " subject_code,bank_account,password,religion,email,lecture_limit,role_id,lecture_rate,ifnull(ladger_id,0) as ladger_id from teacher where teacher_id = '" + id + "'";
            DataTable dt = fun.FetchDataTable(query);
            var joiningdate = string.IsNullOrEmpty(dt.Rows[0]["JoiningDate"].ToString()) ? DateTime.Now : Convert.ToDateTime(dt.Rows[0]["JoiningDate"]);
            ladger_id = Convert.ToInt64(dt.Rows[0]["ladger_id"]);
            txtname.Text = dt.Rows[0]["name"].ToString();
            txtFatherName.Text = dt.Rows[0]["FName"].ToString();
            try
            {
                txtDOB.Value = Convert.ToDateTime(string.IsNullOrEmpty(dt.Rows[0]["birthday"].ToString()) ? DateTime.Now.ToString("yyyy-MM-dd") : dt.Rows[0]["birthday"].ToString());
            }
            catch (Exception)
            {

            }
            txtGender.Text = dt.Rows[0]["sex"].ToString();
            txtAddress.Text = dt.Rows[0]["address"].ToString();
            txtCellNo1.Text = dt.Rows[0]["phone"].ToString();
            try
            {
                txtJoiningDate.Value = joiningdate;
            }
            catch (Exception)
            {
                txtJoiningDate.Value = DateTime.Now;
            }

            chb_attendance.Checked = Convert.ToBoolean(dt.Rows[0]["attendent_sms"]);
            txtSalary.Text = dt.Rows[0]["salary"].ToString();
            txt_gaurdian_phone.Text = dt.Rows[0]["phone2"].ToString();
            txtCNICNo.Text = dt.Rows[0]["CNIC"].ToString();
            dtp_job_start.EditValue = string.IsNullOrEmpty(dt.Rows[0]["timeStart"].ToString()) ? DateTime.Now.ToString("hh:mm t") : dt.Rows[0]["timeStart"].ToString();
            dtp_job_end.EditValue = string.IsNullOrEmpty(dt.Rows[0]["timeEnd"].ToString()) ? DateTime.Now.ToString("hh:mm t") : dt.Rows[0]["timeEnd"].ToString();
            txtDesignation.Text = dt.Rows[0]["designation"].ToString();
            txtStaffType.Text = dt.Rows[0]["staff_type"].ToString();
            txtSubjectCode.Text = dt.Rows[0]["subject_code"].ToString();
            txtAccountN.Text = dt.Rows[0]["bank_account"].ToString();
            txt_password.Text = dt.Rows[0]["password"].ToString();
            txt_religion.Text = dt.Rows[0]["religion"].ToString();
            txtEmail.Text = dt.Rows[0]["email"].ToString();
            txt_lac_limit.Text = dt.Rows[0]["lecture_limit"].ToString();
            ddl_role.EditValue = dt.Rows[0]["role_id"];
            txt_lac_rate.Text = dt.Rows[0]["lecture_rate"].ToString();

        }

        private void load_staff_type()
        {
            string query = "select * from `staff_type`";
            DataTable staff_type_dt = fun.FetchDataTable(query);
            txtStaffType.Properties.Items.Clear();
            foreach (DataRow dr in staff_type_dt.Rows)
            {
                txtStaffType.Properties.Items.Add(dr["type_name"]);
            }
        }
        private void btn_delete_edu_Click(object sender, EventArgs e)
        {
            DataRow dr = gridView2.GetFocusedDataRow();
            if (dr != null)
            {
                if (XtraMessageBox.Show($"Are you sure want to delete " + dr["degree_name"] + " ?", "Warnning", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
                {
                    if (dr["edu_id"] != null && Convert.ToInt32(dr["edu_id"]) > 0)
                    {
                        string query = "delete from teacher_education where edu_id = '" + dr["edu_id"] + "'";
                        fun.ExecuteQuery(query);
                    }
                    gridView2.DeleteRow(gridView2.FocusedRowHandle);
                }
            }
        }

        private void btn_delete_exp_Click(object sender, EventArgs e)
        {
            DataRow dr = gridView5.GetFocusedDataRow();
            if (dr != null)
            {
                if (XtraMessageBox.Show($"Are you sure want to delete " + dr["company"] + " ?", "Warnning", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
                {
                    if (dr["exp_id"] != null && Convert.ToInt32(dr["exp_id"]) > 0)
                    {
                        string query = "delete from teacher_experience where exp_id = '" + dr["exp_id"] + "'";
                        fun.ExecuteQuery(query);
                    }
                    gridView2.DeleteRow(gridView2.FocusedRowHandle);

                }
            }
        }

        private void btn_save_Click(object sender, EventArgs e)
        {
            //EndingDate,passout
            //blood_group,`bundle_rate`,`rate_fsc`,`pf_ded`,`executed_date`,`rate_ucp`,`rate_bsc`
            if (string.IsNullOrEmpty(txtname.Text) || string.IsNullOrEmpty(txtFatherName.Text) || string.IsNullOrEmpty(txtCNICNo.Text) || string.IsNullOrEmpty(txtCellNo1.Text) || string.IsNullOrEmpty(txtGender.Text) || string.IsNullOrEmpty(txtStaffType.Text) || string.IsNullOrEmpty(txt_religion.Text) || string.IsNullOrEmpty(txtDesignation.Text) || ddl_role.EditValue == null)
            {
                XtraMessageBox.Show("Please Select All Compulsory fields and try again", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            string query = "";
            string columns = "name='" + txtname.Text + "',FName='" + txtFatherName.Text + "',birthday='" + txtDOB.Text + "',sex='" + txtGender.Text + "'," +
                " address='" + txtAddress.Text + "',phone='" + txtCellNo1.Text + "',JoiningDate='" + txtJoiningDate.Value.ToString("yyyy-MM-dd") + "',attendent_sms='" + (chb_attendance.Checked ? 1 : 0) + "'," +
                " salary='" + txtSalary.Text + "',phone2='" + txt_gaurdian_phone.Text + "',CNIC='" + txtCNICNo.Text + "',timeStart='" + dtp_job_start.Text + "',timeEnd='" + dtp_job_end.Text + "'," +
                "designation='" + txtDesignation.Text + "',staff_type='" + txtStaffType.Text + "'," +
                        "subject_code='" + txtSubjectCode.Text + "',bank_account='" + txtAccountN.Text + "',password='" + txt_password.Text + "',religion='" + txt_religion.Text + "'," +
                        " email='" + txtEmail.Text + "',lecture_limit='" + txt_lac_limit.Text + "',role_id='" + ddl_role.EditValue + "',lecture_rate='" + txt_lac_rate.Text + "'";
            if (ID > 0)
            {
                query = "update teacher set " + columns + " where teacher_id = '" + ID + "'";
                fun.ExecuteQuery(query);

            }
            else
            {
                query = "INSERT into teacher set " + columns + ",executed_date= '" + DateTime.Now.ToString("yyyy-MM-dd") + "',sync=0;";
                ID = fun.Execute_Insert(query);

            }
            if (ladger_id > 0)
            {
                query = "update `ac_ledger` set title ='" + txtname.Text + "',mobile='" + txtCellNo1.Text + "',email='" + txtEmail.Text + "',address='" + txtAddress.Text + "' where id = '" + ladger_id + "'";
                fun.ExecuteQuery(query);
            }
            else
            {
                query = "INSERT INTO `ac_ledger` (`head_id`,`title`,`balance`,`status`,`mobile`,`email`,`address`,`photo`,is_auto_genrated) VALUES " +
                    " ('6','" + txtname.Text + "','0',1,'" + txtCellNo1.Text + "','" + txtEmail.Text + "','" + txtAddress.Text + "','" + ID + ".Jpeg" + "','1');";
                ladger_id = fun.Execute_Insert(query);
            }
            query = "update teacher set ladger_id = '" + ladger_id + "' where teacher_id = '" + ID + "'";
            fun.ExecuteQuery(query);
            DataTable edu_dt = gc_education.DataSource as DataTable;
            if (edu_dt != null)
            {
                foreach (DataRow dr in edu_dt.Rows)
                {
                    string obtain = string.IsNullOrEmpty(dr["obtain"].ToString()) ? "0" : dr["obtain"].ToString();
                    string total = string.IsNullOrEmpty(dr["total"].ToString()) ? "0" : dr["total"].ToString();
                    string percent = string.IsNullOrEmpty(dr["percent"].ToString()) ? "0" : dr["percent"].ToString();
                    columns = " degree_name = '" + dr["degree_name"] + "',board = '" + dr["board"] + "', obtain = '" + obtain + "', total = '" + total + "', percent = '" + percent + "', teacher_id = '" + ID + "'";
                    if (!string.IsNullOrEmpty(dr["edu_id"].ToString()) && Convert.ToInt32(dr["edu_id"]) > 0)
                    {
                        query = "update teacher_education set " + columns + " where edu_id = '" + dr["edu_id"] + "'";
                    }
                    else
                    {
                        query = "INSERT into teacher_education set " + columns + ";";
                    }
                    fun.ExecuteQuery(query);
                }
            }
            DataTable exp_dt = gc_work.DataSource as DataTable;
            if (exp_dt != null)
            {
                foreach (DataRow dr in exp_dt.Rows)
                {
                    var from_date = string.IsNullOrEmpty(dr["from_date"].ToString()) ? "" : Convert.ToDateTime(dr["from_date"]).ToString("MMM-yyyy");
                    var to_date = string.IsNullOrEmpty(dr["to_date"].ToString()) ? "" : Convert.ToDateTime(dr["to_date"]).ToString("MMM-yyyy");
                    columns = "company = '" + dr["company"] + "',com_designation = '" + dr["com_designation"] + "',from_date = '" + from_date + "',to_date = '" + to_date + "',total_exp = '" + dr["total_exp"] + "',teacher_id = '" + ID + "'";
                    if (!string.IsNullOrEmpty(dr["exp_id"].ToString()) && Convert.ToInt32(dr["exp_id"]) > 0)
                    {
                        query = "update teacher_experience set " + columns + " where exp_id = '" + dr["exp_id"] + "'";
                    }
                    else
                    {
                        query = "INSERT into teacher_experience set " + columns + ";";
                    }

                    fun.ExecuteQuery(query);
                }
            }
            if (picBoxStudent.Image != null)
            {
                string name = ID.ToString();
                fun.saveImage(name, fileOpen, picBoxStudent, @"\Images\Teachers\");
            }
            empty();
            if (ID > 0)
            {
                XtraMessageBox.Show("Staff Info Updated Successfully", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                XtraMessageBox.Show("Staff Info Inserted Successfully", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void empty()
        {
            ID = 0;
            txtname.Text = "";
            txtFatherName.Text = "";
            txtDOB.Value = DateTime.Now;
            txtGender.SelectedIndex = 0;
            txtAddress.Text = "";
            txtCellNo1.Text = "";
            txtJoiningDate.Value = DateTime.Now;
            chb_attendance.Checked = true;
            txtSalary.Text = "0";
            txt_gaurdian_phone.Text = "";
            txtCNICNo.Text = "";
            dtp_job_start.EditValue = DateTime.Now.ToString("hh:mm t");
            dtp_job_end.EditValue = DateTime.Now.ToString("hh:mm t");
            txtDesignation.Text = "";
            txtStaffType.EditValue = null;
            txtSubjectCode.EditValue = null;
            txtAccountN.Text = "";
            txt_password.Text = "";
            txt_religion.SelectedIndex = 0;
            txtEmail.Text = "";
            txt_lac_limit.Text = "";
            ddl_role.EditValue = null;
            txt_lac_rate.Text = "";
            gc_education.DataSource = null;
            gc_work.DataSource = null;
            picBoxStudent.Dispose();
            picBoxStudent.Image = null;
        }
        private void btn_staff_type_Click(object sender, EventArgs e)
        {
            using (staff_types_form de = new staff_types_form())
            {
                if (de.ShowDialog() == DialogResult.Yes) { }
                else
                {
                    load_staff_type();
                }
            }
        }

        private void BtnTakeImage_Click(object sender, EventArgs e)
        {
            TakeImage t = new TakeImage();

            t.ShowDialog();
            picBoxStudent.Image = t.imgCapture.Image;
        }

        private OpenFileDialog fileOpen = new OpenFileDialog();
        private void BtnLoadImage_Click(object sender, EventArgs e)
        {
            fileOpen.Title = "Open Image file";
            fileOpen.Filter = "JPEG Files (*.Jpeg)| *.Jpeg";

            if (fileOpen.ShowDialog() == DialogResult.OK)
            {
                picBoxStudent.Image = Image.FromFile(fileOpen.FileName);
            }
        }

        private void gridView2_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            DataRow dr = gridView2.GetFocusedDataRow();
            double total = 0;
            double obtain = 0;
            if (e.Column.FieldName == "obtain" || e.Column.FieldName == "total")
            {
                if (!string.IsNullOrEmpty(dr["total"].ToString()))
                {
                    total = Convert.ToDouble(dr["total"]);
                }
                else
                {
                    total = 0;
                }

                if (!string.IsNullOrEmpty(dr["obtain"].ToString()))
                {
                    obtain = Convert.ToDouble(dr["obtain"]);
                }
                else
                {
                    obtain = 0;
                }

                if (total > 0 && obtain > 0)
                {
                    gridView2.SetRowCellValue(gridView2.FocusedRowHandle, "percent", Math.Round((obtain / total * 100), 2));
                }
                else
                {
                    gridView2.SetRowCellValue(gridView2.FocusedRowHandle, "percent", 0);
                }

                gridView2.UpdateCurrentRow();
            }
        }
        public static int GetMonthDifference(DateTime startDate, DateTime endDate)
        {
            int monthsApart = 12 * (startDate.Year - endDate.Year) + startDate.Month - endDate.Month;
            return Math.Abs(monthsApart);
        }

        private void gridView5_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            DataRow dr = gridView5.GetFocusedDataRow();
            var from_date = "";
            var to_date = "";
            if (e.Column.FieldName == "from_date" || e.Column.FieldName == "to_date")
            {
                if (!string.IsNullOrEmpty(dr["from_date"].ToString()))
                {
                    from_date = Convert.ToDateTime(dr["from_date"]).ToString();
                }

                if (!string.IsNullOrEmpty(dr["to_date"].ToString()))
                {
                    to_date = Convert.ToDateTime(dr["to_date"]).ToString();
                }

                if (!string.IsNullOrEmpty(from_date) && !string.IsNullOrEmpty(to_date))
                {
                    gridView5.SetRowCellValue(gridView5.FocusedRowHandle, "total_exp", GetMonthDifference(Convert.ToDateTime(from_date), Convert.ToDateTime(to_date)).ToString() + " Months");
                }
                else
                {
                    gridView5.SetRowCellValue(gridView5.FocusedRowHandle, "total_exp", 0);
                }

                gridView5.UpdateCurrentRow();
            }
        }

        private void gridView5_InitNewRow(object sender, DevExpress.XtraGrid.Views.Grid.InitNewRowEventArgs e)
        {
            DataTable exp_dt = gc_work.DataSource as DataTable;
            if (exp_dt.Rows.Count > 4)
            {
                XtraMessageBox.Show("You Can Add Only 4 experiences of one teacher", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                gridView5.DeleteRow(e.RowHandle);
            }
        }

        private void gridView2_InitNewRow(object sender, DevExpress.XtraGrid.Views.Grid.InitNewRowEventArgs e)
        {
            DataTable edu_dt = gc_education.DataSource as DataTable;
            if (edu_dt.Rows.Count > 5)
            {
                XtraMessageBox.Show("You Can Add Only 5 Educations of one teacher", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                gridView2.DeleteRow(e.RowHandle);
            }
        }
    }
}
