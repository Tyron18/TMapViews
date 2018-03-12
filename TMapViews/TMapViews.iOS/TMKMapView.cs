using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Windows.Input;
using CoreLocation;
using MapKit;
using TMapViews.Models;
using UIKit;

namespace TMapViews.iOS
{
    public class TMKMapView : MKMapView
    {
        public TMKMapView()
        {
            var gestureRecognizer = new UITapGestureRecognizer(this.OnMapClicked);
            AddGestureRecognizer(gestureRecognizer);
        }

        private bool _shouldShowPins = true;

        public bool ShouldShowPins
        {
            get => _shouldShowPins;
            set
            {
                _shouldShowPins = value;
                UpdatePins();
            }
        }

        private TLocation _centerMapLocation;
        public TLocation CenterMapLocation
        {
            get => _centerMapLocation;
            set
            {
                _centerMapLocation = value;
                CenterMap();
            }
        }

        private ObservableCollection<ITMapPin> _pinItemSource;
        public ObservableCollection<ITMapPin> PinItemSource
        {
            get => _pinItemSource;
            set
            {
                _pinItemSource = value;
                UpdatePins();
            }
        }

        public ICommand MapClick { get; set; }

        public override MKMapType MapType { get => base.MapType; set => base.MapType = value; }
        public override bool ShowsUserLocation { get => base.ShowsUserLocation; set => base.ShowsUserLocation = value; }
        public MKCoordinateSpan ZoomLevel { get; set; }

        private void CenterMap()
        {
            if (CenterMapLocation != null)
            {
                var region = new MKCoordinateRegion
                {
                    Center = CenterMapLocation.GetCLLocationCoordinate2D(),
                    Span = ZoomLevel
                };
                SetRegion(region, true);
            }
        }

        private void UpdatePins()
        {
            ClearMap();
            if (ShouldShowPins)
            {
                foreach (var pin in PinItemSource)
                    AddAnnotation(pin);
            }
        }

        private void AddAnnotation(ITMapPin pin)
        {
            lock (pin)
            {
                var overlay = MKCircle.Circle(pin.Location.GetCLLocationCoordinate2D(), pin.OverlayRadius);
                AddOverlay(overlay);

                var pinAnntation = new TMKAnnotation(pin);
                AddAnnotation(pinAnntation);
            }
        }

        private void ClearMap()
        {
            RemoveAnnotations(Annotations);
            if (Overlays != null)
                RemoveOverlays(Overlays);
        }

        private void OnMapClicked(UITapGestureRecognizer gesture)
        {
            if (MapClick != null)
            {
                var loc = ConvertPoint(gesture.LocationInView(this), this).ToTLocation();
                if (MapClick.CanExecute(loc))
                    MapClick.Execute(loc);
            }
        }
    }
}