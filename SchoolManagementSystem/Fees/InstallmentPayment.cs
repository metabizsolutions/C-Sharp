using MySql.Data.MySqlClient;
using System;
using System.Collections.ObjectModel;
using System.Data;
using System.Media;
using System.Windows.Forms;

namespace SchoolManagementSystem.Fees
{
    public partial class InstallmentPayment : DevExpress.XtraEditors.XtraForm
    {
        CommonFunctions fun = new CommonFunctions();
        ObservableCollection<Move> move = new ObservableCollection<Move>();
        DataRow FocusRow;

        public InstallmentPayment()
        {
            InitializeComponent();
        }
        public InstallmentPayment(DataRow row)
        {
            InitializeComponent();
            FocusRow = row;
        }
        private void TakePayment_Load(object sender, System.EventArgs e)
        {
            loadcurrent();
        }
        void loadcurrent()
        {
            int due = Convert.ToInt32(FocusRow["Due"]);
            int paid = Convert.ToInt32(FocusRow["Paid"]);
            txtTotal.Text = FocusRow["Amount"].ToString();
            txtpaid.Text = paid.ToString();
            txtdue.Text = due.ToString();
            txtDate.Text = FocusRow["Date"].ToString();
            if(due == 0 || ValidateAmount())
            {
                btnTPAdd.Enabled = false; 
            }
            else
            {
                btnTPAdd.Enabled = true;
            }
            FillGridHistoryTakePayment();
            txtPayment.Text = "";
        }

        void FillGridHistoryTakePayment()
        {
            string query = "SELECT installment_id AS Id,amount,due,IF(status = 1 ,'paid','unpaid') AS status FROM fees_installment WHERE invoice_id = '{0}' ORDER BY installment_id ASC";
            query = string.Format(query, FocusRow[0].ToString());
            DataTable table = fun.FetchDataTable(query);
            gridTakePayment.DataSource = table;
            gridView1.BestFitColumns();
        }

        private void btnTPAdd_Click(object sender, EventArgs e)
        {
            if (txtPayment.Text != "" && txtDate.Text != "")
            {
                
                var date = txtDate.DateTime.ToString("yyyy-MM-dd");
                int amount = Convert.ToInt32(txtPayment.Text.ToString());
                int totalamount = InstallmentAmount()+ amount;
                if (totalamount <= Convert.ToInt32(FocusRow["Amount"]))
                {
                    var query = "INSERT INTO fees_installment SET invoice_id = '{0}',amount = '{1}',due = '{2}',`status` = '0'";
                    query = string.Format(query, FocusRow[0].ToString(), amount, date);
                    fun.ExecuteQuery(query);
                    FillGridHistoryTakePayment();
                    txtPayment.Text = "";
                }
                else
                {
                    MessageBox.Show("Installment Total amount should be Less or equal to Total Fees Amount");
                }
            }
            else
            {
                MessageBox.Show("Payment and Date is required! ", "Info");
                return;
            }
        }
        
        private void simpleButton1_Click(object sender, EventArgs e)
        {
            DataRow row = gridView1.GetDataRow(gridView1.FocusedRowHandle);
            if (row["status"].ToString() == "unpaid")
            {
                string query = "delete from fees_installment where installment_id = '{0}'";
                query = string.Format(query, row["ID"].ToString());
                fun.ExecuteQuery(query);
                FillGridHistoryTakePayment();
                int due = Convert.ToInt32(FocusRow["Due"]);
                if (due == 0 || ValidateAmount())
                {
                    btnTPAdd.Enabled = false;
                }
                else
                {
                    btnTPAdd.Enabled = true;
                }
            }
            else
            {
                MessageBox.Show("Only Unpaid Installment can be deleted!");
            }
        }

        private void InstallmentPayment_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (!ValidateAmount())
            {
                e.Cancel = true;
                MessageBox.Show("Installment Total amount should be equal to Total Fees Amount");
            }
           
            
        }

        public bool ValidateAmount() {
            int amount = InstallmentAmount();   
            if (amount > 0 && amount != Convert.ToInt32(FocusRow["Amount"]))
            {
                return false;
            }
            return true;
        }
        public int InstallmentAmount() {
            int amount = 0;
            string query = "select IFNULL(sum(amount),0) as amount from fees_installment where invoice_id = '{0}'";
            query = string.Format(query, FocusRow["ID"].ToString());
            DataTable table = fun.FetchDataTable(query);
            if (table.Rows.Count > 0)
            {
                 amount = Convert.ToInt32(table.Rows[0]["amount"]);
            }
            return amount;
        }
    }
}
