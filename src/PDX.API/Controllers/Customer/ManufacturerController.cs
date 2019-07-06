using Microsoft.AspNetCore.Mvc;
using PDX.Domain.Customer;
using PDX.Domain.Views;
using PDX.BLL.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Collections;

namespace PDX.API.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    public class ManufacturerController : CrudBaseController<Manufacturer>
    {
        private readonly IManufacturerService _manufacturerService;
        public ManufacturerController(IManufacturerService service) : base(service)
        {
            _manufacturerService = service;
        }

         /// <summary>
        /// Get  manufacturer by product
        /// </summary>
        /// /// <param name="productID"></param>
        /// <returns></returns>
        [Route("ByProduct/{productID}")]
        [HttpGet]
         public async Task<IEnumerable<Manufacturer>> GetProductManufacturer(int productID)
        {
            var result = await _manufacturerService.GetProductManufacturer(productID);
            return result;
        }

        /// <summary>
        /// Search manufacturer
        /// </summary>
        /// <param name="query"></param>
        /// <param name="pageNumber"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        [Route("Search/{query}/{pageNumber}/{pageSize}")]
        [HttpPost]
        public async Task<IEnumerable<Manufacturer>> SearchManufacturer(string query, int pageNumber, int? pageSize = null)
        {
            var result = await _manufacturerService.SearchManufacturer(query,pageNumber,pageSize);
            return (IEnumerable<Manufacturer>)result.Data;
        }

         /// <summary>
        /// Get  manufacturer address by product
        /// </summary>
        /// /// <param name="productID"></param>
        /// <returns></returns>
        [Route("ManufacturerAddress/ByProduct/{productID}")]
        [HttpGet]
         public async Task<IEnumerable<ManufacturerAddress>> GetManufacturerAddress(int productID)
        {
            var result = await _manufacturerService.GetManufacturerAddressByProduct(productID);
            return result;
        }

        /// <summary>
        /// Search manufacturer address
        /// </summary>
        /// <param name="query"></param>
        /// <param name="pageNumber"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        [Route("ManufacturerAddress/Search/{query}/{pageNumber}/{pageSize}")]
        [HttpPost]
        public async Task<IEnumerable<vwManufacturerAddress>> SearchManufacturerAddress(string query, int pageNumber, int? pageSize = null)
        {
            var result = await _manufacturerService.SearchManufacturerAddress(query,pageNumber,pageSize);
            return (IEnumerable<vwManufacturerAddress>)result.Data;
        }
    }
}