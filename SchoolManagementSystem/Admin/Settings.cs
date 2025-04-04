using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using SchoolManagementSystem.Fees;
using SchoolManagementSystem.Exam;
using SchoolManagementSystem.Class;

namespace SchoolManagementSystem.Admin
{
    public partial class Settings : DevExpress.XtraEditors.XtraForm
    {
        public delegate void comboselct();
        public comboselct cobselect;
        public Settings()
        {
            InitializeComponent();
        }
        private void Settings_Leave(object sender, EventArgs e)
        {
            cobselect();
        }

        private void PSettings_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
