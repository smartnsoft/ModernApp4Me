// Copyright (C) 2015 Smart&Soft SAS (http://www.smartnsoft.com/) and contributors
//
// This library is free software; you can redistribute it and/or
// modify it under the terms of the GNU Lesser General Public
// License as published by the Free Software Foundation; either
// version 2.1 of the License, or (at your option) any later version.
//
// This library is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE. See the GNU
// Lesser General Public License for more details.
//
// Contributors:
//   Smart&Soft - initial API and implementation

using System.IO;
using Microsoft.Devices;

namespace ModernApp4Me.WP8.Media
{

    /// <summary>
    /// Enables to manage the music+video Windows Phone Hub.
    /// </summary>
    /// 
    /// <author>Ludovic ROLAND</author>
    /// <since>2014.04.02</since>
    public static class M4MMediaHubManager
    {

        /// <summary>
        /// Updates the now playing Tile of the history list.
        /// </summary>
        /// <param name="imageStream">the <see cref="Stream"/> the image that is displayed with this MediaHistoryItem in the media history area</param>
        /// <param name="source">this <see cref="string"/> is not supported</param>
        /// <param name="title">the title of the <see cref="MediaHistoryItem"/></param>
        /// <param name="contextKey">the key for the <see cref="PlayerContext"/></param>
        /// <param name="contextValue">the value for the <see cref="PlayerContext"/></param>
        public static void UpdateNowPlayingTile(Stream imageStream, string source, string title, string contextKey, string contextValue)
        {
            var item = new MediaHistoryItem()
            {
                ImageStream = imageStream,
                Source = source,
                Title = title
            };

            item.PlayerContext.Add(contextKey, contextValue);

            MediaHistory.Instance.NowPlaying = item;
        }

        /// <summary>
        /// Updates the history list.
        /// </summary>
        /// <param name="imageStream">the <see cref="Stream"/> the image that is displayed with this MediaHistoryItem in the media history area</param>
        /// <param name="source">this <see cref="string"/> is not supported</param>
        /// <param name="title">the title of the <see cref="MediaHistoryItem"/></param>
        /// <param name="contextKey">the key for the <see cref="PlayerContext"/></param>
        /// <param name="contextValue">the value for the <see cref="PlayerContext"/></param>
        public static void AddRecentPlayTile(Stream imageStream, string source, string title, string contextKey, string contextValue)
        {
            var item = new MediaHistoryItem()
            {
                ImageStream = imageStream,
                Source = source,
                Title = title
            };

            item.PlayerContext.Add(contextKey, contextValue);

            MediaHistory.Instance.WriteRecentPlay(item);
        }

    }

}
