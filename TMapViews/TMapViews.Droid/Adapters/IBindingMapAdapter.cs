using Android.Gms.Maps;
using Android.Gms.Maps.Model;
using Android.Runtime;
using TMapViews.Models;

namespace TMapViews.Droid.Adapters
{
    public interface IBindingMapAdapter
    {
        IJavaObject AddBindingMapOverlay(GoogleMap googleMap, IBindingMapOverlay overlay);

        MarkerOptions GetMarkerOptionsForPin(IBindingMapAnnotation pin);
    }
}