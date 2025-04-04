using DevExpress.XtraGrid.Columns;
using DevExpress.XtraGrid.Views.Grid;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SchoolManagementSystem.account_d_e
{
    public partial class ledger_groups : UserControl
    {
        CommonFunctions fun = new CommonFunctions();
        private static ledger_groups _instance;

        public static ledger_groups instance
        {
            get
            {
                if (_instance == null)
                    _instance = new ledger_groups();
                return _instance;
            }
        }
        public ledger_groups()
        {
            InitializeComponent();
            loadfunctions();
        }
        public void loadfunctions()
        {
            fill_grid();
        }
        public void fill_grid()
        {
            string query = "select * from ac_ledger where level = 2 and status = 1";
            gridControl1.DataSource = fun.FetchDataTable(query);
            string[] edit_cols = { "title", "status" };
            string[] hide_cols = {};
            gridcoulmns_settings(gridView1, edit_cols, hide_cols);


        }

        public void gridcoulmns_settings(GridView gw,string[] editable,string[] hide)
        {
            foreach (GridColumn col in gw.Columns)
            {
                string c = col.FieldName;
                if(!editable.Contains(c))
                    gw.Columns[c].OptionsColumn.ReadOnly = true;
                if (hide.Contains(c))
                    gw.Columns[c].Visible = false;
            }
        }

        private void gridView1_RowUpdated(object sender, DevExpress.XtraGrid.Views.Base.RowObjectEventArgs e)
        {
            DataRow row = gridView1.GetDataRow(e.RowHandle);
            string query = "UPDATE `ac_ledger` SET `title` = '"+row["title"]+"',`status` = '"+row["status"]+"' WHERE `id` = '"+row["id"]+"';";
            fun.ExecuteQuery(query);
        }
    }
}
