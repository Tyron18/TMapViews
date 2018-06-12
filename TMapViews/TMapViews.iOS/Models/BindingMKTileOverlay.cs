using MapKit;
using TMapViews.Models;

namespace TMapViews.iOS.Models
{
    public class BindingMKTileOverlay : MKTileOverlay, IBindingMKMapOverlay
    {
        public IBindingMapAnnotation Annotation { get; set; }
        public MKOverlayRenderer Renderer { get; set; }

        public BindingMKTileOverlay(string URLTemplate) : base(URLTemplate)
        {
        }
    }
}