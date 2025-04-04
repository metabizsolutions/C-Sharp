using MySql.Data.MySqlClient;
using System;
using System.Collections.ObjectModel;
using System.Data;
using System.Media;
using System.Windows.Forms;

namespace SchoolManagementSystem.Fees
{
    public partial class InstallmentPaymentBulk : DevExpress.XtraEditors.XtraForm
    {
        CommonFunctions fun = new CommonFunctions();
        ObservableCollection<PreviousFee>  PfeeCard = new ObservableCollection<PreviousFee>();
        DataRow[] select_invoice;

        public InstallmentPaymentBulk()
        {
            InitializeComponent();
        }
        public InstallmentPaymentBulk(DataRow[] invoice_array)
        {
            InitializeComponent();
            select_invoice = invoice_array;
        }
        private void TakePayment_Load(object sender, System.EventArgs e)
        {
            loadcurrent();
        }
        void loadcurrent()
        {
            FillGridHistoryTakePayment();
            txtPayment.Text = "";
        }

        void FillGridHistoryTakePayment()
        {
            DataTable table = new DataTable();
            table.Columns.Add("DueDate");
            table.Columns.Add("Amount%");
            foreach (PreviousFee fee in PfeeCard)
            {
                table.Rows.Add(fee.Date, fee.Amount);
            }
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
                if (totalamount <= 100)
                {
                    PfeeCard.Add(new PreviousFee { Amount = amount.ToString(), Date = date });
                    loadcurrent();
                }
                else
                {
                    MessageBox.Show("Installment percent should be Less or equal to 100%");
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
            try
            {
                PfeeCard.RemoveAt(gridView1.FocusedRowHandle);
                FillGridHistoryTakePayment();
            }
            catch(Exception ep)
            {
                MessageBox.Show("Only selected Installment can be deleted!");
            }
        }

        private void InstallmentPayment_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (PfeeCard.Count > 0 && !ValidateAmount())
            {
                e.Cancel = true;
                MessageBox.Show("Installment Total amount should be equal to 100 %");
            }
        }

        public bool ValidateAmount() {
            int amount = InstallmentAmount();   
            if (amount != 100)
            {
                return false;
            }
            return true;
        }
        public int InstallmentAmount() {
            int amount = 0;
            foreach (PreviousFee fee in PfeeCard)
            {
                amount += Convert.ToInt32(fee.Amount);
            }
            return amount;
        }

        private void btnCreateBulkInstallment_Click(object sender, EventArgs e)
        {
            if (ValidateAmount())
            {
                DataTable message_array = new DataTable();
                message_array.Columns.Add("Status");
                message_array.Columns.Add("Message");

                int amount;
                String date;
                String query = "INSERT INTO fees_installment SET invoice_id = '{0}',amount = '{1}',due = '{2}',`status` = '0'";
                String delete_query = "DELETE FROM fees_installment WHERE invoice_id = '{0}'";
                String sql_insert, sql_delete;
                int total_amount;
                foreach (DataRow row in select_invoice)
                {
                    if (Convert.ToInt32(row["Paid"]) == 0)
                    {
                        total_amount = Convert.ToInt32(row["Amount"]);
                        sql_delete = string.Format(delete_query, row[0].ToString());
                        fun.ExecuteQuery(sql_delete);
                        foreach (PreviousFee fee in PfeeCard)
                        {
                            amount = Convert.ToInt32(0.01*total_amount*Convert.ToInt32(fee.Amount));
                            date = fee.Date;
                            sql_insert = string.Format(query, row[0].ToString(), amount, date);
                            fun.ExecuteQuery(sql_insert);
                        }
                        message_array.Rows.Add("success", row["ID"] + ">" + row["Name"] + " Created ");
                    }
                    else
                    {
                        message_array.Rows.Add("failed",row["ID"]+">"+ row["Name"]+ " already has amount paid");
                    }
                }
                gridView1.Columns.Clear();
                gridTakePayment.DataSource = null;
                gridTakePayment.DataSource = message_array;
                gridView1.BestFitColumns();
            }
            else 
            {
                MessageBox.Show("Installment Total amount should be equal to 100 %");
            }
        }
    }
}
