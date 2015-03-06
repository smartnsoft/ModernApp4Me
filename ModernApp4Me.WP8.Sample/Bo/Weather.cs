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

using System.Windows.Media;
using System.Windows.Media.Imaging;
using ModernApp4Me.WP8.Download;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace ModernApp4Me.WP8.Sample.Bo
{

    /// <author>Ludovic ROLAND</author>
    /// <since>2015.03.05</since>
    public sealed class Weather
    {

        public sealed class Forecast
        {

            public sealed class Information
            {

                [JsonProperty("main")]
                public string Main { get; set; }

                [JsonProperty("description")]
                public string Description { get; set; }

                [JsonProperty("icon")]
                public string IconUrl { get; set; }

                [JsonIgnore]
                public ImageSource Icon
                {
                    get
                    {
                        var image = M4MBitmapDownloader.Instance.GetImage("http://openweathermap.org/img/w/" + IconUrl + ".png") as BitmapImage;
                        
                        if (image != null)
                        {
                            image.DecodePixelHeight = 50;
                        }

                        return image;
                    }
                }

            }

            [JsonProperty("dt_txt")]
            public string DateText { get; set; }

            [JsonProperty("weather")]
            public List<Information> Weathers { get; set; }

            [JsonIgnore]
            public Information FirstWeather
            {
                get
                {
                    return Weathers[0];
                }
            }

        }

        [JsonProperty("list")]
        public List<Forecast> Forecasts { get; set; }

    }
}
