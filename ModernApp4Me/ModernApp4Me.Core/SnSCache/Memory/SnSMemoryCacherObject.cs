using System;

namespace ModernApp4Me.Core.SnSCache.Memory
{

    /// <summary>
    /// Class that reprensents an object stores into the memory cacher.
    /// </summary>
    /// <author>Ludovic ROLAND</author>
    /// <since>2014.03.24</since>
    public sealed class SnSMemoryCacherObject
    {

        public DateTime Date { get; set; }

        public object Value { get; set; }

    }

}
