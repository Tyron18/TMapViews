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
using Android.Support.V4.Content;
using Android.Views;
using Android.Widget;
using Java.Lang;
using MvvmCross.Binding.BindingContext;
using MvvmCross.Droid.Views.Attributes;
using TMapViews.Droid;
using TMapViews.Droid.Adapters;
using TMapViews.Droid.Views;
using TMapViews.Example.Core.ViewModels;
using TMapViews.Example.Droid.Converters;
using TMapViews.Models;
using TMapViews.Models.Interfaces;

namespace TMapViews.Example.Droid.Views.Fragments
{
    [MvxFragmentPresentation(typeof(MainContainerViewModel), Resource.Id.content_frame)]
    public class MultipleMapLocationsFragment : BaseFragment<MultipleMapLocationsViewModel>
    {
        protected override int FragmentLayoutId => Resource.Layout.fragment_multiple_map_locations;
        private BindingMapView _mapView;
        private MultpileMapMarkersAdapter _mapAdapter;
        private TextView _longitude, _latitude;
        private LinearLayout _info;

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            var view = base.OnCreateView(inflater, container, savedInstanceState);

            _mapView = view.FindViewById<BindingMapView>(Resource.Id.binding_map_view);
            _mapView.Zoom = 3;
            _mapAdapter = new MultpileMapMarkersAdapter(this.Activity);

            _info = view.FindViewById<LinearLayout>(Resource.Id.layout_info);
            _latitude = view.FindViewById<TextView>(Resource.Id.txt_latitude);
            _longitude = view.FindViewById<TextView>(Resource.Id.txt_longitude);

            var bindingSet = this.CreateBindingSet<MultipleMapLocationsFragment, MultipleMapLocationsViewModel>();
            bindingSet.Bind(_mapView).For(v => v.CenterMapLocation).To(vm => vm.Center);
            bindingSet.Bind(_mapView).For(v => v.AnnotationSource).To(vm => vm.Pins);
            bindingSet.Bind(_mapView).For(v => v.MarkerClick).To(vm => vm.MarkerTappedCommand);
            bindingSet.Bind(_mapView).For(v => v.MarkerDragStart).To(vm => vm.MarkerDragStartCommand);
            bindingSet.Bind(_mapView).For(v => v.MarkerDragEnd).To(vm => vm.MarkerDragEndCommand);
            bindingSet.Bind(_mapView).For(v => v.MarkerDrag).To(vm => vm.MarkerDragCommand);
            bindingSet.Bind(_info).For(v => v.Visibility).To(vm => vm.Dragging).WithDictionaryConversion
                (
                    new Dictionary<bool, ViewStates>
                    {
                        { true, ViewStates.Visible },
                        { false,ViewStates.Gone }
                    }
                );
            bindingSet.Bind(_latitude).To(vm => vm.Latitude).WithConversion<ParseDoubleValueConverter>();
            bindingSet.Bind(_longitude).To(vm => vm.Longitude).WithConversion<ParseDoubleValueConverter>();
            bindingSet.Apply();

            return view;
        }

        public override void OnResume()
        {
            base.OnResume();
            _mapView?.Initialize(Activity, _mapAdapter);
        }

        public override void OnPause()
        {
            base.OnPause();
            _mapView?.OnPause();
        }

        public override void OnSaveInstanceState(Bundle outState)
        {
            base.OnSaveInstanceState(outState);
            _mapView?.OnSaveInstanceState(outState);
        }

        public override void OnDestroy()
        {
            _mapView?.OnDestroy();
            base.OnDestroy();
        }

        public override void OnLowMemory()
        {
            base.OnLowMemory();
            _mapView.OnLowMemory();
        }
    }

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
                    .InvokeStrokeWidth(0);

                switch (mOverlay.Id)
                {
                    case 1:
                        circleOptions.InvokeFillColor(ContextCompat.GetColor(Context, Android.Resource.Color.HoloBlueLight));
                        break;
                    case 2:
                        circleOptions.InvokeFillColor(ContextCompat.GetColor(Context, Android.Resource.Color.HoloRedLight));
                        break;
                    case 3:
                        circleOptions.InvokeFillColor(ContextCompat.GetColor(Context, Android.Resource.Color.HoloGreenLight));
                        break;
                    case 4:
                        circleOptions.InvokeFillColor(ContextCompat.GetColor(Context, Android.Resource.Color.HoloOrangeLight));
                        break;
                    case 5:
                        circleOptions.InvokeFillColor(ContextCompat.GetColor(Context, Android.Resource.Color.HoloPurple));
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