using System.ComponentModel;

namespace wp4me.SnSViewModelUtils
{
    /// <summary>
    /// Base class to implement the MVVM pattern.
    /// </summary>
    public class SnSViewModelBase : INotifyPropertyChanged
    {
        /*******************************************************/
        /** PROPERTIES.
        /*******************************************************/
        public event PropertyChangedEventHandler PropertyChanged;


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
    }
}
