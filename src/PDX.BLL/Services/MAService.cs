using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using DataTables.AspNet.Core;
using Microsoft.AspNetCore.Http;
using PDX.BLL.Helpers;
using PDX.BLL.Helpers.ObjectDiffer;
using PDX.BLL.Model;
using PDX.BLL.Model.Config;
using PDX.BLL.Repositories;
using PDX.BLL.Services.Interfaces;
using PDX.BLL.Services.Interfaces.Email;
using PDX.BLL.Services.Notification;
using PDX.DAL.Helpers;
using PDX.DAL.Query;
using PDX.DAL.Repositories;
using PDX.Domain.Account;
using PDX.Domain.Commodity;
using PDX.Domain.Common;
using PDX.Domain.Customer;
using PDX.Domain.License;
using PDX.Domain.Public;
using PDX.Domain.Views;

namespace PDX.BLL.Services {
    public class MAService : Service<MA>, IMAService {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IProductService _productService;
        private readonly IDocumentService _documentService;
        private readonly IService<SupplierProduct> _supplierProductService;
        private readonly IService<MAStatus> _maStatusService;
        private readonly IService<MAType> _maTypeService;
        private readonly IService<MAChecklist> _maChecklistService;
        private readonly IMALogStatusService _maLogStatusService;
        private readonly IWIPService _wipService;
        private readonly IChecklistService _checklistService;
        private readonly IUserService _userService;
        private readonly IService<UserAgent> _userAgentService;
        private readonly IService<AgentSupplier> _agentSupplierService;
        private readonly IService<SystemSetting> _settingService;
        private readonly IService<vwMA> _vwMAService;
        private readonly IAgentService _agentService;
        private readonly IService<SystemSetting> _systemSettingService;
        private readonly IService<MAFieldSubmoduleType> _maFieldSubmoduleTypeService;
        private readonly IService<MAReview> _maReviewService;
        private readonly IService<Submodule> _submoduleService;
        private readonly IService<Sequence> _sequenceService;
        private readonly IService<ResponderType> _responderTypeService;
        private readonly NotificationFactory _notificationFacory;
        private readonly IService<UserRole> _userRoleService;
        private readonly IService<MAAssignment> _maAssignmentService;
        private readonly IService<ProductMD> _productMDService;
        private readonly IDiffer _differ;

        private SearchConfig searchConfig = new SearchConfig {
            Strings = new List<string> {
            "MANumber",
            "MAStatus",
            "MAType",
            "MAStatusDisplayName",
            "AgentName",
            "SupplierName",
            "VerificationNumber",
            "BrandName",
            "FullItemName"
            }
        };

        //Key => Role Code, Value => array of MA Statuses
        private IDictionary<string, string[]> _maStatusRoleDictionaryMap = new Dictionary<string, string[]> ();
        //Key => Role Code, Value => Expression
        private IDictionary<string, Expression> _roleExpressionDictionaryMap = new Dictionary<string, Expression> ();
        //Key => MA Status Code, Value => Tuple of Email subject and body
        private IDictionary<string, Tuple<string, string, bool>> _emailContentDictionaryMap = new Dictionary<string, Tuple<string, string, bool>> ();

        public MAService (IUnitOfWork unitOfWork, IMARepository maRepository, IProductService productService,
            IDocumentService documentService, IService<MAStatus> maStatusService,
            IMALogStatusService maLogStatusService, IWIPService wipService,
            IService<SupplierProduct> supplierProductService,
            IService<vwMA> vwMAService, IService<UserAgent> userAgentService,
            IUserService userService, IService<SystemSetting> settingService, IService<AgentSupplier> agentSupplierService,
            IChecklistService checklistService, IService<MAType> maTypeService,
            IService<MAChecklist> maChecklistService, IAgentService agentService,
            IService<SystemSetting> systemSettingService, IService<MAFieldSubmoduleType> FieldSubmoduleTypeService,
            IService<MAReview> maReviewService, IService<Submodule> submoduleService,
            IService<Sequence> sequenceService, IService<ResponderType> responderTypeService,
            NotificationFactory notificationFacory,
            IService<UserRole> userRoleService,
            IService<MAAssignment> maAssignmentService,
            IService<ProductMD> productMDService, IDiffer differ) : base (unitOfWork, maRepository) {

            _unitOfWork = unitOfWork;
            _productService = productService;
            _documentService = documentService;
            _maStatusService = maStatusService;
            _maTypeService = maTypeService;
            _maLogStatusService = maLogStatusService;
            _wipService = wipService;
            _checklistService = checklistService;
            _supplierProductService = supplierProductService;
            _vwMAService = vwMAService;
            _userAgentService = userAgentService;
            _agentSupplierService = agentSupplierService;
            _userService = userService;
            _settingService = settingService;
            _maChecklistService = maChecklistService;
            _agentService = agentService;
            _systemSettingService = systemSettingService;
            _maFieldSubmoduleTypeService = FieldSubmoduleTypeService;
            _maReviewService = maReviewService;
            _submoduleService = submoduleService;
            _sequenceService = sequenceService;
            _responderTypeService = responderTypeService;
            _differ = differ;
            _notificationFacory = notificationFacory;
            _userRoleService = userRoleService;
            _maAssignmentService = maAssignmentService;
            _productMDService = productMDService;

            PopulateMAStatusRoleDictionaryMap ();
            PopulateEmailContentDictionaryMap ();
        }

        public async Task<ApiResponse> CreateMANewApplicationAsync (MABusinessModel maNewApp) {
            var userPrivilage = await CheckIfUserHasAccessCreatingMA (maNewApp.MA.CreatedByUserID, maNewApp.MA.SupplierID);
            if (userPrivilage != null) return userPrivilage;

            var ma = await SaveNewMA (maNewApp);
            if (ma != null) {
                //Product
                maNewApp.Product.GenericName = maNewApp.Product.INN.Name;
                maNewApp.Product.Name = maNewApp.Product.BrandName;
                maNewApp.Product.CreatedByUserID = maNewApp.MA.CreatedByUserID;

                //hold product md temporarly
                ProductMD productMd = null;
                string producttypeCode = maNewApp.Product.ProductType.ProductTypeCode;
                if (producttypeCode == "MDS") {

                    productMd = maNewApp.Product.ProductMD;
                }

                //nullify objects to be saved
                maNewApp.Product.NullifyForeignKeys ();
                //Reset Product ID and Rowguid since it is required to create new product entity
                maNewApp.Product.NullifyPrimaryKeys ();
                maNewApp.Product.NullifyProperty ("RowGuid");
                maNewApp.Product.RowGuid = System.Guid.NewGuid ();
                //ProductSupplier
                var supplierProduct = new SupplierProduct {
                    MAID = ma.ID,
                    SupplierID = ma.SupplierID
                };

                maNewApp.Product.SupplierProducts.Add (supplierProduct);

                var result = await _productService.CreateOrUpdateAsync (maNewApp.Product, false);

                //if medical device update product md
                if (producttypeCode == "MDS") {
                    maNewApp.Product.ProductMD = productMd;
                    maNewApp.Product.ProductMD.NullifyForeignKeys ();
                }
                result = result && await _unitOfWork.SaveAsync ();

                if (result) {
                    //Delete WIP
                    await DeleteWip (maNewApp.Identifier);
                    //Send Email
                    var users = new List<User> ();
                    users.Add (ma.CreatedByUser);
                    Notify (ma.MAStatus.MAStatusCode, ma.MAStatus.Name, ma.MANumber, users);

                    return new ApiResponse {
                        StatusCode = StatusCodes.Status200OK,
                            IsSuccess = result,
                            Message = result ? "Market Authorization with type of new application created successfuly!" : "Error in creating Market Authorization with type of new application",
                            Result = result,
                            Type = typeof (bool).ToString ()
                    };
                }

                //If saving product fails, delete saved MA to rollback
                await RollbackMA (ma.ID);
            }

            return new ApiResponse {
                StatusCode = StatusCodes.Status400BadRequest,
                    IsSuccess = false,
                    Message = "Error in creating Market Authorization with type of new application"
            };
        }

