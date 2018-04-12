using CoreLocation;
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

        public static CLLocationCoordinate2D ToCLLocationCoordinate2D(this Binding2DLocation loc)
            => new CLLocationCoordinate2D(loc.Latitude, loc.Longitude);
    }
}