using System.Collections.Generic;
using System.Threading.Tasks;
using PDX.Domain.Procurement;

namespace PDX.BLL.Services.Interfaces
{
    public interface IIpermitLogStatusService : IService<ImportPermitLogStatus>
    {
        Task<IEnumerable<ImportPermitLogStatus>> GetIPermitStatusHistory(int ipermitID, bool isAgent);
        Task<ImportPermitLogStatus> GetIPermitStatusByCode(string statusCode,int ipermitID);
        Task<List<ImportPermitLogStatus>> GetIPermitStatus(int ipermitID);

    }
}