using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Foundation;
using MapKit;
using TMapViews.iOS;
using TMapViews.Models;
using UIKit;

namespace TMapViews.MvxPlugins.Bindings.iOS
{
    public abstract class MvxBindingMkMapViewDelegate : BindingMKMapViewDelegate
    {
        public sealed override MKAnnotationView GetViewForBindingAnnotation(MKMapView mapView, IBindingMapAnnotation bindingMapAnnotation)
        {
            var anno = GetViewForBindingAnnotation(mapView);
            anno.DataContext = bindingMapAnnotation;
            return anno;
        }

        public abstract MvxBindingMKAnnotationView GetViewForBindingAnnotation(MKMapView mapView);
    }
}