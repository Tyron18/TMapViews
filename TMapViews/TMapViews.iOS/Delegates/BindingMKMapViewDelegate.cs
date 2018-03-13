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
            if (AnnotationSelected != null)
            {
                var annotation = view.Annotation as IMKAnnotation;
                if (AnnotationSelected.CanExecute(annotation))
                    AnnotationSelected.Execute(annotation);
            }
        }

        public sealed override void DidDeselectAnnotationView(MKMapView mapView, MKAnnotationView view)
        {
            if(AnnotationDeselected != null)
            {
                var annotation = view.Annotation as IMKAnnotation;
                if (AnnotationDeselected.CanExecute(annotation))
                    AnnotationDeselected.Execute(annotation);
            }
        }
    }
}
