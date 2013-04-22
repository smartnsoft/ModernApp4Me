using System;
using Microsoft.Phone.Net.NetworkInformation;
using RestSharp;

namespace ModernApp4Me_WP7.SnSWebService
{
    /// <summary>
    /// A basis class for making web service calls easier.
    /// 
    /// When invoking an HTTP method, the caller goes through the following workflow:
    /// 1. the isConnected() method is checked: if the return value is set to false, no request will be attempted and a WebServiceCaller.CallException exception will be thrown ;
    /// 2. the onBeforeHttpRequestExecution(DefaultHttpClient, HttpRequestBase) method will be invoked, so as to let the caller tune the HTTP method request ;
    /// 3. if a connection issue arises (connection time-out, socket time-out, lost of connectivity), a WebServiceCaller.CallException exception will be thrown, and it will Throwable#getCause() the reason for the connection issue ;
    /// 4. if the status code of the HTTP response does not belong to the HttpStatus.SC_OK, HttpStatus.SC_MULTI_STATUS range, the onStatusCodeNotOk(String, WebServiceClient.CallType, HttpEntity, HttpResponse, int, int) method will be invoked.
    /// </summary>
    public sealed class SnSWebServiceCaller
    {
        /*******************************************************/
        /** ATTRIBUTES.
        /*******************************************************/
        private static volatile SnSWebServiceCaller _instance;
        private static readonly object SyncRoot = new Object();


        /*******************************************************/
        /** PROPERTIES.
        /*******************************************************/
        public RestClient Client { get; private set; }
        public RestRequest Request { get; private set; }


        /*******************************************************/
        /** METHODS.
        /*******************************************************/
        /// <summary>
        /// Private constructor according to the singleton pattern.
        /// </summary>
        private SnSWebServiceCaller(RestClient client, RestRequest request)
        {
            Client = clie
        }

        /// <summary>
        /// Function that returns the instance according to the singleton pattern.
        /// </summary>
        /// <returns></returns>
        public static SnSWebServiceCaller GetInstance()
        {
            if (_instance == null) 
            {
                lock (SyncRoot) 
                {
                    if (_instance == null) 
                        _instance = new SnSWebServiceCaller();
                }
            }

            return _instance;
        }

        /// <summary>
        /// Indicates if an Internet connectivity is available.
        /// </summary>
        /// <returns></returns>
        private bool IsConnected()
        {
            return DeviceNetworkInformation.IsNetworkAvailable;
        }

        public string PerformHttpRequest(Uri uri, SnSWebServiceVerbEnum verb, )


        /*******************************************************/
        /** ERROR CLASS.
        /*******************************************************/
    }
}
