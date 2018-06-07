using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CoreLocation;
using Foundation;
using MapKit;
using TMapViews.Models.Interfaces;
using UIKit;

namespace TMapViews.iOS.Models
{
    public class BindingMKPolygon : MKPolygon, IBindingMKMapOverlay
    {
        private MKMapRect _boundingMapRect;
        private CLLocationCoordinate2D _coordinate;
        private MKPolygon[] _interiorPolygons;

        public IBindingMapAnnotation Annotation { get; set; }

        public override MKMapRect BoundingMapRect => _boundingMapRect;
        public override CLLocationCoordinate2D Coordinate => _coordinate;
        public override MKPolygon[] InteriorPolygons => _interiorPolygons;

        private BindingMKPolygon(MKPolygon polygon)
        {
            _boundingMapRect = polygon.BoundingMapRect;
            _coordinate = polygon.Coordinate;
            _interiorPolygons = polygon.InteriorPolygons;
        }

        public new static BindingMKPolygon FromCoordinates(CLLocationCoordinate2D[] coords)
            => new BindingMKPolygon(MKPolygon.FromCoordinates(coords));

        public new static BindingMKPolygon FromCoordinates(CLLocationCoordinate2D[] coords, MKPolygon[] interiorPolygons)
            => new BindingMKPolygon(MKPolygon.FromCoordinates(coords, interiorPolygons));

        public new static BindingMKPolygon FromPoints(MKMapPoint[] points)
            => new BindingMKPolygon(MKPolygon.FromPoints(points));

        public new static BindingMKPolygon FromPoints(MKMapPoint[] points, MKPolygon[] interiorPolygons)
            => new BindingMKPolygon(MKPolygon.FromPoints(points, interiorPolygons));
    }
}