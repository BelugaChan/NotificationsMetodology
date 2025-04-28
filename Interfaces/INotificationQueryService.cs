using NotificationsService.Events;
using NotificationsService.Models;

namespace NotificationsService.Interfaces
{
    public interface INotificationQueryService
    {
        Task<UserNotification?> GetUserNotificationAsync(Guid userId);
    }
}
