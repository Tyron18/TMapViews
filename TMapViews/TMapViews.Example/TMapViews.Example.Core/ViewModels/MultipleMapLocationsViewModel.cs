using MvvmCross.Commands;
using MvvmCross.Navigation;
using MvvmCross.ViewModels;
using System.Threading.Tasks;
using TMapViews.Example.Core.Models;
using TMapViews.Models;

namespace TMapViews.Example.Core.ViewModels
{
    public class MultipleMapLocationsViewModel : MvxViewModel
    {
        private MvxObservableCollection<IBindingMapAnnotation> _pins;

        public MvxObservableCollection<IBindingMapAnnotation> Pins { get => _pins; set => SetProperty(ref _pins, value); }

        private MvxObservableCollection<IBindingMapOverlay> _overlays;
        public MvxObservableCollection<IBindingMapOverlay> Overlays { get => _overlays; set => SetProperty(ref _overlays, value); }

        private Binding2DLocation _center;
        public Binding2DLocation Center { get => _center; set => SetProperty(ref _center, value); }

        private IMvxCommand<ExampleBindingAnnotation> _markerTappedCommand;

        public IMvxCommand<ExampleBindingAnnotation> MarkerTappedCommand
            => _markerTappedCommand ?? (_markerTappedCommand = new MvxCommand<ExampleBindingAnnotation>(MarkerTapped));

        private IMvxCommand<ExampleBindingAnnotation> _markerDragEndCommand;

        public IMvxCommand<ExampleBindingAnnotation> MarkerDragEndCommand
            => _markerDragEndCommand ?? (_markerDragEndCommand = new MvxCommand<ExampleBindingAnnotation>(MarkerDragEnd));

        private IMvxCommand<ExampleBindingAnnotation> _markerDragStartCommand;

        public IMvxCommand<ExampleBindingAnnotation> MarkerDragStartCommand
            => _markerDragStartCommand ?? (_markerDragStartCommand = new MvxCommand<ExampleBindingAnnotation>(MarkerDragStart));

        private IMvxCommand<ExampleBindingAnnotation> _markerDragCommand;

        public IMvxCommand<ExampleBindingAnnotation> MarkerDragCommand
            => _markerDragCommand ?? (_markerDragCommand = new MvxCommand<ExampleBindingAnnotation>(MarkerDrag));

        private IMvxCommand _navigateToLocationTrackingCommand;

        public IMvxCommand NavigateToLocationTrackingCommand
            => _navigateToLocationTrackingCommand ?? (_navigateToLocationTrackingCommand = new MvxAsyncCommand(NavigateToLocationTracking));

        private Task NavigateToLocationTracking()
        => _navigationService.Navigate<LocationTrackingViewModel>();

        private double _latitude;
        private double _longitude;
        private bool _dragging;
        private IMvxNavigationService _navigationService;

        public MultipleMapLocationsViewModel(IMvxNavigationService navigationService)
        {
            _navigationService = navigationService;
        }

        public double Latitude
        {
            get => _latitude;
            set => SetProperty(ref _latitude, value);
        }

        public double Longitude
        {
            get => _longitude;
            set => SetProperty(ref _longitude, value);
        }

        public bool Dragging
        {
            get => _dragging;
            set => SetProperty(ref _dragging, value);
        }

        public override void Prepare()
        {
            Center = new Binding2DLocation
            {
                Longitude = 18.471485,
                Latitude = -33.957252
            };
            Pins = new MvxObservableCollection<IBindingMapAnnotation>
            {
                new ExampleBindingAnnotation
                {
                    Location = Center,
                    Id = 1
                },
                new ExampleBindingAnnotation
                {
                    Location = new Binding2DLocation
                    {
                        Latitude = Center.Latitude + 10,
                        Longitude = Center.Longitude
                    },
                    Id = 2
                },
                new ExampleBindingAnnotation
                {
                    Location = new Binding2DLocation
                    {
                        Latitude = Center.Latitude - 10,
                        Longitude = Center.Longitude
                    },
                    Id = 3
                },
                new ExampleBindingAnnotation
                {
                    Location = new Binding2DLocation
                    {
                        Latitude = Center.Latitude,
                        Longitude = Center.Longitude + 10,
                    },
                    Id = 4
                },
                new ExampleBindingAnnotation
                {
                    Location = new Binding2DLocation
                    {
                        Latitude = Center.Latitude,
                        Longitude = Center.Longitude - 10
                    },
                    Id = 5
                }
            };

            Overlays = new MvxObservableCollection<IBindingMapOverlay>
            {
                new ExampleBindingOverlay
                {
                    Location = Center,
                    Id = 1,
                    Radius = 2000000
                }
            };
        }

        private void MarkerTapped(ExampleBindingAnnotation obj)
        {
            if (obj.Id < 5)
                obj.Id++;
            else
                obj.Id = 1;

            RaisePropertyChanged(() => Pins);
        }

        private void MarkerDragStart(ExampleBindingAnnotation obj)
        {
            MarkerDrag(obj);
            Dragging = true;
        }

        private void MarkerDragEnd(ExampleBindingAnnotation obj)
            => Dragging = false;

        private void MarkerDrag(ExampleBindingAnnotation obj)
        {
            Latitude = obj.Location.Latitude;
            Longitude = obj.Location.Longitude;
        }
    }
}