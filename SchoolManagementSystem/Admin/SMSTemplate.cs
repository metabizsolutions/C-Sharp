using DevExpress.XtraEditors.Repository;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace SchoolManagementSystem.Admin
{
    public partial class SMSTemplate : DevExpress.XtraEditors.XtraUserControl
    {
        ObservableCollection<AllClassSet> allTemplate = new ObservableCollection<AllClassSet>();
        CommonFunctions fun = new CommonFunctions();
        private static SMSTemplate _instance;
        public static SMSTemplate instance
        {
            get
            {
                if (_instance == null)
                    _instance = new SMSTemplate();
                return _instance;
            }
        }
        public SMSTemplate()
        {
            InitializeComponent();
            loadfunctions();
        }
        public void loadfunctions()
        {
            FillGridsmsTemplate();
        }
        public void permissions_btns(bool add, bool Edit, bool Delete)
        {
            gridView1.OptionsBehavior.Editable = false;
            if (Edit)
            {
                gridView1.OptionsBehavior.Editable = true;
            }
        }
        public void FillGridsmsTemplate()
        {
            allTemplate.Clear();
            List<Setting> myType = new List<Setting>
            {
                new Setting{type = "in_sms"},
                new Setting{type = "out_sms"},
                new Setting{type = "absent_sms"},
                new Setting{type = "marks_sms"},
                new Setting{type = "paid_fees_sms"},
                new Setting{type = "unpaid_fees_sms"},
                new Setting{type = "allocation_sms"},
                new Setting{type = "principal_attendance_sms"},
                new Setting{type = "fee_note"},
                 new Setting{type = "lecture_assign_sms"},
                 new Setting{type = "today_feecollection_sms"},
                 new Setting{ type = "visitor_sms"}
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
                        allTemplate.Add(d);
                    }
                }
                con.Close();
            }
            gridsmsTemplate.DataSource = allTemplate;
            // gridView1.BestFitColumns();

            var col0 = gridView1.Columns["Name"];
            col0.OptionsColumn.ReadOnly = true;
            col0.Caption = "Type";
            col0.Width = 15;
            var col2 = gridView1.Columns["Salary"];
            col2.Caption = "Description";
            var col3 = gridView1.Columns["Salary"];
            RepositoryItemMemoEdit rimemo = new RepositoryItemMemoEdit();
            gridView1.Columns["Salary"].ColumnEdit = rimemo;
            gridView1.OptionsView.RowAutoHeight = true;
            gridView1.Appearance.Row.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            gridView1.RowHeight = 50;
        }

        private void gridView1_RowUpdated(object sender, DevExpress.XtraGrid.Views.Base.RowObjectEventArgs e)
        {
            MySqlConnection con = new MySqlConnection(Login.constring);
            if (allTemplate.Count > 0)// && row[0].ToString() != null)
            {
                for (int i = 0; i < allTemplate.Count; i++)
                {
                    var item = allTemplate.FirstOrDefault(j => j.Name == Convert.ToString(allTemplate[i].Name));
                    if (item != null)
                    {
                        item.Salary = allTemplate[i].Salary;
                        con.Open();
                        MySqlCommand cmd = new MySqlCommand("UPDATE settings set description='" + allTemplate[i].Salary + "',sync='0' WHERE type='" + allTemplate[i].Name + "';", con);
                        cmd.ExecuteNonQuery();
                        con.Close();
                    }
                }
            }
            FillGridsmsTemplate();
        }

    }
}
