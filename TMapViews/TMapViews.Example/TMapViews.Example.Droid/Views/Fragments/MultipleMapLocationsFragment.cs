using System.Collections.Generic;
using Android.OS;
using Android.Views;
using Android.Widget;
using MvvmCross.Binding.BindingContext;
using MvvmCross.Platforms.Android.Presenters.Attributes;
using TMapViews.Droid.Views;
using TMapViews.Example.Core.Converters;
using TMapViews.Example.Core.ViewModels;

namespace TMapViews.Example.Droid.Views.Fragments
{
    [MvxFragmentPresentation(typeof(MainContainerViewModel), Resource.Id.content_frame)]
    public partial class MultipleMapLocationsFragment : BaseFragment<MultipleMapLocationsViewModel>
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
            _mapView.Zoom = 70;
            _mapAdapter = new MultpileMapMarkersAdapter(this.Activity);

            _info = view.FindViewById<LinearLayout>(Resource.Id.layout_info);
            _latitude = view.FindViewById<TextView>(Resource.Id.txt_latitude);
            _longitude = view.FindViewById<TextView>(Resource.Id.txt_longitude);

            var bindingSet = this.CreateBindingSet<MultipleMapLocationsFragment, MultipleMapLocationsViewModel>();
            bindingSet.Bind(_mapView).For(v => v.CenterMapLocation).To(vm => vm.Center);
            bindingSet.Bind(_mapView).For(v => v.AnnotationSource).To(vm => vm.Pins);
            bindingSet.Bind(_mapView).For(v => v.MarkerClick).To(vm => vm.MarkerTappedCommand);
            bindingSet.Bind(_mapView).For(v => v.OverlayClick).To(vm => vm.MarkerTappedCommand);
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
            _mapView?.OnLowMemory();
        }
    }
}