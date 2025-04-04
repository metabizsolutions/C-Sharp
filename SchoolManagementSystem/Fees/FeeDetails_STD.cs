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
    public partial class FeeDetails_STD : Form
    {

        public FeeDetails_STD(StudentFeeReport std, int studentid)
        {
            InitializeComponent();
            std.HidePanel();
            std.Dock = DockStyle.Fill;
            
            XUCMainform.Controls.Add(std);
            
        }
    }
}
