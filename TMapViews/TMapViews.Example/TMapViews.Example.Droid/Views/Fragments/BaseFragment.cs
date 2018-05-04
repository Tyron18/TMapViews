using Android.OS;
using Android.Views;
using MvvmCross.Platforms.Android.Views.Fragments;
using MvvmCross.ViewModels;

namespace TMapViews.Example.Droid.Views
{
    public abstract class BaseFragment<TViewModel> : MvxFragment<TViewModel>
        where TViewModel : class, IMvxViewModel
    {
        protected abstract int FragmentLayoutId { get; }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            base.OnCreateView(inflater, container, savedInstanceState);

            // If your bindings are done in XML rather than codebehind, you need to call this.BindingInflate. If not, call inflater.Inflate instead
            var view = inflater.Inflate(FragmentLayoutId, null);

            return view;
        }
    }
}