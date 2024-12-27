using Microsoft.Extensions.Configuration;
using System;
using System.IO;

namespace BusinessAccount
{
    public class AppsettingsManager
    {
        public readonly static IConfiguration Configuration;

        static AppsettingsManager()
        {
            //Configuration = new ConfigurationBuilder()
            //    .SetBasePath(Directory.GetCurrentDirectory())
            //    .AddJsonFile("appsettings.json", optional: true)
            //    .Build();
        }
        public static string GetAppSettings(string key)
        {
            var childrenSection = Configuration.GetSection("AppSettings").GetSection(key);
            return childrenSection.Value;
        }
        public static string GetCaseManageJob(string key)
        {
            var childrenSection = Configuration.GetSection("CaseManageJob").GetSection(key);
            return childrenSection.Value;
        }

        public static int GetIntAppSettings(string key)
        {
            var childrenSection = Configuration.GetSection("AppSettings").GetSection(key);
            int value;
            int.TryParse(childrenSection.Value, out value);
            return value;
        }
        public static string GetPaySettings(string payKey, string key)
        {
            var childrenSection = Configuration.GetSection(payKey).GetSection(key);
            return childrenSection.Value;
        }
    }
}
