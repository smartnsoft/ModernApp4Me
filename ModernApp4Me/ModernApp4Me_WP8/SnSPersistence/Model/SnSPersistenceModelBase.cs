using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace ModernApp4Me_WP8.SnSPersistence.Model
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
        /// <summary>
        /// Raises the OnPropertyChanging methods with in a secure way.
        /// </summary>
        /// <param name="propertyName"></param>
        protected void RaiseOnPropertyChanging([CallerMemberName] string propertyName = "")
        {
            OnPropertyChanging(propertyName);
        }
        
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

        /// <summary>
        /// Notifies that a property is changing.
        /// </summary>
        /// <param name="propertyName"></param>
        public void OnPropertyChanging(string propertyName)
        {
            if (PropertyChanging != null)
            {
                PropertyChanging(this, new PropertyChangingEventArgs(propertyName));
            }
        }

    }
}
