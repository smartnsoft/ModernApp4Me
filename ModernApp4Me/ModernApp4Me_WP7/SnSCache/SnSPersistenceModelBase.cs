using System.ComponentModel;

namespace ModernApp4Me_WP7.SnSCache
{
    /// <summary>
    /// Base class to implement a persistant model class.
    /// </summary>
    public class SnSPersistenceModelBase : INotifyPropertyChanged, INotifyPropertyChanging
    {
        /*******************************************************/
        /** PROPERTIES.
        /*******************************************************/
        public event PropertyChangedEventHandler PropertyChanged;
        public event PropertyChangingEventHandler PropertyChanging;


        /*******************************************************/
        /** METHODS.
        /*******************************************************/
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
