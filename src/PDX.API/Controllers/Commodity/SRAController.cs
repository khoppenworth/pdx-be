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
    public class SRAController : CrudBaseController<SRA>
    {
        private readonly IService<SRA> _sraService;
        public SRAController(IService<SRA> sraService)
        : base(sraService)
        {
            _sraService = sraService;
        }     

         /// <summary>
        /// Get all entities
        /// </summary>
        /// <returns>List of <typeparamref name="T"/></returns>
        [HttpGet]
        public override async Task<IEnumerable<SRA>> GetAllAsync()
        {
            var entities = await _sraService.GetAllAsync(true);
            return entities;
        } 

    }
}