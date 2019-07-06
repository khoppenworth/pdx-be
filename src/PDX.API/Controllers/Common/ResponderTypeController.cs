using Microsoft.AspNetCore.Mvc;
using PDX.Domain.Common;
using PDX.BLL.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PDX.API.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    public class ResponderTypeController:CrudBaseController<ResponderType>
    {
        private readonly IService<ResponderType> _responderTypeService;
        public ResponderTypeController(IService<ResponderType> responderTypeService)
        :base(responderTypeService)
        {
            _responderTypeService = responderTypeService;
        }

        /// <summary>
        /// Get reponder type by its code
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("ByCode/{code}")]
        public async Task<IEnumerable<ResponderType>> GetResponderTypeByCodeAsync(string code)
        {
            var responderType = await _responderTypeService.FindByAsync(dt => dt.ResponderTypeCode == code);
            return responderType;
        }
    }
}