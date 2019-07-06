using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DataTables.AspNet.Core;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.NodeServices;
using PDX.API.Helpers;
using PDX.API.Services.Interfaces;
using PDX.BLL.Model;
using PDX.BLL.Services.Interfaces;
using PDX.Domain.Common;
using PDX.Domain.Document;
using PDX.Domain.License;
using PDX.Domain.Views;

namespace PDX.API.Controllers.License {
    [Authorize]
    [Route ("api/[controller]")]
    public class MAController : CrudBaseController<MA> {
        private readonly IMAService _maService;
        private readonly INodeServices _nodeServices;
        private readonly IGenerateDocuments _generateDocument;
        private readonly IService<PrintLog> _printLog;

        public MAController (IMAService maService, INodeServices nodeServices, IGenerateDocuments generateDocument, IService<PrintLog> printLog) : base (maService) {
            _maService = maService;
            _nodeServices = nodeServices;
            _generateDocument = generateDocument;
            _printLog = printLog;
        }

        /// <summary>
        /// Get single Ma by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        [Route ("MA/{id}")]
        public async Task<MABusinessModel> GetMAAsync (int id) {
            var entity = await _maService.GetMA (id);
            return entity;
        }
        /// <summary>
        /// Insert new MA New Appplication Type
        /// </summary>
        /// <param name="maNewApp"></param>
        /// <returns></returns>
        [HttpPost]
        [Route ("NewApplication")]
        public async Task<ApiResponse> CreateMANewApplicationAsync ([FromBody] MABusinessModel maNewApp) {
            if (ModelState.IsValid) {
                return new ApiResponse { StatusCode = StatusCodes.Status400BadRequest, IsSuccess = false, Message = "Model is not valid." };
            }
            var result = await _maService.CreateMANewApplicationAsync (maNewApp);
            var ma = await _maService.GetAsync (m => m.RowGuid == maNewApp.MA.RowGuid, true);
            if (ma?.MAStatus?.MAStatusCode == "RQST") {
                await _generateDocument.GenerateRegistrationPDFDocument (_nodeServices, ma.ID, ma.MAStatus.MAStatusCode, ma.CreatedByUserID);
            }
            return result;
        }

        /// <summary>
        /// update existing  MA New Appplication Type
        /// </summary>
        /// <param name="maNewApp"></param>
        /// <returns></returns>
        [HttpPut]
        [Route ("NewApplication")]
        public async Task<ApiResponse> UpdateMANewApplicatioAsync ([FromBody] MABusinessModel maNewApp) {
            var result = await _maService.UpdateMAAsync (maNewApp);
            return result;
        }

        /// <summary>
        /// Insert new MA Renewal Appplication Type
        /// </summary>
        /// <param name="maNewApp"></param>
        /// <returns></returns>
        [HttpPost]
        [Route ("Renewal")]
        public async Task<ApiResponse> CreateMARenewalAsync ([FromBody] MABusinessModel maNewApp) {
            var result = await _maService.CreateMARenewalAsync (maNewApp);
            var ma = await _maService.GetAsync (m => m.RowGuid == maNewApp.MA.RowGuid);
            if (ma.MAStatus?.MAStatusCode == "RQST") {
                await _generateDocument.GenerateRegistrationPDFDocument (_nodeServices, ma.ID, ma.MAStatus.MAStatusCode, ma.CreatedByUserID);
            }
            return result;
        }

        /// <summary>
        /// Update existing  Renewal application type
        /// </summary>
        /// <param name="maNewApp"></param>
        /// <returns></returns>
        [HttpPut]
        [Route ("Renewal")]
        public async Task<ApiResponse> UpdateMARenewalAsync ([FromBody] MABusinessModel maNewApp) {
            var result = await _maService.UpdateMAAsync (maNewApp);
            return result;
        }

        /// <summary>
        /// Insert new MA Variation Appplication Type
        /// </summary>
        /// <param name="maVariation"></param>
        /// <returns></returns>
        [HttpPost]
        [Route ("Variation")]
        public async Task<ApiResponse> CreateMAVariationAsync ([FromBody] MABusinessModel maVariation) {
            var result = await _maService.CreateMAVariationAsync (maVariation);
            var ma = await _maService.GetAsync (m => m.RowGuid == maVariation.MA.RowGuid, true);
            if (ma.MAStatus?.MAStatusCode == "RQST") {
                await _generateDocument.GenerateRegistrationPDFDocument (_nodeServices, ma.ID, ma.MAStatus.MAStatusCode, ma.CreatedByUserID);
            }
            return result;
        }

