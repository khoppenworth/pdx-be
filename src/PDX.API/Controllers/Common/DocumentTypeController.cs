using Microsoft.AspNetCore.Mvc;
using PDX.Domain.Common;
using PDX.BLL.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using PDX.DAL.Query;

namespace PDX.API.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    public class DocumentTypeController:CrudBaseController<DocumentType>
    {
        private readonly IService<DocumentType> _documentTypeService;
        public DocumentTypeController(IService<DocumentType> documentTypeService)
        :base(documentTypeService)
        {
            _documentTypeService = documentTypeService;
        }
        
        /// <summary>
        /// Get document type by its code
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("ByCode/{code}")]
        public async Task<IEnumerable<DocumentType>> GetDocumentTypeByCodeAsync(string code)
        {
            var documentType = await _documentTypeService.FindByAsync(dt => dt.DocumentTypeCode == code);
            return documentType;
        }

        /// <summary>
        /// Get Documents Types By Type
        /// </summary>
        /// <param name="isDossier"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("ByType/{isDossier}")]
        public async Task<IEnumerable<DocumentType>> GetDocumentTypesAsync(bool? isDossier = null)
        {
            OrderBy<DocumentType> orderBy = new OrderBy<DocumentType> (qry => qry.OrderBy (e => e.ParentDocumentTypeID).ThenBy (x => x.ID));
            var result = await _documentTypeService.FindByAsync(dt => isDossier == null || ( isDossier != null && dt.IsDossier == isDossier),orderBy.Expression);
            return result;
        }
    }
}