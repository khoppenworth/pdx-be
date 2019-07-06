using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using PDX.Domain;
using PDX.BLL.Services.Interfaces;
using PDX.BLL.Model;

namespace PDX.API.Controllers
{
    /// <summary>
    /// Implementation of BaseController
    /// </summary> 
    /// <typeparam name="T"></typeparam>
    public class BaseController<T>: Controller where T:BaseEntity
    {
        private readonly IService<T> _service;
        /// <summary>
        /// Constructor of BaseController
        /// </summary>
        /// <param name="service"></param>
        public BaseController(IService<T> service)
        {
            _service = service;
        }
        /// <summary>
        /// Get all entities
        /// </summary>
        /// <returns>List of <typeparamref name="T"/></returns>
        [HttpGet]
        public virtual async Task<IEnumerable<T>> GetAllAsync()
        {
            var entities = await _service.GetAllAsync();
            return entities;
        }

        /// <summary>
        /// Get single entity by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns><typeparamref name="T"/></returns>
        [HttpGet("{id}")]
        public virtual async Task<T> GetAsync(int id)
        {
            var entity = await _service.GetAsync(id);
            return entity;
        }

        /// <summary>
        /// Search entity
        /// </summary>
        /// <param name="searchRequest">The search request</param>
        /// <returns>bool</returns>
        [HttpPost]
        [Route("Search")]
        public virtual async Task<DataTablesResult<T>> SearchAsync([FromBody]SearchRequest searchRequest)
        {
            var result = await _service.SearchAsync(searchRequest);
            return result;
        }

    }
}