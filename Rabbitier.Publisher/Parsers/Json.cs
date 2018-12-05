using Newtonsoft.Json;
using System.Text;

namespace Rabbitier.Publisher.Parsers
{
    public static class Json
    {
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
