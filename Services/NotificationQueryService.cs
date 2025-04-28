using Amazon.DynamoDBv2.Model;
using Amazon.DynamoDBv2;
using NotificationsService.Interfaces;
using NotificationsService.Models;
using NotificationsService.Events;
using Amazon.DynamoDBv2.DocumentModel;
using System.Text.Json;

namespace NotificationsService.Services
{
    public class NotificationQueryService : INotificationQueryService
    {
        private readonly IAmazonDynamoDB amazonDynamoDB;
        private const string TableName = "notifications";

        public NotificationQueryService(IDynamoDbConfigure dynamoDbConfigure)
            => amazonDynamoDB = dynamoDbConfigure.Configure();
        public async Task<UserNotification?> GetUserNotificationAsync(Guid userId)
        {
            var request = new QueryRequest
            {
                    TableName = TableName,
                    KeyConditionExpression = "pk = :v_Pk",
                    ExpressionAttributeValues =
                    {
                        {":v_Pk", new AttributeValue{S = userId.ToString() } }
                    }
                
            };

            var response = await amazonDynamoDB.QueryAsync(request);

            if (response.Count == 0)
                return null;

            var itemAsDocuments = response.Items.Select(Document.FromAttributeMap);
            var userNotificationEvents = itemAsDocuments.Select(i => JsonSerializer.Deserialize<Event>(i.ToJson()));
            var userNotification = new UserNotification();

            foreach (var userNotificationEvent in userNotificationEvents)
            {
                userNotification.Apply(userNotificationEvent!);
            }
            return userNotification;
        }
    }
}
