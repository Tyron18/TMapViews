using CoreLocation;
using MapKit;
using TMapViews.Models;

namespace TMapViews.iOS.Models
{
    public class BindingMKCircle : MKCircle, IBindingMKMapOverlay
    {
        protected MKMapRect _boundingMapRect;
        protected CLLocationCoordinate2D _coordinate;
        protected double _radius;

        public override MKMapRect BoundingMapRect => _boundingMapRect;
        public override CLLocationCoordinate2D Coordinate => _coordinate;
        public override double Radius => _radius;

        public IBindingMapAnnotation Annotation { get; set; }
        public MKOverlayRenderer Renderer { get; set; }

        private BindingMKCircle(MKCircle circle)

        {
            _boundingMapRect = circle.BoundingMapRect;
            _coordinate = circle.Coordinate;
            _radius = circle.Radius;
        }

        public new static BindingMKCircle Circle(CLLocationCoordinate2D withcenterCoordinate, double radius)
            => new BindingMKCircle(MKCircle.Circle(withcenterCoordinate, radius));
    }
}