using BusinessAccount.Api.Models;
using BusinessAccount.Core.Data;
using BusinessAccount.Extensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using NewsProject.Application.Contracts.Login;
using NewsProject.Application.Contracts.User;
using NewsProject.Core.Entities;
using System.Security.Claims;
using System.Security.Principal;
using System.Threading.Tasks;

namespace NewsProject.Api.Controllers
{
    [ApiController]
    [Route("api")]
    public class LoginController : ControllerBase
    {
        private readonly ILoginService _loginService;
        private readonly IDistributedCache _redisClient;
        private readonly IConfiguration _configuration;
        private readonly JwtConfig _jwtModel = null;
        public LoginController(ILoginService loginService, IOptions<JwtConfig> jwtModel, IDistributedCache redisClient, IConfiguration configuration)
        {
            _loginService = loginService;
            _jwtModel = jwtModel.Value;
            _redisClient = redisClient;
            _configuration = configuration;
        }

        /// <summary>
        /// 登录接口    
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [Route("user/login")]
        public async Task<ResultApiModel> GetLogin(UserLoginDto userLogin)
        {
            //验证账号密码
            LoginDto baseResult = await _loginService.GetLogin(userLogin);

            if (baseResult != null)
            {
                //获取claims
                var claims = JwtExtensions.Generateclaims(baseResult);

                //获取 token
                string token = JwtExtensions.GenerateTokens(_jwtModel, claims);

                //加入缓存中
                await _redisClient.SetStringAsync("token", token, new DistributedCacheEntryOptions
                {
                    AbsoluteExpirationRelativeToNow = Constant.QxInfoOverExpiredTime
                });

                //拼接返回dto
                LoginResultDto loginResult = new LoginResultDto()
                {
                    name = baseResult.name,
                    account = baseResult.account,
                    username = baseResult.roleaccount,
                    token = token
                };

                return new ResultApiModel()
                {
                    code = 200,
                    data = loginResult,
                    msg = "登录成功！"
                };
            }
            else
            {
                return new ResultApiModel()
                {
                    code = 500,
                    msg = "账号或密码错误！"
                };
            }
        }
    }
}
