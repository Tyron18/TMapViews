﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using MapKit;
using TMapViews.iOS.Models;
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

        private ObservableCollection<IBindingMapPin> _annotationSource;
        public ObservableCollection<IBindingMapPin> AnnotationSource
        {
            get => _annotationSource;
            set
            {
                _annotationSource = value;
                UpdatePins();
            }
        }

        private ObservableCollection<IBindingMapOverlay> _overlaySource;
        public ObservableCollection<IBindingMapOverlay> OverlaySource
        {
            get => _overlaySource;
            set
            {
                _overlaySource = value;
                UpdateOverlays();
            }
        }


        public ICommand MapClick { get; set; }

        public override MKMapType MapType { get => base.MapType; set => base.MapType = value; }
        public override bool ShowsUserLocation { get => base.ShowsUserLocation; set => base.ShowsUserLocation = value; }
        public MKCoordinateSpan ZoomLevel { get; set; }
        public bool ShouldShowOverlays { get; private set; }

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
        private void UpdateOverlays()
        {
            RemoveOverlays(Overlays);
            if (ShouldShowOverlays)
            {
                foreach (var overlay in OverlaySource)
                    AddBindingOverlay(overlay);
            }
        }

        private void AddBindingOverlay(IBindingMapOverlay overlay)
        {
            if (overlay is BindingMKOverlay)
                AddOverlay(overlay);
            else
                throw new InvalidCastException($"Cannot convert type {overlay.GetType()} to MKOverlay");
        }

        private void AddBindingAnnotation(IBindingMapPin pin)
        {
            if (pin is BindingMKAnnotation mkPin)
                AddAnnotation(mkPin);
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