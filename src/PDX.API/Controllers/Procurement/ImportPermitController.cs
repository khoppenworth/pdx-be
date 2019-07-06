using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using DataTables.AspNet.AspNetCore;
using DataTables.AspNet.Core;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.NodeServices;
using PDX.API.Helpers;
using PDX.API.Model.Ipermit;
using PDX.API.Services.Interfaces;
using PDX.BLL.Model;
using PDX.BLL.Services;
using PDX.BLL.Services.Interfaces;
using PDX.BLL.Services.Interfaces.Email;
using PDX.Domain.Account;
using PDX.Domain.Document;
using PDX.Domain.Procurement;
using PDX.Domain.Views;

namespace PDX.API.Controllers
{
    /// <summary>
    /// ImportPermit Controller
    /// </summary>
    [Authorize]
    [Route("api/[controller]")]
    public class ImportPermitController : BaseController<ImportPermit>
    {
        private readonly IImportPermitService _service;
        private readonly IIpermitLogStatusService _statusService;
        private readonly IEmailSender _emailSender;
        private readonly IService<LetterHeading> _letterHeadingService;
        private readonly IService<Letter> _letterService;
        private readonly IGenerateDocuments _generateDocument;
        private readonly IDocumentService _documentService;
        private readonly INodeServices _nodeServices;

        /// <summary>
        /// import permit controller
        /// </summary>
        /// <param name="importPermitService"></param>
        /// <param name="nodeServices"></param>
        /// <param name="statusService"></param>
        /// <param name="letterHeadingService"></param>
        /// <param name="letterService"></param>
        /// <param name="emailSender"></param>
        /// <param name="generateDocument"></param>
        /// <param name="documentService"></param>
        public ImportPermitController(IImportPermitService importPermitService, [FromServices] INodeServices nodeServices, IIpermitLogStatusService statusService, IService<LetterHeading> letterHeadingService, IService<Letter> letterService, IEmailSender emailSender, IGenerateDocuments generateDocument, IDocumentService documentService) : base(importPermitService)
        {
            _service = importPermitService;
            _statusService = statusService;
            _emailSender = emailSender;
            _letterService = letterService;
            _letterHeadingService = letterHeadingService;
            _generateDocument = generateDocument;
            _documentService = documentService;
            _nodeServices = nodeServices;
        }

        /// <summary>
        /// Insert new Import Permit
        /// </summary>
        /// <param name="importPermit"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<bool> CreateImportPermitAsync([FromBody] ImportPermitBusinessModel importPermit)
        {
            var result = await _service.CreateImportPermitAsync(importPermit);
            if (result && !importPermit.IsDraft)
            {
                var savedIPermit = await _service.GetAsync(i => i.RowGuid == importPermit.Identifier, true);
                var subFolder = savedIPermit.ImportPermitType.ImportPermitTypeCode == "PIP" ? "pip" : "ipermit";

                //Use a separate Template for Medical Device
                var fileName = savedIPermit.ImportPermitDetails.FirstOrDefault().Product.ProductType.ProductTypeCode == "MDS" ? "Submit-Medical-Device.html" : "Submit.html";
                var filePath = $".{Path.DirectorySeparatorChar}external{Path.DirectorySeparatorChar}templates{Path.DirectorySeparatorChar}{subFolder}{Path.DirectorySeparatorChar}{fileName}";

                var ipermit = new ImportStatusLog
                {
                    ID = savedIPermit.ID,
                    ChangedBy = savedIPermit.CreatedByUserID,
                    Comment = "New Import Permit Created"
                };
                var documentTypeCode = savedIPermit.ImportPermitType.ImportPermitTypeCode == "PIP" ? "PIPAK" : "ACKL";
                await _generateDocument.GeneratePDFDocument(filePath, ipermit, _nodeServices, documentTypeCode, importPermit.SubmoduleCode, savedIPermit);
            }
            return result;
        }

