﻿using BusinessAccount.Repositories;
using BusinessAccount.Security;
using FreeSql;
using NewsProject.Core.Entities;
using NewsProject.Core.IRepositories;
using System;
using System.Collections.Generic;
using System.Text;

namespace NewsProject.Infrastructure.Repositories
{
    public class LoginRepository : AuditBaseRepository<LoginEntity>, ILoginRepository
    {
        private readonly ICurrentUser _currentUser;
        public LoginRepository(UnitOfWorkManager unitOfWork, ICurrentUser currentUser) : base(unitOfWork, currentUser)
        {
            _currentUser = currentUser;
        }
    }
}