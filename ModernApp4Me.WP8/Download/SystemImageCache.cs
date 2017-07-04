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
