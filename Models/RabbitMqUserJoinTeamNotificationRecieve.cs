using RabbitMqModel.Models;

namespace NotificationsService.Models
{
    public class RabbitMqUserJoinTeamNotificationRecieve : IEvent
    {
        public Guid UserId { get; set; }

        public string UserNickName { get; set; }

        public string TeamName { get; set; }

        public string EventType => "UserJoinTeam";
    }
}
