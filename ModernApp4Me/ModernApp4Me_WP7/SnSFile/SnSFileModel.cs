using System;
using System.Data.Linq.Mapping;
using ModernApp4Me_WP7.SnSCache.Database;

namespace ModernApp4Me_WP7.SnSFile
{
    /// <summary>
    /// Reprensents a file.
    /// </summary>
    [Table]
    public sealed class SnSFileModel : SnSPersistenceModelBase
    {
        /*******************************************************/
        /** ATTRIBUTES.
        /*******************************************************/
        private string _uri;
        private DateTime _date;
        private SnSFileTypeEnum _type;


        /*******************************************************/
        /** PROPERTIES.
        /*******************************************************/
        [Column(IsPrimaryKey = true)]
        public string Uri
        {
            get { return _uri; }
            set
            {
                OnPropertyChanging("_uri");
                _uri = value;
                OnPropertyChanged("_uri");
            }
        }

        [Column]
        public DateTime Date
        {
            get { return _date; }
            set
            {
                OnPropertyChanging("_date");
                _date = value;
                OnPropertyChanged("_date");
            }
        }

        [Column]
        public SnSFileTypeEnum Type
        {
            get { return _type; }
            set
            {
                OnPropertyChanging("_type");
                _type = value;
                OnPropertyChanged("_type");
            }
        }
    }
}
