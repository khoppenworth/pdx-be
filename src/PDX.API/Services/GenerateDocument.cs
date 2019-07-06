using System;
using System.Collections;
using System.Collections.Generic;
using System.Dynamic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.NodeServices;
using Microsoft.Extensions.Options;
using PDX.API.Model.Ipermit;
using PDX.API.Model.Registration;
using PDX.API.Services.Interfaces;
using PDX.BLL.Helpers;
using PDX.BLL.Model;
using PDX.BLL.Model.Config;
using PDX.BLL.Model.Report;
using PDX.BLL.Services.Interfaces;
using PDX.DAL.Reporting.Models;
using PDX.Domain.Customer;
using PDX.Domain.Customer;
using PDX.Domain.Document;
using PDX.Domain.Procurement;

namespace PDX.API.Services {
    public class GenerateDocument : IGenerateDocuments {
        private readonly string _rootUrl;
        private readonly AttachmentConfig _attachmentConfig;
        private readonly IService<LetterHeading> _letterHeadingService;
        private readonly IService<Letter> _letterService;
        private readonly IImportPermitService _service;
        private readonly IIpermitLogStatusService _statusService;
        private readonly IDocumentService _documentService;
        private readonly IService<ModuleDocument> _moduleDocumentService;
        private readonly IMAService _maService;
        private readonly PDX.Logging.ILogger _logger;
        private readonly IChecklistService _checklistService;
        private readonly IMALogStatusService _maStatusService;
        private readonly IService<AgentSupplier> _agentSupplierService;
        private readonly IReportService _reportService;

        public GenerateDocument (IOptions<AttachmentConfig> attachmentConfig, IImportPermitService importPermitService,
            IDocumentService documentService, IService<LetterHeading> letterHeadingService,
            IService<Letter> letterService, IIpermitLogStatusService statusService, IService<ModuleDocument> moduleDocumentService,
            PDX.Logging.ILogger logger, IMAService maService, IChecklistService checklistService,
            IMALogStatusService maStatusService, IService<AgentSupplier> agentSupplierService, IReportService reportService) {
            _attachmentConfig = attachmentConfig.Value;
            _rootUrl = _attachmentConfig.RootUrl;
            _service = importPermitService;
            _documentService = documentService;
            _letterHeadingService = letterHeadingService;
            _letterService = letterService;
            _statusService = statusService;
            _moduleDocumentService = moduleDocumentService;
            _logger = logger;
            _maService = maService;
            _checklistService = checklistService;
            _maStatusService = maStatusService;
            _agentSupplierService = agentSupplierService;
            _reportService = reportService;

        }

