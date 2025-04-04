using MySql.Data.MySqlClient;
using System;
using System.Data;
using System.Media;
using System.Windows.Forms;

namespace SchoolManagementSystem
{
    public partial class ManageParents : DevExpress.XtraEditors.XtraUserControl
    {
        CommonFunctions fun = new CommonFunctions();
        private static ManageParents _instance;

        public static ManageParents instance
        {
            get
            {
                if (_instance == null)
                    _instance = new ManageParents();
                return _instance;
            }
        }
        public ManageParents()
        {
            InitializeComponent();
            loadfunctions();
        }
        public void loadfunctions()
        {
            FillGridParents();
        }
        public void permissions_btns(bool add, bool Edit, bool Delete)
        {
            btnPAdd.Enabled = false;
            if (add)
            {
                btnPAdd.Enabled = true;
            }
            gridView1.OptionsBehavior.Editable = false;
            if (Edit)
            {
                gridView1.OptionsBehavior.Editable = true;
            }
            BtnPDelete.Enabled = false;
            if (Delete)
                BtnPDelete.Enabled = true;
        }
        private void btnPAdd_Click(object sender, EventArgs e)
        {
            MySqlConnection con = new MySqlConnection(Login.constring);
            con.Open();
            try
            {
                MySqlCommand cmd = new MySqlCommand("INSERT into parent(name,email,password,phone,address,profession,phone2,sync) VALUES('" + txtName.Text + "','" + txtEmail.Text + "','" + txtPassword.Text + "','" + txtPhone.Text + "','" + txtAddress.Text + "','" + txtProfession.Text + "','" + txtCell.Text + "','0');", con);
                cmd.ExecuteNonQuery();
            }
            catch (MySqlException ex)
            {
                SystemSounds.Hand.Play();
                MessageBox.Show(ex.Message, "Error");
                return;
            }
            con.Close();
            FillGridParents();
            empty();
        }
        private void empty()
        {
            txtName.Text = "";
            txtEmail.Text = "";
            txtAddress.Text = "";
            txtPassword.Text = "";
            txtPhone.Text = "";
            txtProfession.Text = "";
        }
        private void FillGridParents()
        {
            MySqlConnection con = new MySqlConnection(Login.constring);
            con.Open();
            MySqlCommand cmdP = new MySqlCommand("SELECT * from parent", con);
            MySqlDataAdapter adp = new MySqlDataAdapter(cmdP);
            DataTable table = new DataTable();
            adp.Fill(table);
            gridParents.DataSource = table;
            gridView1.BestFitColumns();
            var col = gridView1.Columns["parent_id"];
            col.OptionsColumn.ReadOnly = true;
            con.Close();
        }

        private void BtnPDelete_Click(object sender, EventArgs e)
        {
            try
            {
                if (MessageBox.Show("Are you sure to delete?", "Confirm Delete!!", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    DataRow row = gridView1.GetDataRow(gridView1.FocusedRowHandle);
                    MySqlConnection con = new MySqlConnection(Login.constring);
                    con.Open();
                    MySqlCommand cmdM = new MySqlCommand("DELETE from parent WHERE parent_id='" + row[0] + "';", con);
                    cmdM.ExecuteNonQuery();
                    con.Close();
                    FillGridParents();
                }
            }
            catch (Exception ex)
            {
                SystemSounds.Hand.Play();
                MessageBox.Show(ex.Message, "Info");
            }
        }

        private void gridView1_RowUpdated(object sender, DevExpress.XtraGrid.Views.Base.RowObjectEventArgs e)
        {
            DataRow row = gridView1.GetDataRow(gridView1.FocusedRowHandle);
            MySqlConnection con = new MySqlConnection(Login.constring);
            con.Open();
            MySqlCommand cmd = new MySqlCommand("UPDATE parent set name='" + row[1] + "',email='" + row[2] + "',password='" + row[3] + "',phone='" + row[4] + "' ,address='" + row[5] + "',profession='" + row[6] + "',phone2='" + row[7] + "',sync='0' WHERE parent_id='" + row[0] + "';", con);
            cmd.ExecuteNonQuery();
            con.Close();
            // FillGridParents();
        }

        private void ManageParents_Enter(object sender, EventArgs e)
        {
            FillGridParents();
        }
    }
}
