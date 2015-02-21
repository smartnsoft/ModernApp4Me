using System.Windows;
using System.Windows.Controls;

namespace ModernApp4Me.WP8.Framework
{

    /// <summary>
    /// A class base that should be extended in order to create template selectors.
    /// </summary>
    /// 
    /// <author>Ludovic ROLAND</author>
    /// <since>2014.03.24</since>
    public abstract class M4MTemplateSelector : ContentControl
    {

        /// <summary>
        /// Returns the selected template.
        /// </summary>
        /// <param name="item">The item</param>
        /// <param name="container">the container wich is a <see cref="DependencyObject"/></param>
        /// <returns>The selected template</returns>
        public abstract DataTemplate SelectTemplate(object item, DependencyObject container);

        protected override void OnContentChanged(object oldContent, object newContent)
        {
            base.OnContentChanged(oldContent, newContent);
            ContentTemplate = SelectTemplate(newContent, this);
        }

    }

}
