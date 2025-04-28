using System.Text.Json.Serialization;

namespace NotificationsService.Events
{
    [JsonPolymorphic]
    [JsonDerivedType(typeof(UserRegisteredNotification),nameof(UserRegisteredNotification))]
    [JsonDerivedType(typeof(UserJoinedTeamNotification),nameof(UserJoinedTeamNotification))]
    [JsonDerivedType(typeof(UserLeftTeamNotification),nameof(UserLeftTeamNotification))]
    public abstract class Event
    {
        public abstract Guid StreamId { get; }
        public DateTime CreatedAt { get; set; }

        [JsonPropertyName("pk")]
        public string Pk => StreamId.ToString();

        [JsonPropertyName("sk")]
        public string Sk => CreatedAt.ToString("0");
    }
}
