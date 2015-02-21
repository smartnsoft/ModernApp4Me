using System;
using System.Linq;
using Microsoft.Phone.Shell;

namespace ModernApp4Me.WP8.Tile
{

    /// <summary>
    /// Enables the manipulation of secondary tiles.
    /// </summary>
    /// 
    /// <author>Ludovic ROLAND</author>
    /// <since>2014.03.24</since>
    //TODO : manage correctly the exception
    public static class M4MSecondaryTileManager
    {

        /// <summary>
        /// Checks if a secondary tile at the specified URI already exists.
        /// </summary>
        /// <param name="navigationUri">the URI of the tile</param>
        /// <returns>true if secondary tile already exists, false otherwife.</returns>
        public static bool IsSecondaryTileExists(string navigationUri)
        {
            return ShellTile.ActiveTiles.FirstOrDefault(x => x.NavigationUri.ToString().Contains(navigationUri)) != null;
        }

        /// <summary>
        /// Tries to delete the secondary tile at the specified URI.
        /// </summary>
        /// <param name="navigationUri">the URI of the tile</param>
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
            catch (Exception)
            {
                //TODO : manage correctly the exception
            }

            return false;
        }

        /// <summary>
        /// Tries to create a secondary tile.
        /// </summary>
        /// <param name="tileData">the tile data</param>
        /// <param name="navigationUri">the URI of the tile</param>
        /// <returns>true in case of success, false otherwise</returns>
        public static bool CreateSecondaryTile(StandardTileData tileData, string navigationUri)
        {
            try
            {
                if (IsSecondaryTileExists(navigationUri) == false)
                {
                    ShellTile.Create(new Uri(navigationUri, UriKind.Relative), tileData);
                    return true;
                }
            }
            catch (Exception)
            {
                //TODO : manage correctly the exception
            }

            return false;
        }
    }
}
