using Android.OS;
using Android.Views;
using MvvmCross.Binding.BindingContext;
using MvvmCross.Platforms.Android.Presenters.Attributes;
using TMapViews.Droid.Views;
using TMapViews.Example.Core.ViewModels;

namespace TMapViews.Example.Droid.Views.Fragments
{
    [MvxFragmentPresentation(typeof(MainContainerViewModel), Resource.Id.content_frame)]
    public class LocationTrackingFragment : BaseFragment<LocationTrackingViewModel>
    {
        protected override int FragmentLayoutId => Resource.Layout.fragment_location_tracking;
        private BindingMapView _mapView;

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            View view = base.OnCreateView(inflater, container, savedInstanceState);

            _mapView = view.FindViewById<BindingMapView>(Resource.Id.map_view);

            BindViews();

            return view;
        }

        private void BindViews()
        {
            var bindingSet = this.CreateBindingSet<LocationTrackingFragment, LocationTrackingViewModel>();
            bindingSet.Bind(_mapView).For(v => v.UserLocation).To(vm => vm.UserLocation);
            bindingSet.Bind(_mapView).For(v => v.MyLocationEnabled).To(vm => vm.CanTrackLocation);
            bindingSet.Bind(_mapView).For(v => v.CenterMapLocation).To(vm => vm.UserLocation);

            bindingSet.Apply();
        }

        public override void OnResume()
        {
            base.OnResume();
            _mapView?.Initialize(Activity);
        }

        public override void OnPause()
        {
            _mapView?.OnPause();
            base.OnPause();
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