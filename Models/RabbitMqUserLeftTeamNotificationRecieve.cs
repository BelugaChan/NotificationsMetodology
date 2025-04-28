using RabbitMqModel.Models;

namespace NotificationsService.Models
{
    public class RabbitMqUserLeftTeamNotificationRecieve
    {
        public Guid UserId { get; set; }

        public string UserNickName { get; set; }
    }
}
