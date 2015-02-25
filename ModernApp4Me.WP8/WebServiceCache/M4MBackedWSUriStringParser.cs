using System;
using System.Globalization;
using System.Threading.Tasks;
using ModernApp4Me.Core.WebService;
using Q42.WinRT.Data;
using ModernApp4Me.Core.WebServiceCache;

namespace ModernApp4Me.WP8.WebServiceCache
{

    /// <summary>
    /// A basis class for managing cache data, making web service calls and parsing json response which uses RestSharp, Q42 and JSON.NET third party libraries.
    /// </summary>
    /// 
    /// <author>Ludovic ROLAND</author>
    /// <since>2015.02.24</since>
    public abstract class M4MBackedWSUriStringParser<TResult, TParameter, TWebServiceCaller> : M4MBaseBackedWSUriStringParser<TResult, TParameter, TWebServiceCaller> where TWebServiceCaller : M4MBaseWebServiceCaller
    {

        public override async Task<TResult> GetRetentionValue(bool fromCache, DateTime expirationDelay, TParameter parameter)
        {
            var restRequest = ComputeRestRequest(parameter);
            var response = await DataCache.GetAsync(restRequest.GetHashCode().ToString(CultureInfo.InvariantCulture), () => WebServiceCaller.ExecuteHttpRequest(restRequest), expirationDelay, fromCache == false);
            return Parse(response);
        }

    }

}
