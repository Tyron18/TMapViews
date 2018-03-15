using MapKit;
using System;
using System.Collections.ObjectModel;
using System.Windows.Input;
using TMapViews.Models;
using UIKit;

namespace TMapViews.iOS
{
    public class BindingMKMapView : MKMapView
    {
        public BindingMKMapView()
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

        private MKCoordinateSpan _zoomLevel;

        public MKCoordinateSpan ZoomLevel
        {
            get => _zoomLevel;
            set
            {
                _zoomLevel = value;
                CenterMap();
            }
        }

        private bool _shouldShowOverlays;

        public bool ShouldShowOverlays
        {
            get => _shouldShowOverlays;
            set
            {
                _shouldShowOverlays = value;
                UpdateOverlays();
            }
        }

        private Binding2DLocation _centerMapLocation;

        public Binding2DLocation CenterMapLocation
        {
            get => _centerMapLocation;
            set
            {
                _centerMapLocation = value;
                CenterMap();
            }
        }

        private ObservableCollection<IBindingMapAnnotation> _annotationSource;

        public ObservableCollection<IBindingMapAnnotation> AnnotationSource
        {
            get => _annotationSource;
            set
            {
                _annotationSource = value;
                UpdatePins();
            }
        }

        public new BindingMKMapViewDelegate Delegate { get => base.Delegate as BindingMKMapViewDelegate; set => base.Delegate = value; }

        public ICommand MapClick { get; set; }

        public override MKMapType MapType { get => base.MapType; set => base.MapType = value; }
        public override bool ShowsUserLocation { get => base.ShowsUserLocation; set => base.ShowsUserLocation = value; }

        private void CenterMap()
        {
            if (CenterMapLocation != null)
            {
                var region = new MKCoordinateRegion
                {
                    Center = CenterMapLocation.ToCLLocation(),
                    Span = ZoomLevel
                };
                SetRegion(region, true);
            }
        }

        private void UpdatePins()
        {
            RemoveAnnotations(Annotations);
            if (ShouldShowPins)
            {
                foreach (var pin in AnnotationSource)
                    AddBindingAnnotation(pin);
            }
        }

        private void AddBindingAnnotation(IBindingMapAnnotation pin)
        {
            if (pin is BindingMKAnnotation mkPin)
                AddAnnotation(mkPin);
            else if (pin is BindingMKOverlay mkOverlay)
                AddAnnotation(mkOverlay);
            else
                throw new InvalidCastException($"Cannot converter type {pin.GetType()} to MKAnnotation");
        }

        private void OnMapClicked(UITapGestureRecognizer gesture)
        {
            if (MapClick != null)
            {
                var loc = Binding2DLocation.FromCLLocation(ConvertPoint(gesture.LocationInView(this), this));
                if (MapClick.CanExecute(loc))
                    MapClick.Execute(loc);
            }
        }
    }
}