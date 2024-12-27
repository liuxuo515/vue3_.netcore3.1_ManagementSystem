using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessAccount.Core.Data
{
    public static class Constant
    {
        /// <summary>
        /// 登录到期时间
        /// </summary>
        public static TimeSpan ExpireTimeSpan { get; } = new TimeSpan(0, 6, 0, 0);

        /// <summary>
        /// 旧的需要使用的token的有效期
        /// add by 张家铭 2021.11.8
        /// </summary>
        public static TimeSpan OldTokenExpireTimeSpan { get; } = new TimeSpan(0, 0, 1, 0);
        /// <summary>
        /// 验证码过期时间 2分钟
        /// lq 2024-08-21
        /// </summary>
        public static TimeSpan FailureExpirationTime { get; } = new TimeSpan(0, 0, 2, 0);

        /// <summary>
        /// 企信过期时间 30天
        /// lq 2024-08-21
        /// </summary>
        public static TimeSpan QxInfoOverExpiredTime { get; } = new TimeSpan(30, 0, 0, 0);
    }
}
