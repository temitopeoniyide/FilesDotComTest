using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace TopeyPay.Domain.IRepository
{
   public interface IRepository<TEntity> where TEntity : class
    {
        Task<TEntity> AddAsync(TEntity entity);
        Task AddRangeAsync(IEnumerable<TEntity> entities);
        void Remove(TEntity entity);
        void RemoveRange(IEnumerable<TEntity> entities);
        Task<IEnumerable<TEntity>> GetAllAsync();
        Task<TEntity> GetByIdAsync(object id);
        Task<IEnumerable<TEntity>> WhereAsync(Expression<Func<TEntity, bool>> predicate);
        Task<TEntity> FirstOrDefaultAsync(Expression<Func<TEntity, bool>> predicate);
        Task<bool> IsExistingAsync(Expression<Func<TEntity, bool>> predicate);
        EntityEntry<TEntity> Update(TEntity entity);
        void UpdateRange(IEnumerable<TEntity> entities);

    }
}
