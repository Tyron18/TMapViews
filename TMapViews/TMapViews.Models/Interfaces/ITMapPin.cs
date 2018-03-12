using System;
using System.Collections.Generic;
using System.Text;

namespace TMapViews.Models
{
    public interface ITMapPin
    {
        TLocation Location { get; set; }
        double OverlayRadius { get; set; }
    }
}
