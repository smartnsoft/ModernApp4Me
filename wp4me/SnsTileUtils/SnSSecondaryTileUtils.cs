using System;
using System.Linq;
using Microsoft.Phone.Shell;
using wp4me.SnsDebugUtils;

namespace wp4me.SnsTileUtils
{
    /// <summary>
    /// Class that provides functions to manipulate secondary tiles.
    /// </summary>
    public sealed class SnSSecondaryTileUtils
    {
        /// <summary>
        /// Check is a secondary tile exists.
        /// </summary>
        /// <param name="navigationUri"></param>
        /// <returns>bool</returns>
        public static bool IsSecondaryTileExists(string navigationUri)
        {
            return ShellTile.ActiveTiles.FirstOrDefault(x => x.NavigationUri.ToString().Contains(navigationUri)) != null;
        }

        /// <summary>
        /// Delete a secondary tile.
        /// </summary>
        /// <param name="navigationUri"></param>
        /// <returns>true in case of success</returns>
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
                SnSDebugUtils.ConsoleWriteLine(e.StackTrace);
                return false;
            }

            return false;
        }

        /// <summary>
        /// Create a secondary tile.
        /// </summary>
        /// <param name="tileData"></param>
        /// <param name="navigationUri"></param>
        /// <returns>true in case of success</returns>
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
                SnSDebugUtils.ConsoleWriteLine(e.StackTrace);
                return false;
            }

            return false;
        }
    }
}
