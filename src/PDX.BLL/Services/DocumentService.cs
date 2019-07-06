using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using iTextSharp.text;
using iTextSharp.text.pdf;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using PDX.BLL.Helpers;
using PDX.BLL.Model;
using PDX.BLL.Services.Interfaces;
using PDX.DAL.Repositories;
using PDX.Domain.Account;
using PDX.Domain.Common;
using PDX.Domain.Document;

namespace PDX.BLL.Services {
    public class DocumentService : Service<Domain.Document.Document>, IDocumentService {
        private readonly Model.Config.AttachmentConfig _attachmentConfig;
        private readonly string _rootUrl;
        private readonly IService<ModuleDocument> _moduleDocumentService;
        private readonly IService<DocumentType> _documentTypeService;
        private readonly PDX.Logging.ILogger _logger;
        private readonly IService<PrintLog> _printLog;
        private readonly IUserService _userService;
        private readonly IService<RolePermission> _rolePermissionService;
        private readonly IList<string> waterMarkedDocuments ;

        /// <summary>
        /// Constructor for DocumentService
        /// </summary>
        /// <param name="unitOfWork"></param>
        /// <param name="attachmentConfig"></param>
        public DocumentService (IUnitOfWork unitOfWork, IOptions<Model.Config.AttachmentConfig> attachmentConfig,
            IService<ModuleDocument> moduleDocumentService, IService<DocumentType> documentTypeService, PDX.Logging.ILogger logger, IService<PrintLog> printLog, IUserService userService, IService<RolePermission> rolePermissionService) : base (unitOfWork) {
            _attachmentConfig = attachmentConfig.Value;
            _rootUrl = _attachmentConfig.RootUrl;
            _moduleDocumentService = moduleDocumentService;
            _documentTypeService = documentTypeService;

            _logger = logger;
            _printLog = printLog;
            _userService = userService;
            _rolePermissionService = rolePermissionService;

            waterMarkedDocuments = new List<string> { "VANL", "MAREJ", "MACRT" };
        }

        /// <summary>
        /// Document creating as async
        /// </summary>
        /// <param name="document"></param>
        /// <returns></returns>
        public async Task<Domain.Document.Document> CreateDocumentAsync (PDX.BLL.Model.Document document) {
            try {
                var moduleDocument = await _moduleDocumentService.GetAsync (document.ModuleDocumentID);
                var moduleCode = moduleDocument.Submodule.Module.ModuleCode;
                string dossierDirectory = string.Empty;

                if (moduleDocument.DocumentType.IsDossier) {
                    var dossierSB = new StringBuilder ();
                    await GetDossierDirectory (moduleDocument.DocumentType.ParentDocumentTypeID, dossierSB);
                    if (dossierSB.Length != 0) {
                        dossierSB.Insert (0, $"{Path.DirectorySeparatorChar}Dossier");
                        dossierDirectory = dossierSB.ToString ();
                    }

                }

                var fileExtension = Path.GetExtension (document.File.FileName);
                var relativeDirectory = $"{Path.DirectorySeparatorChar}{moduleCode}{Path.DirectorySeparatorChar}{document.TempFolderName}{dossierDirectory}";
                var directory = $"{_rootUrl}{relativeDirectory}";
                var filePath = $"{relativeDirectory}{Path.DirectorySeparatorChar}{document.TempFileName}{fileExtension}";

                //Check if current document is already saved 
                Domain.Document.Document docEntity = (await base.FindByAsync (d => d.FilePath == filePath)).FirstOrDefault ();
                var result = true;
                if (docEntity == null) {
                    docEntity = new Domain.Document.Document ();
                    docEntity.CopyProperties (document);
                    await FileHelper.SaveFileAsync (directory, document.TempFileName, document.File);

                    //restore filepath
                    docEntity.FilePath = filePath;
                    result = await base.CreateAsync (docEntity);
                } else {
                    //Replace existing file by the new one (from document)
                    await FileHelper.SaveFileAsync (directory, document.TempFileName, document.File);
                }

                return result ? docEntity : null;
            } catch (Exception e) {
                _logger.Log (e);
                Console.WriteLine (e.Message);
            }
            return null;

        }
        /// <summary>
        /// Read Document As Base64 String
        /// </summary>
        /// <param name="documentID"></param>
        /// <returns></returns>
        public async Task<string> ReadDocumentAsBase64Async (int documentID) {
            try {
                var document = await base.GetAsync (documentID);
                var filePath = $"{_rootUrl}{document.FilePath}";
                var base64 = FileHelper.ReadFileAsBase64 (filePath);
                return base64;
            } catch (Exception ex) {
                _logger.Log (ex);
            }
            return null;
        }

