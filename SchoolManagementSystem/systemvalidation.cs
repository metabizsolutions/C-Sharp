using System;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Management;
using System.Threading.Tasks;
using RestSharp;
using System.Net;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Windows;
using System.Collections.Generic;
using System.IO;

namespace SchoolManagementSystem
{
    class systemvalidation
    {
        CommonFunctions fun = new CommonFunctions();
        public string check_registeration(string key, string code)
        {
            string response = "OK";
            string password = "osp@tnsbay:379";
            int current_timestamp = this.timestamp(DateTime.Now);
            string decryptedstring = StringCipher.Decrypt(key, password);
            string[] string_array = decryptedstring.Split(',');
            if (string_array.Length == 4)
            {
                string school_code = string_array[0];
                int start_timestamp = int.Parse(string_array[1]);
                int end_timestamp = int.Parse(string_array[2]);
                string system_id = string_array[3];

                if (school_code != code)
                {
                    response = "Your school code is not compatible with provided key school code So please contact Softpitch team for correct school code.";
                }
                if (start_timestamp > current_timestamp)
                {
                    response = "Invalid date time for your computer, Please correct date time for your computer.";
                }
                if (end_timestamp < current_timestamp)
                {
                    response = "Your Software Expired, Please Contact to Softpitch team for Billing information and pay bill to reactivate your software and enjoy exciting new features.";
                }
                /*if(system_id != fun.UniqueMachineID(code))
                {
                    response = "Software is Not Installed in this system Please Contect Softpitch Team.";
                }*/
            }
            else
            {
                response = "Invalid validation key, Please contact Softpitch team for correct school code.";
            }

            return response;
        }
        public string extract_keyinfo_softpitch(string key)
        {
            string password = "osp@tnsbay:379";
            string response = "";
            string decryptedstring = StringCipher.Decrypt(key, password);
            string[] string_array = decryptedstring.Split(',');
            if (string_array.Length == 4)
            {
                string school_code = string_array[0];
                int start_timestamp = int.Parse(string_array[1]);
                int end_timestamp = int.Parse(string_array[2]);
                string mac_address = string_array[3];
                response = "About Software: \n School Code: " + school_code + " \n Start Date: " + convert_datetime(start_timestamp) + " \n Expiry Date: " + convert_datetime(end_timestamp) + " \n Mac Address:" + mac_address;
            }
            else
            {
                if (key == "")
                    response = "KEY: Not Activated";
                else
                    response = "KEY: " + decryptedstring;
            }
            return response;
        }
        public string extract_keyinfo_client(string key)
        {
            string password = "osp@tnsbay:379";
            string response = "";
            string decryptedstring = StringCipher.Decrypt(key, password);
            string[] string_array = decryptedstring.Split(',');
            if (string_array.Length == 4)
            {
                string school_code = string_array[0];
                int start_timestamp = int.Parse(string_array[1]);
                int end_timestamp = int.Parse(string_array[2]);
                string mac_address = string_array[3];
                response = "About Software: \n School Code: " + school_code + " \n Mac Address:" + mac_address;
            }
            else
            {
                if (key == "")
                    response = "KEY: Not Activated";
                else
                    response = "KEY: " + decryptedstring;
            }
            return response;
        }
        public int timestamp(DateTime date)
        {
            //create Timespan by subtracting the value provided from
            //the Unix Epoch
            TimeSpan span = (date - new DateTime(1970, 1, 1, 0, 0, 0, 0).ToLocalTime());
            //return the total seconds (which is a UNIX timestamp)
            return (int)span.TotalSeconds;
        }
        public static DateTime convert_datetime(int unixTimeStamp)
        {
            // Unix timestamp is seconds past epoch
            System.DateTime dtDateTime = new DateTime(1970, 1, 1, 0, 0, 0, 0, System.DateTimeKind.Utc);
            dtDateTime = dtDateTime.AddSeconds(unixTimeStamp).ToLocalTime();
            return dtDateTime;
        }
        public string get_mac_address()
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
        public string[] activate()
        {
            systemvalidation objvalidation = new systemvalidation();
            string activation_key = fun.GetSettings("activition_key");
            string response = objvalidation.check_registeration(activation_key, Login.SchoolCode);
            string key_info = objvalidation.extract_keyinfo_client(activation_key);
            return new string[] { response, key_info };
        }
        string foldername = "noticeboardfile";
        string path = Directory.GetCurrentDirectory();
        public string upgrade_key()
        {
            object res = "ok";
            try
            {
                string val = "";
                string system_id = fun.UniqueMachineID(Login.SchoolCode);
                string url = Login.tariq_api_path + "api/upgrade_software_key?system_id=" + system_id + "&code=" + Login.SchoolCode + "";
                JObject json = fun.get_data(url);
                if (json["my_msg"].ToString() == "OK")
                {
                    res = json["message"].ToString();
                    string full_path = path + "\\" + foldername + "\\Activation_info";
                    fun.save_File(url, "Activation_info", full_path, false);
                    if (json["success"].ToString() != "1")
                        MessageBox.Show(res.ToString(), "Key Status", MessageBoxButton.OK, MessageBoxImage.Warning);
                    else
                    {
                        string password = "osp@tnsbay:379";
                        object internet_key = json["data"];
                        byte[] data = Convert.FromBase64String(internet_key.ToString());
                        string end_date = Encoding.UTF8.GetString(data);
                        string key_data = Login.SchoolCode + "," + this.timestamp(DateTime.Now.Date) + "," + this.timestamp(Convert.ToDateTime(end_date)) + "," + system_id;
                        val = StringCipher.Encrypt(key_data, password);
                    }
                    if (val == "")
                    {
                        string cur_key = fun.GetSettings("activition_key");
                        fun.save_key_File(cur_key, "beta.txt");
                    }
                    else
                    {
                        string query = "UPDATE settings set description = '" + val + "',sync='0' WHERE type = 'activition_key'; ";
                        fun.ExecuteQuery(query);
                    }
                }
            }
            catch(NullReferenceException nulex)
            {
                MessageBox.Show(nulex.Message, "Update_key Null Refrence", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Update_key", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            return res.ToString();
        }
    }
    public static class StringCipher
    {
        // This constant is used to determine the keysize of the encryption algorithm in bits.
        // We divide this by 8 within the code below to get the equivalent number of bytes.
        private const int Keysize = 256;

        // This constant determines the number of iterations for the password bytes generation function.
        private const int DerivationIterations = 1000;

        public static string Encrypt(string plainText, string passPhrase)
        {
            try
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
                            using (var memoryStream = new System.IO.MemoryStream())
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
            catch (Exception e)
            {
                return e.ToString();
            }
        }

        public static string Decrypt(string cipherText, string passPhrase)
        {
            try
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
                            using (var memoryStream = new System.IO.MemoryStream(cipherTextBytes))
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
            catch (Exception e)
            {
                return e.ToString();
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
    }
}
