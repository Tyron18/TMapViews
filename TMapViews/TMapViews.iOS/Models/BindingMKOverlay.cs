using CoreLocation;
using MapKit;
using TMapViews.Models;
using TMapViews.Models.Interfaces;
using TMapViews.Models.Models;

namespace TMapViews.iOS
{
    public class BindingMKOverlay : MKOverlay, IBindingMapOverlay
    {
        private CLLocationCoordinate2D _coordinate;

        public override MKMapRect BoundingMapRect => throw new System.NotImplementedException();

        public override CLLocationCoordinate2D Coordinate => _coordinate;

        public I2DLocation Location { get => Coordinate.ToBinding2DLocation(); set => _coordinate = (value as Binding2DLocation).ToCLLocationCoordinate2D(); }
    }
}