using DevExpress.XtraEditors.Mask;
using DevExpress.XtraEditors.Repository;
using MySql.Data.MySqlClient;
using System;
using System.Collections.ObjectModel;
using System.Data;
using System.Media;
using System.Windows.Forms;

namespace SchoolManagementSystem.Class
{
    public partial class CashAssets : DevExpress.XtraEditors.XtraUserControl
    {
        private static CashAssets _instance;
        public static CashAssets instance
        {
            get
            {
                if (_instance == null)
                    _instance = new CashAssets();
                return _instance;
            }
        }
        public CashAssets()
        {
            InitializeComponent();
            loadfunctions();
        }
        public void loadfunctions()
        {
            FillGridClass();
        }
        private void btnCAdd_Click(object sender, System.EventArgs e)
        {
            String query = "insert into cash_assets set title = '{0}', open_balance = '{1}',ondate = '{2}',`disable` = '0'";
            if (txtTitle.Text == "")
            {
                MessageBox.Show("Title is required field!");
                return;
            }
            query = String.Format(query, txtTitle.Text, txtOpenBalance.Text, fun.time());
            int id = fun.ExecuteInsert(query);
            FillGridClass();
            empty();
        }
        private void empty()
        {
            txtTitle.Text = "";
            txtOpenBalance.Text = "0.0";
        }
        public void permissions_btns(bool add, bool Edit, bool Delete)
        {
            btnCAdd.Enabled = false;
            if (add)
            {
                btnCAdd.Enabled = true;
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
        int[] cat_ids;
        public void FillGridClass()
        {
            string query = "SELECT cashasset_id AS id,title,open_balance,open_balance AS current_balance,`disable` " +
                               " FROM cash_assets ORDER BY cashasset_id DESC ";
            DataTable table = fun.GetQueryTable(query);
            gridClass.DataSource = null;
            gridClass.DataSource = table;
            gridView1.BestFitColumns();
            gridView1.Columns["id"].OptionsColumn.ReadOnly = true;
            gridView1.Columns["current_balance"].OptionsColumn.ReadOnly = true;
        }
        

        private void BtnPDelete_Click(object sender, EventArgs e)
        {
            DataRow row = gridView1.GetDataRow(gridView1.FocusedRowHandle);
            if (MessageBox.Show("Do You Really want to delete?", "Confirm delete", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                string id = row["id"].ToString();
                string query_delete = "DELETE FROM cash_assets WHERE cashasset_id = '{0}'";
                query_delete = string.Format(query_delete, id);
                fun.ExecuteQuery(query_delete);
                FillGridClass();
            }
            
        }
        ObservableCollection<AllClass> allClass = new ObservableCollection<AllClass>();
        CommonFunctions fun = new CommonFunctions();
   

        private void gridView1_RowUpdated_1(object sender, DevExpress.XtraGrid.Views.Base.RowObjectEventArgs e)
        {
            DataRow row = gridView1.GetDataRow(gridView1.FocusedRowHandle);
            string query;
            int disable = Convert.ToBoolean(row["disable"].ToString()) == true ? 1 : 0;
            query = "UPDATE cash_assets SET title = '{0}',open_balance = '{1}',`disable` = '{2}' WHERE cashasset_id = '{3}'";
            query = string.Format(query, row["title"].ToString(), row["open_balance"].ToString(), disable, row["id"].ToString());
            fun.ExecuteQuery(query);
            FillGridClass();
        }
    }
}
