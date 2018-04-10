using Android.App;
using Android.OS;
using Android.Views;
using MvvmCross.Droid.Platform;
using TMapViews.Example.Core.ViewModels;

namespace TMapViews.Example.Droid.Views
{
    [Activity(
        Theme = "@style/TMapViews.Example.Droid",
        WindowSoftInputMode = SoftInput.AdjustPan | SoftInput.StateHidden)]
    public class MainActivity : BaseActivity<MainContainerViewModel>
    {
        protected override int ActivityLayoutId => Resource.Layout.layout_activity;
    }
}
