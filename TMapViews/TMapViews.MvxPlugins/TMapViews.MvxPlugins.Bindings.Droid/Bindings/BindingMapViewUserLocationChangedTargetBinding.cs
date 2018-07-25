using MvvmCross.Binding;
using MvvmCross.Platforms.Android.Binding.Target;
using MvvmCross.Platforms.Android.WeakSubscription;
using TMapViews.Droid.Views;
using TMapViews.Models;

namespace TMapViews.MvxPlugins.Bindings.Droid
{
    public class BindingMapViewUserLocationChangedTargetBinding : MvxAndroidTargetBinding<BindingMapView, I3DLocation>
    {
        private MvxAndroidTargetEventSubscription<BindingMapView, I3DLocation> subscribeUserLocationChanged;

        public override MvxBindingMode DefaultMode => MvxBindingMode.OneWayToSource;

        public BindingMapViewUserLocationChangedTargetBinding(BindingMapView target) : base(target)
        {
        }

        protected override void SetValueImpl(BindingMapView target, I3DLocation value)
        {
        }

        public override void SubscribeToEvents()
        {
            if (Target != null)
            {
                subscribeUserLocationChanged = new MvxAndroidTargetEventSubscription<BindingMapView, I3DLocation>(Target, nameof(BindingMapView.UserLocationChanged), OnUserLocationChanged);
            }
        }

        private void OnUserLocationChanged(object sender, I3DLocation e)
        {
            if (Target != null)
                FireValueChanged(Target.UserLocation);
        }

        protected override void Dispose(bool isDisposing)
        {
            if (isDisposing)
            {
                subscribeUserLocationChanged?.Dispose();
                subscribeUserLocationChanged = null;
            }
            base.Dispose(isDisposing);
        }
    }
}