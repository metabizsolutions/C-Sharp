using MySql.Data.MySqlClient;
using System;
using System.Data;

namespace SchoolManagementSystem.Class
{
    public partial class Holidays : DevExpress.XtraEditors.XtraUserControl
    {
        CommonFunctions fun = new CommonFunctions();
        private static Holidays _instance;
        public static Holidays instance
        {
            get
            {
                if (_instance == null)
                    _instance = new Holidays();
                return _instance;
            }
        }
        public Holidays()
        {
            InitializeComponent();
            schedulerControl1.ActiveViewType = DevExpress.XtraScheduler.SchedulerViewType.Month;
            schedulerControl1.Start = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
            //tbl_holidaysTableAdapter.Adapter.RowUpdated += Adapter_RowUpdated;

        }

        private void Adapter_RowUpdated(object sender, MySqlRowUpdatedEventArgs e)
        {
            if (e.Status == UpdateStatus.Continue && e.StatementType == StatementType.Insert)
            {
                int id = 0;
                //Select LAST_INSERT_ID() from appointments
                using (MySqlCommand cmd = new MySqlCommand("SELECT @@IDENTITY", tbl_holidaysTableAdapter.Connection))
                {
                    id = Convert.ToInt32(cmd.ExecuteScalar());
                }
                e.Row["holiday_id"] = id;
            }
        }

        private void Holidays_Load(object sender, EventArgs e)
        {
            //this.tbl_holidaysTableAdapter.Fill(this.tnsbay_schoolDataSet.tbl_holidays);
        }

        private void schedulerStorage1_AppointmentsChanged(object sender, DevExpress.XtraScheduler.PersistentObjectsEventArgs e)
        {
            //tbl_holidaysTableAdapter.Update(tnsbay_schoolDataSet);
            //tnsbay_schoolDataSet.AcceptChanges();
        }

        private void schedulerStorage1_AppointmentsDeleted(object sender, DevExpress.XtraScheduler.PersistentObjectsEventArgs e)
        {
            //tbl_holidaysTableAdapter.Update(tnsbay_schoolDataSet);
            //tnsbay_schoolDataSet.AcceptChanges();
        }

        private void schedulerStorage1_AppointmentsInserted(object sender, DevExpress.XtraScheduler.PersistentObjectsEventArgs e)
        {
            //tbl_holidaysTableAdapter.Update(tnsbay_schoolDataSet);
            //tnsbay_schoolDataSet.AcceptChanges();
        }

        private void schedulerControl1_EditAppointmentFormShowing(object sender, DevExpress.XtraScheduler.AppointmentFormEventArgs e)
        {
            DevExpress.XtraScheduler.SchedulerControl scheduler = ((DevExpress.XtraScheduler.SchedulerControl)(sender));
            SchoolManagementSystem.Admin.CustomAppointmentForm form = new SchoolManagementSystem.Admin.CustomAppointmentForm(scheduler, e.Appointment, e.OpenRecurrenceForm);
            try
            {
                e.DialogResult = form.ShowDialog();
                e.Handled = true;
            }
            finally
            {
                form.Dispose();
            }

        }
    }
}
