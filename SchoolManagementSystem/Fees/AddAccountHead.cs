using DevExpress.XtraEditors.Repository;
using DevExpress.XtraGrid.Columns;
using MySql.Data.MySqlClient;
using System.Data;
using System.Windows.Forms;

namespace SchoolManagementSystem.Principal
{
    public partial class AddAccountHead : DevExpress.XtraEditors.XtraForm
    {
        CommonFunctions fun = new CommonFunctions();
        public AddAccountHead()
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
            if (txtHeadName.Text == "Salary" || txtHeadName.Text == "Student Fee")
            {
                MessageBox.Show(txtHeadName.Text + " name already exist", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            var query = "INSERT into expense_category(name,sync,type) VALUES('" + txtHeadName.Text + "','0', '"+ CbType.Text + "');";
            fun.Execute_Insert(query);
            FillGrid();
            txtHeadName.Text = "";
        }
        void FillGrid()
        {
            var query = "select expense_category_id,name as `Heads`,type from expense_category where name not in ('Salary','Student Fee')";
            gridControl1.DataSource = null;
            gridView1.Columns.Clear();
            gridControl1.DataSource = CommonFunctions.AutoNumberedTable(fun.FetchDataTable(query));
            gridView1.BestFitColumns();
            var col1 = gridView1.Columns["expense_category_id"];
            col1.Visible = false;

            RepositoryItemComboBox riComboC = new RepositoryItemComboBox();
            riComboC.Items.Add("Expense");
            riComboC.Items.Add("Income");
            gridView1.Columns["type"].ColumnEdit = riComboC;


            GridColumn Column1 = gridView1.Columns["Sr#"];
            Column1.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
        }

        private void gridView1_RowUpdated(object sender, DevExpress.XtraGrid.Views.Base.RowObjectEventArgs e)
        {
            DataRow row = gridView1.GetDataRow(gridView1.FocusedRowHandle);
            if(row[2].ToString() == "Salary" || row[2].ToString() == "Student Fee")
            {
                MessageBox.Show(row[2].ToString()+" name already exist","",MessageBoxButtons.OK,MessageBoxIcon.Information);
                return;
            }
            var query = "UPDATE expense_category set name='" + row[2] + "',type='"+row[3]+"' WHERE expense_category_id='" + row[1] + "';";
            fun.Execute_Query(query);

        }

        private void btnDelete_Click(object sender, System.EventArgs e)
        {
            if (MessageBox.Show("Are you sure to delete?", "Confirm Delete!!", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                DataRow row = gridView1.GetDataRow(gridView1.FocusedRowHandle);
                var query = "DELETE from expense_category WHERE expense_category_id='" + row[1] + "';";
                fun.ExecuteQuery(query);
            }
        }
    }
}
