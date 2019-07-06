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
    public class ChecklistTypeController:CrudBaseController<ChecklistType>
    {
        private readonly IService<ChecklistType> _checklistTypeService;
        public ChecklistTypeController(IService<ChecklistType> checklistTypeService)
        :base(checklistTypeService)
        {
            _checklistTypeService = checklistTypeService;
        }
 
    }
}