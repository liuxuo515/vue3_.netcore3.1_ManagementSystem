using BusinessAccount.Core.Data;
using NewsProject.Application.Contracts.User;
using NewsProject.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace NewsProject.Application.Contracts.Login
{
    public interface ILoginService
    {
        Task<LoginDto> GetLogin(UserLoginDto userLogin);
    }
}