        public async Task<ApiResponse> CreateMARenewalAsync (MABusinessModel maRenewal) {
            var userPrivilage = await CheckIfUserHasAccessCreatingMA (maRenewal.MA.CreatedByUserID, maRenewal.MA.SupplierID);
            if (userPrivilage != null) return userPrivilage;

            var maType = await _maTypeService.GetAsync (mt => mt.MATypeCode == maRenewal.SubmoduleCode);
            maRenewal.MA.MATypeID = maType.ID;
            var originalSupplierProduct = await _supplierProductService.GetAsync (sp => sp.MAID == (int) maRenewal.MA.OriginalMAID);

            var ma = await SaveNewMA (maRenewal);
            var result = false;

            if (ma != null) {
                //ProductSupplier
                var supplierProduct = new SupplierProduct {
                MAID = ma.ID,
                SupplierID = ma.SupplierID,
                ProductID = originalSupplierProduct.ProductID
                };
                result = await _supplierProductService.CreateAsync (supplierProduct, false) &&
                    await _unitOfWork.SaveAsync ();
            }

            if (result) {
                //Delete WIP
                await DeleteWip (maRenewal.Identifier);

                //Send Email
                var users = new List<User> ();
                users.Add (ma.CreatedByUser);
                Notify (ma.MAStatus.MAStatusCode, ma.MAStatus.Name, ma.MANumber, users);

                return new ApiResponse {
                    StatusCode = StatusCodes.Status200OK,
                        IsSuccess = true,
                        Message = "Market Authorization with type of renewal application created successfuly!",
                        Result = true,
                        Type = typeof (bool).ToString ()
                };
            }

            //If saving product fails, deleted saved MA to rollback
            await RollbackMA (ma.ID);

            return new ApiResponse {
                StatusCode = StatusCodes.Status400BadRequest,
                    IsSuccess = false,
                    Message = "Error in creating Market Authorization with type of renewal"
            };
        }

        public async Task<ApiResponse> CreateMAVariationAsync (MABusinessModel maVariation) {
            var userPrivilage = await CheckIfUserHasAccessCreatingMA (maVariation.MA.CreatedByUserID, maVariation.MA.SupplierID);
            if (userPrivilage != null) return userPrivilage;

            //MA Type of Variation
            var maType = await _maTypeService.GetAsync (mt => mt.MATypeCode == maVariation.SubmoduleCode);
            maVariation.MA.MATypeID = maType.ID;
            maVariation.MA.IsPremarketLabRequest = false;

            var ma = await SaveNewMA (maVariation);

            if (ma != null) {
                bool result = false;

                //MA field
                var productMAField = _maFieldSubmoduleTypeService.GetAsync (mf => mf.FieldSubmoduleType.Field.FieldCode == "Product" && mf.MAID == maVariation.MA.OriginalMAID);

                if (productMAField != null) {
                    //ProductSupplier
                    var supplierProduct = new SupplierProduct {
                    MAID = ma.ID,
                    SupplierID = ma.SupplierID
                    };
                    maVariation.Product.SupplierProducts.Add (supplierProduct);

                    maVariation.Product.NullifyForeignKeys ();
                    //Reset Product ID and Rowguid since it is required to create new product entity
                    maVariation.Product.NullifyPrimaryKeys ();
                    maVariation.Product.NullifyProperty ("RowGuid");
                    maVariation.Product.RowGuid = System.Guid.NewGuid ();

                    result = await _productService.CreateAsync (maVariation.Product);

                    result = result && await _unitOfWork.SaveAsync ();

                    if (result) {
                        //Save MAReview from applicants comment
                        if (!string.IsNullOrEmpty (maVariation.Comment)) {
                            var maReview = new MAReview {
                                MAID = ma.ID,
                                Comment = maVariation.Comment,
                                ResponderID = maVariation.MA.CreatedByUserID,
                                ResponderTypeID = (await _responderTypeService.GetAsync (rt => rt.ResponderTypeCode == "APL")).ID,
                                SuggestedStatusID = (await _maStatusService.GetAsync (rt => rt.MAStatusCode == "RQST")).ID
                            };

                            await _maReviewService.CreateAsync (maReview);
                        }

                        //Delete WIP
                        await DeleteWip (maVariation.Identifier);

                        //Send Email
                        var users = new List<User> ();
                        users.Add (ma.CreatedByUser);
                        Notify (ma.MAStatus.MAStatusCode, ma.MAStatus.Name, ma.MANumber, users);

                        return new ApiResponse {
                            StatusCode = StatusCodes.Status200OK,
                                IsSuccess = result,
                                Message = result ? "Market Authorization with type of variation created successfuly!" : "Error in creating Market Authorization with type of variation",
                                Result = result,
                                Type = typeof (bool).ToString ()
                        };
                    }

                    //If saving product fails, deleted saved MA to rollback
                    await RollbackMA (ma.ID);

                }

            }

            return new ApiResponse {
                StatusCode = StatusCodes.Status400BadRequest,
                    IsSuccess = false,
                    Message = "Error in creating Market Authorization with type of variation"
            };

        }
        public async Task<ApiResponse> UpdateMAAsync (MABusinessModel maNewApp) {
            maNewApp.MA.IsSRA = !string.IsNullOrEmpty (maNewApp.MA.SRA);
            if (maNewApp.MA.MAVariationSummary != null) {
                maNewApp.MA.MAVariationSummary.CreatedByUserID = maNewApp.MA.CreatedByUserID;
            }

            var oldMAStatusID = maNewApp.MA.MAStatusID;
            var isFIROrRTAResponse = (new List<string> { "RTA", "FIR" }).Contains (maNewApp.MA.MAStatus.MAStatusCode);

            if (isFIROrRTAResponse) {
                maNewApp.MA.MAStatusID = (await _maStatusService.GetAsync (mas => mas.MAStatusCode == $"{maNewApp.MA.MAStatus.MAStatusCode}R")).ID; //RTAR and FIRR
            }

            //hold product md temporarly
            ProductMD productMd = null;
            string producttypeCode = maNewApp.Product.ProductType.ProductTypeCode;
            if (producttypeCode == "MDS") {
                maNewApp.Products = null;
                productMd = maNewApp.Product.ProductMD;
            }

            //nullify objects to be saved
            maNewApp.MA.NullifyForeignKeys ();
            maNewApp.Product.NullifyForeignKeys ();

            var result = await base.UpdateAsync (maNewApp.MA, false);
            result = result && (await _productService.UpdateAsync (maNewApp.Product, false));
            //if medical device update product md
            if (producttypeCode == "MDS") {
                productMd.NullifyForeignKeys ();
                result = result && (await _productMDService.UpdateAsync (productMd, false));
            }

            bool docResult = true;

            if (result) {
                if (!isFIROrRTAResponse) {
                    //Detach saved Reference
                    docResult = await _documentService.DetachDocumentReferenceAsync (maNewApp.MA.ID, false);
                }
                //Add MA Status Log and MAReview Entry
                var maLogStatus = new MALogStatus {
                    MAID = maNewApp.MA.ID,
                    FromStatusID = oldMAStatusID,
                    ToStatusID = maNewApp.MA.MAStatusID,
                    IsCurrent = true,
                    Comment = maNewApp.Comment ?? "No comment is given by user",
                    ModifiedByUserID = maNewApp.MA.ModifiedByUserID
                };

                var maReview = new MAReview {
                    MAID = maNewApp.MA.ID,
                    Comment = maNewApp.Comment ?? "No comment is given by user",
                    ResponderID = maNewApp.MA.CreatedByUserID,
                    ResponderTypeID = (await _responderTypeService.GetAsync (rt => rt.ResponderTypeCode == "APL")).ID,
                    SuggestedStatusID = (await _maStatusService.GetAsync (rt => rt.MAStatusCode == "FIRR")).ID
                };

                result = result && await _maReviewService.CreateAsync (maReview, false);
                result = result && await _maLogStatusService.CreateAsync (maLogStatus, false);

                if (docResult) {
                    foreach (var doc in (maNewApp.Documents.Concat (maNewApp.Dossiers))) {
                        doc.ReferenceID = maNewApp.MA.ID;
                        //doc is domain model. in order to do updated, we need to fetch the actual db model
                        var savedDoc = await _documentService.GetAsync (doc.ID);
                        savedDoc.CopyProperties (doc);
                        docResult = docResult && await _documentService.UpdateAsync (savedDoc, false);
                    }
                }

                result = result && docResult && await _unitOfWork.SaveAsync ();
                var savedMA = await base.GetAsync (maNewApp.MA.ID);
                if (result) {
                    var users = new List<User> ();
                    users.Add (savedMA.CreatedByUser);
                    if (savedMA.MAStatus.MAStatusCode == "RTAR") {
                        users.AddRange ((await _userRoleService.FindByAsync (us => us.Role.RoleCode == "CST" && us.IsActive)).Select (u => u.User).Where (u => u.IsActive).ToList ());
                        users.AddRange ((await _maAssignmentService.FindByAsync (maa => maa.MAID == savedMA.ID)).Where (a => a.ResponderType.ResponderTypeCode == "PRSC" && a.IsActive).Select (u => u.AssignedToUser).ToList ());
                    }
                    if (savedMA.MAStatus.MAStatusCode == "FIRR") {
                        users.AddRange ((await _maAssignmentService.FindByAsync (maa => maa.MAID == savedMA.ID)).Where (a => a.ResponderType.ResponderTypeCode == "PRAS" && a.IsActive).Select (u => u.AssignedToUser).ToList ());
                    }
                    Notify (savedMA.MAStatus.MAStatusCode, savedMA.MAStatus.Name, savedMA.MANumber, users);
                }

                return new ApiResponse {
                    StatusCode = StatusCodes.Status200OK,
                        IsSuccess = result,
                        Message = result ? $"Market Authorization with application number of {maNewApp.MA.MANumber} updated successfuly!" :
                        $"Error in updating Market Authorization with application number of {maNewApp.MA.MANumber}",
                        Result = result,
                        Type = typeof (bool).ToString ()
                };
            }

            return new ApiResponse {
                StatusCode = StatusCodes.Status400BadRequest,
                    IsSuccess = false,
                    Message = $"Error in updating Market Authorization with application number of {maNewApp.MA.MANumber}"
            };

        }

