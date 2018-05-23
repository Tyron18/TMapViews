using MvvmCross.IoC;
using MvvmCross.Navigation;
using MvvmCross.ViewModels;
using Plugin.Permissions;
using Plugin.Permissions.Abstractions;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TMapViews.Models;
using TMapViews.Models.Models;

namespace TMapViews.Example.Core.ViewModels
{
    public class LocationTrackingViewModel : MvxViewModel
    {

        private Binding3DLocation _userLocation;
        private bool _canTrackLocation;

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
    }
}
