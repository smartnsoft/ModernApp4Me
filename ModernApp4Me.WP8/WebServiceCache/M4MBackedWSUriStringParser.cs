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
using System.Globalization;
using System.Threading.Tasks;
using ModernApp4Me.Core.WebService;
using Q42.WinRT.Data;
using ModernApp4Me.Core.WebServiceCache;

namespace ModernApp4Me.WP8.WebServiceCache
{

    /// <summary>
    /// A basis class for managing cache data, making web service calls and parsing json response which uses RestSharp.Portable, Q42 and JSON.NET third party libraries.
    /// </summary>
    /// 
    /// <author>Ludovic ROLAND</author>
    /// <since>2015.02.24</since>
    public abstract class M4MBackedWSUriStringParser<TResult, TParameter, TWebServiceCaller> : M4MBaseBackedWSUriStringParser<TResult, TParameter, TWebServiceCaller> where TWebServiceCaller : M4MBaseWebServiceCaller
    {

        public override async Task<TResult> GetRetentionValue(bool fromCache, DateTime expirationDelay, TParameter parameter)
        {
            var restRequest = ComputeRestRequest(parameter);
            var response = await DataCache.GetAsync(restRequest.GetHashCode().ToString(CultureInfo.InvariantCulture), () => WebServiceCaller.ExecuteHttpRequest(restRequest), expirationDelay, fromCache == false);
            return Parse(response);
        }

    }

}
