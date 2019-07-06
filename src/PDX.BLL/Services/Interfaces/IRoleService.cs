using PDX.Domain.Account;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using PDX.Domain;

namespace PDX.BLL.Services.Interfaces
{
    public interface IRoleService:IService<Role>
    {         
        Task<IEnumerable<Role>> GetRolesByUserAsync(int userID);
    }
}