        public async Task<MABusinessModel> GetMABusinessModel (MA ma) {
            var product = await _productService.GetProductByMAAsync (ma.ID);
            var products = product.ProductType.ProductTypeCode == "MDS" ? await _productService.GetProductsByMAAsync (ma.ID) : null;

            //Filter only Prescreen checklists
            var maChecklists = ma.MAChecklists.Where (mac => mac.Checklist.ChecklistType.ChecklistTypeCode == "PSCR" && mac.Checklist.IsSRA == ma.IsSRA).ToList ();
            var uploadedDocuments = await _documentService.GetDocumentAsync (ma.MAType.MATypeCode, ma.ID);
            var submodule = await _submoduleService.GetAsync (sub => sub.SubmoduleCode == ma.MAType.MATypeCode);

            //Copy UploadedDocuments in Documents
            var documents = new List<Document> ();
            var dossiers = new List<Document> ();

            foreach (var ud in uploadedDocuments) {
                var document = new Document ();
                document.CopyProperties (ud);
                if (ud.ModuleDocument.DocumentType.IsDossier) {
                    dossiers.Add (document);
                } else {
                    documents.Add (document);
                }

            }

            return new MABusinessModel {
                MA = ma,
                    Product = product,
                    Products = products,
                    UploadedDocuments = uploadedDocuments,
                    Documents = documents,
                    Dossiers = dossiers,
                    Checklists = await _checklistService.GetChecklistByMAAsync (maChecklists, ma.MAType.MATypeCode, "PSCR", ma.IsSRA), //Filter Prescreen checklist types
                    SubmoduleCode = ma.MAType.MATypeCode,
                    SubmoduleTypeCode = submodule.SubmoduleType.SubmoduleTypeCode,
                    Identifier = ma.RowGuid,
                    IsLabResultUploaded = uploadedDocuments.Any (d => d.ModuleDocument.DocumentType.DocumentTypeCode == "LABR"),
                    FIRGeneratedCount = (await _maLogStatusService.GetMALogStatuses (ma.ID, "FIR")).Count ()
            };
        }

        public async Task<ApiResponse> GetMABusinessModel (int id) {
            var ma = await this.GetAsync (m => m.ID == id);
            if (ma == null) {
                return new ApiResponse {
                StatusCode = StatusCodes.Status200OK,
                IsSuccess = false,
                Message = "Market Authorization Not found!!",
                Result = null,
                Type = typeof (bool).ToString ()
                };
            }
            if (!ma.IsActive && ma.MAStatus.MAStatusCode == "DEL") {
                return new ApiResponse {
                StatusCode = StatusCodes.Status200OK,
                IsSuccess = false,
                Message = "Market Authorization has been deleted!!",
                Result = null,
                Type = typeof (bool).ToString ()
                };
            }
            var maBusinessModel = await this.GetMABusinessModel (ma);
            return new ApiResponse {
                StatusCode = StatusCodes.Status200OK,
                    IsSuccess = true,
                    Message = "Market Authorization Successfully Returned!!",
                    Result = maBusinessModel,
                    Type = typeof (MABusinessModel).ToString ()
            };;
        }
        public async Task<MABusinessModel> GetMA (int id) {
            var ma = await this.GetAsync (id);
            var product = await _productService.GetProductByMAAsync (ma.ID);
            var submodule = await _submoduleService.GetAsync (sub => sub.SubmoduleCode == ma.MAType.MATypeCode);

            return new MABusinessModel {
                MA = ma,
                    Product = product,
                    SubmoduleCode = ma.MAType.MATypeCode,
                    SubmoduleTypeCode = submodule?.SubmoduleType?.SubmoduleTypeCode,
                    Identifier = ma.RowGuid
            };
        }

