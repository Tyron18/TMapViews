using System;
using CoreLocation;
using Foundation;
using TMapViews.Models.Interfaces;

namespace TMapViews.iOS
{
    public class Binding3DLocation : CLLocation, I3DLocation
    {
        private double _altitude;
        public override double Altitude => _altitude;

        public Binding3DLocation()
        {
        }

        public Binding3DLocation(NSCoder coder) : base(coder)
        {
        }

        public Binding3DLocation(double latitude, double longitude, double altitude = 0.0) : base(latitude, longitude)
        {
            _altitude = altitude;
        }

        public Binding3DLocation(CLLocationCoordinate2D coordinate, double altitude, double hAccuracy, double vAccuracy, NSDate timestamp) : base(coordinate, altitude, hAccuracy, vAccuracy, timestamp)
        {
            _altitude = hAccuracy;
        }

        public Binding3DLocation(CLLocationCoordinate2D coordinate, double altitude, double hAccuracy, double vAccuracy, double course, double speed, NSDate timestamp) : base(coordinate, altitude, hAccuracy, vAccuracy, course, speed, timestamp)
        {
            _altitude = hAccuracy;
        }

        protected Binding3DLocation(NSObjectFlag t) : base(t)
        {
        }

        protected internal Binding3DLocation(IntPtr handle) : base(handle)
        {
        }

        public double Latitude => Coordinate.Latitude;
        public double Longitude => Coordinate.Longitude;
    }
}