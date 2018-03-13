using CoreLocation;
using MapKit;
using System;
using System.Collections.Generic;
using System.Text;
using TMapViews.Models;

namespace TMapViews.iOS
{
    public static class Extensions
    {
        public static ITLocation ToTLocation(this CLLocationCoordinate2D coordinate)
            => new ITLocation()
            {
                Latitude = coordinate.Latitude,
                Longitude = coordinate.Longitude
            };

        public static ITLocation ToTLocation(this CLLocation location)
            => new ITLocation()
            {
                Latitude = location.Coordinate.Latitude,
                Longitude = location.Coordinate.Longitude,
                Altitude = location.Altitude
            };

        public static CLLocationCoordinate2D GetCLLocationCoordinate2D(this ITLocation location)
            => new CLLocationCoordinate2D(location.Latitude, location.Longitude);
    }
}
