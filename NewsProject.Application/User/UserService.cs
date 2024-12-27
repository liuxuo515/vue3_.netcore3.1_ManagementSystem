using AutoMapper;
using BusinessAccount;
using BusinessAccount.Core.Data;
using NewsProject.Application.Contracts.User;
using NewsProject.Core.Entities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace NewsProject.Application.User
{
    public class UserService : ApplicationService, IUserService
    {
        private readonly IFreeSql _freeSql;
        private readonly IMapper _mapper;
        public UserService(IFreeSql freeSql, IMapper mapper)
        {
            _freeSql = freeSql;
            _mapper = mapper;
           
        }

        /// <summary>
        /// 用户管理查询接口
        /// </summary>
        /// <param name="id"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public async Task<BaseResultApiModel> GetUser(string userName, int pageIndex, int pageSize)
        {
            try
            {
                //获取用户
                long count;
                List<tsUserEntity> tsUsersList = await _freeSql.Select<tsUserEntity>()
                    .Where(o => o.IsDel == false)
                    .WhereIf(!string.IsNullOrEmpty(userName), a => a.name.Contains(userName))
                    .Count(out count)
                    .Page(pageIndex, pageSize)
                    .ToListAsync();

                List<int> userIntList = tsUsersList.Select(o => o.Id).ToList();

                //获取角色
                List<LoginRoleDto> tsRolesList = _freeSql.Select<tsUserRoleEntity, tsRoleEntity>()
                    .LeftJoin((a, b) => a.RoleId == b.Id)
                    .Where((a, b) => userIntList.Contains(a.UserId) && a.IsDel == false && b.IsDel == false)
                    .ToList((a, b) => new LoginRoleDto()
                    {
                        rolename = b.rolename,
                        roleid = b.Id,
                        userId = a.UserId
                    });

                //返回数据的dto
                List<UserDto> userDtosList = _mapper.Map<List<UserDto>>(tsUsersList);

                foreach (var userDto in userDtosList)
                {
                    //拼接用户名称
                    List<LoginRoleDto> roleDtosList = tsRolesList.Where(o => o.userId == userDto.id).ToList();
                    if (roleDtosList != null && roleDtosList.Count() > 0)
                    {
                        userDto.role = string.Join(",", roleDtosList.Select(o => o.rolename).ToList()).ToString();
                        userDto.roleId = roleDtosList[0].roleid;
                    }
                }

                BaseResultApiModel baseResultApi = new BaseResultApiModel()
                {
                    code = 200,
                    data = new BaseDataApiModel()
                    {
                        total = count,
                        list = userDtosList
                    }
                };
                return baseResultApi;
            }
            catch (Exception ex)
            {
                return new BaseResultApiModel()
                {
                    code = 500,
                    msg = ex.Message,
                };
            }
        }
        /// <summary>
        /// 用户管理新增 or 修改接口
        /// </summary>
        /// <param name="saveUser"></param>
        /// <returns></returns>
        public async Task<BaseResultApiModel> SaveOrUpdateUser(SaveUserQuery saveUser)
        {
            try
            {
                //开启事务
                using (var uow = _freeSql.CreateUnitOfWork())
                {
                    if (saveUser.id > 0)
                    {
                        //修改用户表
                        int h = await uow.Orm.Update<tsUserEntity>()
                                        .Set(a => a.name, saveUser.name)
                                        .Set(a => a.account, saveUser.account)
                                        .Set(a => a.password, saveUser.password)
                                        .Set(a => a.email, saveUser.email)
                                        .Set(a => a.phone, saveUser.phone)
                                        .Set(a => a.ModifyUserId, saveUser.CreateUserId)
                                        .Set(a => a.ModifyUserName, saveUser.CreateUserName)
                                        .Set(a => a.ModifyDate, saveUser.CreateDate)
                                        .Where(a => a.Id == saveUser.id)
                                        .ExecuteAffrowsAsync();

                        //修改关联关系表
                        h += await uow.Orm.Update<tsUserRoleEntity>()
                                     .Set(a => a.RoleId, saveUser.roleId)
                                     .Where(a => a.UserId == saveUser.id && a.IsDel == false)
                                     .ExecuteAffrowsAsync();

                        uow.Commit();
                        return new BaseResultApiModel()
                        {
                            code = 200,
                            msg = "修改成功！"
                        };
                    }
                    else
                    {
                        //新增用户表
                        tsUserEntity tsUserEntity = _mapper.Map<tsUserEntity>(saveUser);
                        long Id = await uow.Orm.Insert(tsUserEntity).ExecuteIdentityAsync();
                        if (Id > 0)
                        {
                            //新增关联关系表
                            tsUserRoleEntity tsUserRoleEntity = new tsUserRoleEntity()
                            {
                                IsDel = false,
                                UserId = (int)Id,
                                RoleId = saveUser.roleId
                            };
                            await uow.Orm.Insert(tsUserRoleEntity).ExecuteIdentityAsync();
                        }
                        uow.Commit();

                        return new BaseResultApiModel()
                        {
                            code = 200,
                            msg = "新增成功！"
                        };
                    }
                }
            }
            catch (Exception ex)
            {
                return new BaseResultApiModel()
                {
                    code = 500,
                    msg = ex.Message,
                };
            }
        }
        /// <summary>
        /// 删除用户
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public async Task<BaseResultApiModel> DelUser(int Id)
        {
            try
            {
                //开启事务
                using (var uow = _freeSql.CreateUnitOfWork())
                {
                    //删除用户表
                    int h = await _freeSql.Update<tsUserEntity>()
                                      .Set(a => a.IsDel, true)
                                      .Where(a => a.Id == Id)
                                      .ExecuteAffrowsAsync();

                    //删除用户关联关系表
                    h += await _freeSql.Update<tsUserRoleEntity>()
                                .Set(a => a.IsDel, true)
                                .Where(a => a.UserId == Id)
                                .ExecuteAffrowsAsync();

                    uow.Commit();

                    if (h > 0)
                    {
                        return new BaseResultApiModel()
                        {
                            code = 200,
                            msg = "删除成功！"
                        };
                    }
                    else
                    {
                        return new BaseResultApiModel()
                        {
                            code = 500,
                            msg = "删除失败！"
                        };
                    }
                }

            }
            catch (Exception ex)
            {
                return new BaseResultApiModel()
                {
                    code = 500,
                    msg = "删除失败！" + ex.Message
                };
            }
        }

        /// <summary>
        /// 角色下拉框查询接口
        /// </summary>
        /// <returns></returns>
        public async Task<BaseResultApiModel> GetRoleOpts()
        {
            try
            {
                //角色下拉框
                List<RoleOptsDto> roleOptsList = await _freeSql.Select<tsRoleEntity>()
                    .Where(a => a.IsDel == false)
                    .OrderByDescending(o => o.CreateDate)
                    .ToListAsync(a => new RoleOptsDto()
                    {
                        id = a.Id,
                        name = a.rolename,
                    });

                BaseResultApiModel baseResultApi = new BaseResultApiModel()
                {
                    code = 200,
                    data = new BaseDataApiModel()
                    {
                        list = roleOptsList
                    }
                };
                return baseResultApi;
            }
            catch (Exception ex)
            {
                return new BaseResultApiModel()
                {
                    code = 500,
                    msg = ex.Message,
                };
            }
        }

        /// <summary>
        /// 角色权限查询接口
        /// </summary>
        /// <param name="id"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public async Task<BaseResultApiModel> GetRolePermiss(string roleName, string account, int pageIndex, int pageSize)
        {
            try
            {
                //获取角色 权限
                long count;
                List<RolePermissDto> rolePermissDtosList = await _freeSql.Select<tsRoleEntity, tsPermissEntity>()
                    .LeftJoin((a, b) => a.Id == b.roleId)
                    .Where((a, b) => a.IsDel == false && b.IsDel == false)
                    .WhereIf(!string.IsNullOrEmpty(roleName), (a, b) => a.rolename.Contains(roleName))
                    .WhereIf(!string.IsNullOrEmpty(account), (a, b) => a.account == account)
                    .Count(out count)
                    .Page(pageIndex, pageSize)
                    .OrderByDescending((a, b) => a.CreateDate)
                    .ToListAsync((a, b) => new RolePermissDto()
                    {
                        id = a.Id,
                        key = a.account,
                        name = a.rolename,
                        status = a.state,
                        permissStr = b.permiss,
                        permiss = null
                    });

                //将权限逗号分隔权限改为集合
                foreach (var item in rolePermissDtosList)
                {
                    if (item.permissStr.Length > 0)
                    {
                        item.permiss = item.permissStr.Split(',').ToList();
                    }
                }


                BaseResultApiModel baseResultApi = new BaseResultApiModel()
                {
                    code = 200,
                    data = new BaseDataApiModel()
                    {
                        total = count,
                        list = rolePermissDtosList
                    }
                };
                return baseResultApi;
            }
            catch (Exception ex)
            {
                return new BaseResultApiModel()
                {
                    code = 500,
                    msg = ex.Message,
                };
            }
        }

        /// <summary>
        /// 角色 新增 or 修改接口
        /// </summary>
        /// <param name="saveUser"></param>
        /// <returns></returns>
        public async Task<BaseResultApiModel> SaveOrUpdateRole(RoleQuery role)
        {
            try
            {
                //开启事务
                using (var uow = _freeSql.CreateUnitOfWork())
                {
                    if (role.Id > 0)
                    {
                        //修改用户表
                        int h = await uow.Orm.Update<tsRoleEntity>()
                                        .Set(a => a.rolename, role.name)
                                        .Set(a => a.account, role.key)
                                        .Set(a => a.state, role.status)
                                        .Set(a => a.ModifyUserId, role.CreateUserId)
                                        .Set(a => a.ModifyUserName, role.CreateUserName)
                                        .Set(a => a.ModifyDate, role.CreateDate)
                                        .Where(a => a.Id == role.Id)
                                        .ExecuteAffrowsAsync();

                        uow.Commit();
                        return new BaseResultApiModel()
                        {
                            code = 200,
                            msg = "修改成功！"
                        };
                    }
                    else
                    {
                        //新增用户表
                        tsRoleEntity tsRoleEntity = _mapper.Map<tsRoleEntity>(role);
                        long Id = await uow.Orm.Insert(tsRoleEntity).ExecuteIdentityAsync();

                        //新增用户权限表
                        tsPermissEntity tsPermiss = new tsPermissEntity()
                        {
                            roleId = (int)Id,
                            permiss = "1",
                            IsDel = false,
                        };
                        await uow.Orm.Insert(tsPermiss).ExecuteIdentityAsync();

                        uow.Commit();

                        return new BaseResultApiModel()
                        {
                            code = 200,
                            msg = "新增成功！"
                        };
                    }
                }
            }
            catch (Exception ex)
            {
                return new BaseResultApiModel()
                {
                    code = 500,
                    msg = ex.Message,
                };
            }
        }

        /// <summary>
        /// 删除角色
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public async Task<BaseResultApiModel> DelRole(int Id)
        {
            try
            {
                //开启事务
                using (var uow = _freeSql.CreateUnitOfWork())
                {
                    //删除角色表
                    int h = await _freeSql.Update<tsRoleEntity>()
                                      .Set(a => a.IsDel, true)
                                      .Where(a => a.Id == Id)
                                      .ExecuteAffrowsAsync();

                    //删除角色权限表
                    h += await _freeSql.Update<tsPermissEntity>()
                                .Set(a => a.IsDel, true)
                                .Where(a => a.roleId == Id)
                                .ExecuteAffrowsAsync();

                    uow.Commit();

                    if (h > 0)
                    {
                        return new BaseResultApiModel()
                        {
                            code = 200,
                            msg = "删除成功！"
                        };
                    }
                    else
                    {
                        return new BaseResultApiModel()
                        {
                            code = 500,
                            msg = "删除失败！"
                        };
                    }
                }

            }
            catch (Exception ex)
            {
                return new BaseResultApiModel()
                {
                    code = 500,
                    msg = "删除失败！" + ex.Message
                };
            }
        }



        /// <summary>
        /// 角色权限 修改
        /// </summary>
        /// <param name="saveUser"></param>
        /// <returns></returns>
        public async Task<BaseResultApiModel> SaveOrUpdatePermiss(RolePermissQuery rolePermiss)
        {
            try
            {
                //权限逗号分隔
                string permissStr = string.Join(',', rolePermiss.permiss);
                //修改用户表
                int h = await _freeSql.Update<tsPermissEntity>()
                                .Set(a => a.permiss, permissStr)
                                .Where(a => a.Id == rolePermiss.id)
                                .ExecuteAffrowsAsync();

                return new BaseResultApiModel()
                {
                    code = 200,
                    msg = "保存成功！"
                };
            }
            catch (Exception ex)
            {
                return new BaseResultApiModel()
                {
                    code = 500,
                    msg = ex.Message,
                };
            }
        }

    }
}
