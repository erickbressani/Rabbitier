using System.Collections.Generic;

namespace Rabbitier.Publisher
{
    public interface ISender
    {
        ISender AddHeader(KeyValuePair<string, object> header);
        ISender AddHeader(string key, object value);
        void Publish();
    }
}
