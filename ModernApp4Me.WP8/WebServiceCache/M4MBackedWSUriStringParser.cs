using System;
using System.Globalization;
using System.Threading.Tasks;
using ModernApp4Me.WP8.WebService;
using Newtonsoft.Json;
using Q42.WinRT.Data;
using RestSharp;

namespace ModernApp4Me.WP8.WebServiceCache
{

    /// <summary>
    /// A basis class for managing cache data, making web service calls and parsing json response which uses RestSharp, Q42 and JSON.NET third party libraries.
    /// </summary>
    /// 
    /// <author>Ludovic ROLAND</author>
    /// <since>2015.02.24</since>
    public abstract class M4MBackedWSUriStringParser<TResult, TParameter, TWebServiceCaller> where TWebServiceCaller : M4MWebServiceCaller
    {

        public TWebServiceCaller WebServiceCaller { get; set; }

        protected abstract RestRequest ComputeRestRequest(TParameter parameter);

        protected abstract TResult Parse(string response);

        public async Task<TResult> GetRetentionValue(bool fromCache, DateTime expirationDelay, TParameter parameter)
        {
            var restRequest = ComputeRestRequest(parameter);
            var response = await DataCache.GetAsync(restRequest.GetHashCode().ToString(CultureInfo.InvariantCulture), () => WebServiceCaller.ExecuteHttpRequest(restRequest), expirationDelay, fromCache == false);
            return Parse(response);
        }

        public async Task<TResult> GetValue(TParameter parameter)
        {
            return await GetRetentionValue(false, DateTime.Now, parameter);
        }

        protected TResult DeserializeObject(string response)
        {
            return JsonConvert.DeserializeObject<TResult>(response);
        }
    }

}
