using System.Threading.Tasks;
using RestSharp;

namespace ModernApp4Me_WP7.SnSWebService
{
    /// <summary>
    /// Extension fot the RestClient class to avoid callback.
    /// </summary>
    public static class SnSWebServiceExtensions
    {
        public static Task<IRestResponse> ExecuteTaskAsync(this RestClient client, RestRequest request)
        {
            var taskComplete = new TaskCompletionSource<IRestResponse>();

            client.ExecuteAsync(request, (response) =>
            {
                if (response.ErrorException != null)
                {
                    taskComplete.TrySetException(response.ErrorException);
                }
                else
                {
                    taskComplete.TrySetResult(response);
                }
            });

            return taskComplete.Task;
        }

        public static Task<IRestResponse<T>> ExecuteTaskAsync<T>(this RestClient client, RestRequest request) where T : new()
        {
            var taskComplete = new TaskCompletionSource<IRestResponse<T>>();

            client.ExecuteAsync<T>(request, response =>
            {
                if (response.ErrorException != null)
                {
                    taskComplete.TrySetException(response.ErrorException);
                }
                else
                {
                    taskComplete.TrySetResult(response);
                }
            });

            return taskComplete.Task;
        }
    }
}
