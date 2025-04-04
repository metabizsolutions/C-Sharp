// MainTransactionForm.cs
using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace SchoolAccountingSystem
{
    public partial class MainTransactionForm : Form
    {
        private MySqlConnection connection;
        private DataTable ledgerAccounts;

        public MainTransactionForm()
        {
            InitializeComponent();
            InitializeDatabaseConnection();
            LoadLedgerAccounts();
            InitializeVoucherTypes();
            InitializeForm();
        }

        private void InitializeDatabaseConnection()
        {
            string connectionString = "Server=localhost;Database=tnsbay_school;Uid=root;Pwd=;";
            connection = new MySqlConnection(connectionString);
        }

        private void LoadLedgerAccounts()
        {
            try
            {
                connection.Open();
                string query = "SELECT id, title, code FROM ac_ledger WHERE status = 1 ORDER BY code";
                MySqlDataAdapter adapter = new MySqlDataAdapter(query, connection);
                ledgerAccounts = new DataTable();
                adapter.Fill(ledgerAccounts);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading ledger accounts: " + ex.Message);
            }
            finally
            {
                if (connection.State == ConnectionState.Open)
                    connection.Close();
            }
        }

        private void InitializeVoucherTypes()
        {
            try
            {
                connection.Open();
                string query = "SELECT voucher_type_id, title FROM ac_voucher_types WHERE status = 1";
                MySqlDataAdapter adapter = new MySqlDataAdapter(query, connection);
                DataTable voucherTypes = new DataTable();
                adapter.Fill(voucherTypes);

                cmbVoucherType.DataSource = voucherTypes;
                cmbVoucherType.DisplayMember = "title";
                cmbVoucherType.ValueMember = "voucher_type_id";
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading voucher types: " + ex.Message);
            }
            finally
            {
                if (connection.State == ConnectionState.Open)
                    connection.Close();
            }
        }

        private void InitializeForm()
        {
            dtpTransactionDate.Value = DateTime.Now;

            // Initialize entry grid columns
            DataGridViewTextBoxColumn accountIdColumn = new DataGridViewTextBoxColumn();
            accountIdColumn.Name = "AccountId";
            accountIdColumn.HeaderText = "Account ID";
            accountIdColumn.Visible = false;
            dgvEntries.Columns.Add(accountIdColumn);

            DataGridViewTextBoxColumn accountNameColumn = new DataGridViewTextBoxColumn();
            accountNameColumn.Name = "AccountName";
            accountNameColumn.HeaderText = "Account";
            accountNameColumn.ReadOnly = true;
            dgvEntries.Columns.Add(accountNameColumn);

            DataGridViewComboBoxColumn entryTypeColumn = new DataGridViewComboBoxColumn();
            entryTypeColumn.Name = "EntryType";
            entryTypeColumn.HeaderText = "Type";
            entryTypeColumn.DataSource = new string[] { "Debit", "Credit" };
            dgvEntries.Columns.Add(entryTypeColumn);

            DataGridViewTextBoxColumn amountColumn = new DataGridViewTextBoxColumn();
            amountColumn.Name = "Amount";
            amountColumn.HeaderText = "Amount";
            dgvEntries.Columns.Add(amountColumn);

            DataGridViewTextBoxColumn narrationColumn = new DataGridViewTextBoxColumn();
            narrationColumn.Name = "Narration";
            narrationColumn.HeaderText = "Narration";
            dgvEntries.Columns.Add(narrationColumn);
        }

        private void btnAddEntry_Click(object sender, EventArgs e)
        {
            using (var accountForm = new AccountSelectionForm(ledgerAccounts))
            {
                if (accountForm.ShowDialog() == DialogResult.OK)
                {
                    int rowIndex = dgvEntries.Rows.Add();
                    dgvEntries.Rows[rowIndex].Cells["AccountId"].Value = accountForm.SelectedAccountId;
                    dgvEntries.Rows[rowIndex].Cells["AccountName"].Value = accountForm.SelectedAccountName;
                    dgvEntries.Rows[rowIndex].Cells["EntryType"].Value = "Debit";
                    dgvEntries.Rows[rowIndex].Cells["Amount"].Value = 0.00m;
                    dgvEntries.Rows[rowIndex].Cells["Narration"].Value = string.Empty;
                }
            }
        }

        private void btnSaveTransaction_Click(object sender, EventArgs e)
        {
            if (dgvEntries.Rows.Count < 2)
            {
                MessageBox.Show("A transaction must have at least two entries");
                return;
            }

            decimal totalDebits = 0;
            decimal totalCredits = 0;

            foreach (DataGridViewRow row in dgvEntries.Rows)
            {
                if (row.IsNewRow) continue;

                if (row.Cells["EntryType"].Value?.ToString() == "Debit")
                {
                    if (!decimal.TryParse(row.Cells["Amount"].Value?.ToString(), out decimal debitAmount))
                    {
                        MessageBox.Show("Invalid amount in row " + (row.Index + 1));
                        return;
                    }
                    totalDebits += debitAmount;
                }
                else
                {
                    if (!decimal.TryParse(row.Cells["Amount"].Value?.ToString(), out decimal creditAmount))
                    {
                        MessageBox.Show("Invalid amount in row " + (row.Index + 1));
                        return;
                    }
                    totalCredits += creditAmount;
                }
            }

            if (totalDebits != totalCredits)
            {
                MessageBox.Show($"Transaction is not balanced. Debits: {totalDebits:N2}, Credits: {totalCredits:N2}");
                return;
            }

            try
            {
                connection.Open();
                MySqlTransaction mysqlTransaction = connection.BeginTransaction();

                try
                {
                    // Insert transaction header
                    string transactionQuery = @"
                        INSERT INTO ac_transaction 
                        (voucher_type, description, ondate, user_id, total, paid, payment_type, narration)
                        VALUES 
                        (@voucherType, @description, @ondate, @userId, @total, @paid, @paymentType, @narration);
                        SELECT LAST_INSERT_ID();";

                    MySqlCommand transactionCmd = new MySqlCommand(transactionQuery, connection, mysqlTransaction);
                    transactionCmd.Parameters.AddWithValue("@voucherType", cmbVoucherType.SelectedValue);
                    transactionCmd.Parameters.AddWithValue("@description", txtDescription.Text);
                    transactionCmd.Parameters.AddWithValue("@ondate", (int)(dtpTransactionDate.Value - new DateTime(1970, 1, 1)).TotalSeconds);
                    transactionCmd.Parameters.AddWithValue("@userId", 1); // Current user ID
                    transactionCmd.Parameters.AddWithValue("@total", totalDebits);
                    transactionCmd.Parameters.AddWithValue("@paid", totalDebits);
                    transactionCmd.Parameters.AddWithValue("@paymentType", "cash"); // Default
                    transactionCmd.Parameters.AddWithValue("@narration", txtNarration.Text);

                    int transactionId = Convert.ToInt32(transactionCmd.ExecuteScalar());

                    // Insert entries
                    foreach (DataGridViewRow row in dgvEntries.Rows)
                    {
                        if (row.IsNewRow) continue;

                        string entryQuery = @"
                            INSERT INTO ac_transaction_entries 
                            (transaction_id, account_id, entry_type, amount, ondate)
                            VALUES 
                            (@transactionId, @accountId, @entryType, @amount, @ondate)";

                        MySqlCommand entryCmd = new MySqlCommand(entryQuery, connection, mysqlTransaction);
                        entryCmd.Parameters.AddWithValue("@transactionId", transactionId);
                        entryCmd.Parameters.AddWithValue("@accountId", row.Cells["AccountId"].Value);
                        entryCmd.Parameters.AddWithValue("@entryType", row.Cells["EntryType"].Value.ToString().ToLower());
                        entryCmd.Parameters.AddWithValue("@amount", row.Cells["Amount"].Value);
                        entryCmd.Parameters.AddWithValue("@ondate", (int)(dtpTransactionDate.Value - new DateTime(1970, 1, 1)).TotalSeconds);

                        entryCmd.ExecuteNonQuery();
                    }

                    mysqlTransaction.Commit();
                    MessageBox.Show("Transaction saved successfully!");
                    ClearForm();
                }
                catch (Exception ex)
                {
                    mysqlTransaction.Rollback();
                    MessageBox.Show("Error saving transaction: " + ex.Message);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Database error: " + ex.Message);
            }
            finally
            {
                if (connection.State == ConnectionState.Open)
                    connection.Close();
            }
        }

        private void ClearForm()
        {
            dgvEntries.Rows.Clear();
            txtDescription.Clear();
            txtNarration.Clear();
            dtpTransactionDate.Value = DateTime.Now;
        }

        private void dgvEntries_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            // Validate amount when edited
            if (dgvEntries.Columns[e.ColumnIndex].Name == "Amount")
            {
                if (!decimal.TryParse(dgvEntries.Rows[e.RowIndex].Cells["Amount"].Value?.ToString(), out _))
                {
                    MessageBox.Show("Please enter a valid amount");
                    dgvEntries.Rows[e.RowIndex].Cells["Amount"].Value = 0.00m;
                }
            }
        }
    }
}