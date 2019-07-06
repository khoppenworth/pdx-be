using System.Collections.Generic;
using System.Threading.Tasks;
using DataTables.AspNet.Core;
using PDX.BLL.Model;
using PDX.Domain.Common;
using PDX.Domain.License;
using PDX.Domain.Views;

namespace PDX.BLL.Services.Interfaces {
    public interface IMAService : IService<MA> {
        Task<ApiResponse> CreateMANewApplicationAsync (MABusinessModel maNewApp);
        Task<ApiResponse> UpdateMAAsync (MABusinessModel maNewApp);
        Task<ApiResponse> CreateMARenewalAsync (MABusinessModel maRenewal);
        Task<ApiResponse> CreateMAVariationAsync (MABusinessModel maVariation);
        Task<ApiResponse> GetMABusinessModel (int id);
        Task<MABusinessModel> GetMABusinessModel (MA ma);
        Task<MABusinessModel> GetMA (int id);
        Task<MAStatus> GetMAStatus (int id);
        Task<DataTablesResult<vwMA>> GetMAPageAsync (IDataTablesRequest request, int? userID = null, string submoduleTypeCode=null);
        Task<IEnumerable<MA>> GetMAByUserAsync (int userID);
        Task<IEnumerable<MABusinessModel>> GetMAForRenewalByUserAsync (int userID);
        Task<IEnumerable<MABusinessModel>> GetMAForVariationByUserAsync (int userID);
        Task<bool> ChangeMAStatusAsync (MAStatusLogModel maStatusLog);
        Task<bool> InsertOrUpdateMAChecklistAsync (IList<MAChecklist> maChecklist, bool commit = true);
        Task<ApiResponse> ApproveMAAsync (MA ma);
        Task<bool> DeleteMAAsync (MAStatusLogModel maStatusLog);
        Task<bool> GeneratePremarketLabRequestAsync (int maID);
        Task<bool> RollbackMA (int maID);
        Task<MAReview> GetMAReview (int maID, string responderTypeCode, string suggestedStatusCode);
        Task<IEnumerable<Difference>> GetVariationChangesAsync (int maID);
        Task<bool> PopulateDraftMAToWIP ();
    }
}