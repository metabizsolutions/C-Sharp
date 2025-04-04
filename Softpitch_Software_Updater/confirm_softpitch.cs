using System;
using System.Windows.Forms;

namespace Softpitch_Software_Updater
{
    public partial class confirm_softpitch : Form
    {
        public confirm_softpitch()
        {
            InitializeComponent();
        }
        public bool has_password { get; set; }
        private void btn_ok_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txt_password.Text))
            {
                MessageBox.Show("Please Write description ... other wise invoice cannot be delete", "Invoice Deleting Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            has_password = true;
            this.Close();
        }

        private void btn_cancle_Click(object sender, EventArgs e)
        {
            has_password = false;
            this.Close();
        }

        private void confirm_softpitch_Load(object sender, EventArgs e)
        {

        }
    }
}
