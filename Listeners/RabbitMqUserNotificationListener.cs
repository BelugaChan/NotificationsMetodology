using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using NotificationsService.Converters;
using NotificationsService.Interfaces;
using RabbitMqListener.Abstract;
using RabbitMqModel.Models;

namespace NotificationsService.Listeners
{
    public class RabbitMqUserNotificationListener : RabbitMqListenerBase
    {
        private readonly INotificationService notificationService;
        private readonly JsonSerializerSettings serializerSettings = new JsonSerializerSettings
        {
            Converters = [new EventConverter()]
        };
        public RabbitMqUserNotificationListener(INotificationService notificationService,IOptions<RabbitMqOptions> options) : base(options)
        {
            this.notificationService = notificationService;
        }

        protected override string QueueName => "NotificationQueue";

        public override async Task ProcessMessageAsync(string message)
        {
            var result = JsonConvert.DeserializeObject<IEvent>(message,serializerSettings);
            if (result is not null)
                await notificationService.HandleAsync(result);
        }
    }
}
