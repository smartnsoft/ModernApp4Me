using System.Windows.Media;
using System.Windows.Media.Imaging;
using ModernApp4Me.Core.SnSViewModel;
using ModernApp4Me.WP8.SnSDownload.BitmapDownloader;

namespace ModernApp4Me.WP8.SnSViewModel
{
    public class SnSImageViewModel : SnSViewModelBase
    {

        private bool isImageLoaded;

        public bool IsImageLoaded
        {
            get { return isImageLoaded; }
            set
            {
                if (value != isImageLoaded)
                {
                    isImageLoaded = value;
                    RaiseOnPropertyChanged();
                    LoadingRealImg();
                }
            }
        }

        private string imgUrl;

        private BitmapImage img;

        public BitmapImage Img
        {
            get
            {
                return img;
            }

            set
            {
                if (value.Equals(img) == false)
                {
                    img = value;
                    RaiseOnPropertyChanged();
                }
            }
        }

        private void LoadingRealImg()
        {
            var downloadedImage = SnSBitmapDownloader.GetInstance.GetImage(imgUrl);

            if (downloadedImage != null)
            {
                Img = downloadedImage as BitmapImage;
            }
        }

    }
}
