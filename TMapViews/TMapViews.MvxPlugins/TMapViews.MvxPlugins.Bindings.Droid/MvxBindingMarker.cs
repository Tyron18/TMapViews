using Android.Gms.Maps.Model;
using MvvmCross.Base;
using MvvmCross.Binding.BindingContext;
using TMapViews.Models;

namespace TMapViews.MvxPlugins.Bindings.Droid
{
    public abstract class MvxBindingMarker : IMvxBindingContextOwner, IMvxDataConsumer
    {
        public MvxBindingMarker() : base()
        {
            this.CreateBindingContext(string.Empty);
        }

        public object DataContext { get => BindingContext.DataContext; set => BindingContext.DataContext = value; }
        public IMvxBindingContext BindingContext { get; set; }

        public Marker Marker { get; internal set; }
    }
}