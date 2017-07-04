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
