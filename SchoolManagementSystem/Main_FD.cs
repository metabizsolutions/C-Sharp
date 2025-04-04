using DevExpress.XtraBars.Helpers;
using DevExpress.XtraBars.Navigation;
using DevExpress.XtraBars.Ribbon;
using DevExpress.XtraBars.Ribbon.Gallery;
using DevExpress.XtraEditors;
using SchoolManagementSystem.About;
using SchoolManagementSystem.account_d_e;
using SchoolManagementSystem.Admin;
using SchoolManagementSystem.Attendance;
using SchoolManagementSystem.Class;
using SchoolManagementSystem.Dashboard;
using SchoolManagementSystem.Discipline;
using SchoolManagementSystem.Exam;
using SchoolManagementSystem.Fees;
using SchoolManagementSystem.Library;
using SchoolManagementSystem.Materail;
using SchoolManagementSystem.Principal;
using SchoolManagementSystem.Students;
using SchoolManagementSystem.Time;
using SchoolManagementSystem.Transport;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SchoolManagementSystem
{
    public partial class Main_FD : DevExpress.XtraBars.FluentDesignSystem.FluentDesignForm
    {
        public static string SelectedSession;
        public static string theme;
        CommonFunctions fun = new CommonFunctions();
        systemvalidation objvalidation = new systemvalidation();
        BackgroundWorker chack_key_worker = new BackgroundWorker();
        BackgroundWorker bworker_timer = new BackgroundWorker();
        BackgroundWorker server_datetime = new BackgroundWorker();
        string isactive = "";
        string dashboard_imgs = Path.GetDirectoryName(Application.ExecutablePath) + @"\slider_images\dashboard";
        List<string> slider_img = new List<string>();

        string last_login_date_file = Directory.GetCurrentDirectory() + "\\noticeboardfile\\";
        bool is_ok_change_date = false;
        bool is_ok = false;
        bool is_server = false;
        bool is_from_loading = false;
        static bool need_load_funs = true;
        public Main_FD()
        {
            InitializeComponent();
            btn_update_activation.Visibility = DevExpress.XtraBars.BarItemVisibility.Always;
            is_from_loading = true;
            need_load_funs = true;
            is_server = fun.is_server_pc();
            form_loaded();
            if (is_server)
            {
                last_login_date_file += fun.UniqueMachineID() + ".txt";
            }
            else
                last_login_date_file += fun.Client_Unique_NO() + ".txt";
            string last_login_date = "";
            string datetime_server = get_server_datetime().ToString();
            if (File.Exists(last_login_date_file))
            {
                last_login_date = File.ReadAllText(last_login_date_file);
                last_login_date = fun.Decrypt(last_login_date, "Zee0107");
            }
            else
                last_login_date = datetime_server;

            if (Convert.ToDateTime(last_login_date) <= Convert.ToDateTime(datetime_server))
                is_ok = true;
            else if (!is_server)
                is_ok = true;
            if (is_ok)
            {
                is_ok_change_date = true;
                ////internt is connected or not chaking in backgroundworker and changes accordingly
                chack_key_worker.DoWork += Chack_key_worker_DoWork;
                chack_key_worker.RunWorkerCompleted += Chack_key_worker_RunWorkerCompleted;
                chack_internet_key();

                ////chack server datetime
                server_datetime.DoWork += Server_datetime_DoWork;
                server_datetime.RunWorkerCompleted += Server_datetime_RunWorkerCompleted;
                //Running timer Function
                bworker_timer.DoWork += Bworker_timer_DoWork;
                bworker_timer.RunWorkerCompleted += Bworker_timer_RunWorkerCompleted;
                TimeUpdater();// 
                string subject_wise = fun.GetSettings("Institute_Type");
                if (subject_wise == "Subject Wise Institute")
                    ace_subjectwise.Visible = true;
                else
                    ace_subjectwise.Visible = false;
            }
            

        }
        static string server_theme = "";
        static string server_theme_db = "";

        void form_loaded()
        {
            load_profile_list();
            user_permission();
            #region chacking theme from server and local selected theme
            server_theme = fun.server_theme();
            if (!string.IsNullOrEmpty(server_theme))
            {
                server_theme_db = fun.GetSettings("desktop_theme_from_server");
                if (server_theme != server_theme_db)
                {
                    string query = "update settings SET description='" + server_theme + "' where type in ('desktop_theme_from_server','desktop_theme') ;";
                    fun.ExecuteQuery(query);
                    theme = server_theme;
                }
                else
                    theme = fun.GetSettings("desktop_theme");
            }
            else
                theme = fun.GetSettings("desktop_theme");
            if (!string.IsNullOrEmpty(theme))
                DevExpress.LookAndFeel.UserLookAndFeel.Default.SetSkinStyle(theme);
            #endregion
            slider_img = Directory.GetFiles(dashboard_imgs, "*.*", SearchOption.AllDirectories).ToList();
            load_next_image();
            //session dropdownBox
            DataTable dtsession = fun.All_SessionName();
            txtSession.DataSource = dtsession;
            txtSession.DisplayMember = "name";
            txtSession.ValueMember = "session_id";
            if (dtsession.Rows.Count > 0)
            {
                SelectedSession = dtsession.Rows[0]["name"].ToString();
                Login.session_name = SelectedSession;
            }
            //theme change click event
            GalleryControlGallery gallery = ((skinDropDownButtonItem1.DropDownControl as SkinPopupControlContainer).Controls.OfType<GalleryControl>().FirstOrDefault()).Gallery;
            gallery.ItemClick += Gallery_ItemClick;
        }

        public Main_FD(bool is_aboutus)
        {
            InitializeComponent();
            need_load_funs = false;
            load_about_us();
            accordionControlMain.Visible = false;
        }
        public Main_FD(string permission_settings)
        {
            InitializeComponent();
            need_load_funs = false;
        }
        private void Main_FD_Load(object sender, EventArgs e)
        {
            if (need_load_funs)
            {
                if (is_ok)
                {
                    string[] system_info = objvalidation.activate();
                    isactive = system_info[0];
                    if (isactive == "OK")
                    {
                        string query = "select * from `tbl_role` where `is_default` = 1 and `permission_id` = '" + Login.CurrentUserStatus_id + "' and `Type` = 'Desktop';";
                        DataTable dt = fun.FetchDataTable(query);
                        if (dt.Rows.Count > 0)
                        {
                            if (!string.IsNullOrEmpty(dt.Rows[0]["subsubkey"].ToString()))
                                accordionControlMain.SelectedElement = accordionControlMain.Elements[dt.Rows[0]["key"].ToString()].Elements[dt.Rows[0]["subkey"].ToString()].Elements[dt.Rows[0]["subsubkey"].ToString()];
                            if (!string.IsNullOrEmpty(dt.Rows[0]["subkey"].ToString()))
                                accordionControlMain.SelectedElement = accordionControlMain.Elements[dt.Rows[0]["key"].ToString()].Elements[dt.Rows[0]["subkey"].ToString()];
                            if (!string.IsNullOrEmpty(dt.Rows[0]["key"].ToString()))
                                accordionControlMain.SelectedElement = accordionControlMain.Elements[dt.Rows[0]["key"].ToString()];
                        }
                        //else
                        //loaddeshboard();
                    }
                    else
                    {
                        load_about_us();
                        accordionControlMain.Visible = false;
                    }

                    txtHeading.Caption = fun.GetSettings("system_title");
                    //Take Auto Database Backup
                    string path = Directory.GetCurrentDirectory();
                    if (Directory.Exists(path))
                    {
                        BackgroundWorker bw = new BackgroundWorker();
                        bw.DoWork += background_db_backup;
                        bw.RunWorkerAsync();
                    }
                    Login.session_name = txtSession.Text;
                }
                else
                {
                    load_about_us();
                    string cur_key = fun.GetSettings("activition_key");
                    if (!string.IsNullOrEmpty(cur_key))
                        fun.save_key_File(cur_key, "beta.txt");
                    accordionControlMain.Visible = false;
                    XtraMessageBox.Show("Your Server Date is not Correct Please set Your Date", "Date INFO", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            lbl_welcome.Visible = true;
            is_from_loading = false;
            lbl_copyright_year.Text = DateTime.Now.Year.ToString();
        }
        private void Server_datetime_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            label1.Caption = get_server_datetime().ToString();
            string last_login_date = "";
            if (File.Exists(last_login_date_file))
            {
                last_login_date = File.ReadAllText(last_login_date_file);
                last_login_date = fun.Decrypt(last_login_date, "Zee0107");
            }
            else
                last_login_date = label1.Caption;//DateTime.Now.ToString();

            if (is_ok_change_date && Convert.ToDateTime(last_login_date) <= Convert.ToDateTime(label1.Caption))
                fun.save_date_File(DateTime.Now.ToString(), fun.Client_Unique_NO() + ".txt");
            else if (is_ok_change_date && fun.is_server_pc())
            {
                is_ok_change_date = false;
                MessageBox.Show(" Your Date ='" + last_login_date + "' and '" + label1.Caption + "' is not correct please set your datetime other wise your OSP Sytem will be Crash ", "DateTime Warnning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                logout();
            }
        }
        private void Server_datetime_DoWork(object sender, DoWorkEventArgs e)
        {
            if (server_datetime.CancellationPending)
            {
                e.Cancel = true;
                return;
            }
        }
        async void TimeUpdater()
        {
            while (true)
            {
                server_datetime.WorkerReportsProgress = true;
                if (!server_datetime.IsBusy)
                    server_datetime.RunWorkerAsync();

                bworker_timer.WorkerReportsProgress = true;
                if (!bworker_timer.IsBusy)
                    bworker_timer.RunWorkerAsync();

                await Task.Delay(31000);
            }
        }
        private void Bworker_timer_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            System.Threading.Thread.Sleep(1000);
            string smsenabales = fun.GetSettings("enable_feecollection_sms");
            if (smsenabales == "1")// && Convert.ToInt64(todaypaid) > 0)
            {
                var qry = "SELECT if(sum(amount) is null,0,sum(amount)) as Paid FROM `payment` WHERE date = '" + DateTime.Now.ToString("yyyy-MM-dd") + "'";
                object todaypaid = fun.Execute_Scaler_string(qry);

                string time = DateTime.Now.ToString("hh:mm tt");
                string[] todaycollectionsmstime = fun.GetSettings("feecollection_sendsms_time").Split(',');
                if (time == todaycollectionsmstime[0])// && !ismsgsend)
                {
                    string message = fun.GetSettings("today_feecollection_sms");
                    message = message.Replace("[PaidFee]", todaypaid.ToString()).Replace("[Schoolname]", fun.GetSettings("system_name"));
                    string query = "INSERT INTO `sms_que`(`mobile`, `sms`, `status`, `ondate`) VALUES ('" + todaycollectionsmstime[1] + "','" + message + "',0," + fun.time() + ")";
                    fun.ExecuteInsert(query);
                    //ismsgsend = true;
                }
            }
        }
        private void Bworker_timer_DoWork(object sender, DoWorkEventArgs e)
        {
            if (bworker_timer.CancellationPending)
            {
                e.Cancel = true;
                return;
            }
        }

        private void Gallery_ItemClick(object sender, GalleryItemClickEventArgs e)
        {
            fun.update_setting(e.Item.Caption, "desktop_theme");
            theme = e.Item.Caption;
            DevExpress.LookAndFeel.UserLookAndFeel.Default.SetSkinStyle(e.Item.Caption);
            MessageBox.Show("Theme Changed Successfully" + e.Item.Caption, "Theme", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private int img_no = 0;
        void load_next_image()
        {
            if (slider_img.Count <= img_no)
                img_no = 0;
            if (slider_img.Count > 0)
                slider_box.ImageLocation = slider_img[img_no];
            else
                slider_img = Directory.GetFiles(dashboard_imgs, "*.*", SearchOption.AllDirectories).ToList();
            img_no++;
        }
        private void timer1_Tick(object sender, EventArgs e)
        {
            load_next_image();
        }
        string element_selected = "";
        private void accordionControlMain_SelectedElementChanged(object sender, SelectedElementChangedEventArgs e)
        {
            accordionControlMain.Enabled = false;
            element_selected = e.Element.Name;
            if (is_from_loading)
                accordian_control_elements(e.Element);
            else
                fun.loaderform(() => { accordian_control_elements(e.Element); });
            accordionControlMain.Enabled = true;
        }
        private void accordionControlMain_ElementClick(object sender, ElementClickEventArgs e)
        {
            accordionControlMain.Enabled = false;
            if (e.Element.Name == element_selected)
                fun.loaderform(() => { accordian_control_elements(e.Element); });
            accordionControlMain.Enabled = true;
        }
        void accordian_control_elements(AccordionControlElement e)
        {
            if (e.Name == "ace_receipt_template")
                LoadModule("Templates", Fee_Receipt_Template.instance, ace_receipt_template);
            else if (e.Name == "accordionStudentReg")
                LoadModule("Student Management", AddStudent.instance, accordionStudentReg);
            else if (e.Name == "accordionVisitingInfo")
                LoadModule("Visiting Information", VisitingInformation.instance, accordionVisitingInfo);
            else if (e.Name == "accordionPDeshboard")
                LoadModule("Principal Dashboard", PDashboard.instance, accordionPDeshboard);
            else if (e.Name == "accordionDeshboard")
                LoadModule("Dashboard", main_deshboard.instance, accordionDeshboard);

            else if (e.Name == "ace_teacher_card")
                LoadModule("Teacher Card", create_teacher_card.instance, ace_teacher_card);
            else if (e.Name == "ace_card_creation")
                LoadModule("Student Card", create_student_card.instance, ace_card_creation);
            else if (e.Name == "accordionBulkTransfer")
                LoadModule("Bulk Transfer", BulkTransfer.instance, accordionBulkTransfer);
            else if (e.Name == "accordionPassOut")
                LoadModule("Pass Out", PassOutStudents.instance, accordionPassOut);
            else if (e.Name == "accordionBulkOfStudent")
                LoadModule("Bulk Of Students", AddBulkStudent.instance, accordionBulkOfStudent);
            else if (e.Name == "accordionManageParents")
                LoadModule("Manage Parents", ManageParents.instance, accordionManageParents);

            else if (e.Name == "accordionExperienceLetter")
                LoadModule("Experience Letter", ExperienceLetter.instance, accordionExperienceLetter);
            else if (e.Name == "accordionSubjects")
                LoadModule("Manage Subjects", Subjects.instance, accordionSubjects);
            else if (e.Name == "accordionManageSections")
                LoadModule("Manage Sections", ManageSection.instance, accordionManageSections);
            else if (e.Name == "accordionManageClass")
                LoadModule("Manage Class", ManageClass.instance, accordionManageClass);
            else if (e.Name == "accordionManageStaff")
            {
                LoadModule("Manage Staff", ManageStaff.instance, accordionManageStaff);
                //LoadModule("Manage Staff", ManageTeacher.instance, accordionManageStaff);
            }
            else if (e.Name == "accordionStaffMonthlyAttendence")
                LoadModule("Monthly Staff Attendance", TeacherMAttendance.instance, accordionStaffMonthlyAttendence);
            else if (e.Name == "accordionStaffAttendence")
                LoadModule("Staff Attendance", TeacherAttendance.instance, accordionStaffAttendence);
            else if (e.Name == "accordionAttendenceSMS")
                LoadModule("Attendance SMS", SMSAttendance.instance, accordionAttendenceSMS);
            else if (e.Name == "accordionStudentReport")
                LoadModule("Student Report", StudentAttendance.instance, accordionStudentReport);
            else if (e.Name == "accordionClassReport")
                LoadModule("Class Report", ClassAttendance.instance, accordionClassReport);
            else if (e.Name == "ace_leave_managment")
                LoadModule("Leave Managment", Leave_Managment.instance, ace_leave_managment);
            else if (e.Name == "ace_holidays")
                LoadModule("Holidays", Attendance.Holidays.instance, ace_holidays);
            else if (e.Name == "accordionDailyAttendence")
                LoadModule("Daily Attendance", DailyAttendance.instance, accordionDailyAttendence);
            else if (e.Name == "accordionSubTotalMarks")
                LoadModule("Subject Total Marks", SubjectTotalMarks.instance, accordionSubTotalMarks);
            else if (e.Name == "accordionExamList")
                LoadModule("Exam List", ExamList.instance, accordionExamList);
            else if (e.Name == "accordionStudentResult")
                LoadModule("Student Result", StdResult.instance, accordionStudentResult);
            else if (e.Name == "accordionMarksSMS")
                LoadModule("Marks SMS", SendMarksBySMS.instance, accordionMarksSMS);
            else if (e.Name == "accordionMarkSheet")
                LoadModule("Marks Sheet", MarkSheet.instance, accordionMarkSheet);
            else if (e.Name == "ace_multi_class_result")
                LoadModule("Multi Class Result", Multi_ClassResult.instance, ace_multi_class_result);
            else if (e.Name == "accordionClassResult")
                LoadModule("Class Result", ClassResult.instance, accordionClassResult);
            //else if (e.Name == "accordionManageMarksGovt")
               // LoadModule("Manage Marks", ManageMarksGovt.instance, accordionManageMarksGovt);
            else if (e.Name == "accordionManageMarks")
                LoadModule("Manage Marks", ManageMarks.instance, accordionManageMarks);
            else if (e.Name == "accordionClassAllExam")
                LoadModule("Class All Exam", ClassResultAll.instance, accordionClassAllExam);
            else if (e.Name == "accordionResultByDate")
                LoadModule("Result By Date", ClassResultByDateRange.instance, accordionResultByDate);
            else if (e.Name == "accordionResultbysubject")
                LoadModule("Result By Subject", ClassResultBySubjects.instance, accordionResultbysubject);
            else if (e.Name == "accordionChackList")
                LoadModule("Check List", CheckList.instance, accordionChackList);
            else if (e.Name == "accordionAwardList")
                LoadModule("Award List", AwradList.instance, accordionAwardList);
            else if (e.Name == "accordionResultsummery")
                LoadModule("Result Summary", ResultSummary.instance, accordionResultsummery);
            else if (e.Name == "accordionIncomeExpense")
                LoadModule("Income/Expence", ExpenseManagement.instance, accordionIncomeExpense);
            else if (e.Name == "accordionSalaryManagement")
                LoadModule("Salary Management", SalaryManagement.instance, accordionSalaryManagement);
            else if (e.Name == "ace_deleted_invoices")
                LoadModule("Deleted Invoices", deleted_invoices.instance, ace_deleted_invoices);
            else if (e.Name == "accordionFeeManagement")
                LoadModule("Fees Management", IncomeManagement.instance, accordionFeeManagement);
            else if (e.Name == "ace__acc_Fees_DR")
                LoadModule("Daily Fee Report", Fee_Daily_Report.instance, ace__acc_Fees_DR);
            else if (e.Name == "ace_acc_Fees_unSMS")
                LoadModule("UnPaid SMS", Unpaid_SMS.instance, ace_acc_Fees_unSMS);
            else if (e.Name == "ace_subjectwise")
                LoadModule("Subject Fees Details", Subject_wise_fee_details.instance, ace_subjectwise);
            else if (e.Name == "accordionFeeCategory")
                LoadModule("Fees Category", FeesCategory.instance, accordionFeeCategory);
            else if (e.Name == "accordionStudent_Report")
                LoadModule("Student Report", StudentFeeReport.instance, accordionStudent_Report);
            else if (e.Name == "accordionReports")
                LoadModule("Account Reports", MonthlyReports.instance, accordionReports);
            else if (e.Name == "ace_balance_sheet")
                LoadModule("Balance Sheet", Balance_Sheet.instance, ace_balance_sheet);
            else if (e.Name == "accordionActivityCatagory")
                LoadModule("Activity Category", ActivityCategory.instance, accordionActivityCatagory);
            else if (e.Name == "accordionStaffPointsReport")
                LoadModule("Staff Points", IndividualTeacherPoints.instance, accordionStaffPointsReport);
            else if (e.Name == "accordionManagePoints")
                LoadModule("Manage Points", ManagePoints.instance, accordionManagePoints);
            else if (e.Name == "accordionStudentActivity")
                LoadModule("Student Activity", ActivityStudent.instance, accordionStudentActivity);
            else if (e.Name == "accordionBell")
                LoadModule("Bell System", BellSheet.instance, accordionBell);
            else if (e.Name == "accordionFloorSheet")
                LoadModule("Floor Sheet", FloorSheet.instance, accordionFloorSheet);
            else if (e.Name == "accordionExAss_Report")
                LoadModule("Extra Lecture Assignment", ExtraLectureAssign.instance, accordionExAss_Report);
            else if (e.Name == "accordionExLecAssign")
                LoadModule("Extra Lecture", ScheduleAssign.instance, accordionExLecAssign);
            else if (e.Name == "accordionClassTimetable")
                LoadModule("Time Table", ScheduleTiming.instance, accordionClassTimetable);
            else if (e.Name == "ace_sms_history")
                LoadModule("SMS History", sms_history.instance, ace_sms_history);
            else if (e.Name == "accordionMarketingSMS")
                LoadModule("Marketing SMS", MarketingSMS.instance, accordionMarketingSMS);
            else if (e.Name == "accordionEnoticeBoard")
                LoadModule("E-notice Board", EnoticeBoard.instance, accordionEnoticeBoard);
            else if (e.Name == "accordionBackupsync")
                LoadModule("Backup & Sync", Backup.Backup.instance, accordionBackupsync);
            else if (e.Name == "accordionUsers")
                LoadModule("User Management", Users.Users.instance, accordionUsers);
            else if (e.Name == "accordionIssueBooks")
                LoadModule("Issue Books", IssueBooks.instance, accordionIssueBooks);
            else if (e.Name == "accordionAddBooks")
                LoadModule("Add Book", AddBooks.instance, accordionAddBooks);
            else if (e.Name == "ACTeacherProfile")
                LoadModule("Teacher Profile", TeacherProfile.instance, ACTeacherProfile);
            else if (e.Name == "ACCashAssets")
                LoadModule("Cash Assets", CashAssets.instance, ACCashAssets);
            else if (e.Name == "acSalaryCategory")
                LoadModule("Salary Category", SalaryCategory.instance, acSalaryCategory);
            else if (e.Name == "acr_PEF_Report")
                LoadModule("PEF Attendence Report", Pef_Attenddence_report.instance, acr_PEF_Report);
            else if (e.Name == "ace_individual_staff_att")
                LoadModule("Individual Staff Attendance", IndividualTeacherAttendance.instance, ace_individual_staff_att);
            else if (e.Name == "aceFeeinsReport")
                LoadModule("Fee Installment Report", FeeInstallmentReports.instance, aceFeeinsReport);
            else if (e.Name == "accordionAbout")
                load_about_us();
            else if (e.Name == "ace_fee_setting")
                LoadModule("Fees Setting", FeeSetting.instance, ace_fee_setting);
            else if (e.Name == "ace_transport_students")
                LoadModule("Transport Students", Transport_student.instance, ace_transport_students);
            else if (e.Name == "ace_stops")
                LoadModule("Stops", Stops.instance, ace_stops);
            else if (e.Name == "ace_Transport_rout")
                LoadModule("Transport Management", Transport.Transport.instance, ace_Transport_rout);
            else if (e.Name == "accordionMaterialCategory")
                LoadModule("Material Category", MaterialCategory.instance, accordionMaterialCategory);
            else if (e.Name == "accordionmaterialitem")
                LoadModule("Material", MaterialAssign.instance, accordionmaterialitem);
            else if (e.Name == "accordionHos_student")
                LoadModule("Hostellised Students", Hostel.Hostel_Student.instance, accordionHos_student);
            else if (e.Name == "accordionHostelRooms")
                LoadModule("Hostel Rooms Management", Hostel.HostelRooms.instance, accordionHostelRooms);
            else if(e.Name == "ace_ladger")
                LoadModule("Ladgers", ladgers.instance, ace_ladger);
            else if (e.Name == "accordionStaffMonthlyAttendence")
                LoadModule("Monthly Staff Attendance", TeacherMAttendance.instance, accordionStaffMonthlyAttendence);
            else if (e.Name == "accordionStaffAttendence")
                LoadModule("Staff Attendance", TeacherAttendance.instance, accordionStaffAttendence);
            else if (e.Name == "accordionAttendenceSMS")
                LoadModule("Attendance SMS", SMSAttendance.instance, accordionAttendenceSMS);
            else if (e.Name == "accordionStudentReport")
                LoadModule("Student Report", StudentAttendance.instance, accordionStudentReport);
            else if (e.Name == "accordionClassReport")
                LoadModule("Class Report", ClassAttendance.instance, accordionClassReport);
            else if (e.Name == "ace_leave_managment")
                LoadModule("Leave Managment", Leave_Managment.instance, ace_leave_managment);
            else if (e.Name == "ace_holidays")
                LoadModule("Holidays", Attendance.Holidays.instance, ace_holidays);
            else if (e.Name == "accordionDailyAttendence")
                LoadModule("Daily Attendance", DailyAttendance.instance, accordionDailyAttendence);
            else if (e.Name == "accordionSubTotalMarks")
                LoadModule("Subject Total Marks", SubjectTotalMarks.instance, accordionSubTotalMarks);
            else if (e.Name == "accordionExamList")
                LoadModule("Exam List", ExamList.instance, accordionExamList);
            else if (e.Name == "accordionStudentResult")
                LoadModule("Student Result", StdResult.instance, accordionStudentResult);
            else if (e.Name == "accordionMarksSMS")
                LoadModule("Marks SMS", SendMarksBySMS.instance, accordionMarksSMS);
            else if (e.Name == "accordionMarkSheet")
                LoadModule("Marks Sheet", MarkSheet.instance, accordionMarkSheet);
            else if (e.Name == "ace_multi_class_result")
                LoadModule("Multi Class Result", Multi_ClassResult.instance, ace_multi_class_result);
            else if (e.Name == "accordionClassResult")
                LoadModule("Class Result", ClassResult.instance, accordionClassResult);
            else if (e.Name == "accordionManageMarksGovt")
                LoadModule("Manage Marks", ManageMarksGovt.instance, accordionManageMarksGovt);
            else if (e.Name == "accordionManageMarks")
                LoadModule("Manage Marks", ManageMarks.instance, accordionManageMarks);
            else if (e.Name == "accordionClassAllExam")
                LoadModule("Class All Exam", ClassResultAll.instance, accordionClassAllExam);
            else if (e.Name == "accordionResultByDate")
                LoadModule("Result By Date", ClassResultByDateRange.instance, accordionResultByDate);
            else if (e.Name == "accordionResultbysubject")
                LoadModule("Result By Subject", ClassResultBySubjects.instance, accordionResultbysubject);
            else if (e.Name == "accordionChackList")
                LoadModule("Check List", CheckList.instance, accordionChackList);
            else if (e.Name == "accordionAwardList")
                LoadModule("Award List", AwradList.instance, accordionAwardList);
            else if (e.Name == "accordionResultsummery")
                LoadModule("Result Summary", ResultSummary.instance, accordionResultsummery);
            else if (e.Name == "accordionIncomeExpense")
                LoadModule("Income/Expence", ExpenseManagement.instance, accordionIncomeExpense);
            else if (e.Name == "accordionSalaryManagement")
                LoadModule("Salary Management", SalaryManagement.instance, accordionSalaryManagement);
            else if (e.Name == "ace_deleted_invoices")
                LoadModule("Deleted Invoices", deleted_invoices.instance, ace_deleted_invoices);
            else if (e.Name == "accordionFeeManagement")
                LoadModule("Fees Management", IncomeManagement.instance, accordionFeeManagement);
            else if (e.Name == "ace__acc_Fees_DR")
                LoadModule("Daily Fee Report", Fee_Daily_Report.instance, ace__acc_Fees_DR);
            else if (e.Name == "ace_acc_Fees_unSMS")
                LoadModule("UnPaid SMS", Unpaid_SMS.instance, ace_acc_Fees_unSMS);
            else if (e.Name == "ace_subjectwise")
                LoadModule("Subject Fees Details", Subject_wise_fee_details.instance, ace_subjectwise);
            else if (e.Name == "accordionFeeCategory")
                LoadModule("Fees Category", FeesCategory.instance, accordionFeeCategory);
            else if (e.Name == "accordionStudent_Report")
                LoadModule("Student Report", StudentFeeReport.instance, accordionStudent_Report);
            else if (e.Name == "accordionReports")
                LoadModule("Account Reports", MonthlyReports.instance, accordionReports);
            else if (e.Name == "ace_balance_sheet")
                LoadModule("Balance Sheet", Balance_Sheet.instance, ace_balance_sheet);
            else if (e.Name == "accordionActivityCatagory")
                LoadModule("Activity Category", ActivityCategory.instance, accordionActivityCatagory);
            else if (e.Name == "accordionStaffPointsReport")
                LoadModule("Staff Points", IndividualTeacherPoints.instance, accordionStaffPointsReport);
            else if (e.Name == "accordionManagePoints")
                LoadModule("Manage Points", ManagePoints.instance, accordionManagePoints);
            else if (e.Name == "accordionStudentActivity")
                LoadModule("Student Activity", ActivityStudent.instance, accordionStudentActivity);
            else if (e.Name == "accordionBell")
                LoadModule("Bell System", BellSheet.instance, accordionBell);
            else if (e.Name == "accordionFloorSheet")
                LoadModule("Floor Sheet", FloorSheet.instance, accordionFloorSheet);
            else if (e.Name == "accordionExAss_Report")
                LoadModule("Extra Lecture Assignment", ExtraLectureAssign.instance, accordionExAss_Report);
            else if (e.Name == "accordionExLecAssign")
                LoadModule("Extra Lecture", ScheduleAssign.instance, accordionExLecAssign);
            else if (e.Name == "accordionClassTimetable")
                LoadModule("Time Table", ScheduleTiming.instance, accordionClassTimetable);
            else if (e.Name == "ace_sms_history")
                LoadModule("SMS History", sms_history.instance, ace_sms_history);
            else if (e.Name == "accordionMarketingSMS")
                LoadModule("Marketing SMS", MarketingSMS.instance, accordionMarketingSMS);
            else if (e.Name == "accordionEnoticeBoard")
                LoadModule("E-notice Board", EnoticeBoard.instance, accordionEnoticeBoard);
            else if (e.Name == "accordionBackupsync")
                LoadModule("Backup & Sync", Backup.Backup.instance, accordionBackupsync);
            else if (e.Name == "accordionUsers")
                LoadModule("User Management", Users.Users.instance, accordionUsers);
            else if (e.Name == "accordionIssueBooks")
                LoadModule("Issue Books", IssueBooks.instance, accordionIssueBooks);
            else if (e.Name == "accordionAddBooks")
                LoadModule("Add Book", AddBooks.instance, accordionAddBooks);
            else if (e.Name == "ACTeacherProfile")
                LoadModule("Teacher Profile", TeacherProfile.instance, ACTeacherProfile);
            else if (e.Name == "ACCashAssets")
                LoadModule("Cash Assets", CashAssets.instance, ACCashAssets);
            else if (e.Name == "acSalaryCategory")
                LoadModule("Salary Category", SalaryCategory.instance, acSalaryCategory);
            else if (e.Name == "acr_PEF_Report")
                LoadModule("PEF Attendence Report", Pef_Attenddence_report.instance, acr_PEF_Report);
            else if (e.Name == "ace_individual_staff_att")
                LoadModule("Individual Staff Attendance", IndividualTeacherAttendance.instance, ace_individual_staff_att);
            else if (e.Name == "aceFeeinsReport")
                LoadModule("Fee Installment Report", FeeInstallmentReports.instance, aceFeeinsReport);
            else if (e.Name == "accordionAbout")
                load_about_us();
            else if (e.Name == "ace_fee_setting")
                LoadModule("Fees Setting", FeeSetting.instance, ace_fee_setting);
            else if (e.Name == "ace_transport_students")
                LoadModule("Transport Students", Transport_student.instance, ace_transport_students);
            else if (e.Name == "ace_stops")
                LoadModule("Stops", Stops.instance, ace_stops);
            else if (e.Name == "ace_Transport_rout")
                LoadModule("Transport Management", Transport.Transport.instance, ace_Transport_rout);
            else if (e.Name == "accordionMaterialCategory")
                LoadModule("Material Category", MaterialCategory.instance, accordionMaterialCategory);
            else if (e.Name == "accordionmaterialitem")
                LoadModule("Material", MaterialAssign.instance, accordionmaterialitem);
            else if (e.Name == "accordionHos_student")
                LoadModule("Hostellised Students", Hostel.Hostel_Student.instance, accordionHos_student);
            else if (e.Name == "accordionHostelRooms")
                LoadModule("Hostel Rooms Management", Hostel.HostelRooms.instance, accordionHostelRooms);
            else if(e.Name == "ace_custom_info")
                LoadModule("Customized info", custome_salary_genrate.instance, ace_custom_info);
        }
        private void background_db_backup(object sender, DoWorkEventArgs e)
        {
            string partialName = "AutoDbBackup";
            Backup.Backup.instance.TakeBackup(partialName);
        }
        void load_about_us()
        {
            //about_softpitch abtus = new about_softpitch(this);
            //LoadModule("About Us", abtus, accordionAbout);
        }
        string search = "`key`";
        string tabname = "";
        private void LoadModule(string title, Control instance, AccordionControlElement ace, string settings_val = "")
        {
            labTital.Visible = true;
            labTital.Text = title;

            if (!Container.Controls.Contains(instance))
            {
                Container.Controls.Add(instance);
                instance.Dock = DockStyle.Fill;
                Type myType = instance.GetType();
                string ac_name = ace == null ? settings_val : ace.Name;
                if (!string.IsNullOrEmpty(ac_name))
                {
                    if (ace == null)
                        search = "`subkey`";
                    else
                    {
                        int level = ace.Level;
                        if (level == 2)
                            search = "`subsubkey`";
                        else if (level == 1)
                            search = "`subkey`";
                        else
                            search = "`key`";
                    }
                }
                MethodInfo permission_method = myType.GetMethod("permissions_btns");
                if (permission_method != null && !string.IsNullOrEmpty(ac_name))
                {
                    bool add = fun.isAllow("Add", ac_name, search);
                    bool edit = fun.isAllow("Edit", ac_name, search);
                    bool delete = fun.isAllow("Delete", ac_name, search);
                    permission_method.Invoke(instance, new object[] { add, edit, delete });
                }
            }
            else
            {
                string ac_name = ace == null ? settings_val : ace.Name;
                if (!string.IsNullOrEmpty(ac_name))
                {
                    if (ace == null)
                        search = "`subkey`";
                    else
                    {
                        int level = ace.Level;
                        if (level == 2)
                            search = "`subsubkey`";
                        else if (level == 1)
                            search = "`subkey`";
                        else
                            search = "`key`";
                    }
                }
                Type myType = instance.GetType();
                MethodInfo permission_method = myType.GetMethod("permissions_btns");
                if (permission_method != null && !string.IsNullOrEmpty(ac_name))
                {
                    bool add = fun.isAllow("Add", ac_name, search);
                    bool edit = fun.isAllow("Edit", ac_name, search);
                    bool delete = fun.isAllow("Delete", ac_name, search);
                    permission_method.Invoke(instance, new object[] { add, edit, delete });
                }
                
                MethodInfo myMethod = myType.GetMethod("loadfunctions");
                if (myMethod != null)
                    myMethod.Invoke(instance, null);
                SendKeys.SendWait("~");
            }
            
            instance.BringToFront();
            fun.selectall_Controls(instance);
        }
        string per_query = "";
        List<settings_ddl> set_ddl = new List<settings_ddl>();
        public void user_permission()
        {
            per_query = "";
            string query = "SELECT * from tbl_role where permission_id = '" + Login.CurrentUserStatus_id + "' and `Type` = 'Desktop'";
            DataTable permission_dt = fun.FetchDataTable(query);
            for (int i = 0; i < accordionControlMain.Elements.Count; i++)
            {
                accordionControlMain.Elements[i].Visible = false;
                string tabname = accordionControlMain.Elements[i].Name;
                string tabtext = accordionControlMain.Elements[i].Text;
                DataRow rw_main = permission_dt.AsEnumerable().FirstOrDefault(tt => tt.Field<string>("key") == tabname && (tt.Field<string>("subkey") == null || tt.Field<string>("subkey") == ""));
                if (rw_main == null)
                {
                    if (Login.CurrentUserStatus_id == "1")
                    {
                        per_query += "INSERT INTO `tbl_role`(`key`,tab_title, `value`, `permission_id`, `IsAdd`, `IsEdit`, `IsDelete`,`Type`) VALUES " +
                            "('" + tabname + "','" + tabtext + "',1,'" + Login.CurrentUserStatus_id + "',1,1,1,'Desktop');";
                        accordionControlMain.Elements[i].Visible = true;
                    }
                }
                else
                {
                    bool val = Convert.ToBoolean(rw_main["value"]);
                    accordionControlMain.Elements[i].Visible = val;
                }

                if (!string.IsNullOrEmpty(tabtext))
                    subkey_accordian_elements_permission(i, permission_dt);
            }
            #region Settings ddl
            DataRow rw_ddl = permission_dt.AsEnumerable().FirstOrDefault(tt => tt.Field<string>("key") == "Settings" && (tt.Field<string>("subkey") == null || tt.Field<string>("subkey") == ""));
            if (rw_ddl == null)
            {
                if (Login.CurrentUserStatus_id == "1")
                    per_query += "INSERT INTO `tbl_role`(`key`,tab_title, `value`, `permission_id`, `IsAdd`, `IsEdit`, `IsDelete`,`Type`) VALUES ('Settings','Settings_list',1,'" + Login.CurrentUserStatus_id + "',0,0,0,'Desktop');";
            }
            else
            {
                COMBOXSEttings.Visible = Convert.ToBoolean(rw_ddl["value"]);
            }
            set_ddl.Clear();
            set_ddl = settings_ddl();
            for (int i = 0; i < set_ddl.Count; i++)
            {
                string set_val = set_ddl[i].value;
                string set_text = set_ddl[i].description;
                DataRow rw_ddl_lis = permission_dt.AsEnumerable().FirstOrDefault(tt => tt.Field<string>("subkey") == set_val && (string.IsNullOrEmpty(tt.Field<string>("subsubkey"))));
                if (rw_ddl_lis == null)
                {
                    if (Login.CurrentUserStatus_id == "1")
                    {
                        per_query += "INSERT INTO `tbl_role`(`key`,subkey,tab_title, `value`, `permission_id`, `IsAdd`, `IsEdit`, `IsDelete`,`Type`) VALUES ('Settings','" + set_val + "','" + set_text + "',1,'" + Login.CurrentUserStatus_id + "',1,1,1,'Desktop');";
                        COMBOXSEttings.Properties.Items.Add(set_text, set_val, set_ddl[i].image_index);
                    }
                }
                else
                {
                    if (Convert.ToBoolean(rw_ddl_lis["value"]))
                        COMBOXSEttings.Properties.Items.Add(set_text, set_val, set_ddl[i].image_index);
                }

            }
            #endregion
            if (!string.IsNullOrEmpty(per_query))
                fun.ExecuteQuery(per_query);
        }
        public void subkey_accordian_elements_permission(int tab_index, DataTable permission_dt)
        {
            for (int i = 0; i < accordionControlMain.Elements[tab_index].Elements.Count; i++)
            {
                AccordionControlElement acec = accordionControlMain.Elements[tab_index].Elements[i];
                acec.Visible = false;
                string tabname = accordionControlMain.Elements[tab_index].Name;
                string subtabname = acec.Name;
                string subtabtext = acec.Text;
                DataRow rw_sub = permission_dt.AsEnumerable().FirstOrDefault(tt => tt.Field<string>("subkey") == subtabname && (tt.Field<string>("subsubkey") == null || tt.Field<string>("subsubkey") == ""));
                if (rw_sub == null)
                {
                    if (Login.CurrentUserStatus_id == "1")
                    {
                        per_query += "INSERT INTO `tbl_role`(`key`,subkey,tab_title, `value`, `permission_id`, `IsAdd`, `IsEdit`, `IsDelete`,`Type`) VALUES " +
                        "('" + tabname + "','" + subtabname + "','" + subtabtext + "',1,'" + Login.CurrentUserStatus_id + "',1,1,1,'Desktop');";
                        accordionControlMain.Elements[tab_index].Elements[i].Visible = true;
                    }
                }
                else
                {
                    bool val = Convert.ToBoolean(rw_sub["value"]);
                    accordionControlMain.Elements[tab_index].Elements[i].Visible = val;
                }
                if (!string.IsNullOrEmpty(subtabtext))
                    subsubkey_accordian_elements_permission(tab_index, i, permission_dt);
            }
        }
        public void subsubkey_accordian_elements_permission(int tab_index, int subtab_index, DataTable permission_dt)
        {
            for (int i = 0; i < accordionControlMain.Elements[tab_index].Elements[subtab_index].Elements.Count; i++)
            {
                string tabname = accordionControlMain.Elements[tab_index].Name;
                string subtabname = accordionControlMain.Elements[tab_index].Elements[subtab_index].Name;
                AccordionControlElement acec = accordionControlMain.Elements[tab_index].Elements[subtab_index].Elements[i];
                acec.Visible = false;
                string subsubtabname = acec.Name;
                string subsubtabtext = acec.Text;
                DataRow rw_subsub = permission_dt.AsEnumerable().FirstOrDefault(tt => tt.Field<string>("subsubkey") == subsubtabname);
                if (rw_subsub == null)
                {
                    if (Login.CurrentUserStatus_id == "1")
                    {
                        per_query += "INSERT INTO `tbl_role`(`key`,subkey,`subsubkey`,tab_title, `value`, `permission_id`, `IsAdd`, `IsEdit`, `IsDelete`,`Type`) VALUES " +
                        "('" + tabname + "','" + subtabname + "','" + subsubtabname + "','" + subsubtabtext + "',1,'" + Login.CurrentUserStatus_id + "',1,1,1,'Desktop');";
                        acec.Visible = true;
                    }
                }
                else
                    acec.Visible = Convert.ToBoolean(rw_subsub["value"]);
            }
        }
        private void btnLogout_Click(object sender, EventArgs e)
        {

        }
        private void accordionHos_student_Click(object sender, EventArgs e)
        {

        }
        private void accordionmaterialitem_Click(object sender, EventArgs e)
        {

        }
        private void accordionMaterialCategory_Click(object sender, EventArgs e)
        {

        }
        private void ace_Transport_rout_Click(object sender, EventArgs e)
        {

        }
        private void ace_stops_Click(object sender, EventArgs e)
        {
        }
        private void ace_transport_students_Click(object sender, EventArgs e)
        {

        }
        private void panel2_Click(object sender, EventArgs e)
        {
            fun.loaderform(() => { LoadModule("SoftPitch Setting", XUCSoftPitch_settings.instance, null); });
        }
        private void ace_fee_setting_Click(object sender, EventArgs e)
        {

        }
        private void ace_receipt_template_Click(object sender, EventArgs e)
        {

        }
        private void txtSession_SelectedIndexChanged_2(object sender, EventArgs e)
        {
            SelectedSession = txtSession.Text;
            Login.session_name = SelectedSession;
            string SelectedSessionID = txtSession.SelectedValue.ToString();
            if (SelectedSessionID == "System.Data.DataRowView")
                return;
            else if (SelectedSessionID != "1")
            {
                String con_str = Login.constring;
                con_str = con_str.Replace("tnsbay_school", "tnsbay_school_" + SelectedSession.Trim().Replace('-', '_'));
                Login.constring = con_str;
            }
            else
            {
                string[] parts_od_string = Login.constring.Split('=', ';');
                string new_CS = "";
                int count = 0;
                foreach (string str in parts_od_string)
                {
                    count++;
                    if (str.Contains("tnsbay_school"))
                        new_CS += "tnsbay_school;";
                    else if (count % 2 == 0)
                        new_CS += str + ";";
                    else
                        new_CS += str + "=";
                }
                Login.constring = new_CS;
            }

        }
        //async 
        void chack_internet_key()
        {
            //while (true)
            //{
            chack_key_worker.WorkerReportsProgress = true;
            if (!chack_key_worker.IsBusy)
                chack_key_worker.RunWorkerAsync();
            //  await Task.Delay(30000);//30 second
            //}
        }
        private void Chack_key_worker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (fun.is_server_pc())
            {
                try
                {
                    if (is_connected)
                    {
                        barStaticItem.Caption = "online";
                        objvalidation.upgrade_key();
                        fun.Images_adds();
                    }
                    else
                        barStaticItem.Caption = "offline";

                    string[] system_info = objvalidation.activate();
                    isactive = system_info[0];
                    if (isactive == "OK") { }
                    else
                    {
                        load_about_us();
                        accordionControlMain.Visible = false;
                        MessageBox.Show(isactive, "Activation Massage", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
                catch (Exception ex)
                {
                    barStaticItem.Caption = "Error in internet Now offline";
                    MessageBox.Show(ex.Message, "Internet Error Details", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            else
                barStaticItem.Caption = "Client";
            
        }
        bool is_connected = false;
        private void Chack_key_worker_DoWork(object sender, DoWorkEventArgs e)
        {
            is_connected = fun.IsConnectedToInternet();
            if (chack_key_worker.CancellationPending)
            {
                e.Cancel = true;
                return;
            }
        }
        private void current_time_Tick(object sender, EventArgs e)
        {
            label1.Caption = DateTime.Now.ToString();
        }
        public static string currentTime = string.Empty;
        public DateTime get_server_datetime()
        {
            DateTime server_datetime = new DateTime();
            try
            {
                string machineName = "";
                if (!string.IsNullOrEmpty(Login.server_ip) && Login.server_ip != "localhost")
                {
                    machineName = Login.server_ip;//"192.168.10.2";//"KHOKHAR";

                    Process proc = new Process();
                    proc.StartInfo.UseShellExecute = false;
                    proc.StartInfo.RedirectStandardOutput = true;
                    proc.StartInfo.FileName = "net";
                    proc.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
                    proc.StartInfo.CreateNoWindow = true;
                    proc.StartInfo.Arguments = @"time \\" + machineName;
                    proc.Start();
                    proc.WaitForExit();

                    List<string> results = new List<string>();

                    while (!proc.StandardOutput.EndOfStream)
                    {
                        string currentline = proc.StandardOutput.ReadLine();

                        if (!string.IsNullOrEmpty(currentline))
                        {
                            results.Add(currentline);
                        }
                    }



                    if (results.Count > 0 && results[0].ToLower().StartsWith(@"current time at \\" + machineName.ToLower() + " is "))
                    {
                        currentTime = results[0].Substring((@"current time at \\" +
                                      machineName.ToLower() + " is ").Length);
                        if (currentTime.Contains('?'))
                            currentTime = currentTime.Replace("?", "");
                        server_datetime = DateTime.Parse(currentTime);
                    }
                }
                else
                    server_datetime = DateTime.Now;


            }
            catch (Exception ex)
            {
                MessageBox.Show("IP='" + Login.server_ip + "' and time string is = '" + currentTime + "' " + ex.Message, "Getting Server Time", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            return server_datetime;
        }
        List<settings_ddl> set = new List<settings_ddl>();
        public List<settings_ddl> settings_ddl()
        {
            settings_ddl s0 = new settings_ddl() { description = "General Setting", value = "General_Setting", image_index = 0 };
            settings_ddl s1 = new settings_ddl() { description = "SMS Template", value = "SMS_Template", image_index = 1 };
            settings_ddl s2 = new settings_ddl() { description = "SMS Service", value = "SMS_Service", image_index = 1 };
            settings_ddl s3 = new settings_ddl() { description = "SMS Server", value = "SMS_Server", image_index = 1 };
            settings_ddl s4 = new settings_ddl() { description = "Grade Setting", value = "Grade_Setting", image_index = 1 };
            settings_ddl s5 = new settings_ddl() { description = "Session Setting", value = "Session_Setting", image_index = 3 };
            settings_ddl s6 = new settings_ddl() { description = "Bell Setting", value = "Bell_Setting", image_index = 6 };
            settings_ddl s7 = new settings_ddl() { description = "BioMatric Setting", value = "BioMatric_Setting", image_index = 7 };
            set.Add(s0);
            set.Add(s1);
            set.Add(s2);
            set.Add(s3);
            set.Add(s4);
            set.Add(s5);
            set.Add(s6);
            set.Add(s7);
            return set;

        }

        public void changeSettingcombo()
        {
            COMBOXSEttings.EditValue = 0;
        }
        private void COMBOXSEttings_EditValueChanged(object sender, EventArgs e)
        {
            if (COMBOXSEttings.EditValue.ToString() == "0")
                COMBOXSEttings.Width = 40;
            else
            {
                COMBOXSEttings.Width = 140;
                if (COMBOXSEttings.EditValue.ToString() == "General_Setting")
                    fun.loaderform(() => { LoadModule(COMBOXSEttings.Text, GerenalSetting.instance, null, COMBOXSEttings.EditValue.ToString()); });
                else if (COMBOXSEttings.EditValue.ToString() == "SMS_Template")
                    fun.loaderform(() => { LoadModule(COMBOXSEttings.Text, SMSTemplate.instance, null, COMBOXSEttings.EditValue.ToString()); });
                else if (COMBOXSEttings.EditValue.ToString() == "SMS_Service")
                    fun.loaderform(() => { LoadModule(COMBOXSEttings.Text, SMSService.instance, null, COMBOXSEttings.EditValue.ToString()); });
                else if (COMBOXSEttings.EditValue.ToString() == "SMS_Server")
                    fun.loaderform(() => { LoadModule(COMBOXSEttings.Text, SMSServer.instance, null, COMBOXSEttings.EditValue.ToString()); });
                else if (COMBOXSEttings.EditValue.ToString() == "Grade_Setting")
                    fun.loaderform(() => { LoadModule(COMBOXSEttings.Text, ExamGrades.instance, null, COMBOXSEttings.EditValue.ToString()); });
                else if (COMBOXSEttings.EditValue.ToString() == "Session_Setting")
                    fun.loaderform(() => { LoadModule(COMBOXSEttings.Text, ManageSession.instance, null, COMBOXSEttings.EditValue.ToString()); });
                else if (COMBOXSEttings.EditValue.ToString() == "Bell_Setting")
                    fun.loaderform(() => { LoadModule(COMBOXSEttings.Text, BellSetting.instance, null, COMBOXSEttings.EditValue.ToString()); });
                else if (COMBOXSEttings.EditValue.ToString() == "BioMatric_Setting")
                    fun.loaderform(() => { LoadModule(COMBOXSEttings.Text, biomatric_devices.instance, null, COMBOXSEttings.EditValue.ToString()); });
                changeSettingcombo();
            }
        }
        void logout()
        {
            this.Visible = false;
            this.Close();
            Login a = new Login();
            a.ShowDialog();
        }
        public void load_profile_list()
        {
            com_logout.Properties.Items.Clear();
            com_logout.Properties.Items.Add(Login.CurrentUserName, "0", 10);
            com_logout.Properties.Items.Add("Change Password", "Change_Password", 8);
            com_logout.Properties.Items.Add("Logout", "Logout", 9);
            profile_combo();
        }
        public void profile_combo()
        {
            com_logout.EditValue = "0";

        }
        private void com_logout_EditValueChanged(object sender, EventArgs e)
        {

            if (com_logout.EditValue.ToString() != "0")
            {
                if (com_logout.EditValue.ToString() == "Logout")
                    logout();
                else if (com_logout.EditValue.ToString() == "Change_Password")
                {
                    using (Change_Password CP = new Change_Password(this))
                    {
                        if (CP.ShowDialog() == DialogResult.Yes)
                        { }
                        else
                        {

                        }
                    }
                }
                profile_combo();
            }
        }

        private void Main_FD_Leave(object sender, EventArgs e)
        {
            GC.Collect();
            GC.WaitForPendingFinalizers();
        }

        private void btn_update_activation_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            string[] system_info = objvalidation.activate();
            isactive = system_info[0];
            if (isactive == "OK")
            {
                accordionControlMain.Visible = true;
                return;
            }

            
            if (fun.is_server_pc())
            {
                try
                {
                    is_connected = fun.IsConnectedToInternet();
                    if (is_connected)
                    {
                        barStaticItem.Caption = "online";
                        objvalidation.upgrade_key();
                        
                        fun.Images_adds();
                    }
                    else
                        barStaticItem.Caption = "offline";
                }
                catch (Exception ex)
                {
                    barStaticItem.Caption = "Error in internet Now offline";
                    MessageBox.Show(ex.Message, "Internet Error Details", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            else
                barStaticItem.Caption = "Client";

            system_info = objvalidation.activate();
            isactive = system_info[0];
            if (isactive == "OK")
            {
                accordionControlMain.Visible = true;
            }
            else
            {
                load_about_us();
                accordionControlMain.Visible = false;
            }
        }
    }
}
