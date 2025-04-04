using System;
using System.Data;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
using System.Collections.ObjectModel;
using DevExpress.XtraEditors.Repository;
using SchoolManagementSystem.Admin;
using System.Media;

namespace SchoolManagementSystem.Users
{
    public partial class Users : DevExpress.XtraEditors.XtraUserControl
    {
        private static Users _instance;

        public static Users instance
        {
            get
            {
                if (_instance == null)
                    _instance = new Users();
                return _instance;
            }
        }
        public Users()
        {
            InitializeComponent();
            loadfunctions();
        }
        public void loadfunctions()
        {
            LoadUser();
        }
        public void permissions_btns(bool add, bool Edit, bool Delete)
        {
            BtnAddEmployees.Enabled = false;
            gridView5.OptionsBehavior.Editable = false;
            BtnDelEmployee.Enabled = false;
            if (add) { BtnAddEmployees.Enabled = true; }
            if (Edit) { gridView5.OptionsBehavior.Editable = true; }
            if (Delete) { BtnDelEmployee.Enabled = true; }
        }
        #region Users
        ObservableCollection<AllValues> allValue;
        CommonFunctions fun = new CommonFunctions();
        private void btnCLavel_Click(object sender, EventArgs e)
        {
            using (Permissions per = new Permissions())
            //using (Permissions_Desk per = new Permissions_Desk())
            {
                if (per.ShowDialog() == DialogResult.Yes)
                { }
                else
                {
                    user_status();
                }
            }


        }
        private void simpleButton1_Click(object sender, EventArgs e)
        {
            Permissions_TeacherApp per = new Permissions_TeacherApp();
            per.ShowDialog();
        }
        public void LoadUser()
        {
            FillGridEmployees();
            user_status();

        }
        void user_status()
        {
            txtUStatus.Properties.DataSource = fun.all_users_status("Desktop");
            txtUStatus.Properties.DisplayMember = "title";
            txtUStatus.Properties.ValueMember = "permission_id";
        }
        private void BtnAddEmployees_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtUName.Text) || string.IsNullOrEmpty(txtUPass.Text) || string.IsNullOrEmpty(txtUEmail.Text) || string.IsNullOrEmpty(txtUStatus.EditValue.ToString()))
            {
                MessageBox.Show("User name and password should not be empty", "user info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            var id = txtUStatus.EditValue;

            string query = "select * from admin where name ='" + txtUName.Text + "' and password = '" + txtUPass.Text + "'";
            DataTable dt = fun.FetchDataTable(query);
            if (dt.Rows.Count > 0)
            {
                MessageBox.Show("Same User name and Password already exist please add new one", "user info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            query = "INSERT into admin(name,email,password,level,sync) VALUES('" + txtUName.Text + "','" + txtUEmail.Text + "','" + txtUPass.Text + "','" + id + "','0');";
            fun.ExecuteQuery(query);
            FillGridEmployees();
            emptyManagement();
        }
        private void emptyManagement()
        {
            txtUName.Text = "";
            txtUPass.Text = "";
            txtUEmail.Text = "";
            txtUStatus.EditValue = null;
        }
        void FillGridEmployees()
        {
            MySqlConnection con = new MySqlConnection(Login.constring);
            con.Open();
            MySqlCommand cmdU = new MySqlCommand("SELECT  admin_id as ID, name as User,email as Email,level as Level from admin where name not like 'ZeeShan'; ", con);
            MySqlDataAdapter adp = new MySqlDataAdapter(cmdU);
            DataTable table = new DataTable();
            adp.Fill(table);
            gridEmployees.DataSource = table;
            gridView5.BestFitColumns();
            var col = gridView5.Columns["ID"];
            col.OptionsColumn.ReadOnly = true;
            con.Close();

            RepositoryItemSearchLookUpEdit sta = new RepositoryItemSearchLookUpEdit();
            sta.DataSource = fun.all_users_status("Desktop");
            sta.DisplayMember = "title";
            sta.ValueMember = "permission_id";
            gridView5.Columns["Level"].ColumnEdit = sta;
        }
        private void gridView5_RowUpdated(object sender, DevExpress.XtraGrid.Views.Base.RowObjectEventArgs e)
        {
            DataRow row = gridView5.GetDataRow(gridView5.FocusedRowHandle);
            string query = "UPDATE admin set name='" + row["User"] + "',email='" + row["Email"] + "',level='" + row["Level"].ToString() + "',sync='0' WHERE admin_id='" + row["ID"] + "';";
            fun.ExecuteQuery(query);
        }
        Sync sync = new Sync();
        private void BtnDelEmployee_Click(object sender, EventArgs e)
        {
            try
            {
                if (MessageBox.Show("Are you sure to delete?", "Confirm Delete!!", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    DataRow row = gridView5.GetDataRow(gridView5.FocusedRowHandle);
                    string query = "DELETE from admin WHERE admin_id='" + row["ID"] + "';";
                    fun.ExecuteQuery(query);
                    sync.SyncDelete("admin", "admin_id", row[0].ToString());
                    FillGridEmployees();
                }
            }
            catch (Exception ex)
            {
                SystemSounds.Hand.Play();
                MessageBox.Show(ex.Message, "Info");
            }
        }

        #endregion

        private void BtnEditEmploye_Click(object sender, EventArgs e)
        {

        }
    }
}
