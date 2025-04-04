using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SchoolManagementSystem.Transport
{
    public partial class payment : Form
    {
        DataRow dr;
        CommonFunctions fun = new CommonFunctions();
        public payment(DataRow row)
        {
            InitializeComponent();
            fun.selectall_Controls(this);
            lblname.Text = row["name"].ToString();
            lblstop.Text = row["stop"].ToString();
            lbldue.Text = row["due"].ToString();
            dr = row;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (txtpayment.Value <= 0)
            {
                MessageBox.Show("insert payment");
                return;
            }
            string query = "UPDATE `transport_fee` SET `status`='1',`paid`=paid+'" + txtpayment.Value + "',`due`=due-'" + txtpayment.Value + "',`paiddate`='" + fun.CurrentDate() + "' WHERE `id`='" + dr["id"] + "'";
            fun.Execute_Query(query);
            this.Close();
        }
    }
}
