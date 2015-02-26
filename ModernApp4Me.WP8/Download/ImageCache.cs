// Copyright 2010 Andreas Saudemont (andreas.saudemont@gmail.com)
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
//   Andreas Saudemont (andreas.saudemont@gmail.com)
//
// Taken from : https://kawagoe.codeplex.com/

using System;
using System.Windows;
using System.Windows.Media;

namespace ModernApp4Me.WP8.Download
{
    /// <summary>
    /// Defines the base clase for image cache implementations.
    /// </summary>
    public abstract class ImageCache
    {
        /// <summary>
        /// The name of the default image cache.
        /// </summary>
        public const string DefaultImageCacheName = "default";

        private static ImageCache _defaultImageCache = null;
        private static object _defaultImageCacheLock = new object();

        /// <summary>
        /// The default image cache.
        /// If not set explicitely, a <see cref="PersistentImageCache"/> instance is used by default.
        /// </summary>
        public static ImageCache Default
        {
            get
            {
                if (!Deployment.Current.Dispatcher.CheckAccess())
                {
                    throw new UnauthorizedAccessException("invalid cross-thread access");
                }
                lock (_defaultImageCacheLock)
                {
                    if (_defaultImageCache == null)
                    {
                        _defaultImageCache = new PersistentImageCache(DefaultImageCacheName);
                    }
                    return _defaultImageCache;
                }
            }
            set
            {
                if (!Deployment.Current.Dispatcher.CheckAccess())
                {
                    throw new UnauthorizedAccessException("invalid cross-thread access");
                }
                lock (_defaultImageCacheLock)
                {
                    _defaultImageCache = value;
                }
            }
        }

        /// <summary>
        /// Initializes a new <see cref="ImageCache"/> instance.
        /// </summary>
        protected ImageCache(string name)
        {
            if (string.IsNullOrEmpty(name))
            {
                throw new ArgumentException();
            }
            Name = name;
        }

        /// <summary>
        /// The name of the image cache.
        /// </summary>
        protected string Name
        {
            get;
            private set;
        }

        /// <summary>
        /// Retrieves the source for the image with the specified URI from the cache, downloading it
        /// if needed.
        /// </summary>
        /// <param name="imageUri">The URI of the image. Must be an absolute URI.</param>
        /// <returns>An ImageSource object, or <c>null</c> if <paramref name="imageUri"/> is <c>null</c> or not an absolute URI.</returns>
        /// <exception cref="UnauthorizedAccessException">The method is not called in the UI thread.</exception>
        public ImageSource Get(Uri imageUri)
        {
            if (!Deployment.Current.Dispatcher.CheckAccess())
            {
                throw new UnauthorizedAccessException("invalid cross-thread access");
            }
            if (imageUri == null || !imageUri.IsAbsoluteUri)
            {
                return null;
            }
            return GetInternal(imageUri);
        }

        /// <summary>
        /// Retrieves the source for the image with the specified URI from the cache, downloading it
        /// if needed.
        /// </summary>
        /// <param name="imageUriString">The URI of the image. Must be an absolute URI.</param>
        /// <returns>An ImageSource object, or <c>null</c> if <paramref name="imageUriString"/> is <c>null</c>,
        /// the empty string, or not an absolute URI.</returns>
        /// <exception cref="UnauthorizedAccessException">The method is not called in the UI thread.</exception>
        public ImageSource Get(string imageUriString)
        {
            if (!Deployment.Current.Dispatcher.CheckAccess())
            {
                throw new UnauthorizedAccessException("invalid cross-thread access");
            }
            if (string.IsNullOrEmpty(imageUriString))
            {
                return null;
            }
            Uri imageUri;
            try
            {
                imageUri = new Uri(imageUriString, UriKind.Absolute);
            }
            catch (Exception)
            {
                return null;
            }
            return Get(imageUri);
        }

        /// <summary>
        /// The actual implementation of <see cref="ImageCache.Get"/>.
        /// </summary>
        protected abstract ImageSource GetInternal(Uri imageUri);

        /// <summary>
        /// Deletes all the images from the cache.
        /// This method can block the current thread for a long time; it is advised to call it from
        /// a background thread.
        /// </summary>
        public abstract void Clear();

        /// <summary>
        /// Overrides object.ToString().
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return string.Format("ImageCache:{0}", Name);
        }
    }
}
