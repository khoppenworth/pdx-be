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
    public class ChangeLogController : BaseController<ChangeLog>
    {
        private readonly IChangeLogService _service;
        public ChangeLogController(IChangeLogService changeLogService)
        :base(changeLogService)
        {
            _service = changeLogService;
        }


        /// <summary>
        /// Get all ChangeLogs
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public override async Task<IEnumerable<ChangeLog>> GetAllAsync()
        {
            var result = await _service.GetAllAsync();
            return result;
        }

         /// <summary>
        /// Insert or update WPI
        /// </summary>
        /// <param name="changeLog">The module</param>
        /// <returns>bool</returns>
        [Route("InsertOrUpdate")]
        [HttpPost]
        public async Task<bool> InsertOrUpdateAsync([FromBody]ChangeLog changeLog)
        {
            var result = await _service.CreateOrUpdateAsync(changeLog);
            return result;
        }
    }
}
