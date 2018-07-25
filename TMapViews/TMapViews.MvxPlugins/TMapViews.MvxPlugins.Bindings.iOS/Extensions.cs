using MapKit;
using TMapViews.MvxPlugins.Bindings.iOS.Bindings;

namespace TMapViews.MvxPlugins.Bindings.iOS
{
    public static class Extensions
    {
        public static string BindScale(this MKAnnotationView view) => MKAnnotationViewScaleTargetBinding.MKAnnotationViewScaleTargetBindingString;
    }
}