        public async Task<MAStatus> GetMAStatus (int id) {
            var ma = await this.GetAsync (id);
            return ma.MAStatus;
        }

        /// <summary>
        /// Get MA By User
        /// </summary>
        /// <param name="userID"></param>
        /// <returns></returns>
        public async Task<IEnumerable<MA>> GetMAByUserAsync (int userID) {
            //TODO user product type filter
            var userAgent = (await _userAgentService.FindByAsync (ua => ua.UserID == userID)).FirstOrDefault ();
            if (userAgent == null) return null;

            var suppliers = (await _agentSupplierService.FindByAsync (ua => ua.IsActive && ua.AgentID == userAgent.AgentID && ua.AgentLevel.AgentLevelCode == "FAG")).Select (asup => asup.SupplierID);

            var response = await base.FindByAsync (p => suppliers.Contains (p.SupplierID) && !(new List<string> { "DRFT", "WITH", "ARCH" }).Contains (p.MAStatus.MAStatusCode));
            return response;
        }

        public async Task<IEnumerable<MABusinessModel>> GetMAForRenewalByUserAsync (int userID) {
            //TODO user product type filter
            //Negate startDays 
            var startDays = -1 * Convert.ToInt32 ((await _settingService.GetAsync (ss => ss.SystemSettingCode == "MARED")).Value);
            var endDays = Convert.ToInt32 ((await _settingService.GetAsync (ss => ss.SystemSettingCode == "MANET")).Value);

            var maListForRenewal = await this.GetMAByUserAsync (userID);
            maListForRenewal = maListForRenewal.Where (ma => ma.IsActive && ma.MAStatus.MAStatusCode == "APR" && ma.ExpiryDate != null && !ma.MANumber.Contains ("/NMDR/LD") &&
                Enumerable.Range (startDays, (-1 * startDays + endDays)).Contains (Convert.ToInt32 (((DateTime) ma.ExpiryDate - DateTime.UtcNow).TotalDays)));
            var wips = await _wipService.FindByAsync (wip => (new List<string> { "REN", "VAR" }).Contains (wip.Type) && wip.UserID == userID && wip.IsActive);

            var maBusinessModels = new List<MABusinessModel> ();

            //Update maList so that it contains MAs whose expiry date fall in the range of [-startDays, endDays]
            foreach (var ma in maListForRenewal) {
                var submodule = await _submoduleService.GetAsync (sub => sub.SubmoduleCode == ma.MAType.MATypeCode);

                if (!(await this.ExistsAsync (oma => oma.IsActive && oma.OriginalMAID != null && oma.OriginalMAID == ma.ID)) &&
                    !(wips.Any (mab => mab.ContentObject.MA.OriginalMAID == ma.ID))) {
                    var product = await _productService.GetProductByMAAsync (ma.MAType.MATypeCode == "RREN" ? (int) ma.OriginalMAID : ma.ID);
                    var maBusinessModel = new MABusinessModel {
                        MA = ma,
                        Product = product,
                        SubmoduleCode = ma.MAType.MATypeCode,
                        SubmoduleTypeCode = submodule?.SubmoduleType?.SubmoduleTypeCode
                    };
                    maBusinessModels.Add (maBusinessModel);
                }
            }

            return maBusinessModels;
        }

        public async Task<IEnumerable<MABusinessModel>> GetMAForVariationByUserAsync (int userID) {
            //TODO user product type filter
            var maListForVariation = await this.GetMAByUserAsync (userID);
            maListForVariation = maListForVariation.Where (ma => ma.MAStatus.MAStatusCode == "APR" && !ma.MANumber.Contains ("/NMDR/LD") && !ma.IsExpired && ma.IsActive);
            var wips = await _wipService.FindByAsync (wip => (new List<string> { "REN", "VAR" }).Contains (wip.Type) && wip.UserID == userID && wip.IsActive);

            var maBusinessModels = new List<MABusinessModel> ();

            foreach (var ma in maListForVariation) {
                if (!(await this.ExistsAsync (oma => oma.IsActive && oma.MAStatus.MAStatusCode!="REJ" && oma.OriginalMAID != null && oma.OriginalMAID == ma.ID)) &&
                    !(wips.Any (mab => mab.ContentObject.MA.OriginalMAID == ma.ID))) {
                    var product = (await _supplierProductService.GetAsync (sp => sp.MAID == ma.ID))?.Product;
                    var maBusinessModel = new MABusinessModel {
                        MA = ma,
                        BrandName = product?.FullItemName
                    };
                    maBusinessModels.Add (maBusinessModel);
                }
            }

            return maBusinessModels;
        }

        /// <summary>
        /// MA page
        /// </summary>
        /// <param name="request"></param>
        /// <param name="userID"></param>
        /// <returns></returns>
        public async Task<DataTablesResult<vwMA>> GetMAPageAsync (IDataTablesRequest request, int? userID = null, string submoduleTypeCode = null) {
            //TODO user product type filter
            var user = await _userService.GetAsync (us => us.ID == userID);
            var fRole = user.Roles.FirstOrDefault ();
            var suppliers = new List<int> ();
            var maTypes = (await _maTypeService.FindByAsync (mt => fRole.SubmoduleTypes.Contains (mt.SubmoduleType.SubmoduleTypeCode)))?.Where (st => (submoduleTypeCode == null) || (submoduleTypeCode != null && st.SubmoduleType.SubmoduleTypeCode == submoduleTypeCode))?.Select (mt => mt.MATypeCode)?.ToList ();

            //For Food submodule Notification type to jump normal process
            if (fRole.RoleCode == "IPA") {
                var userAgent = (await _userAgentService.FindByAsync (ua => ua.UserID == userID)).FirstOrDefault ();
                if (userAgent == null) return null;
                suppliers = (await _agentSupplierService.FindByAsync (ua => ua.IsActive &&
                        ua.AgentID == userAgent.AgentID &&
                        ua.AgentLevel.AgentLevelCode == "FAG"))
                    .Select (asup => asup.SupplierID).ToList ();

            }

            Expression<Func<vwMA, bool>> predicate = ConstructFilter<vwMA> (userID, fRole.RoleCode, suppliers, request.Search.Value, maTypes);

            //order by expression
            OrderBy<vwMA> orderBy = new OrderBy<vwMA> (qry => qry.OrderBy (e => e.MAStatusPriority).ThenBy (x => x.CreatedDate));
            if (!string.IsNullOrEmpty (fRole.RoleCode)) {
                switch (fRole.RoleCode) {
                    case "IPA":
                        orderBy = new OrderBy<vwMA> (qry => qry.OrderByDescending (e => e.CreatedDate));
                        break;
                    case "ROLE_MODERATOR": 
                    case "ROLE_HEAD":
                        orderBy = new OrderBy<vwMA> (qry => qry.OrderBy (e => e.IsFastTracking)
                            .ThenByDescending (f => f.IsSRA)
                            .ThenBy (g => g.MAStatusPriority)
                            .ThenBy (h => h.VerificationNumber));
                        break;
                }
            }

            var response = await _vwMAService.GetPageAsync (request, predicate, orderBy.Expression);
            return response;
        }