        public async Task GeneratePDFDocument (string htmlFilePath, ImportStatusLog ipermit, INodeServices nodeServices, string documenttypeCode, string submoduleCode, ImportPermit iipermit) {
            try {

                var letterHeading = await _letterHeadingService.GetAsync (1);
                var letter = (await _letterService.GetAsync (x => x.ModuleDocument.DocumentType.DocumentTypeCode == documenttypeCode && x.ModuleDocument.Submodule.SubmoduleCode == submoduleCode));
                var moduleDocument = await _moduleDocumentService.GetAsync (letter.ModuleDocumentID);
                var moduleCode = moduleDocument.Submodule.Module.ModuleCode;

                IpermitPDFData data = new IpermitPDFData (iipermit, letterHeading, letter, ipermit.Comment);
                var ipemitStatues = (await _statusService.GetIPermitStatus (iipermit.ID));

                data.submissionDate = ipemitStatues.FirstOrDefault (s => s.ToImportPermitStatus.ImportPermitStatusCode == "RQST")?.CreatedDate.ToString ();
                data.approvedDate = ipemitStatues.FirstOrDefault (s => s.ToImportPermitStatus.ImportPermitStatusCode == "APR")?.CreatedDate.ToString ();
                data.rejectedDate = ipemitStatues.FirstOrDefault (s => s.ToImportPermitStatus.ImportPermitStatusCode == "REJ")?.CreatedDate.ToString ();
                data.expiryDate = iipermit.ExpiryDate?.ToString ();;
                data.reason = ipemitStatues.FirstOrDefault (s => s.ToImportPermitStatus.ImportPermitStatusCode == "REJ")?.Comment;

                var htmlString = string.Join (" ", System.IO.File.ReadAllLines (htmlFilePath));
                htmlString = buildCSS (htmlString);

                var pdfPath = $".{Path.DirectorySeparatorChar}external{Path.DirectorySeparatorChar}js{Path.DirectorySeparatorChar}pdf";

                var result = await nodeServices.InvokeAsync<byte[]> (pdfPath, htmlString, data);

                var relativeDirectory = $"{Path.DirectorySeparatorChar}{moduleCode}{Path.DirectorySeparatorChar}{data.tempFolderName}";
                var directory = $"{_rootUrl}{relativeDirectory}";
                var filePath = $"{relativeDirectory}{Path.DirectorySeparatorChar}{data.fileName}";

                await FileHelper.SaveFileAsync (directory, data.fileName, result);

                Domain.Document.Document docEntity = new Domain.Document.Document () {
                    ModuleDocumentID = letter.ModuleDocumentID,
                    FilePath = filePath,
                    FileType = "application/pdf",
                    ReferenceID = ipermit.ID,
                    CreatedBy = ipermit.ChangedBy,
                    UpdatedBy = ipermit.ChangedBy
                };
                await _documentService.CreateOrUpdateAsync (docEntity, true, r => r.ModuleDocumentID == docEntity.ModuleDocumentID && r.FilePath == docEntity.FilePath && r.ReferenceID == docEntity.ReferenceID);
            } catch (Exception ex) {
                _logger.Log (ex);
            }
        }

