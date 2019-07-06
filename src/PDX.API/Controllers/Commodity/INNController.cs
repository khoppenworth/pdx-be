using Microsoft.AspNetCore.Mvc;
using PDX.Domain.Commodity;
using PDX.BLL.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using System.Threading.Tasks;
using System.Collections.Generic;
using PDX.Domain.Common;

namespace PDX.API.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    public class INNController : CrudBaseController<INN>
    {
        private readonly IService<INN> _innService;
        private readonly IService<SubmoduleType> _submoduleTypeService;
        public INNController(IService<INN> innService,IService<SubmoduleType> submoduleTypeService)
        : base(innService)
        {
            _innService = innService;
            _submoduleTypeService = submoduleTypeService;
        } 

        /// <summary>
        /// create inn
        /// </summary>
        /// <param name="inn">inn</param>
        /// <returns>bool</returns>
        [HttpPost]
        public override async Task<bool> CreateAsync([FromBody]INN inn)
        {
            var submoduleType = await _submoduleTypeService.GetAsync(sb=>sb.SubmoduleTypeCode==inn.SubmoduleTypeCode);
            inn.SubmoduleTypeID = submoduleType.ID;
            var result = await _innService.CreateAsync(inn);
            return result;
        }    

    }
}