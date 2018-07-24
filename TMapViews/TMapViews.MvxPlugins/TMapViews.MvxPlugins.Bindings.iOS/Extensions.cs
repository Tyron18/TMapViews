using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Foundation;
using MapKit;
using TMapViews.MvxPlugins.Bindings.iOS.Bindings;
using UIKit;

namespace TMapViews.MvxPlugins.Bindings.iOS
{
    public static class Extensions
    {
        public static string BindScale(this MKAnnotationView view) => MKAnnotationViewScaleTargetBinding.MKAnnotationViewScaleTargetBindingString;
    }
}