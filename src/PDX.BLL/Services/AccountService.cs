using System.Threading.Tasks;
using System.Collections.Generic;
using PDX.BLL.Services.Interfaces;
using PDX.Domain.Account;
using PDX.BLL.Helpers;
using PDX.BLL.Model;
using System;

namespace PDX.BLL.Services
{
    public class AccountService : IAccountService
    {
        private readonly IUserService _userService;
        private readonly IMenuService _menuService;
        private readonly IPermissionService _permissionService;
        private readonly PDX.Logging.ILogger _logger;

        public AccountService(IUserService userService, IMenuService menuService, IPermissionService permissionService,
        PDX.Logging.ILogger logger)
        {
            _userService = userService;
            _menuService = menuService;
            _permissionService = permissionService;
            _logger = logger;
        }
        /// <summary>
        /// Change exiting password of a user by a new one
        /// </summary>
        /// <param name="userID">The user identifier.</param>
        /// <param name="oldPassword">old password.</param>
        /// <param name="password">The password.</param>
        /// <returns>bool</returns>
        public async Task<bool> ChangePasswordAsync(int userID, string oldPassword, string password)
        {
            var user = await _userService.GetAsync(userID);
            if (user != null && (await AuthenticateUserAsync(user.Username, oldPassword)))
            {
                string hashedPassword = HashHelper.HashPassword(password);
                user.Password = hashedPassword;
                return await _userService.UpdateAsync(user);
            }
            return false;
        }

        /// <summary>
        /// Change exiting password of a user by a new one
        /// </summary>
        /// <param name="userID">The user identifier.</param>
        /// <param name="password">The password.</param>
        /// <returns>bool</returns>
        public async Task<bool> ChangePasswordAsync(int userID, string password)
        {
            var user = await _userService.GetAsync(userID);
            if (user != null)
            {
                string hashedPassword = HashHelper.HashPassword(password);
                user.Password = hashedPassword;
                return await _userService.UpdateAsync(user);
            }
            return false;
        }

        /// <summary>
        /// Authenticate user
        /// </summary>
        /// <param name="username"></param>
        /// <param name="password"></param>
        /// <returns>bool</returns>

        public async Task<bool> AuthenticateUserAsync(string username, string password)
        {
            try
            {
                var user = await _userService.GetUserByUsername(username);
                var result = user != null && HashHelper.ValidatePassword(password, user.Password);
                return result;
            }
            catch (Exception ex)
            {
                _logger.Log(ex);
            }
            return false;
        }

        /// <summary>
        /// Reset exiting password 
        /// </summary>
        /// <param name="username"></param>
        public async Task<string> ResetPasswordAsync(string username)
        {
            try
            {
                var user = await _userService.GetUserByUsername(username);
                if (user != null)
                {
                    var password = HashHelper.GenerateRandomPassword(8);
                    string hashedPassword = HashHelper.HashPassword(password);
                    user.Password = hashedPassword;
                    var result = await _userService.UpdateAsync(user);
                    return result ? password : null;
                }
                return null;
            }
            catch (Exception ex)
            {
                _logger.Log(ex);
            }
            return null;
        }
    }
}