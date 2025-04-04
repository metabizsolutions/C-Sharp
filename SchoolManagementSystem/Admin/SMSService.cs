using DevExpress.XtraEditors.Repository;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace SchoolManagementSystem.Admin
{
    public partial class SMSService : DevExpress.XtraEditors.XtraUserControl
    {
        CommonFunctions fun = new CommonFunctions();
        ObservableCollection<AllClassSet> allService = new ObservableCollection<AllClassSet>();
        private static SMSService _instance;
        public static SMSService instance
        {
            get
            {
                if (_instance == null)
                    _instance = new SMSService();
                return _instance;
            }
        }
        public SMSService()
        {
            InitializeComponent();
            loadfunctions();
        }
        public void loadfunctions()
        {
            FillGridsmsService();
        }
        public void permissions_btns(bool add, bool Edit, bool Delete)
        {
            gridView1.OptionsBehavior.Editable = false;
            if (Edit)
            {
                gridView1.OptionsBehavior.Editable = true;
            }
        }
        public void FillGridsmsService()
        {
            allService.Clear();
            List<Setting> myType = new List<Setting>
            {
                new Setting{type = "attendance_sms"},
                new Setting{type = "enable_in_sms"},
                new Setting{type = "enable_exit_sms"},
                new Setting{type = "enable_absent_sms"},
                new Setting{type = "enable_faculty_in_sms"},
                new Setting{type = "enable_faculty_exit_sms"},
                new Setting{type = "enable_faculty_absent_sms"},
                new Setting{type = "exam_sms"},
                new Setting{type = "fees_sms"},
                new Setting{type = "enotice_sms"},
                new Setting{type = "enable_allocation_sms"},
                new Setting{type = "sync_flag"},
                new Setting{type = "timetable_allocate_sms"},
                new Setting{type = "bell_flag"},
                new Setting{type = "enable_feecollection_sms"},
                new Setting{type = "enable_visitor_sms"}
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
                            Salary = reader1["description"].ToString() == "1" ? "Active" : "Disabled"
                        };
                        allService.Add(d);
                    }
                }
                con.Close();
            }
            gridsmsService.DataSource = allService;
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
            if (allService.Count > 0)
            {
                for (int i = 0; i < allService.Count; i++)
                {
                    var item = allService.FirstOrDefault(j => j.Name == Convert.ToString(allService[i].Name));
                    if (item != null)
                    {
                        item.Salary = allService[i].Salary;
                        con.Open();
                        string result = allService[i].Salary.ToString();
                        string a = result == "Active" ? "1" : "0";
                        MySqlCommand cmd = new MySqlCommand("UPDATE settings set description='" + a + "',sync='0' WHERE type='" + allService[i].Name + "';", con);
                        cmd.ExecuteNonQuery();
                        con.Close();
                    }
                }
            }
            FillGridsmsService();
        }
    }
}
