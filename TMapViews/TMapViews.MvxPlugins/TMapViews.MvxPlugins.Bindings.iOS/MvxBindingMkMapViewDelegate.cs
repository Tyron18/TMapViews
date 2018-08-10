using Foundation;
using MapKit;
using TMapViews.iOS;
using TMapViews.iOS.Views;
using TMapViews.Models;

namespace TMapViews.MvxPlugins.Bindings.iOS
{
    [Preserve(AllMembers = true)]
    public abstract class MvxBindingMkMapViewDelegate : BindingMKMapViewDelegate
    {
        public sealed override MKAnnotationView GetViewForBindingAnnotation(MKMapView mapView, IBindingMapAnnotation bindingMapAnnotation)
        {
            var anno = GetViewForBindingAnnotation(mapView);
            anno.DataContext = bindingMapAnnotation;
            return anno;
        }

        public abstract MvxBindingMKAnnotationView GetViewForBindingAnnotation(MKMapView mapView);

        public sealed override BindingMKCalloutView GetViewForCallout(MKMapView mapView, IBindingMapAnnotation bindingMapAnnotation)
        {
            var view = GetViewForCallout(mapView);
            if (view != null)
                view.DataContext = bindingMapAnnotation;
            return view;
        }

        public virtual MvxBindingCalloutView GetViewForCallout(MKMapView mapView) => null;
    }
}