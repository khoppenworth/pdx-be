using System.Collections.Generic;
using System.Threading.Tasks;
using DataTables.AspNet.Core;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PDX.API.Helpers;
using PDX.BLL.Model;
using PDX.BLL.Services.Interfaces;
using PDX.Domain.Public;

namespace PDX.API.Controllers.Public
{
    [Authorize]
    [Route("api/[controller]")]
    public class NotificationController: BaseController<Notification>
    { 
        private readonly INotificationService _notificationService;
        public NotificationController(INotificationService notificationService)
        :base(notificationService)
        {
            _notificationService = notificationService;
        }

        [Route("MarkAsRead")]
        [HttpPut]
        public async Task<bool> MarkNotificationAsRead([FromBody] Notification notification)
        {
            var result = await _notificationService.MarkNotificationAsRead(notification);
            return result;
        }

        [Route("MarkAllAsRead")]
        [HttpPut]
        public async Task<bool> MarkNotificationAsRead()
        {
            var userID = this.HttpContext.GetUserID();
            var result = await _notificationService.MarkAllNotificationAsRead(userID);
            return result;
        }

        /// <summary>
        /// get list of notification
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [Route("Recent")]
        public override async Task<DataTablesResult<Notification>> SearchAsync([FromBody]SearchRequest request)
        {
            var result = await _notificationService.GetNotificationPageAsync(request, this.HttpContext.GetUserID());
            return result;
        }

        /// <summary>
        /// get list of notification
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [Route("List")]
        public async Task<DataTablesResult<Notification>> GetNotificationPageAsync([FromBody]IDataTablesRequest request)
        {
            var searchRequest = new SearchRequest {Query = request.Search.Value, PageNumber = request.Draw - 1, PageSize = request.Length};
            var result = await _notificationService.GetNotificationPageAsync(searchRequest, this.HttpContext.GetUserID());
            return result;
        }

        /// <summary>
        /// Get unread notifications for the current user
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("Unread")]
        public async Task<object> GetUnreadNotificationCount()
        {
            var userID = this.HttpContext.GetUserID();
            var result = await _notificationService.GetUnreadNotificationCount(userID);
            return result;
        }
    }
}