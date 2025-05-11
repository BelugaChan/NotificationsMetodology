using RabbitMqModel.Models;

namespace NotificationsService.Models
{
    public class RabbitMqUserLeftTeamNotificationRecieve : IEvent
    {
        public Guid UserId { get; set; }

        public string UserNickName { get; set; }

        public string EventType => "UserLeftTeam";
    }
}
