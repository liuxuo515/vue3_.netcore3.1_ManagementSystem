using BusinessAccount.Core.Data;
using Microsoft.IdentityModel.Tokens;
using NewsProject.Application.Contracts.Login;
using NewsProject.Application.Contracts.User;
using NewsProject.Core.Entities;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IdentityModel.Tokens.Jwt;
using System.Reflection;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace NewsProject.Application.Login
{
    public class LoginService : ILoginService
    {
        private readonly IFreeSql _freeSql;
        public LoginService(IFreeSql freeSql)
        {
            _freeSql = freeSql;
        }
        /// <summary>
        /// 登录
        /// </summary>
        /// <param name="UserPhone"></param>
        /// <returns></returns>
        public async Task<LoginDto> GetLogin(UserLoginDto userLogin)
        {
            try
            {
                LoginDto login = await _freeSql
                    .Select<tsUserEntity, tsUserRoleEntity, tsRoleEntity>()
                    .LeftJoin((a, b, c) => a.Id == b.UserId)
                    .LeftJoin((a, b, c) => b.RoleId == c.Id)
                    .Where((a, b, c) => a.IsDel == false && a.account == userLogin.username && a.password == userLogin.password)
                    .FirstAsync((a, b, c) => new LoginDto()
                    {
                        Id = a.Id,
                        phone = a.phone,
                        name = a.name,
                        email = a.email,
                        account = a.account,
                        roleaccount = c.account,
                    });

                return login;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

    }
}
