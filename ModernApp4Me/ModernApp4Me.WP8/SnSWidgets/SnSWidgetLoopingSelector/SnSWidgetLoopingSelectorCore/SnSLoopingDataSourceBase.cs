using System;
using System.Windows.Controls;
using Microsoft.Phone.Controls.Primitives;

namespace ModernApp4Me.WP8.SnSWidgets.SnSWidgetLoopingSelector.SnSWidgetLoopingSelectorCore
{
    /// <summary>
    /// Abstract the reusable code in a base class
    /// this will allow us to concentrate on the specifics when implementing deriving looping data source classes
    /// </summary>
    public abstract class SnSLoopingDataSourceBase : ILoopingSelectorDataSource
    {
        private object _selectedItem;

        public abstract object GetNext(object relativeTo);

        public abstract object GetPrevious(object relativeTo);

        public object SelectedItem
        {
            get
            {
                return _selectedItem;
            }
            set
            {
                // this will use the Equals method if it is overridden for the data source item class
                if (!Equals(_selectedItem, value))
                {
                    // save the previously selected item so that we can use it
                    // to construct the event arguments for the SelectionChanged event
                    object previousSelectedItem = _selectedItem;
                    _selectedItem = value;
                    // fire the SelectionChanged event
                    OnSelectionChanged(previousSelectedItem, _selectedItem);
                }
            }
        }

        public event EventHandler<SelectionChangedEventArgs> SelectionChanged;

        protected virtual void OnSelectionChanged(object oldSelectedItem, object newSelectedItem)
        {
            var handler = SelectionChanged;
            if (handler != null)
            {
                handler(this, new SelectionChangedEventArgs(new [] { oldSelectedItem }, new [] { newSelectedItem }));
            }
        }
    }
}
