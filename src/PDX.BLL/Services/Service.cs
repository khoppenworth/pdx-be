using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Threading.Tasks;
using DataTables.AspNet.Core;
using PDX.BLL.Helpers;
using PDX.BLL.Helpers.ExpressionPredicateBuilder;
using PDX.BLL.Model;
using PDX.BLL.Services.Interfaces;
using PDX.DAL.Helpers;
using PDX.DAL.Query;
using PDX.DAL.Repositories;
using PDX.Domain;
using PDX.Domain.Common;

namespace PDX.BLL.Services {
    /// <summary>
    /// Generic Service Methods
    /// </summary>
    public class Service<T> : IService<T> where T : BaseEntity {
        private const int PAGE_SIZE = 50;
        private readonly IGenericRepository<T> _repository;
        private readonly IUnitOfWork _unitOfWork;

        /// <summary>
        /// Constructor for Service
        /// </summary>
        /// <param name="unitOfWork"></param>
        public Service (IUnitOfWork unitOfWork) {
            _unitOfWork = unitOfWork;
            _repository = _unitOfWork.Repository<T> ();
        }

        /// <summary>
        /// Constructor for Service
        /// </summary>
        /// <param name="unitOfWork"></param>
        /// <param name="genericRepository"></param>
        public Service (IUnitOfWork unitOfWork, IGenericRepository<T> genericRepository) {
            _unitOfWork = unitOfWork;
            _repository = genericRepository;
        }

        /// <summary>
        /// Get all entities
        /// </summary>
        /// <returns>List of entities</returns>
        public virtual async Task<IEnumerable<T>> GetAllAsync (bool activeOnly = false, Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null) {
            //return active entities if activeOnly is true
            return await (activeOnly ? _repository.FindByAsync (e => e.IsActive, orderBy) : _repository.GetAllAsync (orderBy));
        }

        /// <summary>
        /// Get page
        /// </summary>
        /// <param name="startRow"></param>
        /// <param name="pageSize"></param>
        /// <param name="columns"></param>
        /// <returns></returns>
        public virtual async Task<IEnumerable<T>> GetPageAsync (int startRow, int pageSize, IEnumerable<IColumn> columns = null, Expression<Func<T, bool>> predicate = null, Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null) {
            return await _repository.GetPageAsync (startRow, pageSize, columns, predicate, orderBy);
        }

        /// <summary>
        /// Get page for DataTables
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<DataTablesResult<T>> GetPageAsync (IDataTablesRequest request, Expression<Func<T, bool>> predicate = null, Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null) {
            var sortableColumns = request.Columns.Where (c => c.Sort != null);

            var totalRecords = await _repository.CountAsync (predicate);
            var pageData = await this.GetPageAsync (request.Start, request.Length, sortableColumns, predicate, orderBy);
            var response = new DataTablesResult<T> (request.Draw, totalRecords, totalRecords, pageData);
            return response;
        }

        /// <summary>
        /// Pagination enabled search
        /// </summary>
        /// <param name="searchRequest"></param>
        /// <returns></returns>
        public virtual async Task<DataTablesResult<T>> SearchAsync (SearchRequest searchRequest, bool activeOnly = false) {

            //Get searchable properties
            List<string> stringProperties = typeof (T).GetProperties (
                BindingFlags.FlattenHierarchy |
                BindingFlags.Public |
                BindingFlags.Instance).Where (p => !p.IsDefined (typeof (NotMappedAttribute), true) && p.PropertyType == typeof (string)).Select (p => p.Name).ToList ();

            Expression<Func<T, bool>> predicate = DataTableHelper.ConstructFilter<T> (searchRequest.Query, stringProperties, activeOnly);

            //submodule type filter
            if (typeof (T).GetProperty ("SubmoduleType") != null) {
                ParameterExpression argument = Expression.Parameter (typeof (T), "t");
                Expression left = Expression.Property (argument, typeof (T).GetProperty ("SubmoduleType"));
                left = Expression.Property (left, typeof (SubmoduleType).GetProperty ("SubmoduleTypeCode"));
                Expression right = Expression.Constant (searchRequest.SubmoduleTypeCode, typeof (string));

                Expression predicateSubmodule = Expression.Equal (left, right);

                Expression<Func<T, bool>> expression = predicateSubmodule != null ? Expression.Lambda<Func<T, bool>> (predicateSubmodule, new [] { argument }) : null;
                predicate = predicate != null? expression.AndAlso (predicate) : expression;
            };

            OrderBy<T> orderBy = new OrderBy<T> (qry => qry.OrderBy (e => e.ID));

            if (string.IsNullOrEmpty (searchRequest.Query)) {
                predicate = null;
            }

            var response = await SearchAsync (searchRequest, predicate, orderBy.Expression);
            return response;
        }

        public async Task<DataTablesResult<T>> SearchAsync (SearchRequest searchRequest, System.Linq.Expressions.Expression<Func<T, bool>> predicate, Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null) {

            var totalRecords = await this.CountAsync ();
            var totalFilteredRecords = await this.CountAsync (predicate);
            var pageData = await this.GetPageAsync (searchRequest.PageNumber * (searchRequest.PageSize.HasValue ? (int) searchRequest.PageSize : PAGE_SIZE), searchRequest.PageSize.HasValue ? (int) searchRequest.PageSize : PAGE_SIZE, null, predicate, null);
            var response = new DataTablesResult<T> (searchRequest.PageNumber, totalRecords, totalFilteredRecords, pageData);

            return response;
        }

        /// <summary>
        /// Get entity by a certain condition
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns>List of entities</returns>
        public virtual async Task<IEnumerable<T>> FindByAsync (System.Linq.Expressions.Expression<Func<T, bool>> predicate, Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null) {
            return await _repository.FindByAsync (predicate, orderBy);
        }

