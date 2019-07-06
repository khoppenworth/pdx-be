using System.Collections.Generic;
using System.Threading.Tasks;
using DataTables.AspNet.Core;
using PDX.BLL.Model;
using PDX.Domain.Commodity;
using PDX.Domain.Customer;
using PDX.Domain.Views;

namespace PDX.BLL.Services.Interfaces {
    public interface IProductService : IService<Product> {
        /// <summary>
        /// Get Agent product
        /// </summary>
        /// <param name="agentID"></param>
        /// <returns></returns>
        Task<List<Product>> GetProductByAgent (int agentID);
        Task<ApiResponse> CreateProduct(ProductBusinessModel product);
        Task<IEnumerable<AgentSupplier>> GetAgentSupplierByProduct (int productID);
        Task<List<Domain.Document.Document>> GetProductDocuments (int productID);
        /// <summary>
        /// Get Supplier product
        /// </summary>
        /// <param name="supplierID"></param>
        /// <returns></returns>
        Task<List<Product>> GetSupplierProduct (int supplierID);
        Task<List<Product>> GetSupplierProductForIPermit (int supplierID, string productTypeCode = null);
        Task<IEnumerable<MDModelSize>> GetMDSupplierProductForIPermit (int supplierID, string productTypeCode = null);

        Task<Product> GetProductByMAAsync (int maID);
        Task<IEnumerable<MDModelSize>> GetProductsByMAAsync (int maID);

        Task<List<vwProduct>> SearchProduct(string query);
        Task<List<vwAllProduct>> SearchAllProduct (string query, string productTypeCode, string importPermitTypeCode, int? supplierID=null);

        /// <summary>
        /// Product page
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<DataTablesResult<vwProduct>> GetProductPage (IDataTablesRequest request, string submoduleTypeCode = null);
        Task<DataTablesResult<vwProduct>> GetProductByUserPage (IDataTablesRequest request, int userID, string submoduleTypeCode=null);

    }
}