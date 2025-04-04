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

namespace SchoolManagementSystem.Backup
{
    public partial class backup_form : DevExpress.XtraEditors.XtraForm
    {
        public backup_form()
        {
            InitializeComponent();
        }
        private void LoadModule(XtraUserControl UC, XtraUserControl module)
        {
            if (UC.Controls.Count > 0)
                UC.Controls.Clear();
            module.Dock = DockStyle.Fill;
            UC.Controls.Add(module);
        }

        private void backup_form_Load(object sender, EventArgs e)
        {
                LoadModule(UCTake, Backup.instance);
        }
    }
}
