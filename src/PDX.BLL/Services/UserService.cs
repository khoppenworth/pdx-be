using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using DataTables.AspNet.Core;
using PDX.BLL.Helpers;
using PDX.BLL.Model;
using PDX.BLL.Repositories;
using PDX.BLL.Services.Interfaces;
using PDX.BLL.Services.Interfaces.Email;
using PDX.BLL.Services.Notification;
using PDX.DAL.Repositories;
using PDX.Domain.Account;
using PDX.Domain.Views;

namespace PDX.BLL.Services {
    public class UserService : Service<User>, IUserService {
        private readonly IService<UserRole> _userRoleService;
        private readonly IService<UserAgent> _userAgentService;
         private readonly IService<UserSubmoduleType> _userSubmoduleTypeService;
        private readonly IService<vwUser> _vwUserService;
        private readonly IEmailSender _emailSender;
        private readonly NotificationFactory _notificationFacory;
        private List<string> columns = new List<string> {
            "Email",
            "FirstName",
            "LastName",
            "Username",
            "UserTypeName",
            "Status"
        };

        public UserService (IUnitOfWork unitOfWork, IUserRepository userRepository,
            IService<UserRole> userRoleService,
            IService<UserAgent> userAgentService, IService<vwUser> vwUserService,
            IEmailSender emailSender, NotificationFactory notificationFacory,IService<UserSubmoduleType> userSubmoduleTypeService) : base (unitOfWork, userRepository) {
            _userRoleService = userRoleService;
            _userAgentService = userAgentService;
            _vwUserService = vwUserService;
            _emailSender = emailSender;
            _notificationFacory = notificationFacory;
            _userSubmoduleTypeService = userSubmoduleTypeService;
        }

        /// <summary>
        /// Get single user with its roles
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async override Task<User> GetAsync (int id, bool cacheRemove = false) {
            var user = await base.GetAsync (id, cacheRemove);
            var userRoles = await _userRoleService.FindByAsync (ur => ur.UserID == user.ID);
            userRoles.ToList ().ForEach (ur => {
                ur.Role.SubmoduleTypes = ur.UserSubmoduleTypes?.Select (st => st.SubmoduleType.SubmoduleTypeCode)?.ToList ();
            });
            user.Roles = userRoles.Select (ur => ur.Role).ToList ();
            return user;
        }

        /// <summary>
        /// Get single user with its roles
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        public async override Task<User> GetAsync (Expression<Func<User, bool>> predicate, bool cacheRemove = false) {
            var user = await base.GetAsync (predicate, cacheRemove);
            if (user != null) {
                var userRoles = await _userRoleService.FindByAsync (ur => ur.UserID == user.ID);
                userRoles.ToList ().ForEach (ur => {
                    ur.Role.SubmoduleTypes = ur.UserSubmoduleTypes?.Select (st => st.SubmoduleType.SubmoduleTypeCode)?.ToList ();
                });
                user.Roles = userRoles.Select (ur => ur.Role).ToList ();
            }
            return user;
        }

        /// <summary>
        /// Get Users by Username
        /// </summary>
        /// <param name="username"></param>
        /// <returns></returns>
        public async Task<User> GetUserByUsername (string username) {
            //this call will use the above overriden GetAsync method
            var user = await this.GetAsync (u => u.Username.ToLower () == username.ToLower () && u.IsActive);
            return user;
        }

        /// <summary>
        /// Create User 
        /// </summary>
        /// <param name="user"></param>
        /// <param name="commit"></param>
        /// <param name="createdBy"></param>
        /// <returns></returns>
        public async override Task<bool> CreateAsync (User user, bool commit = true, int? createdBy = null) {
            if (string.IsNullOrEmpty (user.Password)) {
                user.Password = HashHelper.GenerateRandomPassword (8);
            }
            string hashedPassword = HashHelper.HashPassword (user.Password);
            var tempPassword = user.Password;
            user.Password = hashedPassword;

            var result = await base.CreateAsync (user, commit, createdBy);
            if (result) {
                var emailSendObject = new EmailSend ("Your iImport account has been created", tempPassword, "", user.Username, "", "iImport");
                var notifier = _notificationFacory.GetNotification (NotificationType.EMAIL);
                await notifier.Notify (new List<User> () { user }, emailSendObject, "IPAC");
            }
            return result;

        }

        /// <summary>
        /// update existing user
        /// </summary>
        /// <param name="entity"></param>        
        /// <param name="commit"></param>
        /// <param name="ID"></param>
        /// <returns>bool</returns>
        public override async Task<bool> UpdateAsync (User entity, bool commit = true, int? modifiedBy = null) {
            var existingUser = await GetAsync (entity.ID);
            if (existingUser != null) {
                var userAgents = existingUser.UserAgents;
                existingUser.CopyProperties (entity);
                existingUser.UserAgents = userAgents;
                bool result =  await base.UpdateAsync (existingUser, commit);
                return result;
            }
            return false;
        }

        /// <summary>
        /// Delete existing user
        /// </summary>
        /// <param name="ID"></param>
        /// <param name="commit"></param>
        /// <returns>bool</returns>
        public async override Task<bool> DeleteAsync (int ID, bool commit = true) {
            var user = await GetAsync (ID);
            if (user != null) {
                user.IsActive = false;
                return await base.UpdateAsync (user, commit);
            }
            return false;
        }

        /// <summary>
        /// Get Users by Role
        /// </summary>
        /// <param name="roleID"></param>
        /// <returns></returns>
        public async Task<List<User>> GetUsersByRole (int roleID, int userId) {
            
            var user = await this.GetAsync (us => us.ID == userId);
            var fRole = user.Roles.FirstOrDefault ();
            var isFood  = fRole.SubmoduleTypes.Contains("FD");
            var userRoles = await _userRoleService.FindByAsync (ur=>isFood ? ur.RoleID == roleID || ur.Role.RoleCode == "ROLE_FOOD_REVIEWER" : ur.RoleID == roleID);
            var users = userRoles.Select (ur => ur.User).ToList ();
            return users;
        }

        /// <summary>
        /// Get Users by Agent
        /// </summary>
        /// <param name="agentID"></param>
        /// <returns></returns>
        public async Task<List<User>> GetUsersByAgent (int agentID) {
            var userAgents = await _userAgentService.FindByAsync (ur => ur.AgentID == agentID);
            var users = userAgents.Select (ur => ur.User).ToList ();
            return users;
        }

        /// <summary>
        /// Get Users role of agent but not linked with any agent 
        /// </summary>
        /// <returns></returns>
        public async Task<List<User>> GetAgentUsers () {
            var userIDs = (await _userAgentService.FindByAsync (ur => ur.IsActive)).Select (u => u.UserID).ToList ();
            var userRoles = await _userRoleService.FindByAsync (ur => (new List<string> { "IPA", "PIPA", "APCO" }).Contains (ur.Role.RoleCode));
            userRoles = userRoles.Where (ur => !userIDs.Contains (ur.UserID));
            var users = userRoles.Select (ur => ur.User).ToList ();
            return users;
        }

        /// <summary>
        /// Users page
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<DataTablesResult<vwUser>> GetUserPage (IDataTablesRequest request) {
            Expression<Func<vwUser, bool>> predicate = DataTableHelper.ConstructFilter<vwUser> (request.Search.Value, columns);

            var response = await _vwUserService.GetPageAsync (request, predicate, null);
            return response;
        }

    }
}