        /// <summary>
        /// Update existing  Variation application type
        /// </summary>
        /// <param name="maVariation"></param>
        /// <returns></returns>
        [HttpPut]
        [Route ("Variation")]
        public async Task<ApiResponse> UpdateMAVariationAsync ([FromBody] MABusinessModel maVariation) {
            var result = await _maService.UpdateMAAsync (maVariation);
            return result;
        }

        /// <summary>
        /// Get variation changes
        /// </summary>
        /// <param name="maNewApp"></param>
        /// <returns></returns>
        [HttpGet]
        [Route ("Variation/Changes/{maID}")]
        public async Task<IEnumerable<Difference>> GetVariationChangesAsync (int maID) {
            var result = await _maService.GetVariationChangesAsync (maID);
            return result;
        }

        /// <summary>
        /// Get single MA  by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        [Route ("Single/{id}")]
        public async Task<ApiResponse> GetMABusinessModelAsync (int id) {
            var result = await _maService.GetMABusinessModel (id);
            return result;
        }

        /// <summary>
        /// Get MA by User
        /// </summary>
        /// <param name="userID"></param>
        /// <returns></returns>
        [HttpGet]
        [Route ("ByUser/{userID}")]
        public async Task<IEnumerable<MA>> GetMAByUserAsync (int userID) {
            var result = await _maService.GetMAByUserAsync (userID);
            return result;
        }
        /// <summary>
        /// Get MA List for Renewal by User
        /// </summary>
        /// <param name="userID"></param>
        /// <returns></returns>
        [HttpGet]
        [Route ("ForRenewal/{userID}")]
        public async Task<IEnumerable<MABusinessModel>> GetMAForRenewalByUserAsync (int userID) {
            var result = await _maService.GetMAForRenewalByUserAsync (userID);
            return result;
        }

        /// <summary>
        /// Get MA List for Variation by User
        /// </summary>
        /// <param name="userID"></param>
        /// <returns></returns>
        [HttpGet]
        [Route ("ForVariation/{userID}")]
        public async Task<IEnumerable<MABusinessModel>> GetMAForVariationByUserAsync (int userID) {
            var result = await _maService.GetMAForVariationByUserAsync (userID);
            return result;
        }

        /// <summary>
        /// get list of MAs
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [Route ("List/{submoduleTypeCode?}")]
        public async Task<DataTablesResult<vwMA>> GetMAPage ([FromBody] IDataTablesRequest request, string submoduleTypeCode = null) {
            var result = await _maService.GetMAPageAsync (request, this.HttpContext.GetUserID (), submoduleTypeCode == "null" ? null : submoduleTypeCode);
            return result;
        }

        /// <summary>
        /// Delete MA  
        /// </summary>
        /// <param name="maStatusLog"></param>
        /// <returns></returns>
        [HttpPost]
        [Route ("DeleteMA")]
        public async Task<bool> DeleteMA ([FromBody] MAStatusLogModel maStatusLog) {
            var result = await _maService.DeleteMAAsync (maStatusLog);
            return result;
        }

        /// <summary>
        /// Change MA Status 
        /// </summary>
        /// <param name="maStatusLog"></param>
        /// <returns></returns>
        [HttpPost]
        [Route ("ChangeStatus")]
        public async Task<bool> ChangeMAStatus ([FromBody] MAStatusLogModel maStatusLog) {
            var result = await _maService.ChangeMAStatusAsync (maStatusLog);
            if (result) {
                await _generateDocument.GenerateRegistrationPDFDocument (_nodeServices, maStatusLog.MAID, maStatusLog.ToStatusCode, maStatusLog.ChangedByUserID);
            }
            return result;
        }

        /// <summary>
        /// Insert or update MAChecklist 
        /// </summary>
        /// <param name="maChecklist"></param>
        /// <returns></returns>
        [HttpPost]
        [Route ("MAChecklist/InsertOrUpdate")]
        public async Task<bool> InsertOrUpdateMAChecklistAsync ([FromBody] IList<MAChecklist> maChecklist) {
            var result = await _maService.InsertOrUpdateMAChecklistAsync (maChecklist);
            return result;
        }

