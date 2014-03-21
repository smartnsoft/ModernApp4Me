using System;
using System.Linq;
using Microsoft.Phone.Shell;
using ModernApp4Me.Core.SnSLog;

namespace ModernApp4Me_WP8.SnSTile
{
    /// <summary>
    /// Provides functions to manipulate secondary tiles.
    /// </summary>
    public static class SnSSecondaryTile
    {
        /*******************************************************/
        /** METHODS.
        /*******************************************************/
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
        /// <returns></returns>
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
            catch (Exception e)
            {
                SnSLogger.Error(e.StackTrace, "SnSSecondaryTile", "DeleteSecondaryTile");
                return false;
            }

            return false;
        }

        /// <summary>
        /// Creates a secondary tile.
        /// </summary>
        /// <param name="tileData"></param>
        /// <param name="navigationUri"></param>
        /// <returns></returns>
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
            catch (Exception e)
            {
                SnSLogger.Error(e.StackTrace, "SnSSecondaryTile", "CreateSecondaryTile");
                return false;
            }

            return false;
        }
    }
}
