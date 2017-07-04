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

using System;
using System.Globalization;
using System.Threading.Tasks;
using ModernApp4Me.Core.WebService;
using ModernApp4Me.Core.WebServiceCache;
using Q42.WinRT.Data;

namespace ModernApp4Me.Universal.WebServiceCache
{

  /// <author>Ludovic ROLAND</author>
  /// <since>2015.05.07</since>
  /// <summary>
  /// A basis class for managing cache data, making web service calls and parsing json response which uses RestSharp.Portable, Q42 and JSON.NET third party libraries.
  /// </summary>
  public abstract class M4MBackedWSUriStringParser<TResult, TParameter, TWebServiceCaller> : M4MBaseBackedWSUriStringParser<TResult, TParameter, TWebServiceCaller> where TWebServiceCaller : M4MBaseWebServiceCaller
  {

    public override async Task<TResult> GetRetentionValue(bool fromCache, DateTime expirationDelay, TParameter parameter)
    {
      var restRequest = ComputeRestRequest(parameter);
      var response = await DataCache.GetAsync(restRequest.GetHashCode().ToString(CultureInfo.InvariantCulture), () => WebServiceCaller.ExecuteHttpRequest(restRequest), expirationDelay, fromCache == false);
      return Parse(response.Item1);
    }

    public override async Task<M4MInfo<TResult>> GetRetentionInfoValue(bool fromCache, DateTime expirationDelay, TParameter parameter)
    {
      var restRequest = ComputeRestRequest(parameter);
      var response = await DataCache.GetAsync(restRequest.GetHashCode().ToString(CultureInfo.InvariantCulture), () => WebServiceCaller.ExecuteHttpRequest(restRequest), expirationDelay, fromCache == false);
      var businessObject = Parse(response.Item1);
      return new M4MInfo<TResult>() { Value = businessObject, ResponseTime = response.Item2 };
    }

  }

}
