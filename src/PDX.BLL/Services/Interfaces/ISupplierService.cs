using System.Collections.Generic;
using System.Threading.Tasks;
using PDX.Domain.Customer;
using PDX.Domain.Views;
using PDX.BLL.Model;
using DataTables.AspNet.Core; 


namespace PDX.BLL.Services.Interfaces
{
    public interface ISupplierService:IService<Supplier>
    {
        Task<IEnumerable<Supplier>> GetSuppliersByAgent(int agentID, string agentLevelCode = null);
        Task<IEnumerable<Supplier>> GetSuppliersByAgent(int agentID, string productTypeCode = null, string agentLevelCode = null);

        /// <summary>
        /// Suppliers page
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<DataTablesResult<vwSupplier>> GetSupplierPage(IDataTablesRequest request);
        
    }
}