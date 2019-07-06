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
    public class ImportPermitTypeController : CrudBaseController<ImportPermitType>
    {
        private readonly IService<ImportPermitType> _service;
        /// <summary>
        /// constructor of ImportPermitTypeControllers
        /// </summary>
        /// <param name="service"></param>
        public ImportPermitTypeController(IService<ImportPermitType> service)
        :base(service)
        {
            _service = service;
        }

        /// <summary>
        /// Get import permit type by its code
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("ByCode/{code}")]
        public async Task<ImportPermitType> GetImportPermitTypeByCodeAsync(string code)
        {
            var result = await _service.GetAsync(s => s.ImportPermitTypeCode ==  code);
            return result;
        }
    }
}
