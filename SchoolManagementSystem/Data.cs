using System.Collections.ObjectModel;
using System.Data;
using System.Drawing;
using System.Data;
using System.Drawing;
using System.Collections.Generic;

namespace SchoolManagementSystem
{
    public enum MonthOfYear
    {
        January = 1,
        February = 2,
        March = 3,
        April = 4,
        May = 5,
        June = 6,
        July = 7,
        August = 8,
        September = 9,
        October = 10,
        November = 11,
        December = 12,
    }
    public class Attend
    {
        public string key { get; set; }
        public string val { get; set; }
        public string age { get; set; }
    }

    public class Meetings
    {
        public string key { get; set; }
        public string val { get; set; }
        public string type { get; set; }
        public int ID { get; set; }
        public int SectionID { get; set; }
        public string status { get; set; }
    }
    public class appoint
    {
        public int appoint_id { get; set; }
        public string key { get; set; }
        public string val { get; set; }
        public string type { get; set; }
        public string des { get; set; }
        public string dateS { get; set; }
        public string dateE { get; set; }
        public string status { get; set; }
    }

    public class Holiday
    {
        public string Subject { get; set; }
        public string Start { get; set; }
        public string End { get; set; }
        public string Location { get; set; }
        public string AllDay { get; set; }
    }
    public class Notification
    {
        public string header { get; set; }
        public string body { get; set; }
    }


    public class CalCreditHours
    {
        public string index { get; set; }
        public int Classid { get; set; }
        public int Sectionid { get; set; }
        public string CalWorkLoad { get; set; }
    }
    public class CreditHours
    {
        public int Classid { get; set; }
        public int Sectionid { get; set; }
        public string SubjectCode { get; set; }
        public int WorkLoad { get; set; }
    }
    public class SubjectCode
    {
        public string Code { get; set; }
        public int WorkLoad { get; set; }
        public int index { get; set; }
    }
    public class TeacherLLimit
    {
        public int ID { get; set; }
        public int Llimit { get; set; }
        public string SCode { get; set; }
    }
    public class TimeTable
    {
        public string Day { get; set; }
        public string Slat { get; set; }
        public int SlatVal { get; set; }
        public int Sectionid { get; set; }
        public string SectionName { get; set; }
        public int Classid { get; set; }
        public string ClassName { get; set; }
        public string SubjectCode { get; set; }
        public int Teacherid { get; set; }
        public string TeacherName { get; set; }
        public string DayName { get; set; }
    }
    public class Sslats
    {
        public string index { get; set; }
        public int Cid { get; set; }
        public int Sid { get; set; }
        public string CName { get; set; }
        public string SName { get; set; }
        public string SDay { get; set; }
        public string Slats { get; set; }
        public int SlatsVal { get; set; }
        public string Available { get; set; }
        public string Used { get; set; }
        public string SDayName { get; set; }
    }
    public class Tslats
    {
        public string index { get; set; }
        public string SubjectCode { get; set; }
        public int Teacherid { get; set; }
        public string TeacherName { get; set; }
        public string Day { get; set; }
        public string Slats { get; set; }
        public int SlatsVal { get; set; }
        public string Available { get; set; }
        public string Used { get; set; }
        public string TimeStart { get; set; }
        public string TimeEnd { get; set; }

    }

    public class syncArray
    {
        public string Table { get; set; }
        public DataTable Json { get; set; }
    }
    public class ddl_items
    {
        public string student_id { get; set; }
        public string name { get; set; }
    }
    public class ProgressReport
    {
        public int PercentComplete { get; set; }
    }
    public class Setting
    {
        public string type { get; set; }
    }
    public class AllClass
    {
        public string Name { get; set; }
        public string Salary { get; set; }
        public string pf_ded { get; set; }
        public string teacher_id { get; set; }
    }
    public class AllClassSet
    {
        public string Name { get; set; }
        public string Salary { get; set; }
    }
    public class ClassTime
    {
        public string min_time { get; set; }
        public string max_time { get; set; }
    }
    public class AllValues
    {
        public int ID { get; set; }
        public string Name { get; set; }
    }
    public class TeachingStaff
    {
        public string ID { get; set; }
        public string Name { get; set; }
    }
    public class AllSubject
    {
        public string ID { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
    }
    public class AllStudent
    {
        public string Name { get; set; }
        public string Class { get; set; }
    }
    public class StudentInfo
    {
        public string ID { get; set; }
        public string Roll { get; set; }
        public string Name { get; set; }
        public string Father_Name { get; set; }
        public string Class { get; set; }
        public string Section { get; set; }
    }
    public class CheckListItems
    {
        public string Class { get; set; }
        public string Section { get; set; }
        public string Subject { get; set; }
        public bool Check { get; set; }
        public string Subject1 { get; set; }
        public bool Check1 { get; set; }
        public string Teacher { get; set; }
        public string Teacher1 { get; set; }
        public string Date { get; set; }
        public string DateD { get; set; }
        public string DateD1 { get; set; }
        public string Exam { get; set; }
        public string Exam1 { get; set; }


    }
    public class FloorSheetItems
    {
        public string Teacher { get; set; }
        public string Section { get; set; }
        public int Count { get; set; }
        public int LTime { get; set; }
        public string Section1 { get; set; }
        public int Count1 { get; set; }
        public int LTime1 { get; set; }
    }

