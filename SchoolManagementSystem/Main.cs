using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Repository;
using MySql.Data.MySqlClient;
using SchoolManagementSystem.Admin;
using SchoolManagementSystem.Attendance;
using SchoolManagementSystem.Class;
using SchoolManagementSystem.Discipline;
using SchoolManagementSystem.Exam;
using SchoolManagementSystem.Fees;
using SchoolManagementSystem.Library;
using SchoolManagementSystem.Principal;
using SchoolManagementSystem.Students;
using SchoolManagementSystem.Time;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Media;
using System.Windows.Forms;

namespace SchoolManagementSystem
{
    public partial class Main : DevExpress.XtraEditors.XtraForm
    {
        public static string SelectedSession;
        CommonFunctions fun = new CommonFunctions();
        private BackgroundWorker Sync_Worker = new BackgroundWorker();
        private BackgroundWorker Bill_Worker = new BackgroundWorker();

        int Students = 0;
        int Class = 0;
        int Attendance = 0;
        int Exam = 0;
        // int Fees = 0;
        int Library = 0;
        int Setting = 0;
        // int Principal = 0;
        #region UserControl
        //===================== Students ===================================
        AddStudent modStudent;
        ManageParents modMParents;
        AddBulkStudent modAddBulkStudent;
        PassOutStudents modPassoutStudents;
        BulkTransfer modBulkTransfer;
        VisitingInformation modvisitng;

        int SS, SP, SB, SPO, SV = 0,FlagTransfer;

        //===================== Class ===================================
        ManageTeacher modTeacher;
        ManageClass modClass;
        ManageSection modSections;
        Subjects modSubjects;
        //   ClassRoutine modClassRoutine;
        ExperienceLetter modExperienceLetter;
        ClassScheduleTiming modCSTiming;
        int CT, CC, CS, CSU, CR, CE = 0;
        //===================== Attendance ===================================
        DailyAttendance modDailyA;
        ClassAttendance modClassA;
        SMSAttendance modSMSA;
        StudentAttendance modStudentA;
        TeacherAttendance modTA;
        TeacherMAttendance modTMA;
        IndividualTeacherAttendance modITA;
        int ASD, ASC, ASS, ASMS, ATD, ATM, AIT = 0;
        //===================== Exam ===================================
        ExamList modExamList;
        SubjectTotalMarks modTotalMarks;
        MarkSheet modMarkSheet;
        ManageMarks modManageMarks;
        ClassResult modClassResult;
        SendMarksBySMS modSendMarksbySMS;
        StdResult modStdResult;
        AwradList modAwradList;
        ClassResultBySubjects modresultBySubject;
        ClassResultByDateRange modresltByDate;
        CheckList modCheckList;
        ClassResultAll modClassAllResult;
        ResultSummary modResultSummary;

        int EL, ET, EMS, EMM, ECR, ESSMS, ESR, EAL, ERBS, ECL = 0, ERBD = 0, MRS = 0;
        //===================== Fees ===================================

        ExpenseManagement modFExpanseManage;
        IncomeManagement modFIncomeManage;
        FeeInstallmentReports modFInstallmentReports;
        FeesCategory modFeesCategory;
        SalaryCategory modSalaryCategory;
        SalaryManagement modFSalaryManagement;
        MonthlyReports modFMonthlyReport;
        StudentFeeReport modFStdReport;
        CashAssets modCashAssets;
        int FEM, FIM, FIMC, FSM, FMR, SALARYFLAG, FSR = 0, CashAssetsFlag = 0, FeeInstallRepFlag = 0;

        //================= TimeTable ==============================
        TeacherProfile modTTeacherProfile;
        ScheduleTiming modTSchduleTiming;
        ScheduleAssign modTSchduleAssign;
        ExtraLectureAssign modTLectureAssign;
        FloorSheet modTFloorSheet;
        BellSheet modTBellSheet;
        int TTP, TCS, TFS,TAS, TB = 0,ELA;
        //================= Library ==============================
        AddBooks modLAddBook;
        IssueBooks modLIssueBook;

        //================= Setting ==============================
        GerenalSetting modAGerenalSetting;
        SMSTemplate modASmsSetting;
        SMSService modAsmsService;
        SMSServer modAsmsServer;
        FeeSetting modAFeeSetting;
        ExamGrades modAExamGrades;
        ManageSession modASession;
        BellSetting modABellSetting;

        //================= Principal ==============================
        Scheduling modSchedulig;
        Visitor modVisitor;
        AttendanceSummery modAttSummery;
        PDashboard modPDash;
        //  BoysAttendanceSummery modBoysAttSummery;

        int PS, PVL, PAS, PPD = 0;

        #endregion

        public Main()
        {
            InitializeComponent();
            allClass.Clear();
            txtSession.Properties.Items.Clear();
            allClass = fun.GetAllSession();
            foreach (var allclass in allClass)
                txtSession.Properties.Items.Add(allclass.Name);
            txtSession.Text = fun.GetDefaultSessionName();
            activate();
            // LoadTransport();
            // LoadHostel();

            //synchronization on server
            var sync_flag = fun.GetSettings("sync_flag");
            /*
            if (sync_flag == "1")
            {
                Sync_Worker.DoWork += new DoWorkEventHandler(Sync_Worker_DoWork);
                Sync_Worker.RunWorkerCompleted += new RunWorkerCompletedEventHandler(Sync_Worker_RunWorkerCompleted);
                //Sync_Worker.ProgressChanged += new ProgressChangedEventHandler(Sync_Worker_ProgressChanged);
                Sync_Worker.WorkerReportsProgress = true;
                Sync_Worker.WorkerSupportsCancellation = true;
                var sync_timer = fun.GetSettings("sync_timer");
                timer.Interval = fun.ConvertMinutesToMilliseconds(Convert.ToInt32(sync_timer));
                timer.Enabled = true;
            }
            
            else
            */
                timer.Enabled = false;
            
        }



        private void LoadModule(XtraUserControl UC, XtraUserControl module)
        {
            if (UC.Controls.Count > 0)
                UC.Controls.Clear();
            module.Dock = DockStyle.Fill;
            UC.Controls.Add(module);
        }
        private void Main_Load(object sender, EventArgs e)
        {
            labTital.Text = fun.GetSettings("system_title");
        }

        #region Activition 
        string Key = "0";
        string Days;
        string SchoolCode;
        static double DayRemaining;

        void activate()
        {
            systemvalidation objvalidation = new systemvalidation();
            string activation_key = fun.GetSettings("activition_key");
            string school_code = fun.GetSettings("school_code");
            string response = objvalidation.check_registeration(activation_key, school_code);
            string key_info = objvalidation.extract_keyinfo_softpitch(activation_key);
            label_keyinfo_np.Text = key_info;
            if (response == "OK")
            {
                items();
                user();
                
            }
            else
            {
                navigationPageMDashboard.PageVisible = false;
                navigationPageMPDashboard.PageVisible = false;
                navigationPageMStudents.PageVisible = false;
                navigationPageMExamination.PageVisible = false;
                navigationPageMAttendance.PageVisible = false;
                navigationPageMAccounts.PageVisible = false;
                navigationPageMClass.PageVisible = false;
                navigationPageMNoticeboard.PageVisible = false;
                navigationPageMLibrary.PageVisible = false;
                navigationPageMHostel.PageVisible = false;
                navigationPageMTransport.PageVisible = false;
                navigationPageMPrincipal.PageVisible = false;
                navigationPageMTimeTable.PageVisible = false;
                navigationPageMSync.PageVisible = false;
                navigationPageMUsers.PageVisible = false;
                navigationPageMSettings.PageVisible = false;
                labAC.Visible = true;
                txtActivite.Visible = true;
                BtnACTIVITE.Visible = true;

            }
        }
        
