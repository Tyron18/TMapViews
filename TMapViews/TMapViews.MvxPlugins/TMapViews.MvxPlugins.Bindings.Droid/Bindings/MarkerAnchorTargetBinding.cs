using Android.Gms.Maps.Model;
using MvvmCross.Platforms.Android.Binding.Target;

namespace TMapViews.MvxPlugins.Bindings.Droid
{
    public class MarkerAnchorTargetBinding : MvxAndroidTargetBinding<Marker, (float x, float y)>
    {
        public static string AnchorBinding => "BindAnchor";

        public MarkerAnchorTargetBinding(Marker target) : base(target)
        {
        }

        protected override void SetValueImpl(Marker target, (float x, float y) value)
        {
            target.SetAnchor(value.x, value.y);
        }
    }
}