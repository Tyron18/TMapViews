using Android.Gms.Maps.Model;

namespace TMapViews.MvxPlugins.Bindings.Droid
{
    public static class Extensions
    {
        public static string BindIcon(this Marker marker) => MarkerIconTargetBinding.MarkerIconTargetBindingName;

        public static string BindAnchor(this Marker marker) => MarkerAnchorTargetBinding.AnchorBinding;
    }
}