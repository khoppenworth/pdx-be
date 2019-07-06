using Microsoft.AspNetCore.Mvc;
using PDX.Domain.Staging;
using PDX.BLL.Model.Staging;
using PDX.BLL.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using System.Threading.Tasks;
using System.Collections.Generic;
using PDX.BLL.Model;
using DataTables.AspNet.Core;

namespace PDX.API.Controllers.Staging
{
    // [Authorize]
    [Route("api/[controller]")]
    public class MedicalDeviceMigrationController :CrudBaseController<MedicalDeviceHeader>
    {
        private IMedicalDeviceStagingService _medicalDeviceService;
        public MedicalDeviceMigrationController(IMedicalDeviceStagingService medicalDeviceService)
        :base(medicalDeviceService)
        {
            _medicalDeviceService = medicalDeviceService;
        }

         /// <summary>
        /// Insert or update medical device migration
        /// </summary>
        /// <param name="CreateMigrations">The module</param>
        /// <returns>bool</returns>
        [Route("CreateMigrations")]
        [HttpPost]
        public async Task<ApiResponse> CreateMigrations([FromBody]MedicalDeviceMigration medicalDeviceMigration)
        {
            return await _medicalDeviceService.SaveMedicalDeviceMigration(medicalDeviceMigration);
        }

        
         /// <summary>
        /// Insert or update medical device migration
        /// </summary>
        /// <param name="GetMigrations">The module</param>
        /// <returns>bool</returns>
        [Route("GetMigrations")]
        [HttpGet]
        public async Task<IEnumerable<MedicalDeviceMigration>> GetMigrations()
        {
            return await _medicalDeviceService.GetMedicalDeviceMigration();
        }

         /// <summary>
        /// Migration list for Datatable
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [Route("List")]
        public async Task<BLL.Model.DataTablesResult<MedicalDeviceMigration>> GetMigrationtDT([FromBody]IDataTablesRequest request)
        {
            var result = await _medicalDeviceService.GetPageAsync(request);
            return result;
        }
            
    }
}