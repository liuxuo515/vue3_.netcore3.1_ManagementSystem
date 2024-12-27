using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessAccount.Core.Data
{
    public class BaseResultApiModel
    {

        /// <summary>
        /// 200 成功 !200失败
        /// </summary>
        public int? code { get; set; } = 200;
        public string? msg { get; set; } = "success";
        /// <summary>
        /// 返回结果
        /// </summary>
        public BaseDataApiModel data { get; set; }
    }
    public class BaseDataApiModel
    {
        public long total { get; set; }
        public object list { get; set; }
    }


    public class ResultApiModel
    {
        /// <summary>
        /// 200 成功 !200失败
        /// </summary>
        public int? code { get; set; } = 200;
        public string? msg { get; set; } = "success";
        /// <summary>
        /// 返回结果
        /// </summary>
        public object data { get; set; }
    }

}
