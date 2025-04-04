using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using SchoolManagementSystem;
using SchoolManagementSystem.Class;

namespace Accounting.Automatic_Time_Table
{
    public partial class EditSlots : XtraForm
    {
        CommonFunctions fun = new CommonFunctions();
        private readonly ScheduleTiming A_T;
        public EditSlots(ScheduleTiming A_TPage)
        {
            InitializeComponent();
            fun.selectall_Controls(this);
            A_T = A_TPage;
            loadslot();
        }

        void loadslot()
        {
            string query = "select * from time_table_slot";
            DataTable tab = fun.FetchDataTable(query);
            gridEditSlot.DataSource = tab;
        }

        private void gridView1_RowUpdated(object sender, DevExpress.XtraGrid.Views.Base.RowObjectEventArgs e)
        {
            DataRow row = gridView1.GetDataRow(gridView1.FocusedRowHandle);
            string start = Convert.ToDateTime(row["Slot_Start_Time"]).ToString("hh:mm tt");
            string end = Convert.ToDateTime(row["Slot_End_Time"]).ToString("hh:mm tt");
            string query = "update time_table_slot SET slot='"+start+"-"+end+"', slot_type='" + row["slot_type"] + "',Slot_Start_Time = '" + start + "',Slot_End_Time = '" + end + "' where slot_id = '" + row["slot_id"] + "'";
            fun.Execute_Query(query);
            loadslot();
        }

        private void EditSlots_FormClosed(object sender, FormClosedEventArgs e)
        {
            A_T.timetableList();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            string query = "INSERT INTO `time_table_slot`(`slot`,Slot_Start_Time,Slot_End_Time) VALUES ('" + DateTime.Now.ToString("hh:mm tt") + "-" + DateTime.Now.ToString("hh:mm tt") + "','" + DateTime.Now.ToString("hh:mm tt") + "','" + DateTime.Now.ToString("hh:mm tt") + "')";
            fun.Execute_Query(query);
            loadslot();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            DataRow row = gridView1.GetFocusedDataRow();
            string query = "Delete from time_table_slot where slot_id = '" + row["slot_id"] + "'";
            fun.Execute_Query(query);
            loadslot();
        }
    }
}
