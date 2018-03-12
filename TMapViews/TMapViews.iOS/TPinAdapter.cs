using MapKit;
using System;
using System.Collections.Generic;
using System.Text;

namespace TMapViews.iOS
{
    public abstract class TAnnotationAdapter
    {
        public virtual MKAnnotationView GetViewForAnnotation(MKMapView mapView, TMKAnnotation annotation) => throw new NotImplementedException();
        public virtual MKOverlayRenderer OverlayRenderer(MKMapView mapView, TMKOverlay overlay) => throw new NotImplementedException();
    }
}
