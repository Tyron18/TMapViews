using CoreLocation;
using MapKit;
using TMapViews.Models;
using TMapViews.Models.Models;

namespace TMapViews.iOS
{
    public static class Extensions
    {
        public static Binding2DLocation ToBinding2DLocation(this CLLocationCoordinate2D loc)
            => new Binding2DLocation()
            {
                Latitude = loc.Latitude,
                Longitude = loc.Longitude
            };

        public static CLLocationCoordinate2D ToCLLocationCoordinate2D(this I2DLocation loc)
            => new CLLocationCoordinate2D(loc.Latitude, loc.Longitude);

        public static Binding3DLocation ToBinding3DLocation(this MKUserLocation loc)
            => new Binding3DLocation(
                loc.Location.Altitude,
                loc.Coordinate.Latitude,
                loc.Coordinate.Longitude,
                loc.Location.HorizontalAccuracy,
                loc.Location.VerticalAccuracy,
                loc.Location.Speed);
    }
}