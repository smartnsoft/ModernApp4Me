using RestSharp;

namespace ModernApp4Me_WP8.SnSWebService
{
    /// <summary>
    /// A basis class for making web service calls easier.
    /// Uses RestSharp.
    /// </summary>
    public abstract class SnSWebServiceCaller
    {
        /*******************************************************/
        /** PROPERTIES.
        /*******************************************************/
        public RestClient Client { get; private set; }


        /*******************************************************/
        /** METHODS.
        /*******************************************************/
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
