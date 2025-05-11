using NotificationsService.Events;
using System.Text.Json.Serialization;

namespace NotificationsService.Models
{
    public class UserNotification
    {
        [JsonPropertyName("pk")]
        public string Pk => $"{UserId.ToString()}_state";

        [JsonPropertyName("sk")]
        public string Sk => $"{UserId.ToString()}_state";
        public Guid UserId { get; set; }

        public string Email { get; set; }

        public string UserNickName { get; set; } = string.Empty;

        public string TeamName { get; set; } = string.Empty;

        private void Apply(UserRegisteredNotification notification)
        {
            UserId = notification.UserId;
            Email = notification.Email;
        }

        private void Apply(UserJoinedTeamNotification notification)
        {
            UserNickName = notification.UserNickName;
            TeamName = notification.TeamName;
        }

        private void Apply(UserLeftTeamNotification notification)
        {
            UserNickName= notification.UserNickName;
            TeamName = notification.TeamName;
        }

        private void Apply(UserUpdatedEmail userUpdatedEmail)
        {
            Email = userUpdatedEmail.NewEmail;
        }


        public void Apply(Event @event) 
        {
            switch(@event)
            {
                case UserRegisteredNotification userRegisteredNotification:
                    Apply(userRegisteredNotification);
                    break;
                case UserJoinedTeamNotification userJoinedTeamNotification:
                    Apply(userJoinedTeamNotification);
                    break;
                case UserLeftTeamNotification userLeftTeamNotification:
                    Apply(userLeftTeamNotification);
                    break;
                case UserUpdatedEmail userUpdatedEmail:
                    Apply(userUpdatedEmail);
                    break;
            }
        }
    }
}
