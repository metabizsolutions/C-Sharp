using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;

namespace SchoolManagementSystem.Principal
{
    public partial class test : DevExpress.XtraEditors.XtraUserControl
    {
        Stack<Attend> dataview = new Stack<Attend>();

        public test()
        {
            InitializeComponent();
            FillGridCustomer();
        }
        void FillGridCustomer()
        {
            dataview.Clear();
            MySqlConnection con = new MySqlConnection(Login.constring);
            con.Open();
            var qry = "SELECT concat( FORMAT(COUNT(attendance.student_id), 0),'/',FORMAT(count(*),0)) AS Present FROM student join class on class.class_id=student.class_id left JOIN attendance ON attendance.student_id= student.student_id and attendance.status= 1 and attendance.date= current_date()";
            MySqlCommand cmdV = new MySqlCommand(qry, con);
            MySqlDataReader reader1 = cmdV.ExecuteReader();
            if (reader1.HasRows)
            {
                while (reader1.Read())
                {
                    Attend D = new Attend
                    {
                        key = "Total",
                        val = Convert.ToString(reader1["Present"]),
                    };
                    dataview.Push(D);
                }
            }
            gridControl1.DataSource = dataview;
            con.Close();
        }
        private void tileView1_ItemClick(object sender, DevExpress.XtraGrid.Views.Tile.TileViewItemClickEventArgs e)
        {
            object rowID = tileView1.GetRowCellValue(e.Item.RowHandle, colkey);

            var qry = "";
            if (rowID != null)
            {
                var peek = dataview.Peek();
                for (; peek.key != rowID.ToString() && dataview.Count > 0;)
                    dataview.Pop();
                if (rowID.ToString() == "Total")
                    qry = "SELECT student.sex as `key`,concat( FORMAT(COUNT(attendance.student_id), 0),'/',FORMAT(count(*),0)) AS Present FROM student join class on class.class_id=student.class_id left JOIN attendance ON attendance.student_id= student.student_id and attendance.status= 1 and attendance.date= current_date() group by student.sex";

                else
                    qry = "SELECT class.name as `key`,concat( FORMAT(COUNT(attendance.student_id), 0),'/',FORMAT(count(*),0)) AS Present FROM student join class on class.class_id=student.class_id left JOIN attendance ON attendance.student_id= student.student_id and attendance.status= 1 and attendance.date= current_date() where student.sex='" + rowID.ToString() + "' group by class.class_id";

                MySqlConnection con = new MySqlConnection(Login.constring);
                con.Open();
                // qry = "SELECT concat( FORMAT(COUNT(attendance.student_id), 0),'/',FORMAT(count(*),0)) AS Present FROM student join class on class.class_id=student.class_id left JOIN attendance ON attendance.student_id= student.student_id and attendance.status= 1 and attendance.date= current_date() where class.class_id=1";
                MySqlCommand cmdV = new MySqlCommand(qry, con);
                MySqlDataReader reader1 = cmdV.ExecuteReader();
                if (reader1.HasRows)
                {
                    while (reader1.Read())
                    {
                        Attend D = new Attend
                        {
                            key = Convert.ToString(reader1["key"]),
                            val = Convert.ToString(reader1["Present"]),
                        };
                        dataview.Push(D);
                    }
                }
                gridControl1.DataSource = null;
                gridControl1.DataSource = dataview;
                con.Close();
            }

        }
    }
}
