using DevExpress.XtraEditors.Repository;
using DevExpress.XtraGrid;
using MySql.Data.MySqlClient;
using SchoolManagementSystem.Principal;
using System;
using System.Collections.ObjectModel;
using System.Data;
using System.Media;
using System.Windows.Forms;

namespace SchoolManagementSystem.Fees
{
    public partial class ExpenseManagement : DevExpress.XtraEditors.XtraUserControl
    {
        private static ExpenseManagement _instance;

        public static ExpenseManagement instance
        {
            get
            {
                if (_instance == null)
                    _instance = new ExpenseManagement();
                return _instance;
            }
        }
        ObservableCollection<AllClass> allClass = new ObservableCollection<AllClass>();
        CommonFunctions fun = new CommonFunctions();

        public ExpenseManagement()
        {
            InitializeComponent();
            loadfunctions();
        }
        public void loadfunctions()
        {
            fun.DateFormat(txtDate);
            allClass.Clear();
            allClass = fun.GetAllECategory();
            txtECategory.Properties.Items.Clear();
            foreach (var allclass in allClass)
                txtECategory.Properties.Items.Add(allclass.Name);
            DataTable table = fun.cashasset_list();
            if (table.Rows.Count > 0)
            {
                SpinnerCashAsset.Properties.DataSource = table;
                SpinnerCashAsset.Properties.DisplayMember = "title";
                SpinnerCashAsset.Properties.ValueMember = "cashasset_id";
            }
            txtDate.Text = DateTime.Now.ToShortDateString();
            FillGridExpanseManege();
        }
        public void permissions_btns(bool add, bool Edit, bool Delete)
        {
            btnEMAdd.Enabled = false;
            if (add)
            {
                btnEMAdd.Enabled = true;
            }
            gridView1.OptionsBehavior.Editable = false;
            if (Edit)
            {
                gridView1.OptionsBehavior.Editable = true;
            }
            BtnEMDelete.Enabled = false;
            if (Delete)
                BtnEMDelete.Enabled = true;
        }
        private void txtECategory_SelectedValueChanged(object sender, EventArgs e)
        {
            if (txtECategory.Text != "Other Income")
                labelControl2.Text = "Payable";
            else
                labelControl2.Text = "Receivable";
        }
        private void txtPayment_TextChanged(object sender, EventArgs e)
        {
            if (txtPayment.Text == "")
            {
                return;
            }
            try
            {
                if (txtPayment.Text != null)
                {
                    txtDue.Text = (Convert.ToInt32(txtTotal.Text) - Convert.ToInt32(txtPayment.Text)).ToString();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Info");
            }
        }

        private void btnEMAdd_Click(object sender, EventArgs e)
        {
            if (txtPayment.Text == "")
            {
                MessageBox.Show("Enter Payment.....", "Info");
                return;
            }
            var type = (txtECategory.Text == "Other Income") ? "Income" : "Expense";
            MySqlConnection con = new MySqlConnection(Login.constring);
            con.Open();
            try
            {
                var cashasset_id = (SpinnerCashAsset.Text == "" || SpinnerCashAsset.Text == "[EditValue is null]") ? 0 : SpinnerCashAsset.EditValue;
                MySqlCommand cmd = new MySqlCommand("INSERT into payment(expense_category_id,title,payment_type,cashasset_id,description,total,amount,dues,timestamp,date,user_id,sync) VALUES('" + fun.GetExpenseID(txtECategory.Text) + "','" + txtTitle.Text + "','" + type + "','" + cashasset_id + "','" + txtDes.Text + "','" + txtTotal.Text + "','" + txtPayment.Text + "','" + Convert.ToInt32(txtDue.Text) + "','" + fun.GetTimestamp(Convert.ToDateTime(txtDate.Text)) + "','" + Convert.ToDateTime(txtDate.Text).ToString("yyyy-MM-dd") + "','" + Login.CurrentUserID + "','0');", con);
                cmd.ExecuteNonQuery();
            }
            catch (MySqlException ex)
            {
                SystemSounds.Hand.Play();
                MessageBox.Show(ex.Message, "Error");
                return;
            }
            con.Close();
            FillGridExpanseManege();
            empty();
        }
        private void empty()
        {
            txtPayment.Text = "";
            txtDue.Text = "";
            txtTitle.Text = "";
            txtPayment.Text = "";
            txtECategory.Text = "";
            txtTotal.Text = "";
            txtDes.Text = "";
            txtDate.Text = "";
        }
        private void FillGridExpanseManege()
        {
            string query = "SELECT payment_id as ID,expense_category.name as Category,payment.title as Title,expense_category.type as PaymentType,cashasset_id as CashAsset, " +
                            " description as Description, if(total is null,0,total) as Total,amount as Paid, dues as Dues, date as Date, admin.name as User " +
                            " FROM payment " +
                            " inner join expense_category on(expense_category.expense_category_id = payment.expense_category_id) " +
                            " join admin on(admin.admin_id = payment.user_id) " +
                            " where month(date) = '" + DateTime.Now.Month + "'"; 
            
            DataTable table = fun.FetchDataTable(query);
            int totalincome = 0;
            int totalexpense = 0;
            int total = 0;
            string totalvalue = "";
            foreach (DataRow dr in table.Rows)
            {
                if (dr["PaymentType"].ToString() == "Expense")
                    totalexpense = totalexpense + Convert.ToInt32(dr["Paid"]);
                if (dr["PaymentType"].ToString() == "Income")
                    totalincome = totalincome + Convert.ToInt32(dr["Paid"]);
            }
            total = totalincome - totalexpense;
            /*if (total > 0)
                totalvalue = "Credit =" + total;
            else if (total < 0)
                totalvalue = "Debit =" + -(total);
            else
                totalvalue = "Equal =" + total;*/
            totalvalue = "Sum=" + total;
            gridInvoiceExpanse.DataSource = table;
            gridView1.BestFitColumns();
            var col = gridView1.Columns["ID"];
            col.OptionsColumn.ReadOnly = true;
            var col2 = gridView1.Columns["Date"];
            col2.OptionsColumn.ReadOnly = true;
            var col3 = gridView1.Columns["Total"];
            col3.OptionsColumn.ReadOnly = true;
            // this.gridView1.Columns[3].Visible = false;
            var col1 = gridView1.Columns["PaymentType"];
            col1.Group();
            gridView1.ExpandAllGroups();
            RepositoryItemComboBox riComboC = new RepositoryItemComboBox();
            allClass.Clear();
            allClass = fun.GetAllECategory();
            foreach (var allclass in allClass)
                riComboC.Items.Add(allclass.Name);
            gridView1.Columns["Category"].ColumnEdit = riComboC;

            RepositoryItemSearchLookUpEdit riSearch = new RepositoryItemSearchLookUpEdit();
            riSearch.DataSource = fun.cashasset_list();
            riSearch.ValueMember = "cashasset_id";
            riSearch.DisplayMember = "title";
            gridView1.Columns["CashAsset"].ColumnEdit = riSearch;

            gridView1.OptionsView.ShowFooter = true;
            gridView1.GroupSummary.Add(DevExpress.Data.SummaryItemType.Sum, "Paid", gridView1.Columns["Paid"]);
            GridColumnSummaryItem item1 = new GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Sum, "Paid", totalvalue);
            gridView1.Columns["Paid"].Summary.Clear();
            gridView1.Columns["Paid"].Summary.Add(item1);
        }

