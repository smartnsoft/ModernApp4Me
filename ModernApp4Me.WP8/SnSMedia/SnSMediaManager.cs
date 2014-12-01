using System;
using Microsoft.Devices.Radio;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Media;

namespace ModernApp4Me.WP8.SnSMedia
{

    /// <summary>
    /// Singleton class. to manage the currents user media that are playing music and pass the validation point number 6.5.1
    /// </summary>
    /// <author>Ludovic ROLAND</author>
    /// <since>2014.04.02</since>
    public sealed class SnSMediaManager
    {

        private static volatile SnSMediaManager instance;

        private static readonly object InstanceLock = new Object();

        public bool IsRadioPlaying { get; set; }

        public bool IsMediaPlayerPlaying { get; set; }

        public bool HasAccepted { get; set; }

        public static SnSMediaManager Instance
        {
            get
            {
                if (instance == null)
                {
                    lock (InstanceLock)
                    {
                        if (instance == null)
                        {
                            instance = new SnSMediaManager();
                        }
                    }
                }

                return instance;
            }
        }

        private SnSMediaManager()
        {
            ResetMediaManager();
        }

        public void ResetMediaManager()
        {
            IsRadioPlaying = false;
            IsMediaPlayerPlaying = false;
            HasAccepted = false;
        }

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