        public async Task<bool> DeleteMAAsync (MAStatusLogModel maStatusLog) {
            var ma = await this.GetAsync (maStatusLog.MAID);
            if (ma != null) {
                var toStatus = await _maStatusService.GetAsync (mas => mas.MAStatusCode == "DEL");
                var fromStatusID = ma.MAStatusID;
                ma.MAStatusID = toStatus.ID;
                ma.IsActive = false;

                //Update MA
                var result = await base.UpdateAsync (ma);

                //Create MA Status Log
                var maLogStatus = new MALogStatus {
                    MAID = ma.ID,
                    ToStatusID = toStatus.ID,
                    IsCurrent = true,
                    Comment = maStatusLog.Comment,
                    ModifiedByUserID = maStatusLog.ChangedByUserID,
                    FromStatusID = fromStatusID,
                };
                result = result && await _maLogStatusService.CreateAsync (maLogStatus, true);
                return result;
            }
            return false;
        }

        /// <summary>
        /// Change MA Status 
        /// </summary>
        /// <param name="maStatusLog"></param>
        /// <returns></returns>
        public async Task<bool> ChangeMAStatusAsync (MAStatusLogModel maStatusLog) {
            var ma = await this.GetAsync (maStatusLog.MAID);
            if (ma != null) {
                var toStatus = await _maStatusService.GetAsync (mas => mas.MAStatusCode == maStatusLog.ToStatusCode);
                var fromStatusID = ma.MAStatusID;
                ma.MAStatusID = toStatus.ID;

                //Update MA
                var result = await base.UpdateAsync (ma);

                //Create MA Status Log
                var maLogStatus = new MALogStatus {
                    MAID = ma.ID,
                    ToStatusID = toStatus.ID,
                    IsCurrent = true,
                    Comment = maStatusLog.Comment,
                    ModifiedByUserID = maStatusLog.ChangedByUserID,
                    FromStatusID = fromStatusID,
                };

                result = result && await _maLogStatusService.CreateAsync (maLogStatus);
                if (result) {
                    var savedMA = await base.GetAsync (m => m.RowGuid == ma.RowGuid);

                    var users = new List<User> () { savedMA.ModifiedByUser };

                    if (toStatus.MAStatusCode == "VER" || toStatus.MAStatusCode == "FIRR") {
                        users.AddRange ((await _userRoleService.FindByAsync (us => us.Role.RoleCode == "ROLE_MODERATOR" && us.IsActive)).Select (u => u.User).Where (u => u.IsActive).ToList ());
                    }
                    if (toStatus.MAStatusCode == "RTAR" || toStatus.MAStatusCode == "FATCH") {
                        users.AddRange ((await _userRoleService.FindByAsync (us => us.Role.RoleCode == "CST" && us.IsActive)).Select (u => u.User).Where (u => u.IsActive).ToList ());
                        users.AddRange ((await _maAssignmentService.FindByAsync (maa => maa.MAID == savedMA.ID)).Where (a => a.ResponderType.ResponderTypeCode == "PRSC" && a.IsActive).Select (u => u.AssignedToUser).ToList ());
                    }
                    if (toStatus.MAStatusCode == "ASD" || toStatus.MAStatusCode == "FIRR") {
                        users.AddRange ((await _maAssignmentService.FindByAsync (maa => maa.MAID == savedMA.ID)).Where (a => a.ResponderType.ResponderTypeCode == "PRAS" && a.IsActive).Select (u => u.AssignedToUser).ToList ());
                    }
                    if (toStatus.MAStatusCode == "SFA" || toStatus.MAStatusCode == "SFR") {
                        users.AddRange ((await _userRoleService.FindByAsync (us => us.Role.RoleCode == "ROLE_HEAD" && us.IsActive)).Select (u => u.User).Where (u => u.IsActive).ToList ());
                    }
                    Notify (toStatus.MAStatusCode, toStatus.Name, savedMA.MANumber, users);
                }

                //If Status is FIRR (FIR Replied), Make all submitted reviews to Draft
                if (toStatus.MAStatusCode == "FIRR") {
                    var responderTypeCodes = new List<string> { "PRAS", "SCAS", "TLD" };
                    var maReviews = await _maReviewService.FindByAsync (mar => mar.MAID == ma.ID && responderTypeCodes.Contains (mar.ResponderType.ResponderTypeCode));
                    foreach (var maReview in maReviews) {
                        maReview.IsDraft = true;
                    }
                    await _maReviewService.CreateOrUpdateAsync (maReviews, true);
                }

                return result;
            }

            return false;
        }

        /// <summary>
        /// Insert or Update MAChecklist
        /// </summary>
        /// <param name="maChecklist"></param>
        /// <returns></returns>
        public async Task<bool> InsertOrUpdateMAChecklistAsync (IList<MAChecklist> maChecklist, bool commit = true) {
            var result = await _maChecklistService.CreateOrUpdateAsync (maChecklist, commit, "MAID", "ChecklistID", "ResponderTypeID");
            return result;
        }

        /// <summary>
        /// Approve MA
        /// </summary>
        /// <param name="ma"></param>
        /// <returns></returns>
        public async Task<ApiResponse> ApproveMAAsync (MA ma) {
            var maEntity = await base.GetAsync (ma.ID);
            var maStatus = await _maStatusService.GetAsync (s => s.MAStatusCode == "APR");
            var oldMAStatusID = maEntity.MAStatusID;

            var expirtyDate = await GetExpiryDate (maEntity.MAType.MATypeCode, maEntity.OriginalMAID);
            var result = expirtyDate != null;

            //Change status and append reg/expiry dates
            maEntity.MAStatusID = maStatus.ID;
            maEntity.RegistrationDate = DateTime.UtcNow;
            maEntity.ExpiryDate = expirtyDate;

            //Update it
            result = result && await base.UpdateAsync (maEntity, false);

            if (result) {
                //Fetch supplier product corresponding to this MA 
                var supplierProduct = await _supplierProductService.GetAsync (sp => sp.MAID == ma.ID);
                supplierProduct.RegistrationDate = maEntity.RegistrationDate;
                supplierProduct.ExpiryDate = maEntity.ExpiryDate;

                //Update supplierProduct
                result = result && await _supplierProductService.UpdateAsync (supplierProduct, false);

                //Add MA Status Log Entry
                var maLogStatus = new MALogStatus {
                    MAID = ma.ID,
                    FromStatusID = oldMAStatusID,
                    ToStatusID = maStatus.ID,
                    IsCurrent = true,
                    Comment = $"Market Authorization With Application Number of {maEntity.MANumber} is Approved",
                    ModifiedByUserID = ma.ModifiedByUserID
                };
                result = result && await _maLogStatusService.CreateAsync (maLogStatus, false);

                var maType = await _maTypeService.GetAsync (maEntity.MATypeID);

                //Deactivate/Archived Original MA for Variation and Renewal Type
                if ((new List<string> { "RREN", "VMIN", "VMAJ" }).Contains (maType.MATypeCode)) {
                    var originalMA = await this.GetAsync ((int) maEntity.OriginalMAID);
                    var archivedStatus = await _maStatusService.GetAsync (f => f.MAStatusCode == "ARCH");
                    var originalSupplierProduct = await _supplierProductService.GetAsync (sp => sp.MAID == (int) maEntity.OriginalMAID);

                    //Add MA Status Log Entry
                    var originalMALogStatus = new MALogStatus {
                        MAID = originalMA.ID,
                        FromStatusID = originalMA.MAStatusID,
                        ToStatusID = archivedStatus.ID,
                        IsCurrent = true,
                        Comment = $"Market Authorization With Application Number of {originalMA.MANumber} is Archived",
                        ModifiedByUserID = maEntity.ModifiedByUserID
                    };

                    originalMA.IsActive = false;
                    originalMA.MAStatusID = archivedStatus.ID;

                    if (originalSupplierProduct != null) originalSupplierProduct.IsActive = false;

                    await this.UpdateAsync (originalMA, false);
                    await _supplierProductService.UpdateAsync (originalSupplierProduct, false);
                    await _maLogStatusService.CreateAsync (originalMALogStatus, false);
                }

                result = result && await _unitOfWork.SaveAsync ();
                //Send Email
                if (result) {
                    var maEntitySaved = await base.GetAsync (ma.ID);
                    var users = new List<User> () { maEntitySaved.CreatedByUser };
                    Notify (maStatus.MAStatusCode, maStatus.Name, maEntity.MANumber, users);

                }
            }

            return new ApiResponse {
                StatusCode = StatusCodes.Status200OK,
                    IsSuccess = result,
                    Message = result ? $"Market Authorization with application number of {maEntity.MANumber} is approved successfuly!" : "Error in approving Market Authorization",
                    Result = result,
                    Type = typeof (bool).ToString ()
            };

        }

