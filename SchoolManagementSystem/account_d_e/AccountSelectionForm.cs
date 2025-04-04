using System;
using System.Data;
using System.Windows.Controls;
using System.Windows.Forms;

namespace SchoolAccountingSystem
{
    public partial class AccountSelectionForm : Form
    {
        public int SelectedAccountId { get; private set; }
        public string SelectedAccountName { get; private set; }

        private DataTable ledgerAccounts;

        public AccountSelectionForm(DataTable accounts)
        {
            InitializeComponent();
            ledgerAccounts = accounts;
            InitializeAccountGrid();
        }

        private void InitializeAccountGrid()
        {
            dgvAccounts.DataSource = ledgerAccounts;
            dgvAccounts.Columns["id"].Visible = false;
            dgvAccounts.Columns["title"].HeaderText = "Account Name";
            dgvAccounts.Columns["code"].HeaderText = "Account Code";
        }

        private void btnSelect_Click(object sender, EventArgs e)
        {
            if (dgvAccounts.SelectedRows.Count > 0)
            {
                SelectedAccountId = Convert.ToInt32(dgvAccounts.SelectedRows[0].Cells["id"].Value);
                SelectedAccountName = dgvAccounts.SelectedRows[0].Cells["title"].Value.ToString();
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            else
            {
                MessageBox.Show("Please select an account");
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtSearch.Text))
            {
                ledgerAccounts.DefaultView.RowFilter = $"title LIKE '%{txtSearch.Text}%' OR code LIKE '%{txtSearch.Text}%'";
            }
            else
            {
                ledgerAccounts.DefaultView.RowFilter = string.Empty;
            }
        }
    }
}