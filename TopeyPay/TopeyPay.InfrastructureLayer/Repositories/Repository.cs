using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using TopeyPay.Domain.IRepository;

namespace TopeyPay.Infrastructure.Repositories
{
    public class Repository<TEntity> :IRepository<TEntity> where TEntity : class
    {
        protected readonly DbContext Context;
        private readonly DbSet<TEntity> _dbSet;

        public Repository(DbContext context)
        {
            Context = context;
            _dbSet = Context.Set<TEntity>();
        }

        public async Task<TEntity> AddAsync(TEntity entity)
        {
            await _dbSet.AddAsync(entity);

            return entity;
        }

        public async Task AddRangeAsync(IEnumerable<TEntity> entities)
        {
            await _dbSet.AddRangeAsync(entities);
        }

        public async Task<IEnumerable<TEntity>> WhereAsync(Expression<Func<TEntity, bool>> predicate)
        {
            return await _dbSet.Where(predicate).ToListAsync();
        }


        public async Task<IEnumerable<TEntity>> GetAllAsync()
        {
            return await _dbSet.ToListAsync();
        }

        public async Task<TEntity> GetByIdAsync(object id)
        {
            return await _dbSet.FindAsync(id);
        }

        public async Task<bool> IsExistingAsync(Expression<Func<TEntity, bool>> predicate)
        {
            return await _dbSet.AnyAsync(predicate);
        }

        public void Remove(TEntity entity)
        {
            _dbSet.Remove(entity);
        }

        public void RemoveRange(IEnumerable<TEntity> entities)
        {
            _dbSet.RemoveRange(entities);
        }

        public async Task<TEntity> FirstOrDefaultAsync(Expression<Func<TEntity, bool>> predicate)
        {
            return await _dbSet.FirstOrDefaultAsync(predicate);
        }

        public EntityEntry<TEntity> Update(TEntity entity)
        {
            return _dbSet.Update(entity);
        }
        public void UpdateRange(IEnumerable<TEntity> entities)
        {
            _dbSet.UpdateRange(entities);
        }
    }
}
