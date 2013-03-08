using System.Net;
using System.Threading.Tasks;
using wp4me.SnSDebugUtils;

namespace wp4me.SnSWebServiceUtils
{
    /// <summary>
    /// Class that provides an extension to the HttpWebResponse to wait asynchronous web service calling.
    /// </summary>
    public static class SnSHttpExtensions
    {
        public static Task<HttpWebResponse> GetResponseAsync(this HttpWebRequest request)
        {
            var taskComplete = new TaskCompletionSource<HttpWebResponse>();
            request.BeginGetResponse(asyncResponse =>
            {
                try
                {
                    var responseRequest = (HttpWebRequest)asyncResponse.AsyncState;
                    var someResponse = (HttpWebResponse)responseRequest.EndGetResponse(asyncResponse);
                    taskComplete.TrySetResult(someResponse);
                }
                catch (WebException e)
                {
                    SnSDebug.ConsoleWriteLine("GetResponseAsync(this HttpWebRequest request) : Task<HttpWebResponse>", e.StackTrace);
                    var failedResponse = (HttpWebResponse)e.Response;
                    taskComplete.TrySetResult(failedResponse);
                }
            }, request);

            return taskComplete.Task;
        }
    } 
}