        public void user()
        {
            if (Login.CurrentUserStatus_id == "2")
            {
                navigationPane1.SelectedPage = navigationPageMPDashboard;
                navigationPane1.ItemOrientation = Orientation.Vertical;
                //     navigationPane1.PageProperties.ShowMode = DevExpress.XtraBars.Navigation.ItemShowMode.ImageOrText;
            }
            MySqlConnection con = new MySqlConnection(Login.constring);
            con.Open();
            MySqlCommand cmd = new MySqlCommand("SELECT tbl_role.key as k,tbl_role.value as v FROM admin  join tbl_permission on tbl_permission.permission_id=admin.level join tbl_role on tbl_role.permission_id = tbl_permission.permission_id where admin.email = '" + Login.CurrentUserEmail + "'", con);
            MySqlDataReader reader1 = cmd.ExecuteReader();
            if (reader1.HasRows)
            {
                while (reader1.Read())
                {
                    if (reader1["k"].ToString() == "student" && reader1["v"].ToString() == "True")
                        navigationPageMStudents.PageVisible = true;
                    if (reader1["k"].ToString() == "visiting_information" && reader1["v"].ToString() == "True")
                        NBarItemSVisitingInformation.Visible = true;
                    if (reader1["k"].ToString() == "registration" && reader1["v"].ToString() == "True")
                        NBarItemSAddStudent.Visible = true;
                    if (reader1["k"].ToString() == "manage_parent" && reader1["v"].ToString() == "True")
                        NBarItemSManageParents.Visible = true;
                    if (reader1["k"].ToString() == "bulk_of_student" && reader1["v"].ToString() == "True")
                        NBarItemSAddBulkofStudent.Visible = true;
                    if (reader1["k"].ToString() == "passout" && reader1["v"].ToString() == "True")
                        NBarItemSPassout.Visible = true;

                    if (reader1["k"].ToString() == "tabclass" && reader1["v"].ToString() == "True")
                        navigationPageMClass.PageVisible = true;
                    if (reader1["k"].ToString() == "staff" && reader1["v"].ToString() == "True")
                        NBarItemCManageTeacher.Visible = true;
                    if (reader1["k"].ToString() == "classes" && reader1["v"].ToString() == "True")
                        NBarItemCManageClass.Visible = true;
                    if (reader1["k"].ToString() == "section" && reader1["v"].ToString() == "True")
                        NBarItemCManageSection.Visible = true;
                    if (reader1["k"].ToString() == "subject" && reader1["v"].ToString() == "True")
                        NBarItemCSubjects.Visible = true;
                    if (reader1["k"].ToString() == "experience_letter" && reader1["v"].ToString() == "True")
                        NBarItemCExperienceLetter.Visible = true;

                    if (reader1["k"].ToString() == "attendance" && reader1["v"].ToString() == "True")
                        navigationPageMAttendance.PageVisible = true;
                    if (reader1["k"].ToString() == "daily_attendance" && reader1["v"].ToString() == "True")
                        NBarItemDailyAttendance.Visible = true;
                    if (reader1["k"].ToString() == "class_attendance" && reader1["v"].ToString() == "True")
                        NBarItemCAttendance.Visible = true;
                    if (reader1["k"].ToString() == "student_report" && reader1["v"].ToString() == "True")
                        NBarItemStudentAttendance.Visible = true;
                    if (reader1["k"].ToString() == "attendance_sms" && reader1["v"].ToString() == "True")
                        NBarItemSMSAttendance.Visible = true;
                    if (reader1["k"].ToString() == "daily_staff_attendance" && reader1["v"].ToString() == "True")
                        NBarItemTeacherAttendance.Visible = true;
                    if (reader1["k"].ToString() == "staff_attendance" && reader1["v"].ToString() == "True")
                        NBarItemTMA.Visible = true;
                    if (reader1["k"].ToString() == "staff_report" && reader1["v"].ToString() == "True")
                        NBarItemTIA.Visible = true;

                    if (reader1["k"].ToString() == "examination" && reader1["v"].ToString() == "True")
                        navigationPageMExamination.PageVisible = true;
                    if (reader1["k"].ToString() == "exam_list" && reader1["v"].ToString() == "True")
                        NBarItemEExamList.Visible = true;
                    if (reader1["k"].ToString() == "subject_total_marks" && reader1["v"].ToString() == "True")
                        NBarItemESubjectTotalMarks.Visible = true;
                    if (reader1["k"].ToString() == "marks_sheet" && reader1["v"].ToString() == "True")
                        navigationBarItem1.Visible = true;
                    if (reader1["k"].ToString() == "manage_marks" && reader1["v"].ToString() == "True")
                        NBarItemEManageMarks.Visible = true;
                    if (reader1["k"].ToString() == "class_result" && reader1["v"].ToString() == "True")
                        NBarItemEClassResult.Visible = true;
                    if (reader1["k"].ToString() == "std_report" && reader1["v"].ToString() == "True")
                        NBarItemEStdResult.Visible = true;
                    if (reader1["k"].ToString() == "send_marks_sms" && reader1["v"].ToString() == "True")
                        NBarItemESendMarksBySms.Visible = true;
                    if (reader1["k"].ToString() == "award_list" && reader1["v"].ToString() == "True")
                        NBarItemEAwardList.Visible = true;
                    if (reader1["k"].ToString() == "result_by_subject" && reader1["v"].ToString() == "True")
                        NBarItemEResultBySubject.Visible = true;
                    if (reader1["k"].ToString() == "result_by_daterange" && reader1["v"].ToString() == "True")
                        NBarItemEResultByDate.Visible = true;
                    if (reader1["k"].ToString() == "check_list" && reader1["v"].ToString() == "True")
                        NBarItemECheckList.Visible = true;


                    if (reader1["k"].ToString() == "accounts" && reader1["v"].ToString() == "True")
                        navigationPageMAccounts.PageVisible = true;
                    if (reader1["k"].ToString() == "fee_management" && reader1["v"].ToString() == "True")
                        FNBarItemFeeManagement.Visible = true;
                        FInstallmentReports.Visible = true;
                    if (reader1["k"].ToString() == "expance_income" && reader1["v"].ToString() == "True")
                        FNBarItemIncomeExpenseManagement.Visible = true;
                    if (reader1["k"].ToString() == "salary_management" && reader1["v"].ToString() == "True")
                        FNBarItemSalaryManagement.Visible = true;
                    if (reader1["k"].ToString() == "reports" && reader1["v"].ToString() == "True")
                        FNBarItemReport.Visible = true;

                    if (reader1["k"].ToString() == "timetable" && reader1["v"].ToString() == "True")
                        navigationPageMTimeTable.PageVisible = true;
                    if (reader1["k"].ToString() == "teacher_profile" && reader1["v"].ToString() == "True")
                        NBarItemTTP.Visible = true;
                    if (reader1["k"].ToString() == "class_schedule" && reader1["v"].ToString() == "True")
                        NBarItemTCS.Visible = true;
                    if (reader1["k"].ToString() == "floor_sheet" && reader1["v"].ToString() == "True")
                        NBarItemTFS.Visible = true;
                    if (reader1["k"].ToString() == "bell_system" && reader1["v"].ToString() == "True")
                        NBarItemTB.Visible = true;

                    if (reader1["k"].ToString() == "noticeboard" && reader1["v"].ToString() == "True")
                    {
                        navigationPageMNoticeboard.PageVisible = true;
                    }
                    if (reader1["k"].ToString() == "sync" && reader1["v"].ToString() == "True")
                        navigationPageMSync.PageVisible = true;
                    if (reader1["k"].ToString() == "setting" && reader1["v"].ToString() == "True")
                        navigationPageMSettings.PageVisible = true;
                    if (reader1["k"].ToString() == "gerenal_setting" && reader1["v"].ToString() == "True")
                        NBarAItemGerenalSetting.Visible = true;
                    if (reader1["k"].ToString() == "sms_template" && reader1["v"].ToString() == "True")
                        NBarAItemSMSTemplate.Visible = true;
                    if (reader1["k"].ToString() == "sms_service" && reader1["v"].ToString() == "True")
                        NBarAItemSMSService.Visible = true;
                    if (reader1["k"].ToString() == "sms_server" && reader1["v"].ToString() == "True")
                        NBarAItemSMSServer.Visible = true;
                    if (reader1["k"].ToString() == "fee_setting" && reader1["v"].ToString() == "True")
                        NBarAItemFeeSetting.Visible = true;
                    if (reader1["k"].ToString() == "grades_setting" && reader1["v"].ToString() == "True")
                        NBarAItemGradesSeting.Visible = true;
                    if (reader1["k"].ToString() == "session_setting" && reader1["v"].ToString() == "True")
                        NBarAItemManageSession.Visible = true;


                    if (reader1["k"].ToString() == "p_dashboard" && reader1["v"].ToString() == "True")
                        navigationPageMPDashboard.PageVisible = true;
                    if (reader1["k"].ToString() == "dashboard" && reader1["v"].ToString() == "True")
                        navigationPageMDashboard.PageVisible = true;

                    if (reader1["k"].ToString() == "principal" && reader1["v"].ToString() == "True")
                        navigationPageMPrincipal.PageVisible = false;
                    if (reader1["k"].ToString() == "users" && reader1["v"].ToString() == "True")
                    {
                        navigationPageMUsers.PageVisible = true;
                        LoadUser();
                    }
                }
            }
            con.Close();


        }
        void items()
        {
            navigationPageMDashboard.PageVisible = false;
            navigationPageMPDashboard.PageVisible = false;
            navigationPageMStudents.PageVisible = false;
            navigationPageMClass.PageVisible = false;
            navigationPageMAttendance.PageVisible = false;
            navigationPageMExamination.PageVisible = false;
            navigationPageMAccounts.PageVisible = false;
            navigationPageMNoticeboard.PageVisible = false;
            navigationPageMLibrary.PageVisible = false;
            navigationPageMHostel.PageVisible = false;
            navigationPageMTransport.PageVisible = false;
            navigationPageMPrincipal.PageVisible = false;
            navigationPageMTimeTable.PageVisible = false;
            navigationPageMSync.PageVisible = false;
            navigationPageMUsers.PageVisible = false;
            navigationPageMSettings.PageVisible = false;
            navigationPageMSwitchUser.PageVisible = true;
            navigationPageMAbout.PageVisible = true;

            NBarItemSVisitingInformation.Visible = false;
            NBarItemSAddStudent.Visible = false;
            NBarItemSManageParents.Visible = false;
            NBarItemSAddBulkofStudent.Visible = false;
            NBarItemSPassout.Visible = false;

            NBarItemCManageTeacher.Visible = false;
            NBarItemCManageClass.Visible = false;
            NBarItemCManageSection.Visible = false;
            NBarItemCSubjects.Visible = false;
            NBarItemCExperienceLetter.Visible = false;

            NBarItemDailyAttendance.Visible = false;
            NBarItemCAttendance.Visible = false;
            NBarItemStudentAttendance.Visible = false;
            NBarItemSMSAttendance.Visible = false;
            NBarItemTeacherAttendance.Visible = false;
            NBarItemTMA.Visible = false;
            NBarItemTIA.Visible = false;

            NBarItemEExamList.Visible = false;
            NBarItemESubjectTotalMarks.Visible = false;
            navigationBarItem1.Visible = false;
            NBarItemEManageMarks.Visible = false;
            NBarItemEClassResult.Visible = false;
            NBarItemEStdResult.Visible = false;
            NBarItemESendMarksBySms.Visible = false;
            NBarItemEAwardList.Visible = false;
            NBarItemEResultBySubject.Visible = false;
            NBarItemEResultByDate.Visible = false;
            NBarItemECheckList.Visible = false;

            FNBarItemFeeManagement.Visible = false;
            FInstallmentReports.Visible = false;
            FNBarItemIncomeExpenseManagement.Visible = false;
            FNBarItemSalaryManagement.Visible = false;
            FNBarItemReport.Visible = false;

            NBarItemTTP.Visible = false;
            NBarItemTCS.Visible = false;
            NBarItemTFS.Visible = false;
            NBarItemTB.Visible = false;

            NBarAItemGerenalSetting.Visible = false;
            NBarAItemSMSTemplate.Visible = false;
            NBarAItemSMSService.Visible = false;
            NBarAItemSMSServer.Visible = false;
            NBarAItemFeeSetting.Visible = false;
            NBarAItemGradesSeting.Visible = false;
            NBarAItemManageSession.Visible = false;

            string school_code = fun.GetSettings("school_code");
            if(school_code != "SSA2018")
            {
                //NBarResultSummary.Visible = false;
            }


        }
        #endregion

