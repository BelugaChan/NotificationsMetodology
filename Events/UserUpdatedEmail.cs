
namespace NotificationsService.Events
{
    public class UserUpdatedEmail : Event
    {
        public Guid UserId { get; set; }

        public string NewEmail { get; set; }
        public override Guid StreamId => UserId;
    }
}
