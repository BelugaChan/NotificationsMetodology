using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using NotificationsService.Models;
using RabbitMqModel.Models;

namespace NotificationsService.Converters
{
    public class EventConverter : JsonConverter<IEvent>
    {
        public override bool CanWrite => false;
        public override IEvent? ReadJson(JsonReader reader, Type objectType, IEvent? existingValue, bool hasExistingValue, JsonSerializer serializer)
        {
            var jsonObject = JObject.Load(reader);

            if (jsonObject.ContainsKey("Email"))
            {
                return jsonObject.ToObject<RabbitMqUserRegNotificationRecieve>(serializer);
            }
            else if (jsonObject.ContainsKey("TeamName"))
            {
                return jsonObject.ToObject<RabbitMqUserJoinTeamNotificationRecieve>(serializer);
            }
            else if (jsonObject.ContainsKey("UserNickName"))
            {
                return jsonObject.ToObject<RabbitMqUserLeftTeamNotificationRecieve>(serializer);
            }
            throw new JsonSerializationException("Unknown type");
        }

        public override void WriteJson(JsonWriter writer, IEvent? value, JsonSerializer serializer)
        {
            throw new NotImplementedException();
        }
    }
}
