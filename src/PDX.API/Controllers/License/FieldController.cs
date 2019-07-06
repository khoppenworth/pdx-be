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
    // [Authorize]
    [Route("api/[controller]")]
    public class FieldController : BaseController<Field>
    {
        private readonly IFieldService _fieldService;
        public FieldController(IFieldService fieldService) : base(fieldService)
        {
            _fieldService = fieldService;
        }

        
         /// <summary>
        /// Get fields by type
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("ByType/{isVariationType}/{submoduleTypeCode?}")]
        public async Task<IEnumerable<FieldSubmoduleType>> GetFieldByTypeAsync(bool? isVariationType = null,string submoduleTypeCode = null)
        {
            var result = await _fieldService.GetFieldByTypeAsync(isVariationType,submoduleTypeCode);
            return result;
        }

        /// <summary>
        /// Get fields by MA
        /// </summary>
        /// <param name="maID"></param>
        /// <param name="isVariationType"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("ByMA/{maID}/{isVariationType}/{submoduleTypeCode?}")]
        public async Task<IEnumerable<FieldSubmoduleType>> GetFieldByMAAsync(int maID, bool? isVariationType = null,string submoduleTypeCode = null)
        {
            var result = await _fieldService.GetFieldByMA(maID, isVariationType,submoduleTypeCode);
            return result;
        }

        /// <summary>
        /// Get MA Fields by MA
        /// </summary>
        /// <param name="maID"></param>
        /// <param name="isVariationType"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("MAField/{maID}/{isVariationType}")]
        public async Task<IEnumerable<MAFieldSubmoduleType>> GetMAFieldByMAAsync(int maID, bool? isVariationType = null)
        {
            var result = await _fieldService.GetMAFieldByMA(maID, isVariationType);
            return result;
        }

        /// <summary>
        /// Save MA Fields
        /// </summary>
        /// <param name="maFields"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("MAField")]
        public async Task<bool> SaveMAFieldsAsync([FromBody]IList<MAFieldSubmoduleType> maFields)
        {
            var result = await _fieldService.SaveMAField(maFields);
            return result;
        }

        /// <summary>
        /// Insert or update MAField
        /// </summary>
        /// <param name="maFields">The maField</param>
        /// <returns>bool</returns>
        [Route("MAField/InsertOrUpdate")]
        [HttpPost]
        public async Task<bool> InsertOrUpdateAsync([FromBody]IList<MAFieldSubmoduleType> maFields)
        {
            var result = await _fieldService.SaveMAField(maFields);
            return result;
        }
    }
}