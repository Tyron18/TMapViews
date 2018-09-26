using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Foundation;
using MvvmCross.Binding.BindingContext;
using MvvmCross.Platforms.Ios.Binding.Views;
using TMapViews.iOS.Views;
using UIKit;

namespace TMapViews.MvxPlugins.Bindings.iOS
{
    public abstract class MvxBindingCalloutView : BindingMKCalloutView, IMvxBindable
    {
        public IMvxBindingContext BindingContext { get; set; }
        public object DataContext { get => BindingContext.DataContext; set => BindingContext.DataContext = value; }

        public MvxBindingCalloutView()
        {
            this.CreateBindingContext(string.Empty);
        }
    }
}