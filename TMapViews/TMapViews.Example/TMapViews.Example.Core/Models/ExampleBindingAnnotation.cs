using TMapViews.Models.Interfaces;
using TMapViews.Models.Models;

namespace TMapViews.Example.Core.Models
{
    public class ExampleBindingAnnotation : IBindingMapAnnotation
    {
        Binding2DLocation location;
        public Binding2DLocation Location { get => location; set => location = value as Binding2DLocation; }


        public int Id { get; set; }
    }
}
