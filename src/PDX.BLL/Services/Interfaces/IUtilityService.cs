using System;
using System.Threading.Tasks;
using PDX.BLL.Model;

namespace PDX.BLL.Services.Interfaces
{
    public interface IUtilityService
    {      
        /// <summary>
        /// Generate unique identifier
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        Task<Guid> GenerateIdentifier(string key);
    }
}