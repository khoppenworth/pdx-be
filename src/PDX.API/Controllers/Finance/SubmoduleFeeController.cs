using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PDX.BLL.Services.Interfaces;
using PDX.Domain.Document;
using System.Linq;
using PDX.Domain.Finance;

namespace PDX.API.Controllers
{
    /// <summary>
    /// SubmoduleFee Controller
    /// </summary>
    [Authorize]
    [Route("api/[controller]")]
    public class SubmoduleFeeController : BaseController<SubmoduleFee>
    {
        private readonly IService<SubmoduleFee> _submoduleFeeService;
        /// <summary>
        /// Constructor for SubmoduleFeeController
        /// </summary>
        /// <param name="submoduleFeeService"></param>
        public SubmoduleFeeController(IService<SubmoduleFee> submoduleFeeService) : base(submoduleFeeService)
        {
            _submoduleFeeService = submoduleFeeService;
        }

        /// <summary>
        /// Get SubmoduleFees by ModuleCode
        /// </summary>
        /// <param name="id"></param>
        /// <param name="active"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("BySubmodule/{id}/{active}")]
        public async Task<IList<SubmoduleFee>> GetBySubmodule(int id, bool? active){
            var submoduleFees = (await _submoduleFeeService.FindByAsync(md => md.Submodule.ID == id && (active == null || (active!=null && md.IsActive == active)))).Distinct().ToList();
            return submoduleFees;
        }

        /// <summary>
        /// Insert or update submoduleFees
        /// </summary>
        /// <param name="submoduleFees">The module</param>
        /// <returns>bool</returns>
        [Route("InsertOrUpdate")]
        [HttpPost]
        public async Task<bool> InsertOrUpdateAsync([FromBody]IList<SubmoduleFee> submoduleFees)
        {
            var result = await _submoduleFeeService.CreateOrUpdateAsync(submoduleFees, true, "SubmoduleID", "FeeTypeID");
            return result;
        }
    }
}