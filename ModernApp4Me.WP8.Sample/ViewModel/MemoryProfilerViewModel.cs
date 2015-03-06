// Copyright (C) 2015 Smart&Soft SAS (http://www.smartnsoft.com/) and contributors
//
// This library is free software; you can redistribute it and/or
// modify it under the terms of the GNU Lesser General Public
// License as published by the Free Software Foundation; either
// version 3 of the License, or (at your option) any later version.
//
// This library is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE. See the GNU
// Lesser General Public License for more details.
//
// Contributors:
//   Smart&Soft - initial API and implementation

using ModernApp4Me.Core.ViewModel;

namespace ModernApp4Me.WP8.Sample.ViewModel
{

    /// <author>Ludovic ROLAND</author>
    /// <since>2015.03.06</since>
    public sealed class MemoryProfilerViewModel : M4MBaseViewModel
    {

        private string report;

        public string Report
        {
            get { return report; }
            set
            {
                report = value;
                RaiseOnPropertyChanged();
            }
        }

    }
}
