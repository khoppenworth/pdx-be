using PDX.Domain.Common;
using PDX.BLL.Services.Interfaces;
using System.Threading.Tasks;
using System.Collections.Generic;
using PDX.DAL.Repositories;

namespace PDX.BLL.Services
{
    public class PortOfEntrySerivce:Service<PortOfEntry>,IPortOfEntryService
    {
         public PortOfEntrySerivce(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }

       /// <summary>
        /// Gets shipping method ports 
        /// </summary>
        /// <param name="shippingID"></param>
        /// <returns></returns>
        public async Task<IEnumerable<PortOfEntry>> GetShippingPortOfEntry(int shippingID)
        {
            var ports = await FindByAsync(x=>x.ShippingMethodID==shippingID);
            return ports;
        }
    }
}