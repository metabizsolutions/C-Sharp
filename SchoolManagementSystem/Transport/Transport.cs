using DevExpress.XtraEditors;
using MySql.Data.MySqlClient;
using System;
using System.Data;
using System.Windows.Forms;

namespace SchoolManagementSystem.Transport
{
    public partial class Transport : XtraUserControl
    {
        CommonFunctions fun = new CommonFunctions();
        private static Transport _instance;

        public static Transport instance
        {
            get
            {
                if (_instance == null)
                    _instance = new Transport();
                return _instance;
            }
        }
        public Transport()
        {
            InitializeComponent();
            loadfunctions();
        }
        public void loadfunctions()
        {
            FillGridTransport();
            loadrouts();
            loadstops();
        }
        #region Transport
        public void loadstops()
        {
            string query = "select * from transport_stops";
            DataTable stops = fun.FetchDataTable(query);
            txtstops.Properties.DataSource = stops;
            txtstops.Properties.DisplayMember = "name";
            txtstops.Properties.ValueMember = "id";
        }
        public void loadrouts()
        {
            string query = "select transport_id as id,route_name as name from transport";
            DataTable routs = fun.FetchDataTable(query);
            routcombo.Properties.DataSource = routs;
            routcombo.Properties.DisplayMember = "name";
            routcombo.Properties.ValueMember = "id";
        }
        private void LoadModule(XtraUserControl UC, XtraUserControl module)
        {
            if (UC.Controls.Count > 0)
                UC.Controls.Clear();
            module.Dock = DockStyle.Fill;
            UC.Controls.Add(module);
        }

        private void btnTAdd_Click(object sender, EventArgs e)
        {

            string query = "INSERT into transport(route_name,number_of_vehicle,description,route_fare,sync) VALUES" +
                            " ('" + txtTName.Text + "','" + txtTVehicle.Text + "','" + txtTDes.Text + "','" + txtTFare.Text + "','0');";
            long rout_id = fun.Execute_Insert(query);
            object val = txtstops.EditValue;
            FillGridTransport();
            loadrouts();
            emptyT();
        }

        private void emptyT()
        {
            txtTName.Text = "";
            txtTVehicle.Text = "";
            txtTFare.Text = "";
            txtTDes.Text = "";
        }
        public void FillGridTransport()
        {
            string query = "SELECT `transport_id` as ID, `route_name` as RouteName, `number_of_vehicle`, `description`,ts.name as stop, ts.discription as stopDiscription,trbs.order FROM `transport` as tra " +
                           " left join transport_rout_by_stop as trbs on trbs.rout_id = tra.`transport_id` " +
                           " left join transport_stops as ts on ts.id = trbs.stop_id";
            DataTable table = fun.FetchDataTable(query);
            gridTransport.DataSource = table;
            gridView3.BestFitColumns();
            var col = gridView3.Columns["ID"];
            col.OptionsColumn.ReadOnly = true;
            gridView3.Columns["RouteName"].Group();
        }
        private void gridView3_RowUpdated(object sender, DevExpress.XtraGrid.Views.Base.RowObjectEventArgs e)
        {

        }
        private void btnTDelete_Click(object sender, EventArgs e)
        {

        }
        #endregion

        private void btnstop_Click(object sender, EventArgs e)
        {
            string query = "select if(count(*) is null,0,count(*)) as val from transport_rout_by_stop where rout_id = '" + routcombo.EditValue + "'";
            object order = fun.Execute_Scaler_string(query);
            int or = Convert.ToInt32(order) + 1;
            query = "INSERT INTO `transport_rout_by_stop`(`rout_id`, `stop_id`, `order`) VALUES ('" + routcombo.EditValue + "','" + txtstops.EditValue + "','" + or + "');";
            fun.Execute_Query(query);
            FillGridTransport();

        }
    }
}
