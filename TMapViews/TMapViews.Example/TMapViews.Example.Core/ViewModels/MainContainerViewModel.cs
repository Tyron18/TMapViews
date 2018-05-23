using MvvmCross.Commands;
using MvvmCross.IoC;
using MvvmCross.Navigation;
using MvvmCross.ViewModels;
using System.Threading.Tasks;

namespace TMapViews.Example.Core.ViewModels
{
    public class MainContainerViewModel : MvxViewModel
    {
        public IMvxNavigationService NavigationService { get; set; }

        public MainContainerViewModel(IMvxNavigationService navigationService)
        {
            NavigationService = navigationService;
        }

        private IMvxCommand _navigateToLocationTrackingCommand;
        public IMvxCommand NavigateToLocationTrackingCommand
            => _navigateToLocationTrackingCommand ?? (_navigateToLocationTrackingCommand = new MvxAsyncCommand(NavigateToLocationTracking));

        private Task NavigateToLocationTracking()
        => NavigationService.Navigate<LocationTrackingViewModel>();

        private IMvxCommand _navigateToMapPinsCommand;
        public IMvxCommand NavigateToMapPinsCommand
            => _navigateToMapPinsCommand ?? (_navigateToMapPinsCommand = new MvxAsyncCommand(NavigateToMapPins));

        private Task NavigateToMapPins()
            => NavigationService.Navigate<MultipleMapLocationsViewModel>();
    }
}
