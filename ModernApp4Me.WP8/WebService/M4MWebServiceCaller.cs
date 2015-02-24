using System.Net;
using System.Threading.Tasks;
using Microsoft.Phone.Net.NetworkInformation;
using RestSharp;
using ModernApp4Me.Core.LifeCycle;
using ModernApp4Me.WP8.Log;
using System;

namespace ModernApp4Me.WP8.WebService
{

    /// <summary>
    /// A basis class for making web service calls easier which uses RestSharp.
    /// When invoking an HTTP method, the caller goes through the following workflow:
    /// The <see cref="M4MWebServiceCaller.ExecuteHttpRequest()"/> methods is invoked : if the response <see cref="HttpStatusCode"/> is OK, the body is returned ad a <see cref="string"/>.
    /// if the <see cref="HttpStatusCode"/> is not OK, the private method <see cref="M4MWebServiceCaller.OnHttpStatusCodeNotOk()"/> is invoked.
    /// if the <see cref="HttpStatusCode"/> is NotFound and the <see cref="DeviceNetworkInformation.IsNetworkAvailable"/> is false, a <see cref="M4MConnectivityException"/> is thrown.
    /// Otherwife, a <see cref="M4MCallException"/> is thrown.
    /// </summary>
    /// 
    /// <author>Ludovic ROLAND</author>
    /// <since>2014.03.21</since>
    public abstract class M4MWebServiceCaller
    {

        private const int ATTEMPTS_COUNT_MAX = 1;

        public RestClient Client { get; private set; }

        /// <summary>
        /// Basis constructor.
        /// </summary>
        /// <param name="baseUrl">The base URL of the web service API</param>
        /// <param name="timeout">The response time-out</param>
        protected M4MWebServiceCaller(string baseUrl, int timeout)
        {
            Client = new RestClient(baseUrl);
            Client.Timeout = timeout;
        }

        /// <summary>
        /// Executes a call to a web method without any persistence.
        /// </summary>
        /// <param name="restRequest">the <see cref="RestRequest"/></param>
        /// <param name="attempsCount">the current attemp number</param>
        /// <returns>The content of the </returns>
        public async Task<string> ExecuteHttpRequest(RestRequest restRequest, int attempsCount = 0)
        {
            attempsCount++;
            var callType = restRequest.Method.ToString();
            var resource = Client.BuildUri(restRequest).AbsoluteUri;

            if (M4MModernLogger.Instance.IsDebugEnabled() == true)
            {
                M4MModernLogger.Instance.Debug("Running the HTTP '" + callType + "' request '" + resource + "'");
            }

            var start = TimeSpan.FromTicks(DateTime.Now.Ticks);
            var rawResponse = await Client.ExecuteTaskAsync(restRequest);
            var end = TimeSpan.FromTicks(DateTime.Now.Ticks);
            var content = rawResponse.Content;
            var statusCode = rawResponse.StatusCode;

            if (M4MModernLogger.Instance.IsDebugEnabled() == true)
            {
                M4MModernLogger.Instance.Debug("The call to the HTTP " + callType + " request '" + resource + "' took " + (end - start).TotalMilliseconds + " ms and returned the status code '" + statusCode + "'");
            }

            if (statusCode != HttpStatusCode.OK)
            {
                if (attempsCount < M4MWebServiceCaller.ATTEMPTS_COUNT_MAX)
                {
                    content = await ExecuteHttpRequest(restRequest, attempsCount);
                }
                else
                {
                    OnHttpStatusCodeNotOk(resource, statusCode);
                }
            }

            return content;
        }

        /// <summary>
        /// Private function that raises an exception when the result code to a web method id not OK (not 20X).
        /// </summary>
        /// <param name="resource">The uri</param>
        /// <param name="statusCode">the <see cref="HttpStatusCode"/></param>
        protected void OnHttpStatusCodeNotOk(string resource, HttpStatusCode statusCode)
        {
            var message = "The error code of the call to the web method '" + resource +
                          "' is not OK (not 20X). Status: '" + statusCode + "'";

            if (statusCode == HttpStatusCode.NotFound && DeviceNetworkInformation.IsNetworkAvailable == false)
            {
                throw new M4MConnectivityException(message);
            }

            throw new M4MCallException(message);
        }

    }

}
