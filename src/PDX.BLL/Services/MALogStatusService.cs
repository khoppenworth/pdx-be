using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PDX.BLL.Services.Interfaces;
using PDX.DAL.Repositories;
using PDX.Domain.License;

namespace PDX.BLL.Services
{
    public class MALogStatusService : Service<MALogStatus>, IMALogStatusService
    {
        private readonly IUserService _userService;
        public MALogStatusService(IUnitOfWork unitOfWork, IUserService userService) : base(unitOfWork)
        {
            _userService = userService;
        }

        public async Task<IEnumerable<MALogStatus>> GetMAStatusHistory(int maID, int userID)
        {
            IList<string> statuses = new List<string>();
            var user = await _userService.GetAsync(userID);
            var role = user.Roles.FirstOrDefault();

            switch (role.RoleCode)
            {
                case "IPA":
                    statuses = new List<string>() { "DRFT", "RQST", "WITH", "RTA", "RTAR", "REJ", "PRSC", "FATCH", "VER", "FIRR", "FIR", "APR", "REJ", "VOID" };
                    break;
                case "CSD":
                case "CST":
                case "CSO":
                    statuses = new List<string>() { "RQST", "RTAR", "PRSC", "FATCH", "VER" };
                    break;
                case "ROLE_MODERATOR":
                case "ROLE_REVIEWER":
                    statuses = new List<string>() { "VER", "FIRR", "FIR", "SFA", "SFR", "SFIR", "RTAS", "LARQ", "LARS", "APR", "REJ", "VOID","RTL" };
                    break;
                case "ROLE_FOOD_REVIEWER":
                    statuses = new List<string>() { "RQST", "RTAR", "PRSC", "FATCH", "VER", "FIRR", "FIR", "SFA", "SFR", "SFIR", "RTAS", "LARQ", "LARS", "APR", "REJ", "VOID","RTL" };
                    break;
            }


            var logs = await base.FindByAsync(s => s.MAID == maID && s.MA.IsActive);
            if (statuses.Count > 0)
            {
                logs = logs?.Where(l => statuses.Contains(l.ToMAStatus.MAStatusCode));
            }
            return logs;
        }

        public async Task<MALogStatus> GetMALogStatus(int maID, string maStatusCode)
        {
            var maLogStatus = await base.GetAsync(mag => mag.MAID == maID && mag.ToMAStatus.MAStatusCode == maStatusCode);
            return maLogStatus;
        }

         public async Task<IEnumerable<MALogStatus>> GetMALogStatuses(int maID, string maStatusCode)
        {
            var maLogStatus = await base.FindByAsync(mag => mag.MAID == maID && mag.ToMAStatus.MAStatusCode == maStatusCode);
            return maLogStatus;
        }
    }
}