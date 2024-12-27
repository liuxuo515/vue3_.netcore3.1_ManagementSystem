using System.ComponentModel;

namespace BusinessAccount.Data.Enums
{
    public enum ErrorCode
    {
        /// <summary>
        /// 操作成功
        /// </summary>
        Success = 200,
        /// <summary>
        /// 未知错误
        /// </summary>
        UnknownError = 1007,
        /// <summary>
        /// 服务器未知错误
        /// </summary>
        ServerUnknownError = 999,

        /// <summary>
        /// 失败
        /// </summary>
        Error = 1000,

        /// <summary>
        /// 认证失败
        /// </summary>
        AuthenticationFailed = 10000,
        /// <summary>
        /// 无权限
        /// </summary>
        NoPermission = 10001,
        /// <summary>
        /// 失败
        /// </summary>
        Fail = 9999,
        /// <summary>
        /// refreshToken异常
        /// </summary>
        RefreshTokenError = 10100,
        /// <summary>
        /// 资源不存在
        /// </summary>
        NotFound = 10020,
        /// <summary>
        /// 参数错误
        /// </summary>
        [Description("参数错误")]
        ParameterError = 10030,
        /// <summary>
        /// 令牌失效
        /// </summary>
        [Description("令牌失效")]
        TokenInvalidation = 10040,
        /// <summary>
        /// 令牌过期
        /// </summary>
        TokenExpired = 10050,
        /// <summary>
        /// 字段重复
        /// </summary>
        RepeatField = 10060,
        /// <summary>
        /// 禁止操作
        /// </summary>
        Inoperable = 10070,
        //10080 请求方法不允许

        //10110 文件体积过大

        //10120 文件数量过多

        //10130 文件扩展名不符合规范

        //10140 请求过于频繁，请稍后重试
        ManyRequests = 10140
    }


    /// <summary>
    /// 债务人类型
    /// </summary>
    public enum DebtorType
    {

        /// <summary>
        /// 全部
        /// </summary>
        [Description("Whole")]
        全部 = 0,

        /// <summary>
        /// 企业
        /// </summary>
        [Description("Enterprise")]
        企业 = 1,

        /// <summary>
        /// 个人
        /// </summary>
        [Description("Person")]
        个人 = 2
    }

    /// <summary>
    /// 运作状态
    /// </summary>
    public enum OperateStatus
    {

        /// <summary>
        /// 全部
        /// </summary>
        [Description("Whole")]
        全部 = 0,

        /// <summary>
        /// 待分配
        /// </summary>
        [Description("NoAssigned")]
        待分配 = 1,

        /// <summary>
        /// 已分配
        /// </summary>
        [Description("Assigned")]
        已分配 = 2
    }

    /// <summary>
    /// 回款方式
    /// </summary>
    public enum CollectionType
    {
        /// <summary>
        /// 电汇
        /// </summary>
        [Description("Transfer")]
        电汇 = 1,

        /// <summary>
        /// 银行承兑汇票
        /// </summary>
        [Description("Bank")]
        银行承兑汇票 = 2,

        /// <summary>
        /// 商业承兑汇票
        /// </summary>
        [Description("Business")]
        商业承兑汇票 = 3,

        /// <summary>
        /// 以物抵债
        /// </summary>
        [Description("Repayment")]
        以物抵债 = 4
    }

    /// <summary>
    /// 运作环节
    /// </summary>
    public enum OperationType
    {
        ///// <summary>
        ///// 非诉调解阶段
        ///// </summary>
        //[Description("MediationStage")]
        //非诉调解阶段 = 1,

        ///// <summary>
        ///// 诉讼立案阶段
        ///// </summary>
        //[Description("Register")]
        //诉讼立案阶段 = 2,

        ///// <summary>
        ///// 一审阶段
        ///// </summary>
        //[Description("FirstInstance")]
        //一审阶段 = 3,

        ///// <summary>
        ///// 二审阶段
        ///// </summary>
        //[Description("SecondInstance")]
        //二审阶段 = 4,

        ///// <summary>
        ///// 执行阶段
        ///// </summary>
        //[Description("Execute")]
        //执行阶段 = 5,

        ///// <summary>
        ///// 结案阶段
        ///// </summary>
        //[Description("Close")]
        //结案阶段 = 6

        /// <summary>
        /// 非诉阶段
        /// </summary>
        [Description("MediationStage")]
        非诉阶段 = 1,

        /// <summary>
        /// 诉讼阶段
        /// </summary>
        [Description("Register")]
        诉讼阶段 = 2,

        /// <summary>
        /// 执行阶段
        /// </summary>
        [Description("Execute")]
        执行阶段 = 3,

        /// <summary>
        /// 破产阶段
        /// </summary>
        [Description("BankruptcyStage")]
        破产阶段 = 4,

        /// <summary>
        /// 刑事控告
        /// </summary>
        [Description("CriminalCharges")]
        刑事控告 = 5,

        ///// <summary>
        ///// 结案
        ///// </summary>
        //[Description("Closed")]
        //结案 = 6,

        ///// <summary>
        ///// 关闭
        ///// </summary>
        //[Description("Close")]
        //关闭案件 = 7,

    }

    /// <summary>
    /// 运作状态
    /// </summary>
    public enum OperationalStatus
    {
        /// <summary>
        /// 运作中
        /// </summary>
        [Description("Operational")]
        运作中 = 1,

        /// <summary>
        /// 结案
        /// </summary>
        [Description("Closed")]
        结案 = 2,

        /// <summary>
        /// 关闭
        /// </summary>
        [Description("ShutDown")]
        关闭 = 3,
    }

    /// <summary>
    /// 案件类型
    /// </summary>
    public enum CaseStuas
    {
        /// <summary>
        /// 非诉讼案件
        /// </summary>
        [Description("NonLitigation")]
        非诉讼案件 = 1,

        /// <summary>
        /// 诉讼案件
        /// </summary>
        [Description("Proceeding")]
        诉讼案件 = 2,
    }
}
