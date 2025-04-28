namespace NotificationsService.Events
{
    public class UserLeftTeamNotification : Event
    {
        public Guid UserId { get; set; }

        public string UserNickName { get; set; }

        public string TeamName { get; set; }

        public string NotificationMessage => $"Dear, {UserNickName}! You just left a team: {TeamName}";

        public override Guid StreamId => throw new NotImplementedException();
    }
}
