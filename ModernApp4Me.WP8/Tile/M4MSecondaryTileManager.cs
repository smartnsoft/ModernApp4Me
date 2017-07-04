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