        public async Task<Domain.Document.Document> GenerateRegistrationPDFDocument (INodeServices nodeServices, int maId, string statusCode, int userId, string Comment = null) {
            var letterHeading = await _letterHeadingService.GetAsync (1);
            var ma = await _maService.GetMA (maId);
            //check if agent-supplier relationship changes
            //TODO agent-supplier change
            var agentSupplier = await _agentSupplierService.GetAsync (ags => ags.SupplierID == ma.MA.SupplierID && ags.AgentLevel.AgentLevelCode == "FAG" && ags.IsActive);
            if (agentSupplier.AgentID != ma.MA.AgentID) {
                ma.MA.AgentID = agentSupplier.AgentID;
                ma.MA.Agent = agentSupplier.Agent;
            }
            string htmlFilePath = constructHtmlPath (ma.Product.ProductType.SubmoduleType.SubmoduleTypeCode);
            Letter letter = new Letter ();
            ModuleDocument moduleDocument = new ModuleDocument ();
            string moduleCode = null;
            RegistrationPDFData data = null;
            switch (statusCode) {
                case "RQST":
                    letter = (await _letterService.GetAsync (x => x.ModuleDocument.DocumentType.DocumentTypeCode == "MACKL" && x.ModuleDocument.Submodule.SubmoduleCode == ma.MA.MAType.MATypeCode));
                    moduleDocument = await _moduleDocumentService.GetAsync (letter.ModuleDocumentID);
                    data = new RegistrationPDFData (ma, letterHeading, letter);
                    var requestedStatus = await _maStatusService.GetMALogStatus (ma.MA.ID, "RQST");
                    data.applicationDate = requestedStatus?.CreatedDate.ToString ("dd-MM-yyyy");
                    htmlFilePath = $"{htmlFilePath}acknowledgement.html";
                    break;
                case "FIR":
                    letter = (await _letterService.GetAsync (x => x.ModuleDocument.DocumentType.DocumentTypeCode == "MAFIR" && x.ModuleDocument.Submodule.SubmoduleCode == ma.MA.MAType.MATypeCode));
                    moduleDocument = await _moduleDocumentService.GetAsync (letter.ModuleDocumentID);
                    var review = await _maService.GetMAReview (ma.MA.ID, "TLD", "FIR");
                    var noOfFIR = (await _maStatusService.GetMALogStatuses (ma.MA.ID, "FIR")).Count ();
                    data = new RegistrationPDFData (ma, letterHeading, letter, noOfFIR);
                    data.review = review?.Comment;
                    data.firGeneratedDate = review?.CreatedDate.ToString ("dd-MM-yyyy");
                    data.firDueDate = review?.FIRDueDate?.ToString ("dd-MM-yyyy");
                    htmlFilePath = $"{htmlFilePath}firRequest.html";
                    break;
                case "RTA":
                    letter = (await _letterService.GetAsync (x => x.ModuleDocument.DocumentType.DocumentTypeCode == "MADFL" && x.ModuleDocument.Submodule.SubmoduleCode == ma.MA.MAType.MATypeCode));
                    moduleDocument = await _moduleDocumentService.GetAsync (letter.ModuleDocumentID);
                    data = new RegistrationPDFData (ma, letterHeading, letter);
                    data.deficiency = (await _checklistService.GetPrescreeningDeficiency (ma.MA.ID)).ToList ().Select (d => new Checklist (d)).ToList ();
                    htmlFilePath = $"{htmlFilePath}deficiency.html";
                    break;
                case "APR":
                    letter = (await _letterService.GetAsync (x => x.ModuleDocument.DocumentType.DocumentTypeCode == "MACRT" && x.ModuleDocument.Submodule.SubmoduleCode == ma.MA.MAType.MATypeCode));
                    moduleDocument = await _moduleDocumentService.GetAsync (letter.ModuleDocumentID);
                    var approvedStatus = await _maStatusService.GetMALogStatus (ma.MA.ID, "APR");
                    data = new RegistrationPDFData (ma, letterHeading, letter);
                    data.approvalDate = approvedStatus?.CreatedDate.ToString ("dd-MM-yyyy");
                    data.certificateValidDate = ((DateTime) ma.MA.ExpiryDate).ToString ("dd-MM-yyyy");
                    htmlFilePath = $"{htmlFilePath}registration.html";
                    break;
                case "NOTAPR":
                    letter = (await _letterService.GetAsync (x => x.ModuleDocument.DocumentType.DocumentTypeCode == "VANL" && x.ModuleDocument.Submodule.SubmoduleCode == ma.MA.MAType.MATypeCode));
                    moduleDocument = await _moduleDocumentService.GetAsync (letter.ModuleDocumentID);
                    approvedStatus = await _maStatusService.GetMALogStatus (ma.MA.ID, "APR");
                    data = new RegistrationPDFData (ma, letterHeading, letter);
                    data.approvalDate = approvedStatus?.CreatedDate.ToString ("dd-MM-yyyy");
                    data.certificateValidDate = approvedStatus?.CreatedDate.ToString ("dd-MM-yyyy");
                    data.variationChanges = ((await _maService.GetVariationChangesAsync (ma.MA.ID))?.Select (v => new VariationDifference (v))).ToList ();
                    data.review = Comment;
                    htmlFilePath = $"{htmlFilePath}notificationApprove.html";
                    break;
                case "REJ":
                    letter = (await _letterService.GetAsync (x => x.ModuleDocument.DocumentType.DocumentTypeCode == "MAREJ" && x.ModuleDocument.Submodule.SubmoduleCode == ma.MA.MAType.MATypeCode));
                    moduleDocument = await _moduleDocumentService.GetAsync (letter.ModuleDocumentID);
                    review = await _maService.GetMAReview (ma.MA.ID, "TLD", "SFR");
                    data = new RegistrationPDFData (ma, letterHeading, letter);
                    data.review = review?.Comment;
                    htmlFilePath = $"{htmlFilePath}rejection.html";
                    break;
                case "LABR":
                    letter = (await _letterService.GetAsync (x => x.ModuleDocument.DocumentType.DocumentTypeCode == "PMNL" && x.ModuleDocument.Submodule.SubmoduleCode == ma.MA.MAType.MATypeCode));
                    moduleDocument = await _moduleDocumentService.GetAsync (letter.ModuleDocumentID);
                    data = new RegistrationPDFData (ma, letterHeading, letter);
                    data.applicationType = moduleDocument.Submodule.Module.Name;
                    htmlFilePath = $"{htmlFilePath}sampleRequest.html";
                    break;
                case "CONL":
                    letter = (await _letterService.GetAsync (x => x.ModuleDocument.DocumentType.DocumentTypeCode == "CONL" && x.ModuleDocument.Submodule.SubmoduleCode == ma.MA.MAType.MATypeCode));
                    moduleDocument = await _moduleDocumentService.GetAsync (letter.ModuleDocumentID);
                    data = new RegistrationPDFData (ma, letterHeading, letter);
                    data.applicationType = moduleDocument.Submodule.Module.Name;
                    htmlFilePath = $"{htmlFilePath}notificationRequest.html";
                    break;
                case "FNT":
                    letter = (await _letterService.GetAsync (x => x.ModuleDocument.DocumentType.DocumentTypeCode == "FNT" && x.ModuleDocument.Submodule.SubmoduleCode == ma.MA.MAType.MATypeCode));
                    moduleDocument = await _moduleDocumentService.GetAsync (letter.ModuleDocumentID);
                    data = new RegistrationPDFData (ma, letterHeading, letter);
                    data.applicationType = moduleDocument.Submodule.Module.Name;
                    approvedStatus = await _maStatusService.GetMALogStatus (ma.MA.ID, "APR");
                    data.approvalDate = approvedStatus?.CreatedDate.ToString ("dd-MM-yyyy");
                    data.certificateValidDate = ((DateTime) ma.MA.ExpiryDate).ToString ("dd-MM-yyyy");
                    htmlFilePath = $"{htmlFilePath}foodNotificationType.html";
                    break;
                case "MDNT":
                    letter = (await _letterService.GetAsync (x => x.ModuleDocument.DocumentType.DocumentTypeCode == "MDNT" && x.ModuleDocument.Submodule.SubmoduleCode == ma.MA.MAType.MATypeCode));
                    moduleDocument = await _moduleDocumentService.GetAsync (letter.ModuleDocumentID);
                    data = new RegistrationPDFData (ma, letterHeading, letter);
                    data.applicationType = moduleDocument.Submodule.Module.Name;
                    approvedStatus = await _maStatusService.GetMALogStatus (ma.MA.ID, "APR");
                    data.approvalDate = approvedStatus?.CreatedDate.ToString ("dd-MM-yyyy");
                    data.certificateValidDate = ((DateTime) ma.MA.ExpiryDate).ToString ("dd-MM-yyyy");
                    htmlFilePath = $"{htmlFilePath}mdNotificationType.html";
                    break;
                default:
                    break;
            }
            moduleCode = moduleDocument?.Submodule?.Module?.ModuleCode;

            return moduleCode == null? null : await Generate (nodeServices, data, letter, htmlFilePath, moduleCode, userId);

        }

