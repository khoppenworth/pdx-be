using Microsoft.AspNetCore.Mvc;
using PDX.Domain.Common;
using PDX.BLL.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
namespace PDX.API.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    public class ShipmentStatusController:CrudBaseController<ShipmentStatus>
    {
        public ShipmentStatusController(IService<ShipmentStatus> shipmentStatusService)
        :base(shipmentStatusService)
        {
            
        }
    }
}