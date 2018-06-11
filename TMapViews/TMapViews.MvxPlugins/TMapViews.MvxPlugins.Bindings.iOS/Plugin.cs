using System;
using MvvmCross;
using MvvmCross.Binding.Bindings.Target.Construction;
using MvvmCross.Plugin;
using TMapViews.iOS;

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
        }
    }
}