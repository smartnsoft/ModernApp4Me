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

using System;
using System.Linq;
using Microsoft.Phone.Shell;

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
                try
                {
                    return ShellTile.ActiveTiles.FirstOrDefault(x => x.NavigationUri.ToString().Contains(navigationUri)) != null;
                }
                catch (Exception)
                {
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
                try
                {
                    var tile = ShellTile.ActiveTiles.FirstOrDefault(x => x.NavigationUri.ToString().Contains(navigationUri));

                    if (tile != null)
                    {
                        tile.Delete();

                        return true;
                    }
                    
                    return false;
                }
                catch (Exception)
                {
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
                try
                {
                    if (IsExist(navigationUri) == false)
                    {
                        ShellTile.Create(new Uri(navigationUri, UriKind.Relative), tileData);

                        return true;
                    }
                    
                    return false;
                }
                catch (Exception)
                {
                    return false;
                }
            }
        }
    }
}
