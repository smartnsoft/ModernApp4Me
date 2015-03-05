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

using System;
using System.Threading.Tasks;
using System.Windows.Controls;
using Microsoft.Phone.Shell;
using ModernApp4Me.Core.LifeCycle;
using ModernApp4Me.Core.ViewModel;
using ModernApp4Me.WP8.Sample.ViewModel;
using ModernApp4Me.WP8.Sample.WebService;

namespace ModernApp4Me.WP8.Sample.ExceptionHandler
{

    /// <author>Ludovic ROLAND</author>
    /// <since>2015.03.05</since>
    public sealed partial class ExceptionHandlerDetailPage
    {

        private string param;

        public ExceptionHandlerDetailPage()
        {
            InitializeComponent();

            isManagingProgressIndicatorItself = true;
        }

        protected override void LoadQueryString()
        {
            param = NavigationContext.QueryString["param"];
        }

        protected async override Task<M4MBaseViewModel> ComputeViewModel()
        {
            if (ExceptionHandlerPage.BO.Equals(param) == true)
            {
                throw new M4MBusinessObjectUnavailableException();
            }

            if (ExceptionHandlerPage.EXCEPTION.Equals(param) == true)
            {
                throw new NullReferenceException();
            }

            var people = await Services.Instance.GetPeople();
            return new ExceptionHandlerViewModel(){People = people};
        }

        protected override void OnFullfillDisplayObjects()
        {
            DataContext = viewModel;
        }

        protected override Panel RetrieveMainContainer()
        {
            return ContentPanel;
        }

        protected override ProgressIndicator RetrieveProgressIndicator()
        {
            return null;
        }
    }
}