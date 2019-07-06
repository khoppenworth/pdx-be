using Microsoft.AspNetCore.Mvc;
using PDX.Domain.Common;
using PDX.BLL.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;

namespace PDX.API.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    public class MAStatusController : CrudBaseController<MAStatus>
    {
        private readonly IService<MAStatus> _service;
        /// <summary>
        /// constructor of MAStatusControllers
        /// </summary>
        /// <param name="service"></param>
        public MAStatusController(IService<MAStatus> service)
        :base(service)
        {
            _service = service;
        }

        /// <summary>
        /// Get MA status by its code
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("ByCode/{code}")]
        public async Task<IEnumerable<MAStatus>> GetMAStatusByCodeAsync(string code)
        {
            var result = await _service.FindByAsync(s => s.MAStatusCode == code);
            return result;
        }
    }
}