        public async Task<bool> GeneratePremarketLabRequestAsync (int maID) {

            var maEntity = await base.GetAsync (maID);
            maEntity.IsPremarketLabRequest = true;
            var result = await base.UpdateAsync (maEntity);
            return result;
        }

        public async Task<bool> RollbackMA (int maID) {
            var ma = await base.GetAsync (maID);
            //Get Submodule from MAType
            var submoduleCode = await _submoduleService.GetAsync (s => s.SubmoduleCode == ma.MAType.MATypeCode);
            //Get sequence using moduleCode
            var maSequence = await _sequenceService.GetAsync (s => s.TableName == submoduleCode.Module.ModuleCode);
            //Update LastValue of maSequence
            maSequence.LastValue--;

            var result = await _sequenceService.UpdateAsync (maSequence, false);
            result = result && await base.DeleteAsync (maID, false);

            result = result && await _unitOfWork.SaveAsync ();
            return result;
        }

        public async Task<MAReview> GetMAReview (int maID, string responderTypeCode, string suggestedStatusCode) {
            var maReview = (await _maReviewService.FindByAsync (mar => mar.MAID == maID && !mar.IsDraft && mar.ResponderType.ResponderTypeCode == responderTypeCode && mar.SuggestedStatus.MAStatusCode == suggestedStatusCode))?.OrderByDescending (rev => rev.CreatedDate)?.FirstOrDefault ();
            return maReview;
        }

        public async Task<IEnumerable<Difference>> GetVariationChangesAsync (int maID) {
            var toMA = await this.GetMA (maID);

            if (toMA.MA.OriginalMAID != null) {
                var fromMA = await this.GetMA ((int) toMA.MA.OriginalMAID);
                NullifyProductManufacturer (toMA.Product);
                NullifyProductManufacturer (fromMA.Product);
                var difference = _differ.Diff<Product> (toMA.Product, fromMA.Product, null);
                return difference.ChildDiffs.Where (s => s.PropertyType == "String");
            }
            return null;
        }

        public async Task<bool> PopulateDraftMAToWIP () {
            var draftMAs = await this.FindByAsync (ma => ma.MAStatus.MAStatusCode == "DRFT");
            var result = true;

            foreach (var ma in draftMAs) {
                var maBusinessModel = await this.GetMABusinessModel (ma);
                var submodule = await _submoduleService.GetAsync (s => s.SubmoduleCode == ma.MAType.MATypeCode);

                var wip = new WIP {
                    Type = submodule.Module.ModuleCode,
                    ContentObject = maBusinessModel,
                    UserID = ma.CreatedByUserID,
                    RowGuid = maBusinessModel.Identifier
                };

                result = result && await _wipService.CreateOrUpdateAsync (wip);
            }

            return result;
        }

        private void Notify (string statusCode, string status, string applicationNumber, IEnumerable<User> users, string source = "ma") {
            var emailNotifier = _notificationFacory.GetNotification (NotificationType.EMAIL);
            var pushNotifier = _notificationFacory.GetNotification (NotificationType.PUSHNOTIFICATION);
            if (_emailContentDictionaryMap.ContainsKey (statusCode)) {
                var emailTuple = _emailContentDictionaryMap[statusCode];
                if (emailTuple.Item3) emailNotifier.Notify (users, new EmailSend (emailTuple.Item1, emailTuple.Item2, status, users.FirstOrDefault ().Username, applicationNumber, "Application"), "APR");
                //push notification
                var body = String.Format (statusCode == "FIR" ? AlertTemplates.FurtherInformationRequested : AlertTemplates.MarketAuthorizationStatusChange, status, applicationNumber, DateTime.Now);
                pushNotifier.Notify (users, new { body = body, subject = emailTuple.Item1 }, "MASC");

            }
        }

        #region Private Methods
        private async Task<MA> SaveNewMA (MABusinessModel maNewApp) {
            MA ma = null;

            maNewApp.MA.NullifyForeignKeys ();
            maNewApp.MA.NullifyPrimaryKeys ();
            maNewApp.MA.NullifyProperty ("RowGuid");

            //Prepare MA for Saving
            var maStatus = await _maStatusService.GetAsync (s => s.MAStatusCode == "RQST");
            maNewApp.MA.MAStatusID = maStatus.ID;
            maNewApp.MA.RowGuid = maNewApp.Identifier;
            maNewApp.MA.MANumber = maNewApp.Identifier.ToString ();
            maNewApp.MA.IsSRA = maNewApp.MA.IsSRA || !string.IsNullOrEmpty (maNewApp.MA.SRA);
            maNewApp.MA.ModifiedByUserID = maNewApp.MA.CreatedByUserID;
            maNewApp.MA.ExpiryDate = maNewApp.MA.RegistrationDate = null;
            maNewApp.MA.CreatedDate = maNewApp.MA.ModifiedDate = DateTime.UtcNow;
            if (maNewApp.MA.MAVariationSummary != null) {
                maNewApp.MA.MAVariationSummary.CreatedByUserID = maNewApp.MA.CreatedByUserID;
            }

            var result = await base.CreateAsync (maNewApp.MA); //Commit MA here since ID is needed to save ma relationships

            if (result) {
                ma = await SaveMARelatedEntities (maNewApp);
            }

            return ma;
        }

