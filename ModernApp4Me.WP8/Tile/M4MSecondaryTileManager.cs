using System;
using System.Linq;
using Microsoft.Phone.Shell;
using ModernApp4Me.WP8.Log;

namespace ModernApp4Me.WP8.Tile
{

    /// <summary>
    /// Enables the manipulation of secondary tiles.
    /// Tread-safe
    /// </summary>
    /// 
    /// <author>Ludovic ROLAND</author>
    /// <since>2014.03.24</since>
    public static class M4MSecondaryTileManager
    {

        private static readonly object InstanceLock = new Object();

        /// <summary>
        /// Checks if a secondary tile at the specified URI already exists.
        /// </summary>
        /// <param name="navigationUri">the URI of the tile</param>
        /// <returns>true if secondary tile already exists, false otherwife.</returns>
        public static bool IsExist(string navigationUri)
        {
            lock (InstanceLock)
            {
                if (M4MModernLogger.Instance.IsDebugEnabled() == true)
                {
                    M4MModernLogger.Instance.Debug("Checking if a secondary tile with the uri '" + navigationUri + "' exists");
                }

                try
                {
                    return ShellTile.ActiveTiles.FirstOrDefault(x => x.NavigationUri.ToString().Contains(navigationUri)) != null;
                }
                catch (Exception exception)
                {
                    if (M4MModernLogger.Instance.IsErrorEnabled() == true)
                    {
                        M4MModernLogger.Instance.Error("An error occurs while checking if a secondary tile with the uri : '" + navigationUri + "' exists", exception);
                    }

                    return false;
                }
            }
        }

        /// <summary>
        /// Tries to delete the secondary tile at the specified URI.
        /// </summary>
        /// <param name="navigationUri">the URI of the tile</param>
        /// <returns>true in case of success, false otherwise</returns>
        public static bool Delete(string navigationUri)
        {
            lock (InstanceLock)
            {
                if (M4MModernLogger.Instance.IsDebugEnabled() == true)
                {
                    M4MModernLogger.Instance.Debug("Deleting the secondary tile with the uri '" + navigationUri + "'");
                }

                try
                {
                    var tile = ShellTile.ActiveTiles.FirstOrDefault(x => x.NavigationUri.ToString().Contains(navigationUri));

                    if (tile != null)
                    {
                        tile.Delete();

                        return true;
                    }
                    else
                    {
                        if (M4MModernLogger.Instance.IsWarnEnabled() == true)
                        {
                            M4MModernLogger.Instance.Warn("Cannot find the secondary tile with the uri : '" + navigationUri + "'");
                        }

                        return false;
                    }
                }
                catch (Exception exception)
                {
                    if (M4MModernLogger.Instance.IsErrorEnabled() == true)
                    {
                        M4MModernLogger.Instance.Error("An error occurs while deleting the secondary tile with the uri : '" + navigationUri + "'", exception);
                    }

                    return false;
                }
                
            }
        }

        /// <summary>
        /// Tries to create a secondary tile.
        /// </summary>
        /// <param name="tileData">the tile data</param>
        /// <param name="navigationUri">the URI of the tile</param>
        /// <returns>true in case of success, false otherwise</returns>
        public static bool Create(StandardTileData tileData, string navigationUri)
        {
            lock (InstanceLock)
            {
                if (M4MModernLogger.Instance.IsDebugEnabled() == true)
                {
                    M4MModernLogger.Instance.Debug("Creating a secondary tile with the uri '" + navigationUri + "'");
                }

                try
                {
                    if (IsExist(navigationUri) == false)
                    {
                        ShellTile.Create(new Uri(navigationUri, UriKind.Relative), tileData);

                        return true;
                    }
                    else
                    {
                        if (M4MModernLogger.Instance.IsWarnEnabled() == true)
                        {
                            M4MModernLogger.Instance.Warn("Cannot create the secondary tile with the uri : '" + navigationUri + "'");
                        }

                        return false;
                    }
                }
                catch (Exception exception)
                {
                    if (M4MModernLogger.Instance.IsErrorEnabled() == true)
                    {
                        M4MModernLogger.Instance.Error("An error occurs while creating the secondary tile with the uri : '" + navigationUri + "'", exception);
                    }

                    return false;
                }
            }
        }
    }
}
