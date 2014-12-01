using System;
using ModernApp4Me.WP8.SnSWebService;

namespace ModernApp4Me.WP8.Template.WebService
{

    /// <summary>
    /// A single point of access to the web services.
    /// </summary>
    public sealed class Services : SnSWebServiceCaller
    {

        private static volatile Services instance;

        private static readonly object InstanceLock = new Object();

        private Services() :base(Constants.SERVICES_BASE_URL)
        {
        }

        public static Services Instance
        {
            get
            {
                if (instance == null)
                {
                    lock (InstanceLock)
                    {
                        if (instance == null)
                        {
                            instance = new Services();
                        }
                    }
                }

                return instance;
            }
        }

    }

}
