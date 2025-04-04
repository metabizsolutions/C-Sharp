using DevExpress.Snap.Core.API;
using MySql.Data.MySqlClient;
using System;
using System.Collections.ObjectModel;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

namespace SchoolManagementSystem.Class
{
    public partial class ExperienceLetter : DevExpress.XtraEditors.XtraUserControl
    {
        private static ExperienceLetter _instance;

        public static ExperienceLetter instance
        {
            get
            {
                if (_instance == null)
                    _instance = new ExperienceLetter();
                return _instance;
            }
        }
        CommonFunctions fun = new CommonFunctions();
        public ExperienceLetter()
        {
            InitializeComponent();
            loadfunctions();
        }

        public void loadfunctions()
        {
            result();
        }

        public void result()
        {
            var query = "SELECT `teacher_id`,`name`,`birthday`,`sex` as `gender`,`religion`,`blood_group`,`address`,`phone`,`phone2`,`FName`,`CNIC`,`JoiningDate`,`EndingDate`,`designation`,`staff_type`,passout_date FROM teacher where passout = 1";
            DataTable table = fun.FetchDataTable(query);
            gridExperienceLetter.DataSource = null;
            gridView1.Columns.Clear();
            gridExperienceLetter.DataSource = table;
            gridView1.BestFitColumns();
        }
        private void ExperienceLetter_Enter(object sender, System.EventArgs e)
        {
            //result();
        }
        void rejoin_teacher(DataRow dr, int rowhandle)
        {
            string query = "UPDATE `teacher` SET `passout`='0'  WHERE teacher_id = '" + dr["teacher_id"] + "'";
            fun.ExecuteQuery(query);
            gridView1.DeleteRow(rowhandle);
        }
        private void btn_print_Click(object sender, EventArgs e)
        {
            DataRow row = gridView1.GetDataRow(gridView1.FocusedRowHandle);
            fun.receipt_report(row, "experience_card_default.snx").ShowPrintPreview();
        }

        private void btn_rejoin_Click(object sender, EventArgs e)
        {
            DataRow row = gridView1.GetDataRow(gridView1.FocusedRowHandle);
            if (row != null)
            {
                if (MessageBox.Show("Do you realy want to rejoin teacher ='" + row["name"] + "'? YES to Confirm. NO to Discard", "Teacher Rejoin", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    rejoin_teacher(row, gridView1.FocusedRowHandle);
            }
        }
    }
}
