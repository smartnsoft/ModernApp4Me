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
