using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SchoolManagementSystem.Hostel
{
    public partial class HostelRooms : UserControl
    {
        private static HostelRooms _instance;

        public static HostelRooms instance
        {
            get
            {
                if (_instance == null)
                    _instance = new HostelRooms();
                return _instance;
            }
        }
        CommonFunctions fun = new CommonFunctions();
        public HostelRooms()
        {
            InitializeComponent();
            loadfunctions();
        }
        public void loadfunctions()
        {
            HostelsList();
            FillGridHostelRooms();
        }
        public void permissions_btns(bool add, bool Edit, bool Delete)
        {
            btnHRAdd.Enabled = false;
            btncreateHostel.Enabled = false;
            if (add)
            {
                btnHRAdd.Enabled = true;
                btncreateHostel.Enabled = true;
            }
            gridView4.OptionsBehavior.Editable = false;
            if (Edit)
                gridView4.OptionsBehavior.Editable = true;
            btnHRDelete.Enabled = false;
            if (Delete)
                btnHRDelete.Enabled = true;
        }
        public void HostelsList()
        {
            string query = "SELECT * FROM hostels";
            DataTable hosdt = fun.FetchDataTable(query);
            GridHostelList.Properties.DataSource = hosdt;
            GridHostelList.Properties.DisplayMember = "HostelName";
            GridHostelList.Properties.ValueMember = "ID";
        }

        private void btnHRAdd_Click(object sender, EventArgs e)
        {
            if (GridHostelList.EditValue == null)
            {
                MessageBox.Show("Hostel is Require", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (txtHRName.Text == "")
            {
                MessageBox.Show("Room Name is Require", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (txtHRCapacity.Text == "")
            {
                MessageBox.Show("Room Capacity is Require", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            string query = "INSERT INTO `hostel_rooms`(`hostel_id`, `room`, `Capacity`, `Description`) VALUES " +
                "('" + GridHostelList.EditValue + "','" + txtHRName.Text + "','" + txtHRCapacity.Text + "','" + txtHRdes.Text + "')";
            fun.Execute_Insert(query);
            emptyH();
            FillGridHostelRooms();
        }

        private void emptyH()
        {
            txtHRName.Text = "";
            txtHRdes.Text = "";
            txtHRCapacity.Text ="";
            GridHostelList.EditValue = null;
        }

        public void FillGridHostelRooms()
        {
            string query = "SELECT hosr.ID,hos.HostelName,hosr.room,hosr.Capacity,hosr.Description FROM hostel_rooms as hosr "+
                            " join hostels as hos on hos.ID = hosr.hostel_id";
            DataTable table = fun.FetchDataTable(query);
            gridHostelRooms.DataSource = table;
            gridView4.BestFitColumns();
            var col = gridView4.Columns["ID"];
            col.OptionsColumn.ReadOnly = true;
            var col1 = gridView4.Columns["HostelName"];
            col1.OptionsColumn.ReadOnly = true;
        }

        private void gridView4_RowUpdated(object sender, DevExpress.XtraGrid.Views.Base.RowObjectEventArgs e)
        {
            DataRow row = gridView4.GetDataRow(gridView4.FocusedRowHandle);
            string query = "UPDATE `hostel_rooms` SET room='"+row["room"]+"',Capacity='"+row["Capacity"] + "',Description='" + row["Description"] + "' where ID = '" + row["ID"] + "' ";
            fun.Execute_Query(query);
        }

        private void btnHRDelete_Click(object sender, EventArgs e)
        {
            DataRow row = gridView4.GetDataRow(gridView4.FocusedRowHandle);
            if (MessageBox.Show("Do you want to delete hostel Room = '" + row["room"] + "'? YES to Confirm. NO to Discard", "Confirmation Required", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                string query = "DELETE FROM hostel_rooms WHERE ID = '" + row["ID"] + "'";
                fun.Execute_Query(query);
            }
        }

        private void btncreateHostel_Click(object sender, EventArgs e)
        {
            Hostelsform hos = new Hostelsform();
            hos.Show();
        }
    }

}
