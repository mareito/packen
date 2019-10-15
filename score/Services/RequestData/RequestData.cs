using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace score.Services.RequestData
{
    /// <summary>
    /// Implementation of IRequestData Interface
    /// </summary>
    public class RequestData : IRequestData
    {
        private IDictionary<string, string> data;

        public RequestData()
        {
            data = new Dictionary<string, string>();
        }

        public bool exists(string key)
        {
            return data.ContainsKey(key);
        }

        public string getData(string key)
        {
            foreach (var reg in data)
            {
                if (string.Equals(reg.Key, key))
                {
                    return reg.Value;
                }
            }
            return null;
        }

        public void setData(string key, string value)
        {
            if (data.ContainsKey(key))
                data.Remove(key);

            data.Add(key, value);
        }
    }
}
