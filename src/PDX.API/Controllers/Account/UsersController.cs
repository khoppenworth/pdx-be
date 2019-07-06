using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using PDX.Domain.Account;
using PDX.Domain.Views;
using PDX.BLL.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using DataTables.AspNet.Core;
using PDX.API.Helpers;

namespace PDX.API.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    public class UsersController: CrudBaseController<User>
    {
        private readonly IUserService _userService;
        public UsersController(IUserService userService): base(userService)
        {
            _userService = userService;
        }

        /// <summary>
        /// User list for Datatable
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [Route("List")]
        public async Task<BLL.Model.DataTablesResult<vwUser>> GetUsersDT([FromBody]IDataTablesRequest request)
        {
            var result = await _userService.GetUserPage(request);
            return result;
        }

         /// <summary>
         /// Get Users by Role
         /// </summary>
         /// <param name="roleID"></param>
         /// <returns></returns>
         [Route("ByRole/{roleID}")]
         [HttpGet]
         public async Task<List<User>> GetUsersByRole(int roleID){
            var result = await _userService.GetUsersByRole(roleID,this.HttpContext.GetUserID());
            return result;
         }

          /// <summary>
         /// Get Users by Agent
         /// </summary>
         /// <param name="agentID"></param>
         /// <returns></returns>
         [Route("ByAgent/{agentID}")]
         [HttpGet]
         public async Task<List<User>> GetUsersByAgent(int agentID){

            var result = await _userService.GetUsersByAgent(agentID);
            return result;
         }

           /// <summary>
         /// Get Users with role of Agent but not linked with any agent
         /// </summary>
         /// <returns></returns>
         [Route("AgentUsers")]
         [HttpGet]
         public async Task<List<User>> GetAgentUsers(){

            var result = await _userService.GetAgentUsers();
            return result;
         }         
    }
}