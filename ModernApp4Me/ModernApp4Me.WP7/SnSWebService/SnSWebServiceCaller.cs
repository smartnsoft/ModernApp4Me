using System.Net;
using System.Threading.Tasks;
using Microsoft.Phone.Net.NetworkInformation;
using ModernApp4Me.WP7.SnSApp;
using ModernApp4Me.WP7.SnSLog;
using RestSharp;

namespace ModernApp4Me.WP7.SnSWebService
{

    /// <summary>
    /// A basis class for making web service calls easier.
    /// Use RestSharp.
    /// </summary>
    /// <author>Ludovic ROLAND</author>
    /// <since>2014.03.21</since>
    public abstract class SnSWebServiceCaller
    {

        public RestClient Client { get; private set; }

        /// <summary>
        /// Constructor without authenticator.
        /// </summary>
        /// <param name="baseUrl"></param>
        protected SnSWebServiceCaller(string baseUrl)
        {
            Client = new RestClient(baseUrl);
        }

        /// <summary>
        /// Constructor with authenticator.
        /// </summary>
        /// <param name="baseUrl"></param>
        /// <param name="username"></param>
        /// <param name="password"></param>
        protected SnSWebServiceCaller(string baseUrl, string username, string password)
        {
            Client = new RestClient(baseUrl) { Authenticator = new HttpBasicAuthenticator(username, password) };
        }

        /// <summary>
        /// Executes a call to a web method without any persistence.
        /// </summary>
        /// <param name="restRequest"></param>
        /// <returns>The content of the </returns>
        protected async Task<string> ExecuteHttpRequest(RestRequest restRequest)
        {
            var rawResponse = await Client.ExecuteTaskAsync(restRequest);

            if (rawResponse.StatusCode != HttpStatusCode.OK)
            {
                OnHttpStatusCodeNotOk(rawResponse);
            }

            return rawResponse.Content;
        }

        /// <summary>
        /// Private function that raises an exception when the result code to a web method id not OK (not 20X).
        /// </summary>
        /// <param name="rawResponse"></param>
        private void OnHttpStatusCodeNotOk(IRestResponse rawResponse)
        {
            var message = "The error code of the call to the web method '" + rawResponse.Request.Resource +
                          "' is not OK (not 20X). Status: '" + rawResponse.StatusCode + "'";

            SnSLoggerWrapper.Instance.Logger.Error(message);

            if (rawResponse.StatusCode == HttpStatusCode.NotFound && DeviceNetworkInformation.IsNetworkAvailable == false)
            {
                throw new SnSConnectivityException(message);
            }

            throw new SnSCallException(message);
        }

    }

}
