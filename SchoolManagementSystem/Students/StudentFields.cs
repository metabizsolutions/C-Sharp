using DevExpress.XtraEditors.Repository;
using DevExpress.XtraGrid.Columns;
using MySql.Data.MySqlClient;
using System.Data;
using System.Windows.Forms;

namespace SchoolManagementSystem.Students
{
    public partial class StudentFields : DevExpress.XtraEditors.XtraForm
    {
        CommonFunctions fun = new CommonFunctions();
        public StudentFields()
        {
            InitializeComponent();
            fun.selectall_Controls(this);
            FillGrid();
        }
        private void btnSave_Click(object sender, System.EventArgs e)
        {
            if (txtHeadName.Text == "")
            {
                return;
            }
            int is_sheet = chkSheet.Checked ? 1 : 0;
            int type = radioButtonText.Checked ? 1 : 0;
            string lenght = string.IsNullOrEmpty(textBoxLenght.Text) ? "10" : textBoxLenght.Text;
            var query = "INSERT into student_fields(title,is_sheet,Type,Lenght) VALUES('" + txtHeadName.Text + "','"+ is_sheet + "',"+type+","+lenght+");";
            fun.Execute_Insert(query);
            FillGrid();
            txtHeadName.Text = "";
        }
        void FillGrid()
        {
            var query = "SELECT field_id,title,is_sheet,Type,Lenght FROM student_fields ORDER BY title ASC ";
            gridControl1.DataSource = null;
            gridView1.Columns.Clear();
            gridControl1.DataSource = CommonFunctions.AutoNumberedTable(fun.FetchDataTable(query));
            gridView1.BestFitColumns();
        }

        private void gridView1_RowUpdated(object sender, DevExpress.XtraGrid.Views.Base.RowObjectEventArgs e)
        {
            DataRow row = gridView1.GetDataRow(gridView1.FocusedRowHandle);
            var is_sheet = row["is_sheet"].ToString() == "True" ? 1 : 0;
            var Type = row["Type"].ToString() == "True" ? 1 : 0;
            var query = "UPDATE student_fields set title='" + row[2] + "',is_sheet='"+ is_sheet + "',Type="+Type+",Lenght="+ row["Lenght"] + " WHERE field_id='" + row[1] + "';";
            fun.ExecuteInsert(query);

        }

        private void btnDelete_Click(object sender, System.EventArgs e)
        {
            if (MessageBox.Show("Are you sure to delete?", "Confirm Delete!!", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                DataRow row = gridView1.GetDataRow(gridView1.FocusedRowHandle);
                var query = "DELETE from student_fields WHERE field_id='" + row[1] + "';";
                fun.ExecuteQuery(query);
                FillGrid();
            }
        }
    }
}