        /// <summary>
        /// Get single entity by ID
        /// </summary>
        /// <param name="ID"></param>
        /// <param name="noTracking"></param>
        /// <returns>Entity</returns>
        public virtual async Task<T> GetAsync (int ID, bool noTracking = true) {
            return await _repository.GetAsync (ID, noTracking);
        }

        /// <summary>
        /// Get single entity by ID
        /// </summary>
        /// <param name="predicate"></param>
        /// <param name="noTracking"></param>
        /// <returns>Entity</returns>
        public virtual async Task<T> GetAsync (Expression<Func<T, bool>> predicate, bool noTracking = true) {
            return await _repository.GetAsync (predicate, noTracking);
        }

        /// <summary>
        /// Insert new entity
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="commit"></param>
        /// <param name="createdBy"></param>
        /// <returns>bool</returns>
        public virtual async Task<bool> CreateAsync (T entity, bool commit = true, int? createdBy = null) {
            var exists = await ExistsAsync (e => e.ID == entity.ID);
            if (!exists) {
                await _repository.AddAsync (entity);
                if (commit) {
                    return await _unitOfWork.SaveAsync ();
                }
                return true;
            }
            return false;

        }

        /// <summary>
        /// Insert new multiple entities
        /// </summary>
        /// <param name="entities"></param>
        /// <param name="commit"></param>
        /// <param name="createdBy"></param>
        /// <returns></returns>
        public virtual async Task<bool> CreateAsync (IEnumerable<T> entities, bool commit = true, int? createdBy = null) {
            await _repository.AddAsync (entities);
            if (commit) {
                return await _unitOfWork.SaveAsync ();
            }
            return true;
        }

        /// <summary>
        /// Update existing entity
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="commit"></param>
        /// <param name="modifiedBy"></param>
        /// <returns>bool</returns>
        public virtual async Task<bool> UpdateAsync (T entity, bool commit = true, int? modifiedBy = null) {
            var exists = await ExistsAsync (e => e.ID == entity.ID);
            if (exists) {
                entity.NullifyForeignKeys ();
                entity.ModifiedDate = DateTime.UtcNow;
                await _repository.UpdateAsync (entity);
                if (commit) {
                    return await _unitOfWork.SaveAsync ();
                }
                return true;
            }
            return false;
        }

        /// <summary>
        /// Update existing entities
        /// </summary>
        /// <param name="entities"></param>
        /// <param name="commit"></param>
        /// <param name="modifiedBy"></param>
        /// <returns></returns>
        public virtual async Task<bool> UpdateAsync (IEnumerable<T> entities, bool commit = true, int? modifiedBy = null) {
            foreach (var entity in entities) {
                entity.NullifyForeignKeys ();
                entity.ModifiedDate = DateTime.UtcNow;
            }

            await _repository.UpdateAsync (entities);
            if (commit) {
                return await _unitOfWork.SaveAsync ();
            }
            return true;
        }

        /// <summary>
        /// Create or Update entity
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="commit"></param>
        /// <param name="resolveBy"></param>
        /// <returns>bool</returns>
        public virtual async Task<bool> CreateOrUpdateAsync (T entity, bool commit = true, Expression<Func<T, bool>> resolveBy = null) {
            entity.NullifyForeignKeys ();
            var existingEntity = resolveBy != null ? await this.GetAsync (resolveBy) : await this.GetAsync (entity.ID);

            if (existingEntity != null) {
                entity.ID = existingEntity.ID; //Copy ID from existing entity
                return await this.UpdateAsync (entity, commit);;
            }

            return await this.CreateAsync (entity, commit);;
        }

        /// <summary>
        /// Create or Update entities
        /// </summary>
        /// <param name="entities"></param>
        /// <param name="commit"></param>
        /// <param name="resolvingProperties"></param>
        /// <returns>bool</returns>
        public virtual async Task<bool> CreateOrUpdateAsync (IEnumerable<T> entities, bool commit = true, params string[] resolvingProperties) {
            var result = true;
            foreach (var entity in entities) {
                var resolveBy = (Expression<Func<T, bool>>) ReflectionHelper.BuildResolvingExpression (entity, resolvingProperties);
                result = result && (await CreateOrUpdateAsync (entity, commit, resolveBy));
            }
            return result;
        }

        /// <summary>
        /// Delete existing entity
        /// </summary>
        /// <param name="ID"></param>
        /// <param name="commit"></param>
        /// <returns>bool</returns>
        public virtual async Task<bool> DeleteAsync (int ID, bool commit = true) {
            var entity = await GetAsync (ID);
            if (entity != null) {
                await _repository.DeleteAsync (entity);
                if (commit) {
                    return await _unitOfWork.SaveAsync ();
                }
                return true;
            }
            return false;
        }

        /// <summary>
        /// Count Method
        /// </summary>
        /// <param name="filter"></param>
        /// <returns></returns>
        public async Task<int> CountAsync (Expression<Func<T, bool>> filter = null) {
            return await _repository.CountAsync (filter);
        }

        /// <summary>
        /// Chech if an entity exists
        /// </summary>
        /// <param name="filter"></param>
        /// <returns></returns>
        public async Task<bool> ExistsAsync (Expression<Func<T, bool>> filter = null) {
            return await _repository.ExistsAsync (filter);
        }

        /// <summary>
        /// Chech if an entity exists
        /// </summary>
        /// <param name="t"></param>
        /// <returns></returns>
        public async Task<bool> ExistsAsync (T t) {
            return await this.ExistsAsync (e => e.ID == t.ID);
        }
    }
}