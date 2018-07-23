using Android.Content;
using Android.Gms.Maps;
using Android.Gms.Maps.Model;
using Android.Runtime;
using MvvmCross.Binding.BindingContext;
using System.Collections.Generic;
using TMapViews.Droid;
using TMapViews.Droid.Adapters;
using TMapViews.Example.Core.Models;
using TMapViews.Models;
using TMapViews.MvxPlugins.Bindings.Droid;

namespace TMapViews.Example.Droid.Views.Fragments
{
    public partial class MultipleMapLocationsFragment
    {
        internal class MultpileMapMarkersAdapter : MvxBindingMapViewAdapter
        {
            public MultpileMapMarkersAdapter(Context context)
            {
                Context = context;
            }

            public Context Context { get; }

            public override IJavaObject AddBindingMapOverlay(GoogleMap googleMap, IBindingMapOverlay overlay)
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

            public override MvxBindingMarker GetMvxBindingMarker()
            {
                return new ExampleMvxBindingMarker();
            }

            //public MarkerOptions GetMarkerOptionsForPin(IBindingMapAnnotation pin)
            //{
            //    MarkerOptions markerOptions = null;

            // if (pin is ExampleBindingAnnotation mPin) { markerOptions = new
            // MarkerOptions(); markerOptions.SetPosition(new
            // LatLng(pin.Location.Latitude, pin.Location.Longitude))
            // .SetTitle(mPin.Id.ToString()) .Draggable(true); switch (mPin.Id) {
            // case 1:
            // markerOptions.SetIcon(BitmapDescriptorFactory.DefaultMarker(BitmapDescriptorFactory.HueBlue)); break;

            // case 2:
            // markerOptions.SetIcon(BitmapDescriptorFactory.DefaultMarker(BitmapDescriptorFactory.HueRed)); break;

            // case 3:
            // markerOptions.SetIcon(BitmapDescriptorFactory.DefaultMarker(BitmapDescriptorFactory.HueGreen)); break;

            // case 4:
            // markerOptions.SetIcon(BitmapDescriptorFactory.DefaultMarker(BitmapDescriptorFactory.HueYellow)); break;

            //            case 5:
            //                markerOptions.SetIcon(BitmapDescriptorFactory.DefaultMarker(BitmapDescriptorFactory.HueViolet));
            //                break;
            //        }
            //    }
            //    return markerOptions;
            //}
        }
    }

    internal class ExampleMvxBindingMarker : MvxBindingMarker
    {
        public ExampleMvxBindingMarker()
        {
            this.DelayBind(() =>
            {
                var bindingSet = this.CreateBindingSet<ExampleMvxBindingMarker, ExampleBindingAnnotation>();
                bindingSet.Bind(Marker).For(v => v.BindIcon()).To(vm => vm.Id).WithDictionaryConversion(new Dictionary<int, BitmapDescriptor>
                {
                    {1, BitmapDescriptorFactory.DefaultMarker(BitmapDescriptorFactory.HueBlue)},
                    {2, BitmapDescriptorFactory.DefaultMarker(BitmapDescriptorFactory.HueRed)},
                    {3, BitmapDescriptorFactory.DefaultMarker(BitmapDescriptorFactory.HueGreen)},
                    {4, BitmapDescriptorFactory.DefaultMarker(BitmapDescriptorFactory.HueYellow)},
                    {5, BitmapDescriptorFactory.DefaultMarker(BitmapDescriptorFactory.HueViolet)},
                }, BitmapDescriptorFactory.DefaultMarker(BitmapDescriptorFactory.HueMagenta));
                bindingSet.Apply();
            });
        }
    }
}