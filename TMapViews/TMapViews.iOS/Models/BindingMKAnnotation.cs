using CoreLocation;
using Foundation;
using MapKit;
using TMapViews.Models;

namespace TMapViews.iOS.Models
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
            SetBindingCoordinate(anno.Location.ToCLLocationCoordinate2D());
        }

        public override CLLocationCoordinate2D Coordinate => Annotation.Location.ToCLLocationCoordinate2D();

        public void SetTitle(string title) => _title = title;

        public void SetSubtitle(string subtitle) => _subtitle = subtitle;

        [Export("_original_setCoordinate:")]
        public void SetBindingCoordinate(CLLocationCoordinate2D value)
            => Annotation.Location = value.ToBinding2DLocation();

        public override void SetCoordinate(CLLocationCoordinate2D value)
            => SetBindingCoordinate(value);
    }
}