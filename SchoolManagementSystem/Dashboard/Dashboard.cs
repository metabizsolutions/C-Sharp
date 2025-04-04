using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.XtraEditors;

namespace SchoolManagementSystem.Dashboard
{
    public partial class Dashboard : DevExpress.XtraEditors.XtraUserControl
    {
        private static Dashboard _instance;

        public static Dashboard instance
        {
            get
            {
                if (_instance == null)
                    _instance = new Dashboard();
                return _instance;
            }
        }
        public Dashboard()
        {
            InitializeComponent();
            loadfunctions();
        }
        public void loadfunctions()
        {
            dashboardViewer1.ReloadData();
        
        }

        private void dashboardViewer1_Load(object sender, EventArgs e)
        {

        }
    }
}
