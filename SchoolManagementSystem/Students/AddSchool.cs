using DevExpress.XtraGrid.Columns;
using MySql.Data.MySqlClient;
using System;
using System.Collections.ObjectModel;
using System.Data;
using System.Windows.Forms;

namespace SchoolManagementSystem.Principal
{
    public partial class AddSchool : DevExpress.XtraEditors.XtraForm
    {
        ObservableCollection<AllClass> allClass = new ObservableCollection<AllClass>();

        CommonFunctions fun = new CommonFunctions();

        public AddSchool()
        {
            InitializeComponent();
            fun.selectall_Controls(this);
            fun.DateFormat(txtDate);
            FillGrid();
        }
        private void btnSave_Click(object sender, System.EventArgs e)
        {
            MySqlConnection con = new MySqlConnection(Login.constring);
            con.Open();
            var query = "INSERT into tbl_school(name,address,date) VALUES('" + txtVisitorName.Text + "','" + txtSubject.Text + "','" + txtDate.DateTime.ToString("yyyy-MM-dd") + "');";
            MySqlCommand cmd1 = new MySqlCommand(query, con);
            cmd1.ExecuteNonQuery();
            con.Close();
            FillGrid();
        }
        void FillGrid()
        {
            MySqlConnection con = new MySqlConnection(Login.constring);
            con.Open();
            MySqlCommand cmdP = new MySqlCommand("select * from tbl_school", con);
            MySqlDataAdapter adp = new MySqlDataAdapter(cmdP);
            DataTable table = new DataTable();
            adp.Fill(table);
            gridControl1.DataSource = null;
            gridView1.Columns.Clear();
            gridControl1.DataSource = CommonFunctions.AutoNumberedTable(table);
            gridView1.BestFitColumns();
            con.Close();
            var col1 = gridView1.Columns["school_id"];
            col1.Visible = false;
            //GridColumn Column1 = gridView1.Columns["Sr#"];
            //Column1.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
        }

        private void btnDelete_Click(object sender, System.EventArgs e)
        {
            try
            {
                if (MessageBox.Show("Are you sure to delete?", "Confirm Delete!!", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    DataRow row = gridView1.GetDataRow(gridView1.FocusedRowHandle);
                    MySqlConnection con = new MySqlConnection(Login.constring);
                    con.Open();
                    MySqlCommand cmdM = new MySqlCommand("DELETE from tbl_school WHERE school_id='" + row["school_id"] + "';", con);
                    cmdM.ExecuteNonQuery();
                    con.Close();
                    FillGrid();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Info");
            }
        }
    }
}
