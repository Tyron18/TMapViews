namespace TMapViews.Models
{
    public interface I3DLocation : I2DLocation
    {
        double? Altitude { get; }
        double? HorizontalAccuracy { get; }
        double? VerticalAccuracy { get; }
        double? Speed { get; }
    }
}