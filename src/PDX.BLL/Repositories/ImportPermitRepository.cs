using PDX.DAL.Repositories;
using PDX.Domain.Procurement;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PDX.BLL.Helpers;
using PDX.DAL;
using PDX.DAL.Helpers;
using PDX.Domain;
using PDX.Domain.License;
using PDX.Logging;

namespace PDX.BLL.Repositories
{
    public class ImportPermitRepository : GenericRepository<ImportPermit>, IImportPermitRepository
    {
        public ImportPermitRepository(PDXContext dbContext, ILogger logger) : base(dbContext, logger)
        {

        }

        public async override Task UpdateAsync(ImportPermit importPermit)
        {
            try
            {
                
                var existingIP = await base.GetAsync(importPermit.ID);

                //Check if there are Collections to be Removed
                var importPermitDetailsToRemove = existingIP.ImportPermitDetails.Except(importPermit.ImportPermitDetails, new IDComparer());
                //Remove Collections
                RemoveCollection(importPermitDetailsToRemove);

                _dbSet.Update(importPermit);
            }
            catch (Exception ex)
            {
                _logger.Log(ex);
            }

        }
    }
}