        private async Task<MA> SaveMARelatedEntities (MABusinessModel maNewApp) {
            var result = true;
            var ma = await base.GetAsync (maNewApp.MA.ID);

            //Save Documents
            foreach (var doc in (maNewApp.Documents.Concat (maNewApp.Dossiers))) {
                doc.ReferenceID = ma.ID;
                //Translate business model document to db model
                var savedDoc = doc.ID != 0 ? await _documentService.GetAsync (doc.ID) : await _documentService.GetAsync (d => d.RowGuid == doc.RowGuid);

                if (savedDoc == null) {
                    result = false;
                    break;
                }

                savedDoc.CopyProperties (doc);

                result = result && await _documentService.UpdateAsync (savedDoc, false);
            }

            //Add MA Status Log Entry
            var maLogStatus = new MALogStatus {
                MAID = ma.ID,
                ToStatusID = ma.MAStatusID,
                IsCurrent = true,
                Comment = $"New Appliction With Type of {ma.MAType.Name}  Created",
                ModifiedByUserID = ma.CreatedByUserID
            };
            result = result && await _maLogStatusService.CreateAsync (maLogStatus, false);

            return result ? ma : null;
        }

        private async Task<ApiResponse> CheckIfUserHasAccessCreatingMA (int userID, int supplierID) {

            var agentLevel = await _agentService.GetAgentLevelByUserAndSupplier (userID, supplierID);
            if (agentLevel.AgentLevelCode != "FAG") {
                return new ApiResponse {
                StatusCode = StatusCodes.Status405MethodNotAllowed,
                IsSuccess = false,
                Message = "Creating Market Authorization is allowed only for users who are representative of first agents."
                };
            }
            return null;
        }

        private Expression<Func<T, bool>> ConstructFilter<T> (int? userID, string roleCode, IList<int> suppliers, string search = null, IList<string> maTypes = null) {
            Expression<Func<T, bool>> expression = null;
            ParameterExpression argument = Expression.Parameter (typeof (T), "ma");
            Expression predicateBody = null;
            Expression e1 = null, e2 = null, e3 = null, e4 = null, e5 = null;

            PopulateRoleExpressionDictionaryMap (argument, userID, suppliers);

            if (!string.IsNullOrEmpty (roleCode)) {
                if (_maStatusRoleDictionaryMap.ContainsKey (roleCode)) e1 = argument.DynamicContains ("MAStatusCode", _maStatusRoleDictionaryMap[roleCode]);
                if (_roleExpressionDictionaryMap.ContainsKey (roleCode)) e2 = _roleExpressionDictionaryMap[roleCode];

                predicateBody = e2 != null ? Expression.AndAlso (e1, e2) : e1;

                //filter user submodule type 
                if (maTypes != null) {

                    e3 = argument.DynamicContains ("MATypeCode", maTypes);
                    predicateBody = e3 != null?Expression.AndAlso (predicateBody, e3) : predicateBody;
                }

                //filter food notification type
                //if team leader or head and requested status 
                if (roleCode == "ROLE_MODERATOR") {
                    // e4 = Expression.AndAlso (argument.DynamicContains ("MAStatusCode", new List<string> { "RQST" }), argument.DynamicContains ("MATypeCode", new List<string> { "FNT", "NTNIVD", "NTIVD" }));
                    // e4 = Expression.OrElse (e4, argument.GetExpression ("MAStatusCode", "RQST", "NotEqual", typeof (string)));
                    // predicateBody = e4 != null?Expression.AndAlso (predicateBody, e4) : predicateBody;
                }

                //Search filter expression 
                if (!string.IsNullOrEmpty (search) && search.Length > 2) {
                    Expression pb = null;
                    foreach (var str in searchConfig.Strings) {
                        var exp = argument.StringContains (str, search);
                        pb = pb == null ? exp : Expression.OrElse (pb, exp);
                    }
                    predicateBody = Expression.AndAlso (predicateBody, pb);
                }

            }

            expression = Expression.Lambda<Func<T, bool>> (predicateBody, new [] { argument });
            return expression;
        }

        private async Task<DateTime?> GetExpiryDate (string maTypeCode, int? originalMAID = null) {
            //Variation types should take expiry date from original ma 
            if (maTypeCode == "VMIN" || maTypeCode == "VMAJ") {
                var originalMA = originalMAID.HasValue ? await this.GetAsync ((int) originalMAID) : null;
                return originalMA?.ExpiryDate;
            } else if (maTypeCode == "FNT") {
                var maExpSetting = await _systemSettingService.GetAsync (ss => ss.SystemSettingCode == "FNTEXP");
                if (maExpSetting != null) {
                    int expiryInYears = Convert.ToInt32 (maExpSetting.Value);
                    return DateTime.UtcNow.AddYears (expiryInYears);
                }
            } else {
                var maExpSetting = await _systemSettingService.GetAsync (ss => ss.SystemSettingCode == "MAEXP");
                if (maExpSetting != null) {
                    int expiryInYears = Convert.ToInt32 (maExpSetting.Value);
                    return DateTime.UtcNow.AddYears (expiryInYears);
                }
            }
            return null;
        }

        private async Task DeleteWip (Guid identifier) {
            //Deleted Related WIP
            var wip = await _wipService.GetAsync (w => w.RowGuid == identifier);
            if (wip != null) {
                await _wipService.DeleteAsync (wip.ID);
            }
        }

        private void NullifyProductManufacturer (Product product) {
            foreach (var productManufacturer in product.ProductManufacturers) {
                productManufacturer.ManufacturerAddress.Manufacturer.ManufacturerAddresses = null;
            }
        }

