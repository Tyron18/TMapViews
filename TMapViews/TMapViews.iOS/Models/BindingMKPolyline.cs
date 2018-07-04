using CoreLocation;
using Foundation;
using MapKit;
using TMapViews.Models;

namespace TMapViews.iOS.Models
{
    public class BindingMKPolyline : NSObject, IBindingMKMapOverlay
    {
        public IBindingMapAnnotation Annotation { get; set; }
        public MKPolyline PolyLine { get; set; }

        public MKMapRect BoundingMapRect => PolyLine.BoundingMapRect;
        public CLLocationCoordinate2D Coordinate => PolyLine.Coordinate;

        public MKOverlayRenderer Renderer { get; set; }

        private BindingMKPolyline(MKPolyline polyline)
        {
            PolyLine = polyline;
        }

        public static BindingMKPolyline FromCoordinates(CLLocationCoordinate2D[] coords)
            => new BindingMKPolyline(MKPolyline.FromCoordinates(coords));

        public static BindingMKPolyline FromPoints(MKMapPoint[] points)
            => new BindingMKPolyline(MKPolyline.FromPoints(points));
    }
}