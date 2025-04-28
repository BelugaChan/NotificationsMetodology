
namespace NotificationsService.Events
{
    public class UserRegisteredNotification : Event
    {
        public Guid UserId { get; set; }

        public string Email { get; set; }

        public string NotificationMessage => $"Thank you for joining us, {Email}!";

        public override Guid StreamId => UserId;
    }
}
