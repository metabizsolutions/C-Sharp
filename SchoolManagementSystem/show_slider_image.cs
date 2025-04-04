using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SchoolManagementSystem
{
    public partial class show_slider_image : Form
    {
        public show_slider_image()
        {
            InitializeComponent();
        }

        private void pictureBox1_MouseLeave(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
