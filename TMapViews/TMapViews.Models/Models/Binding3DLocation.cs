using System;
using System.Collections.Generic;
using System.Text;
using TMapViews.Models.Interfaces;

namespace TMapViews.Models.Models
{
    public class Binding3DLocation : I3DLocation
    {
        public Binding3DLocation(
            double? altitude = null, 
            double latitude = default(double), 
            double longitude = default(double), 
            double? horizontalAccuracy = null, 
            double? verticalAccuracy = null,
            double? speed = null)
        {
            Altitude = altitude;
            Latitude = latitude;
            Longitude = longitude;
            HorizontalAccuracy = horizontalAccuracy;
            VerticalAccuracy = verticalAccuracy;
            Speed = speed;
        }

        public double? Altitude { get; private set; }

        public double Latitude { get; private set; }

        public double Longitude { get; private set; }

        public double? HorizontalAccuracy { get; private set; }

        public double? VerticalAccuracy { get; private set; }

        public double? Speed { get; private set; }
    }
}
