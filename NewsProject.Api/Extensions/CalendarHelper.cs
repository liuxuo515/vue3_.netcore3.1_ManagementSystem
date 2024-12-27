using Newtonsoft.Json.Linq;
using System.IO;
using System.Net;
using System.Text;
using System;

namespace NewsProject.Api.Extensions
{
    public class CalendarHelper
    {
        /// <summary>
        /// 天聚数行--中国老黄历
        /// </summary>
        /// <returns></returns>
        public string GetCalendar(string data)
        {
            if (string.IsNullOrEmpty(data))
                data = DateTime.Now.ToString("yyyy-MM-dd");
            string url = "https://apis.tianapi.com/lunar/index?key=73cd8bc13afd0652154be983a9e4f9c4&date=" + data;
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            request.Method = "GET";
            request.ContentType = "application/x-www-form-urlencoded";
            //using (Stream stream = request.GetRequestStream())
            //{
            //    byte[] requestBodyBytes = Encoding.UTF8.GetBytes(requestBody);
            //    stream.Write(requestBodyBytes, 0, requestBodyBytes.Length);
            //}
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            using (Stream stream = response.GetResponseStream())
            {
                StreamReader reader = new StreamReader(stream);
                string tianapiResponseBody = reader.ReadToEnd();
                JObject tianapiJsonResponse = JObject.Parse(tianapiResponseBody);
                try
                {
                    string result = tianapiJsonResponse["result"].ToString();
                    return result;
                }
                catch (Exception)
                {
                    string result = "出行";
                    return result;
                }

            }
        }


        /// <summary>
        /// https://www.mxnzp.com/--日历接口
        /// </summary>
        /// <returns></returns>
        public string Get_mxnzp_Calendar(string data)
        {
            if (string.IsNullOrEmpty(data))
                data = DateTime.Now.ToString("yyyyMMdd");
            string url = "https://www.mxnzp.com/api/holiday/single/"+ data + "?app_id=kvbtlnriwdijeild&app_secret=3yYEemd8P0ojolxIGkzceSiamO6UwUe3";
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            request.Method = "GET";
            request.ContentType = "application/x-www-form-urlencoded";
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            using (Stream stream = response.GetResponseStream())
            {
                StreamReader reader = new StreamReader(stream);
                string tianapiResponseBody = reader.ReadToEnd();
                JObject tianapiJsonResponse = JObject.Parse(tianapiResponseBody);
                try
                {
                    string result = tianapiJsonResponse["data"].ToString();
                    return result;
                }
                catch (Exception)
                {
                    string result = "出行";
                    return result;
                }

            }
        }
    }
}
