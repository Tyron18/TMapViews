using Android.Gms.Maps.Model;
using Android.Locations;
using TMapViews.Models;
using TMapViews.Models.Models;

namespace TMapViews.Droid
{
    public static class Extensions
    {
        public static LatLng ToLatLng(this I2DLocation location)
            => new LatLng(location.Latitude, location.Longitude);

        public static Binding2DLocation ToBinding2DLocation(this LatLng latLng)
            => new Binding2DLocation()
            {
                Latitude = latLng.Latitude,
                Longitude = latLng.Longitude
            };

        public static Binding3DLocation ToBinding3DLocation(this Location location)
            => new Binding3DLocation(
                location.HasAltitude ? (double?)location.Altitude : null, 
                location.Latitude,
                location.Longitude,
                location.HasAccuracy? (double?)location.Accuracy : null,
                location.HasVerticalAccuracy ? (double?)location.VerticalAccuracyMeters : null,
                location.HasSpeed ? (double?)location.Speed : null);
    }
}