using System;
using System.Windows.Media;

namespace ModernApp4Me.WP7.SnSDownload.BitmapDownloader
{

    /// <summary>
    /// Image downloading and caching singleton class.
    /// </summary>
    /// <author>Ludovic ROLAND</author>
    /// <since>2014.03.24</since>
    public sealed class SnSBitmapDownloader
    {

        public enum SnSBitmapDownloaderType
        {
            SystemImageCache, PersistentImageCache
        }

        public sealed class SnSBitmapDownloaderConfiguration
        {

            public SnSBitmapDownloaderType Type { get; set; }

            public string Name { get; set; }

            public TimeSpan ExpirationDelay { get; set; }

            public int MemoryCacheCapacity { get; set; }

        }

        private static volatile SnSBitmapDownloader instance;

        private static readonly object InstanceLock = new Object();

        private readonly ImageCache cache;

        public static SnSBitmapDownloaderConfiguration Configuration { get; set; }

        public static SnSBitmapDownloader Instance
        {
            get
            {
                if (instance == null)
                {
                    lock (InstanceLock)
                    {
                        if (instance == null)
                        {
                            instance = new SnSBitmapDownloader();
                        }
                    }
                }

                return instance;
            }
        }

        private SnSBitmapDownloader()
        {
            if (Configuration.Type == SnSBitmapDownloaderType.PersistentImageCache)
            {
                cache = new PersistentImageCache(Configuration.Name)
                    {
                        ExpirationDelay = Configuration.ExpirationDelay,
                        MemoryCacheCapacity = Configuration.MemoryCacheCapacity
                    };
            }
            else
            {
                cache = new SystemImageCache(Configuration.Name);
            }
        }

        public ImageSource GetImage(string imageUri)
        {
            return cache.Get(imageUri);
        }

        public void LoadImage(string imageUri)
        {
            cache.Get(imageUri);
        }
    }
}
