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
    public partial class Default_subjects : Form
    {
        CommonFunctions fun = new CommonFunctions();
        public Default_subjects()
        {
            InitializeComponent();
            fun.selectall_Controls(this);
            load_grid();
        }
        void load_grid()
        {
            string query = "select * from subject_default";
            gridControl1.DataSource = fun.FetchDataTable(query);
        }
        private void btn_Add_Click(object sender, EventArgs e)
        {
            if(string.IsNullOrEmpty(txt_name.Text) && string.IsNullOrEmpty(txt_subjectcode.Text))
            {
                MessageBox.Show("name and code is required","info",MessageBoxButtons.OK,MessageBoxIcon.Information);
                return;
            }
            string query = "INSERT INTO `subject_default`(`name`, `subject_code`, `work_load`, `subject_fee`) VALUES " +
                " ('"+txt_name.Text+"','"+txt_subjectcode.Text+"','"+txt_workload.Text+"','"+txt_subjectfee.Text+"')";
            fun.ExecuteQuery(query);
            load_grid();
            txt_name.Text = "";
            txt_subjectcode.Text = "";
            txt_workload.Text = "0";
            txt_subjectfee.Text = "0";
        }

        private void btn_Delete_Click(object sender, EventArgs e)
        {
            DataRow dr = gridView1.GetFocusedDataRow();
            if (MessageBox.Show("want to delete '"+dr["name"]+"' subject ", "Confirmation Required", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                string query = "DELETE FROM `subject_default` WHERE id = '" + dr["id"] + "';";
                fun.ExecuteQuery(query);
                load_grid();
            }
        }

        private void gridView1_RowUpdated(object sender, DevExpress.XtraGrid.Views.Base.RowObjectEventArgs e)
        {
            DataRow dr = gridView1.GetFocusedDataRow();
            string query = "UPDATE `subject_default` SET `name`='"+dr["name"]+"',`subject_code`='"+dr["subject_code"] +"',`work_load`='"+dr["work_load"] +"',`subject_fee`='"+dr["subject_fee"] +"' WHERE id = '"+dr["id"]+"'";
            fun.ExecuteQuery(query);
        }
    }
}