    public class Move
    {
        public string Total { get; set; }
        public string Paid { get; set; }
        public string Due { get; set; }
        public string Std { get; set; }
        public string No { get; set; }
        public string Date { get; set; }
    }
    public class SendMark
    {
        public string Stdid { get; set; }
        public string StdNo { get; set; }
        public string StdName { get; set; }
        public string StdPhone { get; set; }
        public string Parent { get; set; }
        public string ParentPhone { get; set; }
        public string ExamID { get; set; }
        public string ExamDate { get; set; }
        public string Marks { get; set; }
        public double Obtained { get; set; }
        public int Total { get; set; }
        public string Grade { get; set; }
        public string Position { get; set; }
        public string classname { get; set; }
        public string sectionname { get; set; }
    }
    public class Resultinfo
    {
        public string School { get; set; }
        public Image logo { get; set; }
        public string Stdid { get; set; }
        public string StdNo { get; set; }
        public string StdName { get; set; }
        public string StdFName { get; set; }
        public string StdPhone { get; set; }
        public string StdAddress { get; set; }
        public string StdClass { get; set; }
        public string StdSection { get; set; }
    }
    public class Result
    {
        public string StdNo { get; set; }
        public string Subject { get; set; }
        public string Obtained { get; set; }
        public int Total { get; set; }
        public string Percentage { get; set; }
        public string Position { get; set; }
        public string Min { get; set; }
        public string Max { get; set; }
    }
    public class ManageAttendance
    {
        public string Att_ID { get; set; }
        public string ID { get; set; }
        public string Student { get; set; }
        public string Status { get; set; }
        public object ROLL { get; set; }
    }

    public class FeeCard
    {
        public string School { get; set; }
        public Image logo { get; set; }
        public string ID { get; set; }
        public string StdID { get; set; }
        public string StdName { get; set; }
        public string StdClass { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public int Previous { get; set; }
        public int Current { get; set; }
        public int Concession { get; set; }
        public int Amount { get; set; }
        public int Paid { get; set; }
        public int Due { get; set; }
        public string Status { get; set; }
        public string Date { get; set; }
    }
    public class PassOutCard
    {
        public string School { get; set; }
        public Image logo { get; set; }
        public string StdID { get; set; }
        public string StdName { get; set; }
        public string StdClass { get; set; }
        public string DOB { get; set; }
        public string FName { get; set; }
        public string Session { get; set; }
        public string Date { get; set; }
        public string student_phone { get; set; }

        public string addmission_date { get; set; }
        public string passout_date { get; set; }
        public ObservableCollection<passout_fee_history> fee_history { get; set; }
    }
    public class passout_fee_history
    {
        public string Month { get; set; }
        public string Previous { get; set; }
        public string Current { get; set; }
        public string Concession { get; set; }
        public string Total { get; set; }
        public string Paid { get; set; }
        public string Remaining { get; set; }
    }
    public class ExperienceCard
    {
        public string School { get; set; }
        public Image logo { get; set; }
        public string ID { get; set; }
        public string Name { get; set; }
        public string CNIC { get; set; }
        public string DOB { get; set; }
        public string FName { get; set; }
        public string Joining { get; set; }
        public string ending { get; set; }
        public string subjects { get; set; }
    }

    public class StudentCard
    {
        public string School { get; set; }
        public string School_Address { get; set; }
        public Image logo { get; set; }
        public Image student_image { get; set; }
        public string ID { get; set; }
        public string Name { get; set; }
        public string Roll { get; set; }
        public string Class { get; set; }
        public string Section { get; set; }
    }

