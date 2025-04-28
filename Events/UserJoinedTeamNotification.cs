
namespace NotificationsService.Events
{
    public class UserJoinedTeamNotification : Event
    {
        public Guid UserId { get; set; }

        public string UserNickName { get; set; }

        public string TeamName { get; set; }

        public string NotificationMessage => $"Congratulations, {UserNickName}! You just joined a team: {TeamName}";

        public override Guid StreamId => UserId;
    }
}
