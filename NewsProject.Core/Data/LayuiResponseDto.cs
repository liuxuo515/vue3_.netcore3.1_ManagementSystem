using BusinessAccount.Data.Enums;
using Microsoft.AspNetCore.Http;
using System;
using System.Text;

namespace BusinessAccount.Data
{
    public class LayuiResponseDto : UnifyResponse<LayuiResponseDto>
    {
        #region 构造函数
        public LayuiResponseDto() { }

        public LayuiResponseDto(ErrorCode errorCode) : base(errorCode) { }

        public LayuiResponseDto(ErrorCode errorCode, object message) : base(errorCode, message) { }

        public LayuiResponseDto(ErrorCode errorCode, object message, object datas = null, long total = 0) : base(errorCode, message)
        {
            data = datas;
            count = total;
        }

        public LayuiResponseDto(ErrorCode errorCode, object message, HttpContext httpContext) : base(errorCode, message, httpContext) { }
        #endregion

        /// <summary>
        ///错误码
        /// </summary>
        public ErrorCode code { get { return base.Code; } set { base.Code = value; } }
        /// <summary>
        ///错误信息
        /// </summary>
        public object message { get { return base.Message; } set { base.Message = value; } }
        /// <summary>
        ///请求地址
        /// </summary>
        public string request { get { return base.Request; } set { base.Request = value; } }

        /// <summary>
        /// 返回数据
        /// </summary>
        public object data { get; set; }

        /// <summary>
        /// 数据长度
        /// </summary>
        public long count { get; set; }


        public static LayuiResponseDto Success(string message = "操作成功", object datas = null, long total = 0)
        {
            return new LayuiResponseDto(ErrorCode.Success, message, datas, total);
        }

        public static LayuiResponseDto Error(string message = "操作失败", object datas = null)
        {
            return new LayuiResponseDto(ErrorCode.Fail, message, datas);
        }
    }
}
