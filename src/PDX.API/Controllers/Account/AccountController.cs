using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using PDX.BLL.Services.Interfaces;
using PDX.API.Model;
using PDX.API.Middlewares.Token;
using Microsoft.Extensions.Options;
using System.Linq;
using Microsoft.AspNetCore.Authorization;
using PDX.BLL.Services.Interfaces.Email;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using PDX.Domain.Account;
using PDX.DAL.Query;
using System;
using PDX.API.Model.Account;
using PDX.BLL.Services.Notification;

namespace PDX.API.Controllers
{
    /// <summary>
    /// Account Controller
    /// </summary>
    [Authorize]
    [Route("api/[controller]")]
    public class AccountController : Controller
    {
        private readonly IAccountService _service;
        private readonly IUserService _userService;
        private readonly IPermissionService _permissionService;
        private readonly TokenProvider _tokenProvider;
        private readonly IEmailSender _emailSender;
        private readonly IService<UserLogin> _userLoginService;
        private readonly PDX.Logging.ILogger _logger;
        private readonly NotificationFactory _notificationFactory;


        /// <summary>
        /// Constructor for Account controller
        /// </summary>
        /// <param name="accountService"></param>
        /// <param name="permissionService"></param>
        /// <param name="userService"></param>
        /// <param name="options"></param>
        public AccountController(IAccountService accountService, IUserService userService, IPermissionService permissionService, IOptions<TokenProviderOptions> options, IEmailSender emailSender, IService<UserLogin> userLoginService, PDX.Logging.ILogger logger,NotificationFactory notificationFacory)
        {
            _service = accountService;
            _userService = userService;
            _permissionService = permissionService;
            _tokenProvider = new TokenProvider(options, accountService, userService);
            _emailSender = emailSender;
            _userLoginService = userLoginService;
            _logger = logger;
            _notificationFactory = notificationFacory;
        }

        /// <summary>
        /// Authenticate user 
        /// </summary>
        /// <param name="login"></param>
        /// <returns></returns>
        [AllowAnonymous]
        [HttpPost]
        [Route("Authenticate")]
        public virtual async Task<object> AuthenticateUserAsync([FromBody]Login login)
        {
            if (ModelState.IsValid)
            {
                var result = await _service.AuthenticateUserAsync(login.UserName, login.Password);
                if (result)
                {
                    var token = await _tokenProvider.GenerateToken(login.UserName, login.Password);
                    var user = await _userService.GetUserByUsername(login.UserName);
                    var permissions = await _permissionService.GetPermissionByRolesAsync(user.Roles.Select(r => r.ID));
                    var fRole = user.Roles.OrderBy(r => r.CreatedDate).FirstOrDefault();
                    var tResult = new
                    {
                        User = new
                        {
                            ID = user.ID,
                            FullName = user.FirstName + ' ' + user.LastName,
                            Username = user.Username,
                            Email = user.Email,
                            RowGuid = user.RowGuid,
                            RoleID = fRole.ID,
                            Role = fRole.Name,
                            RoleCode = fRole.RoleCode,
                            Roles = user.Roles,
                            //Agent Rule: if user has single role(Agent)
                            IsAgent = (new List<string> { "IPA", "PIPA" }).Contains(fRole.RoleCode)
                        },
                        Token = token,
                        Permissions = permissions?.Select(p => p?.Name).Distinct()
                    };

                    //last login
                    user.LastLogin = System.DateTime.UtcNow;
                    await _userService.UpdateAsync(user);

                    //log UserLogin
                    if (login.ClientInfo != null)
                    {
                        try
                        {
                            login.ClientInfo.UserID = user.ID;
                            await _userLoginService.CreateAsync(login.ClientInfo);
                        }
                        catch (Exception ex)
                        {
                            _logger.Log(PDX.Logging.Models.LogType.Warning, ex);
                        }
                    }

                    return tResult;
                }
            }
            return null;
        }

        /// <summary>
        /// Logout
        /// </summary>
        /// <param name="logout"></param>
        /// <returns></returns>
        [AllowAnonymous]
        [HttpPost]
        [Route("Logout")]
        public virtual async Task<bool> LogoutAsync([FromBody]Logout logout)
        {
            var lastUserLogin = (await _userLoginService.FindByAsync(ul => ul.UserID == logout.UserID,
                        (new OrderBy<UserLogin>(qry => qry.OrderByDescending(e => e.CreatedDate))).Expression)).FirstOrDefault();

            if (lastUserLogin != null)
            {
                lastUserLogin.LogoutTime = DateTime.UtcNow;
                lastUserLogin.LogoutReason = logout.Reason;
                await _userLoginService.UpdateAsync(lastUserLogin);
            }
            return true;
        }

        /// <summary>
        /// Change Password
        /// </summary>
        /// <param name="changePasssword"></param>
        /// <returns></returns>
        [AllowAnonymous]
        [HttpPost]
        [Route("ChangePassword")]
        public virtual async Task<object> ChangePasswordAsync([FromBody]ChangePassword changePasssword)
        {
            var result = changePasssword.HasOldPassword ? await _service.ChangePasswordAsync(changePasssword.UserID, changePasssword.OldPassword, changePasssword.NewPassword) :
            await _service.ChangePasswordAsync(changePasssword.UserID, changePasssword.NewPassword);
            return new { success = result };
        }

        /// <summary>
        /// Reset Password
        /// </summary>
        /// <param name="username"></param>
        /// <returns></returns>
        [AllowAnonymous]
        [HttpPost]
        [Route("ResetPassword/{username}")]
        public virtual async Task<bool> ResetPasswordAsync(string username)
        {
            var newPassword = await _service.ResetPasswordAsync(username);
            if (!string.IsNullOrEmpty(newPassword))
            {
                var token = await _tokenProvider.GenerateToken(username, newPassword);
                var user = await _userService.GetUserByUsername(username);
                var subject = "Password Reset";

                var headerOrigin = this.Request.Headers["Origin"];
                var message = $"{headerOrigin.ToString()}/#/reset/{user.ID}/{token.access_token}";
                //Send Email 
                var emailNotifier = _notificationFactory.GetNotification(NotificationType.EMAIL);
                await emailNotifier.Notify(new List<User>(){user}, new BLL.Model.EmailSend(subject, message, "APR", $"{user.FirstName} {user.LastName}", null, "Order",message), "IPAR");

                //push notification
                var notifier = _notificationFactory.GetNotification(NotificationType.PUSHNOTIFICATION);
                await notifier.Notify(new List<User>(){user},new BLL.Model.EmailSend(subject, message, null, $"{user.FirstName} {user.LastName}", null),"IPAR");
                return true;        
            }
            return false;
        }

        [HttpGet]
        [Route("ValidateToken")]
        public virtual async Task<object> ValidateToken()
        {
            return new { success = true };
        }

    }
}