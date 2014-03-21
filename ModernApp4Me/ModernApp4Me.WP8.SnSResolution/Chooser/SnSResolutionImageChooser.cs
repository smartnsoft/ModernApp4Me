using System;
using System.Text;
using ModernApp4Me.Core.SnSLog;
using ModernApp4Me.WP8.SnSResolution.Helper;

namespace ModernApp4Me.WP8.SnSResolution.Chooser
{

    /// <summary>
    /// Chooser to choose the best image resolution according to the device resolution.
    /// </summary>
    /// <author>Ludovic ROLAND</author>
    /// <since>2014.03.21</since>
    public static class SnSResolutionImageChooser
    {

        /// <summary>
        /// Returns the best resolution image according to the device resolution.
        /// </summary>
        /// <param name="imageName"></param>
        public static Uri BestResolutionImage(string imageName)
        {
            var imageArray = imageName.Split('.');
            var imageNameBase = imageArray[0];
            var imageExtension = imageArray[1];
            var bestImageNameBuilder = new StringBuilder(imageNameBase);

            switch (SnSResolutionHelper.CurrentResolution())
            {
                case SnSResolutionEnum.HD720P:
                    bestImageNameBuilder.Append("-720.");
                    break;

                case SnSResolutionEnum.WXGA:
                    bestImageNameBuilder.Append("-WXGA.");
                    break;

                case SnSResolutionEnum.WVGA:
                    bestImageNameBuilder.Append("-WVGA.");
                    break;

                default :
                    bestImageNameBuilder.Append("-WVGA.");
                    SnSLogger.Warn("Unknown resolution type", "SnSMultiResolutionImageChooser", "BestResolutionImage");
                    break;
            }

            bestImageNameBuilder.Append(imageExtension);
            return new Uri(bestImageNameBuilder.ToString(), UriKind.Relative);
        }

    }

}
