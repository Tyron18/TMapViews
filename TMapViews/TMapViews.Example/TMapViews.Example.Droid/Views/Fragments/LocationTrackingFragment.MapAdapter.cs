using Android.Content;
using Android.Gms.Maps;
using Android.Gms.Maps.Model;
using Android.Runtime;
using TMapViews.Droid.Adapters;
using TMapViews.Droid.Views;
using TMapViews.Example.Core.Models;
using TMapViews.Models;

namespace TMapViews.Example.Droid.Views.Fragments
{
    public partial class LocationTrackingFragment
    {
        internal class LocationTrackingAdapter : BindingMapAdapter
        {
            public Context Context;

            public LocationTrackingAdapter(Context context, BindingMapView mapView):base(mapView)
            {
                Context = context;
            }

            public override IJavaObject AddBindingMapOverlay(IBindingMapOverlay overlay)
            {
                return null;
            }

            public override MarkerOptions GetMarkerOptionsForPin(IBindingMapAnnotation pin)
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