using Microsoft.AspNetCore.Mvc;
using PDX.Domain.Commodity;
using PDX.BLL.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace PDX.API.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    public class AdminRouteController : CrudBaseController<AdminRoute>
    {
        private readonly IService<AdminRoute> _adminRouteService;
        public AdminRouteController(IService<AdminRoute> adminRouteService)
        : base(adminRouteService)
        {
            _adminRouteService = adminRouteService;
        }     

    }
}