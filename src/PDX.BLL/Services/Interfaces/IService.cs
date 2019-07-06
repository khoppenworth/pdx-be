using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using DataTables.AspNet.Core;
using PDX.BLL.Model;
using PDX.Domain;

namespace PDX.BLL.Services.Interfaces
{
    public interface IService<T> where T : BaseEntity
    {
        /// <summary>
        /// Get All entities
        /// </summary>
        /// <param name="activeOnly"></param>
        /// <param name="orderBy"></param>
        /// <returns></returns>
        Task<IEnumerable<T>> GetAllAsync(bool activeOnly = false, Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null);

        /// <summary>
        /// Get page
        /// </summary>
        /// <param name="startRow"></param>
        /// <param name="pageSize"></param>
        /// <param name="columns"></param>
        /// <param name="predicate"></param>
        /// <param name="orderBy"></param>
        /// <returns></returns>
        Task<IEnumerable<T>> GetPageAsync(int startRow, int pageSize, IEnumerable<IColumn> columns = null, System.Linq.Expressions.Expression<Func<T, bool>> predicate = null,Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null);

        /// <summary>
        /// Get page for DataTables
        /// </summary>
        /// <param name="request"></param>
        /// <param name="predicate"></param>
        /// <param name="orderBy"></param>
        /// <returns></returns>
        Task<DataTablesResult<T>> GetPageAsync(IDataTablesRequest request, System.Linq.Expressions.Expression<Func<T, bool>> predicate = null, Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null);

        /// <summary>
        /// Pagination enabled search
        /// </summary>
        /// <param name="searchRequest"></param>
        /// <returns></returns>
        Task<DataTablesResult<T>> SearchAsync(SearchRequest searchRequest,bool activeOnly = false);

        /// <summary>
        /// Pagination enabled search
        /// </summary>
        /// <param name="searchRequest"></param>
        /// <param name="predicate"></param>
        /// <param name="orderBy"></param>
        /// <returns></returns>
        Task<DataTablesResult<T>> SearchAsync(SearchRequest searchRequest, System.Linq.Expressions.Expression<Func<T, bool>> predicate, Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null);
        
        /// <summary>
        /// Get entity by a certain condition
        /// </summary>
        /// <param name="predicate"></param>
        /// <param name="orderBy"></param>
        /// <returns>List of entities</returns>
        Task<IEnumerable<T>> FindByAsync(Expression<Func<T, bool>> predicate,Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null);

        /// <summary>
        /// Get single entity by ID
        /// </summary>
        /// <param name="ID"></param>
        /// <param name="noTracking"></param>
        /// <returns>Entity</returns>
        Task<T> GetAsync(int ID, bool noTracking = true);

        /// <summary>
        /// Get single entity by ID
        /// </summary>
        /// <param name="predicate"></param>
        /// <param name="noTracking"></param>
        /// <returns>Entity</returns>
        Task<T> GetAsync(Expression<Func<T, bool>> predicate, bool noTracking = true);

        /// <summary>
        /// Insert new entity
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="commit"></param>
        /// <param name="createdBy"></param>
        /// <returns>bool</returns>
        Task<bool> CreateAsync(T entity, bool commit = true, int? createdBy = null);

        /// <summary>
        /// Insert new multiple entities
        /// </summary>
        /// <param name="entities"></param>
        /// <param name="commit"></param>
        /// <param name="createdBy"></param>
        /// <returns></returns>
        Task<bool> CreateAsync(IEnumerable<T> entities, bool commit = true, int? createdBy = null);

        /// <summary>
        /// Update existing entity
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="commit"></param>
        /// <param name="modifiedBy"></param>
        /// <returns>bool</returns>
        Task<bool> UpdateAsync(T entity, bool commit = true, int? modifiedBy = null);

        /// <summary>
        /// Update existing entities
        /// </summary>
        /// <param name="entities"></param>
        /// <param name="commit"></param>
        /// <param name="modifiedBy"></param>
        /// <returns></returns>
        Task<bool> UpdateAsync(IEnumerable<T> entities, bool commit = true, int? modifiedBy = null);

        /// <summary>
        /// Create or Update entity
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="commit"></param>
        /// <param name="resolveBy"></param>
        /// <returns>bool</returns>
        Task<bool> CreateOrUpdateAsync(T entity, bool commit = true, Expression<Func<T, bool>> resolveBy = null);

        /// <summary>
        /// Create or Update entities
        /// </summary>
        /// <param name="entities"></param>
        /// <param name="commit"></param>
        /// <param name="resolvingProperties"></param>
        /// <returns>bool</returns>
        Task<bool> CreateOrUpdateAsync(IEnumerable<T> entities, bool commit = true, params string[] resolvingProperties);

        /// <summary>
        /// Delete existing entity
        /// </summary>
        /// <param name="ID"></param>
        /// <param name="commit"></param>
        /// <returns>bool</returns>
        Task<bool> DeleteAsync(int ID, bool commit = true);

        /// <summary>
        /// Count Method
        /// </summary>
        /// <param name="filter"></param>
        /// <returns></returns>
        Task<int> CountAsync(Expression<Func<T, bool>> filter = null);

        /// <summary>
        /// Chech if an entity exists
        /// </summary>
        /// <param name="filter"></param>
        /// <returns></returns>
        Task<bool> ExistsAsync(Expression<Func<T, bool>> filter = null);

        /// <summary>
        /// Check if an entity exists by id
        /// </summary>
        /// <param name="t"></param>
        /// <returns></returns>
        Task<bool> ExistsAsync(T t);
    }
}