        private void gridView1_RowUpdated(object sender, DevExpress.XtraGrid.Views.Base.RowObjectEventArgs e)
        {
            DataRow row = gridView1.GetDataRow(gridView1.FocusedRowHandle);
            MySqlConnection con = new MySqlConnection(Login.constring);
            con.Open();
            MySqlCommand cmd = new MySqlCommand("UPDATE payment set expense_category_id='" + fun.GetExpenseID(row[1].ToString()) + "',title='" + row[2] + "',payment_type='" + row[3] + "',cashasset_id='" + row[4] + "' ,description='" + row[5] + "',amount='" + row[7] + "',dues='" + (Convert.ToInt32(row[6]) - Convert.ToInt32(row[7])) + "',date='" + Convert.ToDateTime(row[9]).ToString("yyyy-MM-dd") + "',user_id='" + Login.CurrentUserID + "',sync='0' WHERE payment_id='" + row[0] + "';", con);
            cmd.ExecuteNonQuery();
            con.Close();
            FillGridExpanseManege();
        }

        private void BtnEMDelete_Click(object sender, EventArgs e)
        {
            try
            {
                DataRow row = gridView1.GetDataRow(gridView1.FocusedRowHandle);
                MySqlConnection con = new MySqlConnection(Login.constring);
                con.Open();
                MySqlCommand cmdM = new MySqlCommand("DELETE from payment WHERE payment_id='" + row[0] + "';", con);
                cmdM.ExecuteNonQuery();
                con.Close();
                FillGridExpanseManege();
            }
            catch (Exception ex)
            {
                SystemSounds.Hand.Play();
                MessageBox.Show(ex.Message, "Info");
            }
        }

        private void btnAddHeads_Click(object sender, EventArgs e)
        {
            AddAccountHead aa = new AddAccountHead();
            aa.ShowDialog();
            allClass.Clear();
            txtECategory.Properties.Items.Clear();
            allClass = fun.GetAllECategory();
            foreach (var allclass in allClass)
                txtECategory.Properties.Items.Add(allclass.Name);
        }

        private void txtTotal_EditValueChanged(object sender, EventArgs e)
        {
            txtPayment.Text = txtTotal.Text;
        }
    }
}
