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
    public partial class deleted_invoices : UserControl
    {
        private static deleted_invoices _instance;

        public static deleted_invoices instance
        {
            get
            {
                if (_instance == null)
                    _instance = new deleted_invoices();
                return _instance;
            }
        }
        CommonFunctions fun = new CommonFunctions();
        public deleted_invoices()
        {
            InitializeComponent();
            loadfunctions();
        }
        public void loadfunctions()
        {
            loaddata();
        }
        public void loaddata()
        {
            string query = "select invoice_id,std.name as student,cls.name as class,sec.name as section,amount,amount_paid,due,reason,`session`  " +
                " from invoice_deleted " +
                " inner join student as std on std.student_id = invoice_deleted.student_id " +
                " left join class as cls on cls.class_id  = invoice_deleted.class_id " +
                " left join section as sec on sec.section_id = invoice_deleted.section_id " +
                " where `session` = '"+ Login.session_name+ "'";
            
            DataTable dt = fun.FetchDataTable(query);
            gridInvoiceManage.DataSource = dt;
        }
    }
}
