using System.Net;
using System.Threading.Tasks;
using System;
using RestSharp.Portable;

namespace ModernApp4Me.Core.WebService
{

    /// <summary>
    /// A basis class for making web service calls easier which uses RestSharp.Portable.
    /// When invoking an HTTP method, the caller goes through the following workflow:
    /// The <see cref="M4MBaseWebServiceCaller.ExecuteHttpRequest()"/> methods is invoked : if the response <see cref="HttpStatusCode"/> is OK, the body is returned ad a <see cref="string"/>.
    /// if the <see cref="HttpStatusCode"/> is not OK, the private method <see cref="M4MBaseWebServiceCaller.OnHttpStatusCodeNotOk()"/> is invoked.
    /// </summary>
    /// 
    /// <author>Ludovic ROLAND</author>
    /// <since>2015.02.24</since>
    public abstract class M4MBaseWebServiceCaller
    {

        private const int ATTEMPTS_COUNT_MAX = 2;

        private RestClient client;

        /// <summary>
        /// Basis constructor.
        /// </summary>
        /// <param name="baseUrl">The base URL of the web service API</param>
        protected M4MBaseWebServiceCaller(string baseUrl)
        {
            client = new RestClient(baseUrl);
        }

        /// <summary>
        /// Executes a call to a web method without any persistence.
        /// </summary>
        /// <param name="restRequest">the <see cref="RestRequest"/></param>
        /// <param name="attempsCount">the current attemp number</param>
        /// <returns>The content of the </returns>
        public virtual async Task<byte[]> ExecuteHttpRequest(RestRequest restRequest, int attempsCount = 0)
        {
            attempsCount++;
            var callType = restRequest.Method.ToString();
            var resource = client.BuildUri(restRequest).AbsoluteUri;

            Debug("Running the HTTP '" + callType + "' request '" + resource + "'");

            var start = TimeSpan.FromTicks(DateTime.Now.Ticks);
            var rawResponse = await client.Execute(restRequest);
            var end = TimeSpan.FromTicks(DateTime.Now.Ticks);
            var data = rawResponse.RawBytes;
            var statusCode = rawResponse.StatusCode;

            Debug("The call to the HTTP " + callType + " request '" + resource + "' took " + (end - start).TotalMilliseconds + " ms and returned the status code '" + statusCode + "'");

            if (statusCode != HttpStatusCode.OK)
            {
                if (attempsCount < M4MBaseWebServiceCaller.ATTEMPTS_COUNT_MAX)
                {
                    data = await ExecuteHttpRequest(restRequest, attempsCount);
                }
                else
                {
                    OnHttpStatusCodeNotOk(resource, statusCode);
                }
            }

            return data;
        }

        protected abstract void OnHttpStatusCodeNotOk(string resource, HttpStatusCode statusCode);

        protected abstract void Debug(string message);

        protected abstract void Error(string message);

    }

}
