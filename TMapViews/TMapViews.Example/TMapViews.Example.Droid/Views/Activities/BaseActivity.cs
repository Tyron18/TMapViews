using Android.OS;
using Android.Support.V7.Widget;
using MvvmCross.ViewModels;
using MvvmCross.Droid.Support.V7.AppCompat;
using Plugin.CurrentActivity;
using Plugin.Permissions;
using Android.Runtime;
using Android.Content;
using Android.Util;
using Android.Views;
using Android.Widget;
using MvvmCross.Binding.BindingContext;
using TMapViews.Example.Core.ViewModels;

namespace TMapViews.Example.Droid.Views
{
    public abstract class BaseActivity<TViewModel> : MvxAppCompatActivity<TViewModel>
        where TViewModel : class, IMvxViewModel
    {
        protected abstract int ActivityLayoutId { get; }

        protected Android.Support.V7.Widget.Toolbar Toolbar => FindViewById<Android.Support.V7.Widget.Toolbar>(Resource.Id.toolbar);

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            SetContentView(ActivityLayoutId);

            SetSupportActionBar(Toolbar);
        }

        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Android.Content.PM.Permission[] grantResults)
        {
            PermissionsImplementation.Current.OnRequestPermissionsResult(requestCode, permissions, grantResults);
            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }


       
    }
}