using Amazon.DynamoDBv2;
using Amazon.Runtime;
using Microsoft.Extensions.Options;
using NotificationsService.Interfaces;
using NotificationsService.Models;

namespace NotificationsService.Configurations
{
    public class DynamoDbConfigure : IDynamoDbConfigure
    {
        private readonly DynamoDbOptions dynamoDbOptions;
        public DynamoDbConfigure(IOptions<DynamoDbOptions> dynamoDbOptions)
        {
            this.dynamoDbOptions = dynamoDbOptions.Value;
        }
        public IAmazonDynamoDB Configure()
        {
            var credentials = new BasicAWSCredentials("fakeKey", "fakeSecretAccessKey");

            var config = new AmazonDynamoDBConfig
            {
                ServiceURL = $"http://{this.dynamoDbOptions.Host}:{this.dynamoDbOptions.Port}"
            };
            IAmazonDynamoDB amazonDynamoDB = new AmazonDynamoDBClient(credentials, config);

            return amazonDynamoDB;
        }
    }
}
