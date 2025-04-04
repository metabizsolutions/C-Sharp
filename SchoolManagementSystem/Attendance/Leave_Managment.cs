using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SchoolManagementSystem.Attendance
{
    public partial class Leave_Managment : UserControl
    {
        CommonFunctions fun = new CommonFunctions();
        private static Leave_Managment _instance;
        public static Leave_Managment instance
        {
            get
            {
                if (_instance == null)
                    _instance = new Leave_Managment();
                return _instance;
            }
        }
        public Leave_Managment()
        {
            InitializeComponent();
            loadfunctions();
        }
        public void loadfunctions()
        {
            load_classes();
            load_leaves();
            dtp_from_date.MinDate = DateTime.Now.Date;
            dtp_to_date.MinDate = DateTime.Now.Date;
        }
        public void permissions_btns(bool add, bool Edit, bool Delete)
        {
            btn_save.Enabled = false;
            btn_edit.Enabled = false;
            btn_delete.Enabled = false;
            if (add)
            {
                btn_save.Enabled = true;
            }
            if (Edit)
            {
                btn_edit.Enabled = true;
            }
            if (Delete)
            {
                btn_delete.Enabled = true;
            }
        }
        void load_leaves()
        {
            string query = "SELECT ho.`leave_id` AS id,std.student_id,std.name as student,sec.`class_id`,cls.`name` AS class,sec.`section_id`,sec.`name` AS section,ho.`title`,ho.`start_date`,ho.`end_date` " +
                            " FROM `tbl_students_leaves` AS ho " +
                            " inner join student as std on std.student_id = ho.student_id " +
                            " INNER JOIN section AS sec ON sec.`section_id` = ho.section_id " +
                            " INNER JOIN class AS cls ON cls.`class_id` = sec.`class_id` ";
            DataTable dt = fun.FetchDataTable(query);
            grid_leaves.DataSource = dt;
            gridView1.Columns["section_id"].Visible = false;
            gridView1.Columns["class_id"].Visible = false;
            gridView1.Columns["student_id"].Visible = false;
            gridView1.Columns["student"].Group();
            gridView1.ExpandAllGroups();
        }
        void load_classes()
        {
            txtClass.Properties.DataSource = fun.GetAllClasses_dt();
            txtClass.Properties.DisplayMember = "name";
            txtClass.Properties.ValueMember = "class_id";
        }
        private void txtClass_EditValueChanged(object sender, EventArgs e)
        {
            if (txtClass.EditValue != null && !string.IsNullOrEmpty(txtClass.EditValue.ToString()))
            {
                txtSection.Properties.DataSource = fun.GetAllSection_dt(txtClass.EditValue.ToString());
                txtSection.Properties.DisplayMember = "name";
                txtSection.Properties.ValueMember = "section_id";
            }
        }
        private void txtSection_EditValueChanged(object sender, EventArgs e)
        {
            if (txtSection.EditValue != null && !string.IsNullOrEmpty(txtSection.EditValue.ToString()))
            {
                txtstudents.Properties.DataSource = fun.GetAllStudentsinSection(txtSection.EditValue.ToString());
                txtstudents.Properties.DisplayMember = "name";
                txtstudents.Properties.ValueMember = "student_id";
            }
        }
        int leave_id = 0;
        private void btn_save_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtstudents.EditValue.ToString()))
            {
                MessageBox.Show("Please Select Students","Info",MessageBoxButtons.OK,MessageBoxIcon.Information);
                return;
            }
            string[] students = txtstudents.EditValue.ToString().Split(',');
            string query = "";
            foreach (string std in students)
            {
                if (leave_id > 0)
                {
                    query = "select * from tbl_students_leaves where leave_id = '" + leave_id + "' and section_id = '"+txtSection.EditValue.ToString()+"' and student_id = '" + std + "'";
                    DataTable dt = fun.FetchDataTable(query);
                    if (dt.Rows.Count > 0) // update
                    {
                        query = "update `tbl_students_leaves` set `title` = '" + txt_holiday_title.Text + "',`start_date` = '" + dtp_from_date.Value.ToString("yyyy-MM-dd") + "',`end_date` = '" + dtp_to_date.Value.ToString("yyyy-MM-dd") + "',`student_id` = '" + std + "',section_id = '"+txtSection.EditValue.ToString()+"' where `leave_id` = '" + leave_id + "';";

                    }
                    else
                    {
                        query = "INSERT INTO tbl_students_leaves (title,start_date,end_date,student_id,section_id) VALUES ('" + txt_holiday_title.Text + "','" + dtp_from_date.Value.ToString("yyyy-MM-dd") + "','" + dtp_to_date.Value.ToString("yyyy-MM-dd") + "','" + std + "','"+txtSection.EditValue.ToString()+"'); ";
                    }
                }
                else
                {
                    query = "INSERT INTO tbl_students_leaves (title,start_date,end_date,student_id,section_id) VALUES ('" + txt_holiday_title.Text + "','" + dtp_from_date.Value.ToString("yyyy-MM-dd") + "','" + dtp_to_date.Value.ToString("yyyy-MM-dd") + "','" + std + "','" + txtSection.EditValue.ToString() + "'); ";
                }

                fun.ExecuteQuery(query);

            }
            if (leave_id <= 0)
            {
                foreach (DateTime day in fun.EachDay(dtp_from_date.Value, dtp_to_date.Value))
                {
                        fun.attendance_holidays_leaves_chack(txtSection.EditValue, day.Year + "-" + day.Month + "-" + day.Day);
                }
            }
            leave_id = 0;
            load_leaves();
            empty();
        }
        void empty()
        {
            txtClass.EditValue = null;
            txtClass.Text = "";
            txtSection.EditValue = null;
            txtSection.Text = "";
            txtstudents.EditValue = null;
            txtstudents.Text = "";
            txt_holiday_title.Text = "";
            leave_id = 0;
        }
        private void dtp_from_date_ValueChanged(object sender, EventArgs e)
        {
            dtp_to_date.MinDate = dtp_from_date.Value;
        }

        private void btn_delete_Click(object sender, EventArgs e)
        {
            DataRow dr = gridView1.GetFocusedDataRow();
            if (dr != null)
            {
                string query = "delete from tbl_students_leaves where leave_id = '" + dr["id"] + "'";
                fun.ExecuteQuery(query);
                gridView1.DeleteRow(gridView1.FocusedRowHandle);
            }
            load_leaves();
        }

        private void btn_edit_Click(object sender, EventArgs e)
        {
            DataRow dr = gridView1.GetFocusedDataRow();
            if (dr != null)
            {
                dtp_from_date.Value = Convert.ToDateTime(dr["start_date"]);
                dtp_to_date.Value = Convert.ToDateTime(dr["end_date"]);
                txtSection.EditValue = Convert.ToInt32(dr["section_id"]);
                txtClass.EditValue = Convert.ToInt32(dr["class_id"]);
                txtstudents.EditValue = Convert.ToInt32(dr["student_id"]);
                txt_holiday_title.Text = dr["title"].ToString();
                leave_id = Convert.ToInt32(dr["id"]);
            }
        }
    }
}
