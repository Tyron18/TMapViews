using MapKit;
using System.Windows.Input;
using TMapViews.Models.Interfaces;

namespace TMapViews.iOS
{
    public class BindingMKMapViewDelegate : MKMapViewDelegate
    {
        /// <summary>
        /// Executes when user location changes, passing an I3DLocation object.
        /// </summary>
        public ICommand LocationChanged { get; set; }

        /// <summary>
        /// Executes when an annotation is selected, passing an IMKAnnotation object.
        /// </summary>
        public ICommand AnnotationSelected { get; set; }

        /// <summary>
        /// Executes when an annotation is deselected, passing an IMKAnnotation object.
        /// </summary>
        public ICommand AnnotationDeselected { get; set; }

        public ICommand MyLocationClick { get; set; }
        public ICommand MarkerDrag { get; set; }
        public ICommand MarkerDragEnd { get; set; }
        public ICommand MarkerDragStart { get; set; }
        public ICommand CameraMoved { get; set; }

        public sealed override void DidUpdateUserLocation(MKMapView mapView, MKUserLocation userLocation)
        {
            if (LocationChanged != null)
            {
                var coordinate = userLocation as I3DLocation;
                if (LocationChanged.CanExecute(coordinate))
                    LocationChanged.Equals(coordinate);
            }
        }

        public sealed override void DidSelectAnnotationView(MKMapView mapView, MKAnnotationView view)
        {
            if (view.Annotation is BindingMKAnnotation annotation)
            {
                if (AnnotationSelected?.CanExecute(annotation) ?? false)
                    AnnotationSelected.Execute(annotation);
            }
            else if (view.Annotation == mapView.UserLocation)
            {
                var loc = Binding2DLocation.FromCLLocation(view.Annotation.Coordinate);
                if (MyLocationClick?.CanExecute(loc) ?? false)
                    MyLocationClick.Execute(loc);
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
            if (AnnotationDeselected != null)
            {
                var annotation = view.Annotation as IMKAnnotation;
                if (AnnotationDeselected.CanExecute(annotation))
                    AnnotationDeselected.Execute(annotation);
            }
        }

        public override void RegionChanged(MKMapView mapView, bool animated)
        {
            var pos = new Binding3DLocation(mapView.Camera.CenterCoordinate.Latitude, mapView.Camera.CenterCoordinate.Longitude, mapView.Camera.Altitude);
            if (CameraMoved?.CanExecute(pos) ?? false)
                CameraMoved.Execute(pos);
        }
    }
}