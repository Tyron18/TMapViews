using MapKit;
using System.Windows.Input;
using TMapViews.Models.Interfaces;
using TMapViews.Models.Models;

namespace TMapViews.iOS
{
    public class BindingMKMapViewDelegate : MKMapViewDelegate
    {
        /// <summary>
        /// Executes when user location changes. Passes a <paramref name="I3DLocation"/>.
        /// </summary>
        public ICommand LocationChanged { get; set; }

        /// <summary>
        /// Executes when an annotation is selected. Passes a <paramref name="BindingMkAnnotation"/>.
        /// </summary>
        public ICommand MarkerClick { get; set; }

        /// <summary>
        /// Executes when an annotation is selected. Passes a <paramref name="BindingMKOverlay"/>.
        /// </summary>
        public ICommand OverlayClicked { get; set; }

        /// <summary>
        /// Executes when an annotation is deselected. Passes a <paramref name="BindingMkAnnotation"/>.
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
        /// Executes when a marker is dragged. Passes a <paramref name="BindingMkAnnotation"/>.
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
        
        public sealed override void DidUpdateUserLocation(MKMapView mapView, MKUserLocation userLocation)
        {
            var coordinate = userLocation.ToBinding3DLocation();
            if (mapView is BindingMKMapView v)
                v.UserCurrentLocation = coordinate;
            if (LocationChanged != null)
            {
                if (LocationChanged.CanExecute(coordinate))
                    LocationChanged.Equals(coordinate);
            }
        }

        public sealed override void DidSelectAnnotationView(MKMapView mapView, MKAnnotationView view)
        {
            if (view.Annotation is BindingMKAnnotation annotation)
            {
                if (MarkerClick?.CanExecute(annotation.Annotation) ?? false)
                {
                    MarkerClick.Execute(annotation.Annotation);
                    view.Selected = false;
                }
            }
            else if (view.Annotation is BindingMKOverlay overlay)
            {
                if (OverlayClicked?.CanExecute(overlay) ?? false)
                {
                    OverlayClicked.Execute(overlay);
                    view.Selected = false;
                }
            }
            else if (view.Annotation == mapView.UserLocation)
            {
                var loc = view.Annotation.Coordinate.ToBinding2DLocation();
                if (MyLocationClick?.CanExecute(loc) ?? false)
                {
                    MyLocationClick.Execute(loc);
                    view.Selected = false;
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
                        if (MarkerDragStart?.CanExecute(annotation) ?? false)
                            MarkerDragStart.Execute(annotation);
                        break;

                    case MKAnnotationViewDragState.Ending:
                        if (MarkerDragEnd?.CanExecute(annotation) ?? false)
                            MarkerDragEnd.Execute(annotation);
                        break;

                    case MKAnnotationViewDragState.Dragging:
                        if (MarkerDrag?.CanExecute(annotation) ?? false)
                            MarkerDrag.Execute(annotation);
                        break;
                }
            }
        }

        public sealed override void DidDeselectAnnotationView(MKMapView mapView, MKAnnotationView view)
        {
            if (view.Annotation is BindingMKAnnotation annotation)
            {
                if (MarkerDeselected?.CanExecute(annotation) ?? false)
                    MarkerDeselected.Execute(annotation);
            }
            else if (view.Annotation is BindingMKOverlay overlay)
            {
                if (OverlayDeslected?.CanExecute(overlay) ?? false)
                    OverlayDeslected.Execute(overlay);
            }
        }

        public sealed override void RegionChanged(MKMapView mapView, bool animated)
        {
            var pos = new Binding3DLocation(mapView.Camera.CenterCoordinate.Latitude, mapView.Camera.CenterCoordinate.Longitude, mapView.Camera.Altitude);
            if (CameraMoved?.CanExecute(pos) ?? false)
                CameraMoved.Execute(pos);
        }
    }
}