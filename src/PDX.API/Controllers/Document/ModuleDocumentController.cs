using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PDX.BLL.Services.Interfaces;
using PDX.Domain.Document;

namespace PDX.API.Controllers {
    /// <summary>
    /// ModuleDocument Controller
    /// </summary>
    [Authorize]
    [Route ("api/[controller]")]
    public class ModuleDocumentController : CrudBaseController<ModuleDocument> {
        private readonly IModuleDocumentService _moduleDocumentService;
        /// <summary>
        /// Constructor for ModuleDocumentController
        /// </summary>
        /// <param name="moduleDocumentService"></param>
        public ModuleDocumentController (IModuleDocumentService moduleDocumentService) : base (moduleDocumentService) {
            _moduleDocumentService = moduleDocumentService;
        }

        /// <summary>
        /// Get ModuleDocuments by Submodule ID
        /// </summary>
        /// <param name="id"></param>
        /// <param name="active"></param>
        /// <param name="isdossier"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("BySubmodule/{id}/{active}/{isdossier?}")]
        public async Task<IList<ModuleDocument>> GetBySubmodule(int id, bool? active, bool? isdossier = null)
        {
            var moduleDocuments = (await _moduleDocumentService.FindByAsync(md => md.Submodule.ID == id && (active == null || (active != null && md.IsActive == active)) && (isdossier == null || (isdossier != null && md.DocumentType.IsDossier == isdossier)))).Distinct().ToList();
            return moduleDocuments;
        }

        /// <summary>
        /// Get ModuleDocuments by submodule code
        /// </summary>
        /// /// <param name="code"></param>
        /// <returns></returns>
        [Route ("BySubmoduleCode/{code}")]
        [HttpGet]
        public async Task<IList<ModuleDocument>> GetBySubmoduleCode (string code) {
            var moduleDocuments = (await _moduleDocumentService.FindByAsync (md => md.Submodule.SubmoduleCode == code && md.IsActive && !md.DocumentType.IsUploadedLater)).Distinct ().ToList ();
            return moduleDocuments;
        }

        /// <summary>
        /// Get ModuleDocuments by submodule code and Filter SRA
        /// </summary>
        /// /// <param name="code"></param>
        /// <returns></returns>
        [Route ("BySubmoduleCode/{code}/{isSra}")]
        [HttpGet]
        public async Task<IList<ModuleDocument>> GetBySubmoduleCode (string code, bool isSra) {
            var moduleDocuments = (await _moduleDocumentService.FindByAsync (md => md.Submodule.SubmoduleCode == code &&
                md.IsActive &&
                !md.DocumentType.IsUploadedLater &&
                (md.IsSRA == null || (md.IsSRA != null && md.IsSRA == isSra)))).Distinct ().ToList ();
            return moduleDocuments;
        }

        /// <summary>
        /// Get ModuleDocuments by module code
        /// </summary>
        /// /// <param name="code"></param>
        /// <returns></returns>
        [Route ("ByModuleCode/{code}")]
        [HttpGet]
        public async Task<IList<ModuleDocument>> GetByModuleCode (string code) {
            var moduleDocuments = (await _moduleDocumentService.FindByAsync (md => md.Submodule.Module.ModuleCode == code && md.IsActive && !md.DocumentType.IsUploadedLater)).Distinct ().ToList ();
            return moduleDocuments;
        }

        /// <summary>
        /// Get ModuleDocuments by submodule and document type code
        /// </summary>
        /// /// <param name="submoduleCode"></param>
        /// /// <param name="documentTypeCode"></param>
        /// <returns></returns>
        [Route ("ByCodes/{submoduleCode}/{documentTypeCode}")]
        [HttpGet]
        public async Task<ModuleDocument> GetBySubmoduleAndDocumentTypeCode (string submoduleCode, string documentTypeCode) {
            var moduleDocument = (await _moduleDocumentService.GetAsync (md => md.Submodule.SubmoduleCode == submoduleCode && md.DocumentType.DocumentTypeCode == documentTypeCode && md.IsActive));
            return moduleDocument;
        }

        /// <summary>
        /// Insert or update moduleDocuments
        /// </summary>
        /// <param name="moduleDocuments">The module</param>
        /// <returns>bool</returns>
        [Route ("InsertOrUpdate")]
        [HttpPost]
        public async Task<bool> InsertOrUpdateAsync ([FromBody] IList<ModuleDocument> moduleDocuments) {
            var result = await _moduleDocumentService.CreateOrUpdateAsync (moduleDocuments);
            return result;
        }
    }
}