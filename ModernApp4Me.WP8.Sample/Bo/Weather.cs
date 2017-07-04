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
