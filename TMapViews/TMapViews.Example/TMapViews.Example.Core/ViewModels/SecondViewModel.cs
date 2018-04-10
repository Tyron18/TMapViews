using MvvmCross.Core.ViewModels;

namespace TMapViews.Example.Core.ViewModels
{
    public class SecondViewModel : MvxViewModel
    {
        #region Commands

        IMvxCommand _goBackCommand;
        public IMvxCommand GoBackCommand =>
            _goBackCommand ?? (_goBackCommand = new MvxCommand(() => Close(this)));

        #endregion
    }
}
