using DevExpress.XtraEditors;
using SchoolManagementSystem.Fees;
using System.Windows.Forms;

namespace SchoolManagementSystem
{
    public partial class Take : DevExpress.XtraEditors.XtraForm
    {
        TakePayment modFTakePayment;

        public Take(System.Data.DataRow row)
        {
            modFTakePayment = new TakePayment(row);

            InitializeComponent();

        }
        public Take()
        {
            modFTakePayment = new TakePayment();

            InitializeComponent();

        }
        private void LoadModule(XtraUserControl UC, XtraUserControl module)
        {
            if (UC.Controls.Count > 0)
                UC.Controls.Clear();
            module.Dock = DockStyle.Fill;
            UC.Controls.Add(module);
        }

        private void Take_Load(object sender, System.EventArgs e)
        {
            
            if (this.Text == "Take Payment")
                LoadModule(UCTake, modFTakePayment);
        }
    }
}
