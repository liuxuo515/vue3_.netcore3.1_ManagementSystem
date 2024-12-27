using BusinessAccount.Api.Models;
using BusinessAccount.Core.Data;
using BusinessAccount.Extensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;
using NewsProject.Application.Contracts.User;
using System;
using System.Threading.Tasks;

namespace NewsProject.Api.Controllers
{
    [ApiController]
    [Route("api")]
    [Authorize]
    public class UserController : ControllerBase
    {
        private readonly IDistributedCache _redisClient;
        private readonly IUserService _userService;
        public UserController(IDistributedCache redisClient, IUserService userService)
        {
            _redisClient = redisClient;
            _userService = userService;
        }

        /// <summary>
        /// 用户查询接口
        /// </summary>
        /// <param name="id"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("user/getusers")]
        public async Task<BaseResultApiModel> getusers(string userName, int pageIndex = 1, int pageSize = 10)
        {
            return await _userService.GetUser(userName, pageIndex, pageSize);
        }
        /// <summary>
        /// 用户新增 修改接口
        /// </summary>
        /// <param name="saveUser"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("user/saveorupdateuser")]
        public async Task<BaseResultApiModel> saveorupdateuser(SaveUserQuery saveUser)
        {
            //获取当前登录人的信息
            claimsConfig claims = JwtExtensions.GetJwtUserInfo(Request);
            saveUser.CreateDate = DateTime.Now;
            saveUser.CreateUserId = claims.userid == null ? 0 : Convert.ToInt32(claims.userid);
            saveUser.CreateUserName = claims.username ?? "";

            return await _userService.SaveOrUpdateUser(saveUser);
        }
        /// <summary>
        /// 用户删除接口
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("user/deluser")]
        public async Task<BaseResultApiModel> deluser(int Id)
        {
            return await _userService.DelUser(Id);
        }


        /// <summary>
        /// 角色权限查询接口
        /// </summary>
        /// <param name="roleName"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("user/rolepermiss")]
        public async Task<BaseResultApiModel> rolePermiss(string roleName, string account, int pageIndex = 1, int pageSize = 10)
        {
            return await _userService.GetRolePermiss(roleName,  account, pageIndex, pageSize);
        }

        /// <summary>
        /// 角色下拉框查询接口
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("user/getroleopts")]
        public async Task<BaseResultApiModel> GetRoleOpts()
        {
            return await _userService.GetRoleOpts();
        }

        /// <summary>
        /// 角色 新增 or 修改接口
        /// </summary>
        /// <param name="role"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("user/saveorupdaterole")]
        public async Task<BaseResultApiModel> SaveOrUpdateRole(RoleQuery role)
        {
            //获取当前登录人的信息
            claimsConfig claims = JwtExtensions.GetJwtUserInfo(Request);
            role.CreateDate = DateTime.Now;
            role.CreateUserId = claims.userid == null ? 0 : Convert.ToInt32(claims.userid);
            role.CreateUserName = claims.username ?? "";

            return await _userService.SaveOrUpdateRole(role);
        }

        /// <summary>
        /// 角色删除接口
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("user/delrole")]
        public async Task<BaseResultApiModel> DelRole(int Id)
        {
            return await _userService.DelRole(Id);
        }

        /// <summary>
        /// 角色权限 修改
        /// </summary>
        /// <param name="rolePermiss"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("user/saveorupdatepermiss")]
        public async Task<BaseResultApiModel> SaveOrUpdatePermiss(RolePermissQuery rolePermiss)
        {
            return await _userService.SaveOrUpdatePermiss(rolePermiss);
        }

    }
}
