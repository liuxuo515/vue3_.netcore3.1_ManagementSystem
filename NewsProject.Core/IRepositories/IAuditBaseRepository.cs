using FreeSql;
using System;

namespace BusinessAccount.IRepositories
{
    public interface IAuditBaseRepository<TEntity> : IBaseRepository<TEntity, Guid> where TEntity : class
    {
    }

    public interface IAuditBaseRepository<TEntity, TKey> : IBaseRepository<TEntity, TKey> where TEntity : class
    {

    }
}
