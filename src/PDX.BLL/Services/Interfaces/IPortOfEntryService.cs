using PDX.Domain.Common;
using PDX.BLL.Services.Interfaces;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace PDX.BLL.Services.Interfaces
{
    public interface IPortOfEntryService:IService<PortOfEntry>
    {
        /// <summary>
        /// Gets shipping method ports 
        /// </summary>
        /// <param name="shippingID"></param>
        /// <returns></returns>
       Task<IEnumerable<PortOfEntry>> GetShippingPortOfEntry(int shippingID);
    }
}