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
        public static TLocation ToTLocation(this CLLocationCoordinate2D coordinate)
            => new TLocation()
            {
                Latitude = coordinate.Latitude,
                Longitude = coordinate.Longitude
            };

        public static TLocation ToTLocation(this CLLocation location)
            => new TLocation()
            {
                Latitude = location.Coordinate.Latitude,
                Longitude = location.Coordinate.Longitude,
                Altitude = location.Altitude
            };

        public static CLLocationCoordinate2D GetCLLocationCoordinate2D(this TLocation location)
            => new CLLocationCoordinate2D(location.Latitude, location.Longitude);
    }
}
