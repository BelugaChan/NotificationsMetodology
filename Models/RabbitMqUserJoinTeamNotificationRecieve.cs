using RabbitMqModel.Models;

namespace NotificationsService.Models
{
    public class RabbitMqUserJoinTeamNotificationRecieve
    {
        public Guid UserId { get; set; }

        public string UserNickName { get; set; }

        public string TeamName { get; set; }
    }
}
