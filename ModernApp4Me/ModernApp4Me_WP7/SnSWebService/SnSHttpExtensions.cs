using System.Net;
using System.Threading.Tasks;
using ModernApp4Me_WP7.SnSLog;

namespace ModernApp4Me_WP7.SnSWebService
{
    /// <summary>
    /// Extension to the HttpWebResponse to wait asynchronous web service calling.
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
                    SnSLogger.Error(e.StackTrace, "SnSHttpExtensions", "GetResponseAsync");
                    var failedResponse = (HttpWebResponse)e.Response;
                    taskComplete.TrySetResult(failedResponse);
                }
            }, request);

            return taskComplete.Task;
        }
    } 
}
