using BusinessAccount.IRepositories;
using NewsProject.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace NewsProject.Core.IRepositories
{
    public interface IUserRepository : IAuditBaseRepository<tsUserEntity>
    {
    }
}
