using System;
using MapKit;
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
            obj.RegisterCustomBindingFactory<BindingMKMapView>(nameof(BindingMKMapView.UserCurrentLocation), target => new BindingMKMapViewUserCurrentLocationTargetBinding(target));
            obj.RegisterCustomBindingFactory<BindingMKMapView>(nameof(BindingMKMapView.AnnotationSource), target => new BindingMKMapViewAnnotationTargetBinding(target));
            obj.RegisterCustomBindingFactory<BindingMKAnnotation>(nameof(BindingMKAnnotation.Title), target => new MKAnnotationTitleTargetBinding(target));
            obj.RegisterCustomBindingFactory<BindingMKAnnotation>(nameof(BindingMKAnnotation.Subtitle), target => new MKAnnotationSubtitleTargetBinding(target));
            obj.RegisterCustomBindingFactory<MKAnnotationView>(MKAnnotationViewScaleTargetBinding.MKAnnotationViewScaleTargetBindingString, target => new MKAnnotationViewScaleTargetBinding(target));
        }
    }
}