    public class PreviousFee
    {
        public string StdID { get; set; }
        public string Amount { get; set; }
        public string Method { get; set; }
        public string Date { get; set; }
        public string Due { get; set; }
    }
    //public class StudentAtt
    //{
    //    public string StdID { get; set; }
    //    public string Name { get; set; }
    //    public string Status { get; set; }
    //    public string Date { get; set; }
    //}
    public class Salary
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Staff_type { get; set; }
        public string Desgination { get; set; }
        public string Subject { get; set; }
        public string AC { get; set; }
        public int B_salary { get; set; }
        public int Increment { get; set; }
        public int G_salary { get; set; }
        public int WorkingDays { get; set; }
        public int Arrear { get; set; }
        public int Bonus { get; set; }
        public int Fsc { get; set; }
        public int RateF { get; set; }
        public int TotalF { get; set; }
        public int Ucp { get; set; }
        public int RateU { get; set; }
        public int TotalU { get; set; }
        public int Bsc { get; set; }
        public int RateB { get; set; }
        public int TotalB { get; set; }
        public int NBundle { get; set; }
        public int BundleRate { get; set; }
        public int Paper { get; set; }
        public int DayAward { get; set; }
        public int Visiting { get; set; }
        public int Invig { get; set; }
        public int ExtraLect { get; set; }
        public int FixSalary { get; set; }
        public int TotalAddition { get; set; }
        public int Absent { get; set; }
        public int AbsentDed { get; set; }
        public int LateDed { get; set; }
        public int PFDed { get; set; }
        public int MealDed { get; set; }
        public int Tex6 { get; set; }
        public int TotalDed { get; set; }
        public int GTotal { get; set; }
        public int Advance { get; set; }
        public int Payable { get; set; }
        public int Paid { get; set; }
        public int Due { get; set; }
        public string Status { get; set; }
        public string Date { get; set; }
    }
    //public partial class Employees
    //{
    //    public string ID { get; set; }
    //    public string NAME { get; set; }
    //    public string PASSWORD { get; set; }
    //    public string MAIL { get; set; }
    //    public string STATUS { get; set; }
    //}
    public partial class tblImport
    {
        public string Name { get; set; }
        public string DOB { get; set; }
        public string Sex { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Roll { get; set; }
        public string Card { get; set; }
        public string PName { get; set; }
        public string PPhone { get; set; }
        public string PEmail { get; set; }
        public string Profession { get; set; }
        public string stdID { get; set; }
        public string School { get; set; }
    }
    public partial class fee_receipt_data
    {
        public Image logo { get; set; }
        public string system_title { get; set; }
        public string address { get; set; }
        public string phone { get; set; }
        public string account_number { get; set; }
        public string account_name { get; set; }
        public string copy_type { get; set; }
        public string challan_no { get; set; }
        public string ishue_date { get; set; }
        public string due_date { get; set; }
        public string registration { get; set; }
        public string student_id { get; set; }
        public string roll_no { get; set; }
        public string Name { get; set; }
        public string Father { get; set; }
        public string Class { get; set; }
        public string Section { get; set; }
        public string Month { get; set; }
        public string Amount_title { get; set; }
        public string Amount { get; set; }
        public string Amount_In_Word { get; set; }
        public string current_fee { get; set; }
        public string privious { get; set; }
        public string receipt_note { get; set; }
        public string total_concession { get; set; }
        public string total { get; set; }
        public string paid { get; set; }
        public string paid_date { get; set; }
        public string remaining { get; set; }
        public string transaction { get; set; }
        public string late_fee { get; set; }
        public string after_late_fee { get; set; }
        public ObservableCollection<other_fee_details> other_fee_details { get; set; }
        public ObservableCollection<fee_history> fee_history { get; set; }

        //NNS Columns
        public string addmission_fee { get; set; }
        public string annual_charges { get; set; }
        public string monthly_fee { get; set; }
        public string monthly_actual { get; set; }

        public string exam_fee { get; set; }
        public string security_deposit { get; set; }
        public string student_card_charges { get; set; }
        public string other_charges { get; set; }

    }
    public class other_fee_details
    {
        public string afee_title { get; set; }
        public string bfee_amount { get; set; }
    }
    public class fee_history
    {
        public string amonth { get; set; }
        public string bpaiddate { get; set; }
        public string camount { get; set; }
        public string dpaid { get; set; }
        public string edue { get; set; }
    }

    public class settings_ddl
    {
        public string description { get; set; }
        public string value { get; set; }
        public int image_index { get; set; }
    }

}
