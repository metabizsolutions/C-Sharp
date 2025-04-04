using MySql.Data.MySqlClient;
using System;
using System.Data;
using System.Windows.Forms;

namespace SchoolManagementSystem
{
    public class SqlFunctions
    {
        static MySqlConnection con = new MySqlConnection(Login.constring);

        public static void SqlExecuteNonQuery(string query)
        {
            try
            {
                con.Open();
                MySqlCommand cmd = new MySqlCommand(query, con);
                cmd.ExecuteNonQuery();
                con.Close();
            }
            catch (Exception ex)
            {
                con.Close();
                MessageBox.Show(ex.Message, "Info");
            }

        }
        public static DataTable SqlExecuteDataAdapter(string query)
        {
            DataTable table = new DataTable();
            try
            {
                con.Open();
                MySqlCommand cmdP = new MySqlCommand(query, con);
                MySqlDataAdapter adp = new MySqlDataAdapter(cmdP);
                adp.Fill(table);
                con.Close();

            }
            catch (Exception ex)
            {
                con.Close();
                MessageBox.Show(ex.Message, "Info");
            }
            return table;
        }
    }
}
