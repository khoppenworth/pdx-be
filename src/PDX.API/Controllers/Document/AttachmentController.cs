using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using PDX.BLL.Services.Interfaces;
using System.Linq;
using Microsoft.AspNetCore.Authorization;
using System.IO;
using PDX.API.Helpers;

namespace PDX.API.Controllers
{
    /// <summary>
    /// Generic Attachment Controller
    /// </summary>
    [Authorize]
    [Route("api/[controller]")]
    public class AttachmentController : Controller
    {
        private readonly IDocumentService _documentService;

        /// <summary>
        /// Constructor for AttachmentController
        /// </summary>
        /// <param name="documentService"></param>
        public AttachmentController(IDocumentService documentService)
        {
            _documentService = documentService;

        }

        /// <summary>
        /// Get All attachments
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<IList<Domain.Document.Document>> GetAllAttachments()
        {
                var result = await _documentService.GetAllAsync();
                return result.ToList();
        }

        /// <summary>
        /// POST: Read single file content as PhysicalFileResult
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [AllowAnonymous]
        [HttpPost]
        [Route("Single/{id}")]
        public async Task<IActionResult> ReadSingleFile(int id){
            var result = await _documentService.ReadPhysicalFile(id);
            return result;
        }

        /// <summary>
        /// GET: Read single file content as PhysicalFileResult
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [AllowAnonymous]
        [HttpGet]
        [Route("Single/{id}")]
        public async Task<IActionResult> ReadSingle(int id){
            var result = await _documentService.ReadPhysicalFile(id);
            return result;
        }

        /// <summary>
        /// GET: Read single file content as PhysicalFileResult
        /// </summary>
        /// <param name="id"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        [HttpGet]
        [HttpPost]
        [Route("Single/{id}/{userId}/{permission}")]
        public async Task<IActionResult> ReadSingleByPermision(int id,int userId,string permission){
            var result = await _documentService.ReadPhysicalFileByPermission(id,userId,permission);
            return result;
        }

        /// <summary>
        /// Read single file content as PhysicalFileResult
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("Print/{id}")]
        public async Task<IActionResult> PrintSingleFile(int id){            
            var result = await _documentService.PrintPhysicalFile(id, HttpContext.GetUserID());
            return result;
        }

     
         /// <summary>
        /// Read  documents by reference and submodule
        /// </summary>
        /// <param name="submoduleCode"></param>
        /// <param name="referenceId"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("Documents/{submoduleCode}/{referenceId}")]
        public async Task<IList<Domain.Document.Document>> GetIPermitDocuments(string submoduleCode, int referenceId){
            var result = await _documentService.GetDocumentAsync(submoduleCode,referenceId);
            return result;
        }

        

        /// <summary>
        /// Upload attachment
        /// </summary>
        /// <param name="document"></param>
        /// <returns></returns>
        [HttpPost]
        [RequestSizeLimit(150_000_000)]
        public async Task<Domain.Document.Document> UploadAttachment([FromForm]BLL.Model.Document document)
        {
            if (ModelState.IsValid)
            {
                var result = await _documentService.CreateDocumentAsync(document);
                return result;
            }
            return null;
        }

        /// <summary>
        /// Update Uploaded attachment
        /// </summary>
        /// <param name="document"></param>
        /// <returns></returns>
        [HttpPut]
        public async Task<bool> UpdateUploadedAttachment([FromBody]PDX.Domain.Document.Document document)
        {
            if (ModelState.IsValid)
            {
                var result = await _documentService.UpdateAsync(document);
                return result;
            }
            return false;
        }

        /// <summary>
        /// Read file content as Base64 string
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("Base64/{id}")]
        public async Task<object> ReadImageAsBase64Async(int id){
            var result = await _documentService.ReadDocumentAsBase64Async(id);
            return new { Base64 = result};
        }


        
    }
}