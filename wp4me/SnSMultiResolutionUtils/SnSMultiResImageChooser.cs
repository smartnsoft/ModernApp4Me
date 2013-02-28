using System;
using System.Collections.Generic;

namespace wp4me.SnSMultiResolutionUtils
{
    /// <summary>
    /// Class that provides functions to work with the best resolution image according to the phone resolution.
    /// </summary>
    public sealed class SnSMultiResImageChooser
    {
        /*******************************************************/
        /** METHODS AND FUNCTIONS.
        /*******************************************************/
        /// <summary>
        /// Method that returns the best resolution image according to the phone resolution.
        /// </summary>
        /// <param name="imageName">Path of the image</param>
        /// 
        public static Uri BestResolutionImage(string imageName)
        {
            var imageArray = imageName.Split('.');
            var dictionaryImage = new Dictionary<string, string>();

            dictionaryImage["name"] = imageArray[0];
            dictionaryImage["extension"] = imageArray[1];

            switch (SnSResolutionHelper.CurrentResolution)
            {
                case Resolutions.HD720P:
                    return new Uri(dictionaryImage["name"] + "-720p." + dictionaryImage["extension"], UriKind.Relative);

                case Resolutions.WXGA:
                    return new Uri(dictionaryImage["name"] + "-wxga." + dictionaryImage["extension"], UriKind.Relative);

                case Resolutions.WVGA:
                    return new Uri(dictionaryImage["name"] + "-wvga." + dictionaryImage["extension"], UriKind.Relative);

                default:
                    throw new InvalidOperationException("Unknown resolution type");
            }
        }
    }
}
