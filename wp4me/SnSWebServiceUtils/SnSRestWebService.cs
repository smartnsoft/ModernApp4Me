using System;
using System.IO;
using System.Net;
using System.Windows.Media.Imaging;
using wp4me.SnSDebugUtils;

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
        /// <param name="uri"></param>
        /// <returns>the result as a string (more often XML or JSON)</returns>
        public static string GetRequest(Uri uri)
        {
            var response = "";

            var request = WebRequest.CreateHttp(uri);

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

        /// <summary>
        /// Functions that download an image from the Internet.
        /// </summary>
        /// <param name="uri"></param>
        /// <returns>the image</returns>
        public static BitmapImage DownloadImage(Uri uri)
        {
            try
            {
                return new BitmapImage(uri);
            }
            catch (Exception e)
            {
                SnSDebug.ConsoleWriteLine(e.StackTrace);
                return null;
            }
        }
    }
}
