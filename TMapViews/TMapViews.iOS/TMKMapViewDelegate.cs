using MapKit;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using UIKit;

namespace TMapViews.iOS
{
    public class TMKMapViewDelegate : MKMapViewDelegate
    {
        public ICommand LocationChanged { get; set; }

        public TAnnotationAdapter Adapter { get; set; }

        public override void DidUpdateUserLocation(MKMapView mapView, MKUserLocation userLocation)
        {
            if (LocationChanged != null)
            {
                var coordinate = userLocation.Location.ToTLocation();
                if (LocationChanged.CanExecute(coordinate))
                    LocationChanged.Equals(coordinate);
            }
        }

        public override MKAnnotationView GetViewForAnnotation(MKMapView mapView, IMKAnnotation annotation)
        {
            var mkAnnotation = base.GetViewForAnnotation(mapView, annotation);
            if (annotation is TMKAnnotation && Adapter != null)
            {
                try
                {
                    mkAnnotation = Adapter.GetViewForAnnotation(mapView, annotation as TMKAnnotation);
                }
                catch (NotImplementedException)
                {
                    return mkAnnotation;
                }
                catch (Exception e)
                {
                    throw e;
                }
            }
            return mkAnnotation;
        }

        public override MKOverlayRenderer OverlayRenderer(MKMapView mapView, IMKOverlay overlay)
        {
            var overlayRenderer = base.OverlayRenderer(mapView, overlay);
            if (overlay is TMKOverlay && Adapter != null)
            {
                try
                {
                    overlayRenderer = Adapter.OverlayRenderer(mapView, overlay as TMKOverlay);
                }
                catch(NotImplementedException)
                {
                    return overlayRenderer;
                }
                catch(Exception e)
                {
                    throw e;
                }
            }
            return overlayRenderer;
        }
    }
}
