using Newtonsoft.Json;
using System.Text;

namespace Rabbitier.Configuration.Parsers
{
    public static class Json
    {
        public static TMessage Parse<TMessage>(byte[] messageBody) where TMessage : new()
        {
            try
            {
                string encodedMessage = Encoding.UTF8.GetString(messageBody);
                return JsonConvert.DeserializeObject<TMessage>(encodedMessage);
            }
            catch (JsonException)
            {
                return default(TMessage);
            }
        }

        public static byte[] ParseToJson(object message)
        {
            try
            {
                string json = JsonConvert.SerializeObject(message);
                return Encoding.Default.GetBytes(json);
            }
            catch (JsonException)
            {
                return default(byte[]);
            }
        }
    }
}
