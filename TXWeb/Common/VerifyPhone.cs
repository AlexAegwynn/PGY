using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Common
{
    public class VerifyPhone
    {
        public static string SendVerificationCode(string url, string param, string userAgent)
        {
            string result = string.Empty;

            Encoding encoding = Encoding.UTF8;
            byte[] data = encoding.GetBytes(param);

            HttpWebRequest webRequest = WebRequest.Create(url) as HttpWebRequest;
            webRequest.Method = "POST";            
            webRequest.KeepAlive = false;
            webRequest.ContentType = "application/x-www-form-urlencoded";
            webRequest.UserAgent = userAgent;
            webRequest.ContentLength = data.Length;
            webRequest.Timeout = 15000;

            try
            {
                Stream reqStream = webRequest.GetRequestStream();
                reqStream.Write(data, 0, data.Length);
                reqStream.Close();

                WebResponse response = webRequest.GetResponse();
                StreamReader streamReader = new StreamReader(response.GetResponseStream(), encoding);
                result = streamReader.ReadToEnd();
                response.Close();
            }
            catch (Exception)
            {
                result = "";
            }

            return result;
        }
    }
}
