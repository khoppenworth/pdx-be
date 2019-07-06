using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Threading.Tasks;
using DataTables.AspNet.AspNetCore;
using DataTables.AspNet.Core;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using PDX.BLL.Helpers;
using PDX.BLL.Model;
using PDX.BLL.Model.Config;
using PDX.BLL.Repositories;
using PDX.BLL.Services.Interfaces;
using PDX.BLL.Services.Interfaces.Email;
using PDX.BLL.Services.Notification;
using PDX.DAL.Query;
using PDX.DAL.Repositories;
using PDX.Domain;
using PDX.Domain.Account;
using PDX.Domain.Commodity;
using PDX.Domain.Common;
using PDX.Domain.Procurement;
using PDX.Domain.Views;

namespace PDX.BLL.Services {
    public class ImportPermitService : Service<PDX.Domain.Procurement.ImportPermit>, IImportPermitService {

        private const int PAGE_SIZE = 50;

        private readonly IUnitOfWork _unitOfWork;
        private readonly IUserService _userService;
        private readonly IService<UserRole> _userRoleService;
        private readonly IDocumentService _documentService;
        private readonly IService<vwImportPermit> _vwImportPermitService;
        private readonly IService<vwPIP> _vwPIPService;
        private readonly IService<ImportPermitLogStatus> _importLog;
        private readonly IService<ImportPermitStatus> _importPermitStatus;
        private readonly IService<ImportPermitType> _importPermitTypeService;
        private readonly IService<UserAgent> _userAgentService;
        private readonly IService<Submodule> _submoduleService;
        private readonly IService<SupplierProduct> _supplierProduct;
        private readonly IService<SystemSetting> _systemSettingService;
        private readonly IService<Country> _countryService;
        private readonly IAgentService _agentService;
        private readonly IWIPService _wipService;
        private readonly NotificationFactory _notificationFactory;
        private readonly SystemUserConfig _systemUser;

        private SearchConfig searchConfig = new SearchConfig {
            Strings = new List<string> {
            "ImportPermitNumber",
            "ImportPermitStatus",
            "ImportPermitStatusDisplayName",
            "ShippingMethod",
            "PaymentMode",
            "PortOfEntry",
            "AgentName",
            "SupplierName",
            "AssignedUser",
            "PerformaInvoiceNumber"
            }
        };

        private IDictionary<string, Tuple<string, string>> _emailContentDictionaryMap = new Dictionary<string, Tuple<string, string>> ();

        public ImportPermitService (IUnitOfWork unitOfWork, IImportPermitRepository importPermitRepository, IUserService userService, IDocumentService documentService,
            IService<vwImportPermit> vwImportPermitService, IService<vwPIP> vwPIPService, IService<ImportPermitLogStatus> importLog,
            IService<ImportPermitStatus> importPermitStatus, IService<ImportPermitType> importPermitTypeService,
            IService<UserRole> userRoleService, IService<UserAgent> userAgentService, IService<Submodule> submoduleService,
            IService<SupplierProduct> supplierProduct,
            IService<SystemSetting> systemSettingService, IAgentService agentService,
            IService<Country> countryService, IWIPService wipService, NotificationFactory notificationFactory, IOptions<SystemUserConfig> options) : base (unitOfWork, importPermitRepository) {
            _unitOfWork = unitOfWork;
            _userService = userService;
            _userRoleService = userRoleService;
            _documentService = documentService;
            _vwImportPermitService = vwImportPermitService;
            _vwPIPService = vwPIPService;
            _importLog = importLog;
            _importPermitStatus = importPermitStatus;
            _importPermitTypeService = importPermitTypeService;
            _userAgentService = userAgentService;
            _supplierProduct = supplierProduct;
            _submoduleService = submoduleService;
            _systemSettingService = systemSettingService;
            _countryService = countryService;
            _agentService = agentService;
            _wipService = wipService;
            _notificationFactory = notificationFactory;
            _systemUser = options.Value;

            PopulateEmailContentDictionaryMap ();
        }
        public async override Task<ImportPermit> GetAsync (int ID, bool cacheRemove = false) {
            var iPermit = await this.GetAsync (ip => ip.ID == ID, cacheRemove);
            return iPermit;
        }

