using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SchoolManagementSystem.Admin
{
    public partial class Compare_Database : Form
    {
        
        public Compare_Database()
        {
            fun.loaderform(() =>
            {
                InitializeComponent();
                loadorignaldb();
            });
        }
        
        CommonFunctions fun = new CommonFunctions();
        void loadorignaldb()
        {
            try
            {
                //string query = "show full tables where Table_Type = 'BASE TABLE';";
                //Livedb = fun.Compare_FetchDataTable(query, live_db);
                //gridControl1.DataSource = Livedb;
                //Localdb = fun.FetchDataTable(query);
                //gridControl2.DataSource = Localdb;

            }
            catch (Exception e)
            {
                MessageBox.Show(e.ToString(), "Compare DB All Tables");
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            { 
                    
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message, "info", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
        }
    }
}
