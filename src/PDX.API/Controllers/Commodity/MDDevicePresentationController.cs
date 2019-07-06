using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PDX.BLL.Services.Interfaces;
using PDX.Domain.Commodity;
using System.Linq;

namespace PDX.API.Controllers.Commodity {
    [Authorize]
    [Route ("api/[controller]")]
    public class MDDevicePresentationController : BaseController<MDDevicePresentation> {
        IService<MDDevicePresentation> _service;
        public MDDevicePresentationController (IService<MDDevicePresentation> service) : base (service) {
            _service = service;
        }

        /// <summary>
        /// Search product
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        [Route ("ProductDevicePresentation/{mdDevicePresentationID}")]
        [HttpGet]
        public async Task<IEnumerable<MDDevicePresentation>> GetProductDevicePresentation (int mdDevicePresentationID) {
            var selected = await _service.GetAsync (mdDevicePresentationID);
            var result = await _service.FindByAsync (p => p.MDModelSize.ProductID == selected.MDModelSize.ProductID);
            return result?.ToList().Where (md => md.Model == selected.Model && md.Size == selected.Size);
        }
    }
}