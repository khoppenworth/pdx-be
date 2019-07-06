using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using PDX.BLL.Model;
using PDX.BLL.Model.Config;
using PDX.BLL.Services.Interfaces.Email;
using RabbitMQ.Client;

namespace PDX.BLL.Services.Email {
    public class Producer : IProducer {
        private readonly NotificationConfig _notificationSettings;
        private readonly ConnectionFactory _connectionFactory;
        public Producer (IOptions<NotificationConfig> notificationSettings) {
            _notificationSettings = notificationSettings.Value;
            _connectionFactory = new ConnectionFactory () { HostName = _notificationSettings.HostName, VirtualHost = _notificationSettings.VirtualHost, AutomaticRecoveryEnabled = true };
        }
        public async Task RegisterEvent (string json) {
            var queueName = "Events";
            using (var connection = _connectionFactory.CreateConnection("Event Service")){
            var channel = connection.CreateModel();
            
                try
                {
                    channel.QueueDeclarePassive(queueName);
                }
                catch (RabbitMQ.Client.Exceptions.OperationInterruptedException)
                {
                    //queue not found, so recreate the channel and declare a new queue 
                    channel = connection.CreateModel();
                    channel.QueueDeclare(queueName, false, false, false, null);
                }
                var body = Encoding.UTF8.GetBytes(json);
                channel.BasicPublish("", "Events", null, body);
                channel.Close();// close the channel
        }
        }

        public async Task RegisterEvent (Event eEvent) {
             await RegisterEvent(ToJson(eEvent));
        }

        public string ToJson (object obj) {
            var serializerSettings =
                new JsonSerializerSettings {
                    ContractResolver = new CamelCasePropertyNamesContractResolver ()
                };
            return JsonConvert.SerializeObject (obj, serializerSettings);
        }
    }
}