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
    public partial class Degrees : Form
    {
        CommonFunctions fun = new CommonFunctions();
        public Degrees()
        {
            InitializeComponent();
            fun.selectall_Controls(this);
            loaddata();
        }
        void loaddata()
        {
            string query = "select * from degrees";
            DataTable dt = fun.FetchDataTable(query);
            gridControl1.DataSource = dt;
        }
        private void btnCAdd_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtName.Text))
            {
                MessageBox.Show("Insert Degree Name");
                return;
            }
            string query = "INSERT INTO `degrees`(`name`) VALUES ('"+ txtName.Text + "')";
            fun.ExecuteQuery(query);
            loaddata();
        }

        private void BtnPDelete_Click(object sender, EventArgs e)
        {
            DataRow dr = gridView1.GetFocusedDataRow();
            string query = "delete from degrees where degree_id = '" + dr["degree_id"] + "'";
            fun.ExecuteQuery(query);
            loaddata();
        }
    }
}
