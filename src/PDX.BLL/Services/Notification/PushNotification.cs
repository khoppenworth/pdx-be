using System.Collections.Generic;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using PDX.BLL.Model;
using PDX.BLL.Model.Config;
using PDX.BLL.Services.Interfaces;
using PDX.BLL.Services.Interfaces.Email;
using PDX.Domain.Account;
using PDX.Domain.Common;

namespace PDX.BLL.Services.Notification {
    public class PushNotification : INotification {
        private readonly IProducer _producer;
        private readonly INotificationService _notificationService;
        private readonly IService<SystemSetting> _systemSettingService;
        public PushNotification (NotificationConfig notificationSettings, IProducer producer, INotificationService notificationService, IService<SystemSetting> systemSettingService) : base (notificationSettings) {
            _producer = producer;
            _notificationService = notificationService;
            _systemSettingService = systemSettingService;
        }

        public override async Task Notify (IEnumerable<User> users, object data, string eventType) {
            //Restrict if Allow Push Notifications setting is false
            var pushNotificationSetting  = await _systemSettingService.GetAsync(ss => ss.SystemSettingCode == "APN");
            if(!(bool)pushNotificationSetting.ValueObj) return;

            var dataList = new List<object> () {
                data
            };
            var json = _producer.ToJson (dataList);
            var jsonForSave = _producer.ToJson (data);

            var eEvent = base.ConstructEvent ();
            eEvent.EventTypeCode = eventType;

            var notifications = new List<PDX.Domain.Public.Notification> ();

            foreach (var user in users) {
                eEvent.UserEventDatas.Add (new Model.UserEventData () {
                    Userguid = user.RowGuid.ToString (),
                        FullName = $"{user?.FirstName} {user?.LastName}",
                        Data = json,
                        Addresses = new List<UserAddress> () {
                            new UserAddress () {
                                Medium = "FCM"
                            }
                        }
                });
                //construct notification object

                var notification = new PDX.Domain.Public.Notification () {
                    UserID = user.ID,
                    Medium = "FCM",
                    Data = JsonConvert.DeserializeObject<JObject> (string.IsNullOrEmpty (jsonForSave) ? "{}" : jsonForSave)
                };
                notifications.Add (notification);
            }
            await _producer.RegisterEvent (eEvent);

            await _notificationService.CreateAsync (notifications);
        }
    }
}