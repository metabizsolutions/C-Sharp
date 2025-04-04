using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.XtraEditors.Repository;

namespace SchoolManagementSystem.account_d_e
{
    public partial class ladgers : UserControl
    {
        private static ladgers _instance;
        public static ladgers instance
        {
            get
            {
                if (_instance == null)
                    _instance = new ladgers();
                return _instance;
            }
        }
        CommonFunctions fun = new CommonFunctions();
        public ladgers()
        {
            InitializeComponent();
            loadfunctions();
        }
        public void loadfunctions()
        {
            ladger_head_list();
            load_ladgers();
        }
        public void permissions_btns(bool add, bool Edit, bool Delete)
        {

        }
        void ladger_head_list()
        {
            ddl_ladger_head.Properties.DataSource = fun.ladger_head();
            ddl_ladger_head.Properties.DisplayMember = "title";
            ddl_ladger_head.Properties.ValueMember = "head_id";
        }
        Dictionary<string, int> status = new Dictionary<string, int>();
        void load_ladgers()
        {
            string query = "SELECT `id`, `title`,`balance`, `head_id` as ladger_head, `status`, `mobile`, `email`, `address`,is_auto_genrated from `ac_ledger`;";
            gridControl1.DataSource = fun.FetchDataTable(query);
            
            RepositoryItemSearchLookUpEdit Cmbo = new RepositoryItemSearchLookUpEdit();
            Cmbo.DataSource = fun.ladger_head();
            Cmbo.DisplayMember = "title";
            Cmbo.ValueMember = "head_id";
            gridView2.Columns["ladger_head"].ColumnEdit = Cmbo;

            status.Clear();
            status.Add("DeActive", 0);
            status.Add("Active", 1);
            RepositoryItemSearchLookUpEdit sta = new RepositoryItemSearchLookUpEdit();
            sta.DataSource = status;
            sta.DisplayMember = "key";
            sta.ValueMember = "value";
            gridView2.Columns["status"].ColumnEdit = sta;

            string[] editable = { "title", "ladger_head", "status", "mobile", "email", "address" };
            string[] hide = { "is_auto_genrated" };
            fun.gridcoulmns_settings(gridView2, editable, hide);

        }

        private void gridView2_RowUpdated(object sender, DevExpress.XtraGrid.Views.Base.RowObjectEventArgs e)
        {
            DataRow dr = gridView2.GetFocusedDataRow();
            if(dr != null)
            {
                string query = "";
                if (Convert.ToBoolean(dr["is_auto_genrated"]) == true)
                {
                    MessageBox.Show("you cannot change info of this ladger its auto generated","Ladger Info",MessageBoxButtons.OK,MessageBoxIcon.Information);
                    return;
                }
                else
                {
                    query = "UPDATE `ac_ledger` SET `title` = '" + dr["title"] + "',`head_id` = '" + dr["ladger_head"] + "',`status` = '" + dr["status"] + "',`mobile` = '" + dr["mobile"] + "',`email` = '" + dr["email"] + "',`address` = '" + dr["address"] + "' WHERE `id` = '" + dr["id"] + "'; ";
                    fun.ExecuteQuery(query);
                }
            }
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            if(ddl_ladger_head.EditValue == null || string.IsNullOrEmpty(txt_title.Text) || string.IsNullOrEmpty(txt_mobile.Text))
            {
                MessageBox.Show("Please add ladger head title and mobile fields data", "Error Info", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            string query = "INSERT INTO `ac_ledger` (`title`,`balance`,`head_id`,`status`,`mobile`,`email`,`address`) VALUES " +
                " ('"+txt_title.Text+"','"+txt_open_balance.Text+"','"+ddl_ladger_head.EditValue+"','1','"+txt_mobile.Text+"','"+txt_email.Text+"','"+txt_address.Text+"');";
            fun.Execute_Insert(query);
            load_ladgers();
        }
    }
}