        /// <summary>
        /// Approve MA
        /// </summary>
        /// <param name="ma"></param>
        /// <returns></returns>
        [HttpPost]
        [Route ("Approve/{isNotificationType:bool?}")]
        public async Task<ApiResponse> ApproveMAAsync ([FromBody] MABusinessModel ma, bool? isNotificationType = false) {
            var result = await _maService.ApproveMAAsync (ma.MA);
            if (result.IsSuccess && (bool) !isNotificationType) {
                await _generateDocument.GenerateRegistrationPDFDocument (_nodeServices, ma.MA.ID, "APR", ma.MA.ModifiedByUserID);
            } else if (result.IsSuccess && (bool) isNotificationType) {
                await _generateDocument.GenerateRegistrationPDFDocument (_nodeServices, ma.MA.ID, "NOTAPR", ma.MA.ModifiedByUserID, ma.Comment);
            }

            return result;
        }

        /// <summary>
        /// Approve MA
        /// </summary>
        /// <param name="ma"></param>
        /// <returns></returns>
        [HttpPost]
        [Route ("ApproveNotification")]
        public async Task<ApiResponse> ApproveMAAsync ([FromBody] MABusinessModel ma) {
            if (!(new string[] { "FNT", "NTNIVD", "NTIVD" }).Contains (ma.SubmoduleCode)) {
                return new ApiResponse () {
                    StatusCode = StatusCodes.Status400BadRequest,
                        Result = false,
                        IsSuccess = false,
                        Message = "Invalid Application Type"
                };
            }
            var result = await _maService.ApproveMAAsync (ma.MA);
            if (result.IsSuccess && ma.SubmoduleCode == "FNT") {
                await _generateDocument.GenerateRegistrationPDFDocument (_nodeServices, ma.MA.ID, "FNT", ma.MA.ModifiedByUserID);
            }
            else if(result.IsSuccess && ma.SubmoduleCode != "FNT" ){
                await _generateDocument.GenerateRegistrationPDFDocument (_nodeServices, ma.MA.ID, "MDNT", ma.MA.ModifiedByUserID);
            }

            return result;
        }

        /// <summary>
        /// Re-print MA
        /// </summary>
        /// <param name="maID"></param>
        /// <param name="userID"></param>
        /// <returns></returns>
        [HttpGet]
        [Route ("RePrint/{maID}/{userID}")]
        public async Task<bool> RePrintMA (int maID, int userID) {
            var ma = (await _maService.FindByAsync (x => x.ID == maID)).FirstOrDefault ();
            Domain.Document.Document document = null;

            if ((bool) ma?.IsNotificationType && ma?.MAStatus.MAStatusCode == "APR") {
                document = await _generateDocument.GenerateRegistrationPDFDocument (_nodeServices, ma.ID, "NOTAPR", userID);
            } else {
                document = await _generateDocument.GenerateRegistrationPDFDocument (_nodeServices, ma.ID, ma.MAStatus.MAStatusCode, userID);
            }
            var log = new PrintLog () {
                PrintedByUserID = userID,
                DocumentID = document.ID,
                PrintedDate = DateTime.Now
            };
            return await _printLog.CreateAsync (log);
        }
        /// <summary>
        /// Get MA Status by id
        /// </summary>
        /// <param name="maID"></param>
        /// <returns></returns>
        [HttpGet]
        [Route ("Status/{maID}")]
        public async Task<MAStatus> GetMAStatus (int maID) {
            var result = await _maService.GetMAStatus (maID);
            return result;
        }

        /// <summary>
        /// Generate Premarket lab sample request letter
        /// </summary>
        /// <param name="maID"></param>
        /// <param name="userID"></param>
        /// <returns></returns>
        [HttpGet]
        [Route ("Lab/{maID}/{userID}")]
        public async Task<bool> GeneratePremarketLabRequestAsync (int maID, int userID) {
            var result = await _maService.GeneratePremarketLabRequestAsync (maID);
            if (result) {
                await _generateDocument.GenerateRegistrationPDFDocument (_nodeServices, maID, "LABR", userID);
            }
            return result;
        }

        /// <summary>
        /// Generate Premarket lab sample request letter
        /// </summary>
        /// <param name="maID"></param>
        /// <param name="userID"></param>
        /// <returns></returns>
        [HttpGet]
        [Route ("PopulateDraftMAToWip")]
        public async Task<bool> PopulateDraftMAToWIPAsync () {
            var result = await _maService.PopulateDraftMAToWIP ();
            return result;
        }

    }
}