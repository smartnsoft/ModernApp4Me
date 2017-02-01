// Copyright (C) 2016 Smart&Soft SAS (http://www.smartnsoft.com/) and contributors
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

using Windows.Networking.Connectivity;

namespace ModernApp4Me.Universal.Metadata
{

  /// <author>Guillaume Bonnin</author>
  /// <since>2016.10.31</since>
  /// <summary>
  /// Retrieves informations about current connection of the application
  /// </summary>
  public sealed class M4MReachability
  {

    public enum InternetConnectionType
    {
      None,
      Wifi,
      Cellular4G,
      Cellular3G,
      Cellular2G,
      Other // Other internet connections (Ethernet, Unknown cellular types etc.)
    }

    public static bool HasInternetConnection()
    {
      return (M4MReachability.GetCurrentConnectionType() == InternetConnectionType.None ? false : true);
    }

    public static bool HasWifiConnection()
    {
      return (M4MReachability.GetCurrentConnectionType() == InternetConnectionType.Wifi ? true : false);
    }

    public static bool HasCellularConnection()
    {
      var connectionType = M4MReachability.GetCurrentConnectionType();

      switch (connectionType)
      {
        case InternetConnectionType.Cellular4G:
        case InternetConnectionType.Cellular3G:
        case InternetConnectionType.Cellular2G:
          return true;
        default:
          return false;
      }
    }

    public static InternetConnectionType GetCurrentConnectionType()
    {
      try
      {
        var connectionProfile = NetworkInformation.GetInternetConnectionProfile();

        if (connectionProfile.IsWlanConnectionProfile == true)
        {
          return InternetConnectionType.Wifi;
        }
        else if (connectionProfile.IsWwanConnectionProfile == true)
        {
          var wwanDetails = connectionProfile.WwanConnectionProfileDetails.GetCurrentDataClass();

          switch (wwanDetails)
          {
            case WwanDataClass.Edge:
            case WwanDataClass.Gprs:
              return InternetConnectionType.Cellular2G;
            case WwanDataClass.Cdma1xEvdo:
            case WwanDataClass.Cdma1xEvdoRevA:
            case WwanDataClass.Cdma1xEvdoRevB:
            case WwanDataClass.Cdma1xEvdv:
            case WwanDataClass.Cdma1xRtt:
            case WwanDataClass.Cdma3xRtt:
            case WwanDataClass.CdmaUmb:
            case WwanDataClass.Umts:
            case WwanDataClass.Hsdpa:
            case WwanDataClass.Hsupa:
            case WwanDataClass.Hsdpa | WwanDataClass.Hsupa:
              return InternetConnectionType.Cellular3G;
            case WwanDataClass.LteAdvanced:
              return InternetConnectionType.Cellular4G;
            case WwanDataClass.None:
              return InternetConnectionType.None;
            case WwanDataClass.Custom:
            default:
              break;
          }
        }

        return (connectionProfile.GetNetworkConnectivityLevel() == NetworkConnectivityLevel.InternetAccess ? InternetConnectionType.Other : InternetConnectionType.None);
      }
      catch
      {
        return InternetConnectionType.None;
      }
    }

    public static string GetCurrentConnectionString()
    {
      var connectionType = M4MReachability.GetCurrentConnectionType();

      switch (connectionType)
      {
        case InternetConnectionType.Cellular2G:
          return "Cellular (2G)";
        case InternetConnectionType.Cellular3G:
          return "Cellular (3G)";
        case InternetConnectionType.Cellular4G:
          return "Cellular (4G)";
        case InternetConnectionType.Wifi:
          return "WiFi";
        case InternetConnectionType.Other:
          return "Other internet connection";
        case InternetConnectionType.None:
        default:
          return "No connection";
      }
    }

  }

}
