using Microsoft.AspNetCore.Mvc;
using PDX.Domain.Common;
using PDX.BLL.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace PDX.API.Controllers
{
    [Authorize(Policy = "RequireElevatedRights")]
    [Route("api/[controller]")]
    public class SystemSettingController:CrudBaseController<SystemSetting>
    {
        private readonly IService<SystemSetting> _systemSettingService;
        public SystemSettingController(IService<SystemSetting> systemSettingService)
        :base(systemSettingService)
        {
            _systemSettingService = systemSettingService;
        }
        
        /// <summary>
        /// Get all permissions
        /// </summary>
        /// <returns>List of <typeparamref name="SystemSetting"/></returns>        
        [AllowAnonymous] //override api endpoint to make it public
        [HttpGet]
        public override async Task<IEnumerable<SystemSetting>> GetAllAsync()
        {
            var result = await _systemSettingService.GetAllAsync();
            return result;
        }

         /// <summary>
        /// Insert or update systemSettings
        /// </summary>
        /// <param name="systemSettings">The module</param>
        /// <returns>bool</returns>
        [Route("InsertOrUpdate")]
        [HttpPost]
        public async Task<bool> InsertOrUpdateAsync([FromBody]IList<SystemSetting> systemSettings)
        {
            var result = await _systemSettingService.CreateOrUpdateAsync(systemSettings,true, "SystemSettingCode");
            return result;
        }
    }
}