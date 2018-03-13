using System;
using System.Collections.Generic;
using System.Text;

namespace TMapViews.Models.Interfaces
{
    public interface I3DLocation
    {
        double Altitude { get; }
        double Latitude { get; }
        double Longitude { get; }
        double HorizontalAccuracy { get; }
        double VerticalAccuracy { get; }
        double Speed { get; }
    }
}
