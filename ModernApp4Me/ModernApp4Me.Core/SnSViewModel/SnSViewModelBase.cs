using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace ModernApp4Me_Core.SnSViewModel
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
        /// <summary>
        /// Raises the OnPropertyChanged methods with in a secure way.
        /// </summary>
        /// <param name="propertyName"></param>
        protected void RaiseOnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            OnPropertyChanged(propertyName);
        }
        
        /// <summary>
        /// Notifies that a property has changed.
        /// </summary>
        /// <param name="propertyName"></param>
        public void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}
