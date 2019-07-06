using System.Collections.Generic;
using System.Threading.Tasks;
using PDX.BLL.Model;
using PDX.BLL.Model.Config;
using PDX.BLL.Services.Interfaces;
using PDX.BLL.Services.Interfaces.Email;
using PDX.Domain.Account;
using PDX.Domain.Common;

namespace PDX.BLL.Services.Notification {
    public class EmailNotification : INotification {
        private readonly IProducer _producer;
        private readonly IService<SystemSetting> _systemSettingService; 

        public EmailNotification (NotificationConfig notificationSettings, IProducer producer, IService<SystemSetting> systemSettingService) : base (notificationSettings) {
            _producer = producer;
            _systemSettingService = systemSettingService;
        }

        public override async Task Notify (IEnumerable<User> users, object data, string eventType) {
            //Restrict if Allow Email Notifications setting is false
            var emailSetting = await _systemSettingService.GetAsync(ss => ss.SystemSettingCode == "AEN");
            if(!(bool)emailSetting.ValueObj) return;

            var dataList = new List<object> () {
                data
            };
            var json = _producer.ToJson (dataList);

            var eEvent = base.ConstructEvent ();
            eEvent.EventTypeCode = eventType;
            foreach (var user in users) {
                eEvent.UserEventDatas.Add (new Model.UserEventData () {
                    Userguid = user.RowGuid.ToString (),
                        FullName = $"{user?.FirstName} {user?.LastName}",
                        Data = json,
                        Addresses = new List<UserAddress> () {
                            new UserAddress () {
                                Medium = "EMAIL",
                                Address = user.Email
                            }
                        }
                });
            }

            await _producer.RegisterEvent (eEvent);
        }
    }
}