        /// <summary>
        /// Insert new Import Permit
        /// </summary>
        /// <param name="importPermit"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("AutoImportPermit")]
        public async Task<ApiResponse> CreateAutomaticImportPermitAsync([FromBody] ImportPermitBusinessModel importPermit)
        {
            var result = await _service.CreateAutomaticImportPermitAsync(importPermit, this.HttpContext.GetUserID());
            if (!result.IsSuccess || importPermit.IsDraft || importPermit.CurrentStatusCode == ImportPermitStatuses.SFA) return result;

            var savedIPermit = importPermit.Identifier == Guid.Empty ? await _service.GetAsync(i => i.RowGuid == importPermit.ImportPermit.RowGuid, true) : await _service.GetAsync(i => i.RowGuid == importPermit.Identifier, true);
            var subFolder = savedIPermit.ImportPermitType.ImportPermitTypeCode == "PIP" ? "pip" : "ipermit";

            var ipermit = new ImportStatusLog
            {
                ID = savedIPermit.ID,
                ChangedBy = savedIPermit.CreatedByUserID
            };

            var productType = savedIPermit.ImportPermitDetails.FirstOrDefault().Product.ProductType;
            string filePath = "", fileName = "", documentTypeCode = "";

            switch (importPermit.CurrentStatusCode)
            {
                case ImportPermitStatuses.RQST:
                    ipermit.Comment = "New Import Permit Created";
                    //Use a separate Template for Medical Device
                    fileName = (productType != null && productType.ProductTypeCode == "MDS") ? "Submit-Medical-Device.html" : "Submit.html";
                    documentTypeCode = savedIPermit.ImportPermitType.ImportPermitTypeCode == "PIP" ? "PIPAK" : "ACKL";
                    break;

                case ImportPermitStatuses.APR:
                    ipermit.Comment = "New Import Permit Approved Automatically";
                    //Use a separate Template for Medical Device
                    fileName = (productType != null && productType.ProductTypeCode == "MDS") ? "Approve-Medical-Device.html" : "Approve.html";
                    documentTypeCode = savedIPermit.ImportPermitType.ImportPermitTypeCode == "PIP" ? "PIPCR" : "POCR";
                    break;
            }
            filePath = $".{Path.DirectorySeparatorChar}external{Path.DirectorySeparatorChar}templates{Path.DirectorySeparatorChar}{subFolder}{Path.DirectorySeparatorChar}{fileName}";
            await _generateDocument.GeneratePDFDocument(filePath, ipermit, _nodeServices, documentTypeCode, importPermit.SubmoduleCode, savedIPermit);

            return result;
        }

        /// <summary>
        /// update existing Import Permit
        /// </summary>
        /// <param name="importPermit"></param>
        /// <returns></returns>
        [HttpPut]
        public async Task<bool> UpdateImportPermitAsync([FromBody] ImportPermitBusinessModel importPermit)
        {
            var result = await _service.UpdateImportPermitAsync(importPermit);
            return result;
        }

        /// <summary>
        /// assign Import Permit
        /// </summary>
        /// <param name="importPermit"></param>
        /// <returns></returns>
        [Route("Assign")]
        [HttpPut]
        public async Task<bool> AssignImportPermitAsync([FromBody] ImportPermit importPermit)
        {
            var result = await _service.AssignImportPermitAsync(importPermit);
            return result;
        }

        /// <summary>
        /// Get import permist by agent
        /// </summary>
        /// <param name="agentID"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("ByAgent/{agentID}")]
        public async Task<List<ImportPermit>> GetAgentImportPermits(int agentID)
        {
            var result = await _service.GetAgentImportPermits(agentID);
            return result;
        }

        /// <summary>
        /// Get import permist by user
        /// </summary>
        /// <param name="userID"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("ByUser/{userID}")]
        public async Task<List<ImportPermit>> GetUserImportPermits(int userID)
        {
            var result = await _service.GetImportPermitsByUser(userID, "IPRM");
            return result;
        }

