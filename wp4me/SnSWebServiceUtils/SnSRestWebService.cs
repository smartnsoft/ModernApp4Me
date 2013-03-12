using System;
using System.IO;
using System.Net;
using System.Threading.Tasks;
using System.Windows;
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
                    request.UserAgent += userAgent;
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
        }

        /// <summary>
        /// Function that calls a REST web service with the action GET.
        /// </summary>
        /// <param name="uri"></param>
        /// <param name="userAgent"></param>
        /// <returns>the result as a bitmap image</returns>
        public async static Task<BitmapImage> GetAsyncRequestImage(Uri uri, string userAgent = "default")
        {
            try
            {
                var request = (HttpWebRequest) WebRequest.Create(uri);
                request.Method = "GET";

                if (userAgent != "default")
                {
                    request.UserAgent += userAgent;
                }

                var response = await request.GetResponseAsync();

                using (var sr = response.GetResponseStream())
                {
                    var image = new BitmapImage();
                    image.SetSource(sr);
                    return image;
                }
            }
            catch (Exception e)
            {
                SnSDebug.ConsoleWriteLine("GetAsyncRequestImage(Uri uri, string userAgent = \"default\") : Task<string>", e.StackTrace);
                SnSDebug.ConsoleWriteLine("GetAsyncRequestImage(Uri uri, string userAgent = \"default\") : Task<string>", e.Message);
                return null;
            }
        }

        /// <summary>
        /// Function that calls a REST web service with the action GET.
        /// </summary>
        /// <param name="uri"></param>
        /// <param name="userAgent"></param>
        /// <returns>the result as a bitmap image</returns>
        public static BitmapImage GetRequestImage(Uri uri, string userAgent = "default")
        {
            try
            {
                var image = new BitmapImage();

                Deployment.Current.Dispatcher.BeginInvoke(() =>
                    {
                        var request = (HttpWebRequest) WebRequest.Create(uri);
                        request.Method = "GET";

                        if (userAgent != "default")
                        {
                            request.UserAgent += userAgent;
                        }

                        request.BeginGetResponse(result =>
                        {
                            using (var sr = request.EndGetResponse(result))
                            {
                                image.SetSource(sr.GetResponseStream());
                            }
                        }, null);

                        
                    });

                return image;
            }
            catch (Exception e)
            {
                SnSDebug.ConsoleWriteLine("GetRequestImage(Uri uri, string userAgent = \"default\") : Task<string>", e.StackTrace);
                SnSDebug.ConsoleWriteLine("GetRequestImage(Uri uri, string userAgent = \"default\") : Task<string>", e.Message);
                return null;
            }
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
