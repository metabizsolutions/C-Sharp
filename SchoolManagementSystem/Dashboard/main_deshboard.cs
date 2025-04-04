using System;
using System.Data;
using System.Windows.Forms;

namespace SchoolManagementSystem.Dashboard
{
    public partial class main_deshboard : UserControl
    {
        private static main_deshboard _instance;

        public static main_deshboard instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new main_deshboard();
                }

                return _instance;
            }
        }

        private CommonFunctions fun = new CommonFunctions();
        public main_deshboard()
        {
            InitializeComponent();
            loadfunctions();
        }
        public void loadfunctions()
        {
            string query = "SELECT (SELECT COUNT(*) FROM parent AS p JOIN student AS STD ON std.parent_id = p.parent_id WHERE std.passout =0) AS parents, " +
                " (SELECT COUNT(*) FROM class) AS class, " +
                " (SELECT COUNT(*) FROM section) AS section," +
                " (SELECT COUNT(*) FROM teacher AS t WHERE t.passout = 0) AS staff," +
                " (SELECT COUNT(*) FROM student AS STD WHERE std.passout = 0) AS student";
            DataTable total_info = fun.FetchDataTable(query);
            if (total_info.Rows.Count > 0)
            {
                total_parent.Text = total_info.Rows[0]["parents"].ToString();
                total_class.Text = total_info.Rows[0]["class"].ToString();
                total_section.Text = total_info.Rows[0]["section"].ToString();
                total_staff.Text = total_info.Rows[0]["staff"].ToString();
                total_std.Text = total_info.Rows[0]["student"].ToString();
            }
            query = "SELECT COUNT(*) AS total, CONCAT( FORMAT(COUNT(attendance.student_id), 0),'/',FORMAT(COUNT(*),0)) AS Present FROM student " +
                " JOIN class ON class.class_id=student.class_id " +
                " LEFT JOIN attendance ON attendance.student_id= student.student_id AND attendance.status= 1 AND attendance.date= CURRENT_DATE() " +
                " WHERE student.passout = 0";
            DataTable total_pre_std = fun.FetchDataTable(query);
            if (total_pre_std.Rows.Count > 0)
            {
                std_attedace_total.Text = total_pre_std.Rows[0]["Present"].ToString();
            }

            query = "select count(*) as total from student where passout = 1 ";
            DataTable total_passout_std = fun.FetchDataTable(query);
            if (total_passout_std.Rows.Count > 0)
            {
                total_std_passout.Text = total_passout_std.Rows[0]["total"].ToString();
            }

            query = "SELECT COUNT(*) AS total, CONCAT( FORMAT(COUNT(attendance.student_id), 0),'/',FORMAT(COUNT(*),0)) AS Present FROM teacher " +
                " LEFT JOIN attendance ON  attendance.student_id = teacher.`teacher_id` AND attendance.status = 1 AND attendance.date = CURRENT_DATE() " +
                " WHERE teacher.passout = 0";
            DataTable total_pre_teacher = fun.FetchDataTable(query);
            if (total_pre_teacher.Rows.Count > 0)
            {
                total_present_staff.Text = total_pre_teacher.Rows[0]["Present"].ToString();
            }

            query = "select count(*) as total from teacher where passout = 1 ";
            DataTable total_passout_teacher = fun.FetchDataTable(query);
            if (total_passout_teacher.Rows.Count > 0)
            {
                lbl_staff_passout.Text = total_passout_teacher.Rows[0]["total"].ToString();
            }

            query = "SELECT class.name,student.sex AS Gender, CONCAT( FORMAT(COUNT(attendance.student_id), 0),'/',FORMAT(COUNT(*),0)) AS Present " +
                " FROM student " +
                " JOIN class ON class.class_id=student.class_id " +
                " LEFT JOIN attendance ON attendance.student_id= student.student_id AND attendance.status= 1 AND attendance.date= CURRENT_DATE() " +
                " WHERE student.passout = 0 GROUP BY class.class_id";
            DataTable present_info = fun.FetchDataTable(query);
            grid_attendace_gender.DataSource = present_info;

            comboBox1.SelectedIndex = 0;
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            string query = "";
            dtp_from_date.Visible = false;
            dtp_todate.Visible = false;
            btn_absent_count.Visible = false;
            if (comboBox1.SelectedIndex == 2)//More then 3 days absent students list
            {
                dtp_from_date.Visible = true;
                dtp_todate.Visible = true;
                btn_absent_count.Visible = true;
            }
            else
            {
                if (comboBox1.SelectedIndex == 0) //present students list
                {
                    query = "SELECT `student`.`student_id`, `student`.`name`,`class`.`name` AS `class`,section.name as section, `attendance`.`time_in`, `attendance`.`time_out` " +
                    " FROM `student` `student` " +
                    " INNER JOIN `class` ON `class`.`class_id` = `student`.`class_id` " +
                    " INNER JOIN `section` ON `section`.`section_id` = `student`.`section_id` " +
                    " INNER JOIN `attendance` ON `attendance`.`student_id` = `student`.`student_id` " +
                    " INNER JOIN `session` ON `session`.`session_id` = `class`.`session_id` " +
                    " WHERE `attendance`.`status` = '1' AND `attendance`.`date` = CURRENT_DATE() AND `session`.`default_session` = 1  and student.passout = 0";
                }
                else if (comboBox1.SelectedIndex == 1)//present teachers list
                {
                    query = "SELECT teacher.name as Teacher ,teacher.staff_type as Staff_Type,teacher.phone as `Phone #`, " +
                    "attendance.time_in as TimeIn,attendance.time_out  as TimeOut, " +
                    "if(TIME_TO_SEC(TIMEDIFF(attendance.time_in,attendance.setting_in))/60<=0,0,TIME_TO_SEC(TIMEDIFF(attendance.time_in,attendance.setting_in))/60) as `L.Time` " +
                    " from teacher  " +
                    " left join attendance on attendance.student_id=teacher.teacher_id AND attendance.date=CURRENT_DATE()  " +
                    " where `attendance`.`status` = '1' and teacher.passout = 0 " +
                    " order by teacher.name";
                }
                else if (comboBox1.SelectedIndex == 3)//present teachers list
                {
                    query = "SELECT `student`.`student_id`, `student`.`name`,`class`.`name` AS `class`,section.name as section " +
                    " FROM `student` `student` " +
                    " INNER JOIN `class` ON `class`.`class_id` = `student`.`class_id` " +
                    " INNER JOIN `section` ON `section`.`section_id` = `student`.`section_id` " +
                    " INNER JOIN `session` ON `session`.`session_id` = `class`.`session_id` " +
                    " WHERE DATE_FORMAT(student.`addmission_date`,'%Y-%m-%d') = CURRENT_DATE() AND `session`.`default_session` = 1  and student.passout = 0";
                }
                DataTable present_std_list = fun.FetchDataTable(query);
                grid_present_std_list.BeginUpdate();
                try
                {
                    gridView1.Columns.Clear();
                    grid_present_std_list.DataSource = null;
                    grid_present_std_list.DataSource = present_std_list;
                }
                finally
                {
                    grid_present_std_list.EndUpdate();
                }
            }
            if (comboBox1.SelectedIndex == 0)
            {
                gridView1.Columns["class"].Group();
                gridView1.Columns["section"].Group();
                gridView1.ExpandAllGroups();
            }

        }

        private void txt_max_days_absent_ValueChanged(object sender, EventArgs e)
        {
            /*if (txt_max_days_absent.Value > 0)
            {
                var query = "SELECT att.`date`,std.`student_id`, std.`name`, c.`name` AS `class_name`, att.`time_in`, att.`time_out`, att_s.`abbr` AS STATUS " +
                            " FROM `student`  AS STD "+
                            " INNER JOIN `class` AS c ON c.`class_id` = std.`class_id` " +
                            " LEFT JOIN `attendance` AS att ON att.`student_id` = std.`student_id` " +
                            " LEFT JOIN `attendance_status` AS att_s ON att_s.`status` = att.`status` " +
                            " WHERE att.`status` != '1'  AND att.`date` BETWEEN DATE_SUB(DATE(NOW()), INTERVAL " + txt_max_days_absent.Value + " DAY) AND CURRENT_DATE() " +
                            " AND std.`student_id` IN (SELECT att.`student_id` FROM  `attendance` AS att WHERE att.`status` != '1'  AND att.`date` BETWEEN DATE_SUB(DATE(NOW()), INTERVAL " + txt_max_days_absent.Value + " DAY) AND CURRENT_DATE() GROUP BY att.`status`,att.`student_id` HAVING COUNT(att.`student_id`) = " + (txt_max_days_absent.Value+1) + ")"; 

                   
                DataTable present_std_list = fun.FetchDataTable(query);
                grid_present_std_list.BeginUpdate();
                try
                {
                    gridView1.Columns.Clear();
                    grid_present_std_list.DataSource = null;
                    grid_present_std_list.DataSource = present_std_list;
                }
                finally
                {
                    grid_present_std_list.EndUpdate();
                }
                gridView1.Columns["name"].Group();
                gridView1.ExpandAllGroups();
            }*/
        }

        private void btn_print_Click(object sender, EventArgs e)
        {
            fun.ShowGridPreview(grid_present_std_list);
        }

        private void btn_excel_Click(object sender, EventArgs e)
        {
            fun.exporttoexcel(grid_present_std_list);
        }

        private void btn_absent_count_Click(object sender, EventArgs e)
        {
            var query = "";
            if (comboBox1.SelectedIndex == 2)
            {
                query = "SELECT att.`date`,std.`student_id`, std.`name`, c.`name` AS `class_name`, att.`time_in`, att.`time_out`, att_s.`abbr` AS STATUS " +
                            " FROM `student`  AS STD " +
                            " INNER JOIN `class` AS c ON c.`class_id` = std.`class_id` " +
                            " LEFT JOIN `attendance` AS att ON att.`student_id` = std.`student_id` " +
                            " LEFT JOIN `attendance_status` AS att_s ON att_s.`status` = att.`status` " +
                            " WHERE att.`status` != '1'  AND att.`date` BETWEEN '" + dtp_from_date.Value.ToString("yyyy-MM-dd") + "' AND '" + dtp_todate.Value.ToString("yyyy-MM-dd") + "' " +
                            " AND std.`student_id` IN (SELECT att.`student_id` FROM  `attendance` AS att WHERE att.`status` != '1'  AND att.`date` BETWEEN '" + dtp_from_date.Value.ToString("yyyy-MM-dd") + "' AND '" + dtp_todate.Value.ToString("yyyy-MM-dd") + "' GROUP BY att.`status`,att.`student_id`,att.`date`)";
            }
            else if (comboBox1.SelectedIndex == 3)//New Addmission list
            {
                query = "SELECT std.`student_id` AS id,std.`name`,std.`roll`,cls.`name` AS `class`,sec.`name` AS `section`,DATE_FORMAT(`addmission_date`,'%Y-%M-%d') AS Addmission_date  FROM `student` AS STD " +
                    " INNER JOIN class AS cls ON cls.`class_id` = std.`class_id` " +
                    " INNER JOIN section AS sec ON sec.`section_id` = std.`section_id` " +
                    " WHERE DATE_FORMAT(`addmission_date`,'%Y-%m-%d') BETWEEN '" + dtp_from_date.Value.ToString("yyyy-MM-dd") + "' AND '" + dtp_todate.Value.ToString("yyyy-MM-dd") + "' ";
            }

            DataTable present_std_list = fun.FetchDataTable(query);
            grid_present_std_list.BeginUpdate();
            try
            {
                gridView1.Columns.Clear();
                grid_present_std_list.DataSource = null;
                grid_present_std_list.DataSource = present_std_list;
            }
            finally
            {
                grid_present_std_list.EndUpdate();
            }
            gridView1.Columns["name"].Group();
            gridView1.ExpandAllGroups();
        }
    }
}
