using Android.Gms.Maps.Model;
using Android.Runtime;
using System.Collections.Generic;
using TMapViews.Droid;
using TMapViews.Droid.Adapters;
using TMapViews.Droid.Models;
using TMapViews.Droid.Views;
using TMapViews.Models;

namespace TMapViews.MvxPlugins.Bindings.Droid
{
    [Preserve(AllMembers = true)]
    public abstract class MvxBindingMapViewAdapter : BindingMapAdapter
    {
        public MvxBindingMapViewAdapter(BindingMapView mapView) : base(mapView)
        {
        }

        public override MarkerOptions GetMarkerOptionsForPin(IBindingMapAnnotation pin)
        {
            return new MarkerOptions().SetPosition(pin.Location.ToLatLng());
        }

        internal void GetMvxBindingMarker(Marker marker, IBindingMapAnnotation annotation)
        {
            MvxBindingMarker result = GetMvxBindingMarker();
            result.Marker = marker;
            result.DataContext = annotation;
        }

        public abstract MvxBindingMarker GetMvxBindingMarker();

        public override void AddAnnotation(IBindingMapAnnotation annotation)
        {
            if (annotation is IBindingMapAnnotation mMarker)
            {
                using (MarkerOptions markerOptions = GetMarkerOptionsForPin(annotation))
                    if (markerOptions != null)
                    {
                        MvxBindingMarker result = GetMvxBindingMarker();
                        result.DataContext = annotation;
                        markerOptions.SetIcon(result.GetIcon());
                        Marker marker = MapView.GoogleMap.AddMarker(markerOptions);
                        result.Marker = marker;
                        marker.Tag = new AnnotationTag
                        {
                            Annotation = annotation
                        };
                        if (_markers == null)
                            _markers = new List<Marker>();
                        _markers.Add(marker);
                    }
            }
        }
    }
}