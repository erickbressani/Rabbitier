using Newtonsoft.Json;
using System.Text;

namespace Rabbitier.Consumer.Parsers
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
            catch (JsonSerializationException)
            {
                return default(TMessage);
            }
        }
    }
}
