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
    public class PharmacologicalClassificationController : CrudBaseController<PharmacologicalClassification>
    {
        private readonly IService<PharmacologicalClassification> _pharmaClassificationService;
        public PharmacologicalClassificationController(IService<PharmacologicalClassification> pharmaClassificationService)
        : base(pharmaClassificationService)
        {
            _pharmaClassificationService = pharmaClassificationService;
        }     

    }
}