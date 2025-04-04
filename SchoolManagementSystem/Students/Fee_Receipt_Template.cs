using DevExpress.Snap.Core.API;
using DevExpress.XtraEditors;
using System;
using System.Data;
using System.Windows.Forms;

namespace SchoolManagementSystem.Students
{
    public partial class Fee_Receipt_Template : UserControl
    {
        CommonFunctions fun = new CommonFunctions();
        private static Fee_Receipt_Template _instance;

        public static Fee_Receipt_Template instance
        {
            get
            {
                if (_instance == null)
                    _instance = new Fee_Receipt_Template();
                return _instance;
            }
        }
        public Fee_Receipt_Template()
        {
            InitializeComponent();
            ri_ComboBox_type.EditValueChanged += Ri_ComboBox_type_EditValueChanged;

            //loadfunctions();
        }

        private void Ri_ComboBox_type_EditValueChanged(object sender, EventArgs e)
        {
            ComboBoxEdit cbe = ((ComboBoxEdit)sender);
            string type = cbe.Text + "_default.snx";
            snapControl1.LoadDocument(@".\Resources\" + type, SnapDocumentFormat.Snap);
            Receipts(type);
        }
        public static DataRow row;
        void Receipts(string Type)
        {
            if (Type == "fee_receipt_default.snx")
            {
                snapControl1.Document.DataSources.Clear();
                snapControl1.Document.DataSources.Add(new DataSourceInfo("Receipt", fun.showReportCash(row, true, "  Student Copy")));
            }
            else if (Type == "passout_card_default.snx")
            {
                snapControl1.Document.DataSources.Clear();
                snapControl1.Document.DataSources.Add(new DataSourceInfo("PassOut", fun.PassOutCard(row)));
            }
            else if (Type == "experience_card_default.snx")
            {
                snapControl1.Document.DataSources.Clear();
                snapControl1.Document.DataSources.Add(new DataSourceInfo("ExperienceLetter", fun.experience_letter(row)));
            }
            /*else if (Type == "student_card_default.snx")
            {
                snapControl1.Document.DataSources.Clear();
                snapControl1.Document.DataSources.Add(new DataSourceInfo("StudentCard", fun.std_card(row)));
            }*/
        }

        private void barEditItem_receipt_list_EditValueChanged(object sender, EventArgs e)
        {

        }
    }

}
