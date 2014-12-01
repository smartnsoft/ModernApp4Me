using System.Windows;
using System.Windows.Controls;

namespace ModernApp4Me.WP7.SnSTemplate
{

    /// <summary>
    /// Class base for the template selector classes.
    /// </summary>
    /// <author>Ludovic ROLAND</author>
    /// <since>2014.03.24</since>
    public abstract class SnSTemplateSelector : ContentControl
    {

        /// <summary>
        /// Virtual method that return the selected template.
        /// </summary>
        /// <param name="item"></param>
        /// <param name="container"></param>
        /// <returns>The selected template</returns>
        public abstract DataTemplate SelectTemplate(object item, DependencyObject container);

        protected override void OnContentChanged(object oldContent, object newContent)
        {
            base.OnContentChanged(oldContent, newContent);

            ContentTemplate = SelectTemplate(newContent, this);
        }

    }

}