        #region Students

        private void officeNavigationBarStudents_ItemClick(object sender, DevExpress.XtraBars.Navigation.NavigationBarItemEventArgs e)
        {
            if (e.Item == NBarItemSAddStudent)
            {
                if (SS == 0)
                {
                    modStudent = new AddStudent();
                    SS = 1;
                }
                LoadModule(UCStudentes, modStudent);
            }
            else if (e.Item == NBarItemSManageParents)
            {
                if (SP == 0)
                {
                    modMParents = new ManageParents();
                    SP = 1;
                }
                LoadModule(UCStudentes, modMParents);
            }
            else if (e.Item == NBarItemSAddBulkofStudent)
            {
                if (SB == 0)
                {
                    modAddBulkStudent = new AddBulkStudent();
                    SB = 1;
                }
                LoadModule(UCStudentes, modAddBulkStudent);
            }
            else if (e.Item == NBarItemSPassout)
            {
                if (SPO == 0)
                {
                    modPassoutStudents = new PassOutStudents();
                    SPO = 1;
                }
                LoadModule(UCStudentes, modPassoutStudents);
            }
            else if (e.Item == NBarItemSTransfer)
            {
                if (FlagTransfer == 0)
                {
                    modBulkTransfer = new BulkTransfer();
                    FlagTransfer = 1;
                }
                LoadModule(UCStudentes, modBulkTransfer);
            }
            else if (e.Item == NBarItemSVisitingInformation)
            {
                if (SV == 0)
                {
                    modvisitng = new VisitingInformation();
                    SV = 1;
                }
                LoadModule(UCStudentes, modvisitng);
            }
        }
        #endregion

