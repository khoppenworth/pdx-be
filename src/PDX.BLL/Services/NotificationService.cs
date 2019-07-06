using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using DataTables.AspNet.Core;
using PDX.BLL.Helpers;
using PDX.BLL.Model;
using PDX.BLL.Services.Interfaces;
using PDX.DAL.Query;
using PDX.DAL.Repositories;
using PDX.Domain.Public;

namespace PDX.BLL.Services
{
    public class NotificationService : Service<PDX.Domain.Public.Notification>, INotificationService
    {
        public NotificationService(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }

        public async Task<DataTablesResult<PDX.Domain.Public.Notification>> GetNotificationPageAsync(SearchRequest request, int userID)
        {
            Expression<Func<PDX.Domain.Public.Notification, bool>> predicate = this.ConstructFilter<PDX.Domain.Public.Notification>(userID);
            OrderBy<PDX.Domain.Public.Notification> orderBy = new OrderBy<PDX.Domain.Public.Notification>(qry => qry.OrderByDescending(e => e.CreatedDate));

            var response = await this.SearchAsync(request, predicate, orderBy.Expression);
            return response;
        }

        public async Task<object> GetUnreadNotificationCount(int userID)
        {
            var unreadNotifications = await this.FindByAsync(n => n.UserID == userID && !n.IsRead);
            var obj = new { UnreadNotifications = unreadNotifications.Count()};
            return obj;
        }

        public async Task<bool> MarkNotificationAsRead(PDX.Domain.Public.Notification notification)
        {
            notification.IsRead = true;
            var result = await this.UpdateAsync(notification);
            return result;
        }

        public async Task<bool> MarkNotificationAsRead(IList<int> notificationIDs){
            var notifications = await this.FindByAsync(nt => notificationIDs.Contains(nt.ID));
            var result = true;
            foreach(var notification in notifications){
                result = result && (await MarkNotificationAsRead(notification));
            }
            return result;
        }

        public async Task<bool> MarkAllNotificationAsRead(int userID){
            var unreadNotifications = (await this.FindByAsync(n => n.UserID == userID && !n.IsRead)).Select(s => s.ID).ToList();
            var result = await MarkNotificationAsRead(unreadNotifications);
            return result;
        }


        private Expression<Func<T, bool>> ConstructFilter<T>(int userID)
        {
            Expression<Func<T, bool>> expression = null;
            ParameterExpression argument = Expression.Parameter(typeof(T), "ma");
            Expression predicateBody = null;

            predicateBody = argument.GetExpression("UserID", userID, "Equal", typeof(int));

            expression = Expression.Lambda<Func<T, bool>>(predicateBody, new[] { argument });
            return expression;
        }
    }
}