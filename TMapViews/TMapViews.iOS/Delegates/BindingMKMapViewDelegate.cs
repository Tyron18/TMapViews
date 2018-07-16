using MapKit;
using System.Linq;
using System.Windows.Input;
using TMapViews.iOS.Models;
using TMapViews.Models;

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
                    LocationChanged.Execute(coordinate);
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
            else if (view.Annotation is IBindingMKMapOverlay overlay && (overlay is BindingMKCircle))
            {
                if (OverlayClicked?.CanExecute(overlay.Annotation) ?? false)
                {
                    OverlayClicked.Execute(overlay.Annotation);
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
                        if (MarkerDragStart?.CanExecute(annotation.Annotation) ?? false)
                            MarkerDragStart.Execute(annotation.Annotation);
                        break;

                    case MKAnnotationViewDragState.Ending:
                        if (MarkerDragEnd?.CanExecute(annotation.Annotation) ?? false)
                            MarkerDragEnd.Execute(annotation.Annotation);
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
                return GetViewForBindingAnnotation(mapView, bAnno.Annotation);
            return null;
        }

        public virtual MKAnnotationView GetViewForBindingAnnotation(MKMapView mapView, IBindingMapAnnotation bindingMapAnnotation)
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
    }
}