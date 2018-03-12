using CoreLocation;
using MapKit;
using TMapViews.Models;

namespace TMapViews.iOS
{
    public class TMKAnnotation : MKAnnotation, ITMapPin
    {
        public TMKAnnotation() { }

        public TMKAnnotation(ITMapPin pin)
        {
            Location = pin.Location;
            OverlayRadius = pin.OverlayRadius;
        }

        private CLLocationCoordinate2D _coordinate;
        private double _overlayRadius;

        public override CLLocationCoordinate2D Coordinate => _coordinate;

        public TLocation Location { get => Coordinate.ToTLocation(); set => SetCoordinate(value.GetCLLocationCoordinate2D()); }
        public double OverlayRadius { get => _overlayRadius; set => _overlayRadius = value; }

    }
}