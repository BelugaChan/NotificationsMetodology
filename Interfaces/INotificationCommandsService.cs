using NotificationsService.Events;
using NotificationsService.Models;

namespace NotificationsService.Interfaces
{
    public interface INotificationCommandsService
    {
        Task AppendEventAsync<T>(T @event, UserNotification? currentState = null)
            where T : Event;
    }
}