        public async Task<string> GenerateReportDocument (INodeServices nodeServices, int reportID, IList<Filter> filters = null) {
            var letterHeading = await _letterHeadingService.GetAsync (1);
            var tabularReport = await _reportService.GetTabularReport (reportID, true, filters);
            var tb = PrepareTabularReportForExport (tabularReport, letterHeading.CompanyName);

            var pdfPath = $".{Path.DirectorySeparatorChar}external{Path.DirectorySeparatorChar}js{Path.DirectorySeparatorChar}pdf";
            var htmlFilePath = $".{Path.DirectorySeparatorChar}external{Path.DirectorySeparatorChar}templates{Path.DirectorySeparatorChar}report{Path.DirectorySeparatorChar}report.template.html";

            var htmlString = string.Join (" ", System.IO.File.ReadAllLines (htmlFilePath));
            htmlString = buildCSS (htmlString);

            var result = await nodeServices.InvokeAsync<byte[]> (pdfPath, htmlString, tb);

            var relativeDirectory = $"{Path.DirectorySeparatorChar}Temp{Path.DirectorySeparatorChar}Report";
            var directory = $"{_rootUrl}{relativeDirectory}";
            var fileName = $"{tabularReport.Report.Title}.pdf";
            var filePath = $"{relativeDirectory}{Path.DirectorySeparatorChar}{fileName}";

            await FileHelper.SaveFileAsync (directory, fileName, result);

            return filePath;
        }

