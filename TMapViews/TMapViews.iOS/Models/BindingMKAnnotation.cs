using CoreLocation;
using MapKit;
using TMapViews.iOS.Models;
using TMapViews.Models;

namespace TMapViews.iOS
{
    public class BindingMKAnnotation : MKAnnotation, IBindingMapAnnotation
    {
        private CLLocationCoordinate2D _coordinate;

        public BindingMKAnnotation() { }

        public override CLLocationCoordinate2D Coordinate { get => _coordinate; }

        public I2DLocation Location { get => Binding2DLocation.FromCLLocation(Coordinate); set => _coordinate = (value as Binding2DLocation).ToCLLocation(); }
    }
}