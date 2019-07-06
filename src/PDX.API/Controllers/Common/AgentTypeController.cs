using Microsoft.AspNetCore.Mvc;
using PDX.Domain.Common;
using PDX.BLL.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace PDX.API.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    public class AgentTypeController:CrudBaseController<AgentType>
    {
        private readonly IService<AgentType> _agentTypeService;
        public AgentTypeController(IService<AgentType> agentTypeService)
        :base(agentTypeService)
        {
            _agentTypeService = agentTypeService;
        }

        /// <summary>
        /// Get all entities
        /// </summary>
        [HttpGet]
        public override async Task<IEnumerable<AgentType>> GetAllAsync()
        {
            var entities = await _agentTypeService.GetAllAsync(true);
            return entities;
        } 
    }
}