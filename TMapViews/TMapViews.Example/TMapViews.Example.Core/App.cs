using MvvmCross.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;
using TMapViews.Example.Core.ViewModels;

namespace TMapViews.Example.Core
{
    public class App : MvxApplication
    {
        public override void Initialize()
        {
            RegisterAppStart<MultipleMapLocationsViewModel>();
        }
    }
}
