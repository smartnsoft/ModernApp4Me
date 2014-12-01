using System.ComponentModel;

namespace ModernApp4Me.WP7.SnSCache.Database
{

    /// <summary>
    /// Class base to implement a persistant model class.
    /// </summary>
    /// <author>Ludovic ROLAND</author>
    /// <since>2014.03.24</since>
    public abstract class SnDatabase : INotifyPropertyChanged, INotifyPropertyChanging
    {

        public event PropertyChangedEventHandler PropertyChanged;

        public event PropertyChangingEventHandler PropertyChanging;
        
        public void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        public void OnPropertyChanging(string propertyName)
        {
            if (PropertyChanging != null)
            {
                PropertyChanging(this, new PropertyChangingEventArgs(propertyName));
            }
        }

    }

}
