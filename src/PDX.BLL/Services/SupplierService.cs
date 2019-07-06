using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Linq.Expressions;
using DataTables.AspNet.Core;

using PDX.BLL.Services.Interfaces;
using PDX.DAL.Repositories;
using PDX.Domain.Commodity;
using PDX.Domain.Customer;
using PDX.Domain.Views;
using PDX.BLL.Model;
using PDX.BLL.Helpers;
using PDX.DAL.Query;
using PDX.Domain.Procurement;
using PDX.Domain.Common;

namespace PDX.BLL.Services
{
    public class SupplierService : Service<Supplier>, ISupplierService
    {
        private readonly IService<Address> _addressService;
        private readonly IService<AgentSupplier> _agentSupplierService;
        private readonly IService<vwSupplier> _vwSupplierService;
        private readonly IService<ImportPermit> _importPermitService;
        private readonly IStatusLogService _statusLogService;
        private readonly IService<SupplierProduct> _supplierProductService;
        private List<string> columns = new List<string> {
                "Email",
                "Name",
                "CountryName"
        };
        public SupplierService(IUnitOfWork unitOfWork, IService<Address> addressService, IService<AgentSupplier> agentSupplierService, IService<vwSupplier> vwSupplierService, IService<ImportPermit> importPermitService, IStatusLogService statusLogService, 
        IService<SupplierProduct> supplierProductService) : base(unitOfWork)
        {
            _addressService = addressService;
            _agentSupplierService = agentSupplierService;
            _vwSupplierService = vwSupplierService;
            _importPermitService = importPermitService;
            _statusLogService = statusLogService;
            _supplierProductService = supplierProductService;
        }

        public override async Task<bool> UpdateAsync(Supplier supplier, bool commit = true, int? modifiedBy = null)
        {
            var oldSupplier = await this.GetAsync(supplier.ID);
            await _statusLogService.LogStatusAsync(oldSupplier, supplier, nameof(supplier.IsActive), supplier.Remark, (int)modifiedBy);
            //Update Address Information First
            var addressSaved = await _addressService.UpdateAsync(supplier.Address);
            var result = addressSaved? await base.UpdateAsync(supplier, commit): false;
            return result;
        }

        public async Task<IEnumerable<Supplier>> GetSuppliersByAgent(int agentID,string agentLevelCode = null)
        {
            var agentSupplier = await _agentSupplierService.FindByAsync(pm => pm.AgentID == agentID && pm.IsActive && pm.Supplier.IsActive
            && (agentLevelCode == null || (agentLevelCode != null && pm.AgentLevel.AgentLevelCode == agentLevelCode)));
            var suppliers = agentSupplier.Select(m => m.Supplier);
            return suppliers;
        }

        public async Task<IEnumerable<Supplier>> GetSuppliersByAgent(int agentID, string productTypeCode = null, string agentLevelCode = null){
            var suppliers = await this.GetSuppliersByAgent(agentID, agentLevelCode);
            var supplierProducts = await _supplierProductService.FindByAsync(sp => (productTypeCode == null || (productTypeCode != null && sp.Product.ProductType.ProductTypeCode == productTypeCode))
                                    && (suppliers.Select(s => s.ID)).Contains(sp.SupplierID));
            return supplierProducts.Select(s => s.Supplier).Distinct();
        }

        /// <summary>
        /// Suppliers page
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<DataTablesResult<vwSupplier>> GetSupplierPage(IDataTablesRequest request)
        {
            Expression<Func<vwSupplier, bool>> predicate = DataTableHelper.ConstructFilter<vwSupplier>(request.Search.Value, columns);
            OrderBy<vwSupplier> orderBy = new OrderBy<vwSupplier>(qry => qry.OrderBy(e => e.Name));
            var response = await _vwSupplierService.GetPageAsync(request, predicate, orderBy.Expression);
            return response;
        }


    }
}