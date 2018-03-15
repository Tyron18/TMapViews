using Android.Gms.Maps;
using Android.Gms.Maps.Model;
using TMapViews.Droid.Models;
using TMapViews.Models;

namespace TMapViews.Droid.Adapters
{
    public interface IBindingMapAdapter
    {
        Java.Lang.Object AddBindingMapOverlay(GoogleMap googleMap, IBindingMapAnnotation overlay);

        MarkerOptions GetMarkerOptionsForPin(BindingMapMarker pin);
    }
}