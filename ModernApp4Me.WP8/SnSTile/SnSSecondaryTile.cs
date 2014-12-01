using System;
using System.Linq;
using Microsoft.Phone.Shell;
using ModernApp4Me.Core.SnSLog;

namespace ModernApp4Me.WP8.SnSTile
{

    /// <summary>
    /// Provides functions to manipulate secondary tiles.
    /// </summary>
    /// <author>Ludovic ROLAND</author>
    /// <since>2014.03.24</since>
    public static class SnSSecondaryTile
    {

        /// <summary>
        /// Checks is a secondary tile exists.
        /// </summary>
        /// <param name="navigationUri"></param>
        /// <returns></returns>
        public static bool IsSecondaryTileExists(string navigationUri)
        {
            return ShellTile.ActiveTiles.FirstOrDefault(x => x.NavigationUri.ToString().Contains(navigationUri)) != null;
        }

        /// <summary>
        /// Deletes a secondary tile.
        /// </summary>
        /// <param name="navigationUri"></param>
        /// <returns>true in case of success, false otherwise</returns>
        public static bool DeleteSecondaryTile(string navigationUri)
        {
            try
            {
                var tile = ShellTile.ActiveTiles.FirstOrDefault(x => x.NavigationUri.ToString().Contains(navigationUri));

                if (tile != null)
                {
                    tile.Delete();
                    return true;
                }
            }
            catch (Exception exception)
            {
                SnSLoggerWrapper.Instance.Logger.Warn("Cannot remove the tile with the navigation URI '" + navigationUri + "'", exception);
            }

            return false;
        }

        /// <summary>
        /// Creates a secondary tile.
        /// </summary>
        /// <param name="tileData"></param>
        /// <param name="navigationUri"></param>
        /// <returns>true in case of success, false otherwise</returns>
        public static bool CreateSecondaryTile(StandardTileData tileData, string navigationUri)
        {
            try
            {
                if (!IsSecondaryTileExists(navigationUri))
                {
                    ShellTile.Create(new Uri(navigationUri, UriKind.Relative), tileData);
                    return true;
                }
            }
            catch (Exception exception)
            {
                SnSLoggerWrapper.Instance.Logger.Warn("Cannot create the tile with the navigation URI '" + navigationUri + "'", exception);
            }

            return false;
        }
    }
}
