using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SchoolManagementSystem.Fees
{
    public partial class invoice_delete_from : Form
    {
        public bool is_delete { get; set; }
        public invoice_delete_from()
        {
            InitializeComponent();
        }

        private void btn_yes_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txt_description.Text))
            {
                MessageBox.Show("Please Write description ... other wise invoice cannot be delete", "Invoice Deleting Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            is_delete = true;
            this.Close();
        }

        private void btn_no_Click(object sender, EventArgs e)
        {
            is_delete = false;
            this.Close();
        }
    }
}
