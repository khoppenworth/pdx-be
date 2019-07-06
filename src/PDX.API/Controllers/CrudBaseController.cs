using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using PDX.Domain;
using PDX.BLL.Services.Interfaces;
using PDX.API.Helpers;

namespace PDX.API.Controllers
{
    /// <summary>
    /// Implementation of CrudBaseController
    /// </summary>
    public class CrudBaseController<T>: BaseController<T> where T:BaseEntity
    {
        private readonly IService<T> _service;
        /// <summary>
        /// Constructor of CrudBaseController
        /// </summary>
        /// <param name="service"></param>
        public CrudBaseController(IService<T> service)
        :base(service)
        {
            _service = service;
        }

        /// <summary>
        /// create new entity
        /// </summary>
        /// <param name="T">The t.</param>
        /// <returns>bool</returns>
        [HttpPost]
        public virtual async Task<bool> CreateAsync([FromBody]T T)
        {
            var result = await _service.CreateAsync(T);
            return result;
        }


        /// <summary>
        /// update exising entity
        /// </summary>
        /// <param name="T">The t.</param>
        /// <returns>bool</returns>
        [HttpPut]
        public virtual async Task<bool> UpdateAsync([FromBody]T T)
        {
            var result = await _service.UpdateAsync(T, true, this.HttpContext.GetUserID());
            return result;
        }

        /// <summary>
        /// Deletes existing entity by id.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>bool</returns>
        [HttpDelete("{id}")]
        public virtual async Task<bool> DeleteAsync(int id)
        {
            var result = await _service.DeleteAsync(id);
            return result;
        }
    }
}