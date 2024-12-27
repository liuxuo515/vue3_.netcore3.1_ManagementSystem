using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;

namespace NewsProject.Core
{
    public static class ConvertedLunar
    {
        /// <summary> 
        /// 获取对应日期的农历 
        /// </summary> 
        /// <param name="date">公历日期</param> 
        ///<returns></returns> 
        public static string GetLunarCalendar(DateTime date)
        {
            System.Globalization.ChineseLunisolarCalendar chineseDate = new System.Globalization.ChineseLunisolarCalendar();
            string[] TianGan = { "甲", "乙", "丙", "丁", "戊", "己", "庚", "辛", "壬", "癸" };
            //地支 
            string[] DiZhi = { "子", "丑", "寅", "卯", "辰", "巳", "午", "未", "申", "酉", "戌", "亥" };
            //十二生肖 
            string[] ShengXiao = { "鼠", "牛", "虎", "兔", "龙", "蛇", "马", "羊", "猴", "鸡", "狗", "猪" };
            //农历日期 
            string[] DayName = { "*", "初一", "初二", "初三", "初四", "初五", "初六", "初七", "初八", "初九", "初十", "十一", "十二", "十三", "十四", "十五", "十六", "十七", "十八", "十九", "二十", "廿一", "廿二", "廿三", "廿四", "廿五", "廿六", "廿七", "廿八", "廿九", "三十" };
            //农历月份
            string[] MonthName = { "*", "正", "二", "三", "四", "五", "六", "七", "八", "九", "十", "十一", "腊" };
            //公历月计数天 

            string[] romeNumbers = { "0", "1", "2", "3", "4", "5", "6", "7", "8", "9" };
            string[] chineseNumbers = { "O", "一", "二", "三", "四", "五", "六", "七", "八", "九" };

            int lYear = chineseDate.GetYear(date);
            int lMonth = chineseDate.GetMonth(date);
            int lDay = chineseDate.GetDayOfMonth(date);

            /** GetLeapMonth(int year)方法返回一个1到13之间的数字，
             * 比如：1、该年阴历2月有闰月，则返回3
             * 如果：2、该年阴历8月有闰月，则返回9
             * GetMonth(DateTime dateTime)返回是当前月份，忽略是否闰月
             * 比如：1、该年阴历2月有闰月，2月返回2，闰2月返回3
             * 如果：2、该年阴历8月有闰月，8月返回8，闰8月返回9
             */
            int leapMonth = chineseDate.GetLeapMonth(lYear);//获取第几个月是闰月,等于0表示本年无闰月 

            string SYear = "";
            foreach (char c in lYear.ToString())
            {
                var index = romeNumbers.ToList().FindIndex(w => w == c.ToString());
                SYear += (index > -1) ? chineseNumbers[index] : c.ToString();
            }
            string SMonth = string.Empty;
            //如果今年有闰月 
            if (leapMonth > 0)
            {
                //闰月数等于当前月份 
                if (lMonth == leapMonth) SMonth = "闰" + MonthName[lMonth - 1];
                else if (lMonth > leapMonth) SMonth = MonthName[lMonth - 1];
                else SMonth = MonthName[lMonth];
            }
            else
            {
                SMonth = MonthName[lMonth];
            }

            string sDay = DayName[lDay];
            string result = string.Empty;
            result += TianGan[(lYear - 4) % 60 % 10]; //天干
            result += DiZhi[(lYear - 4) % 60 % 12]; //地支
            result += " ";
            result += ShengXiao[(lYear - 4) % 60 % 12];
            result += " ";

            result += SYear + "年";
            result += SMonth + "月";
            result += sDay;

            string resultMD = string.Empty;
            resultMD += SMonth + "月";
            resultMD += sDay;

            return resultMD;


        }
        /// <summary>
        /// 获取对应日期的农历 
        /// </summary>
        /// <param name="date">公历日期</param>
        /// <returns></returns>
        public static string GetLunarCalendar(string date)
        {
            DateTime tempdate = DateTime.MinValue;
            DateTime.TryParse(date, out tempdate);
            if (tempdate == DateTime.MinValue) return string.Empty;
            return GetLunarCalendar(tempdate);
        }
    }
}
