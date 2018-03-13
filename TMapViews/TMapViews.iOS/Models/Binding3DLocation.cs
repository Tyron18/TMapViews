using CoreLocation;
using MapKit;
using TMapViews.Models.Interfaces;

namespace TMapViews.iOS
{
    public class Binding3DLocation : CLLocation, I3DLocation
    {
        public double Latitude => Coordinate.Latitude;
        public double Longitude => Coordinate.Longitude;
    }
}