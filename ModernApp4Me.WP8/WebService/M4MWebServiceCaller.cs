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
using Microsoft.Phone.Net.NetworkInformation;
using ModernApp4Me.Core.LifeCycle;
using ModernApp4Me.Core.WebService;

namespace ModernApp4Me.WP8.WebService
{

  /// <summary>
  /// A basis class for making web service calls easier which uses RestSharp.Portable on Windows Phone.
  /// When invoking an HTTP method, the caller goes through the following workflow:
  /// The <see cref="M4MWebServiceCaller.ExecuteHttpRequest()"/> methods is invoked : if the response <see cref="HttpStatusCode"/> is OK, the body is returned ad a <see cref="string"/>.
  /// if the <see cref="HttpStatusCode"/> is not OK, the private method <see cref="M4MWebServiceCaller.OnHttpStatusCodeNotOk()"/> is invoked.
  /// if the <see cref="HttpStatusCode"/> is NotFound and the <see cref="DeviceNetworkInformation.IsNetworkAvailable"/> is false, a <see cref="M4MConnectivityException"/> is thrown.
  /// Otherwife, a <see cref="M4MCallException"/> is thrown.
  /// </summary>
  /// 
  /// <author>Ludovic ROLAND</author>
  /// <since>2014.03.21</since>
  public abstract class M4MWebServiceCaller : M4MBaseWebServiceCaller
  {

    /// <summary>
    /// Basis constructor.
    /// </summary>
    /// <param name="baseUrl">The base URL of the web service API</param>
    protected M4MWebServiceCaller(string baseUrl) : base(baseUrl)
    {
    }

    /// <summary>
    /// Private function that raises an exception when the result code to a web method id not OK (not 20X).
    /// </summary>
    /// <param name="resource">The uri</param>
    /// <param name="statusCode">the <see cref="HttpStatusCode"/></param>
    protected override void OnHttpStatusCodeNotOk(string resource, HttpStatusCode statusCode)
    {
      var message = "The error code of the call to the web method '" + resource +
                    "' is not OK (not 20X). Status: '" + statusCode + "'";

      if (statusCode == HttpStatusCode.NotFound && NetworkInterface.GetIsNetworkAvailable() == false)
      {
        throw new M4MConnectivityException(message);
      }

      throw new M4MCallException(message);
    }

  }

}
