using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.Gms.Maps;
using Android.Gms.Maps.Model;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using TMapViews.Droid.Models;
using TMapViews.Models;

namespace TMapViews.Droid.Adapters
{
    public interface IBindingMapAdapter
    {
        void AddBindingMapOverlay(GoogleMap googleMap, IBindingMapAnnotation overlay);
        MarkerOptions GetMarkerOptionsForPin(BindingMapMarker pin);
    }
}