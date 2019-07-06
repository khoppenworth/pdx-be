using System.Collections.Generic;
using System.Threading.Tasks;
using PDX.Domain.Common;
using PDX.Domain.License;

namespace PDX.BLL.Services.Interfaces
{
    public interface IMAAssignmentService:IService<MAAssignment>
    {
         Task<ResponderType> GetResponderType(int maID, int userID);
         Task<object> GetGroupedMAAssignments(int maID);
         Task<IList<MAAssignment>> GetMAAssignments(int maID);
    }
}