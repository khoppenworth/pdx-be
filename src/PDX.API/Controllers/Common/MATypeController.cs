using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PDX.BLL.Services.Interfaces;
using PDX.Domain.Common;

namespace PDX.API.Controllers {
    [Authorize]
    [Route ("api/[controller]")]
    public class MATypeController : CrudBaseController<MAType> {
        private readonly IService<MAType> _service;
        private readonly IService<Submodule> _submoduleCode;
        /// <summary>
        /// constructor of MATypeControllers
        /// </summary>
        /// <param name="service"></param>
        public MATypeController (IService<MAType> service, IService<Submodule> submoduleCode) : base (service) {
            _service = service;
            _submoduleCode = submoduleCode;
        }

        /// <summary>
        /// Get MA type by its module code
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        [HttpGet]
        [Route ("ByModuleCode/{code}/{submoduleTypeCode?}")]
        public async Task<IEnumerable<MAType>> GetMATypeByModuleCodeAsync (string code, string submoduleTypeCode = null) {

            var submodules = (await _submoduleCode.FindByAsync (s => s.Module.ModuleCode == code)).Select (sm => sm.SubmoduleCode).ToList ();
            var result = await _service.FindByAsync (s => submodules.Contains (s.MATypeCode) && ((submoduleTypeCode != null && s.SubmoduleType.SubmoduleTypeCode == submoduleTypeCode) || submoduleTypeCode == null));
            return result;
        }

    }
}