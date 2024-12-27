using BusinessAccount.Core.Data;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace NewsProject.Application.Contracts.User
{
    public interface IUserService
    {
        /// <summary>
        /// 获取用户
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        Task<BaseResultApiModel> GetUser(string userName, int pageIndex, int pageSize);
        /// <summary>
        /// 用户管理新增 or 修改接口
        /// </summary>
        /// <param name="saveUser"></param>
        /// <returns></returns>
        Task<BaseResultApiModel> SaveOrUpdateUser(SaveUserQuery saveUser);
        /// <summary>
        /// 角色下拉框查询接口
        /// </summary>
        /// <returns></returns>
        Task<BaseResultApiModel> GetRoleOpts();
        /// <summary>
        /// 删除用户
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        Task<BaseResultApiModel> DelUser(int Id);
        /// <summary>
        /// 获取角色权限
        /// </summary>
        /// <param name="roleName"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        Task<BaseResultApiModel> GetRolePermiss(string roleName, string account, int pageIndex, int pageSize);
        /// <summary>
        /// 角色 新增 or 修改接口
        /// </summary>
        /// <param name="saveUser"></param>
        /// <returns></returns>
        Task<BaseResultApiModel> SaveOrUpdateRole(RoleQuery role);
        /// <summary>
        /// 删除角色
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        Task<BaseResultApiModel> DelRole(int Id);

        /// <summary>
        /// 角色权限 修改
        /// </summary>
        /// <param name="saveUser"></param>
        /// <returns></returns>
        Task<BaseResultApiModel> SaveOrUpdatePermiss(RolePermissQuery rolePermiss);
    }
}
