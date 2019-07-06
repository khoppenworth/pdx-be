using System.Collections.Generic;
using System.Threading.Tasks;
using PDX.BLL.Model;
using PDX.BLL.Model.Config;
using PDX.Domain.Account;

namespace PDX.BLL.Services.Notification
{
    public abstract class INotification
    {
        private readonly NotificationConfig _notificationSettings;
        public INotification( NotificationConfig notificationSettings)
        {
            _notificationSettings = notificationSettings;
        }
         public abstract Task Notify (IEnumerable<User> users,object data,string eventType);
         public Event ConstructEvent(){
             return new Event(){
                 ApplicationCode = _notificationSettings.ApplicationCode,
                 EnvironmentCode = _notificationSettings.EnvironmentCode,
                 UserEventDatas = new List<UserEventData>()
             };
         }
    }
}