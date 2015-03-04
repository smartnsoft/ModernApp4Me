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
