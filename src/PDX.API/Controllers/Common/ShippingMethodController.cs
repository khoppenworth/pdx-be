using Microsoft.AspNetCore.Mvc;
using PDX.Domain.Common;
using PDX.BLL.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;

namespace PDX.API.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    public class ShippingMethodController:CrudBaseController<ShippingMethod>
    {
        public ShippingMethodController(IService<ShippingMethod> shippingMehodService)
        :base(shippingMehodService)
        {
            
        }
    }
}