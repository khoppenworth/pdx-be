using Microsoft.AspNetCore.Mvc;
using PDX.Domain.Common;
using PDX.BLL.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace PDX.API.Controllers
{

    [Authorize]
    [Route("api/[controller]")]
    public class ManufacturerTypeController : CrudBaseController<ManufacturerType>
    {
        private readonly IService<ManufacturerType> _manufacturerTypeService; 
        public ManufacturerTypeController(IService<ManufacturerType> manufacturerTypeService) 
        : base(manufacturerTypeService) 
        { 
            _manufacturerTypeService = manufacturerTypeService; 
        }  
    }
}