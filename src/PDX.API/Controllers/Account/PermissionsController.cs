using Microsoft.AspNetCore.Mvc;
using PDX.Domain.Common;
using PDX.BLL.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using PDX.Domain.Account;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace PDX.API.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    public class PermissionsController:BaseController<Permission>
    {
        private readonly IService<Permission> _service;
        public PermissionsController(IService<Permission> service)
        :base(service)
        {
            _service = service;
        }

        /// <summary>
        /// Get all permissions
        /// </summary>
        /// <returns>List of <typeparamref name="Permission"/></returns>
        //override api endpoint to make it public
        [AllowAnonymous] 
        [HttpGet]
        public override async Task<IEnumerable<Permission>> GetAllAsync()
        {
            var result = await _service.GetAllAsync(true);
            return result;
        }
    }
}