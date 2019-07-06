using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PDX.BLL.Services.Interfaces;
using PDX.Domain.Document;
using System.Linq;
using PDX.Domain.Finance;
using PDX.Domain.Catalog;

namespace PDX.API.Controllers
{
    /// <summary>
    /// SubmoduleChecklist Controller
    /// </summary>
    [Authorize]
    [Route("api/[controller]")]
    public class SubmoduleChecklistController : CrudBaseController<SubmoduleChecklist>
    {
        private readonly ISubmoduleChecklistService _submoduleChecklistService;
        /// <summary>
        /// Constructor for SubmoduleChecklistController
        /// </summary>
        /// <param name="submoduleChecklistService"></param>
        public SubmoduleChecklistController(ISubmoduleChecklistService submoduleChecklistService) : base(submoduleChecklistService)
        {
            _submoduleChecklistService = submoduleChecklistService;
        }

        /// <summary>
        /// Get SubmoduleChecklists by ModuleCode
        /// </summary>
        /// <param name="id"></param>
        /// <param name="active"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("BySubmodule/{id}/{active}")]
        public async Task<IList<SubmoduleChecklist>> GetBySubmodule(int id, bool? active){
            var submoduleChecklists = (await _submoduleChecklistService.FindByAsync(md => md.Submodule.ID == id && (active == null || (active!=null && md.IsActive == active)))).Distinct().ToList();
            return submoduleChecklists;
        }

        /// <summary>
        /// Insert new submoduleChecklist
        /// </summary>
        /// <param name="submoduleChecklist">The module</param>
        /// <returns>bool</returns>
        [HttpPost]
        public override async Task<bool> CreateAsync([FromBody]SubmoduleChecklist submoduleChecklist)
        {
            var result = await _submoduleChecklistService.CreateAsync(submoduleChecklist);
            return result;
        }

        /// <summary>
        /// Delete submoduleChecklist
        /// </summary>
        /// <param name="submoduleChecklist">The module</param>
        /// <returns>bool</returns>
        [HttpPost]
        [Route("Delete")]
        public async Task<bool> DeleteSubmoduleChecklistAsync([FromBody]SubmoduleChecklist submoduleChecklist)
        {
            var result = await _submoduleChecklistService.DeleteSubmoduleChecklistAsync(submoduleChecklist);
            return result;
        }
    }
}