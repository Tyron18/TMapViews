using System;
using MvvmCross;
using MvvmCross.Binding.Bindings.Target.Construction;
using MvvmCross.Plugin;
using TMapViews.Droid.Views;

namespace TMapViews.MvxPlugins.Bindings.Droid
{
    [MvxPlugin]
    [Preserve(AllMembers = true)]
    public class Plugin: IMvxPlugin
    {
        public void Load()
        {
            Mvx.CallbackWhenRegistered<IMvxTargetBindingFactoryRegistry>(RegisterFactories);
        }

        private void RegisterFactories(IMvxTargetBindingFactoryRegistry registry)
        {

            registry?.RegisterCustomBindingFactory<BindingMapView>(nameof(BindingMapView.UserLocation), target => new BindingMapViewUserLocationChangedTargetBinding(target));
        }
    }
}