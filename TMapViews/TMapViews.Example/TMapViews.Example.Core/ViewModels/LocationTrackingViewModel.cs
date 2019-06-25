using MvvmCross.Commands;
using MvvmCross.Navigation;
using MvvmCross.ViewModels;
using Plugin.Permissions;
using Plugin.Permissions.Abstractions;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TMapViews.Example.Core.Models;
using TMapViews.Models;

namespace TMapViews.Example.Core.ViewModels
{
    public class LocationTrackingViewModel : MvxViewModel
    {
        private Binding3DLocation _userLocation;
        private bool _canTrackLocation;
        private MvxObservableCollection<IBindingMapAnnotation> _pins;
        private IMvxCommand<I3DLocation> _userLocationChangedCommand;
        private int _changeCount;

        public Binding3DLocation UserLocation
        {
            get => _userLocation;
            set => SetProperty(ref _userLocation, value);
        }

        public bool CanTrackLocation
        {
            get => _canTrackLocation;
            set => SetProperty(ref _canTrackLocation, value);
        }

        private IMvxNavigationService _navigationService;

        public MvxObservableCollection<IBindingMapAnnotation> Pins
        {
            get => _pins;
            set => SetProperty(ref _pins, value);
        }

        public IMvxCommand<I3DLocation> UserLocationChangedCommand
            => _userLocationChangedCommand ?? (_userLocationChangedCommand = new MvxCommand<I3DLocation>(OnUserLocationChanged));

        public IMvxCommand<I2DLocation> MapClickCommand
            => _mapClickedCommand ?? (_mapClickedCommand = new MvxCommand<I2DLocation>(OnMapClicked));

        private void OnMapClicked(I2DLocation obj)
        {
            this.Pins.RemoveItems(new List<IBindingMapAnnotation>{ this.Pins[0]});
        }

        public LocationTrackingViewModel(IMvxNavigationService navigationService)
        {
            _navigationService = navigationService;
            Pins = new MvxObservableCollection<IBindingMapAnnotation>();
        }

        private IMvxCommand _navigateToMapPinsCommand;
        private MvxCommand<I2DLocation> _mapClickedCommand;

        public IMvxCommand NavigateToMapPinsCommand
            => _navigateToMapPinsCommand ?? (_navigateToMapPinsCommand = new MvxAsyncCommand(NavigateToMapPins));

        private Task NavigateToMapPins()
            => _navigationService.Navigate<MultipleMapLocationsViewModel>();

        public override void ViewAppearing()
        {
            base.ViewAppearing();
            Task.Run(CheckPermissions);
        }

        private async Task CheckPermissions()
        {
            if (await CrossPermissions.Current.CheckPermissionStatusAsync(Permission.LocationWhenInUse) == PermissionStatus.Granted)
                CanTrackLocation = true;
            else
                CanTrackLocation = (await CrossPermissions.Current.RequestPermissionsAsync(Permission.LocationWhenInUse))[Permission.LocationWhenInUse] == PermissionStatus.Granted;
        }

        private void OnUserLocationChanged(I2DLocation loc)
        {
            if (_changeCount >= 5)
            {
                Pins.Add(new ExampleBindingAnnotation { Location = loc, Id = Pins.Count });
                RaisePropertyChanged(() => Pins);
                _changeCount = 0;
            }
            else
            {
                _changeCount++;
            }
        }
    }
}