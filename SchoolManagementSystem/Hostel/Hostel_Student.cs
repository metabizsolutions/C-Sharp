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

namespace SchoolManagementSystem.Hostel
{
    public partial class Hostel_Student : DevExpress.XtraEditors.XtraUserControl
    {
        private static Hostel_Student _instance;

        public static Hostel_Student instance
        {
            get
            {
                if (_instance == null)
                    _instance = new Hostel_Student();
                return _instance;
            }
        }
        CommonFunctions fun = new CommonFunctions();
        public Hostel_Student()
        {
            InitializeComponent();
            loadfunctions();
        }

        public void loadfunctions() {
            loadlists();
            Hostel_student_Grid();
        }
        public void permissions_btns(bool add, bool Edit, bool Delete)
        {
            btnAddstdtoroom.Enabled = false;
            if (add)
                btnAddstdtoroom.Enabled = true;
            gridView4.OptionsBehavior.Editable = false;
            if (Edit)
                gridView4.OptionsBehavior.Editable = true;
            btnHLeft.Enabled = false;
            if (Delete)
                btnHLeft.Enabled = true;
        }
        public void loadlists()
        {
            string query = "SELECT hr.ID,hos.HostelName as Hostel,hr.room as Room,(Capacity -(select count(*) from hostels_student where room_id = hr.ID and LeftDate is null )) as Capacity FROM hostel_rooms as hr "+
                 " join hostels as hos on hos.ID = hr.hostel_id"+
                 " where (Capacity -(select count(*) from hostels_student where room_id = hr.ID and LeftDate is null )) > 0";
            GridRoomsList.Properties.DataSource = fun.FetchDataTable(query);
            GridRoomsList.Properties.DisplayMember = "Room";
            GridRoomsList.Properties.ValueMember = "ID";

            query = "SELECT student_id as ID,name as Student FROM student where not student_id in (SELECT student_id FROM hostels_student where LeftDate is null) and  passout = 0 ";
            GridStudentsList.Properties.DataSource = fun.FetchDataTable(query);
            GridStudentsList.Properties.DisplayMember = "Student";
            GridStudentsList.Properties.ValueMember = "ID";

        }

        private void emptyfield() {
            GridRoomsList.EditValue = null;
            GridStudentsList.EditValue = null;
            txtRRent.Text = "";
            dtpJoingDate.Value = DateTime.Now;
        }
        private void btnAddstdtoroom_Click(object sender, EventArgs e)
        {
            if (GridRoomsList.EditValue == null)
            {
                MessageBox.Show("Rooms is Required", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (GridStudentsList.EditValue == null)
            {
                MessageBox.Show("Student is Required", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string query = "SELECT * FROM `hostel_rooms` WHERE `Capacity` > (select count(*) from hostels_student where room_id = "+GridRoomsList.EditValue+"  and LeftDate is null) and ID = " + GridRoomsList.EditValue + "";
            DataTable HasCapacity = fun.FetchDataTable(query);
            if (HasCapacity.Rows.Count <= Convert.ToInt32(HasCapacity.Rows[0]["Capacity"]))
            {
                query = "select * from hostels_student where student_id = '"+GridStudentsList.EditValue+"'  and LeftDate is null";
               DataTable hasstudent = fun.FetchDataTable(query);
                if (hasstudent.Rows.Count == 0)
                {
                    query = "INSERT INTO `hostels_student`(`room_id`, `student_id`, `Rent`,JoinDate) VALUES (" + GridRoomsList.EditValue + ","+GridStudentsList.EditValue+",'"+txtRRent.Text+"','"+ fun.time(Convert.ToDateTime(dtpJoingDate.Value))+"')";
                    int hs_id = fun.ExecuteInsert(query);
                    //query = "INSERT INTO `hostel_pay_rent`(hsID, `status`, `Paid_amount`, `Due_amount`,Creation_Date) VALUES ("+hs_id+",0,0,'" + txtRRent.Text + "','" + fun.time()+"')";
                    //fun.ExecuteInsert(query);
                }
                else
                {
                    MessageBox.Show("Student with ID='"+GridStudentsList.Text+"' is already exist in Hostel '" + GridRoomsList.EditValue + "' ","Info",MessageBoxButtons.OK,MessageBoxIcon.Information);
                }
            }
            else
            {
                MessageBox.Show("'"+GridRoomsList.Text+"'=Room is full", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            emptyfield();
            loadlists();
            Hostel_student_Grid();
        }

        public void Hostel_student_Grid()
        {
            //Full Query in OSPWebPortal FOr SGC
            string query = "SELECT hs.student_id,h.HostelName,std.name as Name,hr.room as Room,DATE_FORMAT(FROM_UnixTime(hs.JoinDate),'%Y-%m-%d') as JoinDate " +
                      " FROM `hostels_student` as hs " +
                      " join student as std on std.student_id = hs.student_id " +
                      " join hostel_rooms as hr on hr.ID = hs.room_id " +
                      " join hostels as h on h.ID = hr.hostel_id " +
                      " where LeftDate is null ";
            //" where DATE_FORMAT(from_unixtime(hpr.Creation_Date),'%Y-%m') = '$Months'";

            gridHostel_students.DataSource = fun.FetchDataTable(query);
        }

        private void btnHLeft_Click(object sender, EventArgs e)
        {
            DataRow row = gridView4.GetDataRow(gridView4.FocusedRowHandle);
            if (MessageBox.Show("Student = '"+row["Name"]+"' Left the Hostel", "Confirmation Required", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                string query = "UPDATE `hostels_student` SET LeftDate = '"+fun.time(DtpLeftDate.Value)+"' WHERE student_id ='" + row["student_id"] + "' and LeftDate is null";
                fun.ExecuteInsert(query);
                //query = "UPDATE `hostel_pay_rent` as hpr join hostels_student as hs on hs.ID = hpr.hsID SET hpr.Forward=1 WHERE hs.student_id ='" + row["student_id"] + "' and hpr.Forward=0";
                //fun.ExecuteInsert(query);
                loadlists();
                Hostel_student_Grid();
                DtpLeftDate.Value = DateTime.Now;
            }
        }
    }
}
