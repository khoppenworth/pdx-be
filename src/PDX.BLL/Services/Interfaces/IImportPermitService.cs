using System.Collections.Generic;
using System.Threading.Tasks;
using DataTables.AspNet.AspNetCore;
using DataTables.AspNet.Core;
using Microsoft.AspNetCore.Http;
using PDX.BLL.Model;
using PDX.Domain.Procurement;
using PDX.Domain.Views;

namespace PDX.BLL.Services.Interfaces
{
    public interface IImportPermitService:IService<PDX.Domain.Procurement.ImportPermit>
    {
        Task<ImportPermitBusinessModel> GetImportPermitBusinessModel(int id);
        Task<bool> CreateImportPermitAsync(ImportPermitBusinessModel ipermit);
        Task<ApiResponse> CreateAutomaticImportPermitAsync(ImportPermitBusinessModel ipermit, int createdBy);
        Task<bool> UpdateImportPermitAsync(ImportPermitBusinessModel ipermit);
        Task<List<ImportPermit>> GetAgentImportPermits(int agentID);
        Task<List<ImportPermit>> GetImportPermitsByUser(int userID, string importPermitTypeCode);
        Task<DataTablesResult<vwImportPermit>> GetImportPermitPage(IDataTablesRequest request, int? userID = null);
        Task<DataTablesResult<vwPIP>> GetPIPPage(IDataTablesRequest request, int? userID = null);
        Task<bool> ChangeIPermitStatus(ImportStatusLog ipermit,ImportPermitStatuses status);
        Task<bool> CheckDuplicateInvoice(int supplierID, int agentID, string performaInvoice, int? importPermitID = null);
        Task<DataTablesResult<ImportPermit>> SearchImportPermit(string query, int pageNumber, int? pageSize = null);
        Task<bool> AssignImportPermitAsync(ImportPermit importPermit);

    }
}