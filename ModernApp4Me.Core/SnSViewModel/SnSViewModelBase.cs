using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace ModernApp4Me.Core.SnSViewModel
{

    /// <summary>
    /// Base class to implement the MVVM pattern.
    /// </summary>
    /// <author>Ludovic ROLAND</author>
    /// <since>2014.03.21</since>
    public class SnSViewModelBase : INotifyPropertyChanged
    {

        public event PropertyChangedEventHandler PropertyChanged;

        protected void RaiseOnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            OnPropertyChanged(propertyName);
        }
        
        public void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        public SnSViewModelBase() { }

    }

}