using MapKit;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using TMapViews.Models.Interfaces;
using UIKit;

namespace TMapViews.iOS
{
    public class BindingMKMapViewDelegate : MKMapViewDelegate
    {
        public ICommand LocationChanged { get; set; }
        public ICommand AnnotationSelected { get; set; }
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
