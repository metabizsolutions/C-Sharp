using DevExpress.XtraEditors.Repository;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace SchoolManagementSystem.Admin
{
    public partial class SMSServer : DevExpress.XtraEditors.XtraUserControl
    {
        ObservableCollection<AllClassSet> allServer = new ObservableCollection<AllClassSet>();
        CommonFunctions fun = new CommonFunctions();
        private static SMSServer _instance;
        public static SMSServer instance
        {
            get
            {
                if (_instance == null)
                    _instance = new SMSServer();
                return _instance;
            }
        }
        public SMSServer()
        {
            InitializeComponent();
            loadfunctions();
        }
        public void loadfunctions()
        {
            FillGridsmsServer();
        }
        public void permissions_btns(bool add, bool Edit, bool Delete)
        {
            gridView1.OptionsBehavior.Editable = false;
            if (Edit)
            {
                gridView1.OptionsBehavior.Editable = true;
            }
        }
        public void FillGridsmsServer()
        {
            allServer.Clear();
            List<Setting> myType = new List<Setting>
            {
                new Setting{type = "sms_server_url"},
                new Setting{type = "sms_server_token"},
                new Setting{type = "branded_sms"},
                new Setting{type = "absent_minute"}
            };
            MySqlConnection con = new MySqlConnection(Login.constring);

            for (int i = 0; i < myType.Count; i++)
            {
                con.Open();
                MySqlCommand cmd1 = new MySqlCommand("SELECT description FROM settings where type='" + myType[i].type.ToString() + "'", con);
                MySqlDataReader reader1 = cmd1.ExecuteReader();
                if (reader1.HasRows)
                {
                    while (reader1.Read())
                    {
                        AllClassSet d = new AllClassSet
                        {
                            Name = myType[i].type.ToString(),
                            Salary = reader1["description"].ToString()
                        };
                        allServer.Add(d);
                    }
                }
                con.Close();
            }
            gridsmsServer.DataSource = allServer;
            //  gridView1.BestFitColumns();

            var col0 = gridView1.Columns["Name"];
            col0.OptionsColumn.ReadOnly = true;
            col0.Caption = "Type";
            col0.Width = 15;
            var col1 = gridView1.Columns["Salary"];
            col1.Caption = "Description";
            gridView1.RowHeight = 30;
            RepositoryItemComboBox riComboDes = new RepositoryItemComboBox();
            riComboDes.Items.Add("Active");
            riComboDes.Items.Add("Disabled");
            gridView1.Columns["Salary"].ColumnEdit = riComboDes;
        }

        private void gridView1_RowUpdated(object sender, DevExpress.XtraGrid.Views.Base.RowObjectEventArgs e)
        {
            MySqlConnection con = new MySqlConnection(Login.constring);
            if (allServer.Count > 0)// && row[0].ToString() != null)
            {
                for (int i = 0; i < allServer.Count; i++)
                {
                    var item = allServer.FirstOrDefault(j => j.Name == Convert.ToString(allServer[i].Name));
                    if (item != null)
                    {
                        item.Salary = allServer[i].Salary;
                        con.Open();
                        MySqlCommand cmd = new MySqlCommand("UPDATE settings set description='" + allServer[i].Salary + "',sync='0' WHERE type='" + allServer[i].Name + "';", con);
                        cmd.ExecuteNonQuery();
                        con.Close();
                    }
                }
            }
            FillGridsmsServer();
        }
    }
}
