using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PDX.BLL.Services.Interfaces;
using PDX.Domain.Catalog;

namespace PDX.API.Controllers {
    [Authorize]
    [Route ("api/[controller]")]
    public class ChecklistController : BaseController<Checklist> {
        private readonly IChecklistService _checklistService;
        public ChecklistController (IChecklistService checklistService) : base (checklistService) {
            _checklistService = checklistService;
        }

        /// <summary>
        /// Insert or update Checklist
        /// </summary>
        /// <param name="checklists">The module</param>
        /// <returns>bool</returns>
        [Route ("InsertOrUpdate")]
        [HttpPost]
        public async Task<bool> InsertOrUpdateAsync ([FromBody] IList<Checklist> checklists) {
            var result = await _checklistService.CreateOrUpdateAsync (checklists);
            return result;
        }

        /// <summary>
        /// Get checklist by type
        /// </summary>
        /// <param name="checklistID"></param>
        /// <returns></returns>
        [HttpGet]
        [Route ("ByType/{checklistID}")]
        public async Task<IEnumerable<Checklist>> GetChecklistByTypeAsync (int checklistID) {
            var result = await _checklistService.GetChecklistByTypeAsync (checklistID);
            return result;
        }

        /// <summary>
        /// Get checklist by module
        /// </summary>
        /// <param name="moduleCode"></param>
        /// <param name="checklistTypeCode"></param>
        /// <param name="isSra"></param>
        /// <returns></returns>
        [HttpGet]
        [Route ("ByModule/{moduleCode}/{checklistTypeCode}/{isSra}")]
        public async Task<IEnumerable<Checklist>> GetChecklistByModuleAsync (string moduleCode, string checklistTypeCode, bool? isSra) {
            var result = await _checklistService.GetChecklistByModuleAsync (moduleCode, checklistTypeCode, isSra);
            return result;
        }

        /// <summary>
        /// Get checklist by submodule
        /// </summary>
        /// <param name="submoduleCode"></param>
        /// <param name="checklistTypeCode"></param>
        /// <param name="isSra"></param>
        /// <returns></returns>
        [HttpGet]
        [Route ("BySubmodule/{submoduleCode}/{checklistTypeCode}/{isSra}")]
        public async Task<IEnumerable<Checklist>> GetChecklistBySubmoduleAsync (string submoduleCode, string checklistTypeCode, bool? isSra) {
            var result = await _checklistService.GetChecklistBySubmoduleAsync (submoduleCode, checklistTypeCode == "null" ? null : checklistTypeCode, isSra);
            return result;
        }

        /// <summary>
        /// Get answered checklists
        /// </summary>
        /// <param name="maID"></param>
        /// <param name="submoduleCode"></param>
        /// <param name="userID"></param>
        /// <returns></returns>
        [HttpGet]
        [Route ("Answered/{maID}/{submoduleCode}/{userID}/{isSra}")]
        public async Task<IEnumerable<Checklist>> GetAnsweredChecklistsAsync (int maID, string submoduleCode, int userID, bool? isSra) {
            var result = await _checklistService.GetChecklistByMAAsync (maID, submoduleCode, userID, isSra);
            return result;
        }

        /// <summary>
        /// Get answered checklists by checklist type
        /// </summary>
        /// <param name="maID"></param>
        /// <param name="submoduleCode"></param>
        /// <param name="userID"></param>
        /// <returns></returns>
        [HttpGet]
        [Route ("Answered/ByType/{maID}/{submoduleCode}/{checklistTypeCode}")]
        public async Task<IEnumerable<Checklist>> GetAnsweredChecklistsAsync (int maID, string submoduleCode, string checklistTypeCode) {
            var result = await _checklistService.GetChecklistByMAAsync (maID, submoduleCode, checklistTypeCode);
            return result;
        }

        /// <summary>
        /// Get  checklists by checklist type and submoduleType
        /// </summary>
        /// <param name="checklistTypeCode"></param>
        /// <param name="submoduleTypeCode"></param>
        /// <returns></returns>
        [HttpGet]
        [Route ("BySubmoduleType/{checklistTypeCode}/{submoduleTypeCode}")]
        public async Task<IEnumerable<Checklist>> GetChecklistBySubmoduleType (string checklistTypeCode, string submoduleTypeCode) {
            var result = await _checklistService.GetChecklistBySubmoduleType (checklistTypeCode, submoduleTypeCode);
            return result;
        }
    }
}