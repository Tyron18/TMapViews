using Android.App;
using Android.Content;
using Android.OS;
using Android.Util;
using Android.Views;
using Android.Widget;
using MvvmCross.Binding.BindingContext;
using TMapViews.Example.Core.ViewModels;

namespace TMapViews.Example.Droid.Views
{
    [Activity(
        Theme = "@style/TMapViews.Example.Droid",
        WindowSoftInputMode = SoftInput.AdjustPan | SoftInput.StateHidden)]
    public class MainActivity : BaseActivity<MainContainerViewModel>
    {
        protected override int ActivityLayoutId => Resource.Layout.layout_activity;

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            var locationTrackingButton = FindViewById<Button>(Resource.Id.location_trackig_button);
            var mapPinsButton = FindViewById<Button>(Resource.Id.map_pins_button);

            var bindingSet = this.CreateBindingSet<MainActivity, MainContainerViewModel>();
            bindingSet.Bind(locationTrackingButton).To(v => v.NavigateToLocationTrackingCommand);
            bindingSet.Bind(mapPinsButton).To(vm => vm.NavigateToMapPinsCommand);
            bindingSet.Apply();
        }
    }
}
