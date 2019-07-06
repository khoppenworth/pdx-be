using System.Collections.Generic;
using System.Threading.Tasks;
using DataTables.AspNet.Core;
using PDX.BLL.Model;
using PDX.Domain.Public;

namespace PDX.BLL.Services.Interfaces
{
    public interface INotificationService:IService<PDX.Domain.Public.Notification>
    {
         Task<DataTablesResult<PDX.Domain.Public.Notification>> GetNotificationPageAsync(SearchRequest request, int userID);
         Task<object> GetUnreadNotificationCount(int userID);
         Task<bool> MarkNotificationAsRead(PDX.Domain.Public.Notification notification);
         Task<bool> MarkNotificationAsRead(IList<int> notificationIDs);
         Task<bool> MarkAllNotificationAsRead(int userID);
    }
}