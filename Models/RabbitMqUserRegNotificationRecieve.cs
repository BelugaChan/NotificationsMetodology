using RabbitMqModel.Models;

namespace NotificationsService.Models
{
    public class RabbitMqUserRegNotificationRecieve
    {
        public Guid UserId { get; set; }

        public string Email { get; set; }
    }
}
