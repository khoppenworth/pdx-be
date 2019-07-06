using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PDX.BLL.Services.Interfaces;
using PDX.Domain.Commodity;

namespace PDX.API.Controllers {
    [Authorize]
    [Route ("api/[controller]")]
    public class DosageFormController : BaseController<DosageForm> {
        private readonly IService<DosageForm> _dosageFormService;
        public DosageFormController (IService<DosageForm> dosageFormService) : base (dosageFormService) {
            _dosageFormService = dosageFormService;
        }
        /// <summary>
        /// Get all entities
        /// </summary>
        /// <returns>List of <typeparamref name="T"/></returns>
        [HttpGet]
        public override async Task<IEnumerable<DosageForm>> GetAllAsync () {
            var entities = await _dosageFormService.FindByAsync (s => s.SubmoduleType.SubmoduleTypeCode == "MDCN" && s.IsActive);
            return entities;
        }


        /// <summary>
        /// Get dosage form by its module code
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        [HttpGet]
        [Route ("BySubmoduleTypeCode")]
        public async Task<IEnumerable<DosageForm>> GetBySubmoduleTypeCode ([FromQuery]string submoduleTypeCode = "MDCN") {

            var result = await _dosageFormService.FindByAsync (s => s.SubmoduleType.SubmoduleTypeCode == submoduleTypeCode);
            return result;
        }

    }
}