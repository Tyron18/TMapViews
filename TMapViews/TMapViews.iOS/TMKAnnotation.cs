using CoreLocation;
using MapKit;
using TMapViews.Models;

namespace TMapViews.iOS
{
    internal class TMKAnnotation : MKAnnotation, ITMapPin
    {
        public TMKAnnotation() { }

        public TMKAnnotation(ITMapPin pin)
        {
            Location = pin.Location;
            OverlayRadius = pin.OverlayRadius;
        }

        private CLLocationCoordinate2D _coordinate;
        public override CLLocationCoordinate2D Coordinate => _coordinate;

        public TLocation Location { get => Coordinate.ToTLocation(); set => SetCoordinate(value.GetCLLocationCoordinate2D()); }
        public double OverlayRadius { get => throw new System.NotImplementedException(); set => throw new System.NotImplementedException(); }

    }
}