        #region Class
        private void officeNavigationBarClass_ItemClick(object sender, DevExpress.XtraBars.Navigation.NavigationBarItemEventArgs e)
        {
            if (e.Item == NBarItemCManageTeacher)
            {
                if (CT == 0)
                {
                    modTeacher = new ManageTeacher();
                    CT = 1;
                }
                LoadModule(UCClass, modTeacher);
            }

            else if (e.Item == NBarItemCManageClass)
            {
                if (CC == 0)
                {
                    modClass = new ManageClass();
                    CC = 1;
                }
                LoadModule(UCClass, modClass);
            }
            else if (e.Item == NBarItemCManageSection)
            {
                if (CS == 0)
                {
                    modSections = new ManageSection();
                    CS = 1;
                }
                LoadModule(UCClass, modSections);
            }
            else if (e.Item == NBarItemCSubjects)
            {
                if (CSU == 0)
                {
                    modSubjects = new Subjects();
                    CSU = 1;
                }
                LoadModule(UCClass, modSubjects);
            }

            else if (e.Item == NBarItemCExperienceLetter)
            {
                if (CE == 0)
                {
                    modExperienceLetter = new ExperienceLetter();
                    CE = 1;
                }
                LoadModule(UCClass, modExperienceLetter);
            }
            else
            {
                if (CR == 0)
                {
                    modCSTiming = new ClassScheduleTiming();
                    CR = 1;
                }
                LoadModule(UCClass, modCSTiming);
            }
        }
        #endregion

        #region Attendance
        private void officeNavigationBarAttendance_ItemClick(object sender, DevExpress.XtraBars.Navigation.NavigationBarItemEventArgs e)
        {
            // MessageBox.Show("Current feature is not available.\nPlease contact with TNS Bay Administration", "Info");

            if (e.Item == NBarItemDailyAttendance)
            {
                if (ASD == 0)
                {
                    modDailyA = new DailyAttendance();
                    ASD = 1;
                }
                LoadModule(UCAttendance, modDailyA);
            }
            else if (e.Item == NBarItemCAttendance)
            {
                if (ASC == 0)
                {
                    modClassA = new ClassAttendance();
                    ASC = 1;
                }
                LoadModule(UCAttendance, modClassA);
            }
            else if (e.Item == NBarItemStudentAttendance)
            {
                if (ASS == 0)
                {
                    modStudentA = new StudentAttendance();
                    ASS = 1;
                }
                LoadModule(UCAttendance, modStudentA);
            }
            else if (e.Item == NBarItemSMSAttendance)
            {
                if (ASMS == 0)
                {
                    modSMSA = new SMSAttendance();
                    ASMS = 1;
                }
                LoadModule(UCAttendance, modSMSA);
            }
            else if (e.Item == NBarItemTeacherAttendance)
            {
                if (ATD == 0)
                {
                    modTA = new TeacherAttendance();
                    ATD = 1;
                }
                LoadModule(UCAttendance, modTA);
            }
            else if (e.Item == NBarItemTMA)
            {
                if (ATM == 0)
                {
                    modTMA = new TeacherMAttendance();
                    ATM = 1;
                }
                LoadModule(UCAttendance, modTMA);
            }
            else if (e.Item == NBarItemTIA)
            {
                if (AIT == 0)
                {
                    modITA = new IndividualTeacherAttendance();
                    AIT = 1;
                }
                LoadModule(UCAttendance, modITA);
            }
        }
        #endregion

        #region Exam
        private void officeNavigationBarExam_ItemClick(object sender, DevExpress.XtraBars.Navigation.NavigationBarItemEventArgs e)
        {
            if (e.Item == NBarItemEExamList)
            {
                if (EL == 0)
                {
                    modExamList = new ExamList();
                    EL = 1;
                }
                LoadModule(UCExam, modExamList);
            }
            else if (e.Item == NBarItemESubjectTotalMarks)
            {
                if (ET == 0)
                {
                    modTotalMarks = new SubjectTotalMarks();
                    ET = 1;
                }
                LoadModule(UCExam, modTotalMarks);
            }
            else if (e.Item == navigationBarItem1)
            {
                if (EMS == 0)
                {
                    modMarkSheet = new MarkSheet();
                    EMS = 1;
                }
                LoadModule(UCExam, modMarkSheet);
            }
            else if (e.Item == NBarItemEManageMarks)
            {
                if (EMM == 0)
                {
                    modManageMarks = new ManageMarks();
                    EMM = 1;
                }
                LoadModule(UCExam, modManageMarks);
            }
            else if (e.Item == NBarItemEClassResult)
            {
                if (ECR == 0)
                {
                    modClassResult = new ClassResult();
                    ECR = 1;
                }
                LoadModule(UCExam, modClassResult);
            }
            else if (e.Item == NBarItemEStdResult)
            {
                if (ESR == 0)
                {
                    modStdResult = new StdResult();
                    ESR = 1;
                }
                LoadModule(UCExam, modStdResult);
            }
            else if (e.Item == NBarItemESendMarksBySms)
            {
                if (ESSMS == 0)
                {
                    modSendMarksbySMS = new SendMarksBySMS();
                    ESSMS = 1;
                }
                LoadModule(UCExam, modSendMarksbySMS);
            }
            else if (e.Item == NBarItemEAwardList)
            {
                if (EAL == 0)
                {
                    modAwradList = new AwradList();
                    EAL = 1;
                }
                LoadModule(UCExam, modAwradList);
            }
            else if (e.Item == NBarItemEResultBySubject)
            {
                if (ERBS == 0)
                {
                    modresultBySubject = new ClassResultBySubjects();
                    ERBS = 1;
                }
                LoadModule(UCExam, modresultBySubject);
            }
            else if (e.Item == NBarItemEResultByDate)
            {
                if (ERBD == 0)
                {
                    modresltByDate = new ClassResultByDateRange();
                    ERBD = 1;
                }
                LoadModule(UCExam, modresltByDate);
            }
            else if (e.Item == NBarItemECheckList)
            {
                if (ECL == 0)
                {
                    modCheckList = new CheckList();
                    ECL = 1;
                }
                LoadModule(UCExam, modCheckList);
            }
            else if (e.Item == NBarItemAllExam)
            {
                if (ECL == 0)
                {
                    modClassAllResult = new ClassResultAll();
                    ECL = 1;
                }
                LoadModule(UCExam, modClassAllResult);
            }
            else if (e.Item == NBarResultSummary)
            {
                if (MRS == 0)
                {
                    modResultSummary = new ResultSummary();
                    MRS = 1;
                }
                LoadModule(UCExam, modResultSummary);
            }
        }
        #endregion

