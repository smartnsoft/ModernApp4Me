using System;

namespace ModernApp4Me_Core.SnSCache
{
    /// <summary>
    /// Class that reprensents an object stores into the memory cacher.
    /// </summary>
    public sealed class SnSMemoryCacherObject
    {
        /*******************************************************/
        /** PROPERTIES.
        /*******************************************************/
        public DateTime Date { get; set; }
        public object Value { get; set; }
    }
}
