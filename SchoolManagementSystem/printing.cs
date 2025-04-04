using DevExpress.XtraEditors;
using SchoolManagementSystem.Class;
using SchoolManagementSystem.Principal;
using System.Windows.Forms;

namespace SchoolManagementSystem
{
    public partial class printing : DevExpress.XtraEditors.XtraForm
    {
        CommonFunctions fun = new CommonFunctions();
        PDashboard pd;
        Holidays ad;
        test tt;
        public printing()
        {
            InitializeComponent();
            pd = new PDashboard();
            ad = new Holidays();
            tt = new test();
            LoadModule(xtraUserControl1, tt);
        }
        private void LoadModule(XtraUserControl UC, XtraUserControl module)
        {
            if (UC.Controls.Count > 0)
                UC.Controls.Clear();
            module.Dock = DockStyle.Fill;
            UC.Controls.Add(module);
        }

    }
}