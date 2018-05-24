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
    public class LocationTrackingView : BaseViewController<LocationTrackingViewModel>
    {
        private BindingMKMapView _mapView;

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();

            _mapView = new BindingMKMapView()
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

            bindingSet.Apply();
        }
    }
}