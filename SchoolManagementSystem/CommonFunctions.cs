using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Data.OleDb;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Management;
using System.Net;
using System.Net.NetworkInformation;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.LookAndFeel;
using DevExpress.Snap;
using DevExpress.Snap.Core.API;
using DevExpress.Utils;
using DevExpress.XtraBars;
using DevExpress.XtraCharts;
using DevExpress.XtraEditors;
using DevExpress.XtraGrid;
using DevExpress.XtraGrid.Columns;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraPrinting;
using DevExpress.XtraReports.UI;
using DevExpress.XtraSplashScreen;
using MySql.Data.MySqlClient;
using Newtonsoft.Json.Linq;
using RestSharp;
using SchoolManagementSystem.Exam;
using SchoolManagementSystem.Fees;

namespace SchoolManagementSystem
{
    public class CommonFunctions
    {
        public void loaderform(Action Method)
        {
            try
            {
                SplashScreenManager.ShowForm(typeof(WaitForm1));
                Method.Invoke();
                SplashScreenManager.CloseForm();
            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show(ex.Message, Method.Method.Name, MessageBoxButtons.OK, MessageBoxIcon.Error);
                if (!SplashScreenManager.FormInPendingState)
                {
                    SplashScreenManager.CloseForm();
                }
            }
        }

        private BackgroundWorker loading_bg_data = new BackgroundWorker();
        public void gridcoulmns_settings(GridView gw, string[] editable, string[] hide)
        {
            foreach (GridColumn col in gw.Columns)
            {
                string c = col.FieldName;
                gw.Columns[c].OptionsColumn.ReadOnly = true;
                if (editable.Contains(c))
                {
                    gw.Columns[c].OptionsColumn.ReadOnly = false;
                }

                if (hide.Contains(c))
                {
                    gw.Columns[c].Visible = false;
                }
            }
        }
        public DataTable ladger_head()
        {
            string query = "SELECT h.`head_id`,h.`title`,c.`title` AS ladger_category " +
                " FROM `ac_ledger_head` AS h JOIN `ac_ledger_category` AS c ON c.`id` = h.`category_id` " +
                " WHERE h.status = 1;";
            return FetchDataTable(query);
        }

