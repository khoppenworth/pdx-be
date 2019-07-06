using System;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq.Expressions;
using PDX.Domain;
using PDX.DAL.Helpers;
using PDX.DAL.Query;
using DataTables.AspNet.Core;

namespace PDX.DAL.Repositories
{
    public class GenericRepository<T> : IGenericRepository<T> where T : BaseEntity
    {
        protected readonly DbContext _dbContext;
        protected readonly DbSet<T> _dbSet;

        protected Includes<T> _includes;
        protected readonly OrderBy<T> DefaultOrderBy = new OrderBy<T>(qry => qry.OrderByDescending(e => e.CreatedDate));
        protected readonly PDX.Logging.ILogger _logger;


        public GenericRepository(PDXContext dbContext, PDX.Logging.ILogger logger)
        {
            _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
            _dbSet = _dbContext.Set<T>();

            _logger = logger;
            _includes = DbContextHelper.GetNavigations<T>();
        }

        public virtual async Task<IEnumerable<T>> GetAllAsync(Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null)
        {
            try
            {
                var query = QueryDb(null, orderBy ?? DefaultOrderBy.Expression, _includes != null ? _includes.Expression : null);
                var list = await query.ToListAsync();
                return list;
            }
            catch (Exception ex)
            {
                _logger.Log(ex);
            }
            return null;
        }

        public virtual async Task<IEnumerable<T>> GetPageAsync(int startRow, int pageSize, IEnumerable<IColumn> columns = null, Expression<Func<T, bool>> predicate = null, Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null)
        {
            try
            {
                IQueryable<T> query = columns == null || !columns.Any() ? QueryDb(predicate, orderBy != null ? orderBy : DefaultOrderBy.Expression, _includes != null ?
                    _includes.Expression : null) : QueryDb(predicate, null, _includes != null ? _includes.Expression : null, columns);

                var list = await query.Skip(startRow).Take(pageSize).ToListAsync();
                return list;
            }
            catch (Exception ex)
            {
                _logger.Log(ex);
            }
            return null;
        }

        public async virtual Task<IEnumerable<T>> FindByAsync(Expression<Func<T, bool>> predicate, Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null)
        {
            try
            {
                var query = QueryDb(predicate, orderBy ?? DefaultOrderBy.Expression, _includes != null ? _includes.Expression : null);
                var list = await query.ToListAsync();
                return list;
            }
            catch (Exception ex)
            {
                _logger.Log(ex);
            }
            return null;
        }
        public async virtual Task<T> GetAsync(int ID, bool noTracking = true)
        {
            var entity = await GetAsync(x => x.ID == ID, noTracking);
            return entity;
        }

        public async virtual Task<T> GetAsync(Expression<Func<T, bool>> predicate, bool noTracking = true)
        {
            try
            {
                IQueryable<T> query = _dbSet;
                if (noTracking)
                {
                    query.AsNoTracking();
                }
                if (_includes != null)
                {
                    query = _includes.Expression(query);
                }
                var entity = await query.FirstOrDefaultAsync(predicate);
                return entity;
            }
            catch (Exception ex)
            {
                _logger.Log(ex);
            }
            return null;
        }
        public async virtual Task AddAsync(T entity)
        {
            try
            {
                await _dbSet.AddAsync(entity);
            }
            catch (InvalidOperationException ioe)
            {
                _logger.Log(ioe);
            }
            catch (Exception ex)
            {
                _logger.Log(ex);
            }
        }

        public async virtual Task AddAsync(IEnumerable<T> entities)
        {
            try
            {
                await _dbSet.AddRangeAsync(entities);
            }
            catch (InvalidOperationException ioe)
            {
                _logger.Log(ioe);
            }
            catch (Exception ex)
            {
                _logger.Log(ex);
            }
        }

        public async virtual Task DeleteAsync(T entity)
        {
            try
            {
                _dbContext.Entry(entity).State = EntityState.Deleted;
            }
            catch (Exception ex)
            {
                _logger.Log(ex);
            }
        }

        public async virtual Task UpdateAsync(T entity)
        {
            try
            {
                var attachedObject = _dbContext.ChangeTracker.Entries<T>().FirstOrDefault(f => f.Entity.ID == entity.ID);
                if (attachedObject != null)
                {
                    attachedObject.State = EntityState.Detached;
                }
                _dbSet.Update(entity);
            }
            catch (Exception ex)
            {
                _logger.Log(ex);
            }

        }

        public async virtual Task UpdateAsync(IEnumerable<T> entities)
        {
            try
            {
                var IDs = entities.Select(e => e.ID);
                var attachedObjects = _dbContext.ChangeTracker.Entries<T>().Where(f => IDs.Contains(f.Entity.ID));
                foreach (var attachedObject in attachedObjects)
                {
                    if (attachedObject != null)
                    {
                        attachedObject.State = EntityState.Detached;
                    }
                }
                _dbSet.UpdateRange(entities);
            }
            catch (Exception ex)
            {
                _logger.Log(ex);
            }
        }

        public Task<int> CountAsync(Expression<Func<T, bool>> filter = null)
        {
            IQueryable<T> query = QueryDb(filter, null, null, null);
            return query.CountAsync();
        }
        public async Task<bool> ExistsAsync(Expression<Func<T, bool>> filter = null)
        {
            IQueryable<T> query = QueryDb(filter, null, null, null);
            try
            {
                return await query.AnyAsync();
            }
            catch (Exception ex)
            {
                _logger.Log(ex);
            }
            return false;
        }

        protected void RemoveCollection(IEnumerable<BaseEntity> list){
            if(list.Any()){
                list.NullifyForeignKeys();
                _dbContext.RemoveRange(list);
            }
        }


        private IQueryable<T> QueryDb(Expression<Func<T, bool>> filter, Func<IQueryable<T>, IOrderedQueryable<T>> orderBy, Func<IQueryable<T>, IQueryable<T>> includes, IEnumerable<IColumn> columns = null)
        {
            IQueryable<T> query = _dbContext.Set<T>().AsNoTracking();

            if (filter != null)
            {
                query = query.Where(filter);
            }

            if (includes != null)
            {
                query = includes(query);
            }

            if (orderBy != null)
            {
                query = orderBy(query);
            }

            if (columns != null)
            {
                query = query.OrderBy(columns);
            }

            return query;
        }

        #region Dispose
        private bool disposed = false;
        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    _dbContext.Dispose();
                }
                this.disposed = true;
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        #endregion
    }
}
