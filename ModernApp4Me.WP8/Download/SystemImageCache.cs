// Copyright 2010 Andreas Saudemont (andreas.saudemont@gmail.com)
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
//   Andreas Saudemont (andreas.saudemont@gmail.com)
//
// Taken from : https://kawagoe.codeplex.com/

using System;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace ModernApp4Me.WP8.Download
{
    /// <summary>
    /// Implements an <see cref="ImageCache"/> using the cache mechanism provided by the system.
    /// </summary>
    public class SystemImageCache : ImageCache
    {
        /// <summary>
        /// Initializes a new <see cref="SystemImageCache"/> instance with the specified name.
        /// </summary>
        public SystemImageCache(string name)
            : base(name)
        {
        }

        /// <summary>
        /// Implements <see cref="ImageCache.GetInternal"/>.
        /// </summary>
        protected override ImageSource GetInternal(Uri imageUri)
        {
            return new BitmapImage(imageUri);
        }

        /// <summary>
        /// Implements <see cref="ImageCache.Clear"/>.
        /// </summary>
        public override void Clear()
        {
            // do nothing
        }
    }
}
