using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace score.Services.RequestData
{
    /// <summary>
    /// Interface that defines methods for <ref>RequestData</ref> object
    /// </summary>
    public interface IRequestData
    {
        bool exists(string key);
        string getData(string key);
        void setData(string key, string value);
    }
}
