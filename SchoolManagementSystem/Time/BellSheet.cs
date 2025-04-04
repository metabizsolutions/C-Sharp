using DevExpress.XtraEditors.Mask;
using DevExpress.XtraEditors.Repository;
using MySql.Data.MySqlClient;
using System;
using System.Collections.ObjectModel;
using System.Data;
using System.Media;
using System.Windows.Forms;

namespace SchoolManagementSystem.Time
{
    public partial class BellSheet : DevExpress.XtraEditors.XtraUserControl
    {
        private static BellSheet _instance;

        public static BellSheet instance
        {
            get
            {
                if (_instance == null)
                    _instance = new BellSheet();
                return _instance;
            }
        }
        CommonFunctions fun = new CommonFunctions();
        public BellSheet()
        {
            InitializeComponent();
            loadfunctions();
        }
        public void loadfunctions()
        {
            foreach (DayOfWeek day in Enum.GetValues(typeof(DayOfWeek)))
                txtDay.Properties.Items.Add(day);
            txtTime.Properties.Mask.MaskType = MaskType.DateTime;
            txtTime.Properties.Mask.EditMask = "HH:mm";
            txtTime.MaskBox.Mask.UseMaskAsDisplayFormat = true;
        }
        public void permissions_btns(bool add, bool Edit, bool Delete)
        {
            btnSave.Enabled = false;
            if (add)
            {
                btnSave.Enabled = true;
            }
            BtnFind.Enabled = false;
            if (Edit)
            {
                BtnFind.Enabled = true;
            }
            BtnPDelete.Enabled = false;
            if (Delete)
                BtnPDelete.Enabled = true;
        }
        private void TeacherProfile_Enter(object sender, EventArgs e)
        {
            fillBellTable();

        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            MySqlConnection con = new MySqlConnection(Login.constring);
            con.Open();
            try
            {
                var a = txtDay.Text;
                var info = a.Split(',');
                foreach (var day in info)
                {
                    MySqlCommand cmd = new MySqlCommand("INSERT into tbl_bell(day,time_slate,bell_type,sync) VALUES('" + day.Trim() + "','" + txtTime.Text + "','" + txtBillType.Text + "','0');", con);
                    cmd.ExecuteNonQuery();
                }
            }
            catch (MySqlException ex)
            {
                SystemSounds.Hand.Play();
                MessageBox.Show(ex.Message, "Error");
                return;
            }
            con.Close();
            FillGridBillSheet();
        }
        void FillGridBillSheet()
        {
            MySqlConnection con = new MySqlConnection(Login.constring);
            con.Open();
            MySqlCommand cmdP = new MySqlCommand("SELECT * FROM tbl_bell;", con);
            MySqlDataAdapter adp = new MySqlDataAdapter(cmdP);
            DataTable table = new DataTable();
            adp.Fill(table);
            gridBill.DataSource = null;
            gridView1.Columns.Clear();
            gridBill.DataSource = table;
            gridView1.BestFitColumns();

            gridView1.OptionsBehavior.Editable = true;
            var col = gridView1.Columns["bell_id"];
            col.OptionsColumn.ReadOnly = true;
            con.Close();

            RepositoryItemComboBox riComboD = new RepositoryItemComboBox();
            foreach (DayOfWeek day in Enum.GetValues(typeof(DayOfWeek)))
                riComboD.Items.Add(day);
            gridView1.Columns["day"].ColumnEdit = riComboD;

            RepositoryItemComboBox riComboT = new RepositoryItemComboBox();
            riComboT.Items.Add("Assembly");
            riComboT.Items.Add("Lecture");
            riComboT.Items.Add("Break");
            riComboT.Items.Add("Off");
            gridView1.Columns["bell_type"].ColumnEdit = riComboT;

        }

        private void gridView1_RowUpdated(object sender, DevExpress.XtraGrid.Views.Base.RowObjectEventArgs e)
        {
            DataRow row = gridView1.GetDataRow(gridView1.FocusedRowHandle);
            MySqlConnection con = new MySqlConnection(Login.constring);
            con.Open();
            MySqlCommand cmd = new MySqlCommand("UPDATE tbl_bell set day='" + row[1] + "',time_slate='" + row[2] + "',bell_type='" + row[3] + "',sync='0' WHERE bell_id='" + row[0] + "';", con);
            cmd.ExecuteNonQuery();
            con.Close();
            FillGridBillSheet();
        }

        private void BtnPDelete_Click(object sender, EventArgs e)
        {
            try
            {
                if (MessageBox.Show("Are you sure to delete?", "Confirm Delete!!", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    DataRow row = gridView1.GetDataRow(gridView1.FocusedRowHandle);
                    MySqlConnection con = new MySqlConnection(Login.constring);
                    con.Open();
                    MySqlCommand cmdM = new MySqlCommand("DELETE from tbl_bell WHERE bell_id='" + row[0] + "';", con);
                    cmdM.ExecuteNonQuery();
                    con.Close();
                    FillGridBillSheet();
                }
            }
            catch (Exception ex)
            {
                SystemSounds.Hand.Play();
                MessageBox.Show(ex.Message, "Info");
            }
        }

        private void btnPrint_Click(object sender, EventArgs e)
        {


        }

        private void BtnFind_Click(object sender, EventArgs e)
        {
            FillGridBillSheet();

        }
        ObservableCollection<AllValues> allValue;

        void fillBellTable()
        {
            allValue = new ObservableCollection<AllValues>();

            DataTable table = new DataTable();
            DataColumn[] cols ={
                       new DataColumn("Days",typeof(String)),
                   };
            table.Columns.AddRange(cols);

            allValue = fun.GetAllBellTimeSlate();
            foreach (var val in allValue)
            {
                DataColumn[] colsa ={
                      new DataColumn(val.Name,typeof(String)) };
                table.Columns.AddRange(colsa);
            }
            DataRow row = table.NewRow();
            foreach (DayOfWeek day in Enum.GetValues(typeof(DayOfWeek)))
            {
                MySqlConnection con2 = new MySqlConnection(Login.constring);
                con2.Open();
                MySqlCommand cmd3 = new MySqlCommand("SELECT * FROM tbl_bell;", con2);
                MySqlDataReader reader3 = cmd3.ExecuteReader();
                if (reader3.HasRows)
                {
                    while (reader3.Read())
                    {
                        int hasVal = 0;
                        var res = table.AsEnumerable().Where(r => r.Field<string>("Days") == reader3["Day"].ToString());
                        foreach (var re in res)
                        {
                            hasVal = 1;
                        }
                        if (hasVal == 0)
                        {
                            row = table.NewRow();
                            row["Days"] = reader3["Day"].ToString();
                            table.Rows.Add(row);
                        }
                        var rowsToUpdate = table.AsEnumerable().Where(r => r.Field<string>("Days") == reader3["Day"].ToString());

                        foreach (var rows in rowsToUpdate)
                        {
                            foreach (var val in allValue)
                            {
                                string field = val.Name;
                                if (field == reader3["time_slate"].ToString())
                                {
                                    rows.SetField(field, reader3["bell_type"].ToString());
                                }
                            }
                        }
                        hasVal = 0;
                    }
                }
                con2.Close();

            }
            gridBill.DataSource = null;
            gridView1.Columns.Clear();
            gridBill.DataSource = table;
            gridView1.OptionsBehavior.Editable = false;
            gridView1.OptionsView.RowAutoHeight = true;
            gridView1.Appearance.Row.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            gridView1.RowHeight = 35;
        }
    }
}
