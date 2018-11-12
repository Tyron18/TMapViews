using Android.Content;
using Android.Gms.Maps.Model;
using Android.Graphics.Drawables;
using Android.Runtime;
using MvvmCross.Binding.BindingContext;
using System.Collections.Generic;
using TMapViews.Droid;
using TMapViews.Droid.Views;
using TMapViews.Example.Core.Models;
using TMapViews.Models;
using TMapViews.MvxPlugins.Bindings.Droid;

namespace TMapViews.Example.Droid.Views.Fragments
{
    public partial class MultipleMapLocationsFragment
    {
        internal class MultpileMapMarkersAdapter : MvxBindingMapViewAdapter
        {
            public MultpileMapMarkersAdapter(Context context, BindingMapView mapView) : base(mapView)
            {
                Context = context;
            }

            public Context Context { get; }

            public override IJavaObject AddBindingMapOverlay(IBindingMapOverlay overlay)
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
                return new ExampleMvxBindingMarker(Context);
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
        public Context Context { get; }

        public ExampleMvxBindingMarker(Context context)
        {
            Context = context;
            this.DelayBind(() =>
    {
        MvxFluentBindingDescriptionSet<ExampleMvxBindingMarker, ExampleBindingAnnotation> bindingSet = this.CreateBindingSet<ExampleMvxBindingMarker, ExampleBindingAnnotation>();
        bindingSet.Bind(this).For(v => v.Icon).To(vm => vm.Id).WithDictionaryConversion(new Dictionary<int, Drawable>
        {
                    {1, Context.GetDrawable(Resource.Drawable.marker_a)},
                    {2, Context.GetDrawable(Resource.Drawable.marker_b)},
                    {3, Context.GetDrawable(Resource.Drawable.marker_c)},
                    {4, Context.GetDrawable(Resource.Drawable.marker_d)},
                    {5, Context.GetDrawable(Resource.Drawable.marker_e)},
        });
        bindingSet.Bind(this).For(v => v.IconScale).To(vm => vm.Selected).WithDictionaryConversion(new Dictionary<bool, float>
        {
                    { true, 1.3f },
                    {false, 1/1.3f }
        });
        bindingSet.Apply();
    });
        }
    }
}