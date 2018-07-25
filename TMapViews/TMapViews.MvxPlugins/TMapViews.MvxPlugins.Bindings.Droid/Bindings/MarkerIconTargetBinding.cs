using Android.Gms.Maps.Model;
using MvvmCross.Binding;
using MvvmCross.Platforms.Android.Binding.Target;

namespace TMapViews.MvxPlugins.Bindings.Droid
{
    public class MarkerIconTargetBinding : MvxAndroidTargetBinding<Marker, BitmapDescriptor>
    {
        public static string MarkerIconTargetBindingName => "BindIcon";

        public MarkerIconTargetBinding(Marker target) : base(target)
        {
        }

        public override MvxBindingMode DefaultMode => MvxBindingMode.OneWay;

        protected override void SetValueImpl(Marker target, BitmapDescriptor value)
        {
            target.SetIcon(value);
        }
    }
}