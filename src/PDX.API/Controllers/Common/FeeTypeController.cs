using Microsoft.AspNetCore.Mvc;
using PDX.Domain.Common;
using PDX.Domain.Finance;
using PDX.BLL.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;

namespace PDX.API.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    public class FeeTypeController:CrudBaseController<FeeType>
    {
        private readonly IService<FeeType> _feeTypeService;
        private readonly IService<SubmoduleFee> _submoduleFeeService;

        public FeeTypeController(IService<FeeType> feeTypeService, IService<SubmoduleFee> submoduleFeeService)
        :base(feeTypeService)
        {
            _feeTypeService = feeTypeService;
            _submoduleFeeService = submoduleFeeService;
        }

        /// <summary>
        /// Get fee type by its code
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("SubmoduleFee/ByCode/{code}")]
        public async Task<IEnumerable<SubmoduleFee>> GetSubmoduleFeeByCodeAsync(string code)
        {
            var SubmoduleFees = await _submoduleFeeService.FindByAsync(s => s.Submodule.SubmoduleCode == code);
            return SubmoduleFees;
        }
        
    }
}