        #region Accounts Management
        private void officeNavigationBarFees_ItemClick(object sender, DevExpress.XtraBars.Navigation.NavigationBarItemEventArgs e)
        {
            //  MessageBox.Show("Current feature is not available.\nPlease contact with TNS Bay Administration", "Info");
            if (e.Item == FNBarItemFeeManagement)
            {
                if (FIM == 0)
                {
                    modFIncomeManage = new IncomeManagement();
                    FIM = 1;
                }
                LoadModule(UCFees, modFIncomeManage);
            }
            else if (e.Item == FInstallmentReports)
            {
                if (FeeInstallRepFlag == 0)
                {
                    modFInstallmentReports = new FeeInstallmentReports();
                    FeeInstallRepFlag = 1;
                }
                LoadModule(UCFees, modFInstallmentReports);
            }
            
            else if (e.Item == FNBarItemIncomeExpenseManagement)
            {
                if (FEM == 0)
                {
                    modFExpanseManage = new ExpenseManagement();
                    FEM = 1;
                }
                LoadModule(UCFees, modFExpanseManage);
            }
            if (e.Item == NavbarFeesCategory)
            {
                modFeesCategory = new FeesCategory();
                LoadModule(UCFees, modFeesCategory);
            }
            if (e.Item == NavbarSalaryCategory)
            {

                modSalaryCategory = new SalaryCategory();
                LoadModule(UCFees, modSalaryCategory);
            }
            if (e.Item == NavbarCashAssets)
            {

                if (CashAssetsFlag == 0)
                {
                    modCashAssets = new CashAssets();
                    CashAssetsFlag = 1;
                }
                LoadModule(UCFees, modCashAssets);
            }



            else if (e.Item == FNBarItemSalaryManagement)
            {
                if (FSM == 0)
                {
                    modFSalaryManagement = new SalaryManagement();
                    FSM = 1;
                }
                LoadModule(UCFees, modFSalaryManagement);
            }
            else if (e.Item == FNBarItemReport)
            {
                if (FMR == 0)
                {
                    modFMonthlyReport = new MonthlyReports();
                    FMR = 1;
                }
                LoadModule(UCFees, modFMonthlyReport);
            }
            else if (e.Item == FNBarItemStudentReport)
            {
                if (FSR == 0)
                {
                    modFStdReport = new StudentFeeReport();
                    FSR = 1;
                }
                LoadModule(UCFees, modFStdReport);
            }
        }
        #endregion

        ObservableCollection<AllClass> allClass = new ObservableCollection<AllClass>();

        #region Library
        private void officeNavigationBarLibrary_ItemClick(object sender, DevExpress.XtraBars.Navigation.NavigationBarItemEventArgs e)
        {
            //  MessageBox.Show("Current feature is not available.\nPlease contact with TNS Bay Administration", "Info");

            if (e.Item == LNBarItemIssueBooks)
            {
                LoadModule(UCL, modLIssueBook);
            }
            else if (e.Item == LNBarItemAddBooks)
            {
                LoadModule(UCL, modLAddBook);
            }
        }


        #endregion

        #region TimeTable
        private void officeNavigationBarTimeTable_ItemClick(object sender, DevExpress.XtraBars.Navigation.NavigationBarItemEventArgs e)
        {
            if (e.Item == NBarItemTTP)
            {
                if (TTP == 0)
                {
                    modTTeacherProfile = new TeacherProfile();
                    TTP = 1;
                }
                LoadModule(UCTimeTable, modTTeacherProfile);
            }
            if (e.Item == NBarItemTCS)
            {
                if (TCS == 0)
                {
                    modTSchduleTiming = new ScheduleTiming();
                    TCS = 1;
                }
                LoadModule(UCTimeTable, modTSchduleTiming);
            }
            if (e.Item == NBarItemAssign)
            {
                if (TAS == 0)
                {
                    modTSchduleAssign = new ScheduleAssign();
                    TAS = 1;
                }
                LoadModule(UCTimeTable, modTSchduleAssign);
            }
            if (e.Item == NBarItemExtraAssign)
            {
                if (ELA == 0)
                {
                    modTLectureAssign = new ExtraLectureAssign();
                    ELA = 1;
                }
                LoadModule(UCTimeTable, modTLectureAssign);
            }
            
            if (e.Item == NBarItemTFS)
            {
                if (TFS == 0)
                {
                    modTFloorSheet = new FloorSheet();
                    TFS = 1;
                }
                LoadModule(UCTimeTable, modTFloorSheet);
            }
            if (e.Item == NBarItemTB)
            {
                if (TB == 0)
                {
                    modTBellSheet = new BellSheet();
                    TB = 1;
                }
                LoadModule(UCTimeTable, modTBellSheet);
            }
        }
        #endregion

        #region Transport
        void LoadTransport()
        {
            FillGridTransport();
        }
        private void officeNavNotice_ItemClick(object sender, DevExpress.XtraBars.Navigation.NavigationBarItemEventArgs e)
        {
            if (e.Item == navigationBarItemNotice)
            {
                if (flagNotice == 0)
                {
                    EnoticeBoard = new EnoticeBoard();
                    flagNotice = 1;
                }
                LoadModule(UCNoticeboard, EnoticeBoard);
            }
            
            if (e.Item == navigationBarItemMarketing)
            {
                if (flagMarketing == 0)
                {
                    MarketingSMS = new MarketingSMS();
                    flagMarketing = 1;
                }
                LoadModule(UCNoticeboard, MarketingSMS);
            }
            
        }
        private void btnTAdd_Click(object sender, EventArgs e)
        {
            MySqlConnection con = new MySqlConnection(Login.constring);
            con.Open();
            try
            {
                MySqlCommand cmd = new MySqlCommand("INSERT into transport(route_name,number_of_vehicle,description,route_fare,sync) VALUES('" + txtTName.Text + "','" + txtTVehicle.Text + "','" + txtTDes.Text + "','" + txtTFare.Text + "','0');", con);
                cmd.ExecuteNonQuery();
            }
            catch (MySqlException ex)
            {
                SystemSounds.Hand.Play();
                MessageBox.Show(ex.Message, "Error");
                return;
            }
            con.Close();
            FillGridTransport();
            emptyT();
        }

        //Disciple managment
        int DMP = 0, DSR = 0, DAR = 0, flagActivtyCat, flagactivitystudent;

        int flagNotice = 0, flagMarketing = 0;
        EnoticeBoard EnoticeBoard;
        MarketingSMS MarketingSMS;

