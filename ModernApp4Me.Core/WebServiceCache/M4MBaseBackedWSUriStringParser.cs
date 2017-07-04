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
using System.Threading.Tasks;
using Newtonsoft.Json;
using ModernApp4Me.Core.WebService;
using System.Text;
using RestSharp.Portable;

namespace ModernApp4Me.Core.WebServiceCache
{

  /// <summary>
  /// A basis class for managing cache data, making web service calls and parsing json response which uses RestSharp.Portable and JSON.NET third party libraries.
  /// </summary>
  /// 
  /// <author>Ludovic ROLAND</author>
  /// <since>2015.02.24</since>
  public abstract class M4MBaseBackedWSUriStringParser<TResult, TParameter, TWebServiceCaller> where TWebServiceCaller : M4MBaseWebServiceCaller
  {

    public TWebServiceCaller WebServiceCaller { get; set; }

    protected abstract RestRequest ComputeRestRequest(TParameter parameter);

    protected abstract TResult Parse(byte[] response);

    public abstract Task<TResult> GetRetentionValue(bool fromCache, DateTime expirationDelay, TParameter parameter);

    public abstract Task<M4MInfo<TResult>> GetRetentionInfoValue(bool fromCache, DateTime expirationDelay, TParameter parameter);

    public async Task<TResult> GetValue(TParameter parameter)
    {
      return await GetRetentionValue(false, DateTime.Now, parameter);
    }

    public async Task<M4MInfo<TResult>> GetInfoValue(TParameter parameter)
    {
      return await GetRetentionInfoValue(false, DateTime.Now, parameter);
    }

    protected virtual TResult DeserializeObject(byte[] response)
    {
      return JsonConvert.DeserializeObject<TResult>(Encoding.UTF8.GetString(response, 0, response.Length));
    }

  }

}
