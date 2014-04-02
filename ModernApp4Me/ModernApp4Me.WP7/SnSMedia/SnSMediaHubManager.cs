using System.IO;
using Microsoft.Devices;

namespace ModernApp4Me.WP7.SnSMedia
{

    /// <summary>
    /// Class to manage the music+video hub.
    /// </summary>
    /// <author>Ludovic ROLAND</author>
    /// <since>2014.04.02</since>
    public static class SnSMediaHubManager
    {

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
