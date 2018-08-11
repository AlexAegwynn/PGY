using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CM.Model;
using System.Reflection;
using System.Net;
using System.IO;

namespace CM.WebBT
{
	public class Function:TB.HttpAPI.TBPublic.TbBase
	{
		public Jayrock.Json.JsonObject GetJsonObjectBaseS(string result)
		{
			return GetJsonObjectBase(result);
		}

		public Jayrock.Json.JsonArray GetJsonArrayS(string getStr)
		{
			return GetJsonArrayBase(getStr);
		}


		public bool GetHtmlString_UTF8_Post_Base(string cookiesStr, IDictionary<string, string> parameters, ref MurlMsg urlMsg)
		{
			string htmlStr = urlMsg.HtmlStr;
			string returnUrl = urlMsg.ReturnUrl;
			bool isOK = GetHtmlString_UTF8_Post_Base(urlMsg.SubmitUrl, cookiesStr, parameters, ref htmlStr, ref returnUrl);
			urlMsg.HtmlStr = htmlStr;
			urlMsg.ReturnUrl = returnUrl;
			return isOK;
		}

		public bool GetHtmlString_UTF8_Get_Base(string cookiesStr, ref MurlMsg urlMsg)
		{
			string htmlStr = urlMsg.HtmlStr;
			string returnUrl = urlMsg.ReturnUrl;
			bool isOK = GetHtmlString_UTF8_Get_Base(urlMsg.SubmitUrl, cookiesStr, ref htmlStr, ref returnUrl);
			urlMsg.HtmlStr = htmlStr;
			urlMsg.ReturnUrl = returnUrl;
			return isOK;
		}

        
        public bool GetHtmlString_Default_Get(string cookiesStr, ref MurlMsg urlMsg)
        {
            string htmlStr = urlMsg.HtmlStr;
            string returnUrl = urlMsg.ReturnUrl;
            bool isOK = GetHtmlString_Default_Get_Base(urlMsg.SubmitUrl, cookiesStr, ref htmlStr, ref returnUrl);
            urlMsg.HtmlStr = htmlStr;
            urlMsg.ReturnUrl = returnUrl;
            return isOK;
        }

		public string GetHtmlString_UTF8_Get_New(string cookiesStr, ref MurlMsg urlMsg)
		{
			HttpWebRequest request = (HttpWebRequest)HttpWebRequest.Create(urlMsg.SubmitUrl);
			if (cookiesStr != "")
				request.Headers[HttpRequestHeader.Cookie] = cookiesStr;

			request.Method = "Get";
            //request.Host = "www.360doc.com";
            request.Host = "www.toutiao.com";
            //request.Referer = "http://www.360doc.com/index.html";
            request.Referer = "Mozilla/5.0 (Windows NT 6.1; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/65.0.3325.181 Safari/537.36";

			HttpWebResponse response = (HttpWebResponse)request.GetResponse();//执行请求，获取响应对象
			Stream stream = response.GetResponseStream(); //获取响应流
			StreamReader sr = new StreamReader(stream); //创建流读取对象
			string responseHTML = sr.ReadToEnd();//读取响应流                      
			urlMsg.HtmlStr = responseHTML;
			response.Close();//关闭响应流
			//int strleght = output.Length;
			return urlMsg.HtmlStr;
		}

        #region 获取 Cookies
        /// <summary>
        /// 获取 Cookies
        /// </summary>
        public bool GetCookies(System.Uri urlObject, string url, ref string cookies)
        {
            if (urlObject != null)
            {
                url = urlObject.AbsoluteUri;
            }
            return GetCookiesStone(url, ref cookies);
        }
        #endregion

        public string ListToJson<T>(IList<T> list)
        {
            StringBuilder Json = new StringBuilder();
            Json.Append("[");
            if (list.Count > 0)
            {
                for (int i = 0; i < list.Count; i++)
                {
                    T obj = Activator.CreateInstance<T>();
                    PropertyInfo[] pi = obj.GetType().GetProperties();
                    Json.Append("{");

                    for (int j = 0; j < pi.Length; j++)
                    {
                        Type type;
                        object o = pi[j].GetValue(list[i], null);
                        string v = string.Empty;
                        if (o != null)
                        {
                            type = o.GetType();
                            v = o.ToString();
                        }
                        else
                        {
                            type = typeof(string);
                        }
                        //if (pi[i].Name.ToString() != "NumIID")
                        v = v.Replace("\r\n", "");
                        Json.Append("\"" + pi[j].Name.ToString() + "\":" + StringFormat(v, type));
                        if (j < pi.Length - 1)
                        {
                            Json.Append(",");
                        }
                    }
                    Json.Append("}");
                    if (i < list.Count - 1)
                    {
                        Json.Append(",");
                    }
                }
            }
            Json.Append("]");
            return Json.ToString();
        }

        /// <summary>
        /// 格式化字符型、日期型、布尔型
        /// </summary>
        /// <param name="str"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        private static string StringFormat(string str, Type type)
        {
            if (type != typeof(string) && string.IsNullOrEmpty(str))
            {
                str = "\"" + str + "\"";
            }
            else if (type == typeof(string))
            {
                str = "\"" + str + "\"";
            }
            else if (type == typeof(DateTime))
            {
                str = "\"" + str + "\"";
            }
            else if (type == typeof(bool))
            {
                str = str.ToLower();
            }
            else if (type == typeof(byte[]))
            {
                str = "\"" + str + "\"";
            }
            else if (type == typeof(Guid))
            {
                str = "\"" + str + "\"";
            }
            return str;
        }

        /// <summary>
        /// 格式化字符型、日期型、布尔型
        /// </summary>
        /// <param name="str"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        public static string StringFormatS(string str, Type type)
        {
            if (type != typeof(string) && string.IsNullOrEmpty(str))
            {
                str = "\"" + str + "\"";
            }
            else if (type == typeof(string))
            {
                str = "\"" + str + "\"";
            }
            else if (type == typeof(DateTime))
            {
                str = "\"" + str + "\"";
            }
            else if (type == typeof(bool))
            {
                str = str.ToLower();
            }
            else if (type == typeof(byte[]))
            {
                str = "\"" + str + "\"";
            }
            else if (type == typeof(Guid))
            {
                str = "\"" + str + "\"";
            }
            return str;
        }

		#region 【通用方法】错误写入文本
		public void WriteTxt(string Message)
		{
			string pathFile = System.Windows.Forms.Application.StartupPath + @"\Error";
			if (!Directory.Exists(pathFile))
				Directory.CreateDirectory(pathFile);
			string fileName = System.Windows.Forms.Application.StartupPath + @"\Error\"+DateTime.Now.ToString("yyyyMM")+".txt";
			FileStream fs = new FileStream(fileName, FileMode.Append);
			StreamWriter sw = new StreamWriter(fs);
			sw.WriteLine(Message + "\r\n" + DateTime.Now);
			sw.Flush();
			sw.Close();
			fs.Close();
		}
		#endregion
	}
}
