using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using DataTables.AspNet.Core;
using PDX.Domain;

namespace PDX.DAL.Repositories
{
    public interface IGenericRepository<T> where T : BaseEntity
    {
        Task<IEnumerable<T>> GetAllAsync(Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null);
        Task<IEnumerable<T>> GetPageAsync(int startRow, int pageSize, IEnumerable<IColumn> columns = null, Expression<Func<T, bool>> predicate = null,Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null);
        Task<IEnumerable<T>> FindByAsync(Expression<Func<T, bool>> predicate,Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null);
        Task<T> GetAsync(int ID, bool noTracking = true);
        Task<T> GetAsync(Expression<Func<T, bool>> predicate, bool noTracking = true);
        Task AddAsync(T entity);
        Task AddAsync(IEnumerable<T> entities);
        Task DeleteAsync(T entity);
        Task UpdateAsync(T entity);
        Task UpdateAsync(IEnumerable<T> entities);
        Task<int> CountAsync(Expression<Func<T, bool>> filter = null);
        Task<bool> ExistsAsync(Expression<Func<T, bool>> filter = null);
    }
}
