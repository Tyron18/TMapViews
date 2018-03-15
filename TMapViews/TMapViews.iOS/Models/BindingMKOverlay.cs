using CoreLocation;
using MapKit;
using TMapViews.Models;

namespace TMapViews.iOS
{
    public class BindingMKOverlay : MKOverlay, IBindingMapAnnotation
    {
        private CLLocationCoordinate2D _coordinate;

        public override MKMapRect BoundingMapRect => throw new System.NotImplementedException();

        public override CLLocationCoordinate2D Coordinate => _coordinate;

        public I2DLocation Location { get => Binding2DLocation.FromCLLocation(Coordinate); set => _coordinate = (value as Binding2DLocation).ToCLLocation(); }
    }
}