using TMapViews.Models;

namespace TMapViews.iOS.Models
{
    public interface IBindingMKMapAnnotation
    {
        IBindingMapAnnotation Annotation { get; set; }
    }
}