using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Drawing;
using System.Drawing.Imaging;
using System.Drawing.Text;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Web;

namespace School
{
    public class TextLib
    {
        public static DBContext db = new DBContext();
        public static string Message;

        public static string GeneratOTP()
        {
            Random r = new Random();
            return r.Next(1000, 9999).ToString();
        }
        //public static void SendSMS(string SentTo, string Message, string Type)
        //{
        //    var sms = db.SmsSetupModels.Where(x=>x.SmsSetupID==1).SingleOrDefault();
        //    if(sms!=null)
        //    {
        //        string strUrl = "";

        //        if (Type == "Unicode")
        //        {
        //            strUrl = "https://securesmpp.com/api/sendmessage.php?usr=" + sms.UserName + "&apikey=" + sms.Password + "&sndr=" + sms.Sender + "&ph=" + SentTo + "&message=" + Message;
        //        }
        //        else
        //        {
        //            strUrl = "https://securesmpp.com/api/sendmessage.php?usr=" + sms.UserName + "&apikey=" + sms.Password + "&sndr=" + sms.Sender + "&ph=" + SentTo + "&message=" + Message;
        //        }
        //        // Send 
        //        using (var client = new WebClient())
        //        {
        //            var responseString = client.DownloadString(strUrl);
        //        }
        //    }            
        //}
        public static string GetCaptcha()
        {
            //var chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
            var chars = "ABCDEFGHIJKLMNPQRSTUVWXYZ123456789";
            var stringChars = new char[6];
            var random = new Random();

            for (int i = 0; i < stringChars.Length; i++)
            {
                stringChars[i] = chars[random.Next(chars.Length)];
            }
            var finalString = new String(stringChars);

            return finalString;
        }
        public static void DrawCaptch(string captcha,string serverpath)
        {
            Bitmap objBitmap;
            Graphics objGraphics;
            objBitmap = new Bitmap(100, 35);
            objGraphics = Graphics.FromImage(objBitmap);
            objGraphics.Clear(Color.White);

            Pen redPen = new Pen(Color.Red, 1);
            objGraphics.DrawLine(redPen, 5, 4, 95, 32);

            FontFamily fontfml = new FontFamily(GenericFontFamilies.Serif);
            Font font = new Font(fontfml, 16);
            SolidBrush brush = new SolidBrush(Color.Green);
            objGraphics.DrawString(captcha, font, brush, 5, 5);         

            objBitmap.Save(serverpath + "/wwwroot/uploadfiles/captcha.jpg", ImageFormat.Jpeg);
        }
        public static int GetInt(string val)
        {            
            if(int.TryParse(val, out int result) == true)
            {
                return result;
            }
            else
            {
                return 0;
            }            
        }
        public static double GetDouble(string val)
        {
            if (double.TryParse(val, out double result) == true)
            {
                return result;
            }
            else
            {
                return 0;
            }
        }
        public static string IndianRuppes(double Amount)
        {
            decimal Value = decimal.Parse(Amount.ToString(), CultureInfo.InvariantCulture);
            CultureInfo indian = new CultureInfo("hi-IN");
            return string.Format(indian, "{0:c}", Value);
        }
        public static string IndianRuppes(string Amount)
        {
            decimal Value = decimal.Parse(Amount, CultureInfo.InvariantCulture);
            CultureInfo indian = new CultureInfo("hi-IN");
            return string.Format(indian, "{0:c}", Value);
        }
        public static string Encrypt(string Text)
        {
            string EncryptionKey = "MAKV2SPBNI99212";
            byte[] clearBytes = Encoding.Unicode.GetBytes(Text);
            using (Aes encryptor = Aes.Create())
            {
                Rfc2898DeriveBytes pdb = new Rfc2898DeriveBytes(EncryptionKey, new byte[] { 0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d, 0x65, 0x64, 0x76, 0x65, 0x64, 0x65, 0x76 });
                encryptor.Key = pdb.GetBytes(32);
                encryptor.IV = pdb.GetBytes(16);
                using (MemoryStream ms = new MemoryStream())
                {
                    using (CryptoStream cs = new CryptoStream(ms, encryptor.CreateEncryptor(), CryptoStreamMode.Write))
                    {
                        cs.Write(clearBytes, 0, clearBytes.Length);
                        cs.Close();
                    }
                    Text = Convert.ToBase64String(ms.ToArray());
                }
            }
            return Text;
        }
        public static string Decrypt(string Text)
        {
            string EncryptionKey = "MAKV2SPBNI99212";
            Text = Text.Replace(" ", "+");
            byte[] cipherBytes = Convert.FromBase64String(Text);
            using (Aes encryptor = Aes.Create())
            {
                Rfc2898DeriveBytes pdb = new Rfc2898DeriveBytes(EncryptionKey, new byte[] { 0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d, 0x65, 0x64, 0x76, 0x65, 0x64, 0x65, 0x76 });
                encryptor.Key = pdb.GetBytes(32);
                encryptor.IV = pdb.GetBytes(16);
                using (MemoryStream ms = new MemoryStream())
                {
                    using (CryptoStream cs = new CryptoStream(ms, encryptor.CreateDecryptor(), CryptoStreamMode.Write))
                    {
                        cs.Write(cipherBytes, 0, cipherBytes.Length);
                        cs.Close();
                    }
                    Text = Encoding.Unicode.GetString(ms.ToArray());
                }
            }
            return Text;
        }
        public static string UnicodeToUTF8(string myString)
        {
            byte[] utfByte = Encoding.UTF8.GetBytes(myString);
            HttpUtility.UrlEncode(utfByte);
            return HttpUtility.UrlEncode(utfByte);
        }
        public static bool CheckForInternetConnection()
        {
            try
            {
                using (var client = new WebClient())
                using (client.OpenRead("http://clients3.google.com/generate_204"))
                {
                    return true;
                }
            }
            catch
            {
                return false;
            }
        }
        public static string UploadFilewithHTMLControl(IFormFile postedFile,string serverPath, string newFileName)
        {
            //form should have:-  enctype="multipart/form-data".
            // use readURL for upload and show file.
            //HttpPostedFile postedFile = HttpContext.Current.Request.Files[HTMLControlName];
            try
            {
                if (postedFile != null && postedFile.Length > 0)
                {
                    string mainPath = serverPath + "/wwwroot/uploadfiles/"; // Main Path
                    string extension = Path.GetExtension(postedFile.FileName);
                    newFileName = newFileName + extension; // replace file name

                    using (FileStream stream = new FileStream(Path.Combine(mainPath, newFileName), FileMode.Create))
                    {
                        postedFile.CopyTo(stream);
                        return newFileName;
                    }
                }
                else
                {
                    return "No";
                }                
            }            
            catch
            {
                return "No";
            }
        }
        public static Tuple<string, string> UploadPDFwithHTMLControl(IFormFile postedFilePdf, string serverPath, string newFileName)
        {
            string Error;
            if (postedFilePdf == null)
            {
                Error = "Please Upload Your file";
                return new Tuple<string, string>(Error, "No");
            }
            else
            {                 
                int MaxContentLength = 1024 * 1024 * 3; //3 MB
                string[] AllowedFileExtensions = new string[] { ".jpg", ".gif", ".png", ".pdf" };

                if (!AllowedFileExtensions.Contains(postedFilePdf.FileName.Substring(postedFilePdf.FileName.LastIndexOf('.'))))
                {
                    Error = "Please file of type: " + string.Join(", ", AllowedFileExtensions);
                }

                else if (postedFilePdf.Length > MaxContentLength)
                {
                    Error = "Your file is too large, maximum allowed size is: " + MaxContentLength + " MB";
                }
                else
                {
                    var path = Path.Combine(serverPath, "UploadFiles");
                    string extension = Path.GetExtension(postedFilePdf.FileName);
                    newFileName = newFileName + extension; // replace file name
                    using (FileStream stream = new FileStream(Path.Combine(path, newFileName), FileMode.Create))
                    {
                        postedFilePdf.CopyTo(stream);
                    }
                    Error = "OK";
                }                
                 
                return new Tuple<string, string>(Error, newFileName);
                
            }
           
        }         
        public static string GetMultipleLine(string text)
        {
            string[] lines = Regex.Split(text, "\r\n");
            string str = "";
            for (int i = 0; i < lines.Length; i++)
            {
                str = str + lines[i] + "\r\n";
            }
            str = str.Substring(0, str.Length - 2);
            return str;
        }
        public static string GetDateDDMMYYYY(DateTime? date)
        {
            string Date = date.ToString();
            Date = Date.Substring(0, 10);
            try
            {
                return Convert.ToDateTime(Date).ToString("dd-MM-yyyy");
            }
            catch
            {
                DateTime dt = DateTime.ParseExact(Date, "dd-MM-yyyy", CultureInfo.InvariantCulture);
                return dt.ToString();
            }
            //DateTime dt = DateTime.ParseExact(date.ToString(), "dd-MM-yyyy", CultureInfo.InvariantCulture);
            //return dt.ToString();

            //string Date = date.ToString();
            //Date = Date.Substring(0, 10);
            //try
            //{
            //    return Convert.ToDateTime(Date).ToString("dd-MM-yyyy");
            //}
            //catch
            //{

            //}
            //return Convert.ToDateTime(date).ToString("dd-MM-yyyy");
        }
    }
}
