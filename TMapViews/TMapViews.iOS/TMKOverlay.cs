using CoreLocation;
using MapKit;
using System;
using System.Collections.Generic;
using System.Text;
using TMapViews.Models.Interfaces;

namespace TMapViews.iOS
{
    public class TMKOverlay : MKOverlay, ITMapOverlay
    {
        public override MKMapRect BoundingMapRect => throw new NotImplementedException();

        public override CLLocationCoordinate2D Coordinate => throw new NotImplementedException();
    }
}
