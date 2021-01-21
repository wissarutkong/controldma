using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Net;
using System.Reflection;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
using System.Web;

namespace controldma
{
    internal class Cs_Controlcenter
    {
        public Cs_Controlcenter()
        {

        }
        public string toHtmlTag(string TextString)
        {
            if (string.IsNullOrEmpty(TextString))
            {
                return "";
            }

            TextString = TextString.Replace("&lt;", "<");
            TextString = TextString.Replace("&gt;", ">");
            return TextString;
        }

        public void GetIPAddress()
        {
            string IPAddress = string.Empty;

            IPHostEntry Host = default(IPHostEntry);
            string Hostname = null;
            Hostname = System.Environment.MachineName;
            Host = Dns.GetHostEntry(Hostname);
            foreach (IPAddress IP in Host.AddressList)
            {
                if (IP.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork)
                {
                    IPAddress = Convert.ToString(IP);
                }
            }

        }

        // Returns JSON string
        public string GET(string url)
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            try
            {
                WebResponse response = request.GetResponse();
                using (Stream responseStream = response.GetResponseStream())
                {
                    StreamReader reader = new StreamReader(responseStream, System.Text.Encoding.UTF8);
                    return reader.ReadToEnd();
                }
            }
            catch (WebException ex)
            {
                WebResponse errorResponse = ex.Response;
                using (Stream responseStream = errorResponse.GetResponseStream())
                {
                    StreamReader reader = new StreamReader(responseStream, System.Text.Encoding.GetEncoding("utf-8"));
                    String errorText = reader.ReadToEnd();
                    // log errorText
                }
                throw;
            }
        }

        // POST a JSON string
        public void POST(string url, string jsonContent)
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            request.Method = "POST";

            System.Text.UTF8Encoding encoding = new System.Text.UTF8Encoding();
            Byte[] byteArray = encoding.GetBytes(jsonContent);

            request.ContentLength = byteArray.Length;
            request.ContentType = @"application/json";

            using (Stream dataStream = request.GetRequestStream())
            {
                dataStream.Write(byteArray, 0, byteArray.Length);
            }
            long length = 0;
            try
            {
                using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
                {
                    length = response.ContentLength;
                }
            }
            catch (WebException ex)
            {
                // Log exception and throw as for GET example above
            }
        }

        public string MD5Hash(string input)
        {
            StringBuilder hash = new StringBuilder();
            MD5CryptoServiceProvider md5provider = new MD5CryptoServiceProvider();
            byte[] bytes = md5provider.ComputeHash(new UTF8Encoding().GetBytes(input));

            for (int i = 0; i < bytes.Length; i++)
            {
                hash.Append(bytes[i].ToString("x2"));
            }
            return hash.ToString();
        }

        public string HashPasswordUsingPBKDF2(string password)
        {
            var rfc2898DeriveBytes = new Rfc2898DeriveBytes(password, 32)
            {
                IterationCount = 10000
            };

            byte[] hash = rfc2898DeriveBytes.GetBytes(20);

            byte[] salt = rfc2898DeriveBytes.Salt;

            return Convert.ToBase64String(salt) + "|" + Convert.ToBase64String(hash);
        }

        public static string BtnSave()
        {
            string name = "บันทึก";
            if (Culture() != "th")
            {
                name = "Save";
            }
            return name;
        }

        private static string Culture()
        {
            var cookie = HttpContext.Current.Request.Cookies["_Culture"];
            try
            {
                cookie = HttpContext.Current.Request.Cookies["_Culture"];
                Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo(cookie.Value);
                Thread.CurrentThread.CurrentUICulture = Thread.CurrentThread.CurrentCulture;
            }
            catch
            {
                cookie = new HttpCookie("_Culture") { Expires = DateTime.Now.AddDays(1) };
                cookie.Value = "th";
                HttpContext.Current.Response.Cookies.Add(cookie);

                Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo(cookie.Value);
                Thread.CurrentThread.CurrentUICulture = Thread.CurrentThread.CurrentCulture;
            }

            return cookie.Value;
        }

        public string unit_percent() {
            return "%".ToString();
        }

        public Hashtable GetTimPrvtdetaile(DataTable arrtime)
        {
            string t1s = "";
            string ts = "";
            string te = "";
            Hashtable arrTime = new Hashtable();
            try
            {
                object[] Time = new object[2];
                int index = 0;
                foreach (DataRow row in arrtime.Rows) {
                    ts = row["time_start"].ToString();
                    if (t1s == "") {
                        t1s = row["time_start"].ToString();
                    }
                    if (ts != "" && te != "") {
                        Time = new object[2];
                        Time[0] = TimeSpan.Parse(te);
                        Time[1] = TimeSpan.Parse(ts) - TimeSpan.Parse("00:01:00");
                        arrTime.Add(index, Time);
                    }

                    te = ts;
                    index += 1;
                }

                Time = new object[2];
                Time[0] = TimeSpan.Parse(te);
                Time[1] = TimeSpan.Parse(t1s) - TimeSpan.Parse("00:01:00");
                if (t1s == "00:00")
                {
                    Time[1] = TimeSpan.Parse("23:59:00");
                }
                arrTime.Add(index, Time);

            }
            catch
            {
                arrTime = new Hashtable();
            }


            return arrTime;
        }

        public String GetBg_Color(string parm,int failure_mode)
        {
            string bgc = "#FFFFFF";
            switch (failure_mode)
            {
                case 0:
                    bgc = "#CCCCCC";
                    break;
                case 1:
                    if (parm != "p")
                    {
                        bgc = "#CCCCCC";
                    }
                    break;
                case 2:
                    if (parm != "f")
                    {
                        bgc = "#CCCCCC";
                    }
                    break;
                case 3:
                    if (parm != "v")
                    {
                        bgc = "#CCCCCC";
                    }
                    break;
            }

            return bgc;
        }

    }
}