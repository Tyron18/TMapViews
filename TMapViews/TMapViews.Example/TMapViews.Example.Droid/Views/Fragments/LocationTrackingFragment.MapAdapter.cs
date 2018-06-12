using Android.Content;
using Android.Gms.Maps;
using Android.Gms.Maps.Model;
using Android.Runtime;
using TMapViews.Droid.Adapters;
using TMapViews.Example.Core.Models;
using TMapViews.Models;

namespace TMapViews.Example.Droid.Views.Fragments
{
    public partial class LocationTrackingFragment
    {
        internal class LocationTrackingAdapter : IBindingMapAdapter
        {
            public Context Context;

            public LocationTrackingAdapter(Context context)
            {
                Context = context;
            }

            public IJavaObject AddBindingMapOverlay(GoogleMap googleMap, IBindingMapOverlay overlay)
            {
                return null;
            }

            public MarkerOptions GetMarkerOptionsForPin(IBindingMapAnnotation pin)
            {
                MarkerOptions markerOptions = null;

                if (pin is ExampleBindingAnnotation mPin)
                {
                    markerOptions = new MarkerOptions();
                    markerOptions.SetPosition(new LatLng(pin.Location.Latitude, pin.Location.Longitude))
                    .SetTitle(mPin.Id.ToString())
                    .SetIcon(BitmapDescriptorFactory.FromResource(Resource.Drawable.sphere));
                }
                return markerOptions;
            }
        }
    }
}