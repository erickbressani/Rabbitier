using Newtonsoft.Json;
using System.Text;

namespace Rabbitier.Subscriber.Parsers
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
    }
}
