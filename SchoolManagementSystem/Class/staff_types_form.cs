using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SchoolManagementSystem.Class
{
    public partial class staff_types_form : Form
    {
        CommonFunctions fun = new CommonFunctions();
        public staff_types_form()
        {
            InitializeComponent();
            fun.selectall_Controls(this);
            load_types();
        }
        public void load_types()
        {
            string query = "select * from `staff_type`";
            DataTable dt = fun.FetchDataTable(query);
            staff_types_grid.DataSource = dt;
        }
        private void btn_add_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txt_staff_type.Text))
            {
                MessageBox.Show("Staff type required", "error", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            string query = "insert into `staff_type` (`type_name`) values ('"+ txt_staff_type.Text + "');";
            fun.ExecuteQuery(query);
            load_types();
        }

        private void btn_delete_Click(object sender, EventArgs e)
        {
            DataRow dr = gridView1.GetFocusedDataRow();
            string query = "Delete from `staff_type` where id = '"+dr["id"]+"'";
            fun.ExecuteQuery(query);
            load_types();
        }
    }
}
