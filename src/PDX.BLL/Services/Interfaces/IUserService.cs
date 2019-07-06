using System.Threading.Tasks;
using System.Collections.Generic;
using DataTables.AspNet.Core;
using PDX.BLL.Model;
using PDX.Domain.Account;
using PDX.Domain.Views;

namespace PDX.BLL.Services.Interfaces
{
    public interface IUserService:IService<User>
    {
        /// <summary>
        /// Get Users by Role
        /// </summary>
        /// <param name="roleID"></param>
        /// <returns></returns>
        Task<List<User>> GetUsersByRole(int roleID,int userId);

        /// <summary>
        /// Get Users by Agent
        /// </summary>
        /// <param name="agentID"></param>
        /// <returns></returns>
        Task<List<User>> GetUsersByAgent(int agentID);

        /// <summary>
        /// Get Users by Username
        /// </summary>
        /// <param name="username"></param>
        /// <returns></returns>
        Task<User> GetUserByUsername(string username);

        /// <summary>
        /// Get Users role of agent but not linked with any agent 
        /// </summary>
        /// <returns></returns>
        Task<List<User>> GetAgentUsers();

        /// <summary>
        /// Users page
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<DataTablesResult<vwUser>> GetUserPage(IDataTablesRequest request);
    }
}