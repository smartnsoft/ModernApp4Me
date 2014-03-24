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

    }

}
