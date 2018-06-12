using MapKit;

namespace TMapViews.iOS.Models
{
    public interface IBindingMKMapOverlay : IMKOverlay, IBindingMKMapAnnotation
    {
        MKOverlayRenderer Renderer { get; set; }
    }
}