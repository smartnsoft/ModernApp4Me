using System;
using System.Text;
using ModernApp4Me.WP8.Metadata;

namespace ModernApp4Me.WP8.Util
{

    /// <summary>
    /// Chooses the best image from the assets according to the device resolution.
    /// </summary>
    /// 
    /// <author>Ludovic ROLAND</author>
    /// <since>2014.03.21</since>
    // Inspired by https://msdn.microsoft.com/en-us/library/windows/apps/jj206974%28v=vs.105%29.aspx
    public static class M4MResolutionImageChooser
    {

        public static Uri GetBestResolutionImage(string imageName)
        {
            var array = imageName.Split('.');
            var imageNameBase = array[0];
            var imageExtension = array[1];
            var imageNameChooser = new StringBuilder(imageNameBase);

            switch (M4MDeviceResolution.CurrentResolution)
            {
                case M4MDeviceResolution.Resolution.HD:
                    imageNameChooser.Append("-HD.");
                    break;

                case M4MDeviceResolution.Resolution.WXGA:
                    imageNameChooser.Append("-WXGA.");
                    break;

                case M4MDeviceResolution.Resolution.WVGA:
                    imageNameChooser.Append("-WVGA.");
                    break;

                default :
                    imageNameChooser.Append("-WVGA.");
                    break;
            }

            imageNameChooser.Append(imageExtension);

            return new Uri(imageNameChooser.ToString(), UriKind.Relative);
        }

    }

}
