using TMapViews.Models;
using TMapViews.Models.Interfaces;
using TMapViews.Models.Models;

namespace TMapViews.Example.Core.Models
{
    public class ExampleBindingAnnotation : IBindingMapAnnotation
    {
        I2DLocation location;
        public I2DLocation Location { get => location; set => location = value; }


        public int Id { get; set; }
    }
}
