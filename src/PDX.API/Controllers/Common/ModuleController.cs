using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using PDX.Domain.Common;
using PDX.Domain.Document;
using PDX.BLL.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using System.Threading.Tasks;

namespace PDX.API.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    public class ModuleController:CrudBaseController<Module>
    {
        private readonly IService<Module> _moduleService;
        public ModuleController(IService<Module> moduleService)
        :base(moduleService)
        {
            _moduleService = moduleService;
        }

         /// <summary>
        /// Get module by code
        /// </summary>
        /// /// <param name="code"></param>
        /// <returns></returns>
        [Route("ByCode/{code}")]
        [HttpGet]
        public async Task<Module> GetModuleByCode(string code)
        {
            var result = await _moduleService.GetAsync(s => s.ModuleCode == code);
            return result;
        }
        
    }
}