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

namespace ModernApp4Me.WP8.Sample.Bo
{

    /// <author>Ludovic ROLAND</author>
    /// <since>2015.03.05</since>
    public sealed class Person
    {

        [JsonProperty("id")]
        public int Label { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("email")]
        public string Email { get; set; }

        [JsonProperty("age")]
        public int Age { get; set; }

        [JsonProperty("avatar")]
        public string AvatarUrl { get; set; }

        [JsonIgnore]
        public ImageSource Avatar
        {
            get
            {
                var image = M4MBitmapDownloader.Instance.GetImage(AvatarUrl) as BitmapImage;
                if (image != null)
                {
                    image.DecodePixelHeight = 100;
                    return image;
                }

                return null;
            }
        }

    }
}
