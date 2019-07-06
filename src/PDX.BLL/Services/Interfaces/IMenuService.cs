using PDX.Domain.Account;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using PDX.Domain;

namespace PDX.BLL.Services.Interfaces
{
    public interface IMenuService:IService<Menu>
    {         
        /// <summary>
        /// Gets user menu 
        /// </summary>
        /// <param name="userID"></param>
        /// <returns></returns>
       Task<IEnumerable<Menu>> GetMenusByUserAsync(int userID);

       /// <summary>
       /// save menu roles
       /// </summary>
       /// <param name="menuRoles"></param>
       /// <returns></returns>
       Task<bool> SaveMenuRolesAsync(IList<MenuRole> menuRoles);
    }
}