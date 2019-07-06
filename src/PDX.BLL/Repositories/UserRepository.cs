using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using PDX.BLL.Helpers;
using PDX.DAL;
using PDX.DAL.Helpers;
using PDX.DAL.Repositories;
using PDX.Domain.Account;
using PDX.Logging;

namespace PDX.BLL.Repositories
{
    public class UserRepository : GenericRepository<User>, IUserRepository
    {
        public UserRepository(PDXContext dbContext, ILogger logger) : base(dbContext, logger)
        {

        }

        public async override Task UpdateAsync(User user)
        {
            try
            {
                
                var existingUser = await base.GetAsync(user.ID);

                //Check if there are Collections to be Removed
                var userAgentsToRemove = existingUser.UserAgents.Except(user.UserAgents, new IDComparer());
                var userRolesToRemove = existingUser.UserRoles.Except(user.UserRoles, new IDComparer());
                var userSubmodulesToRemove = existingUser.UserRoles.SelectMany(ur=>ur.UserSubmoduleTypes).Except(user.UserRoles.SelectMany(ur=>ur.UserSubmoduleTypes),new IDComparer());

                //Remove Collections
                RemoveCollection(userAgentsToRemove);
                RemoveCollection(userRolesToRemove);
                RemoveCollection(userSubmodulesToRemove);

                _dbSet.Update(user);
            }
            catch (Exception ex)
            {
                _logger.Log(ex);
            }

        }
    }
}