        private List<string> Attendace_status_list = new List<string>();
        public DataTable Attendance_status()
        {
            string query = "select title,status from attendance_status";
            return FetchDataTable(query);
        }
        public IEnumerable<Control> GetAllControls(Control control, Type type = null, string containsName = null)
        {
            var controls = control.Controls.Cast<Control>();
            //check the all value, if true then get all the controls
            //otherwise get the controls of the specified type
            if (type == null)
            {
                return controls.SelectMany(ctrl => GetAllControls(ctrl, type, containsName)).Concat(controls);
            }
            else if (containsName != null)
            {
                return controls.SelectMany(ctrl => GetAllControls(ctrl, type, containsName)).Concat(controls).Where(c => c.GetType() == type && c.Name.Contains(containsName));
            }
            else
            {
                return controls.SelectMany(ctrl => GetAllControls(ctrl, type, containsName)).Concat(controls).Where(c => c.GetType() == type);
            }
        }
        public void ShowGridPreview(GridControl grid)
        {
            // Check whether the GridControl can be previewed.
            if (!grid.IsPrintingAvailable)
            {
                System.Windows.Forms.MessageBox.Show("The 'DevExpress.XtraPrinting' library is not found", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            // Open the Preview window.
            grid.ShowPrintPreview();
        }
        // This constant is used to determine the keysize of the encryption algorithm in bits.
        // We divide this by 8 within the code below to get the equivalent number of bytes.
        private const int Keysize = 256;

        // This constant determines the number of iterations for the password bytes generation function.
        private const int DerivationIterations = 1000;

        public string Encrypt(string plainText, string passPhrase)
        {

            // Salt and IV is randomly generated each time, but is preprended to encrypted cipher text
            // so that the same Salt and IV values can be used when decrypting.  
            var saltStringBytes = Generate256BitsOfRandomEntropy();
            var ivStringBytes = Generate256BitsOfRandomEntropy();
            var plainTextBytes = Encoding.UTF8.GetBytes(plainText);
            using (var password = new Rfc2898DeriveBytes(passPhrase, saltStringBytes, DerivationIterations))
            {
                var keyBytes = password.GetBytes(Keysize / 8);
                using (var symmetricKey = new RijndaelManaged())
                {
                    symmetricKey.BlockSize = 256;
                    symmetricKey.Mode = CipherMode.CBC;
                    symmetricKey.Padding = PaddingMode.PKCS7;
                    using (var encryptor = symmetricKey.CreateEncryptor(keyBytes, ivStringBytes))
                    {
                        using (var memoryStream = new MemoryStream())
                        {
                            using (var cryptoStream = new CryptoStream(memoryStream, encryptor, CryptoStreamMode.Write))
                            {
                                cryptoStream.Write(plainTextBytes, 0, plainTextBytes.Length);
                                cryptoStream.FlushFinalBlock();
                                // Create the final bytes as a concatenation of the random salt bytes, the random iv bytes and the cipher bytes.
                                var cipherTextBytes = saltStringBytes;
                                cipherTextBytes = cipherTextBytes.Concat(ivStringBytes).ToArray();
                                cipherTextBytes = cipherTextBytes.Concat(memoryStream.ToArray()).ToArray();
                                memoryStream.Close();
                                cryptoStream.Close();
                                return Convert.ToBase64String(cipherTextBytes);
                            }
                        }
                    }
                }

            }


        }
        public string Decrypt(string cipherText, string passPhrase)
        {
            // Get the complete stream of bytes that represent:
            // [32 bytes of Salt] + [32 bytes of IV] + [n bytes of CipherText]
            var cipherTextBytesWithSaltAndIv = Convert.FromBase64String(cipherText);
            // Get the saltbytes by extracting the first 32 bytes from the supplied cipherText bytes.
            var saltStringBytes = cipherTextBytesWithSaltAndIv.Take(Keysize / 8).ToArray();
            // Get the IV bytes by extracting the next 32 bytes from the supplied cipherText bytes.
            var ivStringBytes = cipherTextBytesWithSaltAndIv.Skip(Keysize / 8).Take(Keysize / 8).ToArray();
            // Get the actual cipher text bytes by removing the first 64 bytes from the cipherText string.
            var cipherTextBytes = cipherTextBytesWithSaltAndIv.Skip((Keysize / 8) * 2).Take(cipherTextBytesWithSaltAndIv.Length - ((Keysize / 8) * 2)).ToArray();

            using (var password = new Rfc2898DeriveBytes(passPhrase, saltStringBytes, DerivationIterations))
            {
                var keyBytes = password.GetBytes(Keysize / 8);
                using (var symmetricKey = new RijndaelManaged())
                {
                    symmetricKey.BlockSize = 256;
                    symmetricKey.Mode = CipherMode.CBC;
                    symmetricKey.Padding = PaddingMode.PKCS7;
                    using (var decryptor = symmetricKey.CreateDecryptor(keyBytes, ivStringBytes))
                    {
                        using (var memoryStream = new MemoryStream(cipherTextBytes))
                        {
                            using (var cryptoStream = new CryptoStream(memoryStream, decryptor, CryptoStreamMode.Read))
                            {
                                var plainTextBytes = new byte[cipherTextBytes.Length];
                                var decryptedByteCount = cryptoStream.Read(plainTextBytes, 0, plainTextBytes.Length);
                                memoryStream.Close();
                                cryptoStream.Close();
                                return Encoding.UTF8.GetString(plainTextBytes, 0, decryptedByteCount);
                            }
                        }
                    }
                }
            }
        }
        private static byte[] Generate256BitsOfRandomEntropy()
        {
            var randomBytes = new byte[32]; // 32 Bytes will give us 256 bits.
            using (var rngCsp = new RNGCryptoServiceProvider())
            {
                // Fill the array with cryptographically secure random bytes.
                rngCsp.GetBytes(randomBytes);
            }
            return randomBytes;
        }

        public bool CheckForInternetConnection()
        {
            try
            {
                using (var client = new WebClient())
                using (var stream = client.OpenRead("http://www.google.com"))
                {
                    return true;
                }
            }
            catch
            {
                return false;
            }
        }
        public string getUUID()
        {
            Process process = new Process();
            ProcessStartInfo startInfo = new ProcessStartInfo();
            startInfo.WindowStyle = System.Diagnostics.ProcessWindowStyle.Hidden;
            startInfo.FileName = "CMD.exe";
            startInfo.Arguments = "/C wmic csproduct get UUID";
            process.StartInfo = startInfo;
            process.StartInfo.UseShellExecute = false;
            process.StartInfo.RedirectStandardOutput = true;
            process.Start();
            process.WaitForExit();
            string output = process.StandardOutput.ReadToEnd();
            return output;
        }
        public string FetchMacId()
        {
            NetworkInterface[] nics = NetworkInterface.GetAllNetworkInterfaces();
            String sMacAddress = string.Empty;
            foreach (NetworkInterface adapter in nics)
            {
                if (sMacAddress == String.Empty)// only return MAC Address from first card  
                {
                    //IPInterfaceProperties properties = adapter.GetIPProperties(); Line is not required
                    sMacAddress = adapter.GetPhysicalAddress().ToString();
                }
            }
            return sMacAddress;
        }
        public string GetProcessorId()
        {
            String strProcessorId = string.Empty;
            SelectQuery query = new SelectQuery("Win32_Processor");
            ManagementObjectSearcher search = new ManagementObjectSearcher(query);
            foreach (ManagementObject info in search.Get())
            {
                strProcessorId = info["processorId"].ToString();
            }
            return strProcessorId;
        }
        public string ImageToBase64(Image image, System.Drawing.Imaging.ImageFormat format)
        {
            using (MemoryStream ms = new MemoryStream())
            {
                // Convert Image to byte[]
                image.Save(ms, format);
                byte[] imageBytes = ms.ToArray();

                // Convert byte[] to Base64 String
                string base64String = Convert.ToBase64String(imageBytes);
                return base64String;
            }
        }
        public Image Base64ToImage(string base64Image)
        {
            if (string.IsNullOrEmpty(base64Image))
            {
                return null;
            }

            using (MemoryStream ms = new MemoryStream(Convert.FromBase64String(base64Image)))
            {
                Image image = Image.FromStream(ms, true);
                return image;
            }

        }
        public int ConvertMinutesToMilliseconds(int minutes)
        {
            double val = TimeSpan.FromMinutes(minutes).TotalMilliseconds;
            return Convert.ToInt32(val);
        }
        public Image ScaleImage(Image image, int maxWidth, int maxHeight)
        {
            var ratioX = (double)maxWidth / image.Width;
            var ratioY = (double)maxHeight / image.Height;
            var ratio = Math.Min(ratioX, ratioY);

            var newWidth = (int)(image.Width * ratio);
            var newHeight = (int)(image.Height * ratio);

            var newImage = new Bitmap(newWidth, newHeight);

            using (var graphics = Graphics.FromImage(newImage))
            {
                graphics.DrawImage(image, 0, 0, newWidth, newHeight);
            }

            return newImage;
        }

        public string NumberToWords(int number)
        {
            if (number == 0)
            {
                return "zero";
            }

            if (number < 0)
            {
                return "minus " + NumberToWords(Math.Abs(number));
            }

            string words = "";

            if ((number / 1000000) > 0)
            {
                words += NumberToWords(number / 1000000) + " Million ";
                number %= 1000000;
            }

            if ((number / 1000) > 0)
            {
                words += NumberToWords(number / 1000) + " Thousand ";
                number %= 1000;
            }

            if ((number / 100) > 0)
            {
                words += NumberToWords(number / 100) + " Hundred ";
                number %= 100;
            }

            if (number > 0)
            {
                if (words != "")
                {
                    words += "and ";
                }

                var unitsMap = new[] { "Zero", "One", "Two", "Three", "Four", "Five", "Six", "Seven", "Eight", "Nine", "Ten", "Eleven", "Twelve", "Thirteen", "Fourteen", "Fifteen", "Sixteen", "Seventeen", "Eighteen", "Nineteen" };
                var tensMap = new[] { "Zero", "Ten", "Twenty", "Thirty", "Forty", "Fifty", "Sixty", "Seventy", "Eighty", "Ninety" };

                if (number < 20)
                {
                    words += unitsMap[number];
                }
                else
                {
                    words += tensMap[number / 10];
                    if ((number % 10) > 0)
                    {
                        words += "-" + unitsMap[number % 10];
                    }
                }
            }

            return words;
        }

        private ObservableCollection<TeachingStaff> teachingStaff = new ObservableCollection<TeachingStaff>();
        private ObservableCollection<AllClass> allClass = new ObservableCollection<AllClass>();
        private ObservableCollection<AllStudent> allStudent = new ObservableCollection<AllStudent>();
        private ObservableCollection<StudentInfo> Studentinfo = new ObservableCollection<StudentInfo>();
        private ObservableCollection<AllValues> allValue;
        private List<DateTime> dateHolidays;

        public List<DateTime> GetAllHolidaysBetween(DateTime inStart, DateTime inEnd)
        {
            dateHolidays = new List<DateTime>();
            MySqlConnection con = new MySqlConnection(Login.constring);
            con.Open();
            var queryHoliday = "SELECT * FROM tbl_holidays ORDER BY holiday_id DESC ";
            MySqlCommand cmdholiday = new MySqlCommand(queryHoliday, con);
            MySqlDataReader readerholiday = cmdholiday.ExecuteReader();
            if (readerholiday.HasRows)
            {
                while (readerholiday.Read())
                {
                    DateTime start = Convert.ToDateTime(readerholiday["start_date"].ToString());
                    DateTime end = Convert.ToDateTime(readerholiday["end_date"].ToString());

                    // var lastDayOfMonth = date.AddMonths(1).AddDays(-1);
                    for (; start < end;)
                    {
                        if (start >= inStart && start <= inEnd && start.DayOfWeek != DayOfWeek.Sunday) // was creating is 
                        {
                            dateHolidays.Add(start);

                        }
                        start = start.AddDays(1);
                    }
                }
            }
            con.Close();
            return dateHolidays;
        }
        public List<DateTime> GetAllHolidaysBetween(DateTime inStart, DateTime inEnd, int section_id)
        {
            dateHolidays = new List<DateTime>();
            MySqlConnection con = new MySqlConnection(Login.constring);
            con.Open();
            var queryHoliday = "SELECT * FROM tbl_holidays WHERE section_id = " + section_id + " ORDER BY holiday_id DESC ";
            MySqlCommand cmdholiday = new MySqlCommand(queryHoliday, con);
            MySqlDataReader readerholiday = cmdholiday.ExecuteReader();
            if (readerholiday.HasRows)
            {
                while (readerholiday.Read())
                {
                    DateTime start = Convert.ToDateTime(readerholiday["start_date"].ToString());
                    DateTime end = Convert.ToDateTime(readerholiday["end_date"].ToString());

                    // var lastDayOfMonth = date.AddMonths(1).AddDays(-1);
                    for (; start < end;)
                    {
                        if (start >= inStart && start <= inEnd && start.DayOfWeek != DayOfWeek.Sunday) // was creating is 
                        {
                            dateHolidays.Add(start);

                        }
                        start = start.AddDays(1);
                    }
                }
            }
            con.Close();
            return dateHolidays;
        }
        public List<DateTime> GetAllHolidaysAMonth(DateTime date, string sectionID)
        {
            dateHolidays = new List<DateTime>();
            MySqlConnection con = new MySqlConnection(Login.constring);
            con.Open();
            string month_last_date = date.AddMinutes(1).AddDays(-1).ToString("yyyy-MM-dd");
            var queryHoliday = "SELECT * FROM tbl_holidays WHERE section_id = '" + sectionID + "'  AND start_date >= '" + date.ToString("yyyy-MM-dd") + "'  AND end_date <='2022-01-30' ";
            MySqlCommand cmdholiday = new MySqlCommand(queryHoliday, con);
            MySqlDataReader readerholiday = cmdholiday.ExecuteReader();
            if (readerholiday.HasRows)
            {
                while (readerholiday.Read())
                {
                    DateTime start = Convert.ToDateTime(readerholiday["start_date"].ToString());
                    DateTime end = Convert.ToDateTime(readerholiday["end_date"].ToString());

                    var lastDayOfMonth = date.AddMonths(1).AddDays(-1);
                    if (start >= date && end < lastDayOfMonth)
                    {
                        for (; start < end;)
                        {
                            if (!dateHolidays.Contains(start))
                            {
                                dateHolidays.Add(start);
                            }

                            start = start.AddDays(1);
                        }
                    }
                }
            }
            con.Close();
            return dateHolidays;
        }
        public List<DateTime> GetAllHolidaysAMonth(DateTime date)
        {
            dateHolidays = new List<DateTime>();
            MySqlConnection con = new MySqlConnection(Login.constring);
            con.Open();
            var queryHoliday = "SELECT * FROM tbl_holidays";
            MySqlCommand cmdholiday = new MySqlCommand(queryHoliday, con);
            MySqlDataReader readerholiday = cmdholiday.ExecuteReader();
            if (readerholiday.HasRows)
            {
                while (readerholiday.Read())
                {
                    DateTime start = Convert.ToDateTime(readerholiday["start_date"].ToString());
                    DateTime end = Convert.ToDateTime(readerholiday["end_date"].ToString());

                    var lastDayOfMonth = date.AddMonths(1).AddDays(-1);
                    if (start >= date && end < lastDayOfMonth)
                    {
                        for (; start < end;)
                        {
                            if (!dateHolidays.Contains(start))
                            {
                                dateHolidays.Add(start);
                            }

                            start = start.AddDays(1);
                        }
                    }
                }
            }
            con.Close();
            return dateHolidays;
        }
        public int CountSundaysBetween(DateTime startDate, DateTime endDate)
        {
            int weekEndCount = 0;
            if (startDate > endDate)
            {
                DateTime temp = startDate;
                startDate = endDate;
                endDate = temp;
            }
            TimeSpan diff = endDate - startDate;
            int days = diff.Days;
            for (var i = 0; i <= days; i++)
            {
                var testDate = startDate.AddDays(i);
                if (testDate.DayOfWeek == DayOfWeek.Sunday)
                {
                    weekEndCount += 1;
                }
            }
            return weekEndCount;

        }
        public List<String> GetSundays(DateTime start_date, DateTime end_date)
        {
            List<String> days_list = new List<String>();
            for (DateTime date = start_date; date <= end_date; date = date.AddDays(1))
            {
                if (date.DayOfWeek == DayOfWeek.Sunday)
                {
                    days_list.Add(date.ToString("yyyy-MM-dd"));
                }
            }

            return days_list;
        }
        public String GetExludeDates(DateTime dateS, DateTime dateE)
        {
            String holiday_str = "";
            var res = this.GetAllHolidaysBetween(dateS, dateE);
            var sunday_date = this.GetSundays(dateS, dateE);
            List<String> days_list = new List<String>();
            if (res.Count > 0)
            {
                foreach (var mydate in res)
                {
                    days_list.Add("'" + mydate.ToString("yyyy-MM-dd") + "'");
                }
            }
            if (sunday_date.Count > 0)
            {
                foreach (var mydate in sunday_date)
                {
                    days_list.Add("'" + mydate + "'");
                }
            }
            if (sunday_date.Count > 0)
            {
                holiday_str = String.Join(",", days_list);
            }
            return holiday_str;
        }
        public int CountSundays(int year, int month)
        {
            var firstDay = new DateTime(year, month, 1);

            var day29 = firstDay.AddDays(28);
            var day30 = firstDay.AddDays(29);
            var day31 = firstDay.AddDays(30);

            if ((day29.Month == month && day29.DayOfWeek == DayOfWeek.Sunday)
            || (day30.Month == month && day30.DayOfWeek == DayOfWeek.Sunday)
            || (day31.Month == month && day31.DayOfWeek == DayOfWeek.Sunday))
            {
                return 5;
            }
            else
            {
                return 4;
            }
        }

        private List<string> workingDays;
        public List<string> GetAllWorkingDaysS(int section_id)
        {
            workingDays = new List<string>();
            MySqlConnection con = new MySqlConnection(Login.constring);
            con.Open();
            MySqlCommand cmd1 = new MySqlCommand("SELECT * FROM section where section_id='" + section_id + "'", con);
            MySqlDataReader reader1 = cmd1.ExecuteReader();
            if (reader1.HasRows)
            {
                while (reader1.Read())
                {
                    var a = reader1["days"].ToString();
                    var info = a.Split(',');
                    foreach (var day in info)
                    {
                        workingDays.Add(day.Trim());
                    }
                }
            }

            con.Close();
            return workingDays;
        }
        public DataTable GetAllClasses_dt()
        {
            string query = "SELECT class_id, name FROM class ORDER BY class_id ASC ";
            return FetchDataTable(query);
        }
        public DataTable GetAllSection_dt(string class_id)
        {
            string where = "";
            if (class_id == "-1") // all sections
                where = "";
            else
                where = "where class_id in (" + class_id + ")";
            string query = "SELECT section_id, name FROM section " + where + "  ORDER BY section_id ASC ";
            return FetchDataTable(query);
        }
        public ObservableCollection<AllValues> GetAllClasses()
        {
            allValue = new ObservableCollection<AllValues>();
            MySqlConnection con = new MySqlConnection(Login.constring);
            con.Open();
            MySqlCommand cmd1 = new MySqlCommand("SELECT class_id, name FROM class ORDER BY name ASC ", con);
            MySqlDataReader reader1 = cmd1.ExecuteReader();
            if (reader1.HasRows)
            {
                while (reader1.Read())
                {
                    AllValues d = new AllValues
                    {
                        ID = int.Parse(reader1["class_id"].ToString()),
                        Name = reader1["name"].ToString()
                    };
                    allValue.Add(d);
                }
            }
            con.Close();
            return allValue;
        }
        public ObservableCollection<AllValues> GetAllSchool()
        {
            allValue = new ObservableCollection<AllValues>();
            MySqlConnection con = new MySqlConnection(Login.constring);
            con.Open();
            MySqlCommand cmd1 = new MySqlCommand("SELECT * FROM tbl_school ORDER BY `name` ASC ", con);
            MySqlDataReader reader1 = cmd1.ExecuteReader();
            if (reader1.HasRows)
            {
                while (reader1.Read())
                {
                    AllValues d = new AllValues
                    {
                        ID = int.Parse(reader1["school_id"].ToString()),
                        Name = reader1["name"].ToString()
                    };
                    allValue.Add(d);
                }
            }
            con.Close();
            return allValue;
        }
        public string GetPSchoolN(int id)
        {
            var name = "";
            MySqlConnection con = new MySqlConnection(Login.constring);
            con.Open();
            MySqlCommand cmd1 = new MySqlCommand("SELECT * FROM tbl_school where school_id='" + id + "' ", con);
            MySqlDataReader reader1 = cmd1.ExecuteReader();
            if (reader1.HasRows)
            {
                while (reader1.Read())
                {
                    name = reader1["name"].ToString();

                }
            }
            con.Close();
            return name;
        }

        public ObservableCollection<AllValues> GetTeaLectureI(int id, DateTime start, DateTime end)
        {
            allValue = new ObservableCollection<AllValues>();
            MySqlConnection con = new MySqlConnection(Login.constring);
            con.Open();
            MySqlCommand cmd = new MySqlCommand("SELECT institute.name as Institute,floor_sheet.teacher_id, count(*) as Leture FROM floor_sheet "
                + "join class on floor_sheet.class_id=class.class_id "
                + "join institute on institute.id=class.institute_id "
                + "where  date >= '" + start.ToString("yyyy-MM-dd") + "' and date<='" + end.ToString("yyyy-MM-dd") + "' and floor_sheet.teacher_id!=0 and floor_sheet.teacher_id= '" + id + "' group by floor_sheet.teacher_id,class.institute_id", con);
            MySqlDataReader reader1 = cmd.ExecuteReader();
            if (reader1.HasRows)
            {
                while (reader1.Read())
                {
                    AllValues d = new AllValues
                    {
                        ID = int.Parse(reader1["Leture"].ToString()),
                        Name = reader1["Institute"].ToString()
                    };
                    allValue.Add(d);
                }
            }
            con.Close();
            return allValue;
        }




        public ObservableCollection<AllClass> GetAllVisitorID()
        {
            allClass.Clear();
            MySqlConnection con = new MySqlConnection(Login.constring);
            con.Open();
            MySqlCommand cmd1 = new MySqlCommand("SELECT * FROM visitor ", con);
            MySqlDataReader reader1 = cmd1.ExecuteReader();
            if (reader1.HasRows)
            {
                while (reader1.Read())
                {
                    AllClass d = new AllClass
                    {
                        Name = reader1["VID"].ToString()
                    };
                    allClass.Add(d);
                }
            }
            con.Close();
            return allClass;
        }
        public string GetClassName(int id)
        {
            string name = "";
            MySqlConnection con = new MySqlConnection(Login.constring);
            con.Open();
            MySqlCommand cmd = new MySqlCommand("SELECT name from class Where class_id='" + id + "'", con);
            MySqlDataReader reader1 = cmd.ExecuteReader();
            if (reader1.HasRows)
            {
                while (reader1.Read())
                {
                    name = reader1["name"].ToString();
                }
            }
            con.Close();
            return name;
        }
        public string GetSectionName(int id)
        {
            string name = "";
            MySqlConnection con = new MySqlConnection(Login.constring);
            con.Open();
            MySqlCommand cmd = new MySqlCommand("SELECT name from section Where section_id='" + id + "'", con);
            MySqlDataReader reader1 = cmd.ExecuteReader();
            if (reader1.HasRows)
            {
                while (reader1.Read())
                {
                    name = reader1["name"].ToString();
                }
            }
            con.Close();
            return name;
        }

        public int GetClassID(string name)
        {
            int id = 0;
            MySqlConnection con = new MySqlConnection(Login.constring);
            con.Open();
            MySqlCommand cmd = new MySqlCommand("SELECT class_id from class Where name='" + name + "'", con);
            MySqlDataReader reader1 = cmd.ExecuteReader();
            if (reader1.HasRows)
            {
                while (reader1.Read())
                {
                    id = Convert.ToInt32(reader1["class_id"]);
                }
            }
            con.Close();
            return id;
        }
        public ObservableCollection<AllClass> GetClassTiming()
        {
            allClass.Clear();
            MySqlConnection con = new MySqlConnection(Login.constring);
            con.Open();
            MySqlCommand cmd1 = new MySqlCommand("SELECT min(time_start) as ts, max(time_end) as te FROM class;", con);
            MySqlDataReader reader1 = cmd1.ExecuteReader();
            if (reader1.HasRows)
            {
                while (reader1.Read())
                {
                    AllClass d = new AllClass
                    {
                        Name = reader1["ts"].ToString(),
                        Salary = reader1["te"].ToString()
                    };
                    allClass.Add(d);
                }
            }
            con.Close();
            return allClass;
        }
        public ClassTime GetClassTime()
        {
            allClass.Clear();
            MySqlConnection con = new MySqlConnection(Login.constring);
            con.Open();
            MySqlCommand cmd1 = new MySqlCommand("SELECT min(time_start) as time_start, max(time_end) as time_end FROM class;", con);
            MySqlDataReader reader1 = cmd1.ExecuteReader();
            ClassTime ClassTime = new ClassTime();
            if (reader1.HasRows)
            {
                reader1.Read();

                ClassTime.min_time = reader1["time_start"].ToString();
                ClassTime.max_time = reader1["time_end"].ToString();
            }
            con.Close();
            return ClassTime;
        }
        public DataTable GetSectionTable()
        {
            MySqlConnection con = new MySqlConnection(Login.constring);
            DataTable table = new DataTable();
            string query = "SELECT class.class_id, class.name AS class, section.section_id,section.name AS section,section.time_start,section.time_end  " +
                            " FROM section " +
                            " INNER JOIN class ON class.class_id = section.class_id " +
                            " ORDER BY section.class_id ASC, section.section_id ASC";

            MySqlCommand cmd = new MySqlCommand(query, con);
            MySqlDataAdapter adp = new MySqlDataAdapter(cmd);
            adp.Fill(table);
            return table;
        }
        public DataTable GetQueryTable(string query)
        {

            DataTable table = new DataTable();
            try

            {
                MySqlConnection con = new MySqlConnection(Login.constring);
                MySqlCommand cmd = new MySqlCommand(query, con);
                MySqlDataAdapter adp = new MySqlDataAdapter(cmd);
                adp.Fill(table);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Query:" + query + "Exception:" + ex.Message);
            }
            return table;
        }



        public ObservableCollection<AllClass> GetAllClass()
        {
            allClass.Clear();
            MySqlConnection con = new MySqlConnection(Login.constring);
            con.Open();
            MySqlCommand cmd1 = new MySqlCommand("SELECT name FROM class;", con);
            MySqlDataReader reader1 = cmd1.ExecuteReader();
            if (reader1.HasRows)
            {
                while (reader1.Read())
                {
                    AllClass d = new AllClass
                    {
                        Name = reader1["name"].ToString()
                    };
                    allClass.Add(d);
                }
            }
            con.Close();
            return allClass;
        }
        public object get_student_class_id(string student_id)
        {
            string query = "select class_id from student where student_id = '" + student_id + "'";
            return Execute_Scaler_string(query);
        }
        public int GetClassIDisSession(string name, string session)
        {
            int id = 0;
            MySqlConnection con = new MySqlConnection(Login.constring);
            con.Open();
            MySqlCommand cmd = new MySqlCommand("SELECT class_id from class Where name='" + name + "'", con);
            MySqlDataReader reader1 = cmd.ExecuteReader();
            if (reader1.HasRows)
            {
                while (reader1.Read())
                {
                    id = Convert.ToInt32(reader1["class_id"]);
                }
            }
            con.Close();
            return id;
        }

        public ObservableCollection<AllClass> GetAllClassisSession(string name)
        {
            allClass = new ObservableCollection<AllClass>();
            MySqlConnection con = new MySqlConnection(Login.constring);
            con.Open();
            MySqlCommand cmd1 = new MySqlCommand("SELECT class_id, name FROM class where 1 = 1;", con);
            MySqlDataReader reader1 = cmd1.ExecuteReader();
            if (reader1.HasRows)
            {
                while (reader1.Read())
                {
                    AllClass d = new AllClass
                    {
                        Name = reader1["name"].ToString(),
                        Salary = reader1["class_id"].ToString()
                    };
                    allClass.Add(d);
                }
            }
            con.Close();
            return allClass;
        }

        public ObservableCollection<AllClass> GetAllClassFee()
        {
            allClass.Clear();
            MySqlConnection con = new MySqlConnection(Login.constring);
            con.Open();
            MySqlCommand cmd1 = new MySqlCommand("SELECT class.class_id, class.name,cf.monthly FROM class left JOIN class_fees AS cf ON cf.class_id = class.class_id", con);
            MySqlDataReader reader1 = cmd1.ExecuteReader();
            if (reader1.HasRows)
            {
                while (reader1.Read())
                {
                    AllClass d = new AllClass
                    {
                        Name = reader1["name"].ToString(),
                        Salary = reader1["monthly"].ToString()
                    };
                    allClass.Add(d);
                }
            }
            con.Close();
            return allClass;
        }
        public int GetClass_lateFee(int classid)
        {
            int id = 0;
            MySqlConnection con = new MySqlConnection(Login.constring);
            con.Open();
            MySqlCommand cmd = new MySqlCommand("SELECT class.class_id, class.name,cf.late_fee FROM class left JOIN class_fees AS cf ON cf.class_id = class.class_id where class.class_id='" + classid + "'", con);
            MySqlDataReader reader1 = cmd.ExecuteReader();
            if (reader1.HasRows)
            {
                while (reader1.Read())
                {
                    var temp = reader1["late_fee"].ToString() == "" ? "0" : reader1["late_fee"].ToString();
                    id = int.Parse(temp);
                    //  id = 500;
                }
            }
            con.Close();
            return id;
        }
        public int GetClassMonthlyFee(int classid)
        {
            int id = 0;
            MySqlConnection con = new MySqlConnection(Login.constring);
            con.Open();
            MySqlCommand cmd = new MySqlCommand("SELECT class.class_id, class.name,cf.monthly FROM class left JOIN class_fees AS cf ON cf.class_id = class.class_id where class.class_id='" + classid + "'", con);
            MySqlDataReader reader1 = cmd.ExecuteReader();
            if (reader1.HasRows)
            {
                while (reader1.Read())
                {
                    var temp = reader1["monthly"].ToString() == "" ? "0" : reader1["monthly"].ToString();
                    id = int.Parse(temp);
                    //  id = 500;
                }
            }
            con.Close();
            return id;
        }
        public int GetClassAdmissionFee(int classid)
        {
            int id = 0;
            MySqlConnection con = new MySqlConnection(Login.constring);
            con.Open();
            MySqlCommand cmd = new MySqlCommand("SELECT class.class_id, class.name,cf.admission FROM class left JOIN class_fees AS cf ON cf.class_id = class.class_id where class.class_id='" + classid + "'", con);
            MySqlDataReader reader1 = cmd.ExecuteReader();
            if (reader1.HasRows)
            {
                while (reader1.Read())
                {
                    var temp = reader1["admission"].ToString() == "" ? "0" : reader1["admission"].ToString();
                    id = int.Parse(temp);

                }
            }
            con.Close();
            return id;
        }
        public int GetClassSecurityDeposit(int classid)
        {
            int id = 0;
            MySqlConnection con = new MySqlConnection(Login.constring);
            con.Open();
            MySqlCommand cmd = new MySqlCommand("SELECT class.class_id, class.name,cf.security_deposit FROM class left JOIN class_fees AS cf ON cf.class_id = class.class_id where class.class_id='" + classid + "'", con);
            MySqlDataReader reader1 = cmd.ExecuteReader();
            if (reader1.HasRows)
            {
                while (reader1.Read())
                {
                    var temp = reader1["security_deposit"].ToString() == "" ? "0" : reader1["security_deposit"].ToString();
                    id = int.Parse(temp);
                    //  id = 500;
                }
            }
            con.Close();
            return id;
        }
        public int GetClassAnnualCharges(int classid)
        {
            int id = 0;
            MySqlConnection con = new MySqlConnection(Login.constring);
            con.Open();
            MySqlCommand cmd = new MySqlCommand("SELECT class.class_id, class.name,cf.annual_charges FROM class left JOIN class_fees AS cf ON cf.class_id = class.class_id where class.class_id='" + classid + "'", con);
            MySqlDataReader reader1 = cmd.ExecuteReader();
            if (reader1.HasRows)
            {
                while (reader1.Read())
                {
                    var temp = reader1["annual_charges"].ToString() == "" ? "0" : reader1["annual_charges"].ToString();
                    id = int.Parse(temp);

                }
            }
            con.Close();
            return id;
        }
        public int GetClassExamCharges(int classid)
        {
            int id = 0;
            MySqlConnection con = new MySqlConnection(Login.constring);
            con.Open();
            MySqlCommand cmd = new MySqlCommand("SELECT class.class_id, class.name,cf.exam_charges FROM class left JOIN class_fees AS cf ON cf.class_id = class.class_id where class.class_id='" + classid + "'", con);
            MySqlDataReader reader1 = cmd.ExecuteReader();
            if (reader1.HasRows)
            {
                while (reader1.Read())
                {
                    var temp = reader1["exam_charges"].ToString() == "" ? "0" : reader1["exam_charges"].ToString();
                    id = int.Parse(temp);
                    //  id = 500;
                }
            }
            con.Close();
            return id;
        }
        public int GetClassCardCharges(int classid)
        {
            int id = 0;
            MySqlConnection con = new MySqlConnection(Login.constring);
            con.Open();
            MySqlCommand cmd = new MySqlCommand("SELECT class.class_id, class.name,cf.card_charges FROM class left JOIN class_fees AS cf ON cf.class_id = class.class_id where class.class_id='" + classid + "'", con);
            MySqlDataReader reader1 = cmd.ExecuteReader();
            if (reader1.HasRows)
            {
                while (reader1.Read())
                {
                    var temp = reader1["card_charges"].ToString() == "" ? "0" : reader1["card_charges"].ToString();
                    id = int.Parse(temp);

                }
            }
            con.Close();
            return id;
        }
        public int GetStudentFeeConcession(int Stdid)
        {
            int id = 0;
            MySqlConnection con = new MySqlConnection(Login.constring);
            con.Open();
            MySqlCommand cmd = new MySqlCommand("SELECT fee_concession from student where student_id='" + Stdid + "'", con);
            MySqlDataReader reader1 = cmd.ExecuteReader();
            if (reader1.HasRows)
            {
                while (reader1.Read())
                {
                    id = Convert.ToInt32(reader1["fee_concession"]);
                }
            }
            con.Close();
            return id;
        }
        public int GetStudentAFeeConcession(int Stdid)
        {
            int id = 0;
            MySqlConnection con = new MySqlConnection(Login.constring);
            con.Open();
            MySqlCommand cmd = new MySqlCommand("SELECT addmission_concession from student where student_id='" + Stdid + "'", con);
            MySqlDataReader reader1 = cmd.ExecuteReader();
            if (reader1.HasRows)
            {
                while (reader1.Read())
                {
                    id = Convert.ToInt32(reader1["addmission_concession"]);
                }
            }
            con.Close();
            return id;
        }
        public int GetStudentSDFeeConcession(int Stdid)
        {
            int id = 0;
            MySqlConnection con = new MySqlConnection(Login.constring);
            con.Open();
            MySqlCommand cmd = new MySqlCommand("SELECT security_concession from student where student_id='" + Stdid + "'", con);
            MySqlDataReader reader1 = cmd.ExecuteReader();
            if (reader1.HasRows)
            {
                while (reader1.Read())
                {
                    id = Convert.ToInt32(reader1["security_concession"]);
                }
            }
            con.Close();
            return id;
        }
        public int GetStudentACFeeConcession(int Stdid)
        {
            int id = 0;
            MySqlConnection con = new MySqlConnection(Login.constring);
            con.Open();
            MySqlCommand cmd = new MySqlCommand("SELECT annual_concession from student where student_id='" + Stdid + "'", con);
            MySqlDataReader reader1 = cmd.ExecuteReader();
            if (reader1.HasRows)
            {
                while (reader1.Read())
                {
                    id = Convert.ToInt32(reader1["annual_concession"]);
                }
            }
            con.Close();
            return id;
        }
        public int GetStudentECFeeConcession(int Stdid)
        {
            int id = 0;
            MySqlConnection con = new MySqlConnection(Login.constring);
            con.Open();
            MySqlCommand cmd = new MySqlCommand("SELECT exam_concession from student where student_id='" + Stdid + "'", con);
            MySqlDataReader reader1 = cmd.ExecuteReader();
            if (reader1.HasRows)
            {
                while (reader1.Read())
                {
                    id = Convert.ToInt32(reader1["exam_concession"]);
                }
            }
            con.Close();
            return id;
        }


        public int GetExpenseID(string name)
        {
            int id = 0;
            MySqlConnection con = new MySqlConnection(Login.constring);
            con.Open();
            MySqlCommand cmd = new MySqlCommand("SELECT expense_category_id from expense_category Where name='" + name + "'", con);
            MySqlDataReader reader1 = cmd.ExecuteReader();
            if (reader1.HasRows)
            {
                while (reader1.Read())
                {
                    id = Convert.ToInt32(reader1["expense_category_id"]);
                }
            }
            con.Close();
            return id;
        }
        public int GetBookCategoryID(string name)
        {
            int id = 0;
            MySqlConnection con = new MySqlConnection(Login.constring);
            con.Open();
            MySqlCommand cmd = new MySqlCommand("SELECT books_category_id from books_category Where name='" + name + "'", con);
            MySqlDataReader reader1 = cmd.ExecuteReader();
            if (reader1.HasRows)
            {
                while (reader1.Read())
                {
                    id = Convert.ToInt32(reader1["books_category_id"]);
                }
            }
            con.Close();
            return id;
        }
        public ObservableCollection<AllClass> GetAllECategory()
        {
            allClass.Clear();
            MySqlConnection con = new MySqlConnection(Login.constring);
            con.Open();
            MySqlCommand cmd1 = new MySqlCommand("SELECT name FROM expense_category;", con);
            MySqlDataReader reader1 = cmd1.ExecuteReader();
            if (reader1.HasRows)
            {
                while (reader1.Read())
                {
                    AllClass d = new AllClass
                    {
                        Name = reader1["name"].ToString()
                    };
                    allClass.Add(d);
                }
            }
            con.Close();
            return allClass;
        }

        public ObservableCollection<AllClass> GetAllBooksCategory()
        {
            allClass.Clear();
            MySqlConnection con = new MySqlConnection(Login.constring);
            con.Open();
            MySqlCommand cmd1 = new MySqlCommand("SELECT name FROM books_category;", con);
            MySqlDataReader reader1 = cmd1.ExecuteReader();
            if (reader1.HasRows)
            {
                while (reader1.Read())
                {
                    AllClass d = new AllClass
                    {
                        Name = reader1["name"].ToString()
                    };
                    allClass.Add(d);
                }
            }
            con.Close();
            return allClass;
        }

        public ObservableCollection<AllClass> GetAllBooks()
        {
            allClass.Clear();
            MySqlConnection con = new MySqlConnection(Login.constring);
            con.Open();
            MySqlCommand cmd1 = new MySqlCommand("SELECT * FROM book;", con);
            MySqlDataReader reader1 = cmd1.ExecuteReader();
            if (reader1.HasRows)
            {
                while (reader1.Read())
                {
                    var ss = reader1["name"].ToString() + ">" + reader1["author"].ToString();
                    AllClass d = new AllClass
                    {
                        Name = ss
                    };
                    allClass.Add(d);
                }
            }
            con.Close();
            return allClass;
        }
        public int GetBookQty(string name)
        {
            int id = 0;
            MySqlConnection con = new MySqlConnection(Login.constring);
            con.Open();
            MySqlCommand cmd = new MySqlCommand("SELECT qty from book Where name='" + name.Split('>')[0] + "'", con);
            MySqlDataReader reader1 = cmd.ExecuteReader();
            if (reader1.HasRows)
            {
                while (reader1.Read())
                {
                    id = Convert.ToInt32(reader1["qty"]);
                }
            }
            con.Close();
            return id;
        }
        public int GetBookID(string name)
        {
            int id = 0;
            MySqlConnection con = new MySqlConnection(Login.constring);
            con.Open();
            MySqlCommand cmd = new MySqlCommand("SELECT book_id from book Where name='" + name.Split('>')[0] + "'", con);
            MySqlDataReader reader1 = cmd.ExecuteReader();
            if (reader1.HasRows)
            {
                while (reader1.Read())
                {
                    id = Convert.ToInt32(reader1["book_id"]);
                }
            }
            con.Close();
            return id;
        }

        public int GetTeacherID(string name)
        {
            int id = 0;
            MySqlConnection con = new MySqlConnection(Login.constring);
            con.Open();
            MySqlCommand cmd = new MySqlCommand("SELECT teacher_id from teacher Where name='" + name + "'", con);
            MySqlDataReader reader1 = cmd.ExecuteReader();
            if (reader1.HasRows)
            {
                while (reader1.Read())
                {
                    id = Convert.ToInt32(reader1["teacher_id"]);
                }
            }
            con.Close();
            return id;
        }
        public string GetTeacherName(int id)
        {
            string name = "";
            MySqlConnection con = new MySqlConnection(Login.constring);
            con.Open();
            MySqlCommand cmd = new MySqlCommand("SELECT name from teacher Where teacher_id='" + id + "'", con);
            MySqlDataReader reader1 = cmd.ExecuteReader();
            if (reader1.HasRows)
            {
                while (reader1.Read())
                {
                    name = reader1["name"].ToString();
                }
            }
            con.Close();
            return name;
        }
        public string GetTeachersStartingTime(string name)
        {
            string time = "";
            MySqlConnection con = new MySqlConnection(Login.constring);
            con.Open();
            MySqlCommand cmd = new MySqlCommand("SELECT * from teacher Where name='" + name + "'", con);
            MySqlDataReader reader1 = cmd.ExecuteReader();
            if (reader1.HasRows)
            {
                while (reader1.Read())
                {
                    time = reader1["timeStart"].ToString();
                }
            }
            con.Close();
            return time;
        }
        public string GetTeachersSubject(string name)
        {
            string subject = "";
            MySqlConnection con = new MySqlConnection(Login.constring);
            con.Open();
            MySqlCommand cmd = new MySqlCommand("SELECT * from teacher Where name='" + name + "'", con);
            MySqlDataReader reader1 = cmd.ExecuteReader();
            if (reader1.HasRows)
            {
                while (reader1.Read())
                {
                    subject = reader1["subject_code"].ToString();
                }
            }
            con.Close();
            return subject;
        }
        public double GetTeacherSalary(int id)
        {
            double salary = 0;
            MySqlConnection con = new MySqlConnection(Login.constring);
            con.Open();
            MySqlCommand cmd1 = new MySqlCommand("SELECT * FROM teacher where teacher_id='" + id + "'", con);
            MySqlDataReader reader1 = cmd1.ExecuteReader();
            if (reader1.HasRows)
            {
                while (reader1.Read())
                {
                    salary = int.Parse(reader1["salary"].ToString());
                }
            }
            con.Close();
            return salary;
        }
        public DataTable degrees()
        {
            string query = "select degree_id,name from degrees";
            DataTable dt = FetchDataTable(query);
            return dt;
        }
        public void print_receipt_cash(DataRow row)
        {
            FeeRecipt_single output = new FeeRecipt_single();
            FeeRecipt_single MReport = new FeeRecipt_single(row);
            MReport.CreateDocument(false);
            output.Pages.AddRange(MReport.Pages);
            output.PrintingSystem.ContinuousPageNumbering = true;
            ReportPrintTool printTool = new ReportPrintTool(output);

            printTool.PreviewForm.PrintingSystem.Document.AutoFitToPagesWidth = 1;
            printTool.ShowRibbonPreviewDialog(UserLookAndFeel.Default);
        }

        private int[] cat_ids;
        private String[] cat_titles;
        private object grid_ID;
        public DataTable fee_categories()
        {
            string query_cat = "select fca.fee_cat_id,fc.fees_title " +
                                "from invoice " +
                                "inner join fees_cat_amount as fca on fca.invoice_id = invoice.invoice_id " +
                                "left join fees_category as fc on fc.fee_cat_id = fca.fee_cat_id " +
                                "where invoice.forward = 0 " +
                                "group by fca.fee_cat_id ";
            DataTable table_cat = FetchDataTable(query_cat);
            return table_cat;
        }
        public DataTable invoice_grid_data(int invoice_id = 0)
        {

            DataTable table_cat = fee_categories();
            cat_ids = new int[table_cat.Rows.Count];
            cat_titles = new String[table_cat.Rows.Count];
            int count = 0;
            int fee_cat_id;
            String extra_col = "";
            foreach (DataRow row in table_cat.Rows)
            {
                fee_cat_id = Convert.ToInt32(row["fee_cat_id"].ToString());
                cat_ids[count] = fee_cat_id;
                cat_titles[count] = row["fees_title"].ToString();
                extra_col += ",IFNULL((SELECT amount FROM fees_cat_amount WHERE fee_cat_id = '" + fee_cat_id + "' AND invoice_id = invoice.invoice_id ORDER BY id DESC LIMIT 1),0)" +
                    " AS '" + row["fees_title"].ToString() + "'";
                count++;

            }
            string where = "";
            if (invoice_id > 0)
            {
                where = " and invoice.invoice_id = '" + invoice_id + "'";
            }

            string query = "SELECT invoice_id as ID,student.student_id as StdID,student.name as Name,student.registration_no,student.sex as Gender,if(student.passout=0,'current','passout') as type,title as Title,IFNULL(invoice.other_fee,0) AS Other_Fee,IFNULL(late_fee,0) as late_fee,previous_fee as Previous,current_fee as Current,invoice.fee_concession as Concession,amount as Amount,amount_paid as Paid,due as Due,status as Status,date as Date ,class.class_id,class.name as Class,section.name as Section,remarks as Remarks,IFNULL(invoice.payment_details, invoice.description) AS Detail,due_date,number_month AS month, " +
                " (SELECT COUNT(installment_id) FROM fees_installment WHERE fees_installment.invoice_id = invoice.invoice_id ) AS installment,student.roll " +
                extra_col + ",parent.name AS Father,parent.phone as ParentPhone, " +
                " (SELECT ifnull(`transaction`,0) FROM `payment` WHERE `invoice_id` = invoice.invoice_id ORDER BY `payment_id` DESC LIMIT 1) as transaction, " +
                " (SELECT DATE_FORMAT(date, '%d-%m-%Y') FROM `payment` WHERE `invoice_id` = invoice.invoice_id ORDER BY `payment_id` DESC LIMIT 1) as paid_date " +
                " FROM invoice " +
                " inner join student on student.student_id = invoice.student_id " +
                " inner join parent on student.parent_id = parent.parent_id " +
                " left join class on class.class_id=student.class_id  " +
                " left join section on section.section_id=student.section_id " +
                " where 1 = 1 AND forward = 0 " + where;// and student.passout = 0";

            DataTable table = FetchDataTable(query);

            return table;
        }
        public DataTable GetAllTeacher_dt(string where = "")
        {
            string query = "SELECT `teacher_id`,`name`,`subject_code` FROM `teacher`  where passout = 0 " + where + ";";
            return FetchDataTable(query);
        }
        public DataTable GetTeacher_details(int teacher_id)
        {
            string query = "SELECT * FROM `teacher` where `teacher_id` = '" + teacher_id + "';";
            return FetchDataTable(query);
        }
        public ObservableCollection<AllClass> GetAllTeacher()
        {
            allClass.Clear();
            MySqlConnection con = new MySqlConnection(Login.constring);
            try
            {
                con.Open();
                MySqlCommand cmd1 = new MySqlCommand("SELECT * FROM teacher where passout = 0  order by subject_code;", con);
                MySqlDataReader reader1 = cmd1.ExecuteReader();
                if (reader1.HasRows)
                {
                    while (reader1.Read())
                    {
                        AllClass d = new AllClass
                        {
                            Name = reader1["name"].ToString(),
                            Salary = reader1["salary"].ToString(),
                            teacher_id = reader1["teacher_id"].ToString()
                        };
                        allClass.Add(d);
                    }
                }
            }
            catch (MySqlException ex)
            {
                MessageBox.Show("Execute Query: " + ex.ToString());
            }
            catch (Exception e)
            {
                MessageBox.Show("Execute Query: " + e.ToString());
            }
            finally
            {
                con.Close();
            }
            return allClass;
        }


        public ObservableCollection<AllClass> GetAllTeacher(int classid)
        {
            allClass.Clear();
            MySqlConnection con = new MySqlConnection(Login.constring);
            try
            {
                con.Open();
                var query = "SELECT teacher.* FROM teacher INNER JOIN class ON class.teacher_id = teacher.teacher_id "
                                     + " WHERE class.class_id = '" + classid + "' and passout = 0 GROUP BY teacher.teacher_id ";
                MySqlCommand cmd1 = new MySqlCommand(query, con);
                MySqlDataReader reader1 = cmd1.ExecuteReader();
                if (reader1.HasRows)
                {
                    while (reader1.Read())
                    {
                        AllClass d = new AllClass
                        {
                            Name = reader1["name"].ToString(),
                            Salary = reader1["teacher_id"].ToString() + ">" + reader1["phone"].ToString()

                        };
                        allClass.Add(d);
                    }
                }
            }
            catch (MySqlException ex)
            {
                MessageBox.Show("Execute Query: " + ex.ToString());
            }
            catch (Exception e)
            {
                MessageBox.Show("Execute Query: " + e.ToString());
            }
            finally
            {
                con.Close();
            }
            return allClass;
        }
        public ObservableCollection<AllClass> GetAllTeacherWId()
        {
            allClass.Clear();
            MySqlConnection con = new MySqlConnection(Login.constring);
            con.Open();
            MySqlCommand cmd1 = new MySqlCommand("SELECT * FROM teacher where passout = 0 order by subject_code;", con);
            MySqlDataReader reader1 = cmd1.ExecuteReader();
            if (reader1.HasRows)
            {
                while (reader1.Read())
                {
                    AllClass d = new AllClass
                    {
                        Name = reader1["name"].ToString(),
                        Salary = reader1["teacher_id"].ToString() + ">" + reader1["phone"].ToString() + ">" + reader1["address"].ToString() + ">" + reader1["designation"].ToString()

                    };
                    allClass.Add(d);
                }
            }
            con.Close();
            return allClass;
        }
        public ObservableCollection<TeachingStaff> GetAllTeachingStaffWId(string type)
        {
            if (type == "")
            {
                type = "1";
            }

            teachingStaff = new ObservableCollection<TeachingStaff>();
            MySqlConnection con = new MySqlConnection(Login.constring);
            con.Open();
            MySqlCommand cmd1 = new MySqlCommand("SELECT * FROM teacher where " + type + " and passout = 0 order by subject_code;", con);
            MySqlDataReader reader1 = cmd1.ExecuteReader();
            if (reader1.HasRows)
            {
                while (reader1.Read())
                {
                    TeachingStaff d = new TeachingStaff
                    {
                        ID = reader1["teacher_id"].ToString(),
                        Name = reader1["name"].ToString()


                    };
                    teachingStaff.Add(d);
                }
            }
            con.Close();
            return teachingStaff;
        }
        public int GetStaffId(string name)
        {
            int id = 0;
            MySqlConnection con = new MySqlConnection(Login.constring);
            con.Open();
            MySqlCommand cmd = new MySqlCommand("SELECT teacher_id from teacher Where name='" + name + "'", con);
            MySqlDataReader reader1 = cmd.ExecuteReader();
            if (reader1.HasRows)
            {
                while (reader1.Read())
                {
                    id = Convert.ToInt32(reader1["teacher_id"]);
                }
            }
            con.Close();
            return id;
        }
        public int GetSessionID(string name)
        {
            int id = 0;
            MySqlConnection con = new MySqlConnection(Login.constring);
            con.Open();
            MySqlCommand cmd = new MySqlCommand("SELECT session_id from session Where name='" + name + "'", con);
            MySqlDataReader reader1 = cmd.ExecuteReader();
            if (reader1.HasRows)
            {
                while (reader1.Read())
                {
                    id = Convert.ToInt32(reader1["session_id"]);
                }
            }
            con.Close();
            return id;
        }
        public DataTable All_SessionName()
        {
            string query = "SELECT session_id,name from session order by session_id";
            DataTable dtsession = FetchDataTable(query);
            return dtsession;
        }
        public string GetDefaultSessionName()
        {
            string id = "";
            MySqlConnection con = new MySqlConnection(Login.constring);
            con.Open();
            MySqlCommand cmd = new MySqlCommand("SELECT name from session Where default_session='1'", con);
            MySqlDataReader reader1 = cmd.ExecuteReader();
            if (reader1.HasRows)
            {
                while (reader1.Read())
                {
                    id = (reader1["name"]).ToString();
                }
            }
            con.Close();
            return id;
        }
        public ObservableCollection<AllClass> GetAllSession()
        {
            allClass.Clear();
            MySqlConnection con = new MySqlConnection(Login.constring);
            con.Open();
            MySqlCommand cmd1 = new MySqlCommand("SELECT * FROM session;", con);
            MySqlDataReader reader1 = cmd1.ExecuteReader();
            if (reader1.HasRows)
            {
                while (reader1.Read())
                {
                    AllClass d = new AllClass
                    {
                        Name = reader1["name"].ToString(),
                        Salary = reader1["session_id"].ToString()

                    };
                    allClass.Add(d);
                }
            }
            con.Close();
            return allClass;
        }

        public int GetStudentID(string name)
        {
            int id = 0;
            MySqlConnection con = new MySqlConnection(Login.constring);
            con.Open();
            MySqlCommand cmd = new MySqlCommand("SELECT student_id from student Where name='" + name.Trim() + "'", con);
            MySqlDataReader reader1 = cmd.ExecuteReader();
            if (reader1.HasRows)
            {
                while (reader1.Read())
                {
                    id = Convert.ToInt32(reader1["student_id"]);
                }
            }
            con.Close();
            return id;
        }
        public DataTable GetStudentInfo_n(int std_id)
        {
            string query = "SELECT students.* ,tbl_school.name AS School " +
                        "FROM students " +
                        "LEFT JOIN tbl_school ON tbl_school.school_id = students.P_School " +
                        "Where SrNo = '" + std_id + "'";
            return FetchDataTable(query);
        }
        public string GetStudentInfo(string name)
        {
            string info = "";
            MySqlConnection con = new MySqlConnection(Login.constring);
            con.Open();
            var query = "";
            if (name.Contains('>'))
            {
                query = "SELECT students.* ,tbl_school.name AS School " +
                        "FROM students " +
                        "LEFT JOIN tbl_school ON tbl_school.school_id = students.P_School " +
                        " Where students.name = '" + name.Split('>')[0] + "' ";
            }
            else
            {
                query = "SELECT students.* ,tbl_school.name AS School " +
                        "FROM students " +
                        "LEFT JOIN tbl_school ON tbl_school.school_id = students.P_School " +
                        "Where SrNo = '" + name.Split('>')[0] + "'";
            }

            MySqlCommand cmd = new MySqlCommand(query, con);
            MySqlDataReader reader1 = cmd.ExecuteReader();
            if (reader1.HasRows)
            {
                while (reader1.Read())
                {
                    var F = reader1["FatherName"].ToString() == "" ? "Null" : reader1["FatherName"].ToString();
                    var FC = reader1["FatherPhone"].ToString() == "" ? "Null" : reader1["FatherPhone"].ToString();
                    var A = reader1["Address"].ToString() == "" ? "Null" : reader1["Address"].ToString();
                    var R = reader1["Roll"].ToString() == "" ? "Null" : reader1["Roll"].ToString();
                    var C = reader1["Class"].ToString() == "" ? "Null" : reader1["Class"].ToString();
                    var S = reader1["Section"].ToString() == "" ? "Null" : reader1["Section"].ToString();
                    var Dd = reader1["Birthday"].ToString();
                    var D = "";
                    var PS = "";
                    var HS = "";
                    try
                    {
                        D = reader1["Birthday"].ToString() == "" ? "Null" : Convert.ToDateTime(reader1["Birthday"].ToString()).ToString("dd-MM-yyyy");
                        PS = reader1["School"].ToString() == "" ? "-" : reader1["School"].ToString();
                        HS = "-";//  reader1["Higher_Study"].ToString() == "" ? "-" : reader1["Higher_Study"].ToString();
                    }
                    catch (Exception)
                    {

                    }

                    var G = reader1["Gender"].ToString() == "" ? "Null" : reader1["Gender"].ToString();

                    var M = "-";// reader1["Matric"].ToString() == "" ? "-" : reader1["Matric"].ToString();
                    var FY = "-";//  reader1["1st_Year"].ToString() == "" ? "-" : reader1["1st_Year"].ToString();
                    var SY = "-";//  reader1["2nd_Year"].ToString() == "" ? "-" : reader1["2nd_Year"].ToString();
                    var SC = "-";// reader1["Category"].ToString() == "" ? "-" : reader1["Category"].ToString();

                    var SP = reader1["Phone"].ToString() == "" ? "-" : reader1["Phone"].ToString();
                    var SN = reader1["Name"].ToString() == "" ? "-" : reader1["Name"].ToString();

                    var SI = reader1["SrNo"].ToString() == "" ? "-" : reader1["SrNo"].ToString();
                    var SR = "-";// reader1["Religion"].ToString() == "" ? "-" : reader1["Religion"].ToString();
                    info = F + ">" + FC + ">" + A + ">" + R + ">" + C + ">" + S + ">" + D + ">" + G + ">" + M + ">" + FY + ">" + SY + ">" + SC + ">" + SP + ">" + SN + ">" + PS + ">" + HS + ">" + SI + ">" + SR;
                }

            }
            con.Close();
            return info;
        }
        public string GetStudentInfo(int id)
        {
            string info = "";
            MySqlConnection con = new MySqlConnection(Login.constring);
            con.Open();
            MySqlCommand cmd = new MySqlCommand("SELECT * FROM student where student_id='" + id + "'", con);
            MySqlDataReader reader1 = cmd.ExecuteReader();
            if (reader1.HasRows)
            {
                while (reader1.Read())
                {
                    var A = reader1["address"].ToString() == "" ? "Null" : reader1["address"].ToString();
                    var R = reader1["roll"].ToString() == "" ? "Null" : reader1["roll"].ToString();
                    var N = reader1["name"].ToString() == "" ? "Null" : reader1["name"].ToString();
                    var C = reader1["class_id"].ToString() == "" ? "Null" : reader1["class_id"].ToString();
                    var S = reader1["section_id"].ToString() == "" ? "Null" : reader1["section_id"].ToString();

                    info = N + ">" + C + ">" + S + ">" + A + ">" + R;
                }

            }
            con.Close();
            return info;
        }
        public int GetStudentSectionID(int id)
        {
            int Sid = 0;
            MySqlConnection con = new MySqlConnection(Login.constring);
            con.Open();
            MySqlCommand cmd = new MySqlCommand("SELECT * from student Where student_id='" + id + "'", con);
            MySqlDataReader reader1 = cmd.ExecuteReader();
            if (reader1.HasRows)
            {
                while (reader1.Read())
                {
                    Sid = Convert.ToInt32(reader1["section_id"]);
                }
            }
            con.Close();
            return Sid;
        }

        public ObservableCollection<AllClass> GetAllStudents()
        {
            allClass.Clear();
            MySqlConnection con = new MySqlConnection(Login.constring);
            con.Open();
            MySqlCommand cmd1 = new MySqlCommand("SELECT student.name as student,class.name as class FROM student left join class on (class.class_id=student.class_id)", con);
            MySqlDataReader reader1 = cmd1.ExecuteReader();
            if (reader1.HasRows)
            {
                while (reader1.Read())
                {
                    AllClass d = new AllClass
                    {
                        Name = reader1["student"].ToString() + ">" + reader1["class"].ToString(),
                        Salary = reader1["class"].ToString()
                    };
                    allClass.Add(d);
                }
            }
            con.Close();
            return allClass;
        }
        public ObservableCollection<AllClass> GetAllStudentsIsSession(string session)
        {
            allClass.Clear();
            MySqlConnection con = new MySqlConnection(Login.constring);
            con.Open();
            MySqlCommand cmd1 = new MySqlCommand("SELECT student_id, student.name as student,class.name as class FROM student left join class on (class.class_id=student.class_id) where 1 = 1", con);
            MySqlDataReader reader1 = cmd1.ExecuteReader();
            if (reader1.HasRows)
            {
                while (reader1.Read())
                {
                    AllClass d = new AllClass
                    {
                        Name = reader1["student_id"].ToString(),
                        Salary = reader1["class"].ToString()
                    };
                    allClass.Add(d);
                }
            }
            con.Close();
            return allClass;
        }
        public ObservableCollection<AllClass> GetAllStudents(int id)
        {
            allClass.Clear();
            MySqlConnection con = new MySqlConnection(Login.constring);
            con.Open();
            MySqlCommand cmd1 = new MySqlCommand("SELECT student.name as student,class.name as class FROM student left join class on (class.class_id=student.class_id) where student.class_id='" + id + "'", con);
            MySqlDataReader reader1 = cmd1.ExecuteReader();
            if (reader1.HasRows)
            {
                while (reader1.Read())
                {
                    AllClass d = new AllClass
                    {
                        Name = reader1["student"].ToString(),
                        Salary = reader1["class"].ToString()
                    };
                    allClass.Add(d);
                }
            }
            con.Close();
            return allClass;
        }
        public DataTable GetAllStudentsinSection(string section_id, string str = "")
        {
            string query = "SELECT student.student_id,student.name,class.name as class,sec.name as section " +
                " FROM student " +
                " inner join class on (class.class_id=student.class_id) " +
                " inner join section as sec on sec.section_id = student.section_id " +
                " where student.section_id in (" + section_id + ") and student.passout != 1 " + str;
            return FetchDataTable(query);
        }
        public ObservableCollection<AllStudent> GetAllStudentsisSection(int id, string str)
        {
            allStudent = new ObservableCollection<AllStudent>();
            MySqlConnection con = new MySqlConnection(Login.constring);
            con.Open();
            MySqlCommand cmd1 = new MySqlCommand("SELECT student_id, student.name as student,class.name as class FROM student " +
                "left join class on (class.class_id=student.class_id)  where student.section_id='" + id + "' " + str + "", con);
            MySqlDataReader reader1 = cmd1.ExecuteReader();
            if (reader1.HasRows)
            {
                while (reader1.Read())
                {
                    AllStudent d = new AllStudent
                    {
                        Name = reader1["student_id"].ToString() + " > " + reader1["student"].ToString(),
                        Class = reader1["class"].ToString()
                    };
                    allStudent.Add(d);
                }
            }
            con.Close();
            return allStudent;
        }

        public ObservableCollection<AllStudent> GetAllStudentIsSession(string session)
        {
            allStudent = new ObservableCollection<AllStudent>();
            MySqlConnection con = new MySqlConnection(Login.constring);
            con.Open();
            MySqlCommand cmd1 = new MySqlCommand("SELECT student_id, student.name as student,class.name as class FROM student left join class on (class.class_id=student.class_id)  where 1 = 1", con);
            MySqlDataReader reader1 = cmd1.ExecuteReader();
            if (reader1.HasRows)
            {
                while (reader1.Read())
                {
                    AllStudent d = new AllStudent
                    {
                        Name = reader1["student"].ToString(),
                        Class = reader1["class"].ToString()
                    };
                    allStudent.Add(d);
                }
            }
            con.Close();
            return allStudent;
        }
        public ObservableCollection<AllStudent> GetAllStudentIsSession(string session, string str)
        {
            allStudent = new ObservableCollection<AllStudent>();
            MySqlConnection con = new MySqlConnection(Login.constring);
            con.Open();
            MySqlCommand cmd1 = new MySqlCommand("SELECT student_id, student.name as student,class.name as class FROM student left join class on (class.class_id=student.class_id)  where 1 = 1" + str + "", con);
            MySqlDataReader reader1 = cmd1.ExecuteReader();
            if (reader1.HasRows)
            {
                while (reader1.Read())
                {
                    AllStudent d = new AllStudent
                    {
                        Name = reader1["student_id"].ToString(),
                        Class = reader1["class"].ToString()
                    };
                    allStudent.Add(d);
                }
            }
            con.Close();
            return allStudent;
        }

        public ObservableCollection<AllClass> GetAllStudentsWId()
        {
            allClass.Clear();
            MySqlConnection con = new MySqlConnection(Login.constring);
            con.Open();
            MySqlCommand cmd1 = new MySqlCommand("SELECT student_id,student.name as student,phone FROM student", con);
            MySqlDataReader reader1 = cmd1.ExecuteReader();
            if (reader1.HasRows)
            {
                while (reader1.Read())
                {
                    AllClass d = new AllClass
                    {
                        Name = reader1["student"].ToString() + ">" + reader1["phone"].ToString(),
                        Salary = reader1["student_id"].ToString()
                    };
                    allClass.Add(d);
                }
            }
            con.Close();
            return allClass;
        }
        public ObservableCollection<AllClass> GetAllStudentsWId(int id)
        {
            allClass.Clear();
            MySqlConnection con = new MySqlConnection(Login.constring);
            con.Open();
            MySqlCommand cmd1 = new MySqlCommand("SELECT student_id,student.name as student,class.name as class FROM student left join class on (class.class_id=student.class_id) where student.class_id='" + id + "'", con);
            MySqlDataReader reader1 = cmd1.ExecuteReader();
            if (reader1.HasRows)
            {
                while (reader1.Read())
                {
                    AllClass d = new AllClass
                    {
                        Name = reader1["student"].ToString(),
                        Salary = reader1["student_id"].ToString()
                    };
                    allClass.Add(d);
                }
            }
            con.Close();
            return allClass;
        }
        public ObservableCollection<AllClass> GetAllStudentsWId_S_C_S(int classid)
        {
            allClass.Clear();
            MySqlConnection con = new MySqlConnection(Login.constring);
            con.Open();
            MySqlCommand cmd1 = new MySqlCommand("SELECT student_id,student.name as student,student.phone as phone,class.name as class,parent.phone as Pphone FROM student join parent on parent.parent_id=student.parent_id left join class on (class.class_id=student.class_id) where student.class_id='" + classid + "' ", con);
            MySqlDataReader reader1 = cmd1.ExecuteReader();
            if (reader1.HasRows)
            {
                while (reader1.Read())
                {
                    AllClass d = new AllClass
                    {
                        Name = reader1["student"].ToString() + ">" + reader1["phone"].ToString() + ">" + reader1["Pphone"].ToString(),
                        Salary = reader1["student_id"].ToString()
                    };
                    allClass.Add(d);
                }
            }
            con.Close();
            return allClass;
        }

        public DataTable GetAllStudentsWId_S_C_S(int classid, int sectionid, string str = "")
        {
            string query = "SELECT student_id,student.name as student,student.phone as phone,class.name as class,parent.phone as Pphone FROM student " +
                " join parent on parent.parent_id=student.parent_id " +
                " left join class on (class.class_id=student.class_id) " +
                " where student.passout = 0 AND student.class_id='" + classid + "' and student.section_id='" + sectionid + "'" + str + "";
            DataTable dt = FetchDataTable(query);
            return dt;
        }
        public ObservableCollection<StudentInfo> GetAllStudentsWId_S_C_S(string str)
        {
            Studentinfo.Clear();
            MySqlConnection con = new MySqlConnection(Login.constring);
            con.Open();
            MySqlCommand cmd1 = new MySqlCommand("SELECT student_id,student.name as student,student.roll as roll,parent.name as parent,class.name as class ,section.name as section FROM student join parent on parent.parent_id=student.parent_id left join class on (class.class_id=student.class_id) join section on (section.section_id=student.section_id)" + str + " ", con);
            MySqlDataReader reader1 = cmd1.ExecuteReader();
            if (reader1.HasRows)
            {
                while (reader1.Read())
                {
                    StudentInfo d = new StudentInfo
                    {
                        Name = reader1["student"].ToString(),
                        ID = reader1["student_id"].ToString(),
                        Father_Name = reader1["parent"].ToString(),
                        Class = reader1["class"].ToString(),
                        Roll = reader1["roll"].ToString(),
                        Section = reader1["section"].ToString()
                    };
                    Studentinfo.Add(d);
                }
            }
            con.Close();
            return Studentinfo;
        }
        public string GetClassStartingTime(int id)
        {
            string time = "";
            MySqlConnection con = new MySqlConnection(Login.constring);
            con.Open();
            MySqlCommand cmd = new MySqlCommand("SELECT class.time_start as timeStart FROM student join class on class.class_id=student.class_id where student_id='" + id + "'", con);
            MySqlDataReader reader1 = cmd.ExecuteReader();
            if (reader1.HasRows)
            {
                while (reader1.Read())
                {
                    time = reader1["timeStart"].ToString();
                }
            }
            con.Close();
            return time;
        }


        public int GetExamID(string name)
        {
            int id = 0;
            MySqlConnection con = new MySqlConnection(Login.constring);
            con.Open();
            MySqlCommand cmd = new MySqlCommand("SELECT exam_id FROM exam Where name='" + name.Trim() + "'", con);
            MySqlDataReader reader1 = cmd.ExecuteReader();
            if (reader1.HasRows)
            {
                while (reader1.Read())
                {
                    id = Convert.ToInt32(reader1["exam_id"]);
                }
            }
            con.Close();
            return id;
        }
        public int GetExamIDIsSession(string name, int sessionId)
        {
            int id = 0;
            MySqlConnection con = new MySqlConnection(Login.constring);
            con.Open();
            MySqlCommand cmd = new MySqlCommand("SELECT exam_id FROM exam Where name='" + name.Trim() + "'", con);
            MySqlDataReader reader1 = cmd.ExecuteReader();
            if (reader1.HasRows)
            {
                while (reader1.Read())
                {
                    id = Convert.ToInt32(reader1["exam_id"]);
                }
            }
            con.Close();
            return id;
        }
        public DataTable GetExamDetail_dt(string exam_id)
        {
            string query = "SELECT * FROM exam Where exam_id='" + exam_id + "'";
            return FetchDataTable(query);
        }
        public ObservableCollection<AllClass> GetExamDetailIsSession(string name, string session)
        {
            MySqlConnection con = new MySqlConnection(Login.constring);
            allClass.Clear();
            con.Open();
            MySqlCommand cmd = new MySqlCommand("SELECT * FROM exam Where name='" + name.Trim() + "'", con);
            MySqlDataReader reader1 = cmd.ExecuteReader();
            if (reader1.HasRows)
            {
                while (reader1.Read())
                {
                    AllClass d = new AllClass
                    {
                        Name = reader1["exam_id"].ToString(),
                        Salary = reader1["date"].ToString(),

                    };
                    allClass.Add(d);
                }
            }

            con.Close();
            return allClass;
        }
        public ObservableCollection<AllClass> GetAllExams()
        {
            allClass.Clear();
            MySqlConnection con = new MySqlConnection(Login.constring);
            con.Open();
            MySqlCommand cmd1 = new MySqlCommand("SELECT name FROM exam order by exam.name asc;", con);
            MySqlDataReader reader1 = cmd1.ExecuteReader();
            if (reader1.HasRows)
            {
                while (reader1.Read())
                {
                    AllClass d = new AllClass
                    {
                        Name = reader1["name"].ToString()
                    };
                    allClass.Add(d);
                }
            }
            con.Close();
            return allClass;
        }
        public DataTable GetAllExams_dt()
        {
            string query = "SELECT exam_id,name FROM exam order by exam.exam_id desc;";
            return FetchDataTable(query);
        }
        public ObservableCollection<AllClass> GetAllExamsIsSession(string name)
        {
            allClass.Clear();
            MySqlConnection con = new MySqlConnection(Login.constring);
            con.Open();
            MySqlCommand cmd1 = new MySqlCommand("SELECT * FROM exam where 1 = 1 order by exam.exam_id desc;", con);
            MySqlDataReader reader1 = cmd1.ExecuteReader();
            if (reader1.HasRows)
            {
                while (reader1.Read())
                {
                    AllClass d = new AllClass
                    {
                        Name = reader1["name"].ToString(),
                        Salary = reader1["exam_id"].ToString()
                    };
                    allClass.Add(d);
                }
            }
            con.Close();
            return allClass;
        }
        public DataTable GetAllExamsIsC_S(string classid, string sectionid, string subjectid, string dateRang)
        {
            string query = "SELECT MAX( m.exam_id)AS exam_id, MAX(exam.name ) AS NAME,tms.marks AS total " +
                            " FROM mark AS m " +
                            " JOIN exam ON exam.exam_id = m.exam_id " +
                            " INNER JOIN tbl_mark_subject AS tms ON tms.`class_id` = m.`class_id` AND tms.`section_id` = m.`section_id` AND tms.`subject_id` = m.`subject_id` AND tms.`exam_id` = m.`exam_id` " +
                            " WHERE m.class_id in (" + classid + ") AND m.`section_id` in (" + sectionid + ") AND m.subject_id in (" + subjectid + ")  " + dateRang + "  GROUP BY m.exam_id ORDER BY m.exam_id ASC; ";
            return FetchDataTable(query);
        }
        public DataTable GetAllExamsIsC_Sec(int classid, string subjects_ids, string dateRang)
        {
            string query = "SELECT max( m.exam_id)as exam_id, max(exam.name )as name  " +
                " FROM mark as m " +
                " join exam on exam.exam_id=m.exam_id where m.class_id='" + classid + "' and m.subject_id in (" + subjects_ids + ")" + dateRang + " " +
                " group by mark.exam_id order by  mark.exam_id  asc;";
            return FetchDataTable(query);
        }
        public int GetParentID(int Sid)
        {
            int id = 0;
            MySqlConnection con = new MySqlConnection(Login.constring);
            con.Open();
            MySqlCommand cmd = new MySqlCommand("SELECT parent_id FROM student Where student_id='" + Sid + "'", con);
            MySqlDataReader reader1 = cmd.ExecuteReader();
            if (reader1.HasRows)
            {
                while (reader1.Read())
                {
                    id = Convert.ToInt32(reader1["parent_id"]);
                }
            }
            con.Close();
            return id;
        }

        public int GetParentID(string name)
        {
            var a = name.Split('>');


            int id = 0;
            MySqlConnection con = new MySqlConnection(Login.constring);
        A:
            con.Open();

            MySqlCommand cmd = new MySqlCommand("SELECT parent_id FROM parent Where name='" + a[0] + "' and phone='" + a[1] + "'", con);
            MySqlDataReader reader1 = cmd.ExecuteReader();
            if (reader1.HasRows)
            {
                while (reader1.Read())
                {
                    id = Convert.ToInt32(reader1["parent_id"]);
                }
            }
            else
            {
                con.Close();
                con.Open();
                cmd = new MySqlCommand("INSERT into parent(name,phone,sync) VALUES('" + a[0] + "','" + a[1] + "','0');", con);
                cmd.ExecuteNonQuery(); con.Close();
                goto A;
            }
            con.Close();
            return id;
        }

        public string GetParentInfo(string name)
        {
            var a = name.Split('>');
            var info = "";
            try
            {
                MySqlConnection con = new MySqlConnection(Login.constring);
                con.Open();
                MySqlCommand cmd = new MySqlCommand("SELECT * FROM parent Where name='" + a[0] + "' and phone='" + a[1] + "'", con);
                MySqlDataReader reader1 = cmd.ExecuteReader();
                if (reader1.HasRows)
                {
                    while (reader1.Read())
                    {
                        info = reader1["email"].ToString() + ">" + reader1["phone"].ToString() + ">" + reader1["address"].ToString();

                    }
                }
                con.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine("{0} Exception caught.", ex);
                //MessageBox.Show(ex.Message, "Info");
            }
            return info;
        }
        public ObservableCollection<AllClass> GetAllParent()
        {
            allClass.Clear();
            MySqlConnection con = new MySqlConnection(Login.constring);
            con.Open();
            MySqlCommand cmd1 = new MySqlCommand("SELECT parent.* FROM parent INNER JOIN student ON student.parent_id = parent.parent_id ;", con);
            MySqlDataReader reader1 = cmd1.ExecuteReader();
            if (reader1.HasRows)
            {
                while (reader1.Read())
                {
                    AllClass d = new AllClass
                    {
                        Name = reader1["name"].ToString(),
                        Salary = reader1["parent_id"].ToString() + ">" + reader1["phone"].ToString()
                    };
                    allClass.Add(d);
                }
            }
            con.Close();
            return allClass;
        }
        public ObservableCollection<AllClass> GetAllParent(int classid)
        {
            allClass.Clear();
            MySqlConnection con = new MySqlConnection(Login.constring);
            con.Open();
            MySqlCommand cmd1 = new MySqlCommand("SELECT * FROM parent inner join student on student.parent_id=parent.parent_id where student.class_id='" + classid + "' ;", con);
            MySqlDataReader reader1 = cmd1.ExecuteReader();
            if (reader1.HasRows)
            {
                while (reader1.Read())
                {
                    AllClass d = new AllClass
                    {
                        Name = reader1["name"].ToString(),
                        Salary = reader1["parent_id"].ToString() + ">" + reader1["phone"].ToString()
                    };
                    allClass.Add(d);
                }
            }
            con.Close();
            return allClass;
        }
        public ObservableCollection<AllClass> GetAllParent(int classid, int sectionid)
        {
            allClass.Clear();
            MySqlConnection con = new MySqlConnection(Login.constring);
            con.Open();
            MySqlCommand cmd1 = new MySqlCommand("SELECT * FROM parent inner join student on student.parent_id=parent.parent_id where student.class_id='" + classid + "' and student.section_id='" + sectionid + "' ;", con);
            MySqlDataReader reader1 = cmd1.ExecuteReader();
            if (reader1.HasRows)
            {
                while (reader1.Read())
                {
                    AllClass d = new AllClass
                    {
                        Name = reader1["name"].ToString(),
                        Salary = reader1["parent_id"].ToString() + ">" + reader1["phone"].ToString()
                    };
                    allClass.Add(d);
                }
            }
            con.Close();
            return allClass;
        }

        //public int GetSectionID(string name)
        //{
        //    int id = 0;
        //    MySqlConnection con = new MySqlConnection(Login.constring);
        //    con.Open();
        //    MySqlCommand cmd = new MySqlCommand("SELECT session_id from session Where name='" + name + "'", con);
        //    MySqlDataReader reader1 = cmd.ExecuteReader();
        //    if (reader1.HasRows)
        //    {
        //        while (reader1.Read())
        //        {
        //            id = Convert.ToInt32(reader1["session_id"]);
        //        }
        //    }
        //    con.Close();
        //    return id;
        //}
        public ObservableCollection<AllClass> GetAllSection()
        {
            allClass.Clear();
            MySqlConnection con = new MySqlConnection(Login.constring);
            con.Open();
            MySqlCommand cmd1 = new MySqlCommand("SELECT section_id from section;", con);
            MySqlDataReader reader1 = cmd1.ExecuteReader();
            if (reader1.HasRows)
            {
                while (reader1.Read())
                {
                    AllClass d = new AllClass
                    {
                        Name = reader1["name"].ToString(),
                    };
                    allClass.Add(d);
                }
            }
            con.Close();
            return allClass;
        }
        public int GetSectionIDisClass(string name, int classID)
        {
            int id = 0;
            MySqlConnection con = new MySqlConnection(Login.constring);
            con.Open();
            MySqlCommand cmd = new MySqlCommand("SELECT section_id from section Where name='" + name.Trim() + "'and class_id='" + classID + "'", con);
            MySqlDataReader reader1 = cmd.ExecuteReader();
            if (reader1.HasRows)
            {
                while (reader1.Read())
                {
                    id = Convert.ToInt32(reader1["section_id"]);
                }
            }
            con.Close();
            return id;
        }
        public int GetSectionID(string name)
        {
            int id = 0;
            MySqlConnection con = new MySqlConnection(Login.constring);
            con.Open();
            MySqlCommand cmd = new MySqlCommand("SELECT section_id from section Where name='" + name + "'", con);
            MySqlDataReader reader1 = cmd.ExecuteReader();
            if (reader1.HasRows)
            {
                while (reader1.Read())
                {
                    id = Convert.ToInt32(reader1["section_id"]);
                }
            }
            con.Close();
            return id;
        }
        public ObservableCollection<AllClass> GetAllSectionisClass(string name)
        {
            allClass = new ObservableCollection<AllClass>();
            MySqlConnection con = new MySqlConnection(Login.constring);
            con.Open();
            MySqlCommand cmd1 = new MySqlCommand("SELECT * from section where class_id='" + GetClassIDisSession(name, Main_FD.SelectedSession) + "';", con);
            MySqlDataReader reader1 = cmd1.ExecuteReader();
            if (reader1.HasRows)
            {
                while (reader1.Read())
                {
                    AllClass d = new AllClass
                    {
                        Name = reader1["name"].ToString(),

                    };
                    allClass.Add(d);
                }
            }
            con.Close();
            return allClass;
        }
        public ObservableCollection<AllClass> GetAllSectionisClass(int id)
        {
            allClass = new ObservableCollection<AllClass>();
            MySqlConnection con = new MySqlConnection(Login.constring);
            con.Open();
            MySqlCommand cmd1 = new MySqlCommand("SELECT * from section where class_id='" + id + "';", con);
            MySqlDataReader reader1 = cmd1.ExecuteReader();
            if (reader1.HasRows)
            {
                while (reader1.Read())
                {
                    AllClass d = new AllClass
                    {
                        Name = reader1["name"].ToString().Trim(),
                        Salary = reader1["section_id"].ToString().Trim()

                    };
                    allClass.Add(d);
                }
            }
            con.Close();
            return allClass;
        }

        public int GetSubjectID(string name)
        {
            int id = 0;
            MySqlConnection con = new MySqlConnection(Login.constring);
            con.Open();
            MySqlCommand cmd = new MySqlCommand("SELECT subject_id FROM subject Where name='" + name.Trim() + "'", con);
            MySqlDataReader reader1 = cmd.ExecuteReader();
            if (reader1.HasRows)
            {
                while (reader1.Read())
                {
                    id = Convert.ToInt32(reader1["subject_id"]);
                }
            }
            con.Close();
            return id;
        }
        public int GetSubjectID(string name, int sectionId)
        {
            int id = 0;
            MySqlConnection con = new MySqlConnection(Login.constring);
            con.Open();
            MySqlCommand cmd = new MySqlCommand("SELECT subject_id FROM subject Where name='" + name.Trim() + "'and section_id='" + sectionId + "'", con);
            MySqlDataReader reader1 = cmd.ExecuteReader();
            if (reader1.HasRows)
            {
                while (reader1.Read())
                {
                    id = Convert.ToInt32(reader1["subject_id"]);
                }
            }
            con.Close();
            return id;
        }
        public int GetSubjectTotalMarks(int class_id, int section_id, int subjectID, int examID)
        {
            int id = 0;
            string query = "SELECT marks FROM tbl_mark_subject where class_id = '" + class_id + "' and section_id='" + section_id + "' and subject_id='" + subjectID + "' and exam_id='" + examID + "'";
            id = Convert.ToInt32(Execute_Scaler_string(query));
            return id;
        }
        public ObservableCollection<AllClass> GetAllSubjectCode()
        {
            allClass.Clear();
            MySqlConnection con = new MySqlConnection(Login.constring);
            con.Open();
            MySqlCommand cmd1 = new MySqlCommand("SELECT IFNULL(max(subject_code),0) AS `SubjectCode` FROM subject group by  subject_code ", con);
            MySqlDataReader reader1 = cmd1.ExecuteReader();
            if (reader1.HasRows)
            {
                while (reader1.Read())
                {
                    AllClass d = new AllClass
                    {
                        Name = reader1["SubjectCode"].ToString()
                    };
                    allClass.Add(d);
                }
            }
            con.Close();
            return allClass;
        }
        public ObservableCollection<AllClass> GetAllSubjectCode(string classID)
        {
            allClass.Clear();
            MySqlConnection con = new MySqlConnection(Login.constring);
            con.Open();
            MySqlCommand cmd1 = new MySqlCommand("SELECT max(subject_code) as code ,max(subject.name) as name FROM subject join section on section.section_id =subject.section_id  where section.class_id in(" + classID + ") group by subject_code", con);
            MySqlDataReader reader1 = cmd1.ExecuteReader();
            if (reader1.HasRows)
            {
                while (reader1.Read())
                {
                    AllClass d = new AllClass
                    {
                        Name = reader1["code"].ToString(),
                        Salary = reader1["name"].ToString()
                    };
                    allClass.Add(d);
                }
            }
            con.Close();
            return allClass;
        }

        public ObservableCollection<AllClass> GetAllSubject()
        {
            allClass.Clear();
            MySqlConnection con = new MySqlConnection(Login.constring);
            con.Open();
            MySqlCommand cmd1 = new MySqlCommand("SELECT name FROM subject ", con);
            MySqlDataReader reader1 = cmd1.ExecuteReader();
            if (reader1.HasRows)
            {
                while (reader1.Read())
                {
                    AllClass d = new AllClass
                    {
                        Name = reader1["name"].ToString()
                    };
                    allClass.Add(d);
                }
            }
            con.Close();
            return allClass;
        }
        public DataTable GetAllSubject_dt(string section_id)
        {
            string query = "SELECT ss.subject_id,sub_de.`name` FROM `section_subject` AS ss  " +
                " INNER JOIN `subject_default` AS sub_de ON sub_de.`id` = ss.`subject_id` " +
                " WHERE section_id = '" + section_id + "' ";
            return FetchDataTable(query);
        }
        public DataTable GetAllSubjects_dt()
        {
            string query = "SELECT IFNULL(max(name),0) AS `Subject` FROM subject_default group by  name ";
            return FetchDataTable(query);
        }
        public ObservableCollection<AllClass> GetAllSubjects()
        {
            allClass.Clear();
            MySqlConnection con = new MySqlConnection(Login.constring);
            con.Open();
            MySqlCommand cmd1 = new MySqlCommand("SELECT IFNULL(max(name),0) AS `Subject` FROM subject group by  name ", con);
            MySqlDataReader reader1 = cmd1.ExecuteReader();
            if (reader1.HasRows)
            {
                while (reader1.Read())
                {
                    AllClass d = new AllClass
                    {
                        Name = reader1["Subject"].ToString()
                    };
                    allClass.Add(d);
                }
            }
            con.Close();
            return allClass;
        }
        public ObservableCollection<AllClass> GetAllSubjectIsClass(string classN)
        {
            allClass.Clear();
            MySqlConnection con = new MySqlConnection(Login.constring);
            con.Open();
            MySqlCommand cmd1 = new MySqlCommand("SELECT name FROM subject where class_id='" + GetClassIDisSession(classN, GetDefaultSessionName()) + "'", con);
            MySqlDataReader reader1 = cmd1.ExecuteReader();
            if (reader1.HasRows)
            {
                while (reader1.Read())
                {
                    AllClass d = new AllClass
                    {
                        Name = reader1["name"].ToString()
                    };
                    allClass.Add(d);
                }
            }
            con.Close();
            return allClass;
        }
        public DataTable GetAllSubject_by_subject_ids(string subject_id)
        {
            string query = "select ss.`subject_id`,sub_def.`name`,sub_def.subject_code from `section_subject` as ss inner join `subject_default` as sub_def on sub_def.`id` = ss.`subject_id` where ss.`subject_id` in (" + subject_id + ") GROUP BY sub_def.`id` ";
            return FetchDataTable(query);
        }
        public DataTable GetAllSubject_by_section_dt(string section_id)
        {
            string query = "select ss.`subject_id`,sub_def.`name`,sub_def.subject_code from `section_subject` as ss inner join `subject_default` as sub_def on sub_def.`id` = ss.`subject_id` where ss.`section_id` in (" + section_id + ") GROUP BY sub_def.`id` ";
            return FetchDataTable(query);
        }
        public DataTable GetAllSubject_by_class_dt(string class_id)
        {
            string query = "SELECT ss.`subject_id`,sub_def.`name` FROM `section_subject` AS ss INNER JOIN `subject_default` AS sub_def ON sub_def.`id` = ss.`subject_id` WHERE ss.`class_id` ='" + class_id + "' GROUP BY sub_def.`id`;";
            return FetchDataTable(query);
        }
        public ObservableCollection<AllClass> GetAllSubject(int id)
        {
            allClass = new ObservableCollection<AllClass>();
            MySqlConnection con = new MySqlConnection(Login.constring);
            con.Open();
            MySqlCommand cmd1 = new MySqlCommand("SELECT * FROM subject where section_id='" + id + "' ", con);
            MySqlDataReader reader1 = cmd1.ExecuteReader();
            if (reader1.HasRows)
            {
                while (reader1.Read())
                {
                    AllClass d = new AllClass
                    {
                        Name = reader1["name"].ToString(),
                        Salary = reader1["subject_id"].ToString()
                    };
                    allClass.Add(d);
                }
            }
            con.Close();
            return allClass;
        }

        private ObservableCollection<AllSubject> allSubject = new ObservableCollection<AllSubject>();
        public ObservableCollection<AllSubject> Get_studnt_subject(int stdid, int class_id)
        {
            allSubject = new ObservableCollection<AllSubject>();
            string query = "(SELECT GROUP_CONCAT(subject_id) as subject_ids FROM (SELECT subject_id FROM `mark` WHERE student_id ='" + stdid + "' and class_id = '" + class_id + "' and subject_id > 0 GROUP BY subject_id) AS tb)";
            DataTable subject_ids = FetchDataTable(query);

            if (subject_ids.Rows.Count > 0)
            {
                MySqlConnection con = new MySqlConnection(Login.constring);
                con.Open();
                MySqlCommand cmd1 = new MySqlCommand("SELECT * FROM subject where subject_id in (" + subject_ids.Rows[0]["subject_ids"] + ") order by subject_code", con);
                MySqlDataReader reader1 = cmd1.ExecuteReader();
                if (reader1.HasRows)
                {
                    while (reader1.Read())
                    {
                        AllSubject d = new AllSubject
                        {
                            Name = reader1["name"].ToString(),
                            ID = reader1["subject_id"].ToString(),
                            Code = reader1["subject_code"].ToString()

                        };
                        allSubject.Add(d);
                    }
                }
                con.Close();
            }
            return allSubject;
        }
        public ObservableCollection<AllSubject> GetAllSubjectISS(int id)
        {
            allSubject = new ObservableCollection<AllSubject>();
            MySqlConnection con = new MySqlConnection(Login.constring);
            con.Open();
            MySqlCommand cmd1 = new MySqlCommand("SELECT * FROM `section_subject` AS sub INNER JOIN `subject_default`  AS sd ON sd.`id` = sub.`subject_id` WHERE sub.`section_id` = " + id, con);
            MySqlDataReader reader1 = cmd1.ExecuteReader();
            if (reader1.HasRows)
            {
                while (reader1.Read())
                {
                    AllSubject d = new AllSubject
                    {
                        Name = reader1["name"].ToString(),
                        ID = reader1["subject_id"].ToString(),
                        Code = reader1["subject_code"].ToString()

                    };
                    allSubject.Add(d);
                }
            }
            con.Close();
            return allSubject;
        }
        public ObservableCollection<AllSubject> GetAll_Student_Subjects(int id, string semister)
        {
            allSubject = new ObservableCollection<AllSubject>();
            MySqlConnection con = new MySqlConnection(Login.constring);
            con.Open();
            string query = "SELECT student_id,subject_id,name,subject_code,semister FROM ( " +
                            " SELECT std.`student_id`,subject.subject_id,subject.name,subject.subject_code,sec.semister FROM `student` as std " +
                            " join section as sec on sec.section_id = std.section_id " +
                            " join subject on subject.section_id = sec.section_id " +
                            " join tbl_mark_subject as tms on tms.subject_id = subject.subject_id " +
                            " union all " +
                            " SELECT std.`student_id`,ss.subject_id,ss.name,ss.subject_code,ss.semister FROM `student` as std " +
                            " join section as sec on sec.section_id = std.section_id " +
                            " join semister_subject as ss on ss.section_id = sec.section_id" +
                            " join tbl_mark_subject as tms on tms.subject_id = ss.subject_id) as tb " +
                            " where tb.student_id = '" + id + "' and tb.semister in (" + semister + "); ";
            MySqlCommand cmd1 = new MySqlCommand(query, con);
            MySqlDataReader reader1 = cmd1.ExecuteReader();
            if (reader1.HasRows)
            {
                while (reader1.Read())
                {
                    AllSubject d = new AllSubject
                    {
                        Name = reader1["name"].ToString(),
                        ID = reader1["subject_id"].ToString(),
                        Code = reader1["subject_code"].ToString()

                    };
                    allSubject.Add(d);
                }
            }
            con.Close();
            return allSubject;
        }


        public ObservableCollection<AllClass> GetSetting(string name)
        {

            MySqlConnection con = new MySqlConnection(Login.constring);
            allClass = new ObservableCollection<AllClass>();
            con.Open();
            MySqlCommand cmd1 = new MySqlCommand("SELECT * FROM settings where type='" + name + "' ", con);
            MySqlDataReader reader1 = cmd1.ExecuteReader();
            if (reader1.HasRows)
            {
                while (reader1.Read())
                {
                    AllClass d = new AllClass
                    {
                        Name = reader1["description"].ToString()
                    };
                    allClass.Add(d);
                }
            }
            con.Close();
            return allClass;
        }
        public string GetSettings(string name)
        {
            var des = "";
            try
            {
                MySqlConnection con = new MySqlConnection(Login.constring);
                con.Open();
                MySqlCommand cmd1 = new MySqlCommand("SELECT * FROM settings where type='" + name + "' ", con);
                MySqlDataReader reader1 = cmd1.ExecuteReader();
                if (reader1.HasRows)
                {
                    while (reader1.Read())
                    {
                        des = reader1["description"].ToString();
                    }
                }

                con.Close();
            }
            catch (Exception ex)
            {
                if (name == "device_password")
                {
                    des = "0";
                }

                MessageBox.Show(ex.Message, "Settings Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return des;
        }
        public int GetFailMarks()
        {
            var des = 33;
            MySqlConnection con = new MySqlConnection(Login.constring);
            con.Open();
            MySqlCommand cmd1 = new MySqlCommand("SELECT mark_upto FROM grade WHERE `name` = 'F'", con);
            MySqlDataReader reader1 = cmd1.ExecuteReader();
            if (reader1.HasRows)
            {
                reader1.Read();
                des = int.Parse(reader1["mark_upto"].ToString());
            }
            con.Close();
            return des;
        }
        public DataTable classresult_bysubject(string orderby, int exam_id, int class_id, int section_id)
        {
            string order = "";
            if (orderby == "Roll#")
            {
                order = " order by `Roll` ";
            }

            if (orderby == "%Avg")
            {
                order = " order by INET_ATON(REPLACE(TRIM(RPAD(Average,8,' 0')),' ','.')) desc ";
            }

            DataTable table = new DataTable();
            DataTable all_subjects_dt = new DataTable();
            if (section_id != 0)
            {
                all_subjects_dt = GetAllSubject_by_section_dt(section_id.ToString());
                string metricmarks = "";
                if (GetSettings("show_metric_number") == "1")
                {
                    metricmarks = "(SELECT IFNULL(sfv.value,'') AS val FROM student_fields_values AS sfv " +
                            " WHERE sfv.student_id = student.student_id and sfv.field_id = 2) as `9thMarks`,(SELECT IFNULL(sfv.value, '') AS val FROM student_fields_values AS sfv " +
                            " WHERE sfv.student_id = student.student_id and sfv.field_id = 3) as `10thMarks`,";
                }
                if (all_subjects_dt.Rows.Count > 0)
                {
                    var subject_cols = "";
                    var query = "SELECT student.student_id as ID,student.roll As Roll ,student.name AS Student ,parent.name AS Parent ,student.section_id as Section," + metricmarks + "";
                    foreach (DataRow dr in all_subjects_dt.Rows)
                    {
                        int total = GetSubjectTotalMarks(class_id, section_id, Convert.ToInt32(dr["subject_id"]), exam_id);
                        subject_cols += "`" + dr["name"] + "(" + total + ")" + "`,";
                        query += " IFNULL(MAX(case mark.subject_id when '" + dr["subject_id"] + "'  then " +
                            " (CASE WHEN mark.subject_id  = IFNULL((fbst.`subject_id`),0) THEN (CASE WHEN mark_obtained = '-1' THEN 'A' ELSE mark_obtained END)  else '-' END) else '-' end),0) AS `" + dr["name"] + "(" + total + ")" + "`,";
                    }
                    query += " sum(CASE when mark.subject_id = IFNULL((fbst.`subject_id`),0) Then (case WHEN mark_obtained = '-1' THEN '0' ELSE mark_obtained END) else 0 end) as Obtained," +
                        " sum( CASE when mark.subject_id = IFNULL((fbst.`subject_id`),0)  then (tbl_mark_subject.marks) else '0' end) as `Total`" +
                        " FROM mark inner " +
                        " join student on(student.student_id = mark.student_id) " +
                        " INNER JOIN fee_by_subject_teacher AS fbst ON fbst.`student_id` = mark.`student_id` AND fbst.`section_id` =mark.`section_id` " +
                        " join parent on parent.parent_id=student.parent_id " +
                        " join tbl_mark_subject on tbl_mark_subject.subject_id=mark.subject_id and tbl_mark_subject.exam_id=mark.exam_id "
                        + " where student.passout = 0 AND student.class_id = '" + class_id + "'and student.section_id='" + section_id + "' and mark.exam_id='" + exam_id + "'" +
                                "GROUP BY mark.student_id ,mark.class_id ";
                    query = "set @Average=0;SELECT ID,Roll,Student,Parent,Section," + subject_cols + " Obtained,Total,@Average:=round((Obtained/Total*100),2) as Average," +
                        " (SELECT grade.comment FROM grade WHERE grade.mark_from <= @Average AND grade.mark_upto > @Average LIMIT 1) AS Result FROM(" + query + ") AS tbl " + order + " ";
                    table = FetchDataTable(query);
                    table = CommonFunctions.AutoNumberedTable(table);
                    if (orderby != "Roll#")
                    {
                        DataColumn newColumn = new DataColumn("Rank", typeof(string));
                        newColumn.DefaultValue = "-";
                        table.Columns.Add(newColumn);
                        int R = 0;
                        double rs = -1;
                        foreach (DataRow row in table.Rows)
                        {
                            try
                            {
                                if (rs != Convert.ToDouble(row["Average"]))
                                {
                                    R++;
                                }

                                row["Rank"] = R;
                                rs = Convert.ToDouble(row["Average"]);
                                if (rs == 0)
                                {
                                    break;
                                }
                            }
                            catch (Exception ex)
                            {

                            }
                        }
                    }
                }
            }
            return table;
        }
        public DataTable classresult(string orderby, int exam_id, int class_id, int section_id, bool ignore_absent)
        {
            string order = ""; int n = 5;
            if (orderby == "Roll#")
            {
                order = "order by `Roll` ";
            }

            if (orderby == "%Avg")
            {
                order = "order by `Average`DESC ,student.name ";
            }

            DataTable table = new DataTable();
            DataTable all_subjects_dt = new DataTable();
            if (section_id != 0)
            {
                all_subjects_dt = GetAllSubject_by_section_dt(section_id.ToString());
                string metricmarks = "";
                if (GetSettings("show_metric_number") == "1")
                {
                    metricmarks = "(SELECT IFNULL(sfv.value,'') AS val FROM student_fields_values AS sfv " +
                            " WHERE sfv.student_id = student.student_id and sfv.field_id = 2) as `9thMarks`,(SELECT IFNULL(sfv.value, '') AS val FROM student_fields_values AS sfv " +
                            " WHERE sfv.student_id = student.student_id and sfv.field_id = 3) as `10thMarks`,";
                }
                if (all_subjects_dt.Rows.Count > 0)
                {
                    var query = "SELECT student.student_id as ID,student.roll As Roll ,student.name AS Student ,parent.name AS Parent ,student.section_id as Section," + metricmarks + "";
                    foreach (DataRow dr in all_subjects_dt.Rows)
                    {
                        int total = GetSubjectTotalMarks(class_id, section_id, Convert.ToInt32(dr["subject_id"]), exam_id);
                        query += " IFNULL(MAX(CASE mark.subject_id WHEN '" + dr["subject_id"] + "' THEN (CASE mark_obtained WHEN '-1' THEN 'A' when '-2' then 'N/A' ELSE mark_obtained END) END),0) AS `" + dr["subject_code"] + "(" + total + ")" + "`,";
                    }
                    if (ignore_absent == false)
                    {
                        query += "sum(CASE when mark_obtained > 0 THEN mark_obtained ELSE '0' END) as Obtained,sum(tms.marks) as `Total`,round((sum(CASE WHEN mark_obtained > 0 THEN mark_obtained ELSE '0' END)/sum(tms.marks)*100 ),2)as `Average` ";
                    }
                    else
                    {
                        query += "sum(CASE when mark_obtained > 0 THEN mark_obtained ELSE '0' END) as Obtained,sum(CASE when mark_obtained > 0 THEN tms.marks ELSE '0' END) as `Total`,round((sum(CASE WHEN mark_obtained > 0 THEN mark_obtained ELSE '0' END)/sum(CASE when mark_obtained > 0 THEN tms.marks ELSE '0' END)*100 ),2)as `Average` ";
                    }
                    query += " FROM mark " +
                    " inner join student on(student.student_id = mark.student_id) and student.section_id = mark.section_id and  student.class_id = mark.class_id " +
                    " join parent on parent.parent_id=student.parent_id " +
                    " JOIN tbl_mark_subject AS tms ON tms.subject_id=mark.subject_id AND tms.exam_id=mark.exam_id  AND tms.`class_id` = mark.`class_id` AND tms.`section_id` = mark.`section_id` " +
                    " where student.passout = 0 AND student.class_id = '" + class_id + "'and student.section_id='" + section_id + "' and mark.exam_id='" + exam_id + "' AND mark.subject_id IN (SELECT subject_id FROM section_subject WHERE section_id = '" + section_id + "') " +
                    " GROUP BY mark.class_id,mark.section_id,mark.student_id  " + order + "";
                    query = "SELECT tbl.*, (SELECT grade.comment FROM grade WHERE grade.mark_from <= tbl.Average AND grade.mark_upto > tbl.Average LIMIT 1) AS Result FROM(" + query + ") AS tbl ";
                    table = FetchDataTable(query);
                    //table = CommonFunctions.AutoNumberedTable(table);
                    if (orderby != "Roll#")
                    {
                        n = 6;
                        DataColumn newColumn = new DataColumn("Rank", typeof(string));
                        newColumn.DefaultValue = "-";
                        table.Columns.Add(newColumn);
                        int R = 0;
                        double rs = -1;
                        foreach (DataRow row in table.Rows)
                        {
                            try
                            {
                                if (rs != Convert.ToDouble(row["Obtained"]))
                                {
                                    R++;
                                }

                                row["Rank"] = R;
                                rs = Convert.ToDouble(row["Obtained"]);
                                if (rs == 0)
                                {
                                    break;
                                }
                            }
                            catch (Exception ex)
                            {
                                MessageBox.Show("Student named = '" + row["Student"] + "' has obtained marks like " + row["Obtained"] + " Please set these marks", "");
                            }
                        }
                    }
                }
            }
            else
            {
                all_subjects_dt = GetAllSubject_by_class_dt(class_id.ToString());
                if (all_subjects_dt.Rows.Count > 0)
                {
                    var query = "SELECT student.student_id as ID,student.roll As Roll ,concat( student.name,' / ',parent.name) as Student,parent.phone as Phone,section.name as Section,";
                    foreach (DataRow dr in all_subjects_dt.Rows)
                    {
                        query += " concat(IFNULL(MAX(CASE mark.subject_id WHEN '" + dr["subject_id"] + "' THEN (CASE mark_obtained WHEN '-1' THEN 'A' when '-2' then 'N/A' ELSE mark_obtained END) END),0),'/',IFNULL(MAX(CASE mark.subject_id WHEN '" + dr["subject_id"] + "' THEN tms.`marks` END),0)) AS `" + dr["name"] + "`,";
                    }
                    //query += " IFNULL(MAX(CASE subject.subject_code WHEN '" + allClass[i].Name + "' THEN concat( (CASE WHEN mark_obtained = '-1' THEN 'A' ELSE mark_obtained END),'/',tbl_mark_subject.marks ) END),0) AS `" + allClass[i].Salary + "`,";
                    query += "sum(CASE WHEN mark_obtained > 0 THEN mark_obtained ELSE '0' END) as Obtained,sum(tms.marks) as `Total`,round((sum(CASE WHEN mark_obtained > 0 THEN mark_obtained ELSE '0' END)/sum(tms.marks)*100 ),2)as `Average` " +
                        "FROM mark " +
                        " inner join student on(student.student_id = mark.student_id) and student.section_id = mark.section_id and  student.class_id = mark.class_id " +
                        " join parent on parent.parent_id=student.parent_id " +
                        " JOIN tbl_mark_subject AS tms ON tms.subject_id=mark.subject_id AND tms.exam_id=mark.exam_id  AND tms.`class_id` = mark.`class_id` AND tms.`section_id` = mark.`section_id` " +
                        " join section on section.section_id=student.section_id "
                        + " where student.passout != 1 AND student.class_id = '" + class_id + "' and mark.exam_id='" + exam_id + "'" +
                                "GROUP BY mark.student_id " + order + "";
                    query = "SELECT tbl.*, (SELECT grade.comment FROM grade WHERE grade.mark_from <= tbl.Average AND grade.mark_upto > tbl.Average LIMIT 1) AS Result FROM(" + query + ") AS tbl ";
                    table = FetchDataTable(query);
                    table = CommonFunctions.AutoNumberedTable(table);
                    if (orderby != "Roll#")
                    {
                        n = 6;
                        DataColumn newColumn = new DataColumn("Rank", typeof(string));
                        newColumn.DefaultValue = "-";
                        table.Columns.Add(newColumn);
                        int R = 0;
                        double rs = -1;
                        foreach (DataRow row in table.Rows)
                        {
                            try
                            {
                                if (rs != Convert.ToDouble(row["Obtained"]))
                                {
                                    R++;
                                }

                                row["Rank"] = R;
                                rs = Convert.ToDouble(row["Obtained"]);
                                if (rs == 0)
                                {
                                    break;
                                }
                            }
                            catch (Exception ex)
                            {
                                MessageBox.Show("Student named = '" + row["Student"] + "' has obtained marks like " + row["Obtained"] + " Please set these marks", "");
                            }
                        }
                    }
                }

            }
            return table;
        }

        public DataTable multi_classresult(string orderby, string exam_id, string class_id, string section_id, bool ignore_absent)
        {
            string order = ""; int n = 5;
            if (orderby == "Roll#")
            {
                order = "order by `Roll` ";
            }

            if (orderby == "%Avg")
            {
                order = "order by `Average`DESC ,student.name ";
            }

            DataTable all_subjects_dt = GetAllSubject_by_section_dt(section_id);
            DataTable table = new DataTable();
            if (all_subjects_dt.Rows.Count > 0)
            {
                var query = "SELECT student.student_id as ID,student.roll As Roll ,concat( student.name,' / ',parent.name) as Student,parent.phone as Phone,section.name as Section,";
                foreach (DataRow dr in all_subjects_dt.Rows)
                {
                    query += " concat(IFNULL(SUM(CASE mark.subject_id WHEN '" + dr["subject_id"] + "' THEN (IF(mark_obtained > 0,mark_obtained,0)) END),0),'/',IFNULL(SUM(CASE mark.subject_id WHEN '" + dr["subject_id"] + "' THEN tms.`marks` END),0)) AS `" + dr["subject_code"] + "`,";
                }
                if (ignore_absent == false)
                {
                    query += "sum(CASE when mark_obtained > 0 THEN mark_obtained ELSE '0' END) as Obtained,sum(tms.marks) as `Total`,round((sum(CASE WHEN mark_obtained > 0 THEN mark_obtained ELSE '0' END)/sum(tms.marks)*100 ),2)as `Average` ";
                }
                else
                {
                    query += "sum(CASE when mark_obtained > 0 THEN mark_obtained ELSE '0' END) as Obtained,sum(CASE when mark_obtained > 0 THEN tms.marks ELSE '0' END) as `Total`,round((sum(CASE WHEN mark_obtained > 0 THEN mark_obtained ELSE '0' END)/sum(CASE when mark_obtained > 0 THEN tms.marks ELSE '0' END)*100 ),2)as `Average` ";
                }
                query += "FROM mark " +
                " inner join student on(student.student_id = mark.student_id) and student.class_id = mark.class_id and student.section_id = mark.section_id " +
                " join parent on parent.parent_id=student.parent_id " +
                " JOIN tbl_mark_subject AS tms ON tms.subject_id=mark.subject_id AND tms.exam_id=mark.exam_id  AND tms.`class_id` = mark.`class_id` AND tms.`section_id` = mark.`section_id` " +
                " join section on section.section_id=student.section_id "
                + " where student.passout != 1 AND mark.class_id = '" + class_id + "' and mark.section_id in (" + section_id + ") and mark.exam_id in(" + exam_id + ")" +
                        "GROUP BY mark.student_id " + order + "";
                query = "SELECT tbl.*, (SELECT grade.comment FROM grade WHERE grade.mark_from <= tbl.Average AND grade.mark_upto > tbl.Average LIMIT 1) AS Result FROM(" + query + ") AS tbl ";

                table = FetchDataTable(query);
                table = CommonFunctions.AutoNumberedTable(table);
                if (orderby != "Roll#")
                {
                    n = 6;
                    DataColumn newColumn = new DataColumn("Rank", typeof(string));
                    newColumn.DefaultValue = "-";
                    table.Columns.Add(newColumn);
                    int R = 0;
                    double rs = -1;
                    foreach (DataRow row in table.Rows)
                    {
                        try
                        {
                            if (rs != Convert.ToDouble(row["Obtained"]))
                            {
                                R++;
                            }

                            row["Rank"] = R;
                            rs = Convert.ToDouble(row["Obtained"]);
                            if (rs == 0)
                            {
                                break;
                            }
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show("Student named = '" + row["Student"] + "' has obtained marks like " + row["Obtained"] + " Please set these marks", "");
                        }
                    }
                }
            }
            return table;
        }
        public DataTable multi_classresult_send_mark_by_sms(string exam_id, string class_id, string section_id, string subject_ids, string orderby = "%Avg", bool current = false)
        {
            string order = "", where = "", mark_subject_join = "", mark_join = ""; int n = 5;
            if (current) {
                where = " AND student.class_id = '" + class_id + "' and student.section_id in (" + section_id + ")";
                mark_subject_join = " left JOIN tbl_mark_subject AS tms ON tms.subject_id=mark.subject_id AND tms.exam_id=mark.exam_id  AND tms.`class_id` = student.`class_id` AND tms.`section_id` = student.`section_id` ";
                mark_join = " inner join student on student.student_id = mark.student_id AND student.`class_id`= mark.`class_id` AND student.`section_id` = mark.`section_id` ";
            }
            else
            {
                where = " AND mark.class_id = '" + class_id + "' and mark.section_id in (" + section_id + ")";
                mark_subject_join = " left JOIN tbl_mark_subject AS tms ON tms.subject_id=mark.subject_id AND tms.exam_id=mark.exam_id  AND tms.`class_id` = mark.`class_id` AND tms.`section_id` = mark.`section_id` ";
                mark_join = " inner join student on (student.student_id = mark.student_id) ";
            }
            if (orderby == "Roll#")
            {
                order = " order by `Roll` ";
            }

            if (orderby == "%Avg")
            {
                order = " order by `Average`DESC ,student.name ";
            }

            DataTable all_subjects_dt = GetAllSubject_by_subject_ids(subject_ids);//GetAllSubject_by_section_dt(section_id);
            DataTable table = new DataTable();
            if (all_subjects_dt.Rows.Count > 0)
            {
                string query = "SELECT student.student_id,student.roll as Roll_Number, student.name AS student, student.phone AS student_phone, parent.name AS parent, parent.phone AS parent_phone,class.name as class,section.name as section,";
                foreach (DataRow dr in all_subjects_dt.Rows)
                {
                    query += " concat(IFNULL(SUM(CASE mark.subject_id WHEN '" + dr["subject_id"] + "' THEN mark_obtained END),0),'/',IFNULL(SUM(CASE tms.subject_id WHEN '" + dr["subject_id"] + "' THEN IF(mark_obtained < 0, 0, tms.`marks`) END),0)) AS `" + dr["name"] + "`,";
                }
                //query += " IFNULL(MAX(CASE subject.subject_code WHEN '" + allClass[i].Name + "' THEN concat( (CASE WHEN mark_obtained = '-1' THEN 'A' ELSE mark_obtained END),'/',tbl_mark_subject.marks ) END),0) AS `" + allClass[i].Salary + "`,";
                query += "sum(CASE WHEN mark_obtained > 0 THEN mark_obtained ELSE '0' END) as Obtained,sum(IF(mark_obtained < 0 , 0, tms.marks)) as `Total`,IF(mark_obtained < 0, 0, round((sum(CASE WHEN mark_obtained > 0 THEN mark_obtained ELSE '0'END)/sum(tms.marks)*100 ),2)) AS `Average` " +
                    " FROM mark " +
                    mark_join +
                    " join parent on parent.parent_id=student.parent_id " +
                    mark_subject_join +
                    " join class on class.class_id=student.class_id " +
                    " join section on section.section_id=student.section_id " +
                    " where student.passout != 1 and mark.exam_id in(" + exam_id + ") and mark.subject_id in (" + subject_ids + ")" + where +
                    " GROUP BY mark.student_id " + order + "";
                query = "SELECT tbl.*, (SELECT grade.comment FROM grade WHERE grade.mark_from <= tbl.Average AND grade.mark_upto > tbl.Average LIMIT 1) AS Result FROM(" + query + ") AS tbl ";
                table = FetchDataTable(query);
                table = CommonFunctions.AutoNumberedTable(table);
                if (orderby != "Roll#")
                {
                    n = 6;
                    DataColumn newColumn = new DataColumn("Rank", typeof(string));
                    newColumn.DefaultValue = "-";
                    table.Columns.Add(newColumn);
                    int R = 0;
                    double rs = -1;
                    foreach (DataRow row in table.Rows)
                    {
                        try
                        {
                            if (rs != Convert.ToDouble(row["Obtained"]))
                            {
                                R++;
                            }

                            row["Rank"] = R;
                            rs = Convert.ToDouble(row["Obtained"]);
                            if (rs == 0)
                            {
                                break;
                            }
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show("Student named = '" + row["Student"] + "' has obtained marks like " + row["Obtained"] + " Please set these marks", "");
                        }
                    }
                }
            }
            return table;
        }

        public int time(DateTime date)
        {
            var dateTime = new DateTime(date.Year, date.Month, date.Day, date.Hour, date.Minute, date.Second, DateTimeKind.Local);
            var epoch = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
            var unixDateTime = (dateTime.ToUniversalTime() - epoch).TotalSeconds;
            return Convert.ToInt32(unixDateTime);
        }

        public string GetTimestamp(DateTime value)
        {
            //Int32 unixTimestamp = (Int32)(DateTime.UtcNow.Subtract(value)).TotalSeconds;
            //return unixTimestamp;
            return value.ToString("yyyyMMdd");
        }
        public string CurrentDate(DateTime value)
        {
            return value.Month + "/" + value.Day + "/" + value.Year;
        }

        public void DateFormat(DevExpress.XtraEditors.DateEdit name)
        {
            name.Properties.DisplayFormat.FormatString = "M/d/yyyy";
            name.Properties.DisplayFormat.FormatType = FormatType.DateTime;
            name.Properties.EditFormat.FormatString = "M/d/yyyy";
            name.Properties.EditFormat.FormatType = FormatType.DateTime;
            name.EditValue = DateTime.Now;
            name.Properties.Mask.EditMask = "M/d/yyyy";
        }
        public void DateFormatOnlyYM(DevExpress.XtraEditors.DateEdit name)
        {
            name.Properties.DisplayFormat.FormatString = "MM-yyyy";
            name.Properties.DisplayFormat.FormatType = FormatType.DateTime;
            name.Properties.EditFormat.FormatString = "MM-yyyy";
            name.Properties.EditFormat.FormatType = FormatType.DateTime;
            name.EditValue = DateTime.Now;
            name.Properties.Mask.EditMask = "MM-yyyy";
        }

        public void DateFormatOnlyY(DevExpress.XtraEditors.DateEdit name)
        {
            name.Properties.DisplayFormat.FormatString = "yyyy";
            name.Properties.DisplayFormat.FormatType = FormatType.DateTime;
            name.Properties.EditFormat.FormatString = "yyyy";
            name.Properties.EditFormat.FormatType = FormatType.DateTime;
            name.EditValue = DateTime.Now;
            name.Properties.Mask.EditMask = "yyyy";
        }
        public Task ProcessDate(double marksend, double total, IProgress<ProgressReport> progress)
        {
            var progressReport = new ProgressReport();
            return Task.Run(() =>
            {
                progressReport.PercentComplete = Convert.ToInt32(marksend / total * 100); //index++ * 100 / totalProgress;
                progress.Report(progressReport);
                Thread.Sleep(5);
            });
        }

        public bool isAllow(string type, string key, string searchtype = "`key`")
        {
            string query = "SELECT * FROM tbl_role where permission_id='" + Login.CurrentUserStatus_id + "' and " + searchtype + " ='" + key + "'";
            DataTable perdt = FetchDataTable(query);
            bool value = false;

            if (perdt.Rows.Count > 0)
            {
                if (type == "Add")
                {
                    value = Convert.ToBoolean(perdt.Rows[0]["IsAdd"]);
                }
                else if (type == "Edit")
                {
                    value = Convert.ToBoolean(perdt.Rows[0]["IsEdit"]);
                }
                else if (type == "Delete")
                {
                    value = Convert.ToBoolean(perdt.Rows[0]["IsDelete"]);
                }
            }
            return value;
        }
        public DataTable FetchData_excel(string filename, string sql)
        {
            DataTable tbl = new DataTable();
            string ConStringOleDb = string.Format("Provider=Microsoft.ACE.OLEDB.12.0;Data Source={0};" + "Extended Properties=\"Excel 12.0 Xml;HDR=YES;IMEX=1\";", filename);
            OleDbConnection con = new OleDbConnection(ConStringOleDb);
            try
            {
                con.Open();
                OleDbCommand cmd = new OleDbCommand(sql, con);
                OleDbDataAdapter adp = new OleDbDataAdapter(cmd);
                adp.Fill(tbl);

            }
            catch (MySqlException ex)
            {
                throw;
            }
            catch (Exception e)
            {
                MessageBox.Show("Execute Query: " + e.Message);
            }
            finally
            {
                con.Close();
            }
            return tbl;
        }


        public void insert_subject_section(string cls_id, string sec_id, string sub_id, string tech_id, string worklaod, string subject_fee)
        {
            string query = "select * from section_subject as ss where ss.class_id ='" + cls_id + "' and ss.section_id = '" + sec_id + "' and subject_id ='" + sub_id + "'";
            DataTable dt_sub = FetchDataTable(query);
            if (dt_sub.Rows.Count <= 0)
            {
                query = "INSERT into section_subject (class_id,subject_id,section_id,work_load,teacher_id,subject_fee) VALUES " +
                    "('" + cls_id + "','" + sub_id + "','" + sec_id + "','" + worklaod + "','" + tech_id + "','" + subject_fee + "');";
                ExecuteQuery(query);
            }
        }

        public void exporttoexcel(GridControl grid)
        {
            using (SaveFileDialog sfd = new SaveFileDialog() { Filter = "Excel WorkBook|*.xlsx" })
            {
                if (sfd.ShowDialog() == DialogResult.OK)
                {
                    string path = sfd.FileName; //@"E:/newdata.xlsx";
                    grid.ExportToXlsx(path, new XlsxExportOptionsEx
                    {
                        AllowGrouping = DefaultBoolean.False,
                        AllowFixedColumnHeaderPanel = DefaultBoolean.False
                    });
                    System.Diagnostics.Process.Start(path);
                }
            }

        }
        public void ImportVisitFile(string filename, string SClass)
        {
            String query = "";
            var txt = "";
            tblImport impdata;
            var classID = GetClassIDisSession(SClass, GetDefaultSessionName());
            loaderform(() =>
            {

                ObservableCollection<tblImport> DataAdapter = new ObservableCollection<tblImport>();
                string ConStringOleDb = string.Format("Provider=Microsoft.ACE.OLEDB.12.0;Data Source={0};" + "Extended Properties=\"Excel 12.0 Xml;HDR=YES;IMEX=1\";", filename);
                DataAdapter.Clear();
                OleDbConnection xlConn = new OleDbConnection(ConStringOleDb);
                xlConn.Open();
                OleDbCommand selectCmd = new OleDbCommand("SELECT * FROM [Sheet1$]", xlConn);
                OleDbDataReader dr = selectCmd.ExecuteReader();
                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        var name = dr[0];
                        if (name.ToString().Trim() != "")
                        {
                            impdata = new tblImport()
                            {
                                Name = UppercaseFirst(Convert.ToString(dr[0]).Trim()),
                                PName = UppercaseFirst(Convert.ToString(dr[1]).Trim()),
                                Card = UppercaseFirst(Convert.ToString(dr[2]).Trim()),
                                Phone = UppercaseFirst(Convert.ToString(dr[3]).Trim()),
                                PPhone = UppercaseFirst(Convert.ToString(dr[4]).Trim()),
                                Address = UppercaseFirst(Convert.ToString(dr[5]).Trim()),
                                Sex = UppercaseFirst(Convert.ToString(dr[6]).Trim()),
                                Profession = UppercaseFirst(Convert.ToString(dr[7]).Trim()),
                                School = UppercaseFirst(Convert.ToString(dr[8]).Trim())
                            };
                            DataAdapter.Add(impdata);
                        }
                    }
                }
                xlConn.Close();

                for (int i = 0; i < DataAdapter.Count; i++)
                {
                    impdata = DataAdapter[i];
                    txt += (i - 1) + ":Student: " + DataAdapter[i].Name + "\n\r";
                    query = "INSERT INTO visitor SET NAME = '{0}',FName = '{1}',9thM = '{2}',Cell='{3}',Sms='{4}',Address ='{5}',sex='{6}',FatherOcc='{7}',School='{8}',ClassId='{9}' ";
                    query = String.Format(query, impdata.Name, impdata.PName, impdata.Card, impdata.Phone, impdata.PPhone, impdata.Address, impdata.Sex, impdata.Profession, impdata.School, classID);
                    ExecuteQuery(query);
                }
            });
            if (txt != "")
            {
                MessageBox.Show("Success: \n\r " + txt);
            }
        }
        public int upsert_School(string Name)
        {
            int id = 0;
            string query = "SELECT * FROM tbl_school WHERE name = '" + Name + "'";
            DataTable table = this.FetchDataTable(query);
            if (table.Rows.Count > 0)
            {
                id = Convert.ToInt32(table.Rows[0]["school_id"]);
            }
            else
            {
                query = "INSERT INTO tbl_school SET name = '" + Name + "'";
                id = Convert.ToInt32(this.Execute_Insert(query));
            }
            return id;
        }
        public DataTable Compare_FetchDataTable(string sql, string constring)
        {
            DataTable tbl = new DataTable();
            try
            {
                MySqlConnection con = new MySqlConnection(constring);
                con.Open();
                MySqlCommand cmd = new MySqlCommand(sql, con);
                MySqlDataAdapter adp = new MySqlDataAdapter(cmd);
                adp.Fill(tbl);
                con.Close();
            }
            catch (Exception e)
            {
                MessageBox.Show("Query:" + sql + "\n Execute Query: " + e.ToString());
            }
            return tbl;
        }
        public DataTable FetchDataTable_storeproc(string storeproc_name, Dictionary<string, string> param)
        {
            DataTable tbl = new DataTable();
            try
            {
                MySqlConnection con = new MySqlConnection(Login.constring);
                con.Open();
                MySqlCommand cmd = new MySqlCommand(storeproc_name, con);
                cmd.CommandType = CommandType.StoredProcedure;
                foreach (var item in param)
                {
                    cmd.Parameters.Add(new MySqlParameter(item.Value, item.Key));
                }
                MySqlDataAdapter adp = new MySqlDataAdapter(cmd);
                adp.Fill(tbl);
                con.Close();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return tbl;
        }
        public DataTable FetchDataTable(string sql, MySqlConnection con)
        {
            DataTable tbl = new DataTable();
            MySqlCommand cmd = new MySqlCommand(sql, con);
            MySqlDataAdapter adp = new MySqlDataAdapter(cmd);
            adp.Fill(tbl);
            return tbl;
        }
        public DataTable FetchDataTable_Datareader(string sql)
        {
            DataTable tbl = new DataTable();
            try
            {
                MySqlConnection con = new MySqlConnection(Login.constring);
                con.Open();
                MySqlCommand cmd1 = new MySqlCommand(sql, con);
                MySqlDataReader reader1 = cmd1.ExecuteReader();
                tbl.Load(reader1);
                con.Close();
            }
            catch (Exception ep)
            {
            }

            return tbl;
        }
        public DataTable FetchDataTable(string sql)
        {
            DataTable tbl = new DataTable();
            MySqlConnection con = new MySqlConnection(Login.constring);
            try
            {
                con.Open();
                MySqlCommand cmd = new MySqlCommand(sql, con);
                MySqlDataAdapter adp = new MySqlDataAdapter(cmd);
                adp.Fill(tbl);

            }
            catch (MySqlException ex)
            {
                MessageBox.Show(sql + ".   Execute Query: " + ex.Message);
            }
            catch (Exception e)
            {
                MessageBox.Show(sql + ".   Execute Query: " + e.Message);
            }
            finally
            {
                con.Close();
            }
            return tbl;
        }
        public long Execute_Insert(string sql, MySqlConnection con, MySqlTransaction tra)
        {
            long id = 0;
            MySqlCommand cmd = new MySqlCommand(sql, con, tra);
            cmd.ExecuteNonQuery();
            id = cmd.LastInsertedId;
            return id;
        }
        public long Execute_Insert(string sql)
        {
            long id = 0;
            MySqlConnection con = new MySqlConnection(Login.constring);
            try
            {
                con.Open();
                MySqlCommand cmd = new MySqlCommand(sql, con);
                cmd.ExecuteNonQuery();
                id = cmd.LastInsertedId;
            }
            catch (MySqlException ex)
            {
                MessageBox.Show("Execute Query: " + ex.Message);
            }
            catch (Exception e)
            {
                MessageBox.Show("Execute Query: " + e.Message);
            }
            finally
            {
                con.Close();
            }
            return id;
        }
        public object Execute_Scaler_string(string sql, MySqlConnection con)
        {
            object value = null;
            DataTable tbl = new DataTable();
            MySqlCommand cmd = new MySqlCommand(sql, con);
            cmd.ExecuteNonQuery();
            value = cmd.ExecuteScalar();
            return value;
        }
        public object Execute_Scaler_string(string sql)
        {
            object value = null;
            MySqlConnection con = new MySqlConnection(Login.constring);
            try
            {
                con.Open();
                MySqlCommand cmd = new MySqlCommand(sql, con);
                cmd.ExecuteNonQuery();
                value = cmd.ExecuteScalar();
            }
            catch (MySqlException ex)
            {
                MessageBox.Show("Execute Query: " + ex.Message);
            }
            catch (Exception e)
            {
                MessageBox.Show("Execute Query: " + e.Message);
            }
            finally
            {
                con.Close();
            }
            return value;
        }

        public string UppercaseFirst(string s)
        {
            // Check for empty string.
            if (string.IsNullOrEmpty(s))
            {
                return string.Empty;
            }
            // Return char and concat substring.
            return char.ToUpper(s[0]) + s.Substring(1);
        }

        public void SendSMSFeePaid(string stdid, string amount, string total)
        {
            loaderform(() =>
            {

                MySqlConnection con = new MySqlConnection(Login.constring);
                MySqlConnection con1 = new MySqlConnection(Login.constring);
                var SMS = GetSetting("fees_sms");
                if (SMS[0].Name == "1")
                {
                    string phone = "";
                    con.Open();
                    MySqlCommand cmd1 = new MySqlCommand("SELECT student.name AS student, parent.name AS parent, student.phone AS sphone, parent.phone AS pphone "
                        + " FROM student LEFT JOIN parent ON parent.parent_id = student.parent_id WHERE student.student_id = '" + stdid + "'", con);
                    MySqlDataReader reader1 = cmd1.ExecuteReader();
                    if (reader1.HasRows)
                    {
                        while (reader1.Read())
                        {
                            phone = (reader1["pphone"].ToString() != "") ? reader1["pphone"].ToString() : reader1["sphone"].ToString();
                            allClass.Clear();
                            var mark = GetSetting("paid_fees_sms");
                            string mark_template = mark[0].Name;
                            var a = mark_template.Trim().Replace("[parent]", reader1["parent"].ToString())
                                    .Replace("[student]", reader1["student"].ToString())
                                    .Replace("[amount]", amount)
                                    .Replace("[total]", total)
                                    .Replace("[date]", DateTime.Now.ToString());
                            con1.Open();
                            MySqlCommand cmd = new MySqlCommand("INSERT into sms_que(mobile,sms,status) VALUES('" + phone + "','" + a + "','" + 0 + "');", con1);
                            cmd.ExecuteNonQuery();
                            con1.Close();

                        }
                    }
                    con.Close();
                }
            });
        }

        public void SendSMSFeeUNPaid(int stdid, string total, string date, string detail)
        {
            MySqlConnection con = new MySqlConnection(Login.constring);
            MySqlConnection con1 = new MySqlConnection(Login.constring);

            allClass.Clear();
            allClass = GetSetting("fees_sms");
            if (allClass[0].Name == "1")
            {
                string phone = "";
                con.Open();
                MySqlCommand cmd1 = new MySqlCommand("SELECT student.name AS student, parent.name AS parent, student.phone AS sphone, parent.phone AS pphone "
                    + " FROM student LEFT JOIN parent ON parent.parent_id = student.parent_id WHERE student.student_id = '" + stdid + "'", con);
                MySqlDataReader reader1 = cmd1.ExecuteReader();
                if (reader1.HasRows)
                {
                    while (reader1.Read())
                    {
                        phone = (reader1["pphone"].ToString() != "") ? reader1["pphone"].ToString() : reader1["sphone"].ToString();
                        allClass.Clear();
                        allClass = GetSetting("unpaid_fees_sms");
                        string mark_template = allClass[0].Name;
                        var a = mark_template.Trim().Replace("[parent]", reader1["parent"].ToString())
                                .Replace("[student]", reader1["student"].ToString())
                                .Replace("[date]", date)
                                .Replace("[detail]", detail)
                                .Replace("[amount]", total);
                        con1.Open();
                        MySqlCommand cmd = new MySqlCommand("INSERT into sms_que(mobile,sms,status) VALUES('" + phone + "','" + a + "','" + 0 + "');", con1);
                        cmd.ExecuteNonQuery();
                        con1.Close();
                    }
                }
                con.Close();
            }
        }
        public static DataTable AutoNumberedTable(DataTable SourceTable)
        {
            DataTable ResultTable = new DataTable();
            if (SourceTable.Rows.Count > 0)
            {
                DataColumn AutoNumberColumn = new DataColumn();

                AutoNumberColumn.ColumnName = "Sr#";

                AutoNumberColumn.DataType = typeof(int);

                AutoNumberColumn.AutoIncrement = true;

                AutoNumberColumn.AutoIncrementSeed = 1;

                AutoNumberColumn.AutoIncrementStep = 1;

                ResultTable.Columns.Add(AutoNumberColumn);

                ResultTable.Merge(SourceTable);
            }
            else
            {
                ResultTable = SourceTable;
            }

            return ResultTable;

        }

        public string GetPermissionID(string name, string app_type)
        {
            string id = "";
            MySqlConnection con = new MySqlConnection(Login.constring);
            con.Open();
            MySqlCommand cmd = new MySqlCommand("SELECT * from tbl_permission Where title='" + name.Trim() + "' and app_type='" + app_type + "'", con);
            MySqlDataReader reader1 = cmd.ExecuteReader();
            if (reader1.HasRows)
            {
                while (reader1.Read())
                {
                    id = reader1["permission_id"].ToString();
                }
            }
            con.Close();
            return id;
        }
        public DataTable all_users_status(string app_type)
        {
            string query = "SELECT * from tbl_permission where app_type='" + app_type + "' ";
            return FetchDataTable(query);
        }
        public ObservableCollection<AllValues> GetAllPermissionType(string app_type)
        {
            allValue = new ObservableCollection<AllValues>();
            MySqlConnection con = new MySqlConnection(Login.constring);
            con.Open();
            MySqlCommand cmd = new MySqlCommand("SELECT * from tbl_permission where app_type='" + app_type + "' ", con);
            MySqlDataReader reader1 = cmd.ExecuteReader();
            if (reader1.HasRows)
            {
                while (reader1.Read())
                {
                    AllValues d = new AllValues
                    {
                        ID = int.Parse(reader1["permission_id"].ToString()),
                        Name = reader1["title"].ToString()
                    };
                    allValue.Add(d);
                }
            }
            con.Close();
            return allValue;
        }
        public ObservableCollection<AllValues> GetAllBellTimeSlate()
        {
            allValue = new ObservableCollection<AllValues>();
            MySqlConnection con = new MySqlConnection(Login.constring);
            con.Open();
            MySqlCommand cmd = new MySqlCommand("SELECT max(time_slate) as ts FROM tbl_bell group by time_slate", con);
            MySqlDataReader reader1 = cmd.ExecuteReader();
            if (reader1.HasRows)
            {
                while (reader1.Read())
                {
                    AllValues d = new AllValues
                    {
                        Name = reader1["ts"].ToString()
                    };
                    allValue.Add(d);
                }
            }
            con.Close();
            return allValue;
        }
        #region  Student Report Card
        private string Zone(decimal percent)
        {
            string zoneval = "";
            if (percent >= 94)
            {
                zoneval = "Champion";
            }
            else if (percent > 90 && percent <= 94)
            {
                zoneval = "Victor";
            }
            else if (percent > 85 && percent <= 90)
            {
                zoneval = "Warrior";
            }
            else if (percent > 75 && percent <= 85)
            {
                zoneval = "Green";
            }
            else if (percent > 70 && percent <= 75)
            {
                zoneval = "Blue";
            }
            else if (percent > 65 && percent <= 70)
            {
                zoneval = "Yellow";
            }
            else if (percent <= 64)
            {
                zoneval = "Red";
            }

            return zoneval;
        }
        private static bool IsValidHexString(IEnumerable<char> hexString)
        {
            return hexString.Select(currentCharacter =>
                        (currentCharacter >= '0' && currentCharacter <= '9') ||
                        (currentCharacter >= 'a' && currentCharacter <= 'f') ||
                        (currentCharacter >= 'A' && currentCharacter <= 'F')).All(isHexCharacter => isHexCharacter);
        }

        private DataTable ResultTable;
        private string stdName;
        private ObservableCollection<AllSubject> allResultSub;
        private DateTime min = DateTime.Now, max = DateTime.Now;
        public int GetStudentSectionId(int studentId)
        {
            int sectionId = -1; // Initialize with a default value (e.g., -1 to indicate not found)
            string query = "SELECT section_id FROM student WHERE student_id = @studentId";

            using (MySqlConnection con = new MySqlConnection(Login.constring))
            {
                MySqlCommand cmd = new MySqlCommand(query, con);
                cmd.Parameters.AddWithValue("@studentId", studentId); // Parameterized query to avoid SQL injection

                con.Open();
                MySqlDataReader reader = cmd.ExecuteReader();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        sectionId = Convert.ToInt32(reader["section_id"]); // Retrieve the section_id
                    }
                }

                con.Close();
            }

            return sectionId; // Return the section_id (or -1 if not found)
        }

        public XtraStdReport std_result_card(int std_id, DateTime start, DateTime end, bool IgnoreAbsent, bool rank, GridControl resultControl, GridView resultView, GridControl AttndControl, GridView AttndgridView, int[] exam_array, bool showattendance, bool is_multi_student, bool show_section)
        {
            GridControl resultControlnew = new GridControl();
            GridControl attendControlnew = new GridControl();
            var queryExamList = "";
            var queryGrandAge = "";
            var where_exam = "";
            string exam_date_range = "";
            if (exam_array.Length > 0)
            {
                where_exam = " AND exam.exam_id IN(" + String.Join(",", exam_array) + ") ";
                queryExamList = "SELECT * FROM exam WHERE 1 = 1 AND exam_id IN(" + String.Join(",", exam_array) + ") order by date ";
                queryGrandAge = "and exam.exam_id IN(" + String.Join(",", exam_array) + ") ";
            }
            else
            {
                exam_date_range = " and exam.date between '" + start.ToString("yyyy-MM-dd") + "' and '" + end.ToString("yyyy-MM-dd") + "'";
                queryExamList = "SELECT * FROM exam where 1=1 " + exam_date_range + " order by date;";
                queryGrandAge = "and exam.date>='" + start.ToString("yyyy-MM-dd") + "' and exam.date<='" + end.ToString("yyyy-MM-dd") + "'";
            }
            DataTable readerM = FetchDataTable(queryExamList);
            List<string> MList = new List<string>();
            if (readerM.Rows.Count > 0)
            {
                DateTime mcount = new DateTime();
                foreach (DataRow dr in readerM.Rows)
                {
                    var d = dr["date"].ToString();
                    DateTime dateM = Convert.ToDateTime(d);
                    if (!(dateM.Month == mcount.Month && dateM.Year == mcount.Year))
                    {
                        MList.Add(dateM.ToShortDateString());
                        mcount = dateM;
                    }
                }
                try
                {
                    allResultSub = new ObservableCollection<AllSubject>();
                    ResultTable = new DataTable();
                    var info = GetStudentInfo(std_id);
                    int classID = int.Parse(info.Split('>')[1]);
                    int sectionID = int.Parse(info.Split('>')[2]);
                    stdName = info.Split('>')[0];
                    // stdID = int.Parse(searchLookUpEdit1.Text);
                    resultControl.DataSource = null;
                    resultView.Columns.Clear();
                    MySqlConnection con = new MySqlConnection(Login.constring);
                    DataTable sec_subjects = GetAllSubject_by_section_dt(sectionID.ToString());
                    con.Open();
                    if (sec_subjects.Rows.Count > 0)
                    {
                        string col = "";
                        string col3 = "";
                        if (show_section)
                        {
                            col = "sec.name as section,";
                            col3 = "'' as section ,";
                        }
                        if (IgnoreAbsent == false)
                        {
                            var query = "SELECT " + col + "std.roll as Roll, exam.name as `Examination`,DATE_FORMAT(exam.date,'%Y-%m-%d') as Exam_Date,DATE_FORMAT(exam.date,'%Y-%m') as Month,";
                            var query3 = "SELECT " + col3 + "'' as Roll, 'Average' as `Examination`,'" + DateTime.Now.ToString("yyyy-MM-dd") + "' as Exam_Date ,'" + DateTime.Now.ToString("yyyy-MM") + "' as Month,";
                            for (int i = 0; i < sec_subjects.Rows.Count; i++)
                            {
                                query += " IFNULL(MAX(CONCAT(CASE m.subject_id WHEN '" + sec_subjects.Rows[i]["subject_id"] + "' THEN (CASE mark_obtained WHEN '-1' THEN 'A' when '-2' then 'N/A' ELSE mark_obtained END) END,' / ',CASE m.subject_id WHEN '" + sec_subjects.Rows[i]["subject_id"] + "' THEN tms.marks end)),'TNC') AS `" + sec_subjects.Rows[i]["subject_code"] + "`,";
                                query3 += " IFNULL(CONVERT(round(sum(CASE m.subject_id WHEN '" + sec_subjects.Rows[i]["subject_id"] + "' THEN (CASE mark_obtained WHEN '-1' THEN 'A' when '-2' then 'N/A' ELSE mark_obtained END) END)/sum(CASE m.subject_id WHEN '" + sec_subjects.Rows[i]["subject_id"] + "' THEN tms.marks END)*100,2), char(45)),'TNC') as `" + sec_subjects.Rows[i]["subject_code"] + "`,";
                            }
                            query += "CONCAT(CONVERT(sum(IF(mark_obtained < 0, 0, mark_obtained)),char(45)),'/',CONVERT(SUM(tms.`marks`),char(45)) )as `Total`,round((sum(IF(mark_obtained = '-1' OR mark_obtained = '-2', 0, mark_obtained))/SUM(tms.`marks`)*100 ),2)as `%`, if(round((sum(mark_obtained)/SUM(tms.`marks`)*100 ),2)<=(SELECT mark_upto FROM grade where name='F'),'Fail','Pass') as  Result,'' as Zone,'' as `R-S`,'' as `R-C` " +
                                " FROM mark as m " +
                                " inner join student as std on std.student_id = m.student_id " +
                                " inner join exam on exam.exam_id=m.exam_id " +
                                " INNER JOIN section AS sec ON sec.`section_id` = m.`section_id` " +
                                " INNER JOIN tbl_mark_subject AS tms ON tms.`class_id` = m.`class_id` AND tms.`section_id` = m.`section_id` AND tms.`subject_id` = m.`subject_id` AND tms.`exam_id` = m.`exam_id`  "
                              + "where std.student_id='" + std_id + "'  " + exam_date_range + where_exam +
                                      " GROUP BY m.student_id ,m.section_id ,exam.exam_id ";
                            query3 += " '' as Total,'' AS `%`,'' AS Result,'' as Zone,'' as `R-S`,'' as `R-C` " +
                                " FROM mark as m " +
                                " inner join student as std on std.student_id = m.student_id " +
                                " inner join exam on exam.exam_id=m.exam_id " +
                                " INNER JOIN tbl_mark_subject AS tms ON tms.`class_id` = m.`class_id` AND tms.`section_id` = m.`section_id` AND tms.`subject_id` = m.`subject_id` AND tms.`exam_id` = m.`exam_id`  "
                              + " where std.student_id='" + std_id + "' " + exam_date_range + where_exam + " order by `Exam_Date`";
                            query += " union all " + query3;
                            DataTable table = FetchDataTable(query);

                            decimal obtain = 0;
                            decimal total = 0;
                            decimal percent = 0;
                            for (int row = 0; row <= table.Rows.Count - 1; row++)
                            {
                                string[] totalmarks = table.Rows[row]["Total"].ToString().Split('/');
                                if (totalmarks.Length == 2 && table.Rows[row]["Examination"].ToString() != "Average")
                                {
                                    try
                                    {
                                        total = total + Convert.ToDecimal(totalmarks[1].Trim());
                                        obtain = obtain + Convert.ToDecimal(totalmarks[0].Trim());
                                    }
                                    catch (Exception ex)
                                    {
                                        MessageBox.Show("Please Set marks in Exam" + table.Rows[row]["Examination"].ToString() + "where marks are shwing like obtain/total =" + totalmarks[0].Trim() + "/" + totalmarks[1].Trim(), "Marks ishue", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                    }
                                }
                                else
                                {
                                    table.Rows[row]["Total"] = string.Concat(obtain.ToString(), "/", total.ToString());
                                    if (total > 0)
                                    {
                                        percent = Math.Round((obtain / total * 100), 2);
                                    }
                                    else
                                    {
                                        percent = 0;
                                    }

                                    table.Rows[row]["%"] = percent;
                                    obtain = 0;
                                    total = 0;
                                }
                                table.Rows[row]["Zone"] = Zone(Convert.ToDecimal(string.IsNullOrEmpty(table.Rows[row]["%"].ToString()) ? "0" : table.Rows[row]["%"]));
                            }
                            ResultTable = table;

                            var query5 = "SELECT " + col3 + "'' as Roll, 'Grand Average' as `Examination`,'" + DateTime.Now.ToString("yyyy-MM-dd") + "' as Exam_Date ,'" + end.ToString("yyyy-MM") + "' as Month,";
                            for (int i = 0; i < sec_subjects.Rows.Count; i++)
                            {
                                query5 += "CONVERT(round(sum(CASE m.subject_id WHEN '" + sec_subjects.Rows[i]["subject_id"] + "' THEN IF(mark_obtained < 0, 0, mark_obtained) END)/sum(CASE m.subject_id WHEN '" + sec_subjects.Rows[i]["subject_id"] + "' THEN tms.marks END)*100,2), char(45)) as `" + sec_subjects.Rows[i]["subject_code"] + "`,";
                            }

                            query5 += "'' AS Total,CONVERT(round(sum(CASE mark_obtained WHEN '-1' THEN 'A' when '-2' then 'N/A' ELSE mark_obtained END)/ (sum(tms.marks))*100,2), CHAR(45))as `%`,'' AS Result,'' as `R-S`,'' as `R-C` FROM mark as m " +
                                        " inner join student as std on std.student_id = m.student_id " +
                                        " inner join exam on exam.exam_id=m.exam_id " +
                                        " INNER JOIN tbl_mark_subject AS tms ON tms.`class_id` = m.`class_id` AND tms.`section_id` = m.`section_id` AND tms.`subject_id` = m.`subject_id` AND tms.`exam_id` = m.`exam_id`   " +
                                        " where std.student_id='" + std_id + "' " + queryGrandAge + " ";
                            MySqlCommand cmdT = new MySqlCommand(query5, con);
                            MySqlDataAdapter adpT = new MySqlDataAdapter(cmdT);
                            DataTable tableT = new DataTable();

                            adpT.Fill(tableT);
                            ResultTable.Merge(tableT);
                            var rowsToUpdate = ResultTable.AsEnumerable().Where(r => r.Field<string>("Exam_Date") == DateTime.Now.ToString("yyyy-MM-dd"));
                            foreach (var rows in rowsToUpdate)
                            {
                                //rows.SetField("Exam_Date", "");
                            }

                        }
                        else
                        {
                            DataTable tableT = new DataTable();
                            var query = "SELECT " + col + "std.roll as Roll, exam.name as `Examination`,DATE_FORMAT(exam.date,'%Y-%m-%d') as Exam_Date,DATE_FORMAT(exam.date,'%Y-%m') as Month,";
                            var query3 = "SELECT " + col3 + "'' as Roll, 'Average' as `Examination`,'" + DateTime.Now.ToString("yyyy-MM-dd") + "' as Exam_Date ,'" + DateTime.Now.ToString("yyyy-MM") + "' as Month,";
                            for (int i = 0; i < sec_subjects.Rows.Count; i++)
                            {
                                query += " IFNULL(MAX(CONCAT(CASE m.subject_id WHEN '" + sec_subjects.Rows[i]["subject_id"] + "' THEN (CASE mark_obtained WHEN '-1' THEN 'A' when '-2' then 'N/A' ELSE mark_obtained END) END,' / ',CASE m.subject_id WHEN '" + sec_subjects.Rows[i]["subject_id"] + "' THEN tms.marks end)),(select if(tms.test_not_deliver = 1,'TND','TNC'))) AS `" + sec_subjects.Rows[i]["subject_code"] + "`,";
                                query3 += "CONVERT(round(sum(CASE m.subject_id WHEN '" + sec_subjects.Rows[i]["subject_id"] + "' THEN IF(mark_obtained < 0, 0, mark_obtained) END)/sum(CASE  WHEN m.subject_id='" + sec_subjects.Rows[i]["subject_id"] + "' AND mark_obtained > 0  THEN marks ELSE 0 END)*100,2), char(45)) as `" + sec_subjects.Rows[i]["subject_code"] + "`,";
                            }
                            query += "CONCAT(CONVERT(sum(CASE mark_obtained WHEN '-1' THEN 'A' when '-2' then 'N/A' ELSE mark_obtained END),char(45)),'/',CONVERT(sum(if(mark_obtained < 0,0, marks)),char(45)) )as `Total`,round((sum(IF(mark_obtained < 0, 0, mark_obtained))/(sum(if(mark_obtained < 0,0, marks)))*100 ),2)as `%`, if(round((sum(mark_obtained)/SUM(tms.`marks`)*100 ),2)<=(SELECT mark_upto FROM grade where name='F'),'Fail','Pass') as  Result,'' as Zone,'' as `R-S`,'' as `R-C` " +
                                " FROM mark as m " +
                                " inner join student as std on std.student_id = m.student_id " +
                                " inner join exam on exam.exam_id=m.exam_id " +
                                " INNER JOIN tbl_mark_subject AS tms ON tms.`class_id` = m.`class_id` AND tms.`section_id` = m.`section_id` AND tms.`subject_id` = m.`subject_id` AND tms.`exam_id` = m.`exam_id`  " +
                                " INNER JOIN section AS sec ON sec.`section_id` = m.`section_id` " +
                                "where std.student_id='" + std_id + "' " + exam_date_range + where_exam +
                                      "GROUP BY m.student_id, m.section_id ,exam.exam_id  ";
                            query3 += "''as Total,'' AS `%`,'' AS Result,'' as Zone,'' as `R-S`,'' as `R-C` " +
                                " FROM mark as m " +
                                " inner join student as std on std.student_id = m.student_id " +
                                " inner join exam on exam.exam_id=m.exam_id " +
                                " INNER JOIN tbl_mark_subject AS tms ON tms.`class_id` = m.`class_id` AND tms.`section_id` = m.`section_id` AND tms.`subject_id` = m.`subject_id` AND tms.`exam_id` = m.`exam_id`  " +
                                " where std.student_id='" + std_id + "' " + exam_date_range + where_exam + " order by `Exam_Date`";
                            query += " union all " + query3;
                            DataTable table = FetchDataTable(query);
                            decimal obtain = 0;
                            decimal total = 0;
                            decimal percent = 0;
                            for (int row = 0; row <= table.Rows.Count - 1; row++)
                            {
                                string[] totalmarks = table.Rows[row]["Total"].ToString().Split('/');
                                if (totalmarks.Length == 2 && table.Rows[row]["Examination"].ToString() != "Average")
                                {
                                    try
                                    {
                                        obtain = obtain + Convert.ToDecimal(totalmarks[0].Trim());
                                        total = total + Convert.ToDecimal(totalmarks[1].Trim());
                                    }
                                    catch (Exception ex)
                                    {
                                        MessageBox.Show("Please Set marks in Exam" + table.Rows[0]["Examination"].ToString() + "where marks are shwing like obtain/total =" + totalmarks[0].Trim() + "/" + totalmarks[1].Trim(), "Marks ishue", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                    }
                                }
                                else
                                {
                                    table.Rows[row]["Total"] = string.Concat(obtain.ToString(), "/", total.ToString());
                                    if (obtain > 0)
                                    {
                                        percent = Math.Round((obtain / total * 100), 2);
                                    }
                                    else
                                    {
                                        percent = 0;
                                    }

                                    table.Rows[row]["%"] = percent;
                                    obtain = 0;
                                    total = 0;
                                }
                                table.Rows[row]["Zone"] = Zone(Convert.ToDecimal(string.IsNullOrEmpty(table.Rows[row]["%"].ToString()) ? "0" : table.Rows[row]["%"]));
                            }
                            ResultTable = table;//ResultTable.Merge(table);
                            var query5 = "SELECT " + col3 + "'' as Roll, 'Grand Average' as `Examination`,'" + DateTime.Now.ToString("yyyy-MM-dd") + "' as Exam_Date ,'" + end.ToString("yyyy-MM") + "' as Month,";
                            for (int i = 0; i < sec_subjects.Rows.Count; i++)
                            {
                                query5 += "CONVERT(round(sum(CASE m.subject_id WHEN '" + sec_subjects.Rows[i]["subject_id"] + "' THEN IF(mark_obtained < 0, 0, mark_obtained) END)/sum(CASE  WHEN m.subject_id='" + sec_subjects.Rows[i]["subject_id"] + "' AND mark_obtained > 0  THEN marks ELSE 0 END)*100,2), char(45)) as `" + sec_subjects.Rows[i]["subject_code"] + "`,";
                            }

                            query5 += "'' AS Total,CONVERT(round(sum(IF(mark_obtained < 0, 0, mark_obtained))/ (sum(if(mark_obtained < 0,0, marks)))*100,2), CHAR(45))as `%`,'' AS Result,'' as `R-S`,'' as `R-C` FROM mark as m " +
                                " inner join student as std on std.student_id = m.student_id " +
                                " inner join exam on exam.exam_id=m.exam_id " +
                                " INNER JOIN tbl_mark_subject AS tms ON tms.`class_id` = m.`class_id` AND tms.`section_id` = m.`section_id` AND tms.`subject_id` = m.`subject_id` AND tms.`exam_id` = m.`exam_id`  " +
                                " where std.student_id='" + std_id + "' " + queryGrandAge + " ";
                            MySqlCommand cmdT = new MySqlCommand(query5, con);
                            MySqlDataAdapter adpT = new MySqlDataAdapter(cmdT);

                            adpT.Fill(tableT);
                            ResultTable.Merge(tableT);
                            var rowsToUpdate = ResultTable.AsEnumerable().Where(r => r.Field<string>("Exam_Date") == DateTime.Now.ToString("yyyy-MM-dd"));
                            foreach (var rows in rowsToUpdate)
                            {
                                rows.SetField("Exam_Date", "");
                            }
                        }
                        //resultView.Columns.Clear();
                        //resultControl.DataSource = null;
                        DataTable dtCloned = ResultTable.Clone();
                        dtCloned.Columns["Exam_Date"].DataType = typeof(string);

                        foreach (DataRow row in ResultTable.Rows)
                        {
                            //if (string.IsNullOrEmpty(row["Exam_Date"].ToString()))
                            //row["Exam_Date"] = DateTime.Now.ToString("yyyy-MM-dd");
                            dtCloned.ImportRow(row);
                        }
                        if (rank == true)
                        {
                            for (int i = 0; i < dtCloned.Rows.Count; i++)
                            {
                                var s = dtCloned.Rows[i]["Examination"].ToString();
                                if (s != "Grand Average" && s != "Average")
                                {
                                    var exam_id = GetExamID(s);
                                    dtCloned.Rows[i]["R-S"] = exam_rank(exam_id, classID, sectionID, std_id);
                                    dtCloned.Rows[i]["R-C"] = exam_rank(exam_id, classID, 0, std_id);
                                }
                            }
                        }
                        resultControl.BeginUpdate();
                        resultControlnew.BeginUpdate();
                        try
                        {
                            resultView.Columns.Clear();
                            resultControl.DataSource = null;
                            resultControl.DataSource = dtCloned;

                            resultControlnew.DataSource = null;
                            resultControlnew.DataSource = dtCloned;
                        }
                        finally
                        {
                            resultControl.EndUpdate();
                            resultControlnew.EndUpdate();
                        }
                        //resultControl.DataSource = dtCloned;
                        //resultControlnew.DataSource = dtCloned;
                        resultView.GroupRowHeight = 25;
                        //resultView.Appearance.Row.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
                        var col2 = resultView.Columns["Month"];
                        col2.Group();
                        resultView.ExpandAllGroups();
                        resultView.BestFitColumns();
                        if (is_multi_student)
                        {
                            resultView.Columns["Examination"].Width = 245;
                        }

                        var col1 = resultView.Columns["Roll"];
                        col1.Visible = false;
                        var col_3 = resultView.Columns["Result"];
                        col_3.Visible = false;
                        resultView.Columns["Zone"].Visible = false;
                        for (int i = 0; i <= (sec_subjects.Rows.Count + 3); i++)
                        {
                            GridColumn Column = resultView.Columns[i + 2];
                            Column.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
                        }
                        if (rank == true)
                        {
                            GridColumn Column1 = resultView.Columns["R-C"];
                            Column1.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
                            GridColumn Column2 = resultView.Columns["R-S"];
                            Column2.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
                        }
                        else
                        {
                            var col5 = resultView.Columns["R-C"];
                            col5.Visible = false;
                            var col6 = resultView.Columns["R-S"];
                            col6.Visible = false;
                        }
                        resultControl.LookAndFeel.Style = DevExpress.LookAndFeel.LookAndFeelStyle.Flat;
                        resultControl.LookAndFeel.UseDefaultLookAndFeel = false;


                        resultView.AppearancePrint.HeaderPanel.Options.UseBackColor = true;
                        resultView.AppearancePrint.HeaderPanel.BackColor = Color.White;
                        resultView.AppearancePrint.GroupRow.Options.UseBackColor = true;
                        resultView.AppearancePrint.GroupRow.BackColor = Color.LightGray;
                        resultView.AppearancePrint.GroupRow.ForeColor = Color.Black;
                        resultView.AppearancePrint.GroupRow.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
                        var col4 = resultView.Columns["%"];
                        col2.Caption = "";
                        col4.Caption = "%Age";

                    }

                    con.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Please Chack your exam marks", "info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            /*else
            {
                MessageBox.Show("No Exam Found in this Date Range", "Info", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }*/


            Image logo = Base64ToImage(Login.Logo);
            var school = GetSettings("system_title");
            XtraStdReport report = new XtraStdReport();
            report.LabTitle.Text = school;
            report.labName.Text = stdName;
            DataTable std = GetStudentInfo_n(std_id);
            if (std.Rows.Count > 0)
            {
                report.PicStdBox.Image = get_image(@"\Images\Students\", std_id.ToString() + "_std", true, std.Rows[0]["Gender"].ToString());
                report.LabFName.Text = std.Rows[0]["FatherName"].ToString();
                report.LabClass.Text = std.Rows[0]["Class"].ToString();
                report.LabSection.Text = std.Rows[0]["Section"].ToString();
                report.LabSAddress.Text = std.Rows[0]["Address"].ToString();
                report.labRoll.Text = std.Rows[0]["Roll"].ToString();
                report.labStudentId.Text = std_id.ToString();
                //report.LabPhone.Text = std[1];
                report.LabDob.Text = std.Rows[0]["Birthday"].ToString();
                report.LabGender.Text = std.Rows[0]["Gender"].ToString();
                report.LabCell.Text = std.Rows[0]["Phone"].ToString() + "-" + std.Rows[0]["Phone"].ToString();
                var res = std.Rows[0]["School"].ToString() == "-" || std.Rows[0]["School"].ToString() == "" ? "0" : std.Rows[0]["School"].ToString();
                report.LabPSchool.Text = res;
                /*if (std.Rows[0]["Name"].ToString() != "-")
                {
                    report.labFStudy.Visible = true;
                    report.labFStudy.Text = "Mr.\\Ms. " + stdName + " continue higher study at " + std[15];
                }*/
            }
            string principal = GetSettings("principal");
            string exam_controller = GetSettings("controller_exam");
            report.LabPrincipal.Text = "( " + principal + " )";
            report.exam_controler.Text = "( " + exam_controller + " )";
            report.PicIogoBox.Image = logo;
            if (!string.IsNullOrEmpty(principal))
            {
                report.picPrincipal_Sign.Image = Base64ToImage(Login.Principal_Sign);
                report.picPrincipal_Sign.Visible = true;
                report.LabPrincipal.Visible = true;
                report.xrLine2.Visible = true;
                report.xrLabel19.Visible = true;
            }
            if (!string.IsNullOrEmpty(exam_controller))
            {
                report.PicExam_Sign.Image = Base64ToImage(Login.Exam_Sign);
                report.PicExam_Sign.Visible = true;
                report.xrlblExams.Visible = true;
                report.LabController2.Visible = true;
                report.xrLine1.Visible = true;
                report.exam_controler.Visible = true;
            }

            //Student Extra Field manegement 
            String FieldQuery = "SELECT sf.title, IFNULL(sfv.value,'') AS val FROM student_fields AS sf " +
                                "LEFT JOIN student_fields_values AS sfv ON sfv.field_id = sf.field_id AND sfv.student_id = '{0}'  " +
                                "WHERE sf.is_sheet = 1  " +
                                "ORDER BY sf.field_id ASC  " +
                                "LIMIT 3 ";
            FieldQuery = String.Format(FieldQuery, std_id);
            DataTable FieldTable = FetchDataTable(FieldQuery);
            int field_count = 1;
            foreach (DataRow row in FieldTable.Rows)
            {
                if (field_count == 1)
                {
                    report.FieldLabel1.Text = row["title"].ToString();
                    report.FieldValue1.Text = row["val"].ToString();
                    report.FieldLabel1.Visible = true;
                    report.FieldValue1.Visible = true;
                }
                else if (field_count == 2)
                {
                    report.FieldLabel2.Text = row["title"].ToString();
                    report.FieldValue2.Text = row["val"].ToString();
                    report.FieldLabel2.Visible = true;
                    report.FieldValue2.Visible = true;
                }
                else
                {
                    report.FieldLabel3.Text = row["title"].ToString();
                    report.FieldValue3.Text = row["val"].ToString();
                    report.FieldLabel3.Visible = true;
                    report.FieldValue3.Visible = true;
                }
                field_count++;
            }


            resultControlnew.MainView = resultView;
            report.GridControl = resultControlnew;
            report.xrlblExams.Visible = true;
            report.GridControl.Width = 100;
            report.LabAddress.Text = GetSettings("address");
            report.LabTel.Text = GetSettings("phone");
            report.LabEDate.Text = min.ToString("dd-MM-yyyy");

            var vp = GetSettings("vice_d/n");
            if (vp != "")
            {
                report.picVice_Sign.Visible = true;
                if (Login.Vice_Principal_Sign != "")
                {
                    report.picVice_Sign.Image = Base64ToImage(Login.Vice_Principal_Sign);
                }

                report.labVL.Visible = true;
                report.LabViceD.Visible = true;
                report.LabViceD.Text = vp.Split('/')[0];
                report.LabViceN.Visible = true;
                report.LabViceN.Text = vp.Split('/').Length > 1 ? vp.Split('/')[1] : "";
            }

            DataTable table2 = new DataTable("Table1");
            table2.Columns.Add("Argument", typeof(Int32));
            table2.Columns.Add("Value", typeof(Int32));
            Random rnd = new Random();
            DataRow row1 = null;
            int j = 1;
            int GAvg = 0;
            if (ResultTable != null)
            {
                for (int i = 0; i < ResultTable.Rows.Count; i++)
                {
                    if (ResultTable.Rows[i]["%"].ToString() != "")
                    {
                        var t = ResultTable.Rows[i]["%"];
                        decimal s = Convert.ToDecimal(t.ToString().Replace('"', '0'));
                        int val = Convert.ToInt32(s);
                        if (i + 1 != ResultTable.Rows.Count)
                        {
                            row1 = table2.NewRow();
                            row1["Argument"] = j;
                            row1["Value"] = val;
                            table2.Rows.Add(row1);
                        }
                        else
                        {
                            GAvg = val;
                        }
                        j++;
                    }
                }
            }
            DataTable table3 = new DataTable("Table1");
            table3.Columns.Add("Argument", typeof(Int32));
            table3.Columns.Add("Value", typeof(Int32));
            row1 = table3.NewRow();
            row1["Argument"] = 1;
            row1["Value"] = GAvg;
            table3.Rows.Add(row1);

            // report.xrChart1.DataSource = table;
            Series series = new Series("Series1", ViewType.Line);
            report.xrChart1.Series.Add(series);
            series.DataSource = table2;
            series.ArgumentScaleType = ScaleType.Numerical;
            series.ArgumentDataMember = "Argument";
            series.ValueScaleType = ScaleType.Numerical;
            series.ValueDataMembers.AddRange(new string[] { "Value" });
            Series series2 = new Series("Series2", ViewType.Bar);
            report.xrChart1.Series.Add(series2);
            series2.DataSource = table3;
            series2.View.Color = Color.OrangeRed;
            series2.ArgumentScaleType = ScaleType.Numerical;
            series2.ArgumentDataMember = "Argument";
            series2.ValueScaleType = ScaleType.Numerical;
            series2.ValueDataMembers.AddRange(new string[] { "Value" });


            // Set some properties to get a nice-looking chart.
            // ((SideBySideBarSeriesView)series.View).ColorEach = true;
            ((XYDiagram)report.xrChart1.Diagram).AxisY.Visibility = DevExpress.Utils.DefaultBoolean.False;
            report.xrChart1.Legend.Visibility = DevExpress.Utils.DefaultBoolean.False;

            if (showattendance)
            {

                attendControlnew = new GridControl();
                int maxday = 0;
                DateTime startDate, endDate;
                IDictionary<string, int> res_array;
                DataTable AttndTable = new DataTable();
                AttndTable.Columns.Add("Month");
                AttndTable.Columns.Add("Present");
                AttndTable.Columns.Add("Absent");
                AttndTable.Columns.Add("OffDay");
                AttndTable.Columns.Add("Leave");
                AttndTable.Columns.Add("LateMin");
                AttndTable.Columns.Add("Days");
                DataRow dtrow;
                DateTime mystart = start;
                //Start from First attendance
                DateTime FirstDate = start;
                var queryDate = "SELECT `date` FROM attendance WHERE student_id='" + std_id + "' ORDER BY attendance_id ASC LIMIT 1";
                DataTable StartTable = FetchDataTable(queryDate);
                if (StartTable.Rows.Count > 0)
                {
                    FirstDate = Convert.ToDateTime(StartTable.Rows[0]["date"].ToString());
                    if ((start.Year < FirstDate.Year) || (start.Year == FirstDate.Year && start.Month <= FirstDate.Month))
                    {
                        start = FirstDate;
                        mystart = FirstDate;
                    }
                }
                while (start <= end || (start.Month == end.Month && start.Year == end.Year))
                {
                    maxday = DateTime.DaysInMonth(start.Year, start.Month);
                    startDate = new DateTime(start.Year, start.Month, 1);
                    endDate = new DateTime(start.Year, start.Month, maxday);
                    if (startDate.Month == FirstDate.Month && startDate.Year == FirstDate.Year)
                    {
                        startDate = FirstDate;
                    }
                    res_array = attendance_summary(std_id, startDate, endDate);
                    dtrow = AttndTable.NewRow();
                    dtrow["Month"] = start.ToString("MMM") + "-" + start.Year;
                    dtrow["Present"] = res_array["present"];
                    if (!IgnoreAbsent)
                    {
                        dtrow["Absent"] = res_array["absent"];
                    }
                    else
                    {
                        dtrow["Absent"] = "-";
                    }

                    dtrow["OffDay"] = res_array["sunday"] + res_array["holidays"];
                    dtrow["Leave"] = res_array["leave"] + res_array["sleave"];
                    dtrow["LateMin"] = res_array["late_min"];
                    dtrow["Days"] = res_array["total"];
                    AttndTable.Rows.Add(dtrow);
                    start = start.AddMonths(1);
                }

                //SUM total
                maxday = DateTime.DaysInMonth(end.Year, end.Month);
                startDate = new DateTime(mystart.Year, mystart.Month, 1);
                if (startDate.Month == FirstDate.Month && startDate.Year == FirstDate.Year)
                {
                    startDate = FirstDate;
                }
                endDate = new DateTime(end.Year, end.Month, maxday);
                res_array = attendance_summary(std_id, startDate, endDate);
                dtrow = AttndTable.NewRow();
                dtrow["Month"] = "Total";
                dtrow["Present"] = res_array["present"];
                if (!IgnoreAbsent)
                {
                    dtrow["Absent"] = res_array["absent"];
                }
                else
                {
                    dtrow["Absent"] = "-";
                }

                dtrow["OffDay"] = res_array["sunday"] + res_array["holidays"];
                dtrow["Leave"] = res_array["leave"] + res_array["sleave"];
                dtrow["LateMin"] = res_array["late_min"];
                dtrow["Days"] = res_array["total"];
                AttndTable.Rows.Add(dtrow);

                Series series1 = new Series("Attendance Chart", ViewType.Pie);
                series1.Points.Add(new SeriesPoint("Present", res_array["present"]));
                series1.Points.Add(new SeriesPoint("Absent", res_array["absent"]));
                series1.Points.Add(new SeriesPoint("OffDays", res_array["sunday"] + res_array["holidays"]));
                series1.Points.Add(new SeriesPoint("Leave", res_array["leave"] + res_array["sleave"]));
                report.PieGridControl = DrawPieChart(series1);


                AttndControl.DataSource = null;
                AttndgridView.Columns.Clear();
                AttndControl.DataSource = CommonFunctions.AutoNumberedTable(AttndTable);
                attendControlnew.DataSource = CommonFunctions.AutoNumberedTable(AttndTable);
                //AttndgridView.BestFitColumns();
                AttndgridView.OptionsPrint.AutoWidth = false;
                foreach (GridColumn col in AttndgridView.Columns)
                {

                    col.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
                    col.AppearanceCell.Font = new Font("Tahoma", 9f, FontStyle.Regular);
                    if (col.FieldName == "Month")
                    {
                        col.Width = 75;
                    }
                    else
                    {
                        col.Width = 55;
                    }
                }
                attendControlnew.MainView = AttndgridView;
                report.AtndGridControl = attendControlnew;
                report.xrlblAttendence.Visible = true;
            }
            return report;
        }

        public ChartControl DrawPieChart(Series series1)
        {
            // Create an empty chart.
            ChartControl pieChart = new ChartControl();
            // Create a pie series.

            // Add the series to the chart.
            pieChart.Series.Add(series1);

            // Format the the series labels.
            series1.Label.TextPattern = "{A}: {VP:p0}";

            // Adjust the position of series labels. 
            ((PieSeriesLabel)series1.Label).Position = PieSeriesLabelPosition.TwoColumns;

            // Detect overlapping of series labels.
            ((PieSeriesLabel)series1.Label).ResolveOverlappingMode = ResolveOverlappingMode.Default;

            // Access the view-type-specific options of the series.
            PieSeriesView myView = (PieSeriesView)series1.View;

            // Show a title for the series.
            myView.Titles.Add(new SeriesTitle());
            myView.Titles[0].Text = series1.Name;


            //myView.RuntimeExploding = true;
            myView.HeightToWidthRatio = 1;
            //pieChart.Width = 200; 
            // Hide the legend (if necessary).
            pieChart.Legend.Visibility = DevExpress.Utils.DefaultBoolean.True;
            // Add the chart to the form.
            //pieChart.Dock = DockStyle.Right;
            return pieChart;
            //this.Controls.Add(pieChart);
        }

        private int exam_rank(int exam_id, int class_id, int section_id, int std_id)
        {
            var qry = "";
            int rank = 0;
            if (section_id != 0)
            {
                qry = " student.section_id = '" + section_id + "'and";
            }

            MySqlConnection con = new MySqlConnection(Login.constring);
            var query = "SELECT student.student_id as ID,sum(CASE WHEN mark_obtained = '-1' THEN '0' ELSE mark_obtained END) as Obtained "
                       + " FROM mark inner join student on(student.student_id = mark.student_id)  where "
                       + qry + " student.class_id = '" + class_id + "' and mark.exam_id = '" + exam_id + "' "// And mark.student_id = '" + std_id + "' "
                       + "GROUP BY mark.student_id, mark.class_id  order by `Obtained` DESC ,student.name; ";

            MySqlCommand cmd = new MySqlCommand(query, con);
            MySqlDataAdapter adp = new MySqlDataAdapter(cmd);
            DataTable table = new DataTable();
            adp.Fill(table);
            int R = 0;
            int rs = 0;

            foreach (DataRow row in table.Rows)
            {
                if (-1 != Convert.ToInt32(row["Obtained"]) && -2 != Convert.ToInt32(row["Obtained"]))
                {
                    R++;
                }

                rank = R;
                rs = Convert.ToInt32(row["Obtained"]);
                if (Convert.ToInt32(row["ID"]) == std_id)
                {
                    break;
                }
            }
            return rank;
        }
        public IDictionary<string, int> attendance_summary(int student_id, DateTime min, DateTime max)
        {
            MySqlConnection con = new MySqlConnection(Login.constring);
            IDictionary<string, int> res_array = new Dictionary<string, int>();
            int section_id = GetStudentSectionId(student_id);
            List<DateTime> dateHolidays = new List<DateTime>();
            dateHolidays = GetAllHolidaysBetween(min, max, section_id);
            int sun = CountSundaysBetween(min, max);
            var queryAtt = "";
            String where = "";
            int total = 0;

            int days = 1 + int.Parse((max - min).TotalDays.ToString().Split('.')[0]);
            if (max.Month == DateTime.Now.Month && max.Year == DateTime.Now.Year && max.Day > DateTime.Now.Day)
            {
                int remaining = DateTime.Now.AddDays(1 - DateTime.Now.Day).AddMonths(1).AddDays(-1).Day - DateTime.Now.Day;
                days = days - remaining;
            }
            queryAtt = " ";

            total = days - sun - dateHolidays.Count;
            String holiday_str = this.GetExludeDates(min, max);
            if (holiday_str != "")
            {
                where += " AND NOT attendance.date IN(" + holiday_str + ")";
            }

            if (Convert.ToInt32(student_id) > 10000) //teacher
            {
                queryAtt = "SELECT  SUM(IF(attendance.status='1',1,0)) AS 'Present',SUM(IF(attendance.status='2',1,0)) AS 'Leave', " +
                            " SUM(IF(attendance.status = '3', 1, 0)) AS 'SLeave'," +
                            " IFNULL(SUM(TIME_TO_SEC(IF(TIMEDIFF(attendance.time_in, attendance.setting_in) > 0,TIMEDIFF(attendance.time_in, attendance.setting_in),0))),0) AS late_sec" +
                            " FROM teacher " +
                            "LEFT JOIN attendance ON attendance.student_id = teacher.teacher_id AND attendance.date >= '" + min.ToString("yyyy-MM-dd") + "'  AND attendance.date <= '" + max.ToString("yyyy-MM-dd") + "' " +
                            "WHERE attendance.student_id = '" + student_id + "' " + where;
            }
            else
            {
                queryAtt = "SELECT  SUM(IF(attendance.status='1',1,0)) AS 'Present',SUM(IF(attendance.status='2',1,0)) AS 'Leave', " +
                            "SUM(IF(attendance.status = '3', 1, 0)) AS 'SLeave', 0 AS late_sec" +
                            "  FROM student " +
                            "LEFT JOIN attendance ON attendance.student_id = student.student_id AND attendance.date >= '" + min.ToString("yyyy-MM-dd") + "'  AND attendance.date <= '" + max.ToString("yyyy-MM-dd") + "' " +
                            "WHERE attendance.student_id = '" + student_id + "' " + where;
            }
            con.Open();
            MySqlCommand cmdAtt = new MySqlCommand(queryAtt, con);
            MySqlDataReader readerAtt = cmdAtt.ExecuteReader();
            if (readerAtt.HasRows)
            {
                readerAtt.Read();
                int lat_min = Convert.ToInt32(Convert.ToInt32(readerAtt["late_sec"]) / 60);
                res_array["present"] = (readerAtt["Present"].ToString() == "") ? 0 : Convert.ToInt32(readerAtt["Present"]);// Convert.ToInt32(ss);
                res_array["leave"] = (readerAtt["Leave"].ToString() == "") ? 0 : Convert.ToInt32(readerAtt["Leave"]);
                res_array["sleave"] = (readerAtt["SLeave"].ToString() == "") ? 0 : Convert.ToInt32(readerAtt["SLeave"]);
                res_array["absent"] = total - res_array["present"] - res_array["leave"];
                res_array["present"] = res_array["present"] + res_array["sleave"];
                res_array["sunday"] = sun;
                res_array["working"] = total;
                res_array["holidays"] = dateHolidays.Count;
                res_array["late_min"] = lat_min > 0 ? lat_min : 0;
                res_array["total"] = days;
            }
            con.Close();
            return res_array;

        }
        public IDictionary<string, int> lecture_summary(int teacher_id, DateTime min, DateTime max)
        {
            IDictionary<string, int> res_array = new Dictionary<string, int>();
            res_array["missed_lecture"] = 0;
            res_array["extra_lecture"] = 0;
            res_array["total_lecture"] = 0;
            String sql = "SELECT IFNULL(SUM(IF(teacher_id = '{0}', 1,0)),0) AS missed,  IFNULL(SUM(IF(extra_teacher_id = '{0}', 1,0)),0) AS extra," +
                "IFNULL((SELECT COUNT(*) FROM floor_sheet WHERE teacher_id = '{0}' AND `date` BETWEEN '{1}' AND '{2}'),0) AS lecture " +
                            " FROM extra_lecture " +
                            " WHERE(teacher_id = '{0}' OR extra_teacher_id = '{0}') AND(`date` BETWEEN '{1}' AND '{2}') ";
            sql = String.Format(sql, teacher_id, min.ToString("yyyy-MM-dd"), max.ToString("yyyy-MM-dd"));
            DataTable table = this.FetchDataTable(sql);
            if (table.Rows.Count > 0)
            {
                res_array["missed_lecture"] = Convert.ToInt32(table.Rows[0]["missed"].ToString());
                res_array["extra_lecture"] = Convert.ToInt32(table.Rows[0]["extra"].ToString());
                res_array["total_lecture"] = Convert.ToInt32(table.Rows[0]["lecture"].ToString());
            }
            return res_array;
        }
        public void Execute_Query(string sql)
        {
            int id = 0;
            MySqlConnection con = new MySqlConnection(Login.constring);
            try
            {
                con.Open();
                MySqlCommand cmd = new MySqlCommand(sql, con);
                cmd.ExecuteNonQuery();
            }
            catch (MySqlException ex)
            {
                MessageBox.Show("Execute Query: " + ex.Message);
            }
            catch (Exception e)
            {
                MessageBox.Show("Execute Query: " + e.Message);
            }
            finally
            {
                con.Close();
            }
        }
        public void ExecuteQuery(string query)
        {
            try
            {
                MySqlConnection con = new MySqlConnection(Login.constring);
                con.Open();
                MySqlCommand cmd = new MySqlCommand(query, con);
                cmd.ExecuteNonQuery();
                con.Close();
            }
            catch (MySqlException mse)
            {
                if (mse.Number == 1449) // The user specified as a definer ('tnsbay'@'%') does not exist
                {
                    MessageBox.Show("Please change definer of your views and store Procedure in MYSQL DataBase", "Definer Ishue", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else if (mse.Number == 1292) // Date Format Error
                {
                    MessageBox.Show("DateTime Formate Error", "DateTime", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    MessageBox.Show(mse.Message, "MySqlException", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Query_Exception", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        public string GetSlotData(string class_id, string section_id, string day, int slot_id)
        {
            string res = "-";
            string check_query = "SELECT time_table.id,time_table.subject_code,teacher.name,time_table.room FROM time_table " +
                "INNER JOIN teacher ON teacher.teacher_id = time_table.teacher_id " +
                "WHERE class_id = '" + class_id + "' AND section_id = '" + section_id + "' AND `day` = '" + day + "' AND slot_id = '" + slot_id + "'";
            DataTable CheckData = this.GetQueryTable(check_query);
            if (CheckData.Rows.Count > 0)
            {
                res = CheckData.Rows[0]["id"] + ">" + CheckData.Rows[0]["subject_code"] + "-R" + CheckData.Rows[0]["room"] + " \n" + CheckData.Rows[0]["name"];
            }
            return res;
        }
        public string GetSlotDataAbsent(string class_id, string section_id, string day, int slot_id, string date)
        {
            //> present, : absent, < assign
            string res = "-";
            string seprater = ">";
            string teacher = "";
            string check_query = "SELECT time_table.id,time_table.subject_code,teacher.name,time_table.room,IFNULL(attendance.attendance_id,0) AS attendance_id, IFNULL(el.teacher_id,0) AS assign_id,IF(el.teacher_id > 0,(SELECT t2.name FROM teacher AS t2 WHERE t2.teacher_id = el.teacher_id),'') AS assign " +
                                    " FROM time_table " +
                                    " INNER JOIN teacher ON teacher.teacher_id = time_table.teacher_id " +
                                    " LEFT JOIN attendance ON attendance.student_id = teacher.teacher_id AND attendance.date = '{4}' " +
                                    " LEFT JOIN extra_lecture AS el ON el.class_id = time_table.class_id  AND el.section_id = time_table.section_id AND el.date = '{4}' AND el.slate_id = time_table.slot_id  " +
                                    " WHERE time_table.class_id = '{0}' AND time_table.section_id = '{1}' AND time_table.day = '{2}' AND time_table.slot_id = '{3}'";
            check_query = string.Format(check_query, class_id, section_id, day, slot_id, date);
            DataTable CheckData = this.GetQueryTable(check_query);

            if (CheckData.Rows.Count > 0)
            {
                teacher = CheckData.Rows[0]["name"].ToString();
                if (Convert.ToUInt32(CheckData.Rows[0]["attendance_id"].ToString()) == 0)
                { //Absent
                    if (Convert.ToUInt32(CheckData.Rows[0]["assign_id"].ToString()) > 0) //Assign
                    {
                        seprater = "<";
                        teacher = CheckData.Rows[0]["assign"].ToString();
                    }
                    else
                    {
                        seprater = ":";
                    }
                }
                res = CheckData.Rows[0]["id"] + seprater + CheckData.Rows[0]["subject_code"] + "-R" + CheckData.Rows[0]["room"] + " \n" + teacher;
            }
            return res;
        }

        public int[] GetTeacherAbsentArray(String date)
        {
            string sql = "SELECT teacher.teacher_id, attendance.* FROM teacher  " +
                        " LEFT JOIN attendance ON teacher.teacher_id = attendance.student_id AND `date` = '{0}'" +
                        " WHERE attendance.date IS NULL";
            sql = string.Format(sql, date);
            DataTable table = FetchDataTable(sql);
            int[] absent_array = new int[table.Rows.Count];

            for (int i = 0; i < table.Rows.Count; i++)
            {
                absent_array[i] = Convert.ToInt32(table.Rows[i]["teacher_id"].ToString());
            }
            return absent_array;
        }
        public int ExecuteInsert(string query)
        {
            query = query + ";select last_insert_id();";
            MySqlConnection con = new MySqlConnection(Login.constring);
            con.Open();
            MySqlCommand cmd = new MySqlCommand(query, con);
            int id = Convert.ToInt32(cmd.ExecuteScalar());
            con.Close();
            return id;
        }
        public int time()
        {
            var dateTime = DateTime.Now;
            var epoch = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
            var unixDateTime = (dateTime.ToUniversalTime() - epoch).TotalSeconds;
            return Convert.ToInt32(unixDateTime);
        }
        public DataTable cashasset_list()
        {
            string query = "SELECT cashasset_id,title,open_balance FROM cash_assets where `disable` = 0 ORDER BY title DESC";
            DataTable table = this.FetchDataTable(query);
            return table;
        }
        public DataTable ActivityCategory_list()
        {
            string query = "SELECT activity_cat_id AS id,title FROM activity_category WHERE disable = 0";
            DataTable table = this.FetchDataTable(query);
            return table;
        }
        public int GetMonth(string objMonth)
        {
            string m = "0";
            foreach (MonthOfYear month in Enum.GetValues(typeof(MonthOfYear)))
            {
                if (objMonth.ToString() == month.ToString())
                {
                    m = month.GetHashCode().ToString();
                    break;
                }
            }
            return Convert.ToInt32(m);
        }
        public DateTime GetDate(string objYear, string objMonth)
        {
            var m = "";
            foreach (MonthOfYear month in Enum.GetValues(typeof(MonthOfYear)))
            {
                if (objMonth.ToString() == month.ToString())
                {
                    m = month.GetHashCode().ToString();
                    break;
                }
            }
            string date = objYear.ToString() + "-" + m + "-1";
            DateTime Date = Convert.ToDateTime(date);
            return Date;
        }
        public string CurrentDate()
        {
            DateTime dateTime = DateTime.Now;
            return dateTime.ToString("yyyy-MM-dd");
        }
        public string ToInDate(String date)
        {
            try
            {
                date = date != "" ? Convert.ToDateTime(date).ToString("yyyy-MM-dd") : "";
            }
            catch (Exception)
            {
            }
            return date;
        }
        public Boolean CheckPermission(string name)
        {
            Boolean flag = false;
            String query = "SELECT * FROM tbl_role WHERE value = 1 AND `key` = '{0}' AND permission_id = '{1}' ";
            query = String.Format(query, name, Login.CurrentUserStatus_id);
            DataTable table = FetchDataTable(query);
            if (table.Rows.Count > 0)
            {
                flag = true;
            }
            return flag;
        }
        #endregion

        public Boolean ChangeSection(String student_id, String old_section_id, String new_section_id, DataTable tablesub)
        {
            String myquery;
            Boolean res = false;
            try
            {

                myquery = "UPDATE student SET section_id = '{0}', class_id = (select class_id from section where section_id = '{0}') WHERE student_id = '{1}';";

                myquery = String.Format(myquery, new_section_id, student_id);
                this.ExecuteQuery(myquery);
                res = true;

            }
            catch (Exception ep)
            {
                MessageBox.Show(ep.ToString());
            }
            return res;
        }
        public void saveImage(string name, OpenFileDialog fileOpen, DevExpress.XtraEditors.PictureEdit picBoxStudent, string imgpath)
        {
            string appPath = Path.GetDirectoryName(Application.ExecutablePath) + imgpath; // <---
            string filename = "";
            if (Directory.Exists(appPath) == false)                                              // <---
            {                                                                                    // <---
                Directory.CreateDirectory(appPath);                                              // <---
            }
            // if (fileOpen.ShowDialog() == DialogResult.OK)
            string iName = fileOpen.SafeFileName;
            if (iName != "")
            {
                try
                {
                    var a = name + ".Jpeg";
                    string filepath = fileOpen.FileName;

                    File.Copy(filepath, appPath + a, true);
                }
                catch (Exception exp)
                {
                    MessageBox.Show("Unable to open file " + exp.Message);
                }
            }
            else
            {
                filename = appPath + name + ".Jpeg";
                Image image = picBoxStudent.Image;
                FileStream fstream = new FileStream(filename, FileMode.Create);
                image.Save(fstream, System.Drawing.Imaging.ImageFormat.Jpeg);
                fstream.Close();
            }
            fileOpen.Dispose();
        }
        [DllImport("wininet.dll")]
        private static extern bool InternetGetConnectedState(out int Description, int ReservedVal);
        public bool IsConnectedToInternet()
        {
            int Desc;
            return InternetGetConnectedState(out Desc, 0);
            /*string host = "https://www.google.com/";  
            bool result = false;  
            Ping p = new Ping();  
            try  
            {  
            PingReply reply = p.Send(host, 3000);  
            if (reply.Status == IPStatus.Success)  
            return true;  
            }  
            catch { }  
            return result; */
        }

        public JObject get_data(string url)
        {
            JObject json = new JObject();
            if (!IsConnectedToInternet())
            {
                json["my_msg"] = "Please chack your internet Connection";
            }
            else
            {
                try
                {
                    ServicePointManager.Expect100Continue = true;
                    ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
                    ServicePointManager.ServerCertificateValidationCallback = delegate { return true; };
                    var client = new RestClient(url);
                    client.AddDefaultHeader("Content-Type", "application/json");
                    client.Timeout = -1;
                    var request = new RestRequest(Method.GET);
                    IRestResponse response = client.Execute(request);
                    json = JObject.Parse(response.Content);
                    json["my_msg"] = "OK";
                }
                catch (Exception ex)
                {
                    //MessageBox.Show(ex.Message, "API Error", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    json["my_msg"] = ex.Message;//"Please chack your internet Connection";
                }
            }
            return json;
        }
        private bool is_server = false;
        public bool is_server_pc()
        {
            is_server = true;
            string path = Directory.GetCurrentDirectory();
            FileInfo f = new FileInfo(path);
            string drive = Path.GetPathRoot(f.FullName);
            if (drive.Contains(':'))
            {
                is_server = true;
            }

            return is_server;
        }
        public string network_server_name()
        {
            string path = Directory.GetCurrentDirectory();
            FileInfo f = new FileInfo(path);
            string drive = Path.GetPathRoot(f.FullName);

            return drive;
        }
        public JObject post_data(string url, DataTable data)
        {
            JObject json = new JObject();
            try
            {
                ServicePointManager.Expect100Continue = true;
                ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
                ServicePointManager.ServerCertificateValidationCallback = delegate { return true; };
                var client = new RestClient(url);
                client.AddDefaultHeader("Content-Type", "application/json");
                client.Timeout = -1;
                var request = new RestRequest(Method.POST);
                request.AddHeader("Cookie", "ci_session=2jd4n1ohubikbvtmfkicl9i340lbtp15");
                string name = "";
                string value = "";
                foreach (DataRow dr in data.Rows)
                {
                    name = dr["name"].ToString();
                    value = dr["data"].ToString();
                    if (dr["type"].ToString() == "file")
                    {
                        request.AddFile(name, value);
                    }

                    if (dr["type"].ToString() == "data")
                    {
                        request.AddParameter(name, value);
                    }
                }
                IRestResponse response = client.Execute(request);
                json = JObject.Parse(response.Content);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "API Error", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            return json;
        }
        public JObject uploading_file_in_server(string url, string code, string file_name, string file_path)
        {
            JObject json = new JObject();
            if (!IsConnectedToInternet())
            {
                json["my_msg"] = "Please chack your internet Connection";
            }
            else
            {
                try
                {
                    ServicePointManager.Expect100Continue = true;
                    ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
                    ServicePointManager.ServerCertificateValidationCallback = delegate { return true; };
                    var client = new RestClient(url);
                    client.Timeout = -1;
                    var request = new RestRequest(Method.POST);
                    request.AddHeader("Cookie", "ci_session=2jd4n1ohubikbvtmfkicl9i340lbtp15");
                    //"/E:/Practice/StudentManagementSGC - With Class Library/SchoolManagementSystem/bin/Debug/noticeboardfile/test_db.zip"
                    request.AddFile(file_name, file_path);
                    IRestResponse response = client.Execute(request);
                    json["my_msg"] = "OK";
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "API Error", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            return json;
        }

        public string UniqueMachineID(string salt = "")
        {
            //CMD -> diskpart -> select disk 0 -> detail disk
            string output = "";
            //check into dabatase

            output = this.GetSettings("machine_id");
            if (output == "")
            {
                string path = Directory.GetCurrentDirectory();
                FileInfo f = new FileInfo(path);
                string actual_drive = Path.GetPathRoot(f.FullName);
                var drives = new ManagementObjectSearcher("Select * from Win32_LogicalDiskToPartition").Get().Cast<ManagementObject>();
                foreach (var drive in drives)
                {
                    var driveLetter = Regex.Match((string)drive["Dependent"], @"DeviceID=""(.*)""").Groups[1].Value;
                    var driveNumber = Regex.Match((string)drive["Antecedent"], @"Disk #(\d*),").Groups[1].Value;
                    //var primary_partition = Regex.Match((string)drive["PrimaryPartition"], @"Disk #(\d*),").Groups[1].Value;
                    bool hasmatched1 = (driveLetter + @"\" == actual_drive) ? true : false;
                    if (hasmatched1)
                    {
                        ManagementObjectSearcher win32DiskDrives = new ManagementObjectSearcher("select * from Win32_DiskDrive where Index = " + driveNumber + "");
                        foreach (ManagementObject win32DiskDrive in win32DiskDrives.Get())
                        {
                            PropertyDataCollection prop = win32DiskDrive.Properties;
                            output = string.Format("{0:X}", prop["Signature"].Value);//win32DiskDrive.Properties["Signature"].Value
                            break;
                        }
                    }
                    if (output != "")
                    {
                        break;
                    }
                }

                if (output != "")
                {
                    string query = "INSERT INTO `settings`(type,description) VALUES ('machine_id', '" + output + "')";
                    this.Execute_Insert(query);
                }

            }

            return output;
        }
        public string Client_Unique_NO(int index = 0)
        {

            string output = "";
            ManagementObjectSearcher win32DiskDrives = new ManagementObjectSearcher("select * from Win32_DiskDrive where Index = " + index + "");
            foreach (ManagementObject win32DiskDrive in win32DiskDrives.Get())
            {
                output = string.Format("{0:X}", win32DiskDrive.Properties["Signature"].Value);
                break;
            }
            return output;
        }
        public string server_theme()
        {
            JObject json = get_data(Login.sarwar_api_path + "Desktop_Api/desktop_theme?code=" + Login.SchoolCode + "");
            if (json["my_msg"].ToString() == "OK")
            {
                return json["theme_name"].ToString();
            }
            else
            {
                return "";
            }
        }

        public void Images_adds()
        {
            try
            {
                string fileName = "";
                string fileName_inapi = "";
                string appPath = "";
                string url = Login.tariq_api_path + "api/advertisement_banner?group_id=1&code=" + Login.SchoolCode + "";
                JObject json = get_data(url);
                if (json["my_msg"].ToString() == "OK")
                {
                    if (json.Count > 0)
                    {
                        JArray login_img = JArray.Parse(json["login"].ToString());
                        if (login_img != null)
                        {
                            appPath = Path.GetDirectoryName(Application.ExecutablePath) + @"\slider_images\login";
                            foreach (string img in login_img)
                            {
                                fileName = Path.GetFileName(img);
                                List<string> hasfile = Directory.GetFiles(appPath, fileName, SearchOption.AllDirectories).ToList();
                                if (hasfile.Count <= 0)
                                {
                                    using (WebClient client = new WebClient())
                                    {
                                        try
                                        {
                                            client.DownloadFile(new Uri(img), Path.GetDirectoryName(Application.ExecutablePath) + @"\slider_images\login\" + fileName);
                                        }
                                        catch (Exception e) { }
                                    }
                                }
                            }
                            List<string> hasfile_list = Directory.GetFiles(appPath, "*", SearchOption.AllDirectories).ToList();
                            foreach (string img_val in hasfile_list)
                            {
                                fileName = Path.GetFileName(img_val);
                                bool img_find = false;
                                foreach (string img in login_img)
                                {
                                    fileName_inapi = Path.GetFileName(img);
                                    if (fileName == fileName_inapi)
                                    {
                                        img_find = true;
                                        break;
                                    }
                                    else
                                    {
                                        img_find = false;
                                    }
                                }
                                if (!img_find)
                                {
                                    File.Delete(img_val);
                                }
                            }
                        }
                        JArray dashboard_img = JArray.Parse(json["dashboard"].ToString());
                        if (dashboard_img != null)
                        {
                            appPath = Path.GetDirectoryName(Application.ExecutablePath) + @"\slider_images\dashboard";
                            foreach (string img in dashboard_img)
                            {
                                fileName = Path.GetFileName(img);
                                List<string> hasfile = Directory.GetFiles(appPath, fileName, SearchOption.AllDirectories).ToList();
                                if (hasfile.Count <= 0)
                                {
                                    using (WebClient client = new WebClient())
                                    {
                                        try
                                        {
                                            client.DownloadFile(new Uri(img), Path.GetDirectoryName(Application.ExecutablePath) + @"\slider_images\dashboard\" + fileName);
                                        }
                                        catch (Exception e) { }
                                    }
                                }
                            }
                            List<string> hasfile_list = Directory.GetFiles(appPath, "*", SearchOption.AllDirectories).ToList();
                            foreach (string img_val in hasfile_list)
                            {
                                fileName = Path.GetFileName(img_val);
                                bool img_find = false;
                                foreach (string img in dashboard_img)
                                {
                                    fileName_inapi = Path.GetFileName(img);
                                    if (fileName == fileName_inapi)
                                    {
                                        img_find = true;
                                        break;
                                    }
                                    else
                                    {
                                        img_find = false;
                                    }
                                }
                                if (!img_find)
                                {
                                    File.Delete(img_val);
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {

            }
        }

        public Color GetColor(string val)
        {
            Color c = new Color();
            Match m = Regex.Match(val, @"A=(?<Alpha>\d+),\s*R=(?<Red>\d+),\s*G=(?<Green>\d+),\s*B=(?<Blue>\d+)");
            if (m.Success)
            {
                int alpha = int.Parse(m.Groups["Alpha"].Value);
                int red = int.Parse(m.Groups["Red"].Value);
                int green = int.Parse(m.Groups["Green"].Value);
                int blue = int.Parse(m.Groups["Blue"].Value);
                c = Color.FromArgb(alpha, red, green, blue);

            }
            return c;
        }
        public SnapControl receipt_report(DataRow dr, string type, bool showDetail = true, string copy_type = "  Student Copy")
        {
            SnapControl sc = new SnapControl();
            sc.LoadDocument(@".\Resources\" + type, SnapDocumentFormat.Snap);
            sc.Document.DataSources.Clear();
            if (type == "fee_receipt_default.snx")
            {
                sc.Document.DataSources.Add(new DataSourceInfo("Receipt", showReportCash(dr, showDetail, copy_type)));
            }
            else if (type == "passout_card_default.snx")
            {
                sc.Document.DataSources.Add(new DataSourceInfo("PassOut", PassOutCard(dr)));
            }
            else if (type == "experience_card_default.snx")
            {
                sc.Document.DataSources.Add(new DataSourceInfo("ExperienceLetter", experience_letter(dr)));
            }

            return sc;
        }
        public string fee_receipts(DataRow dr, bool showDetail, string copy_type = "  Student Copy", string type = "fee_receipt_default.snx")
        {
            string path = Path.Combine(Path.GetDirectoryName(Application.ExecutablePath), "Resources\\fee_receipt.xml");
            using (FileStream fs = new FileStream(path, FileMode.Create))
            {
                receipt_report(dr, type, showDetail, copy_type).ExportDocument(fs, DevExpress.XtraRichEdit.DocumentFormat.OpenXml);
                return fs.Name;
            }
        }
        public ObservableCollection<other_fee_details> other_fee_details(int invoice_id)
        {
            string query = "SELECT fc.fees_title,fca.amount FROM fees_cat_amount AS fca " +
                                        " LEFT JOIN fees_category AS fc ON fc.fee_cat_id = fca.fee_cat_id " +
                                        " WHERE fca.invoice_id = '" + invoice_id + "'  GROUP BY fca.`fee_cat_id`";
            DataTable retb = FetchDataTable(query);

            ObservableCollection<other_fee_details> aa = new ObservableCollection<other_fee_details>();
            int i = 0;
            foreach (DataRow row in retb.Rows)
            {
                other_fee_details D = new other_fee_details();
                D.afee_title = row["fees_title"].ToString();
                D.bfee_amount = row["amount"].ToString();
                aa.Add(D);
            }
            return aa;
        }
        public ObservableCollection<fee_history> fee_history(int student_id)
        {
            string query = @"SELECT MONTHNAME(invoice.date) AS month,payment.date AS paid_date, invoice.amount, invoice.amount_paid AS paid, invoice.due 
                            FROM  invoice
                            left join  payment ON invoice.invoice_id = payment.invoice_id
                            WHERE invoice.student_id = " + student_id + " GROUP BY invoice.invoice_id ORDER BY invoice.date DESC limit 12 ";
            DataTable list = FetchDataTable(query);

            ObservableCollection<fee_history> items = new ObservableCollection<fee_history>();
            int i = 0;
            foreach (DataRow row in list.Rows)
            {
                fee_history item = new fee_history();
                item.amonth = row["month"].ToString();
                item.bpaiddate = row["paid_date"].ToString();
                item.camount = row["amount"].ToString();
                item.dpaid = row["paid"].ToString();
                item.edue = row["due"].ToString();
                items.Add(item);
            }
            return items;
        }

        public bool has_spacial_character(string str)
        {
            var regx = new Regex("[^a-zA-Z0-9_.]");
            return regx.IsMatch(str);
        }

        public bool onlyNumericAllowed(string txtboxs)
        {
            bool result = true;
            if (Regex.IsMatch(txtboxs, "^[0-9]*$"))
            {
                result = true;
            }
            else
            {
                result = false;
            }

            return result;
        }
        public bool onlyDecimalNumber(string txtboxs)
        {
            bool result = true;
            if (System.Text.RegularExpressions.Regex.IsMatch(txtboxs, "(^[0-9]*[0-9]+[0-9]*\\.[0-9]*$)|(^[0-9]*\\.[0-9]*[0-9]+[0-9]*$)|(^[0-9]*[0-9]+[0-9]*$)"))
            {
                result = true;
            }
            else
            {
                result = false;
            }

            return result;
        }
        private ObservableCollection<fee_receipt_data> aa = new ObservableCollection<fee_receipt_data>();
        public ObservableCollection<fee_receipt_data> showReportCash(DataRow data, bool showDetail, string copy_type)
        {
            //SnapControl sc = new SnapControl();
            //sc.LoadDocument(@".\Resources\fee_receipt_template.snx", SnapDocumentFormat.Snap);
            aa.Clear();
            fee_receipt_data report = new fee_receipt_data();

            Image logo = Base64ToImage(Login.Logo);
            report.logo = logo;
            report.system_title = GetSettings("system_title");
            report.address = GetSettings("address");
            report.phone = GetSettings("phone");
            report.account_name = GetSettings("account_name");
            report.account_number = GetSettings("account_number");
            report.receipt_note = (GetSettings("fee_note")).Replace(";", Environment.NewLine);
            if (data != null)
            {
                //PfeeCard = new ObservableCollection<PreviousFee>();
                MySqlConnection con = new MySqlConnection(Login.constring);
                DateTime date = Convert.ToDateTime(data["Date"].ToString() == "" ? DateTime.Now.ToShortDateString() : data["Date"].ToString());
                //PreviousFee dd;
                string Detail = data["Detail"].ToString();
                String query;
                DataTable table;
                query = "select * from class_fees where class_id = '" + data["class_id"].ToString() + "'";
                DataTable class_fees = FetchDataTable(query);
                int class_late_fee = class_fees.Rows.Count > 0 ? int.Parse(class_fees.Rows[0]["late_fee"].ToString()) : 0;
                if (Detail.Contains("Merge Student"))
                {
                    Detail = Detail.Replace("Merge Student Ids:", "");
                    Detail = Detail.TrimEnd(';');
                    String[] str_array = Detail.Split(new char[] { ';' });
                    if (str_array.Length > 0)
                    {
                        /*foreach (string str in str_array)
                        {
                            dd = new PreviousFee { Amount = str.Split(new char[] { ':' })[1].ToString(), Method = str.Split(new char[':'])[0].ToString() };
                            //PfeeCard.Add(dd);
                        }*/
                    }
                }
                else
                {
                    string current_amt = string.IsNullOrEmpty(data["Current"].ToString()) ? "0" : data["Current"].ToString();
                    string discount_chk = string.IsNullOrEmpty(data["Concession"].ToString()) ? "0" : data["Concession"].ToString();
                    int current = Convert.ToInt32(current_amt) - Convert.ToInt32(discount_chk);//Convert.ToInt32(data[6]) - Convert.ToInt32(data[7]) - Convert.ToInt32(data[9]);
                    report.monthly_actual = current_amt.ToString();
                    report.current_fee = current.ToString();
                    report.privious = data["Previous"].ToString();

                }
                report.transaction = data["transaction"].ToString();
                report.late_fee = data["late_fee"].ToString();
                if (!string.IsNullOrEmpty(data["paid_date"].ToString()))
                {
                    //DateTime paid_date = DateTime.ParseExact(data["paid_date"].ToString(),"yyyy-MM-dd",null);
                    //string paid_date_val = paid_date.Day + "-" + paid_date.Month + "-" + paid_date.Year;
                    report.paid_date = data["paid_date"].ToString();
                }
                report.other_fee_details = other_fee_details(Convert.ToInt32(data["ID"]));
                int stdid = Convert.ToInt32(data["StdID"]);
                report.fee_history = fee_history(stdid);
                int classID = Convert.ToInt32(string.IsNullOrEmpty(data["class_id"].ToString()) ? 0 : data["class_id"]);
                string[] fee_details = data["detail"].ToString().Split(';');
                int monthly = 0, monthly_concession = 0;
                int admission = 0, adminsion_concession = 0;
                int anuul = 0, anual_concession = 0;
                int security = 0, security_concession = 0;
                int exam = 0, exam_concession = 0;
                int stdcard_charges = 0, other_charges = 0;
                foreach (string fee in fee_details)
                {
                    if (!string.IsNullOrEmpty(fee))
                    {
                        string[] val = fee.Split(':', ',');
                        if (val[0] == "Tution Fee")
                        {
                            monthly = Convert.ToInt32(string.IsNullOrEmpty(val[1]) ? "0" : val[1].Trim());
                            monthly_concession = GetStudentFeeConcession(stdid);
                        }
                        if (val[0] == "Admission Fee")
                        {
                            admission = Convert.ToInt32(string.IsNullOrEmpty(val[1]) ? "0" : val[1].Trim());
                            adminsion_concession = GetStudentAFeeConcession(stdid);
                        }
                        if (val[0] == "Annual Charges")
                        {
                            anuul = Convert.ToInt32(string.IsNullOrEmpty(val[1]) ? "0" : val[1].Trim());
                            anual_concession = GetStudentACFeeConcession(stdid);
                        }

                        if (val[0] == "Security Deposit")
                        {
                            security = Convert.ToInt32(string.IsNullOrEmpty(val[1]) ? "0" : val[1].Trim());
                            security_concession = GetStudentSDFeeConcession(stdid);
                        }
                        if (val[0] == "Exam Charges")
                        {
                            exam = Convert.ToInt32(string.IsNullOrEmpty(val[1]) ? "0" : val[1].Trim());
                            exam_concession = GetStudentECFeeConcession(stdid);
                        }
                        else if (val[0] == "Student Card Charges")
                        {
                            stdcard_charges = Convert.ToInt32(string.IsNullOrEmpty(val[1]) ? "0" : val[1].Trim());
                        }
                        else if (val.Length > 1)
                        {
                            other_charges = Convert.ToInt32(onlyNumericAllowed(val[1].Trim()) ? val[1].Trim() : "0");
                        }
                    }
                }
                //run time change concession
                int concession = Convert.ToInt32(string.IsNullOrEmpty(data["Concession"].ToString()) ? "0" : data["Concession"].ToString());
                concession = concession - (adminsion_concession + anual_concession + security_concession + exam_concession + monthly_concession);

                report.monthly_fee = (monthly - monthly_concession - concession).ToString();
                report.addmission_fee = (admission - adminsion_concession).ToString();
                report.annual_charges = (anuul - anual_concession).ToString();

                report.security_deposit = (security - security_concession).ToString();
                report.exam_fee = (exam - exam_concession).ToString();
                report.student_card_charges = stdcard_charges.ToString();

                report.other_charges = other_charges.ToString();

                report.total_concession = data["Concession"].ToString();
                report.challan_no = data["ID"].ToString();
                report.registration = data["registration_no"].ToString();
                report.copy_type = copy_type;
                report.student_id = data["StdID"].ToString();
                report.roll_no = data["Roll"].ToString();
                report.Father = data["Father"].ToString();
                report.Name = data["Name"].ToString();
                report.Class = data["Class"].ToString();
                report.Section = data["Section"].ToString();
                report.Amount_title = "Amount";
                report.paid = data["Paid"].ToString();
                report.remaining = data["Due"].ToString();

                report.Month = Convert.ToDateTime(data["Date"].ToString() == "" ? DateTime.Now.ToShortDateString() : data["Date"].ToString()).ToString("MMMM-yyyy");
                var amount = data["Amount"].ToString();

                if (Convert.ToInt32(data["installment"].ToString()) > 0)
                {
                    string ins_query = "SELECT title,installment_id,IFNULL(Remaining,0) AS total,amount,due,previous FROM fees_installment WHERE invoice_id = '" + data["ID"].ToString() + "' AND status = 0 ORDER BY installment_id ASC limit 1";
                    table = FetchDataTable(ins_query);
                    if (Convert.ToInt32(data["Due"]) > 0 && table.Rows.Count <= 0)
                    {
                        ins_query = "select title, installment_id,IFNULL(Remaining,0) AS total,amount,due,previous from fees_installment where status = 1 AND invoice_id = '" + data["ID"].ToString() + "' order by installment_id desc limit 1";
                        table = FetchDataTable(ins_query);
                    }
                    amount = table.Rows.Count > 0 ? table.Rows[0]["total"].ToString() : "0";
                    report.Amount_title = table.Rows.Count > 0 ? table.Rows[0]["title"].ToString() : "Installment";
                    report.Month = table.Rows.Count > 0 ? Convert.ToDateTime(table.Rows[0]["due"]).ToString("MMM/yyyy") : "";
                    report.due_date = table.Rows.Count > 0 ? Convert.ToDateTime(table.Rows[0]["due"]).ToString("yyyy-MM-dd") : "";

                    string current_ins = table.Rows.Count > 0 ? table.Rows[0]["amount"].ToString() : "0";
                    report.current_fee = current_ins;
                    report.monthly_actual = amount;
                    report.privious = table.Rows.Count > 0 ? table.Rows[0]["Previous"].ToString() : "0";
                    report.paid = (Convert.ToInt32(string.IsNullOrEmpty(report.current_fee) ? "0" : report.current_fee) + Convert.ToInt32(string.IsNullOrEmpty(report.privious) ? "0" : report.privious)).ToString();

                }
                else
                {
                    string duedate = string.IsNullOrEmpty(data["due_date"].ToString()) ? "0001-01-01" : data["due_date"].ToString() == "0000-00-00" ? "0001-01-01" : data["due_date"].ToString();
                    report.due_date = (Convert.ToDateTime(duedate)).ToString("dd-MMM-yyyy");
                }
                report.Amount = amount;
                report.after_late_fee = (class_late_fee + int.Parse(amount)).ToString();
                report.Amount_In_Word = NumberToWords(int.Parse(amount)) + " Only.";

                report.ishue_date = CurrentDate();
                /*int number_month = Convert.ToInt32(data["Month"].ToString() == "" ? "0" : data["Month"].ToString());
                int start_month = Convert.ToInt32(Convert.ToDateTime(data["Date"].ToString() == "" ? DateTime.Now.ToShortDateString() : data["Date"].ToString()).Month);
                String[] month_array = { "Jan", "Feb", "March", "Apr", "May", "June", "July", "Aug", "Sept", "Oct", "Nov", "Dec" };
                String[] FeeMontharray = new String[number_month];
                for (int i = 0; i < number_month; i++)
                {
                    int month = ((start_month + i) - 1);
                    if (month <= 11)
                        FeeMontharray[i] = month_array[start_month + i - 1];
                    else
                    {
                        month = month - 11;
                        FeeMontharray[i] = month_array[month];
                    }
                }
                String FeeMonthStr = string.Join(",", FeeMontharray);*/




                //report.ReportPrintOptions.PrintOnEmptyDataSource = false;
                /*if (showDetail) // for bank
                {
                    if (GetSettings("bank_receipt_detail") == "0")
                    {
                        report.DetailFlag = true;
                    }
                }*/

            }
            Type type = typeof(fee_receipt_data);
            PropertyInfo[] properties = type.GetProperties();
            foreach (var property in properties)
            {
                if (property.Name == "logo" && property.GetValue(report) == null)
                {
                    property.SetValue(report, Image.FromFile(@".\Resources\softpitch_logo.jpg"));
                }
                else if (property.Name == "other_fee_details" && property.GetValue(report) == null)
                {
                    ObservableCollection<other_fee_details> aa = new ObservableCollection<other_fee_details>();
                    other_fee_details D = new other_fee_details();
                    D.afee_title = "Fee_Title";
                    D.bfee_amount = "0";
                    aa.Add(D);
                    property.SetValue(report, aa);
                }
                else if (property.Name == "fee_history" && property.GetValue(report) == null)
                {
                    ObservableCollection<fee_history> items = new ObservableCollection<fee_history>();
                    fee_history item = new fee_history();
                    item.amonth = "Month";
                    item.bpaiddate = "Paid Date";
                    item.camount = "Amount";
                    item.dpaid = "Paid";
                    item.edue = "Due";
                    items.Add(item);
                    property.SetValue(report, items);
                }
                else if (property.GetValue(report) == null)
                {
                    property.SetValue(report, " - ");
                }
            }
            aa.Add(report);
            return aa;
        }
        public DataTable fee_history(int stdID, bool yearly)
        {
            string feeremaining = "";
            string query = "";
            //string is_yearly = GetSettings("");
            if (yearly)
            { //yearly
                feeremaining = " ,@running_total:= @running_total + t.paid AS TotalPaid, ROUND((Total - @running_total),0)  as Remaining";
                query = "SELECT date,Privious,Current, Concession, Total, t.paid " + feeremaining + ", Month FROM( " +
                               " SELECT payment.date as date,invoice.previous_fee as Privious,invoice.current_fee as Current,invoice.fee_concession as Concession, " +
                               " payment.total as Total,payment.amount as paid,DATE_FORMAT(invoice.date,'%Y-%m') as Month FROM invoice " +
                               " left join payment on payment.`invoice_id` = invoice.`invoice_id` " +
                               " where invoice.student_id = '" + stdID + "') t JOIN(SELECT @running_total:= 0) r ORDER BY t.`date`";
            }
            else
            {// monthly
                feeremaining = ", t.due as Remaining";
                query = "SELECT date,Month,Privious,Current, Concession,late_fee, Total, t.paid  " + feeremaining + "  FROM (  " +
                             " SELECT invoice.due, DATE_FORMAT(invoice.date,'%Y-%m') AS `date`,DATE_FORMAT(invoice.date,'%Y-%m') as Month,invoice.previous_fee as Privious,invoice.current_fee as Current,  invoice.fee_concession as Concession,invoice.late_fee, invoice.amount as Total, invoice.amount_paid as paid  " +
                             " FROM invoice " +
                             " where invoice.student_id = '" + stdID + "') t " +
                             " JOIN(SELECT @running_total:= 0) r ORDER BY t.Month";
            }
            DataTable table = FetchDataTable(query);
            return table;
        }
        public ObservableCollection<passout_fee_history> passout_fee_history(int student_id)
        {
            string is_yearly = GetSettings("has_yearly_fee");
            DataTable retb = fee_history(student_id, Convert.ToBoolean(is_yearly == "0" ? false : true));
            ObservableCollection<passout_fee_history> aa = new ObservableCollection<passout_fee_history>();
            int i = 0;
            foreach (DataRow row in retb.Rows)
            {
                passout_fee_history D = new passout_fee_history();
                D.Month = row["Month"].ToString();
                D.Previous = row["Privious"].ToString();
                D.Current = row["Current"].ToString();
                D.Concession = row["Concession"].ToString();
                D.Total = row["Total"].ToString();
                D.Paid = row["paid"].ToString();
                D.Remaining = row["Remaining"].ToString();
                aa.Add(D);
            }
            return aa;
        }

        private ObservableCollection<PassOutCard> passoutCard = new ObservableCollection<PassOutCard>();
        public ObservableCollection<PassOutCard> PassOutCard(DataRow data)
        {
            passoutCard.Clear();
            PassOutCard d = new PassOutCard();
            Image logo = Base64ToImage(Login.Logo);
            d.logo = logo;
            d.School = GetSettings("system_title");
            d.Date = DateTime.Now.ToShortDateString();
            if (data != null)
            {
                d.StdID = data["SrNo"].ToString();
                d.StdName = data["Name"].ToString();
                d.StdClass = data["Class"].ToString();
                d.DOB = data["Birthday"].ToString();
                d.FName = data["FatherName"].ToString();
                d.student_phone = data["Phone"].ToString();
                d.addmission_date = data["addmission_date"].ToString();
                d.passout_date = data["passout_date"].ToString();
                d.Session = Main_FD.SelectedSession;
                d.fee_history = passout_fee_history(Convert.ToInt32(data["SrNo"].ToString()));
            }
            Type type = typeof(PassOutCard);
            PropertyInfo[] properties = type.GetProperties();
            foreach (var property in properties)
            {
                if (property.Name == "logo" && property.GetValue(d) == null)
                {
                    property.SetValue(d, Image.FromFile(@".\Resources\softpitch_logo.jpg"));
                }

                if (property.Name == "fee_history") { }
                else if (property.GetValue(d) == null)
                {
                    property.SetValue(d, property.Name);
                }
            }
            passoutCard.Add(d);
            return passoutCard;
        }

        private string teacher_subjects(int teacher_id)
        {
            string subjects = "";
            string query = "SELECT GROUP_CONCAT(`name`) as subjects FROM (SELECT  * FROM `subject` WHERE teacher_id = '" + teacher_id + "'GROUP BY `name`) AS tb;";
            DataTable dt = FetchDataTable(query);
            if (dt.Rows.Count > 0)
            {
                subjects = dt.Rows[0]["subjects"].ToString();
            }

            return subjects;
        }

        private ObservableCollection<ExperienceCard> experienceCard = new ObservableCollection<ExperienceCard>();
        public ObservableCollection<ExperienceCard> experience_letter(DataRow data)
        {
            experienceCard.Clear();
            ExperienceCard d = new ExperienceCard();
            Image logo = Base64ToImage(Login.Logo);
            d.logo = logo;
            d.School = GetSettings("system_title");
            if (data != null)
            {
                d.ID = data["teacher_id"].ToString();
                d.Name = data["name"].ToString();
                d.CNIC = data["CNIC"].ToString();
                d.DOB = data["birthday"].ToString();
                d.FName = data["FName"].ToString();
                d.Joining = data["JoiningDate"].ToString();
                d.ending = data["EndingDate"].ToString();
                d.subjects = teacher_subjects(Convert.ToInt32(data["teacher_id"].ToString()));
            }
            Type type = typeof(ExperienceCard);
            PropertyInfo[] properties = type.GetProperties();
            foreach (var property in properties)
            {
                if (property.Name == "logo" && property.GetValue(d) == null)
                {
                    property.SetValue(d, Image.FromFile(@".\Resources\softpitch_logo.jpg"));
                }
                else if (property.GetValue(d) == null)
                {
                    property.SetValue(d, property.Name);
                }
            }
            experienceCard.Add(d);
            return experienceCard;
        }

        private ObservableCollection<StudentCard> std_Card = new ObservableCollection<StudentCard>();
        public ObservableCollection<StudentCard> std_card(DataRow data)
        {
            std_Card.Clear();
            StudentCard d = new StudentCard();
            Image logo = Base64ToImage(Login.Logo);
            d.logo = logo;
            d.School = GetSettings("system_title");
            d.School_Address = GetSettings("address");
            if (data != null)
            {
                d.ID = data["student_id"].ToString();
                d.Name = data["name"].ToString();
                d.Roll = data["roll"].ToString();
                d.Roll = data["class"].ToString();
                d.Roll = data["section"].ToString();
            }
            Type type = typeof(StudentCard);
            PropertyInfo[] properties = type.GetProperties();
            foreach (var property in properties)
            {
                if (property.Name == "logo" && property.GetValue(d) == null)
                {
                    property.SetValue(d, Image.FromFile(@".\Resources\softpitch_logo.jpg"));
                }
                else if (property.GetValue(d) == null)
                {
                    property.SetValue(d, property.Name);
                }
            }
            std_Card.Add(d);
            return std_Card;
        }

        public Image get_image(String folder, string picname, bool isRecursive, string gender)
        {
            String[] filters = new String[] { "jpg", "jpeg", "png", "gif", "tiff", "bmp", "svg" };
            string searchFolder = Path.GetDirectoryName(Application.ExecutablePath).ToString() + folder;// @"\Images\Students\";
            List<String> filesFound = new List<String>();
            var searchOption = isRecursive ? SearchOption.AllDirectories : SearchOption.TopDirectoryOnly;
            foreach (var filter in filters)
            {
                filesFound.AddRange(Directory.GetFiles(searchFolder, String.Format(picname + ".{0}", filter), searchOption));
            }
            Image std_img = null;
            if (filesFound.Count > 0)
            {
                std_img = Image.FromFile(filesFound[0]);
            }
            else if (File.Exists(@".\" + gender + "_Student.png"))
            {
                std_img = Image.FromFile(@".\" + gender + "_Student.png");
            }

            return std_img;
        }
        public Image get_image_teacher(String folder, string picname, bool isRecursive, string gender)
        {
            String[] filters = new String[] { "jpg", "jpeg", "png", "gif", "tiff", "bmp", "svg" };
            string searchFolder = Path.GetDirectoryName(Application.ExecutablePath).ToString() + folder;// @"\Images\Students\";
            List<String> filesFound = new List<String>();
            var searchOption = isRecursive ? SearchOption.AllDirectories : SearchOption.TopDirectoryOnly;
            foreach (var filter in filters)
            {
                filesFound.AddRange(Directory.GetFiles(searchFolder, String.Format(picname + ".{0}", filter), searchOption));
            }

            Image std_img = null;
            if (filesFound.Count > 0)
            {
                std_img = Image.FromFile(filesFound[0]);
            }
            else if (File.Exists(@".\" + gender + "_Teacher.png"))
            {
                std_img = Image.FromFile(@".\" + gender + "_Teacher.png");//Path.GetDirectoryName(Application.ExecutablePath) + @"\student_icon.png" 
            }

            return std_img;
        }
        public void EncriptDB(string input, string output, string strHash)
        {
            FileStream inStream, outStream;
            CryptoStream CryStream;
            TripleDESCryptoServiceProvider TDC = new TripleDESCryptoServiceProvider();
            MD5CryptoServiceProvider md5 = new MD5CryptoServiceProvider();

            byte[] byteHash, byteTexto;

            inStream = new FileStream(input, FileMode.Open, FileAccess.Read);
            outStream = new FileStream(output, FileMode.OpenOrCreate, FileAccess.Write);

            byteHash = md5.ComputeHash(ASCIIEncoding.ASCII.GetBytes(strHash));
            byteTexto = File.ReadAllBytes(input);

            md5.Clear();

            TDC.Key = byteHash;
            TDC.Mode = CipherMode.ECB;

            CryStream = new CryptoStream(outStream, TDC.CreateEncryptor(), CryptoStreamMode.Write);

            int byteRead;
            long Lenght, position = 0;
            Lenght = inStream.Length;

            while (position < Lenght)
            {
                byteRead = inStream.Read(byteTexto, 0, byteTexto.Length);
                position += byteRead;

                CryStream.Write(byteTexto, 0, byteRead);
            }

            inStream.Close();
            outStream.Close();
        }
        public void DecriptDB(string input, string output, string strHash)
        {
            try
            {
                FileStream inStream, outStream;
                CryptoStream CryStream;
                TripleDESCryptoServiceProvider TDC = new TripleDESCryptoServiceProvider();
                MD5CryptoServiceProvider md5 = new MD5CryptoServiceProvider();

                byte[] byteHash, byteTexto;

                inStream = new FileStream(input, FileMode.Open, FileAccess.Read);
                outStream = new FileStream(output, FileMode.OpenOrCreate, FileAccess.Write);

                byteHash = md5.ComputeHash(ASCIIEncoding.ASCII.GetBytes(strHash));
                byteTexto = File.ReadAllBytes(input);

                md5.Clear();

                TDC.Key = byteHash;
                TDC.Mode = CipherMode.ECB;

                CryStream = new CryptoStream(outStream, TDC.CreateDecryptor(), CryptoStreamMode.Write);

                int byteRead;
                long Lenght, position = 0;
                Lenght = inStream.Length;

                while (position < Lenght)
                {
                    byteRead = inStream.Read(byteTexto, 0, byteTexto.Length);
                    position += byteRead;

                    CryStream.Write(byteTexto, 0, byteRead);
                }

                inStream.Close();
                outStream.Close();
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message, "Decriptor");
            }
        }

        private string foldername = "noticeboardfile";
        private string path = Directory.GetCurrentDirectory();
        private string Directorypath_file = Path.GetDirectoryName(Application.ExecutablePath) + "\\DbBackup\\testfile.txt";// its is use to create temporary file in directory and the we delete it after use that
        public void save_File(string CurrentKey, string file_name, string save_path, bool isencripted = true)
        {

            // File name
            FileStream stream = null;
            try
            {
                // Create a FileStream with mode CreateNew  
                stream = new FileStream(save_path, FileMode.OpenOrCreate);
                // Create a StreamWriter from FileStream  
                using (StreamWriter writer = new StreamWriter(stream, Encoding.UTF8))
                {
                    if (isencripted)
                    {
                        writer.WriteLine(Encrypt(CurrentKey, "Zee0107"));
                    }
                    else
                    {
                        writer.WriteLine(CurrentKey);
                    }
                }
            }
            finally
            {
                if (stream != null)
                {
                    stream.Dispose();
                }
            }

            string FolderName = Directory.GetDirectories(path).Where(s => s.Equals(path + "\\" + foldername)).LastOrDefault();
            if (FolderName != path + "\\" + foldername)
            {
                System.IO.Directory.CreateDirectory(path + "\\" + foldername);
            }

        }
        public void save_date_File(string date, string file_name)
        {
            string full_path = path + "\\" + foldername + "\\" + file_name;
            save_File(date, file_name, full_path);
        }
        public void save_key_File(string CurrentKey, string file_name)
        {
            string full_path = path + "\\" + foldername + "\\" + file_name;
            save_File(CurrentKey, file_name, full_path);
            update_setting("", "activition_key");
        }
        public void update_setting(string description, string type)
        {
            string query = "update settings SET description='" + description + "' where type = '" + type + "' ;";
            ExecuteQuery(query);
        }
        public void report_print(XtraReport report)
        {
            ReportPrintTool printTool = new ReportPrintTool(report);
            printTool.Report.CreateDocument(false);
            printTool.PreviewForm.PrintingSystem.Document.AutoFitToPagesWidth = 1;
            string theme = GetSettings("desktop_theme");
            printTool.ShowRibbonPreviewDialog(UserLookAndFeel.Default);
        }

        public string savestring(string cs)
        {
            MySqlConnectionStringBuilder builder = new MySqlConnectionStringBuilder(cs);
            string pass = Encrypt(builder.Password, "Zee0107");
            builder.Password = pass;
            string newstring = builder.ConnectionString;
            return newstring;
        }
        public void AddUpdateAppSettings(string key, string value)
        {
            try
            {
                //string cfgPath = Path.Combine(@"..\", "App_settings.config");
                //var configMap = new ExeConfigurationFileMap { ExeConfigFilename = cfgPath };
                //ConfigurationManager.AppSettings.Set("update_type", "test");
                Configuration c = ConfigurationManager.OpenExeConfiguration(@"..\Softpitch_Software_Updater.exe");
                var settings = c.AppSettings.Settings;
                //check if input key is new or already existing
                if (settings[key] == null)
                {
                    //if new then add the value
                    settings.Add(key, value);
                }
                else
                {
                    //if not then update the key value
                    settings[key].Value = value;
                }
                c.Save(ConfigurationSaveMode.Modified);
                ConfigurationManager.RefreshSection("appSettings");
            }
            catch (ConfigurationErrorsException exs)
            {
                throw new Exception("Error writing app settings" + exs.InnerException);
            }
        }

        public IEnumerable<Control> GetAll(Control control, Type type = null)
        {
            var controls = control.Controls.Cast<Control>();
            //check the all value, if true then get all the controls
            //otherwise get the controls of the specified type
            if (type == null)
            {
                return controls.SelectMany(ctrl => GetAll(ctrl, type)).Concat(controls);
            }
            else
            {
                return controls.SelectMany(ctrl => GetAll(ctrl, type)).Concat(controls).Where(c => c.GetType() == type);
            }
        }

        public void selectall_Controls(Control instance)
        {
            string font_family = GetSettings("font_family");
            float font_size = Convert.ToInt32(string.IsNullOrEmpty(GetSettings("font_size")) ? "0" : GetSettings("font_size"));
            if (font_size <= 0) return;
            if (string.IsNullOrEmpty(font_family)) return;
            var l = GetAll(instance, typeof(Label));
            foreach (Label lbl in l)
            {
                lbl.Font = new Font(font_family, font_size, lbl.Font.Style);
            }
            var lc = GetAll(instance, typeof(LabelControl));
            foreach (LabelControl lbl in lc)
            {
                lbl.Font = new Font(font_family, font_size, lbl.Appearance.Font.Style);
            }
            var te = GetAll(instance, typeof(TextEdit));
            foreach (TextEdit txt_e in te)
            {
                txt_e.Font = new Font(font_family, font_size, txt_e.Font.Style);
                txt_e.Properties.Appearance.Font = new Font(font_family, font_size, txt_e.Properties.Appearance.Font.Style);
            }
            var t = GetAll(instance, typeof(TextBox));
            foreach (TextBox txt in t)
            {
                txt.Font = new Font(font_family, font_size, txt.Font.Style);
            }
            var dtp = GetAll(instance, typeof(DateTimePicker));
            foreach (DateTimePicker txt in dtp)
            {
                txt.Font = new Font(font_family, font_size, txt.Font.Style);
            }
            var de = GetAll(instance, typeof(DateEdit));
            foreach (DateEdit txt in de)
            {
                txt.Font = new Font(font_family, font_size, txt.Font.Style);
                txt.Properties.Appearance.Font = new Font(font_family, font_size, txt.Properties.Appearance.Font.Style);
            }
            var sle = GetAll(instance, typeof(SearchLookUpEdit));
            foreach (SearchLookUpEdit txt in sle)
            {
                txt.Font = new Font(font_family, font_size, txt.Font.Style);
                GridView v = txt.Properties.PopupView as GridView;
                v.Appearance.Row.Font = new Font(font_family, font_size, v.Appearance.Row.Font.Style);
                v.Appearance.HeaderPanel.Font = new Font(font_family, font_size, v.Appearance.HeaderPanel.Font.Style);
                v.Appearance.FooterPanel.Font = new Font(font_family, font_size, v.Appearance.FooterPanel.Font.Style);
                v.Appearance.GroupRow.Font = new Font(font_family, font_size, v.Appearance.GroupRow.Font.Style);
                v.Appearance.GroupPanel.Font = new Font(font_family, font_size, v.Appearance.GroupPanel.Font.Style);
                v.Appearance.GroupFooter.Font = new Font(font_family, font_size, v.Appearance.GroupFooter.Font.Style);
                v.Appearance.FilterPanel.Font = new Font(font_family, font_size, v.Appearance.FilterPanel.Font.Style);
            }
            var le = GetAll(instance, typeof(LookUpEdit));
            foreach (LookUpEdit txt in le)
            {
                txt.Font = new Font(font_family, font_size, txt.Font.Style);
                txt.Properties.Appearance.Font = new Font(font_family, font_size, txt.Properties.Appearance.Font.Style);
            }
            var c = GetAll(instance, typeof(CheckEdit));
            foreach (CheckEdit chb in c)
            {
                chb.Font = new Font(font_family, font_size, chb.Font.Style);
                chb.Properties.Appearance.Font = new Font(font_family, font_size, chb.Properties.Appearance.Font.Style);
            }
            var cbe = GetAll(instance, typeof(ComboBoxEdit));
            foreach (ComboBoxEdit chb in cbe)
            {
                chb.Font = new Font(font_family, font_size, chb.Font.Style);
                chb.Properties.Appearance.Font = new Font(font_family, font_size, chb.Properties.Appearance.Font.Style);
                chb.Properties.AppearanceDropDown.Font = new Font(font_family, font_size, chb.Properties.AppearanceDropDown.Font.Style);
            }
            var ccbe = GetAll(instance, typeof(CheckedComboBoxEdit));
            foreach (CheckedComboBoxEdit chb in ccbe)
            {
                chb.Font = new Font(font_family, font_size, chb.Font.Style);
                chb.Properties.Appearance.Font = new Font(font_family, font_size, chb.Properties.Appearance.Font.Style);
                chb.Properties.AppearanceDropDown.Font = new Font(font_family, font_size, chb.Properties.AppearanceDropDown.Font.Style);
            }

            var g = GetAll(instance, typeof(DevExpress.XtraGrid.GridControl));
            foreach (DevExpress.XtraGrid.GridControl gc in g)
            {
                gc.Font = new Font(font_family, font_size);
                DevExpress.XtraGrid.Registrator.BaseInfoRegistrator br = gc.MainView.BaseInfo;
                //
                if (br != null && br.ViewName == "TileView")
                {
                    DevExpress.XtraGrid.Views.Tile.TileView tile = gc.MainView as DevExpress.XtraGrid.Views.Tile.TileView;
                    tile.Appearance.ItemNormal.FontSizeDelta = (int)font_size;
                    tile.Appearance.ItemNormal.FontSizeDelta = (int)font_size;
                }
                else
                {
                    GridView v = gc.MainView as GridView;
                    v.Appearance.Row.Font = new Font(font_family, font_size, v.Appearance.Row.Font.Style);
                    v.Appearance.HeaderPanel.Font = new Font(font_family, font_size, v.Appearance.HeaderPanel.Font.Style);
                    v.Appearance.FooterPanel.Font = new Font(font_family, font_size, v.Appearance.FooterPanel.Font.Style);
                    v.Appearance.GroupRow.Font = new Font(font_family, font_size, v.Appearance.GroupRow.Font.Style);
                    v.Appearance.GroupPanel.Font = new Font(font_family, font_size, v.Appearance.GroupPanel.Font.Style);
                    v.Appearance.GroupFooter.Font = new Font(font_family, font_size, v.Appearance.GroupFooter.Font.Style);
                    v.Appearance.FilterPanel.Font = new Font(font_family, font_size, v.Appearance.FilterPanel.Font.Style);
                }
            }
            var s = GetAll(instance, typeof(SimpleButton));
            foreach (SimpleButton sbtn in s)
            {
                sbtn.Font = new Font(font_family, font_size, sbtn.Font.Style);
                sbtn.Appearance.Font = new Font(font_family, font_size, sbtn.Appearance.Font.Style);
                sbtn.AutoSize = true;
            }
            var ll = GetAll(instance, typeof(LinkLabel));
            foreach (LinkLabel link_l in ll)
            {
                link_l.Font = new Font(font_family, font_size, link_l.Font.Style);
            }
            var gb = GetAll(instance, typeof(GroupBox));
            foreach (GroupBox link_l in gb)
            {
                link_l.Font = new Font(font_family, font_size, link_l.Font.Style);
            }
        }

        public bool IsBase64String(string s)
        {
            return (s.Trim().Length % 4 == 0) && Regex.IsMatch(s, @"^[a-zA-Z0-9\+/]*={0,3}$", RegexOptions.None);

        }
        public int[] MultiSelectVal(DevExpress.XtraEditors.GridLookUpEdit GridExamList)
        {
            GridCheckMarksSelection gridCheckMark = GridExamList.Properties.Tag as GridCheckMarksSelection;
            ArrayList valueList = gridCheckMark.Selection;
            int[] exam_array = new int[valueList.Count];
            int count = 0;
            foreach (DataRowView rv in valueList)
            {
                exam_array[count] = Convert.ToInt32(rv["id"]);
                count++;
            }
            return exam_array;
        }

        public void attendance_holidays_leaves_chack(object sectionID, string day)
        {
            string query = "SELECT * FROM `tbl_holidays` AS h WHERE h.`section_id` = '" + sectionID + "' AND '" + day + "' BETWEEN h.`start_date` AND h.`end_date`";
            DataTable cls_holiday = FetchDataTable(query);
            query = "select student_id,section_id from student where section_id = '" + sectionID + "' " +
                " and not student_id in (select student_id from attendance Where date='" + day + "')";
            DataTable dt_std = FetchDataTable(query);
            foreach (DataRow std in dt_std.Rows)
            {
                query = " select * from tbl_students_leaves where section_id = '" + std["section_id"] + "' and student_id = '" + std["student_id"] + "' and '" + day + "' between start_date and end_date ";
                DataTable leave_dt = FetchDataTable(query);
                if (leave_dt.Rows.Count > 0)
                {
                    query = "INSERT into attendance(status,student_id,date,time_in,time_out,sync) " +
                        " VALUES ('2','" + std["student_id"].ToString() + "','" + day + "','','','0')";
                }
                else if (cls_holiday.Rows.Count > 0)
                {
                    query = "INSERT into attendance(status,student_id,date,time_in,time_out,sync) " +
                        " VALUES ('4','" + std["student_id"].ToString() + "','" + day + "','','','0')";
                }
                else
                {
                    query = "INSERT into attendance(status,student_id,date,time_in,time_out,sync) " +
                        " VALUES ('1','" + std["student_id"].ToString() + "','" + day + "','" + DateTime.Now.ToString("HH:mm") + "','','0')";
                }
                ExecuteQuery(query);
            }
        }

        public IEnumerable<DateTime> EachDay(DateTime from, DateTime thru)
        {
            for (var day = from.Date; day.Date <= thru.Date; day = day.AddDays(1))
                yield return day;
        }
        //change section and shift marks to new section/class 
        public void TransferMarks(int studentId, int oldSectionId, int newSectionId)
        {
            string updateQuery = $"UPDATE mark SET section_id = {newSectionId}, class_id = (select class_id from section where section_id = {newSectionId}) WHERE student_id = {studentId}";
            ExecuteQuery(updateQuery);
            return;
            //for PGC ktd done this point to do in setting later 

            string query = $"SELECT section_id FROM mark WHERE student_id = {studentId} GROUP BY section_id ";
            DataTable dataTable = FetchDataTable(query);
            foreach (DataRow row in dataTable.Rows)
            {
                oldSectionId = Convert.ToInt32(row["section_id"]);
                // Step 1: Fetch the obtained marks for the student in the old section
                var oldMarksList = GetStudentMarks(studentId, oldSectionId);
                // Step 3: Adjust and transfer marks
                foreach (var mark in oldMarksList)
                {
                    int subjectId = mark.SubjectId;
                    var oldTotalMarks = GetSectionTotalMarks(oldSectionId, mark.ExamId);
                    var newTotalMarks = GetSectionTotalMarks(newSectionId, mark.ExamId);
                    // Check if the subject exists in both old and new sections
                    if (oldTotalMarks.ContainsKey(subjectId) && newTotalMarks.ContainsKey(subjectId))
                    {
                        int oldTotal = oldTotalMarks[subjectId];
                        int newTotal = newTotalMarks[subjectId];

                        // Calculate the adjusted marks for the new section
                        double adjustedMarks = (mark.ObtainedMarks / (double)oldTotal) * newTotal;

                        // Step 4: Update or insert marks for the student in the new section
                        UpdateStudentMarks(studentId, mark.ClassId, newSectionId, mark.ExamId, subjectId, adjustedMarks);
                    }
                }
            }
    }

        // Helper method to fetch student marks for a section using ExecuteQuery
        private List<Marks> GetStudentMarks(int studentId, int sectionId)
        {
            List<Marks> marksList = new List<Marks>();

            string query = $"SELECT student_id, exam_id, subject_id, section_id, class_id, mark_obtained " +
                            $"FROM mark WHERE student_id = {studentId} AND section_id = {sectionId}";

            DataTable dataTable = FetchDataTable(query);

            foreach (DataRow row in dataTable.Rows)
            {
                marksList.Add(new Marks
                {
                    StudentId = Convert.ToInt32(row["student_id"]),
                    ExamId = Convert.ToInt32(row["exam_id"]),
                    SubjectId = Convert.ToInt32(row["subject_id"]),
                    SectionId = Convert.ToInt32(row["section_id"]),
                    ClassId = Convert.ToInt32(row["class_id"]),
                    ObtainedMarks = Convert.ToDouble(row["mark_obtained"])
                });
            }

            return marksList;
        }

        // Helper method to fetch total marks for each subject in a section using ExecuteQuery
        private Dictionary<int, int> GetSectionTotalMarks(int sectionId, int examId)
        {
            Dictionary<int, int> totalMarks = new Dictionary<int, int>();

            // Adjusted query to include exam_id along with section_id
            string query = $"SELECT subject_id, MAX(marks) AS marks FROM tbl_mark_subject WHERE section_id = {sectionId} AND exam_id = {examId} GROUP BY subject_id ";

            DataTable dataTable = FetchDataTable(query);

            foreach (DataRow row in dataTable.Rows)
            {
                totalMarks.Add(Convert.ToInt32(row["subject_id"]), Convert.ToInt32(row["marks"]));
            }

            return totalMarks;
        }

        // Helper method to update or insert student marks in the new section using ExecuteQuery
        private void UpdateStudentMarks(int studentId, int classId, int sectionId, int examId, int subjectId, double adjustedMarks)
        {
            // Check if marks already exist
            string checkQuery = $"SELECT COUNT(*) FROM mark WHERE student_id = {studentId} AND exam_id = {examId} AND subject_id = {subjectId}";

            DataTable checkResult = FetchDataTable(checkQuery);

            int count = Convert.ToInt32(checkResult.Rows[0][0]);

            if (count > 0)
            {
                // If marks exist, update them
                string updateQuery = $"UPDATE mark SET mark_obtained = {adjustedMarks}, section_id = {sectionId}, class_id= {classId} WHERE student_id = {studentId} " +
                                        $" AND exam_id = {examId} AND subject_id = {subjectId}";

                ExecuteQuery(updateQuery);
            }
        }
        

    // Class representing marks
    public class Marks
    {
        public int StudentId { get; set; }
        public int ExamId { get; set; }
        public int SubjectId { get; set; }
        public int SectionId { get; set; }
        public int ClassId { get; set; }
        public double ObtainedMarks { get; set; }
    }





    }
}
