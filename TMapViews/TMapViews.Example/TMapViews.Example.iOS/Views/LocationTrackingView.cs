using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Cirrious.FluentLayouts.Touch;
using Foundation;
using MvvmCross.Binding.BindingContext;
using TMapViews.Example.Core.ViewModels;
using TMapViews.iOS;
using UIKit;

namespace TMapViews.Example.iOS.Views
{
    public partial class LocationTrackingView : BaseViewController<LocationTrackingViewModel>
    {
        private BindingMKMapView _mapView;
        private LocationTrackingMapDelegate _delegate;

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();

            _delegate = new LocationTrackingMapDelegate();

            _mapView = new BindingMKMapView(_delegate)
            {
                TranslatesAutoresizingMaskIntoConstraints = false
            };

            View.Add(_mapView);

            View.AddConstraints(new FluentLayout[]
            {
                _mapView.AtTopOf(View),
                _mapView.AtLeftOf(View),
                _mapView.AtRightOf(View),
                _mapView.AtBottomOf(View)
            });

            var bindingSet = this.CreateBindingSet<LocationTrackingView, LocationTrackingViewModel>();

            bindingSet.Bind(_mapView).For(v => v.UserCurrentLocation).To(vm => vm.UserLocation);
            bindingSet.Bind(_mapView).For(v => v.CenterMapLocation).To(vm => vm.UserLocation);
            bindingSet.Bind(_mapView).For(v => v.ShowsUserLocation).To(vm => vm.CanTrackLocation);
            bindingSet.Bind(_mapView).For(v => v.AnnotationSource).To(vm => vm.Pins);
            bindingSet.Bind(_delegate).For(v => v.LocationChanged).To(vm => vm.UserLocationChangedCommand);

            _mapView.Delegate = _delegate;

            bindingSet.Apply();
        }
    }
}