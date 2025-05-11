using RabbitMqModel.Models;

namespace NotificationsService.Models
{
    public class RabbitMqUserRegNotificationRecieve : IEvent
    {
        public Guid UserId { get; set; }

        public string Email { get; set; }

        public string EventType => "UserReg";
    }
}
