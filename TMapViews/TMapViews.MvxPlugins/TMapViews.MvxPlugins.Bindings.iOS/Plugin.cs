using System;
using MvvmCross;
using MvvmCross.Binding.Bindings.Target.Construction;
using MvvmCross.Plugin;
using TMapViews.iOS;
using TMapViews.iOS.Models;
using TMapViews.MvxPlugins.Bindings.iOS.Bindings;

namespace TMapViews.MvxPlugins.Bindings.iOS
{
    [MvxPlugin]
    [Preserve(AllMembers = true)]
    public class Plugin : IMvxPlugin
    {
        public void Load()
        {
            Mvx.CallbackWhenRegistered<IMvxTargetBindingFactoryRegistry>(RegisterFactories);
        }

        private void RegisterFactories(IMvxTargetBindingFactoryRegistry obj)
        {
            obj.RegisterCustomBindingFactory<BindingMKMapView>(nameof(BindingMKMapView.UserCurrentLocation), view => new BindingMKMapViewUserCurrentLocationTargetBinding(view));
            obj.RegisterCustomBindingFactory<BindingMKMapView>(nameof(BindingMKMapView.AnnotationSource), view => new BindingMKMapViewAnnotationTargetBinding(view));
            obj.RegisterCustomBindingFactory<BindingMKAnnotation>(nameof(BindingMKAnnotation.Title), view => new MKAnnotationTitleTargetBinding(view));
            obj.RegisterCustomBindingFactory<BindingMKAnnotation>(nameof(BindingMKAnnotation.Subtitle), view => new MKAnnotationSubtitleTargetBinding(view));
        }
    }
}