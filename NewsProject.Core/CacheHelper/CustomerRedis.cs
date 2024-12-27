
using Microsoft.Extensions.Caching.Distributed;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace BusinessAccount.CacheHelper
{
    public class CustomerRedis: IRedisClient
    {
        readonly string _prefix;

        public CustomerRedis(string prefix)
        {
            _prefix = prefix;
        }

        private string FullKey(string key)
        {
            return _prefix + key;
        }

        public string GetRedis(string key)
        {
            var result = RedisHelper.Get(FullKey(key));
            if (result == null)
            {
                result = RedisHelper.Get("KuaiWin_" + key);
            }
            return result;
        }

        public T Get<T>(string key) where T : new()
        {
            var result = RedisHelper.Get<T>(FullKey(key));
            if (result == null)
            {
                result = RedisHelper.Get<T>("KuaiWin_" + key);
            }
            return result;
        }

        public async Task<string> GetAsync(string key)
        {
            var result = await RedisHelper.GetAsync(FullKey(key));
            if (result == null)
            {
                result = await RedisHelper.GetAsync(key);
                if (result == null)
                {
                    result = await RedisHelper.GetAsync("KuaiWin_" + key);
                }
            }
            return result;
        }
        //public async Task<string> GetNoPerfixAsync(string key)
        //{
        //    var result = await RedisHelper.GetAsync(key);
        //    if (result == null)
        //    {
        //        result = await RedisHelper.GetAsync("KuaiWin_" + key);
        //    }
        //    return result;
        //}

        //public async Task<string> GetAsync(string key)
        //{
        //    var result = await RedisHelper.GetAsync(FullKey(key));
        //    if (result == null)
        //    {
        //        result = await RedisHelper.GetAsync("KuaiWin_" + key);
        //    }
        //    return result;
        //}

        public async Task<T> GetAsync<T>(string key) where T : new()
        {
            var result = await RedisHelper.GetAsync<T>(FullKey(key));
            if (result == null)
            {
                result = await RedisHelper.GetAsync<T>("KuaiWin_" + key);
            }
            return result;
        }

        //public async Task SetNoPerfixAsync(string key, object t, int expiresSec = 0)
        //{
        //    await RedisHelper.SetAsync(key, t, expiresSec);
        //}
        public async Task SetAsync(string key, object t, TimeSpan expiresSec)
        {
            await RedisHelper.SetAsync(FullKey(key), t, expiresSec);
        }
        public async Task SetAsync(string key, object t, int expiresSec = 0)
        {
            await RedisHelper.SetAsync(FullKey(key), t, expiresSec);
        }

        //public async Task SetNoPerfixAsync(string key, object t, TimeSpan expiresSec)
        //{
        //    await RedisHelper.SetAsync(key, t, expiresSec);
        //}

        public void Set(string key, object t, int expiresSec = 0)
        {
            RedisHelper.Set(FullKey(key), t, expiresSec);
        }

        public async Task<long> DelAsync(string key)
        {
            //await SetAsync(key, null, -1);
            //await SetAsync(key, "", -1);
            var result = await RedisHelper.DelAsync(key);
            if (result == null || result == 0)
            {
                result = await RedisHelper.DelAsync(FullKey(key));
                if (result == null || result == 0)
                {
                    result = await RedisHelper.DelAsync("KuaiWin_" + key);
                }
            }
            return result;
        }

        //public async Task<long> DelNoPerfixAsync(string key)
        //{
        //    await SetNoPerfixAsync(key, null, -1);
        //    await SetNoPerfixAsync(key, "", -1);
        //    var result = await RedisHelper.DelAsync(key);
        //    if (result == null || result == 0)
        //    {
        //        result = await RedisHelper.DelAsync("KuaiWin_" + key);
        //    }
        //    return result;
        //}

        public long Del(string key)
        {
            Set(key, null, -1);
            Set(key, "", -1);
            var result = RedisHelper.Del(FullKey(key));
            if (result == null || result == 0)
            {
                result = RedisHelper.Del("KuaiWin_" + key);
            }
            return result;
        }

        /// <summary>
        /// 批量删除
        /// </summary>
        /// <param name="keys"></param>
        /// <returns></returns>
        public async Task<long> DelAsync(string[] keys)
        {
            if (keys == null || keys.Length == 0) return 0;
            var keyList = new ArrayList(keys);
            while (keyList != null && keyList.Count > 0)
            {
                var result = await RedisHelper.DelAsync(FullKey(keyList[0].ToString()));
                if (result == null)
                {
                    result = await RedisHelper.DelAsync("KuaiWin_" + keyList[0].ToString());
                }
                keyList.RemoveAt(0);
            }

            return 1;
        }


        public async Task<T> CacheShellAsync<T>(string key, int timeoutSeconds, Func<Task<T>> getDataAsync)
        {
            return await RedisHelper.CacheShellAsync(FullKey(key), timeoutSeconds, getDataAsync);
        }

        public T CacheShell<T>(string key, int timeoutSeconds, Func<T> getDataAsync)
        {
            return RedisHelper.CacheShell(FullKey(key), timeoutSeconds, getDataAsync);
        }

        public async Task<bool> ExpireAsync(string key, int seconds)
        {
            return await RedisHelper.ExpireAsync(FullKey(key), seconds);
        }

        /// <summary>
        /// 根据匹配模式获取到所有匹配到的key
        /// </summary>
        /// <param name="keyStr"></param>
        /// <returns></returns>
        public async Task<string[]> GetKeys(string keyStr)
        {
            return await RedisHelper.KeysAsync(keyStr);
        }

        /// <summary>
        /// 根据匹配模式获取到所有匹配到的key
        /// </summary>
        /// <param name="keyStr"></param>
        /// <returns></returns>
        public async Task<string[]> GetKeys(string[] keyStr)
        {
            var result = new List<string>();
            for (int i = 0; i < keyStr.Length; i++)
            {
                var keys = await RedisHelper.KeysAsync("KuaiWin_" + keyStr[i]);
                if (keys != null && keys.Length > 0)
                    result.AddRange(keys);
            }
            return result.ToArray();
        }


        #region 分布式缓存使用（未实现）
        public Task<byte[]> GetAsync(string key, CancellationToken token = default)
        {
            return null;
        }

        public void Refresh(string key)
        {
            throw new NotImplementedException();
        }

        public Task RefreshAsync(string key, CancellationToken token = default)
        {
            throw new NotImplementedException();
        }

        public void Remove(string key)
        {
            throw new NotImplementedException();
        }

        public Task RemoveAsync(string key, CancellationToken token = default)
        {
            throw new NotImplementedException();
        }

        public void Set(string key, byte[] value, DistributedCacheEntryOptions options)
        {
            throw new NotImplementedException();
        }

        public Task SetAsync(string key, byte[] value, DistributedCacheEntryOptions options, CancellationToken token = default)
        {
            throw new NotImplementedException();
        }

        byte[] IDistributedCache.Get(string key)
        {
            throw new NotImplementedException();
        }
        #endregion
    }
}
