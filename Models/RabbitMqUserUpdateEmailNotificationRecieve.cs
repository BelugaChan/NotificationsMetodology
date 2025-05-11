using RabbitMqModel.Models;

namespace NotificationsService.Models
{
    public class RabbitMqUserUpdateEmailNotificationRecieve : IEvent
    {
        public Guid UserId { get; set; }

        public string NewEmail { get; set; }
        public string EventType => "EmailUpdate";
    }
}