        public async override Task<ImportPermit> GetAsync (Expression<Func<ImportPermit, bool>> predicate, bool cacheRemove = false) {
            var iPermit = await base.GetAsync (predicate, cacheRemove);
            var supProduct = await _supplierProduct.FindByAsync (f => f.SupplierID == iPermit.SupplierID && f.IsExpired.HasValue && f.IsActive);
            foreach (var ipermitDetail in iPermit.ImportPermitDetails) {
                //Expiry date from product-supplier expirydate
                var product = supProduct.FirstOrDefault (f => f.ProductID == ipermitDetail.ProductID);
                ipermitDetail.Product.ExpiryDate = product?.ExpiryDate;
                ipermitDetail.Product.RegistrationDate = product?.RegistrationDate;
                ipermitDetail.Product.ProductStatus = product?.RegistrationDate == null? "Not Registered": "Registered";
            }
            return iPermit;
        }

        /// <summary>
        /// Get import permit business model 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<ImportPermitBusinessModel> GetImportPermitBusinessModel (int id) {
            var ipermit = await this.GetAsync (id);

            return new ImportPermitBusinessModel () {
                    ImportPermit = ipermit,
                    UploadedDocuments = await _documentService.GetDocumentAsync (id),
                    Identifier = ipermit.RowGuid,
                    SubmoduleCode = ipermit.Submodule?.SubmoduleCode
            };
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="ipermit"></param>
        /// <returns></returns>
        public async Task<bool> CreateImportPermitAsync (ImportPermitBusinessModel ipermit) {
            //Calculate Amount from Details
            ipermit.ImportPermit.Amount = ipermit.ImportPermit.FreightCost + (ipermit.ImportPermit.Insurance ?? 0) + (ipermit.ImportPermit.ImportPermitDetails.Sum (x => ((x.Quantity * x.UnitPrice) - (x.Discount ?? 0)))) - (ipermit.ImportPermit.Discount ?? 0);

            string statusCode = ipermit.IsDraft ? "DRFT" : "RQST";
            var status = await _importPermitStatus.GetAsync (s => s.ImportPermitStatusCode == statusCode);

            ipermit.ImportPermit.ImportPermitStatusID = status.ID;
            ipermit.ImportPermit.RowGuid = ipermit.Identifier;

            //Set RequestedDate if application is not draft
            ipermit.ImportPermit.RequestedDate = ipermit.IsDraft ? (Nullable<DateTime>) null : DateTime.UtcNow;
            ipermit.ImportPermit.ExpiryDate = await GetExpiryDate (ipermit.ImportPermit.SupplierID, ipermit.ImportPermit.ImportPermitDetails);
            ipermit.ImportPermit.CreatedDate = ipermit.ImportPermit.ModifiedDate = DateTime.UtcNow;

            //Set SubmoduleID for PIP Type
            var ipType = await _importPermitTypeService.GetAsync (ipermit.ImportPermit.ImportPermitTypeID);
            if (ipType.ImportPermitTypeCode == "PIP") {
                var agent = await _agentService.GetAsync (ipermit.ImportPermit.AgentID);
                var submodule = await _submoduleService.GetAsync (s => s.SubmoduleCode == agent.AgentType.AgentTypeCode);
                ipermit.ImportPermit.SubmoduleID = submodule?.ID;
            }

            var result = await base.CreateAsync (ipermit.ImportPermit);
            bool docResult = true, logStatus = true;
            if (result) {
                var savedImportPermit = (await base.GetAsync (ip => ip.RowGuid == ipermit.ImportPermit.RowGuid, true));
                foreach (var doc in ipermit.Documents) {
                    doc.ReferenceID = savedImportPermit.ID;
                    var savedDoc = await _documentService.GetAsync (doc.ID);
                    savedDoc.CopyProperties (doc);
                    docResult = docResult && await _documentService.UpdateAsync (savedDoc);
                }

                var importLog = new ImportPermitLogStatus () {
                    ToStatusID = status.ID,
                    IsCurrent = true,
                    Comment = $"New {savedImportPermit.ImportPermitType.Name} Created",
                    ModifiedByUserID = savedImportPermit.CreatedByUserID,
                    ImportPermitID = savedImportPermit.ID
                };

                logStatus = await _importLog.CreateAsync (importLog);

                //Send email
                if (!ipermit.IsDraft) {
                    //bring CST user
                    var cst = (await _userRoleService.FindByAsync (us => us.Role.RoleCode == "CST" && us.IsActive)).Select (u => u.User).Where (u => u.IsActive);
                    var users = new List<User> () { savedImportPermit.User };
                    users.AddRange (cst);
                    Notify (savedImportPermit.ImportPermitStatus.ImportPermitStatusCode, savedImportPermit.ImportPermitStatus.Name, savedImportPermit.ImportPermitNumber, users);
                }

                //Delete Related WIP
                var wip = await _wipService.GetAsync (w => w.RowGuid == ipermit.Identifier);
                if (wip != null) {
                    await _wipService.DeleteAsync (wip.ID);
                }

            }
            return result && docResult && logStatus;
        }

