using CoreLocation;
using TMapViews.Models;

namespace TMapViews.iOS
{
    public class Binding2DLocation : I2DLocation
    {
        public double Latitude { get; set; }
        public double Longitude { get; set; }

        public static Binding2DLocation FromCLLocation(CLLocationCoordinate2D coordinate) =>
            new Binding2DLocation()
            {
                Latitude = coordinate.Latitude,
                Longitude = coordinate.Longitude
            };

        public CLLocationCoordinate2D ToCLLocation() =>
            new CLLocationCoordinate2D()
            {
                Latitude = Latitude,
                Longitude = Longitude
            };
    }
}