using System.Collections.Generic;
using System.Threading.Tasks;
using PDX.Domain;
using PDX.Domain.Logging;

namespace PDX.BLL.Services.Interfaces
{
    public interface IStatusLogService: IService<StatusLog>
    {
        /// <summary>
        /// status log 
        /// </summary>
        /// <param name="oldEntity"></param>
        /// <param name="newEntity"></param>
        /// <param name="fieldName"></param>
        /// <param name="comment"></param>
        /// <param name="modifiedBy"></param>
        /// <returns></returns>
         Task<bool> LogStatusAsync(BaseEntity oldEntity, BaseEntity newEntity, string fieldName, string comment, int modifiedBy);

        /// <summary>
        ///  Get statusLog by entity type and id
        /// </summary>
        /// <param name="entityType"></param>
        /// <param name="entityID"></param>
        /// <returns></returns>
        Task<IList<StatusLog>> GetStatusLogByEntityID(string entityType, int entityID);
    }
}