        /// <summary>
        /// Get single import permit by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("Single/{id}")]
        public async Task<ImportPermitBusinessModel> GetImportPermitBusinessModel(int id)
        {
            var result = await _service.GetImportPermitBusinessModel(id);
            return result;
        }

        /// <summary>
        /// get list of import permits
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [Route("List")]
        public async Task<DataTablesResult<vwImportPermit>> GetImportPermitsDT([FromBody] IDataTablesRequest request)
        {
            var userIDPrimitives = HttpContext.Request.Form["userID"];
            int? userID = System.Convert.ToInt32(userIDPrimitives[0]);
            var result = await _service.GetImportPermitPage(request, userID);
            return result;
        }

        [HttpPost]
        [Route("Submit")]
        public async Task<bool> SubmitIPermit([FromBody] ImportStatusLog ipermit)
        {
            var result = await _service.ChangeIPermitStatus(ipermit, ImportPermitStatuses.RQST);
            if (result)
            {
                var savedIPermit = await _service.GetAsync(i => i.ID == ipermit.ID);
                var subFolder = savedIPermit.ImportPermitType.ImportPermitTypeCode == "PIP" ? "pip" : "ipermit";
                //Use a separate Template for Medical Device
                var fileName = savedIPermit.ImportPermitDetails.FirstOrDefault().Product.ProductType.ProductTypeCode == "MDS" ? "Submit-Medical-Device.html" : "Submit.html";
                var filePath = $".{Path.DirectorySeparatorChar}external{Path.DirectorySeparatorChar}templates{Path.DirectorySeparatorChar}{subFolder}{Path.DirectorySeparatorChar}{fileName}";
                var documentTypeCode = savedIPermit.ImportPermitType.ImportPermitTypeCode == "PIP" ? "PIPAK" : "ACKL";
                await _generateDocument.GeneratePDFDocument(filePath, ipermit, _nodeServices, documentTypeCode, ipermit.SubmoduleCode, savedIPermit);
            }
            return result;
        }

        [HttpPost]
        [Route("SubmitForApproval")]
        public async Task<bool> SubmitIPermitForApproval([FromBody] ImportStatusLog ipermit)
        {
            var result = await _service.ChangeIPermitStatus(ipermit, ImportPermitStatuses.SFA);
            return result;
        }

        [HttpPost]
        [Route("SubmitForRejection")]
        public async Task<bool> SubmitIPermitForRejection([FromBody] ImportStatusLog ipermit)
        {
            var result = await _service.ChangeIPermitStatus(ipermit, ImportPermitStatuses.SFR);
            return result;
        }

        [HttpPost]
        [Route("ReturnToAgent")]
        public async Task<bool> ReturnIPermitToAgent([FromBody] ImportStatusLog ipermit)
        {
            var result = await _service.ChangeIPermitStatus(ipermit, ImportPermitStatuses.RTA);
            return result;
        }

        [HttpPost]
        [Route("ReturnToCSO")]
        public async Task<bool> ReturnIPermitToCSO([FromBody] ImportStatusLog ipermit)
        {
            var result = await _service.ChangeIPermitStatus(ipermit, ImportPermitStatuses.RTC);
            return result;
        }

        [HttpPost]
        [Route("Approve")]
        public async Task<bool> ApproveIPermit([FromBody] ImportStatusLog ipermit)
        {
            var result = await _service.ChangeIPermitStatus(ipermit, ImportPermitStatuses.APR);
            if (result)
            {
                var savedIPermit = await _service.GetAsync(i => i.ID == ipermit.ID);
                var subFolder = savedIPermit.ImportPermitType.ImportPermitTypeCode == "PIP" ? "pip" : "ipermit";
                //Use a separate Template for Medical Device
                var productType = savedIPermit.ImportPermitDetails.FirstOrDefault().Product.ProductType;
                var fileName = (productType != null && productType.ProductTypeCode == "MDS") ? "Approve-Medical-Device.html" : "Approve.html";
                var filePath = $".{Path.DirectorySeparatorChar}external{Path.DirectorySeparatorChar}templates{Path.DirectorySeparatorChar}{subFolder}{Path.DirectorySeparatorChar}{fileName}";

                var documentTypeCode = savedIPermit.ImportPermitType.ImportPermitTypeCode == "PIP" ? "PIPCR" : "POCR";

                await _generateDocument.GeneratePDFDocument(filePath, ipermit, _nodeServices, documentTypeCode, ipermit.SubmoduleCode, savedIPermit);
            }
            return result;
        }

