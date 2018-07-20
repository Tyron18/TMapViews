using System;
using MvvmCross;
using MvvmCross.Binding;
using MvvmCross.Binding.Bindings.Target;
using TMapViews.iOS;
using TMapViews.Models;

namespace TMapViews.MvxPlugins.Bindings.iOS.Bindings
{
    [Preserve(AllMembers = true)]
    public class BindingMKMapViewUserCurrentLocationTargetBinding : MvxTargetBinding<BindingMKMapView, I3DLocation>
    {
        private bool _subscribed;

        public BindingMKMapViewUserCurrentLocationTargetBinding(BindingMKMapView target) : base(target)
        {
        }

        public override MvxBindingMode DefaultMode => MvxBindingMode.OneWayToSource;

        protected override void SetValue(I3DLocation value)
        {
        }

        public override void SubscribeToEvents()
        {
            base.SubscribeToEvents();
            if (Target != null)
            {
                Target.UserLocationChanged += UserLocationChanged;
                _subscribed = true;
            }
        }

        protected override void Dispose(bool isDisposing)
        {
            base.Dispose(isDisposing);
            if (Target != null && _subscribed)
            {
                Target.UserLocationChanged -= UserLocationChanged;
                _subscribed = false;
            }
        }

        private void UserLocationChanged(object sender, I3DLocation e)
        {
            FireValueChanged(Target.UserCurrentLocation);
        }
    }
}