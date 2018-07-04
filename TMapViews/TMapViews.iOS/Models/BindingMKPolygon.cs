using CoreLocation;
using Foundation;
using MapKit;
using TMapViews.Models;

namespace TMapViews.iOS.Models
{
    public class BindingMKPolygon : NSObject, IBindingMKMapOverlay
    {
        public IBindingMapAnnotation Annotation { get; set; }
        public MKPolygon Polygon { get; set; }

        public MKMapRect BoundingMapRect => Polygon.BoundingMapRect;
        public CLLocationCoordinate2D Coordinate => Polygon.Coordinate;
        public MKPolygon[] InteriorPolygons => Polygon.InteriorPolygons;

        public MKOverlayRenderer Renderer { get; set; }

        private BindingMKPolygon(MKPolygon polygon)
        {
            Polygon = polygon;
        }

        public static BindingMKPolygon FromCoordinates(CLLocationCoordinate2D[] coords)
            => new BindingMKPolygon(MKPolygon.FromCoordinates(coords));

        public static BindingMKPolygon FromCoordinates(CLLocationCoordinate2D[] coords, MKPolygon[] interiorPolygons)
            => new BindingMKPolygon(MKPolygon.FromCoordinates(coords, interiorPolygons));

        public static BindingMKPolygon FromPoints(MKMapPoint[] points)
            => new BindingMKPolygon(MKPolygon.FromPoints(points));

        public static BindingMKPolygon FromPoints(MKMapPoint[] points, MKPolygon[] interiorPolygons)
            => new BindingMKPolygon(MKPolygon.FromPoints(points, interiorPolygons));
    }
}