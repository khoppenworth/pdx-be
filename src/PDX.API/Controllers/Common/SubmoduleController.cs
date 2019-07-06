using System.Linq;
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
    public class SubmoduleController:BaseController<Submodule>
    {
        private readonly IService<Submodule> _submoduleService;
        public SubmoduleController(IService<Submodule> submoduleService)        
        :base(submoduleService)
        {
            _submoduleService = submoduleService;
        }        
        /// <summary>
        /// Get ModuleDocuments by Module
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("ByModule/{id}")]
        public async Task<IList<Submodule>> GetByModule(int id){
            var moduleDocuments = (await _submoduleService.FindByAsync(md => md.ModuleID == id)).ToList();
            return moduleDocuments;
        }
    }
}