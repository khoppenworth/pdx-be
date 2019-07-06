using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using DataTables.AspNet.Core;
using PDX.BLL.Helpers;
using PDX.BLL.Model;
using PDX.BLL.Services.Interfaces;
using PDX.DAL.Query;
using PDX.DAL.Repositories;
using PDX.Domain.Commodity;
using PDX.Domain.Customer;
using PDX.Domain.Views;

namespace PDX.BLL.Services
{
    public class ManufacturerService : Service<Manufacturer>, IManufacturerService
    {
        private const int PAGE_SIZE = 50;
        private readonly IService<ProductManufacturer> _productManufacturer;
        private readonly IService<vwManufacturerAddress> _vwManufacturerAddressService;
        private readonly IService<ManufacturerAddress> _manufacturerAddressService;
        public ManufacturerService(IUnitOfWork unitOfWork, IService<ProductManufacturer> productManufacturerService,
        IService<vwManufacturerAddress> vwManufacturerAddressService, IService<ManufacturerAddress> manufacturerAddressService) : base(unitOfWork)
        {
            _productManufacturer = productManufacturerService;
            _vwManufacturerAddressService = vwManufacturerAddressService;
            _manufacturerAddressService = manufacturerAddressService;
        }

        public async Task<IEnumerable<Manufacturer>> GetProductManufacturer(int productID)
        {
            //only manufacturers with type of 'Finished Product Manufacturer' are considered as main manufacturer
            var productManufacturer = await _productManufacturer.FindByAsync(pm => pm.ProductID == productID && pm.IsActive && pm.ManufacturerType.ManufacturerTypeCode == "FIN_PROD_MANUF");
            var manufacturer = productManufacturer.Select(m => m.ManufacturerAddress.Manufacturer);
            return manufacturer;
        }

        public async Task<IEnumerable<ManufacturerAddress>> GetManufacturerAddressByProduct(int productID)
        {
            //only manufacturers with type of 'Finished Product Manufacturer' are considered as main manufacturer
            var productManufacturer = await _productManufacturer.FindByAsync(pm => pm.ProductID == productID && pm.IsActive && pm.ManufacturerType.ManufacturerTypeCode == "FIN_PROD_MANUF");
            var manufacturerAddresses = productManufacturer.Select(m => m.ManufacturerAddress);
            return manufacturerAddresses;
        }

        /// <summary>
        /// Search Manufacturer
        /// </summary>
        /// <param name="query"></param>
        /// <param name="pageNumber"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public async Task<DataTablesResult<Manufacturer>> SearchManufacturer(string query, int pageNumber, int? pageSize = null){

            Expression<Func<Manufacturer, bool>> predicate = DataTableHelper.ConstructFilter<Manufacturer>(query, new List<string>{"Name"});
            OrderBy<Manufacturer> orderBy = new OrderBy<Manufacturer>(qry => qry.OrderBy(e => e.Name));

            if(string.IsNullOrEmpty(query)){
                predicate = null;
            }
            
            var totalRecords = await base.CountAsync(predicate);
            var pageData = await this.GetPageAsync(pageNumber * (pageSize.HasValue ? (int) pageSize : PAGE_SIZE), pageSize.HasValue ? (int) pageSize : PAGE_SIZE, null, predicate, orderBy.Expression);
            var response = new DataTablesResult<Manufacturer>(pageNumber, totalRecords, totalRecords, pageData);

            return response;
        }

         /// <summary>
        /// Search Manufacturer Address
        /// </summary>
        /// <param name="query"></param>
        /// <param name="pageNumber"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public async Task<DataTablesResult<vwManufacturerAddress>> SearchManufacturerAddress(string query, int pageNumber, int? pageSize = null){

            Expression<Func<vwManufacturerAddress, bool>> predicate = DataTableHelper.ConstructFilter<vwManufacturerAddress>(query, new List<string>{"Name", "Country"});
            OrderBy<vwManufacturerAddress> orderBy = new OrderBy<vwManufacturerAddress>(qry => qry.OrderBy(e => e.Name));

            if(string.IsNullOrEmpty(query)){
                predicate = null;
            }
            
            var totalRecords = await _vwManufacturerAddressService.CountAsync(predicate);
            var pageData = await _vwManufacturerAddressService.GetPageAsync(pageNumber * (pageSize.HasValue ? (int) pageSize : PAGE_SIZE), pageSize.HasValue ? (int) pageSize : PAGE_SIZE, null, predicate, orderBy.Expression);
            
            var response = new DataTablesResult<vwManufacturerAddress>(pageNumber, totalRecords, totalRecords, pageData);

            return response;
        }
    }
}