        ManagePoints objmanagepoints;
        ActivityCategory objactivitycategory;
        ActivityStudent objactivitystudent;
        IndividualTeacherPoints objindividualpoints;
        private void officeDisciplinenavbar_ItemClick(object sender, DevExpress.XtraBars.Navigation.NavigationBarItemEventArgs e)
        {
            if (e.Item == navigationManagePoints)
            {
                if (DMP == 0)
                {
                    objmanagepoints = new ManagePoints();
                    DMP = 1;
                }
                LoadModule(UCDescipline, objmanagepoints);
            }
            
            if (e.Item == navigationStaffReport)
            {
                if (DSR == 0)
                {
                    objindividualpoints = new IndividualTeacherPoints();
                    DSR = 1;
                }
                LoadModule(UCDescipline, objindividualpoints);
            }

            if (e.Item == navigationActivityCategory)
            {
                if (flagActivtyCat == 0)
                {
                    objactivitycategory = new ActivityCategory();
                    flagActivtyCat = 1;
                }
                LoadModule(UCDescipline, objactivitycategory);
            }
            if (e.Item == navigationStudentActivity)
            {
                if (flagactivitystudent == 0)
                {
                    objactivitystudent = new ActivityStudent();
                    flagactivitystudent = 1;
                }
                LoadModule(UCDescipline, objactivitystudent);
            }



        }

        private void navigationPageMAbout_Paint(object sender, PaintEventArgs e)
        {

        }

        private void emptyT()
        {
            txtTName.Text = "";
            txtTVehicle.Text = "";
            txtTFare.Text = "";
            txtTDes.Text = "";
        }



