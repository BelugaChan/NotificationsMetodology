using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.DocumentModel;
using Amazon.DynamoDBv2.Model;
using NotificationsService.Events;
using NotificationsService.Interfaces;
using NotificationsService.Models;
using System.Text.Json;

namespace NotificationsService.Services
{
    public class UserBioCommandService : IUserBioCommandService
    {
        private readonly IAmazonDynamoDB amazonDynamoDB;
        private const string TableName = "notifications";

        public UserBioCommandService(IDynamoDbConfigure dynamoDbConfigure)
        {
            amazonDynamoDB = dynamoDbConfigure.Configure();
        }

        public async Task AppendEventAsync<T>(T @event, UserNotification? currentState = null) where T : Event
        {
            @event.CreatedAt = DateTime.UtcNow;

            var eventAsJson = JsonSerializer.Serialize<Event>(@event);
            var itemAsDoc = Document.FromJson(eventAsJson);
            var itemAsAttributes = itemAsDoc.ToAttributeMap();

            var request = new PutItemRequest
            {
                TableName = TableName,
                Item = itemAsAttributes
            };

            await amazonDynamoDB.PutItemAsync(request);
        }
    }
}
