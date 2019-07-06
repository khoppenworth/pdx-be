using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using PDX.DAL.Helpers;
using PDX.Domain;

namespace PDX.DAL.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private PDXContext _context;
        private readonly PDX.Logging.ILogger _logger;

        private IDictionary<string, object> _repositoryDict;

        public UnitOfWork(PDXContext context, PDX.Logging.ILogger logger)
        {
            _context = context;
            _logger = logger;
            _repositoryDict = new Dictionary<string, object>();
            var dbSetTypes = _context.GetAllDbSetTypes();
            InitializeRepositories(dbSetTypes);
        }

        private void InitializeRepositories(IList<Type> dbSetTypes)
        {
            foreach (Type type in dbSetTypes)
            {
                if (!_repositoryDict.ContainsKey(type.Name))
                {
                    var rType = typeof(GenericRepository<>);
                    var instance = Activator.CreateInstance(rType.MakeGenericType(type), _context, _logger);
                    _repositoryDict.Add(type.Name, instance);
                }
            }
        }

        public IGenericRepository<T> Repository<T>() where T : BaseEntity
        {
            var type = typeof(T);
            return (IGenericRepository<T>)_repositoryDict[type.Name];
        }


        public async Task<bool> SaveAsync()
        {
            using (var transaction = await _context.Database.BeginTransactionAsync())
            {
                try
                {
                    CheckDisposed();
                    await _context.SaveChangesAsync();
                    transaction.Commit();
                    return true;
                }
                catch (Exception ex)
                {
                    _logger.Log(ex);
                    Console.WriteLine(ex.Message);
                    this.UndoChangesOnDbContext();
                    transaction.Rollback();
                    return false;
                }
            }

        }

        /// <summary>
        /// Query ChangeTracker of DbContext for dirty items. 
        /// Set deleted items state to unchanged and added items to detached. 
        /// For modified items, use original values and set current values of the entry. Finally set state of modified entry to unchanged
        /// </summary>
        private void UndoChangesOnDbContext()
        {
            var changedEntries = _context.ChangeTracker.Entries().Where(e => e.State != EntityState.Unchanged).ToList();
            foreach (var entry in changedEntries)
            {
                switch (entry.State)
                {
                    case EntityState.Modified:
                        entry.CurrentValues.SetValues(entry.OriginalValues);
                        entry.State = EntityState.Unchanged;
                        break;
                    case EntityState.Added:
                        entry.State = EntityState.Detached;
                        break;
                    case EntityState.Deleted:
                        entry.State = EntityState.Unchanged;
                        break;
                }
            }
        }

        #region IDisposable Implementation

        protected bool _isDisposed = false;

        protected void CheckDisposed()
        {
            if (_isDisposed)
            {
                var ex = new ObjectDisposedException("The UnitOfWork is already disposed and cannot be used anymore.");
                _logger.Log(ex);
                throw ex;
            }
        }
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!disposing) return;
            if (_context == null) return;

            //Clear _repositoryDict
            if (_repositoryDict != null)
            {
                _repositoryDict.Clear();
            }

            // dispose the db context
            _context.Dispose();

            _isDisposed = true;
        }
        ~UnitOfWork()
        {
            Dispose(false);
        }
        #endregion
    }
}
