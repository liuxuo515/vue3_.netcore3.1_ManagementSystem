using BusinessAccount.Common;
using BusinessAccount.Data.Enums;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System;

namespace BusinessAccount.Data
{
    public class UnifyResponse<T> where T : UnifyResponse<T>, new()
    {
        /// <summary>
        ///错误码
        /// </summary>
        protected ErrorCode Code { get; set; }

        /// <summary>
        ///错误信息
        /// </summary>
        protected object Message { get; set; }

        /// <summary>
        ///请求地址
        /// </summary>
        protected string Request { get; set; }

        public UnifyResponse(ErrorCode errorCode, object message, HttpContext httpContext)
        {
            Code = errorCode;
            Message = message;
            Request = Utils.GetRequest(httpContext);
        }

        public UnifyResponse(ErrorCode errorCode, object message)
        {
            Code = errorCode;
            Message = message ?? throw new ArgumentNullException(nameof(message));
        }

        public UnifyResponse(ErrorCode errorCode)
        {
            Code = errorCode;
        }

        public UnifyResponse()
        {
        }

        public static T Success(string message = "操作成功")
        {
            return (new UnifyResponse<T>(ErrorCode.Success, message)) as T;
        }

        public static T Error(string message = "操作失败")
        {
            return (new UnifyResponse<T>(ErrorCode.Fail, message)) as T;
        }

        public override string ToString()
        {
            return JsonConvert.SerializeObject(this, new JsonSerializerSettings()
            {
                ContractResolver = new DefaultContractResolver()
                {
                    NamingStrategy = new SnakeCaseNamingStrategy()
                }
            });
        }
    }
}
