using PDX.BLL.Services.Interfaces;
using PDX.DAL.Repositories;
using PDX.Domain.Document;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace PDX.BLL.Services
{
    public class ModuleDocumentService : Service<ModuleDocument>, IModuleDocumentService
    {
        public ModuleDocumentService(IUnitOfWork unitOfWork) : base(unitOfWork)
        {

        }
        /// <summary>
        /// Get active module documents 
        /// </summary>
        /// <returns></returns>
        public async override  Task<IEnumerable<ModuleDocument>> GetAllAsync(bool activeOnly = false, Func<IQueryable<ModuleDocument>, IOrderedQueryable<ModuleDocument>> orderBy = null)
        {
            var result = await base.GetAllAsync(true);
            return result;
        }
    }
}