        [HttpPost]
        [Route("Reject")]
        public async Task<bool> RejectIPermit([FromBody] ImportStatusLog ipermit)
        {
            var result = await _service.ChangeIPermitStatus(ipermit, ImportPermitStatuses.REJ);
            if (result)
            {
                var savedIPermit = await _service.GetAsync(i => i.ID == ipermit.ID);
                var subFolder = savedIPermit.ImportPermitType.ImportPermitTypeCode == "PIP" ? "pip" : "ipermit";
                var filePath = $".{Path.DirectorySeparatorChar}external{Path.DirectorySeparatorChar}templates{Path.DirectorySeparatorChar}{subFolder}{Path.DirectorySeparatorChar}Reject.html";
                var documentTypeCode = savedIPermit.ImportPermitType.ImportPermitTypeCode == "PIP" ? "PIPRR" : "PORR";
                await _generateDocument.GeneratePDFDocument(filePath, ipermit, _nodeServices, documentTypeCode, ipermit.SubmoduleCode, savedIPermit);
            }
            return result;
        }

        [HttpPost]
        [Route("Withdraw")]
        public async Task<bool> WithdrawIPermit([FromBody] ImportStatusLog ipermit)
        {
            var result = await _service.ChangeIPermitStatus(ipermit, ImportPermitStatuses.WITH);
            return result;
        }

        [HttpPost]
        [Route("Void")]
        public async Task<bool> VoidIPermit([FromBody] ImportStatusLog ipermit)
        {
            var result = await _service.ChangeIPermitStatus(ipermit, ImportPermitStatuses.VOID);
            return result;
        }

        [HttpPost]
        [Route("DuplicateInvoice")]
        public async Task<bool> CheckDuplicateInvoice([FromBody] IpermitInvoiceDuplicates ipermit)
        {
            var result = await _service.CheckDuplicateInvoice(ipermit.SupplierID, ipermit.AgentID, ipermit.PerformaInvoiceNumber, ipermit.ImportPermitID);
            return result;
        }

        /// <summary>
        /// get list of pips
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [Route("PIP/List")]
        public async Task<DataTablesResult<vwPIP>> GetPIPPage([FromBody] IDataTablesRequest request)
        {
            var userIDPrimitives = HttpContext.Request.Form["userID"];
            int? userID = System.Convert.ToInt32(userIDPrimitives[0]);
            var result = await _service.GetPIPPage(request, userID);
            return result;
        }

        /// <summary>
        /// Get pre import permist by user
        /// </summary>
        /// <param name="userID"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("PIP/ByUser/{userID}")]
        public async Task<List<ImportPermit>> GetPreImportPermitsByUser(int userID)
        {
            var result = await _service.GetImportPermitsByUser(userID, "PIP");
            return result;
        }
        /// <summary>
        /// Search entity
        /// </summary>
        /// <param name="searchRequest">The search request</param>
        /// <returns>bool</returns>
        [HttpPost]
        [Route("Search")]
        public override async Task<DataTablesResult<ImportPermit>> SearchAsync([FromBody] SearchRequest searchRequest)
        {
            var result = await _service.SearchImportPermit(searchRequest.Query, searchRequest.PageNumber, searchRequest.PageSize);
            return result;
        }

    }
}