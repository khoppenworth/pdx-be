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
    public class AnswerTypeController:CrudBaseController<AnswerType>
    {
        private readonly IService<AnswerType> _answerTypeService;
        public AnswerTypeController(IService<AnswerType> answerTypeService)
        :base(answerTypeService)
        {
            _answerTypeService = answerTypeService;
        }
 
    }
}