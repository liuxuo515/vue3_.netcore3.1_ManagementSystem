
using AspNetCoreRateLimit;
using CSRedis;
using BusinessAccount.CacheHelper;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Caching.Redis;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace BusinessAccount.Extensions
{
    public static class DependencyInjectionExtensions
    {
        #region 初始化 Redis配置
        public static void AddCsRedisCore(this IServiceCollection services, IConfiguration configuration)
        {
            IConfigurationSection csRediSection = configuration.GetSection("ConnectionStrings:CsRedis");
            CSRedisClient csRedisClient = new CSRedisClient(csRediSection.Value);
            //初始化 RedisHelper
            RedisHelper.Initialization(csRedisClient);
            //注册mvc分布式缓存
            services.AddSingleton<IDistributedCache>(new CSRedisCache(RedisHelper.Instance));
        }
        #endregion

    }
}
