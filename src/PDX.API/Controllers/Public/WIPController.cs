using Microsoft.AspNetCore.Mvc;
using PDX.Domain.Common;
using PDX.BLL.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using System.Threading.Tasks;
using System.Collections.Generic;
using PDX.Domain.Public;

namespace PDX.API.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    public class WIPController : BaseController<WIP>
    {
        private readonly IWIPService _service;
        public WIPController(IWIPService wipService)
        :base(wipService)
        {
            _service = wipService;
        }


        /// <summary>
        /// Get all WIPs
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public override async Task<IEnumerable<WIP>> GetAllAsync()
        {
            var result = await _service.GetAllAsync();
            return result;
        }

        /// <summary>
        /// Get WIP by ID
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public override async Task<WIP> GetAsync(int id)
        {
            var result = await _service.GetAsync(id);
            return result;
        }

        /// <summary>
        /// Get WIP by User
        /// </summary>
        /// <param name="type"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        [HttpGet("ByUser/{type}/{userId}")]
        public async Task<IEnumerable<WIP>> GetByUserAsync(string type , int userId)
        {
            var result = await _service.FindByAsync(s => s.IsActive && s.Type == type && s.UserID == userId);
            return result;
        }

         /// <summary>
        /// Insert or update WPI
        /// </summary>
        /// <param name="wip">The module</param>
        /// <returns>bool</returns>
        [Route("InsertOrUpdate")]
        [HttpPost]
        public async Task<bool> InsertOrUpdateAsync([FromBody]WIP wip)
        {
            var result = await _service.CreateOrUpdateAsync(wip);
            return result;
        }

        /// <summary>
        /// Deletes existing wip by id.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>bool</returns>
        [HttpDelete("{id}")]
        public virtual async Task<bool> DeleteAsync(int id)
        {
            var result = await _service.DeleteAsync(id);
            return result;
        }
    }
}
