using Android.Content;
using Android.Gms.Maps;
using Android.Gms.Maps.Model;
using Android.Runtime;
using TMapViews.Droid;
using TMapViews.Droid.Adapters;
using TMapViews.Example.Core.Models;
using TMapViews.Models.Interfaces;

namespace TMapViews.Example.Droid.Views.Fragments
{
    public partial class MultipleMapLocationsFragment
    {
        internal class MultpileMapMarkersAdapter : IBindingMapAdapter
        {
            public MultpileMapMarkersAdapter(Context context)
            {
                Context = context;
            }

            public Context Context { get; }

            public IJavaObject AddBindingMapOverlay(GoogleMap googleMap, IBindingMapOverlay overlay)
            {
                CircleOptions circleOptions = null;

                if (overlay is ExampleBindingOverlay mOverlay)
                {
                    circleOptions = new CircleOptions()
                        .InvokeCenter(mOverlay.Location.ToLatLng())
                        .InvokeRadius(mOverlay.Radius)
                        .InvokeStrokeWidth(0)
                        .Clickable(true);

                    switch (mOverlay.Id)
                    {
                        case 1:
                            circleOptions.InvokeFillColor(Context.GetColor(Android.Resource.Color.HoloBlueLight));
                            break;
                        case 2:
                            circleOptions.InvokeFillColor(Context.GetColor(Android.Resource.Color.HoloRedLight));
                            break;
                        case 3:
                            circleOptions.InvokeFillColor(Context.GetColor(Android.Resource.Color.HoloGreenLight));
                            break;
                        case 4:
                            circleOptions.InvokeFillColor(Context.GetColor(Android.Resource.Color.HoloOrangeLight));
                            break;
                        case 5:
                            circleOptions.InvokeFillColor(Context.GetColor(Android.Resource.Color.HoloPurple));
                            break;
                    }
                }

                return circleOptions;
            }

            public MarkerOptions GetMarkerOptionsForPin(IBindingMapAnnotation pin)
            {
                MarkerOptions markerOptions = null;

                if (pin is ExampleBindingAnnotation mPin)
                {
                    markerOptions = new MarkerOptions();
                    markerOptions.SetPosition(new LatLng(pin.Location.Latitude, pin.Location.Longitude))
                    .SetTitle(mPin.Id.ToString())
                    .Draggable(true);
                    switch (mPin.Id)
                    {
                        case 1:
                            markerOptions.SetIcon(BitmapDescriptorFactory.DefaultMarker(BitmapDescriptorFactory.HueBlue));
                            break;
                        case 2:
                            markerOptions.SetIcon(BitmapDescriptorFactory.DefaultMarker(BitmapDescriptorFactory.HueRed));
                            break;
                        case 3:
                            markerOptions.SetIcon(BitmapDescriptorFactory.DefaultMarker(BitmapDescriptorFactory.HueGreen));
                            break;
                        case 4:
                            markerOptions.SetIcon(BitmapDescriptorFactory.DefaultMarker(BitmapDescriptorFactory.HueYellow));
                            break;
                        case 5:
                            markerOptions.SetIcon(BitmapDescriptorFactory.DefaultMarker(BitmapDescriptorFactory.HueViolet));
                            break;
                    }
                }
                return markerOptions;
            }
        }
    }
}