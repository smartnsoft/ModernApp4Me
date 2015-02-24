using System;
using System.Globalization;
using System.Threading.Tasks;
using Newtonsoft.Json;
using ModernApp4Me.Core.WebService;
using RestSharp.Portable;
using System.Text;

namespace ModernApp4Me.Core.WebServiceCache
{

    /// <summary>
    /// A basis class for managing cache data, making web service calls and parsing json response which uses RestSharp, Q42 and JSON.NET third party libraries.
    /// </summary>
    /// 
    /// <author>Ludovic ROLAND</author>
    /// <since>2015.02.24</since>
    public abstract class M4MBaseBackedWSUriStringParser<TResult, TParameter, TWebServiceCaller> where TWebServiceCaller : M4MBaseWebServiceCaller
    {

        public TWebServiceCaller WebServiceCaller { get; set; }

        protected abstract RestRequest ComputeRestRequest(TParameter parameter);

        protected abstract TResult Parse(byte[] response);

        public abstract Task<TResult> GetRetentionValue(bool fromCache, DateTime expirationDelay, TParameter parameter);

        public async Task<TResult> GetValue(TParameter parameter)
        {
            return await GetRetentionValue(false, DateTime.Now, parameter);
        }

        protected virtual TResult DeserializeObject(byte[] response)
        {
            return JsonConvert.DeserializeObject<TResult>(Encoding.UTF8.GetString(response, 0, response.Length));
        }

    }

}
