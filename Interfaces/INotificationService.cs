using RabbitMqModel.Models;

namespace NotificationsService.Interfaces
{
    public interface INotificationService
    {
        Task HandleAsync(IEvent message);
    }
}
