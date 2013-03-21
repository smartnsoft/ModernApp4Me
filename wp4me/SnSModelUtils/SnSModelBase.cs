using System.ComponentModel;

namespace wp4me.SnSModelUtils
{
    /// <summary>
    /// Base class to implement a persistant model class.
    /// </summary>
    public class SnSModelBase : INotifyPropertyChanged, INotifyPropertyChanging
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
