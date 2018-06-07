using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CoreLocation;
using Foundation;
using MapKit;
using TMapViews.Models;
using TMapViews.Models.Interfaces;
using UIKit;

namespace TMapViews.iOS.Models
{
    public class BindingMKCircle : MKCircle, IBindingMKMapOverlay
    {
        protected MKMapRect _boundingMapRect;
        protected CLLocationCoordinate2D _coordinate;
        protected double _radius;

        public override MKMapRect BoundingMapRect => _boundingMapRect;
        public override CLLocationCoordinate2D Coordinate => _coordinate;
        public override double Radius => _radius;

        public IBindingMapAnnotation Annotation { get; set; }

        private BindingMKCircle(MKCircle circle)

        {
            _boundingMapRect = circle.BoundingMapRect;
            _coordinate = circle.Coordinate;
            _radius = circle.Radius;
        }

        public new static BindingMKCircle Circle(CLLocationCoordinate2D withcenterCoordinate, double radius)
        {
            var x = MKCircle.Circle(withcenterCoordinate, radius);
            try
            {
                return new BindingMKCircle(x);
            }
            catch (Exception e)
            {
                throw;
            }
        }
    }
}