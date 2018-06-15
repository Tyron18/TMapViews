namespace TMapViews.Models
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

        public double Latitude { get; set; }

        public double Longitude { get; set; }

        public double? HorizontalAccuracy { get; private set; }

        public double? VerticalAccuracy { get; private set; }

        public double? Speed { get; private set; }
    }
}