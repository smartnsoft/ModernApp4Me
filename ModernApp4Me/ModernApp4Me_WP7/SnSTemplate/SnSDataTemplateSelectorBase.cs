using System.Windows;
using System.Windows.Controls;

namespace ModernApp4Me_WP7.SnSTemplate
{
    /// <summary>
    /// Class base for the template selector classes.
    /// </summary>
    public class SnSDataTemplateSelectorBase : ContentControl
    {
        /*******************************************************/
        /** METHODS.
        /*******************************************************/
        public virtual DataTemplate SelectTemplate(object item, DependencyObject container)
        {
            return null;
        }

        protected override void OnContentChanged(object oldContent, object newContent)
        {
            base.OnContentChanged(oldContent, newContent);

            ContentTemplate = SelectTemplate(newContent, this);
        }
    }
}
