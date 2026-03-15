using Course.Order.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Course.Order.Application.Contracts.Repositories
{
    public interface IGenericRepository<TId , TEntity> where TEntity : BaseEntity<TId> where TId : struct
    {
        public Task<bool> AnyAsync(TId id);
        public Task<bool> AnyAsync(Expression<Func<TEntity, bool>> predicate);
        Task<List<TEntity>> GetAllAsync();
        Task<List<TEntity>> GetAllPagedAsync(int pageNumber , int pageSize);
        ValueTask<TEntity?> GetByIdAsync(TId id);
        IQueryable<TEntity> Where(Expression<Func<TEntity, bool>> predicate);
        void Add(TEntity entity);
        void Update(TEntity entity);
        void Remove(TEntity entity);

    }
}
