using System.Collections.Generic;
using System.Threading.Tasks;
using PDX.Domain.License;

namespace PDX.BLL.Services.Interfaces
{
    public interface IMALogStatusService:IService<MALogStatus>
    {
         Task<IEnumerable<MALogStatus>> GetMAStatusHistory(int maID, int userID);

         Task<MALogStatus> GetMALogStatus(int maID, string maStatusCode);
         Task<IEnumerable<MALogStatus>> GetMALogStatuses(int maID, string maStatusCode);
    }
}