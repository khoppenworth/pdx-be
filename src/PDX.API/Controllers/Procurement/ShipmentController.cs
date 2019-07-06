using System.Threading.Tasks;
using DataTables.AspNet.Core;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PDX.API.Helpers;
using PDX.BLL.Model;
using PDX.BLL.Services.Interfaces;
using PDX.Domain.Procurement;
using PDX.Domain.Views;

namespace PDX.API.Controllers.Procurement
{
    [Authorize]
    [Route("api/[controller]")]
    public class ShipmentController : BaseController<Shipment>
    {
        private readonly IShipmentService _shipmentService;

        public ShipmentController(IShipmentService shipmentService):base(shipmentService)
        {
            _shipmentService = shipmentService;
        }

        /// <summary>
        /// Create Shipment
        /// </summary>
        /// <param name="shipment"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ApiResponse> CreateShipmentAsync([FromBody] ShipmentBusinessModel shipment)
        {
            var result = await _shipmentService.CreateShipmentAsync(shipment);
            return result;
        }

        /// <summary>
        /// get list of shipments
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [Route("List")]
        public async Task<DataTablesResult<vwShipment>> GetShipmentPage([FromBody]IDataTablesRequest request)
        {
            var result = await _shipmentService.GetShipmentPageAsync(request, this.HttpContext.GetUserID());
            return result;
        }

         /// <summary>
        /// Get single import permit by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("Single/{id}")]
        public async Task<ShipmentBusinessModel> GetShipmentBusinessModel(int id)
        {
            var result = await _shipmentService.GetShipmentBusinessModel(id);
            return result;
        }
    }
}