        public async Task<ApiResponse> CreateAutomaticImportPermitAsync (ImportPermitBusinessModel importPermit, int createdBy) {
            var result = false;
            createdBy = importPermit.CurrentStatusCode == ImportPermitStatuses.RQST? createdBy: (int) (await _userRoleService.GetAsync (us => us.User.Username == _systemUser.Username))?.UserID;
            var ipermit = new ImportStatusLog () { ChangedBy = createdBy, ID = importPermit.ImportPermit.ID };
            var returnResponse = new ApiResponse ();
            switch (importPermit.CurrentStatusCode) {
                case ImportPermitStatuses.RQST:
                    result = await this.CreateImportPermitAsync (importPermit);
                    returnResponse.Message = result? "Import Permit Created": "Unable to create Import Permit";
                    break;
                case ImportPermitStatuses.SFA:
                    ipermit.Comment = "Import Permit Automatically submitted For Approval";
                    result = await this.ChangeIPermitStatus (ipermit, ImportPermitStatuses.SFA);
                    returnResponse.Message = result? "Import Permit Submitted For Approval": "Unable to submit Import Permit";
                    break;
                case ImportPermitStatuses.APR:
                    ipermit.Comment = "Import Permit Automatically Approved";
                    result = await this.ChangeIPermitStatus (ipermit, ImportPermitStatuses.APR);
                    returnResponse.Message = result? "Import Permit Approved": "Unable to approve Import Permit";
                    break;
            }
            var savedID = importPermit.ImportPermit.ID != 0 ? importPermit.ImportPermit.ID : (await this.GetAsync (i => i.RowGuid == importPermit.Identifier, true))?.ID;
            var savedImportPermit = await this.GetImportPermitBusinessModel ((int) savedID);

            returnResponse.IsSuccess = result;
            returnResponse.StatusCode = result?StatusCodes.Status200OK : StatusCodes.Status400BadRequest;
            returnResponse.Result = result? savedImportPermit : null;
            return returnResponse;
        }
        public async Task<bool> UpdateImportPermitAsync (ImportPermitBusinessModel ipermit) {

            var savedImportPermit = (await base.GetAsync (ipermit.ImportPermit.ID));
            ipermit.ImportPermit.Amount = ipermit.ImportPermit.FreightCost + (ipermit.ImportPermit.Insurance ?? 0) + (ipermit.ImportPermit.ImportPermitDetails.Sum (x => ((x.Quantity * x.UnitPrice) - (x.Discount ?? 0)))) - (ipermit.ImportPermit.Discount ?? 0);

            var result = await base.UpdateAsync (ipermit.ImportPermit);
            bool docResult = true, docUpdateResult = true, logStatus = true;

            if (result) {
                //Detach saved reference 
                docUpdateResult = await _documentService.DetachDocumentReferenceAsync (ipermit.ImportPermit.ID);
                if (docUpdateResult) {
                    foreach (var doc in ipermit.Documents) {
                        doc.ReferenceID = savedImportPermit.ID;
                        var savedDoc = await _documentService.GetAsync (doc.ID);
                        savedDoc.CopyProperties (doc);
                        docResult = docResult && await _documentService.UpdateAsync (savedDoc);
                    }
                }

            }

            return result && docResult && docUpdateResult && logStatus;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="agentID"></param>
        /// <returns></returns>
        public async Task<List<ImportPermit>> GetAgentImportPermits (int agentID) {
            var result = (await base.FindByAsync (ip => ip.AgentID == agentID)).ToList ();
            return result;
        }

        public async Task<List<ImportPermit>> GetImportPermitsByUser (int userID, string importPermitTypeCode) {
            var user = await _userService.GetAsync (us => us.ID == userID);
            var userAgent = (await _userAgentService.FindByAsync (ua => ua.UserID == userID)).FirstOrDefault ();
            if (userAgent == null) return null;
            var result = (await base.FindByAsync (ip => ip.ImportPermitType.ImportPermitTypeCode == importPermitTypeCode && ip.AgentID == userAgent.AgentID)).ToList ();
            return result;
        }

        /// <summary>
        /// Get single IPermit Page
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<DataTablesResult<vwImportPermit>> GetImportPermitPage (IDataTablesRequest request, int? userID = null) {
            var user = await _userService.GetAsync (us => us.ID == userID);
            var fRole = user.Roles.FirstOrDefault ();
            var userAgent = (await _userAgentService.FindByAsync (ua => ua.UserID == userID)).FirstOrDefault ();

            Expression<Func<vwImportPermit, bool>> predicate = ConstructFilter<vwImportPermit> (userID, fRole.RoleCode, userAgent?.AgentID, request.Search.Value);

            //order by expression
            OrderBy<vwImportPermit> orderBy = new OrderBy<vwImportPermit> (qry => qry.OrderBy (e => e.ImportPermitStatusPriority).ThenBy (x => x.SubmissionDate));
            if (!string.IsNullOrEmpty (fRole.RoleCode) && fRole.RoleCode == "IPA") {
                orderBy = new OrderBy<vwImportPermit> (qry => qry.OrderByDescending (e => e.SubmissionDate));
            }

            var response = await _vwImportPermitService.GetPageAsync (request, predicate, orderBy.Expression);
            return response;
        }

        /// <summary>
        /// Get single PIP page
        /// </summary>
        /// <param name="request"></param>
        /// <param name="userID"></param>
        /// <returns></returns>
        public async Task<DataTablesResult<vwPIP>> GetPIPPage (IDataTablesRequest request, int? userID = default (int?)) {
            var user = await _userService.GetAsync (us => us.ID == userID);
            var fRole = user.Roles.FirstOrDefault ();
            var userAgent = (await _userAgentService.FindByAsync (ua => ua.UserID == userID)).FirstOrDefault ();

            Expression<Func<vwPIP, bool>> predicate = ConstructFilter<vwPIP> (userID, fRole.RoleCode, userAgent?.AgentID, request.Search.Value);

            //order by expression
            OrderBy<vwPIP> orderBy = new OrderBy<vwPIP> (qry => qry.OrderBy (e => e.ImportPermitStatusPriority).ThenBy (x => x.SubmissionDate));
            if (!string.IsNullOrEmpty (fRole.RoleCode) && fRole.RoleCode == "PIPA") {
                orderBy = new OrderBy<vwPIP> (qry => qry.OrderByDescending (e => e.SubmissionDate));
            }

            DataTablesResult<vwPIP> response = await _vwPIPService.GetPageAsync (request, predicate, orderBy.Expression);
            return response;
        }

        public async Task<bool> ChangeIPermitStatus (ImportStatusLog ipermit, ImportPermitStatuses status) {
            var toStatus = (await _importPermitStatus.GetAsync (s => s.ImportPermitStatusCode == status.ToString ()));

            var iimport = await base.GetAsync (ipermit.ID);
            var fromId = iimport.ImportPermitStatusID;
            iimport.ImportPermitStatusID = toStatus.ID;

            if (toStatus.ImportPermitStatusCode == "APR") {
                iimport.ExpiryDate = await GetExpiryDate (iimport.SupplierID, iimport.ImportPermitDetails);
            }
            var originaliimport = new ImportPermit ();
            originaliimport.CopyProperties (iimport);

            //users for notification
            var users = new List<User> () { originaliimport.User };

            if (toStatus.ImportPermitStatusCode == "RQST") {
                iimport.RequestedDate = DateTime.UtcNow;
                var cst = (await _userRoleService.FindByAsync (us => us.Role.RoleCode == "CST" && us.IsActive && us.User.IsActive)).Select (u => u.User);
                users.AddRange (cst);
            }

            var resultUpdate = await base.UpdateAsync (iimport);
            var result = false;
            if (resultUpdate) {
                var importLog = new ImportPermitLogStatus () {
                    FromStatusID = fromId,
                    ToStatusID = toStatus.ID,
                    IsCurrent = true,
                    Comment = ipermit.Comment,
                    ModifiedByUserID = ipermit.ChangedBy,
                    ImportPermitID = ipermit.ID
                };

                result = await _importLog.CreateAsync (importLog);
            }
            if (resultUpdate && result) Notify (toStatus.ImportPermitStatusCode, toStatus.Name, iimport.ImportPermitNumber, users);

            return resultUpdate && result;
        }

        public async Task<bool> CheckDuplicateInvoice (int supplierID, int agentID, string performaInvoice, int? importPermitID = null) {
            var ip = (await base.FindByAsync (i => i.PerformaInvoiceNumber == performaInvoice && i.SupplierID == supplierID && i.AgentID == agentID &&
                (importPermitID == null || (importPermitID != null && i.ID != importPermitID)))).ToList ();
            return ip.Count () > 0 ? true : false;
        }

        /// <summary>
        /// Search Import Permit
        /// </summary>
        /// <param name="query"></param>
        /// <param name="pageNumber"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public async Task<DataTablesResult<ImportPermit>> SearchImportPermit (string query, int pageNumber, int? pageSize = null) {

            ParameterExpression argument = Expression.Parameter (typeof (ImportPermit), "ip");
            Expression predicate = argument.StringContains ("ImportPermitNumber", query);

            Expression left = Expression.Property (argument, typeof (ImportPermit).GetProperty ("ImportPermitStatus"));
            left = Expression.Property (left, typeof (ImportPermitStatus).GetProperty ("ImportPermitStatusCode"));
            Expression right = Expression.Constant ("APR", typeof (string));

            Expression predicateStatus = Expression.Equal (left, right);

            predicate = Expression.AndAlso (predicate, predicateStatus);

            Expression<Func<ImportPermit, bool>> expression = predicate != null ? Expression.Lambda<Func<ImportPermit, bool>> (predicate, new [] { argument }) : null;
            OrderBy<ImportPermit> orderBy = new OrderBy<ImportPermit> (qry => qry.OrderBy (e => e.ImportPermitNumber));

            if (string.IsNullOrEmpty (query)) {
                predicate = null;
            }

            var totalRecords = await base.CountAsync (expression);
            var pageData = await this.GetPageAsync (pageNumber * (pageSize.HasValue ? (int) pageSize : PAGE_SIZE), pageSize.HasValue ? (int) pageSize : PAGE_SIZE, null, expression, orderBy.Expression);
            var response = new DataTablesResult<ImportPermit> (pageNumber, totalRecords, totalRecords, pageData);

            return response;
        }

