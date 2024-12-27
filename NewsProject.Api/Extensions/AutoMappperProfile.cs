using AutoMapper;
using NewsProject.Application.Contracts.User;
using NewsProject.Core.Entities;
using System.Collections.Generic;

namespace NewsProject.Api.Extensions
{
    public class AutoMappperProfile : Profile
    {
        public AutoMappperProfile()
        {
            //统一AutoMapper转换
            CreateMap<tsUserEntity, UserDto>().ForMember(o => o.date, p => p.MapFrom(s => s.CreateDate));
            CreateMap<SaveUserQuery, tsUserEntity>();
            CreateMap<RoleQuery, tsRoleEntity>()
                .ForMember(o => o.rolename, p => p.MapFrom(s => s.name))
                .ForMember(o => o.state, p => p.MapFrom(s => s.status))
                .ForMember(o => o.account, p => p.MapFrom(s => s.key)); 

        }
    }
}
