using System.Collections.Generic;
using System.Threading.Tasks;
using PDX.BLL.Services.Interfaces;
using PDX.DAL.Repositories;
using PDX.Domain.Procurement;
using System.Linq;
namespace PDX.BLL.Services
{
    public class IpermitLogStatusService : Service<ImportPermitLogStatus>, IIpermitLogStatusService
    {

        public IpermitLogStatusService(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }

        public async Task<IEnumerable<ImportPermitLogStatus>> GetIPermitStatusHistory(int ipermitID, bool isAgent)
        {
            var statuses = new List<string>() { "DRFT", "RQST", "WITH", "RTA", "REJ", "APR", "VOID" ,"SFA"};

            var logs = (await base.FindByAsync(s => s.ImportPermitID == ipermitID));
            if (isAgent)
            {
                logs = logs.Where(l => statuses.Contains(l.ToImportPermitStatus.ImportPermitStatusCode));
            }
            return logs.OrderBy(s => s.CreatedDate);
        }

        public async Task<ImportPermitLogStatus> GetIPermitStatusByCode(string statusCode,int ipermitID)
        {

            var status = (await base.FindByAsync(s => s.ToImportPermitStatus.ImportPermitStatusCode == statusCode && s.ImportPermitID==ipermitID))
            .OrderByDescending(s => s.CreatedDate).FirstOrDefault();
            return status;
        }

          public async Task<List<ImportPermitLogStatus>> GetIPermitStatus(int ipermitID)
        {

            var status = (await base.FindByAsync(s => s.ImportPermitID==ipermitID))
            .OrderByDescending(s => s.CreatedDate).ToList();
            return status;
        }
    }
}