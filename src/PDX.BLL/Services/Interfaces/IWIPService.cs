using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using PDX.Domain.Public;

namespace PDX.BLL.Services.Interfaces
{
    public interface IWIPService:IService<WIP>
    {
        Task<bool> WIPContentExistAsync<WContent>(IList<string> types, Func<WContent, bool> predicate) where WContent : class;
    }
}