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

using System;
using System.Windows.Media;

namespace ModernApp4Me.WP8.Download
{

    /// <summary>
    /// Responsible for downloading the bitmaps in dedicated threads.
    /// </summary>
    /// 
    /// <author>Ludovic ROLAND</author>
    /// <since>2014.03.24</since>
    public sealed class M4MBitmapDownloader
    {

        public enum BitmapDownloaderType
        {
            SystemImageCache, PersistentImageCache
        }

        public sealed class BitmapDownloaderConfiguration
        {

            public BitmapDownloaderType Type { get; set; }

            public string Name { get; set; }

            public TimeSpan ExpirationDelay { get; set; }

            public int MemoryCacheCapacity { get; set; }

        }

        private static volatile M4MBitmapDownloader instance;

        private static readonly object InstanceLock = new Object();

        private readonly ImageCache cache;

        public static BitmapDownloaderConfiguration Configuration { get; set; }


        private M4MBitmapDownloader()
        {
            if (Configuration.Type == BitmapDownloaderType.PersistentImageCache)
            {
                cache = new PersistentImageCache(Configuration.Name)
                {
                    ExpirationDelay = Configuration.ExpirationDelay,
                    MemoryCacheCapacity = Configuration.MemoryCacheCapacity
                };
            }
            else
            {
                cache = new SystemImageCache(Configuration.Name);
            }
        }

        public static M4MBitmapDownloader Instance
        {
            get
            {
                if (instance == null)
                {
                    lock (InstanceLock)
                    {
                        if (instance == null)
                        {
                            instance = new M4MBitmapDownloader();
                        }
                    }
                }

                return instance;
            }
        }

        /// <summary>
        /// Downloads and returns the <see cref="ImageSource"/>.
        /// </summary>
        /// <param name="imageUri">The URI of the image</param>
        /// <returns>The <see cref="ImageSource"/> that has been downloaded</returns>
        public ImageSource GetImage(string imageUri)
        {
            return cache.Get(imageUri);
        }

        /// <summary>
        /// Downloads the image to the cache.
        /// </summary>
        /// <param name="imageUri">The URI of the image</param>
        public void LoadImage(string imageUri)
        {
            cache.Get(imageUri);
        }

    }

}
