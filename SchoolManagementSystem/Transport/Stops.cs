using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SchoolManagementSystem.Transport
{
    public partial class Stops : DevExpress.XtraEditors.XtraUserControl
    {
        CommonFunctions fun = new CommonFunctions();
        private static Stops _instance;

        public static Stops instance
        {
            get
            {
                if (_instance == null)
                    _instance = new Stops();
                return _instance;
            }
        }
        public Stops()
        {
            InitializeComponent();
            loadstops();
        }

        private void btn_add_Click(object sender, EventArgs e)
        {
            if(txtstop_name.Text == "")
            {
                MessageBox.Show("please write stop name");
                return;
            }
            string query = "INSERT INTO `transport_stops`(`name`) VALUES ('"+txtstop_name.Text+"')";
            fun.ExecuteQuery(query);
            txtstop_name.Text = "";
            loadstops();
        }

        public void loadstops()
        {
            string query = "select * from transport_stops";
            gridControl1.DataSource = fun.FetchDataTable(query);
            
        }

        private void gridView1_RowUpdated(object sender, DevExpress.XtraGrid.Views.Base.RowObjectEventArgs e)
        {
            DataRow dr = gridView1.GetFocusedDataRow();
            string query = "UPDATE `transport_stops` SET name='"+dr["name"]+"',discription='"+dr["discription"] +"' WHERE id = '"+dr["id"]+"'";
            fun.ExecuteQuery(query);
            loadstops();
        }
    }
}
