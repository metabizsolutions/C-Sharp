using MySql.Data.MySqlClient;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Data;
using System.Data.SQLite;
using System.Net;
using System.Text;

namespace SchoolManagementSystem
{
    class Sync
    {
        List<syncArray> SyncDataAdapter = new List<syncArray>();
        MySqlConnection con = new MySqlConnection(Login.constring);
        SQLiteConnection conSQL = new SQLiteConnection(Login.constringSQLite);

        MySqlCommand cmd;
        MySqlDataAdapter adp;
        DataTable table;
        syncArray values;

        public void sync(string School_Code, string Api_Key, string Api_Url)
        {
            var delete_data = sync_table_detele();
            var sync_data = sync_table();
            if (delete_data != "[]" || sync_data != "[]")
            {
                Main_FD a = new Main_FD();
                var response = Post(Api_Url + "authentication", new NameValueCollection() {
                                         { "school_code", School_Code },
                                         { "api_key", Api_Key}  });
                var Auth_result = parse(response);
                var result = Auth_result;
                if (sync_data != "[]")
                {
                    if (Convert.ToInt32(Auth_result["success"]) == 1)
                    {
                        response = Post(Api_Url + "sync_data", new NameValueCollection() {
                                         { "school_code", School_Code },
                                         { "api_key", Api_Key },
                                         { "api_token", Auth_result["api_token"].ToString() },
                                         { "table_data", sync_data}  });
                        result = parse(response);
                        var result_t = result["table"];
                        foreach (var val in result_t)
                        {
                            var res = val["success"];
                            foreach (var valID in res)
                            {
                                //a.labStatus.Text += "Sync " + val["table"] + "->" + val["key"] + "where Values is" + valID + "\r\n";
                                con.Open();
                                var query = "UPDATE " + val["table"] + " set sync='1' WHERE " + val["key"] + "='" + valID + "';";
                                cmd = new MySqlCommand(query, con);
                                cmd.ExecuteNonQuery();
                                con.Close();
                            }
                        }
                    }
                }
                if (delete_data != "[]")
                {
                    response = Post(Api_Url + "sync_delete", new NameValueCollection() {
                                         { "school_code", School_Code },
                                         { "api_key", Api_Key },
                                         { "api_token", Auth_result["api_token"].ToString() },
                                         { "table_data", delete_data}  });
                    result = parse(response);
                    var result_t = result["table"];
                    foreach (var val in result_t)
                    {
                        //a.labStatus.Text += "Sync Delete from tblLog ID where Values is" + val + "\r\n";

                        con.Open();
                        var query = "DELETE FROM tblLog WHERE ID='" + val + "';";
                        cmd = new MySqlCommand(query, con);
                        cmd.ExecuteNonQuery();
                        con.Close();
                    }
                }

            }
        }

        public static byte[] Post(string uri, NameValueCollection pairs)
        {
            byte[] response = null;
           
            return response;
        }

        public static JObject parse(byte[] json)
        {
            string jsonStr = Encoding.UTF8.GetString(json);
            return JObject.Parse(jsonStr);
        }

        public string sync_table()
        {
            SyncDataAdapter.Clear();
            List<string> tables = new List<string>();
            tables.Add("admin");
            tables.Add("teacher");
            tables.Add("class");
            tables.Add("class_fees");
            tables.Add("exam");
            tables.Add("expense_category");
            tables.Add("grade");
            tables.Add("invoice");
            tables.Add("mark");
            tables.Add("noticeboard");
            tables.Add("parent");
            tables.Add("payment");
            tables.Add("salary");
            tables.Add("section");
            tables.Add("student");
            tables.Add("subject");
            tables.Add("tbl_mark_subject");
            tables.Add("session");
            tables.Add("attendance");
            tables.Add("extra_lecture");
            tables.Add("floor_sheet");
            tables.Add("time_table");
            tables.Add("time_teacher");
            tables.Add("time_table");
            foreach (var tbl in tables)
            {
                con.Open();
                cmd = new MySqlCommand("SELECT * from " + tbl + " where sync =0 or sync is null limit 1500", con);
                adp = new MySqlDataAdapter(cmd);
                table = new DataTable();
                adp.Fill(table);
                con.Close();
                var count = table.Rows.Count;
                if (count > 0)
                {

                    values = new syncArray
                    {
                        Table = tbl,
                        Json = table
                    };
                    SyncDataAdapter.Add(values);
                }
            }
            string Syncjson = JsonConvert.SerializeObject(SyncDataAdapter); //MessageBox.Show(json);
            Syncjson = Syncjson.Replace("\\", "");

            return Syncjson;
        }

        public void SyncDelete(string table, string key, string val)
        {
            con.Open();
            cmd = new MySqlCommand("insert into tbllog (TableName,RowKey ,RowVal) values ('" + table + "','" + key + "', '" + val + "');", con);
            cmd.ExecuteNonQuery();
            con.Close();
        }
        public void CheckDeleteRecord(string School_Code, string Api_Key, string Api_Url)
        {
            var response = Post(Api_Url + "authentication", new NameValueCollection() {
                                         { "school_code", School_Code },
                                         { "api_key", Api_Key}  });
            var result = parse(response);
            var data = sync_table_detele();
            data = "";
            if (Convert.ToInt32(result["success"]) == 1)
            {
                response = Post(Api_Url + "sync_date", new NameValueCollection() {
                                         { "school_code", School_Code },
                                         { "api_key", Api_Key },
                                         { "api_token", result["api_token"].ToString() },
                                         { "table_data", data}  });
                result = parse(response);
                var result_t = result["table"];
                foreach (var val in result_t)
                {
                    var res = val["success"];
                    foreach (var valID in res)
                    {
                        con.Open();
                        var query = "UPDATE " + val["table"] + " set sync='1' WHERE " + val["key"] + "='" + valID + "';";
                        cmd = new MySqlCommand(query, con);
                        cmd.ExecuteNonQuery();
                        con.Close();
                    }
                    SQLiteCommand cmdSQL1 = new SQLiteCommand("delete from tblLog;", conSQL);
                    cmdSQL1.ExecuteNonQuery();
                }
            }
        }
        string sync_table_detele()
        {
            SyncDataAdapter.Clear();
            con.Open();
            cmd = new MySqlCommand("select * from tbllog", con);
            adp = new MySqlDataAdapter(cmd);
            table = new DataTable();
            adp.Fill(table);
            con.Close();
            var count = table.Rows.Count;
            if (count > 0)
            {
                values = new syncArray
                {
                    Table = "tbllog",
                    Json = table
                };
                SyncDataAdapter.Add(values);
            }
            string Syncjson = JsonConvert.SerializeObject(SyncDataAdapter); //MessageBox.Show(json);
            Syncjson = Syncjson.Replace("\\", "");
            return Syncjson;
        }

    }
}
