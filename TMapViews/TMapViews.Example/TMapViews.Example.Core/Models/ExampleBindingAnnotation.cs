using TMapViews.Models;

namespace TMapViews.Example.Core.Models
{
    public class ExampleBindingAnnotation : IBindingMapAnnotation
    {
        private I2DLocation location;
        public I2DLocation Location { get => location; set => location = value; }

        public int Id { get; set; }
    }
}