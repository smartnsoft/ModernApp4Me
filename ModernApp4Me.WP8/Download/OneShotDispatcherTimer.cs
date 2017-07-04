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
using System.Windows.Threading;

namespace ModernApp4Me.WP8.Download
{
    /// <summary>
    /// Provides a one-shot timer integrated to the Dispatcher queue.
    /// </summary>
    public class OneShotDispatcherTimer
    {
        /// <summary>
        /// Creates a new <see cref="OneShotDispatcherTimer"/> and starts it.
        /// </summary>
        /// <param name="duration">The duration of the timer.</param>
        /// <param name="callback">The delegate that will be called when the timer fires.</param>
        /// <returns>The newly created timer.</returns>
        public static OneShotDispatcherTimer CreateAndStart(TimeSpan duration, EventHandler callback)
        {
            OneShotDispatcherTimer timer = new OneShotDispatcherTimer();
            timer.Duration = duration;
            timer.Fired += callback;
            timer.Start();
            return timer;
        }

        private TimeSpan _duration = TimeSpan.Zero;
        private DispatcherTimer _timer = null;

        /// <summary>
        /// Initializes a new <see cref="OneShotDispatcherTimer"/> instance.
        /// </summary>
        public OneShotDispatcherTimer()
        {
        }

        /// <summary>
        /// The duration of the timer. The default is 00:00:00.
        /// </summary>
        /// <remarks>
        /// Setting the value of this property takes effect the next time the timer is started.
        /// </remarks>
        /// <exception cref="ArgumentOutOfRangeException">The specified value when setting this property represents
        /// a negative time internal.</exception>
        public TimeSpan Duration
        {
            get
            {
                return _duration;
            }
            set
            {
                if (value.TotalMilliseconds < 0)
                {
                    throw new ArgumentOutOfRangeException();
                }
                _duration = value;
            }
        }

        /// <summary>
        /// Indicates whether the timer is currently started.
        /// </summary>
        public bool IsStarted
        {
            get
            {
                return (_timer != null);
            }
        }

        /// <summary>
        /// Occurs when the one-shot timer fires.
        /// </summary>
        public event EventHandler Fired;

        /// <summary>
        /// Raises the <see cref="Fired"/> event.
        /// </summary>
        private void RaiseFired()
        {
            if (Fired != null)
            {
                try
                {
                    Fired(this, EventArgs.Empty);
                }
                catch (Exception) { }
            }
        }

        /// <summary>
        /// Starts the timer.
        /// This method has no effect if the timer is already started.
        /// </summary>
        /// <remarks>
        /// The same <see cref="OneShotDispatcherTimer"/> instance can be started and stopped multiple times.
        /// </remarks>
        public void Start()
        {
            if (_timer != null)
            {
                return;
            }

            _timer = new DispatcherTimer();
            _timer.Interval = _duration;
            _timer.Tick += OnTimerTick;
            _timer.Start();
        }

        /// <summary>
        /// Stops the timer.
        /// This method has no effect if the timer is not started.
        /// </summary>
        /// <remarks>
        /// The <see cref="Fired"/> event is guaranteed not to be raised once this method has been invoked
        /// and until the timer is started again.
        /// </remarks>
        public void Stop()
        {
            if (_timer == null)
            {
                return;
            }
            try
            {
                _timer.Stop();
            }
            catch (Exception) { }
            _timer = null;
        }

        /// <summary>
        /// Listens to Tick events on the underlying timer.
        /// </summary>
        private void OnTimerTick(object sender, EventArgs e)
        {
            if (sender != _timer)
            {
                return;
            }
            Stop();
            RaiseFired();
        }
    }
}
