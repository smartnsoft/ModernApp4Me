using System;
using System.IO;
using System.Net;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;
using ImageTools;
using ImageTools.IO;
using ImageTools.IO.Png;
using wp4me.SnSDebugUtils;
using wp4me.SnSIsolatedStorageUtils;

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
        /// <param name="userAgent"></param>
        /// <returns>the result as a string (more often XML or JSON)</returns>
        public async static Task<string> GetRequest(Uri uri, string userAgent = "default")
        {
            try
            {
                var request = (HttpWebRequest) WebRequest.Create(uri);
                request.Method = "GET";

                if (userAgent != "default")
                {
                    request.Headers["UserAgent"] += "|" + userAgent + "|";
                }

                var response = await request.GetResponseAsync();

                using (var sr = new StreamReader(response.GetResponseStream()))
                {
                    return sr.ReadToEnd();
                }
            }
            catch (Exception e)
            {
                SnSDebug.ConsoleWriteLine("GetRequest(Uri uri, string userAgent = \"default\") : Task<string>", e.StackTrace);
                return "";
            }

            /*var response = "";

            var request = WebRequest.CreateHttp(uri);

            if (userAgent != "default")
            {
                request.Headers["UserAgent"] = userAgent;
            }

            request.BeginGetResponse(r =>
            {
                var httpRequest = (HttpWebRequest) r.AsyncState;
                var httResponse = (HttpWebResponse) httpRequest.EndGetResponse(r);

                using (var streamReader = new StreamReader(httResponse.GetResponseStream()))
                {
                    response = streamReader.ReadToEnd();
                }
            }, request);

            return response;*/
        }

        /// <summary>
        /// Functions that downloads an image from the Internet.
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
                SnSDebug.ConsoleWriteLine("DownloadImage(Uri uri) : BitmapImage", e.StackTrace);
                return null;
            }
        }

        /// <summary>
        /// Functions that downloads and saves an image from the Internet.
        /// </summary>
        /// <param name="uri"></param>
        /// <param name="imageName"></param>
        /// <param name="fileMode"></param>
        public static void DownloadAndSaveImage(Uri uri, string imageName, FileMode fileMode)
        {
            try
            {
                Decoders.AddDecoder<PngDecoder>();
                var image = new ExtendedImage();
                image.LoadingCompleted += (sender, args) => SnSIsolatedStorageFile.WriteImageAsPng(imageName, sender as ExtendedImage, fileMode);
                image.UriSource = uri;
            }
            catch (Exception e)
            {
                SnSDebug.ConsoleWriteLine("DownloadAndSaveImage(Uri uri, string imageName, FileMode fileMode) : void", e.StackTrace);
            }
        }
    }
}
