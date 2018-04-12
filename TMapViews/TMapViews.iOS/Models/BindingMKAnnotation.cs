using System;
using CoreLocation;
using MapKit;
using TMapViews.Models;
using TMapViews.Models.Interfaces;
using TMapViews.Models.Models;

namespace TMapViews.iOS
{
    public class BindingMKAnnotation : MKAnnotation
    {
        private string _title = "";
        private string _subtitle = "";

        public virtual string AnnotationIdentifier { get; set; } = "0";

        public IBindingMapAnnotation Annotation { get; private set; }
        public override string Title  => _title;
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