        private void FillGridTransport()
        {
            MySqlConnection con = new MySqlConnection(Login.constring);
            con.Open();
            MySqlCommand cmdP = new MySqlCommand("SELECT transport_id as ID,route_name as RouteName,number_of_vehicle as NumberOfVehicle,description as Description,route_fare as Fare FROM transport", con);
            MySqlDataAdapter adp = new MySqlDataAdapter(cmdP);
            DataTable table = new DataTable();
            adp.Fill(table);
            gridTransport.DataSource = table;
            gridView3.BestFitColumns();
            var col = gridView3.Columns["ID"];
            col.OptionsColumn.ReadOnly = true;
            con.Close();
        }
        private void gridView3_RowUpdated(object sender, DevExpress.XtraGrid.Views.Base.RowObjectEventArgs e)
        {
            DataRow row = gridView3.GetDataRow(gridView3.FocusedRowHandle);
            MySqlConnection con = new MySqlConnection(Login.constring);
            con.Open();
            MySqlCommand cmd = new MySqlCommand("UPDATE transport set route_name='" + row[1] + "',number_of_vehicle='" + row[2] + "',description='" + row[3] + "',route_fare='" + row[4] + "',sync='0' WHERE transport_id='" + row[0] + "';", con);
            cmd.ExecuteNonQuery();
            con.Close();
            FillGridTransport();
        }
        private void btnTDelete_Click(object sender, EventArgs e)
        {
            try
            {
                if (MessageBox.Show("Are you sure to delete?", "Confirm Delete!!", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    DataRow row = gridView3.GetDataRow(gridView3.FocusedRowHandle);
                    MySqlConnection con = new MySqlConnection(Login.constring);
                    con.Open();
                    MySqlCommand cmdM = new MySqlCommand("DELETE from transport WHERE transport_id='" + row[0] + "';", con);
                    cmdM.ExecuteNonQuery();
                    con.Close();
                    FillGridTransport();
                }
            }
            catch (Exception ex)
            {
                SystemSounds.Hand.Play();
                MessageBox.Show(ex.Message, "Info");
            }
        }
        #endregion

        #region Hostel
        void LoadHostel()
        {
            FillGridHostel();
        }
        private void btnHAdd_Click(object sender, EventArgs e)
        {
            MySqlConnection con = new MySqlConnection(Login.constring);
            con.Open();
            try
            {
                MySqlCommand cmd = new MySqlCommand("INSERT into dormitory(name,number_of_room,description,sync) VALUES('" + txtHName.Text + "','" + txtHRoom.Text + "','" + txtHdes.Text + "','0');", con);
                cmd.ExecuteNonQuery();
            }
            catch (MySqlException ex)
            {
                SystemSounds.Hand.Play();
                MessageBox.Show(ex.Message, "Error");
                return;
            }
            con.Close();
            FillGridHostel();
            emptyH();
        }
        private void emptyH()
        {
            txtHName.Text = "";
            txtHRoom.Text = "";
            txtHdes.Text = "";
        }
        private void FillGridHostel()
        {
            MySqlConnection con = new MySqlConnection(Login.constring);
            con.Open();
            MySqlCommand cmdP = new MySqlCommand("SELECT dormitory_id as ID,name as Name,number_of_room as NumberOfRoom,description as Description FROM dormitory", con);
            MySqlDataAdapter adp = new MySqlDataAdapter(cmdP);
            DataTable table = new DataTable();
            adp.Fill(table);
            gridHostel.DataSource = table;
            gridView4.BestFitColumns();
            var col = gridView4.Columns["ID"];
            col.OptionsColumn.ReadOnly = true;
            con.Close();
        }


        private void gridView4_RowUpdated(object sender, DevExpress.XtraGrid.Views.Base.RowObjectEventArgs e)
        {
            DataRow row = gridView4.GetDataRow(gridView4.FocusedRowHandle);
            MySqlConnection con = new MySqlConnection(Login.constring);
            con.Open();
            MySqlCommand cmd = new MySqlCommand("UPDATE dormitory set name='" + row[1] + "',number_of_room='" + row[2] + "',description='" + row[3] + "',sync='0' WHERE dormitory_id='" + row[0] + "';", con);
            cmd.ExecuteNonQuery();
            con.Close();
            FillGridHostel();
        }

        private void btnHDelete_Click(object sender, EventArgs e)
        {
            try
            {
                if (MessageBox.Show("Are you sure to delete?", "Confirm Delete!!", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    DataRow row = gridView4.GetDataRow(gridView4.FocusedRowHandle);
                    MySqlConnection con = new MySqlConnection(Login.constring);
                    con.Open();
                    MySqlCommand cmdM = new MySqlCommand("DELETE from dormitory WHERE dormitory_id='" + row[0] + "';", con);
                    cmdM.ExecuteNonQuery();
                    con.Close();
                    FillGridHostel();
                }
            }
            catch (Exception ex)
            {
                SystemSounds.Hand.Play();
                MessageBox.Show(ex.Message, "Info");
            }
        }
        #endregion

        #region Admin Setting
        private void officeNavigationBarAdmin_ItemClick(object sender, DevExpress.XtraBars.Navigation.NavigationBarItemEventArgs e)
        {
            if (e.Item == NBarAItemGerenalSetting)
            {
                LoadModule(UCAdmin, modAGerenalSetting);
            }
            else if (e.Item == NBarAItemSMSTemplate)
            {
                LoadModule(UCAdmin, modASmsSetting);
            }
            else if (e.Item == NBarAItemSMSService)
            {
                LoadModule(UCAdmin, modAsmsService);
            }
            else if (e.Item == NBarAItemSMSServer)
            {
                LoadModule(UCAdmin, modAsmsServer);
            }
            else if (e.Item == NBarAItemFeeSetting)
            {
                LoadModule(UCAdmin, modAFeeSetting);
            }
            else if (e.Item == NBarAItemGradesSeting)
            {
                LoadModule(UCAdmin, modAExamGrades);
            }
            else if (e.Item == NBarAItemManageSession)
            {
                LoadModule(UCAdmin, modASession);
            }
            else if (e.Item == NBarAItemBellTune)
            {
                LoadModule(UCAdmin, modABellSetting);
            }
            else if (e.Item == NBarAItemHolidays)
            {
            }

        }
        #endregion


        Sync sync = new Sync();
        #region Switch User and Sync
        private void Sync_Worker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {

        }

        private void timer_Tick(object sender, EventArgs e)
        {
            if (!Sync_Worker.IsBusy)
                Sync_Worker.RunWorkerAsync();
        }
        private void btnSync_Click(object sender, EventArgs e)
        {
            if (!Sync_Worker.IsBusy)
                Sync_Worker.RunWorkerAsync();

        }
        private void Sync_Worker_DoWork(object sender, DoWorkEventArgs e)
        {
            MySqlConnection con = new MySqlConnection(Login.constring);
            var sync_flag = fun.GetSettings("sync_flag");
            if (fun.CheckForInternetConnection() && sync_flag == "1")
            {
                var school_code = fun.GetSettings("school_code");
                var api_key = fun.GetSettings("api_key");
                var api_url = fun.GetSettings("api_url");
                sync.sync(school_code, api_key, api_url);
                con.Open();
                MySqlCommand cmd = new MySqlCommand("UPDATE settings set description='" + DateTime.Now.ToString("yyyy-MM-dd") + "',sync='0' WHERE type='last_sync';", con);
                cmd.ExecuteNonQuery();
                con.Close();
            }
        }
        private void BtnBBrowse_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog fbd = new FolderBrowserDialog();
            if (fbd.ShowDialog() == DialogResult.OK)
            {
                txtBackup.Text = fbd.SelectedPath;
                BtnBackup.Enabled = true;
            }
        }

        private void BtnBackup_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtBackup.Text == string.Empty)
                {
                    MessageBox.Show("Please Enter the backup file location.");
                }
                else
                {
                    // string constring = "server=localhost;user=root;pwd=qwerty;database=test;";
                    // string file = "C:\\backup.sql";
                    using (MySqlConnection conn = new MySqlConnection(Login.constring))
                    {
                        using (MySqlCommand cmd = new MySqlCommand())
                        {
                            using (MySqlBackup mb = new MySqlBackup(cmd))
                            {
                                cmd.Connection = conn;
                                conn.Open();
                                mb.ExportToFile(txtBackup.Text.Trim() + "\\backup -" + DateTime.Now.ToString("yyyy-MM-dd") + ".sql");
                                conn.Close();
                            }
                        }
                        MessageBox.Show("Database beckup done successefully");
                        BtnBackup.Enabled = false;
                    }
                }
            }
            catch (Exception ex)
            {
                SystemSounds.Hand.Play();
                MessageBox.Show(ex.Message, "Info");
            }
        }

        private void BtnRBrowse_Click(object sender, EventArgs e)
        {
            OpenFileDialog dlg = new OpenFileDialog();
            dlg.Filter = "MySql SERVER database backup files|*.sql";
            dlg.Title = "Datebase Restore";
            if (dlg.ShowDialog() == DialogResult.OK)
            {
                txtRestore.Text = dlg.FileName;
                BtnRestore.Enabled = true;
            }
        }

        private void BtnRestore_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtRestore.Text == string.Empty)
                {
                    MessageBox.Show("Please Select the backup file.");
                }
                else
                {
                    using (MySqlConnection conn = new MySqlConnection(Login.constring))
                    {
                        using (MySqlCommand cmd = new MySqlCommand())
                        {
                            using (MySqlBackup mb = new MySqlBackup(cmd))
                            {
                                cmd.Connection = conn;
                                conn.Open();
                                mb.ImportFromFile(txtRestore.Text.Trim());
                                conn.Close();
                            }
                        }
                        MessageBox.Show("Database restore Successefully");
                        BtnRestore.Enabled = false;
                    }
                }
            }
            catch (Exception ex)
            {
                SystemSounds.Hand.Play();
                MessageBox.Show(ex.Message, "Info");
            }
        }
        #endregion

        #region Users
        ObservableCollection<AllValues> allValue;

        private void btnCLavel_Click(object sender, EventArgs e)
        {
            Permissions_Desk per = new Permissions_Desk();
            per.ShowDialog();
            allValue = new ObservableCollection<AllValues>();
            txtUStatus.Properties.Items.Clear();
            allValue = fun.GetAllPermissionType("Desktop");
            foreach (var val in allValue)
                txtUStatus.Properties.Items.Add(val.Name);


        }
        private void simpleButton1_Click(object sender, EventArgs e)
        {
            Permissions_TeacherApp per = new Permissions_TeacherApp();
            per.ShowDialog();
        }
        void LoadUser()
        {
            FillGridEmployees();
            allValue = new ObservableCollection<AllValues>();
            txtUStatus.Properties.Items.Clear();
            allValue = fun.GetAllPermissionType("Desktop");
            foreach (var val in allValue)
                txtUStatus.Properties.Items.Add(val.Name);

        }

        private void BtnAddEmployees_Click(object sender, EventArgs e)
        {
            var id = fun.GetPermissionID(txtUStatus.Text.Trim(), "Desktop");

            MySqlConnection con = new MySqlConnection(Login.constring);
            con.Open();
            MySqlCommand cmd1 = new MySqlCommand("INSERT into admin(name,email,password,level,sync) VALUES('" + txtUName.Text + "','" + txtUEmail.Text + "','" + txtUPass.Text + "','" + id + "','0');", con);
            cmd1.ExecuteNonQuery();
            con.Close();
            FillGridEmployees();
            emptyManagement();
        }
        private void emptyManagement()
        {
            txtUName.Text = "";
            txtUPass.Text = "";
            txtUEmail.Text = "";
        }
        void FillGridEmployees()
        {
            MySqlConnection con = new MySqlConnection(Login.constring);
            con.Open();
            MySqlCommand cmdU = new MySqlCommand("SELECT  admin_id as ID, name as User,email as Email,password as Password,tbl_permission.title as Level from admin join tbl_permission on tbl_permission.permission_id = admin.level where name not like 'ZeeShan'; ", con);
            MySqlDataAdapter adp = new MySqlDataAdapter(cmdU);
            DataTable table = new DataTable();
            adp.Fill(table);
            gridEmployees.DataSource = table;
            gridView5.BestFitColumns();
            var col = gridView5.Columns["ID"];
            col.OptionsColumn.ReadOnly = true;
            con.Close();


            RepositoryItemComboBox riComboReceiver = new RepositoryItemComboBox();
            allValue = new ObservableCollection<AllValues>();
            allValue = fun.GetAllPermissionType("Desktop");
            foreach (var val in allValue)
                riComboReceiver.Items.Add(val.Name);
            gridView5.Columns["Level"].ColumnEdit = riComboReceiver;



        }
        private void gridView5_RowUpdated(object sender, DevExpress.XtraGrid.Views.Base.RowObjectEventArgs e)
        {
            DataRow row = gridView5.GetDataRow(gridView5.FocusedRowHandle);
            var id = fun.GetPermissionID(row[4].ToString().Trim(), "Desktop");

            MySqlConnection con = new MySqlConnection(Login.constring);
            con.Open();
            MySqlCommand cmd = new MySqlCommand("UPDATE admin set name='" + row[1] + "',email='" + row[2] + "',password='" + row[3] + "' ,level='" + id + "',sync='0' WHERE admin_id='" + row[0] + "';", con);
            cmd.ExecuteNonQuery();
            con.Close();
            FillGridEmployees();
        }

        private void BtnDelEmployee_Click(object sender, EventArgs e)
        {
            try
            {
                if (MessageBox.Show("Are you sure to delete?", "Confirm Delete!!", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    DataRow row = gridView5.GetDataRow(gridView5.FocusedRowHandle);
                    MySqlConnection con = new MySqlConnection(Login.constring);
                    con.Open();
                    MySqlCommand cmdM = new MySqlCommand("DELETE from admin WHERE admin_id='" + row[0] + "';", con);
                    cmdM.ExecuteNonQuery();
                    sync.SyncDelete("admin", "admin_id", row[0].ToString());

                    con.Close();
                    FillGridEmployees();
                }
            }
            catch (Exception ex)
            {
                SystemSounds.Hand.Play();
                MessageBox.Show(ex.Message, "Info");
            }
        }

        #endregion

        #region About
        string query;
        // int idd = 0;
        private void BtnACTIVITE_Click(object sender, EventArgs e)
        {

            if (txtActivite.Text.Trim() == "")
            {
                MessageBox.Show("Enter correct Code", "Info");
                return;
            }

            systemvalidation objvalidation = new systemvalidation();
            string activation_key = txtActivite.Text.Trim();
            string school_code = fun.GetSettings("school_code");
            string response = objvalidation.check_registeration(activation_key, school_code);
            string key_info = objvalidation.extract_keyinfo_softpitch(activation_key);

            if (response == "OK")
            {
                MySqlConnection con = new MySqlConnection(Login.constring);
                con.Open();
                query = "UPDATE settings set description = '" + txtActivite.Text.Trim() + "',sync='0' WHERE type = 'activition_key'; ";
                MySqlCommand cmd = new MySqlCommand(query, con);
                cmd.ExecuteNonQuery();
                con.Close();
                
                MessageBox.Show("Congratulation Your key Upgraded! \n "+ key_info);
            }
            else
            {
                MessageBox.Show(response);
            }
            activate();
        }

       

        #endregion

        #region Main
        private void Main_Enter(object sender, EventArgs e)
        {
        }
        private void navigationPane1_SelectedPageChanged(object sender, DevExpress.XtraBars.Navigation.SelectedPageChangedEventArgs e)
        {

            if (navigationPane1.SelectedPage == navigationPageMSettings)
            {
                if (Setting == 0)
                {
                    fun.loaderform(() =>
                    {

                        modAGerenalSetting = new GerenalSetting();
                        modASmsSetting = new SMSTemplate();
                        modAsmsService = new SMSService();
                        modAsmsServer = new SMSServer();
                        modAExamGrades = new ExamGrades();
                        modAFeeSetting = new FeeSetting();
                        modASession = new ManageSession();
                        modABellSetting = new BellSetting();
                        Setting = 1;
                    });

                }
            }
            if (navigationPane1.SelectedPage == navigationPageMLibrary)
            {
                if (Library == 0)
                {
                    fun.loaderform(() =>
                    {

                        modLAddBook = new AddBooks();
                        modLIssueBook = new IssueBooks();
                        Library = 1;
                    });

                }
            }

            if (navigationPane1.SelectedPage == navigationPageMSwitchUser)
            {
                this.Visible = false;
                this.Close();
                Login a = new Login();
                a.ShowDialog();
            }
            if (navigationPane1.SelectedPage == navigationPageMDashboard)
            {
                dashboardViewer1.ReloadData();
            }
            if (navigationPane1.SelectedPage == navigationPageMPDashboard)
            {
                if (PPD == 0)
                    modPDash = new PDashboard();
                LoadModule(UCPDashboard, modPDash);
            }
            if (navigationPane1.SelectedPage == navigationPageMSync)
            {
                //if (fun.CheckForInternetConnection())
                //{
                //    //MessageBox.Show("Current feature is not available.\nPlease contact with TNS Bay Administration", "Info");
                //    var school_code = fun.GetSettings("school_code");
                //    var api_key = fun.GetSettings("api_key");
                //    var api_url = fun.GetSettings("api_url");
                //    sync.sync(school_code, api_key, api_url);
                //}
                //else
                //    MessageBox.Show("Please Check Internet Connection.", "Info");
            }
            //if (navigationPane1.SelectedPage == navigationPageMHostel ||
            //    navigationPane1.SelectedPage == navigationPageMLibrary ||
            //    navigationPane1.SelectedPage == navigationPageMNoticeboard ||
            //    navigationPane1.SelectedPage == navigationPageMTransport)
            //    MessageBox.Show("Current feature is not available.\nPlease contact with TNS Bay Administration", "Info");

        }

        private void txtSession_SelectedIndexChanged(object sender, EventArgs e)
        {
            SelectedSession = txtSession.SelectedItem.ToString();
            if (Students == 1)
                modStudent.FillGridStudent();
            if (Attendance == 1)
                modAFeeSetting.FillGridFeeSetting();
            if (Exam == 1)
            {
                modExamList.FillGridExam();
                //modClassResult.result();
            }
            if (Class == 1)
            {
                modClass.FillGridClass();
                modSections.FillGridSections();
                modSubjects.FillGridSubject();
                //  modClassRoutine.FillGridClassRoutine();
            }
            if (SelectedSession.Trim() != fun.GetDefaultSessionName().Trim())
            {
                String con_str = Login.constring;
                con_str = con_str.Replace("tnsbay_school", "tnsbay_school_" + SelectedSession.Trim().Replace('-', '_'));
                Login.constring = con_str;
            }
            //else
                //Login.constring = Login.temp;
        }
        #endregion


       
        #region Principal
        private void officeNavigationBar2_ItemClick(object sender, DevExpress.XtraBars.Navigation.NavigationBarItemEventArgs e)
        {
            if (e.Item == NBarItemPScheduling)
            {
                if (PS == 0)
                {
                    modSchedulig = new Scheduling();
                    PS = 1;
                }
                LoadModule(UCPrincipal, modSchedulig);
            }
            if (e.Item == NBarItemPVisitor)
            {
                if (PVL == 0)
                {
                    modVisitor = new Visitor();
                    PVL = 1;
                }
                LoadModule(UCPrincipal, modVisitor);
            }

            if (e.Item == NBarItemPAttendanceSummery)
            {
                if (PAS == 0)
                {
                    modAttSummery = new AttendanceSummery();
                    PAS = 1;
                }
                LoadModule(UCPrincipal, modAttSummery);
            }


        }
        #endregion
    }
}