        private void PopulateMAStatusRoleDictionaryMap () {
            //Role => Administrator
            _maStatusRoleDictionaryMap.Add ("ADM", new string[] { "APR", "ARCH", "ASD", "FATCH", "FIRR", "FIR", "PRSC", "REJ", "RQST", "RTA", "RTAS", "RTL", "RTAR", "SFA", "SFIR", "SFR", "STL", "VER" });
            //Role => SuperAdmin
            _maStatusRoleDictionaryMap.Add ("SADM", new string[] { "APR", "ARCH", "ASD", "FATCH", "FIRR", "FIR", "PRSC", "REJ", "RQST", "RTA", "RTAS", "RTL", "RTAR", "SFA", "SFIR", "SFR", "STL", "VER" });
            //Role => Applicant
            _maStatusRoleDictionaryMap.Add ("IPA", new string[] { "APR", "ASD", "DRFT", "FATCH", "FIRR", "FIR", "PRSC", "REJ", "RQST", "RTA", "RTAS", "RTL", "RTAR", "SFA", "SFIR", "SFR", "STL", "VER", "VOID", "WITH" });
            //Role => Customer Service Officer
            _maStatusRoleDictionaryMap.Add ("CSO", new string[] { "APR", "FATCH", "PRSC", "REJ", "RQST", "RTA", "RTAR", "VER" });
            //Role => Customer Service Director
            _maStatusRoleDictionaryMap.Add ("CSD", new string[] { "APR", "FATCH", "PRSC", "REJ", "RQST", "RTA", "RTAR", "VER" });
            //Role => Customer Service Team Leader
            _maStatusRoleDictionaryMap.Add ("CST", new string[] { "APR", "FATCH", "PRSC", "REJ", "RQST", "RTA", "RTAR", "VER" });
            //Role => Lab Team Leader
            _maStatusRoleDictionaryMap.Add ("ROLE_LAB_MODERATOR", new string[] { "LARQ", "LARS", "STL", "ASD", "FIRR", "FIR", "RTAS", "SFA", "SFR" });
            //Role => Lab Director
            _maStatusRoleDictionaryMap.Add ("ROLE_LAB_HEAD", new string[] { "LARQ", "LARS", "STL", "ASD", "FIRR", "FIR", "RTAS", "SFA", "SFR" });
            //Role => Lab Officer
            _maStatusRoleDictionaryMap.Add ("ROLE_LAB", new string[] { "LARQ", "LARS", "STL", "ASD", "FIRR", "FIR", "RTAS", "SFA", "SFR" });
            //Role => Medicine Dossier Assessor
            _maStatusRoleDictionaryMap.Add ("ROLE_REVIEWER", new string[] { "APR", "ASD", "FIRR", "FIR", "REJ", "RTAS", "SFA", "SFR", "STL" });
            //Role => Food Dossier Assessor
            _maStatusRoleDictionaryMap.Add ("ROLE_FOOD_REVIEWER", new string[] { "FATCH", "PRSC", "REJ", "RQST", "RTA", "RTAR", "VER","APR", "ASD", "FIRR", "FIR", "REJ", "RTAS", "SFA", "SFR", "STL" });
            //Role => Medicine Registration Director
            _maStatusRoleDictionaryMap.Add ("ROLE_HEAD", new string[] { "APR", "ASD", "FATCH", "FIRR", "FIR", "PRSC", "REJ", "RQST", "RTA", "RTAS", "RTL", "RTAR", "SFA", "SFIR", "SFR", "STL", "VER", "VOID" });
            //Role => Medicine Registration Team Leader
            _maStatusRoleDictionaryMap.Add ("ROLE_MODERATOR", new string[] { "APR", "ASD", "FATCH", "FIRR", "FIR", "PRSC", "REJ", "RQST", "RTA", "RTAS", "RTL", "RTAR", "SFA", "SFIR", "SFR", "STL", "VER", "VOID" });
            //Role => Secretary Profile
            _maStatusRoleDictionaryMap.Add ("ROLE_CLINICAL", new string[] { "APR", "ASD", "FIRR", "FIR", "RTAS", "RTL", "SFA", "SFIR", "SFR", "STL" });
            //Role => Quality Assurance
            _maStatusRoleDictionaryMap.Add ("QA", new string[] { "APR", "ASD", "FATCH", "FIRR", "FIR", "PRSC", "REJ", "RQST", "RTA", "RTAS", "RTL", "RTAR", "SFA", "SFIR", "SFR", "STL", "VER", "VOID" });

            //Role => Port Inspectors
            _maStatusRoleDictionaryMap.Add ("PINS", new string[] { "APR" });
        }

        private void PopulateRoleExpressionDictionaryMap (ParameterExpression argument, int? userID, IList<int> suppliers) {
            //Role => Applicant
            _roleExpressionDictionaryMap.Add ("IPA", argument.DynamicContains ("SupplierID", suppliers));
            //Role => Customer Service Officer
            _roleExpressionDictionaryMap.Add ("CSO", argument.GetExpression ("PrescreenerUserID", userID, "Equal", typeof (int?)));
            //Role => Medicine Dossier Assessor
            _roleExpressionDictionaryMap.Add ("ROLE_REVIEWER", Expression.OrElse (argument.GetExpression ("PrimaryAssessorUserID", userID, "Equal", typeof (int?)), argument.GetExpression ("SecondaryAssessorUserID", userID, "Equal", typeof (int?))));
            //Role => Food Dossier Assessor
            _roleExpressionDictionaryMap.Add ("ROLE_FOOD_REVIEWER",Expression.OrElse (argument.GetExpression ("PrescreenerUserID", userID, "Equal", typeof (int?)), Expression.OrElse (argument.GetExpression ("PrimaryAssessorUserID", userID, "Equal", typeof (int?)), argument.GetExpression ("SecondaryAssessorUserID", userID, "Equal", typeof (int?)))));
            
            //Role => Lab Team Leader
            _roleExpressionDictionaryMap.Add ("ROLE_LAB_MODERATOR", argument.GetExpression ("IsLabRequested", true, "Equal", typeof (bool)));
            //Role => Lab Director
            _roleExpressionDictionaryMap.Add ("ROLE_LAB_HEAD", argument.GetExpression ("IsLabRequested", true, "Equal", typeof (bool)));
            //Role => Lab Officer
            _roleExpressionDictionaryMap.Add ("ROLE_LAB", argument.GetExpression ("IsLabRequested", true, "Equal", typeof (bool)));
        }

        private void PopulateEmailContentDictionaryMap () {
            //MA Status Code => Approved
            _emailContentDictionaryMap.Add ("APR", new Tuple<string, string, bool> ("Registration Approved", "Your application has been approved.", true));
            //MA Status Code => Rejected
            _emailContentDictionaryMap.Add ("REJ", new Tuple<string, string, bool> ("Registration Rejected", "Your application has been rejected.", true));
            //MA Status Code => Requested
            _emailContentDictionaryMap.Add ("RQST", new Tuple<string, string, bool> ("Registration Submitted", "Your application has been submitted.", true));
            //MA Status Code => Returned to Applicant
            _emailContentDictionaryMap.Add ("RTA", new Tuple<string, string, bool> ("Registration Returned", "Your application has been returned, check the comment given.", true));
            //MA Status Code => Further Information Required
            _emailContentDictionaryMap.Add ("FIR", new Tuple<string, string, bool> ("Registration Further Information Required", "Your application has required further information, check the comment given.", true));
            //MA Status Code => Prescreened
            _emailContentDictionaryMap.Add ("PRSC", new Tuple<string, string, bool> ("Registration Pre-screened", "Your application has been prescreened, please upload registration fee receipt.", true));
            //MA Status Code => Verfied
            _emailContentDictionaryMap.Add ("VER", new Tuple<string, string, bool> ("Registration Verfied", "Your application has been verified", true));
            //MA Status Code => AssignedW
            _emailContentDictionaryMap.Add ("ASD", new Tuple<string, string, bool> ("Registration Assigned", "Your application has been assigned", false));
            //MA Status Code => RTA replied
            _emailContentDictionaryMap.Add ("RTAR", new Tuple<string, string, bool> ("Registration Replied", "Your application has been replied", false));
            //MA Status Code => Fee Attached
            _emailContentDictionaryMap.Add ("FATCH", new Tuple<string, string, bool> ("Registration Fee Attached", "Your application has been fee attached", false));
            //MA Status Code => FIR - Replied
            _emailContentDictionaryMap.Add ("FIRR", new Tuple<string, string, bool> ("Registration FIR Replied", "Your application has been FIR replied", false));
            //MA Status Code => SF Approved
            _emailContentDictionaryMap.Add ("SFA", new Tuple<string, string, bool> ("Registration Submitted for approval", "Your application has been submitted for approval", false));
            //MA Status Code => SF Rejection
            _emailContentDictionaryMap.Add ("SFR", new Tuple<string, string, bool> ("Registration Submmited for rejection", "Your application has been submitted for rejection", false));

        }
        #endregion
    }
}