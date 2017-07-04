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
using Microsoft.Devices.Radio;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Media;

namespace ModernApp4Me.WP8.Media
{

    /// <summary>
    /// Manages the medias source like the radio or the music player in order to be compliant with the validation point number 6.5.1
    /// </summary>
    /// 
    /// <author>Ludovic ROLAND</author>
    /// <since>2014.04.02</since>
    public sealed class M4MMediaManager
    {

        private static volatile M4MMediaManager instance;

        private static readonly object InstanceLock = new Object();

        public bool IsRadioPlaying { get; set; }

        public bool IsMediaPlayerPlaying { get; set; }

        public bool HasAccepted { get; set; }

        public static M4MMediaManager Instance
        {
            get
            {
                if (instance == null)
                {
                    lock (InstanceLock)
                    {
                        if (instance == null)
                        {
                            instance = new M4MMediaManager();
                        }
                    }
                }

                return instance;
            }
        }

        private M4MMediaManager()
        {
            ResetMediaManager();
        }

        /// <summary>
        /// Resets the <see cref="M4MMediaManager"/> parameters.
        /// </summary>
        public void ResetMediaManager()
        {
            IsRadioPlaying = false;
            IsMediaPlayerPlaying = false;
            HasAccepted = false;
        }

        /// <summary>
        /// Scans the <see cref="MediaPlayer"/> and the <see cref="FMRadio"/> in order to update the <see cref="M4MMediaManager"/> parameters.
        /// </summary>
        public void ScanForUserMedias()
        {
            FrameworkDispatcher.Update();

            try
            {
                if (FMRadio.Instance.PowerMode == RadioPowerMode.On || FMRadio.Instance.SignalStrength > 0.0)
                {
                    IsMediaPlayerPlaying = true;
                    IsRadioPlaying = true;
                }
                else if (MediaPlayer.State == MediaState.Playing)
                {
                    IsMediaPlayerPlaying = true;
                }
            }
            catch (RadioDisabledException)
            {
                if (MediaPlayer.State == MediaState.Playing)
                {
                    IsMediaPlayerPlaying = true;
                }
            }
        }

        /// <summary>
        /// Resumes the <see cref="FMRadio"/> or the <see cref="MediaPlayer"/>.
        /// </summary>
        public void ResumeUserMedia()
        {
            if (IsMediaPlayerPlaying == true)
            {
                if (IsRadioPlaying == true)
                {
                    FMRadio.Instance.PowerMode = RadioPowerMode.On;
                }
                else
                {
                    MediaPlayer.Resume();
                }
            }
        }

    }

}
