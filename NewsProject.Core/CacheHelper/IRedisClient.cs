using Microsoft.Extensions.Caching.Distributed;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BusinessAccount.CacheHelper
{
    public interface IRedisClient: IDistributedCache
    {
        string GetRedis(string key);
        Task<string> GetAsync(string key);
        void Set(string key, object t, int expiresSec = 0);
        Task SetAsync(string key, object t, TimeSpan expiresSec);
        Task SetAsync(string key, object t, int expiresSec = 0);
        //Task SetNoPerfixAsync(string key, object t, TimeSpan expiresSec);
        //Task SetNoPerfixAsync(string key, object t, int expiresSec = 0);
        T Get<T>(string key) where T : new();
        Task<T> GetAsync<T>(string key) where T : new();

        Task<long> DelAsync(string key);

        //Task<long> DelNoPerfixAsync(string key);

        long Del(string key);

        /// <summary>
        /// 批量删除
        /// </summary>
        /// <param name="keys"></param>
        /// <returns></returns>
        Task<long> DelAsync(string[] keys);
        /// <summary>
        /// 缓存壳
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <param name="timeoutSeconds"></param>
        /// <param name="getDataAsync"></param>
        /// <returns></returns>
        Task<T> CacheShellAsync<T>(string key, int timeoutSeconds, Func<Task<T>> getDataAsync);


        /// <summary>
        /// 缓存壳
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <param name="timeoutSeconds"></param>
        /// <param name="getDataAsync"></param>
        /// <returns></returns>
        T CacheShell<T>(string key, int timeoutSeconds, Func<T> getData);

        /// <summary>
        /// 为指定的key设置过期时间
        /// </summary>
        /// <param name="key"></param>
        /// <param name="seconds"></param>
        /// <returns></returns>
        Task<bool> ExpireAsync(string key, int seconds);

        /// <summary>
        /// 根据匹配模式获取到所有匹配到的key
        /// </summary>
        /// <param name="keyStr"></param>
        /// <returns></returns>
        Task<string[]> GetKeys(string keyStr);

        /// <summary>
        /// 根据匹配模式获取到所有匹配到的key
        /// </summary>
        /// <param name="keyStr"></param>
        /// <returns></returns>
        Task<string[]> GetKeys(string[] keyStr);
    }
}
