using TMapViews.Models.Interfaces;

namespace TMapViews.Example.Core.Models
{
    public class ExampleBindingOverlay : ExampleBindingAnnotation, IBindingMapOverlay
    {
        public double Radius { get; set; }
    }
}
