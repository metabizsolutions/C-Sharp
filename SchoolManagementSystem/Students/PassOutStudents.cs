using DevExpress.Snap.Core.API;
using MySql.Data.MySqlClient;
using System;
using System.Collections.ObjectModel;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

namespace SchoolManagementSystem.Students
{
    public partial class PassOutStudents : DevExpress.XtraEditors.XtraUserControl
    {
        private static PassOutStudents _instance;

        public static PassOutStudents instance
        {
            get
            {
                if (_instance == null)
                    _instance = new PassOutStudents();
                return _instance;
            }
        }
        CommonFunctions fun = new CommonFunctions();
        public PassOutStudents()
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
            var query = "SELECT `SrNo`,`Name`,`FatherName`,`FatherPhone`,`Phone`,`Class`,`Section`,`Birthday`,`Gender`,`Address`,`Roll`,`Email`,`HostelRoom`,passout_date,addmission_date FROM students where PassOut='1'";
            DataTable table = fun.FetchDataTable(query);
            gridPassOut.DataSource = null;
            gridView1.Columns.Clear();
            gridPassOut.DataSource = table;
            gridView1.BestFitColumns();
            gridView1.Columns["Class"].GroupIndex = 0;
            gridView1.Columns["Section"].GroupIndex = 1;
            gridView1.ExpandAllGroups();
        }

        private void PassOutStudents_Enter(object sender, System.EventArgs e)
        {
            //result();
        }
        void rejoin_student(DataRow dr,int rowhandle)
        {
            string query = "UPDATE `student` SET `passout`='0'  WHERE student_id = '" + dr["SrNo"] + "'";
            fun.ExecuteQuery(query);
            gridView1.DeleteRow(rowhandle);
        }
        private void btn_rejoin_Click(object sender, EventArgs e)
        {
            DataRow row = gridView1.GetDataRow(gridView1.FocusedRowHandle);
            if (row != null)
            {
                if (MessageBox.Show("Do you realy want to rejoin Student Name ='" + row["Name"] + "' and Father Name ='"+row["FatherName"] +"'? YES to Confirm. NO to Discard", "Teacher Rejoin", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    rejoin_student(row, gridView1.FocusedRowHandle);
            }
        }
        private void btn_print_Click(object sender, EventArgs e)
        {
            DataRow row = gridView1.GetDataRow(gridView1.FocusedRowHandle);
            fun.receipt_report(row, "passout_card_default.snx").ShowPrintPreview();
        }
    }
}
