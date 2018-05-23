using System;
using MvvmCross;
using MvvmCross.Binding;
using MvvmCross.Binding.Bindings.Target.Construction;
using MvvmCross.Platforms.Android.Binding.Target;
using MvvmCross.Platforms.Android.WeakSubscription;
using MvvmCross.Plugin;
using TMapViews.Droid.Views;
using TMapViews.Models.Models;

namespace TMapViews.MvxPlugins.Bindings.Droid
{
    public class BindingMapViewUserLocationChangedTargetBinding : MvxAndroidTargetBinding<BindingMapView, Binding3DLocation>
    {
        private MvxAndroidTargetEventSubscription<BindingMapView, Binding3DLocation> subscribeUserLocationChanged;

        public override MvxBindingMode DefaultMode => MvxBindingMode.OneWayToSource;

        public BindingMapViewUserLocationChangedTargetBinding(BindingMapView target) : base(target)
        {
        }

        protected override void SetValueImpl(BindingMapView target, Binding3DLocation value)
        {
        }

        public override void SubscribeToEvents()
        {
            if(Target != null)
            {
                subscribeUserLocationChanged = new MvxAndroidTargetEventSubscription<BindingMapView, Binding3DLocation>(Target, nameof(BindingMapView.UserLocationChanged), OnUserLocationChanged);
            }
        }

        private void OnUserLocationChanged(object sender, Binding3DLocation e)
        {
            if (Target != null)
                FireValueChanged(Target.UserLocation);
        }

        protected override void Dispose(bool isDisposing)
        {
            if(isDisposing)
            {
                subscribeUserLocationChanged?.Dispose();
                subscribeUserLocationChanged = null;
            }
            base.Dispose(isDisposing);
        }

    }
}