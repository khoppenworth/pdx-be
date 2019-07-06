using System.Collections.Generic;
using System.Threading.Tasks;
using PDX.Domain.Account;
using PDX.Domain.Customer;
using PDX.Domain.Views;
using PDX.BLL.Model;
using DataTables.AspNet.Core;
using PDX.Domain.Common;

namespace PDX.BLL.Services.Interfaces
{
    public interface IAgentService : IService<Agent>
    {
        /// <summary>
        /// GetAgentByUser
        /// </summary>
        /// <param name="userID"></param>
        /// <returns></returns>
        Task<List<Agent>> GetAgentByUser(int userID);

        /// <summary>
        /// Create User Agent
        /// </summary>
        /// <param name="userAgent"></param>
        /// <returns></returns>
        Task<bool> CreateUserAgent(UserAgent userAgent);
       /// <summary>
       /// Insert or Update AgentSupplier
       /// </summary>
       /// <param name="agentSupplier"></param>
       /// <param name="modifiedBy"></param>
       /// <returns></returns>
        Task<bool> CreateOrUpdateAgentSupplier(AgentSupplier agentSupplier, int? modifiedBy = null);
        /// <summary>
        /// Agents by Supplier
        /// </summary>
        /// <param name="supplierID"></param>
        /// <returns></returns>
        Task<List<Agent>> GetAgentBySupplier(int supplierID);
        /// <summary>
        /// AgentSuppiers by Supplier
        /// </summary>
        /// <param name="supplierID"></param>
        /// <returns></returns>
        Task<List<AgentSupplier>> GetAgentSuppliersBySupplier(int supplierID);
        /// <summary>
        /// AgentSuppiers by Agent
        /// </summary>
        /// <param name="agentID"></param>
        /// <returns></returns>
        Task<List<AgentSupplier>> GetAgentSuppliersByAgent(int agentID);
        /// <summary>
        /// AgentSuppiers by Agent and Supplier
        /// </summary>
        /// <param name="agentID"></param>
        /// <returns></returns>
        Task<AgentSupplier> GetAgentSuppliersBySupplierAndAgent(int supplierID, int agentID);
        /// <summary>
        /// Agents page
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<DataTablesResult<vwAgent>> GetAgentPage(IDataTablesRequest request);

        /// <summary>
        /// /// Agent list for ipermit
        /// </summary>
        /// <returns></returns>
        Task<List<Agent>> GetAgentsForIpermit();

        /// <summary>
        ///  Agent list by agent type code
        /// </summary>
        /// <param name="agentTypeCode"></param>
        /// <returns></returns>
        Task<List<Agent>> GetAgentsByTypeCode(string agentTypeCode);

        /// <summary>
        /// Get AgentLevel by UserID and supplierID
        /// </summary>
        /// <param name="userID"></param>
        /// <param name="supplierID"></param>
        /// <returns></returns>
        Task<AgentLevel> GetAgentLevelByUserAndSupplier(int userID, int supplierID);
    }
}