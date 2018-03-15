using Android.Gms.Maps.Model;
using TMapViews.Models;

namespace TMapViews.Droid.Models
{
    public class Binding2DLocation : I2DLocation
    {
        public double Latitude { get; set; }
        public double Longitude { get; set; }

        public static Binding2DLocation FromLatLng(LatLng latLng)
            => new Binding2DLocation
            {
                Latitude = latLng.Latitude,
                Longitude = latLng.Longitude
            };

        public LatLng LatLng
            => new LatLng(Latitude, Longitude);
    }
}