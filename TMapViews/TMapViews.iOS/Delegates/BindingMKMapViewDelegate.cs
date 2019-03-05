using Cirrious.FluentLayouts.Touch;
using CoreGraphics;
using MapKit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Input;
using TMapViews.iOS.Models;
using TMapViews.iOS.Views;
using TMapViews.Models;
using UIKit;

namespace TMapViews.iOS
{
    public class BindingMKMapViewDelegate : MKMapViewDelegate
    {
        

        /// <summary>
        /// Executes when user location changes. Passes a <paramref name="I3DLocation"/>.
        /// </summary>
        public ICommand LocationChanged { get; set; }

        /// <summary>
        /// Executes when an annotation is clicked. Passes a <paramref name="BindingMKAnnotation"/>.
        /// </summary>
        public ICommand MarkerClick { get; set; }

        /// <summary>
        /// Executes when an annotation is selected. Passes a <paramref name="BindingMKOverlay"/>.
        /// </summary>
        public ICommand OverlayClicked { get; set; }

        /// <summary>
        /// Executes whan an annotation is selected. Passes a <paramref name="BindingMKAnnotation"/>
        /// </summary>
        public ICommand MarkerSelected { get; set; }

        /// <summary>
        /// Executes when an annotation is deselected. Passes a <paramref name="BindingMKAnnotation"/>.
        /// </summary>
        public ICommand MarkerDeselected { get; set; }

        /// <summary>
        /// Executes when an overlay is deselected. Passes a <paramref name="BindingMKOverlay"/>.
        /// </summary>
        public ICommand OverlayDeslected { get; set; }

        /// <summary>
        /// Executes when the user location annotation is clicked. Passes a
        /// <paramref name="Binding2DLocation"/>.
        /// </summary>
        public ICommand MyLocationClick { get; set; }

        /// <summary>
        /// Executes when a marker is dragged. Passes a <paramref name="BindingMKAnnotation"/>.
        /// </summary>
        public ICommand MarkerDrag { get; set; }

        /// <summary>
        /// Executes when a marker stops being dragged. Passes a <paramref name="BindingMKAnnotation"/>.
        /// </summary>
        public ICommand MarkerDragEnd { get; set; }

        /// <summary>
        /// Executes when a marker starts being dragged. Passes a <paramref name="BindingMKAnnotation"/>.
        /// </summary>
        public ICommand MarkerDragStart { get; set; }

        /// <summary>
        /// Executes when the camera moves. Passes a <paramref name="Binding3DLocation"/>.
        /// </summary>
        public ICommand CameraMoved { get; set; }

        /// <summary>
        /// Executes when a callout is clicked. Passes a <paramref name="IBindingMapAnnotation"/>
        /// </summary>
        public ICommand CalloutClicked { get; set; }

        private IEnumerable<IBindingMapAnnotation> _annotationSource;

        public IEnumerable<IBindingMapAnnotation> AnnotationSource
        {
            get => _annotationSource;
            set
            {
                _annotationSource = value;
                UpdatePins();
            }
        }

        private IEnumerable<IBindingMapOverlay> _overlaySource;
        private BindingMKMapView _mapView;

        public IEnumerable<IBindingMapOverlay> OverlaySource
        {
            get => _overlaySource;
            set
            {
                _overlaySource = value;
                UpdatePins();
            }
        }

        public BindingMKMapViewDelegate(BindingMKMapView mapView)
        {
            _mapView = mapView;
        }

        public void UpdatePins()
        {
            _mapView.RemoveAnnotations(_mapView.Annotations);
            _mapView.RemoveOverlays(_mapView.Overlays);
            if (_mapView.AnnotationsVisible)
            {
                if (AnnotationSource != null)
                    foreach (var pin in AnnotationSource)
                        AddBindingAnnotation(pin);

                if (OverlaySource != null)
                    foreach (var overlay in OverlaySource)
                        AddBindingOverlay(overlay);
            }
        }

        private void AddBindingAnnotation(IBindingMapAnnotation pin)
        {
            AddAnnotation(new BindingMKAnnotation(pin));
        }

        private void AddBindingOverlay(IBindingMapOverlay overlay)
        {
            var mapOverlay = GetViewForBindingOverlay(_mapView, overlay);
            if (mapOverlay != null)
            {
                if (mapOverlay is BindingMKPolyline polyLine)
                {
                    AddOverlay(polyLine.PolyLine);
                }
                else if (mapOverlay is BindingMKPolygon polygon)
                {
                    AddOverlay(polygon.Polygon);
                }
                else
                {
                    mapOverlay.Annotation = overlay;
                    AddOverlay(mapOverlay);
                }
            }
        }

        public void AddAnnotation(IMKAnnotation annotation) => _mapView.AddAnnotation(annotation);
        public void AddOverlay(IMKOverlay overlay) => _mapView.AddOverlay(overlay);
        public void RemoveAnnotation(IMKAnnotation annotation) => _mapView.RemoveAnnotation(annotation);
        public void RemoveOverlay(IMKOverlay overlay) => _mapView.RemoveOverlay(overlay);

        public IMKAnnotation[] Annotations => _mapView.Annotations;
        public IMKOverlay[] Overlays => _mapView.Overlays;
               
        public sealed override void DidUpdateUserLocation(MKMapView mapView, MKUserLocation userLocation)
        {
            var coordinate = userLocation.ToBinding3DLocation();
            if (mapView is BindingMKMapView v)
                v.UserCurrentLocation = coordinate;
            if (LocationChanged != null)
            {
                if (LocationChanged.CanExecute(coordinate))
                    LocationChanged.Execute(coordinate);
            }
        }

