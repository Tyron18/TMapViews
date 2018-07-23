using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.Gms.Maps;
using Android.Gms.Maps.Model;
using Android.Runtime;
using TMapViews.Droid;
using TMapViews.Droid.Adapters;
using TMapViews.Models;

namespace TMapViews.MvxPlugins.Bindings.Droid
{
    public abstract class MvxBindingMapViewAdapter : IBindingMapAdapter
    {
        public abstract IJavaObject AddBindingMapOverlay(GoogleMap googleMap, IBindingMapOverlay overlay);

        public MarkerOptions GetMarkerOptionsForPin(IBindingMapAnnotation pin)
        {
            return new MarkerOptions().SetPosition(pin.Location.ToLatLng());
        }

        internal void GetMvxBindingMarker(Marker marker, IBindingMapAnnotation annotation)
        {
            var result = GetMvxBindingMarker();
            result.Marker = marker;
            result.DataContext = annotation;
        }

        public abstract MvxBindingMarker GetMvxBindingMarker();
    }
}