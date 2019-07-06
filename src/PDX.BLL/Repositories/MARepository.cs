using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PDX.BLL.Helpers;
using PDX.DAL;
using PDX.DAL.Helpers;
using PDX.DAL.Repositories;
using PDX.Domain;
using PDX.Domain.License;
using PDX.Logging;

namespace PDX.BLL.Repositories
{
    public class MARepository : GenericRepository<MA>, IMARepository
    {
        public MARepository(PDXContext context, ILogger logger) : base(context, logger)
        {
        }

        public async override Task UpdateAsync(MA ma)
        {
            try
            {
                
                var existingMA = await base.GetAsync(ma.ID);

                //Check if there are collections to be Removed
                var maAssignmentsToRemove = existingMA.MAAssignments.Except(ma.MAAssignments, new IDComparer());
                var foreignApplicationsToRemove = existingMA.ForeignApplications.Except(ma.ForeignApplications, new IDComparer());
                //TODO: make sure that this code returns(ma.MAChecklists.FirstOrDefault().ResponderTypeID) the same respondertype with existing ma
                var maChecklistsToRemove = existingMA.MAChecklists.Where(mac => mac.ResponderTypeID == ma.MAChecklists.FirstOrDefault().ResponderTypeID).Except(ma.MAChecklists, new IDComparer());
                var maPaymentsToRemove = existingMA.MAPayments.Except(ma.MAPayments, new IDComparer());

                //Remove Collections
                RemoveCollection(maAssignmentsToRemove);
                RemoveCollection(foreignApplicationsToRemove);
                RemoveCollection(maChecklistsToRemove);
                RemoveCollection(maPaymentsToRemove);

                _dbSet.Update(ma);
            }
            catch (Exception ex)
            {
                _logger.Log(ex);
            }

        }
    }
}