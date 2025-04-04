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
using MySql.Data.MySqlClient;
using System.Media;

namespace SchoolManagementSystem.Hostel
{
    public partial class Hostel : DevExpress.XtraEditors.XtraUserControl
    {
        private static Hostel _instance;

        public static Hostel instance
        {
            get
            {
                if (_instance == null)
                    _instance = new Hostel();
                return _instance;
            }
        }
        CommonFunctions fun = new CommonFunctions();
        public Hostel()
        {
            InitializeComponent();
            loadfunctions();
        }
        public void loadfunctions()
        {
            FillGridHostel();
        }
        public void permissions_btns(bool add, bool Edit, bool Delete)
        {
            btnHAdd.Enabled = false;
            if (add)
                btnHAdd.Enabled = true;
            gridView4.OptionsBehavior.Editable = false;
            if (Edit)
                gridView4.OptionsBehavior.Editable = true;
            btnHDelete.Enabled = false;
            if (Delete)
                btnHDelete.Enabled = true;
        }
        #region Hostel
        private void btnHAdd_Click(object sender, EventArgs e)
        {
            if (txtHName.Text == "")
            {
                MessageBox.Show("Please write Hostel Name", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            string query = "INSERT INTO `hostels`(`HostelName`, `Description`) VALUES ('"+txtHName.Text+"','"+txtHdes.Text+"')";
            fun.Execute_Insert(query);
            emptyH();
            FillGridHostel();
        }
        private void emptyH()
        {
            txtHName.Text = "";
            txtHdes.Text = "";
        }
        public void FillGridHostel()
        {
            string query = "SELECT * FROM `hostels`";
            DataTable table = fun.FetchDataTable(query);
            gridHostel.DataSource = table;
            gridView4.BestFitColumns();
            var col = gridView4.Columns["ID"];
            col.OptionsColumn.ReadOnly = true;
        }
        private void gridView4_RowUpdated(object sender, DevExpress.XtraGrid.Views.Base.RowObjectEventArgs e)
        {
            DataRow row = gridView4.GetDataRow(gridView4.FocusedRowHandle);
            string query = "UPDATE `hostels` SET `HostelName`='"+ row[1] + "',`Description`='"+ row[2] + "' WHERE ID="+ row[0] + "";
            fun.Execute_Query(query);
        }
        private void btnHDelete_Click(object sender, EventArgs e)
        {
            DataRow row = gridView4.GetDataRow(gridView4.FocusedRowHandle);
            if (MessageBox.Show("Do you want to delete hostel = '" + row[1] + "'? YES to Confirm. NO to Discard", "Confirmation Required", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                string query = "DELETE FROM `hostels` WHERE `ID` = '" + row[0] + "'";
                fun.Execute_Query(query);
            }
        }
        #endregion
    }
}