        public sealed override void DidSelectAnnotationView(MKMapView mapView, MKAnnotationView view)
        {
            if (view.Annotation is BindingMKAnnotation annotation)
            {
                if (MarkerSelected?.CanExecute(annotation.Annotation) ?? false)
                {
                    MarkerSelected.Execute(annotation.Annotation);
                }
                var callout = GetViewForCallout(mapView, annotation.Annotation);
                if(callout != null)
                {
                    callout.Annotation = annotation.Annotation;
                    callout.SetNeedsLayout();
                    callout.LayoutIfNeeded();
                    view.AddSubview(callout);
                    callout.XPosLayout = callout.WithSameCenterX(view).Plus(callout.XOffset);
                    callout.YPosLayout = callout.WithSameCenterY(view).Plus(callout.YOffset);
                    view.AddConstraints(
                        new FluentLayout[]
                        {
                            callout.YPosLayout,
                            callout.XPosLayout
                        });
                    RegisterTapEvents(callout);
                }
            }
            else if (view.Annotation is IBindingMKMapOverlay overlay && (overlay is BindingMKCircle))
            {
                if (OverlayClicked?.CanExecute(overlay.Annotation) ?? false)
                {
                    OverlayClicked.Execute(overlay.Annotation);
                }
            }
            else if (view.Annotation == mapView.UserLocation)
            {
                var loc = view.Annotation.Coordinate.ToBinding2DLocation();
                if (MyLocationClick?.CanExecute(loc) ?? false)
                {
                    MyLocationClick.Execute(loc);
                }
            }
        }

        public override void ChangedDragState(MKMapView mapView, MKAnnotationView annotationView, MKAnnotationViewDragState newState, MKAnnotationViewDragState oldState)
        {
            if (annotationView.Annotation is BindingMKAnnotation annotation)
            {
                switch (newState)
                {
                    case MKAnnotationViewDragState.Starting:
                        if (MarkerDragStart?.CanExecute(annotation.Annotation) ?? false)
                            MarkerDragStart.Execute(annotation.Annotation);
                        annotationView.SetDragState(MKAnnotationViewDragState.Dragging, true);
                        break;

                    case MKAnnotationViewDragState.Ending:
                        if (MarkerDragEnd?.CanExecute(annotation.Annotation) ?? false)
                            MarkerDragEnd.Execute(annotation.Annotation);
                        annotationView.SetDragState(MKAnnotationViewDragState.None, true);
                        break;

                    case MKAnnotationViewDragState.Dragging:
                        if (MarkerDrag?.CanExecute(annotation.Annotation) ?? false)
                            MarkerDrag.Execute(annotation.Annotation);
                        break;
                }
            }
        }

        public sealed override void DidDeselectAnnotationView(MKMapView mapView, MKAnnotationView view)
        {
            if (view.Annotation is BindingMKAnnotation annotation)
            {
                if (MarkerDeselected?.CanExecute(annotation.Annotation) ?? false)
                    MarkerDeselected.Execute(annotation.Annotation);
                var callout = view?.Subviews.FirstOrDefault(x => x is BindingMKCalloutView);
                callout?.RemoveFromSuperview();
            }
            else if (view.Annotation is IBindingMKMapOverlay overlay)
            {
                if (OverlayDeslected?.CanExecute(overlay.Annotation) ?? false)
                    OverlayDeslected.Execute(overlay.Annotation);
            }
        }

        public sealed override void RegionChanged(MKMapView mapView, bool animated)
        {
            var pos = new Binding3DLocation(mapView.Camera.CenterCoordinate.Latitude, mapView.Camera.CenterCoordinate.Longitude, mapView.Camera.Altitude);
            if (CameraMoved?.CanExecute(pos) ?? false)
                CameraMoved.Execute(pos);
        }

        public sealed override MKAnnotationView GetViewForAnnotation(MKMapView mapView, IMKAnnotation annotation)
        {
            if (annotation is BindingMKAnnotation bAnno)
            {
                var view = GetViewForBindingAnnotation(mapView, bAnno.Annotation);
                RegisterTapEvents(view);
                return view;
            }
            return null;
        }

        private void RegisterTapEvents(UIView view)
        {
            var tap = new UITapGestureRecognizer(HandleAnnotationTap) { CancelsTouchesInView = true };
            view.AddGestureRecognizer(tap);
        }

        private void HandleAnnotationTap(UITapGestureRecognizer gesture)
        {
            if (gesture.View is MKAnnotationView view && view.Annotation is BindingMKAnnotation anno)
            {
                if (MarkerClick?.CanExecute(anno.Annotation) ?? false)
                    MarkerClick.Execute(anno.Annotation);
            }
            else if(gesture.View is BindingMKCalloutView callout)
            {
                if (CalloutClicked?.CanExecute(callout.Annotation) ?? false)
                    CalloutClicked.Execute(callout.Annotation);
            }
        }

        public virtual BindingMKAnnotationView GetViewForBindingAnnotation(MKMapView mapView, IBindingMapAnnotation bindingMapAnnotation)
        => null;

        public sealed override MKOverlayView GetViewForOverlay(MKMapView mapView, IMKOverlay overlay)
            => base.GetViewForOverlay(mapView, overlay);

        public override MKOverlayRenderer OverlayRenderer(MKMapView mapView, IMKOverlay overlay)
        {
            if (overlay is IBindingMKMapOverlay bOverlay)
                return bOverlay.Renderer;
            return null;
        }

        public virtual IBindingMKMapOverlay GetViewForBindingOverlay(MKMapView mapView, IBindingMapOverlay bindingMapOverlay) => null;

        public virtual BindingMKCalloutView GetViewForCallout(MKMapView mapView, IBindingMapAnnotation bindingMapAnnotation) => null;
    }
}