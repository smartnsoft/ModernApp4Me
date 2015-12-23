﻿// Copyright (C) 2015 Smart&Soft SAS (http://www.smartnsoft.com/) and contributors
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

using System.Net;
using Windows.Networking.Connectivity;
using ModernApp4Me.Core.LifeCycle;
using ModernApp4Me.Core.WebService;

namespace ModernApp4Me.Universal.WebService
{

  /// <author>Ludovic ROLAND</author>
  /// <since>2015.05.07</since>
  /// <summary>
  /// A basis class for making web service calls easier which uses RestSharp.Portable on Windows Phone.
  /// When invoking an HTTP method, the caller goes through the following workflow:
  /// The <see cref="M4MWebServiceCaller.ExecuteHttpRequest"/> methods is invoked : if the response <see cref="HttpStatusCode"/> is OK, the body is returned ad a <see cref="string"/>.
  /// if the <see cref="HttpStatusCode"/> is not OK, the private method <see cref="M4MWebServiceCaller.OnHttpStatusCodeNotOk"/> is invoked.
  /// if the <see cref="HttpStatusCode"/> is NotFound and the <see cref="NetworkInformation.GetInternetConnectionProfile().GetNetworkConnectivityLevel()"/> is equals to <see cref="NetworkConnectivityLevel.None"/>, a <see cref="M4MConnectivityException"/> is thrown.
  /// Otherwife, a <see cref="M4MCallException"/> is thrown.
  /// </summary>
  public abstract class M4MWebServiceCaller : M4MBaseWebServiceCaller
  {

    /// <summary>
    /// Basis constructor.
    /// </summary>
    /// <param name="baseUrl">The base URL of the web service API</param>
    protected M4MWebServiceCaller(string baseUrl)
      : base(baseUrl)
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

      var internetConnectionProfile = NetworkInformation.GetInternetConnectionProfile();

      if ((statusCode == HttpStatusCode.NotFound || statusCode == HttpStatusCode.InternalServerError) && (internetConnectionProfile == null || internetConnectionProfile.GetNetworkConnectivityLevel() == NetworkConnectivityLevel.None))
      {
        throw new M4MConnectivityException(message);
      }

      throw new M4MCallException(message);
    }

  }

}
