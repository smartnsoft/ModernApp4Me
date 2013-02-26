using System;
using System.IO;
using System.Net;

namespace wp4me.SnSWebServiceUtils
{
    /// <summary>
    /// Class that calls REST web services.
    /// </summary>
    public sealed class SnSRestWebService
    {
        /*******************************************************/
        /** METHODS AND FUNCTIONS.
        /*******************************************************/
        /// <summary>
        /// Function that calls a REST web service with the action GET.
        /// </summary>
        /// <param name="url"></param>
        /// <returns>the result as a string (more often XML or JSON)</returns>
        public static string GetRequest(string url)
        {
            string response = "";

            var request = (HttpWebRequest) WebRequest.CreateHttp(new Uri(url, UriKind.Absolute));

            request.BeginGetResponse(r =>
            {
                var httpRequest = (HttpWebRequest) r.AsyncState;
                var httResponse = (HttpWebResponse) httpRequest.EndGetResponse(r);

                using (var streamReader = new StreamReader(httResponse.GetResponseStream()))
                {
                    response = streamReader.ReadToEnd();
                }
            }, request);

            return response;
        }
    }
}
