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
    public partial class Holidays : UserControl
    {
        CommonFunctions fun = new CommonFunctions();
        private static Holidays _instance;
        public static Holidays instance
        {
            get
            {
                if (_instance == null)
                    _instance = new Holidays();
                return _instance;
            }
        }
        public Holidays()
        {
            InitializeComponent();
            loadfunctions();
        }
        public void loadfunctions()
        {
            load_classes();
            load_holidays();
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
        void load_holidays()
        {
            string query = "SELECT ho.`holiday_id` AS id,sec.`class_id`,cls.`name` AS class,sec.`section_id`,sec.`name` AS section,ho.`title`,ho.`start_date`,ho.`end_date` " +
                            " FROM `tbl_holidays` AS ho " +
                            " INNER JOIN section AS sec ON sec.`section_id` = ho.`section_id` " +
                            " INNER JOIN class AS cls ON cls.`class_id` = sec.`class_id`";
            DataTable dt = fun.FetchDataTable(query);
            grid_leaves.DataSource = dt;
            gridView1.Columns["section_id"].Visible = false;
            gridView1.Columns["class_id"].Visible = false;
            gridView1.Columns["class"].Group();
            gridView1.Columns["section"].Group();
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
        int holiday_id = 0;
        private void btn_save_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtSection.EditValue.ToString()))
            {
                MessageBox.Show("Please Select Sections", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            string[] sections = txtSection.EditValue.ToString().Split(',');
            string query = "";
            foreach (string sec in sections)
            {
                if(holiday_id > 0)
                {
                    query = "select * from tbl_holidays where holiday_id = '"+holiday_id+"' and section_id = '"+sec+"'";
                    DataTable dt = fun.FetchDataTable(query);
                    if(dt.Rows.Count > 0) // update
                    {
                        query = "update `tbl_holidays` set `title` = '" + txt_holiday_title.Text + "',`start_date` = '" + dtp_from_date.Value.ToString("yyyy-MM-dd") + "',`end_date` = '" + dtp_to_date.Value.ToString("yyyy-MM-dd") + "',`section_id` = '" + sec + "' where `holiday_id` = '" + holiday_id + "';";
                    
                    }
                    else
                    {
                        query = "INSERT INTO tbl_holidays (title,start_date,end_date,section_id) VALUES ('" + txt_holiday_title.Text + "','" + dtp_from_date.Value.ToString("yyyy-MM-dd") + "','" + dtp_to_date.Value.ToString("yyyy-MM-dd") + "','" + sec + "'); ";
                    }
                }
                else
                {
                    query = "INSERT INTO tbl_holidays (title,start_date,end_date,section_id) VALUES ('" + txt_holiday_title.Text + "','" + dtp_from_date.Value.ToString("yyyy-MM-dd") + "','" + dtp_to_date.Value.ToString("yyyy-MM-dd") + "','" + sec + "'); ";
                }

                fun.ExecuteQuery(query);
            }
            if(holiday_id <= 0)
            {
                foreach (DateTime day in fun.EachDay(dtp_from_date.Value, dtp_to_date.Value))
                {
                    foreach (string sec in sections)
                        fun.attendance_holidays_leaves_chack(sec, day.Year + "-" + day.Month + "-" + day.Day);
                }
            }
            holiday_id = 0;
            load_holidays();
            empty();
        }

        void empty()
        {
            txtClass.EditValue = null;
            txtClass.Text = "";
            txtSection.EditValue = null;
            txtSection.Text = "";
            txt_holiday_title.Text = "";
            holiday_id = 0;
        }
        private void dtp_from_date_ValueChanged(object sender, EventArgs e)
        {
            dtp_to_date.MinDate = dtp_from_date.Value;
        }

        private void btn_delete_Click(object sender, EventArgs e)
        {
            DataRow dr = gridView1.GetFocusedDataRow();
            if(dr != null)
            {
                string query = "delete from tbl_holidays where holiday_id = '"+dr["id"]+"'";
                fun.ExecuteQuery(query);
                gridView1.DeleteRow(gridView1.FocusedRowHandle);
            }
            load_holidays();
        }

        private void gridView1_RowUpdated(object sender, DevExpress.XtraGrid.Views.Base.RowObjectEventArgs e)
        {
            
        }

        private void btn_edit_Click(object sender, EventArgs e)
        {
            DataRow dr = gridView1.GetFocusedDataRow();
            if (dr != null)
            {
                dtp_from_date.Value = Convert.ToDateTime(dr["start_date"]);
                dtp_to_date.Value = Convert.ToDateTime(dr["end_date"]);
                txtClass.EditValue = Convert.ToInt32(dr["class_id"]);
                txtClass.Update();
                txtSection.EditValue = Convert.ToInt32(dr["section_id"]);
                txtSection.Update();
                txt_holiday_title.Text = dr["title"].ToString();
                holiday_id = Convert.ToInt32(dr["id"]);
            }
        }
    }
}
