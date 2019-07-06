using Microsoft.Extensions.Options;
using PDX.BLL.Model.Config;
using PDX.BLL.Services.Interfaces;
using PDX.BLL.Services.Interfaces.Email;
using PDX.Domain.Common;

namespace PDX.BLL.Services.Notification {
    public class NotificationFactory {

        private readonly NotificationConfig _notificationSettings;
        private readonly IProducer _producer;
         private readonly INotificationService _notificationService;
         private readonly IService<SystemSetting> _systemSettingService;
        public NotificationFactory (IOptions<NotificationConfig> notificationSettings,IProducer producer,INotificationService notificationService, IService<SystemSetting> systemSettingService) {
            _notificationSettings = notificationSettings.Value;
            _producer = producer;
            _notificationService = notificationService;
            _systemSettingService = systemSettingService;
        }
        public INotification GetNotification (NotificationType type) {
            switch (type) {
                case NotificationType.EMAIL:
                    return new EmailNotification (_notificationSettings,_producer, _systemSettingService);
                case NotificationType.PUSHNOTIFICATION:
                    return new PushNotification (_notificationSettings,_producer,_notificationService, _systemSettingService);
                default:
                    return new EmailNotification (_notificationSettings,_producer, _systemSettingService);
            }
        }
    }
}