// Copyright (C) 2015 Smart&Soft SAS (http://www.smartnsoft.com/) and contributors
//
// This library is free software; you can redistribute it and/or
// modify it under the terms of the GNU Lesser General Public
// License as published by the Free Software Foundation; either
// version 3 of the License, or (at your option) any later version.
//
// This library is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE. See the GNU
// Lesser General Public License for more details.
//
// Contributors:
//   Smart&Soft - initial API and implementation

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

    public async Task<TResult> GetValue(TParameter parameter)
    {
      return await GetRetentionValue(false, DateTime.Now, parameter);
    }

    protected virtual TResult DeserializeObject(byte[] response)
    {
      return JsonConvert.DeserializeObject<TResult>(Encoding.UTF8.GetString(response, 0, response.Length));
    }

  }

}
