using System;
using System.Data.Linq.Mapping;
using ModernApp4Me_WP7.SnSCache.Database;

namespace ModernApp4Me_WP7.SnSWebService
{
    /// <summary>
    /// Reprensents a web service.
    /// </summary>
    [Table]
    public sealed class SnSWebServiceModel : SnSPersistenceModelBase
    {
        /*******************************************************/
        /** ATTRIBUTES.
        /*******************************************************/
        private string _uri;
        private DateTime _date;
        private string _content;
        private SnSWebServiceTypeEnum _type;


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
        public string Content
        {
            get { return _content; }
            set
            {
                OnPropertyChanging("_content");
                _content = value;
                OnPropertyChanged("_content");
            }
        }

        [Column]
        public SnSWebServiceTypeEnum Type
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
