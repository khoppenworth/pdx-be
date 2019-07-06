using System.Collections.Generic;
using System.Threading.Tasks;
using DataTables.AspNet.Core;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PDX.API.Helpers;
using PDX.BLL.Model;
using PDX.BLL.Services.Interfaces;
using PDX.Domain.License;
using PDX.Domain.Views;

namespace PDX.API.Controllers.License
{
    [Authorize]
    [Route("api/[controller]")]
    public class MALogStatusController : BaseController<MALogStatus>
    {
        private readonly IMALogStatusService _maLogStatusService;
        public MALogStatusController(IMALogStatusService maLogStatusService) : base(maLogStatusService)
        {
            _maLogStatusService = maLogStatusService;
        }


        /// <summary>
        /// Insert new MA Status Log
        /// </summary>
        /// <param name="maLogStatus">The t.</param>
        /// <returns>bool</returns>
        [HttpPost]
        public virtual async Task<bool> CreateAsync([FromBody]MALogStatus maLogStatus)
        {
            var result = await _maLogStatusService.CreateAsync(maLogStatus);
            return result;
        }
        
        /// <summary>
        /// Get MA History
        /// </summary>
        /// <param name="maID"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("History/{maID}")]
        public async Task<IEnumerable<MALogStatus>> GetMAStatusHistory(int maID)
        {
            var userID = this.HttpContext.GetUserID();
            var logs = await _maLogStatusService.GetMAStatusHistory(maID, userID);
            return logs;
        }

    }
}