        public async Task<bool> AssignImportPermitAsync (ImportPermit importPermit) {
            var savedImportPermit = await GetAsync (importPermit.ID);
            var result = await UpdateAsync (importPermit);

            //Notify to assigned cso 
            if (result && importPermit.AssignedUserID != null && savedImportPermit.AssignedUserID != importPermit.AssignedUserID) {
                var assignedUser = await _userService.GetAsync ((int) importPermit.AssignedUserID);
                var users = new List<User> () { assignedUser };
                var pushNotifier = _notificationFactory.GetNotification (NotificationType.PUSHNOTIFICATION);
                await pushNotifier.Notify (users, new { body = string.Format ("Import Permit with number of {0} has been assigned to you.", importPermit.ImportPermitNumber), subject = "Import Permit Assigned" }, "IPSC");
            }

            return result;
        }

        private Expression<Func<T, bool>> ConstructFilter<T> (int? userID, string roleCode, int? agentID, string search = null) {
            Expression<Func<T, bool>> expression = null;
            ParameterExpression argument = Expression.Parameter (typeof (T), "ip");
            Expression predicateBody = null;

            if (!string.IsNullOrEmpty (roleCode)) {
                if (roleCode == "IPA" || roleCode == "PIPA" || roleCode == "APCO") {
                    predicateBody = argument.GetExpression ("AgentID", agentID, "Equal", typeof (int));
                } else if (roleCode == "CSO") {
                    Expression e1 = argument.GetExpression ("AssignedUserID", userID, "Equal", typeof (int?));
                    Expression e2 = argument.DynamicContains ("ImportPermitStatusCode", new List<string> { "DRFT", "WITH" });
                    predicateBody = Expression.AndAlso (e1, Expression.Not (e2));
                } else if (roleCode == "PINS") {
                    predicateBody = argument.GetExpression ("ImportPermitStatusCode", "APR", "Equal", typeof (string));
                } else {
                    predicateBody = Expression.Not (argument.DynamicContains ("ImportPermitStatusCode", new List<string> { "DRFT", "WITH" }));
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

        private void Notify (string statusCode, string status, string ipermitNumber, IEnumerable<User> users) {
            var emailNotifier = _notificationFactory.GetNotification (NotificationType.EMAIL);
            var pushNotifier = _notificationFactory.GetNotification (NotificationType.PUSHNOTIFICATION);
            if (_emailContentDictionaryMap.ContainsKey (statusCode)) {
                var emailTuple = _emailContentDictionaryMap[statusCode];

                emailNotifier.Notify (users, new EmailSend (emailTuple.Item1, emailTuple.Item2, status, users.FirstOrDefault ().Username, ipermitNumber, "Order"), "APR");
                //push notification
                var body = String.Format (AlertTemplates.ImportPermitStatusChange, status, ipermitNumber, DateTime.Now);
                pushNotifier.Notify (users, new { body = body, subject = emailTuple.Item1 }, "IPSC");

            }
        }

        private async Task<DateTime?> GetExpiryDate (int supplierID, ICollection<ImportPermitDetail> ipDetail) {
            var productIDs = ipDetail.Select (ip => ip.ProductID);
            var supplierProducts = await _supplierProduct.FindByAsync (sp => sp.SupplierID == supplierID && sp.IsActive && productIDs.Contains (sp.ProductID) && sp.ExpiryDate != null && !(bool) sp.IsExpired);
            var expiryDate = supplierProducts.Select (s => s.ExpiryDate).Min ();

            var stg = await _systemSettingService.GetAsync (s => s.SystemSettingCode == "IPY");
            var value = Convert.ToInt32 (stg.Value);
            return expiryDate != null ? new DateTime (Math.Min (((DateTime) expiryDate).Ticks, (DateTime.UtcNow).AddYears (value).Ticks)) : (DateTime.UtcNow).AddYears (value);
        }

        #region private
        private void PopulateEmailContentDictionaryMap () {
            //iImport Status Code => Approved
            _emailContentDictionaryMap.Add ("APR", new Tuple<string, string> ("Import Permit Approved", "Your import permit has been approved."));
            //iImport Status Code => Rejected
            _emailContentDictionaryMap.Add ("REJ", new Tuple<string, string> ("Import Permit Rejected", "Your import permit has been rejected."));
            //iImport Status Code => Requested
            _emailContentDictionaryMap.Add ("RQST", new Tuple<string, string> ("Import Permit Submitted", "Your import permit has been submitted."));
            //iImport Status Code => Returned to Applicant
            _emailContentDictionaryMap.Add ("RTA", new Tuple<string, string> ("Import Permit Returned", "Your import permit has been returned, check the comment given."));

        }

        #endregion

    }
}