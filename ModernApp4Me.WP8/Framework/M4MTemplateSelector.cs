// Copyright (C) 2015 Smart&Soft SAS (http://www.smartnsoft.com/) and contributors
//
// This library is free software; you can redistribute it and/or
// modify it under the terms of the GNU Lesser General Public
// License as published by the Free Software Foundation; either
// version 2.1 of the License, or (at your option) any later version.
//
// This library is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE. See the GNU
// Lesser General Public License for more details.
//
// Contributors:
//   Smart&Soft - initial API and implementation

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
