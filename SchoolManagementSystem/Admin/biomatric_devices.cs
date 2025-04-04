using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SchoolManagementSystem.Admin
{
    public partial class biomatric_devices : UserControl
    {
        private static biomatric_devices _instance;

        public static biomatric_devices instance
        {
            get
            {
                if (_instance == null)
                    _instance = new biomatric_devices();
                return _instance;
            }
        }
        CommonFunctions fun = new CommonFunctions();
        int device_id =0;
        public biomatric_devices()
        {
            InitializeComponent();
            loadfunctions();
        }
        public void loadfunctions()
        {
            device_id = 0;
            load_grid();
        }
        public void permissions_btns(bool add, bool Edit, bool Delete)
        {
            btn_add.Enabled = false;
            btn_edit.Enabled = false;
            btn_delete.Enabled = false;
            if(add)
                btn_add.Enabled = true;
            if (Edit)
                btn_edit.Enabled = true;
            if (Delete)
                btn_delete.Enabled = true;
        }
        void load_grid()
        {
            string query = "select `device_id`,`ip`,`port`,`last_connected`,`last_disconnected`,if(`status`= 1,'Active','De-Active') as `status`,`type` from tbl_biometric_devices";
            gridControl1.DataSource = fun.FetchDataTable(query);
        }
        private void btn_add_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txt_ip.Text) || string.IsNullOrEmpty(txt_port.Text) || string.IsNullOrEmpty(com_status.Text) || string.IsNullOrEmpty(com_type.Text))
            {
                MessageBox.Show("Please fill all fields");
                return;
            }
            int status = com_status.Text == "Active" ? 1 : 0;
            string query = "";
            if (device_id > 0)
                query = "UPDATE `tbl_biometric_devices` SET `ip` = '" + txt_ip.Text + "',`port` = '" + txt_port.Text + "',`status` = '" + status + "',`type` = '" + com_type.Text + "' WHERE `device_id` = '" + device_id + "';";
            else
                query = "INSERT INTO `tbl_biometric_devices` (`ip`,`port`,`status`,`type`) VALUES ('" + txt_ip.Text + "','" + txt_port.Text + "','" + status + "','" + com_type.Text + "'); ";

            fun.ExecuteQuery(query);
            empty();
            load_grid();
        }
        void empty()
        {
            txt_ip.Text = "";
            txt_port.Text = "";
            com_status.SelectedIndex = 0;
            com_type.SelectedIndex = 0;
            device_id = 0;
        }

        private void btn_edit_Click(object sender, EventArgs e)
        {
            DataRow dr = gridView1.GetFocusedDataRow();
            txt_ip.Text = dr["ip"].ToString();
            txt_port.Text = dr["port"].ToString();
            com_status.SelectedIndex = dr["status"].ToString() == "Active" ? 0 : 1;
            com_type.SelectedIndex = dr["type"].ToString() == "outer" ? 0 : 1;
            device_id = Convert.ToInt32(dr["device_id"].ToString());
        }

        private void btn_delete_Click(object sender, EventArgs e)
        {
            DataRow dr = gridView1.GetFocusedDataRow();
            if (dr != null)
            {
                DialogResult a = MessageBox.Show("Are your sure you want to Delete This Device", "Bio Matric", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
                if (a == DialogResult.Yes)
                {
                    string query = "DELETE FROM `tbl_biometric_devices` WHERE `device_id` = '" + dr["device_id"].ToString() + "';";
                    fun.ExecuteQuery(query);
                    load_grid();
                }
            }
        }
    }
}
