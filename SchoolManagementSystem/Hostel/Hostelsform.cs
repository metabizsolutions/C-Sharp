using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SchoolManagementSystem.Hostel
{
    public partial class Hostelsform : DevExpress.XtraEditors.XtraForm
    {
        CommonFunctions fun = new CommonFunctions();
        Hostel objhostel = new Hostel();
        public Hostelsform()
        {
            InitializeComponent();
            
            if (XUCHosForm.Controls.Count > 0)
                XUCHosForm.Controls.Clear();
            objhostel.Dock = DockStyle.Fill;
            XUCHosForm.Controls.Add(objhostel);
        }

        private void Hostelsform_FormClosed(object sender, FormClosedEventArgs e)
        {
            //Hostel.instance.Dispose();
        }
    }
}
