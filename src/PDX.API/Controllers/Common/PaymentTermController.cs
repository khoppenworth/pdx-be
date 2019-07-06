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
    public class PaymentTermController:CrudBaseController<PaymentTerm>
    {
        private readonly IService<PaymentTerm> _paymentTermService;
        public PaymentTermController(IService<PaymentTerm> paymentTermService)
        :base(paymentTermService)
        {
            _paymentTermService = paymentTermService;
        }

        /// <summary>
        /// Get all entities
        /// </summary>
        [HttpGet]
        public override async Task<IEnumerable<PaymentTerm>> GetAllAsync()
        {
            var entities = await _paymentTermService.GetAllAsync(true);
            return entities;
        } 
    }
}