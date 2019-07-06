using Microsoft.AspNetCore.Mvc;
using PDX.Domain.Common;
using PDX.BLL.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using System.Threading.Tasks;
using System.Collections.Generic;
using PDX.Domain.Public;
using System;

namespace PDX.API.Controllers
{
    [Route("api/[controller]")]
    public class UtilityController 
    {
        private readonly IUtilityService _service;
        public UtilityController(IUtilityService service)
        {
            _service = service;
        }


        /// <summary>
        /// Get all Utilitys
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("Identifier/{key}")]
        public async Task<object> GenerateIdentifier(string key)
        {
            var result = await _service.GenerateIdentifier(key);
            return new { data = result };
        }
    }
}
