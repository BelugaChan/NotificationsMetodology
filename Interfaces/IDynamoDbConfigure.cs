using Amazon.DynamoDBv2;

namespace NotificationsService.Interfaces
{
    public interface IDynamoDbConfigure
    {
        IAmazonDynamoDB Configure();
    }
}