        /// <summary>
        /// Read Physical File
        /// </summary>
        /// <param name="documentID"></param>
        /// <returns></returns>
        public async Task<PhysicalFileResult> ReadPhysicalFile (int documentID) {
            try {
                var document = await base.GetAsync (documentID);
                var filePath = $"{_rootUrl}{document.FilePath}";
                //override path
                filePath = filePath.Replace ('\\', Path.DirectorySeparatorChar);
                var filePathWaterMark = filePath;

                if (document.ModuleDocument.DocumentType.IsSystemGenerated && waterMarkedDocuments.Contains(document.ModuleDocument.DocumentType.DocumentTypeCode)) {
                    filePathWaterMark = filePath.Replace (document.ModuleDocument.DocumentType.Name, $"{document.ModuleDocument.DocumentType.Name} Water-Marked");
                    await GenerateWaterMark (filePath, filePathWaterMark);
                }

                var result = new PhysicalFileResult (filePathWaterMark, document.FileType);
                return result;
            } catch (Exception ex) {
                _logger.Log (ex);
            }
            return null;
        }

        public async Task<PhysicalFileResult> ReadPhysicalFile(string relativeFilePath, string fileType)
        {
            try
            {
                var filePath = $"{_rootUrl}{relativeFilePath}";
                //override path
                filePath = filePath.Replace('\\', Path.DirectorySeparatorChar);

                var result = new PhysicalFileResult(filePath, fileType);
                return result;
            }
            catch (Exception ex)
            {
                _logger.Log(ex);
            }
            return null;
        }

        public async Task<PhysicalFileResult> ReadPhysicalFileByPermission(int documentID, int userID, string permission)
        {
            try
            {
                var document = await base.GetAsync(documentID);
                var filePath = $"{_rootUrl}{document.FilePath}";
                //override path
                filePath = filePath.Replace ('\\', Path.DirectorySeparatorChar);
                var filePathWaterMark = filePath;
                //check if the user has permission to generate without the water mark
                var user = await _userService.GetAsync (userID);
                var roleIDs = user.Roles.Select (r => r.ID);
                //var menus = await FindByAsync(x => x.MenuRoles.Any(y => y.IsActive && roleIDs.Contains(y.RoleID)));
                var permissions = (await _rolePermissionService.FindByAsync (x => x.IsActive && roleIDs.Contains (x.RoleID))).Select (rp => rp.Permission.PermissionCode);
                
                if (document.ModuleDocument.DocumentType.IsSystemGenerated && !permissions.Contains (permission) && waterMarkedDocuments.Contains(document.ModuleDocument.DocumentType.DocumentTypeCode)) {
                    filePathWaterMark = filePath.Replace (document.ModuleDocument.DocumentType.Name, $"{document.ModuleDocument.DocumentType.Name} Water-Marked");
                    await GenerateWaterMark (filePath, filePathWaterMark);
                }

                var result = new PhysicalFileResult (filePathWaterMark, document.FileType);
                return result;
            } catch (Exception ex) {
                _logger.Log (ex);
            }
            return null;
        }
        public async Task<PhysicalFileResult> PrintPhysicalFile (int documentID, int userID) {
            try {
                var document = await base.GetAsync (documentID);
                var filePath = $"{_rootUrl}{document.FilePath}";
                //override path
                filePath = filePath.Replace ('\\', Path.DirectorySeparatorChar);
                var result = new PhysicalFileResult (filePath, document.FileType);

                var printlog = new PrintLog {
                    PrintedByUserID = userID,
                    DocumentID = documentID,
                    PrintedDate = DateTime.UtcNow,
                };

                await _printLog.CreateAsync (printlog);

                return result;
            } catch (Exception ex) {
                _logger.Log (ex);
            }
            return null;
        }

