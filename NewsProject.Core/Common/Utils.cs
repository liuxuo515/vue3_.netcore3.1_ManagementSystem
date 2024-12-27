using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using BusinessAccount.Extensions;
using Microsoft.AspNetCore.Http;

namespace BusinessAccount.Common
{
    public static class Utils
    {
        public static string[] GetProxyArray()
        {
            var result = new List<string>() {
                //"120.43.97.181:8888","139.203.7.15:9999","119.112.206.151:9999","175.21.219.95:8888"
                "223.243.69.174:4267","180.122.104.101:4257","27.156.197.187:4245"
            };

            return result.ToArray();

            //var result = new List<string>();
            //var ips = new[] { "127.0.0.1", "172.16.41.237" };
            //var portMin = 21500;
            //var portMax = 22100;

            //ips.ForEach(ip =>
            //{
            //    var current = portMin;
            //    while (current < portMax)
            //    {
            //        result.Add($"{ip}:{current}");
            //        current++;
            //    }
            //});

            //return result.ToArray();
        }

        /// <summary>
        /// 继承HashAlgorithm
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="stream"></param>
        /// <returns></returns>
        public static string GetHash<T>(Stream stream) where T : HashAlgorithm
        {
            StringBuilder sb = new StringBuilder();

            MethodInfo create = typeof(T).GetMethod("Create", new Type[] { });
            using (T crypt = (T)create.Invoke(null, null))
            {
                if (crypt != null)
                {
                    byte[] hashBytes = crypt.ComputeHash(stream);
                    foreach (byte bt in hashBytes)
                    {
                        sb.Append(bt.ToString("x2"));
                    }
                }
            }
            return sb.ToString();
        }

        public static string GetRequest(HttpContext httpContext)
        {
            return httpContext.Request.Method + " " + httpContext.Request.Path;
        }

        /// <summary>
        /// 获取时间描述（x小时x分）
        /// </summary>
        /// <param name="second"></param>
        /// <returns></returns>
        public static string GetTimeDiffer(int second)
        { 

            //将秒数转化为时分秒 duration为秒数 
                TimeSpan ts = new TimeSpan(0, 0, second);
                int _hours = 0;
                if (ts.Days > 0)
                {
                    _hours = ts.Days * 24;
                }
                string str = "";
                if (ts.Hours > 0)
                {

                    str = String.Format("{0:00}", ts.Hours + _hours) + "小时" + String.Format("{0:00}", ts.Minutes) + "分" + String.Format("{0:00}", ts.Seconds) + "秒";
                }
                if (ts.Hours == 0 && ts.Minutes > 0)
                {
                    str = "00小时";
                    if (_hours > 0)
                    {
                        str = String.Format("{0:00}", ts.Hours + _hours) + "小时";
                    }
                    str += String.Format("{0:00}", ts.Minutes) + "分" + String.Format("{0:00}", ts.Seconds) + "秒";
                }
                if (ts.Hours == 0 && ts.Minutes == 0)
                {
                    str = "00小时";
                    if (_hours > 0)
                    {
                        str = String.Format("{0:00}", ts.Hours + _hours);
                    }
                    str += "00分" + String.Format("{0:00}", ts.Seconds) + "秒";
                }
                return str; 
             
        }

        /// <summary>
        /// 获取时间描述（x小时x分）
        /// </summary>
        /// <param name="second"></param>
        /// <returns></returns>
        public static string GetTimeDiffer(long second)
        {
            if (second > 60 * 60)
                return second / 3600 + "小时" + second / 60 + "分钟" + second % 60 + "秒";
            if (second > 60)
                return second / 60 + "分钟" + second % 60 + "秒";
            if (second > 0)
                return second + "秒";
            return "";
        }

        public static string GetTimeDifferNow(DateTime dt)
        {
            TimeSpan span = DateTime.Now - dt;

            if (span.TotalDays > 60)
            {
                return dt.ToDateString();
            }

            if (span.TotalDays > 30)
            {
                return "1个月前";
            }

            if (span.TotalDays > 14)
            {
                return "2周前";
            }

            if (span.TotalDays > 7)
            {
                return "1周前";
            }

            if (span.TotalDays > 1)
            {
                return $"{(int)Math.Floor(span.TotalDays)}天前";
            }

            if (span.TotalHours > 1)
            {
                return $"{(int)Math.Floor(span.TotalHours)}小时前";
            }

            if (span.TotalMinutes > 1)
            {
                return $"{(int)Math.Floor(span.TotalMinutes)}分钟前";
            }

            if (span.TotalSeconds >= 1)
            {
                return $"{(int)Math.Floor(span.TotalSeconds)}秒前";
            }

            return "1秒前";
        }

        /// <summary>
        /// 校验手机号码是否符合标准。
        /// </summary>
        /// <param name="mobile"></param>
        /// <returns></returns>
        public static bool ValidateMobile(string mobile)
        {
            if (string.IsNullOrEmpty(mobile))
                return false;
            return Regex.IsMatch(mobile, @"^(13|14|15|16|18|19|17)\d{9}$");
        }

        /// <summary>
        ///   时间戳转本地时间
        /// </summary> 
        public static DateTime ToLocalDateTime(this long unix, bool allowZero = false)
        {
            if (!allowZero && unix <= 0)
                throw new ArgumentException("时间戳值为 0 。", nameof(unix));

            var dto = DateTimeOffset.FromUnixTimeSeconds(unix);
            return dto.ToLocalTime().DateTime;
        }

        /// <summary>
        ///   时间戳转本地时间
        /// </summary> 
        public static DateTime ToLocalDateTime(this long unix)
        {
            var dto = DateTimeOffset.FromUnixTimeSeconds(unix);
            return dto.ToLocalTime().DateTime;
        }

        /// <summary>
        ///   时间转时间戳转
        /// </summary> 
        public static long ToUnixTimestamp(this DateTime dt)
        {
            DateTimeOffset dto = new DateTimeOffset(dt);
            return dto.ToUnixTimeSeconds();
        }
    }
}
