using System;
using CoreLocation;
using MapKit;
using TMapViews.iOS.Models;
using TMapViews.Models.Interfaces;

namespace TMapViews.iOS
{
    public class BindingMKAnnotation : MKAnnotation, IBindingMKMapAnnotation
    {
        private string _title = "";
        private string _subtitle = "";

        public IBindingMapAnnotation Annotation { get; set; }
        public override string Title => _title;
        public override string Subtitle => _subtitle;

        public BindingMKAnnotation(IBindingMapAnnotation anno)
        {
            Annotation = anno;
        }

        public override CLLocationCoordinate2D Coordinate { get => Annotation.Location.ToCLLocationCoordinate2D(); }

        public void SetTitle(string title) => _title = title;

        public void SetSubtitle(string subtitle) => _subtitle = subtitle;
    }
}