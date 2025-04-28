using Microsoft.AspNetCore.SignalR;
using NotificationsService.Events;
using NotificationsService.Hubs;
using NotificationsService.Interfaces;
using NotificationsService.Models;
using RabbitMqModel.Models;

namespace NotificationsService.Services
{
    public class NotificationService : INotificationService
    {
        private readonly INotificationCommandsService commandService;
        private readonly IHubContext<NotificationHub> hubContext;
        public NotificationService(INotificationCommandsService commandService,IHubContext<NotificationHub> hubContext)
        {
            this.commandService = commandService;
            this.hubContext = hubContext;
        }
        public async Task HandleAsync(IEvent message)
        {
            switch (message)
            {
                case RabbitMqUserRegNotificationRecieve regMessage:
                    var userRegistered = new UserRegisteredNotification
                    {
                        UserId = regMessage.UserId,
                        Email = regMessage.Email
                    };

                    await commandService.AppendEventAsync(userRegistered);
                    await hubContext.Clients.User(userRegistered.UserId.ToString())
                        .SendAsync("ReceiveNotification", userRegistered.NotificationMessage);
                    break;
                case RabbitMqUserJoinTeamNotificationRecieve joinTeamMessage:
                    var userJoined = new UserJoinedTeamNotification
                    {
                        UserId = joinTeamMessage.UserId,
                        TeamName = joinTeamMessage.TeamName,
                        UserNickName = joinTeamMessage.UserNickName
                    };
                    await commandService.AppendEventAsync(userJoined);
                    await hubContext.Clients.User(userJoined.UserId.ToString())
                       .SendAsync("ReceiveNotification", userJoined.NotificationMessage);
                    break;
                case RabbitMqUserLeftTeamNotificationRecieve leaveTeamMessage:
                    var userLeft = new UserLeftTeamNotification
                    {
                        UserId = leaveTeamMessage.UserId,
                        TeamName = "No team (free unit)",
                        UserNickName = leaveTeamMessage.UserNickName
                    };
                    await commandService.AppendEventAsync(userLeft);
                    await hubContext.Clients.User(userLeft.UserId.ToString())
                       .SendAsync("ReceiveNotification", userLeft.NotificationMessage);
                    break;

            }
        }
    }
}
