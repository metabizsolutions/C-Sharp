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
    public partial class ActivityCategory : DevExpress.XtraEditors.XtraUserControl
    {
        private static ActivityCategory _instance;

        public static ActivityCategory instance
        {
            get
            {
                if (_instance == null)
                    _instance = new ActivityCategory();
                return _instance;
            }
        }
        public ActivityCategory()
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
            String query = "INSERT INTO activity_category SET title = '{0}'";
            if(txtTitle.Text == "")
            {
                MessageBox.Show("Title is required field!");
                return;
            }
            query = String.Format(query, txtTitle.Text);
            fun.ExecuteInsert(query);
            FillGridClass();
            empty();
        }
        private void empty()
        {
            txtTitle.Text = "";
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

        public void FillGridClass()
        {
            string query_cat = "SELECT * FROM activity_category ORDER BY activity_cat_id DESC ";
            DataTable table = fun.GetQueryTable(query_cat);
            
            gridClass.DataSource = null;
            gridClass.DataSource = table;
            gridView1.BestFitColumns();

        }
        

        private void BtnPDelete_Click(object sender, EventArgs e)
        {
            DataRow row = gridView1.GetDataRow(gridView1.FocusedRowHandle);
            if (MessageBox.Show("Do You Really want to delete?", "Confirm delete", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                string activity_cat_id = row["activity_cat_id"].ToString();
                string query_delete = "DELETE FROM activity_category WHERE activity_cat_id = '{0}'";
                query_delete = string.Format(query_delete, activity_cat_id);
                fun.ExecuteQuery(query_delete);
                MessageBox.Show("Your record deleted successfully! ");
                FillGridClass();
            }
            
            
            
        }
        ObservableCollection<AllClass> allClass = new ObservableCollection<AllClass>();
        CommonFunctions fun = new CommonFunctions();
       

        private void gridView1_RowUpdated_1(object sender, DevExpress.XtraGrid.Views.Base.RowObjectEventArgs e)
        {
            DataRow row = gridView1.GetDataRow(gridView1.FocusedRowHandle);
            int disable = Convert.ToBoolean(row["disable"]) ? 1 : 0;
            string query = "UPDATE activity_category SET title = '{0}', `disable` = '{1}' WHERE activity_cat_id = '{2}'";
            query = string.Format(query, row["title"], disable, row["activity_cat_id"]);
            fun.ExecuteQuery(query);
        }
    }
}