        public async Task<byte[]> ReadFile (int documentID) {
            try {
                var document = await base.GetAsync (documentID);
                var filePath = $"{_rootUrl}{document.FilePath}";
                var fileBytes = FileHelper.ReadFile (filePath);
                return fileBytes;
            } catch (Exception ex) {
                _logger.Log (ex);
            }
            return null;
        }

        public async Task<List<Domain.Document.Document>> GetDocumentAsync (string submoduleCode, int referenceID) {
            var ipDocument = await base.FindByAsync (d => d.ReferenceID == referenceID && d.ModuleDocument.Submodule.SubmoduleCode == submoduleCode);
            var documents = ipDocument.Select (d => d.ModuleDocument);
            return ipDocument.ToList ();
        }

        public async Task<List<Domain.Document.Document>> GetDocumentAsync (int referenceID) {
            var ipDocument = await base.FindByAsync (d => d.ReferenceID == referenceID);
            var documents = ipDocument.Select (d => d.ModuleDocument);
            return ipDocument.ToList ();
        }

        public async Task<bool> DetachDocumentReferenceAsync (int referenceID, bool commit = true) {
            var ipDocument = await base.FindByAsync (d => d.ReferenceID == referenceID);
            var result = true;
            foreach (var docEntity in ipDocument) {
                docEntity.ReferenceID = 0;
                result = result && await base.UpdateAsync (docEntity, commit);
            }
            return result;
        }

        private async Task GenerateWaterMark (string filePath, string filePathWaterMark) {
            try {
                using (FileStream stream = new FileStream (filePathWaterMark, FileMode.OpenOrCreate)) {
                    PdfReader pdfReader = new PdfReader (filePath);

                    PdfStamper pdfStamper = new PdfStamper (pdfReader, stream);
                    for (int pageIndex = 1; pageIndex <= pdfReader.NumberOfPages; pageIndex++) {
                        //Rectangle class in iText represent geomatric representation... in this case, rectanle object would contain page geomatry
                        Rectangle pageRectangle = pdfReader.GetPageSizeWithRotation (pageIndex);
                        //pdfcontentbyte object contains graphics and text content of page returned by pdfstamper
                        PdfContentByte pdfData = pdfStamper.GetUnderContent (pageIndex);
                        //create fontsize for watermark
                        pdfData.SetFontAndSize (BaseFont.CreateFont (BaseFont.HELVETICA_BOLD, BaseFont.CP1252, BaseFont.NOT_EMBEDDED), 45);
                        //create new graphics state and assign opacity
                        PdfGState graphicsState = new PdfGState ();
                        graphicsState.FillOpacity = 0.4F;
                        //set graphics state to pdfcontentbyte
                        pdfData.SetGState (graphicsState);
                        //set color of watermark
                        pdfData.SetColorFill (BaseColor.Gray);
                        //indicates start of writing of text
                        pdfData.BeginText ();
                        //show text as per position and rotation
                        pdfData.ShowTextAligned (Element.ALIGN_CENTER, "Not Official Copy", pageRectangle.Width / 2, pageRectangle.Height / 2, 45);

                        //call endText to invalid font set
                        pdfData.EndText ();
                    }
                    //close stamper and output filestream
                    //stream.Dispose();
                    pdfStamper.Close ();
                    pdfReader.Close ();
                }
            } catch (Exception ex) {
                _logger.Log (ex);
            }
        }

        private async Task GetDossierDirectory (int? documentDocumentTypeID, StringBuilder path) {
            if (documentDocumentTypeID == null) return;
            var documentType = await _documentTypeService.GetAsync ((int) documentDocumentTypeID);

            path = path.Insert (0, $"{Path.DirectorySeparatorChar}{documentType.DocumentTypeCode}");
            await GetDossierDirectory (documentType.ParentDocumentTypeID, path);
        }
    }
}