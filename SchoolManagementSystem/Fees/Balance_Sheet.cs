using System;
using System.Data;
using System.Windows.Forms;
using System.Collections.ObjectModel;
using DevExpress.XtraEditors.Repository;
using DevExpress.XtraGrid;
using DevExpress.XtraReports.UI;
using DevExpress.LookAndFeel;
using System.Drawing;

namespace SchoolManagementSystem.Fees
{
    public partial class Balance_Sheet : UserControl
    {
        private static Balance_Sheet _instance;

        public static Balance_Sheet instance
        {
            get
            {
                if (_instance == null)
                    _instance = new Balance_Sheet();
                return _instance;
            }
        }
        ObservableCollection<AllClass> allClass = new ObservableCollection<AllClass>();
        CommonFunctions fun = new CommonFunctions();
        public Balance_Sheet()
        {
            InitializeComponent();
            loadfunctions();
        }

        public void loadfunctions()
        {
        }
        private void FillGridExpanseManege()
        {
            // Get date range
            string fromDate = dtp_Date.Value.ToString("yyyy-MM-dd");
            string toDate = to_date.Value.ToString("yyyy-MM-dd");
            int openingBalance = 0;

            // 1. Fetch Opening Balance before fromDate
            string query = @"
                SELECT IFNULL(SUM(IF(payment_type = 'Income',amount,amount*-1)), 0) AS OpeningBalance 
                FROM payment 
                WHERE payment_type IN('Income','Expense') AND `date` < '" + fromDate + "'";

            object op_bl = fun.Execute_Scaler_string(query);
            openingBalance = op_bl == null ? 0 : Convert.ToInt32(op_bl);

            // 2. Fetch Income and combine with Opening Balance
            query = @"
                SET @count := 0;
                SELECT @count := @count + 1 AS SR, 'Opening Balance' AS Title, " + openingBalance + @" AS Amount, '" + fromDate + @"' AS date
                UNION ALL
                SELECT @count := @count + 1 AS SR, ec.name AS Title, amount, payment.date
                FROM payment 
                LEFT JOIN expense_category ec ON ec.expense_category_id = payment.expense_category_id
                WHERE payment_type = 'Income' AND `date` BETWEEN '" + fromDate + @"' AND '" + toDate + @"'";

            DataTable incomeTable = fun.FetchDataTable(query);
            gridIncome.DataSource = incomeTable;

            int totalIncome = 0;
            foreach (DataRow dr in incomeTable.Rows)
            {
                totalIncome += Convert.ToInt32(dr["Amount"]);
            }

            // Add summary for total income
            GridColumnSummaryItem incomeSummary = new GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Sum, "Amount", totalIncome.ToString());
            gridView2.Columns["Amount"].Summary.Clear();
            gridView2.Columns["Amount"].Summary.Add(incomeSummary);

            // 3. Calculate Closing Balance
            int closingBalance =  totalIncome;

            // 4. Fetch Expenses and calculate total
            query = @"
                SET @count := 0;
                SELECT @count := @count + 1 AS SR, ec.name AS Title, amount AS Amount
                FROM payment 
                LEFT JOIN expense_category ec ON ec.expense_category_id = payment.expense_category_id
                WHERE payment_type = 'Expense' AND `date` BETWEEN '" + fromDate + @"' AND '" + toDate + @"'
                UNION ALL
                SELECT @count := @count + 1, 'Total Expense', IFNULL(SUM(amount), 0) AS Amount
                FROM payment 
                LEFT JOIN expense_category ec ON ec.expense_category_id = payment.expense_category_id
                WHERE payment_type = 'Expense' AND `date` BETWEEN '" + fromDate + @"' AND '" + toDate + @"'";

            DataTable expenseTable = fun.FetchDataTable(query);
            

            int totalExpense = 0;
            foreach (DataRow dr in expenseTable.Rows)
            {
                if (dr["Title"].ToString() == "Total Expense")
                    totalExpense = Convert.ToInt32(dr["Amount"]);
            }

            

            DataRow closingBalanceRow = expenseTable.NewRow(); 
            closingBalanceRow["SR"] = expenseTable.Rows.Count + 1; 
            closingBalanceRow["Title"] = "Closing Balance"; 
            closingBalanceRow["Amount"] = closingBalance - totalExpense;
            expenseTable.Rows.Add(closingBalanceRow);

            gridExpense.DataSource = expenseTable;
            // Add summary for final balance
            GridColumnSummaryItem expenseSummary = new GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Sum, "Amount", closingBalance.ToString());
            gridView1.Columns["Amount"].Summary.Clear();
            gridView1.Columns["Amount"].Summary.Add(expenseSummary);
        }



        private void btnprint_Click(object sender, EventArgs e)
        {
            DataTable expdt = gridExpense.DataSource as DataTable;
            DataTable incdt = gridIncome.DataSource as DataTable;
            balance_sheet_rpt rpt = new balance_sheet_rpt(incdt, expdt);
            //rpt.incomeGrid = gridIncome;
            //rpt.expenseGrid = gridExpense;
            rpt.LabAddress.Text = fun.GetSettings("address");
            rpt.LabTel.Text = fun.GetSettings("phone");
            Image logo = fun.Base64ToImage(Login.Logo);
            rpt.PicIogoBox.Image = logo;
            rpt.LabTitle.Text = fun.GetSettings("system_title");
            rpt.Landscape = false;
            rpt.CreateDocument(true);
            rpt.ShowPrintMarginsWarning = false;
            rpt.ShowPreview();
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            FillGridExpanseManege();
        }

    }
}