        private async Task<Domain.Document.Document> Generate (INodeServices nodeServices, RegistrationPDFData data, Letter letter, string htmlFilePath, string moduleCode, int userId) {
            try {
                var htmlString = string.Join (" ", System.IO.File.ReadAllLines (htmlFilePath));
                htmlString = buildCSS (htmlString);

                var pdfPath = $".{Path.DirectorySeparatorChar}external{Path.DirectorySeparatorChar}js{Path.DirectorySeparatorChar}pdf";

                var result = await nodeServices.InvokeAsync<byte[]> (pdfPath, htmlString, data);

                var relativeDirectory = $"{Path.DirectorySeparatorChar}{moduleCode}{Path.DirectorySeparatorChar}{data.tempFolderName}";
                var directory = $"{_rootUrl}{relativeDirectory}";
                var filePath = $"{relativeDirectory}{Path.DirectorySeparatorChar}{data.fileName}";

                await FileHelper.SaveFileAsync (directory, data.fileName, result);

                Domain.Document.Document docEntity = new Domain.Document.Document () {
                    ModuleDocumentID = letter.ModuleDocumentID,
                    FilePath = filePath,
                    FileType = "application/pdf",
                    ReferenceID = data.maId,
                    CreatedBy = userId,
                    UpdatedBy = userId
                };
                await _documentService.CreateOrUpdateAsync (docEntity, true, ds => ds.FilePath == docEntity.FilePath);
                return docEntity;
            } catch (Exception ex) {
                _logger.Log (ex);
            }
            return null;

        }

        private object PrepareTabularReportForExport (TabularReport tabularReport, string company) {
            var report = new {

                title = tabularReport.Report.Title,
                company = company,
                columnDefinitions = tabularReport.Report.ColumnDefinitions.Where (cl => (bool) cl["IsVisible"]).Select (cl => new { title = cl["Title"].ToString (), fieldName = cl["FieldName"].ToString () })
            };

            var data = tabularReport.Data.Select (d => {
                var dataDict = ((IDictionary<string, object>) d);
                IDictionary<string, object> obj = new ExpandoObject ();

                obj.Add ("columnDefinitions", report.columnDefinitions);
                foreach (var col in report.columnDefinitions) {
                    var fieldName = col.fieldName.ToString ();
                    obj.Add (fieldName, dataDict[fieldName]);
                }
                return obj;
            });

            return new { report = report, data = data };
        }

        private string buildCSS (string htmlString) {
            var bootstrapUrl = $"{Directory.GetCurrentDirectory()}{Path.DirectorySeparatorChar}assets{Path.DirectorySeparatorChar}bootstrap.css";
            var bootstrapStyleContent = string.Join (" ", System.IO.File.ReadAllLines (bootstrapUrl));
            htmlString = htmlString.Replace ("{{:bootstrapStyleContent}}", bootstrapStyleContent);

            var customCssUrl = $"{Directory.GetCurrentDirectory()}{Path.DirectorySeparatorChar}assets{Path.DirectorySeparatorChar}custom.css";
            var customStyleContent = string.Join (" ", System.IO.File.ReadAllLines (customCssUrl));
            htmlString = htmlString.Replace ("{{:customStyleContent}}", customStyleContent);

            return htmlString;
        }

        private string constructHtmlPath (string submoduleType) {
            string basePath = $".{Path.DirectorySeparatorChar}external{Path.DirectorySeparatorChar}templates{Path.DirectorySeparatorChar}registration{Path.DirectorySeparatorChar}";
            switch (submoduleType) {
                case "FD":
                    return $"{basePath}food{Path.DirectorySeparatorChar}";
                case "MD":
                    return $"{basePath}medicalDevice{Path.DirectorySeparatorChar}";;
                case "MDCN":
                    return basePath;
                default:
                    return basePath;
            }
        }
    }
}