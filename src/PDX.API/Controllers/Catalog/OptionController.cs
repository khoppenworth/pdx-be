using Microsoft.AspNetCore.Mvc;
using PDX.BLL.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using System.Threading.Tasks;
using System.Collections.Generic;
using PDX.Domain.Catalog;

namespace PDX.API.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    public class OptionController:BaseController<Option>
    {
        private readonly IService<Option> _optionService;
        public OptionController(IService<Option> optionService)
        :base(optionService)
        {
            _optionService = optionService;
        }
 
    }
}