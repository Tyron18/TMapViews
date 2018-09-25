using Android.Runtime;
using Android.Views;
using MvvmCross.Base;
using MvvmCross.Binding.BindingContext;
using MvvmCross.Platforms.Android.Binding.BindingContext;

namespace TMapViews.MvxPlugins.Bindings.Droid
{
    [Preserve(AllMembers = true)]
    public class MvxBindingInfoViewHolder : Java.Lang.Object, IMvxBindingContextOwner, IMvxDataConsumer
    {
        public View View { get; set; }

        public MvxBindingInfoViewHolder(View view, IMvxAndroidBindingContext bindingContext) : base()
        {
            this.CreateBindingContext(string.Empty);
            View = view;
            BindingContext = bindingContext;
        }

        public object DataContext { get => BindingContext.DataContext; set => BindingContext.DataContext = value; }
        public IMvxBindingContext BindingContext { get; set; }

    }
}