namespace BusinessAccount.Api.Models
{
    public class JwtConfig
    {
        /// <summary>
        /// 密钥
        /// </summary>
        public string SecurityKey { get; set; }
        /// <summary>
        /// 所属者
        /// </summary>
        public string Issuer { get; set; }

        public string Audience { get; set; }

        /// <summary>
        /// 过期时间
        /// </summary>
        public int Expiration { get; set; }

    }

    public class claimsConfig
    {
        public string userid { get; set; }
        public string username { get; set; }
        public string userphone { get; set; }
        public string useremail { get; set; }
    }
}
