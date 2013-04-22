using System;
using System.Collections.Generic;
using System.Text;
using ModernApp4Me_Core.SnSLog;

namespace ModernApp4Me_WP8.SnSResolution
{
    /// <summary>
    /// Chooser to choose the best image resolution according to the device resolution.
    /// </summary>
    public static class SnSResolutionImageChooser
    {
        /*******************************************************/
        /** METHODS.
        /*******************************************************/
        /// <summary>
        /// Returns the best resolution image according to the device resolution.
        /// </summary>
        /// <param name="imageName"></param>
        /// 
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
