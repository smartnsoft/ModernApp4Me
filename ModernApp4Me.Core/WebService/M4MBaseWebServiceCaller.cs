// The MIT License (MIT)
//
// Copyright (c) 2017 Smart&Soft
//
// Permission is hereby granted, free of charge, to any person obtaining a copy
// of this software and associated documentation files (the "Software"), to deal
// in the Software without restriction, including without limitation the rights
// to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
// copies of the Software, and to permit persons to whom the Software is
// furnished to do so, subject to the following conditions:
//
// The above copyright notice and this permission notice shall be included in all
// copies or substantial portions of the Software.
//
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
// IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
// FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
// AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
// LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
// OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
// SOFTWARE.

using System.Net;
using System.Threading.Tasks;
using System;
using RestSharp.Portable;
using RestSharp.Portable.WebRequest;

namespace ModernApp4Me.Core.WebService
{

  /// <author>Ludovic ROLAND</author>
  /// <since>2015.02.24</since>
  /// <summary>
  /// A basis class for making web service calls easier which uses RestSharp.Portable.
  /// When invoking an HTTP method, the caller goes through the following workflow:
  /// The <see cref="M4MBaseWebServiceCaller.ExecuteHttpRequest"/> methods is invoked : if the response <see cref="HttpStatusCode"/> is OK, the body is returned ad a <see cref="string"/>.
  /// if the <see cref="HttpStatusCode"/> is not OK, the private method <see cref="M4MBaseWebServiceCaller.OnHttpStatusCodeNotOk"/> is invoked.
  /// </summary>
  public abstract class M4MBaseWebServiceCaller
  {

    private const int ATTEMPTS_COUNT_MAX = 2;

    protected RestClient client;

    /// <summary>
    /// Basis constructor.
    /// </summary>
    /// <param name="baseUrl">The base URL of the web service API</param>
    protected M4MBaseWebServiceCaller(string baseUrl)
    {
        client = new RestClient(baseUrl) { IgnoreResponseStatusCode = true };
    }

    /// <summary>
    /// Executes a call to a web method without any persistence.
    /// </summary>
    /// <param name="restRequest">the <see cref="RestRequest"/></param>
    /// <param name="attempsCount">the current attemp number</param>
    /// <returns>The content of the </returns>
    public virtual async Task<Tuple<byte[], double>> ExecuteHttpRequest(RestRequest restRequest, int attempsCount = 0)
    {
      attempsCount++;
      var callType = restRequest.Method.ToString();
      var resource = client.BuildUri(restRequest).AbsoluteUri;

      Debug("Running the HTTP '" + callType + "' request '" + resource + "'");

      var start = TimeSpan.FromTicks(DateTime.Now.Ticks);
      var rawResponse = await client.Execute(restRequest);
      var end = TimeSpan.FromTicks(DateTime.Now.Ticks);
      var responseTimeInMs = (end - start).TotalMilliseconds; 
      var data = rawResponse.RawBytes;
      var tuple = new Tuple<byte[], double>(data, responseTimeInMs);
      var statusCode = rawResponse.StatusCode;

      Debug("The call to the HTTP " + callType + " request '" + resource + "' took " + responseTimeInMs + " ms and returned the status code '" + statusCode + "'");

      if (statusCode != HttpStatusCode.OK)
      {
        if (attempsCount < M4MBaseWebServiceCaller.ATTEMPTS_COUNT_MAX)
        {
          tuple = await ExecuteHttpRequest(restRequest, attempsCount);
        }
        else
        {
          OnHttpStatusCodeNotOk(resource, statusCode);
        }
      }

      return tuple;
    }

    protected abstract void OnHttpStatusCodeNotOk(string resource, HttpStatusCode statusCode);

    protected abstract void Debug(string message);

    protected abstract void Error(string message);

  }

}
