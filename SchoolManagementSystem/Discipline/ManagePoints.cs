using DevExpress.XtraEditors.Repository;
using System;
using System.Collections.ObjectModel;
using System.Data;

namespace SchoolManagementSystem.Discipline
{
    public partial class ManagePoints : DevExpress.XtraEditors.XtraUserControl
    {
        private static ManagePoints _instance;

        public static ManagePoints instance
        {
            get
            {
                if (_instance == null)
                    _instance = new ManagePoints();
                return _instance;
            }
        }
        //  ObservableCollection<AllClass> allClass;
        ObservableCollection<TeachingStaff> teachingStaff;
        ObservableCollection<FloorSheetItems> list;
        ObservableCollection<AllClass> allClass;

        CommonFunctions fun = new CommonFunctions();
        public ManagePoints()
        {
            InitializeComponent();
            loadfunctions();
        }
        public void loadfunctions()
        {
            txtDateManage.Text = DateTime.Now.ToString("MM/dd/yyyy");
            //dasdsa
            int a = 7;
        }
        

        private void btnSave_Click(object sender, EventArgs e)
        {
            int alreadyExit = 0;
            fun.loaderform(() =>
            {
                string date = Convert.ToDateTime(txtDateManage.Text).ToString("yyyy-MM-dd");
                var query = "SELECT * FROM tbl_bonus_points WHERE on_date = '" + date + "'";
                DataTable table = fun.FetchDataTable(query);
                if (table.Rows.Count == 0)
                {
                    string attendance_points = fun.GetSettings("attendance_points");
                    string dress_points = fun.GetSettings("dress_points");
                    string ClassTime_points = fun.GetSettings("ClassTime_points");
                    string PaperSubmit_points = fun.GetSettings("PaperSubmit_points");
                    string PaperDays_points = fun.GetSettings("PaperDays_points");
                    string course_points = fun.GetSettings("course_points");

                    query = " INSERT INTO tbl_bonus_points(teacher_id,on_date,dress,paper_days,course_plan,attendance_time,class_time,paper_submission) " +
                            " SELECT teacher_id,'{0}' AS on_date,'{1}' AS dress, '{2}' AS paper_days,'{3}' AS course_plan, " +
                            " IFNULL((SELECT IF(TIME_TO_SEC(TIMEDIFF(setting_in, time_in)) > 0, 0, {4}) FROM attendance WHERE attendance.date = '{0}' AND student_id = teacher.teacher_id LIMIT 1),0) AS attendance_time, " +
                            " (SELECT IF(IFNULL(SUM(late_time), 0) > 0, 0, {5}) FROM floor_sheet WHERE `date` = '{0}' AND teacher_id = teacher.teacher_id LIMIT 1) AS class_time, " +
                            " (SELECT IF(IFNULL(COUNT(*), 0) > 0, {6}, 0) FROM tbl_mark_subject WHERE due_date = '{0}' AND submit_on IS NULL AND teacher_id = teacher.teacher_id LIMIT 1) AS paper_submission " +
                            " FROM teacher";
                    query = string.Format(query, date, dress_points, PaperDays_points, course_points, attendance_points, ClassTime_points, PaperSubmit_points);
                    fun.ExecuteQuery(query);
                }
                FillGridPoints(txtDateManage.Text);
            });
        }
        RepositoryItemSearchLookUpEdit riSearch;
        void FillGridPoints(string date)
        {
            date = Convert.ToDateTime(date).ToString("yyyy-MM-dd");
            var query = "SELECT teacher.name, teacher.staff_type,tbp.* FROM teacher " +
                " LEFT JOIN tbl_bonus_points AS tbp ON tbp.teacher_id = teacher.teacher_id AND tbp.on_date = '"+date+"'";
            DataTable table = fun.FetchDataTable(query);
            gridView1.Columns.Clear();
            GridManagePoints.DataSource = table;
            gridView1.BestFitColumns();
            gridView1.Columns["bonus_id"].OptionsColumn.ReadOnly = true;
            gridView1.Columns["teacher_id"].OptionsColumn.ReadOnly = true;
            gridView1.Columns["on_date"].OptionsColumn.ReadOnly = true;
            gridView1.Columns["name"].OptionsColumn.ReadOnly = true;
            gridView1.Columns["staff_type"].OptionsColumn.ReadOnly = true;



        }

        private void gridView1_RowUpdated(object sender, DevExpress.XtraGrid.Views.Base.RowObjectEventArgs e)
        {
            DataRow row = gridView1.GetDataRow(gridView1.FocusedRowHandle);
            string query = string.Format("UPDATE tbl_bonus_points SET attendance_time = '{0}', dress = '{1}', class_time = '{2}',paper_submission = '{3}',paper_days = '{4}',course_plan = '{5}' WHERE bonus_id = '{6}'", row[4].ToString(), row[5].ToString(), row[6].ToString(), row[7].ToString(), row[8].ToString(), row[9].ToString(), row[2].ToString());
            fun.ExecuteQuery(query);
        }

        

        private void btnPrint_Click(object sender, EventArgs e)
        {
            GridManagePoints.ShowPrintPreview();
        }
        
    }
}
